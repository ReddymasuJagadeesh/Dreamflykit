using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.BeautyHub.Interfaces;
using Dreamflykit.Application.BeautyHub.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.BeautyHub;

public sealed class BeautyHubService : IBeautyHubService
{
    private const string CacheKey = "beauty_category_hub_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<BeautyHubService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public BeautyHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<BeautyHubService> logger)
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

        _filePath = Path.Combine(dataDir, "beauty_hub.json");
    }

    public async Task<BeautyHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out BeautyHubDto? cached) && cached != null)
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
                    var dto = JsonSerializer.Deserialize<BeautyHubDto>(json, JsonOptions);
                    if (dto != null)
                    {
                        var def = BeautyHubDto.CreateDefault();
                        dto.ThemeSettings ??= def.ThemeSettings;
                        dto.HeroBanner ??= def.HeroBanner;
                        if (dto.HeroBanner.Slides == null || dto.HeroBanner.Slides.Count == 0)
                        {
                            dto.HeroBanner.Slides = def.HeroBanner.Slides;
                        }

                        dto.PromoBanners ??= def.PromoBanners;
                        if (dto.PromoBanners.Cards == null || dto.PromoBanners.Cards.Count == 0)
                        {
                            dto.PromoBanners.Cards = def.PromoBanners.Cards;
                        }

                        dto.SubCategories ??= def.SubCategories;
                        if (dto.SubCategories.Items == null || dto.SubCategories.Items.Count == 0)
                        {
                            dto.SubCategories.Items = def.SubCategories.Items;
                        }

                        dto.SpotlightBanner ??= def.SpotlightBanner;
                        dto.SpecialGiftsForHer ??= def.SpecialGiftsForHer;
                        if (dto.SpecialGiftsForHer.Cards == null || dto.SpecialGiftsForHer.Cards.Count == 0)
                        {
                            dto.SpecialGiftsForHer.Cards = def.SpecialGiftsForHer.Cards;
                        }

                        dto.GrandGiftsForHim ??= def.GrandGiftsForHim;
                        if (dto.GrandGiftsForHim.Cards == null || dto.GrandGiftsForHim.Cards.Count == 0)
                        {
                            dto.GrandGiftsForHim.Cards = def.GrandGiftsForHim.Cards;
                        }

                        dto.DualBanners ??= def.DualBanners;
                        dto.PremiumGifts ??= def.PremiumGifts;
                        if (dto.PremiumGifts.Brands == null || dto.PremiumGifts.Brands.Count == 0)
                        {
                            dto.PremiumGifts.Brands = def.PremiumGifts.Brands;
                        }

                        dto.FestiveLooks ??= def.FestiveLooks;
                        if (dto.FestiveLooks.Cards == null || dto.FestiveLooks.Cards.Count == 0)
                        {
                            dto.FestiveLooks.Cards = def.FestiveLooks.Cards;
                        }

                        dto.LimitedDealsRibbon ??= def.LimitedDealsRibbon;
                        dto.FlashDealsTimer ??= def.FlashDealsTimer;
                        if (dto.FlashDealsTimer.Cards == null || dto.FlashDealsTimer.Cards.Count == 0)
                        {
                            dto.FlashDealsTimer.Cards = def.FlashDealsTimer.Cards;
                        }

                        dto.BeautyPicksUnder99 ??= def.BeautyPicksUnder99;
                        dto.BiggestBrands ??= def.BiggestBrands;
                        if (dto.BiggestBrands.Brands == null || dto.BiggestBrands.Brands.Count == 0)
                        {
                            dto.BiggestBrands.Brands = def.BiggestBrands.Brands;
                        }

                        dto.BeautyPartners ??= def.BeautyPartners;
                        if (dto.BeautyPartners.Items == null || dto.BeautyPartners.Items.Count == 0)
                        {
                            dto.BeautyPartners.Items = def.BeautyPartners.Items;
                        }

                        dto.BrandNewInBeauty ??= def.BrandNewInBeauty;
                        if (dto.BrandNewInBeauty.Cards == null || dto.BrandNewInBeauty.Cards.Count == 0)
                        {
                            dto.BrandNewInBeauty.Cards = def.BrandNewInBeauty.Cards;
                        }

                        _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(10));
                        return dto;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load Beauty Hub JSON from file: {Path}", _filePath);
            }
        }

        var fallback = BeautyHubDto.CreateDefault();
        try
        {
            var fallbackJson = JsonSerializer.Serialize(fallback, JsonOptions);
            await File.WriteAllTextAsync(_filePath, fallbackJson, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to save initial default Beauty Hub config to {Path}", _filePath);
        }

        _cache.Set(CacheKey, fallback, TimeSpan.FromMinutes(10));
        return fallback;
    }

    public async Task UpdateConfigAsync(
        BeautyHubDto dto,
        string? updatedBy = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        try
        {
            var json = JsonSerializer.Serialize(dto, JsonOptions);
            await File.WriteAllTextAsync(_filePath, json, cancellationToken);
            _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(10));
            _logger.LogInformation("Beauty Hub configuration successfully updated by {User}", updatedBy ?? "System");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write Beauty Hub JSON file: {Path}", _filePath);
            throw;
        }
    }
}

