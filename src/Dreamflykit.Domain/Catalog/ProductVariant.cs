using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Catalog;

public sealed class ProductVariant : AuditableEntity
{
    private ProductVariant()
    {
    }

    public ProductVariant(
        Guid productId,
        string name,
        string sku,
        decimal mrp,
        decimal sellingPrice,
        bool isDefault = false)
    {
        Validate(
            productId,
            name,
            sku,
            mrp,
            sellingPrice);

        ProductId = productId;
        Name = name.Trim();
        Sku = sku.Trim().ToUpperInvariant();
        Mrp = mrp;
        SellingPrice = sellingPrice;
        IsDefault = isDefault;
        IsActive = true;
    }

    public Guid ProductId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Sku { get; private set; } = string.Empty;

    public decimal Mrp { get; private set; }

    public decimal SellingPrice { get; private set; }

    public bool IsDefault { get; private set; }

    public bool IsActive { get; private set; }

    public decimal DiscountAmount =>
        Mrp - SellingPrice;

    public decimal DiscountPercentage =>
        Mrp == 0
            ? 0
            : Math.Round(
                ((Mrp - SellingPrice) / Mrp) * 100,
                2);

    public void Update(
        string name,
        string sku,
        decimal mrp,
        decimal sellingPrice,
        bool isDefault)
    {
        Validate(
            ProductId,
            name,
            sku,
            mrp,
            sellingPrice);

        Name = name.Trim();
        Sku = sku.Trim().ToUpperInvariant();
        Mrp = mrp;
        SellingPrice = sellingPrice;
        IsDefault = isDefault;

        MarkUpdated();
    }

    public void SetDefault(bool isDefault)
    {
        IsDefault = isDefault;

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

    private static void Validate(
        Guid productId,
        string name,
        string sku,
        decimal mrp,
        decimal sellingPrice)
    {
        if (productId == Guid.Empty)
        {
            throw new DomainException(
                "Product ID is required.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Product variant name is required.");
        }

        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new DomainException(
                "Product SKU is required.");
        }

        if (mrp <= 0)
        {
            throw new DomainException(
                "MRP must be greater than zero.");
        }

        if (sellingPrice <= 0)
        {
            throw new DomainException(
                "Selling price must be greater than zero.");
        }

        if (sellingPrice > mrp)
        {
            throw new DomainException(
                "Selling price cannot be greater than MRP.");
        }
    }
}