using Dreamflykit.Domain.Media;

namespace Dreamflykit.Application.Media.Models;

/// <summary>
/// Query parameters for the Admin Media Library.
/// </summary>
public sealed class MediaLibraryQuery
{
    public MediaType? MediaType { get; init; }

    public MediaAssetStatus? Status { get; init; }

    /// <summary>Filter by the user who uploaded the asset.</summary>
    public Guid? UploadedBy { get; init; }

    /// <summary>Date filter — assets created on or after this date.</summary>
    public DateTime? CreatedFrom { get; init; }

    /// <summary>Date filter — assets created on or before this date.</summary>
    public DateTime? CreatedTo { get; init; }

    /// <summary>Full-text search across AltText, Title, Caption, OriginalFileName.</summary>
    public string? Search { get; init; }

    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 48;

    public string SortBy { get; init; } = "CreatedAtUtc";

    public bool SortDescending { get; init; } = true;
}

/// <summary>
/// Request model for generating a pre-signed S3 upload URL.
/// </summary>
public sealed class PresignedUploadUrlRequest
{
    public required MediaType MediaType { get; init; }

    /// <summary>Owner resource ID (e.g., ProductId, BannerId) for key construction.</summary>
    public required Guid OwnerId { get; init; }

    /// <summary>MIME type of the file to be uploaded. Must be in allowed list.</summary>
    public required string ContentType { get; init; }

    /// <summary>Original filename (for audit only).</summary>
    public required string OriginalFileName { get; init; }
}

/// <summary>
/// Result of a pre-signed URL generation request.
/// </summary>
public sealed class PresignedUploadUrlResult
{
    /// <summary>
    /// The temporary pre-signed S3 URL the browser should PUT the file to directly.
    /// Expires after MediaOptions.PresignedUrlExpirySeconds.
    /// </summary>
    public string UploadUrl { get; init; } = string.Empty;

    /// <summary>
    /// The S3 object key that will be created. Return this to the client
    /// so they can call ConfirmUpload after PUT completes.
    /// </summary>
    public string ObjectKey { get; init; } = string.Empty;

    /// <summary>
    /// Temporary media asset ID. Client must call ConfirmUpload with this ID
    /// after the direct PUT completes.
    /// </summary>
    public Guid MediaAssetId { get; init; }

    /// <summary>When this URL expires.</summary>
    public DateTime ExpiresAtUtc { get; init; }
}
