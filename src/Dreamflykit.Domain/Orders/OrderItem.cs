using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Orders;

/// <summary>
/// A single line item within an order. Prices are snapshotted at order placement —
/// subsequent catalog price changes never affect historical orders.
/// </summary>
public sealed class OrderItem : AuditableEntity
{
    // EF Core private constructor
    private OrderItem()
    {
    }

    public OrderItem(
        Guid productVariantId,
        string productName,
        string variantName,
        string sku,
        string? imageUrl,
        decimal unitPrice,
        decimal mrp,
        int quantity,
        Guid orderId = default)
    {
        OrderId = orderId;
        ProductVariantId = productVariantId;
        ProductName = productName;
        VariantName = variantName;
        Sku = sku;
        ImageUrl = imageUrl;
        UnitPrice = unitPrice;
        Mrp = mrp;
        Quantity = quantity;
    }

    // ----------------------------------------------------------
    // Properties
    // ----------------------------------------------------------

    public Guid OrderId { get; private set; }

    public Guid ProductVariantId { get; private set; }

    /// <summary>Product name snapshotted at time of order.</summary>
    public string ProductName { get; private set; } = string.Empty;

    /// <summary>Variant name snapshotted at time of order.</summary>
    public string VariantName { get; private set; } = string.Empty;

    /// <summary>SKU snapshotted at time of order.</summary>
    public string Sku { get; private set; } = string.Empty;

    /// <summary>Product image URL snapshotted at time of order.</summary>
    public string? ImageUrl { get; private set; }

    /// <summary>Selling price per unit (INR) snapshotted at time of order.</summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>MRP per unit (INR) snapshotted at time of order.</summary>
    public decimal Mrp { get; private set; }

    public int Quantity { get; private set; }

    public decimal LineTotal => UnitPrice * Quantity;

    public decimal LineMrp => Mrp * Quantity;

    public decimal LineDiscount => LineMrp - LineTotal;
}
