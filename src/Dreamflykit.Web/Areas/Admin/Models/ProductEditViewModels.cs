using System.ComponentModel.DataAnnotations;

namespace Dreamflykit.Web.Areas.Admin.Models;

public sealed class AdminProductCreateViewModel
{
    [Required(ErrorMessage = "Category is required")]
    public Guid CategoryId { get; set; }

    public Guid? SubcategoryId { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(200, ErrorMessage = "Product name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Slug is required")]
    [StringLength(200, ErrorMessage = "Slug cannot exceed 200 characters")]
    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Short summary is required")]
    [StringLength(500, ErrorMessage = "Short description cannot exceed 500 characters")]
    public string ShortDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product description is required")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "SKU is required")]
    [StringLength(50, ErrorMessage = "SKU cannot exceed 50 characters")]
    public string Sku { get; set; } = string.Empty;

    [Required(ErrorMessage = "MRP is required")]
    [Range(1, 10000000, ErrorMessage = "MRP must be greater than zero")]
    public decimal Mrp { get; set; }

    [Required(ErrorMessage = "Selling price is required")]
    [Range(1, 10000000, ErrorMessage = "Selling price must be greater than zero")]
    public decimal SellingPrice { get; set; }

    [Range(0, 9999, ErrorMessage = "Display order must be 0 or positive")]
    public int DisplayOrder { get; set; } = 0;

    public bool IsFeatured { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public Guid? PrimaryMediaAssetId { get; set; }
    public string? PrimaryImagePreviewUrl { get; set; }
}

public sealed class AdminProductEditViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Category is required")]
    public Guid CategoryId { get; set; }

    public Guid? SubcategoryId { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(200, ErrorMessage = "Product name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Slug is required")]
    [StringLength(200, ErrorMessage = "Slug cannot exceed 200 characters")]
    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Short summary is required")]
    [StringLength(500, ErrorMessage = "Short description cannot exceed 500 characters")]
    public string ShortDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product description is required")]
    public string Description { get; set; } = string.Empty;

    public Guid VariantId { get; set; }

    [Required(ErrorMessage = "SKU is required")]
    [StringLength(50, ErrorMessage = "SKU cannot exceed 50 characters")]
    public string Sku { get; set; } = string.Empty;

    [Required(ErrorMessage = "MRP is required")]
    [Range(1, 10000000, ErrorMessage = "MRP must be greater than zero")]
    public decimal Mrp { get; set; }

    [Required(ErrorMessage = "Selling price is required")]
    [Range(1, 10000000, ErrorMessage = "Selling price must be greater than zero")]
    public decimal SellingPrice { get; set; }

    [Range(0, 9999, ErrorMessage = "Display order must be 0 or positive")]
    public int DisplayOrder { get; set; } = 0;

    public bool IsFeatured { get; set; }

    public bool IsActive { get; set; }

    public Guid? PrimaryMediaAssetId { get; set; }
    public string? PrimaryImagePreviewUrl { get; set; }
}
