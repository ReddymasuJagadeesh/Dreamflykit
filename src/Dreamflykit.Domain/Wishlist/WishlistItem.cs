using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Wishlist;

public sealed class WishlistItem : AuditableEntity
{
    private WishlistItem()
    {
    }

    public WishlistItem(
        Guid wishlistId,
        Guid productVariantId,
        string productName,
        string variantName,
        string sku,
        string? imageUrl,
        decimal unitPrice,
        decimal mrp)
    {
        if (productVariantId == Guid.Empty)
            throw new DomainException("Product variant ID is required.");

        if (string.IsNullOrWhiteSpace(productName))
            throw new DomainException("Product name is required.");

        WishlistId = wishlistId;
        ProductVariantId = productVariantId;
        ProductName = productName.Trim();
        VariantName = variantName.Trim();
        Sku = sku.Trim();
        ImageUrl = imageUrl?.Trim();
        UnitPrice = unitPrice;
        Mrp = mrp;
    }

    public Guid WishlistId { get; private set; }

    public Guid ProductVariantId { get; private set; }

    public string ProductName { get; private set; } = string.Empty;

    public string VariantName { get; private set; } = string.Empty;

    public string Sku { get; private set; } = string.Empty;

    public string? ImageUrl { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal Mrp { get; private set; }

    public int DiscountPercentage =>
        Mrp > UnitPrice && Mrp > 0
            ? (int)Math.Round((Mrp - UnitPrice) / Mrp * 100m)
            : 0;

    public void UpdatePrice(decimal unitPrice, decimal mrp)
    {
        UnitPrice = unitPrice;
        Mrp = mrp;
        MarkUpdated();
    }
}
