using Dreamflykit.Application.CountryKits.Interfaces;
using Dreamflykit.Application.CountryKits.Models;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Infrastructure.CountryKits;
using Dreamflykit.Web.Areas.Admin.Controllers;
using Dreamflykit.Web.Areas.Admin.Models;
using Dreamflykit.Web.Controllers;
using Dreamflykit.Web.Models.Kits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Dreamflykit.Tests;

public class CountryKitsHubTests
{
    private readonly Mock<IWebHostEnvironment> _envMock;
    private readonly IMemoryCache _cache;
    private readonly Mock<ILogger<CountryKitsHubService>> _loggerMock;
    private readonly string _testTempDir;

    public CountryKitsHubTests()
    {
        _testTempDir = Path.Combine(Path.GetTempPath(), "DreamflykitTests_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_testTempDir);
        var wwwrootDir = Path.Combine(_testTempDir, "wwwroot");
        Directory.CreateDirectory(wwwrootDir);

        _envMock = new Mock<IWebHostEnvironment>();
        _envMock.Setup(e => e.WebRootPath).Returns(wwwrootDir);

        _cache = new MemoryCache(new MemoryCacheOptions());
        _loggerMock = new Mock<ILogger<CountryKitsHubService>>();
    }

    [Fact]
    public void CountryKitsHubDto_CreateDefault_InitializesCountriesAndKits()
    {
        // Act
        var hub = CountryKitsHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub);
        Assert.NotNull(hub.Countries);
        Assert.True(hub.Countries.Count >= 6, "Expected at least 6 default countries");

        Assert.True(hub.Countries.ContainsKey("usa"));
        Assert.True(hub.Countries.ContainsKey("uk"));
        Assert.True(hub.Countries.ContainsKey("canada"));
        Assert.True(hub.Countries.ContainsKey("australia"));
        Assert.True(hub.Countries.ContainsKey("germany"));
        Assert.True(hub.Countries.ContainsKey("ireland"));

        var usa = hub.Countries["usa"];
        Assert.Equal("USA", usa.CountryName);
        Assert.Equal("🇺🇸", usa.FlagEmoji);
        Assert.Equal("USA Student Kits", usa.HeroTitle);
        Assert.True(usa.IsLandmarkUploadingSoon);
        Assert.NotEmpty(usa.Feature1);
        Assert.NotEmpty(usa.Feature2);
        Assert.NotEmpty(usa.Feature3);
        Assert.NotEmpty(usa.Feature4);

        Assert.NotNull(usa.Kits);
        Assert.True(usa.Kits.Count >= 6);

        // Verify kit sizes
        Assert.Contains(usa.Kits, k => k.Size.Equals("small", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(usa.Kits, k => k.Size.Equals("medium", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(usa.Kits, k => k.Size.Equals("large", StringComparison.OrdinalIgnoreCase));

        // Verify badges
        Assert.Contains(usa.Kits, k => k.Badge == "Best Seller");
        Assert.Contains(usa.Kits, k => k.Badge == "Most Popular");
        Assert.Contains(usa.Kits, k => k.Badge == "Best Value");

        // Verify prices and discounts
        foreach (var kit in usa.Kits)
        {
            Assert.True(kit.SellingPrice > 0);
            Assert.True(kit.Mrp >= kit.SellingPrice);
            Assert.True(kit.Rating >= 4.0);
            Assert.True(kit.ReviewCount > 0);
            Assert.True(kit.IsUploadingSoon);
            Assert.NotEmpty(kit.IncludedCategories);
        }
    }

    [Theory]
    [InlineData("USA", "usa")]
    [InlineData("usa", "usa")]
    [InlineData("United States", "usa")]
    [InlineData("United States of America", "usa")]
    [InlineData("UK", "uk")]
    [InlineData("United Kingdom", "uk")]
    [InlineData("Great Britain", "uk")]
    [InlineData("Canada", "canada")]
    [InlineData("Australia", "australia")]
    [InlineData("New Zealand", "new-zealand")]
    [InlineData("Germany", "germany")]
    public void NormalizeSlug_HandlesVariousCountryNames(string input, string expected)
    {
        var result = CountryKitsHubDto.NormalizeSlug(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CountryKitsHubService_GetAndSaveProfile_PersistsChanges()
    {
        // Arrange
        var service = new CountryKitsHubService(_cache, _envMock.Object, _loggerMock.Object);

        // Act - Get profile
        var profile = await service.GetCountryProfileAsync("usa");
        Assert.NotNull(profile);
        Assert.Equal("USA", profile.CountryName);

        // Update profile
        profile.HeroTitle = "Updated USA Campus Kits 2026";
        profile.StartingPrice = 5199;
        var saveResult = await service.SaveCountryProfileAsync(profile);
        Assert.True(saveResult);

        // Re-fetch
        var updated = await service.GetCountryProfileAsync("usa");
        Assert.Equal("Updated USA Campus Kits 2026", updated.HeroTitle);
        Assert.Equal(5199, updated.StartingPrice);
    }

    [Fact]
    public async Task CountryKitsHubService_ResetDefaults_RestoresDefaults()
    {
        // Arrange
        var service = new CountryKitsHubService(_cache, _envMock.Object, _loggerMock.Object);

        var profile = await service.GetCountryProfileAsync("uk");
        profile.HeroTitle = "Custom UK Title Modified";
        await service.SaveCountryProfileAsync(profile);

        // Reset
        var resetResult = await service.ResetDefaultsAsync("uk");
        Assert.True(resetResult);

        var resetProfile = await service.GetCountryProfileAsync("uk");
        Assert.Equal("UK Student Kits", resetProfile.HeroTitle);
    }

    [Fact]
    public async Task KitsController_CountryKits_ReturnsViewWithFilteredKits()
    {
        // Arrange
        var service = new CountryKitsHubService(_cache, _envMock.Object, _loggerMock.Object);
        var controller = new KitsController(service);

        // Act - Fetch USA Kits
        var result = await controller.CountryKits("usa", size: "medium", minPrice: 5000, maxPrice: 15000);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("CountryKits", viewResult.ViewName);

        var model = Assert.IsType<CountryKitsViewModel>(viewResult.Model);
        Assert.Equal("usa", model.CountrySlug);
        Assert.Equal("USA", model.CountryName);
        Assert.Equal("medium", model.SelectedSize);
        Assert.NotEmpty(model.CuratedKits);

        // Filtered kits should match size
        var filtered = model.GetFilteredKits();
        Assert.All(filtered, k => Assert.Equal("medium", k.Size, ignoreCase: true));
    }

    [Fact]
    public async Task AdminCountryKitsController_AddAndSaveCountry_Success()
    {
        // Arrange
        var service = new CountryKitsHubService(_cache, _envMock.Object, _loggerMock.Object);
        var mediaMock = new Mock<IMediaService>();
        var urlMock = new Mock<IMediaUrlService>();
        var controllerLoggerMock = new Mock<ILogger<CountryKitsController>>();

        var controller = new CountryKitsController(service, mediaMock.Object, urlMock.Object, controllerLoggerMock.Object);
        controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

        // Act - Add new country
        var newCountry = new CountryKitProfileDto
        {
            CountryName = "Singapore",
            CountrySlug = "singapore",
            FlagEmoji = "🇸🇬",
            Tagline = "Gateway to Asia-Pacific Education",
            HeroTitle = "Singapore Student Kits",
            StartingPrice = 6999
        };

        var addResult = await controller.AddCountry(newCountry, null, default);
        var jsonResult = Assert.IsType<JsonResult>(addResult);
        dynamic data = jsonResult.Value!;
        Assert.True((bool)data.success);

        // Verify country exists
        var fetched = await service.GetCountryProfileAsync("singapore");
        Assert.NotNull(fetched);
        Assert.Equal("Singapore", fetched.CountryName);
        Assert.Equal("🇸🇬", fetched.FlagEmoji);
        Assert.Equal(3, fetched.Kits.Count); // Should have created 3 starter kits
    }
}

