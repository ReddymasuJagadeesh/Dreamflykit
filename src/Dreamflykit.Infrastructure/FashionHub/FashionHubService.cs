using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.FashionHub.Interfaces;
using Dreamflykit.Application.FashionHub.Models;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.FashionHub;

public sealed class FashionHubService : IFashionHubService
{
    private const string HubSlug = "fashion";
    private const string HubTitle = "Fashion Hub";
    private const string FallbackFileName = "fashion_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<FashionHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public FashionHubService(
        ICategoryHubRepository hubRepository,
        ILogger<FashionHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public FashionHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<FashionHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<FashionHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<FashionHubDto>(HubSlug, FallbackFileName, cancellationToken);
        if (dto != null)
        {
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = FashionHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
    }

    public async Task UpdateConfigAsync(FashionHubDto dto, Guid updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (dto.EthnicWear == null || dto.Thermals == null || dto.MenClothing == null || dto.WomenClothing == null || dto.MenFootwear == null)
        {
            var existing = await GetConfigAsync(cancellationToken);
            dto.EthnicWear ??= existing.EthnicWear ?? FashionEthnicWearDto.CreateDefault();
            dto.Thermals ??= existing.Thermals ?? FashionThermalsDto.CreateDefault();
            dto.MenClothing ??= existing.MenClothing ?? FashionMenClothingDto.CreateDefault();
            dto.WomenClothing ??= existing.WomenClothing ?? FashionWomenClothingDto.CreateDefault();
            dto.MenFootwear ??= existing.MenFootwear ?? FashionMenFootwearDto.CreateDefault();
        }

        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, dto, cancellationToken);
        _logger.LogInformation("Fashion Hub configuration updated successfully in PostgreSQL by {UserId}", updatedBy);
    }

    public async Task ResetToDefaultAsync(CancellationToken cancellationToken = default)
    {
        var defaultDto = FashionHubDto.CreateDefault();
        await UpdateConfigAsync(defaultDto, Guid.Empty, cancellationToken);
    }

    private static void EnsureDefaults(FashionHubDto dto)
    {
        var def = FashionHubDto.CreateDefault();
        dto.HeroBanner ??= def.HeroBanner;
        if (dto.HeroBanner.Slides == null || dto.HeroBanner.Slides.Count == 0)
        {
            dto.HeroBanner.Slides = def.HeroBanner.Slides;
        }
        if (dto.HeroBanner.SlideIntervalSeconds <= 0)
        {
            dto.HeroBanner.SlideIntervalSeconds = 5;
        }
        dto.CategoriesSection ??= def.CategoriesSection;
        dto.TopPicksSection ??= def.TopPicksSection;
        dto.TrustBadgesSection ??= def.TrustBadgesSection;
        dto.BottomBanner ??= def.BottomBanner;
        if (dto.MenClothing == null || dto.MenClothing.CategoriesSection == null || dto.MenClothing.CategoriesSection.Categories.Count < 17)
        {
            dto.MenClothing = def.MenClothing;
        }
        else if (dto.MenClothing.SubcategoryPages == null || dto.MenClothing.SubcategoryPages.Count < 17)
        {
            dto.MenClothing.SubcategoryPages = def.MenClothing.SubcategoryPages;
        }
        dto.WomenClothing ??= def.WomenClothing;
        if (dto.MenFootwear == null || dto.MenFootwear.CategoriesSection == null || dto.MenFootwear.CategoriesSection.Categories.Count < 7)
        {
            dto.MenFootwear = def.MenFootwear;
        }
        dto.Hoodies ??= def.Hoodies;
        dto.Sweatshirts ??= def.Sweatshirts;
        if (dto.EthnicWear == null || dto.EthnicWear.CategoriesSection == null || dto.EthnicWear.CategoriesSection.Categories.Count < 15)
        {
            dto.EthnicWear = def.EthnicWear;
        }
        if (dto.Thermals == null || dto.Thermals.CircleItems == null || dto.Thermals.CircleItems.Count < 4)
        {
            dto.Thermals = def.Thermals;
        }
    }
}
