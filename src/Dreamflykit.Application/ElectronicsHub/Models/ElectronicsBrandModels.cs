namespace Dreamflykit.Application.ElectronicsHub.Models;

public sealed class ElectronicsBrandHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public int BannerHeight { get; set; } = 360;
    public string TitleColor { get; set; } = "#063f91";
    public string TitleHighlightColor { get; set; } = "#0284c7";
    public string SubtitleColor { get; set; } = "#334155";
    public List<ElectronicsHeroBadgeDto> Badges { get; set; } = new();
}

public sealed class ElectronicsBrandFilterOptionDto
{
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int Count { get; set; }
}

public sealed class ElectronicsBrandFilterGroupDto
{
    public string Title { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public List<ElectronicsBrandFilterOptionDto> Options { get; set; } = new();
}

public sealed class ElectronicsBrandProductDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string BrandSlug { get; set; } = string.Empty;
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public int DiscountPercent { get; set; }
    public double Rating { get; set; } = 4.8;
    public int ReviewCount { get; set; } = 84;
    public List<string> KeySpecs { get; set; } = new();
    public string Processor { get; set; } = string.Empty;
    public string Ram { get; set; } = string.Empty;
    public string Storage { get; set; } = string.Empty;
    public string ScreenSize { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public string OperatingSystem { get; set; } = string.Empty;
    public string FormFactor { get; set; } = string.Empty;
    public string CampusPerk { get; set; } = "1 Year Official Campus Warranty • 110V-240V Universal Power";
    public bool InStock { get; set; } = true;
    public Guid? DefaultVariantId { get; set; }
}

public sealed class ElectronicsBrandShowcaseData
{
    public string SubcategorySlug { get; set; } = string.Empty;
    public string SubcategoryName { get; set; } = string.Empty;
    public string SubcategoryEmoji { get; set; } = "💻";
    public string BrandSlug { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public ElectronicsBrandItemDto BrandItem { get; set; } = new();
    public ElectronicsBrandHeroBannerDto HeroBanner { get; set; } = new();
    public List<ElectronicsBrandFilterGroupDto> FilterGroups { get; set; } = new();
    public List<ElectronicsBrandProductDto> Products { get; set; } = new();
}

