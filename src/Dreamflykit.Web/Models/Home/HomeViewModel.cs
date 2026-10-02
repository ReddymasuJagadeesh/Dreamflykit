namespace Dreamflykit.Web.Models.Home;

public sealed class HomeViewModel
{
    public IReadOnlyList<HomeCountryKitViewModel> CountryKits { get; init; }
        = Array.Empty<HomeCountryKitViewModel>();

    public string? HeroBannerUrl { get; init; }
    public string? HeroBannerAlt { get; init; }
    public string? HeroBannerLink { get; init; }
    public IReadOnlyList<HomeBannerDto> Banners { get; init; } = Array.Empty<HomeBannerDto>();
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