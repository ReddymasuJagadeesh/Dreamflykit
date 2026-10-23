using Dreamflykit.Application.HealthHub.Models;
using Dreamflykit.Web.Models.Products;
using Xunit;

namespace Dreamflykit.Tests;

public class HealthHubTests
{
    [Fact]
    public void HealthHubDto_CreateDefault_ConfiguresHeroBannerWithSlidesAndTiming()
    {
        // Act
        var hub = HealthHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub);
        Assert.NotNull(hub.HeroBanner);
        Assert.True(hub.HeroBanner.IsEnabled);
        Assert.True(hub.HeroBanner.EnableSlideshow);
        Assert.True(hub.HeroBanner.AutoPlay);
        Assert.Equal(5, hub.HeroBanner.SlideIntervalSeconds);
        Assert.Equal("#0d7a5f", hub.HeroBanner.ThemeColor);

        // Slides
        Assert.NotNull(hub.HeroBanner.Slides);
        Assert.True(hub.HeroBanner.Slides.Count >= 2);

        var slide1 = hub.HeroBanner.Slides[0];
        Assert.Equal("FLAT 20% OFF", slide1.DiscountBadge);
        Assert.Equal("Your Health,", slide1.Title);
        Assert.Equal("Our Priority.", slide1.TitleHighlight);
        Assert.False(string.IsNullOrWhiteSpace(slide1.ImageUrl));

        // Slide 1 Badges
        Assert.NotNull(slide1.Badges);
        Assert.Equal(4, slide1.Badges.Count);
        Assert.Contains(slide1.Badges, b => b.Label == "100% Genuine Medicines");
        Assert.Contains(slide1.Badges, b => b.Label == "Doctor Approved");
        Assert.Contains(slide1.Badges, b => b.Label == "Easy Returns");
        Assert.Contains(slide1.Badges, b => b.Label == "Fast & Safe Delivery");
    }

    [Fact]
    public void HealthHubDto_CreateDefault_ConfiguresNineShopByCategories()
    {
        // Act
        var hub = HealthHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.CategoriesSection);
        Assert.True(hub.CategoriesSection.IsEnabled);
        Assert.Equal("Shop by Category", hub.CategoriesSection.Title);
        Assert.NotNull(hub.CategoriesSection.Categories);
        Assert.Equal(9, hub.CategoriesSection.Categories.Count);

        var expectedSlugs = new[]
        {
            "medicines",
            "lab-tests",
            "health-conditions",
            "personal-care",
            "health-devices",
            "baby-care",
            "elderly-care",
            "offers",
            "prescription-upload"
        };

        var actualSlugs = hub.CategoriesSection.Categories.Select(c => c.Slug).ToList();
        foreach (var expectedSlug in expectedSlugs)
        {
            Assert.Contains(expectedSlug, actualSlugs);
        }
    }

    [Fact]
    public void HealthHubDto_CreateDefault_ConfiguresTenPopularHealthCategories()
    {
        // Act
        var hub = HealthHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.PopularSection);
        Assert.True(hub.PopularSection.IsEnabled);
        Assert.Equal("Popular Health Categories", hub.PopularSection.Title);
        Assert.NotNull(hub.PopularSection.Categories);
        Assert.Equal(10, hub.PopularSection.Categories.Count);

        var expectedSlugs = new[]
        {
            "pain-relief",
            "fever",
            "cough-cold",
            "diabetes-care",
            "heart-care",
            "vitamins-supplements",
            "stomach-care",
            "skin-care",
            "women-care",
            "ayurveda"
        };

        var actualSlugs = hub.PopularSection.Categories.Select(c => c.Slug).ToList();
        foreach (var expectedSlug in expectedSlugs)
        {
            Assert.Contains(expectedSlug, actualSlugs);
        }
    }

    [Fact]
    public void HealthHubDto_CreateDefault_ConfiguresSixGuarantees()
    {
        // Act
        var hub = HealthHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.TrustBarSection);
        Assert.True(hub.TrustBarSection.IsEnabled);
        Assert.NotNull(hub.TrustBarSection.Items);
        Assert.Equal(6, hub.TrustBarSection.Items.Count);

        Assert.Contains(hub.TrustBarSection.Items, i => i.Title.Contains("100% Genuine Medicines"));
        Assert.Contains(hub.TrustBarSection.Items, i => i.Title.Contains("Flat 20% OFF"));
        Assert.Contains(hub.TrustBarSection.Items, i => i.Title.Contains("Easy Returns"));
        Assert.Contains(hub.TrustBarSection.Items, i => i.Title.Contains("Fast Delivery"));
        Assert.Contains(hub.TrustBarSection.Items, i => i.Title.Contains("Secure Payments"));
        Assert.Contains(hub.TrustBarSection.Items, i => i.Title.Contains("24/7 Customer Support"));
    }

    [Theory]
    [InlineData("health-care", true)]
    [InlineData("healthcare", true)]
    [InlineData("health_care", true)]
    [InlineData("HEALTH-CARE", true)]
    [InlineData("fashion", false)]
    [InlineData("study-essentials", false)]
    public void ProductListViewModel_CorrectlyIdentifiesHealthCareCategory(string slug, bool expected)
    {
        // Act
        var model = new ProductListViewModel
        {
            CategorySlug = slug
        };

        // Assert
        Assert.Equal(expected, model.IsHealthCareHub);
    }
}
