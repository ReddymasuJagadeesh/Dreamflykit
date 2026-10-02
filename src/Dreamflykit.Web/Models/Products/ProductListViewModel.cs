using Dreamflykit.Application.Catalog.Models;

namespace Dreamflykit.Web.Models.Products;

public sealed class ProductListViewModel
{
    public IReadOnlyList<ProductListItem> Products { get; init; }
        = Array.Empty<ProductListItem>();

    public int Page { get; init; }

    public int PageSize { get; init; }

    public int TotalCount { get; init; }

    public int TotalPages { get; init; }

    public bool HasPreviousPage { get; init; }

    public bool HasNextPage { get; init; }

    public string? SearchTerm { get; init; }

    public Guid? CategoryId { get; init; }

    public Guid? SubcategoryId { get; init; }

    public decimal? MinPrice { get; init; }

    public decimal? MaxPrice { get; init; }

    public ProductSortOption Sort { get; init; }
        = ProductSortOption.Relevance;

    public string? CategorySlug { get; init; }

    public string? CategoryName { get; init; }

    public bool? InStockOnly { get; init; }

    public int? MinDiscount { get; init; }

    public IReadOnlyList<CategoryFacet> Categories { get; init; }
        = Array.Empty<CategoryFacet>();

    public IReadOnlyList<PriceFacetRange> PriceRanges { get; init; }
        = Array.Empty<PriceFacetRange>();

    public IReadOnlyList<DiscountFacet> Discounts { get; init; }
        = Array.Empty<DiscountFacet>();

    public bool HasActiveFilters =>
        !string.IsNullOrWhiteSpace(SearchTerm) ||
        CategoryId.HasValue ||
        !string.IsNullOrWhiteSpace(CategorySlug) ||
        MinPrice.HasValue ||
        MaxPrice.HasValue ||
        (MinDiscount.HasValue && MinDiscount.Value > 0) ||
        (InStockOnly.HasValue && InStockOnly.Value);
}