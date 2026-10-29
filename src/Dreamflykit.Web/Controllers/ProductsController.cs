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
    private readonly Dreamflykit.Application.MobilesHub.Interfaces.IMobilesHubService _mobilesHubService;
    private readonly Dreamflykit.Application.AppliancesHub.Interfaces.IAppliancesHubService _appliancesHubService;
    private readonly Dreamflykit.Application.BeautyHub.Interfaces.IBeautyHubService _beautyHubService;
    private readonly Dreamflykit.Application.FoodHub.Interfaces.IFoodHubService _foodHubService;
    private readonly Dreamflykit.Application.TravelHub.Interfaces.ITravelHubService _travelHubService;

    public ProductsController(
        IProductService productService,
        ICategoryService categoryService,
        IStudyHubService studyHubService,
        Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService fashionHubService,
        Dreamflykit.Application.HealthHub.Interfaces.IHealthHubService healthHubService,
        Dreamflykit.Application.MobilesHub.Interfaces.IMobilesHubService mobilesHubService,
        Dreamflykit.Application.AppliancesHub.Interfaces.IAppliancesHubService appliancesHubService,
        Dreamflykit.Application.BeautyHub.Interfaces.IBeautyHubService beautyHubService,
        Dreamflykit.Application.FoodHub.Interfaces.IFoodHubService foodHubService,
        Dreamflykit.Application.TravelHub.Interfaces.ITravelHubService travelHubService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _studyHubService = studyHubService;
        _fashionHubService = fashionHubService;
        _healthHubService = healthHubService;
        _mobilesHubService = mobilesHubService;
        _appliancesHubService = appliancesHubService;
        _beautyHubService = beautyHubService;
        _foodHubService = foodHubService;
        _travelHubService = travelHubService;
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
    [HttpGet("/categories/{category}")]
    [HttpGet("/categories/{category}/{subcategorySlug}")]
    public async Task<IActionResult> Index(
        [FromQuery(Name = "q")] string? q = null,
        [FromQuery] string? search = null,
        [FromQuery] Guid? categoryId = null,
        [FromRoute] string? category = null,
        [FromQuery(Name = "category")] string? queryCategory = null,
        [FromRoute] string? subcategorySlug = null,
        [FromQuery(Name = "subcategory")] string? querySubcategory = null,
        [FromQuery] Guid? subcategoryId = null,
        [FromQuery(Name = "subcatItem")] string? subcatItem = null,
        [FromQuery(Name = "item")] string? item = null,
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
        var effectiveCategory = !string.IsNullOrWhiteSpace(category)
            ? category.Trim().ToLowerInvariant()
            : (!string.IsNullOrWhiteSpace(queryCategory) ? queryCategory.Trim().ToLowerInvariant() : null);

        var studyCategorySlugs = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "stationery-study-supplies",
            "tech-for-study",
            "document-organization",
            "desk-room-study-essential",
            "college-campus-essentials",
            "online-classes-productivity"
        };

        string? currentSubcategorySlug = !string.IsNullOrWhiteSpace(subcategorySlug)
            ? subcategorySlug.Trim().ToLowerInvariant()
            : (!string.IsNullOrWhiteSpace(querySubcategory) ? querySubcategory.Trim().ToLowerInvariant() : null);

        if (!string.IsNullOrWhiteSpace(effectiveCategory) && studyCategorySlugs.Contains(effectiveCategory))
        {
            if (string.IsNullOrWhiteSpace(currentSubcategorySlug))
            {
                currentSubcategorySlug = effectiveCategory;
            }
            effectiveCategory = "study-essentials";
        }

        var isStudyCategory = string.Equals(effectiveCategory, "study-essentials", StringComparison.OrdinalIgnoreCase) ||
                              string.Equals(effectiveCategory, "study_essentials", StringComparison.OrdinalIgnoreCase);

        var isFashionCategory = string.Equals(effectiveCategory, "fashion", StringComparison.OrdinalIgnoreCase);

        var isHealthCategory = string.Equals(effectiveCategory, "health-care", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "healthcare", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "health_care", StringComparison.OrdinalIgnoreCase);

        var isMobilesCategory = string.Equals(effectiveCategory, "mobiles", StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(effectiveCategory, "mobile", StringComparison.OrdinalIgnoreCase);

        var isAppliancesCategory = string.Equals(effectiveCategory, "appliances", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(effectiveCategory, "appliance", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(effectiveCategory, "kitchen-appliances", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(effectiveCategory, "kitchen_appliances", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(effectiveCategory, "tv-and-appliances", StringComparison.OrdinalIgnoreCase);

        var isBeautyCategory = string.Equals(effectiveCategory, "beauty", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "beauty-and-personal-care", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "beauty-personal-care", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "bpc", StringComparison.OrdinalIgnoreCase);

        var isFoodCategory = string.Equals(effectiveCategory, "food", StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(effectiveCategory, "foods", StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(effectiveCategory, "groceries", StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(effectiveCategory, "grocery", StringComparison.OrdinalIgnoreCase);

        var isTravelCategory = string.Equals(effectiveCategory, "travel-gear", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "travel", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "travel_gear", StringComparison.OrdinalIgnoreCase);

        CategoryDetails? catDetails = null;
        string? currentSubcategoryName = null;

        if (!string.IsNullOrWhiteSpace(effectiveCategory))
        {
            catDetails = await _categoryService.GetCategoryDetailsAsync(effectiveCategory, cancellationToken);
            if (catDetails != null)
            {
                categoryId = catDetails.CategoryId;

                if (!string.IsNullOrWhiteSpace(currentSubcategorySlug))
                {
                    var matchingSub = catDetails.Subcategories
                        .FirstOrDefault(s => s.Slug.Equals(currentSubcategorySlug, StringComparison.OrdinalIgnoreCase));
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

        var effectiveSubcatItem = !string.IsNullOrWhiteSpace(subcatItem)
            ? subcatItem.Trim()
            : (!string.IsNullOrWhiteSpace(item) ? item.Trim() : null);

        var effectiveSearch = !string.IsNullOrWhiteSpace(q)
            ? q.Trim()
            : (!string.IsNullOrWhiteSpace(search) ? search.Trim() : (!string.IsNullOrWhiteSpace(effectiveSubcatItem) && !isStudyCategory ? effectiveSubcatItem : null));

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

        var isEthnicWearCategory = string.Equals(effectiveCategory, "ethnic-wear", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(effectiveCategory, "ethnic_wear", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(effectiveCategory, "ethnic", StringComparison.OrdinalIgnoreCase) ||
                                   (isFashionCategory && (string.Equals(currentSubcategorySlug, "ethnic-wear", StringComparison.OrdinalIgnoreCase) ||
                                                          string.Equals(currentSubcategorySlug, "ethnic_wear", StringComparison.OrdinalIgnoreCase) ||
                                                          string.Equals(currentSubcategorySlug, "ethnic", StringComparison.OrdinalIgnoreCase)));

        var isThermalsCategory = string.Equals(effectiveCategory, "thermals", StringComparison.OrdinalIgnoreCase) ||
                                 string.Equals(effectiveCategory, "thermal", StringComparison.OrdinalIgnoreCase) ||
                                 (isFashionCategory && (string.Equals(currentSubcategorySlug, "thermals", StringComparison.OrdinalIgnoreCase) ||
                                                        string.Equals(currentSubcategorySlug, "thermal", StringComparison.OrdinalIgnoreCase)));

        var isMenClothingCategory = !isEthnicWearCategory && !isThermalsCategory &&
                                    (string.Equals(effectiveCategory, "men-clothing", StringComparison.OrdinalIgnoreCase) ||
                                     string.Equals(effectiveCategory, "men_clothing", StringComparison.OrdinalIgnoreCase) ||
                                     (isFashionCategory && (string.Equals(currentSubcategorySlug, "men-clothing", StringComparison.OrdinalIgnoreCase) ||
                                                            string.Equals(currentSubcategorySlug, "men_clothing", StringComparison.OrdinalIgnoreCase))));

        var isWomenClothingCategory = string.Equals(effectiveCategory, "women-clothing", StringComparison.OrdinalIgnoreCase) ||
                                      string.Equals(effectiveCategory, "women_clothing", StringComparison.OrdinalIgnoreCase) ||
                                      (isFashionCategory && (string.Equals(currentSubcategorySlug, "women-clothing", StringComparison.OrdinalIgnoreCase) ||
                                                             string.Equals(currentSubcategorySlug, "women_clothing", StringComparison.OrdinalIgnoreCase)));

        var isHoodiesCategory = string.Equals(effectiveCategory, "hoodies", StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(effectiveCategory, "hoodie", StringComparison.OrdinalIgnoreCase) ||
                                (isFashionCategory && (string.Equals(currentSubcategorySlug, "hoodies", StringComparison.OrdinalIgnoreCase) ||
                                                       string.Equals(currentSubcategorySlug, "hoodie", StringComparison.OrdinalIgnoreCase) ||
                                                       string.Equals(currentSubcategorySlug, "hoodies-sweatshirts", StringComparison.OrdinalIgnoreCase) ||
                                                       string.Equals(currentSubcategorySlug, "mens-hoodies", StringComparison.OrdinalIgnoreCase)));

        var isSweatshirtsCategory = string.Equals(effectiveCategory, "sweatshirts", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(effectiveCategory, "sweatshirt", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(effectiveCategory, "mens-sweatshirts", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(effectiveCategory, "mens-sweatshirt", StringComparison.OrdinalIgnoreCase) ||
                                    (isFashionCategory && (string.Equals(currentSubcategorySlug, "sweatshirts", StringComparison.OrdinalIgnoreCase) ||
                                                           string.Equals(currentSubcategorySlug, "sweatshirt", StringComparison.OrdinalIgnoreCase) ||
                                                           string.Equals(currentSubcategorySlug, "mens-sweatshirts", StringComparison.OrdinalIgnoreCase) ||
                                                           string.Equals(currentSubcategorySlug, "mens-sweatshirt", StringComparison.OrdinalIgnoreCase)));

        var isAnyFashionTemplate = isFashionCategory || isMenClothingCategory || isWomenClothingCategory || isHoodiesCategory || isSweatshirtsCategory || isEthnicWearCategory || isThermalsCategory;

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
            CategoryName = (isStudyCategory && !string.IsNullOrWhiteSpace(currentSubcategoryName))
                           ? currentSubcategoryName
                           : (isEthnicWearCategory ? "Ethnic Wear" :
                           (isThermalsCategory ? "Thermals" :
                           (isHoodiesCategory ? "Hoodies" :
                           (isSweatshirtsCategory ? "Sweatshirts" :
                           (isWomenClothingCategory ? "Women Clothing" :
                           (isMenClothingCategory ? "Men Clothing" :
                           (result.CurrentCategoryName ?? catDetails?.Name))))))),
            CategoryImageUrl = catDetails?.ImageUrl,
            CategoryDescription = !string.IsNullOrWhiteSpace(catDetails?.Name)
                ? $"Explore premium {catDetails.Name} student essentials, verified university supplies, and travel-ready gear curated for international students."
                : null,
            SubcategoryId = subcategoryId,
            CurrentSubcategoryName = isEthnicWearCategory ? "Ethnic Wear" :
                                     (isThermalsCategory ? "Thermals" :
                                     (isHoodiesCategory ? "Hoodies" :
                                     (isSweatshirtsCategory ? "Sweatshirts" :
                                     (isWomenClothingCategory ? "Women Clothing" :
                                     (isMenClothingCategory ? "Men Clothing" : currentSubcategoryName))))),
            CurrentSubcategorySlug = currentSubcategorySlug,
            CurrentSubcatItem = effectiveSubcatItem,
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
                : (isEthnicWearCategory ? "ethnic-wear" :
                  (isThermalsCategory ? "thermals" :
                  (isHoodiesCategory ? "hoodies" :
                  (isSweatshirtsCategory ? "sweatshirts" :
                  (isWomenClothingCategory ? "women-clothing" :
                  (isMenClothingCategory ? "men-clothing" :
                  ((isStudyCategory || isFashionCategory || isHealthCategory || isMobilesCategory || isAppliancesCategory || isBeautyCategory || isFoodCategory || isTravelCategory) && !subcategoryId.HasValue && string.IsNullOrWhiteSpace(currentSubcategorySlug) ? "hub" : "categories"))))))),
            StudyHub = isStudyCategory
                        ? await _studyHubService.GetConfigAsync(cancellationToken)
                        : null,
            FashionHub = isAnyFashionTemplate
                        ? await _fashionHubService.GetConfigAsync(cancellationToken)
                        : null,
            HealthHub = isHealthCategory
                        ? await _healthHubService.GetConfigAsync(cancellationToken)
                        : null,
            MobilesHub = isMobilesCategory
                        ? await _mobilesHubService.GetConfigAsync(cancellationToken)
                        : null,
            AppliancesHub = isAppliancesCategory
                        ? await _appliancesHubService.GetConfigAsync(cancellationToken)
                        : null,
            BeautyHub = isBeautyCategory
                        ? await _beautyHubService.GetConfigAsync(cancellationToken)
                        : null,
            FoodHub = isFoodCategory
                        ? await _foodHubService.GetConfigAsync(cancellationToken)
                        : null,
            TravelHub = isTravelCategory
                        ? await _travelHubService.GetConfigAsync(cancellationToken)
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