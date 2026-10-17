using Dreamflykit.Application.Media.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

/// <summary>
/// High-performance media delivery controller that securely serves and caches
/// assets stored in private AWS S3 buckets to guest visitors, logged-in customers,
/// and admin preview portals without exposing raw S3 credentials or returning 403 Forbidden.
/// </summary>
[AllowAnonymous]
public sealed class MediaDeliveryController : Controller
{
    private readonly IMediaStorageService _storage;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<MediaDeliveryController> _logger;

    public MediaDeliveryController(
        IMediaStorageService storage,
        IWebHostEnvironment environment,
        ILogger<MediaDeliveryController> logger)
    {
        _storage = storage;
        _environment = environment;
        _logger = logger;
    }

    [HttpGet("media/{*path}")]
    [ResponseCache(Duration = 2592000, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> GetMedia(string? path, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return NotFound();
        }

        // Normalize object key (all assets stored under media/ in S3)
        var cleanPath = path.Trim().TrimStart('/');
        var objectKey = cleanPath.StartsWith("media/", StringComparison.OrdinalIgnoreCase)
            ? cleanPath
            : $"media/{cleanPath}";

        var cacheRoot = Path.Combine(_environment.ContentRootPath, "App_Data", "MediaCache");
        var safeRelativePath = objectKey.Replace('/', Path.DirectorySeparatorChar);
        var cachedFilePath = Path.Combine(cacheRoot, safeRelativePath);

        var contentType = GetContentType(objectKey);

        // 1. Check local disk cache for instant zero-latency serving
        if (System.IO.File.Exists(cachedFilePath))
        {
            Response.Headers.CacheControl = "public, max-age=2592000, immutable";
            return PhysicalFile(cachedFilePath, contentType);
        }

        // 1b. Check local disk cache for un-suffixed base file if a variant was requested
        string? fallbackKey = null;
        if (objectKey.Contains("_thumb.") || objectKey.Contains("_card.") || objectKey.Contains("_detail."))
        {
            fallbackKey = objectKey
                .Replace("_thumb.", ".")
                .Replace("_card.", ".")
                .Replace("_detail.", ".");
            var fallbackCachedFilePath = Path.Combine(cacheRoot, fallbackKey.Replace('/', Path.DirectorySeparatorChar));
            if (System.IO.File.Exists(fallbackCachedFilePath))
            {
                Response.Headers.CacheControl = "public, max-age=2592000, immutable";
                return PhysicalFile(fallbackCachedFilePath, contentType);
            }
        }

        // 1c. Check local disk cache for plural/singular alternate path (media/product/ vs media/products/)
        string? altKey = null;
        if (objectKey.StartsWith("media/products/", StringComparison.OrdinalIgnoreCase))
        {
            altKey = "media/product/" + objectKey["media/products/".Length..];
        }
        else if (objectKey.StartsWith("media/product/", StringComparison.OrdinalIgnoreCase))
        {
            altKey = "media/products/" + objectKey["media/product/".Length..];
        }
        else if (objectKey.StartsWith("media/categories/", StringComparison.OrdinalIgnoreCase))
        {
            altKey = "media/category/" + objectKey["media/categories/".Length..];
        }
        else if (objectKey.StartsWith("media/category/", StringComparison.OrdinalIgnoreCase))
        {
            altKey = "media/categories/" + objectKey["media/category/".Length..];
        }

        if (altKey != null)
        {
            var altCachedFilePath = Path.Combine(cacheRoot, altKey.Replace('/', Path.DirectorySeparatorChar));
            if (System.IO.File.Exists(altCachedFilePath))
            {
                Response.Headers.CacheControl = "public, max-age=2592000, immutable";
                return PhysicalFile(altCachedFilePath, contentType);
            }
        }

        // 2. Fetch from backend storage service (checks S3 or persistent storage)
        var s3Object = await _storage.GetObjectAsync(objectKey, cancellationToken);

        // Fallback: if variant was requested but doesn't exist in storage, try original key
        if (s3Object is null && fallbackKey != null)
        {
            s3Object = await _storage.GetObjectAsync(fallbackKey, cancellationToken);
        }

        // Fallback: try alternate plural/singular key
        if (s3Object is null && altKey != null)
        {
            s3Object = await _storage.GetObjectAsync(altKey, cancellationToken);
        }

        if (s3Object is null)
        {
            _logger.LogWarning("Media object not found in storage: {ObjectKey}", objectKey);
            return NotFound();
        }

        await using (s3Object)
        {
            try
            {
                var directory = Path.GetDirectoryName(cachedFilePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Write atomically to cache
                var tempFile = Path.Combine(directory!, $"{Guid.NewGuid():N}.tmp");
                await using (var fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write, FileShare.None, 81920, useAsync: true))
                {
                    await s3Object.Stream.CopyToAsync(fileStream, cancellationToken);
                }

                if (!System.IO.File.Exists(cachedFilePath))
                {
                    System.IO.File.Move(tempFile, cachedFilePath, overwrite: true);
                }
                else
                {
                    System.IO.File.Delete(tempFile);
                }

                Response.Headers.CacheControl = "public, max-age=2592000, immutable";
                return PhysicalFile(cachedFilePath, contentType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to cache media object {ObjectKey}. Streaming directly.", objectKey);

                // If disk cache fails, stream directly in memory
                var memoryStream = new MemoryStream();
                await s3Object.Stream.CopyToAsync(memoryStream, cancellationToken);
                memoryStream.Position = 0;
                Response.Headers.CacheControl = "public, max-age=2592000, immutable";
                return File(memoryStream, s3Object.ContentType ?? contentType);
            }
        }
    }

    private static string GetContentType(string path)
    {
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return ext switch
        {
            ".webp" => "image/webp",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".svg" => "image/svg+xml",
            ".ico" => "image/x-icon",
            _ => "application/octet-stream"
        };
    }
}
