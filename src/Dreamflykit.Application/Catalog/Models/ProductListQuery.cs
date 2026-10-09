namespace Dreamflykit.Application.Catalog.Models;

public sealed class ProductListQuery
{
    public Guid? CategoryId { get; init; }

    public Guid? SubcategoryId { get; init; }

    public string? SearchTerm { get; init; }

    public decimal? MinPrice { get; init; }

    public decimal? MaxPrice { get; init; }

    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 24;

    public ProductSortOption Sort { get; init; }
        = ProductSortOption.Relevance;

    public string? CategorySlug { get; init; }

    public string? SubcategorySlug { get; init; }

    public bool? InStockOnly { get; init; }

    public int? MinDiscount { get; init; }

    public IReadOnlyDictionary<string, IReadOnlyList<string>> Attributes { get; init; }
        = new Dictionary<string, IReadOnlyList<string>>();

    public int Skip
    {
        get
        {
            var page = Page < 1 ? 1 : Page;
            var pageSize = PageSize is < 1 or > 100 ? 24 : PageSize;

            return (page - 1) * pageSize;
        }
    }

    public int NormalizedPage =>
        Page < 1 ? 1 : Page;

    public int NormalizedPageSize =>
        PageSize is < 1 or > 100 ? 24 : PageSize;
}

public enum ProductSortOption
{
    Relevance = 1,
    PriceLowToHigh = 2,
    PriceHighToLow = 3,
    Newest = 4,
    NameAscending = 5,
    NameDescending = 6,
    DiscountHighToLow = 7
}