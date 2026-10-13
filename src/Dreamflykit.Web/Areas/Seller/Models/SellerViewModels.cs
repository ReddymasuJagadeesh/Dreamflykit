using System.ComponentModel.DataAnnotations;
using Dreamflykit.Domain.Catalog;
using Microsoft.AspNetCore.Http;

namespace Dreamflykit.Web.Areas.Seller.Models;

public sealed class SellerDashboardViewModel
{
    public string SellerName { get; init; } = string.Empty;
    public string SellerEmail { get; init; } = string.Empty;
    public int TotalProducts { get; init; }
    public int PendingCount { get; init; }
    public int ApprovedCount { get; init; }
    public int RejectedCount { get; init; }
    public int PromotedCount { get; init; }
    public IReadOnlyList<SellerProductListItemViewModel> RecentProducts { get; init; } = Array.Empty<SellerProductListItemViewModel>();
}

public sealed class SellerProductListItemViewModel
{
    public Guid ProductId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string CategoryName { get; init; } = string.Empty;
    public string? SubcategoryName { get; init; }
    public decimal SellingPrice { get; init; }
    public decimal Mrp { get; init; }
    public string? ImageUrl { get; init; }
    public bool IsActive { get; init; }
    public bool IsPromoted { get; init; }
    public ProductApprovalStatus ApprovalStatus { get; init; }
    public string? RejectionReason { get; init; }
    public DateTime CreatedAtUtc { get; init; }
}

public sealed class SellerProductListViewModel
{
    public IReadOnlyList<SellerProductListItemViewModel> Products { get; init; } = Array.Empty<SellerProductListItemViewModel>();
    public string? SearchTerm { get; init; }
    public string? StatusFilter { get; init; }
    public int TotalCount { get; init; }
    public int PendingCount { get; init; }
    public int ApprovedCount { get; init; }
    public int RejectedCount { get; init; }
    public int PromotedCount { get; init; }
}

public sealed class SellerProductCreateViewModel
{
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(200, ErrorMessage = "Product name cannot exceed 200 characters")]
    [Display(Name = "Product Title")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "URL Slug is required")]
    [StringLength(220, ErrorMessage = "Slug cannot exceed 220 characters")]
    [RegularExpression(@"^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Slug must contain only lowercase letters, numbers, and hyphens (e.g. travel-adapter-pro).")]
    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a Category")]
    public Guid CategoryId { get; set; }

    public Guid? SubcategoryId { get; set; }

    [StringLength(500, ErrorMessage = "Short summary cannot exceed 500 characters")]
    public string? ShortDescription { get; set; }

    [Required(ErrorMessage = "Product description is required")]
    [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters")]
    public string Description { get; set; } = string.Empty;

    public IFormFile? ImageFile { get; set; }

    public string? AltText { get; set; }
}

public sealed class SellerProductEditViewModel
{
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "URL Slug is required")]
    [StringLength(220)]
    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a Category")]
    public Guid CategoryId { get; set; }

    public Guid? SubcategoryId { get; set; }

    [StringLength(500)]
    public string? ShortDescription { get; set; }

    [Required(ErrorMessage = "Product description is required")]
    [StringLength(5000)]
    public string Description { get; set; } = string.Empty;

    public string? ExistingImageUrl { get; set; }

    public IFormFile? NewImageFile { get; set; }

    public string? AltText { get; set; }

    public ProductApprovalStatus ApprovalStatus { get; set; }

    public string? RejectionReason { get; set; }

    public decimal CurrentSellingPrice { get; set; }
    public decimal CurrentMrp { get; set; }
}

public sealed class SellerProductMediaViewModel
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public IReadOnlyList<SellerProductMediaItemViewModel> MediaItems { get; set; } = Array.Empty<SellerProductMediaItemViewModel>();
}

public sealed class SellerProductMediaItemViewModel
{
    public Guid MediaId { get; init; }
    public string Url { get; init; } = string.Empty;
    public string AltText { get; init; } = string.Empty;
    public bool IsPrimary { get; init; }
}
