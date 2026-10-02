using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Media;

/// <summary>
/// Central media asset record. Stores metadata about a file in AWS S3.
/// The actual binary is stored in S3; PostgreSQL stores metadata only.
/// </summary>
public sealed class MediaAsset : AuditableEntity
{
    private MediaAsset()
    {
    }

    public MediaAsset(
        MediaType mediaType,
        string objectKey,
        string originalFileName,
        string contentType,
        string fileExtension,
        long fileSize,
        int width,
        int height,
        string altText,
        Guid uploadedBy)
    {
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            throw new DomainException("Object key is required.");
        }

        if (string.IsNullOrWhiteSpace(originalFileName))
        {
            throw new DomainException("Original file name is required.");
        }

        if (string.IsNullOrWhiteSpace(contentType))
        {
            throw new DomainException("Content type is required.");
        }

        if (string.IsNullOrWhiteSpace(fileExtension))
        {
            throw new DomainException("File extension is required.");
        }

        if (fileSize <= 0)
        {
            throw new DomainException("File size must be greater than zero.");
        }

        if (width <= 0 || height <= 0)
        {
            throw new DomainException("Image dimensions must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(altText))
        {
            throw new DomainException("Alt text is required for accessibility.");
        }

        if (altText.Length > 300)
        {
            throw new DomainException("Alt text cannot exceed 300 characters.");
        }

        if (uploadedBy == Guid.Empty)
        {
            throw new DomainException("Uploaded by user ID is required.");
        }

        MediaType = mediaType;
        ObjectKey = objectKey.Trim();
        OriginalFileName = originalFileName.Trim();
        ContentType = contentType.Trim().ToLowerInvariant();
        FileExtension = fileExtension.Trim().ToLowerInvariant();
        FileSize = fileSize;
        Width = width;
        Height = height;
        AltText = altText.Trim();
        UploadedBy = uploadedBy;
        Status = MediaAssetStatus.Uploaded;
    }

    // ----------------------------------------------------------------
    // Identity & Classification
    // ----------------------------------------------------------------

    /// <summary>
    /// Controlled media category — product, category, banner, etc.
    /// </summary>
    public MediaType MediaType { get; private set; }

    /// <summary>
    /// The S3 object key used to locate the file (e.g., media/products/{productId}/{mediaId}.webp).
    /// Never build URLs from this directly — always use MediaUrlService.
    /// </summary>
    public string ObjectKey { get; private set; } = string.Empty;

    /// <summary>
    /// The original filename provided by the uploader.
    /// Stored for audit purposes only — never used as the S3 key.
    /// </summary>
    public string OriginalFileName { get; private set; } = string.Empty;

    // ----------------------------------------------------------------
    // File Properties
    // ----------------------------------------------------------------

    public string ContentType { get; private set; } = string.Empty;

    public string FileExtension { get; private set; } = string.Empty;

    /// <summary>File size in bytes.</summary>
    public long FileSize { get; private set; }

    /// <summary>Image width in pixels.</summary>
    public int Width { get; private set; }

    /// <summary>Image height in pixels.</summary>
    public int Height { get; private set; }

    // ----------------------------------------------------------------
    // Display Metadata
    // ----------------------------------------------------------------

    /// <summary>Accessibility alt text — required for all customer-facing images.</summary>
    public string AltText { get; private set; } = string.Empty;

    public string? Caption { get; private set; }

    public string? Title { get; private set; }

    // ----------------------------------------------------------------
    // Lifecycle
    // ----------------------------------------------------------------

    public MediaAssetStatus Status { get; private set; }

    // ----------------------------------------------------------------
    // Audit
    // ----------------------------------------------------------------

    public Guid UploadedBy { get; private set; }

    public Guid? UpdatedBy { get; private set; }

    // ----------------------------------------------------------------
    // Domain Methods
    // ----------------------------------------------------------------

    public void SetReady()
    {
        if (Status != MediaAssetStatus.Uploaded && Status != MediaAssetStatus.Processing)
        {
            throw new DomainException(
                $"Cannot mark as Ready from status {Status}.");
        }

        Status = MediaAssetStatus.Ready;
        MarkUpdated();
    }

    public void Publish(Guid publishedBy)
    {
        if (Status != MediaAssetStatus.Ready)
        {
            throw new DomainException(
                "Only Ready assets can be published.");
        }

        if (publishedBy == Guid.Empty)
        {
            throw new DomainException("Publisher user ID is required.");
        }

        Status = MediaAssetStatus.Published;
        UpdatedBy = publishedBy;
        MarkUpdated();
    }

    public void Unpublish(Guid unpublishedBy)
    {
        if (Status != MediaAssetStatus.Published)
        {
            throw new DomainException(
                "Only Published assets can be unpublished.");
        }

        Status = MediaAssetStatus.Ready;
        UpdatedBy = unpublishedBy;
        MarkUpdated();
    }

    public void Archive(Guid archivedBy)
    {
        if (Status == MediaAssetStatus.Deleted)
        {
            throw new DomainException("Cannot archive a deleted asset.");
        }

        Status = MediaAssetStatus.Archived;
        UpdatedBy = archivedBy;
        MarkUpdated();
    }

    public void SoftDelete(Guid deletedBy)
    {
        Status = MediaAssetStatus.Deleted;
        UpdatedBy = deletedBy;
        MarkUpdated();
    }

    public void MarkFailed()
    {
        Status = MediaAssetStatus.Failed;
        MarkUpdated();
    }

    public void UpdateMetadata(
        string altText,
        string? caption,
        string? title,
        Guid updatedBy)
    {
        if (string.IsNullOrWhiteSpace(altText))
        {
            throw new DomainException("Alt text is required.");
        }

        if (altText.Length > 300)
        {
            throw new DomainException("Alt text cannot exceed 300 characters.");
        }

        if (updatedBy == Guid.Empty)
        {
            throw new DomainException("Updated by user ID is required.");
        }

        AltText = altText.Trim();
        Caption = caption?.Trim();
        Title = title?.Trim();
        UpdatedBy = updatedBy;
        MarkUpdated();
    }

    public void SetProcessing()
    {
        Status = MediaAssetStatus.Processing;
        MarkUpdated();
    }
}
