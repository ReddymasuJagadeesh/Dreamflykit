using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.Categories.Models;
using Dreamflykit.Application.StudyHub.Interfaces;
using Dreamflykit.Application.StudyHub.Models;
using Dreamflykit.Web.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

public sealed class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IStudyHubService _studyHubService;
    private readonly Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService _fashionHubService;
    private readonly Dreamflykit.Application.HealthHub.Interfaces.IHealthHubService _healthHubService;

    public ProductsController(
        IProductService productService,
        ICategoryService categoryService,
        IStudyHubService studyHubService,
        Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService fashionHubService,
        Dreamflykit.Application.HealthHub.Interfaces.IHealthHubService healthHubService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _studyHubService = studyHubService;
        _fashionHubService = fashionHubService;
        _healthHubService = healthHubService;
    }

    [HttpGet("/products")]
    [HttpGet("/products/search")]
    [HttpGet("/category/{category}")]
    [HttpGet("/category/{category}/{subcategorySlug}")]
    [HttpGet("/categories/{category}")]
    [HttpGet("/categories/{category}/{subcategorySlug}")]
    public async Task<IActionResult> Index(
        [FromQuery(Name = "q")] string? q,
        [FromQuery] string? search,
        [FromQuery] Guid? categoryId,
        [FromRoute] string? category,
        [FromQuery(Name = "category")] string? queryCategory,
        [FromRoute] string? subcategorySlug,
        [FromQuery] Guid? subcategoryId,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] bool? inStockOnly,
        [FromQuery] int? minDiscount,
        [FromQuery] ProductSortOption sort = ProductSortOption.Relevance,
        [FromQuery] string? view = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 24,
        CancellationToken cancellationToken = default)
    {
        var effectiveCategory = !string.IsNullOrWhiteSpace(category)
            ? category.Trim().ToLowerInvariant()
            : (!string.IsNullOrWhiteSpace(queryCategory) ? queryCategory.Trim().ToLowerInvariant() : null);

        var isStudyCategory = string.Equals(effectiveCategory, "study-essentials", StringComparison.OrdinalIgnoreCase) ||
                              string.Equals(effectiveCategory, "study_essentials", StringComparison.OrdinalIgnoreCase);

        var isFashionCategory = string.Equals(effectiveCategory, "fashion", StringComparison.OrdinalIgnoreCase);

        var isHealthCategory = string.Equals(effectiveCategory, "health-care", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "healthcare", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "health_care", StringComparison.OrdinalIgnoreCase);

        CategoryDetails? catDetails = null;
        string? currentSubcategoryName = null;
        string? currentSubcategorySlug = subcategorySlug;

        if (!string.IsNullOrWhiteSpace(effectiveCategory))
        {
            catDetails = await _categoryService.GetCategoryDetailsAsync(effectiveCategory, cancellationToken);
            if (catDetails != null)
            {
                categoryId = catDetails.CategoryId;

                if (!string.IsNullOrWhiteSpace(subcategorySlug))
                {
                    var matchingSub = catDetails.Subcategories
                        .FirstOrDefault(s => s.Slug.Equals(subcategorySlug, StringComparison.OrdinalIgnoreCase));
                    if (matchingSub != null)
                    {
                        subcategoryId = matchingSub.SubcategoryId;
                        currentSubcategoryName = matchingSub.Name;
                    }
                }
            }
        }

        if (subcategoryId.HasValue && string.IsNullOrWhiteSpace(currentSubcategoryName) && catDetails != null)
        {
            var matchingSub = catDetails.Subcategories.FirstOrDefault(s => s.SubcategoryId == subcategoryId.Value);
            if (matchingSub != null)
            {
                currentSubcategoryName = matchingSub.Name;
                currentSubcategorySlug = matchingSub.Slug;
            }
        }

        var effectiveSearch = !string.IsNullOrWhiteSpace(q)
            ? q.Trim()
            : (!string.IsNullOrWhiteSpace(search) ? search.Trim() : null);

        var query = new ProductListQuery
        {
            SearchTerm = effectiveSearch,
            CategoryId = categoryId,
            CategorySlug = effectiveCategory,
            SubcategoryId = subcategoryId,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            InStockOnly = inStockOnly,
            MinDiscount = minDiscount,
            Sort = sort,
            Page = page,
            PageSize = pageSize
        };

        var result = await _productService.SearchProductsAsync(
            query,
            cancellationToken);

        var model = new ProductListViewModel
        {
            Products = result.Items,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages,
            HasPreviousPage = result.HasPreviousPage,
            HasNextPage = result.HasNextPage,
            SearchTerm = effectiveSearch,
            CategoryId = categoryId,
            CategorySlug = result.CurrentCategorySlug ?? effectiveCategory,
            CategoryName = result.CurrentCategoryName ?? catDetails?.Name,
            CategoryImageUrl = catDetails?.ImageUrl,
            CategoryDescription = !string.IsNullOrWhiteSpace(catDetails?.Name)
                ? $"Explore premium {catDetails.Name} student essentials, verified university supplies, and travel-ready gear curated for international students."
                : null,
            SubcategoryId = subcategoryId,
            CurrentSubcategoryName = currentSubcategoryName,
            CurrentSubcategorySlug = currentSubcategorySlug,
            Subcategories = catDetails?.Subcategories ?? Array.Empty<SubcategoryListItem>(),
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            InStockOnly = inStockOnly,
            MinDiscount = minDiscount,
            Sort = sort,
            Categories = result.Categories,
            PriceRanges = result.PriceRanges,
            Discounts = result.Discounts,
            ViewMode = !string.IsNullOrWhiteSpace(view)
                ? view.Trim().ToLowerInvariant()
                : ((isStudyCategory || isFashionCategory || isHealthCategory) && !subcategoryId.HasValue && string.IsNullOrWhiteSpace(currentSubcategorySlug) ? "hub" : "categories"),
            StudyHub = isStudyCategory
                        ? await _studyHubService.GetConfigAsync(cancellationToken)
                        : null,
            FashionHub = isFashionCategory
                        ? await _fashionHubService.GetConfigAsync(cancellationToken)
                        : null,
            HealthHub = isHealthCategory
                        ? await _healthHubService.GetConfigAsync(cancellationToken)
                        : null
        };

        var isAjax = string.Equals(Request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(Request.Query["partial"], "true", StringComparison.OrdinalIgnoreCase);

        if (isAjax)
        {
            if (isStudyCategory)
            {
                return PartialView("_StudyEssentialsProductsListPartial", model);
            }
            return PartialView("_CatalogProductsListPartial", model);
        }

        return View(model);
    }

    [HttpGet("/products/autocomplete")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> Autocomplete(
        [FromQuery] string? q,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(q) || q.Trim().Length < 2)
        {
            return Json(new SearchAutocompleteResult { Query = q ?? string.Empty });
        }

        var result = await _productService.GetAutocompleteSuggestionsAsync(
            q.Trim(),
            cancellationToken);

        return Json(result);
    }

    [HttpGet("/product/{slug}")]
    public async Task<IActionResult> Details(
        string slug,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var product =
            await _productService.GetDetailsAsync(
                slug,
                cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        var similarResult = await _productService.SearchProductsAsync(
            new ProductListQuery
            {
                CategorySlug = product.CategorySlug,
                SubcategorySlug = product.SubcategorySlug,
                Page = 1,
                PageSize = 8
            },
            cancellationToken);

        var similarItems = similarResult.Items
            .Where(p => p.ProductId != product.ProductId)
            .Take(5)
            .ToList();

        if (similarItems.Count < 4 && !string.IsNullOrWhiteSpace(product.CategorySlug))
        {
            var fallbackResult = await _productService.SearchProductsAsync(
                new ProductListQuery
                {
                    CategorySlug = product.CategorySlug,
                    Page = 1,
                    PageSize = 8
                },
                cancellationToken);

            var fallbackItems = fallbackResult.Items
                .Where(p => p.ProductId != product.ProductId && !similarItems.Any(s => s.ProductId == p.ProductId))
                .Take(5 - similarItems.Count);

            similarItems.AddRange(fallbackItems);
        }

        var model = new ProductDetailsViewModel
        {
            ProductId = product.ProductId,
            SimilarProducts = similarItems,
            Name = product.Name,
            Slug = product.Slug,
            Description = product.Description,
            ShortDescription = product.ShortDescription,
            CategoryName = product.CategoryName,
            CategorySlug = product.CategorySlug,
            SubcategoryName = product.SubcategoryName,
            SubcategorySlug = product.SubcategorySlug,
            IsActive = product.IsActive,
            IsFeatured = product.IsFeatured,

            Variants = product.Variants
                .Select(variant => new ProductVariantViewModel
                {
                    VariantId = variant.VariantId,
                    Sku = variant.Sku,
                    Mrp = variant.Mrp,
                    SellingPrice = variant.SellingPrice,
                    IsActive = variant.IsActive,
                    IsInStock = variant.IsInStock
                })
                .ToList(),

            Media = product.Media
                .Select(media => new ProductMediaViewModel
                {
                    MediaId = media.MediaId,
                    Url = media.Url,
                    AltText = media.AltText,
                    DisplayOrder = media.DisplayOrder,
                    IsPrimary = media.IsPrimary
                })
                .OrderByDescending(media => media.IsPrimary)
                .ThenBy(media => media.DisplayOrder)
                .ToList(),

            Attributes = product.Attributes
                .Select(attribute => new ProductAttributeViewModel
                {
                    AttributeId = attribute.AttributeId,
                    AttributeName = attribute.AttributeName,
                    Value = attribute.Value
                })
                .ToList()
        };

        return View(model);
    }
}