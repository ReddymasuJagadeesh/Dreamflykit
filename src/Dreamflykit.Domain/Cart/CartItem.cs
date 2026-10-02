using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Cart;

/// <summary>
/// A single line item in the shopping cart.
/// Prices are snapshotted at the time of adding — the cart always
/// reflects the price the customer saw, not any subsequent changes.
/// </summary>
public sealed class CartItem : AuditableEntity
{
    private CartItem()
    {
    }

    internal CartItem(
        Guid cartId,
        Guid productVariantId,
        string productName,
        string variantName,
        string sku,
        string? imageUrl,
        decimal unitPrice,
        decimal mrp,
        int quantity)
    {
        CartId = cartId;
        ProductVariantId = productVariantId;
        ProductName = productName.Trim();
        VariantName = variantName.Trim();
        Sku = sku.Trim().ToUpperInvariant();
        ImageUrl = imageUrl;
        UnitPrice = unitPrice;
        Mrp = mrp;
        Quantity = quantity;
    }

    // --------------------------------------------------------
    // Properties
    // --------------------------------------------------------

    public Guid CartId { get; private set; }

    public Guid ProductVariantId { get; private set; }

    /// <summary>Product name snapshotted at the time of add.</summary>
    public string ProductName { get; private set; } = string.Empty;

    /// <summary>Variant name snapshotted at the time of add.</summary>
    public string VariantName { get; private set; } = string.Empty;

    /// <summary>SKU snapshotted at the time of add.</summary>
    public string Sku { get; private set; } = string.Empty;

    /// <summary>Primary image URL snapshotted at the time of add.</summary>
    public string? ImageUrl { get; private set; }

    /// <summary>Selling price snapshotted at the time of add (INR).</summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>Maximum Retail Price snapshotted at the time of add (INR).</summary>
    public decimal Mrp { get; private set; }

    public int Quantity { get; private set; }

    public decimal LineTotal => UnitPrice * Quantity;

    public decimal LineMrp => Mrp * Quantity;

    public decimal LineDiscount => LineMrp - LineTotal;

    public decimal DiscountPercentage =>
        Mrp <= 0
            ? 0
            : Math.Round(((Mrp - UnitPrice) / Mrp) * 100, 0);

    // --------------------------------------------------------
    // Domain operations
    // --------------------------------------------------------

    /// <summary>
    /// Increases quantity by the given delta.
    /// Total quantity must not exceed 99.
    /// </summary>
    internal void IncreaseQuantity(int delta)
    {
        var newQty = Quantity + delta;

        if (newQty > 99)
        {
            throw new DomainException(
                $"Cannot add more than 99 units of '{ProductName}'.");
        }

        Quantity = newQty;
        MarkUpdated();
    }

    /// <summary>
    /// Sets the quantity to an explicit value (1–99).
    /// </summary>
    internal void SetQuantity(int quantity)
    {
        if (quantity is < 1 or > 99)
        {
            throw new DomainException(
                "Quantity must be between 1 and 99.");
        }

        Quantity = quantity;
        MarkUpdated();
    }
}
