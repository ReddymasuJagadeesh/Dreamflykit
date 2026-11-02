using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.StoryBooksHub.Interfaces;
using Dreamflykit.Application.StoryBooksHub.Models;
using Dreamflykit.Infrastructure.StoryBooksHub;
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

public class StoryBooksHubTests
{
    [Fact]
    public void StoryBooksHubDto_CreateDefault_InitializesBillboardAndSections()
    {
        // Act
        var dto = StoryBooksHubDto.CreateDefault();

        // Assert
        Assert.NotNull(dto);
        Assert.NotNull(dto.HeroBillboard);
        Assert.True(dto.HeroBillboard.IsEnabled);
        Assert.Equal("Bon Appétit, Your Majesty", dto.HeroBillboard.Title);
        Assert.True(dto.HeroBillboard.IsUploadingSoon);
        Assert.NotEmpty(dto.HeroBillboard.Author);
        Assert.True(dto.HeroBillboard.Price > 0);
        Assert.True(dto.HeroBillboard.Rating >= 4.0);

        // Verify Genres and Journals
        Assert.NotNull(dto.GenresAndJournals);
        Assert.NotEmpty(dto.GenresAndJournals);
        var genreSlugs = dto.GenresAndJournals.Select(g => g.Slug).ToList();
        Assert.Contains("academic-journals", genreSlugs);
        Assert.Contains("campus-fiction", genreSlugs);
        Assert.Contains("mystery-thrillers", genreSlugs);

        // Verify Languages
        Assert.NotNull(dto.Languages);
        Assert.NotEmpty(dto.Languages);
        var langCodes = dto.Languages.Select(l => l.Code).ToList();
        Assert.Contains("en", langCodes);
        Assert.Contains("te", langCodes);
        Assert.Contains("hi", langCodes);
        Assert.Contains("ta", langCodes);

        // Verify Preferences
        Assert.NotNull(dto.Preferences);
        Assert.NotEmpty(dto.Preferences);
        var prefKeys = dto.Preferences.Select(p => p.Key).ToList();
        Assert.Contains("original", prefKeys);
        Assert.Contains("translated", prefKeys);

        // Verify Sections
        Assert.NotNull(dto.Sections);
        Assert.NotEmpty(dto.Sections);
        Assert.Contains(dto.Sections, s => s.DisplayType == "top10");
        Assert.Contains(dto.Sections, s => s.DisplayType == "continue-reading");
    }

    [Fact]
    public void StoryBooksHubDto_Helpers_ReturnExpectedItems()
    {
        // Arrange
        var dto = StoryBooksHubDto.CreateDefault();

        // Act
        var allBooks = dto.GetAllBooks();
        var continueReading = dto.GetContinueReadingBooks();
        var top10 = dto.GetTop10Books();

        // Assert
        Assert.NotEmpty(allBooks);
        Assert.NotEmpty(continueReading);
        Assert.NotEmpty(top10);

        Assert.All(continueReading, b => Assert.False(string.IsNullOrWhiteSpace(b.ReadingProgress)));
        Assert.All(top10, b => Assert.True(b.Top10Rank > 0));
    }

    [Fact]
    public async Task StoryBooksHubService_GetConfigAsync_ReturnsValidConfig()
    {
        // Arrange
        var envMock = new Mock<IWebHostEnvironment>();
        envMock.Setup(e => e.WebRootPath).Returns(Path.GetTempPath());

        var cache = new MemoryCache(new MemoryCacheOptions());
        var loggerMock = new Mock<ILogger<StoryBooksHubService>>();

        var service = new StoryBooksHubService(cache, envMock.Object, loggerMock.Object);

        // Act
        var config = await service.GetConfigAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(config);
        Assert.NotNull(config.HeroBillboard);
        Assert.Equal("BOOKSFLIX", config.ThemeSettings.BrandName);
    }

    [Fact]
    public async Task ProductsController_StoryBooksCategory_ActivatesHub()
    {
        // Arrange
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

        var mockStoryBooksService = new Mock<IStoryBooksHubService>();
        mockStoryBooksService.Setup(s => s.GetConfigAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(StoryBooksHubDto.CreateDefault());

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
            mockStoryBooksService.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await controller.Index(category: "story-books");

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ProductListViewModel>(viewResult.Model);

        Assert.True(model.IsStoryBooksHub);
        Assert.NotNull(model.StoryBooksHub);
        Assert.Equal("hub", model.ViewMode);
        Assert.Equal("Bon Appétit, Your Majesty", model.StoryBooksHub.HeroBillboard.Title);
    }
}
