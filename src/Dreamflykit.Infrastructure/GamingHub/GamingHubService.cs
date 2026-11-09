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
            page = new GamingSubcategoryPageDto
            {
                CategorySlug = normSlug,
                CategoryName = catName,
                IconEmoji = item?.IconEmoji ?? "🎮",
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
                Products = GamingHubDto.CreateDefaultTopProducts()
            };
        }

        return page;
    }

    private static void EnsureDefaults(GamingHubDto dto)
    {
        if (dto.ShopByCategory == null || dto.ShopByCategory.Items == null || dto.ShopByCategory.Items.Count == 0)
        {
            dto.ShopByCategory = new GamingShopByCategorySectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                Subtitle = "Consoles, VR, Racing Wheels, Arcade, Handheld PCs & Gaming Ecosystem",
                Items = GamingHubDto.CreateDefaultCategoryItems()
            };
        }

        if (dto.SubcategoryPages == null || dto.SubcategoryPages.Count == 0)
        {
            dto.SubcategoryPages = GamingHubDto.CreateDefaultSubcategoryPages();
        }
    }
}
