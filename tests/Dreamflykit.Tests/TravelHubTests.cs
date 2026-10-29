using Dreamflykit.Application.TravelHub.Models;
using Dreamflykit.Infrastructure.TravelHub;
using Dreamflykit.Web.Models.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Dreamflykit.Tests;

public class TravelHubTests
{
    [Fact]
    public void TravelHubDto_CreateDefault_InitializesFullTravelTemplate()
    {
        // Act
        var hub = TravelHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub);
        Assert.NotNull(hub.ThemeSettings);
        Assert.Equal("Dreamflykit • Travel Gear & Luggage", hub.ThemeSettings.StoreTitle);
        Assert.Equal("#0f2b48", hub.ThemeSettings.PrimaryColor);
        Assert.Equal("#0284c7", hub.ThemeSettings.AccentColor);

        // Top Notice
        Assert.NotNull(hub.TopNoticeBar);
        Assert.True(hub.TopNoticeBar.IsEnabled);
        Assert.Contains("Delivering Across USA", hub.TopNoticeBar.Item1Text);

        // Hero Banner
        Assert.NotNull(hub.HeroBanner);
        Assert.True(hub.HeroBanner.IsEnabled);
        Assert.Equal("Travel Gear", hub.HeroBanner.Heading);
        Assert.Equal("✈️", hub.HeroBanner.HeadingBadge);
        Assert.True(hub.HeroBanner.IsUploadingSoon);
        Assert.Equal(4, hub.HeroBanner.Badges.Count);
        Assert.Equal(5, hub.HeroBanner.PerfectJourneys.Count);

        // Verify Hero Badges
        Assert.Contains(hub.HeroBanner.Badges, b => b.Title == "Curated for Every Traveler");
        Assert.Contains(hub.HeroBanner.Badges, b => b.Title == "Premium Quality");
        Assert.Contains(hub.HeroBanner.Badges, b => b.Title == "Smart & Compact");
        Assert.Contains(hub.HeroBanner.Badges, b => b.Title == "Ready for Any Destination");

        // Verify Hero Journey Personas
        Assert.Contains(hub.HeroBanner.PerfectJourneys, j => j.Title == "MS / Study Abroad");
        Assert.Contains(hub.HeroBanner.PerfectJourneys, j => j.Title == "Honeymoon Trips");
        Assert.Contains(hub.HeroBanner.PerfectJourneys, j => j.Title == "Family Vacations");
        Assert.Contains(hub.HeroBanner.PerfectJourneys, j => j.Title == "Business Trips");
        Assert.Contains(hub.HeroBanner.PerfectJourneys, j => j.Title == "Weekend Getaways");

        // Sidebar Widgets
        Assert.NotNull(hub.Sidebar);
        Assert.Equal("Travel For", hub.Sidebar.TravelForTitle);
        Assert.Equal(5, hub.Sidebar.TravelForItems.Count);
        Assert.NotNull(hub.Sidebar.QuizCard);
        Assert.True(hub.Sidebar.QuizCard.IsEnabled);
        Assert.Equal("Not Sure What You Need?", hub.Sidebar.QuizCard.Title);
        Assert.Equal("Take Quiz Now →", hub.Sidebar.QuizCard.ButtonText);
        Assert.True(hub.Sidebar.QuizCard.IsUploadingSoon);

        // 12 Shop by Categories
        Assert.NotNull(hub.ShopByCategories);
        Assert.NotNull(hub.ShopByCategories.Cards);
        Assert.Equal(12, hub.ShopByCategories.Cards.Count);

        var expectedSlugs = new[]
        {
            "luggage-suitcases",
            "packing-organizers",
            "comfort-essentials",
            "adapters-converters",
            "bottles-hydration",
            "toiletry-hygiene",
            "travel-documents",
            "sim-connectivity",
            "travel-insurance",
            "locks-security",
            "tech-accessories",
            "travel-entertainment"
        };

        var actualSlugs = hub.ShopByCategories.Cards.Select(c => c.Slug).ToList();
        foreach (var slug in expectedSlugs)
        {
            Assert.Contains(slug, actualSlugs);
        }

        // All 12 cards should default to IsUploadingSoon == true as requested
        Assert.All(hub.ShopByCategories.Cards, c =>
        {
            Assert.True(c.IsUploadingSoon);
            Assert.True(c.IsActive);
            Assert.False(string.IsNullOrWhiteSpace(c.Title));
            Assert.False(string.IsNullOrWhiteSpace(c.Subtitle));
            Assert.False(string.IsNullOrWhiteSpace(c.TargetUrl));
        });

        // Bottom Trust Badges
        Assert.NotNull(hub.BottomTrustBar);
        Assert.True(hub.BottomTrustBar.IsEnabled);
        Assert.Equal(5, hub.BottomTrustBar.Badges.Count);
        Assert.Contains(hub.BottomTrustBar.Badges, b => b.Title == "Fast & Reliable Delivery");
        Assert.Contains(hub.BottomTrustBar.Badges, b => b.Title == "100% Secure Payments");
        Assert.Contains(hub.BottomTrustBar.Badges, b => b.Title == "Premium Quality Products");
        Assert.Contains(hub.BottomTrustBar.Badges, b => b.Title == "24/7 Customer Support");
        Assert.Contains(hub.BottomTrustBar.Badges, b => b.Title == "Easy Returns");
    }

    [Fact]
    public void ProductListViewModel_IsTravelHub_ReturnsTrueForTravelSlugs()
    {
        var vm1 = new ProductListViewModel { CategorySlug = "travel-gear" };
        var vm2 = new ProductListViewModel { CategorySlug = "TRAVEL" };
        var vm3 = new ProductListViewModel { CategorySlug = "travel_gear" };
        var vm4 = new ProductListViewModel { CategorySlug = "food" };

        Assert.True(vm1.IsTravelHub);
        Assert.True(vm2.IsTravelHub);
        Assert.True(vm3.IsTravelHub);
        Assert.False(vm4.IsTravelHub);
    }

    [Fact]
    public async Task TravelHubService_ReturnsDefaultWhenNoFileExists()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), "travel_test_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns(tempDir);

            var cache = new MemoryCache(new MemoryCacheOptions());
            var logger = NullLogger<TravelHubService>.Instance;

            var service = new TravelHubService(cache, envMock.Object, logger);

            // Act
            var config = await service.GetConfigAsync();

            // Assert
            Assert.NotNull(config);
            Assert.Equal("Dreamflykit • Travel Gear & Luggage", config.ThemeSettings.StoreTitle);
            Assert.Equal(12, config.ShopByCategories.Cards.Count);

            // Check that file was created
            var savedFile = Path.Combine(tempDir, "data", "travel_hub.json");
            Assert.True(File.Exists(savedFile));
        }
        finally
        {
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }
}

