namespace Dreamflykit.Domain.Common;

/// <summary>
/// Global typography and text theme settings manageable by Super Admin.
/// Constrained with safety guardrails to prevent layout breakages.
/// </summary>
public sealed class SystemThemeSetting : AuditableEntity
{
    public const double MinFontScale = 0.85;
    public const double MaxFontScale = 1.15;
    public const int MinBaseFontSize = 13;
    public const int MaxBaseFontSize = 18;

    private SystemThemeSetting() : base() { }

    public SystemThemeSetting(
        string fontFamily,
        double fontScale,
        int fontSizeBasePx,
        string? headingFontFamily,
        Guid updatedBy) : base(Guid.NewGuid())
    {
        Update(fontFamily, fontScale, fontSizeBasePx, headingFontFamily, updatedBy);
    }

    public string FontFamily { get; private set; } = "Plus Jakarta Sans";
    public double FontScale { get; private set; } = 1.0;
    public int FontSizeBasePx { get; private set; } = 15;
    public string? HeadingFontFamily { get; private set; }
    public Guid UpdatedBy { get; private set; }

    public void Update(
        string fontFamily,
        double fontScale,
        int fontSizeBasePx,
        string? headingFontFamily,
        Guid updatedBy)
    {
        if (string.IsNullOrWhiteSpace(fontFamily))
        {
            throw new DomainException("Font family is required.");
        }

        if (fontScale < MinFontScale || fontScale > MaxFontScale)
        {
            throw new DomainException($"Font scale must be between {MinFontScale:P0} and {MaxFontScale:P0} to maintain UI layout stability.");
        }

        if (fontSizeBasePx < MinBaseFontSize || fontSizeBasePx > MaxBaseFontSize)
        {
            throw new DomainException($"Base font size must be between {MinBaseFontSize}px and {MaxBaseFontSize}px.");
        }

        FontFamily = fontFamily.Trim();
        FontScale = Math.Round(fontScale, 2);
        FontSizeBasePx = fontSizeBasePx;
        HeadingFontFamily = string.IsNullOrWhiteSpace(headingFontFamily) ? null : headingFontFamily.Trim();
        UpdatedBy = updatedBy;
        MarkUpdated();
    }
}
