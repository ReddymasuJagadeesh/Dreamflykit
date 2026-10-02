using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Common;

namespace Dreamflykit.Application.Catalog.Services;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<ProductListItem>> GetProductsAsync(
        ProductListQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var normalizedQuery = new ProductListQuery
        {
            CategoryId = query.CategoryId,
            SubcategoryId = query.SubcategoryId,
            SearchTerm = string.IsNullOrWhiteSpace(query.SearchTerm)
                ? null
                : query.SearchTerm.Trim(),
            MinPrice = query.MinPrice,
            MaxPrice = query.MaxPrice,
            Page = query.NormalizedPage,
            PageSize = query.NormalizedPageSize,
            Sort = query.Sort,
            Attributes = query.Attributes
        };

        if (normalizedQuery.MinPrice.HasValue &&
            normalizedQuery.MinPrice.Value < 0)
        {
            throw new ArgumentException(
                "Minimum price cannot be negative.",
                nameof(query));
        }

        if (normalizedQuery.MaxPrice.HasValue &&
            normalizedQuery.MaxPrice.Value < 0)
        {
            throw new ArgumentException(
                "Maximum price cannot be negative.",
                nameof(query));
        }

        if (normalizedQuery.MinPrice.HasValue &&
            normalizedQuery.MaxPrice.HasValue &&
            normalizedQuery.MinPrice.Value >
            normalizedQuery.MaxPrice.Value)
        {
            throw new ArgumentException(
                "Minimum price cannot be greater than maximum price.",
                nameof(query));
        }

        return await _repository.GetProductsAsync(
            normalizedQuery,
            cancellationToken);
    }

    public async Task<ProductDetails?> GetDetailsAsync(
        string slug,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new ArgumentException(
                "Product slug is required.",
                nameof(slug));
        }

        var normalizedSlug =
            slug.Trim().ToLowerInvariant();

        return await _repository.GetDetailsAsync(
            normalizedSlug,
            cancellationToken);
    }

    public async Task<ProductSearchResult> SearchProductsAsync(
        ProductListQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var normalizedQuery = new ProductListQuery
        {
            CategoryId = query.CategoryId,
            SubcategoryId = query.SubcategoryId,
            CategorySlug = string.IsNullOrWhiteSpace(query.CategorySlug) ? null : query.CategorySlug.Trim().ToLowerInvariant(),
            SearchTerm = string.IsNullOrWhiteSpace(query.SearchTerm) ? null : query.SearchTerm.Trim(),
            MinPrice = query.MinPrice is < 0 ? null : query.MinPrice,
            MaxPrice = query.MaxPrice is < 0 ? null : query.MaxPrice,
            InStockOnly = query.InStockOnly,
            MinDiscount = query.MinDiscount is < 0 ? null : query.MinDiscount,
            Page = query.NormalizedPage,
            PageSize = query.NormalizedPageSize,
            Sort = query.Sort,
            Attributes = query.Attributes
        };

        if (normalizedQuery.MinPrice.HasValue &&
            normalizedQuery.MaxPrice.HasValue &&
            normalizedQuery.MinPrice.Value > normalizedQuery.MaxPrice.Value)
        {
            // Swap if min > max to protect against inverted filters
            normalizedQuery = new ProductListQuery
            {
                CategoryId = normalizedQuery.CategoryId,
                SubcategoryId = normalizedQuery.SubcategoryId,
                CategorySlug = normalizedQuery.CategorySlug,
                SearchTerm = normalizedQuery.SearchTerm,
                MinPrice = normalizedQuery.MaxPrice,
                MaxPrice = normalizedQuery.MinPrice,
                InStockOnly = normalizedQuery.InStockOnly,
                MinDiscount = normalizedQuery.MinDiscount,
                Page = normalizedQuery.Page,
                PageSize = normalizedQuery.PageSize,
                Sort = normalizedQuery.Sort,
                Attributes = normalizedQuery.Attributes
            };
        }

        return await _repository.SearchProductsAsync(
            normalizedQuery,
            cancellationToken);
    }

    public async Task<SearchAutocompleteResult> GetAutocompleteSuggestionsAsync(
        string query,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return new SearchAutocompleteResult();
        }

        var sanitized = query.Trim();
        if (sanitized.Length < 2)
        {
            return new SearchAutocompleteResult { Query = sanitized };
        }

        return await _repository.GetAutocompleteSuggestionsAsync(
            sanitized,
            cancellationToken);
    }
}