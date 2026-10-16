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
    private readonly Dreamflykit.Application.Catalog.Interfaces.IProductRepository _productRepository;

    public HomeController(
        ICountryService countryService,
        DreamflykitDbContext db,
        IMediaUrlService urlService,
        Dreamflykit.Application.Catalog.Interfaces.IProductRepository productRepository)
    {
        _countryService = countryService;
        _db = db;
        _urlService = urlService;
        _productRepository = productRepository;
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

        // 3. Load active categories with S3 images
        var activeCategories = await _db.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .Take(15)
            .ToListAsync(cancellationToken);

        var catMediaIds = activeCategories
            .Where(c => c.ImageMediaAssetId.HasValue)
            .Select(c => c.ImageMediaAssetId!.Value)
            .Distinct()
            .ToList();

        var catMediaMap = catMediaIds.Count > 0
            ? await _db.MediaAssets
                .AsNoTracking()
                .Where(m => catMediaIds.Contains(m.Id))
                .ToDictionaryAsync(m => m.Id, m => _urlService.GetThumbnailUrl(m.ObjectKey), cancellationToken)
            : new Dictionary<Guid, string>();

        var categoryDtos = activeCategories.Select(c => new HomeCategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            DisplayOrder = c.DisplayOrder,
            ImageUrl = c.ImageMediaAssetId.HasValue && catMediaMap.TryGetValue(c.ImageMediaAssetId.Value, out var url)
                ? url
                : null
        }).ToList();

        // 4. Load Active Products for Flipkart-style Homepage Showcases
        var allActiveProducts = await _productRepository.GetActiveProductsAsync(cancellationToken);

        // Trending Deals: Ordered by highest discount percentage
        var trendingDeals = allActiveProducts
            .OrderByDescending(p => p.DiscountPercentage)
            .ThenByDescending(p => p.MRP)
            .Take(10)
            .ToList();

        // Featured Products: Prioritize IsFeatured, then top active products
        var featuredProducts = allActiveProducts
            .Where(p => p.IsFeatured)
            .Take(10)
            .ToList();

        if (featuredProducts.Count < 6)
        {
            var fallbackPool = allActiveProducts
                .Where(p => !featuredProducts.Any(fp => fp.ProductId == p.ProductId))
                .Take(10 - featuredProducts.Count);
            featuredProducts.AddRange(fallbackPool);
        }

        // 5. Check dynamic section configuration from HomepageSections
        var trendingSec = await _db.HomepageSections
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SectionKey == HomepageSectionKey.TrendingDeals, cancellationToken);

        var featuredSec = await _db.HomepageSections
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SectionKey == HomepageSectionKey.FeaturedProducts, cancellationToken);

        // 2b. Build dynamic hero slider slides (Admin Banners + Curated Slides)
        var heroSlides = new List<HomeHeroSlideDto>();

        foreach (var b in bannerDtos)
        {
            heroSlides.Add(new HomeHeroSlideDto
            {
                ImageUrl = b.ImageUrl,
                Title = b.Title,
                Subtitle = b.Subtitle,
                CtaText = b.CtaText,
                CtaUrl = b.CtaUrl,
                AltText = b.Title
            });
        }

        if (!string.IsNullOrEmpty(heroBannerUrl) && !heroSlides.Any(s => s.ImageUrl == heroBannerUrl))
        {
            heroSlides.Insert(0, new HomeHeroSlideDto
            {
                ImageUrl = heroBannerUrl,
                Title = heroSection?.Title ?? "Your Journey. Our Kit.",
                Subtitle = heroSection?.Subtitle ?? "Everything you need for your study abroad and travel journey",
                CtaText = "Find My Kit",
                CtaUrl = heroBannerLink,
                AltText = heroBannerAlt ?? "Dreamflykit Hero"
            });
        }

        if (heroSlides.Count < 2)
        {
            var defaultSlides = new List<HomeHeroSlideDto>
            {
                new()
                {
                    ImageUrl = "/images/hero/hero.webp",
                    Title = "Your Journey. Our Kit.",
                    Subtitle = "Everything you need for your study abroad and travel journey",
                    CtaText = "Find My Kit",
                    CtaUrl = "/Kits/FindMyKit",
                    AltText = "Dreamflykit - Your Journey. Our Kit."
                },
                new()
                {
                    ImageUrl = "/images/hero/hero-electronics.webp",
                    Title = "International Tech & Power Adapters",
                    Subtitle = "Universal travel adapters, global 5G SIM kits, and heavy-duty power banks",
                    CtaText = "Explore Electronics",
                    CtaUrl = "/category/electronics",
                    AltText = "Dreamflykit Electronics & Power Adapters"
                },
                new()
                {
                    ImageUrl = "/images/hero/hero-travel-gear.webp",
                    Title = "Curated Study Abroad Luggage & Kits",
                    Subtitle = "Durable luggage, packing cubes, and university stationery ready for flight",
                    CtaText = "Shop Travel Gear",
                    CtaUrl = "/category/travel-gear",
                    AltText = "Dreamflykit Study Abroad Travel Gear"
                }
            };

            foreach (var def in defaultSlides)
            {
                if (!heroSlides.Any(s => s.ImageUrl == def.ImageUrl))
                {
                    heroSlides.Add(def);
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
                HeroSlides = heroSlides,
                Banners = bannerDtos,
                Categories = categoryDtos,
                TrendingDeals = trendingDeals,
                FeaturedProducts = featuredProducts,
                TrendingDealsTitle = trendingSec?.Title ?? "Top Deals of the Day",
                TrendingDealsSubtitle = trendingSec?.Subtitle ?? "Exclusive student abroad discounts & special offers",
                FeaturedProductsTitle = featuredSec?.Title ?? "Best of Study & Travel Essentials",
                FeaturedProductsSubtitle = featuredSec?.Subtitle ?? "Student-curated essentials trusted worldwide"
            };

        return View(model);
    }
}