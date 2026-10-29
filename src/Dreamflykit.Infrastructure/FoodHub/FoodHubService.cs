using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.FoodHub.Interfaces;
using Dreamflykit.Application.FoodHub.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.FoodHub;

public sealed class FoodHubService : IFoodHubService
{
    private const string CacheKey = "food_category_hub_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<FoodHubService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public FoodHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<FoodHubService> logger)
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

        _filePath = Path.Combine(dataDir, "food_hub.json");
    }

    public async Task<FoodHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out FoodHubDto? cached) && cached != null)
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
                    var dto = JsonSerializer.Deserialize<FoodHubDto>(json, JsonOptions);
                    if (dto != null)
                    {
                        var def = FoodHubDto.CreateDefault();
                        dto.ThemeSettings ??= def.ThemeSettings;
                        dto.TopNoticeBar ??= def.TopNoticeBar;
                        dto.HeroBanner ??= def.HeroBanner;
                        if (dto.HeroBanner.TrustBadges == null || dto.HeroBanner.TrustBadges.Count == 0)
                        {
                            dto.HeroBanner.TrustBadges = def.HeroBanner.TrustBadges;
                        }

                        dto.ShopByCategories ??= def.ShopByCategories;
                        if (dto.ShopByCategories.Cards == null || dto.ShopByCategories.Cards.Count == 0)
                        {
                            dto.ShopByCategories.Cards = def.ShopByCategories.Cards;
                        }

                        dto.PromoCards ??= def.PromoCards;
                        if (dto.PromoCards.Cards == null || dto.PromoCards.Cards.Count == 0)
                        {
                            dto.PromoCards.Cards = def.PromoCards.Cards;
                        }

                        dto.BestSellers ??= def.BestSellers;
                        if (dto.BestSellers.Items == null || dto.BestSellers.Items.Count == 0)
                        {
                            dto.BestSellers.Items = def.BestSellers.Items;
                        }

                        dto.BottomTrustBar ??= def.BottomTrustBar;
                        dto.StayUpdated ??= def.StayUpdated;

                        _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(10));
                        return dto;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load Food Hub JSON from file: {Path}", _filePath);
            }
        }

        var fallback = FoodHubDto.CreateDefault();
        try
        {
            var fallbackJson = JsonSerializer.Serialize(fallback, JsonOptions);
            await File.WriteAllTextAsync(_filePath, fallbackJson, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to save initial default Food Hub config to {Path}", _filePath);
        }

        _cache.Set(CacheKey, fallback, TimeSpan.FromMinutes(10));
        return fallback;
    }

    public async Task UpdateConfigAsync(
        FoodHubDto dto,
        string? updatedBy = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        try
        {
            var json = JsonSerializer.Serialize(dto, JsonOptions);
            await File.WriteAllTextAsync(_filePath, json, cancellationToken);
            _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(10));
            _logger.LogInformation("Food Hub configuration successfully updated by {User}", updatedBy ?? "System");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write Food Hub JSON file: {Path}", _filePath);
            throw;
        }
    }
}

