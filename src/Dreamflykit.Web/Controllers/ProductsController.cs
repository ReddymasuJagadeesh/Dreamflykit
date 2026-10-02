using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Web.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

public sealed class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(
        IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("/products")]
    [HttpGet("/products/search")]
    public async Task<IActionResult> Index(
        [FromQuery(Name = "q")] string? q,
        [FromQuery] string? search,
        [FromQuery] Guid? categoryId,
        [FromQuery] string? category,
        [FromQuery] Guid? subcategoryId,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] bool? inStockOnly,
        [FromQuery] int? minDiscount,
        [FromQuery] ProductSortOption sort = ProductSortOption.Relevance,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 24,
        CancellationToken cancellationToken = default)
    {
        var effectiveSearch = !string.IsNullOrWhiteSpace(q) ? q.Trim() : (!string.IsNullOrWhiteSpace(search) ? search.Trim() : null);

        var query = new ProductListQuery
        {
            SearchTerm = effectiveSearch,
            CategoryId = categoryId,
            CategorySlug = category,
            SubcategoryId = subcategoryId,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            InStockOnly = inStockOnly,
            MinDiscount = minDiscount,
            Sort = sort,
            Page = page,
            PageSize = pageSize
        };

        var result = await _productService.SearchProductsAsync(
            query,
            cancellationToken);

        var model = new ProductListViewModel
        {
            Products = result.Items,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages,
            HasPreviousPage = result.HasPreviousPage,
            HasNextPage = result.HasNextPage,
            SearchTerm = effectiveSearch,
            CategoryId = categoryId,
            CategorySlug = result.CurrentCategorySlug ?? category,
            CategoryName = result.CurrentCategoryName,
            SubcategoryId = subcategoryId,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            InStockOnly = inStockOnly,
            MinDiscount = minDiscount,
            Sort = sort,
            Categories = result.Categories,
            PriceRanges = result.PriceRanges,
            Discounts = result.Discounts
        };

        return View(model);
    }

    [HttpGet("/products/autocomplete")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> Autocomplete(
        [FromQuery] string? q,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(q) || q.Trim().Length < 2)
        {
            return Json(new SearchAutocompleteResult { Query = q ?? string.Empty });
        }

        var result = await _productService.GetAutocompleteSuggestionsAsync(
            q.Trim(),
            cancellationToken);

        return Json(result);
    }

    [HttpGet("/product/{slug}")]
    public async Task<IActionResult> Details(
        string slug,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var product =
            await _productService.GetDetailsAsync(
                slug,
                cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        var model = new ProductDetailsViewModel
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Slug = product.Slug,
            Description = product.Description,
            ShortDescription = product.ShortDescription,
            CategoryName = product.CategoryName,
            CategorySlug = product.CategorySlug,
            SubcategoryName = product.SubcategoryName,
            SubcategorySlug = product.SubcategorySlug,
            IsActive = product.IsActive,
            IsFeatured = product.IsFeatured,

            Variants = product.Variants
                .Select(variant => new ProductVariantViewModel
                {
                    VariantId = variant.VariantId,
                    Sku = variant.Sku,
                    Mrp = variant.Mrp,
                    SellingPrice = variant.SellingPrice,
                    IsActive = variant.IsActive,
                    IsInStock = variant.IsInStock
                })
                .ToList(),

            Media = product.Media
                .Select(media => new ProductMediaViewModel
                {
                    MediaId = media.MediaId,
                    Url = media.Url,
                    AltText = media.AltText,
                    DisplayOrder = media.DisplayOrder,
                    IsPrimary = media.IsPrimary
                })
                .OrderByDescending(media => media.IsPrimary)
                .ThenBy(media => media.DisplayOrder)
                .ToList(),

            Attributes = product.Attributes
                .Select(attribute => new ProductAttributeViewModel
                {
                    AttributeId = attribute.AttributeId,
                    AttributeName = attribute.AttributeName,
                    Value = attribute.Value
                })
                .ToList()
        };

        return View(model);
    }
}