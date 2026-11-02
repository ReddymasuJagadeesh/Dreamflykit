using Dreamflykit.Application.CountryKits.Models;

namespace Dreamflykit.Web.Models.Kits;

public sealed class CountryKitsViewModel
{
    public string CountrySlug { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string FlagEmoji { get; set; } = "🇺🇸";
    public string HeroTitle { get; set; } = string.Empty;
    public string HeroSubtitle { get; set; } = string.Empty;
    public string LandmarkImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsLandmarkUploadingSoon { get; set; } = false;
    public string Feature1 { get; set; } = "Curated for American Lifestyle";
    public string Feature2 { get; set; } = "Essential Items for Every Student";
    public string Feature3 { get; set; } = "Premium Quality Products";
    public string Feature4 { get; set; } = "Value for Your Money";

    public string SelectedSize { get; set; } = "all"; // "all", "small", "medium", "large"
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string SelectedSort { get; set; } = "popular"; // "popular", "price-low", "price-high", "rating"
    public string SelectedCategoryFilter { get; set; } = "all";

    public List<CountryKitItemDto> CuratedKits { get; set; } = new();
    public List<CountryKitProfileDto> AllCountries { get; set; } = new();

    // Legacy backwards compatibility properties
    public IReadOnlyList<CountryKitItemViewModel> Kits { get; set; } = Array.Empty<CountryKitItemViewModel>();

    public List<CountryKitItemDto> GetFilteredKits()
    {
        var list = CuratedKits.Where(k => k.IsActive);

        // Size filter
        if (!string.IsNullOrWhiteSpace(SelectedSize) && !string.Equals(SelectedSize, "all", StringComparison.OrdinalIgnoreCase))
        {
            list = list.Where(k => string.Equals(k.Size, SelectedSize, StringComparison.OrdinalIgnoreCase));
        }

        // Price range
        if (MinPrice.HasValue)
        {
            list = list.Where(k => k.SellingPrice >= MinPrice.Value);
        }
        if (MaxPrice.HasValue)
        {
            list = list.Where(k => k.SellingPrice <= MaxPrice.Value);
        }

        // Sorting
        list = SelectedSort?.ToLowerInvariant() switch
        {
            "price-low" => list.OrderBy(k => k.SellingPrice),
            "price-high" => list.OrderByDescending(k => k.SellingPrice),
            "rating" => list.OrderByDescending(k => k.Rating).ThenByDescending(k => k.ReviewCount),
            _ => list.OrderBy(k => k.DisplayOrder).ThenByDescending(k => k.Rating)
        };

        return list.ToList();
    }
}

public sealed class CountryKitItemViewModel
{
    public Guid KitId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string SizeName { get; init; } = string.Empty;
    public decimal Mrp { get; init; }
    public decimal SellingPrice { get; init; }
    public decimal DiscountPercentage { get; init; }
    public bool IsFeatured { get; init; }
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public double Rating { get; set; } = 4.8;
    public int ReviewCount { get; set; } = 850;
    public string Badge { get; set; } = string.Empty;
}
