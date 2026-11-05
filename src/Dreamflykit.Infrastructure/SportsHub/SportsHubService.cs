using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.SportsHub.Interfaces;
using Dreamflykit.Application.SportsHub.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.SportsHub;

public sealed class SportsHubService : ISportsHubService
{
    private const string CacheKey = "sports_hub_config_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<SportsHubService> _logger;
    private readonly string _filePath;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public SportsHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<SportsHubService> logger)
    {
        _cache = cache;
        _env = env;
        _logger = logger;

        var rootPath = !string.IsNullOrWhiteSpace(env.WebRootPath)
            ? env.WebRootPath
            : Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        _filePath = Path.Combine(rootPath, "data", "sports_hub.json");
    }

    public async Task<SportsHubDto> GetConfigAsync(CancellationToken ct = default)
    {
        if (_cache.TryGetValue(CacheKey, out SportsHubDto? cached) && cached != null)
        {
            return cached;
        }

        await Semaphore.WaitAsync(ct);
        try
        {
            if (_cache.TryGetValue(CacheKey, out cached) && cached != null)
            {
                return cached;
            }

            SportsHubDto? dto = null;
            if (File.Exists(_filePath))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(_filePath, ct);
                    dto = JsonSerializer.Deserialize<SportsHubDto>(json, JsonOptions);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to read SportsHub configuration from {Path}", _filePath);
                }
            }

            if (dto == null)
            {
                dto = SportsHubDto.CreateDefault();
                await SaveToFileAsync(dto, ct);
            }

            // Ensure categories and subpages are populated
            if (dto.ShopByCategory == null || dto.ShopByCategory.Items == null || dto.ShopByCategory.Items.Count == 0)
            {
                dto.ShopByCategory = new SportsShopByCategorySectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Explore verified university tournament sports gear, workout equipment, and athletic apparel",
                    Items = SportsHubDto.CreateDefaultCategoryItems()
                };
            }

            if (dto.SubcategoryPages == null || dto.SubcategoryPages.Count == 0)
            {
                dto.SubcategoryPages = SportsHubDto.CreateDefaultSubcategoryPages();
            }

            _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(20));
            return dto;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public async Task<bool> UpdateConfigAsync(SportsHubDto model, string? userId, CancellationToken ct = default)
    {
        if (model == null) return false;

        await Semaphore.WaitAsync(ct);
        try
        {
            var success = await SaveToFileAsync(model, ct);
            if (success)
            {
                _cache.Set(CacheKey, model, TimeSpan.FromMinutes(20));
                _logger.LogInformation("SportsHub configuration updated successfully by user {UserId}", userId);
            }
            return success;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public async Task<SportsSubcategoryPageDto?> GetSubcategoryPageAsync(string subcategorySlug, CancellationToken ct = default)
    {
        var config = await GetConfigAsync(ct);
        var normSlug = (subcategorySlug ?? "").Trim().ToLowerInvariant();

        var page = config.SubcategoryPages?.FirstOrDefault(p =>
            string.Equals(p.CategorySlug, normSlug, StringComparison.OrdinalIgnoreCase) ||
            p.CategorySlug.Contains(normSlug) ||
            normSlug.Contains(p.CategorySlug));

        if (page == null)
        {
            // Find category item name if present
            var item = config.ShopByCategory?.Items?.FirstOrDefault(i =>
                string.Equals(i.Slug, normSlug, StringComparison.OrdinalIgnoreCase));

            var catName = item?.Name ?? (string.IsNullOrWhiteSpace(normSlug) ? "Sports Gear" : normSlug.Replace("-", " "));
            page = new SportsSubcategoryPageDto
            {
                CategorySlug = normSlug,
                CategoryName = catName,
                IconEmoji = item?.IconEmoji ?? "🏅",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    IsEnabled = true,
                    Title = catName,
                    TitleHighlight = "GENUINE ATHLETIC GEAR • FAST CAMPUS DELIVERY",
                    Subtitle = $"Discover professional {catName} equipment, tournament-grade gear, and athletic accessories curated for university students.",
                    ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 280
                },
                FilterTags = new List<string> { "Category", "Equipment Type", "Brand", "Price Range" },
                Products = SportsHubDto.CreateDefaultSubcategoryPages().FirstOrDefault()?.Products ?? new List<SportsProductItemDto>()
            };
        }

        return page;
    }

    public async Task ResetToDefaultAsync(CancellationToken ct = default)
    {
        await Semaphore.WaitAsync(ct);
        try
        {
            var def = SportsHubDto.CreateDefault();
            await SaveToFileAsync(def, ct);
            _cache.Set(CacheKey, def, TimeSpan.FromMinutes(20));
            _logger.LogInformation("SportsHub configuration was reset to defaults.");
        }
        finally
        {
            Semaphore.Release();
        }
    }

    private async Task<bool> SaveToFileAsync(SportsHubDto dto, CancellationToken ct)
    {
        try
        {
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var json = JsonSerializer.Serialize(dto, JsonOptions);
            await File.WriteAllTextAsync(_filePath, json, ct);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist SportsHub configuration to {Path}", _filePath);
            return false;
        }
    }
}

