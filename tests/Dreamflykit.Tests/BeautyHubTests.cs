using System.Text.Json;
using Dreamflykit.Application.BeautyHub.Models;
using Dreamflykit.Infrastructure.BeautyHub;
using Dreamflykit.Web.Models.Products;
using Moq;
using Xunit;

namespace Dreamflykit.Tests;

public sealed class BeautyHubTests
{
    [Fact]
    public void CreateDefault_PopulatesCompleteFlipkartBeautyStoreSections()
    {
        // Act
        var dto = BeautyHubDto.CreateDefault();

        // Assert
        Assert.NotNull(dto);
        Assert.NotNull(dto.ThemeSettings);
        Assert.Equal("BIG BANG DIWALI", dto.ThemeSettings.ThemeName);
        Assert.True(dto.ThemeSettings.IsFestiveEnabled);
        Assert.Equal("FestiveArchedGolden", dto.ThemeSettings.FrameDesignStyle);

        // Hero Banners
        Assert.NotNull(dto.HeroBanner);
        Assert.True(dto.HeroBanner.IsEnabled);
        Assert.True(dto.HeroBanner.Slides.Count >= 3);
        Assert.Contains(dto.HeroBanner.Slides, s => s.Title.Contains("LOWEST PRICE", StringComparison.OrdinalIgnoreCase));

        // Promo 3 Banners
        Assert.NotNull(dto.PromoBanners);
        Assert.Equal(3, dto.PromoBanners.Cards.Count);
        Assert.Contains(dto.PromoBanners.Cards, c => c.Title.Contains("GEN-Z", StringComparison.OrdinalIgnoreCase));

        // 9 Core Beauty Categories (User Spec)
        Assert.NotNull(dto.SubCategories);
        Assert.Equal(9, dto.SubCategories.Items.Count);
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Makeup");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Skin");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Fragrance");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Hair");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Bath & Body");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Gifts");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Trending Now");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "New Launches");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Minis");

        // Category Pages & Subcategories
        Assert.NotNull(dto.CategoryPages);
        Assert.Equal(9, dto.CategoryPages.Count);
        Assert.Contains(dto.CategoryPages, p => p.CategorySlug == "makeup");
        Assert.Contains(dto.CategoryPages, p => p.CategorySlug == "skin");
        Assert.Contains(dto.CategoryPages, p => p.CategorySlug == "fragrance");
        Assert.Contains(dto.CategoryPages, p => p.CategorySlug == "hair");
        Assert.Contains(dto.CategoryPages, p => p.CategorySlug == "bath-body");
        Assert.Contains(dto.CategoryPages, p => p.CategorySlug == "gifts");
        Assert.Contains(dto.CategoryPages, p => p.CategorySlug == "trending-now");
        Assert.Contains(dto.CategoryPages, p => p.CategorySlug == "new-launches");
        Assert.Contains(dto.CategoryPages, p => p.CategorySlug == "minis");

        // Subcategory Pages & Products
        Assert.NotNull(dto.SubcategoryPages);
        Assert.True(dto.SubcategoryPages.Count >= 6);
        Assert.Contains(dto.SubcategoryPages, sp => sp.SubcategorySlug == "lips");
        Assert.Contains(dto.SubcategoryPages, sp => sp.SubcategorySlug == "face");
        Assert.Contains(dto.SubcategoryPages, sp => sp.SubcategorySlug == "serums-treatments");

        // Spotlight
        Assert.NotNull(dto.SpotlightBanner);
        Assert.Equal("The Global Luxe Store", dto.SpotlightBanner.Title);

        // Arched Sections
        Assert.NotNull(dto.SpecialGiftsForHer);
        Assert.Equal(6, dto.SpecialGiftsForHer.Cards.Count);
        Assert.Contains(dto.SpecialGiftsForHer.Cards, c => c.Title == "Lip makeup");

        Assert.NotNull(dto.GrandGiftsForHim);
        Assert.Equal(6, dto.GrandGiftsForHim.Cards.Count);
        Assert.Contains(dto.GrandGiftsForHim.Cards, c => c.Title == "Beard care");

        // Premium Gifts (9 Brands)
        Assert.NotNull(dto.PremiumGifts);
        Assert.Equal(9, dto.PremiumGifts.Brands.Count);
        Assert.Contains(dto.PremiumGifts.Brands, b => b.BrandName == "Eucerin");
        Assert.Contains(dto.PremiumGifts.Brands, b => b.BrandName == "DAVIDOFF");

        // Festive Looks (5 Looks)
        Assert.NotNull(dto.FestiveLooks);
        Assert.Equal(5, dto.FestiveLooks.Cards.Count);
        Assert.Contains(dto.FestiveLooks.Cards, l => l.LookName == "Flawless finish");

        // Flash Deals with Countdown
        Assert.NotNull(dto.FlashDealsTimer);
        Assert.True(dto.FlashDealsTimer.Cards.Count >= 7);

        // Biggest Brands
        Assert.NotNull(dto.BiggestBrands);
        Assert.True(dto.BiggestBrands.Brands.Count >= 8);
    }

    [Theory]
    [InlineData("beauty", true)]
    [InlineData("Beauty", true)]
    [InlineData("BEAUTY", true)]
    [InlineData("beauty-and-personal-care", true)]
    [InlineData("beauty-personal-care", true)]
    [InlineData("bpc", true)]
    [InlineData("fashion", false)]
    [InlineData("mobiles", false)]
    public void IsBeautyHub_DetectsCategorySlugsCorrectly(string categorySlug, bool expected)
    {
        // Arrange
        var vm = new ProductListViewModel
        {
            CategorySlug = categorySlug
        };

        // Assert
        Assert.Equal(expected, vm.IsBeautyHub);
    }

    [Fact]
    public void IsBeautyCategoryLanding_ReturnsTrue_WhenCategoryPageIsPresentAndSubcategoryIsNull()
    {
        var vm = new ProductListViewModel
        {
            CategorySlug = "beauty",
            BeautyCategoryPage = new BeautyCategoryPageDto
            {
                CategorySlug = "makeup",
                CategoryName = "Makeup"
            },
            BeautySubcategoryPage = null
        };

        Assert.True(vm.IsBeautyCategoryLanding);
        Assert.False(vm.IsBeautySubcategory);
    }

    [Fact]
    public void IsBeautySubcategory_ReturnsTrue_WhenSubcategoryPageIsPresent()
    {
        var vm = new ProductListViewModel
        {
            CategorySlug = "beauty",
            BeautyCategoryPage = new BeautyCategoryPageDto
            {
                CategorySlug = "makeup",
                CategoryName = "Makeup"
            },
            BeautySubcategoryPage = new BeautySubcategoryPageDto
            {
                CategorySlug = "makeup",
                SubcategorySlug = "lips",
                SubcategoryName = "Lips"
            }
        };

        Assert.False(vm.IsBeautyCategoryLanding);
        Assert.True(vm.IsBeautySubcategory);
    }

    [Fact]
    public async Task BeautyHubService_GetCategoryPageAsync_ReturnsCorrectPage()
    {
        var mockRepo = new Mock<Dreamflykit.Application.Catalog.Interfaces.ICategoryHubRepository>();
        mockRepo.Setup(r => r.GetPayloadAsync<BeautyHubDto>("beauty", "beauty_hub.json", It.IsAny<CancellationToken>()))
            .ReturnsAsync(BeautyHubDto.CreateDefault());

        var service = new BeautyHubService(mockRepo.Object, Mock.Of<Microsoft.Extensions.Logging.ILogger<BeautyHubService>>());

        var page = await service.GetCategoryPageAsync("makeup");

        Assert.NotNull(page);
        Assert.Equal("makeup", page.CategorySlug);
        Assert.Equal("Makeup", page.CategoryName);
        Assert.NotEmpty(page.Subcategories);
    }

    [Fact]
    public async Task BeautyHubService_GetSubcategoryPageAsync_ReturnsCorrectSubPage()
    {
        var mockRepo = new Mock<Dreamflykit.Application.Catalog.Interfaces.ICategoryHubRepository>();
        mockRepo.Setup(r => r.GetPayloadAsync<BeautyHubDto>("beauty", "beauty_hub.json", It.IsAny<CancellationToken>()))
            .ReturnsAsync(BeautyHubDto.CreateDefault());

        var service = new BeautyHubService(mockRepo.Object, Mock.Of<Microsoft.Extensions.Logging.ILogger<BeautyHubService>>());

        // Test Makeup Lips
        var subPage = await service.GetSubcategoryPageAsync("makeup", "lips");
        Assert.NotNull(subPage);
        Assert.Equal("lips", subPage.SubcategorySlug);
        Assert.Equal("Lips", subPage.SubcategoryName);
        Assert.NotEmpty(subPage.Products);

        // Test Hair Conditioners
        var condPage = await service.GetSubcategoryPageAsync("hair", "conditioners");
        Assert.NotNull(condPage);
        Assert.Equal("conditioners", condPage.SubcategorySlug);
        Assert.Equal("Conditioners", condPage.SubcategoryName);
        Assert.NotEmpty(condPage.Products);
        Assert.NotEmpty(condPage.SubcategoryItems);

        // Test Hair Shampoo
        var shampooPage = await service.GetSubcategoryPageAsync("hair", "shampoo");
        Assert.NotNull(shampooPage);
        Assert.Equal("shampoo", shampooPage.SubcategorySlug);
        Assert.NotEmpty(shampooPage.Products);

        // Test Dynamic Fallback
        var dynamicPage = await service.GetSubcategoryPageAsync("minis", "trial-kits");
        Assert.NotNull(dynamicPage);
        Assert.Equal("trial-kits", dynamicPage.SubcategorySlug);
        Assert.NotEmpty(dynamicPage.Products);
    }

    [Fact]
    public void JsonSerialization_PreservesAllBeautyHubProperties()
    {
        // Arrange
        var original = BeautyHubDto.CreateDefault();

        // Act
        var json = JsonSerializer.Serialize(original, new JsonSerializerOptions { WriteIndented = true });
        var restored = JsonSerializer.Deserialize<BeautyHubDto>(json);

        // Assert
        Assert.NotNull(restored);
        Assert.Equal(original.ThemeSettings.ThemeName, restored.ThemeSettings.ThemeName);
        Assert.Equal(original.HeroBanner.Slides.Count, restored.HeroBanner.Slides.Count);
        Assert.Equal(original.SubCategories.Items.Count, restored.SubCategories.Items.Count);
        Assert.Equal(original.CategoryPages.Count, restored.CategoryPages.Count);
        Assert.Equal(original.SubcategoryPages.Count, restored.SubcategoryPages.Count);
        Assert.Equal(original.SpecialGiftsForHer.Cards.Count, restored.SpecialGiftsForHer.Cards.Count);
        Assert.Equal(original.GrandGiftsForHim.Cards.Count, restored.GrandGiftsForHim.Cards.Count);
        Assert.Equal(original.PremiumGifts.Brands.Count, restored.PremiumGifts.Brands.Count);
        Assert.Equal(original.FestiveLooks.Cards.Count, restored.FestiveLooks.Cards.Count);

        // Sync to wwwroot data file
        var currentDir = new DirectoryInfo(AppContext.BaseDirectory);
        while (currentDir != null && !File.Exists(Path.Combine(currentDir.FullName, "src", "Dreamflykit.Web", "wwwroot", "data", "beauty_hub.json")))
        {
            currentDir = currentDir.Parent;
        }

        if (currentDir != null)
        {
            var dataFilePath = Path.Combine(currentDir.FullName, "src", "Dreamflykit.Web", "wwwroot", "data", "beauty_hub.json");
            File.WriteAllText(dataFilePath, json);
        }
    }
}
