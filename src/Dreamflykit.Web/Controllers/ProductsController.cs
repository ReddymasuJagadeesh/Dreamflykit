using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.StudyHub.Interfaces;
using Dreamflykit.Application.FashionHub.Interfaces;
using Dreamflykit.Application.HealthHub.Interfaces;
using Dreamflykit.Application.MobilesHub.Interfaces;
using Dreamflykit.Application.AppliancesHub.Interfaces;
using Dreamflykit.Application.BeautyHub.Interfaces;
using Dreamflykit.Application.FoodHub.Interfaces;
using Dreamflykit.Application.TravelHub.Interfaces;
using Dreamflykit.Application.LuggageHub.Interfaces;
using Dreamflykit.Application.ElectronicsHub.Interfaces;
using Dreamflykit.Application.StoryBooksHub.Interfaces;
using Dreamflykit.Application.GamingHub.Interfaces;
using Dreamflykit.Application.SportsHub.Interfaces;
using Dreamflykit.Web.Models.Products;
using Dreamflykit.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

public sealed class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryHubCoordinator _categoryHubCoordinator;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public ProductsController(
        IProductService productService,
        ICategoryHubCoordinator categoryHubCoordinator)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _categoryHubCoordinator = categoryHubCoordinator ?? throw new ArgumentNullException(nameof(categoryHubCoordinator));
    }

    [Obsolete("Use ProductsController(IProductService, ICategoryHubCoordinator)")]
    public ProductsController(
        IProductService productService,
        ICategoryService categoryService,
        IStudyHubService studyHubService,
        IFashionHubService fashionHubService,
        IHealthHubService healthHubService,
        IMobilesHubService mobilesHubService,
        IAppliancesHubService appliancesHubService,
        IBeautyHubService beautyHubService,
        IFoodHubService foodHubService,
        ITravelHubService travelHubService,
        ILuggageHubService luggageHubService,
        IElectronicsHubService electronicsHubService,
        IStoryBooksHubService? storyBooksHubService = null,
        IGamingHubService? gamingHubService = null,
        ISportsHubService? sportsHubService = null)
        : this(
            productService,
            new CategoryHubCoordinator(
                productService,
                categoryService,
                studyHubService,
                fashionHubService,
                healthHubService,
                mobilesHubService,
                appliancesHubService,
                beautyHubService,
                foodHubService,
                travelHubService,
                luggageHubService,
                electronicsHubService,
                storyBooksHubService,
                gamingHubService,
                sportsHubService))
    {
    }

    [HttpGet("/category/study-essentials/category/study-essentials/{slug}")]
    public IActionResult RedirectMalformedStudyUrl(string slug)
    {
        return RedirectPermanent($"/category/study-essentials/{slug}");
    }

    [HttpGet("/products")]
    [HttpGet("/products/search")]
    [HttpGet("/category/{category}")]
    [HttpGet("/category/{category}/{subcategorySlug}")]
    [HttpGet("/category/{category}/{subcategorySlug}/{brandSlug}")]
    [HttpGet("/categories/{category}")]
    [HttpGet("/categories/{category}/{subcategorySlug}")]
    [HttpGet("/categories/{category}/{subcategorySlug}/{brandSlug}")]
    public async Task<IActionResult> Index(
        [FromQuery(Name = "q")] string? q = null,
        [FromQuery] string? search = null,
        [FromQuery] Guid? categoryId = null,
        [FromRoute] string? category = null,
        [FromQuery(Name = "category")] string? queryCategory = null,
        [FromRoute] string? subcategorySlug = null,
        [FromQuery(Name = "subcategory")] string? querySubcategory = null,
        [FromRoute] string? brandSlug = null,
        [FromQuery(Name = "brand")] string? queryBrand = null,
        [FromQuery] Guid? subcategoryId = null,
        [FromQuery(Name = "subcatItem")] string? subcatItem = null,
        [FromQuery(Name = "item")] string? item = null,
        [FromQuery(Name = "sub")] string? sub = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] bool? inStockOnly = null,
        [FromQuery] int? minDiscount = null,
        [FromQuery] ProductSortOption sort = ProductSortOption.Relevance,
        [FromQuery] string? view = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 24,
        CancellationToken cancellationToken = default)
    {
        var context = new CategoryLandingContext
        {
            Q = q,
            Search = search,
            CategoryId = categoryId,
            Category = category,
            QueryCategory = queryCategory,
            SubcategorySlug = subcategorySlug,
            QuerySubcategory = querySubcategory,
            BrandSlug = brandSlug,
            QueryBrand = queryBrand,
            SubcategoryId = subcategoryId,
            SubcatItem = subcatItem,
            Item = item,
            Sub = sub,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            InStockOnly = inStockOnly,
            MinDiscount = minDiscount,
            Sort = sort,
            View = view,
            Page = page,
            PageSize = pageSize,
            GenderQuery = Request.Query["gender"],
            DepartmentQuery = Request.Query["department"],
            FootwearQuery = Request.Query["footwear"]
        };

        var model = await _categoryHubCoordinator.CoordinateCategoryLandingAsync(context, cancellationToken);
        if (model.PermanentRedirectUrl != null)
        {
            return RedirectPermanent(model.PermanentRedirectUrl);
        }

        var isAjax = string.Equals(Request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(Request.Query["partial"], "true", StringComparison.OrdinalIgnoreCase);

        if (isAjax)
        {
            if (model.IsStudyCategory)
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

        var product = await _productService.GetDetailsAsync(
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
