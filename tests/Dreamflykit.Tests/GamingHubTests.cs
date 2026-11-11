using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.GamingHub.Interfaces;
using Dreamflykit.Application.GamingHub.Models;
using Dreamflykit.Infrastructure.GamingHub;
using Dreamflykit.Web.Areas.Admin.Controllers;
using Dreamflykit.Web.Controllers;
using Dreamflykit.Web.Models.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Dreamflykit.Tests;

public class GamingHubTests
{
    private static readonly string[] Expected22Categories = new[]
    {
        "playstation", "xbox", "nintendo", "steam-deck", "vr-gaming",
        "steering-wheels", "preloaded-consoles", "asus-rog", "lenovo-legion",
        "retro-gaming-arcade", "game-projector", "open-box-gaming", "drones",
        "bluetooth-speaker", "gaming-hard-drive", "motion-sensor", "fitness-gaming",
        "microphone", "controllers-and-gamepads", "playstation-ecosystem",
        "xbox-ecosystem", "retro-budget-gaming"
    };

    [Fact]
    public void GamingHubDto_CreateDefault_InitializesHeroBannerAnd22Categories()
    {
        // Act
        var dto = GamingHubDto.CreateDefault();

        // Assert
        Assert.NotNull(dto);
        Assert.NotNull(dto.HeroBanner);
        Assert.True(dto.HeroBanner.IsEnabled);
        Assert.NotEmpty(dto.HeroBanner.Slides);
        Assert.True(dto.HeroBanner.Slides.Count >= 3);
        Assert.All(dto.HeroBanner.Slides, s =>
        {
            Assert.False(string.IsNullOrWhiteSpace(s.Title));
            Assert.False(string.IsNullOrWhiteSpace(s.TargetUrl));
            Assert.True(s.IsUploadingSoon);
        });

        // Verify Shop by Category contains all 22 gaming categories
        Assert.NotNull(dto.ShopByCategory);
        Assert.True(dto.ShopByCategory.IsEnabled);
        Assert.Equal(22, dto.ShopByCategory.Items.Count);

        var actualSlugs = dto.ShopByCategory.Items.Select(c => c.Slug).ToList();
        foreach (var expectedSlug in Expected22Categories)
        {
            Assert.Contains(expectedSlug, actualSlugs);
        }

        // Verify each Category Item has researched subcategories and important notes
        Assert.All(dto.ShopByCategory.Items, item =>
        {
            Assert.NotEmpty(item.Subcategories);
            Assert.True(item.Subcategories.Count >= 4, $"Category {item.Name} should have at least 4 subcategories.");
            Assert.False(string.IsNullOrWhiteSpace(item.ImportantNote), $"Category {item.Name} should have an important note.");
        });

        // Verify Subcategory Pages (each category has a dedicated landing page)
        Assert.NotNull(dto.SubcategoryPages);
        Assert.Equal(22, dto.SubcategoryPages.Count);
        Assert.All(dto.SubcategoryPages, page =>
        {
            Assert.NotNull(page.HeroBanner);
            Assert.False(string.IsNullOrWhiteSpace(page.CategoryName));
            Assert.NotEmpty(page.Subcategories);
            Assert.True(page.Subcategories.Count >= 4);
            Assert.NotEmpty(page.Products);
            Assert.All(page.Products, p =>
            {
                Assert.True(p.NumericPrice > 0, $"Product {p.Title} should have valid numeric price.");
                Assert.True(p.IsUploadingSoon);
                Assert.False(string.IsNullOrWhiteSpace(p.SubcategoryTag), $"Product {p.Title} should have a subcategory tag.");
                Assert.False(string.IsNullOrWhiteSpace(p.Description), $"Product {p.Title} should have a description.");
            });
        });

        // Verify Top Picks
        Assert.NotNull(dto.TopPicks);
        Assert.True(dto.TopPicks.IsEnabled);
        Assert.NotEmpty(dto.TopPicks.Products);
    }

    [Theory]
    [InlineData("playstation", "PlayStation")]
    [InlineData("xbox", "Xbox")]
    [InlineData("nintendo", "Nintendo")]
    [InlineData("steam-deck", "Steam Deck")]
    [InlineData("vr-gaming", "VR Gaming")]
    [InlineData("steering-wheels", "Steering Wheel")]
    public async Task GamingHubService_GetSubcategoryPageAsync_ReturnsConfiguredPage(string slug, string expectedName)
    {
        // Arrange
        var mockRepo = new Mock<ICategoryHubRepository>();
        mockRepo.Setup(r => r.GetPayloadAsync<GamingHubDto>("gaming-hub", "gaming_hub.json", It.IsAny<CancellationToken>()))
            .ReturnsAsync(GamingHubDto.CreateDefault());

        var logger = new Mock<ILogger<GamingHubService>>();
        var service = new GamingHubService(mockRepo.Object, logger.Object);

        // Act
        var page = await service.GetSubcategoryPageAsync(slug);

        // Assert
        Assert.NotNull(page);
        Assert.Equal(slug, page.CategorySlug);
        Assert.Contains(expectedName, page.CategoryName);
        Assert.NotEmpty(page.Subcategories);
        Assert.NotEmpty(page.Products);
    }

    [Fact]
    public async Task ProductsController_GamingHub_EndToEndRouting_RendersCorrectExplorerViews()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), "GamingControllerTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        var wwwrootDir = Path.Combine(tempDir, "wwwroot");
        Directory.CreateDirectory(wwwrootDir);

        try
        {
            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns(wwwrootDir);
            var cache = new MemoryCache(new MemoryCacheOptions());
            var gamingService = new GamingHubService(cache, envMock.Object, new Mock<ILogger<GamingHubService>>().Object);

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductSearchResult
                {
                    Items = new List<ProductListItem>(),
                    TotalCount = 0,
                    Page = 1,
                    PageSize = 24
                });

            var mockCatService = new Mock<ICategoryService>();

            var controller = new Dreamflykit.Web.Controllers.ProductsController(
                mockProductService.Object,
                mockCatService.Object,
                Mock.Of<Dreamflykit.Application.StudyHub.Interfaces.IStudyHubService>(),
                Mock.Of<Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService>(),
                Mock.Of<Dreamflykit.Application.HealthHub.Interfaces.IHealthHubService>(),
                Mock.Of<Dreamflykit.Application.MobilesHub.Interfaces.IMobilesHubService>(),
                Mock.Of<Dreamflykit.Application.AppliancesHub.Interfaces.IAppliancesHubService>(),
                Mock.Of<Dreamflykit.Application.BeautyHub.Interfaces.IBeautyHubService>(),
                Mock.Of<Dreamflykit.Application.FoodHub.Interfaces.IFoodHubService>(),
                Mock.Of<Dreamflykit.Application.TravelHub.Interfaces.ITravelHubService>(),
                Mock.Of<Dreamflykit.Application.LuggageHub.Interfaces.ILuggageHubService>(),
                Mock.Of<Dreamflykit.Application.ElectronicsHub.Interfaces.IElectronicsHubService>(),
                storyBooksHubService: null,
                gamingHubService: gamingService,
                sportsHubService: null
            );

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act 1: Main Gaming Hub landing
            var result1 = await controller.Index(category: "gaming-hub", cancellationToken: default) as ViewResult;
            Assert.NotNull(result1);
            var model1 = result1.Model as ProductListViewModel;
            Assert.NotNull(model1);
            Assert.True(model1.IsGamingHub);
            Assert.Equal("hub", model1.ViewMode);
            Assert.NotNull(model1.GamingHub);
            Assert.Equal(22, model1.GamingHub.ShopByCategory.Items.Count);

            // Act 2: Subcategory Page (e.g. PlayStation)
            var result2 = await controller.Index(category: "gaming-hub", subcategorySlug: "playstation", cancellationToken: default) as ViewResult;
            Assert.NotNull(result2);
            var model2 = result2.Model as ProductListViewModel;
            Assert.NotNull(model2);
            Assert.True(model2.IsGamingHub);
            Assert.True(model2.IsGamingSubcategory);
            Assert.Equal("gaming-sub", model2.ViewMode);
            Assert.NotNull(model2.GamingSubcategoryPage);
            Assert.Equal("PlayStation", model2.GamingSubcategoryPage.CategoryName);
            Assert.NotEmpty(model2.GamingSubcategoryPage.Subcategories);

            // Act 3: Categories Explorer View (e.g. view=categories)
            var result3 = await controller.Index(category: "gaming-hub", view: "categories", cancellationToken: default) as ViewResult;
            Assert.NotNull(result3);
            var model3 = result3.Model as ProductListViewModel;
            Assert.NotNull(model3);
            Assert.True(model3.IsGamingHub);
            Assert.Equal("gaming-sub", model3.ViewMode);
            Assert.Equal("playstation", model3.CurrentSubcategorySlug);
            Assert.NotNull(model3.GamingSubcategoryPage);
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
