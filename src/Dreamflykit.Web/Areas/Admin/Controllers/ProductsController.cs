using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Domain.Catalog;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Route("Admin/[controller]")]
public sealed class ProductsController : AdminBaseController
{
    private readonly DreamflykitDbContext _dbContext;
    private readonly IMediaService _mediaService;
    private readonly IMediaUrlService _urlService;

    public ProductsController(
        DreamflykitDbContext dbContext,
        IMediaService mediaService,
        IMediaUrlService urlService)
    {
        _dbContext = dbContext;
        _mediaService = mediaService;
        _urlService = urlService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(
        [FromQuery] string? search,
        [FromQuery] Guid? categoryId,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Products.AsNoTracking().AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLowerInvariant();
            query = query.Where(p => p.Name.ToLower().Contains(term) || p.Slug.Contains(term));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var rawProducts = await query
            .OrderBy(p => p.DisplayOrder)
            .ThenBy(p => p.Name)
            .Take(100)
            .Select(p => new
            {
                ProductId = p.Id,
                Name = p.Name,
                Slug = p.Slug,
                CategoryName = _dbContext.Categories
                    .Where(c => c.Id == p.CategoryId)
                    .Select(c => c.Name)
                    .FirstOrDefault() ?? "Uncategorized",
                SellingPrice = _dbContext.ProductVariants
                    .Where(v => v.ProductId == p.Id && v.IsActive)
                    .OrderBy(v => v.SellingPrice)
                    .Select(v => v.SellingPrice)
                    .FirstOrDefault(),
                Mrp = _dbContext.ProductVariants
                    .Where(v => v.ProductId == p.Id && v.IsActive)
                    .OrderBy(v => v.SellingPrice)
                    .Select(v => v.Mrp)
                    .FirstOrDefault(),
                MediaInfo = _dbContext.ProductMedia
                    .Where(m => m.ProductId == p.Id && m.IsActive)
                    .OrderBy(m => m.DisplayOrder)
                    .Select(m => new { m.MediaUrl, m.MediaAssetId })
                    .FirstOrDefault(),
                IsActive = p.IsActive,
                IsFeatured = p.IsFeatured
            })
            .ToListAsync(cancellationToken);

        // Fetch S3 media keys for any assets
        var assetIds = rawProducts
            .Where(p => p.MediaInfo?.MediaAssetId != null)
            .Select(p => p.MediaInfo!.MediaAssetId!.Value)
            .Distinct()
            .ToList();

        var assetMap = await _dbContext.MediaAssets
            .AsNoTracking()
            .Where(a => assetIds.Contains(a.Id))
            .ToDictionaryAsync(a => a.Id, a => _urlService.GetThumbnailUrl(a.ObjectKey), cancellationToken);

        var products = rawProducts.Select(p =>
        {
            string? imageUrl = null;
            if (p.MediaInfo != null)
            {
                if (p.MediaInfo.MediaAssetId.HasValue && assetMap.TryGetValue(p.MediaInfo.MediaAssetId.Value, out var cdnUrl))
                {
                    imageUrl = cdnUrl;
                }
                else if (!string.IsNullOrWhiteSpace(p.MediaInfo.MediaUrl))
                {
                    imageUrl = p.MediaInfo.MediaUrl;
                }
            }

            return new AdminProductListItemViewModel
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Slug = p.Slug,
                CategoryName = p.CategoryName,
                SellingPrice = p.SellingPrice,
                Mrp = p.Mrp,
                ImageUrl = imageUrl,
                IsActive = p.IsActive,
                IsFeatured = p.IsFeatured
            };
        }).ToList();

        var model = new AdminProductListViewModel
        {
            Products = products,
            SearchTerm = search,
            CategoryId = categoryId,
            TotalCount = totalCount
        };

        return View(model);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        ViewBag.Categories = await _dbContext.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .ToListAsync(cancellationToken);

        var model = new AdminProductCreateViewModel
        {
            Sku = $"DFK-{DateTime.UtcNow:yyMMdd}-{Random.Shared.Next(100, 999)}",
            IsActive = true
        };

        return View(model);
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminProductCreateViewModel model, CancellationToken cancellationToken)
    {
        if (await _dbContext.Products.AnyAsync(p => p.Slug == model.Slug.Trim().ToLowerInvariant(), cancellationToken))
        {
            ModelState.AddModelError(nameof(model.Slug), "A product with this slug already exists.");
        }

        if (model.SellingPrice > model.Mrp)
        {
            ModelState.AddModelError(nameof(model.SellingPrice), "Selling price cannot exceed MRP.");
        }

        if (await _dbContext.ProductVariants.AnyAsync(v => v.Sku == model.Sku.Trim().ToUpperInvariant(), cancellationToken))
        {
            ModelState.AddModelError(nameof(model.Sku), "This SKU is already in use by another product variant.");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _dbContext.Categories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .ToListAsync(cancellationToken);

            if (model.CategoryId != Guid.Empty)
            {
                ViewBag.Subcategories = await _dbContext.Subcategories
                    .AsNoTracking()
                    .Where(s => s.CategoryId == model.CategoryId && s.IsActive)
                    .OrderBy(s => s.DisplayOrder)
                    .ThenBy(s => s.Name)
                    .ToListAsync(cancellationToken);
            }

            return View(model);
        }

        var product = new Product(
            model.CategoryId,
            model.Name.Trim(),
            model.Slug.Trim().ToLowerInvariant(),
            model.ShortDescription.Trim(),
            model.Description.Trim(),
            model.DisplayOrder);

        if (model.SubcategoryId.HasValue && model.SubcategoryId.Value != Guid.Empty)
        {
            product.SetSubcategory(model.SubcategoryId.Value);
        }

        if (model.IsFeatured)
        {
            product.SetFeatured(true);
        }

        if (!model.IsActive)
        {
            product.Deactivate();
        }

        _dbContext.Products.Add(product);

        var variant = new ProductVariant(
            product.Id,
            "Default",
            model.Sku.Trim().ToUpperInvariant(),
            model.Mrp,
            model.SellingPrice,
            isDefault: true);

        _dbContext.ProductVariants.Add(variant);

        if (model.PrimaryMediaAssetId.HasValue && model.PrimaryMediaAssetId.Value != Guid.Empty)
        {
            var media = new ProductMedia(
                product.Id,
                model.PrimaryMediaAssetId.Value,
                model.Name.Trim(),
                displayOrder: 0,
                isPrimary: true);

            _dbContext.ProductMedia.Add(media);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Product '{product.Name}' created successfully! You can now manage its image gallery.";
        return RedirectToAction(nameof(Media), new { id = product.Id });
    }

    [HttpGet("Edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        var variant = await _dbContext.ProductVariants
            .AsNoTracking()
            .Where(v => v.ProductId == id)
            .OrderByDescending(v => v.IsDefault)
            .FirstOrDefaultAsync(cancellationToken);

        ViewBag.Categories = await _dbContext.Categories
            .AsNoTracking()
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .ToListAsync(cancellationToken);

        ViewBag.Subcategories = await _dbContext.Subcategories
            .AsNoTracking()
            .Where(s => s.CategoryId == product.CategoryId)
            .OrderBy(s => s.DisplayOrder)
            .ThenBy(s => s.Name)
            .ToListAsync(cancellationToken);

        var primaryMedia = await _dbContext.ProductMedia
            .AsNoTracking()
            .Where(m => m.ProductId == id && m.IsActive)
            .OrderByDescending(m => m.IsPrimary)
            .ThenBy(m => m.DisplayOrder)
            .FirstOrDefaultAsync(cancellationToken);

        string? previewUrl = null;
        if (primaryMedia?.MediaAssetId != null)
        {
            var asset = await _dbContext.MediaAssets
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == primaryMedia.MediaAssetId.Value, cancellationToken);

            if (asset is not null)
            {
                previewUrl = _urlService.GetThumbnailUrl(asset.ObjectKey);
            }
        }
        else if (!string.IsNullOrWhiteSpace(primaryMedia?.MediaUrl))
        {
            previewUrl = primaryMedia.MediaUrl;
        }

        var model = new AdminProductEditViewModel
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            SubcategoryId = product.SubcategoryId,
            Name = product.Name,
            Slug = product.Slug,
            ShortDescription = product.ShortDescription,
            Description = product.Description,
            DisplayOrder = product.DisplayOrder,
            IsFeatured = product.IsFeatured,
            IsActive = product.IsActive,
            VariantId = variant?.Id ?? Guid.Empty,
            Sku = variant?.Sku ?? string.Empty,
            Mrp = variant?.Mrp ?? 0,
            SellingPrice = variant?.SellingPrice ?? 0,
            PrimaryMediaAssetId = primaryMedia?.MediaAssetId,
            PrimaryImagePreviewUrl = previewUrl
        };

        return View(model);
    }

    [HttpPost("Edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, AdminProductEditViewModel model, CancellationToken cancellationToken)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        if (await _dbContext.Products.AnyAsync(p => p.Id != id && p.Slug == model.Slug.Trim().ToLowerInvariant(), cancellationToken))
        {
            ModelState.AddModelError(nameof(model.Slug), "Another product with this slug already exists.");
        }

        if (model.SellingPrice > model.Mrp)
        {
            ModelState.AddModelError(nameof(model.SellingPrice), "Selling price cannot exceed MRP.");
        }

        if (await _dbContext.ProductVariants.AnyAsync(v => v.ProductId != id && v.Sku == model.Sku.Trim().ToUpperInvariant(), cancellationToken))
        {
            ModelState.AddModelError(nameof(model.Sku), "This SKU is already in use by another product.");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _dbContext.Categories
                .AsNoTracking()
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .ToListAsync(cancellationToken);

            ViewBag.Subcategories = await _dbContext.Subcategories
                .AsNoTracking()
                .Where(s => s.CategoryId == model.CategoryId)
                .OrderBy(s => s.DisplayOrder)
                .ThenBy(s => s.Name)
                .ToListAsync(cancellationToken);

            return View(model);
        }

        product.Update(
            model.CategoryId,
            model.Name.Trim(),
            model.Slug.Trim().ToLowerInvariant(),
            model.ShortDescription.Trim(),
            model.Description.Trim(),
            model.DisplayOrder);

        if (model.SubcategoryId.HasValue && model.SubcategoryId.Value != Guid.Empty)
        {
            product.SetSubcategory(model.SubcategoryId.Value);
        }
        else
        {
            product.RemoveSubcategory();
        }

        product.SetFeatured(model.IsFeatured);

        if (model.IsActive && !product.IsActive)
        {
            product.Activate();
        }
        else if (!model.IsActive && product.IsActive)
        {
            product.Deactivate();
        }

        // Update default variant
        var variant = await _dbContext.ProductVariants
            .Where(v => v.ProductId == id)
            .OrderByDescending(v => v.IsDefault)
            .FirstOrDefaultAsync(cancellationToken);

        if (variant is not null)
        {
            variant.Update(variant.Name, model.Sku.Trim().ToUpperInvariant(), model.Mrp, model.SellingPrice, variant.IsDefault);
        }
        else
        {
            var newVariant = new ProductVariant(product.Id, "Default", model.Sku.Trim().ToUpperInvariant(), model.Mrp, model.SellingPrice, isDefault: true);
            _dbContext.ProductVariants.Add(newVariant);
        }

        // Handle primary image update if chosen
        if (model.PrimaryMediaAssetId.HasValue && model.PrimaryMediaAssetId.Value != Guid.Empty)
        {
            var existingMedia = await _dbContext.ProductMedia
                .Where(m => m.ProductId == id && m.MediaAssetId == model.PrimaryMediaAssetId.Value)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingMedia is not null)
            {
                // Reset others and mark this primary
                var allMedia = await _dbContext.ProductMedia.Where(m => m.ProductId == id).ToListAsync(cancellationToken);
                foreach (var m in allMedia)
                {
                    m.SetPrimary(m.Id == existingMedia.Id);
                }
            }
            else
            {
                // Unset existing primaries
                var existingPrimaries = await _dbContext.ProductMedia.Where(m => m.ProductId == id && m.IsPrimary).ToListAsync(cancellationToken);
                foreach (var p in existingPrimaries)
                {
                    p.SetPrimary(false);
                }

                var newMedia = new ProductMedia(product.Id, model.PrimaryMediaAssetId.Value, model.Name.Trim(), displayOrder: 0, isPrimary: true);
                _dbContext.ProductMedia.Add(newMedia);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Product '{product.Name}' updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Media/{id:guid}")]
    public async Task<IActionResult> Media(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        var mediaRecords = await _dbContext.ProductMedia
            .AsNoTracking()
            .Where(pm => pm.ProductId == id && pm.IsActive)
            .OrderBy(pm => pm.DisplayOrder)
            .ToListAsync(cancellationToken);

        var assetIds = mediaRecords
            .Where(m => m.MediaAssetId.HasValue)
            .Select(m => m.MediaAssetId!.Value)
            .Distinct()
            .ToList();

        var assetMap = await _dbContext.MediaAssets
            .AsNoTracking()
            .Where(a => assetIds.Contains(a.Id))
            .ToDictionaryAsync(a => a.Id, a => _urlService.GetThumbnailUrl(a.ObjectKey), cancellationToken);

        var items = mediaRecords.Select(m => new ProductMediaItemViewModel
        {
            ProductMediaId = m.Id,
            MediaAssetId = m.MediaAssetId,
            Url = m.MediaAssetId.HasValue && assetMap.TryGetValue(m.MediaAssetId.Value, out var cdnUrl)
                ? cdnUrl
                : m.MediaUrl,
            AltText = m.AltText,
            DisplayOrder = m.DisplayOrder,
            IsPrimary = m.IsPrimary
        }).ToList();

        var viewModel = new ProductMediaManagementViewModel
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductSlug = product.Slug,
            MediaItems = items
        };

        return View(viewModel);
    }

    [HttpPost("AssignMedia")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignMedia(
        [FromForm] AssignProductMediaRequest request,
        CancellationToken cancellationToken)
    {
        var success = await _mediaService.AssignToProductAsync(
            request.ProductId,
            request.MediaAssetId,
            request.IsPrimary,
            request.DisplayOrder,
            CurrentUserId,
            cancellationToken);

        if (success)
        {
            TempData["SuccessMessage"] = "Image assigned to product successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to assign image.";
        }

        return RedirectToAction(nameof(Media), new { id = request.ProductId });
    }

    [HttpPost("SetPrimaryMedia")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetPrimaryMedia(
        [FromForm] Guid productId,
        [FromForm] Guid productMediaId,
        CancellationToken cancellationToken)
    {
        var success = await _mediaService.SetProductPrimaryMediaAsync(
            productId, productMediaId, CurrentUserId, cancellationToken);

        if (success)
        {
            TempData["SuccessMessage"] = "Primary image updated.";
        }

        return RedirectToAction(nameof(Media), new { id = productId });
    }

    [HttpPost("RemoveMedia")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveMedia(
        [FromForm] Guid productId,
        [FromForm] Guid productMediaId,
        CancellationToken cancellationToken)
    {
        var success = await _mediaService.RemoveFromProductAsync(
            productMediaId, CurrentUserId, cancellationToken);

        if (success)
        {
            TempData["SuccessMessage"] = "Image removed from product.";
        }

        return RedirectToAction(nameof(Media), new { id = productId });
    }

    [HttpPost("ToggleStatus/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (product is null)
        {
            return NotFound();
        }

        if (product.IsActive)
        {
            product.Deactivate();
        }
        else
        {
            product.Activate();
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        TempData["SuccessMessage"] = $"Product '{product.Name}' is now {(product.IsActive ? "Active" : "Inactive")}.";

        return RedirectToAction("Index");
    }
}
