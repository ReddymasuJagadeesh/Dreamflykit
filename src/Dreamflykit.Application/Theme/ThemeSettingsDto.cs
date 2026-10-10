namespace Dreamflykit.Application.Theme;

public sealed class ThemeSettingsDto
{
    public string FontFamily { get; init; } = TypographyCatalog.DefaultFontName;
    public double FontScale { get; init; } = TypographyCatalog.DefaultFontScale;
    public int FontSizeBasePx { get; init; } = TypographyCatalog.DefaultBaseFontSizePx;
    public string? HeadingFontFamily { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }

    public string EffectiveCssFamily => TypographyCatalog.GetFontOrDefault(FontFamily).CssFamily;
    public string? GoogleFontUrl => TypographyCatalog.GetFontOrDefault(FontFamily).GoogleFontUrl;
    
    public string EffectiveHeadingCssFamily => 
        string.IsNullOrWhiteSpace(HeadingFontFamily) 
            ? EffectiveCssFamily 
            : TypographyCatalog.GetFontOrDefault(HeadingFontFamily).CssFamily;

    public string? HeadingGoogleFontUrl =>
        string.IsNullOrWhiteSpace(HeadingFontFamily)
            ? null
            : TypographyCatalog.GetFontOrDefault(HeadingFontFamily).GoogleFontUrl;

    public double ComputedFontSizePx => Math.Round(FontSizeBasePx * FontScale, 1);
}

public sealed class UpdateThemeSettingsCommand
{
    public string FontFamily { get; set; } = TypographyCatalog.DefaultFontName;
    public double FontScale { get; set; } = TypographyCatalog.DefaultFontScale;
    public int FontSizeBasePx { get; set; } = TypographyCatalog.DefaultBaseFontSizePx;
    public string? HeadingFontFamily { get; set; }
}
