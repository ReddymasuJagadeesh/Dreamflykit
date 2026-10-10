using System.Text;
using Dreamflykit.Application.Theme;
using Dreamflykit.Domain.Common;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.Theme;

public sealed class ThemeSettingService : IThemeSettingService
{
    private const string CacheKey = "system_theme_settings_cache";
    private readonly DreamflykitDbContext _db;
    private readonly IMemoryCache _cache;
    private readonly ILogger<ThemeSettingService> _logger;

    public ThemeSettingService(
        DreamflykitDbContext db,
        IMemoryCache cache,
        ILogger<ThemeSettingService> logger)
    {
        _db = db;
        _cache = cache;
        _logger = logger;
    }

    public async Task<ThemeSettingsDto> GetThemeSettingsAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out ThemeSettingsDto? cached) && cached != null)
        {
            return cached;
        }

        var entity = await _db.SystemThemeSettings
            .AsNoTracking()
            .OrderByDescending(s => s.CreatedAtUtc)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            var defaultDto = new ThemeSettingsDto
            {
                FontFamily = TypographyCatalog.DefaultFontName,
                FontScale = TypographyCatalog.DefaultFontScale,
                FontSizeBasePx = TypographyCatalog.DefaultBaseFontSizePx,
                HeadingFontFamily = null,
                UpdatedAtUtc = null
            };

            _cache.Set(CacheKey, defaultDto, TimeSpan.FromHours(4));
            return defaultDto;
        }

        var dto = new ThemeSettingsDto
        {
            FontFamily = entity.FontFamily,
            FontScale = entity.FontScale,
            FontSizeBasePx = entity.FontSizeBasePx,
            HeadingFontFamily = entity.HeadingFontFamily,
            UpdatedAtUtc = entity.UpdatedAtUtc ?? entity.CreatedAtUtc
        };

        _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
        return dto;
    }

    public async Task<(bool Success, string? ErrorMessage)> UpdateThemeSettingsAsync(
        UpdateThemeSettingsCommand command,
        Guid updatedBy,
        CancellationToken cancellationToken = default)
    {
        // 1. Strict Whitelist Font Validation (Prevents CSS injection / Malicious fonts)
        if (!TypographyCatalog.IsSupported(command.FontFamily))
        {
            return (false, $"Font '{command.FontFamily}' is not an authorized font. Please select a supported font.");
        }

        if (!string.IsNullOrWhiteSpace(command.HeadingFontFamily) && !TypographyCatalog.IsSupported(command.HeadingFontFamily))
        {
            return (false, $"Heading font '{command.HeadingFontFamily}' is not an authorized font.");
        }

        // 2. Strict Scale Bounds Check (Prevents Layout Breakage)
        if (command.FontScale < SystemThemeSetting.MinFontScale || command.FontScale > SystemThemeSetting.MaxFontScale)
        {
            return (false, $"Font scale {command.FontScale:P0} is outside the safe range of {SystemThemeSetting.MinFontScale:P0} to {SystemThemeSetting.MaxFontScale:P0}. Excess scale breaks navigation, buttons, and cards.");
        }

        if (command.FontSizeBasePx < SystemThemeSetting.MinBaseFontSize || command.FontSizeBasePx > SystemThemeSetting.MaxBaseFontSize)
        {
            return (false, $"Base font size must be between {SystemThemeSetting.MinBaseFontSize}px and {SystemThemeSetting.MaxBaseFontSize}px.");
        }

        try
        {
            var entity = await _db.SystemThemeSettings
                .OrderByDescending(s => s.CreatedAtUtc)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
            {
                entity = new SystemThemeSetting(
                    command.FontFamily,
                    command.FontScale,
                    command.FontSizeBasePx,
                    command.HeadingFontFamily,
                    updatedBy);

                _db.SystemThemeSettings.Add(entity);
            }
            else
            {
                entity.Update(
                    command.FontFamily,
                    command.FontScale,
                    command.FontSizeBasePx,
                    command.HeadingFontFamily,
                    updatedBy);
            }

            await _db.SaveChangesAsync(cancellationToken);

            // Invalidate cache immediately so changes take effect across entire app
            _cache.Remove(CacheKey);
            _logger.LogInformation("System theme settings updated by {UserId}. Font: {Font}, Scale: {Scale}", updatedBy, command.FontFamily, command.FontScale);

            return (true, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update system theme settings");
            return (false, "An unexpected error occurred while saving theme settings. Please try again.");
        }
    }

    public async Task<string> RenderDynamicThemeHtmlAsync(CancellationToken cancellationToken = default)
    {
        var settings = await GetThemeSettingsAsync(cancellationToken);
        var sb = new StringBuilder();

        // 1. Google Font Preconnect & Stylesheets (if external font)
        var fontUrls = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        if (!string.IsNullOrWhiteSpace(settings.GoogleFontUrl))
        {
            fontUrls.Add(settings.GoogleFontUrl);
        }
        if (!string.IsNullOrWhiteSpace(settings.HeadingGoogleFontUrl))
        {
            fontUrls.Add(settings.HeadingGoogleFontUrl);
        }

        if (fontUrls.Count > 0)
        {
            sb.AppendLine("    <link rel=\"preconnect\" href=\"https://fonts.googleapis.com\">");
            sb.AppendLine("    <link rel=\"preconnect\" href=\"https://fonts.gstatic.com\" crossorigin>");
            foreach (var url in fontUrls)
            {
                sb.AppendLine($"    <link href=\"{url}\" rel=\"stylesheet\">");
            }
        }

        // 2. Dynamic CSS Variables & Root Rules
        sb.AppendLine("    <style id=\"dk-dynamic-theme-css\">");
        sb.AppendLine("        :root {");
        sb.AppendLine($"            --dk-font-family: {settings.EffectiveCssFamily};");
        sb.AppendLine($"            --dk-heading-font-family: {settings.EffectiveHeadingCssFamily};");
        sb.AppendLine($"            --dk-font-scale: {settings.FontScale.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)};");
        sb.AppendLine($"            --dk-font-size-base: {settings.FontSizeBasePx}px;");
        sb.AppendLine("        }");
        sb.AppendLine("        html {");
        sb.AppendLine("            font-size: calc(var(--dk-font-size-base) * var(--dk-font-scale));");
        sb.AppendLine("        }");
        sb.AppendLine("        body, button, input, select, textarea {");
        sb.AppendLine("            font-family: var(--dk-font-family);");
        sb.AppendLine("        }");
        sb.AppendLine("        h1, h2, h3, h4, h5, h6, .dk-heading, .dk-brand-text, .dk-admin-card-title {");
        sb.AppendLine("            font-family: var(--dk-heading-font-family);");
        sb.AppendLine("        }");
        sb.AppendLine("    </style>");

        return sb.ToString();
    }
}
