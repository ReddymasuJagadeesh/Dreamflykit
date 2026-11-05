import os

code = """using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.Categories.Models;
using Dreamflykit.Application.ElectronicsHub.Interfaces;
using Dreamflykit.Application.ElectronicsHub.Models;
using Dreamflykit.Web.Controllers;
using Dreamflykit.Web.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Dreamflykit.Tests;

public class ElectronicsHubTests
{
    [Fact]
    public void ElectronicsHubDto_CreateDefault_InitializesTenCategoryCardsWithUploadingSoon()
    {
        // Act
        var dto = ElectronicsHubDto.CreateDefault();

        // Assert
        Assert.NotNull(dto);
        Assert.NotNull(dto.CategoriesSection);
        Assert.Equal("Shop by Category", dto.CategoriesSection.Title);
        Assert.Equal(10, dto.CategoriesSection.Categories.Count);

        // Verify each card has default settings and uploading soon
        foreach (var cat in dto.CategoriesSection.Categories)
        {
            Assert.False(string.IsNullOrWhiteSpace(cat.Name));
            Assert.False(string.IsNullOrWhiteSpace(cat.Slug));
            Assert.True(cat.IsUploadingSoon, $"Category {cat.Name} should have IsUploadingSoon = true");
            Assert.True(cat.IsActive, $"Category {cat.Name} should be active");
            Assert.Contains("uploading-soon", cat.ImageUrl);
        }

        // Verify all 10 exact categories from user specification exist in correct order
        var slugs = dto.CategoriesSection.Categories.Select(c => c.Slug).ToList();
        Assert.Equal("laptops", slugs[0]);
        Assert.Equal("earphones", slugs[1]);
        Assert.Equal("grooming", slugs[2]);
        Assert.Equal("mobile-cases", slugs[3]);
        Assert.Equal("storage-devices", slugs[4]);
        Assert.Equal("chargers-cables", slugs[5]);
        Assert.Equal("power-banks", slugs[6]);
        Assert.Equal("watches", slugs[7]);
        Assert.Equal("computer-accessories", slugs[8]);
        Assert.Equal("speakers", slugs[9]);
    }

    [Fact]
    public void ElectronicsHubDto_CreateDefault_InitializesTenSubcategoryPagesWithHeroAndBrands()
    {
        // Act
        var dto = ElectronicsHubDto.CreateDefault();

        // Assert
        Assert.NotNull(dto.SubcategoryPages);
        Assert.Equal(10, dto.SubcategoryPages.Count);

        // Check Laptops subcategory
        var laptopPage = dto.SubcategoryPages.FirstOrDefault(p => p.CategorySlug == "laptops");
        Assert.NotNull(laptopPage);
        Assert.Equal("Laptops", laptopPage.CategoryName);
        Assert.NotNull(laptopPage.HeroBanner);
        Assert.True(laptopPage.HeroBanner.IsUploadingSoon);
        Assert.Equal(400, laptopPage.HeroBanner.BannerHeight);
        Assert.NotNull(laptopPage.ShopByBrands);
        Assert.True(laptopPage.ShopByBrands.IsEnabled);
        Assert.NotEmpty(laptopPage.ShopByBrands.Brands);
        Assert.NotEmpty(laptopPage.CuratedProducts);

        var brandNames = laptopPage.ShopByBrands.Brands.Select(b => b.Name).ToList();
        Assert.Contains("Apple", brandNames);
        Assert.Contains("Dell", brandNames);
        Assert.Contains("HP", brandNames);
        Assert.Contains("Lenovo", brandNames);

        // Check all brands have uploading soon placeholder
        foreach (var brand in laptopPage.ShopByBrands.Brands)
        {
            Assert.True(brand.IsUploadingSoon);
            Assert.Contains("uploading-soon", brand.LogoUrl);
        }

        // Check Earphones subcategory
        var earphonePage = dto.SubcategoryPages.FirstOrDefault(p => p.CategorySlug == "earphones");
        Assert.NotNull(earphonePage);
        Assert.Contains("Sony", earphonePage.ShopByBrands.Brands.Select(b => b.Name));
        Assert.Contains("Bose", earphonePage.ShopByBrands.Brands.Select(b => b.Name));

        // Check Grooming subcategory
        var groomingPage = dto.SubcategoryPages.FirstOrDefault(p => p.CategorySlug == "grooming");
        Assert.NotNull(groomingPage);
        Assert.Contains("Philips", groomingPage.ShopByBrands.Brands.Select(b => b.Name));
        Assert.Contains("Braun", groomingPage.ShopByBrands.Brands.Select(b => b.Name));

        // Check Power Banks subcategory
        var powerBankPage = dto.SubcategoryPages.FirstOrDefault(p => p.CategorySlug == "power-banks");
        Assert.NotNull(powerBankPage);
        Assert.Contains("Anker", powerBankPage.ShopByBrands.Brands.Select(b => b.Name));
        Assert.Contains("Xiaomi", powerBankPage.ShopByBrands.Brands.Select(b => b.Name));

        // Check Speakers subcategory
        var speakerPage = dto.SubcategoryPages.FirstOrDefault(p => p.CategorySlug == "speakers");
        Assert.NotNull(speakerPage);
        Assert.Contains("JBL", speakerPage.ShopByBrands.Brands.Select(b => b.Name));
        Assert.Contains("Bose", speakerPage.ShopByBrands.Brands.Select(b => b.Name));
    }

    [Fact]
    public void ElectronicsHubDto_HeroBanner_DefaultIsConfiguredForStudents()
    {
        // Act
        var dto = ElectronicsHubDto.CreateDefault();

        // Assert
        Assert.NotNull(dto.HeroBanner);
        Assert.True(dto.HeroBanner.IsEnabled);
        Assert.True(dto.HeroBanner.IsUploadingSoon);
        Assert.Equal("Student Electronics for", dto.HeroBanner.Title);
        Assert.Equal("Every Global Campus", dto.HeroBanner.TitleHighlight);
        Assert.Equal(440, dto.HeroBanner.BannerHeight);
        Assert.NotEmpty(dto.HeroBanner.Badges);
        Assert.NotNull(dto.HeroBanner.RightCard);
    }

    [Fact]
    public async Task ProductsController_WhenElectronicsCategoryRequested_ReturnsHubViewModeAndElectronicsHub()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();
        var categoryServiceMock = new Mock<ICategoryService>();
        var studyHubMock = new Mock<Dreamflykit.Application.StudyHub.Interfaces.IStudyHubService>();
        var fashionHubMock = new Mock<Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService>();
        var healthHubMock = new Mock<Dreamflykit.Application.HealthHub.Interfaces.IHealthHubService>();
        var mobilesHubMock = new Mock<Dreamflykit.Application.MobilesHub.Interfaces.IMobilesHubService>();
        var appliancesHubMock = new Mock<Dreamflykit.Application.AppliancesHub.Interfaces.IAppliancesHubService>();
        var beautyHubMock = new Mock<Dreamflykit.Application.BeautyHub.Interfaces.IBeautyHubService>();
        var foodHubMock = new Mock<Dreamflykit.Application.FoodHub.Interfaces.IFoodHubService>();
        var travelHubMock = new Mock<Dreamflykit.Application.TravelHub.Interfaces.ITravelHubService>();
        var luggageHubMock = new Mock<Dreamflykit.Application.LuggageHub.Interfaces.ILuggageHubService>();
        var electronicsHubMock = new Mock<IElectronicsHubService>();

        var defaultElecHub = ElectronicsHubDto.CreateDefault();
        electronicsHubMock.Setup(e => e.GetConfigAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(defaultElecHub);

        categoryServiceMock.Setup(c => c.GetCategoryDetailsAsync("electronics", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CategoryDetails
            {
                CategoryId = Guid.NewGuid(),
                Name = "Electronics",
                Slug = "electronics",
                Subcategories = new List<SubcategoryListItem>()
            });

        productServiceMock.Setup(p => p.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductSearchResult
            {
                Items = new List<ProductListItem>(),
                Page = 1,
                PageSize = 24,
                TotalCount = 0
            });

        var controller = new ProductsController(
            productServiceMock.Object,
            categoryServiceMock.Object,
            studyHubMock.Object,
            fashionHubMock.Object,
            healthHubMock.Object,
            mobilesHubMock.Object,
            appliancesHubMock.Object,
            beautyHubMock.Object,
            foodHubMock.Object,
            travelHubMock.Object,
            luggageHubMock.Object,
            electronicsHubMock.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await controller.Index(category: "electronics");

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ProductListViewModel>(viewResult.Model);

        Assert.Equal("hub", model.ViewMode);
        Assert.True(model.IsElectronicsCategory);
        Assert.True(model.IsElectronicsHub);
        Assert.NotNull(model.ElectronicsHub);
        Assert.Equal(10, model.ElectronicsHub.CategoriesSection.Categories.Count);
    }

    [Fact]
    public async Task ProductsController_WhenElectronicsSubcategoryRequested_ResolvesSubcategoryAndSetsSubcategoryHubFlag()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();
        var categoryServiceMock = new Mock<ICategoryService>();
        var studyHubMock = new Mock<Dreamflykit.Application.StudyHub.Interfaces.IStudyHubService>();
        var fashionHubMock = new Mock<Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService>();
        var healthHubMock = new Mock<Dreamflykit.Application.HealthHub.Interfaces.IHealthHubService>();
        var mobilesHubMock = new Mock<Dreamflykit.Application.MobilesHub.Interfaces.IMobilesHubService>();
        var appliancesHubMock = new Mock<Dreamflykit.Application.AppliancesHub.Interfaces.IAppliancesHubService>();
        var beautyHubMock = new Mock<Dreamflykit.Application.BeautyHub.Interfaces.IBeautyHubService>();
        var foodHubMock = new Mock<Dreamflykit.Application.FoodHub.Interfaces.IFoodHubService>();
        var travelHubMock = new Mock<Dreamflykit.Application.TravelHub.Interfaces.ITravelHubService>();
        var luggageHubMock = new Mock<Dreamflykit.Application.LuggageHub.Interfaces.ILuggageHubService>();
        var electronicsHubMock = new Mock<IElectronicsHubService>();

        var defaultElecHub = ElectronicsHubDto.CreateDefault();
        electronicsHubMock.Setup(e => e.GetConfigAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(defaultElecHub);

        categoryServiceMock.Setup(c => c.GetCategoryDetailsAsync("electronics", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CategoryDetails
            {
                CategoryId = Guid.NewGuid(),
                Name = "Electronics",
                Slug = "electronics",
                Subcategories = new List<SubcategoryListItem>()
            });

        productServiceMock.Setup(p => p.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductSearchResult
            {
                Items = new List<ProductListItem>(),
                Page = 1,
                PageSize = 24,
                TotalCount = 0
            });

        var controller = new ProductsController(
            productServiceMock.Object,
            categoryServiceMock.Object,
            studyHubMock.Object,
            fashionHubMock.Object,
            healthHubMock.Object,
            mobilesHubMock.Object,
            appliancesHubMock.Object,
            beautyHubMock.Object,
            foodHubMock.Object,
            travelHubMock.Object,
            luggageHubMock.Object,
            electronicsHubMock.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act - user visits with "laptops"
        var result = await controller.Index(category: "electronics", subcategorySlug: "laptops");

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ProductListViewModel>(viewResult.Model);

        Assert.True(model.IsElectronicsCategory);
        Assert.True(model.IsElectronicsSubcategoryHub);
        Assert.Equal("laptops", model.CurrentSubcategorySlug);
        Assert.Equal("Laptops", model.CurrentSubcategoryName);
        Assert.NotNull(model.ElectronicsHub);
    }

    [Fact]
    public void ElectronicsBrandCatalogService_GetBrandShowcase_AppleLaptops_ReturnsHeroAndFiltersAndProducts()
    {
        // Act
        var showcase = Dreamflykit.Application.ElectronicsHub.Services.ElectronicsBrandCatalogService.GetBrandShowcase(
            "laptops-macbooks", "apple");

        // Assert
        Assert.NotNull(showcase);
        Assert.Equal("Apple", showcase.BrandName);
        Assert.Equal("apple", showcase.BrandSlug);
        Assert.Equal("Laptops", showcase.SubcategoryName);
        Assert.NotNull(showcase.HeroBanner);
        Assert.True(showcase.HeroBanner.IsUploadingSoon);
        Assert.NotEmpty(showcase.FilterGroups);
        Assert.NotEmpty(showcase.Products);

        // Every product has uploading soon placeholder
        foreach (var p in showcase.Products)
        {
            Assert.True(p.IsUploadingSoon);
            Assert.Contains("uploading-soon", p.ImageUrl);
        }
    }

    [Fact]
    public void ElectronicsBrandCatalogService_GetBrandShowcase_SonyHeadphones_ReturnsAudioFiltersAndProducts()
    {
        // Act
        var showcase = Dreamflykit.Application.ElectronicsHub.Services.ElectronicsBrandCatalogService.GetBrandShowcase(
            "headphones-audio", "sony");

        // Assert
        Assert.NotNull(showcase);
        Assert.Equal("Sony", showcase.BrandName);
        Assert.Equal("Earphones", showcase.SubcategoryName);
        Assert.NotNull(showcase.HeroBanner);
        Assert.Contains("Campus", showcase.HeroBanner.TitleHighlight);
        Assert.NotEmpty(showcase.Products);
    }

    [Fact]
    public async Task ProductsController_ElectronicsBrandRoute_SetsBrandHubViewModel()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();
        var categoryServiceMock = new Mock<ICategoryService>();
        var studyHubMock = new Mock<Dreamflykit.Application.StudyHub.Interfaces.IStudyHubService>();
        var fashionHubMock = new Mock<Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService>();
        var healthHubMock = new Mock<Dreamflykit.Application.HealthHub.Interfaces.IHealthHubService>();
        var mobilesHubMock = new Mock<Dreamflykit.Application.MobilesHub.Interfaces.IMobilesHubService>();
        var appliancesHubMock = new Mock<Dreamflykit.Application.AppliancesHub.Interfaces.IAppliancesHubService>();
        var beautyHubMock = new Mock<Dreamflykit.Application.BeautyHub.Interfaces.IBeautyHubService>();
        var foodHubMock = new Mock<Dreamflykit.Application.FoodHub.Interfaces.IFoodHubService>();
        var travelHubMock = new Mock<Dreamflykit.Application.TravelHub.Interfaces.ITravelHubService>();
        var luggageHubMock = new Mock<Dreamflykit.Application.LuggageHub.Interfaces.ILuggageHubService>();
        var electronicsHubMock = new Mock<IElectronicsHubService>();

        var defaultElecHub = ElectronicsHubDto.CreateDefault();
        electronicsHubMock.Setup(e => e.GetConfigAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(defaultElecHub);

        categoryServiceMock.Setup(c => c.GetCategoryDetailsAsync("electronics", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CategoryDetails
            {
                CategoryId = Guid.NewGuid(),
                Name = "Electronics",
                Slug = "electronics",
                Subcategories = new List<SubcategoryListItem>()
            });

        productServiceMock.Setup(p => p.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductSearchResult
            {
                Items = new List<ProductListItem>(),
                Page = 1,
                PageSize = 24,
                TotalCount = 0
            });

        var controller = new ProductsController(
            productServiceMock.Object,
            categoryServiceMock.Object,
            studyHubMock.Object,
            fashionHubMock.Object,
            healthHubMock.Object,
            mobilesHubMock.Object,
            appliancesHubMock.Object,
            beautyHubMock.Object,
            foodHubMock.Object,
            travelHubMock.Object,
            luggageHubMock.Object,
            electronicsHubMock.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act - user visits with brandSlug parameter
        var result = await controller.Index(category: "electronics", subcategorySlug: "laptops-macbooks", brandSlug: "apple");

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ProductListViewModel>(viewResult.Model);

        Assert.True(model.IsElectronicsCategory);
        Assert.True(model.IsElectronicsBrandHub);
        Assert.NotNull(model.ElectronicsBrandHero);
        Assert.Equal("apple", model.CurrentBrandSlug);
    }
}
"""

path = r'tests/Dreamflykit.Tests/ElectronicsHubTests.cs'
with open(path, 'w', encoding='utf-8') as f:
    f.write(code)

print("Updated ElectronicsHubTests.cs successfully!")
