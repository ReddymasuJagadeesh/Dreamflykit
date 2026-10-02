using Dreamflykit.Domain.Common;
using Dreamflykit.Domain.Media;

namespace Dreamflykit.Domain.Catalog;

public sealed class Subcategory : AuditableEntity
{
    private Subcategory()
    {
    }

    public Subcategory(
        Guid categoryId,
        string name,
        string slug,
        int displayOrder = 0)
    {
        if (categoryId == Guid.Empty)
        {
            throw new DomainException(
                "Category ID is required.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Subcategory name is required.");
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new DomainException(
                "Subcategory slug is required.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Subcategory display order cannot be negative.");
        }

        CategoryId = categoryId;
        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        DisplayOrder = displayOrder;
        IsActive = true;
    }

    public Guid CategoryId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Slug { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    public int DisplayOrder { get; private set; }

    /// <summary>
    /// Reference to the centralized MediaAsset for this subcategory's image.
    /// Null means no image has been assigned yet.
    /// </summary>
    public Guid? ImageMediaAssetId { get; private set; }

    public void SetImage(Guid mediaAssetId)
    {
        if (mediaAssetId == Guid.Empty)
        {
            throw new DomainException("Media asset ID cannot be empty.");
        }

        ImageMediaAssetId = mediaAssetId;
        MarkUpdated();
    }

    public void RemoveImage()
    {
        ImageMediaAssetId = null;
        MarkUpdated();
    }

    public void Update(
        string name,
        string slug,
        int displayOrder)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Subcategory name is required.");
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new DomainException(
                "Subcategory slug is required.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Subcategory display order cannot be negative.");
        }

        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        DisplayOrder = displayOrder;

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