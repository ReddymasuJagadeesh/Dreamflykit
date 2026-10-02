using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Common;

namespace Dreamflykit.Application.Catalog.Interfaces;

public interface IProductService
{
    Task<PagedResult<ProductListItem>> GetProductsAsync(
        ProductListQuery query,
        CancellationToken cancellationToken);

    Task<ProductDetails?> GetDetailsAsync(
        string slug,
        CancellationToken cancellationToken);

    Task<ProductSearchResult> SearchProductsAsync(
        ProductListQuery query,
        CancellationToken cancellationToken);

    Task<SearchAutocompleteResult> GetAutocompleteSuggestionsAsync(
        string query,
        CancellationToken cancellationToken);
}