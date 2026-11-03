using Dreamflykit.Application.FoodHub.Models;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Infrastructure.FoodHub;
using Dreamflykit.Web.Areas.Admin.Controllers;
using Dreamflykit.Web.Models.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Dreamflykit.Tests;

public class FoodHubTests
{
    [Fact]
    public void FoodHubDto_CreateDefault_InitializesFullFreshBasketTemplateWithFullWidthSlides()
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

        // Hero Banner & Full-Width Slides
        Assert.NotNull(hub.HeroBanner);
        Assert.True(hub.HeroBanner.IsEnabled);
        Assert.True(hub.HeroBanner.IsFullWidth);
        Assert.Equal(5, hub.HeroBanner.AutoSlideIntervalSeconds);
        Assert.Equal("Good Food. Good Mood.", hub.HeroBanner.Heading);
        Assert.Equal("Your one-stop shop for groceries, snacks & more.", hub.HeroBanner.Subtitle);
        Assert.Equal("Shop Now →", hub.HeroBanner.ButtonText);
        Assert.NotNull(hub.HeroBanner.TrustBadges);
        Assert.Equal(4, hub.HeroBanner.TrustBadges.Count);

        // Multi-slides verification
        Assert.NotNull(hub.HeroBanner.Slides);
        Assert.True(hub.HeroBanner.Slides.Count >= 4, "Expected at least 4 default hero slides");

        foreach (var slide in hub.HeroBanner.Slides)
        {
            Assert.False(string.IsNullOrWhiteSpace(slide.Id));
            Assert.False(string.IsNullOrWhiteSpace(slide.Title));
            Assert.False(string.IsNullOrWhiteSpace(slide.Subtitle));
            Assert.False(string.IsNullOrWhiteSpace(slide.ButtonText));
            Assert.False(string.IsNullOrWhiteSpace(slide.ButtonUrl));
            Assert.False(string.IsNullOrWhiteSpace(slide.BgGradient));
            Assert.True(slide.IsActive);
        }

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
    public async Task FoodHubService_GetConfigAsync_PreservesSlidesOnSave()
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
            Assert.NotEmpty(config.HeroBanner.Slides);

            // Add custom slide
            config.HeroBanner.Slides.Add(new FoodHeroSlideDto
            {
                Id = "custom-slide-unit-test",
                Title = "Fresh Festive Sweets & Namkeen",
                Subtitle = "Traditional festival sweets crafted with pure desi ghee.",
                ButtonText = "Shop Sweets →",
                ButtonUrl = "/category/food?subcatItem=Sweets",
                BgGradient = "linear-gradient(135deg, #b45309 0%, #f59e0b 100%)",
                IsActive = true
            });

            await service.UpdateConfigAsync(config, "UnitTestUser");

            // Re-fetch
            var updated = await service.GetConfigAsync();
            Assert.Contains(updated.HeroBanner.Slides, s => s.Id == "custom-slide-unit-test");
            var addedSlide = updated.HeroBanner.Slides.First(s => s.Id == "custom-slide-unit-test");
            Assert.Equal("Fresh Festive Sweets & Namkeen", addedSlide.Title);
        }
        finally
        {
            if (Directory.Exists(tempDir))
            {
                try { Directory.Delete(tempDir, true); } catch { }
            }
        }
    }

    [Fact]
    public async Task AdminFoodHubController_UploadImage_ReturnsUrl()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), "Dreamflykit_FoodHub_UploadTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns(tempDir);

            var cache = new MemoryCache(new MemoryCacheOptions());
            var service = new FoodHubService(cache, envMock.Object, NullLogger<FoodHubService>.Instance);
            var mediaMock = new Mock<IMediaService>();
            var urlMock = new Mock<IMediaUrlService>();

            var uploadAsset = new Dreamflykit.Application.Media.Models.MediaAssetDto
            {
                Id = Guid.NewGuid(),
                ObjectKey = "banners/food-test.webp"
            };
            mediaMock.Setup(m => m.UploadAsync(It.IsAny<MediaUploadRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(MediaUploadResult.Success(uploadAsset));
            urlMock.Setup(u => u.GetUrl(It.IsAny<string>()))
                   .Returns("/media/banners/food-test.webp");

            var controller = new FoodHubController(service, mediaMock.Object, urlMock.Object, envMock.Object, NullLogger<FoodHubController>.Instance);
            var user = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity(new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "Admin")
            }, "mock"));
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Mock an image file upload
            var content = "fake image bytes"u8.ToArray();
            var formFile = new FormFile(new MemoryStream(content), 0, content.Length, "file", "test-banner.webp")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/webp"
            };

            // Act
            var result = await controller.UploadImage(formFile, "slide_img", default);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var json = System.Text.Json.JsonSerializer.Serialize(jsonResult.Value);
            using var doc = System.Text.Json.JsonDocument.Parse(json);
            Assert.True(doc.RootElement.GetProperty("success").GetBoolean());
            Assert.False(string.IsNullOrWhiteSpace(doc.RootElement.GetProperty("url").GetString()));
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
