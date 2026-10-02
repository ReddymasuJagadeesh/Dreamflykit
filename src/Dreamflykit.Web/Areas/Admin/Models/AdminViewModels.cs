using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Domain.Orders;

namespace Dreamflykit.Web.Areas.Admin.Models;

public sealed class AdminDashboardViewModel
{
    public int TotalOrders { get; init; }
    public decimal TotalRevenue { get; init; }
    public int TotalProducts { get; init; }
    public int TotalCustomers { get; init; }
    public int PendingOrders { get; init; }
    public int DeliveredOrders { get; init; }
    public IReadOnlyList<AdminOrderListItemViewModel> RecentOrders { get; init; } = Array.Empty<AdminOrderListItemViewModel>();
    public bool IsSuperAdmin { get; init; }
}

public sealed class AdminOrderListItemViewModel
{
    public Guid OrderId { get; init; }
    public string OrderNumber { get; init; } = string.Empty;
    public string CustomerName { get; init; } = string.Empty;
    public string CustomerEmail { get; init; } = string.Empty;
    public int ItemCount { get; init; }
    public decimal TotalAmount { get; init; }
    public OrderStatus Status { get; init; }
    public string StatusLabel => Status.ToString();
    public DateTime CreatedAtUtc { get; init; }
    public string PaymentMethod { get; init; } = "COD";
}

public sealed class AdminOrderListViewModel
{
    public IReadOnlyList<AdminOrderListItemViewModel> Orders { get; init; } = Array.Empty<AdminOrderListItemViewModel>();
    public OrderStatus? SelectedStatus { get; init; }
    public string? SearchTerm { get; init; }
    public int TotalCount { get; init; }
}

public sealed class AdminOrderDetailsViewModel
{
    public Guid OrderId { get; init; }
    public string OrderNumber { get; init; } = string.Empty;
    public string CustomerName { get; init; } = string.Empty;
    public string CustomerEmail { get; init; } = string.Empty;
    public string CustomerPhone { get; init; } = string.Empty;
    public OrderStatus Status { get; init; }
    public string PaymentMethod { get; init; } = "COD";
    public string? TrackingNumber { get; init; }
    public decimal TotalAmount { get; init; }
    public decimal Subtotal { get; init; }
    public decimal ShippingFee { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public DateTime? ShippedAtUtc { get; init; }
    public DateTime? DeliveredAtUtc { get; init; }
    public string ShippingFullName { get; init; } = string.Empty;
    public string ShippingPhone { get; init; } = string.Empty;
    public string ShippingAddressLine { get; init; } = string.Empty;
    public string ShippingCity { get; init; } = string.Empty;
    public string ShippingState { get; init; } = string.Empty;
    public string ShippingPostalCode { get; init; } = string.Empty;
    public string ShippingCountry { get; init; } = string.Empty;
    public string? CancellationReason { get; init; }
    public string? PaymentOrderId { get; init; }
    public IReadOnlyList<AdminOrderItemViewModel> Items { get; init; } = Array.Empty<AdminOrderItemViewModel>();
    public IReadOnlyList<OrderStatus> AvailableNextStatuses { get; init; } = Array.Empty<OrderStatus>();
}

public sealed class AdminOrderItemViewModel
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public string VariantName { get; init; } = string.Empty;
    public string Sku { get; init; } = string.Empty;
    public decimal UnitPrice { get; init; }
    public int Quantity { get; init; }
    public decimal TotalPrice => UnitPrice * Quantity;
    public string? ImageUrl { get; init; }
}

public sealed class AdminProductListViewModel
{
    public IReadOnlyList<AdminProductListItemViewModel> Products { get; init; } = Array.Empty<AdminProductListItemViewModel>();
    public string? SearchTerm { get; init; }
    public Guid? CategoryId { get; init; }
    public int TotalCount { get; init; }
}

public sealed class AdminProductListItemViewModel
{
    public Guid ProductId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string CategoryName { get; init; } = string.Empty;
    public decimal SellingPrice { get; init; }
    public decimal Mrp { get; init; }
    public string? ImageUrl { get; init; }
    public bool IsActive { get; init; }
    public bool IsFeatured { get; init; }
}

public sealed class AdminUserListItemViewModel
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public IReadOnlyList<string> Roles { get; init; } = Array.Empty<string>();
    public bool IsLockedOut { get; init; }
    public bool IsActive { get; init; }
    public DateTime? LockoutEnd { get; init; }
}
