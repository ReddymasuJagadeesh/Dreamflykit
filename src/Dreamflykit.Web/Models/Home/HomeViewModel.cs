namespace Dreamflykit.Web.Models.Home;

public sealed class HomeViewModel
{
    public IReadOnlyList<HomeCountryKitViewModel> CountryKits { get; init; }
        = Array.Empty<HomeCountryKitViewModel>();

    public string? HeroBannerUrl { get; init; }
    public string? HeroBannerAlt { get; init; }
    public string? HeroBannerLink { get; init; }
    public IReadOnlyList<HomeBannerDto> Banners { get; init; } = Array.Empty<HomeBannerDto>();
    public IReadOnlyList<HomeCategoryDto> Categories { get; init; } = Array.Empty<HomeCategoryDto>();

    public IReadOnlyList<Dreamflykit.Application.Catalog.Models.ProductListItem> TrendingDeals { get; init; }
        = Array.Empty<Dreamflykit.Application.Catalog.Models.ProductListItem>();

    public IReadOnlyList<Dreamflykit.Application.Catalog.Models.ProductListItem> FeaturedProducts { get; init; }
        = Array.Empty<Dreamflykit.Application.Catalog.Models.ProductListItem>();

    public string TrendingDealsTitle { get; init; } = "Top Deals of the Day";
    public string TrendingDealsSubtitle { get; init; } = "Exclusive student abroad discounts & special offers";
    public string FeaturedProductsTitle { get; init; } = "Best of Study & Travel Essentials";
    public string FeaturedProductsSubtitle { get; init; } = "Student-curated essentials trusted worldwide";
}

public sealed class HomeCategoryDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string? ImageUrl { get; init; }
    public int DisplayOrder { get; init; }
}

public sealed class HomeCountryKitViewModel
{
    public string CountryName { get; init; } = string.Empty;
    public string CountryRouteValue { get; init; } = string.Empty;
}

public sealed class HomeBannerDto
{
    public string ImageUrl { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string? Subtitle { get; init; }
    public string? CtaText { get; init; }
    public string? CtaUrl { get; init; }
}