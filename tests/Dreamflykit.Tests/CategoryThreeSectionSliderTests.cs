using Dreamflykit.Application.CategoryShowcase.Interfaces;
using Dreamflykit.Application.CategoryShowcase.Models;
using Dreamflykit.Infrastructure.CategoryShowcase;
using Dreamflykit.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Dreamflykit.Tests;

public class CategoryThreeSectionSliderTests
{
    [Fact]
    public void CreateDefault_InitializesKeyCategoriesWithAtLeast6Slides()
    {
        // Act
        var hub = CategoryThreeSectionHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.Categories);
        Assert.True(hub.Categories.ContainsKey("electronics"));
        Assert.True(hub.Categories.ContainsKey("appliances"));
        Assert.True(hub.Categories.ContainsKey("fashion"));
        Assert.True(hub.Categories.ContainsKey("study-essentials"));
        Assert.True(hub.Categories.ContainsKey("travel-gear"));
        Assert.True(hub.Categories.ContainsKey("mobiles"));

        var elec = hub.Categories["electronics"];
        Assert.True(elec.IsEnabled);
        Assert.True(elec.AutoPlay);
        Assert.True(elec.Slides.Count >= 6);

        // Verify slide properties
        var firstSlide = elec.Slides[0];
        Assert.False(string.IsNullOrWhiteSpace(firstSlide.Title));
        Assert.False(string.IsNullOrWhiteSpace(firstSlide.BadgeText));
        Assert.False(string.IsNullOrWhiteSpace(firstSlide.PriceText));
        Assert.False(string.IsNullOrWhiteSpace(firstSlide.EmiBadgeText));
        Assert.False(string.IsNullOrWhiteSpace(firstSlide.ThemeGlowColor));
        Assert.True(firstSlide.IsUploadingSoon);
    }

    [Fact]
    public void GetConfigForCategory_ReturnsFallbackForUnknownSlug()
    {
        // Arrange
        var hub = new CategoryThreeSectionHubDto();

        // Act
        var config = hub.GetConfigForCategory("custom-category");

        // Assert
        Assert.NotNull(config);
        Assert.Equal("custom-category", config.CategorySlug);
        Assert.True(config.IsEnabled);
        Assert.True(config.Slides.Count >= 6);
    }

    [Fact]
    public async Task Service_GetCategorySliderAsync_ReturnsConfig()
    {
        // Arrange
        var cache = new MemoryCache(new MemoryCacheOptions());
        var envMock = new Mock<IWebHostEnvironment>();
        var tempDir = Path.Combine(Path.GetTempPath(), "DreamflykitTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        envMock.Setup(e => e.WebRootPath).Returns(tempDir);

        var loggerMock = new Mock<ILogger<CategoryThreeSectionSliderService>>();
        var service = new CategoryThreeSectionSliderService(cache, envMock.Object, loggerMock.Object);

        try
        {
            // Act
            var config = await service.GetCategorySliderAsync("electronics");

            // Assert
            Assert.NotNull(config);
            Assert.Equal("electronics", config.CategorySlug);
            Assert.True(config.Slides.Count >= 6);
        }
        finally
        {
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Fact]
    public async Task Service_UpdateCategorySliderAsync_PersistsNewSlide()
    {
        // Arrange
        var cache = new MemoryCache(new MemoryCacheOptions());
        var envMock = new Mock<IWebHostEnvironment>();
        var tempDir = Path.Combine(Path.GetTempPath(), "DreamflykitTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        envMock.Setup(e => e.WebRootPath).Returns(tempDir);

        var loggerMock = new Mock<ILogger<CategoryThreeSectionSliderService>>();
        var service = new CategoryThreeSectionSliderService(cache, envMock.Object, loggerMock.Object);

        try
        {
            var config = await service.GetCategorySliderAsync("fashion");
            config.Slides.Add(new CategorySliderSlideDto
            {
                Id = "custom_slide_1",
                CategorySlug = "fashion",
                Title = "Custom Cashmere Overcoat",
                PriceText = "From ₹7,999*",
                IsUploadingSoon = true
            });

            // Act
            var updated = await service.UpdateCategorySliderAsync(config);
            var reloaded = await service.GetCategorySliderAsync("fashion");

            // Assert
            Assert.True(updated);
            Assert.Contains(reloaded.Slides, s => s.Title == "Custom Cashmere Overcoat");
        }
        finally
        {
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Fact]
    public async Task Controller_Index_ReturnsViewWithActiveConfig()
    {
        // Arrange
        var serviceMock = new Mock<ICategoryThreeSectionSliderService>();
        var defaultHub = CategoryThreeSectionHubDto.CreateDefault();
        serviceMock.Setup(s => s.GetAllSlidersAsync(It.IsAny<CancellationToken>()))
                   .ReturnsAsync(defaultHub);

        var envMock = new Mock<IWebHostEnvironment>();
        var loggerMock = new Mock<ILogger<CategorySlidersController>>();

        var controller = new CategorySlidersController(serviceMock.Object, envMock.Object, loggerMock.Object);

        // Act
        var result = await controller.Index("electronics");

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<CategoryThreeSectionConfigDto>(viewResult.Model);
        Assert.Equal("electronics", model.CategorySlug);
        Assert.Equal("electronics", controller.ViewBag.ActiveSlug);
    }

    [Fact]
    public async Task Controller_Save_ValidConfig_ReturnsJsonSuccess()
    {
        // Arrange
        var serviceMock = new Mock<ICategoryThreeSectionSliderService>();
        serviceMock.Setup(s => s.UpdateCategorySliderAsync(It.IsAny<CategoryThreeSectionConfigDto>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(true);

        var envMock = new Mock<IWebHostEnvironment>();
        var loggerMock = new Mock<ILogger<CategorySlidersController>>();

        var controller = new CategorySlidersController(serviceMock.Object, envMock.Object, loggerMock.Object);

        var payload = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "appliances",
            CategoryName = "Appliances",
            IsEnabled = true
        };

        // Act
        var result = await controller.Save(payload);

        // Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        dynamic val = jsonResult.Value!;
        Assert.True((bool)val.GetType().GetProperty("success")!.GetValue(val)!);
    }
}

