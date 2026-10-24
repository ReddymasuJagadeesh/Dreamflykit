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
        // 1. File size (allow up to at least 25 MB for high-resolution cameras)
        var maxAllowedBytes = Math.Max(_options.MaxFileSizeBytes, 25 * 1024 * 1024);
        if (stream.Length > maxAllowedBytes)
        {
            return $"File exceeds maximum size of {maxAllowedBytes / 1024 / 1024} MB.";
        }

        // 2. Declared content type normalization (handle browsers sending application/octet-stream for webp/jpg)
        var ext = Path.GetExtension(originalFileName).ToLowerInvariant();
        var normalizedContentType = contentType?.ToLowerInvariant()?.Trim();
        if (string.IsNullOrWhiteSpace(normalizedContentType) ||
            normalizedContentType == "application/octet-stream" ||
            normalizedContentType == "binary/octet-stream")
        {
            normalizedContentType = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".webp" => "image/webp",
                _ => normalizedContentType
            };
        }

        if (normalizedContentType == "image/pjpeg") normalizedContentType = "image/jpeg";
        if (normalizedContentType == "image/x-png") normalizedContentType = "image/png";

        if (string.IsNullOrWhiteSpace(normalizedContentType) ||
            !_options.AllowedContentTypes.Contains(normalizedContentType))
        {
            return $"Content type '{contentType}' is not allowed. " +
                   $"Allowed: {string.Join(", ", _options.AllowedContentTypes)}";
        }

        // 3. File extension
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
    /// High-performance multi-variant generation that decodes the source image ONCE into memory,
    /// cascades resizing (Original -> Detail -> Card -> Thumb) with high-efficiency Bicubic resampling,
    /// and encodes all variants to WebP. Disposing GeneratedVariants disposes all streams.
    /// </summary>
    public async Task<GeneratedVariants> GenerateAllVariantsAsync(
        Stream sourceStream,
        int detailMax,
        int cardMax,
        int thumbMax,
        CancellationToken cancellationToken = default)
    {
        sourceStream.Position = 0;
        using var originalImage = await Image.LoadAsync(sourceStream, cancellationToken);
        var origWidth = originalImage.Width;
        var origHeight = originalImage.Height;

        var webpEncoder = new WebpEncoder
        {
            Quality = 80,
            FileFormat = WebpFileFormatType.Lossy
        };

        // 1. Detail variant (up to detailMax)
        using var detailImage = originalImage.Clone(ctx =>
        {
            if (originalImage.Width > detailMax || originalImage.Height > detailMax)
            {
                ctx.Resize(new ResizeOptions
                {
                    Size = new Size(detailMax, detailMax),
                    Mode = ResizeMode.Max,
                    Sampler = KnownResamplers.Bicubic
                });
            }
        });

        var detailStream = new MemoryStream();
        await detailImage.SaveAsync(detailStream, webpEncoder, cancellationToken);
        detailStream.Position = 0;

        // 2. Card variant (cascaded down from detailImage - already <= detailMax)
        using var cardImage = detailImage.Clone(ctx =>
        {
            if (detailImage.Width > cardMax || detailImage.Height > cardMax)
            {
                ctx.Resize(new ResizeOptions
                {
                    Size = new Size(cardMax, cardMax),
                    Mode = ResizeMode.Max,
                    Sampler = KnownResamplers.Bicubic
                });
            }
        });

        var cardStream = new MemoryStream();
        await cardImage.SaveAsync(cardStream, webpEncoder, cancellationToken);
        cardStream.Position = 0;

        // 3. Thumb variant (cascaded down from cardImage - already <= cardMax)
        using var thumbImage = cardImage.Clone(ctx =>
        {
            if (cardImage.Width > thumbMax || cardImage.Height > thumbMax)
            {
                ctx.Resize(new ResizeOptions
                {
                    Size = new Size(thumbMax, thumbMax),
                    Mode = ResizeMode.Max,
                    Sampler = KnownResamplers.Bicubic
                });
            }
        });

        var thumbStream = new MemoryStream();
        await thumbImage.SaveAsync(thumbStream, webpEncoder, cancellationToken);
        thumbStream.Position = 0;

        return new GeneratedVariants
        {
            DetailStream = detailStream,
            CardStream = cardStream,
            ThumbStream = thumbStream,
            Width = origWidth,
            Height = origHeight
        };
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

        // Resize maintaining aspect ratio with fast Bicubic resampler
        if (image.Width > maxDimension || image.Height > maxDimension)
        {
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(maxDimension, maxDimension),
                Mode = ResizeMode.Max,
                Sampler = KnownResamplers.Bicubic
            }));
        }

        var outputStream = new MemoryStream();

        await image.SaveAsync(outputStream, new WebpEncoder
        {
            Quality = 80,
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

public sealed class GeneratedVariants : IAsyncDisposable, IDisposable
{
    public required MemoryStream DetailStream { get; init; }
    public required MemoryStream CardStream { get; init; }
    public required MemoryStream ThumbStream { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }

    public void Dispose()
    {
        DetailStream.Dispose();
        CardStream.Dispose();
        ThumbStream.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await DetailStream.DisposeAsync();
        await CardStream.DisposeAsync();
        await ThumbStream.DisposeAsync();
    }
}
