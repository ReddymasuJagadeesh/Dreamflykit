namespace Dreamflykit.Application.Media.Models;

/// <summary>
/// Result returned after a media asset is uploaded and processed.
/// </summary>
public sealed class MediaAssetDto
{
    public Guid Id { get; init; }

    public string MediaType { get; init; } = string.Empty;

    public string ObjectKey { get; init; } = string.Empty;

    /// <summary>
    /// CDN URL for the original uploaded image.
    /// Built by MediaUrlService — never a raw S3 URL.
    /// </summary>
    public string Url { get; init; } = string.Empty;

    /// <summary>CDN URL for the 200px thumbnail variant.</summary>
    public string ThumbnailUrl { get; init; } = string.Empty;

    /// <summary>CDN URL for the 500px card variant.</summary>
    public string CardUrl { get; init; } = string.Empty;

    public string OriginalFileName { get; init; } = string.Empty;

    public string ContentType { get; init; } = string.Empty;

    public long FileSize { get; init; }

    public int Width { get; init; }

    public int Height { get; init; }

    public string AltText { get; init; } = string.Empty;

    public string? Caption { get; init; }

    public string? Title { get; init; }

    public string Status { get; init; } = string.Empty;

    public Guid UploadedBy { get; init; }

    public DateTime CreatedAtUtc { get; init; }

    public DateTime? UpdatedAtUtc { get; init; }

    /// <summary>
    /// Resources that currently reference this asset.
    /// Used in Media Library to show usage and prevent accidental deletion.
    /// </summary>
    public IReadOnlyList<MediaAssetUsageDto> UsedBy { get; init; }
        = Array.Empty<MediaAssetUsageDto>();
}

public sealed class MediaAssetUsageDto
{
    public string ResourceType { get; init; } = string.Empty;

    public Guid ResourceId { get; init; }

    public string ResourceName { get; init; } = string.Empty;
}
