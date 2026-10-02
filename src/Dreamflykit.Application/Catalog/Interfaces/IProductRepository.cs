using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Common;

namespace Dreamflykit.Application.Catalog.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<ProductListItem>> GetActiveProductsAsync(
        CancellationToken cancellationToken);

    Task<ProductDetails?> GetProductDetailsAsync(
        string slug,
        CancellationToken cancellationToken);

    Task<PagedResult<ProductListItem>> GetProductsAsync(
        ProductListQuery query,
        CancellationToken cancellationToken);

    Task<ProductDetails?> GetDetailsAsync(
        string slug,
        CancellationToken cancellationToken);

    /// <summary>
    /// Returns variant details for add-to-cart validation.
    /// Returns null if the variant does not exist or is not active.
    /// </summary>
    Task<ProductVariantLookup?> GetActiveVariantAsync(
        Guid productVariantId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Performs faceted search with dynamic category, price, and discount facets.
    /// </summary>
    Task<ProductSearchResult> SearchProductsAsync(
        ProductListQuery query,
        CancellationToken cancellationToken);

    /// <summary>
    /// Returns live search suggestions, matching products, and categories for the search input dropdown.
    /// </summary>
    Task<SearchAutocompleteResult> GetAutocompleteSuggestionsAsync(
        string query,
        CancellationToken cancellationToken);
}