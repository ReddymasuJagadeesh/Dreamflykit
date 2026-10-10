using System.ComponentModel.DataAnnotations;
using Dreamflykit.Application.Theme;
using Dreamflykit.Domain.Common;

namespace Dreamflykit.Web.Areas.Admin.Models;

public sealed class ThemeSettingsViewModel
{
    [Required(ErrorMessage = "Please select a font family.")]
    [Display(Name = "Primary Font Family")]
    public string FontFamily { get; set; } = TypographyCatalog.DefaultFontName;

    [Range(SystemThemeSetting.MinFontScale, SystemThemeSetting.MaxFontScale,
        ErrorMessage = "Font scale must be between 85% (0.85) and 115% (1.15) to prevent UI layout breakage.")]
    [Display(Name = "Font Size Scale")]
    public double FontScale { get; set; } = TypographyCatalog.DefaultFontScale;

    [Range(SystemThemeSetting.MinBaseFontSize, SystemThemeSetting.MaxBaseFontSize,
        ErrorMessage = "Base font size must be between 13px and 18px.")]
    [Display(Name = "Base Root Font Size (px)")]
    public int FontSizeBasePx { get; set; } = TypographyCatalog.DefaultBaseFontSizePx;

    [Display(Name = "Headings Font Family (Optional)")]
    public string? HeadingFontFamily { get; set; }

    public IReadOnlyList<FontDefinition> SupportedFonts { get; set; } = TypographyCatalog.SupportedFonts;

    public DateTime? UpdatedAtUtc { get; set; }

    public double ComputedFontSizePx => Math.Round(FontSizeBasePx * FontScale, 1);
}
