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
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = BeautyHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
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
        if (dto.SubCategories.Items == null || dto.SubCategories.Items.Count == 0)
        {
            dto.SubCategories.Items = def.SubCategories.Items;
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
