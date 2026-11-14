namespace Dreamflykit.Application.MobilesHub.Models;

/// <summary>
/// Full showcase page data for an individual smartphone brand (e.g. Apple iPhone, Samsung, OnePlus, etc.).
/// </summary>
public sealed class MobileBrandPageDto
{
    public string BrandSlug { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string CategoryTagline { get; set; } = string.Empty;
    public string LogoEmoji { get; set; } = "📱";
    public MobileBrandHeroBannerDto HeroBanner { get; set; } = new();
    public List<MobileBrandFilterGroupDto> FilterGroups { get; set; } = new();
    public List<MobileBrandProductDto> Products { get; set; } = new();

    // Advanced color theme settings managed by Admin
    public string ThemePrimaryColor { get; set; } = "#2563eb";
    public string ThemeAccentColor { get; set; } = "#7c3aed";
}

/// <summary>
/// Panoramic brand hero banner with custom color palette & uploading-soon state.
/// </summary>
public sealed class MobileBrandHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string BadgeText { get; set; } = "OFFICIAL BRAND STORE • VERIFIED SMARTPHONES";
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public int BannerHeight { get; set; } = 320;
    public string ThemeBgGradient { get; set; } = "linear-gradient(135deg, #0b1120 0%, #1e1b4b 55%, #0f172a 100%)";
    public string TitleColor { get; set; } = "#ffffff";
    public string TitleHighlightColor { get; set; } = "#38bdf8";
    public string SubtitleColor { get; set; } = "#cbd5e1";
    public string FilterAccentColor { get; set; } = "#2563eb";
}

public sealed class MobileBrandFilterOptionDto
{
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int Count { get; set; }
}

public sealed class MobileBrandFilterGroupDto
{
    public string Title { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public bool IsMoreFilter { get; set; } = false;
    public List<MobileBrandFilterOptionDto> Options { get; set; } = new();
}

public sealed class MobileBrandProductDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string BrandSlug { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public int DiscountPercent { get; set; }
    public double Rating { get; set; } = 4.8;
    public int ReviewCount { get; set; } = 120;
    public List<string> KeySpecs { get; set; } = new();
    public List<string> FilterTags { get; set; } = new();
    public string Series { get; set; } = string.Empty;
    public string Chipset { get; set; } = string.Empty;
    public string Ram { get; set; } = string.Empty;
    public string Storage { get; set; } = string.Empty;
    public string Display { get; set; } = string.Empty;
    public string BatteryCamera { get; set; } = string.Empty;
    public string Condition { get; set; } = "Brand New";
    public string CampusPerk { get; set; } = "Global 5G Dual eSIM • 1-Year Official Warranty • 0% Student EMI";
    public bool InStock { get; set; } = true;
}

public sealed class MobileBrandShowcaseData
{
    public string BrandSlug { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public MobileBrandItemDto? BrandItem { get; set; }
    public MobileBrandPageDto Page { get; set; } = new();
    public List<MobileBrandItemDto> AllBrands { get; set; } = new();
}

