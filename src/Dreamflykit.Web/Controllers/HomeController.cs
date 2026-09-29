using Dreamflykit.Application.Geography;
using Dreamflykit.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

public sealed class HomeController : Controller
{
    private readonly ICountryService _countryService;

    public HomeController(
        ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var countries =
            await _countryService
                .GetActiveCountriesWithKitsAsync(
                    cancellationToken);

        var model =
            new HomeViewModel
            {
                CountryKits = countries
                    .Select(country =>
                        new HomeCountryKitViewModel
                        {
                            CountryName =
                                country.CountryName,

                            CountryRouteValue =
                                country.CountryName
                        })
                    .ToList()
            };

        return View(model);
    }
}