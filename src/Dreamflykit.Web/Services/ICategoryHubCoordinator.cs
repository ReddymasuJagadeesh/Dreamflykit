using Dreamflykit.Web.Models.Products;

namespace Dreamflykit.Web.Services;

/// <summary>
/// Coordinates category landing pages, subcategory dispatching, brand showcases, and catalog query assembly.
/// Decouples MVC controllers from domain-specific hub services and presentation view models.
/// </summary>
public interface ICategoryHubCoordinator
{
    Task<ProductListViewModel> CoordinateCategoryLandingAsync(
        CategoryLandingContext context,
        CancellationToken cancellationToken = default);
}

