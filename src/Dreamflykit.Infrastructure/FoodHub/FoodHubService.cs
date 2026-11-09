using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.FoodHub.Interfaces;
using Dreamflykit.Application.FoodHub.Models;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.FoodHub;

public sealed class FoodHubService : IFoodHubService
{
    private const string HubSlug = "food";
    private const string HubTitle = "Food Hub";
    private const string FallbackFileName = "food_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<FoodHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public FoodHubService(
        ICategoryHubRepository hubRepository,
        ILogger<FoodHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public FoodHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<FoodHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<FoodHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<FoodHubDto>(HubSlug, FallbackFileName, cancellationToken);
        if (dto != null)
        {
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = FoodHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
    }

    public async Task UpdateConfigAsync(
        FoodHubDto dto,
        string? updatedBy = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, dto, cancellationToken);
        _logger.LogInformation("Food Hub configuration successfully updated in PostgreSQL by {User}", updatedBy ?? "System");
    }

    private static void EnsureDefaults(FoodHubDto dto)
    {
        var def = FoodHubDto.CreateDefault();
        dto.ThemeSettings ??= def.ThemeSettings;
        dto.TopNoticeBar ??= def.TopNoticeBar;
        dto.HeroBanner ??= def.HeroBanner;
        if (dto.HeroBanner.TrustBadges == null || dto.HeroBanner.TrustBadges.Count == 0)
        {
            dto.HeroBanner.TrustBadges = def.HeroBanner.TrustBadges;
        }
        if (dto.HeroBanner.Slides == null || dto.HeroBanner.Slides.Count == 0)
        {
            dto.HeroBanner.Slides = def.HeroBanner.Slides;
        }

        dto.ShopByCategories ??= def.ShopByCategories;
        if (dto.ShopByCategories.Cards == null || dto.ShopByCategories.Cards.Count == 0)
        {
            dto.ShopByCategories.Cards = def.ShopByCategories.Cards;
        }

        dto.PromoCards ??= def.PromoCards;
        if (dto.PromoCards.Cards == null || dto.PromoCards.Cards.Count == 0)
        {
            dto.PromoCards.Cards = def.PromoCards.Cards;
        }

        dto.BestSellers ??= def.BestSellers;
        if (dto.BestSellers.Items == null || dto.BestSellers.Items.Count == 0)
        {
            dto.BestSellers.Items = def.BestSellers.Items;
        }

        dto.BottomTrustBar ??= def.BottomTrustBar;
        dto.StayUpdated ??= def.StayUpdated;
    }
}
