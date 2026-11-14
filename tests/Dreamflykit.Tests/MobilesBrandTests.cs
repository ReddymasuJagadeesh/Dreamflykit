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
    private static readonly string[] Expected11Brands = new[]
    {
        "apple", "motorola", "realme", "poco", "google-pixel",
        "samsung", "vivo", "oppo", "nothing", "redmi", "oneplus"
    };

    private static readonly string[] Removed7Brands = new[]
    {
        "hmd", "boltt", "mivi", "tecno", "lava", "infinix", "ai-plus"
    };

    [Fact]
    public void MobilesHubDto_CreateDefault_ContainsAll11PopularBrandsWithResearchedFilters()
    {
        // Act
        var dto = MobilesHubDto.CreateDefault();

        // Assert
        Assert.NotNull(dto);
        Assert.NotNull(dto.BrandsSection);
        Assert.Equal(11, dto.BrandsSection.Brands.Count);

        foreach (var brand in dto.BrandsSection.Brands)
        {
            Assert.Contains(brand.Slug, Expected11Brands);
            Assert.DoesNotContain(brand.Slug, Removed7Brands);
            Assert.StartsWith("/category/mobiles/", brand.ProductUrl);
        }

        Assert.NotNull(dto.BrandPages);
        Assert.Equal(11, dto.BrandPages.Count);

        foreach (var expectedSlug in Expected11Brands)
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

    [Fact]
    public void MobilesHubJsonFile_ContainsOnly11ActiveBrands_AndZeroRemovedBrands()
    {
        var slnRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../.."));
        var targetFile = Path.Combine(slnRoot, "src", "Dreamflykit.Web", "wwwroot", "data", "mobiles_hub.json");
        if (File.Exists(targetFile))
        {
            var json = File.ReadAllText(targetFile);
            var parsed = System.Text.Json.JsonSerializer.Deserialize<MobilesHubDto>(json, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.NotNull(parsed);
            Assert.Equal(11, parsed.BrandsSection.Brands.Count);
            foreach (var b in parsed.BrandsSection.Brands)
            {
                Assert.Contains(b.Slug, Expected11Brands);
                Assert.DoesNotContain(b.Slug, Removed7Brands);
            }
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
        Assert.Equal(11, loaded.BrandPages.Count);
    }

    [Fact]
    public async Task MobilesHubService_WhenAdminRemovesBrands_DoesNotResurrectDeletedBrands()
    {
        // Arrange: Admin deliberately removed brands, leaving only Apple and Samsung
        var adminSavedDto = MobilesHubDto.CreateDefault();
        adminSavedDto.BrandsSection.Brands = new List<MobileBrandItemDto>
        {
            new() { Id = "brand-1", Name = "iPhone", Slug = "apple", ProductUrl = "/category/mobiles/apple", DisplayOrder = 1 },
            new() { Id = "brand-2", Name = "Samsung", Slug = "samsung", ProductUrl = "/category/mobiles/samsung", DisplayOrder = 2 }
        };
        adminSavedDto.BrandPages = new List<MobileBrandPageDto>
        {
            new() { BrandSlug = "apple", BrandName = "iPhone" },
            new() { BrandSlug = "samsung", BrandName = "Samsung" }
        };

        var mockRepo = new Mock<ICategoryHubRepository>();
        mockRepo.Setup(r => r.GetPayloadAsync<MobilesHubDto>("mobiles", "mobiles_hub.json", It.IsAny<CancellationToken>()))
            .ReturnsAsync(adminSavedDto);

        var logger = new Mock<ILogger<MobilesHubService>>();
        var service = new MobilesHubService(mockRepo.Object, logger.Object);

        // Act
        var loaded = await service.GetConfigAsync();

        // Assert: Admin's deletions must be strictly respected!
        Assert.Equal(2, loaded.BrandsSection.Brands.Count);
        Assert.Equal("apple", loaded.BrandsSection.Brands[0].Slug);
        Assert.Equal("samsung", loaded.BrandsSection.Brands[1].Slug);

        // BrandPages should also only contain the 2 active brands
        Assert.Equal(2, loaded.BrandPages.Count);
        Assert.Contains(loaded.BrandPages, p => p.BrandSlug == "apple");
        Assert.Contains(loaded.BrandPages, p => p.BrandSlug == "samsung");
    }

    [Fact]
    public async Task MobilesHubService_PurgesRequested7Brands_WhenFoundInLegacyStorage()
    {
        // Arrange: Legacy payload with old 18 brands (including HMD, boltt, MIVI, Tecno, Ai+, Infinix, LAVA)
        var legacyDto = MobilesHubDto.CreateDefault();
        legacyDto.BrandsSection.Brands.Add(new MobileBrandItemDto { Id = "b-hmd", Name = "HMD", Slug = "hmd" });
        legacyDto.BrandsSection.Brands.Add(new MobileBrandItemDto { Id = "b-bolt", Name = "boltt", Slug = "boltt" });
        legacyDto.BrandsSection.Brands.Add(new MobileBrandItemDto { Id = "b-mivi", Name = "MIVI", Slug = "mivi" });
        legacyDto.BrandsSection.Brands.Add(new MobileBrandItemDto { Id = "b-tecno", Name = "Tecno", Slug = "tecno" });
        legacyDto.BrandsSection.Brands.Add(new MobileBrandItemDto { Id = "b-ai", Name = "Ai+", Slug = "ai-plus" });
        legacyDto.BrandsSection.Brands.Add(new MobileBrandItemDto { Id = "b-inf", Name = "Infinix", Slug = "infinix" });
        legacyDto.BrandsSection.Brands.Add(new MobileBrandItemDto { Id = "b-lava", Name = "LAVA", Slug = "lava" });

        var mockRepo = new Mock<ICategoryHubRepository>();
        mockRepo.Setup(r => r.GetPayloadAsync<MobilesHubDto>("mobiles", "mobiles_hub.json", It.IsAny<CancellationToken>()))
            .ReturnsAsync(legacyDto);

        var logger = new Mock<ILogger<MobilesHubService>>();
        var service = new MobilesHubService(mockRepo.Object, logger.Object);

        // Act
        var loaded = await service.GetConfigAsync();

        // Assert: All 7 requested brands must be purged!
        Assert.Equal(11, loaded.BrandsSection.Brands.Count);
        foreach (var purgedSlug in Removed7Brands)
        {
            Assert.DoesNotContain(loaded.BrandsSection.Brands, b => string.Equals(b.Slug, purgedSlug, StringComparison.OrdinalIgnoreCase));
            Assert.DoesNotContain(loaded.BrandPages, p => string.Equals(p.BrandSlug, purgedSlug, StringComparison.OrdinalIgnoreCase));
        }

        mockRepo.Verify(r => r.SavePayloadAsync("mobiles", "Mobiles Hub", "mobiles_hub.json", It.IsAny<MobilesHubDto>(), It.IsAny<CancellationToken>()), Times.Once);
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

