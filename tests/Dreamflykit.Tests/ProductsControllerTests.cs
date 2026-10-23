using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.Categories.Models;
using Dreamflykit.Application.StudyHub.Interfaces;
using Dreamflykit.Web.Controllers;
using Dreamflykit.Web.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Dreamflykit.Tests;

public class ProductsControllerTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly Mock<ICategoryService> _categoryServiceMock;
    private readonly Mock<IStudyHubService> _studyHubServiceMock;
    private readonly Mock<Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService> _fashionHubServiceMock;

    public ProductsControllerTests()
    {
        _productServiceMock = new Mock<IProductService>();
        _categoryServiceMock = new Mock<ICategoryService>();
        _studyHubServiceMock = new Mock<IStudyHubService>();
        _fashionHubServiceMock = new Mock<Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService>();
    }

    private ProductsController CreateController(bool isAjax = false, bool partialQuery = false)
    {
        var controller = new ProductsController(
            _productServiceMock.Object,
            _categoryServiceMock.Object,
            _studyHubServiceMock.Object,
            _fashionHubServiceMock.Object);

        var httpContext = new DefaultHttpContext();
        if (isAjax)
        {
            httpContext.Request.Headers["X-Requested-With"] = "XMLHttpRequest";
        }
        if (partialQuery)
        {
            httpContext.Request.QueryString = new QueryString("?partial=true");
        }

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        return controller;
    }

    [Fact]
    public async Task Index_StandardBrowserNavigation_ReturnsFullViewResult()
    {
        // Arrange
        var controller = CreateController(isAjax: false, partialQuery: false);

        _productServiceMock
            .Setup(p => p.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductSearchResult
            {
                Items = new List<ProductListItem>(),
                Page = 1,
                PageSize = 24,
                TotalCount = 0
            });

        // Act
        var result = await controller.Index(
            q: null,
            search: null,
            categoryId: null,
            category: "study-essentials",
            queryCategory: null,
            subcategorySlug: null,
            subcategoryId: null,
            minPrice: null,
            maxPrice: null,
            inStockOnly: null,
            minDiscount: null,
            sort: ProductSortOption.Relevance);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ProductListViewModel>(viewResult.Model);
        Assert.Equal("study-essentials", model.CategorySlug);
    }

    [Fact]
    public async Task Index_CategoryAjaxClick_ReturnsPartialViewForStudyEssentials()
    {
        // Arrange
        var controller = CreateController(isAjax: true, partialQuery: false);

        _productServiceMock
            .Setup(p => p.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductSearchResult
            {
                Items = new List<ProductListItem>
                {
                    new ProductListItem
                    {
                        ProductId = Guid.NewGuid(),
                        Name = "Engineering Scientific Calculator FX-991EX",
                        Slug = "fx-991ex-calculator",
                        SellingPrice = 1499,
                        MRP = 1999
                    }
                },
                Page = 1,
                PageSize = 24,
                TotalCount = 1
            });

        _categoryServiceMock
            .Setup(c => c.GetCategoryDetailsAsync("study-essentials", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CategoryDetails
            {
                CategoryId = Guid.NewGuid(),
                Name = "Study Essentials",
                Slug = "study-essentials",
                Subcategories = new List<SubcategoryListItem>
                {
                    new SubcategoryListItem
                    {
                        SubcategoryId = Guid.NewGuid(),
                        Name = "Tech for study",
                        Slug = "tech-for-study"
                    }
                }
            });

        // Act
        var result = await controller.Index(
            q: null,
            search: null,
            categoryId: null,
            category: "study-essentials",
            queryCategory: null,
            subcategorySlug: "tech-for-study",
            subcategoryId: null,
            minPrice: null,
            maxPrice: null,
            inStockOnly: null,
            minDiscount: null,
            sort: ProductSortOption.Relevance);

        // Assert
        var partialResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_StudyEssentialsProductsListPartial", partialResult.ViewName);
        var model = Assert.IsType<ProductListViewModel>(partialResult.Model);
        Assert.Single(model.Products);
        Assert.Equal("tech-for-study", model.CurrentSubcategorySlug);
        Assert.Equal("Tech for study", model.CurrentSubcategoryName);
    }

    [Fact]
    public async Task Index_CatalogAjaxFilter_ReturnsPartialViewForGeneralCatalog()
    {
        // Arrange
        var controller = CreateController(isAjax: false, partialQuery: true);

        _productServiceMock
            .Setup(p => p.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductSearchResult
            {
                Items = new List<ProductListItem>(),
                Page = 1,
                PageSize = 24,
                TotalCount = 0
            });

        // Act
        var result = await controller.Index(
            q: null,
            search: null,
            categoryId: null,
            category: "travel-gear",
            queryCategory: null,
            subcategorySlug: null,
            subcategoryId: null,
            minPrice: null,
            maxPrice: null,
            inStockOnly: null,
            minDiscount: null,
            sort: ProductSortOption.PriceLowToHigh);

        // Assert
        var partialResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_CatalogProductsListPartial", partialResult.ViewName);
    }

    [Fact]
    public async Task Index_EmptyResults_ReturnsModelWithEmptyProductsList()
    {
        // Arrange
        var controller = CreateController(isAjax: true, partialQuery: false);

        _productServiceMock
            .Setup(p => p.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductSearchResult
            {
                Items = new List<ProductListItem>(),
                Page = 1,
                PageSize = 24,
                TotalCount = 0
            });

        // Act
        var result = await controller.Index(
            q: null,
            search: null,
            categoryId: null,
            category: "study-essentials",
            queryCategory: null,
            subcategorySlug: "nonexistent-category-slug",
            subcategoryId: null,
            minPrice: null,
            maxPrice: null,
            inStockOnly: null,
            minDiscount: null,
            sort: ProductSortOption.Relevance);

        // Assert
        var partialResult = Assert.IsType<PartialViewResult>(result);
        var model = Assert.IsType<ProductListViewModel>(partialResult.Model);
        Assert.Empty(model.Products);
        Assert.Equal(0, model.TotalCount);
    }
}
