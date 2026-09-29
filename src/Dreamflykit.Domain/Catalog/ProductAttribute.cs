using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Catalog;

public sealed class ProductAttribute : AuditableEntity
{
    private ProductAttribute()
    {
    }

    public ProductAttribute(
        string name,
        string code,
        int displayOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Attribute name is required.");
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new DomainException(
                "Attribute code is required.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Attribute display order cannot be negative.");
        }

        Name = name.Trim();
        Code = code.Trim().ToLowerInvariant();
        DisplayOrder = displayOrder;
        IsActive = true;
    }

    public string Name { get; private set; } =
        string.Empty;

    public string Code { get; private set; } =
        string.Empty;

    public int DisplayOrder { get; private set; }

    public bool IsActive { get; private set; }

    public void Update(
        string name,
        string code,
        int displayOrder)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Attribute name is required.");
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new DomainException(
                "Attribute code is required.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Attribute display order cannot be negative.");
        }

        Name = name.Trim();
        Code = code.Trim().ToLowerInvariant();
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