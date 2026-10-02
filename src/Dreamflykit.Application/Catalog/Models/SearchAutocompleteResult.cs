namespace Dreamflykit.Application.Catalog.Models;

public sealed class AutocompleteProductItem
{
    public Guid ProductId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string? ImageUrl { get; init; }
    public decimal SellingPrice { get; init; }
    public decimal Mrp { get; init; }
    public string CategoryName { get; init; } = string.Empty;
}

public sealed class AutocompleteCategoryItem
{
    public Guid CategoryId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public int ProductCount { get; init; }
}

public sealed class SearchAutocompleteResult
{
    public string Query { get; init; } = string.Empty;
    public IReadOnlyList<AutocompleteProductItem> Products { get; init; } = Array.Empty<AutocompleteProductItem>();
    public IReadOnlyList<AutocompleteCategoryItem> Categories { get; init; } = Array.Empty<AutocompleteCategoryItem>();
    public IReadOnlyList<string> Suggestions { get; init; } = Array.Empty<string>();
}
