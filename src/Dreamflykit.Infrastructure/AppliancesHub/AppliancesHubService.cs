using Dreamflykit.Application.AppliancesHub.Interfaces;
using Dreamflykit.Application.AppliancesHub.Models;
using Dreamflykit.Application.Catalog.Interfaces;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.AppliancesHub;

public sealed class AppliancesHubService : IAppliancesHubService
{
    private const string HubSlug = "appliances";
    private const string HubTitle = "Appliances Hub";
    private const string FallbackFileName = "appliances_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<AppliancesHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public AppliancesHubService(
        ICategoryHubRepository hubRepository,
        ILogger<AppliancesHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public AppliancesHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<AppliancesHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<AppliancesHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<AppliancesHubDto>(HubSlug, FallbackFileName, cancellationToken);
        if (dto != null)
        {
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = AppliancesHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
    }

    public async Task UpdateConfigAsync(AppliancesHubDto dto, Guid? updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, dto, cancellationToken);
        _logger.LogInformation("Appliances Hub configuration updated in PostgreSQL by {UserId}", updatedBy);
    }

    private static void EnsureDefaults(AppliancesHubDto dto)
    {
        var def = AppliancesHubDto.CreateDefault();
        dto.ThemeSettings ??= def.ThemeSettings;
        dto.HeroBanner ??= def.HeroBanner;
        if (dto.HeroBanner.Slides == null || dto.HeroBanner.Slides.Count == 0)
        {
            dto.HeroBanner.Slides = def.HeroBanner.Slides;
        }
        dto.BankOffer ??= def.BankOffer;
        dto.SubCategories ??= def.SubCategories;
        if (dto.SubCategories.Items == null || dto.SubCategories.Items.Count == 0)
        {
            dto.SubCategories.Items = def.SubCategories.Items;
        }
        dto.BrandSpotlight ??= def.BrandSpotlight;
        dto.CreditPromos ??= def.CreditPromos;
        dto.LimitedTimeDeals ??= def.LimitedTimeDeals;
        if (dto.LimitedTimeDeals.Slots == null || dto.LimitedTimeDeals.Slots.Count == 0)
        {
            dto.LimitedTimeDeals.Slots = def.LimitedTimeDeals.Slots;
        }
        dto.GoldenHighlights ??= def.GoldenHighlights;
        if (dto.GoldenHighlights.Items == null || dto.GoldenHighlights.Items.Count == 0)
        {
            dto.GoldenHighlights.Items = def.GoldenHighlights.Items;
        }
        dto.HomeAppliancesDeals ??= def.HomeAppliancesDeals;
        dto.KitchenAppliancesDeals ??= def.KitchenAppliancesDeals;
        dto.NoCostEmiSection ??= def.NoCostEmiSection;
        dto.EverydayLivingSection ??= def.EverydayLivingSection;
        dto.SubcategoryPages ??= def.SubcategoryPages;
        if (dto.SubcategoryPages == null || dto.SubcategoryPages.Count == 0)
        {
            dto.SubcategoryPages = def.SubcategoryPages;
        }
    }
}
