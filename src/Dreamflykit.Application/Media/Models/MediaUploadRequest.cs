using Dreamflykit.Domain.Media;

namespace Dreamflykit.Application.Media.Models;

/// <summary>
/// Request model for uploading a media file.
/// Passed from the controller to MediaService after the file is received.
/// </summary>
public sealed class MediaUploadRequest
{
    /// <summary>The raw file bytes from the multipart upload.</summary>
    public required Stream FileStream { get; init; }

    /// <summary>Original filename from the upload (for audit only — not used as storage key).</summary>
    public required string OriginalFileName { get; init; }

    /// <summary>MIME type declared by the browser.</summary>
    public required string ContentType { get; init; }

    /// <summary>The category of media being uploaded (Product, Banner, etc.).</summary>
    public required MediaType MediaType { get; init; }

    /// <summary>
    /// The ID of the resource that will own this media (e.g., ProductId, BannerId).
    /// Used to construct the S3 object key path.
    /// </summary>
    public required Guid OwnerId { get; init; }

    /// <summary>Alt text for accessibility.</summary>
    public required string AltText { get; init; }

    /// <summary>Optional caption for the image.</summary>
    public string? Caption { get; init; }

    /// <summary>Optional display title.</summary>
    public string? Title { get; init; }

    /// <summary>The user performing the upload (for audit).</summary>
    public required Guid UploadedBy { get; init; }
}

/// <summary>
/// Result of a media upload operation.
/// </summary>
public sealed class MediaUploadResult
{
    public bool IsSuccess { get; init; }

    public MediaAssetDto? Asset { get; init; }

    public string? ErrorMessage { get; init; }

    public static MediaUploadResult Success(MediaAssetDto asset) =>
        new() { IsSuccess = true, Asset = asset };

    public static MediaUploadResult Failure(string error) =>
        new() { IsSuccess = false, ErrorMessage = error };
}
