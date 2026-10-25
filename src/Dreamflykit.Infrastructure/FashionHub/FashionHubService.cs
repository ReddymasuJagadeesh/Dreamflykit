using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.FashionHub.Interfaces;
using Dreamflykit.Application.FashionHub.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.FashionHub;

public sealed class FashionHubService : IFashionHubService
{
    private const string CacheKey = "fashion_category_hub_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<FashionHubService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public FashionHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<FashionHubService> logger)
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

        _filePath = Path.Combine(dataDir, "fashion_hub.json");
    }

    public async Task<FashionHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out FashionHubDto? cached) && cached != null)
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
                    var dto = JsonSerializer.Deserialize<FashionHubDto>(json, JsonOptions);
                    if (dto != null)
                    {
                        var def = FashionHubDto.CreateDefault();
                        dto.HeroBanner ??= def.HeroBanner;
                        dto.CategoriesSection ??= def.CategoriesSection;
                        dto.TopPicksSection ??= def.TopPicksSection;
                        dto.TrustBadgesSection ??= def.TrustBadgesSection;
                        dto.BottomBanner ??= def.BottomBanner;
                        dto.MenClothing ??= def.MenClothing;

                        _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
                        return dto;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed reading fashion hub config from {FilePath}", _filePath);
            }
        }

        var defaultDto = FashionHubDto.CreateDefault();
        try
        {
            var defaultJson = JsonSerializer.Serialize(defaultDto, JsonOptions);
            await File.WriteAllTextAsync(_filePath, defaultJson, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not write initial fashion hub json to {FilePath}", _filePath);
        }

        _cache.Set(CacheKey, defaultDto, TimeSpan.FromHours(4));
        return defaultDto;
    }

    public async Task UpdateConfigAsync(FashionHubDto dto, Guid updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        try
        {
            var json = JsonSerializer.Serialize(dto, JsonOptions);
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            await File.WriteAllTextAsync(_filePath, json, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write Fashion Hub config to {FilePath}", _filePath);
            throw new InvalidOperationException("Failed to persist Fashion Hub configuration: " + ex.Message, ex);
        }

        _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
        _logger.LogInformation("Fashion Hub configuration updated successfully by {UserId}", updatedBy);
    }

    public async Task ResetToDefaultAsync(CancellationToken cancellationToken = default)
    {
        var defaultDto = FashionHubDto.CreateDefault();
        await UpdateConfigAsync(defaultDto, Guid.Empty, cancellationToken);
    }
}
