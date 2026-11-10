using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.SportsHub.Interfaces;
using Dreamflykit.Application.SportsHub.Models;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.SportsHub;

public sealed class SportsHubService : ISportsHubService
{
    private const string HubSlug = "sports-hub";
    private const string HubTitle = "Sports Hub";
    private const string FallbackFileName = "sports_hub.json";

    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<SportsHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public SportsHubService(
        ICategoryHubRepository hubRepository,
        ILogger<SportsHubService> logger)
    {
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public SportsHubService(
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<SportsHubService> logger)
        : this(new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(null, cache, env), logger)
    {
    }

    public async Task<SportsHubDto> GetConfigAsync(CancellationToken ct = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<SportsHubDto>(HubSlug, FallbackFileName, ct);
        if (dto != null)
        {
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = SportsHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, ct);
        return defaultDto;
    }

    public async Task<bool> UpdateConfigAsync(SportsHubDto model, string? userId, CancellationToken ct = default)
    {
        if (model == null) return false;

        try
        {
            await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, model, ct);
            _logger.LogInformation("SportsHub configuration updated successfully in PostgreSQL by user {UserId}", userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist SportsHub configuration to PostgreSQL");
            return false;
        }
    }

    public async Task<SportsSubcategoryPageDto?> GetSubcategoryPageAsync(string subcategorySlug, CancellationToken ct = default)
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

            var catName = item?.Name ?? (string.IsNullOrWhiteSpace(normSlug) ? "Sports Gear" : normSlug.Replace("-", " "));
            page = new SportsSubcategoryPageDto
            {
                CategorySlug = normSlug,
                CategoryName = catName,
                IconEmoji = item?.IconEmoji ?? "🏅",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    IsEnabled = true,
                    Title = catName,
                    TitleHighlight = "GENUINE ATHLETIC GEAR • FAST CAMPUS DELIVERY",
                    Subtitle = $"Discover professional {catName} equipment, tournament-grade gear, and athletic accessories curated for university students.",
                    ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 280
                },
                FilterTags = new List<string> { "Category", "Equipment Type", "Brand", "Price Range" },
                Products = SportsHubDto.CreateDefaultSubcategoryPages().FirstOrDefault()?.Products ?? new List<SportsProductItemDto>()
            };
        }

        return page;
    }

    public async Task ResetToDefaultAsync(CancellationToken ct = default)
    {
        var def = SportsHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, def, ct);
        _logger.LogInformation("SportsHub configuration was reset to defaults in PostgreSQL.");
    }

    private static void EnsureDefaults(SportsHubDto dto)
    {
        if (dto.ShopByCategory == null || dto.ShopByCategory.Items == null || dto.ShopByCategory.Items.Count == 0)
        {
            dto.ShopByCategory = new SportsShopByCategorySectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                Subtitle = "Explore verified university tournament sports gear, workout equipment, and athletic apparel",
                Items = SportsHubDto.CreateDefaultCategoryItems()
            };
        }

        if (dto.SubcategoryPages == null || dto.SubcategoryPages.Count == 0)
        {
            dto.SubcategoryPages = SportsHubDto.CreateDefaultSubcategoryPages();
        }
    }
}
