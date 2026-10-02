using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.Categories.Models;

namespace Dreamflykit.Application.Categories.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(
        ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CategoryListItem>>
        GetActiveCategoriesAsync(
            CancellationToken cancellationToken)
    {
        return await _repository.GetActiveCategoriesAsync(
            cancellationToken);
    }

    public async Task<CategoryDetails?>
        GetCategoryDetailsAsync(
            string slug,
            CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return null;
        }

        return await _repository.GetCategoryDetailsAsync(
            slug.Trim().ToLowerInvariant(),
            cancellationToken);
    }

    public async Task<IReadOnlyList<SubcategoryListItem>>
        GetActiveSubcategoriesAsync(
            Guid categoryId,
            CancellationToken cancellationToken)
    {
        if (categoryId == Guid.Empty)
        {
            return Array.Empty<SubcategoryListItem>();
        }

        return await _repository.GetActiveSubcategoriesAsync(
            categoryId,
            cancellationToken);
    }

    public async Task<SubcategoryListItem?>
        GetSubcategoryAsync(
            string categorySlug,
            string subcategorySlug,
            CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(categorySlug) ||
            string.IsNullOrWhiteSpace(subcategorySlug))
        {
            return null;
        }

        return await _repository.GetSubcategoryAsync(
            categorySlug.Trim().ToLowerInvariant(),
            subcategorySlug.Trim().ToLowerInvariant(),
            cancellationToken);
    }
}