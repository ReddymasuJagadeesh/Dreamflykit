namespace Dreamflykit.Web.Models.Home;

public sealed class HomeViewModel
{
    public IReadOnlyList<HomeCountryKitViewModel> CountryKits { get; init; }
        = Array.Empty<HomeCountryKitViewModel>();
}

public sealed class HomeCountryKitViewModel
{
    public string CountryName { get; init; } = string.Empty;

    public string CountryRouteValue { get; init; } = string.Empty;
}