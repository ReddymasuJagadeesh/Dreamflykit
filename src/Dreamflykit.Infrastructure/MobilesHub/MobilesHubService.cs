using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.MobilesHub.Interfaces;
using Dreamflykit.Application.MobilesHub.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.MobilesHub;

public sealed class MobilesHubService : IMobilesHubService
{
    private const string CacheKey = "mobiles_category_hub_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<MobilesHubService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public MobilesHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<MobilesHubService> logger)
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

        _filePath = Path.Combine(dataDir, "mobiles_hub.json");
    }

    public async Task<MobilesHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out MobilesHubDto? cached) && cached != null)
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
                    var dto = JsonSerializer.Deserialize<MobilesHubDto>(json, JsonOptions);
                    if (dto != null)
                    {
                        var def = MobilesHubDto.CreateDefault();
                        dto.HeroBanner ??= def.HeroBanner;
                        if (dto.HeroBanner.Slides == null || dto.HeroBanner.Slides.Count == 0)
                        {
                            dto.HeroBanner.Slides = def.HeroBanner.Slides;
                        }
                        dto.BrandsSection ??= def.BrandsSection;
                        dto.HighlightBanner ??= def.HighlightBanner;
                        dto.TopDealsSection ??= def.TopDealsSection;
                        dto.ConfusedSection ??= def.ConfusedSection;
                        dto.PromoBanner ??= def.PromoBanner;
                        dto.UnmissableDealsSection ??= def.UnmissableDealsSection;

                        _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
                        return dto;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed reading mobiles hub config from {FilePath}", _filePath);
            }
        }

        var defaultDto = MobilesHubDto.CreateDefault();
        try
        {
            var defaultJson = JsonSerializer.Serialize(defaultDto, JsonOptions);
            await File.WriteAllTextAsync(_filePath, defaultJson, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not write initial mobiles hub json to {FilePath}", _filePath);
        }

        _cache.Set(CacheKey, defaultDto, TimeSpan.FromHours(4));
        return defaultDto;
    }

    public async Task UpdateConfigAsync(MobilesHubDto dto, Guid? updatedBy, CancellationToken cancellationToken = default)
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
            _logger.LogError(ex, "Failed to write Mobiles Hub config to {FilePath}", _filePath);
            throw new InvalidOperationException("Failed to persist Mobiles Hub configuration: " + ex.Message, ex);
        }

        _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
        _logger.LogInformation("Mobiles Hub configuration updated successfully by {UserId}", updatedBy);
    }

    public async Task ResetToDefaultAsync(CancellationToken cancellationToken = default)
    {
        var defaultDto = MobilesHubDto.CreateDefault();
        await UpdateConfigAsync(defaultDto, Guid.Empty, cancellationToken);
    }
}
