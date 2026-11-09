using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Web.Controllers;
using Dreamflykit.Web.Models.Products;
using Dreamflykit.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Dreamflykit.Tests;

public class ProductsControllerDecompositionTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly Mock<ICategoryHubCoordinator> _coordinatorMock;

    public ProductsControllerDecompositionTests()
    {
        _productServiceMock = new Mock<IProductService>();
        _coordinatorMock = new Mock<ICategoryHubCoordinator>();
    }

    private ProductsController CreateController(bool isAjax = false, bool isHtmx = false, bool partialQuery = false)
    {
        var controller = new ProductsController(
            _productServiceMock.Object,
            _coordinatorMock.Object);

        var httpContext = new DefaultHttpContext();
        if (isAjax)
        {
            httpContext.Request.Headers["X-Requested-With"] = "XMLHttpRequest";
        }
        if (isHtmx)
        {
            httpContext.Request.Headers["HX-Request"] = "true";
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
    public async Task Index_BrowserNavigation_ReturnsFullViewWithCoordinatedModel()
    {
        // Arrange
        var controller = CreateController(isAjax: false);

        _productServiceMock
            .Setup(p => p.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductSearchResult
            {
                Items = new List<ProductListItem>(),
                Page = 1,
                PageSize = 24,
                TotalCount = 0
            });

        _coordinatorMock
            .Setup(c => c.CoordinateCategoryLandingAsync(It.IsAny<CategoryLandingContext>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductListViewModel
            {
                CategorySlug = "fashion"
            });

        // Act
        var result = await controller.Index(
            q: null,
            search: null,
            categoryId: null,
            category: "fashion",
            queryCategory: null,
            subcategorySlug: null,
            brandSlug: null,
            subcategoryId: null,
            subcatItem: null,
            item: null,
            sub: null,
            minPrice: null,
            maxPrice: null,
            inStockOnly: null,
            minDiscount: null,
            sort: ProductSortOption.Newest,
            view: "grid",
            page: 1,
            pageSize: 24,
            cancellationToken: CancellationToken.None);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ProductListViewModel>(viewResult.Model);
        Assert.Equal("fashion", model.CategorySlug);
        _coordinatorMock.Verify(c => c.CoordinateCategoryLandingAsync(It.IsAny<CategoryLandingContext>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Index_AjaxRequest_ReturnsCatalogProductsListPartial()
    {
        // Arrange
        var controller = CreateController(isAjax: true);

        var testItems = new List<ProductListItem>
        {
            new() { ProductId = Guid.NewGuid(), Name = "Oxford Shirt", Slug = "oxford-shirt", SellingPrice = 999m }
        };

        _productServiceMock
            .Setup(p => p.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductSearchResult
            {
                Items = testItems,
                Page = 1,
                PageSize = 24,
                TotalCount = 1
            });

        _coordinatorMock
            .Setup(c => c.CoordinateCategoryLandingAsync(It.IsAny<CategoryLandingContext>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductListViewModel
            {
                CategorySlug = "fashion",
                Products = testItems
            });

        // Act
        var result = await controller.Index(
            q: "shirt",
            search: null,
            categoryId: null,
            category: null,
            queryCategory: null,
            subcategorySlug: null,
            brandSlug: null,
            subcategoryId: null,
            subcatItem: null,
            item: null,
            sub: null,
            minPrice: null,
            maxPrice: null,
            inStockOnly: null,
            minDiscount: null,
            sort: ProductSortOption.Newest,
            view: "grid",
            page: 1,
            pageSize: 24,
            cancellationToken: CancellationToken.None);

        // Assert
        var partialResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_CatalogProductsListPartial", partialResult.ViewName);
        var model = Assert.IsType<ProductListViewModel>(partialResult.Model);
        Assert.Single(model.Products);
    }

    [Fact]
    public async Task Index_StudyEssentialsAjax_ReturnsStudyEssentialsProductsListPartial()
    {
        // Arrange
        var controller = CreateController(partialQuery: true);

        _productServiceMock
            .Setup(p => p.SearchProductsAsync(It.IsAny<ProductListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductSearchResult
            {
                Items = new List<ProductListItem>(),
                Page = 1,
                PageSize = 24,
                TotalCount = 0
            });

        _coordinatorMock
            .Setup(c => c.CoordinateCategoryLandingAsync(It.IsAny<CategoryLandingContext>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProductListViewModel
            {
                CategorySlug = "study-essentials"
            });

        // Act
        var result = await controller.Index(
            q: null,
            search: null,
            categoryId: null,
            category: "study-essentials",
            queryCategory: null,
            subcategorySlug: null,
            brandSlug: null,
            subcategoryId: null,
            subcatItem: null,
            item: null,
            sub: null,
            minPrice: null,
            maxPrice: null,
            inStockOnly: null,
            minDiscount: null,
            sort: ProductSortOption.Newest,
            view: "grid",
            page: 1,
            pageSize: 24,
            cancellationToken: CancellationToken.None);

        // Assert
        var partialResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_StudyEssentialsProductsListPartial", partialResult.ViewName);
    }

    [Fact]
    public void RedirectMalformedStudyUrl_ReturnsPermanentRedirect()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.RedirectMalformedStudyUrl("laptops");

        // Assert
        var redirectResult = Assert.IsType<RedirectResult>(result);
        Assert.True(redirectResult.Permanent);
        Assert.Equal("/category/study-essentials/laptops", redirectResult.Url);
    }

    [Fact]
    public void ProductListViewModel_BuildFilterUrl_CorrectlyEncodesQueryParameters()
    {
        // Arrange
        var vm = new ProductListViewModel
        {
            CategorySlug = "fashion",
            SearchTerm = "hoodie",
            Sort = ProductSortOption.PriceLowToHigh,
            Page = 2,
            PageSize = 12
        };

        // Act
        var url = vm.BuildFilterUrl(
            catSlug: "fashion",
            subcatSlug: "winter-wear",
            minP: 500,
            maxP: 3000,
            inStock: true,
            discount: 10,
            sortOpt: ProductSortOption.PriceHighToLow,
            pageNum: 1);

        // Assert
        Assert.Contains("/category/fashion/winter-wear", url);
        Assert.Contains("q=hoodie", url);
        Assert.Contains("minPrice=500", url);
        Assert.Contains("maxPrice=3000", url);
        Assert.Contains("inStockOnly=true", url);
        Assert.Contains("minDiscount=10", url);
        Assert.Contains("sort=PriceHighToLow", url);
    }

    [Fact]
    public void ProductListViewModel_ClearParamUrl_RemovesSpecifiedParameter()
    {
        // Arrange
        var vm = new ProductListViewModel
        {
            CategorySlug = "fashion",
            SearchTerm = "shoes",
            MinPrice = 1000m,
            MaxPrice = 5000m,
            InStockOnly = true
        };

        // Act - clear price
        var clearedPriceUrl = vm.ClearParamUrl("price");

        // Assert
        Assert.DoesNotContain("minPrice", clearedPriceUrl);
        Assert.DoesNotContain("maxPrice", clearedPriceUrl);
        Assert.Contains("inStockOnly=true", clearedPriceUrl);
        Assert.Contains("/category/fashion", clearedPriceUrl);
    }
}
