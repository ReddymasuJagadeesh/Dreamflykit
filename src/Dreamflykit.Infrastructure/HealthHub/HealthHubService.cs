using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.HealthHub.Interfaces;
using Dreamflykit.Application.HealthHub.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.HealthHub;

public sealed class HealthHubService : IHealthHubService
{
    private const string CacheKey = "health_category_hub_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<HealthHubService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public HealthHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<HealthHubService> logger)
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

        _filePath = Path.Combine(dataDir, "health_hub.json");
    }

    public async Task<HealthHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out HealthHubDto? cached) && cached != null)
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
                    var dto = JsonSerializer.Deserialize<HealthHubDto>(json, JsonOptions);
                    if (dto != null)
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

                        _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
                        return dto;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed reading health hub config from {FilePath}", _filePath);
            }
        }

        var defaultDto = HealthHubDto.CreateDefault();
        try
        {
            var defaultJson = JsonSerializer.Serialize(defaultDto, JsonOptions);
            await File.WriteAllTextAsync(_filePath, defaultJson, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not write initial health hub json to {FilePath}", _filePath);
        }

        _cache.Set(CacheKey, defaultDto, TimeSpan.FromHours(4));
        return defaultDto;
    }

    public async Task UpdateConfigAsync(HealthHubDto dto, Guid updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        try
        {
            var json = JsonSerializer.Serialize(dto, JsonOptions);
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            await File.WriteAllTextAsync(_filePath, json, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write Health Hub config to {FilePath}", _filePath);
            throw new InvalidOperationException("Failed to persist Health Hub configuration: " + ex.Message, ex);
        }

        _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
        _logger.LogInformation("Health Hub configuration updated successfully by {UserId}", updatedBy);
    }

    public async Task ResetToDefaultAsync(CancellationToken cancellationToken = default)
    {
        var defaultDto = HealthHubDto.CreateDefault();
        await UpdateConfigAsync(defaultDto, Guid.Empty, cancellationToken);
    }
}
