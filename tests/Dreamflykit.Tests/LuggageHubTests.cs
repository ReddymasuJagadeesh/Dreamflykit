using Dreamflykit.Application.LuggageHub.Models;
using Dreamflykit.Infrastructure.LuggageHub;
using Dreamflykit.Web.Models.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Dreamflykit.Tests;

public class LuggageHubTests
{
    [Fact]
    public void LuggageHubDto_CreateDefault_InitializesFullLuggageTemplate()
    {
        // Act
        var hub = LuggageHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub);
        Assert.NotNull(hub.FestivalTheme);
        Assert.Equal("Flight Season Special", hub.FestivalTheme.FestivalName);
        Assert.Equal("#0f2b48", hub.FestivalTheme.PrimaryNavy);
        Assert.Equal("#f97316", hub.FestivalTheme.AccentOrange);

        // Hero Banner
        Assert.NotNull(hub.HeroBanner);
        Assert.True(hub.HeroBanner.IsEnabled);
        Assert.Equal("Pack your dreams.", hub.HeroBanner.HeadingLine1);
        Assert.Equal("Carry your tomorrow.", hub.HeroBanner.HeadingLine2);
        Assert.True(hub.HeroBanner.IsUploadingSoon);
        Assert.Equal(3, hub.HeroBanner.Badges.Count);

        // Verify Badges
        Assert.Contains(hub.HeroBanner.Badges, b => b.Title == "Strong Outside");
        Assert.Contains(hub.HeroBanner.Badges, b => b.Title == "Light Inside");
        Assert.Contains(hub.HeroBanner.Badges, b => b.Title == "Safe All The Way");

        // Verify Hero Floating Offer
        Assert.NotNull(hub.HeroBanner.OfferBox);
        Assert.True(hub.HeroBanner.OfferBox.IsEnabled);
        Assert.Equal("UP TO 15% OFF", hub.HeroBanner.OfferBox.DiscountTitle);
        Assert.Equal("TRAVEL FAR. TRAVEL SMART.", hub.HeroBanner.OfferBox.TopTag);

        // Reassurance Bar (5 items)
        Assert.NotNull(hub.ReassuranceBar);
        Assert.True(hub.ReassuranceBar.IsEnabled);
        Assert.Equal(5, hub.ReassuranceBar.Items.Count);
        Assert.Contains(hub.ReassuranceBar.Items, i => i.Title == "Free Delivery");
        Assert.Contains(hub.ReassuranceBar.Items, i => i.Title == "Easy Returns");
        Assert.Contains(hub.ReassuranceBar.Items, i => i.Title == "Secure Payments");
        Assert.Contains(hub.ReassuranceBar.Items, i => i.Title == "24/7 Support");
        Assert.Contains(hub.ReassuranceBar.Items, i => i.Title == "Best Price Guarantee");

        // 8 Shop by Categories
        Assert.NotNull(hub.ShopByCategory);
        Assert.NotNull(hub.ShopByCategory.Cards);
        Assert.Equal(8, hub.ShopByCategory.Cards.Count);

        var expectedSlugs = new[]
        {
            "suitcases",
            "backpacks",
            "duffel-bags",
            "cabin-luggage",
            "luggage-sets",
            "laptop-bags",
            "kids-luggage",
            "travel-accessories"
        };

        var actualSlugs = hub.ShopByCategory.Cards.Select(c => c.Slug).ToList();
        foreach (var slug in expectedSlugs)
        {
            Assert.Contains(slug, actualSlugs);
        }

        // All 8 cards should default to IsUploadingSoon == true
        Assert.All(hub.ShopByCategory.Cards, c =>
        {
            Assert.True(c.IsUploadingSoon);
            Assert.True(c.IsActive);
            Assert.False(string.IsNullOrWhiteSpace(c.Title));
            Assert.False(string.IsNullOrWhiteSpace(c.Subtitle));
            Assert.False(string.IsNullOrWhiteSpace(c.TargetUrl));
        });

        // 3 Promo Banners
        Assert.NotNull(hub.PromoBanners);
        Assert.True(hub.PromoBanners.IsEnabled);
        Assert.Equal(3, hub.PromoBanners.Cards.Count);

        Assert.Contains(hub.PromoBanners.Cards, p => p.Title == "New Arrivals");
        Assert.Contains(hub.PromoBanners.Cards, p => p.Title == "Top Brands");
        Assert.Contains(hub.PromoBanners.Cards, p => p.Title == "Student Special");
    }

    [Fact]
    public async Task LuggageHubService_GetConfigAsync_ReturnsDefaultWhenNoFileExists()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), "Dreamflykit_Test_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        Directory.CreateDirectory(Path.Combine(tempDir, "wwwroot", "data"));

        try
        {
            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns(Path.Combine(tempDir, "wwwroot"));

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var logger = NullLogger<LuggageHubService>.Instance;

            var service = new LuggageHubService(memoryCache, envMock.Object, logger);

            // Act
            var config = await service.GetConfigAsync();

            // Assert
            Assert.NotNull(config);
            Assert.Equal("Flight Season Special", config.FestivalTheme.FestivalName);
            Assert.Equal(8, config.ShopByCategory.Cards.Count);
            Assert.Equal(3, config.PromoBanners.Cards.Count);
        }
        finally
        {
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Fact]
    public async Task LuggageHubService_UpdateConfigAsync_PersistsAndInvalidatesCache()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), "Dreamflykit_Test_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        Directory.CreateDirectory(Path.Combine(tempDir, "wwwroot", "data"));

        try
        {
            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns(Path.Combine(tempDir, "wwwroot"));

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var logger = NullLogger<LuggageHubService>.Instance;

            var service = new LuggageHubService(memoryCache, envMock.Object, logger);

            var config = await service.GetConfigAsync();
            config.FestivalTheme.FestivalName = "Diwali Luggage Extravaganza";
            config.HeroBanner.HeadingLine1 = "Fly With Style";

            // Act
            await service.UpdateConfigAsync(config);
            var reloaded = await service.GetConfigAsync();

            // Assert
            Assert.Equal("Diwali Luggage Extravaganza", reloaded.FestivalTheme.FestivalName);
            Assert.Equal("Fly With Style", reloaded.HeroBanner.HeadingLine1);
        }
        finally
        {
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Fact]
    public void ProductListViewModel_IsLuggageHub_ReturnsTrueWhenLuggageCategoryOrSubcategory()
    {
        // Arrange & Act
        var vmDirect = new ProductListViewModel
        {
            CategorySlug = "luggage-bags",
            LuggageHub = LuggageHubDto.CreateDefault()
        };

        var vmSub = new ProductListViewModel
        {
            CategorySlug = "travel-gear",
            CurrentSubcategorySlug = "luggage-suitcases",
            LuggageHub = LuggageHubDto.CreateDefault()
        };

        // Assert
        Assert.True(vmDirect.IsLuggageHub);
        Assert.NotNull(vmDirect.LuggageHub);
        Assert.Equal(8, vmDirect.LuggageHub.ShopByCategory.Cards.Count);

        Assert.True(vmSub.IsLuggageHub);
        Assert.NotNull(vmSub.LuggageHub);
    }
}
