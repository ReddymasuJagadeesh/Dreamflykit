using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.StoryBooksHub.Interfaces;
using Dreamflykit.Application.StoryBooksHub.Models;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.StoryBooksHub;

public sealed class StoryBooksHubService : IStoryBooksHubService
{
    private const string HubSlug = "storybooks";
    private const string HubTitle = "Story Books Hub";
    private const string FallbackFileName = "storybooks_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<StoryBooksHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public StoryBooksHubService(
        ICategoryHubRepository hubRepository,
        ILogger<StoryBooksHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public StoryBooksHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<StoryBooksHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<StoryBooksHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<StoryBooksHubDto>(HubSlug, FallbackFileName, cancellationToken);
        if (dto != null)
        {
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = StoryBooksHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
    }

    public async Task UpdateConfigAsync(StoryBooksHubDto config, Guid? updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(config);
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, config, cancellationToken);
        _logger.LogInformation("StoryBooks Hub configuration successfully updated in PostgreSQL by {UpdatedBy}", updatedBy);
    }

    public async Task ResetToDefaultAsync(CancellationToken cancellationToken = default)
    {
        var defaultDto = StoryBooksHubDto.CreateDefault();
        await UpdateConfigAsync(defaultDto, null, cancellationToken);
    }

    private static void EnsureDefaults(StoryBooksHubDto dto)
    {
        var def = StoryBooksHubDto.CreateDefault();
        dto.ThemeSettings ??= def.ThemeSettings;
        dto.HeroBillboard ??= def.HeroBillboard;
        if (dto.GenresAndJournals == null || dto.GenresAndJournals.Count == 0)
        {
            dto.GenresAndJournals = def.GenresAndJournals;
        }
        if (dto.Languages == null || dto.Languages.Count == 0)
        {
            dto.Languages = def.Languages;
        }
        if (dto.Preferences == null || dto.Preferences.Count == 0)
        {
            dto.Preferences = def.Preferences;
        }
        if (dto.Sections == null || dto.Sections.Count == 0)
        {
            dto.Sections = def.Sections;
        }
    }
}
