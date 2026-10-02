using Dreamflykit.Application.Common;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Application.Media.Options;
using Dreamflykit.Domain.Catalog;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dreamflykit.Infrastructure.Media;

/// <summary>
/// Core media management service. Orchestrates:
///   1. Validation (MediaValidationService)
///   2. Image processing + variant generation
///   3. S3 storage (IMediaStorageService)
///   4. Database persistence (DreamflykitDbContext)
///   5. CDN URL construction (IMediaUrlService)
///
/// All operations are auditable through MediaAuditLog.
/// </summary>
public sealed class MediaService : IMediaService
{
    private readonly DreamflykitDbContext _db;
    private readonly IMediaStorageService _storage;
    private readonly MediaValidationService _validator;
    private readonly IMediaUrlService _urlService;
    private readonly MediaOptions _options;
    private readonly ILogger<MediaService> _logger;

    public MediaService(
        DreamflykitDbContext db,
        IMediaStorageService storage,
        MediaValidationService validator,
        IMediaUrlService urlService,
        IOptions<MediaOptions> options,
        ILogger<MediaService> logger)
    {
        _db = db;
        _storage = storage;
        _validator = validator;
        _urlService = urlService;
        _options = options.Value;
        _logger = logger;
    }

    // ---------------------------------------------------------------
    // UPLOAD
    // ---------------------------------------------------------------

    public async Task<MediaUploadResult> UploadAsync(
        MediaUploadRequest request,
        CancellationToken cancellationToken = default)
    {
        // 1. Validate
        var validationError = await _validator.ValidateAsync(
            request.FileStream,
            request.ContentType,
            request.OriginalFileName,
            cancellationToken);

        if (validationError is not null)
        {
            return MediaUploadResult.Failure(validationError);
        }

        // 2. Generate new asset ID and S3 object key
        var mediaAssetId = Guid.NewGuid();
        var mediaTypeName = request.MediaType.ToString();
        var objectKey = MediaUrlService.BuildObjectKey(mediaTypeName, request.OwnerId, mediaAssetId);

        // 3. Process original image to WebP (detail variant)
        var (processedStream, width, height) = await _validator.ProcessVariantAsync(
            request.FileStream,
            _options.Variants.Detail,
            cancellationToken);

        // 4. Upload original (detail) to S3
        var uploadSuccess = await _storage.PutObjectAsync(
            objectKey,
            processedStream,
            "image/webp",
            cancellationToken);

        await processedStream.DisposeAsync();

        if (!uploadSuccess)
        {
            return MediaUploadResult.Failure("Failed to upload file to storage. Please try again.");
        }

        // 5. Generate and upload thumbnail variant
        request.FileStream.Position = 0;
        var (thumbStream, _, _) = await _validator.ProcessVariantAsync(
            request.FileStream,
            _options.Variants.Thumbnail,
            cancellationToken);

        var thumbKey = MediaUrlService.BuildVariantObjectKey(mediaTypeName, request.OwnerId, mediaAssetId, "thumb");
        await _storage.PutObjectAsync(thumbKey, thumbStream, "image/webp", cancellationToken);
        await thumbStream.DisposeAsync();

        // 6. Generate and upload card variant
        request.FileStream.Position = 0;
        var (cardStream, _, _) = await _validator.ProcessVariantAsync(
            request.FileStream,
            _options.Variants.Card,
            cancellationToken);

        var cardKey = MediaUrlService.BuildVariantObjectKey(mediaTypeName, request.OwnerId, mediaAssetId, "card");
        await _storage.PutObjectAsync(cardKey, cardStream, "image/webp", cancellationToken);
        await cardStream.DisposeAsync();

        // 7. Persist MediaAsset to database
        var asset = new MediaAsset(
            mediaType: request.MediaType,
            objectKey: objectKey,
            originalFileName: request.OriginalFileName,
            contentType: "image/webp",
            fileExtension: ".webp",
            fileSize: request.FileStream.Length,
            width: width,
            height: height,
            altText: request.AltText,
            uploadedBy: request.UploadedBy);

        _db.MediaAssets.Add(asset);
        asset.SetReady();

        // 8. Audit log
        _db.MediaAuditLogs.Add(new MediaAuditLog(
            userId: request.UploadedBy,
            action: "Uploaded",
            mediaAssetId: asset.Id,
            resourceType: mediaTypeName,
            resourceId: request.OwnerId,
            oldValue: null,
            newValue: objectKey,
            notes: request.OriginalFileName));

        await _db.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "MediaAsset {Id} uploaded by {UserId} — key: {ObjectKey}",
            asset.Id, request.UploadedBy, objectKey);

        return MediaUploadResult.Success(MapToDto(asset));
    }

    public async Task<PresignedUploadUrlResult> GeneratePresignedUploadUrlAsync(
        PresignedUploadUrlRequest request,
        CancellationToken cancellationToken = default)
    {
        var mediaAssetId = Guid.NewGuid();
        var mediaTypeName = request.MediaType.ToString();
        var objectKey = MediaUrlService.BuildObjectKey(mediaTypeName, request.OwnerId, mediaAssetId);

        var uploadUrl = await _storage.GeneratePresignedPutUrlAsync(
            objectKey,
            request.ContentType,
            _options.PresignedUrlExpirySeconds,
            cancellationToken);

        var expiresAt = DateTime.UtcNow.AddSeconds(_options.PresignedUrlExpirySeconds);

        return new PresignedUploadUrlResult
        {
            UploadUrl = uploadUrl,
            ObjectKey = objectKey,
            MediaAssetId = mediaAssetId,
            ExpiresAtUtc = expiresAt
        };
    }

    public async Task<MediaUploadResult> ConfirmUploadAsync(
        Guid mediaAssetId,
        Guid confirmedBy,
        CancellationToken cancellationToken = default)
    {
        var asset = await _db.MediaAssets
            .FirstOrDefaultAsync(a => a.Id == mediaAssetId, cancellationToken);

        if (asset is null)
        {
            return MediaUploadResult.Failure("Media asset not found.");
        }

        var exists = await _storage.ObjectExistsAsync(asset.ObjectKey, cancellationToken);

        if (!exists)
        {
            return MediaUploadResult.Failure(
                "Upload could not be confirmed — file not found in storage.");
        }

        asset.SetReady();

        _db.MediaAuditLogs.Add(new MediaAuditLog(
            userId: confirmedBy,
            action: "UploadConfirmed",
            mediaAssetId: asset.Id,
            resourceType: asset.MediaType.ToString(),
            resourceId: null,
            oldValue: null,
            newValue: asset.ObjectKey,
            notes: null));

        await _db.SaveChangesAsync(cancellationToken);

        return MediaUploadResult.Success(MapToDto(asset));
    }

    // ---------------------------------------------------------------
    // READ
    // ---------------------------------------------------------------

    public async Task<MediaAssetDto?> GetByIdAsync(
        Guid mediaAssetId,
        CancellationToken cancellationToken = default)
    {
        var asset = await _db.MediaAssets
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == mediaAssetId, cancellationToken);

        return asset is null ? null : MapToDto(asset);
    }

    public async Task<PagedResult<MediaAssetDto>> GetLibraryAsync(
        MediaLibraryQuery query,
        CancellationToken cancellationToken = default)
    {
        var q = _db.MediaAssets.AsNoTracking().AsQueryable();

        if (query.MediaType.HasValue)
        {
            q = q.Where(a => a.MediaType == query.MediaType.Value);
        }

        if (query.Status.HasValue)
        {
            q = q.Where(a => a.Status == query.Status.Value);
        }

        if (query.UploadedBy.HasValue)
        {
            q = q.Where(a => a.UploadedBy == query.UploadedBy.Value);
        }

        if (query.CreatedFrom.HasValue)
        {
            q = q.Where(a => a.CreatedAtUtc >= query.CreatedFrom.Value);
        }

        if (query.CreatedTo.HasValue)
        {
            q = q.Where(a => a.CreatedAtUtc <= query.CreatedTo.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.ToLower();
            q = q.Where(a =>
                a.AltText.ToLower().Contains(search) ||
                (a.Title != null && a.Title.ToLower().Contains(search)) ||
                (a.Caption != null && a.Caption.ToLower().Contains(search)) ||
                a.OriginalFileName.ToLower().Contains(search));
        }

        // Exclude hard-deleted assets
        q = q.Where(a => a.Status != MediaAssetStatus.Deleted);

        var totalCount = await q.CountAsync(cancellationToken);

        // Sorting
        q = query.SortBy switch
        {
            "FileSize" => query.SortDescending
                ? q.OrderByDescending(a => a.FileSize)
                : q.OrderBy(a => a.FileSize),
            "AltText" => query.SortDescending
                ? q.OrderByDescending(a => a.AltText)
                : q.OrderBy(a => a.AltText),
            _ => query.SortDescending
                ? q.OrderByDescending(a => a.CreatedAtUtc)
                : q.OrderBy(a => a.CreatedAtUtc)
        };

        var items = await q
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<MediaAssetDto>
        {
            Items = items.Select(MapToDto).ToList(),
            Page = query.Page,
            PageSize = query.PageSize,
            TotalCount = totalCount
        };
    }

    // ---------------------------------------------------------------
    // UPDATE
    // ---------------------------------------------------------------

    public async Task<bool> UpdateMetadataAsync(
        Guid mediaAssetId,
        string altText,
        string? caption,
        string? title,
        Guid updatedBy,
        CancellationToken cancellationToken = default)
    {
        var asset = await _db.MediaAssets
            .FirstOrDefaultAsync(a => a.Id == mediaAssetId, cancellationToken);

        if (asset is null)
        {
            return false;
        }

        var oldAlt = asset.AltText;
        asset.UpdateMetadata(altText, caption, title, updatedBy);

        _db.MediaAuditLogs.Add(new MediaAuditLog(
            userId: updatedBy,
            action: "MetadataUpdated",
            mediaAssetId: asset.Id,
            resourceType: "MediaAsset",
            resourceId: asset.Id,
            oldValue: oldAlt,
            newValue: altText,
            notes: null));

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }

    // ---------------------------------------------------------------
    // LIFECYCLE
    // ---------------------------------------------------------------

    public async Task<bool> PublishAsync(
        Guid mediaAssetId,
        Guid publishedBy,
        CancellationToken cancellationToken = default)
    {
        var asset = await _db.MediaAssets
            .FirstOrDefaultAsync(a => a.Id == mediaAssetId, cancellationToken);

        if (asset is null) return false;

        asset.Publish(publishedBy);

        _db.MediaAuditLogs.Add(new MediaAuditLog(
            userId: publishedBy,
            action: "Published",
            mediaAssetId: asset.Id,
            resourceType: "MediaAsset",
            resourceId: asset.Id,
            oldValue: "Ready",
            newValue: "Published",
            notes: null));

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> UnpublishAsync(
        Guid mediaAssetId,
        Guid unpublishedBy,
        CancellationToken cancellationToken = default)
    {
        var asset = await _db.MediaAssets
            .FirstOrDefaultAsync(a => a.Id == mediaAssetId, cancellationToken);

        if (asset is null) return false;

        asset.Unpublish(unpublishedBy);

        _db.MediaAuditLogs.Add(new MediaAuditLog(
            userId: unpublishedBy,
            action: "Unpublished",
            mediaAssetId: asset.Id,
            resourceType: "MediaAsset",
            resourceId: asset.Id,
            oldValue: "Published",
            newValue: "Ready",
            notes: null));

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ArchiveAsync(
        Guid mediaAssetId,
        Guid archivedBy,
        CancellationToken cancellationToken = default)
    {
        var asset = await _db.MediaAssets
            .FirstOrDefaultAsync(a => a.Id == mediaAssetId, cancellationToken);

        if (asset is null) return false;

        asset.Archive(archivedBy);

        _db.MediaAuditLogs.Add(new MediaAuditLog(
            userId: archivedBy,
            action: "Archived",
            mediaAssetId: asset.Id,
            resourceType: "MediaAsset",
            resourceId: asset.Id,
            oldValue: asset.Status.ToString(),
            newValue: "Archived",
            notes: null));

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid mediaAssetId,
        Guid deletedBy,
        CancellationToken cancellationToken = default)
    {
        var asset = await _db.MediaAssets
            .FirstOrDefaultAsync(a => a.Id == mediaAssetId, cancellationToken);

        if (asset is null) return false;

        // Soft delete in DB first
        var objectKey = asset.ObjectKey;
        asset.SoftDelete(deletedBy);

        _db.MediaAuditLogs.Add(new MediaAuditLog(
            userId: deletedBy,
            action: "Deleted",
            mediaAssetId: asset.Id,
            resourceType: "MediaAsset",
            resourceId: asset.Id,
            oldValue: objectKey,
            newValue: "Deleted",
            notes: null));

        await _db.SaveChangesAsync(cancellationToken);

        // Then remove from S3 (fire-and-forget on failure — S3 cleanup service will retry)
        await _storage.DeleteObjectAsync(objectKey, cancellationToken);
        await _storage.DeleteObjectAsync(
            MediaUrlService.BuildVariantObjectKey(
                asset.MediaType.ToString(), deletedBy, mediaAssetId, "thumb"),
            cancellationToken);
        await _storage.DeleteObjectAsync(
            MediaUrlService.BuildVariantObjectKey(
                asset.MediaType.ToString(), deletedBy, mediaAssetId, "card"),
            cancellationToken);

        return true;
    }

    // ---------------------------------------------------------------
    // PRODUCT MEDIA
    // ---------------------------------------------------------------

    public async Task<bool> AssignToProductAsync(
        Guid productId,
        Guid mediaAssetId,
        bool isPrimary,
        int displayOrder,
        Guid assignedBy,
        CancellationToken cancellationToken = default)
    {
        var asset = await _db.MediaAssets
            .FirstOrDefaultAsync(a => a.Id == mediaAssetId, cancellationToken);

        if (asset is null) return false;

        var productMedia = new ProductMedia(productId, mediaAssetId, asset.AltText, displayOrder, isPrimary);

        if (isPrimary)
        {
            // Clear existing primary flag
            await _db.ProductMedia
                .Where(pm => pm.ProductId == productId && pm.IsPrimary)
                .ExecuteUpdateAsync(s => s.SetProperty(pm => pm.IsPrimary, false), cancellationToken);
        }

        _db.ProductMedia.Add(productMedia);

        _db.MediaAuditLogs.Add(new MediaAuditLog(
            userId: assignedBy,
            action: "AssignedToProduct",
            mediaAssetId: mediaAssetId,
            resourceType: "Product",
            resourceId: productId,
            oldValue: null,
            newValue: $"IsPrimary={isPrimary},Order={displayOrder}",
            notes: null));

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> SetProductMediaOrderAsync(
        Guid productId,
        IReadOnlyList<(Guid MediaId, int Order)> orderedMedia,
        Guid updatedBy,
        CancellationToken cancellationToken = default)
    {
        foreach (var (mediaId, order) in orderedMedia)
        {
            await _db.ProductMedia
                .Where(pm => pm.Id == mediaId && pm.ProductId == productId)
                .ExecuteUpdateAsync(
                    s => s.SetProperty(pm => pm.DisplayOrder, order),
                    cancellationToken);
        }

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> SetProductPrimaryMediaAsync(
        Guid productId,
        Guid mediaId,
        Guid updatedBy,
        CancellationToken cancellationToken = default)
    {
        // Clear all primary flags
        await _db.ProductMedia
            .Where(pm => pm.ProductId == productId && pm.IsPrimary)
            .ExecuteUpdateAsync(s => s.SetProperty(pm => pm.IsPrimary, false), cancellationToken);

        // Set new primary
        await _db.ProductMedia
            .Where(pm => pm.Id == mediaId && pm.ProductId == productId)
            .ExecuteUpdateAsync(s => s.SetProperty(pm => pm.IsPrimary, true), cancellationToken);

        return true;
    }

    public async Task<bool> RemoveFromProductAsync(
        Guid productMediaId,
        Guid removedBy,
        CancellationToken cancellationToken = default)
    {
        var productMedia = await _db.ProductMedia
            .FirstOrDefaultAsync(pm => pm.Id == productMediaId, cancellationToken);

        if (productMedia is null) return false;

        _db.ProductMedia.Remove(productMedia);

        _db.MediaAuditLogs.Add(new MediaAuditLog(
            userId: removedBy,
            action: "RemovedFromProduct",
            mediaAssetId: productMedia.MediaAssetId,
            resourceType: "Product",
            resourceId: productMedia.ProductId,
            oldValue: null,
            newValue: null,
            notes: $"ProductMedia {productMediaId} removed"));

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }

    // ---------------------------------------------------------------
    // MAPPING
    // ---------------------------------------------------------------

    private MediaAssetDto MapToDto(MediaAsset asset) =>
        new()
        {
            Id = asset.Id,
            MediaType = asset.MediaType.ToString(),
            ObjectKey = asset.ObjectKey,
            Url = _urlService.GetUrl(asset.ObjectKey),
            ThumbnailUrl = _urlService.GetThumbnailUrl(asset.ObjectKey),
            CardUrl = _urlService.GetCardUrl(asset.ObjectKey),
            OriginalFileName = asset.OriginalFileName,
            ContentType = asset.ContentType,
            FileSize = asset.FileSize,
            Width = asset.Width,
            Height = asset.Height,
            AltText = asset.AltText,
            Caption = asset.Caption,
            Title = asset.Title,
            Status = asset.Status.ToString(),
            UploadedBy = asset.UploadedBy,
            CreatedAtUtc = asset.CreatedAtUtc,
            UpdatedAtUtc = asset.UpdatedAtUtc
        };
}
