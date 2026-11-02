using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.StoryBooksHub.Interfaces;
using Dreamflykit.Application.StoryBooksHub.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.StoryBooksHub;

public sealed class StoryBooksHubService : IStoryBooksHubService
{
    private const string CacheKey = "storybooks_category_hub_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<StoryBooksHubService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public StoryBooksHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<StoryBooksHubService> logger)
    {
        _cache = cache;
        _env = env;
        _logger = logger;

        var rootPath = !string.IsNullOrWhiteSpace(env.WebRootPath)
            ? env.WebRootPath
            : Path.Combine(AppContext.BaseDirectory, "wwwroot");

        var dataDir = Path.Combine(rootPath, "data");
        if (!Directory.Exists(dataDir))
        {
            try
            {
                Directory.CreateDirectory(dataDir);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not create directory: {DataDir}", dataDir);
            }
        }

        _filePath = Path.Combine(dataDir, "storybooks_hub.json");
    }

    public async Task<StoryBooksHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out StoryBooksHubDto? cached) && cached != null)
        {
            return cached;
        }

        if (File.Exists(_filePath))
        {
            try
            {
                var json = await File.ReadAllTextAsync(_filePath, cancellationToken);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var dto = JsonSerializer.Deserialize<StoryBooksHubDto>(json, JsonOptions);
                    if (dto != null)
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

                        _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(30));
                        return dto;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading StoryBooks Hub configuration from {FilePath}. Falling back to default.", _filePath);
            }
        }

        var defaultDto = StoryBooksHubDto.CreateDefault();
        _cache.Set(CacheKey, defaultDto, TimeSpan.FromMinutes(30));
        return defaultDto;
    }

    public async Task UpdateConfigAsync(StoryBooksHubDto config, Guid? updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(config);

        try
        {
            var json = JsonSerializer.Serialize(config, JsonOptions);
            await File.WriteAllTextAsync(_filePath, json, cancellationToken);
            _cache.Set(CacheKey, config, TimeSpan.FromMinutes(30));
            _logger.LogInformation("StoryBooks Hub configuration successfully updated by {UpdatedBy}", updatedBy);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist StoryBooks Hub configuration to {FilePath}", _filePath);
            throw;
        }
    }

    public async Task ResetToDefaultAsync(CancellationToken cancellationToken = default)
    {
        var defaultDto = StoryBooksHubDto.CreateDefault();
        await UpdateConfigAsync(defaultDto, null, cancellationToken);
    }
}

