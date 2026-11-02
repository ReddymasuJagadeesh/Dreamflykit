using Dreamflykit.Application.CountryKits.Interfaces;
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
    private readonly ICountryKitsHubService? _countryKitsHubService;

    public KitsController(
        IFindMyKitService findMyKitService,
        ICountryService countryService,
        IKitService kitService,
        ICountryKitsHubService? countryKitsHubService = null)
    {
        _findMyKitService = findMyKitService;
        _countryService = countryService;
        _kitService = kitService;
        _countryKitsHubService = countryKitsHubService;
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
    // COUNTRY KITS (Matches User Reference Screenshot)
    // ============================================================

    [HttpGet("/kits/{countrySlug}", Name = "CountryKits")]
    [HttpGet("/country-kits/{countrySlug}")]
    public async Task<IActionResult> CountryKits(
        string countrySlug,
        [FromQuery] string? size = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] string? sort = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(countrySlug))
        {
            return NotFound();
        }

        var normalizedSlug = Application.CountryKits.Models.CountryKitsHubDto.NormalizeSlug(countrySlug);

        // Try getting profile from rich CountryKitsHubService
        if (_countryKitsHubService != null)
        {
            var profile = await _countryKitsHubService.GetCountryProfileAsync(normalizedSlug, cancellationToken);
            var allProfiles = await _countryKitsHubService.GetAllProfilesAsync(cancellationToken);

            var model = new CountryKitsViewModel
            {
                CountrySlug = profile.CountrySlug,
                CountryName = profile.CountryName,
                FlagEmoji = profile.FlagEmoji,
                HeroTitle = profile.HeroTitle,
                HeroSubtitle = profile.HeroSubtitle,
                LandmarkImageUrl = profile.LandmarkImageUrl,
                IsLandmarkUploadingSoon = profile.IsLandmarkUploadingSoon,
                Feature1 = profile.Feature1,
                Feature2 = profile.Feature2,
                Feature3 = profile.Feature3,
                Feature4 = profile.Feature4,
                SelectedSize = string.IsNullOrWhiteSpace(size) ? "all" : size.ToLowerInvariant(),
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SelectedSort = string.IsNullOrWhiteSpace(sort) ? "popular" : sort.ToLowerInvariant(),
                CuratedKits = profile.Kits ?? new(),
                AllCountries = allProfiles?.Countries?.Values?.Where(c => c.IsActive)?.OrderBy(c => c.DisplayOrder)?.ToList() ?? new()
            };

            return View("CountryKits", model);
        }

        // Fallback to legacy database service
        var kits = await _kitService.GetCountryKitsAsync(countrySlug, cancellationToken);
        if (kits.Count == 0)
        {
            return NotFound();
        }

        var legacyModel = new CountryKitsViewModel
        {
            CountrySlug = countrySlug,
            CountryName = kits.First().CountryName,
            HeroTitle = $"{kits.First().CountryName} Student Kits",
            HeroSubtitle = $"Everything you need for a comfortable and successful study abroad journey in {kits.First().CountryName}.",
            Kits = kits
                .Select(kit => new CountryKitItemViewModel
                {
                    KitId = kit.KitId,
                    Name = kit.Name,
                    Slug = kit.Slug,
                    Description = kit.Description,
                    SizeName = kit.SizeName,
                    Mrp = kit.Mrp,
                    SellingPrice = kit.SellingPrice,
                    DiscountPercentage = kit.DiscountPercentage,
                    IsFeatured = kit.IsFeatured
                })
                .ToList()
        };

        return View("CountryKits", legacyModel);
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
