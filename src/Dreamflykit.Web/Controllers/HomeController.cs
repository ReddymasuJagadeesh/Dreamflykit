using Dreamflykit.Application.Geography;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Controllers;

public sealed class HomeController : Controller
{
    private readonly ICountryService _countryService;
    private readonly DreamflykitDbContext _db;
    private readonly IMediaUrlService _urlService;

    public HomeController(
        ICountryService countryService,
        DreamflykitDbContext db,
        IMediaUrlService urlService)
    {
        _countryService = countryService;
        _db = db;
        _urlService = urlService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var countries =
            await _countryService
                .GetActiveCountriesWithKitsAsync(
                    cancellationToken);

        // 1. Check for dynamic Hero section
        var heroSection = await _db.HomepageSections
            .AsNoTracking()
            .FirstOrDefaultAsync(
                s => s.SectionKey == HomepageSectionKey.HeroBanner && s.IsActive && s.IsPublished,
                cancellationToken);

        string? heroBannerUrl = null;
        string? heroBannerAlt = null;
        string? heroBannerLink = heroSection?.LinkUrl ?? "/Kits/FindMyKit";

        if (heroSection?.MediaAssetId != null)
        {
            var heroAsset = await _db.MediaAssets
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == heroSection.MediaAssetId.Value, cancellationToken);

            if (heroAsset != null)
            {
                heroBannerUrl = _urlService.GetUrl(heroAsset.ObjectKey);
                heroBannerAlt = heroAsset.AltText;
            }
        }

        // 2. Load active promotional banners
        var now = DateTime.UtcNow;
        var activeBanners = await _db.Banners
            .AsNoTracking()
            .Where(b => b.IsActive && b.IsPublished &&
                        (!b.StartDateUtc.HasValue || b.StartDateUtc.Value <= now) &&
                        (!b.EndDateUtc.HasValue || b.EndDateUtc.Value >= now) &&
                        b.MediaAssetId.HasValue)
            .OrderBy(b => b.DisplayOrder)
            .Take(5)
            .ToListAsync(cancellationToken);

        var bannerDtos = new List<HomeBannerDto>();
        if (activeBanners.Any())
        {
            var bannerMediaIds = activeBanners.Select(b => b.MediaAssetId!.Value).Distinct().ToList();
            var bannerMedia = await _db.MediaAssets
                .AsNoTracking()
                .Where(m => bannerMediaIds.Contains(m.Id))
                .ToDictionaryAsync(m => m.Id, m => _urlService.GetUrl(m.ObjectKey), cancellationToken);

            foreach (var b in activeBanners)
            {
                if (bannerMedia.TryGetValue(b.MediaAssetId!.Value, out var url))
                {
                    bannerDtos.Add(new HomeBannerDto
                    {
                        ImageUrl = url,
                        Title = b.Title,
                        Subtitle = b.Subtitle,
                        CtaText = b.CtaText,
                        CtaUrl = b.CtaUrl
                    });
                }
            }
        }

        var model =
            new HomeViewModel
            {
                CountryKits = countries
                    .Select(country =>
                        new HomeCountryKitViewModel
                        {
                            CountryName = country.CountryName,
                            CountryRouteValue = country.CountryName
                        })
                    .ToList(),
                HeroBannerUrl = heroBannerUrl,
                HeroBannerAlt = heroBannerAlt,
                HeroBannerLink = heroBannerLink,
                Banners = bannerDtos
            };

        return View(model);
    }
}