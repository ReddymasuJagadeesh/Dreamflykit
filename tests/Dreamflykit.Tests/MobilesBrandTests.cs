using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.MobilesHub.Models;
using Dreamflykit.Application.MobilesHub.Services;
using Dreamflykit.Infrastructure.MobilesHub;
using Dreamflykit.Web.Models.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Dreamflykit.Tests;

public class MobilesBrandTests
{
    private static readonly string[] Expected18Brands = new[]
    {
        "apple", "motorola", "realme", "poco", "google-pixel", "hmd",
        "boltt", "mivi", "tecno", "samsung", "vivo", "oppo",
        "ai-plus", "nothing", "infinix", "lava", "redmi", "oneplus"
    };

    [Fact]
    public void MobilesHubDto_CreateDefault_ContainsAll18PopularBrandsWithResearchedFilters()
    {
        // Act
        var dto = MobilesHubDto.CreateDefault();

        // Assert
        Assert.NotNull(dto);
        Assert.NotNull(dto.BrandsSection);
        Assert.Equal(18, dto.BrandsSection.Brands.Count);

        foreach (var brand in dto.BrandsSection.Brands)
        {
            Assert.Contains(brand.Slug, Expected18Brands);
            Assert.StartsWith("/category/mobiles/", brand.ProductUrl);
        }

        Assert.NotNull(dto.BrandPages);
        Assert.Equal(18, dto.BrandPages.Count);

        foreach (var expectedSlug in Expected18Brands)
        {
            var page = dto.BrandPages.FirstOrDefault(p => p.BrandSlug == expectedSlug);
            Assert.NotNull(page);
            Assert.NotNull(page.HeroBanner);
            Assert.True(page.HeroBanner.IsUploadingSoon);
            Assert.False(string.IsNullOrWhiteSpace(page.HeroBanner.Title));
            Assert.False(string.IsNullOrWhiteSpace(page.HeroBanner.ThemeBgGradient));
            Assert.False(string.IsNullOrWhiteSpace(page.ThemePrimaryColor));

            // Every brand has multiple researched filter groups + More Filters
            Assert.NotEmpty(page.FilterGroups);
            Assert.True(page.FilterGroups.Count >= 3, $"Brand {expectedSlug} should have at least 3 filter groups.");
            Assert.Contains(page.FilterGroups, g => g.IsMoreFilter);

            // Every brand has multiple smartphones with specs and filter tags matching filters
            Assert.NotEmpty(page.Products);
            Assert.All(page.Products, p =>
            {
                Assert.True(p.Price > 0);
                Assert.True(p.IsUploadingSoon);
                Assert.NotEmpty(p.KeySpecs);
                Assert.NotEmpty(p.FilterTags);
            });
        }
    }

    [Theory]
    [InlineData("apple", "iPhone", "Apple Intelligence")]
    [InlineData("samsung", "Samsung", "Galaxy AI")]
    [InlineData("google-pixel", "Google", "Tensor")]
    [InlineData("oneplus", "OnePlus", "SuperVOOC")]
    [InlineData("nothing", "Nothing", "Glyph")]
    [InlineData("motorola", "motorola", "Hello UI")]
    [InlineData("poco", "POCO", "Esports")]
    [InlineData("lava", "LAVA", "Android")]
    [InlineData("hmd", "HMD", "Repair")]
    [InlineData("boltt", "boltt", "Wristphone")]
    [InlineData("mivi", "MIVI", "TWS")]
    [InlineData("ai-plus", "Ai+", "NPU")]
    public void MobileBrandCatalogService_ReturnsUniqueResearchedFiltersPerBrand(string slug, string expectedBrandName, string expectedKeyword)
    {
        // Act
        var showcase = MobileBrandCatalogService.GetBrandShowcase(slug);

        // Assert
        Assert.NotNull(showcase);
        Assert.Equal(slug, showcase.BrandSlug);
        Assert.Equal(expectedBrandName, showcase.BrandName);
        Assert.NotNull(showcase.Page.HeroBanner);

        var allText = showcase.Page.HeroBanner.Title + " " +
                      showcase.Page.HeroBanner.TitleHighlight + " " +
                      showcase.Page.HeroBanner.Subtitle + " " +
                      string.Join(" ", showcase.Page.FilterGroups.SelectMany(g => g.Options.Select(o => o.Label)));

        Assert.Contains(expectedKeyword, allText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task MobilesHubService_UpgradesLegacyBrandUrls_AndPopulatesBrandPages()
    {
        // Arrange: Simulate legacy payload without BrandPages and with ?q= URLs
        var legacyDto = MobilesHubDto.CreateDefault();
        legacyDto.BrandPages = new List<MobileBrandPageDto>();
        legacyDto.BrandsSection.Brands[0].ProductUrl = "/products?category=mobiles&q=iPhone";

        var mockRepo = new Mock<ICategoryHubRepository>();
        mockRepo.Setup(r => r.GetPayloadAsync<MobilesHubDto>("mobiles", "mobiles_hub.json", It.IsAny<CancellationToken>()))
            .ReturnsAsync(legacyDto);

        var logger = new Mock<ILogger<MobilesHubService>>();
        var service = new MobilesHubService(mockRepo.Object, logger.Object);

        // Act
        var loaded = await service.GetConfigAsync();

        // Assert
        Assert.Equal("/category/mobiles/apple", loaded.BrandsSection.Brands[0].ProductUrl);
        Assert.Equal(18, loaded.BrandPages.Count);
    }

    [Fact]
    public async Task ProductsController_MobilesBrandRoute_ResolvesBrandShowcaseViewMode()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), "MobilesBrandTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        var wwwrootDir = Path.Combine(tempDir, "wwwroot");
        Directory.CreateDirectory(wwwrootDir);

        try
        {
            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns(wwwrootDir);
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mobilesService = new MobilesHubService(cache, envMock.Object, new Mock<ILogger<MobilesHubService>>().Object);

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
                mobilesHubService: mobilesService,
                Mock.Of<Dreamflykit.Application.AppliancesHub.Interfaces.IAppliancesHubService>(),
                Mock.Of<Dreamflykit.Application.BeautyHub.Interfaces.IBeautyHubService>(),
                Mock.Of<Dreamflykit.Application.FoodHub.Interfaces.IFoodHubService>(),
                Mock.Of<Dreamflykit.Application.TravelHub.Interfaces.ITravelHubService>(),
                Mock.Of<Dreamflykit.Application.LuggageHub.Interfaces.ILuggageHubService>(),
                Mock.Of<Dreamflykit.Application.ElectronicsHub.Interfaces.IElectronicsHubService>(),
                storyBooksHubService: null,
                gamingHubService: null,
                sportsHubService: null
            );

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act 1: Main Mobiles Hub
            var resHub = await controller.Index(category: "mobiles", cancellationToken: default) as ViewResult;
            Assert.NotNull(resHub);
            var modelHub = resHub.Model as ProductListViewModel;
            Assert.NotNull(modelHub);
            Assert.True(modelHub.IsMobilesHub);
            Assert.False(modelHub.IsMobilesBrandHub);
            Assert.Equal("hub", modelHub.ViewMode);

            // Act 2: Clicking iPhone -> /category/mobiles/apple
            var resBrand = await controller.Index(category: "mobiles", subcategorySlug: "apple", cancellationToken: default) as ViewResult;
            Assert.NotNull(resBrand);
            var modelBrand = resBrand.Model as ProductListViewModel;
            Assert.NotNull(modelBrand);
            Assert.True(modelBrand.IsMobilesHub);
            Assert.True(modelBrand.IsMobilesBrandHub);
            Assert.Equal("brand", modelBrand.ViewMode);
            Assert.Equal("apple", modelBrand.CurrentBrandSlug);
            Assert.Equal("iPhone", modelBrand.CurrentBrandName);
            Assert.NotNull(modelBrand.MobilesBrandShowcase);
            Assert.NotEmpty(modelBrand.MobilesBrandShowcase.Page.Products);
            Assert.NotEmpty(modelBrand.MobilesBrandShowcase.Page.FilterGroups);
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

