using Dreamflykit.Application.FashionHub.Models;

namespace Dreamflykit.Tests;

public class FashionHubTests
{
    [Fact]
    public void FashionHubDto_CreateDefault_ConfiguresHeroBannerWithBadgesAndRightCard()
    {
        // Act
        var hub = FashionHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub);
        Assert.NotNull(hub.HeroBanner);
        Assert.True(hub.HeroBanner.IsEnabled);
        Assert.Equal("Fashion for", hub.HeroBanner.Title);
        Assert.Equal("Every Journey", hub.HeroBanner.TitleHighlight);
        Assert.False(string.IsNullOrWhiteSpace(hub.HeroBanner.Subtitle));

        // Badges
        Assert.NotNull(hub.HeroBanner.Badges);
        Assert.Equal(4, hub.HeroBanner.Badges.Count);
        Assert.Contains(hub.HeroBanner.Badges, b => b.Label == "Trendy & Comfortable");
        Assert.Contains(hub.HeroBanner.Badges, b => b.Label == "Premium Quality");
        Assert.Contains(hub.HeroBanner.Badges, b => b.Label == "Affordable Prices");
        Assert.Contains(hub.HeroBanner.Badges, b => b.Label == "Loved by 10K+ Students");

        // Right Card ("Look Good. Travel Smart.")
        Assert.NotNull(hub.HeroBanner.RightCard);
        Assert.Equal("Look Good.", hub.HeroBanner.RightCard.Title);
        Assert.Equal("Travel Smart.", hub.HeroBanner.RightCard.TitleHighlight);
        Assert.Equal(3, hub.HeroBanner.RightCard.Features.Count);
        Assert.Contains(hub.HeroBanner.RightCard.Features, f => f.Title == "Travel Friendly");
        Assert.Contains(hub.HeroBanner.RightCard.Features, f => f.Title == "Lightweight & Easy");
        Assert.Contains(hub.HeroBanner.RightCard.Features, f => f.Title == "All-Season Wear");
    }

    [Fact]
    public void FashionHubDto_CreateDefault_ConfiguresAllTenShopByCategories()
    {
        // Act
        var hub = FashionHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.CategoriesSection);
        Assert.True(hub.CategoriesSection.IsEnabled);
        Assert.Equal("Shop by Category", hub.CategoriesSection.Title);
        Assert.NotNull(hub.CategoriesSection.Categories);
        Assert.Equal(10, hub.CategoriesSection.Categories.Count);

        var expectedSlugs = new[]
        {
            "men-clothing",
            "women-clothing",
            "men-footwear",
            "women-footwear",
            "watches",
            "sunglasses",
            "caps-hats",
            "belts-wallets",
            "socks",
            "accessories"
        };

        var actualSlugs = hub.CategoriesSection.Categories.Select(c => c.Slug).ToList();
        foreach (var expectedSlug in expectedSlugs)
        {
            Assert.Contains(expectedSlug, actualSlugs);
        }

        // Each category defaults to IsUploadingSoon = true until admin uploads photo
        Assert.All(hub.CategoriesSection.Categories, c => Assert.True(c.IsUploadingSoon));
    }

    [Fact]
    public void FashionHubDto_CreateDefault_ConfiguresTopPicksProducts()
    {
        // Act
        var hub = FashionHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.TopPicksSection);
        Assert.True(hub.TopPicksSection.IsEnabled);
        Assert.Equal("Top Picks for Men & Women", hub.TopPicksSection.Title);
        Assert.NotNull(hub.TopPicksSection.Products);
        Assert.Equal(8, hub.TopPicksSection.Products.Count);

        // Check key products
        Assert.Contains(hub.TopPicksSection.Products, p => p.Name == "Men's Hoodie" && p.Price == 999m);
        Assert.Contains(hub.TopPicksSection.Products, p => p.Name == "Men's Bomber Jacket" && p.Price == 1499m);
        Assert.Contains(hub.TopPicksSection.Products, p => p.Name == "Women's Sweatshirt" && p.Price == 899m);
        Assert.Contains(hub.TopPicksSection.Products, p => p.Name == "Women's Denim Jacket" && p.Price == 1299m);
    }

    [Fact]
    public void FashionHubDto_CreateDefault_ConfiguresTrustBadgesAndBottomBar()
    {
        // Act
        var hub = FashionHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.TrustBadgesSection);
        Assert.Equal(6, hub.TrustBadgesSection.Badges.Count);

        Assert.NotNull(hub.BottomBanner);
        Assert.True(hub.BottomBanner.IsEnabled);
        Assert.Equal("Not Sure What You Need?", hub.BottomBanner.QuizTitle);
        Assert.Equal("Find My Kit", hub.BottomBanner.QuizButtonText);
        Assert.Equal("Kits for Top Destinations", hub.BottomBanner.DestinationsTitle);
        Assert.Equal("Bulk Orders for Universities", hub.BottomBanner.BulkTitle);
        Assert.Equal("Contact Us", hub.BottomBanner.BulkButtonText);
    }
}
