using Dreamflykit.Domain.Common;
using Dreamflykit.Domain.Media;

namespace Dreamflykit.Domain.Catalog;

public sealed class ProductMedia : AuditableEntity
{
    private ProductMedia()
    {
    }

    /// <summary>
    /// Creates a product media record backed by a centralized MediaAsset (S3 path).
    /// </summary>
    public ProductMedia(
        Guid productId,
        Guid mediaAssetId,
        string altText,
        int displayOrder = 0,
        bool isPrimary = false)
    {
        if (productId == Guid.Empty)
        {
            throw new DomainException(
                "Product ID is required.");
        }

        if (mediaAssetId == Guid.Empty)
        {
            throw new DomainException(
                "Media asset ID is required.");
        }

        if (string.IsNullOrWhiteSpace(altText))
        {
            throw new DomainException(
                "Media alt text is required.");
        }

        if (altText.Length > 300)
        {
            throw new DomainException(
                "Media alt text cannot exceed 300 characters.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Media display order cannot be negative.");
        }

        ProductId = productId;
        MediaAssetId = mediaAssetId;
        // MediaUrl kept for backward-compat; set to empty for new S3-backed records.
        MediaUrl = string.Empty;
        AltText = altText.Trim();
        DisplayOrder = displayOrder;
        IsPrimary = isPrimary;
        IsActive = true;
    }

    /// <summary>
    /// Legacy constructor — used by existing seeded data with local URLs.
    /// Do NOT use for new uploads. Use the MediaAsset-based constructor instead.
    /// </summary>
    public ProductMedia(
        Guid productId,
        string mediaUrl,
        string altText,
        int displayOrder = 0,
        bool isPrimary = false)
    {
        if (productId == Guid.Empty)
        {
            throw new DomainException(
                "Product ID is required.");
        }

        if (string.IsNullOrWhiteSpace(mediaUrl))
        {
            throw new DomainException(
                "Media URL is required.");
        }

        if (mediaUrl.Length > 2000)
        {
            throw new DomainException(
                "Media URL cannot exceed 2000 characters.");
        }

        if (string.IsNullOrWhiteSpace(altText))
        {
            throw new DomainException(
                "Media alt text is required.");
        }

        if (altText.Length > 300)
        {
            throw new DomainException(
                "Media alt text cannot exceed 300 characters.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Media display order cannot be negative.");
        }

        ProductId = productId;
        MediaUrl = mediaUrl.Trim();
        AltText = altText.Trim();
        DisplayOrder = displayOrder;
        IsPrimary = isPrimary;
        IsActive = true;
    }

    public Guid ProductId { get; private set; }

    /// <summary>
    /// FK to the centralized MediaAsset. Null for legacy seeded records.
    /// All new uploads must set this.
    /// </summary>
    public Guid? MediaAssetId { get; private set; }

    /// <summary>
    /// Legacy field — used only for locally-seeded placeholder images.
    /// For S3-backed images, use MediaAsset.ObjectKey via MediaUrlService.
    /// </summary>
    public string MediaUrl { get; private set; } =
        string.Empty;

    public string AltText { get; private set; } =
        string.Empty;

    public int DisplayOrder { get; private set; }

    public bool IsPrimary { get; private set; }

    public bool IsActive { get; private set; }

    /// <summary>Update alt text and display order for an S3-backed media record.</summary>
    public void Update(
        string altText,
        int displayOrder)
    {
        if (string.IsNullOrWhiteSpace(altText))
        {
            throw new DomainException(
                "Media alt text is required.");
        }

        if (altText.Length > 300)
        {
            throw new DomainException(
                "Media alt text cannot exceed 300 characters.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Media display order cannot be negative.");
        }

        AltText = altText.Trim();
        DisplayOrder = displayOrder;

        MarkUpdated();
    }

    /// <summary>Legacy update for URL-based records only.</summary>
    public void Update(
        string mediaUrl,
        string altText,
        int displayOrder)
    {
        if (string.IsNullOrWhiteSpace(mediaUrl))
        {
            throw new DomainException(
                "Media URL is required.");
        }

        if (mediaUrl.Length > 2000)
        {
            throw new DomainException(
                "Media URL cannot exceed 2000 characters.");
        }

        Update(altText, displayOrder);
        MediaUrl = mediaUrl.Trim();
    }

    public void SetPrimary(bool isPrimary)
    {
        IsPrimary = isPrimary;

        MarkUpdated();
    }

    public void Activate()
    {
        IsActive = true;

        MarkUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;

        MarkUpdated();
    }
}