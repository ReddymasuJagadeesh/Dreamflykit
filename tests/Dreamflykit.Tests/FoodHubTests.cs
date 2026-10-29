using Dreamflykit.Application.FoodHub.Models;
using Dreamflykit.Infrastructure.FoodHub;
using Dreamflykit.Web.Models.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Dreamflykit.Tests;

public class FoodHubTests
{
    [Fact]
    public void FoodHubDto_CreateDefault_InitializesFullFreshBasketTemplate()
    {
        // Act
        var hub = FoodHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub);
        Assert.NotNull(hub.ThemeSettings);
        Assert.Equal("FreshBasket • Food & Grocery", hub.ThemeSettings.StoreTitle);

        // Top Notice
        Assert.NotNull(hub.TopNoticeBar);
        Assert.True(hub.TopNoticeBar.IsEnabled);
        Assert.Contains("Free Shipping", hub.TopNoticeBar.Item1Text);

        // Hero Banner
        Assert.NotNull(hub.HeroBanner);
        Assert.True(hub.HeroBanner.IsEnabled);
        Assert.Equal("Good Food. Good Mood.", hub.HeroBanner.Heading);
        Assert.Equal("Your one-stop shop for groceries, snacks & more.", hub.HeroBanner.Subtitle);
        Assert.Equal("Shop Now →", hub.HeroBanner.ButtonText);
        Assert.NotNull(hub.HeroBanner.TrustBadges);
        Assert.Equal(4, hub.HeroBanner.TrustBadges.Count);

        // Shop by 7 Categories
        Assert.NotNull(hub.ShopByCategories);
        Assert.NotNull(hub.ShopByCategories.Cards);
        Assert.Equal(7, hub.ShopByCategories.Cards.Count);

        var expectedSlugs = new[]
        {
            "groceries",
            "instant-noodles",
            "nuts-dry-fruits",
            "spices-seasonings",
            "breakfast-cereals",
            "snacks",
            "pickles"
        };

        var actualSlugs = hub.ShopByCategories.Cards.Select(c => c.Slug).ToList();
        foreach (var slug in expectedSlugs)
        {
            Assert.Contains(slug, actualSlugs);
        }

        // Verify Groceries has sub-items
        var groceries = hub.ShopByCategories.Cards.First(c => c.Slug == "groceries");
        Assert.Contains(groceries.Items, i => i.Contains("Flour"));
        Assert.Contains(groceries.Items, i => i.Contains("Oils"));

        // 3 Promo Cards
        Assert.NotNull(hub.PromoCards);
        Assert.NotNull(hub.PromoCards.Cards);
        Assert.Equal(3, hub.PromoCards.Cards.Count);
        Assert.All(hub.PromoCards.Cards, p =>
        {
            Assert.False(string.IsNullOrWhiteSpace(p.Heading));
            Assert.False(string.IsNullOrWhiteSpace(p.Subtitle));
            Assert.False(string.IsNullOrWhiteSpace(p.ButtonText));
            Assert.False(string.IsNullOrWhiteSpace(p.ImageUrl));
        });

        // Best Sellers
        Assert.NotNull(hub.BestSellers);
        Assert.NotEmpty(hub.BestSellers.Items);
        Assert.Contains(hub.BestSellers.Items, i => i.Title.Contains("Atta"));
        Assert.Contains(hub.BestSellers.Items, i => i.Title.Contains("Sunflower Oil"));
        Assert.Contains(hub.BestSellers.Items, i => i.Title.Contains("Maggi"));

        // Bottom Trust Bar
        Assert.NotNull(hub.BottomTrustBar);
        Assert.False(string.IsNullOrWhiteSpace(hub.BottomTrustBar.Item1Title));

        // Stay Updated Newsletter
        Assert.NotNull(hub.StayUpdated);
        Assert.Equal("Stay Updated!", hub.StayUpdated.Heading);
    }

    [Theory]
    [InlineData("food", true)]
    [InlineData("FOOD", true)]
    [InlineData("foods", true)]
    [InlineData("groceries", true)]
    [InlineData("beauty", false)]
    [InlineData("mobiles", false)]
    [InlineData("study-essentials", false)]
    public void ProductListViewModel_IsFoodHub_IdentifiesFoodCategoryCorrectly(string categorySlug, bool expectedIsFood)
    {
        // Arrange
        var model = new ProductListViewModel
        {
            CategorySlug = categorySlug
        };

        // Act & Assert
        Assert.Equal(expectedIsFood, model.IsFoodHub);
    }

    [Fact]
    public async Task FoodHubService_GetConfigAsync_ReturnsConfigAndCachesResult()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), "Dreamflykit_FoodHub_Test_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns(tempDir);

            var cache = new MemoryCache(new MemoryCacheOptions());
            var service = new FoodHubService(cache, envMock.Object, NullLogger<FoodHubService>.Instance);

            // Act
            var config = await service.GetConfigAsync();

            // Assert
            Assert.NotNull(config);
            Assert.Equal("Good Food. Good Mood.", config.HeroBanner.Heading);

            // Mutate and save
            config.HeroBanner.Heading = "Fresh Groceries Delivered Daily";
            await service.UpdateConfigAsync(config, "UnitTestUser");

            // Re-fetch (should get updated config)
            var updated = await service.GetConfigAsync();
            Assert.Equal("Fresh Groceries Delivered Daily", updated.HeroBanner.Heading);
        }
        finally
        {
            if (Directory.Exists(tempDir))
            {
                try { Directory.Delete(tempDir, true); } catch { }
            }
        }
    }
}

