using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.TravelHub.Interfaces;
using Dreamflykit.Application.TravelHub.Models;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.TravelHub;

public sealed class TravelHubService : ITravelHubService
{
    private const string HubSlug = "travel-gear";
    private const string HubTitle = "Travel Gear Hub";
    private const string FallbackFileName = "travel_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<TravelHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public TravelHubService(
        ICategoryHubRepository hubRepository,
        ILogger<TravelHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public TravelHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<TravelHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<TravelHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<TravelHubDto>(HubSlug, FallbackFileName, cancellationToken);
        if (dto != null)
        {
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = TravelHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
    }

    public async Task UpdateConfigAsync(
        TravelHubDto dto,
        string? updatedBy = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, dto, cancellationToken);
        _logger.LogInformation("Travel Hub configuration successfully updated in PostgreSQL by {User}", updatedBy ?? "System");
    }

    private static void EnsureDefaults(TravelHubDto dto)
    {
        var def = TravelHubDto.CreateDefault();
        dto.ThemeSettings ??= def.ThemeSettings;
        dto.TopNoticeBar ??= def.TopNoticeBar;
        dto.HeroBanner ??= def.HeroBanner;
        if (dto.HeroBanner.Badges == null || dto.HeroBanner.Badges.Count == 0)
        {
            dto.HeroBanner.Badges = def.HeroBanner.Badges;
        }
        if (dto.HeroBanner.PerfectJourneys == null || dto.HeroBanner.PerfectJourneys.Count == 0)
        {
            dto.HeroBanner.PerfectJourneys = def.HeroBanner.PerfectJourneys;
        }

        dto.Sidebar ??= def.Sidebar;
        if (dto.Sidebar.TravelForItems == null || dto.Sidebar.TravelForItems.Count == 0)
        {
            dto.Sidebar.TravelForItems = def.Sidebar.TravelForItems;
        }
        dto.Sidebar.QuizCard ??= def.Sidebar.QuizCard;

        dto.ShopByCategories ??= def.ShopByCategories;
        if (dto.ShopByCategories.Cards == null || dto.ShopByCategories.Cards.Count == 0)
        {
            dto.ShopByCategories.Cards = def.ShopByCategories.Cards;
        }

        dto.BottomTrustBar ??= def.BottomTrustBar;
        if (dto.BottomTrustBar.Badges == null || dto.BottomTrustBar.Badges.Count == 0)
        {
            dto.BottomTrustBar.Badges = def.BottomTrustBar.Badges;
        }
    }
}
