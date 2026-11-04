using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.GamingHub.Interfaces;
using Dreamflykit.Application.GamingHub.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.GamingHub;

public sealed class GamingHubService : IGamingHubService
{
    private const string CacheKey = "gaming_hub_config_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<GamingHubService> _logger;
    private readonly string _filePath;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public GamingHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<GamingHubService> logger)
    {
        _cache = cache;
        _env = env;
        _logger = logger;

        var rootPath = !string.IsNullOrWhiteSpace(env.WebRootPath)
            ? env.WebRootPath
            : Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        _filePath = Path.Combine(rootPath, "data", "gaming_hub.json");
    }

    public async Task<GamingHubDto> GetConfigAsync(CancellationToken ct = default)
    {
        if (_cache.TryGetValue(CacheKey, out GamingHubDto? cached) && cached != null)
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

            GamingHubDto? dto = null;
            if (File.Exists(_filePath))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(_filePath, ct);
                    dto = JsonSerializer.Deserialize<GamingHubDto>(json, JsonOptions);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to read GamingHub configuration from {Path}", _filePath);
                }
            }

            if (dto == null)
            {
                dto = GamingHubDto.CreateDefault();
                await SaveToFileAsync(dto, ct);
            }

            // Ensure categories and subpages are populated
            if (dto.ShopByCategory == null || dto.ShopByCategory.Items == null || dto.ShopByCategory.Items.Count == 0)
            {
                dto.ShopByCategory = new GamingShopByCategorySectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Consoles, VR, Racing Wheels, Arcade, Handheld PCs & Gaming Ecosystem",
                    Items = GamingHubDto.CreateDefaultCategoryItems()
                };
            }

            if (dto.SubcategoryPages == null || dto.SubcategoryPages.Count == 0)
            {
                dto.SubcategoryPages = GamingHubDto.CreateDefaultSubcategoryPages();
            }

            _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(20));
            return dto;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public async Task<bool> UpdateConfigAsync(GamingHubDto model, string? userId, CancellationToken ct = default)
    {
        if (model == null) return false;

        await Semaphore.WaitAsync(ct);
        try
        {
            var success = await SaveToFileAsync(model, ct);
            if (success)
            {
                _cache.Set(CacheKey, model, TimeSpan.FromMinutes(20));
                _logger.LogInformation("GamingHub configuration updated successfully by user {UserId}", userId);
            }
            return success;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public async Task<GamingSubcategoryPageDto> GetSubcategoryPageAsync(string subcategorySlug, CancellationToken ct = default)
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

            var catName = item?.Name ?? (string.IsNullOrWhiteSpace(normSlug) ? "Gaming Consoles" : normSlug.Replace("-", " "));
            page = new GamingSubcategoryPageDto
            {
                CategorySlug = normSlug,
                CategoryName = catName,
                IconEmoji = item?.IconEmoji ?? "🎮",
                HeroBanner = new GamingSubcategoryHeroDto
                {
                    IsEnabled = true,
                    Title = catName,
                    TitleHighlight = "Gaming Store",
                    Subtitle = $"Discover the best {catName} deals, certified genuine bundles, low-latency performance and student warranty.",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    BannerHeight = 260
                },
                FilterTags = new List<string> { "Condition", "Compatibility", "Brand", "Storage" },
                Products = GamingHubDto.CreateDefaultTopProducts()
            };
        }

        return page;
    }

    private async Task<bool> SaveToFileAsync(GamingHubDto dto, CancellationToken ct)
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
            _logger.LogError(ex, "Failed to persist GamingHub configuration to {Path}", _filePath);
            return false;
        }
    }
}

