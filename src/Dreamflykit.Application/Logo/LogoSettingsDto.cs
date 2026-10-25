namespace Dreamflykit.Application.Logo;

public sealed class LogoSettingsDto
{
    // Section 1: Left 'D' Logo Symbol
    public string Section1DLogoUrl { get; set; } = "/images/branding/dreamflykit-d-symbol.jpg";
    public bool Section1UploadingSoon { get; set; } = false;

    // Section 2: Dreamflykit Brand Logo (Right after Section 1)
    public string Section2BrandLogoUrl { get; set; } = "/images/branding/dreamflykit-brand-text.jpg";
    public bool Section2UploadingSoon { get; set; } = false;

    // Backward-compat aliases
    public string LogoUrl
    {
        get => Section2BrandLogoUrl;
        set => Section2BrandLogoUrl = value;
    }

    public string? ScrolledLogoUrl
    {
        get => Section1DLogoUrl;
        set => Section1DLogoUrl = value ?? "/images/branding/dreamflykit-d-symbol.jpg";
    }

    public string SiteTitle { get; set; } = "Dreamflykit";
    public string Tagline { get; set; } = "Your Journey. Our Kit.";
    public string? BadgeSubtitle { get; set; } = "India's Student Travel Store";
    public bool ShowBrandTagline { get; set; } = true;
    public DateTime? UpdatedAtUtc { get; set; }
}
