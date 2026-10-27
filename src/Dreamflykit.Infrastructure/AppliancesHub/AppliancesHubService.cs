using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.AppliancesHub.Interfaces;
using Dreamflykit.Application.AppliancesHub.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.AppliancesHub;

public sealed class AppliancesHubService : IAppliancesHubService
{
    private const string CacheKey = "appliances_category_hub_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<AppliancesHubService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public AppliancesHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<AppliancesHubService> logger)
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

        _filePath = Path.Combine(dataDir, "appliances_hub.json");
    }

    public async Task<AppliancesHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out AppliancesHubDto? cached) && cached != null)
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
                    var dto = JsonSerializer.Deserialize<AppliancesHubDto>(json, JsonOptions);
                    if (dto != null)
                    {
                        var def = AppliancesHubDto.CreateDefault();
                        dto.ThemeSettings ??= def.ThemeSettings;
                        dto.HeroBanner ??= def.HeroBanner;
                        if (dto.HeroBanner.Slides == null || dto.HeroBanner.Slides.Count == 0)
                        {
                            dto.HeroBanner.Slides = def.HeroBanner.Slides;
                        }
                        dto.BankOffer ??= def.BankOffer;
                        dto.SubCategories ??= def.SubCategories;
                        if (dto.SubCategories.Items == null || dto.SubCategories.Items.Count == 0)
                        {
                            dto.SubCategories.Items = def.SubCategories.Items;
                        }
                        dto.BrandSpotlight ??= def.BrandSpotlight;
                        dto.CreditPromos ??= def.CreditPromos;
                        dto.LimitedTimeDeals ??= def.LimitedTimeDeals;
                        if (dto.LimitedTimeDeals.Slots == null || dto.LimitedTimeDeals.Slots.Count == 0)
                        {
                            dto.LimitedTimeDeals.Slots = def.LimitedTimeDeals.Slots;
                        }
                        dto.GoldenHighlights ??= def.GoldenHighlights;
                        if (dto.GoldenHighlights.Items == null || dto.GoldenHighlights.Items.Count == 0)
                        {
                            dto.GoldenHighlights.Items = def.GoldenHighlights.Items;
                        }
                        dto.HomeAppliancesDeals ??= def.HomeAppliancesDeals;
                        dto.KitchenAppliancesDeals ??= def.KitchenAppliancesDeals;
                        dto.NoCostEmiSection ??= def.NoCostEmiSection;
                        dto.EverydayLivingSection ??= def.EverydayLivingSection;

                        _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
                        return dto;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed reading appliances hub config from {FilePath}", _filePath);
            }
        }

        var defaultDto = AppliancesHubDto.CreateDefault();
        _cache.Set(CacheKey, defaultDto, TimeSpan.FromHours(4));
        return defaultDto;
    }

    public async Task UpdateConfigAsync(AppliancesHubDto dto, Guid? updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        try
        {
            var json = JsonSerializer.Serialize(dto, JsonOptions);
            await File.WriteAllTextAsync(_filePath, json, cancellationToken);
            _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
            _logger.LogInformation("Appliances Hub configuration updated by {UserId}", updatedBy);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed writing appliances hub config to {FilePath}", _filePath);
            throw;
        }
    }
}
