using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Catalog;

public sealed class ProductAttributeValue : AuditableEntity
{
    private ProductAttributeValue()
    {
    }

    public ProductAttributeValue(
        Guid productId,
        Guid attributeId,
        string value)
    {
        if (productId == Guid.Empty)
        {
            throw new DomainException(
                "Product ID is required.");
        }

        if (attributeId == Guid.Empty)
        {
            throw new DomainException(
                "Attribute ID is required.");
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException(
                "Attribute value is required.");
        }

        if (value.Length > 500)
        {
            throw new DomainException(
                "Attribute value cannot exceed 500 characters.");
        }

        ProductId = productId;
        AttributeId = attributeId;
        Value = value.Trim();
    }

    public Guid ProductId { get; private set; }

    public Guid AttributeId { get; private set; }

    public string Value { get; private set; } =
        string.Empty;

    public void UpdateValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException(
                "Attribute value is required.");
        }

        if (value.Length > 500)
        {
            throw new DomainException(
                "Attribute value cannot exceed 500 characters.");
        }

        Value = value.Trim();

        MarkUpdated();
    }
}