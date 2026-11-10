using Dreamflykit.Application.Catalog.Models;

namespace Dreamflykit.Web.Models.Products;

/// <summary>
/// Encapsulates all query and route parameters for category hub and product catalog requests.
/// </summary>
public sealed class CategoryLandingContext
{
    public string? Q { get; init; }
    public string? Search { get; init; }
    public Guid? CategoryId { get; init; }
    public string? Category { get; init; }
    public string? QueryCategory { get; init; }
    public string? SubcategorySlug { get; init; }
    public string? QuerySubcategory { get; init; }
    public string? BrandSlug { get; init; }
    public string? QueryBrand { get; init; }
    public Guid? SubcategoryId { get; init; }
    public string? SubcatItem { get; init; }
    public string? Item { get; init; }
    public string? Sub { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public bool? InStockOnly { get; init; }
    public int? MinDiscount { get; init; }
    public ProductSortOption Sort { get; init; } = ProductSortOption.Relevance;
    public string? View { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 24;
    public string? GenderQuery { get; init; }
    public string? DepartmentQuery { get; init; }
    public string? FootwearQuery { get; init; }
}

