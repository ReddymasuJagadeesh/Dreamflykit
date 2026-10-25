using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Dreamflykit.Web.Areas.Admin.Models;

public sealed class LogoSettingsViewModel
{
    // Section 1: Left 'D' Logo Symbol
    [Display(Name = "Section 1: 'D' Logo Image URL")]
    public string Section1DLogoUrl { get; set; } = "/images/branding/dreamflykit-d-symbol.jpg";

    [Display(Name = "Section 1: Show 'Uploading Soon' Placeholder")]
    public bool Section1UploadingSoon { get; set; } = false;

    // Section 2: Dreamflykit Brand Text Logo
    [Display(Name = "Section 2: 'Dreamflykit' Brand Logo Image URL")]
    public string Section2BrandLogoUrl { get; set; } = "/images/branding/dreamflykit-brand-text.jpg";

    [Display(Name = "Section 2: Show 'Uploading Soon' Placeholder")]
    public bool Section2UploadingSoon { get; set; } = false;

    // Backward compatibility
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

    [Required(ErrorMessage = "Site title is required.")]
    [MaxLength(100)]
    [Display(Name = "Brand Name")]
    public string SiteTitle { get; set; } = "Dreamflykit";

    [MaxLength(150)]
    [Display(Name = "Brand Tagline")]
    public string Tagline { get; set; } = "Your Journey. Our Kit.";

    [MaxLength(150)]
    [Display(Name = "Bank of Baroda Style Header Subtitle")]
    public string? BadgeSubtitle { get; set; } = "India's Student Travel Store";

    [Display(Name = "Display Tagline Beside Logo")]
    public bool ShowBrandTagline { get; set; } = true;

    public DateTime? UpdatedAtUtc { get; set; }

    // Upload properties
    public IFormFile? Section1File { get; set; }
    public IFormFile? Section2File { get; set; }
    public IFormFile? MainLogoFile { get => Section2File; set => Section2File = value; }
    public IFormFile? ScrolledLogoFile { get => Section1File; set => Section1File = value; }
}
