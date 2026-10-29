using System.Text.Json;
using Dreamflykit.Application.BeautyHub.Models;
using Dreamflykit.Web.Models.Products;
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

        // 17 Category Shortcuts
        Assert.NotNull(dto.SubCategories);
        Assert.Equal(17, dto.SubCategories.Items.Count);
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Skincare");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Makeup");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Hair care");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Fragrances");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Men's grooming");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "Derma store");
        Assert.Contains(dto.SubCategories.Items, i => i.Title == "K-beauty");

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
    public void JsonSerialization_PreservesAllBeautyHubProperties()
    {
        // Arrange
        var original = BeautyHubDto.CreateDefault();

        // Act
        var json = JsonSerializer.Serialize(original);
        var restored = JsonSerializer.Deserialize<BeautyHubDto>(json);

        // Assert
        Assert.NotNull(restored);
        Assert.Equal(original.ThemeSettings.ThemeName, restored.ThemeSettings.ThemeName);
        Assert.Equal(original.HeroBanner.Slides.Count, restored.HeroBanner.Slides.Count);
        Assert.Equal(original.SubCategories.Items.Count, restored.SubCategories.Items.Count);
        Assert.Equal(original.SpecialGiftsForHer.Cards.Count, restored.SpecialGiftsForHer.Cards.Count);
        Assert.Equal(original.GrandGiftsForHim.Cards.Count, restored.GrandGiftsForHim.Cards.Count);
        Assert.Equal(original.PremiumGifts.Brands.Count, restored.PremiumGifts.Brands.Count);
        Assert.Equal(original.FestiveLooks.Cards.Count, restored.FestiveLooks.Cards.Count);
    }
}

