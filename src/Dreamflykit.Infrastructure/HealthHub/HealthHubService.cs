using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.HealthHub.Interfaces;
using Dreamflykit.Application.HealthHub.Models;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.HealthHub;

public sealed class HealthHubService : IHealthHubService
{
    private const string HubSlug = "health-care";
    private const string HubTitle = "Health Care Hub";
    private const string FallbackFileName = "health_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<HealthHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public HealthHubService(
        ICategoryHubRepository hubRepository,
        ILogger<HealthHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public HealthHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<HealthHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<HealthHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<HealthHubDto>(HubSlug, FallbackFileName, cancellationToken);
        if (dto != null)
        {
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = HealthHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
    }

    public async Task UpdateConfigAsync(HealthHubDto dto, Guid updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, dto, cancellationToken);
        _logger.LogInformation("Health Hub configuration updated successfully in PostgreSQL by {UserId}", updatedBy);
    }

    public async Task ResetToDefaultAsync(CancellationToken cancellationToken = default)
    {
        var defaultDto = HealthHubDto.CreateDefault();
        await UpdateConfigAsync(defaultDto, Guid.Empty, cancellationToken);
    }

    private static void EnsureDefaults(HealthHubDto dto)
    {
        var def = HealthHubDto.CreateDefault();
        dto.HeroBanner ??= def.HeroBanner;
        if (dto.HeroBanner.Slides == null || dto.HeroBanner.Slides.Count == 0)
        {
            dto.HeroBanner.Slides = def.HeroBanner.Slides;
        }
        dto.CategoriesSection ??= def.CategoriesSection;
        dto.PopularSection ??= def.PopularSection;
        dto.TrustBarSection ??= def.TrustBarSection;
    }
}
