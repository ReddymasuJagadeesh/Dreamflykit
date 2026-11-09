using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.ElectronicsHub.Interfaces;
using Dreamflykit.Application.ElectronicsHub.Models;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.ElectronicsHub;

public sealed class ElectronicsHubService : IElectronicsHubService
{
    private const string HubSlug = "electronics";
    private const string HubTitle = "Electronics Hub";
    private const string FallbackFileName = "electronics_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<ElectronicsHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public ElectronicsHubService(
        ICategoryHubRepository hubRepository,
        ILogger<ElectronicsHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public ElectronicsHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<ElectronicsHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<ElectronicsHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<ElectronicsHubDto>(HubSlug, FallbackFileName, cancellationToken);
        if (dto != null)
        {
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = ElectronicsHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
    }

    public async Task UpdateConfigAsync(ElectronicsHubDto dto, Guid updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, dto, cancellationToken);
        _logger.LogInformation("Electronics Hub config successfully updated in PostgreSQL by user {UserId}.", updatedBy);
    }

    public async Task ResetToDefaultAsync(CancellationToken cancellationToken = default)
    {
        var defaultDto = ElectronicsHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        _logger.LogInformation("Electronics Hub config reset to defaults in PostgreSQL.");
    }

    private static void EnsureDefaults(ElectronicsHubDto dto)
    {
        var def = ElectronicsHubDto.CreateDefault();
        dto.ThemeSettings ??= def.ThemeSettings;
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
        if (dto.CategoriesSection.Categories == null || dto.CategoriesSection.Categories.Count == 0)
        {
            dto.CategoriesSection.Categories = def.CategoriesSection.Categories;
        }
        dto.CuratedProductsSection ??= def.CuratedProductsSection;
        dto.TrustBadgesSection ??= def.TrustBadgesSection;
        dto.BottomBanner ??= def.BottomBanner;
        dto.SubcategoryPages ??= def.SubcategoryPages;
        if (dto.SubcategoryPages == null || dto.SubcategoryPages.Count == 0)
        {
            dto.SubcategoryPages = def.SubcategoryPages;
        }
    }
}
