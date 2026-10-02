namespace Dreamflykit.Application.Cart.Models;

/// <summary>
/// Full cart summary returned to the UI layer.
/// </summary>
public sealed class CartSummary
{
    public Guid? CartId { get; init; }

    public IReadOnlyList<CartLineItem> Items { get; init; }
        = Array.Empty<CartLineItem>();

    public int TotalItems { get; init; }

    public decimal Subtotal { get; init; }

    public decimal TotalMrp { get; init; }

    public decimal TotalDiscount { get; init; }

    public bool IsEmpty => Items.Count == 0;
}

/// <summary>
/// A single line in the cart summary.
/// </summary>
public sealed class CartLineItem
{
    public Guid ProductVariantId { get; init; }

    public string ProductName { get; init; } = string.Empty;

    public string VariantName { get; init; } = string.Empty;

    public string Sku { get; init; } = string.Empty;

    public string? ImageUrl { get; init; }

    public decimal UnitPrice { get; init; }

    public decimal Mrp { get; init; }

    public int Quantity { get; init; }

    public decimal LineTotal { get; init; }

    public decimal LineMrp { get; init; }

    public decimal LineDiscount { get; init; }

    public decimal DiscountPercentage { get; init; }
}

/// <summary>
/// Command to add a product variant to the cart.
/// </summary>
public sealed class AddToCartCommand
{
    public Guid ProductVariantId { get; init; }

    public int Quantity { get; init; } = 1;
}
