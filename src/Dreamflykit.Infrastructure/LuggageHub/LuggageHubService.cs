using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.LuggageHub.Interfaces;
using Dreamflykit.Application.LuggageHub.Models;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.LuggageHub;

public sealed class LuggageHubService : ILuggageHubService
{
    private const string HubSlug = "luggage";
    private const string HubTitle = "Luggage Hub";
    private const string FallbackFileName = "luggage_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<LuggageHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public LuggageHubService(
        ICategoryHubRepository hubRepository,
        ILogger<LuggageHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public LuggageHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<LuggageHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<LuggageHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<LuggageHubDto>(HubSlug, FallbackFileName, cancellationToken);
        if (dto != null)
        {
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = LuggageHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
    }

    public async Task UpdateConfigAsync(
        LuggageHubDto dto,
        string? updatedBy = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, dto, cancellationToken);
        _logger.LogInformation("Luggage Hub configuration successfully updated in PostgreSQL by {User}", updatedBy ?? "System");
    }

    private static void EnsureDefaults(LuggageHubDto dto)
    {
        var def = LuggageHubDto.CreateDefault();
        dto.FestivalTheme ??= def.FestivalTheme;
        dto.HeroBanner ??= def.HeroBanner;
        if (dto.HeroBanner.Badges == null || dto.HeroBanner.Badges.Count == 0)
        {
            dto.HeroBanner.Badges = def.HeroBanner.Badges;
        }
        dto.HeroBanner.OfferBox ??= def.HeroBanner.OfferBox;

        dto.ReassuranceBar ??= def.ReassuranceBar;
        if (dto.ReassuranceBar.Items == null || dto.ReassuranceBar.Items.Count == 0)
        {
            dto.ReassuranceBar.Items = def.ReassuranceBar.Items;
        }

        dto.ShopByCategory ??= def.ShopByCategory;
        if (dto.ShopByCategory.Cards == null || dto.ShopByCategory.Cards.Count == 0)
        {
            dto.ShopByCategory.Cards = def.ShopByCategory.Cards;
        }

        dto.PromoBanners ??= def.PromoBanners;
        if (dto.PromoBanners.Cards == null || dto.PromoBanners.Cards.Count == 0)
        {
            dto.PromoBanners.Cards = def.PromoBanners.Cards;
        }
    }
}
