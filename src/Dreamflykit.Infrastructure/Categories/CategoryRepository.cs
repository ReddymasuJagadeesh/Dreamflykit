using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.Categories.Models;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Categories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly DreamflykitDbContext _dbContext;
    private readonly IMediaUrlService _urlService;

    public CategoryRepository(
        DreamflykitDbContext dbContext,
        IMediaUrlService urlService)
    {
        _dbContext = dbContext;
        _urlService = urlService;
    }

    private async Task<Dictionary<Guid, string>> GetAssetKeysAsync(
        IEnumerable<Guid?> assetIds,
        CancellationToken cancellationToken)
    {
        var validIds = assetIds.Where(id => id.HasValue).Select(id => id!.Value).Distinct().ToList();
        if (validIds.Count == 0) return new Dictionary<Guid, string>();

        return await _dbContext.MediaAssets
            .AsNoTracking()
            .Where(a => validIds.Contains(a.Id))
            .ToDictionaryAsync(a => a.Id, a => a.ObjectKey, cancellationToken);
    }

    public async Task<IReadOnlyList<CategoryListItem>>
        GetActiveCategoriesAsync(
            CancellationToken cancellationToken)
    {
        var rawCats = await _dbContext.Categories
            .AsNoTracking()
            .Where(category => category.IsActive)
            .OrderBy(category => category.DisplayOrder)
            .ThenBy(category => category.Name)
            .Select(category => new
            {
                CategoryId = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                DisplayOrder = category.DisplayOrder,
                ImageMediaAssetId = category.ImageMediaAssetId,
                ProductCount = _dbContext.Products.Count(
                    product =>
                        product.CategoryId == category.Id &&
                        product.IsActive)
            })
            .ToListAsync(cancellationToken);

        var assetMap = await GetAssetKeysAsync(rawCats.Select(c => c.ImageMediaAssetId), cancellationToken);

        return rawCats.Select(c => new CategoryListItem
        {
            CategoryId = c.CategoryId,
            Name = c.Name,
            Slug = c.Slug,
            DisplayOrder = c.DisplayOrder,
            ProductCount = c.ProductCount,
            ImageUrl = c.ImageMediaAssetId.HasValue && assetMap.TryGetValue(c.ImageMediaAssetId.Value, out var key)
                ? _urlService.GetThumbnailUrl(key)
                : null
        }).ToList();
    }

    public async Task<CategoryDetails?>
        GetCategoryDetailsAsync(
            string slug,
            CancellationToken cancellationToken)
    {
        var cat = await _dbContext.Categories
            .AsNoTracking()
            .Where(category =>
                category.IsActive &&
                category.Slug == slug)
            .Select(category => new
            {
                CategoryId = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                ImageMediaAssetId = category.ImageMediaAssetId,
                Subcategories = _dbContext.Subcategories
                    .AsNoTracking()
                    .Where(subcategory =>
                        subcategory.CategoryId == category.Id &&
                        subcategory.IsActive)
                    .OrderBy(subcategory => subcategory.DisplayOrder)
                    .ThenBy(subcategory => subcategory.Name)
                    .Select(subcategory => new
                    {
                        SubcategoryId = subcategory.Id,
                        CategoryId = subcategory.CategoryId,
                        Name = subcategory.Name,
                        Slug = subcategory.Slug,
                        DisplayOrder = subcategory.DisplayOrder,
                        ImageMediaAssetId = subcategory.ImageMediaAssetId,
                        ProductCount = _dbContext.Products.Count(
                            product =>
                                product.SubcategoryId ==
                                subcategory.Id &&
                                product.IsActive)
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (cat is null) return null;

        var allAssetIds = new List<Guid?>();
        if (cat.ImageMediaAssetId.HasValue) allAssetIds.Add(cat.ImageMediaAssetId);
        allAssetIds.AddRange(cat.Subcategories.Select(s => s.ImageMediaAssetId));

        var assetMap = await GetAssetKeysAsync(allAssetIds, cancellationToken);

        return new CategoryDetails
        {
            CategoryId = cat.CategoryId,
            Name = cat.Name,
            Slug = cat.Slug,
            ImageUrl = cat.ImageMediaAssetId.HasValue && assetMap.TryGetValue(cat.ImageMediaAssetId.Value, out var catKey)
                ? _urlService.GetCardUrl(catKey)
                : null,
            Subcategories = cat.Subcategories.Select(s => new SubcategoryListItem
            {
                SubcategoryId = s.SubcategoryId,
                CategoryId = s.CategoryId,
                Name = s.Name,
                Slug = s.Slug,
                DisplayOrder = s.DisplayOrder,
                ProductCount = s.ProductCount,
                ImageUrl = s.ImageMediaAssetId.HasValue && assetMap.TryGetValue(s.ImageMediaAssetId.Value, out var subKey)
                    ? _urlService.GetThumbnailUrl(subKey)
                    : null
            }).ToList()
        };
    }

    public async Task<IReadOnlyList<SubcategoryListItem>>
        GetActiveSubcategoriesAsync(
            Guid categoryId,
            CancellationToken cancellationToken)
    {
        var rawSubs = await _dbContext.Subcategories
            .AsNoTracking()
            .Where(subcategory =>
                subcategory.CategoryId == categoryId &&
                subcategory.IsActive)
            .OrderBy(subcategory => subcategory.DisplayOrder)
            .ThenBy(subcategory => subcategory.Name)
            .Select(subcategory => new
            {
                SubcategoryId = subcategory.Id,
                CategoryId = subcategory.CategoryId,
                Name = subcategory.Name,
                Slug = subcategory.Slug,
                DisplayOrder = subcategory.DisplayOrder,
                ImageMediaAssetId = subcategory.ImageMediaAssetId,
                ProductCount = _dbContext.Products.Count(
                    product =>
                        product.SubcategoryId ==
                        subcategory.Id &&
                        product.IsActive)
            })
            .ToListAsync(cancellationToken);

        var assetMap = await GetAssetKeysAsync(rawSubs.Select(s => s.ImageMediaAssetId), cancellationToken);

        return rawSubs.Select(s => new SubcategoryListItem
        {
            SubcategoryId = s.SubcategoryId,
            CategoryId = s.CategoryId,
            Name = s.Name,
            Slug = s.Slug,
            DisplayOrder = s.DisplayOrder,
            ProductCount = s.ProductCount,
            ImageUrl = s.ImageMediaAssetId.HasValue && assetMap.TryGetValue(s.ImageMediaAssetId.Value, out var subKey)
                ? _urlService.GetThumbnailUrl(subKey)
                : null
        }).ToList();
    }

    public async Task<SubcategoryListItem?>
        GetSubcategoryAsync(
            string categorySlug,
            string subcategorySlug,
            CancellationToken cancellationToken)
    {
        var sub = await (
            from category in
                _dbContext.Categories.AsNoTracking()

            join subcategory in
                _dbContext.Subcategories.AsNoTracking()
                on category.Id equals subcategory.CategoryId

            where category.IsActive
                  && subcategory.IsActive
                  && category.Slug == categorySlug
                  && subcategory.Slug == subcategorySlug

            select new
            {
                SubcategoryId = subcategory.Id,
                CategoryId = category.Id,
                Name = subcategory.Name,
                Slug = subcategory.Slug,
                DisplayOrder = subcategory.DisplayOrder,
                ImageMediaAssetId = subcategory.ImageMediaAssetId,
                ProductCount = _dbContext.Products.Count(
                    product =>
                        product.SubcategoryId ==
                        subcategory.Id &&
                        product.IsActive)
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (sub is null) return null;

        var assetMap = await GetAssetKeysAsync(new[] { sub.ImageMediaAssetId }, cancellationToken);

        return new SubcategoryListItem
        {
            SubcategoryId = sub.SubcategoryId,
            CategoryId = sub.CategoryId,
            Name = sub.Name,
            Slug = sub.Slug,
            DisplayOrder = sub.DisplayOrder,
            ProductCount = sub.ProductCount,
            ImageUrl = sub.ImageMediaAssetId.HasValue && assetMap.TryGetValue(sub.ImageMediaAssetId.Value, out var key)
                ? _urlService.GetThumbnailUrl(key)
                : null
        };
    }
}