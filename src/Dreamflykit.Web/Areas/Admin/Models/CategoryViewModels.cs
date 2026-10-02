using System.ComponentModel.DataAnnotations;

namespace Dreamflykit.Web.Areas.Admin.Models;

public sealed class AdminCategoryListViewModel
{
    public List<AdminCategoryItemViewModel> Categories { get; set; } = new();
}

public sealed class AdminCategoryItemViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public Guid? ImageMediaAssetId { get; set; }
    public string? ImageUrl { get; set; }
    public int ProductCount { get; set; }
    public List<AdminSubcategoryItemViewModel> Subcategories { get; set; } = new();
}

public sealed class AdminSubcategoryItemViewModel
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public Guid? ImageMediaAssetId { get; set; }
    public string? ImageUrl { get; set; }
    public int ProductCount { get; set; }
}

public sealed class AdminCategoryCreateViewModel
{
    [Required(ErrorMessage = "Category name is required")]
    [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Slug is required")]
    [StringLength(100, ErrorMessage = "Slug cannot exceed 100 characters")]
    public string Slug { get; set; } = string.Empty;

    [Range(0, 9999, ErrorMessage = "Display order must be 0 or positive")]
    public int DisplayOrder { get; set; } = 0;

    public Guid? ImageMediaAssetId { get; set; }
    public string? ImagePreviewUrl { get; set; }
}

public sealed class AdminCategoryEditViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Slug is required")]
    [StringLength(100, ErrorMessage = "Slug cannot exceed 100 characters")]
    public string Slug { get; set; } = string.Empty;

    [Range(0, 9999, ErrorMessage = "Display order must be 0 or positive")]
    public int DisplayOrder { get; set; } = 0;

    public bool IsActive { get; set; }

    public Guid? ImageMediaAssetId { get; set; }
    public string? ImagePreviewUrl { get; set; }
}

public sealed class AdminSubcategoryCreateViewModel
{
    [Required(ErrorMessage = "Parent Category is required")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "Subcategory name is required")]
    [StringLength(100, ErrorMessage = "Subcategory name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Slug is required")]
    [StringLength(100, ErrorMessage = "Slug cannot exceed 100 characters")]
    public string Slug { get; set; } = string.Empty;

    [Range(0, 9999, ErrorMessage = "Display order must be 0 or positive")]
    public int DisplayOrder { get; set; } = 0;

    public Guid? ImageMediaAssetId { get; set; }
    public string? ImagePreviewUrl { get; set; }
}

public sealed class AdminSubcategoryEditViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Parent Category is required")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "Subcategory name is required")]
    [StringLength(100, ErrorMessage = "Subcategory name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Slug is required")]
    [StringLength(100, ErrorMessage = "Slug cannot exceed 100 characters")]
    public string Slug { get; set; } = string.Empty;

    [Range(0, 9999, ErrorMessage = "Display order must be 0 or positive")]
    public int DisplayOrder { get; set; } = 0;

    public bool IsActive { get; set; }

    public Guid? ImageMediaAssetId { get; set; }
    public string? ImagePreviewUrl { get; set; }
}
