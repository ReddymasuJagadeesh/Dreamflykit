using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.Logo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.Logo;

public sealed class LogoSettingService : ILogoSettingService
{
    private const string CacheKey = "dk_logo_settings_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<LogoSettingService> _logger;
    private readonly string _filePath;
    private readonly string _uploadDir;

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".png", ".webp", ".svg", ".jpg", ".jpeg", ".gif"
    };

    private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public LogoSettingService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<LogoSettingService> logger)
    {
        _cache = cache;
        _env = env;
        _logger = logger;

        var webRoot = !string.IsNullOrWhiteSpace(env.WebRootPath)
            ? env.WebRootPath
            : Path.Combine(AppContext.BaseDirectory, "wwwroot");

        var dataDir = Path.Combine(webRoot, "data");
        if (!Directory.Exists(dataDir))
        {
            try { Directory.CreateDirectory(dataDir); } catch { }
        }

        _filePath = Path.Combine(dataDir, "logo_settings.json");

        _uploadDir = Path.Combine(webRoot, "uploads", "branding");
        if (!Directory.Exists(_uploadDir))
        {
            try { Directory.CreateDirectory(_uploadDir); } catch { }
        }
    }

    public async Task<LogoSettingsDto> GetLogoSettingsAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out LogoSettingsDto? cached) && cached != null)
        {
            return cached;
        }

        LogoSettingsDto settings;
        if (File.Exists(_filePath))
        {
            try
            {
                var json = await File.ReadAllTextAsync(_filePath, cancellationToken);
                settings = JsonSerializer.Deserialize<LogoSettingsDto>(json, JsonOptions) ?? new LogoSettingsDto();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to read logo settings from {FilePath}. Using defaults.", _filePath);
                settings = new LogoSettingsDto();
            }
        }
        else
        {
            settings = new LogoSettingsDto();
            try
            {
                var initialJson = JsonSerializer.Serialize(settings, JsonOptions);
                await File.WriteAllTextAsync(_filePath, initialJson, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not initialize logo settings file at {FilePath}", _filePath);
            }
        }

        _cache.Set(CacheKey, settings, TimeSpan.FromHours(4));
        return settings;
    }

    public async Task<(bool Success, string? ErrorMessage)> UpdateLogoSettingsAsync(
        LogoSettingsDto dto,
        Guid updatedBy,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        try
        {
            dto.UpdatedAtUtc = DateTime.UtcNow;
            var json = JsonSerializer.Serialize(dto, JsonOptions);
            await File.WriteAllTextAsync(_filePath, json, cancellationToken);

            _cache.Remove(CacheKey);
            _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
            return (true, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist logo settings to {FilePath}", _filePath);
            return (false, "Could not save logo settings: " + ex.Message);
        }
    }

    public async Task<(bool Success, string? FileUrl, string? ErrorMessage)> UploadLogoFileAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        bool isScrolledIcon,
        Guid updatedBy,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(fileStream);

        if (string.IsNullOrWhiteSpace(fileName))
        {
            return (false, null, "Filename is required.");
        }

        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(ext))
        {
            return (false, null, $"Unsupported file type '{ext}'. Allowed types: PNG, WebP, SVG, JPG, GIF.");
        }

        if (fileStream.Length > MaxFileSizeBytes)
        {
            return (false, null, "File size exceeds the 5MB limit.");
        }

        try
        {
            var prefix = isScrolledIcon ? "section1-d" : "section2-brand";
            var uniqueFileName = $"{prefix}-{DateTime.UtcNow:yyyyMMddHHmmssfff}{ext}";
            var destinationPath = Path.Combine(_uploadDir, uniqueFileName);

            using (var fileOut = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fileStream.Position = 0;
                await fileStream.CopyToAsync(fileOut, cancellationToken);
            }

            var relativeUrl = $"/uploads/branding/{uniqueFileName}";

            // Update current settings with the new URL
            var settings = await GetLogoSettingsAsync(cancellationToken);
            if (isScrolledIcon)
            {
                settings.Section1DLogoUrl = relativeUrl;
                settings.Section1UploadingSoon = false;
            }
            else
            {
                settings.Section2BrandLogoUrl = relativeUrl;
                settings.Section2UploadingSoon = false;
            }

            await UpdateLogoSettingsAsync(settings, updatedBy, cancellationToken);

            return (true, relativeUrl, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload logo file {FileName}", fileName);
            return (false, null, "Failed to upload logo file: " + ex.Message);
        }
    }

    public async Task ResetToDefaultAsync(
        string logoType,
        Guid updatedBy,
        CancellationToken cancellationToken = default)
    {
        var settings = await GetLogoSettingsAsync(cancellationToken);

        if (string.Equals(logoType, "section1", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(logoType, "scrolled", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(logoType, "d", StringComparison.OrdinalIgnoreCase))
        {
            settings.Section1DLogoUrl = "/images/branding/dreamflykit-d-symbol.jpg";
            settings.Section1UploadingSoon = false;
        }
        else if (string.Equals(logoType, "section2", StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(logoType, "main", StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(logoType, "brand", StringComparison.OrdinalIgnoreCase))
        {
            settings.Section2BrandLogoUrl = "/images/branding/dreamflykit-brand-text.jpg";
            settings.Section2UploadingSoon = false;
        }
        else
        {
            settings.Section1DLogoUrl = "/images/branding/dreamflykit-d-symbol.jpg";
            settings.Section1UploadingSoon = false;
            settings.Section2BrandLogoUrl = "/images/branding/dreamflykit-brand-text.jpg";
            settings.Section2UploadingSoon = false;
            settings.SiteTitle = "Dreamflykit";
            settings.Tagline = "Your Journey. Our Kit.";
            settings.BadgeSubtitle = "India's Student Travel Store";
            settings.ShowBrandTagline = true;
        }

        await UpdateLogoSettingsAsync(settings, updatedBy, cancellationToken);
    }
}
