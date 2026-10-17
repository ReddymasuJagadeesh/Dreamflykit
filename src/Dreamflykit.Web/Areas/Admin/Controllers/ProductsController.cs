using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Domain.Catalog;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Dreamflykit.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Route("Admin/[controller]")]
public sealed class ProductsController : AdminBaseController
{
    private readonly DreamflykitDbContext _dbContext;
    private readonly IMediaService _mediaService;
    private readonly IMediaUrlService _urlService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProductsController(
        DreamflykitDbContext dbContext,
        IMediaService mediaService,
        IMediaUrlService urlService,
        UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _mediaService = mediaService;
        _urlService = urlService;
        _userManager = userManager;
    }

    private async Task<(Guid? AssetId, string? ErrorMessage)> UploadProductMediaFileAsync(IFormFile? file, Guid productId, string altText, CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0) return (null, "No file selected.");

        await using var stream = file.OpenReadStream();
        var request = new Dreamflykit.Application.Media.Models.MediaUploadRequest
        {
            FileStream = stream,
            OriginalFileName = file.FileName,
            ContentType = file.ContentType,
            MediaType = Dreamflykit.Domain.Media.MediaType.Product,
            OwnerId = productId,
            AltText = !string.IsNullOrWhiteSpace(altText) ? altText : Path.GetFileNameWithoutExtension(file.FileName),
            UploadedBy = CurrentUserId != Guid.Empty ? CurrentUserId : Guid.NewGuid()
        };

        var result = await _mediaService.UploadAsync(request, cancellationToken);
        return result.IsSuccess ? (result.Asset?.Id, null) : (null, result.ErrorMessage);
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(
        [FromQuery] string? search,
        [FromQuery] Guid? categoryId,
        [FromQuery] string? status,
        [FromQuery] Guid? sellerId,
        CancellationToken cancellationToken)
    {
        var pendingApprovalCount = await _dbContext.Products
            .CountAsync(p => p.ApprovalStatus == ProductApprovalStatus.PendingApproval, cancellationToken);

        var query = _dbContext.Products.AsNoTracking().AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (sellerId.HasValue)
        {
            query = query.Where(p => p.SellerId == sellerId.Value);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            if (status.Equals("PendingApproval", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(p => p.ApprovalStatus == ProductApprovalStatus.PendingApproval);
            }
            else if (status.Equals("Approved", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(p => p.ApprovalStatus == ProductApprovalStatus.Approved);
            }
            else if (status.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(p => p.ApprovalStatus == ProductApprovalStatus.Rejected);
            }
            else if (status.Equals("SellerOnly", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(p => p.SellerId != null);
            }
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLowerInvariant();
            query = query.Where(p => p.Name.ToLower().Contains(term) || p.Slug.Contains(term));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var rawProducts = await query
            .OrderByDescending(p => p.ApprovalStatus == ProductApprovalStatus.PendingApproval)
            .ThenBy(p => p.DisplayOrder)
            .ThenBy(p => p.Name)
            .Take(100)
            .Select(p => new
            {
                ProductId = p.Id,
                Name = p.Name,
                Slug = p.Slug,
                SellerId = p.SellerId,
                ApprovalStatus = p.ApprovalStatus,
                IsPromoted = p.IsPromoted,
                RejectionReason = p.RejectionReason,
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

        // Fetch seller names if any products have sellerId
        var rawSellerIds = rawProducts.Where(p => p.SellerId.HasValue).Select(p => p.SellerId!.Value).Distinct().ToList();
        var sellerMap = new Dictionary<Guid, string>();
        if (rawSellerIds.Count > 0)
        {
            sellerMap = await _dbContext.Users
                .AsNoTracking()
                .Where(u => rawSellerIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => $"{u.FirstName} {u.LastName}".Trim(), cancellationToken);
        }

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

            string? sellerName = null;
            if (p.SellerId.HasValue && sellerMap.TryGetValue(p.SellerId.Value, out var sName))
            {
                sellerName = sName;
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
                IsFeatured = p.IsFeatured,
                IsPromoted = p.IsPromoted,
                ApprovalStatus = p.ApprovalStatus,
                SellerId = p.SellerId,
                SellerName = sellerName,
                RejectionReason = p.RejectionReason
            };
        }).ToList();

        var model = new AdminProductListViewModel
        {
            Products = products,
            SearchTerm = search,
            CategoryId = categoryId,
            StatusFilter = status,
            TotalCount = totalCount,
            PendingApprovalCount = pendingApprovalCount
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

        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var (uploadedAssetId, uploadError) = await UploadProductMediaFileAsync(model.ImageFile, product.Id, product.Name, cancellationToken);
            if (uploadedAssetId.HasValue)
            {
                model.PrimaryMediaAssetId = uploadedAssetId.Value;
            }
            else
            {
                TempData["WarningMessage"] = $"Product created, but image upload failed: {uploadError}";
            }
        }

        if (model.PrimaryMediaAssetId.HasValue && model.PrimaryMediaAssetId.Value != Guid.Empty)
        {
            var media = new ProductMedia(
                product.Id,
                model.PrimaryMediaAssetId.Value,
                model.Name.Trim(),
                displayOrder: 0,
                isPrimary: true);

            var asset = await _dbContext.MediaAssets
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == model.PrimaryMediaAssetId.Value, cancellationToken);
            if (asset != null)
            {
                media.SetMediaUrl(_urlService.GetCardUrl(asset.ObjectKey));
            }

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

        // Handle direct image file upload or picker selection
        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var (uploadedAssetId, uploadError) = await UploadProductMediaFileAsync(model.ImageFile, product.Id, product.Name, cancellationToken);
            if (uploadedAssetId.HasValue)
            {
                model.PrimaryMediaAssetId = uploadedAssetId.Value;
            }
            else
            {
                TempData["WarningMessage"] = $"Product updated, but image upload failed: {uploadError}";
            }
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
                var asset = await _dbContext.MediaAssets
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == model.PrimaryMediaAssetId.Value, cancellationToken);
                if (asset != null)
                {
                    newMedia.SetMediaUrl(_urlService.GetCardUrl(asset.ObjectKey));
                }
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

    [HttpPost("UploadDirectMedia")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadDirectMedia(
        [FromForm] Guid productId,
        [FromForm] IFormFile? file,
        [FromForm] bool isPrimary,
        CancellationToken cancellationToken)
    {
        if (file != null && file.Length > 0)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
            if (product != null)
            {
                var (assetId, errorMessage) = await UploadProductMediaFileAsync(file, productId, product.Name, cancellationToken);
                if (assetId.HasValue)
                {
                    var maxOrder = await _dbContext.ProductMedia
                        .Where(m => m.ProductId == productId)
                        .Select(m => (int?)m.DisplayOrder)
                        .MaxAsync(cancellationToken) ?? 0;

                    await _mediaService.AssignToProductAsync(
                        productId,
                        assetId.Value,
                        isPrimary,
                        maxOrder + 1,
                        CurrentUserId,
                        cancellationToken);

                    TempData["SuccessMessage"] = "Photo uploaded and attached to product gallery successfully.";
                    return RedirectToAction(nameof(Media), new { id = productId });
                }
                else
                {
                    TempData["ErrorMessage"] = $"Upload failed: {errorMessage ?? "Could not process image file."}";
                    return RedirectToAction(nameof(Media), new { id = productId });
                }
            }
        }

        TempData["ErrorMessage"] = "Please select a valid image file to upload.";
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

    [HttpPost("ToggleFeatured/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleFeatured(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (product is null)
        {
            return NotFound();
        }

        product.SetFeatured(!product.IsFeatured);
        await _dbContext.SaveChangesAsync(cancellationToken);
        TempData["SuccessMessage"] = $"Product '{product.Name}' is {(product.IsFeatured ? "now Featured on Homepage showcase" : "removed from Featured showcase")}.";

        return RedirectToAction("Index");
    }

    [HttpGet("Review/{id:guid}")]
    public async Task<IActionResult> Review(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        var categoryName = await _dbContext.Categories
            .Where(c => c.Id == product.CategoryId)
            .Select(c => c.Name)
            .FirstOrDefaultAsync(cancellationToken) ?? "Uncategorized";

        string? subcategoryName = null;
        if (product.SubcategoryId.HasValue)
        {
            subcategoryName = await _dbContext.Subcategories
                .Where(s => s.Id == product.SubcategoryId.Value)
                .Select(s => s.Name)
                .FirstOrDefaultAsync(cancellationToken);
        }

        string? sellerName = null;
        string? sellerEmail = null;
        string? sellerPhone = null;

        if (product.SellerId.HasValue)
        {
            var sellerUser = await _userManager.FindByIdAsync(product.SellerId.Value.ToString());
            if (sellerUser != null)
            {
                sellerName = $"{sellerUser.FirstName} {sellerUser.LastName}".Trim();
                sellerEmail = sellerUser.Email;
                sellerPhone = sellerUser.PhoneNumber;
            }
        }

        var existingVariant = await _dbContext.ProductVariants
            .AsNoTracking()
            .Where(v => v.ProductId == id)
            .OrderByDescending(v => v.IsDefault)
            .FirstOrDefaultAsync(cancellationToken);

        // Fetch product media
        var mediaList = await _dbContext.ProductMedia
            .AsNoTracking()
            .Where(m => m.ProductId == id && m.IsActive)
            .OrderBy(m => m.IsPrimary ? 0 : 1)
            .ThenBy(m => m.DisplayOrder)
            .ToListAsync(cancellationToken);

        var assetIds = mediaList.Where(m => m.MediaAssetId.HasValue).Select(m => m.MediaAssetId!.Value).Distinct().ToList();
        var assetMap = await _dbContext.MediaAssets
            .AsNoTracking()
            .Where(a => assetIds.Contains(a.Id))
            .ToDictionaryAsync(a => a.Id, a => _urlService.GetCardUrl(a.ObjectKey), cancellationToken);

        string? primaryImageUrl = null;
        var additionalImages = new List<string>();

        foreach (var m in mediaList)
        {
            string url = "";
            if (m.MediaAssetId.HasValue && assetMap.TryGetValue(m.MediaAssetId.Value, out var cdnUrl))
            {
                url = cdnUrl;
            }
            else if (!string.IsNullOrWhiteSpace(m.MediaUrl))
            {
                url = m.MediaUrl;
            }

            if (!string.IsNullOrEmpty(url))
            {
                if (m.IsPrimary && primaryImageUrl == null)
                {
                    primaryImageUrl = url;
                }
                else
                {
                    additionalImages.Add(url);
                }
            }
        }

        if (primaryImageUrl == null && additionalImages.Count > 0)
        {
            primaryImageUrl = additionalImages[0];
            additionalImages.RemoveAt(0);
        }

        var suggestedSku = existingVariant?.Sku ?? $"DFK-{DateTime.UtcNow:yyMMdd}-{Random.Shared.Next(1000, 9999)}";

        var model = new AdminReviewProductViewModel
        {
            ProductId = product.Id,
            Name = product.Name,
            Slug = product.Slug,
            CategoryName = categoryName,
            SubcategoryName = subcategoryName,
            ShortDescription = product.ShortDescription,
            Description = product.Description,
            SellerId = product.SellerId,
            SellerName = sellerName,
            SellerEmail = sellerEmail,
            SellerPhone = sellerPhone,
            ApprovalStatus = product.ApprovalStatus,
            RejectionReason = product.RejectionReason,
            ImageUrl = primaryImageUrl,
            AdditionalImages = additionalImages,
            CreatedAtUtc = product.CreatedAtUtc,
            Mrp = existingVariant?.Mrp ?? 0,
            SellingPrice = existingVariant?.SellingPrice ?? 0,
            Sku = suggestedSku,
            IsFeatured = product.IsFeatured
        };

        return View(model);
    }

    [HttpPost("ApprovePrice")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApprovePrice(AdminReviewProductViewModel model, CancellationToken cancellationToken)
    {
        if (model.SellingPrice <= 0)
        {
            ModelState.AddModelError(nameof(model.SellingPrice), "Selling price must be greater than zero.");
        }

        if (model.Mrp <= 0)
        {
            ModelState.AddModelError(nameof(model.Mrp), "MRP must be greater than zero.");
        }

        if (model.SellingPrice > model.Mrp)
        {
            ModelState.AddModelError(nameof(model.SellingPrice), "Selling price cannot exceed MRP.");
        }

        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == model.ProductId, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Please check the price inputs: MRP and Selling Price must be positive, and Selling Price cannot exceed MRP.";
            return RedirectToAction(nameof(Review), new { id = model.ProductId });
        }

        var sku = !string.IsNullOrWhiteSpace(model.Sku)
            ? model.Sku.Trim().ToUpperInvariant()
            : $"DFK-{DateTime.UtcNow:yyMMdd}-{Random.Shared.Next(1000, 9999)}";

        // Check if SKU already exists for another variant
        var existingVariant = await _dbContext.ProductVariants
            .FirstOrDefaultAsync(v => v.ProductId == product.Id, cancellationToken);

        if (existingVariant != null)
        {
            existingVariant.Update(
                existingVariant.Name,
                sku,
                model.Mrp,
                model.SellingPrice,
                existingVariant.IsDefault);
            existingVariant.Activate();
        }
        else
        {
            existingVariant = new ProductVariant(
                product.Id,
                "Default",
                sku,
                model.Mrp,
                model.SellingPrice,
                isDefault: true);
            _dbContext.ProductVariants.Add(existingVariant);
        }

        if (model.IsFeatured)
        {
            product.SetFeatured(true);
        }

        // Approve and activate product (GO LIVE!)
        product.Approve(CurrentUserId, DateTimeOffset.UtcNow);

        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Product '{product.Name}' has been approved with price ₹{model.SellingPrice:N0} (MRP: ₹{model.Mrp:N0}) and is now LIVE on Dreamflykit!";
        return RedirectToAction(nameof(Index), new { status = "PendingApproval" });
    }

    [HttpPost("Reject")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject([FromForm] Guid productId, [FromForm] string reason, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(reason))
        {
            TempData["ErrorMessage"] = "Please provide a reason when rejecting a seller submission.";
            return RedirectToAction(nameof(Review), new { id = productId });
        }

        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        product.Reject(reason);
        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["WarningMessage"] = $"Product '{product.Name}' was rejected. The seller has been notified with the feedback reason.";
        return RedirectToAction(nameof(Index));
    }
}
