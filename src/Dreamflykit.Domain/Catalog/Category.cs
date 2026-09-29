using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Catalog;

public sealed class Category : AuditableEntity
{
    private Category()
    {
    }

    public Category(
        string name,
        string slug,
        int displayOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Category name is required.");
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new DomainException(
                "Category slug is required.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Category display order cannot be negative.");
        }

        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        DisplayOrder = displayOrder;
        IsActive = true;
    }

    public string Name { get; private set; } = string.Empty;

    public string Slug { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    public int DisplayOrder { get; private set; }

    public void Update(
        string name,
        string slug,
        int displayOrder)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Category name is required.");
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new DomainException(
                "Category slug is required.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Category display order cannot be negative.");
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