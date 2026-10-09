using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Web.Models.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

public sealed class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public CategoriesController(
        ICategoryService categoryService,
        IProductService productService)
    {
        _categoryService = categoryService;
        _productService = productService;
    }

    // ============================================================
    // CATEGORY LIST
    // GET /categories
    // ============================================================

    [HttpGet("/categories")]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var categories =
            await _categoryService.GetActiveCategoriesAsync(
                cancellationToken);

        var model = new CategoryListViewModel
        {
            Categories = categories
                .Select(category =>
                    new CategoryListItemViewModel
                    {
                        CategoryId = category.CategoryId,
                        Name = category.Name,
                        Slug = category.Slug,
                        DisplayOrder = category.DisplayOrder,
                        ProductCount = category.ProductCount,
                        ImageUrl = category.ImageUrl
                    })
                .ToList()
        };

        return View(model);
    }
}