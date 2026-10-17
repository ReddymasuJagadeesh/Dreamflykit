using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Options;
using Microsoft.Extensions.Options;

namespace Dreamflykit.Infrastructure.Media;

/// <summary>
/// Constructs CDN URLs from S3 object keys.
/// This is the ONLY place in the codebase that builds image URLs for customer delivery.
/// 
/// Architecture: Customer -> CloudFront CDN -> S3 (private)
/// The CDN domain is configured in MediaOptions:CdnBaseUrl.
/// No raw S3 URLs are ever served to customers.
/// </summary>
public sealed class MediaUrlService : IMediaUrlService
{
    private readonly string _cdnBaseUrl;

    public MediaUrlService(IOptions<MediaOptions> options)
    {
        var mediaOpts = options.Value;
        var url = mediaOpts.CdnBaseUrl?.TrimEnd('/');

        // If CdnBaseUrl is not configured, contains placeholders, or directly points to a raw S3 bucket endpoint
        // (which is private and returns 403 Forbidden to anonymous public requests without AWS credentials):
        if (string.IsNullOrWhiteSpace(url) ||
            url.Contains("YOUR_CLOUDFRONT_DOMAIN", StringComparison.OrdinalIgnoreCase) ||
            url.Contains("your-cdn-id", StringComparison.OrdinalIgnoreCase) ||
            url.Contains("your_cdn_id", StringComparison.OrdinalIgnoreCase) ||
            url.Contains("your-domain", StringComparison.OrdinalIgnoreCase) ||
            url.Contains("placeholder", StringComparison.OrdinalIgnoreCase) ||
            url.Contains("example.com", StringComparison.OrdinalIgnoreCase) ||
            url.Contains(".s3.", StringComparison.OrdinalIgnoreCase) ||
            url.Contains("s3.amazonaws.com", StringComparison.OrdinalIgnoreCase))
        {
            // Route through the web application's secure media delivery endpoint (/media/...)
            _cdnBaseUrl = string.Empty;
        }
        else
        {
            _cdnBaseUrl = url;
        }
    }

    /// <summary>
    /// Builds the CDN or application URL for an original/full-size image.
    /// </summary>
    /// <param name="objectKey">S3 object key (e.g., media/products/{id}/{assetId}.webp)</param>
    public string GetUrl(string objectKey)
    {
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            return "/images/products/product-placeholder.webp";
        }

        var cleanKey = objectKey.TrimStart('/');
        return string.IsNullOrEmpty(_cdnBaseUrl)
            ? $"/{cleanKey}"
            : $"{_cdnBaseUrl}/{cleanKey}";
    }

    /// <summary>
    /// Builds the CDN URL for the thumbnail variant (200px max dimension).
    /// Convention: object key has a _thumb suffix before the extension.
    /// </summary>
    public string GetThumbnailUrl(string objectKey)
    {
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            return "/images/products/product-placeholder.webp";
        }
        if (objectKey.Contains("_thumb.") || objectKey.Contains("_card.") || objectKey.Contains("_detail."))
        {
            return GetUrl(objectKey);
        }
        return GetVariantUrl(objectKey, "thumb");
    }

    /// <summary>
    /// Builds the CDN URL for the card variant (500px max dimension).
    /// Used in product listing grids and category pages.
    /// </summary>
    public string GetCardUrl(string objectKey)
    {
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            return "/images/products/product-placeholder.webp";
        }
        if (objectKey.Contains("_card.") || objectKey.Contains("_thumb.") || objectKey.Contains("_detail."))
        {
            return GetUrl(objectKey);
        }
        return GetVariantUrl(objectKey, "card");
    }

    /// <summary>
    /// Builds the CDN URL for the detail variant (1200px max dimension).
    /// Used in product detail pages and full-size previews.
    /// </summary>
    public string GetDetailUrl(string objectKey)
    {
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            return "/images/products/product-placeholder.webp";
        }
        if (objectKey.Contains("_detail.") || objectKey.Contains("_card.") || objectKey.Contains("_thumb."))
        {
            return GetUrl(objectKey);
        }
        return GetVariantUrl(objectKey, "detail");
    }

    private string GetVariantUrl(string objectKey, string variantSuffix)
    {
        // Insert _variant before the file extension
        // Input:  media/products/{id}/{assetId}.webp
        // Output: media/products/{id}/{assetId}_thumb.webp
        var lastDotIndex = objectKey.LastIndexOf('.');

        if (lastDotIndex < 0)
        {
            return GetUrl($"{objectKey}_{variantSuffix}");
        }

        var keyWithoutExt = objectKey[..lastDotIndex];
        var ext = objectKey[lastDotIndex..];

        return GetUrl($"{keyWithoutExt}_{variantSuffix}{ext}");
    }

    /// <summary>
    /// Constructs the S3 object key for a given media asset.
    /// Format: media/{type_lower}/{ownerGuid}/{mediaAssetGuid}.webp
    /// All images are stored as WebP after processing.
    /// </summary>
    public static string BuildObjectKey(
        string mediaType,
        Guid ownerId,
        Guid mediaAssetId)
    {
        return $"media/{mediaType.ToLowerInvariant()}/{ownerId:N}/{mediaAssetId:N}.webp";
    }

    /// <summary>
    /// Builds the object key for a specific image variant.
    /// </summary>
    public static string BuildVariantObjectKey(
        string mediaType,
        Guid ownerId,
        Guid mediaAssetId,
        string variant)
    {
        return $"media/{mediaType.ToLowerInvariant()}/{ownerId:N}/{mediaAssetId:N}_{variant}.webp";
    }
}
