using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.Categories.Models;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Categories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly DreamflykitDbContext _dbContext;

    public CategoryRepository(
        DreamflykitDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<CategoryListItem>>
        GetActiveCategoriesAsync(
            CancellationToken cancellationToken)
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .Where(category => category.IsActive)
            .OrderBy(category => category.DisplayOrder)
            .ThenBy(category => category.Name)
            .Select(category => new CategoryListItem
            {
                CategoryId = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                DisplayOrder = category.DisplayOrder,

                ProductCount = _dbContext.Products.Count(
                    product =>
                        product.CategoryId == category.Id &&
                        product.IsActive)
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<CategoryDetails?>
        GetCategoryDetailsAsync(
            string slug,
            CancellationToken cancellationToken)
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .Where(category =>
                category.IsActive &&
                category.Slug == slug)
            .Select(category => new CategoryDetails
            {
                CategoryId = category.Id,
                Name = category.Name,
                Slug = category.Slug,

                Subcategories = _dbContext.Subcategories
                    .AsNoTracking()
                    .Where(subcategory =>
                        subcategory.CategoryId == category.Id &&
                        subcategory.IsActive)
                    .OrderBy(subcategory => subcategory.DisplayOrder)
                    .ThenBy(subcategory => subcategory.Name)
                    .Select(subcategory => new SubcategoryListItem
                    {
                        SubcategoryId = subcategory.Id,
                        CategoryId = subcategory.CategoryId,
                        Name = subcategory.Name,
                        Slug = subcategory.Slug,
                        DisplayOrder = subcategory.DisplayOrder,

                        ProductCount = _dbContext.Products.Count(
                            product =>
                                product.SubcategoryId ==
                                subcategory.Id &&
                                product.IsActive)
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<SubcategoryListItem>>
        GetActiveSubcategoriesAsync(
            Guid categoryId,
            CancellationToken cancellationToken)
    {
        return await _dbContext.Subcategories
            .AsNoTracking()
            .Where(subcategory =>
                subcategory.CategoryId == categoryId &&
                subcategory.IsActive)
            .OrderBy(subcategory => subcategory.DisplayOrder)
            .ThenBy(subcategory => subcategory.Name)
            .Select(subcategory => new SubcategoryListItem
            {
                SubcategoryId = subcategory.Id,
                CategoryId = subcategory.CategoryId,
                Name = subcategory.Name,
                Slug = subcategory.Slug,
                DisplayOrder = subcategory.DisplayOrder,

                ProductCount = _dbContext.Products.Count(
                    product =>
                        product.SubcategoryId ==
                        subcategory.Id &&
                        product.IsActive)
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<SubcategoryListItem?>
        GetSubcategoryAsync(
            string categorySlug,
            string subcategorySlug,
            CancellationToken cancellationToken)
    {
        return await (
            from category in
                _dbContext.Categories.AsNoTracking()

            join subcategory in
                _dbContext.Subcategories.AsNoTracking()
                on category.Id equals subcategory.CategoryId

            where category.IsActive
                  && subcategory.IsActive
                  && category.Slug == categorySlug
                  && subcategory.Slug == subcategorySlug

            select new SubcategoryListItem
            {
                SubcategoryId = subcategory.Id,
                CategoryId = category.Id,
                Name = subcategory.Name,
                Slug = subcategory.Slug,
                DisplayOrder = subcategory.DisplayOrder,

                ProductCount = _dbContext.Products.Count(
                    product =>
                        product.SubcategoryId ==
                        subcategory.Id &&
                        product.IsActive)
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}