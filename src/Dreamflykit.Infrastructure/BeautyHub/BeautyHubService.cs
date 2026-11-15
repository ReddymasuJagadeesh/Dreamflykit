using Dreamflykit.Application.BeautyHub.Interfaces;
using Dreamflykit.Application.BeautyHub.Models;
using Dreamflykit.Application.Catalog.Interfaces;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.BeautyHub;

public sealed class BeautyHubService : IBeautyHubService
{
    private const string HubSlug = "beauty";
    private const string HubTitle = "Beauty Hub";
    private const string FallbackFileName = "beauty_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<BeautyHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public BeautyHubService(
        ICategoryHubRepository hubRepository,
        ILogger<BeautyHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public BeautyHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<BeautyHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<BeautyHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<BeautyHubDto>(HubSlug, FallbackFileName, cancellationToken);
        if (dto != null)
        {
            bool needsResave = dto.SubCategories?.Items == null ||
                               dto.SubCategories.Items.Count != 9 ||
                               dto.SubCategories.Items.Any(i => i.Slug == "appliances" || i.Slug == "natural" || i.Slug == "mom-baby" || i.Slug == "health-wellness" || i.Slug == "men" || i.Slug == "fashion-selects" || i.Slug == "skincare" || i.Slug == "top-50-deals" || i.Slug == "mens-grooming" || i.Slug == "derma-store" || i.Slug == "k-beauty") ||
                               dto.SubcategoryPages == null ||
                               dto.SubcategoryPages.Count < BeautyHubDto.CreateDefaultSubcategoryPages().Count;

            EnsureDefaults(dto);

            if (needsResave)
            {
                await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, dto, cancellationToken);
            }

            return dto;
        }

        var defaultDto = BeautyHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
    }

    public async Task<BeautyCategoryPageDto?> GetCategoryPageAsync(string categorySlug, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(categorySlug)) return null;

        var config = await GetConfigAsync(cancellationToken);
        var page = config.CategoryPages?.FirstOrDefault(p => string.Equals(p.CategorySlug, categorySlug, StringComparison.OrdinalIgnoreCase));
        if (page != null) return page;

        return BeautyHubDto.CreateDefaultCategoryPages()
            .FirstOrDefault(p => string.Equals(p.CategorySlug, categorySlug, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<BeautySubcategoryPageDto?> GetSubcategoryPageAsync(string categorySlug, string? subcategorySlug = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(categorySlug)) return null;

        var config = await GetConfigAsync(cancellationToken);
        if (!string.IsNullOrWhiteSpace(subcategorySlug))
        {
            var match = config.SubcategoryPages?.FirstOrDefault(p =>
                string.Equals(p.CategorySlug, categorySlug, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(p.SubcategorySlug, subcategorySlug, StringComparison.OrdinalIgnoreCase));

            if (match != null) return match;

            var defaultMatch = BeautyHubDto.CreateDefaultSubcategoryPages().FirstOrDefault(p =>
                string.Equals(p.CategorySlug, categorySlug, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(p.SubcategorySlug, subcategorySlug, StringComparison.OrdinalIgnoreCase));

            if (defaultMatch != null) return defaultMatch;

            return BeautyHubDto.GenerateDynamicSubcategoryPage(categorySlug, subcategorySlug);
        }

        // Return first subcategory page of this category if subcategory not explicitly specified
        var firstMatch = config.SubcategoryPages?.FirstOrDefault(p =>
            string.Equals(p.CategorySlug, categorySlug, StringComparison.OrdinalIgnoreCase));

        if (firstMatch != null) return firstMatch;

        var defFirst = BeautyHubDto.CreateDefaultSubcategoryPages().FirstOrDefault(p =>
            string.Equals(p.CategorySlug, categorySlug, StringComparison.OrdinalIgnoreCase));

        if (defFirst != null) return defFirst;

        var catPage = await GetCategoryPageAsync(categorySlug, cancellationToken);
        var firstSubSlug = catPage?.Subcategories?.FirstOrDefault()?.SubcategorySlug ?? "all";

        return BeautyHubDto.GenerateDynamicSubcategoryPage(categorySlug, firstSubSlug);
    }

    public async Task UpdateConfigAsync(
        BeautyHubDto dto,
        string? updatedBy = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, dto, cancellationToken);
        _logger.LogInformation("Beauty Hub configuration successfully updated in PostgreSQL by {User}", updatedBy ?? "System");
    }

    public async Task ResetToDefaultAsync(CancellationToken cancellationToken = default)
    {
        var defaultDto = BeautyHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        _logger.LogInformation("Beauty Hub configuration successfully reset to default factory state.");
    }

    private static void EnsureDefaults(BeautyHubDto dto)
    {
        var def = BeautyHubDto.CreateDefault();
        dto.ThemeSettings ??= def.ThemeSettings;
        dto.HeroBanner ??= def.HeroBanner;
        if (dto.HeroBanner.Slides == null || dto.HeroBanner.Slides.Count == 0)
        {
            dto.HeroBanner.Slides = def.HeroBanner.Slides;
        }

        dto.PromoBanners ??= def.PromoBanners;
        if (dto.PromoBanners.Cards == null || dto.PromoBanners.Cards.Count == 0)
        {
            dto.PromoBanners.Cards = def.PromoBanners.Cards;
        }

        dto.SubCategories ??= def.SubCategories;
        dto.SubCategories.Title = "Shop by Category";
        // Ensure strictly the 9 core categories from user spec are present (remove legacy 17 shortcuts or 11 items)
        if (dto.SubCategories.Items == null || dto.SubCategories.Items.Count != 9 ||
            dto.SubCategories.Items.Any(i => i.Slug == "appliances" || i.Slug == "natural" || i.Slug == "mom-baby" || i.Slug == "health-wellness" || i.Slug == "men" || i.Slug == "fashion-selects" || i.Slug == "skincare" || i.Slug == "top-50-deals" || i.Slug == "mens-grooming" || i.Slug == "derma-store" || i.Slug == "k-beauty"))
        {
            dto.SubCategories.Items = def.SubCategories.Items;
            dto.SubCategories.Title = "Shop by Category";
        }

        dto.CategoryPages ??= def.CategoryPages;
        if (dto.CategoryPages.Count == 0 || dto.CategoryPages.Count != 9 ||
            dto.CategoryPages.Any(p => p.CategorySlug == "appliances" || p.CategorySlug == "natural" || p.CategorySlug == "men" || p.CategorySlug == "fashion-selects"))
        {
            dto.CategoryPages = def.CategoryPages;
        }

        dto.SubcategoryPages ??= def.SubcategoryPages;
        if (dto.SubcategoryPages.Count == 0 || dto.SubcategoryPages.Count < def.SubcategoryPages.Count || dto.SubcategoryPages.Any(p => p.CategorySlug == "appliances" || p.CategorySlug == "natural"))
        {
            dto.SubcategoryPages = def.SubcategoryPages;
        }

        dto.SpotlightBanner ??= def.SpotlightBanner;
        dto.SpecialGiftsForHer ??= def.SpecialGiftsForHer;
        if (dto.SpecialGiftsForHer.Cards == null || dto.SpecialGiftsForHer.Cards.Count == 0)
        {
            dto.SpecialGiftsForHer.Cards = def.SpecialGiftsForHer.Cards;
        }

        dto.GrandGiftsForHim ??= def.GrandGiftsForHim;
        if (dto.GrandGiftsForHim.Cards == null || dto.GrandGiftsForHim.Cards.Count == 0)
        {
            dto.GrandGiftsForHim.Cards = def.GrandGiftsForHim.Cards;
        }

        dto.DualBanners ??= def.DualBanners;
        dto.PremiumGifts ??= def.PremiumGifts;
        if (dto.PremiumGifts.Brands == null || dto.PremiumGifts.Brands.Count == 0)
        {
            dto.PremiumGifts.Brands = def.PremiumGifts.Brands;
        }

        dto.FestiveLooks ??= def.FestiveLooks;
        if (dto.FestiveLooks.Cards == null || dto.FestiveLooks.Cards.Count == 0)
        {
            dto.FestiveLooks.Cards = def.FestiveLooks.Cards;
        }

        dto.LimitedDealsRibbon ??= def.LimitedDealsRibbon;
        dto.FlashDealsTimer ??= def.FlashDealsTimer;
        if (dto.FlashDealsTimer.Cards == null || dto.FlashDealsTimer.Cards.Count == 0)
        {
            dto.FlashDealsTimer.Cards = def.FlashDealsTimer.Cards;
        }

        dto.BeautyPicksUnder99 ??= def.BeautyPicksUnder99;
        dto.BiggestBrands ??= def.BiggestBrands;
        if (dto.BiggestBrands.Brands == null || dto.BiggestBrands.Brands.Count == 0)
        {
            dto.BiggestBrands.Brands = def.BiggestBrands.Brands;
        }

        dto.BeautyPartners ??= def.BeautyPartners;
        if (dto.BeautyPartners.Items == null || dto.BeautyPartners.Items.Count == 0)
        {
            dto.BeautyPartners.Items = def.BeautyPartners.Items;
        }

        dto.BrandNewInBeauty ??= def.BrandNewInBeauty;
        if (dto.BrandNewInBeauty.Cards == null || dto.BrandNewInBeauty.Cards.Count == 0)
        {
            dto.BrandNewInBeauty.Cards = def.BrandNewInBeauty.Cards;
        }
    }
}
