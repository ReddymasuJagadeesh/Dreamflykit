using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.GamingHub.Interfaces;
using Dreamflykit.Application.GamingHub.Models;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.GamingHub;

public sealed class GamingHubService : IGamingHubService
{
    private const string HubSlug = "gaming-hub";
    private const string HubTitle = "Gaming Hub";
    private const string FallbackFileName = "gaming_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<GamingHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public GamingHubService(
        ICategoryHubRepository hubRepository,
        ILogger<GamingHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public GamingHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<GamingHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<GamingHubDto> GetConfigAsync(CancellationToken ct = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<GamingHubDto>(HubSlug, FallbackFileName, ct);
        if (dto != null)
        {
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = GamingHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, ct);
        return defaultDto;
    }

    public async Task<bool> UpdateConfigAsync(GamingHubDto model, string? userId, CancellationToken ct = default)
    {
        if (model == null) return false;

        try
        {
            await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, model, ct);
            _logger.LogInformation("GamingHub configuration updated successfully in PostgreSQL by user {UserId}", userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist GamingHub configuration to PostgreSQL");
            return false;
        }
    }

    public async Task<GamingSubcategoryPageDto> GetSubcategoryPageAsync(string subcategorySlug, CancellationToken ct = default)
    {
        var config = await GetConfigAsync(ct);
        var normSlug = (subcategorySlug ?? "").Trim().ToLowerInvariant();

        var page = config.SubcategoryPages?.FirstOrDefault(p =>
            string.Equals(p.CategorySlug, normSlug, StringComparison.OrdinalIgnoreCase) ||
            p.CategorySlug.Contains(normSlug) ||
            normSlug.Contains(p.CategorySlug));

        if (page == null)
        {
            var item = config.ShopByCategory?.Items?.FirstOrDefault(i =>
                string.Equals(i.Slug, normSlug, StringComparison.OrdinalIgnoreCase));

            var catName = item?.Name ?? (string.IsNullOrWhiteSpace(normSlug) ? "Gaming Consoles" : normSlug.Replace("-", " "));
            var defaultPages = GamingHubDto.CreateDefaultSubcategoryPages();
            page = defaultPages.FirstOrDefault(p => string.Equals(p.CategorySlug, normSlug, StringComparison.OrdinalIgnoreCase))
                   ?? new GamingSubcategoryPageDto
                   {
                       CategorySlug = normSlug,
                       CategoryName = catName,
                       IconEmoji = item?.IconEmoji ?? "🎮",
                       Subcategories = item?.Subcategories ?? new List<string>(),
                       ImportantNote = item?.ImportantNote,
                       HeroBanner = new GamingSubcategoryHeroDto
                       {
                           IsEnabled = true,
                           Title = catName,
                           TitleHighlight = "Gaming Store",
                           Subtitle = $"Discover the best {catName} deals, certified genuine bundles, low-latency performance and student warranty.",
                           ImageUrl = "",
                           IsUploadingSoon = true,
                           BannerHeight = 260
                       },
                       FilterTags = new List<string> { "Condition", "Compatibility", "Brand", "Storage" },
                       Products = GamingHubDto.GenerateSampleProductsForCategory(normSlug, catName, item?.Subcategories)
                   };
        }

        return page;
    }

    private static void EnsureDefaults(GamingHubDto dto)
    {
        var defaultItems = GamingHubDto.CreateDefaultCategoryItems();
        var defaultPages = GamingHubDto.CreateDefaultSubcategoryPages();

        if (dto.ShopByCategory == null || dto.ShopByCategory.Items == null || dto.ShopByCategory.Items.Count == 0)
        {
            dto.ShopByCategory = new GamingShopByCategorySectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                Subtitle = "Consoles, VR, Racing Wheels, Arcade, Handheld PCs & Gaming Ecosystem",
                Items = defaultItems
            };
        }
        else
        {
            // Sync researched subcategories and notes into existing items if empty
            foreach (var item in dto.ShopByCategory.Items)
            {
                var match = defaultItems.FirstOrDefault(d => string.Equals(d.Slug, item.Slug, StringComparison.OrdinalIgnoreCase));
                if (match != null)
                {
                    if (item.Subcategories == null || item.Subcategories.Count == 0)
                    {
                        item.Subcategories = new List<string>(match.Subcategories);
                    }
                    if (string.IsNullOrWhiteSpace(item.ImportantNote))
                    {
                        item.ImportantNote = match.ImportantNote;
                    }
                }
            }
        }

        if (dto.SubcategoryPages == null || dto.SubcategoryPages.Count == 0)
        {
            dto.SubcategoryPages = defaultPages;
        }
        else
        {
            // Ensure every category has an entry in subpages
            foreach (var defPage in defaultPages)
            {
                var existingPage = dto.SubcategoryPages.FirstOrDefault(p =>
                    string.Equals(p.CategorySlug, defPage.CategorySlug, StringComparison.OrdinalIgnoreCase));

                if (existingPage == null)
                {
                    dto.SubcategoryPages.Add(defPage);
                }
                else
                {
                    if (existingPage.Subcategories == null || existingPage.Subcategories.Count == 0)
                    {
                        existingPage.Subcategories = new List<string>(defPage.Subcategories);
                    }
                    if (string.IsNullOrWhiteSpace(existingPage.ImportantNote))
                    {
                        existingPage.ImportantNote = defPage.ImportantNote;
                    }
                    if (existingPage.Products == null || existingPage.Products.Count == 0)
                    {
                        existingPage.Products = new List<GamingProductItemDto>(defPage.Products);
                    }
                    else
                    {
                        for (int i = 0; i < existingPage.Products.Count; i++)
                        {
                            var prod = existingPage.Products[i];
                            if (string.IsNullOrWhiteSpace(prod.SubcategoryTag) && existingPage.Subcategories.Count > 0)
                            {
                                prod.SubcategoryTag = existingPage.Subcategories[i % existingPage.Subcategories.Count];
                            }
                            if (string.IsNullOrWhiteSpace(prod.Description))
                            {
                                prod.Description = $"Verified genuine {existingPage.CategoryName} gaming hardware with campus warranty and student delivery.";
                            }
                        }
                    }
                }
            }
        }
    }
}
