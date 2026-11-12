using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.Categories.Models;
using Dreamflykit.Application.StudyHub.Interfaces;
using Dreamflykit.Application.StudyHub.Models;
using Dreamflykit.Application.FashionHub.Interfaces;
using Dreamflykit.Application.FashionHub.Models;
using Dreamflykit.Application.HealthHub.Interfaces;
using Dreamflykit.Application.HealthHub.Models;
using Dreamflykit.Application.MobilesHub.Interfaces;
using Dreamflykit.Application.MobilesHub.Models;
using Dreamflykit.Application.AppliancesHub.Interfaces;
using Dreamflykit.Application.AppliancesHub.Models;
using Dreamflykit.Application.BeautyHub.Interfaces;
using Dreamflykit.Application.BeautyHub.Models;
using Dreamflykit.Application.FoodHub.Interfaces;
using Dreamflykit.Application.FoodHub.Models;
using Dreamflykit.Application.TravelHub.Interfaces;
using Dreamflykit.Application.TravelHub.Models;
using Dreamflykit.Application.LuggageHub.Interfaces;
using Dreamflykit.Application.LuggageHub.Models;
using Dreamflykit.Application.ElectronicsHub.Interfaces;
using Dreamflykit.Application.ElectronicsHub.Models;
using Dreamflykit.Application.StoryBooksHub.Interfaces;
using Dreamflykit.Application.StoryBooksHub.Models;
using Dreamflykit.Application.GamingHub.Interfaces;
using Dreamflykit.Application.GamingHub.Models;
using Dreamflykit.Application.SportsHub.Interfaces;
using Dreamflykit.Application.SportsHub.Models;
using Dreamflykit.Web.Models.Products;

namespace Dreamflykit.Web.Services;

public sealed class CategoryHubCoordinator : ICategoryHubCoordinator
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IStudyHubService _studyHubService;
    private readonly IFashionHubService _fashionHubService;
    private readonly IHealthHubService _healthHubService;
    private readonly IMobilesHubService _mobilesHubService;
    private readonly IAppliancesHubService _appliancesHubService;
    private readonly IBeautyHubService _beautyHubService;
    private readonly IFoodHubService _foodHubService;
    private readonly ITravelHubService _travelHubService;
    private readonly ILuggageHubService _luggageHubService;
    private readonly IElectronicsHubService _electronicsHubService;
    private readonly IStoryBooksHubService? _storyBooksHubService;
    private readonly IGamingHubService? _gamingHubService;
    private readonly ISportsHubService? _sportsHubService;

    public CategoryHubCoordinator(
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
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        _studyHubService = studyHubService ?? throw new ArgumentNullException(nameof(studyHubService));
        _fashionHubService = fashionHubService ?? throw new ArgumentNullException(nameof(fashionHubService));
        _healthHubService = healthHubService ?? throw new ArgumentNullException(nameof(healthHubService));
        _mobilesHubService = mobilesHubService ?? throw new ArgumentNullException(nameof(mobilesHubService));
        _appliancesHubService = appliancesHubService ?? throw new ArgumentNullException(nameof(appliancesHubService));
        _beautyHubService = beautyHubService ?? throw new ArgumentNullException(nameof(beautyHubService));
        _foodHubService = foodHubService ?? throw new ArgumentNullException(nameof(foodHubService));
        _travelHubService = travelHubService ?? throw new ArgumentNullException(nameof(travelHubService));
        _luggageHubService = luggageHubService ?? throw new ArgumentNullException(nameof(luggageHubService));
        _electronicsHubService = electronicsHubService ?? throw new ArgumentNullException(nameof(electronicsHubService));
        _storyBooksHubService = storyBooksHubService;
        _gamingHubService = gamingHubService;
        _sportsHubService = sportsHubService;
    }

    public async Task<ProductListViewModel> CoordinateCategoryLandingAsync(
        CategoryLandingContext ctx,
        CancellationToken cancellationToken = default)
    {
        var effectiveCategory = !string.IsNullOrWhiteSpace(ctx.Category)
            ? ctx.Category.Trim().ToLowerInvariant()
            : (!string.IsNullOrWhiteSpace(ctx.QueryCategory) ? ctx.QueryCategory.Trim().ToLowerInvariant() : null);

        var studyCategorySlugs = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "stationery-study-supplies", "tech-for-study", "document-organization",
            "desk-room-study-essential", "college-campus-essentials", "online-classes-productivity"
        };

        string? currentSubcategorySlug = !string.IsNullOrWhiteSpace(ctx.SubcategorySlug)
            ? ctx.SubcategorySlug.Trim().ToLowerInvariant()
            : (!string.IsNullOrWhiteSpace(ctx.QuerySubcategory)
                ? ctx.QuerySubcategory.Trim().ToLowerInvariant()
                : (!string.IsNullOrWhiteSpace(ctx.Sub) ? ctx.Sub.Trim().ToLowerInvariant() : null));

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

        var isElectronicsCategory = string.Equals(effectiveCategory, "electronics", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(effectiveCategory, "electronic", StringComparison.OrdinalIgnoreCase);

        var isStoryBooksCategory = string.Equals(effectiveCategory, "story-books", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(effectiveCategory, "story_books", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(effectiveCategory, "storybooks", StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(effectiveCategory, "books", StringComparison.OrdinalIgnoreCase);

        var isSportsCategory = string.Equals(effectiveCategory, "sports-hub", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "sports_hub", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "sports", StringComparison.OrdinalIgnoreCase);

        var sportsSubcategorySlugs = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "cricket", "football", "badminton", "gym-fitness", "basketball-volleyball",
            "tennis-table-tennis", "sports-footwear", "athletic-wear", "swimming",
            "sports-bags", "yoga-recovery", "outdoor-trekking"
        };

        if (!isSportsCategory && !string.IsNullOrWhiteSpace(effectiveCategory) && sportsSubcategorySlugs.Contains(effectiveCategory))
        {
            if (string.IsNullOrWhiteSpace(currentSubcategorySlug))
            {
                currentSubcategorySlug = effectiveCategory;
            }
            effectiveCategory = "sports-hub";
            isSportsCategory = true;
        }

        var isGamingCategory = string.Equals(effectiveCategory, "gaming-hub", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "gaming_hub", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "gaming", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(effectiveCategory, "gaming-zone", StringComparison.OrdinalIgnoreCase);

        var gamingSubcategorySlugs = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "playstation", "xbox", "nintendo", "steam-deck", "vr-gaming",
            "steering-wheels", "preloaded-consoles", "asus-rog", "lenovo-legion",
            "retro-gaming-arcade", "game-projector", "open-box-gaming", "drones",
            "bluetooth-speaker", "gaming-hard-drive", "motion-sensor", "fitness-gaming",
            "microphone", "controllers-and-gamepads", "playstation-ecosystem",
            "xbox-ecosystem", "retro-budget-gaming"
        };

        if (!isGamingCategory && !string.IsNullOrWhiteSpace(effectiveCategory) && gamingSubcategorySlugs.Contains(effectiveCategory))
        {
            if (string.IsNullOrWhiteSpace(currentSubcategorySlug))
            {
                currentSubcategorySlug = effectiveCategory;
            }
            effectiveCategory = "gaming-hub";
            isGamingCategory = true;
        }

        var isLuggageCategory = string.Equals(effectiveCategory, "luggage-bags", StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(effectiveCategory, "luggage_bags", StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(effectiveCategory, "luggage", StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(effectiveCategory, "luggage-suitcases", StringComparison.OrdinalIgnoreCase) ||
                                (isTravelCategory && (string.Equals(currentSubcategorySlug, "luggage-suitcases", StringComparison.OrdinalIgnoreCase) ||
                                                      string.Equals(ctx.SubcategorySlug, "luggage-suitcases", StringComparison.OrdinalIgnoreCase) ||
                                                      string.Equals(ctx.QuerySubcategory, "luggage-suitcases", StringComparison.OrdinalIgnoreCase) ||
                                                      string.Equals(ctx.SubcatItem, "Luggage & Suitcases", StringComparison.OrdinalIgnoreCase)));

        if (isLuggageCategory && !string.Equals(effectiveCategory, "luggage-bags", StringComparison.OrdinalIgnoreCase))
        {
            return new ProductListViewModel { PermanentRedirectUrl = "/category/luggage-bags" };
        }

        CategoryDetails? catDetails = null;
        string? currentSubcategoryName = null;
        var categoryId = ctx.CategoryId;
        var subcategoryId = ctx.SubcategoryId;

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

        SportsHubDto? sportsHubConfig = null;
        SportsSubcategoryPageDto? sportsSubPage = null;

        if (isSportsCategory && _sportsHubService != null)
        {
            sportsHubConfig = await _sportsHubService.GetConfigAsync(cancellationToken);
            if (!string.IsNullOrWhiteSpace(currentSubcategorySlug))
            {
                sportsSubPage = await _sportsHubService.GetSubcategoryPageAsync(currentSubcategorySlug, cancellationToken);
                if (sportsSubPage != null && string.IsNullOrWhiteSpace(currentSubcategoryName))
                {
                    currentSubcategoryName = sportsSubPage.CategoryName;
                }
            }
            else if (string.Equals(ctx.View, "categories", StringComparison.OrdinalIgnoreCase))
            {
                currentSubcategorySlug = "cricket";
                sportsSubPage = await _sportsHubService.GetSubcategoryPageAsync(currentSubcategorySlug, cancellationToken);
                if (sportsSubPage != null && string.IsNullOrWhiteSpace(currentSubcategoryName))
                {
                    currentSubcategoryName = sportsSubPage.CategoryName;
                }
            }
        }

        GamingHubDto? gamingHubConfig = null;
        GamingSubcategoryPageDto? gamingSubPage = null;

        if (isGamingCategory && _gamingHubService != null)
        {
            gamingHubConfig = await _gamingHubService.GetConfigAsync(cancellationToken);
            if (!string.IsNullOrWhiteSpace(currentSubcategorySlug))
            {
                gamingSubPage = await _gamingHubService.GetSubcategoryPageAsync(currentSubcategorySlug, cancellationToken);
                if (gamingSubPage != null && string.IsNullOrWhiteSpace(currentSubcategoryName))
                {
                    currentSubcategoryName = gamingSubPage.CategoryName;
                }
            }
            else if (string.Equals(ctx.View, "categories", StringComparison.OrdinalIgnoreCase))
            {
                currentSubcategorySlug = "playstation";
                gamingSubPage = await _gamingHubService.GetSubcategoryPageAsync(currentSubcategorySlug, cancellationToken);
                if (gamingSubPage != null && string.IsNullOrWhiteSpace(currentSubcategoryName))
                {
                    currentSubcategoryName = gamingSubPage.CategoryName;
                }
            }
        }

        string? effectiveBrand = !string.IsNullOrWhiteSpace(ctx.BrandSlug)
            ? ctx.BrandSlug.Trim().ToLowerInvariant()
            : (!string.IsNullOrWhiteSpace(ctx.QueryBrand) ? ctx.QueryBrand.Trim().ToLowerInvariant() : null);

        ElectronicsBrandShowcaseData? elecBrandShowcase = null;
        AppliancesBrandShowcaseData? appBrandShowcase = null;
        MobilesHubDto? mobilesHubConfig = null;
        MobileBrandShowcaseData? mobilesBrandShowcase = null;
        string? currentAppliancesBrandSlug = null;
        string? currentAppliancesBrandName = null;
        string? currentBrandSlug = null;
        string? currentBrandName = null;

        if (isMobilesCategory)
        {
            mobilesHubConfig = await _mobilesHubService.GetConfigAsync(cancellationToken);
            var candidateMobBrand = !string.IsNullOrWhiteSpace(effectiveBrand)
                ? effectiveBrand
                : (!string.IsNullOrWhiteSpace(currentSubcategorySlug) && !string.Equals(currentSubcategorySlug, "brand", StringComparison.OrdinalIgnoreCase) && !string.Equals(currentSubcategorySlug, "all", StringComparison.OrdinalIgnoreCase)
                    ? currentSubcategorySlug
                    : null);

            if (!string.IsNullOrWhiteSpace(candidateMobBrand))
            {
                mobilesBrandShowcase = Dreamflykit.Application.MobilesHub.Services.MobileBrandCatalogService.GetBrandShowcase(candidateMobBrand, mobilesHubConfig);
                currentBrandSlug = mobilesBrandShowcase.BrandSlug;
                currentBrandName = mobilesBrandShowcase.BrandName;
            }
        }

        if (isElectronicsCategory && !string.IsNullOrWhiteSpace(currentSubcategorySlug))
        {
            var elecHubConfig = await _electronicsHubService.GetConfigAsync(cancellationToken);
            var subPage = elecHubConfig.SubcategoryPages?.FirstOrDefault(p =>
                string.Equals(p.CategorySlug, currentSubcategorySlug, StringComparison.OrdinalIgnoreCase) ||
                (currentSubcategorySlug.Contains("laptop") && p.CategorySlug.Contains("laptop")) ||
                ((currentSubcategorySlug.Contains("earphone") || currentSubcategorySlug.Contains("headphone") || currentSubcategorySlug.Contains("audio")) && (p.CategorySlug.Contains("earphone") || p.CategorySlug.Contains("headphone"))) ||
                (currentSubcategorySlug.Contains("groom") && p.CategorySlug.Contains("groom")) ||
                ((currentSubcategorySlug.Contains("case") || currentSubcategorySlug.Contains("cover")) && p.CategorySlug.Contains("case")) ||
                ((currentSubcategorySlug.Contains("storage") || currentSubcategorySlug.Contains("ssd")) && p.CategorySlug.Contains("storage")) ||
                ((currentSubcategorySlug.Contains("charger") || currentSubcategorySlug.Contains("cable") || currentSubcategorySlug.Contains("adapter")) && (p.CategorySlug.Contains("charger") || p.CategorySlug.Contains("cable"))) ||
                (currentSubcategorySlug.Contains("power") && p.CategorySlug.Contains("power")) ||
                (currentSubcategorySlug.Contains("watch") && p.CategorySlug.Contains("watch")) ||
                ((currentSubcategorySlug.Contains("accessory") || currentSubcategorySlug.Contains("accessories") || currentSubcategorySlug.Contains("mouse") || currentSubcategorySlug.Contains("keyboard")) && p.CategorySlug.Contains("access")) ||
                (currentSubcategorySlug.Contains("speaker") && p.CategorySlug.Contains("speaker")));

            if (subPage != null)
            {
                currentSubcategoryName = subPage.CategoryName;
                currentSubcategorySlug = subPage.CategorySlug;

                var searchCandidate = !string.IsNullOrWhiteSpace(ctx.Q) ? ctx.Q.Trim() : (!string.IsNullOrWhiteSpace(ctx.Search) ? ctx.Search.Trim() : null);
                if (string.IsNullOrWhiteSpace(effectiveBrand) && !string.IsNullOrWhiteSpace(searchCandidate))
                {
                    var matchedBrand = subPage.ShopByBrands?.Brands?.FirstOrDefault(b =>
                        string.Equals(b.Slug, searchCandidate, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(b.Name, searchCandidate, StringComparison.OrdinalIgnoreCase) ||
                        searchCandidate.Contains(b.Slug, StringComparison.OrdinalIgnoreCase) ||
                        searchCandidate.Contains(b.Name, StringComparison.OrdinalIgnoreCase));

                    if (matchedBrand != null)
                    {
                        effectiveBrand = matchedBrand.Slug;
                    }
                }

                if (!string.IsNullOrWhiteSpace(effectiveBrand))
                {
                    elecBrandShowcase = Dreamflykit.Application.ElectronicsHub.Services.ElectronicsBrandCatalogService.GetBrandShowcase(
                        currentSubcategorySlug, effectiveBrand, elecHubConfig);
                    currentBrandSlug = elecBrandShowcase.BrandSlug;
                    currentBrandName = elecBrandShowcase.BrandName;
                }
            }
        }

        if (isAppliancesCategory && !string.IsNullOrWhiteSpace(currentSubcategorySlug))
        {
            var appHubConfig = await _appliancesHubService.GetConfigAsync(cancellationToken);
            var subPage = appHubConfig.SubcategoryPages?.FirstOrDefault(p =>
                string.Equals(p.CategorySlug, currentSubcategorySlug, StringComparison.OrdinalIgnoreCase) ||
                (currentSubcategorySlug.Contains("tv") && p.CategorySlug.Contains("television")) ||
                (currentSubcategorySlug.Contains("telev") && p.CategorySlug.Contains("television")) ||
                (currentSubcategorySlug.Contains("laund") && p.CategorySlug.Contains("laundry")) ||
                (currentSubcategorySlug.Contains("wash") && p.CategorySlug.Contains("laundry")) ||
                (currentSubcategorySlug.Contains("ac") && p.CategorySlug.Contains("air-conditioners")) ||
                (currentSubcategorySlug.Contains("fan") && p.CategorySlug.Contains("fans")) ||
                (currentSubcategorySlug.Contains("micro") && p.CategorySlug.Contains("microwaves")) ||
                (currentSubcategorySlug.Contains("365") && p.CategorySlug.Contains("value-365")) ||
                (currentSubcategorySlug.Contains("appliance") && p.CategorySlug.Contains("small-appliances")) ||
                (currentSubcategorySlug.Contains("geys") && p.CategorySlug.Contains("geysers")) ||
                (currentSubcategorySlug.Contains("stab") && p.CategorySlug.Contains("stabilizers")) ||
                (currentSubcategorySlug.Contains("kitch") && p.CategorySlug.Contains("kitchen-appliances")) ||
                (currentSubcategorySlug.Contains("fridg") && p.CategorySlug.Contains("refrigerators")) ||
                (currentSubcategorySlug.Contains("refrig") && p.CategorySlug.Contains("refrigerators")) ||
                (currentSubcategorySlug.Contains("home") && p.CategorySlug.Contains("home-appliances")) ||
                (currentSubcategorySlug.Contains("break") && p.CategorySlug.Contains("breakfast-appliances")) ||
                ((currentSubcategorySlug.Contains("top") || currentSubcategorySlug.Contains("fk")) && p.CategorySlug.Contains("top-rated")) ||
                ((currentSubcategorySlug.Contains("invert") || currentSubcategorySlug.Contains("backup")) && p.CategorySlug.Contains("inverters")) ||
                (currentSubcategorySlug.Contains("dish") && p.CategorySlug.Contains("dishwashers")));

            if (subPage != null)
            {
                currentSubcategoryName = subPage.CategoryName;
                currentSubcategorySlug = subPage.CategorySlug;

                effectiveBrand = ctx.BrandSlug ?? ctx.QueryBrand;
                var searchCandidate = !string.IsNullOrWhiteSpace(ctx.Q) ? ctx.Q.Trim() : (!string.IsNullOrWhiteSpace(ctx.Search) ? ctx.Search.Trim() : null);
                if (string.IsNullOrWhiteSpace(effectiveBrand) && !string.IsNullOrWhiteSpace(searchCandidate))
                {
                    var matchedBrand = subPage.ShopByBrands?.Brands?.FirstOrDefault(b =>
                        string.Equals(b.Slug, searchCandidate, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(b.Name, searchCandidate, StringComparison.OrdinalIgnoreCase) ||
                        searchCandidate.Contains(b.Slug, StringComparison.OrdinalIgnoreCase) ||
                        searchCandidate.Contains(b.Name, StringComparison.OrdinalIgnoreCase));

                    if (matchedBrand != null)
                    {
                        effectiveBrand = matchedBrand.Slug;
                    }
                }

                if (!string.IsNullOrWhiteSpace(effectiveBrand))
                {
                    appBrandShowcase = Dreamflykit.Application.AppliancesHub.Services.AppliancesBrandCatalogService.GetBrandShowcase(
                        currentSubcategorySlug, effectiveBrand, appHubConfig);
                    currentAppliancesBrandSlug = appBrandShowcase.BrandSlug;
                    currentAppliancesBrandName = appBrandShowcase.BrandName;
                }
            }
        }

        if (isLuggageCategory && !categoryId.HasValue)
        {
            var travelCatDetails = await _categoryService.GetCategoryDetailsAsync("travel-gear", cancellationToken);
            if (travelCatDetails != null)
            {
                categoryId = travelCatDetails.CategoryId;
                var luggageSub = travelCatDetails.Subcategories
                    .FirstOrDefault(s => s.Slug.Equals("luggage-suitcases", StringComparison.OrdinalIgnoreCase));
                if (luggageSub != null)
                {
                    subcategoryId = luggageSub.SubcategoryId;
                    currentSubcategoryName = luggageSub.Name;
                    currentSubcategorySlug = "luggage-suitcases";
                }
            }
        }

        var effectiveSubcatItem = !string.IsNullOrWhiteSpace(ctx.SubcatItem)
            ? ctx.SubcatItem.Trim()
            : (!string.IsNullOrWhiteSpace(ctx.Item) ? ctx.Item.Trim() : null);

        var effectiveSearch = !string.IsNullOrWhiteSpace(ctx.Q)
            ? ctx.Q.Trim()
            : (!string.IsNullOrWhiteSpace(ctx.Search) ? ctx.Search.Trim() : (!string.IsNullOrWhiteSpace(effectiveSubcatItem) && !isStudyCategory && !isSportsCategory && !isGamingCategory ? effectiveSubcatItem : null));

        var query = new ProductListQuery
        {
            SearchTerm = effectiveSearch,
            CategoryId = categoryId,
            CategorySlug = isLuggageCategory ? "travel-gear" : effectiveCategory,
            SubcategoryId = subcategoryId,
            MinPrice = ctx.MinPrice,
            MaxPrice = ctx.MaxPrice,
            InStockOnly = ctx.InStockOnly,
            MinDiscount = ctx.MinDiscount,
            Sort = ctx.Sort,
            Page = ctx.Page,
            PageSize = ctx.PageSize
        };

        var result = await _productService.SearchProductsAsync(query, cancellationToken);

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

        var womenSubcategorySlugs = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "t-shirts-tops", "tops-t-shirts", "t-shirts", "tops", "shirts-blouses", "shirts", "blouses",
            "jeans", "dresses", "leggings", "trousers-pants", "trousers", "pants", "thermals", "women-thermals",
            "hoodies-sweatshirts", "women-hoodies", "women-sweatshirts", "jackets-coats", "jackets", "coats",
            "nightwear", "sleepwear", "innerwear", "activewear", "formal-wear", "formal",
            "gym-wear-sportswear", "sportswear", "gym-wear", "gym"
        };

        static string NormalizeWomenSubSlug(string slug)
        {
            var s = slug.Trim().ToLowerInvariant();
            return s switch
            {
                "tops-t-shirts" or "t-shirts" or "tops" => "t-shirts-tops",
                "shirts" or "blouses" => "shirts-blouses",
                "trousers" or "pants" => "trousers-pants",
                "women-thermals" => "thermals",
                "women-hoodies" or "women-sweatshirts" => "hoodies-sweatshirts",
                "jackets" or "coats" => "jackets-coats",
                "sleepwear" => "nightwear",
                "formal" => "formal-wear",
                "sportswear" or "gym-wear" or "gym" => "gym-wear-sportswear",
                _ => s
            };
        }

        string? effectiveWomenSubSlug = null;
        if (string.Equals(effectiveCategory, "fashion", StringComparison.OrdinalIgnoreCase) &&
            (string.Equals(currentSubcategorySlug, "women-clothing", StringComparison.OrdinalIgnoreCase) ||
             string.Equals(currentSubcategorySlug, "women_clothing", StringComparison.OrdinalIgnoreCase)) &&
            !string.IsNullOrWhiteSpace(ctx.BrandSlug))
        {
            effectiveWomenSubSlug = NormalizeWomenSubSlug(ctx.BrandSlug);
        }
        else if ((string.Equals(effectiveCategory, "women-clothing", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(effectiveCategory, "women_clothing", StringComparison.OrdinalIgnoreCase)) &&
                 !string.IsNullOrWhiteSpace(currentSubcategorySlug))
        {
            effectiveWomenSubSlug = NormalizeWomenSubSlug(currentSubcategorySlug);
        }
        else if (string.Equals(effectiveCategory, "fashion", StringComparison.OrdinalIgnoreCase) &&
                 !string.IsNullOrWhiteSpace(currentSubcategorySlug) &&
                 womenSubcategorySlugs.Contains(currentSubcategorySlug) &&
                 !string.Equals(currentSubcategorySlug, "women-clothing", StringComparison.OrdinalIgnoreCase) &&
                 !string.Equals(currentSubcategorySlug, "men-clothing", StringComparison.OrdinalIgnoreCase))
        {
            var distinctWomenOnly = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "t-shirts-tops", "tops-t-shirts", "shirts-blouses", "dresses", "leggings", "trousers-pants",
                "nightwear", "innerwear", "formal-wear", "gym-wear-sportswear"
            };

            if (distinctWomenOnly.Contains(currentSubcategorySlug) ||
                string.Equals(ctx.GenderQuery, "women", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(ctx.DepartmentQuery, "women", StringComparison.OrdinalIgnoreCase))
            {
                effectiveWomenSubSlug = NormalizeWomenSubSlug(currentSubcategorySlug);
            }
        }
        else if (!string.IsNullOrWhiteSpace(ctx.Sub) && womenSubcategorySlugs.Contains(ctx.Sub))
        {
            effectiveWomenSubSlug = NormalizeWomenSubSlug(ctx.Sub);
        }

        var isWomenSubcategory = !string.IsNullOrWhiteSpace(effectiveWomenSubSlug);
        FashionWomenSubcategoryPageDto? womenSubPage = null;
        if (isWomenSubcategory)
        {
            var fConfig = await _fashionHubService.GetConfigAsync(cancellationToken);
            var allPages = fConfig?.WomenClothing?.SubcategoryPages;
            if (allPages != null && allPages.Count > 0)
            {
                womenSubPage = allPages.FirstOrDefault(p =>
                    string.Equals(p.CategorySlug, effectiveWomenSubSlug, StringComparison.OrdinalIgnoreCase));
            }

            if (womenSubPage == null)
            {
                var defaultPages = FashionWomenClothingDto.CreateDefaultSubcategoryPages();
                womenSubPage = defaultPages.FirstOrDefault(p =>
                    string.Equals(p.CategorySlug, effectiveWomenSubSlug, StringComparison.OrdinalIgnoreCase))
                    ?? defaultPages.FirstOrDefault();
            }

            if (womenSubPage != null)
            {
                currentSubcategoryName = womenSubPage.CategoryName;
                currentSubcategorySlug = womenSubPage.CategorySlug;
            }
        }

        string NormalizeMenFootwearSubSlug(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return "casual-shoes";
            s = s.Trim().ToLowerInvariant().Replace('_', '-');
            return s switch
            {
                "casual" or "casuals" or "casual-shoe" => "casual-shoes",
                "sports" or "sport" or "sports-shoe" or "running" or "running-shoes" => "sports-shoes",
                "formal" or "formals" or "formal-shoe" or "office-shoes" or "dress-shoes" => "formal-shoes",
                "sneaker" or "streetwear-sneakers" => "sneakers",
                "sandals" or "floaters" or "sandals-and-floaters" or "sandals_floaters" => "sandals-floaters",
                "flipflops" or "flip-flop" or "slippers" or "slides" => "flip-flops",
                "sock" or "cotton-socks" => "socks",
                _ => s
            };
        }

        string? effectiveMenFootwearSubSlug = null;
        if (string.Equals(effectiveCategory, "fashion", StringComparison.OrdinalIgnoreCase) &&
            (string.Equals(currentSubcategorySlug, "men-footwear", StringComparison.OrdinalIgnoreCase) ||
             string.Equals(currentSubcategorySlug, "mens-footwear", StringComparison.OrdinalIgnoreCase) ||
             string.Equals(currentSubcategorySlug, "men_footwear", StringComparison.OrdinalIgnoreCase)) &&
            !string.IsNullOrWhiteSpace(ctx.BrandSlug))
        {
            effectiveMenFootwearSubSlug = NormalizeMenFootwearSubSlug(ctx.BrandSlug);
        }
        else if ((string.Equals(effectiveCategory, "men-footwear", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(effectiveCategory, "mens-footwear", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(effectiveCategory, "men_footwear", StringComparison.OrdinalIgnoreCase)) &&
                 !string.IsNullOrWhiteSpace(currentSubcategorySlug))
        {
            effectiveMenFootwearSubSlug = NormalizeMenFootwearSubSlug(currentSubcategorySlug);
        }
        else if (string.Equals(effectiveCategory, "fashion", StringComparison.OrdinalIgnoreCase) &&
                 (string.Equals(currentSubcategorySlug, "men-footwear", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(currentSubcategorySlug, "mens-footwear", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(currentSubcategorySlug, "men_footwear", StringComparison.OrdinalIgnoreCase)) &&
                 !string.IsNullOrWhiteSpace(ctx.QuerySubcategory))
        {
            effectiveMenFootwearSubSlug = NormalizeMenFootwearSubSlug(ctx.QuerySubcategory);
        }
        else if (string.Equals(effectiveCategory, "fashion", StringComparison.OrdinalIgnoreCase) &&
                 (string.Equals(currentSubcategorySlug, "men-footwear", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(currentSubcategorySlug, "mens-footwear", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(currentSubcategorySlug, "men_footwear", StringComparison.OrdinalIgnoreCase)) &&
                 !string.IsNullOrWhiteSpace(ctx.FootwearQuery))
        {
            effectiveMenFootwearSubSlug = NormalizeMenFootwearSubSlug(ctx.FootwearQuery);
        }

        var isMenFootwearSubcategory = !string.IsNullOrWhiteSpace(effectiveMenFootwearSubSlug);
        FashionMenFootwearSubcategoryPageDto? menFootwearSubPage = null;
        if (isMenFootwearSubcategory)
        {
            var fConfig = await _fashionHubService.GetConfigAsync(cancellationToken);
            var allPages = fConfig?.MenFootwear?.SubcategoryPages;
            if (allPages != null && allPages.Count > 0)
            {
                menFootwearSubPage = allPages.FirstOrDefault(p =>
                    string.Equals(p.CategorySlug, effectiveMenFootwearSubSlug, StringComparison.OrdinalIgnoreCase));
            }

            if (menFootwearSubPage == null)
            {
                var defaultPages = FashionMenFootwearDto.CreateDefaultSubcategoryPages();
                menFootwearSubPage = defaultPages.FirstOrDefault(p =>
                    string.Equals(p.CategorySlug, effectiveMenFootwearSubSlug, StringComparison.OrdinalIgnoreCase))
                    ?? defaultPages.FirstOrDefault();
            }

            if (menFootwearSubPage != null)
            {
                currentSubcategoryName = menFootwearSubPage.CategoryName;
                currentSubcategorySlug = menFootwearSubPage.CategorySlug;
            }
        }

        var isMenFootwearCategory = !isMenFootwearSubcategory &&
                                    (string.Equals(effectiveCategory, "men-footwear", StringComparison.OrdinalIgnoreCase) ||
                                     string.Equals(effectiveCategory, "mens-footwear", StringComparison.OrdinalIgnoreCase) ||
                                     string.Equals(effectiveCategory, "men_footwear", StringComparison.OrdinalIgnoreCase) ||
                                     (isFashionCategory && (string.Equals(currentSubcategorySlug, "men-footwear", StringComparison.OrdinalIgnoreCase) ||
                                                            string.Equals(currentSubcategorySlug, "mens-footwear", StringComparison.OrdinalIgnoreCase) ||
                                                            string.Equals(currentSubcategorySlug, "men_footwear", StringComparison.OrdinalIgnoreCase))));

        string NormalizeMenClothingSubSlug(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return "shirts";
            s = s.Trim().ToLowerInvariant().Replace('_', '-');
            return s switch
            {
                "ethnic" or "ethnicwear" or "ethnic-wears" => "ethnic-wear",
                "thermal" or "thermal-wear" => "thermals",
                "tshirt" or "tshirts" or "t-shirt" or "tees" or "tee" or "polos" or "t-shirts-polos" => "t-shirts",
                "shirt" or "formal-shirts" or "casual-shirts" => "shirts",
                "jackets" or "jacket" or "coat" or "coats" or "jackets-and-coats" or "jackets_coats" => "jackets-coats",
                "hoodie" or "mens-hoodies" => "hoodies",
                "sweatshirt" or "mens-sweatshirts" => "sweatshirts",
                "jean" or "denim" or "denims" => "jeans",
                "trouser" or "chinos" or "trousers-chinos" => "trousers",
                "pant" or "cargos" or "joggers" => "pants",
                "inner-wear" or "briefs" or "boxers" or "innerwear-sleepwear" => "innerwear",
                "sleep-wear" or "pajamas" or "nightwear" => "sleepwear",
                "short" or "boxer-shorts" => "shorts",
                "sock" or "cotton-socks" => "socks",
                "cap" or "hats" or "beanies" => "caps",
                "towel" or "bath-towels" => "towels",
                "belt" or "leather-belts" => "belts",
                _ => s
            };
        }

        string? effectiveMenClothingSubSlug = null;
        if (string.Equals(effectiveCategory, "fashion", StringComparison.OrdinalIgnoreCase) &&
            (string.Equals(currentSubcategorySlug, "men-clothing", StringComparison.OrdinalIgnoreCase) ||
             string.Equals(currentSubcategorySlug, "mens-clothing", StringComparison.OrdinalIgnoreCase) ||
             string.Equals(currentSubcategorySlug, "men_clothing", StringComparison.OrdinalIgnoreCase)) &&
            !string.IsNullOrWhiteSpace(ctx.BrandSlug))
        {
            effectiveMenClothingSubSlug = NormalizeMenClothingSubSlug(ctx.BrandSlug);
        }
        else if ((string.Equals(effectiveCategory, "men-clothing", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(effectiveCategory, "mens-clothing", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(effectiveCategory, "men_clothing", StringComparison.OrdinalIgnoreCase)) &&
                 !string.IsNullOrWhiteSpace(currentSubcategorySlug))
        {
            effectiveMenClothingSubSlug = NormalizeMenClothingSubSlug(currentSubcategorySlug);
        }
        else if (string.Equals(effectiveCategory, "fashion", StringComparison.OrdinalIgnoreCase) &&
                 (string.Equals(currentSubcategorySlug, "men-clothing", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(currentSubcategorySlug, "mens-clothing", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(currentSubcategorySlug, "men_clothing", StringComparison.OrdinalIgnoreCase)) &&
                 !string.IsNullOrWhiteSpace(ctx.QuerySubcategory))
        {
            effectiveMenClothingSubSlug = NormalizeMenClothingSubSlug(ctx.QuerySubcategory);
        }
        else if (string.Equals(effectiveCategory, "fashion", StringComparison.OrdinalIgnoreCase) &&
                 (string.Equals(currentSubcategorySlug, "men-clothing", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(currentSubcategorySlug, "mens-clothing", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(currentSubcategorySlug, "men_clothing", StringComparison.OrdinalIgnoreCase)) &&
                 !string.IsNullOrWhiteSpace(ctx.Sub))
        {
            effectiveMenClothingSubSlug = NormalizeMenClothingSubSlug(ctx.Sub);
        }

        var isMenClothingSubcategory = !string.IsNullOrWhiteSpace(effectiveMenClothingSubSlug);
        FashionMenClothingSubcategoryPageDto? menClothingSubPage = null;
        if (isMenClothingSubcategory)
        {
            var fConfig = await _fashionHubService.GetConfigAsync(cancellationToken);
            var allPages = fConfig?.MenClothing?.SubcategoryPages;
            if (allPages != null && allPages.Count > 0)
            {
                menClothingSubPage = allPages.FirstOrDefault(p =>
                    string.Equals(p.CategorySlug, effectiveMenClothingSubSlug, StringComparison.OrdinalIgnoreCase));
            }

            if (menClothingSubPage == null)
            {
                var defaultPages = FashionMenClothingDto.CreateDefaultSubcategoryPages();
                menClothingSubPage = defaultPages.FirstOrDefault(p =>
                    string.Equals(p.CategorySlug, effectiveMenClothingSubSlug, StringComparison.OrdinalIgnoreCase))
                    ?? defaultPages.FirstOrDefault();
            }

            if (menClothingSubPage != null)
            {
                currentSubcategoryName = menClothingSubPage.CategoryName;
                currentSubcategorySlug = menClothingSubPage.CategorySlug;
            }
        }

        var isMenClothingCategory = !isMenClothingSubcategory && !isWomenSubcategory && !isEthnicWearCategory && !isThermalsCategory &&
                                    (string.Equals(effectiveCategory, "men-clothing", StringComparison.OrdinalIgnoreCase) ||
                                     string.Equals(effectiveCategory, "men_clothing", StringComparison.OrdinalIgnoreCase) ||
                                     (isFashionCategory && (string.Equals(currentSubcategorySlug, "men-clothing", StringComparison.OrdinalIgnoreCase) ||
                                                            string.Equals(currentSubcategorySlug, "men_clothing", StringComparison.OrdinalIgnoreCase))));

        var isWomenClothingCategory = !isWomenSubcategory &&
                                      (string.Equals(effectiveCategory, "women-clothing", StringComparison.OrdinalIgnoreCase) ||
                                       string.Equals(effectiveCategory, "women_clothing", StringComparison.OrdinalIgnoreCase) ||
                                       (isFashionCategory && (string.Equals(currentSubcategorySlug, "women-clothing", StringComparison.OrdinalIgnoreCase) ||
                                                              string.Equals(currentSubcategorySlug, "women_clothing", StringComparison.OrdinalIgnoreCase))));

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

        var isAnyFashionTemplate = isFashionCategory || isWomenSubcategory || isMenFootwearSubcategory || isMenFootwearCategory || isMenClothingSubcategory || isMenClothingCategory || isWomenClothingCategory || isHoodiesCategory || isSweatshirtsCategory || isEthnicWearCategory || isThermalsCategory;

        string determinedViewMode;
        if (isSportsCategory)
        {
            determinedViewMode = (!string.IsNullOrWhiteSpace(currentSubcategorySlug) || string.Equals(ctx.View, "categories", StringComparison.OrdinalIgnoreCase)) ? "sports-sub" : "hub";
        }
        else if (isGamingCategory)
        {
            determinedViewMode = (!string.IsNullOrWhiteSpace(currentSubcategorySlug) || string.Equals(ctx.View, "categories", StringComparison.OrdinalIgnoreCase)) ? "gaming-sub" : "hub";
        }
        else if (isLuggageCategory)
        {
            determinedViewMode = "hub";
        }
        else if (!string.IsNullOrWhiteSpace(ctx.View))
        {
            determinedViewMode = ctx.View.Trim().ToLowerInvariant();
        }
        else if (elecBrandShowcase != null || appBrandShowcase != null || mobilesBrandShowcase != null)
        {
            determinedViewMode = "brand";
        }
        else if (isWomenSubcategory) determinedViewMode = "women-sub";
        else if (isMenFootwearSubcategory) determinedViewMode = "men-footwear-sub";
        else if (isMenFootwearCategory) determinedViewMode = "men-footwear";
        else if (isMenClothingSubcategory) determinedViewMode = "men-clothing-sub";
        else if (isEthnicWearCategory) determinedViewMode = "ethnic-wear";
        else if (isThermalsCategory) determinedViewMode = "thermals";
        else if (isHoodiesCategory) determinedViewMode = "hoodies";
        else if (isSweatshirtsCategory) determinedViewMode = "sweatshirts";
        else if (isWomenClothingCategory) determinedViewMode = "women-clothing";
        else if (isMenClothingCategory) determinedViewMode = "men-clothing";
        else if ((isStudyCategory || isFashionCategory || isHealthCategory || (isMobilesCategory && mobilesBrandShowcase == null) || isAppliancesCategory || isBeautyCategory || isFoodCategory || isTravelCategory || isElectronicsCategory || isStoryBooksCategory) && !subcategoryId.HasValue && string.IsNullOrWhiteSpace(currentSubcategorySlug))
        {
            determinedViewMode = "hub";
        }
        else
        {
            determinedViewMode = "categories";
        }

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
            CategorySlug = isSportsCategory ? "sports-hub" : (isLuggageCategory ? "luggage-bags" : (isGamingCategory ? "gaming-hub" : (isMobilesCategory ? "mobiles" : (result.CurrentCategorySlug ?? effectiveCategory)))),
            CategoryName = isSportsCategory
                           ? (!string.IsNullOrWhiteSpace(currentSubcategoryName) ? currentSubcategoryName : "Sports Hub")
                           : (isGamingCategory
                           ? (!string.IsNullOrWhiteSpace(currentSubcategoryName) ? currentSubcategoryName : "Gaming Hub")
                           : ((isStudyCategory && !string.IsNullOrWhiteSpace(currentSubcategoryName))
                           ? currentSubcategoryName
                           : (isEthnicWearCategory ? "Ethnic Wear" :
                           (isThermalsCategory ? "Thermals" :
                           (isHoodiesCategory ? "Hoodies" :
                           (isSweatshirtsCategory ? "Sweatshirts" :
                           (isMenFootwearCategory ? "Men Footwear" : (isWomenClothingCategory ? "Women Clothing" :
                           (isMenClothingCategory ? "Men Clothing" :
                           (isLuggageCategory ? "Luggage & Bags" :
                           (result.CurrentCategoryName ?? catDetails?.Name))))))))))),
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
            MinPrice = ctx.MinPrice,
            MaxPrice = ctx.MaxPrice,
            InStockOnly = ctx.InStockOnly,
            MinDiscount = ctx.MinDiscount,
            Sort = ctx.Sort,
            Categories = result.Categories,
            PriceRanges = result.PriceRanges,
            Discounts = result.Discounts,
            ViewMode = determinedViewMode,
            StudyHub = isStudyCategory
                        ? await _studyHubService.GetConfigAsync(cancellationToken)
                        : null,
            FashionHub = isAnyFashionTemplate
                        ? await _fashionHubService.GetConfigAsync(cancellationToken)
                        : null,
            HealthHub = isHealthCategory
                        ? await _healthHubService.GetConfigAsync(cancellationToken)
                        : null,
            MobilesHub = mobilesHubConfig ?? (isMobilesCategory ? await _mobilesHubService.GetConfigAsync(cancellationToken) : null),
            MobilesBrandShowcase = mobilesBrandShowcase,
            AppliancesHub = isAppliancesCategory
                        ? await _appliancesHubService.GetConfigAsync(cancellationToken)
                        : null,
            BeautyHub = isBeautyCategory
                        ? await _beautyHubService.GetConfigAsync(cancellationToken)
                        : null,
            FoodHub = isFoodCategory
                        ? await _foodHubService.GetConfigAsync(cancellationToken)
                        : null,
            TravelHub = isTravelCategory && !isLuggageCategory
                        ? await _travelHubService.GetConfigAsync(cancellationToken)
                        : null,
            LuggageHub = isLuggageCategory
                        ? await _luggageHubService.GetConfigAsync(cancellationToken)
                        : null,
            ElectronicsHub = isElectronicsCategory
                        ? await _electronicsHubService.GetConfigAsync(cancellationToken)
                        : null,
            StoryBooksHub = (isStoryBooksCategory && _storyBooksHubService != null)
                        ? await _storyBooksHubService.GetConfigAsync(cancellationToken)
                        : null,
            SportsHub = sportsHubConfig,
            SportsSubcategoryPage = sportsSubPage,
            GamingHub = gamingHubConfig,
            GamingSubcategoryPage = gamingSubPage,
            WomenSubcategoryPage = womenSubPage,
            IsWomenSubcategory = isWomenSubcategory,
            MenFootwearSubcategoryPage = menFootwearSubPage,
            IsMenFootwearSubcategory = isMenFootwearSubcategory,
            MenClothingSubcategoryPage = menClothingSubPage,
            IsMenClothingSubcategory = isMenClothingSubcategory,
            CurrentAppliancesBrandSlug = currentAppliancesBrandSlug,
            CurrentAppliancesBrandName = currentAppliancesBrandName,
            AppliancesBrandHero = appBrandShowcase?.HeroBanner,
            AppliancesBrandFilters = appBrandShowcase?.FilterGroups,
            AppliancesBrandProducts = appBrandShowcase?.Products,
            CurrentBrandSlug = currentBrandSlug,
            CurrentBrandName = currentBrandName,
            ElectronicsBrandHero = elecBrandShowcase?.HeroBanner,
            ElectronicsBrandFilters = elecBrandShowcase?.FilterGroups,
            ElectronicsBrandProducts = elecBrandShowcase?.Products
        };

        return model;
    }
}
