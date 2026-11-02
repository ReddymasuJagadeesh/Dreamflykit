using System.Text.Json;
using Dreamflykit.Application.CategoryShowcase.Interfaces;
using Dreamflykit.Application.CategoryShowcase.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.CategoryShowcase;

public sealed class CategoryThreeSectionSliderService : ICategoryThreeSectionSliderService
{
    private const string CacheKey = "CategoryThreeSectionSliders_Data";
    private static readonly SemaphoreSlim Semaphore = new(1, 1);
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<CategoryThreeSectionSliderService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public CategoryThreeSectionSliderService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<CategoryThreeSectionSliderService> logger)
    {
        _cache = cache;
        _env = env;
        _logger = logger;
        _filePath = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "data", "category_three_section_sliders.json");
    }

    public async Task<CategoryThreeSectionConfigDto> GetCategorySliderAsync(string categorySlug, CancellationToken ct = default)
    {
        var all = await GetAllSlidersAsync(ct);
        return all.GetConfigForCategory(categorySlug);
    }

    public async Task<CategoryThreeSectionHubDto> GetAllSlidersAsync(CancellationToken ct = default)
    {
        if (_cache.TryGetValue(CacheKey, out CategoryThreeSectionHubDto? cached) && cached != null)
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

            CategoryThreeSectionHubDto? data = null;

            if (File.Exists(_filePath))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(_filePath, ct);
                    data = JsonSerializer.Deserialize<CategoryThreeSectionHubDto>(json, JsonOptions);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to deserialize CategoryThreeSectionSliders from file {Path}", _filePath);
                }
            }

            if (data == null || data.Categories == null || data.Categories.Count == 0)
            {
                data = CategoryThreeSectionHubDto.CreateDefault();
                await SaveToFileAsync(data, ct);
            }

            _cache.Set(CacheKey, data, TimeSpan.FromMinutes(30));
            return data;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public async Task<bool> UpdateCategorySliderAsync(CategoryThreeSectionConfigDto config, CancellationToken ct = default)
    {
        if (config == null || string.IsNullOrWhiteSpace(config.CategorySlug)) return false;

        await Semaphore.WaitAsync(ct);
        try
        {
            var all = await GetAllSlidersAsync(ct);
            var slug = config.CategorySlug.Trim().ToLowerInvariant();

            all.Categories[slug] = config;

            var success = await SaveToFileAsync(all, ct);
            if (success)
            {
                _cache.Set(CacheKey, all, TimeSpan.FromMinutes(30));
                _logger.LogInformation("Successfully updated 3-section slider for category '{Slug}'", slug);
            }
            return success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update category slider for '{Slug}'", config.CategorySlug);
            return false;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public async Task<bool> ResetDefaultsAsync(string? categorySlug = null, CancellationToken ct = default)
    {
        await Semaphore.WaitAsync(ct);
        try
        {
            var defaults = CategoryThreeSectionHubDto.CreateDefault();

            if (string.IsNullOrWhiteSpace(categorySlug))
            {
                var success = await SaveToFileAsync(defaults, ct);
                if (success)
                {
                    _cache.Set(CacheKey, defaults, TimeSpan.FromMinutes(30));
                }
                return success;
            }
            else
            {
                var all = await GetAllSlidersAsync(ct);
                var slug = categorySlug.Trim().ToLowerInvariant();

                if (defaults.Categories.TryGetValue(slug, out var defaultCatConfig))
                {
                    all.Categories[slug] = defaultCatConfig;
                }
                else
                {
                    all.Categories[slug] = defaults.GetConfigForCategory(slug);
                }

                var success = await SaveToFileAsync(all, ct);
                if (success)
                {
                    _cache.Set(CacheKey, all, TimeSpan.FromMinutes(30));
                }
                return success;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset defaults for CategorySliders (slug: {Slug})", categorySlug);
            return false;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    private async Task<bool> SaveToFileAsync(CategoryThreeSectionHubDto data, CancellationToken ct)
    {
        try
        {
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var json = JsonSerializer.Serialize(data, JsonOptions);
            await File.WriteAllTextAsync(_filePath, json, ct);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save category sliders to {Path}", _filePath);
            return false;
        }
    }
}

