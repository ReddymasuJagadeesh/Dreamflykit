using Dreamflykit.Application.Categories.Models;

namespace Dreamflykit.Application.Categories.Interfaces;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryListItem>>
        GetActiveCategoriesAsync(
            CancellationToken cancellationToken);

    Task<CategoryDetails?>
        GetCategoryDetailsAsync(
            string slug,
            CancellationToken cancellationToken);

    Task<IReadOnlyList<SubcategoryListItem>>
        GetActiveSubcategoriesAsync(
            Guid categoryId,
            CancellationToken cancellationToken);

    Task<SubcategoryListItem?>
        GetSubcategoryAsync(
            string categorySlug,
            string subcategorySlug,
            CancellationToken cancellationToken);
}