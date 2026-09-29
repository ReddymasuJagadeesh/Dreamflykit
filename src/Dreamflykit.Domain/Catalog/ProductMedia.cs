using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Catalog;

public sealed class ProductMedia : AuditableEntity
{
    private ProductMedia()
    {
    }

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

    public string MediaUrl { get; private set; } =
        string.Empty;

    public string AltText { get; private set; } =
        string.Empty;

    public int DisplayOrder { get; private set; }

    public bool IsPrimary { get; private set; }

    public bool IsActive { get; private set; }

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

        MediaUrl = mediaUrl.Trim();
        AltText = altText.Trim();
        DisplayOrder = displayOrder;

        MarkUpdated();
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