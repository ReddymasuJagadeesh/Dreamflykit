using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.MobilesHub.Interfaces;
using Dreamflykit.Application.MobilesHub.Models;
using Dreamflykit.Application.MobilesHub.Services;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.MobilesHub;

public sealed class MobilesHubService : IMobilesHubService
{
    private const string HubSlug = "mobiles";
    private const string HubTitle = "Mobiles Hub";
    private const string FallbackFileName = "mobiles_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<MobilesHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public MobilesHubService(
        ICategoryHubRepository hubRepository,
        ILogger<MobilesHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public MobilesHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<MobilesHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<MobilesHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<MobilesHubDto>(HubSlug, FallbackFileName, cancellationToken);
        if (dto != null)
        {
            var brandCountBefore = dto.BrandsSection?.Brands?.Count ?? 0;
            EnsureDefaults(dto);
            var brandCountAfter = dto.BrandsSection?.Brands?.Count ?? 0;
            if (brandCountBefore != brandCountAfter)
            {
                await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, dto, cancellationToken);
            }
            return dto;
        }

        var defaultDto = MobilesHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
    }

    public async Task UpdateConfigAsync(MobilesHubDto config, Guid? updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(config);
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, config, cancellationToken);
        _logger.LogInformation("Mobiles Hub configuration updated successfully in PostgreSQL by {UserId}", updatedBy);
    }

    public async Task ResetToDefaultAsync(CancellationToken cancellationToken = default)
    {
        var defaultDto = MobilesHubDto.CreateDefault();
        await UpdateConfigAsync(defaultDto, Guid.Empty, cancellationToken);
    }

    private static void EnsureDefaults(MobilesHubDto dto)
    {
        var def = MobilesHubDto.CreateDefault();
        dto.HeroBanner ??= def.HeroBanner;
        if (dto.HeroBanner.Slides == null || dto.HeroBanner.Slides.Count == 0)
        {
            dto.HeroBanner.Slides = def.HeroBanner.Slides;
        }

        // Slugs explicitly purged per administrator instruction
        var purgedSlugs = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "hmd", "boltt", "mivi", "tecno", "lava", "infinix", "ai-plus", "ai"
        };

        dto.BrandsSection ??= def.BrandsSection;
        if (dto.BrandsSection.Brands == null)
        {
            dto.BrandsSection.Brands = def.BrandsSection.Brands;
        }
        else
        {
            // Remove purged legacy brands if present
            dto.BrandsSection.Brands.RemoveAll(b => !string.IsNullOrWhiteSpace(b.Slug) && purgedSlugs.Contains(b.Slug));

            // Upgrade legacy query links to dedicated brand showcase routes
            foreach (var b in dto.BrandsSection.Brands)
            {
                if (!string.IsNullOrWhiteSpace(b.Slug) &&
                    (string.IsNullOrWhiteSpace(b.ProductUrl) || b.ProductUrl.Contains("?category=mobiles&q=", StringComparison.OrdinalIgnoreCase)))
                {
                    b.ProductUrl = $"/category/mobiles/{b.Slug.Trim().ToLowerInvariant()}";
                }
            }
        }

        if (dto.BrandPages == null)
        {
            dto.BrandPages = def.BrandPages;
        }
        else
        {
            // Respect Admin deletions: only keep brand pages for currently active brands
            var activeBrandSlugs = new HashSet<string>(
                dto.BrandsSection.Brands.Select(b => b.Slug?.Trim().ToLowerInvariant() ?? string.Empty)
                    .Where(s => !string.IsNullOrEmpty(s) && !purgedSlugs.Contains(s)),
                StringComparer.OrdinalIgnoreCase);

            // Remove any orphan or purged brand pages that the admin explicitly deleted from BrandsSection
            dto.BrandPages.RemoveAll(p => string.IsNullOrWhiteSpace(p.BrandSlug) || 
                                          purgedSlugs.Contains(p.BrandSlug) || 
                                          (activeBrandSlugs.Count > 0 && !activeBrandSlugs.Contains(p.BrandSlug)));

            // For any brand in BrandsSection that doesn't have a brand page yet, auto-create one
            foreach (var brand in dto.BrandsSection.Brands)
            {
                if (string.IsNullOrWhiteSpace(brand.Slug)) continue;
                var existing = dto.BrandPages.FirstOrDefault(p =>
                    string.Equals(p.BrandSlug, brand.Slug, StringComparison.OrdinalIgnoreCase));
                if (existing == null)
                {
                    var fallback = MobileBrandCatalogService.GetBrandShowcase(brand.Slug);
                    dto.BrandPages.Add(fallback.Page);
                }
                else
                {
                    existing.HeroBanner ??= new MobileBrandHeroBannerDto
                    {
                        Title = brand.Name,
                        BadgeText = "OFFICIAL BRAND STORE",
                        ThemeBgGradient = "linear-gradient(135deg, #0b1120 0%, #1e1b4b 55%, #0f172a 100%)"
                    };
                    if (existing.FilterGroups == null || existing.FilterGroups.Count == 0)
                    {
                        var fallback = MobileBrandCatalogService.GetBrandShowcase(brand.Slug);
                        existing.FilterGroups = fallback.Page.FilterGroups;
                    }
                    if (existing.Products == null || existing.Products.Count == 0)
                    {
                        var fallback = MobileBrandCatalogService.GetBrandShowcase(brand.Slug);
                        existing.Products = fallback.Page.Products;
                    }
                }
            }
        }

        dto.HighlightBanner ??= def.HighlightBanner;
        if (dto.HighlightBanner.Sections == null || dto.HighlightBanner.Sections.Count == 0)
        {
            dto.HighlightBanner.Sections = def.HighlightBanner.Sections;
        }
        dto.TopDealsSection ??= def.TopDealsSection;
        dto.ConfusedSection ??= def.ConfusedSection;
        dto.PromoBanner ??= def.PromoBanner;
        dto.UnmissableDealsSection ??= def.UnmissableDealsSection;
    }
}
