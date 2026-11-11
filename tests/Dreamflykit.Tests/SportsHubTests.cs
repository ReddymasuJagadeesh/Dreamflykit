using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.SportsHub.Interfaces;
using Dreamflykit.Application.SportsHub.Models;
using Dreamflykit.Infrastructure.SportsHub;
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

public class SportsHubTests
{
    [Fact]
    public void SportsHubDto_CreateDefault_InitializesHeroBannerAnd12Categories()
    {
        // Act
        var dto = SportsHubDto.CreateDefault();

        // Assert
        Assert.NotNull(dto);
        Assert.NotNull(dto.FestivalTheme);
        Assert.True(dto.FestivalTheme.IsEnabled);
        Assert.Equal("SPORTS SPECIAL", dto.FestivalTheme.BadgeText);

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

        // Verify Shop by Category contains all 12 sports categories
        Assert.NotNull(dto.ShopByCategory);
        Assert.True(dto.ShopByCategory.IsEnabled);
        Assert.Equal(12, dto.ShopByCategory.Items.Count);

        var expectedCategories = new[]
        {
            "cricket", "football", "badminton", "gym-fitness", "basketball-volleyball",
            "tennis-table-tennis", "sports-footwear", "athletic-wear", "swimming",
            "sports-bags", "yoga-recovery", "outdoor-trekking"
        };

        var actualSlugs = dto.ShopByCategory.Items.Select(c => c.Slug).ToList();
        foreach (var expectedSlug in expectedCategories)
        {
            Assert.Contains(expectedSlug, actualSlugs);
        }

        // Verify each Category Item has populated subcategories
        Assert.All(dto.ShopByCategory.Items, item =>
        {
            Assert.NotEmpty(item.Subcategories);
            Assert.True(item.Subcategories.Count >= 5);
        });

        // Verify Subcategory Pages (each category has a dedicated landing page)
        Assert.NotNull(dto.SubcategoryPages);
        Assert.Equal(12, dto.SubcategoryPages.Count);
        Assert.All(dto.SubcategoryPages, page =>
        {
            Assert.NotNull(page.HeroBanner);
            Assert.False(string.IsNullOrWhiteSpace(page.CategoryName));
            Assert.NotEmpty(page.Subcategories);
            Assert.True(page.Subcategories.Count >= 5);
            Assert.NotEmpty(page.Products);
            Assert.All(page.Products, p =>
            {
                Assert.True(p.Price > 0);
                Assert.True(p.IsUploadingSoon);
                Assert.False(string.IsNullOrWhiteSpace(p.SubcategoryTag));
                Assert.False(string.IsNullOrWhiteSpace(p.Description));
            });
        });

        // Verify Top Picks
        Assert.NotNull(dto.TopPicks);
        Assert.True(dto.TopPicks.IsEnabled);
        Assert.NotEmpty(dto.TopPicks.Products);

        // Verify Trust Badges & Bottom Banner
        Assert.NotNull(dto.TrustBadges);
        Assert.NotEmpty(dto.TrustBadges.Badges);
        Assert.NotNull(dto.BottomBanner);
        Assert.True(dto.BottomBanner.IsEnabled);
    }

    [Fact]
    public async Task SportsHubService_GetConfigAsync_CreatesDefaultAndCaches()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), "SportsHubTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        var wwwrootDir = Path.Combine(tempDir, "wwwroot");
        Directory.CreateDirectory(wwwrootDir);

        try
        {
            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns(wwwrootDir);

            var cache = new MemoryCache(new MemoryCacheOptions());
            var loggerMock = new Mock<ILogger<SportsHubService>>();
            var service = new SportsHubService(cache, envMock.Object, loggerMock.Object);

            // Act
            var config = await service.GetConfigAsync();

            // Assert
            Assert.NotNull(config);
            Assert.Equal(12, config.ShopByCategory.Items.Count);

            // Verify subcategory retrieval
            var cricketPage = await service.GetSubcategoryPageAsync("cricket");
            Assert.NotNull(cricketPage);
            Assert.Equal("Cricket", cricketPage.CategoryName);
            Assert.NotEmpty(cricketPage.Products);

            // Verify update
            config.FestivalTheme.AnnouncementText = "Updated Sports Festival Announcement";
            var updated = await service.UpdateConfigAsync(config, "test-user");
            Assert.True(updated);

            var reloaded = await service.GetConfigAsync();
            Assert.Equal("Updated Sports Festival Announcement", reloaded.FestivalTheme.AnnouncementText);
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
    public void ProductListViewModel_IsSportsHub_RecognizesSportsCategorySlugs()
    {
        var vm1 = new ProductListViewModel { CategorySlug = "sports-hub" };
        Assert.True(vm1.IsSportsHub);
        Assert.False(vm1.IsSportsSubcategory);

        var vm2 = new ProductListViewModel { CategorySlug = "sports_hub" };
        Assert.True(vm2.IsSportsHub);

        var vm3 = new ProductListViewModel { CategorySlug = "sports" };
        Assert.True(vm3.IsSportsHub);

        var vmSub = new ProductListViewModel { CategorySlug = "sports-hub", CurrentSubcategorySlug = "cricket" };
        Assert.True(vmSub.IsSportsHub);
        Assert.True(vmSub.IsSportsSubcategory);
    }

    [Fact]
    public async Task ProductsController_SportsCategory_ResolvesHubAndSubcategory()
    {
        // Arrange
        var tempDir = Path.Combine(Path.GetTempPath(), "SportsControllerTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        var wwwrootDir = Path.Combine(tempDir, "wwwroot");
        Directory.CreateDirectory(wwwrootDir);

        try
        {
            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns(wwwrootDir);
            var cache = new MemoryCache(new MemoryCacheOptions());
            var sportsService = new SportsHubService(cache, envMock.Object, new Mock<ILogger<SportsHubService>>().Object);

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

            var controller = new ProductsController(
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
                gamingHubService: null,
                sportsHubService: sportsService
            );

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act 1: Main Sports Hub
            var result1 = await controller.Index(category: "sports-hub", cancellationToken: default) as ViewResult;
            Assert.NotNull(result1);
            var model1 = result1.Model as ProductListViewModel;
            Assert.NotNull(model1);
            Assert.True(model1.IsSportsHub);
            Assert.Equal("hub", model1.ViewMode);
            Assert.NotNull(model1.SportsHub);
            Assert.Equal(12, model1.SportsHub.ShopByCategory.Items.Count);

            // Act 2: Subcategory Page (e.g. Football)
            var result2 = await controller.Index(category: "sports-hub", subcategorySlug: "football", cancellationToken: default) as ViewResult;
            Assert.NotNull(result2);
            var model2 = result2.Model as ProductListViewModel;
            Assert.NotNull(model2);
            Assert.True(model2.IsSportsHub);
            Assert.True(model2.IsSportsSubcategory);
            Assert.Equal("sports-sub", model2.ViewMode);
            Assert.NotNull(model2.SportsSubcategoryPage);
            Assert.Equal("Football", model2.SportsSubcategoryPage.CategoryName);
            // Act 3: Categories Explorer View (e.g. view=categories)
            var result3 = await controller.Index(category: "sports-hub", view: "categories", cancellationToken: default) as ViewResult;
            Assert.NotNull(result3);
            var model3 = result3.Model as ProductListViewModel;
            Assert.NotNull(model3);
            Assert.True(model3.IsSportsHub);
            Assert.Equal("sports-sub", model3.ViewMode);
            Assert.Equal("cricket", model3.CurrentSubcategorySlug);
            Assert.NotNull(model3.SportsSubcategoryPage);
            Assert.NotEmpty(model3.SportsSubcategoryPage.Subcategories);
            Assert.NotEmpty(model3.SportsSubcategoryPage.Products);
            Assert.All(model3.SportsSubcategoryPage.Products, p =>
            {
                Assert.False(string.IsNullOrWhiteSpace(p.SubcategoryTag));
                Assert.False(string.IsNullOrWhiteSpace(p.Description));
                Assert.True(p.IsUploadingSoon);
            });
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
    public void SportsHubDto_All12Categories_HaveResearchedSubcategoriesAndProducts()
    {
        // Arrange & Act
        var dto = SportsHubDto.CreateDefault();

        var expectedCategories = new[]
        {
            "cricket", "football", "badminton", "gym-fitness", "basketball-volleyball",
            "tennis-table-tennis", "sports-footwear", "athletic-wear", "swimming",
            "sports-bags", "yoga-recovery", "outdoor-trekking"
        };

        // Assert
        Assert.Equal(12, dto.ShopByCategory.Items.Count);
        Assert.Equal(12, dto.SubcategoryPages.Count);

        foreach (var slug in expectedCategories)
        {
            var catItem = dto.ShopByCategory.Items.FirstOrDefault(c => c.Slug == slug);
            Assert.NotNull(catItem);
            Assert.NotEmpty(catItem.Subcategories);
            Assert.True(catItem.Subcategories.Count >= 5, $"Category {slug} has less than 5 subcategories.");

            var subPage = dto.SubcategoryPages.FirstOrDefault(p => p.CategorySlug == slug);
            Assert.NotNull(subPage);
            Assert.NotEmpty(subPage.Subcategories);
            Assert.NotEmpty(subPage.Products);
            Assert.True(subPage.Products.Count >= 4, $"Category {slug} has less than 4 products.");

            foreach (var prod in subPage.Products)
            {
                Assert.False(string.IsNullOrWhiteSpace(prod.Name));
                Assert.False(string.IsNullOrWhiteSpace(prod.SubcategoryTag), $"Product '{prod.Name}' in {slug} missing SubcategoryTag");
                Assert.False(string.IsNullOrWhiteSpace(prod.Description), $"Product '{prod.Name}' in {slug} missing Description");
                Assert.True(prod.Price > 0);
                Assert.True(prod.IsUploadingSoon);
            }
        }
    }
}
