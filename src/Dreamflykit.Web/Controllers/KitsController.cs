using Dreamflykit.Application.Geography;
using Dreamflykit.Application.Kits.Interfaces;
using Dreamflykit.Application.Kits.Models;
using Dreamflykit.Web.Models.Kits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dreamflykit.Web.Controllers;

public sealed class KitsController : Controller
{
    private readonly IFindMyKitService _findMyKitService;
    private readonly ICountryService _countryService;
    private readonly IKitService _kitService;

    public KitsController(
        IFindMyKitService findMyKitService,
        ICountryService countryService,
        IKitService kitService)
    {
        _findMyKitService = findMyKitService;
        _countryService = countryService;
        _kitService = kitService;
    }

    // ============================================================
    // FIND MY KIT
    // ============================================================

    [HttpGet("/Kits/FindMyKit")]
    public async Task<IActionResult> FindMyKit(
        CancellationToken cancellationToken)
    {
        var model = new FindMyKitViewModel();

        await PopulateCountriesAsync(
            model,
            cancellationToken);

        return View(model);
    }

    [HttpPost("/Kits/FindMyKit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FindMyKit(
        FindMyKitViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCountriesAsync(
                model,
                cancellationToken);

            return View(model);
        }

        var request = new FindMyKitRequest
        {
            Name = model.Name,
            DestinationCountryId =
                model.DestinationCountryId!.Value,
            ArrivalDate =
                model.ArrivalDate!.Value,
            Phone = model.Phone
        };

        var result =
            await _findMyKitService.FindAsync(
                request,
                cancellationToken);

        var viewModel =
            new FindMyKitResultsViewModel
            {
                Name = result.Name,

                CountryName =
                    result.CountryName,

                SeasonName =
                    result.SeasonName,

                ArrivalDate =
                    model.ArrivalDate.Value,

                Kits = result.Kits
                    .Select(kit =>
                        new FindMyKitResultItemViewModel
                        {
                            KitId = kit.KitId,
                            Name = kit.Name,
                            Slug = kit.Slug,
                            Description = kit.Description,
                            SizeName = kit.SizeName,
                            Mrp = kit.Mrp,
                            SellingPrice = kit.SellingPrice,
                            DiscountPercentage =
                                kit.DiscountPercentage,
                            IsFeatured =
                                kit.IsFeatured
                        })
                    .ToList()
            };

        return View(
            "Results",
            viewModel);
    }

    // ============================================================
    // COUNTRY KITS
    // ============================================================

    [HttpGet( "/kits/{countrySlug}", Name = "CountryKits")]
    public async Task<IActionResult> CountryKits(
        string countrySlug,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(countrySlug))
        {
            return NotFound();
        }

        var kits =
            await _kitService.GetCountryKitsAsync(
                countrySlug,
                cancellationToken);

        if (kits.Count == 0)
        {
            return NotFound();
        }

        var model =
            new CountryKitsViewModel
            {
                CountryName =
                    kits.First().CountryName,

                Kits = kits
                    .Select(kit =>
                        new CountryKitItemViewModel
                        {
                            KitId = kit.KitId,
                            Name = kit.Name,
                            Slug = kit.Slug,
                            Description = kit.Description,
                            SizeName = kit.SizeName,
                            Mrp = kit.Mrp,
                            SellingPrice = kit.SellingPrice,
                            DiscountPercentage =
                                kit.DiscountPercentage,
                            IsFeatured =
                                kit.IsFeatured
                        })
                    .ToList()
            };

        return View(
            "CountryKits",
            model);
    }

    // ============================================================
    // KIT DETAILS
    // ============================================================

    [HttpGet("/kit/{kitSlug}")]
    public async Task<IActionResult> Details(
        string kitSlug,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(kitSlug))
        {
            return NotFound();
        }

        var kit =
            await _kitService.GetDetailsAsync(
                kitSlug,
                cancellationToken);

        if (kit is null)
        {
            return NotFound();
        }

        var model =
            new KitDetailsViewModel
            {
                KitId = kit.KitId,

                Name = kit.Name,

                Description = kit.Description,

                SizeName = kit.SizeName,

                Mrp = kit.Mrp,

                SellingPrice = kit.SellingPrice,

                DiscountAmount =
                    kit.DiscountAmount,

                DiscountPercentage =
                    kit.DiscountPercentage,

                IsFeatured =
                    kit.IsFeatured
            };

        return View(
            "Details",
            model);
    }

    // ============================================================
    // HELPERS
    // ============================================================

    private async Task PopulateCountriesAsync(
        FindMyKitViewModel model,
        CancellationToken cancellationToken)
    {
        var countries =
            await _countryService.GetActiveCountriesAsync(
                cancellationToken);

        model.Countries =
            countries
                .Select(country =>
                    new SelectListItem
                    {
                        Value =
                            country.Id.ToString(),

                        Text =
                            country.Name
                    })
                .ToList();
    }
}