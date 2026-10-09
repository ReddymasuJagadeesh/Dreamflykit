using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Domain.Catalog;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Route("Admin/[controller]")]
public sealed class CategoriesController : AdminBaseController
{
    private readonly DreamflykitDbContext _dbContext;
    private readonly IMediaUrlService _urlService;
    private readonly IMediaService _mediaService;

    public CategoriesController(
        DreamflykitDbContext dbContext,
        IMediaUrlService urlService,
        IMediaService mediaService)
    {
        _dbContext = dbContext;
        _urlService = urlService;
        _mediaService = mediaService;
    }

    private async Task<Guid?> UploadMediaFileAsync(IFormFile? file, Guid ownerId, string altText, CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0) return null;

        await using var stream = file.OpenReadStream();
        var request = new Dreamflykit.Application.Media.Models.MediaUploadRequest
        {
            FileStream = stream,
            OriginalFileName = file.FileName,
            ContentType = file.ContentType,
            MediaType = Dreamflykit.Domain.Media.MediaType.Category,
            OwnerId = ownerId,
            AltText = altText,
            UploadedBy = CurrentUserId
        };

        var result = await _mediaService.UploadAsync(request, cancellationToken);
        return result.IsSuccess ? result.Asset?.Id : null;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var categories = await _dbContext.Categories
            .AsNoTracking()
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .ToListAsync(cancellationToken);

        var subcategories = await _dbContext.Subcategories
            .AsNoTracking()
            .OrderBy(s => s.DisplayOrder)
            .ThenBy(s => s.Name)
            .ToListAsync(cancellationToken);

        var productCounts = await _dbContext.Products
            .AsNoTracking()
            .GroupBy(p => p.CategoryId)
            .Select(g => new { CategoryId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.CategoryId, x => x.Count, cancellationToken);

        var subcategoryProductCounts = await _dbContext.Products
            .AsNoTracking()
            .Where(p => p.SubcategoryId.HasValue)
            .GroupBy(p => p.SubcategoryId!.Value)
            .Select(g => new { SubcategoryId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.SubcategoryId, x => x.Count, cancellationToken);

        var assetIds = categories
            .Where(c => c.ImageMediaAssetId.HasValue)
            .Select(c => c.ImageMediaAssetId!.Value)
            .Union(subcategories.Where(s => s.ImageMediaAssetId.HasValue).Select(s => s.ImageMediaAssetId!.Value))
            .Distinct()
            .ToList();

        var assetMap = await _dbContext.MediaAssets
            .AsNoTracking()
            .Where(a => assetIds.Contains(a.Id))
            .ToDictionaryAsync(a => a.Id, a => _urlService.GetThumbnailUrl(a.ObjectKey), cancellationToken);

        var categoryItems = categories.Select(c => new AdminCategoryItemViewModel
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            DisplayOrder = c.DisplayOrder,
            IsActive = c.IsActive,
            ImageMediaAssetId = c.ImageMediaAssetId,
            ImageUrl = c.ImageMediaAssetId.HasValue && assetMap.TryGetValue(c.ImageMediaAssetId.Value, out var url) ? url : null,
            ProductCount = productCounts.TryGetValue(c.Id, out var count) ? count : 0,
            Subcategories = subcategories
                .Where(s => s.CategoryId == c.Id)
                .Select(s => new AdminSubcategoryItemViewModel
                {
                    Id = s.Id,
                    CategoryId = s.CategoryId,
                    CategoryName = c.Name,
                    Name = s.Name,
                    Slug = s.Slug,
                    DisplayOrder = s.DisplayOrder,
                    IsActive = s.IsActive,
                    ImageMediaAssetId = s.ImageMediaAssetId,
                    ImageUrl = s.ImageMediaAssetId.HasValue && assetMap.TryGetValue(s.ImageMediaAssetId.Value, out var subUrl) ? subUrl : null,
                    ProductCount = subcategoryProductCounts.TryGetValue(s.Id, out var subCount) ? subCount : 0
                }).ToList()
        }).ToList();

        var model = new AdminCategoryListViewModel
        {
            Categories = categoryItems
        };

        return View(model);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View(new AdminCategoryCreateViewModel());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminCategoryCreateViewModel model, CancellationToken cancellationToken)
    {
        if (await _dbContext.Categories.AnyAsync(c => c.Slug == model.Slug.Trim().ToLowerInvariant(), cancellationToken))
        {
            ModelState.AddModelError(nameof(model.Slug), "A category with this slug already exists.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var category = new Category(
            model.Name.Trim(),
            model.Slug.Trim().ToLowerInvariant(),
            model.DisplayOrder);

        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var uploadedAssetId = await UploadMediaFileAsync(model.ImageFile, category.Id, model.Name.Trim(), cancellationToken);
            if (uploadedAssetId.HasValue)
            {
                category.SetImage(uploadedAssetId.Value);
            }
        }
        else if (model.ImageMediaAssetId.HasValue && model.ImageMediaAssetId.Value != Guid.Empty)
        {
            category.SetImage(model.ImageMediaAssetId.Value);
        }

        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Category '{category.Name}' created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (category is null)
        {
            return NotFound();
        }

        string? previewUrl = null;
        if (category.ImageMediaAssetId.HasValue)
        {
            var asset = await _dbContext.MediaAssets
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == category.ImageMediaAssetId.Value, cancellationToken);

            if (asset is not null)
            {
                previewUrl = _urlService.GetThumbnailUrl(asset.ObjectKey);
            }
        }

        var model = new AdminCategoryEditViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            DisplayOrder = category.DisplayOrder,
            IsActive = category.IsActive,
            ImageMediaAssetId = category.ImageMediaAssetId,
            ImagePreviewUrl = previewUrl
        };

        return View(model);
    }

    [HttpPost("Edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, AdminCategoryEditViewModel model, CancellationToken cancellationToken)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (category is null)
        {
            return NotFound();
        }

        if (await _dbContext.Categories.AnyAsync(c => c.Id != id && c.Slug == model.Slug.Trim().ToLowerInvariant(), cancellationToken))
        {
            ModelState.AddModelError(nameof(model.Slug), "Another category with this slug already exists.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        category.Update(model.Name.Trim(), model.Slug.Trim().ToLowerInvariant(), model.DisplayOrder);

        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var uploadedAssetId = await UploadMediaFileAsync(model.ImageFile, category.Id, model.Name.Trim(), cancellationToken);
            if (uploadedAssetId.HasValue)
            {
                category.SetImage(uploadedAssetId.Value);
            }
        }
        else if (model.ImageMediaAssetId.HasValue && model.ImageMediaAssetId.Value != Guid.Empty)
        {
            category.SetImage(model.ImageMediaAssetId.Value);
        }
        else
        {
            category.RemoveImage();
        }

        if (model.IsActive && !category.IsActive)
        {
            category.Activate();
        }
        else if (!model.IsActive && category.IsActive)
        {
            category.Deactivate();
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Category '{category.Name}' updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Toggle/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(Guid id, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FindAsync(new object[] { id }, cancellationToken);
        if (category is null)
        {
            return NotFound();
        }

        if (category.IsActive)
        {
            category.Deactivate();
        }
        else
        {
            category.Activate();
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("CreateSubcategory")]
    public async Task<IActionResult> CreateSubcategory([FromQuery] Guid? categoryId, CancellationToken cancellationToken)
    {
        ViewBag.Categories = await _dbContext.Categories
            .AsNoTracking()
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .ToListAsync(cancellationToken);

        var model = new AdminSubcategoryCreateViewModel
        {
            CategoryId = categoryId ?? Guid.Empty
        };

        return View(model);
    }

    [HttpPost("CreateSubcategory")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSubcategory(AdminSubcategoryCreateViewModel model, CancellationToken cancellationToken)
    {
        if (await _dbContext.Subcategories.AnyAsync(s => s.CategoryId == model.CategoryId && s.Slug == model.Slug.Trim().ToLowerInvariant(), cancellationToken))
        {
            ModelState.AddModelError(nameof(model.Slug), "A subcategory with this slug already exists in this category.");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _dbContext.Categories
                .AsNoTracking()
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .ToListAsync(cancellationToken);

            return View(model);
        }

        var subcategory = new Subcategory(
            model.CategoryId,
            model.Name.Trim(),
            model.Slug.Trim().ToLowerInvariant(),
            model.DisplayOrder);

        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var uploadedAssetId = await UploadMediaFileAsync(model.ImageFile, subcategory.Id, model.Name.Trim(), cancellationToken);
            if (uploadedAssetId.HasValue)
            {
                subcategory.SetImage(uploadedAssetId.Value);
            }
        }
        else if (model.ImageMediaAssetId.HasValue && model.ImageMediaAssetId.Value != Guid.Empty)
        {
            subcategory.SetImage(model.ImageMediaAssetId.Value);
        }

        _dbContext.Subcategories.Add(subcategory);
        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Subcategory '{subcategory.Name}' created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("EditSubcategory/{id:guid}")]
    public async Task<IActionResult> EditSubcategory(Guid id, CancellationToken cancellationToken)
    {
        var subcategory = await _dbContext.Subcategories
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (subcategory is null)
        {
            return NotFound();
        }

        ViewBag.Categories = await _dbContext.Categories
            .AsNoTracking()
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .ToListAsync(cancellationToken);

        string? previewUrl = null;
        if (subcategory.ImageMediaAssetId.HasValue)
        {
            var asset = await _dbContext.MediaAssets
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == subcategory.ImageMediaAssetId.Value, cancellationToken);

            if (asset is not null)
            {
                previewUrl = _urlService.GetThumbnailUrl(asset.ObjectKey);
            }
        }

        var model = new AdminSubcategoryEditViewModel
        {
            Id = subcategory.Id,
            CategoryId = subcategory.CategoryId,
            Name = subcategory.Name,
            Slug = subcategory.Slug,
            DisplayOrder = subcategory.DisplayOrder,
            IsActive = subcategory.IsActive,
            ImageMediaAssetId = subcategory.ImageMediaAssetId,
            ImagePreviewUrl = previewUrl
        };

        return View(model);
    }

    [HttpPost("EditSubcategory/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditSubcategory(Guid id, AdminSubcategoryEditViewModel model, CancellationToken cancellationToken)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        var subcategory = await _dbContext.Subcategories
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (subcategory is null)
        {
            return NotFound();
        }

        if (await _dbContext.Subcategories.AnyAsync(s => s.Id != id && s.CategoryId == model.CategoryId && s.Slug == model.Slug.Trim().ToLowerInvariant(), cancellationToken))
        {
            ModelState.AddModelError(nameof(model.Slug), "Another subcategory with this slug already exists in this category.");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _dbContext.Categories
                .AsNoTracking()
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .ToListAsync(cancellationToken);

            return View(model);
        }

        subcategory.Update(model.Name.Trim(), model.Slug.Trim().ToLowerInvariant(), model.DisplayOrder);

        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var uploadedAssetId = await UploadMediaFileAsync(model.ImageFile, subcategory.Id, model.Name.Trim(), cancellationToken);
            if (uploadedAssetId.HasValue)
            {
                subcategory.SetImage(uploadedAssetId.Value);
            }
        }
        else if (model.ImageMediaAssetId.HasValue && model.ImageMediaAssetId.Value != Guid.Empty)
        {
            subcategory.SetImage(model.ImageMediaAssetId.Value);
        }
        else
        {
            subcategory.RemoveImage();
        }

        if (model.IsActive && !subcategory.IsActive)
        {
            subcategory.Activate();
        }
        else if (!model.IsActive && subcategory.IsActive)
        {
            subcategory.Deactivate();
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Subcategory '{subcategory.Name}' updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("ToggleSubcategory/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleSubcategory(Guid id, CancellationToken cancellationToken)
    {
        var subcategory = await _dbContext.Subcategories.FindAsync(new object[] { id }, cancellationToken);
        if (subcategory is null)
        {
            return NotFound();
        }

        if (subcategory.IsActive)
        {
            subcategory.Deactivate();
        }
        else
        {
            subcategory.Activate();
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("GetSubcategories")]
    public async Task<IActionResult> GetSubcategories([FromQuery] Guid categoryId, CancellationToken cancellationToken)
    {
        var subcategories = await _dbContext.Subcategories
            .AsNoTracking()
            .Where(s => s.CategoryId == categoryId && s.IsActive)
            .OrderBy(s => s.DisplayOrder)
            .ThenBy(s => s.Name)
            .Select(s => new { id = s.Id, name = s.Name })
            .ToListAsync(cancellationToken);

        return Json(subcategories);
    }
}
