using Dreamflykit.Application.Common;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Route("Admin/[controller]")]
public sealed class MediaController : AdminBaseController
{
    private readonly IMediaService _mediaService;
    private readonly IMediaPermissionService _permissionService;
    private readonly DreamflykitDbContext _db;
    private readonly ILogger<MediaController> _logger;

    public MediaController(
        IMediaService mediaService,
        IMediaPermissionService permissionService,
        DreamflykitDbContext db,
        ILogger<MediaController> logger)
    {
        _mediaService = mediaService;
        _permissionService = permissionService;
        _db = db;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(
        [FromQuery] MediaType? mediaType,
        [FromQuery] MediaAssetStatus? status,
        [FromQuery] string? search,
        [FromQuery] string? sortBy,
        [FromQuery] bool sortDescending = true,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 24,
        [FromQuery] bool picker = false,
        CancellationToken cancellationToken = default)
    {
        var query = new MediaLibraryQuery
        {
            MediaType = mediaType,
            Status = status,
            Search = search,
            SortBy = sortBy ?? "CreatedAtUtc",
            SortDescending = sortDescending,
            Page = page,
            PageSize = pageSize
        };

        var result = await _mediaService.GetLibraryAsync(query, cancellationToken);

        if (Request.Headers.Accept.ToString().Contains("application/json") ||
            string.Equals(Request.Query["format"], "json", StringComparison.OrdinalIgnoreCase))
        {
            return Json(new
            {
                items = result.Items,
                page = result.Page,
                pageSize = result.PageSize,
                totalCount = result.TotalCount,
                totalPages = result.TotalPages
            });
        }

        var viewModel = new MediaLibraryViewModel
        {
            Assets = result,
            MediaType = mediaType,
            Status = status,
            Search = search,
            SortBy = sortBy,
            SortDescending = sortDescending,
            Page = page,
            PageSize = pageSize
        };

        ViewData["IsPicker"] = picker;
        return View(viewModel);
    }

    [HttpGet("Upload")]
    public IActionResult Upload([FromQuery] MediaType? mediaType, [FromQuery] Guid? ownerId)
    {
        var model = new MediaUploadViewModel
        {
            MediaType = mediaType ?? MediaType.Product,
            OwnerId = ownerId ?? Guid.NewGuid()
        };

        return View(model);
    }

    [HttpPost("Upload")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(
        [FromForm] MediaUploadViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var hasPermission = await _permissionService.HasPermissionAsync(
            CurrentUserId, model.MediaType, MediaPermissionType.Upload, cancellationToken);

        if (!hasPermission)
        {
            ModelState.AddModelError(string.Empty, "You do not have permission to upload this media type.");
            return View(model);
        }

        if (model.File is null || model.File.Length == 0)
        {
            ModelState.AddModelError(nameof(model.File), "Please select an image file.");
            return View(model);
        }

        await using var stream = model.File.OpenReadStream();

        var request = new MediaUploadRequest
        {
            FileStream = stream,
            OriginalFileName = model.File.FileName,
            ContentType = model.File.ContentType,
            MediaType = model.MediaType,
            OwnerId = model.OwnerId,
            AltText = model.AltText,
            UploadedBy = CurrentUserId
        };

        var result = await _mediaService.UploadAsync(request, cancellationToken);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Upload failed.");
            return View(model);
        }

        TempData["SuccessMessage"] = "Image uploaded and processed successfully.";
        return RedirectToAction(nameof(Detail), new { id = result.Asset!.Id });
    }

    [HttpGet("Detail/{id:guid}")]
    public async Task<IActionResult> Detail(Guid id, CancellationToken cancellationToken)
    {
        var asset = await _mediaService.GetByIdAsync(id, cancellationToken);

        if (asset is null)
        {
            return NotFound();
        }

        var usages = new List<MediaUsageItem>();

        // Check products
        var productReferences = await _db.ProductMedia
            .AsNoTracking()
            .Where(pm => pm.MediaAssetId == id)
            .Join(_db.Products, pm => pm.ProductId, p => p.Id, (pm, p) => new { p.Id, p.Name })
            .ToListAsync(cancellationToken);

        foreach (var p in productReferences)
        {
            usages.Add(new MediaUsageItem
            {
                ResourceType = "Product",
                ResourceTitle = p.Name,
                ResourceUrl = $"/Admin/Products/Edit/{p.Id}"
            });
        }

        // Check categories
        var catReferences = await _db.Categories
            .AsNoTracking()
            .Where(c => c.ImageMediaAssetId == id)
            .Select(c => new { c.Id, c.Name })
            .ToListAsync(cancellationToken);

        foreach (var c in catReferences)
        {
            usages.Add(new MediaUsageItem
            {
                ResourceType = "Category",
                ResourceTitle = c.Name,
                ResourceUrl = $"/Admin/Categories/Edit/{c.Id}"
            });
        }

        // Check banners
        var bannerReferences = await _db.Banners
            .AsNoTracking()
            .Where(b => b.MediaAssetId == id)
            .Select(b => new { b.Id, b.Title })
            .ToListAsync(cancellationToken);

        foreach (var b in bannerReferences)
        {
            usages.Add(new MediaUsageItem
            {
                ResourceType = "Banner",
                ResourceTitle = b.Title,
                ResourceUrl = $"/Admin/Banners/Edit/{b.Id}"
            });
        }

        // Check homepage sections
        var sectionReferences = await _db.HomepageSections
            .AsNoTracking()
            .Where(hs => hs.MediaAssetId == id)
            .Select(hs => new { hs.Id, hs.Title })
            .ToListAsync(cancellationToken);

        foreach (var s in sectionReferences)
        {
            usages.Add(new MediaUsageItem
            {
                ResourceType = "Homepage Section",
                ResourceTitle = s.Title,
                ResourceUrl = $"/Admin/HomepageSections/Edit/{s.Id}"
            });
        }

        var viewModel = new MediaDetailViewModel
        {
            Asset = asset,
            Usages = usages
        };

        return View(viewModel);
    }

    [HttpGet("Edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var asset = await _mediaService.GetByIdAsync(id, cancellationToken);

        if (asset is null)
        {
            return NotFound();
        }

        var model = new MediaEditViewModel
        {
            Id = asset.Id,
            AltText = asset.AltText,
            Title = asset.Title,
            Caption = asset.Caption
        };

        ViewData["Asset"] = asset;
        return View(model);
    }

    [HttpPost("Edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        Guid id,
        [FromForm] MediaEditViewModel model,
        CancellationToken cancellationToken)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            var asset = await _mediaService.GetByIdAsync(id, cancellationToken);
            ViewData["Asset"] = asset;
            return View(model);
        }

        var success = await _mediaService.UpdateMetadataAsync(
            id, model.AltText, model.Caption, model.Title, CurrentUserId, cancellationToken);

        if (!success)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Media metadata updated successfully.";
        return RedirectToAction(nameof(Detail), new { id });
    }

    [HttpPost("Publish/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Publish(Guid id, CancellationToken cancellationToken)
    {
        var success = await _mediaService.PublishAsync(id, CurrentUserId, cancellationToken);
        if (success)
        {
            TempData["SuccessMessage"] = "Asset published successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Could not publish asset.";
        }
        return RedirectToAction(nameof(Detail), new { id });
    }

    [HttpPost("Unpublish/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Unpublish(Guid id, CancellationToken cancellationToken)
    {
        var success = await _mediaService.UnpublishAsync(id, CurrentUserId, cancellationToken);
        if (success)
        {
            TempData["SuccessMessage"] = "Asset unpublished successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Could not unpublish asset.";
        }
        return RedirectToAction(nameof(Detail), new { id });
    }

    [HttpPost("Archive/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Archive(Guid id, CancellationToken cancellationToken)
    {
        var success = await _mediaService.ArchiveAsync(id, CurrentUserId, cancellationToken);
        if (success)
        {
            TempData["SuccessMessage"] = "Asset archived.";
        }
        else
        {
            TempData["ErrorMessage"] = "Could not archive asset.";
        }
        return RedirectToAction(nameof(Detail), new { id });
    }

    [HttpPost("Delete/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var asset = await _mediaService.GetByIdAsync(id, cancellationToken);
        if (asset is null)
        {
            return NotFound();
        }

        if (Enum.TryParse<MediaType>(asset.MediaType, out var parsedType))
        {
            var canDelete = await _permissionService.HasPermissionAsync(
                CurrentUserId, parsedType, MediaPermissionType.Delete, cancellationToken);

            if (!canDelete)
            {
                TempData["ErrorMessage"] = "You do not have permission to delete this asset.";
                return RedirectToAction(nameof(Detail), new { id });
            }
        }

        var success = await _mediaService.DeleteAsync(id, CurrentUserId, cancellationToken);
        if (success)
        {
            TempData["SuccessMessage"] = "Asset deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        TempData["ErrorMessage"] = "Failed to delete asset.";
        return RedirectToAction(nameof(Detail), new { id });
    }
}
