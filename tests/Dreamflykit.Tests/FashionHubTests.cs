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

    [Fact]
    public void FashionHubDto_CreateDefault_ConfiguresHeroSlidesAndIntervalTimeframe()
    {
        // Act
        var hub = FashionHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.HeroBanner);
        Assert.Equal(5, hub.HeroBanner.SlideIntervalSeconds);
        Assert.NotNull(hub.HeroBanner.Slides);
        Assert.Equal(3, hub.HeroBanner.Slides.Count);

        // Slide 1: Main Fashion
        var slide1 = hub.HeroBanner.Slides[0];
        Assert.Equal("Fashion for", slide1.Title);
        Assert.Equal("Every Journey", slide1.TitleHighlight);
        Assert.Equal("/category/fashion/women-clothing", slide1.ButtonUrl);

        // Slide 2: Hoodies
        var slide2 = hub.HeroBanner.Slides[1];
        Assert.Equal("Premium Comfort", slide2.Title);
        Assert.Equal("Hoodies & Sweatshirts", slide2.TitleHighlight);
        Assert.Equal("/category/fashion/hoodies", slide2.ButtonUrl);

        // Slide 3: Sweatshirts
        var slide3 = hub.HeroBanner.Slides[2];
        Assert.Equal("All-Weather Outfits", slide3.Title);
        Assert.Equal("Men & Women Collection", slide3.TitleHighlight);
        Assert.Equal("/category/fashion/sweatshirts", slide3.ButtonUrl);
    }

    [Fact]
    public void FashionHubDto_CreateDefault_ConfiguresWomenClothingTemplate()
    {
        // Act
        var hub = FashionHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.WomenClothing);
        Assert.True(hub.WomenClothing.IsEnabled);

        // Hero
        Assert.NotNull(hub.WomenClothing.HeroBanner);
        Assert.Equal("Women Clothing", hub.WomenClothing.HeroBanner.Title);
        Assert.Equal("Stylish. Comfortable. Travel-ready.", hub.WomenClothing.HeroBanner.TitleHighlight);
        Assert.Equal(4, hub.WomenClothing.HeroBanner.Badges.Count);
        Assert.Equal(4, hub.WomenClothing.HeroBanner.RightCard.Features.Count);

        // 10 Categories from Screenshot 2
        Assert.NotNull(hub.WomenClothing.CategoriesSection);
        Assert.Equal(10, hub.WomenClothing.CategoriesSection.Categories.Count);
        var catNames = hub.WomenClothing.CategoriesSection.Categories.Select(c => c.Name).ToList();
        Assert.Contains("Tops & T-Shirts", catNames);
        Assert.Contains("Shirts & Blouses", catNames);
        Assert.Contains("Dresses", catNames);
        Assert.Contains("Hoodies & Sweatshirts", catNames);
        Assert.Contains("Jackets & Coats", catNames);
        Assert.Contains("Bottom Wear", catNames);
        Assert.Contains("Activewear", catNames);
        Assert.Contains("Innerwear & Sleepwear", catNames);
        Assert.Contains("Ethnic Wear", catNames);
        Assert.Contains("Accessories", catNames);

        // 8 Top Picks from Screenshot 2
        Assert.NotNull(hub.WomenClothing.TopPicksSection);
        Assert.Equal(8, hub.WomenClothing.TopPicksSection.Products.Count);
        var prodNames = hub.WomenClothing.TopPicksSection.Products.Select(p => p.Name).ToList();
        Assert.Contains("Oversized Cotton T-Shirt", prodNames);
        Assert.Contains("Floral Maxi Dress", prodNames);
        Assert.Contains("Women's Linen Shirt", prodNames);
        Assert.Contains("High Waist Leggings", prodNames);
        Assert.Contains("Women's Hoodie", prodNames);
        Assert.Contains("Mom Fit Jeans", prodNames);
        Assert.Contains("Trench Coat", prodNames);
        Assert.Contains("Cotton Night Suit", prodNames);
    }

    [Fact]
    public void FashionHubDto_CreateDefault_ConfiguresHoodiesTemplate()
    {
        // Act
        var hub = FashionHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.Hoodies);
        Assert.True(hub.Hoodies.IsEnabled);
        Assert.Equal("NEW COLLECTION", hub.Hoodies.Tag);
        Assert.Equal("HOODIES", hub.Hoodies.Title);
        Assert.Equal("MADE FOR EVERY DAY", hub.Hoodies.TitleHighlight);
        Assert.Equal("SHOP ALL HOODIES", hub.Hoodies.ShopAllButtonText);
        Assert.Equal("NEW ARRIVALS", hub.Hoodies.NewArrivalsButtonText);

        // 3 Promo Feature Cards from Screenshot 3
        Assert.NotNull(hub.Hoodies.PromoCards);
        Assert.Equal(3, hub.Hoodies.PromoCards.Count);
        var promoTitles = hub.Hoodies.PromoCards.Select(p => p.Title).ToList();
        Assert.Contains("MINIMAL ESSENTIALS", promoTitles);
        Assert.Contains("GRAPHIC STATEMENT", promoTitles);
        Assert.Contains("PREMIUM COMFORT", promoTitles);

        // 6 Fits from Screenshot 3
        Assert.NotNull(hub.Hoodies.FitSection);
        Assert.Equal("SHOP BY FIT", hub.Hoodies.FitSection.Title);
        Assert.Equal(6, hub.Hoodies.FitSection.Items.Count);
        var fitNames = hub.Hoodies.FitSection.Items.Select(f => f.Name).ToList();
        Assert.Contains("Oversized Fit", fitNames);
        Assert.Contains("Regular Fit", fitNames);
        Assert.Contains("Relaxed Fit", fitNames);
        Assert.Contains("Zip-Up Hoodies", fitNames);
        Assert.Contains("Drop Shoulder", fitNames);
        Assert.Contains("Sleeveless Hoodies", fitNames);
    }

    [Fact]
    public void FashionHubDto_CreateDefault_ConfiguresSweatshirtsTemplate()
    {
        // Act
        var hub = FashionHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.Sweatshirts);
        Assert.True(hub.Sweatshirts.IsEnabled);
        Assert.Equal("DREAMFLYKIT OUTLET", hub.Sweatshirts.OutletTag);
        Assert.Equal("MEN'S COLLECTION", hub.Sweatshirts.CollectionTag);
        Assert.Equal("SWEATSHIRTS", hub.Sweatshirts.Title);
        Assert.Equal("SHOP ALL SWEATSHIRTS", hub.Sweatshirts.ShopAllButtonText);

        // 4 Badges under buttons
        Assert.NotNull(hub.Sweatshirts.HeroBadges);
        Assert.Equal(4, hub.Sweatshirts.HeroBadges.Count);
        var badgeTitles = hub.Sweatshirts.HeroBadges.Select(b => b.Title).ToList();
        Assert.Contains("Soft & Comfortable", badgeTitles);
        Assert.Contains("Premium Quality", badgeTitles);
        Assert.Contains("Best Prices Everyday", badgeTitles);
        Assert.Contains("Easy Care Fabric", badgeTitles);

        // 3 Promo Cards from Screenshot 4
        Assert.NotNull(hub.Sweatshirts.PromoCards);
        Assert.Equal(3, hub.Sweatshirts.PromoCards.Count);
        var promoTitles = hub.Sweatshirts.PromoCards.Select(p => p.Title).ToList();
        Assert.Contains("EVERYDAY ESSENTIALS", promoTitles);
        Assert.Contains("GRAPHIC SWEATSHIRTS", promoTitles);
        Assert.Contains("PREMIUM COMFORT", promoTitles);

        // 10 Styles from Screenshot 4
        Assert.NotNull(hub.Sweatshirts.StyleSection);
        Assert.Equal("SHOP BY STYLE", hub.Sweatshirts.StyleSection.Title);
        Assert.Equal(10, hub.Sweatshirts.StyleSection.Items.Count);
        var styleNames = hub.Sweatshirts.StyleSection.Items.Select(s => s.Name).ToList();
        Assert.Contains("Crew Neck", styleNames);
        Assert.Contains("Hoodies", styleNames);
        Assert.Contains("Oversized", styleNames);
        Assert.Contains("Zip-Up", styleNames);
        Assert.Contains("Printed", styleNames);
        Assert.Contains("Solid", styleNames);
        Assert.Contains("Half Zip", styleNames);
        Assert.Contains("Fleece", styleNames);
        Assert.Contains("Colorblock", styleNames);
        Assert.Contains("Washed", styleNames);

        // Trust badges
        Assert.NotNull(hub.Sweatshirts.TrustBadges);
        Assert.Equal(4, hub.Sweatshirts.TrustBadges.Count);
    }
}
