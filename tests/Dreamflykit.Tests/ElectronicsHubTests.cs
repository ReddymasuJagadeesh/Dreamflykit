using Dreamflykit.Application.Catalog.Interfaces;
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

        // Verify key categories exist
        var slugs = dto.CategoriesSection.Categories.Select(c => c.Slug).ToList();
        Assert.Contains("laptops-macbooks", slugs);
        Assert.Contains("headphones-audio", slugs);
        Assert.Contains("travel-adapters", slugs);
        Assert.Contains("power-banks-chargers", slugs);
        Assert.Contains("student-sims", slugs);
        Assert.Contains("storage-ssds", slugs);
        Assert.Contains("smartwatches-wearables", slugs);
        Assert.Contains("cables-hubs-docks", slugs);
        Assert.Contains("monitors-displays", slugs);
        Assert.Contains("calculators-tech-tools", slugs);
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
}
