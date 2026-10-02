using Dreamflykit.Application.Common;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Domain.Media;

namespace Dreamflykit.Application.Media.Interfaces;

/// <summary>
/// Defines all media management operations.
/// Controllers depend only on this interface — never on the implementation directly.
/// </summary>
public interface IMediaService
{
    // ---------------------------------------------------------------
    // Upload
    // ---------------------------------------------------------------

    /// <summary>
    /// Validates, processes, and uploads a media file to S3.
    /// Creates a MediaAsset record in the database.
    /// </summary>
    Task<MediaUploadResult> UploadAsync(
        MediaUploadRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates a pre-signed S3 URL for direct browser-to-S3 upload.
    /// Creates a Draft MediaAsset record that must be confirmed after upload completes.
    /// </summary>
    Task<PresignedUploadUrlResult> GeneratePresignedUploadUrlAsync(
        PresignedUploadUrlRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Called after a direct browser-to-S3 PUT completes.
    /// Verifies the object exists in S3, reads metadata, and marks the asset as Uploaded.
    /// </summary>
    Task<MediaUploadResult> ConfirmUploadAsync(
        Guid mediaAssetId,
        Guid confirmedBy,
        CancellationToken cancellationToken = default);

    // ---------------------------------------------------------------
    // Read
    // ---------------------------------------------------------------

    Task<MediaAssetDto?> GetByIdAsync(
        Guid mediaAssetId,
        CancellationToken cancellationToken = default);

    Task<PagedResult<MediaAssetDto>> GetLibraryAsync(
        MediaLibraryQuery query,
        CancellationToken cancellationToken = default);

    // ---------------------------------------------------------------
    // Update
    // ---------------------------------------------------------------

    Task<bool> UpdateMetadataAsync(
        Guid mediaAssetId,
        string altText,
        string? caption,
        string? title,
        Guid updatedBy,
        CancellationToken cancellationToken = default);

    // ---------------------------------------------------------------
    // Lifecycle
    // ---------------------------------------------------------------

    Task<bool> PublishAsync(
        Guid mediaAssetId,
        Guid publishedBy,
        CancellationToken cancellationToken = default);

    Task<bool> UnpublishAsync(
        Guid mediaAssetId,
        Guid unpublishedBy,
        CancellationToken cancellationToken = default);

    Task<bool> ArchiveAsync(
        Guid mediaAssetId,
        Guid archivedBy,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft deletes the DB record and schedules the S3 object for physical deletion.
    /// </summary>
    Task<bool> DeleteAsync(
        Guid mediaAssetId,
        Guid deletedBy,
        CancellationToken cancellationToken = default);

    // ---------------------------------------------------------------
    // Product Media
    // ---------------------------------------------------------------

    /// <summary>
    /// Assigns an existing MediaAsset to a product (creates ProductMedia record).
    /// </summary>
    Task<bool> AssignToProductAsync(
        Guid productId,
        Guid mediaAssetId,
        bool isPrimary,
        int displayOrder,
        Guid assignedBy,
        CancellationToken cancellationToken = default);

    Task<bool> SetProductMediaOrderAsync(
        Guid productId,
        IReadOnlyList<(Guid MediaId, int Order)> orderedMedia,
        Guid updatedBy,
        CancellationToken cancellationToken = default);

    Task<bool> SetProductPrimaryMediaAsync(
        Guid productId,
        Guid mediaId,
        Guid updatedBy,
        CancellationToken cancellationToken = default);

    Task<bool> RemoveFromProductAsync(
        Guid productMediaId,
        Guid removedBy,
        CancellationToken cancellationToken = default);
}
