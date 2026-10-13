using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Domain.Catalog;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Seller.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Seller.Controllers;

[Route("Seller/[controller]")]
public sealed class ProductsController : SellerBaseController
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

    private async Task<Guid?> UploadProductMediaFileAsync(IFormFile? file, Guid productId, string altText, CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0) return null;

        await using var stream = file.OpenReadStream();
        var request = new MediaUploadRequest
        {
            FileStream = stream,
            OriginalFileName = file.FileName,
            ContentType = file.ContentType,
            MediaType = MediaType.Product,
            OwnerId = productId,
            AltText = altText,
            UploadedBy = CurrentUserId
        };

        var result = await _mediaService.UploadAsync(request, cancellationToken);
        return result.IsSuccess ? result.Asset?.Id : null;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(
        [FromQuery] string? search,
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        var sellerId = CurrentUserId;
        var query = _dbContext.Products.AsNoTracking().Where(p => p.SellerId == sellerId);

        var totalCount = await query.CountAsync(cancellationToken);
        var pendingCount = await query.CountAsync(p => p.ApprovalStatus == ProductApprovalStatus.PendingApproval, cancellationToken);
        var approvedCount = await query.CountAsync(p => p.ApprovalStatus == ProductApprovalStatus.Approved, cancellationToken);
        var rejectedCount = await query.CountAsync(p => p.ApprovalStatus == ProductApprovalStatus.Rejected, cancellationToken);
        var promotedCount = await query.CountAsync(p => p.IsPromoted, cancellationToken);

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
            else if (status.Equals("Promoted", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(p => p.IsPromoted);
            }
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLowerInvariant();
            query = query.Where(p => p.Name.ToLower().Contains(term) || p.Slug.Contains(term));
        }

        var rawItems = await query
            .OrderByDescending(p => p.CreatedAtUtc)
            .Take(100)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Slug,
                CategoryName = _dbContext.Categories.Where(c => c.Id == p.CategoryId).Select(c => c.Name).FirstOrDefault() ?? "Uncategorized",
                SubcategoryName = p.SubcategoryId.HasValue ? _dbContext.Subcategories.Where(s => s.Id == p.SubcategoryId.Value).Select(s => s.Name).FirstOrDefault() : null,
                SellingPrice = _dbContext.ProductVariants.Where(v => v.ProductId == p.Id && v.IsActive).OrderBy(v => v.SellingPrice).Select(v => v.SellingPrice).FirstOrDefault(),
                Mrp = _dbContext.ProductVariants.Where(v => v.ProductId == p.Id && v.IsActive).OrderBy(v => v.SellingPrice).Select(v => v.Mrp).FirstOrDefault(),
                MediaInfo = _dbContext.ProductMedia.Where(m => m.ProductId == p.Id && m.IsActive).OrderBy(m => m.DisplayOrder).Select(m => new { m.MediaUrl, m.MediaAssetId }).FirstOrDefault(),
                p.IsActive,
                p.IsPromoted,
                p.ApprovalStatus,
                p.RejectionReason,
                CreatedAtUtc = p.CreatedAtUtc
            })
            .ToListAsync(cancellationToken);

        var assetIds = rawItems.Where(p => p.MediaInfo?.MediaAssetId != null).Select(p => p.MediaInfo!.MediaAssetId!.Value).Distinct().ToList();
        var assetMap = await _dbContext.MediaAssets
            .AsNoTracking()
            .Where(a => assetIds.Contains(a.Id))
            .ToDictionaryAsync(a => a.Id, a => _urlService.GetThumbnailUrl(a.ObjectKey), cancellationToken);

        var items = rawItems.Select(p =>
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

            return new SellerProductListItemViewModel
            {
                ProductId = p.Id,
                Name = p.Name,
                Slug = p.Slug,
                CategoryName = p.CategoryName,
                SubcategoryName = p.SubcategoryName,
                SellingPrice = p.SellingPrice,
                Mrp = p.Mrp,
                ImageUrl = imageUrl,
                IsActive = p.IsActive,
                IsPromoted = p.IsPromoted,
                ApprovalStatus = p.ApprovalStatus,
                RejectionReason = p.RejectionReason,
                CreatedAtUtc = p.CreatedAtUtc
            };
        }).ToList();

        var model = new SellerProductListViewModel
        {
            Products = items,
            SearchTerm = search,
            StatusFilter = status,
            TotalCount = totalCount,
            PendingCount = pendingCount,
            ApprovedCount = approvedCount,
            RejectedCount = rejectedCount,
            PromotedCount = promotedCount
        };

        return View(model);
    }

    private async Task<List<Category>> GetSellerAllowedCategoriesAsync(CancellationToken cancellationToken)
    {
        var assignedCategoryIds = await _dbContext.SellerCategoryAssignments
            .AsNoTracking()
            .Where(a => a.SellerId == CurrentUserId)
            .Select(a => a.CategoryId)
            .ToListAsync(cancellationToken);

        var query = _dbContext.Categories
            .AsNoTracking()
            .Where(c => c.IsActive);

        if (assignedCategoryIds.Count > 0)
        {
            query = query.Where(c => assignedCategoryIds.Contains(c.Id));
        }

        return await query
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .ToListAsync(cancellationToken);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        ViewBag.Categories = await GetSellerAllowedCategoriesAsync(cancellationToken);
        return View(new SellerProductCreateViewModel());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SellerProductCreateViewModel model, CancellationToken cancellationToken)
    {
        var allowedCategories = await GetSellerAllowedCategoriesAsync(cancellationToken);
        ViewBag.Categories = allowedCategories;

        if (model.CategoryId == Guid.Empty || !allowedCategories.Any(c => c.Id == model.CategoryId))
        {
            ModelState.AddModelError(nameof(model.CategoryId), "Please select an authorized Category from your assigned list.");
        }

        var cleanSlug = (model.Slug ?? string.Empty).Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(cleanSlug))
        {
            cleanSlug = (model.Name ?? string.Empty).Trim().ToLowerInvariant().Replace(" ", "-");
        }

        if (await _dbContext.Products.AnyAsync(p => p.Slug == cleanSlug, cancellationToken))
        {
            ModelState.AddModelError(nameof(model.Slug), "A product with this URL slug already exists. Please choose a unique slug.");
        }

        if (string.IsNullOrWhiteSpace(model.Description))
        {
            ModelState.AddModelError(nameof(model.Description), "Please enter a detailed description for this product.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var shortDesc = !string.IsNullOrWhiteSpace(model.ShortDescription)
            ? model.ShortDescription.Trim()
            : (model.Description.Length > 180 ? model.Description.Substring(0, 180) + "..." : model.Description).Trim();

        // Create product - NO price is provided by seller!
        var product = new Product(
            model.CategoryId,
            model.Name.Trim(),
            cleanSlug,
            shortDesc,
            model.Description.Trim(),
            displayOrder: 0);

        if (model.SubcategoryId.HasValue && model.SubcategoryId.Value != Guid.Empty)
        {
            product.SetSubcategory(model.SubcategoryId.Value);
        }

        // Explicitly assign seller and set status to PendingApproval and IsActive = false
        product.AssignSeller(CurrentUserId);

        _dbContext.Products.Add(product);

        // Upload primary image if attached
        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var uploadedAssetId = await UploadProductMediaFileAsync(model.ImageFile, product.Id, model.Name.Trim(), cancellationToken);
            if (uploadedAssetId.HasValue)
            {
                var media = new ProductMedia(
                    product.Id,
                    uploadedAssetId.Value,
                    !string.IsNullOrWhiteSpace(model.AltText) ? model.AltText.Trim() : model.Name.Trim(),
                    displayOrder: 0,
                    isPrimary: true);

                var asset = await _dbContext.MediaAssets
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == uploadedAssetId.Value, cancellationToken);
                if (asset != null)
                {
                    media.SetMediaUrl(_urlService.GetCardUrl(asset.ObjectKey));
                }

                _dbContext.ProductMedia.Add(media);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Product '{product.Name}' submitted successfully! It has been placed in the Super Admin review queue for pricing assignment and approval.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.SellerId == CurrentUserId, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        ViewBag.Categories = await GetSellerAllowedCategoriesAsync(cancellationToken);

        if (product.CategoryId != Guid.Empty)
        {
            ViewBag.Subcategories = await _dbContext.Subcategories
                .AsNoTracking()
                .Where(s => s.CategoryId == product.CategoryId && s.IsActive)
                .OrderBy(s => s.DisplayOrder)
                .ThenBy(s => s.Name)
                .ToListAsync(cancellationToken);
        }

        var primaryMedia = await _dbContext.ProductMedia
            .AsNoTracking()
            .Where(m => m.ProductId == id && m.IsActive)
            .OrderBy(m => m.IsPrimary ? 0 : 1)
            .FirstOrDefaultAsync(cancellationToken);

        string? imgUrl = null;
        if (primaryMedia != null)
        {
            if (primaryMedia.MediaAssetId.HasValue)
            {
                var asset = await _dbContext.MediaAssets.FirstOrDefaultAsync(a => a.Id == primaryMedia.MediaAssetId.Value, cancellationToken);
                if (asset != null) imgUrl = _urlService.GetCardUrl(asset.ObjectKey);
            }
            if (string.IsNullOrEmpty(imgUrl)) imgUrl = primaryMedia.MediaUrl;
        }

        var variant = await _dbContext.ProductVariants
            .AsNoTracking()
            .Where(v => v.ProductId == id)
            .OrderByDescending(v => v.IsDefault)
            .FirstOrDefaultAsync(cancellationToken);

        var model = new SellerProductEditViewModel
        {
            ProductId = product.Id,
            Name = product.Name,
            Slug = product.Slug,
            CategoryId = product.CategoryId,
            SubcategoryId = product.SubcategoryId,
            ShortDescription = product.ShortDescription,
            Description = product.Description,
            ExistingImageUrl = imgUrl,
            ApprovalStatus = product.ApprovalStatus,
            RejectionReason = product.RejectionReason,
            CurrentSellingPrice = variant?.SellingPrice ?? 0,
            CurrentMrp = variant?.Mrp ?? 0
        };

        return View(model);
    }

    [HttpPost("Edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, SellerProductEditViewModel model, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.SellerId == CurrentUserId, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        var allowedCategories = await GetSellerAllowedCategoriesAsync(cancellationToken);
        ViewBag.Categories = allowedCategories;

        if (model.CategoryId == Guid.Empty || !allowedCategories.Any(c => c.Id == model.CategoryId))
        {
            ModelState.AddModelError(nameof(model.CategoryId), "Please select an authorized Category from your assigned list.");
        }

        var cleanSlug = (model.Slug ?? string.Empty).Trim().ToLowerInvariant();
        if (await _dbContext.Products.AnyAsync(p => p.Slug == cleanSlug && p.Id != id, cancellationToken))
        {
            ModelState.AddModelError(nameof(model.Slug), "This URL slug is already taken by another product.");
        }

        if (string.IsNullOrWhiteSpace(model.Description))
        {
            ModelState.AddModelError(nameof(model.Description), "Please enter a detailed description for this product.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var shortDesc = !string.IsNullOrWhiteSpace(model.ShortDescription)
            ? model.ShortDescription.Trim()
            : (model.Description.Length > 180 ? model.Description.Substring(0, 180) + "..." : model.Description).Trim();

        product.Update(
            model.CategoryId,
            model.Name.Trim(),
            cleanSlug,
            shortDesc,
            model.Description.Trim(),
            product.DisplayOrder);

        if (model.SubcategoryId.HasValue && model.SubcategoryId.Value != Guid.Empty)
        {
            product.SetSubcategory(model.SubcategoryId.Value);
        }
        else
        {
            product.RemoveSubcategory();
        }

        // If product was rejected, editing and saving automatically resubmits it for review!
        if (product.ApprovalStatus == ProductApprovalStatus.Rejected)
        {
            product.ResubmitForApproval();
        }

        // Upload new image if provided
        if (model.NewImageFile != null && model.NewImageFile.Length > 0)
        {
            var uploadedAssetId = await UploadProductMediaFileAsync(model.NewImageFile, product.Id, model.Name.Trim(), cancellationToken);
            if (uploadedAssetId.HasValue)
            {
                // Mark previous primary images as non-primary
                var existingPrimaries = await _dbContext.ProductMedia
                    .Where(m => m.ProductId == product.Id && m.IsPrimary)
                    .ToListAsync(cancellationToken);
                foreach (var ep in existingPrimaries)
                {
                    ep.SetPrimary(false);
                }

                var media = new ProductMedia(
                    product.Id,
                    uploadedAssetId.Value,
                    !string.IsNullOrWhiteSpace(model.AltText) ? model.AltText.Trim() : model.Name.Trim(),
                    displayOrder: 0,
                    isPrimary: true);

                var asset = await _dbContext.MediaAssets
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == uploadedAssetId.Value, cancellationToken);
                if (asset != null)
                {
                    media.SetMediaUrl(_urlService.GetCardUrl(asset.ObjectKey));
                }

                _dbContext.ProductMedia.Add(media);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Product '{product.Name}' updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Promotes all products belonging to the seller at once.
    /// User requirement: "and sellers casn promot their products all at one so for them make all proudts at one"
    /// </summary>
    [HttpPost("PromoteAll")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PromoteAll(CancellationToken cancellationToken)
    {
        var sellerId = CurrentUserId;
        var products = await _dbContext.Products
            .Where(p => p.SellerId == sellerId)
            .ToListAsync(cancellationToken);

        if (products.Count == 0)
        {
            TempData["ErrorMessage"] = "You do not have any products to promote. Please add products first.";
            return RedirectToAction(nameof(Index));
        }

        foreach (var p in products)
        {
            p.SetPromoted(true);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"⚡ All {products.Count} of your products are now PROMOTED! They will display the featured promotional badge across Dreamflykit.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("TogglePromote/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TogglePromote(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.SellerId == CurrentUserId, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        product.SetPromoted(!product.IsPromoted);
        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Product '{product.Name}' is {(product.IsPromoted ? "now Promoted" : "no longer Promoted")}.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Delete/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.SellerId == CurrentUserId, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        // Sellers can only delete pending or rejected products; approved live products require Super Admin action
        if (product.ApprovalStatus == ProductApprovalStatus.Approved)
        {
            TempData["ErrorMessage"] = "Live, approved products cannot be deleted directly. Please contact Super Admin.";
            return RedirectToAction(nameof(Index));
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Product '{product.Name}' was deleted.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Media/{id:guid}")]
    public async Task<IActionResult> Media(Guid id, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id && p.SellerId == CurrentUserId, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

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

        var items = mediaList.Select(m =>
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

            return new SellerProductMediaItemViewModel
            {
                MediaId = m.Id,
                Url = url,
                AltText = m.AltText,
                IsPrimary = m.IsPrimary
            };
        }).ToList();

        var model = new SellerProductMediaViewModel
        {
            ProductId = product.Id,
            ProductName = product.Name,
            MediaItems = items
        };

        return View(model);
    }

    [HttpPost("UploadMedia/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadMedia(Guid id, IFormFile? file, string? altText, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id && p.SellerId == CurrentUserId, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        if (file == null || file.Length == 0)
        {
            TempData["ErrorMessage"] = "Please select an image file to upload.";
            return RedirectToAction(nameof(Media), new { id });
        }

        var assetId = await UploadProductMediaFileAsync(file, id, !string.IsNullOrWhiteSpace(altText) ? altText : product.Name, cancellationToken);
        if (assetId.HasValue)
        {
            var isFirst = !await _dbContext.ProductMedia.AnyAsync(m => m.ProductId == id && m.IsActive, cancellationToken);
            var media = new ProductMedia(
                id,
                assetId.Value,
                !string.IsNullOrWhiteSpace(altText) ? altText.Trim() : product.Name,
                displayOrder: 1,
                isPrimary: isFirst);

            var asset = await _dbContext.MediaAssets.FirstOrDefaultAsync(a => a.Id == assetId.Value, cancellationToken);
            if (asset != null)
            {
                media.SetMediaUrl(_urlService.GetCardUrl(asset.ObjectKey));
            }

            _dbContext.ProductMedia.Add(media);
            await _dbContext.SaveChangesAsync(cancellationToken);

            TempData["SuccessMessage"] = "Photo uploaded and added to product gallery!";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to upload image. Please ensure it is a valid JPG/PNG/WebP file under 5MB.";
        }

        return RedirectToAction(nameof(Media), new { id });
    }
}
