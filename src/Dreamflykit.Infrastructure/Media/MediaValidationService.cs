using Dreamflykit.Application.Media.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace Dreamflykit.Infrastructure.Media;

/// <summary>
/// Validates uploads (MIME type, magic bytes, size, dimensions)
/// and generates WebP-converted image variants.
/// </summary>
public sealed class MediaValidationService
{
    private readonly MediaOptions _options;
    private readonly ILogger<MediaValidationService> _logger;

    // Magic bytes for allowed image formats
    private static readonly byte[] JpegMagic = [0xFF, 0xD8, 0xFF];
    private static readonly byte[] PngMagic = [0x89, 0x50, 0x4E, 0x47];
    private static readonly byte[] WebpMagic = [0x52, 0x49, 0x46, 0x46]; // RIFF

    public MediaValidationService(
        IOptions<MediaOptions> options,
        ILogger<MediaValidationService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    /// <summary>
    /// Validates the file stream. Returns an error message or null if valid.
    /// </summary>
    public async Task<string?> ValidateAsync(
        Stream stream,
        string contentType,
        string originalFileName,
        CancellationToken cancellationToken = default)
    {
        // 1. File size
        if (stream.Length > _options.MaxFileSizeBytes)
        {
            return $"File exceeds maximum size of {_options.MaxFileSizeBytes / 1024 / 1024} MB.";
        }

        // 2. Declared content type
        if (!_options.AllowedContentTypes.Contains(contentType.ToLowerInvariant()))
        {
            return $"Content type '{contentType}' is not allowed. " +
                   $"Allowed: {string.Join(", ", _options.AllowedContentTypes)}";
        }

        // 3. File extension
        var ext = Path.GetExtension(originalFileName).ToLowerInvariant();
        if (!_options.AllowedExtensions.Contains(ext))
        {
            return $"File extension '{ext}' is not allowed. " +
                   $"Allowed: {string.Join(", ", _options.AllowedExtensions)}";
        }

        // 4. Magic bytes (actual format verification, not just declared MIME)
        stream.Position = 0;
        var headerBuffer = new byte[8];
        var bytesRead = await stream.ReadAsync(headerBuffer.AsMemory(0, 8), cancellationToken);

        if (!IsValidImageFormat(headerBuffer, bytesRead))
        {
            return "File content does not match a valid image format (JPEG, PNG, or WebP).";
        }

        // 5. Dimensions
        stream.Position = 0;

        try
        {
            var imageInfo = await Image.IdentifyAsync(stream, cancellationToken);

            if (imageInfo.Width < _options.MinWidth || imageInfo.Height < _options.MinHeight)
            {
                return $"Image dimensions ({imageInfo.Width}x{imageInfo.Height}) are below " +
                       $"minimum ({_options.MinWidth}x{_options.MinHeight}).";
            }

            if (imageInfo.Width > _options.MaxWidth || imageInfo.Height > _options.MaxHeight)
            {
                return $"Image dimensions ({imageInfo.Width}x{imageInfo.Height}) exceed " +
                       $"maximum ({_options.MaxWidth}x{_options.MaxHeight}).";
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not identify image dimensions for {FileName}", originalFileName);
            return "Could not read image dimensions. The file may be corrupted.";
        }
        finally
        {
            stream.Position = 0;
        }

        return null; // null = valid
    }

    /// <summary>
    /// Converts the source image to WebP and resizes to the specified max dimension.
    /// Returns a new MemoryStream with the WebP bytes. Caller must dispose.
    /// </summary>
    public async Task<(MemoryStream Stream, int Width, int Height)> ProcessVariantAsync(
        Stream sourceStream,
        int maxDimension,
        CancellationToken cancellationToken = default)
    {
        sourceStream.Position = 0;

        using var image = await Image.LoadAsync(sourceStream, cancellationToken);

        // Resize maintaining aspect ratio
        if (image.Width > maxDimension || image.Height > maxDimension)
        {
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(maxDimension, maxDimension),
                Mode = ResizeMode.Max,
                Sampler = KnownResamplers.Lanczos3
            }));
        }

        var outputStream = new MemoryStream();

        await image.SaveAsync(outputStream, new WebpEncoder
        {
            Quality = 82,
            FileFormat = WebpFileFormatType.Lossy
        }, cancellationToken);

        outputStream.Position = 0;

        return (outputStream, image.Width, image.Height);
    }

    private static bool IsValidImageFormat(byte[] header, int bytesRead)
    {
        if (bytesRead < 4)
        {
            return false;
        }

        // JPEG
        if (StartsWith(header, JpegMagic))
        {
            return true;
        }

        // PNG
        if (StartsWith(header, PngMagic))
        {
            return true;
        }

        // WebP (RIFF....WEBP)
        if (StartsWith(header, WebpMagic) && bytesRead >= 8)
        {
            return true;
        }

        return false;
    }

    private static bool StartsWith(byte[] data, byte[] prefix)
    {
        if (data.Length < prefix.Length) return false;
        for (int i = 0; i < prefix.Length; i++)
        {
            if (data[i] != prefix[i]) return false;
        }
        return true;
    }
}
