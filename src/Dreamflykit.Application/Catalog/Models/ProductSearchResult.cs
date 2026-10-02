namespace Dreamflykit.Application.Catalog.Models;

public sealed class CategoryFacet
{
    public Guid CategoryId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public int Count { get; init; }
    public bool IsSelected { get; init; }
}

public sealed class PriceFacetRange
{
    public string Key { get; init; } = string.Empty;
    public string Label { get; init; } = string.Empty;
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public int Count { get; init; }
    public bool IsSelected { get; init; }
}

public sealed class DiscountFacet
{
    public int MinDiscount { get; init; }
    public string Label { get; init; } = string.Empty;
    public int Count { get; init; }
    public bool IsSelected { get; init; }
}

public sealed class ProductSearchResult
{
    public IReadOnlyList<ProductListItem> Items { get; init; } = Array.Empty<ProductListItem>();
    public int TotalCount { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 24;

    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;

    public IReadOnlyList<CategoryFacet> Categories { get; init; } = Array.Empty<CategoryFacet>();
    public IReadOnlyList<PriceFacetRange> PriceRanges { get; init; } = Array.Empty<PriceFacetRange>();
    public IReadOnlyList<DiscountFacet> Discounts { get; init; } = Array.Empty<DiscountFacet>();

    public decimal MinPriceBound { get; init; }
    public decimal MaxPriceBound { get; init; }
    public string? CurrentCategoryName { get; init; }
    public string? CurrentCategorySlug { get; init; }
}
