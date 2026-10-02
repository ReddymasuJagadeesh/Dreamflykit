using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Web.Models.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

public sealed class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(
        ICategoryService categoryService)
    {
        _categoryService = categoryService;
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
                        ProductCount = category.ProductCount
                    })
                .ToList()
        };

        return View(model);
    }


    // ============================================================
    // CATEGORY DETAILS
    // GET /category/{slug}
    // ============================================================

    [HttpGet("/category/{slug}")]
    public async Task<IActionResult> Details(
        string slug,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var category =
            await _categoryService.GetCategoryDetailsAsync(
                slug,
                cancellationToken);

        if (category is null)
        {
            return NotFound();
        }

        var model = new CategoryDetailsViewModel
        {
            CategoryId = category.CategoryId,

            Name = category.Name,

            Slug = category.Slug,

            Subcategories = category.Subcategories
                .Select(subcategory =>
                    new SubcategoryListItemViewModel
                    {
                        SubcategoryId =
                            subcategory.SubcategoryId,

                        CategoryId =
                            subcategory.CategoryId,

                        Name =
                            subcategory.Name,

                        Slug =
                            subcategory.Slug,

                        DisplayOrder =
                            subcategory.DisplayOrder,

                        ProductCount =
                            subcategory.ProductCount
                    })
                .ToList()
        };

        return View(model);
    }


    // ============================================================
    // SUBCATEGORY
    // GET /category/{categorySlug}/{subcategorySlug}
    // ============================================================

    [HttpGet("/category/{categorySlug}/{subcategorySlug}")]
    public async Task<IActionResult> Subcategory(
        string categorySlug,
        string subcategorySlug,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(categorySlug) ||
            string.IsNullOrWhiteSpace(subcategorySlug))
        {
            return NotFound();
        }

        var subcategory =
            await _categoryService.GetSubcategoryAsync(
                categorySlug,
                subcategorySlug,
                cancellationToken);

        if (subcategory is null)
        {
            return NotFound();
        }

        return View(
            "Subcategory",
            subcategory);
    }
}