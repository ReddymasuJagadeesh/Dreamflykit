using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.Caching.Interfaces;
using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Domain.Catalog;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.Catalog;

public sealed class CategoryHubRepository : ICategoryHubRepository
{
    private readonly DreamflykitDbContext? _context;
    private readonly IMemoryCache _cache;
    private readonly ICacheService? _cacheService;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<CategoryHubRepository> _logger;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public CategoryHubRepository(
        DreamflykitDbContext? context,
        IMemoryCache cache,
        IWebHostEnvironment env)
        : this(context, cache, env, Microsoft.Extensions.Logging.Abstractions.NullLogger<CategoryHubRepository>.Instance, null)
    {
    }

    public CategoryHubRepository(
        DreamflykitDbContext? context,
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<CategoryHubRepository>? logger)
        : this(context, cache, env, logger, null)
    {
    }

    [ActivatorUtilitiesConstructor]
    public CategoryHubRepository(
        DreamflykitDbContext? context,
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<CategoryHubRepository>? logger,
        ICacheService? cacheService = null)
    {
        _context = context;
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _env = env ?? throw new ArgumentNullException(nameof(env));
        _logger = logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger<CategoryHubRepository>.Instance;
        _cacheService = cacheService;
    }

    public async Task<CategoryHubConfiguration?> GetBySlugAsync(string hubSlug, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(hubSlug) || _context == null) return null;

        var normalizedSlug = hubSlug.Trim().ToLowerInvariant();
        return await _context.CategoryHubConfigurations
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.HubSlug == normalizedSlug, ct);
    }

    public async Task<T?> GetPayloadAsync<T>(string hubSlug, string fallbackFileName, CancellationToken ct = default) where T : class
    {
        if (string.IsNullOrWhiteSpace(hubSlug)) return null;

        var normalizedSlug = hubSlug.Trim().ToLowerInvariant();
        var cacheKey = $"category_hub_payload_{normalizedSlug}";

        if (_cacheService != null)
        {
            var distributed = await _cacheService.GetAsync<T>(cacheKey, ct);
            if (distributed != null)
            {
                return distributed;
            }
        }
        else if (_cache.TryGetValue(cacheKey, out T? cached) && cached != null)
        {
            return cached;
        }

        // 1. Try reading from PostgreSQL
        if (_context != null)
        {
            var config = await _context.CategoryHubConfigurations
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.HubSlug == normalizedSlug, ct);

            if (config != null && !string.IsNullOrWhiteSpace(config.ConfigPayloadJson) && config.ConfigPayloadJson != "{}")
            {
                try
                {
                    var parsed = JsonSerializer.Deserialize<T>(config.ConfigPayloadJson, JsonOptions);
                    if (parsed != null)
                    {
                        if (_cacheService != null)
                        {
                            await _cacheService.SetAsync(cacheKey, parsed, TimeSpan.FromMinutes(30), ct);
                        }
                        else
                        {
                            _cache.Set(cacheKey, parsed, TimeSpan.FromMinutes(30));
                        }
                        return parsed;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to deserialize PostgreSQL JSON for hub '{Slug}'", normalizedSlug);
                }
            }
        }

        // 2. Fallback to reading disk file and auto-seeding to PostgreSQL
        var root = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var filePath = Path.Combine(root, "data", fallbackFileName);

        if (File.Exists(filePath))
        {
            try
            {
                var fileJson = await File.ReadAllTextAsync(filePath, ct);
                var parsedFromFile = JsonSerializer.Deserialize<T>(fileJson, JsonOptions);

                if (parsedFromFile != null)
                {
                    // Seed into PostgreSQL in the background if absent
                    if (_context != null)
                    {
                        await AutoSeedToDatabaseAsync(normalizedSlug, hubSlug, fileJson, ct);
                    }

                    if (_cacheService != null)
                    {
                        await _cacheService.SetAsync(cacheKey, parsedFromFile, TimeSpan.FromMinutes(30), ct);
                    }
                    else
                    {
                        _cache.Set(cacheKey, parsedFromFile, TimeSpan.FromMinutes(30));
                    }
                    return parsedFromFile;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to read fallback file {Path} for hub '{Slug}'", filePath, normalizedSlug);
            }
        }

        return null;
    }

    public async Task SavePayloadAsync<T>(string hubSlug, string title, string fallbackFileName, T payload, CancellationToken ct = default) where T : class
    {
        if (string.IsNullOrWhiteSpace(hubSlug)) return;

        var normalizedSlug = hubSlug.Trim().ToLowerInvariant();
        var json = JsonSerializer.Serialize(payload, JsonOptions);

        if (_context != null)
        {
            var existing = await _context.CategoryHubConfigurations
                .FirstOrDefaultAsync(c => c.HubSlug == normalizedSlug, ct);

            if (existing != null)
            {
                existing.UpdateContent(
                    title: string.IsNullOrWhiteSpace(title) ? existing.Title : title,
                    subtitle: existing.Subtitle,
                    heroBadge: existing.HeroBadge,
                    heroBannerUrl: existing.HeroBannerUrl,
                    heroActionText: existing.HeroActionText,
                    heroActionUrl: existing.HeroActionUrl,
                    configPayloadJson: json,
                    isActive: true);
            }
            else
            {
                var newConfig = new CategoryHubConfiguration(
                    hubSlug: normalizedSlug,
                    title: string.IsNullOrWhiteSpace(title) ? hubSlug : title,
                    subtitle: null,
                    heroBadge: null,
                    heroBannerUrl: null,
                    heroActionText: null,
                    heroActionUrl: null,
                    configPayloadJson: json,
                    isActive: true);

                _context.CategoryHubConfigurations.Add(newConfig);
            }

            await _context.SaveChangesAsync(ct);
        }

        // Update cache across both local memory and distributed cache
        var cacheKey = $"category_hub_payload_{normalizedSlug}";
        if (_cacheService != null)
        {
            await _cacheService.SetAsync(cacheKey, payload, TimeSpan.FromMinutes(30), ct);
        }
        else
        {
            _cache.Set(cacheKey, payload, TimeSpan.FromMinutes(30));
        }

        // Mirror to fallback file for backward compatibility
        try
        {
            var root = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var dir = Path.Combine(root, "data");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var filePath = Path.Combine(dir, fallbackFileName);
            await File.WriteAllTextAsync(filePath, json, ct);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to mirror hub '{Slug}' to flat file {File}", normalizedSlug, fallbackFileName);
        }
    }

    public async Task InvalidateHubCacheAsync(string hubSlug, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(hubSlug)) return;

        var normalizedSlug = hubSlug.Trim().ToLowerInvariant();
        var cacheKey = $"category_hub_payload_{normalizedSlug}";

        _cache.Remove(cacheKey);

        if (_cacheService != null)
        {
            await _cacheService.RemoveAsync(cacheKey, ct);
        }

        _logger.LogInformation("Invalidated cache for category hub '{Slug}'", normalizedSlug);
    }

    public async Task<List<CategoryHubConfiguration>> GetAllActiveAsync(CancellationToken ct = default)
    {
        if (_context == null) return new List<CategoryHubConfiguration>();

        return await _context.CategoryHubConfigurations
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.Title)
            .ToListAsync(ct);
    }

    private async Task AutoSeedToDatabaseAsync(string slug, string title, string json, CancellationToken ct)
    {
        if (_context == null) return;

        try
        {
            var exists = await _context.CategoryHubConfigurations.AnyAsync(c => c.HubSlug == slug, ct);
            if (!exists)
            {
                var config = new CategoryHubConfiguration(
                    hubSlug: slug,
                    title: title,
                    subtitle: null,
                    heroBadge: null,
                    heroBannerUrl: null,
                    heroActionText: null,
                    heroActionUrl: null,
                    configPayloadJson: json,
                    isActive: true);

                _context.CategoryHubConfigurations.Add(config);
                await _context.SaveChangesAsync(ct);
                _logger.LogInformation("Auto-seeded Category Hub '{Slug}' from flat file into PostgreSQL", slug);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Auto-seeding Category Hub '{Slug}' to database failed", slug);
        }
    }
}

