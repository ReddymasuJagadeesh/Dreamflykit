using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.ElectronicsHub.Interfaces;
using Dreamflykit.Application.ElectronicsHub.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.ElectronicsHub;

public sealed class ElectronicsHubService : IElectronicsHubService
{
    private const string CacheKey = "electronics_category_hub_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ElectronicsHubService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public ElectronicsHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<ElectronicsHubService> logger)
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

        _filePath = Path.Combine(dataDir, "electronics_hub.json");
    }

    public async Task<ElectronicsHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out ElectronicsHubDto? cached) && cached != null)
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
                    var dto = JsonSerializer.Deserialize<ElectronicsHubDto>(json, JsonOptions);
                    if (dto != null)
                    {
                        var def = ElectronicsHubDto.CreateDefault();
                        dto.ThemeSettings ??= def.ThemeSettings;
                        dto.HeroBanner ??= def.HeroBanner;
                        if (dto.HeroBanner.Slides == null || dto.HeroBanner.Slides.Count == 0)
                        {
                            dto.HeroBanner.Slides = def.HeroBanner.Slides;
                        }
                        if (dto.HeroBanner.SlideIntervalSeconds <= 0)
                        {
                            dto.HeroBanner.SlideIntervalSeconds = 5;
                        }
                        dto.CategoriesSection ??= def.CategoriesSection;
                        if (dto.CategoriesSection.Categories == null || dto.CategoriesSection.Categories.Count == 0)
                        {
                            dto.CategoriesSection.Categories = def.CategoriesSection.Categories;
                        }
                        dto.CuratedProductsSection ??= def.CuratedProductsSection;
                        dto.TrustBadgesSection ??= def.TrustBadgesSection;
                        dto.BottomBanner ??= def.BottomBanner;
                        dto.SubcategoryPages ??= def.SubcategoryPages;
                        if (dto.SubcategoryPages == null || dto.SubcategoryPages.Count == 0)
                        {
                            dto.SubcategoryPages = def.SubcategoryPages;
                        }

                        _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(10));
                        return dto;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load Electronics Hub config from {FilePath}, falling back to defaults.", _filePath);
            }
        }

        var defaultDto = ElectronicsHubDto.CreateDefault();
        await SaveToFileAsync(defaultDto, cancellationToken);
        _cache.Set(CacheKey, defaultDto, TimeSpan.FromMinutes(10));
        return defaultDto;
    }

    public async Task UpdateConfigAsync(ElectronicsHubDto dto, Guid updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        await SaveToFileAsync(dto, cancellationToken);
        _cache.Set(CacheKey, dto, TimeSpan.FromMinutes(10));
        _logger.LogInformation("Electronics Hub config successfully updated by user {UserId}.", updatedBy);
    }

    public async Task ResetToDefaultAsync(CancellationToken cancellationToken = default)
    {
        var defaultDto = ElectronicsHubDto.CreateDefault();
        await SaveToFileAsync(defaultDto, cancellationToken);
        _cache.Set(CacheKey, defaultDto, TimeSpan.FromMinutes(10));
        _logger.LogInformation("Electronics Hub config reset to defaults.");
    }

    private async Task SaveToFileAsync(ElectronicsHubDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var json = JsonSerializer.Serialize(dto, JsonOptions);
            await File.WriteAllTextAsync(_filePath, json, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write Electronics Hub config to {FilePath}", _filePath);
            throw;
        }
    }
}

