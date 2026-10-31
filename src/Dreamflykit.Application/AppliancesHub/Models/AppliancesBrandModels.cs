namespace Dreamflykit.Application.AppliancesHub.Models;

public sealed class AppliancesBrandHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string TagBadge { get; set; } = "⚡ VERIFIED APPLIANCE BRAND • 100% GENUINE";
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public int BannerHeight { get; set; } = 360;
    public string TitleColor { get; set; } = "#ffffff";
    public string TitleHighlightColor { get; set; } = "#fbbf24";
    public string SubtitleColor { get; set; } = "#fef08a";
    public string BackgroundGradient { get; set; } = "linear-gradient(135deg, #1e293b 0%, #0f172a 100%)";
    public List<AppliancesHeroBadgeDto> Badges { get; set; } = new();
}

public sealed class AppliancesHeroBadgeDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Label { get; set; } = string.Empty;
    public string Icon { get; set; } = "zap";
}

public sealed class AppliancesBrandFilterOptionDto
{
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int Count { get; set; }
    public bool IsSelected { get; set; }
}

public sealed class AppliancesBrandFilterGroupDto
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public List<AppliancesBrandFilterOptionDto> Options { get; set; } = new();
    public bool IsExpanded { get; set; } = true;
}

public sealed class AppliancesBrandProductDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public int DiscountPercent { get; set; }
    public double Rating { get; set; } = 4.7;
    public int ReviewCount { get; set; } = 120;
    public List<string> KeySpecs { get; set; } = new();
    public string EnergyStar { get; set; } = "5 Star";
    public string WarrantyBadge { get; set; } = "1-Year Comprehensive + 10-Year Motor";
    public string CampusPerk { get; set; } = "Free On-Site Student Delivery & Demo";
    public bool InStock { get; set; } = true;

    // Filter-matching properties (lowercased)
    public string ScreenSize { get; set; } = string.Empty;
    public string DisplayTech { get; set; } = string.Empty;
    public string SmartTvOs { get; set; } = string.Empty;
    public string RefreshRate { get; set; } = string.Empty;
    public string ApplianceType { get; set; } = string.Empty;
    public string Capacity { get; set; } = string.Empty;
    public string Features { get; set; } = string.Empty;
}

public sealed class AppliancesBrandShowcaseData
{
    public string SubcategorySlug { get; set; } = string.Empty;
    public string SubcategoryName { get; set; } = string.Empty;
    public string SubcategoryEmoji { get; set; } = "🔌";
    public string BrandSlug { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public AppliancesBrandItemDto? BrandItem { get; set; }
    public AppliancesBrandHeroBannerDto HeroBanner { get; set; } = new();
    public List<AppliancesBrandFilterGroupDto> FilterGroups { get; set; } = new();
    public List<AppliancesBrandProductDto> Products { get; set; } = new();
}

