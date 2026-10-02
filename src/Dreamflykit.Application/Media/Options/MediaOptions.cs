namespace Dreamflykit.Application.Media.Options;

/// <summary>
/// Media management configuration options.
/// All values here are non-sensitive and can be set in appsettings.json.
/// </summary>
public sealed class MediaOptions
{
    public const string SectionName = "Media";

    /// <summary>
    /// Base URL of the CDN (CloudFront).
    /// Example: https://d1234abcd.cloudfront.net or https://cdn.yourdomain.com
    /// Used by MediaUrlService to construct customer-facing image URLs.
    /// </summary>
    public string CdnBaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// S3 bucket name — should match S3Options.BucketName.
    /// </summary>
    public string BucketName { get; set; } = string.Empty;

    /// <summary>
    /// AWS region (e.g., ap-south-1).
    /// </summary>
    public string AwsRegion { get; set; } = string.Empty;

    /// <summary>
    /// Maximum upload file size in bytes. Default: 5 MB.
    /// </summary>
    public long MaxFileSizeBytes { get; set; } = 5 * 1024 * 1024;

    /// <summary>
    /// Allowed MIME content types for uploads.
    /// </summary>
    public string[] AllowedContentTypes { get; set; } =
        ["image/jpeg", "image/png", "image/webp"];

    /// <summary>
    /// Allowed file extensions for uploads.
    /// </summary>
    public string[] AllowedExtensions { get; set; } =
        [".jpg", ".jpeg", ".png", ".webp"];

    /// <summary>Minimum image width in pixels.</summary>
    public int MinWidth { get; set; } = 100;

    /// <summary>Minimum image height in pixels.</summary>
    public int MinHeight { get; set; } = 100;

    /// <summary>Maximum image width in pixels.</summary>
    public int MaxWidth { get; set; } = 8000;

    /// <summary>Maximum image height in pixels.</summary>
    public int MaxHeight { get; set; } = 8000;

    /// <summary>
    /// How long a pre-signed upload URL is valid in seconds. Default: 5 minutes.
    /// Keep this short — it should only cover the time to complete a single upload.
    /// </summary>
    public int PresignedUrlExpirySeconds { get; set; } = 300;

    /// <summary>Image variant maximum dimensions.</summary>
    public MediaVariantOptions Variants { get; set; } = new();
}

public sealed class MediaVariantOptions
{
    /// <summary>Max width/height for thumbnail variant.</summary>
    public int Thumbnail { get; set; } = 200;

    /// <summary>Max width/height for card variant (used in product listings).</summary>
    public int Card { get; set; } = 500;

    /// <summary>Max width/height for detail variant (used in product detail).</summary>
    public int Detail { get; set; } = 1200;
}
