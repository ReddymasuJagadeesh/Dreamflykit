using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.TravelHub.Interfaces;
using Dreamflykit.Application.TravelHub.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.TravelHub;

public sealed class TravelHubService : ITravelHubService
{
    private const string CacheKey = "travel_category_hub_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<TravelHubService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public TravelHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<TravelHubService> logger)
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
                _logger.LogWarning(ex, "Could not create data directory: {DataDir}", dataDir);
            }
        }

        _filePath = Path.Combine(dataDir, "travel_hub.json");
    }

    public async Task<TravelHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out TravelHubDto? cached) && cached != null)
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
                    var dto = JsonSerializer.Deserialize<TravelHubDto>(json, JsonOptions);
                    if (dto != null)
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

                        _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(10));
                        return dto;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load Travel Hub JSON from file: {Path}", _filePath);
            }
        }

        var fallback = TravelHubDto.CreateDefault();
        try
        {
            var fallbackJson = JsonSerializer.Serialize(fallback, JsonOptions);
            await File.WriteAllTextAsync(_filePath, fallbackJson, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to save initial default Travel Hub config to {Path}", _filePath);
        }

        _cache.Set(CacheKey, fallback, TimeSpan.FromMinutes(10));
        return fallback;
    }

    public async Task UpdateConfigAsync(
        TravelHubDto dto,
        string? updatedBy = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        try
        {
            var json = JsonSerializer.Serialize(dto, JsonOptions);
            await File.WriteAllTextAsync(_filePath, json, cancellationToken);
            _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(10));
            _logger.LogInformation("Travel Hub configuration successfully updated by {User}", updatedBy ?? "System");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write Travel Hub JSON file: {Path}", _filePath);
            throw;
        }
    }
}

