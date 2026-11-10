using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.MobilesHub.Interfaces;
using Dreamflykit.Application.MobilesHub.Models;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.MobilesHub;

public sealed class MobilesHubService : IMobilesHubService
{
    private const string HubSlug = "mobiles";
    private const string HubTitle = "Mobiles Hub";
    private const string FallbackFileName = "mobiles_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<MobilesHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public MobilesHubService(
        ICategoryHubRepository hubRepository,
        ILogger<MobilesHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public MobilesHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<MobilesHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<MobilesHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<MobilesHubDto>(HubSlug, FallbackFileName, cancellationToken);
        if (dto != null)
        {
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = MobilesHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
    }

    public async Task UpdateConfigAsync(MobilesHubDto config, Guid? updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(config);
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, config, cancellationToken);
        _logger.LogInformation("Mobiles Hub configuration updated successfully in PostgreSQL by {UserId}", updatedBy);
    }

    public async Task ResetToDefaultAsync(CancellationToken cancellationToken = default)
    {
        var defaultDto = MobilesHubDto.CreateDefault();
        await UpdateConfigAsync(defaultDto, Guid.Empty, cancellationToken);
    }

    private static void EnsureDefaults(MobilesHubDto dto)
    {
        var def = MobilesHubDto.CreateDefault();
        dto.HeroBanner ??= def.HeroBanner;
        if (dto.HeroBanner.Slides == null || dto.HeroBanner.Slides.Count == 0)
        {
            dto.HeroBanner.Slides = def.HeroBanner.Slides;
        }
        dto.BrandsSection ??= def.BrandsSection;
        dto.HighlightBanner ??= def.HighlightBanner;
        if (dto.HighlightBanner.Sections == null || dto.HighlightBanner.Sections.Count == 0)
        {
            dto.HighlightBanner.Sections = def.HighlightBanner.Sections;
        }
        dto.TopDealsSection ??= def.TopDealsSection;
        dto.ConfusedSection ??= def.ConfusedSection;
        dto.PromoBanner ??= def.PromoBanner;
        dto.UnmissableDealsSection ??= def.UnmissableDealsSection;
    }
}
