using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.LuggageHub.Interfaces;
using Dreamflykit.Application.LuggageHub.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.LuggageHub;

public sealed class LuggageHubService : ILuggageHubService
{
    private const string CacheKey = "luggage_category_hub_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<LuggageHubService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public LuggageHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<LuggageHubService> logger)
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

        _filePath = Path.Combine(dataDir, "luggage_hub.json");
    }

    public async Task<LuggageHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out LuggageHubDto? cached) && cached != null)
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
                    var dto = JsonSerializer.Deserialize<LuggageHubDto>(json, JsonOptions);
                    if (dto != null)
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

                        _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(10));
                        return dto;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load Luggage Hub JSON from file: {Path}", _filePath);
            }
        }

        var fallback = LuggageHubDto.CreateDefault();
        try
        {
            var fallbackJson = JsonSerializer.Serialize(fallback, JsonOptions);
            await File.WriteAllTextAsync(_filePath, fallbackJson, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to save initial default Luggage Hub config to {Path}", _filePath);
        }

        _cache.Set(CacheKey, fallback, TimeSpan.FromMinutes(10));
        return fallback;
    }

    public async Task UpdateConfigAsync(
        LuggageHubDto dto,
        string? updatedBy = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        try
        {
            var json = JsonSerializer.Serialize(dto, JsonOptions);
            await File.WriteAllTextAsync(_filePath, json, cancellationToken);
            _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(10));
            _logger.LogInformation("Luggage Hub configuration successfully updated by {User}", updatedBy ?? "System");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write Luggage Hub JSON file: {Path}", _filePath);
            throw;
        }
    }
}

