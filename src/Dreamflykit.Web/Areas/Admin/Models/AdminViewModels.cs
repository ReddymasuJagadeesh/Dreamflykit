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
    public int PendingSellerApprovals { get; init; }
    public int TotalSellers { get; init; }
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
    public string? StatusFilter { get; init; }
    public int TotalCount { get; init; }
    public int PendingApprovalCount { get; init; }
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
    public bool IsPromoted { get; init; }
    public Dreamflykit.Domain.Catalog.ProductApprovalStatus ApprovalStatus { get; init; } = Dreamflykit.Domain.Catalog.ProductApprovalStatus.Approved;
    public Guid? SellerId { get; init; }
    public string? SellerName { get; init; }
    public string? RejectionReason { get; init; }
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

public sealed class AdminSellerListItemViewModel
{
    public Guid SellerId { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public bool IsLockedOut { get; init; }
    public int TotalProducts { get; init; }
    public int PendingProducts { get; init; }
    public int ApprovedProducts { get; init; }
    public IReadOnlyList<string> AssignedCategoryNames { get; init; } = Array.Empty<string>();
}

public sealed class AdminSellerListViewModel
{
    public IReadOnlyList<AdminSellerListItemViewModel> Sellers { get; init; } = Array.Empty<AdminSellerListItemViewModel>();
    public string? SearchTerm { get; init; }
    public int TotalCount { get; init; }
}

public sealed class AdminCreateSellerViewModel
{
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "First name is required")]
    [System.ComponentModel.DataAnnotations.StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Last name is required")]
    [System.ComponentModel.DataAnnotations.StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Email is required")]
    [System.ComponentModel.DataAnnotations.EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Phone number is required")]
    [System.ComponentModel.DataAnnotations.Phone(ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Password is required")]
    [System.ComponentModel.DataAnnotations.MinLength(12, ErrorMessage = "Password must be at least 12 characters and contain uppercase, lowercase, number, and special character.")]
    public string Password { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Confirm password is required")]
    [System.ComponentModel.DataAnnotations.Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    // Allowed Categories chosen by Super Admin
    public List<Guid> SelectedCategoryIds { get; set; } = new();
}

public sealed class AdminEditSellerViewModel
{
    public Guid SellerId { get; set; }

    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "First name is required")]
    [System.ComponentModel.DataAnnotations.StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Last name is required")]
    [System.ComponentModel.DataAnnotations.StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Phone number is required")]
    [System.ComponentModel.DataAnnotations.Phone(ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    // Allowed Categories chosen by Super Admin
    public List<Guid> SelectedCategoryIds { get; set; } = new();
}

public sealed class AdminReviewProductViewModel
{
    public Guid ProductId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string CategoryName { get; init; } = string.Empty;
    public string? SubcategoryName { get; init; }
    public string ShortDescription { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Guid? SellerId { get; init; }
    public string? SellerName { get; init; }
    public string? SellerEmail { get; init; }
    public string? SellerPhone { get; init; }
    public Dreamflykit.Domain.Catalog.ProductApprovalStatus ApprovalStatus { get; init; }
    public string? RejectionReason { get; init; }
    public string? ImageUrl { get; init; }
    public IReadOnlyList<string> AdditionalImages { get; init; } = Array.Empty<string>();
    public DateTime CreatedAtUtc { get; init; }

    // Pricing input by Super Admin
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "MRP is required")]
    [System.ComponentModel.DataAnnotations.Range(1, 10000000, ErrorMessage = "MRP must be greater than zero")]
    public decimal Mrp { get; set; }

    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Selling price is required")]
    [System.ComponentModel.DataAnnotations.Range(1, 10000000, ErrorMessage = "Selling price must be greater than zero")]
    public decimal SellingPrice { get; set; }

    public string? Sku { get; set; }

    public bool IsFeatured { get; set; }
}
