using Dreamflykit.Domain.Orders;

namespace Dreamflykit.Application.Orders.Models;

/// <summary>Command to place a new order from the cart.</summary>
public sealed class PlaceOrderCommand
{
    public Guid UserId { get; init; }

    public Guid ShippingAddressId { get; init; }

    public Guid BillingAddressId { get; init; }
}

/// <summary>Full order details returned to the UI.</summary>
public sealed class OrderDetails
{
    public Guid OrderId { get; init; }
    public string OrderNumber { get; init; } = string.Empty;
    public OrderStatus Status { get; init; }
    public string StatusDisplay { get; init; } = string.Empty;
    public DateTime PlacedAtUtc { get; init; }
    public DateTime? DeliveredAtUtc { get; init; }
    public bool CanCancel { get; init; }
    public bool CanReturn { get; init; }
    public OrderAddressDto ShippingAddress { get; init; } = null!;
    public OrderAddressDto BillingAddress { get; init; } = null!;
    public IReadOnlyList<OrderItemDto> Items { get; init; } = Array.Empty<OrderItemDto>();
    public decimal Subtotal { get; init; }
    public decimal TotalMrp { get; init; }
    public decimal TotalDiscount { get; init; }
    public decimal DeliveryCharge { get; init; }
    public decimal GrandTotal { get; init; }
}

/// <summary>Summary item for the orders list page.</summary>
public sealed class OrderSummary
{
    public Guid OrderId { get; init; }
    public string OrderNumber { get; init; } = string.Empty;
    public OrderStatus Status { get; init; }
    public string StatusDisplay { get; init; } = string.Empty;
    public DateTime PlacedAtUtc { get; init; }
    public int ItemCount { get; init; }
    public decimal GrandTotal { get; init; }
    public bool CanCancel { get; init; }
}

public sealed class OrderListResult
{
    public IReadOnlyList<OrderSummary> Orders { get; init; } = Array.Empty<OrderSummary>();
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}

public sealed class OrderItemDto
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
}

public sealed class OrderAddressDto
{
    public string FullName { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string AddressLine1 { get; init; } = string.Empty;
    public string? AddressLine2 { get; init; }
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string DisplayString { get; init; } = string.Empty;
}
