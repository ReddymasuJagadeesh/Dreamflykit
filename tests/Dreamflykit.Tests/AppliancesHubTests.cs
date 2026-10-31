using Dreamflykit.Application.AppliancesHub.Interfaces;
using Dreamflykit.Application.AppliancesHub.Models;
using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.Categories.Models;
using Dreamflykit.Web.Controllers;
using Dreamflykit.Web.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Dreamflykit.Tests;

public class AppliancesHubTests
{
    [Fact]
    public void AppliancesHubDto_CreateDefault_InitializesSixteenSubcategoryPagesWithHeroAndBrands()
    {
        // Act
        var dto = AppliancesHubDto.CreateDefault();

        // Assert
        Assert.NotNull(dto);
        Assert.NotNull(dto.SubcategoryPages);
        Assert.Equal(16, dto.SubcategoryPages.Count);

        // Verify key subcategories exist
        var slugs = dto.SubcategoryPages.Select(p => p.CategorySlug).ToList();
        Assert.Contains("television", slugs);
        Assert.Contains("laundry", slugs);
        Assert.Contains("air-conditioners", slugs);
        Assert.Contains("fans", slugs);
        Assert.Contains("microwaves", slugs);
        Assert.Contains("value-365", slugs);
        Assert.Contains("small-appliances", slugs);
        Assert.Contains("geysers", slugs);
        Assert.Contains("stabilizers", slugs);
        Assert.Contains("kitchen-appliances", slugs);
        Assert.Contains("refrigerators", slugs);
        Assert.Contains("home-appliances", slugs);
        Assert.Contains("breakfast-appliances", slugs);
        Assert.Contains("top-rated", slugs);
        Assert.Contains("inverters", slugs);
        Assert.Contains("dishwashers", slugs);

        // Verify Television subcategory has brands and hero banner
        var tvPage = dto.SubcategoryPages.FirstOrDefault(p => p.CategorySlug == "television");
        Assert.NotNull(tvPage);
        Assert.Equal("Television", tvPage.CategoryName);
        Assert.NotNull(tvPage.HeroBanner);
        Assert.True(tvPage.HeroBanner.IsUploadingSoon);
        Assert.NotNull(tvPage.ShopByBrands);
        Assert.NotEmpty(tvPage.ShopByBrands.Brands);

        var tvBrandNames = tvPage.ShopByBrands.Brands.Select(b => b.Name).ToList();
        Assert.Contains("Samsung", tvBrandNames);
        Assert.Contains("LG", tvBrandNames);
        Assert.Contains("Sony", tvBrandNames);
        Assert.Contains("TCL", tvBrandNames);

        // Verify all brand logos have uploading soon
        foreach (var brand in tvPage.ShopByBrands.Brands)
        {
            Assert.True(brand.IsUploadingSoon);
            Assert.Contains("uploading-soon", brand.LogoUrl);
        }

        // Verify ACs subcategory brands
        var acPage = dto.SubcategoryPages.FirstOrDefault(p => p.CategorySlug == "air-conditioners");
        Assert.NotNull(acPage);
        var acBrandNames = acPage.ShopByBrands.Brands.Select(b => b.Name).ToList();
        Assert.Contains("Daikin", acBrandNames);
        Assert.Contains("Voltas", acBrandNames);
        Assert.Contains("Blue Star", acBrandNames);
    }

    [Fact]
    public async Task ProductsController_WhenAppliancesSubcategoryRequested_ResolvesSubcategoryAndSetsSubcategoryHubFlag()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();
        var categoryServiceMock = new Mock<ICategoryService>();
        var appliancesHubServiceMock = new Mock<IAppliancesHubService>();

        var defaultAppliancesDto = AppliancesHubDto.CreateDefault();
        appliancesHubServiceMock.Setup(s => s.GetConfigAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(defaultAppliancesDto);

        var searchResult = new ProductSearchResult
        {
            Items = new List<ProductListItem>(),
            Page = 1,
            PageSize = 24,
            TotalCount = 0
        };

        productServiceMock.Setup(p => p.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(searchResult);

        var appliancesCat = new CategoryDetails
        {
            CategoryId = Guid.NewGuid(),
            Name = "Appliances",
            Slug = "appliances",
            Subcategories = new List<SubcategoryListItem>()
        };

        categoryServiceMock.Setup(c => c.GetCategoryDetailsAsync("appliances", It.IsAny<CancellationToken>()))
            .ReturnsAsync(appliancesCat);

        var controller = new ProductsController(
            productServiceMock.Object,
            categoryServiceMock.Object,
            new Mock<Dreamflykit.Application.StudyHub.Interfaces.IStudyHubService>().Object,
            new Mock<Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService>().Object,
            new Mock<Dreamflykit.Application.HealthHub.Interfaces.IHealthHubService>().Object,
            new Mock<Dreamflykit.Application.MobilesHub.Interfaces.IMobilesHubService>().Object,
            appliancesHubServiceMock.Object,
            new Mock<Dreamflykit.Application.BeautyHub.Interfaces.IBeautyHubService>().Object,
            new Mock<Dreamflykit.Application.FoodHub.Interfaces.IFoodHubService>().Object,
            new Mock<Dreamflykit.Application.TravelHub.Interfaces.ITravelHubService>().Object,
            new Mock<Dreamflykit.Application.LuggageHub.Interfaces.ILuggageHubService>().Object,
            new Mock<Dreamflykit.Application.ElectronicsHub.Interfaces.IElectronicsHubService>().Object
        );

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act - Request Television subcategory
        var actionResult = await controller.Index(
            category: "appliances",
            subcategorySlug: "television",
            cancellationToken: CancellationToken.None);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(actionResult);
        var model = Assert.IsType<ProductListViewModel>(viewResult.Model);

        Assert.True(model.IsAppliancesHub);
        Assert.True(model.IsAppliancesSubcategoryHub);
        Assert.Equal("television", model.CurrentSubcategorySlug);
        Assert.Equal("Television", model.CurrentSubcategoryName);
        Assert.NotNull(model.AppliancesHub);
        Assert.Equal(16, model.AppliancesHub.SubcategoryPages.Count);
    }

    [Fact]
    public void AppliancesBrandCatalogService_GetBrandShowcase_SamsungTelevision_ReturnsHeroAndFiltersAndProducts()
    {
        // Act
        var showcase = Dreamflykit.Application.AppliancesHub.Services.AppliancesBrandCatalogService.GetBrandShowcase(
            "television", "samsung");

        // Assert
        Assert.NotNull(showcase);
        Assert.Equal("Samsung", showcase.BrandName);
        Assert.Equal("samsung", showcase.BrandSlug);
        Assert.Equal("television", showcase.SubcategorySlug);

        // Hero banner check
        Assert.NotNull(showcase.HeroBanner);
        Assert.True(showcase.HeroBanner.IsEnabled);
        Assert.True(showcase.HeroBanner.IsUploadingSoon);
        Assert.Contains("Samsung", showcase.HeroBanner.Title);

        // Faceted filters check
        Assert.NotNull(showcase.FilterGroups);
        Assert.NotEmpty(showcase.FilterGroups);
        var filterKeys = showcase.FilterGroups.Select(f => f.Key).ToList();
        Assert.Contains("screensize", filterKeys);
        Assert.Contains("displaytech", filterKeys);
        Assert.Contains("refreshrate", filterKeys);
        Assert.Contains("energystar", filterKeys);

        // Products check
        Assert.NotNull(showcase.Products);
        Assert.NotEmpty(showcase.Products);
        Assert.All(showcase.Products, p =>
        {
            Assert.Contains("Samsung", p.Name);
            Assert.True(p.Price > 0);
            Assert.True(p.IsUploadingSoon);
        });
    }

    [Fact]
    public void AppliancesBrandCatalogService_GetBrandShowcase_LgLaundry_ReturnsLaundryFiltersAndProducts()
    {
        // Act
        var showcase = Dreamflykit.Application.AppliancesHub.Services.AppliancesBrandCatalogService.GetBrandShowcase(
            "laundry", "lg");

        // Assert
        Assert.NotNull(showcase);
        Assert.Equal("LG", showcase.BrandName);
        Assert.Equal("lg", showcase.BrandSlug);

        // Laundry-specific faceted filters check
        Assert.NotNull(showcase.FilterGroups);
        var filterKeys = showcase.FilterGroups.Select(f => f.Key).ToList();
        Assert.Contains("type", filterKeys);
        Assert.Contains("capacity", filterKeys);
        Assert.Contains("energystar", filterKeys);
        Assert.Contains("features", filterKeys);

        // LG products check
        Assert.NotEmpty(showcase.Products);
        Assert.All(showcase.Products, p => Assert.Contains("LG", p.Name));
    }

    [Fact]
    public async Task ProductsController_AppliancesBrandRoute_SetsBrandHubViewModel()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();
        var categoryServiceMock = new Mock<ICategoryService>();
        var appliancesHubServiceMock = new Mock<IAppliancesHubService>();

        var defaultAppliancesDto = AppliancesHubDto.CreateDefault();
        appliancesHubServiceMock.Setup(s => s.GetConfigAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(defaultAppliancesDto);

        productServiceMock.Setup(p => p.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductSearchResult
            {
                Items = new List<ProductListItem>(),
                Page = 1,
                PageSize = 24,
                TotalCount = 0
            });

        categoryServiceMock.Setup(c => c.GetCategoryDetailsAsync("appliances", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CategoryDetails
            {
                CategoryId = Guid.NewGuid(),
                Name = "Appliances",
                Slug = "appliances",
                Subcategories = new List<SubcategoryListItem>()
            });

        var controller = new ProductsController(
            productServiceMock.Object,
            categoryServiceMock.Object,
            new Mock<Dreamflykit.Application.StudyHub.Interfaces.IStudyHubService>().Object,
            new Mock<Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService>().Object,
            new Mock<Dreamflykit.Application.HealthHub.Interfaces.IHealthHubService>().Object,
            new Mock<Dreamflykit.Application.MobilesHub.Interfaces.IMobilesHubService>().Object,
            appliancesHubServiceMock.Object,
            new Mock<Dreamflykit.Application.BeautyHub.Interfaces.IBeautyHubService>().Object,
            new Mock<Dreamflykit.Application.FoodHub.Interfaces.IFoodHubService>().Object,
            new Mock<Dreamflykit.Application.TravelHub.Interfaces.ITravelHubService>().Object,
            new Mock<Dreamflykit.Application.LuggageHub.Interfaces.ILuggageHubService>().Object,
            new Mock<Dreamflykit.Application.ElectronicsHub.Interfaces.IElectronicsHubService>().Object
        );

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act - Request /category/appliances/television/samsung
        var actionResult = await controller.Index(
            category: "appliances",
            subcategorySlug: "television",
            brandSlug: "samsung",
            cancellationToken: CancellationToken.None);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(actionResult);
        var model = Assert.IsType<ProductListViewModel>(viewResult.Model);

        Assert.True(model.IsAppliancesHub);
        Assert.True(model.IsAppliancesBrandHub);
        Assert.False(model.IsAppliancesSubcategoryHub);
        Assert.Equal("samsung", model.CurrentAppliancesBrandSlug);
        Assert.Equal("Samsung", model.CurrentAppliancesBrandName);
        Assert.Equal("television", model.CurrentSubcategorySlug);
        Assert.Equal("Television", model.CurrentSubcategoryName);
        Assert.NotNull(model.AppliancesBrandHero);
        Assert.NotEmpty(model.AppliancesBrandFilters!);
        Assert.NotEmpty(model.AppliancesBrandProducts!);
    }
}
