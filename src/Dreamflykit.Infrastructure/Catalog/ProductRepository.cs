using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Common;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Catalog;

public sealed class ProductRepository : IProductRepository
{
    private readonly DreamflykitDbContext _dbContext;

    public ProductRepository(
        DreamflykitDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ProductListItem>>
        GetActiveProductsAsync(
            CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .Where(product => product.IsActive)
            .OrderBy(product => product.Name)
            .Select(product => new ProductListItem
            {
                ProductId = product.Id,
                DefaultVariantId = _dbContext.ProductVariants
                    .Where(v => v.ProductId == product.Id && v.IsActive)
                    .OrderBy(v => v.SellingPrice)
                    .Select(v => v.Id)
                    .FirstOrDefault(),
                CategoryId = product.CategoryId,
                SubcategoryId = product.SubcategoryId,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,

                ImageUrl = _dbContext.ProductMedia
                    .Where(media =>
                        media.ProductId == product.Id)
                    .OrderBy(media => media.DisplayOrder)
                    .Select(media => media.MediaUrl)
                    .FirstOrDefault(),

                MRP = _dbContext.ProductVariants
                    .Where(variant =>
                        variant.ProductId == product.Id &&
                        variant.IsActive)
                    .OrderBy(variant => variant.SellingPrice)
                    .Select(variant => variant.Mrp)
                    .FirstOrDefault(),

                SellingPrice = _dbContext.ProductVariants
                    .Where(variant =>
                        variant.ProductId == product.Id &&
                        variant.IsActive)
                    .OrderBy(variant => variant.SellingPrice)
                    .Select(variant => variant.SellingPrice)
                    .FirstOrDefault(),

                IsActive = product.IsActive
            })
            .ToListAsync(cancellationToken);
    }


    public async Task<ProductDetails?>
        GetProductDetailsAsync(
            string slug,
            CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return null;
        }

        slug = slug.Trim().ToLowerInvariant();

        return await _dbContext.Products
            .AsNoTracking()
            .Where(product =>
                product.IsActive &&
                product.Slug == slug)
            .Select(product => new ProductDetails
            {
                ProductId = product.Id,

                Name = product.Name,

                Slug = product.Slug,

                Description = product.Description,

                ShortDescription = product.ShortDescription,

                CategoryName = string.Empty,

                CategorySlug = string.Empty,

                SubcategoryName = null,

                SubcategorySlug = null,

                Variants = _dbContext.ProductVariants
                    .Where(variant =>
                        variant.ProductId == product.Id &&
                        variant.IsActive)
                    .OrderBy(variant => variant.SellingPrice)
                    .Select(variant => new ProductVariantItem
                    {
                        VariantId = variant.Id,

                        Sku = variant.Sku,

                        Mrp = variant.Mrp,

                        SellingPrice =
                            variant.SellingPrice,

                        IsActive =
                            variant.IsActive,

                        IsInStock = true
                    })
                    .ToList(),

                Media = _dbContext.ProductMedia
                    .Where(media =>
                        media.ProductId == product.Id)
                    .OrderBy(media => media.DisplayOrder)
                    .Select(media => new ProductMediaItem
                    {
                        MediaId = media.Id,

                        Url = media.MediaUrl,

                        AltText = media.AltText,

                        DisplayOrder =
                            media.DisplayOrder,

                        IsPrimary =
                            media.IsPrimary
                    })
                    .ToList(),

                Attributes = Array.Empty<ProductAttributeItem>(),

                IsActive = product.IsActive,

                IsFeatured = product.IsFeatured
            })
            .FirstOrDefaultAsync(
                cancellationToken);
    }

    public async Task<PagedResult<ProductListItem>> GetProductsAsync(
        ProductListQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var products = _dbContext.Products
            .AsNoTracking()
            .Where(product => product.IsActive);

        if (query.CategoryId.HasValue)
        {
            products = products.Where(p =>
                p.CategoryId == query.CategoryId.Value);
        }

        if (query.SubcategoryId.HasValue)
        {
            products = products.Where(p =>
                p.SubcategoryId == query.SubcategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var searchTerm = query.SearchTerm.ToLowerInvariant();
            products = products.Where(p =>
                p.Name.ToLower().Contains(searchTerm) ||
                p.Description.ToLower().Contains(searchTerm));
        }

        var orderedProducts = query.Sort switch
        {
            ProductSortOption.PriceLowToHigh => products.OrderBy(p => p.Id),
            ProductSortOption.PriceHighToLow => products.OrderByDescending(p => p.Id),
            ProductSortOption.Newest => products.OrderByDescending(p => p.CreatedAtUtc),
            ProductSortOption.NameAscending => products.OrderBy(p => p.Name),
            ProductSortOption.NameDescending => products.OrderByDescending(p => p.Name),
            _ => products.OrderBy(p => p.Name)
        };

        var totalCount = await orderedProducts.CountAsync(cancellationToken);

        var pagedProducts = await orderedProducts
            .Skip(query.Skip)
            .Take(query.NormalizedPageSize)
            .Select(product => new ProductListItem
            {
                ProductId = product.Id,
                DefaultVariantId = _dbContext.ProductVariants
                    .Where(v => v.ProductId == product.Id && v.IsActive)
                    .OrderBy(v => v.SellingPrice)
                    .Select(v => v.Id)
                    .FirstOrDefault(),
                CategoryId = product.CategoryId,
                SubcategoryId = product.SubcategoryId,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                PrimaryImageUrl = _dbContext.ProductMedia
                    .Where(media =>
                        media.ProductId == product.Id &&
                        media.IsPrimary)
                    .Select(media => media.MediaUrl)
                    .FirstOrDefault(),
                ImageUrl = _dbContext.ProductMedia
                    .Where(media =>
                        media.ProductId == product.Id)
                    .OrderBy(media => media.DisplayOrder)
                    .Select(media => media.MediaUrl)
                    .FirstOrDefault(),
                MRP = _dbContext.ProductVariants
                    .Where(variant =>
                        variant.ProductId == product.Id &&
                        variant.IsActive)
                    .OrderBy(variant => variant.SellingPrice)
                    .Select(variant => variant.Mrp)
                    .FirstOrDefault(),
                SellingPrice = _dbContext.ProductVariants
                    .Where(variant =>
                        variant.ProductId == product.Id &&
                        variant.IsActive)
                    .OrderBy(variant => variant.SellingPrice)
                    .Select(variant => variant.SellingPrice)
                    .FirstOrDefault(),
                IsActive = product.IsActive,
                IsFeatured = product.IsFeatured,
                IsInStock = true
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<ProductListItem>
        {
            Items = pagedProducts,
            TotalCount = totalCount,
            Page = query.NormalizedPage,
            PageSize = query.NormalizedPageSize
        };
    }

    public async Task<ProductDetails?> GetDetailsAsync(
        string slug,
        CancellationToken cancellationToken)
    {
        return await GetProductDetailsAsync(slug, cancellationToken);
    }

    public async Task<ProductVariantLookup?> GetActiveVariantAsync(
        Guid productVariantId,
        CancellationToken cancellationToken)
    {
        if (productVariantId == Guid.Empty)
        {
            return null;
        }

        return await _dbContext.ProductVariants
            .AsNoTracking()
            .Where(variant =>
                variant.Id == productVariantId &&
                variant.IsActive)
            .Select(variant => new ProductVariantLookup
            {
                ProductVariantId = variant.Id,
                ProductId = variant.ProductId,
                ProductName = _dbContext.Products
                    .Where(p => p.Id == variant.ProductId)
                    .Select(p => p.Name)
                    .FirstOrDefault() ?? string.Empty,
                VariantName = variant.Name,
                Sku = variant.Sku,
                PrimaryImageUrl = _dbContext.ProductMedia
                    .Where(m =>
                        m.ProductId == variant.ProductId &&
                        m.IsPrimary &&
                        m.IsActive)
                    .Select(m => m.MediaUrl)
                    .FirstOrDefault(),
                SellingPrice = variant.SellingPrice,
                Mrp = variant.Mrp,
                IsActive = variant.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ProductSearchResult> SearchProductsAsync(
        ProductListQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        Guid? categoryId = query.CategoryId;
        string? categoryName = null;
        string? categorySlug = query.CategorySlug;

        if (!categoryId.HasValue && !string.IsNullOrWhiteSpace(categorySlug))
        {
            var cat = await _dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Slug == categorySlug, cancellationToken);
            if (cat != null)
            {
                categoryId = cat.Id;
                categoryName = cat.Name;
            }
        }
        else if (categoryId.HasValue)
        {
            var cat = await _dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoryId.Value, cancellationToken);
            if (cat != null)
            {
                categoryName = cat.Name;
                categorySlug = cat.Slug;
            }
        }

        var baseQuery = _dbContext.Products
            .AsNoTracking()
            .Where(product => product.IsActive);

        if (categoryId.HasValue)
        {
            baseQuery = baseQuery.Where(p => p.CategoryId == categoryId.Value);
        }

        if (query.SubcategoryId.HasValue)
        {
            baseQuery = baseQuery.Where(p => p.SubcategoryId == query.SubcategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var searchTerm = query.SearchTerm.ToLower().Trim();
            baseQuery = baseQuery.Where(p =>
                p.Name.ToLower().Contains(searchTerm) ||
                p.Description.ToLower().Contains(searchTerm) ||
                p.ShortDescription.ToLower().Contains(searchTerm));
        }

        if (query.MinPrice.HasValue)
        {
            baseQuery = baseQuery.Where(p => _dbContext.ProductVariants
                .Any(v => v.ProductId == p.Id && v.IsActive && v.SellingPrice >= query.MinPrice.Value));
        }

        if (query.MaxPrice.HasValue)
        {
            baseQuery = baseQuery.Where(p => _dbContext.ProductVariants
                .Any(v => v.ProductId == p.Id && v.IsActive && v.SellingPrice <= query.MaxPrice.Value));
        }

        if (query.MinDiscount.HasValue && query.MinDiscount.Value > 0)
        {
            baseQuery = baseQuery.Where(p => _dbContext.ProductVariants
                .Any(v => v.ProductId == p.Id && v.IsActive && v.Mrp > 0 &&
                          (((v.Mrp - v.SellingPrice) / v.Mrp) * 100) >= query.MinDiscount.Value));
        }

        var orderedQuery = query.Sort switch
        {
            ProductSortOption.PriceLowToHigh => baseQuery.OrderBy(p => _dbContext.ProductVariants
                .Where(v => v.ProductId == p.Id && v.IsActive)
                .Min(v => (decimal?)v.SellingPrice) ?? decimal.MaxValue),

            ProductSortOption.PriceHighToLow => baseQuery.OrderByDescending(p => _dbContext.ProductVariants
                .Where(v => v.ProductId == p.Id && v.IsActive)
                .Max(v => (decimal?)v.SellingPrice) ?? decimal.MinValue),

            ProductSortOption.DiscountHighToLow => baseQuery.OrderByDescending(p => _dbContext.ProductVariants
                .Where(v => v.ProductId == p.Id && v.IsActive && v.Mrp > 0)
                .Max(v => (decimal?)(((v.Mrp - v.SellingPrice) / v.Mrp) * 100)) ?? 0),

            ProductSortOption.Newest => baseQuery.OrderByDescending(p => p.CreatedAtUtc),

            ProductSortOption.NameAscending => baseQuery.OrderBy(p => p.Name),

            ProductSortOption.NameDescending => baseQuery.OrderByDescending(p => p.Name),

            _ => baseQuery.OrderBy(p => p.DisplayOrder).ThenBy(p => p.Name)
        };

        var totalCount = await orderedQuery.CountAsync(cancellationToken);

        var pagedItems = await orderedQuery
            .Skip(query.Skip)
            .Take(query.NormalizedPageSize)
            .Select(product => new ProductListItem
            {
                ProductId = product.Id,
                DefaultVariantId = _dbContext.ProductVariants
                    .Where(v => v.ProductId == product.Id && v.IsActive)
                    .OrderBy(v => v.SellingPrice)
                    .Select(v => v.Id)
                    .FirstOrDefault(),
                CategoryId = product.CategoryId,
                SubcategoryId = product.SubcategoryId,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                PrimaryImageUrl = _dbContext.ProductMedia
                    .Where(media => media.ProductId == product.Id && media.IsPrimary)
                    .Select(media => media.MediaUrl)
                    .FirstOrDefault() ??
                    _dbContext.ProductMedia
                        .Where(media => media.ProductId == product.Id)
                        .OrderBy(media => media.DisplayOrder)
                        .Select(media => media.MediaUrl)
                        .FirstOrDefault(),
                ImageUrl = _dbContext.ProductMedia
                    .Where(media => media.ProductId == product.Id)
                    .OrderBy(media => media.DisplayOrder)
                    .Select(media => media.MediaUrl)
                    .FirstOrDefault(),
                MRP = _dbContext.ProductVariants
                    .Where(variant => variant.ProductId == product.Id && variant.IsActive)
                    .OrderBy(variant => variant.SellingPrice)
                    .Select(variant => variant.Mrp)
                    .FirstOrDefault(),
                SellingPrice = _dbContext.ProductVariants
                    .Where(variant => variant.ProductId == product.Id && variant.IsActive)
                    .OrderBy(variant => variant.SellingPrice)
                    .Select(variant => variant.SellingPrice)
                    .FirstOrDefault(),
                IsActive = product.IsActive,
                IsFeatured = product.IsFeatured,
                IsInStock = true
            })
            .ToListAsync(cancellationToken);

        // Build Category Facets
        var categoryCountsQuery = _dbContext.Products
            .AsNoTracking()
            .Where(p => p.IsActive);

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var searchTerm = query.SearchTerm.ToLower().Trim();
            categoryCountsQuery = categoryCountsQuery.Where(p =>
                p.Name.ToLower().Contains(searchTerm) ||
                p.Description.ToLower().Contains(searchTerm));
        }

        var availableCategories = await _dbContext.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .Select(c => new CategoryFacet
            {
                CategoryId = c.Id,
                Name = c.Name,
                Slug = c.Slug,
                Count = categoryCountsQuery.Count(p => p.CategoryId == c.Id),
                IsSelected = categoryId.HasValue && categoryId.Value == c.Id
            })
            .Where(cf => cf.Count > 0 || cf.IsSelected)
            .ToListAsync(cancellationToken);

        // Price facet brackets
        var priceRanges = new List<PriceFacetRange>
        {
            new() { Key = "0-500", Label = "Under ₹500", MinPrice = 0, MaxPrice = 500, IsSelected = query.MinPrice == 0 && query.MaxPrice == 500 },
            new() { Key = "500-1000", Label = "₹500 - ₹1,000", MinPrice = 500, MaxPrice = 1000, IsSelected = query.MinPrice == 500 && query.MaxPrice == 1000 },
            new() { Key = "1000-2000", Label = "₹1,000 - ₹2,000", MinPrice = 1000, MaxPrice = 2000, IsSelected = query.MinPrice == 1000 && query.MaxPrice == 2000 },
            new() { Key = "2000-5000", Label = "₹2,000 - ₹5,000", MinPrice = 2000, MaxPrice = 5000, IsSelected = query.MinPrice == 2000 && query.MaxPrice == 5000 },
            new() { Key = "5000+", Label = "₹5,000 and Above", MinPrice = 5000, MaxPrice = null, IsSelected = query.MinPrice == 5000 && query.MaxPrice == null }
        };

        // Discount facet options
        var discounts = new List<DiscountFacet>
        {
            new() { MinDiscount = 40, Label = "40% or more", IsSelected = query.MinDiscount == 40 },
            new() { MinDiscount = 30, Label = "30% or more", IsSelected = query.MinDiscount == 30 },
            new() { MinDiscount = 20, Label = "20% or more", IsSelected = query.MinDiscount == 20 },
            new() { MinDiscount = 10, Label = "10% or more", IsSelected = query.MinDiscount == 10 }
        };

        return new ProductSearchResult
        {
            Items = pagedItems,
            TotalCount = totalCount,
            Page = query.NormalizedPage,
            PageSize = query.NormalizedPageSize,
            Categories = availableCategories,
            PriceRanges = priceRanges,
            Discounts = discounts,
            CurrentCategoryName = categoryName,
            CurrentCategorySlug = categorySlug
        };
    }

    public async Task<SearchAutocompleteResult> GetAutocompleteSuggestionsAsync(
        string query,
        CancellationToken cancellationToken)
    {
        var term = query.Trim().ToLowerInvariant();

        var products = await _dbContext.Products
            .AsNoTracking()
            .Where(p => p.IsActive && (p.Name.ToLower().Contains(term) || p.Slug.Contains(term)))
            .OrderBy(p => p.DisplayOrder)
            .ThenBy(p => p.Name)
            .Take(5)
            .Select(p => new AutocompleteProductItem
            {
                ProductId = p.Id,
                Name = p.Name,
                Slug = p.Slug,
                CategoryName = _dbContext.Categories
                    .Where(c => c.Id == p.CategoryId)
                    .Select(c => c.Name)
                    .FirstOrDefault() ?? string.Empty,
                ImageUrl = _dbContext.ProductMedia
                    .Where(m => m.ProductId == p.Id && m.IsActive)
                    .OrderBy(m => m.DisplayOrder)
                    .Select(m => m.MediaUrl)
                    .FirstOrDefault(),
                SellingPrice = _dbContext.ProductVariants
                    .Where(v => v.ProductId == p.Id && v.IsActive)
                    .OrderBy(v => v.SellingPrice)
                    .Select(v => v.SellingPrice)
                    .FirstOrDefault(),
                Mrp = _dbContext.ProductVariants
                    .Where(v => v.ProductId == p.Id && v.IsActive)
                    .OrderBy(v => v.SellingPrice)
                    .Select(v => v.Mrp)
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        var categories = await _dbContext.Categories
            .AsNoTracking()
            .Where(c => c.IsActive && c.Name.ToLower().Contains(term))
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .Take(3)
            .Select(c => new AutocompleteCategoryItem
            {
                CategoryId = c.Id,
                Name = c.Name,
                Slug = c.Slug,
                ProductCount = _dbContext.Products.Count(p => p.CategoryId == c.Id && p.IsActive)
            })
            .ToListAsync(cancellationToken);

        var suggestions = products
            .Select(p => p.Name)
            .Distinct()
            .Take(4)
            .ToList();

        return new SearchAutocompleteResult
        {
            Query = query,
            Products = products,
            Categories = categories,
            Suggestions = suggestions
        };
    }
}