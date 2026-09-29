using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Catalog;

public sealed class Product : AuditableEntity
{
    private Product()
    {
    }

    public Product(
        Guid categoryId,
        string name,
        string slug,
        string shortDescription,
        string description,
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
                "Product name is required.");
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new DomainException(
                "Product slug is required.");
        }

        if (string.IsNullOrWhiteSpace(shortDescription))
        {
            throw new DomainException(
                "Product short description is required.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new DomainException(
                "Product description is required.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Product display order cannot be negative.");
        }

        CategoryId = categoryId;
        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        ShortDescription = shortDescription.Trim();
        Description = description.Trim();
        DisplayOrder = displayOrder;
        IsActive = true;
    }

    public Guid CategoryId { get; private set; }

    public Guid? SubcategoryId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Slug { get; private set; } = string.Empty;

    public string ShortDescription { get; private set; } =
        string.Empty;

    public string Description { get; private set; } =
        string.Empty;

    public bool IsActive { get; private set; }

    public bool IsFeatured { get; private set; }

    public int DisplayOrder { get; private set; }

    public void SetSubcategory(Guid? subcategoryId)
    {
        if (subcategoryId == Guid.Empty)
        {
            throw new DomainException(
                "Subcategory ID cannot be empty.");
        }

        SubcategoryId = subcategoryId;

        MarkUpdated();
    }

    public void RemoveSubcategory()
    {
        SubcategoryId = null;

        MarkUpdated();
    }

    public void Update(
        Guid categoryId,
        string name,
        string slug,
        string shortDescription,
        string description,
        int displayOrder)
    {
        if (categoryId == Guid.Empty)
        {
            throw new DomainException(
                "Category ID is required.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Product name is required.");
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new DomainException(
                "Product slug is required.");
        }

        if (string.IsNullOrWhiteSpace(shortDescription))
        {
            throw new DomainException(
                "Product short description is required.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new DomainException(
                "Product description is required.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Product display order cannot be negative.");
        }

        CategoryId = categoryId;
        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        ShortDescription = shortDescription.Trim();
        Description = description.Trim();
        DisplayOrder = displayOrder;

        MarkUpdated();
    }

    public void SetFeatured(bool isFeatured)
    {
        IsFeatured = isFeatured;

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