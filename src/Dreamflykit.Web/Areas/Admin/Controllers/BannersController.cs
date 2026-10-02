using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Route("Admin/[controller]")]
public sealed class BannersController : AdminBaseController
{
    private readonly DreamflykitDbContext _db;
    private readonly IMediaUrlService _urlService;

    public BannersController(
        DreamflykitDbContext db,
        IMediaUrlService urlService)
    {
        _db = db;
        _urlService = urlService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var banners = await _db.Banners
            .AsNoTracking()
            .OrderBy(b => b.DisplayOrder)
            .ToListAsync(cancellationToken);

        var mediaIds = banners.Where(b => b.MediaAssetId.HasValue)
            .Select(b => b.MediaAssetId!.Value)
            .Distinct()
            .ToList();

        var mediaMap = await _db.MediaAssets
            .AsNoTracking()
            .Where(m => mediaIds.Contains(m.Id))
            .ToDictionaryAsync(m => m.Id, m => _urlService.GetUrl(m.ObjectKey), cancellationToken);

        var dtoList = banners.Select(b => new BannerItemDto
        {
            Id = b.Id,
            MediaAssetId = b.MediaAssetId ?? Guid.Empty,
            ImageUrl = b.MediaAssetId.HasValue && mediaMap.TryGetValue(b.MediaAssetId.Value, out var url) ? url : string.Empty,
            Title = b.Title,
            Subtitle = b.Subtitle,
            CtaText = b.CtaText,
            CtaUrl = b.CtaUrl,
            DisplayOrder = b.DisplayOrder,
            IsActive = b.IsActive,
            IsPublished = b.IsPublished,
            StartDateUtc = b.StartDateUtc,
            EndDateUtc = b.EndDateUtc
        }).ToList();

        return View(new BannerListViewModel { Banners = dtoList });
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View(new BannerEditViewModel());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [FromForm] BannerEditViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var banner = new Banner(
            model.Title,
            model.MediaAssetId == Guid.Empty ? null : model.MediaAssetId,
            model.DisplayOrder,
            CurrentUserId);

        banner.Update(
            model.Title,
            model.Subtitle,
            model.CtaText,
            model.CtaUrl,
            model.DisplayOrder,
            model.StartDateUtc,
            model.EndDateUtc,
            model.MediaAssetId == Guid.Empty ? null : model.MediaAssetId,
            CurrentUserId);

        if (model.IsPublished)
        {
            banner.Publish(CurrentUserId);
        }

        if (!model.IsActive)
        {
            banner.Deactivate();
        }

        _db.Banners.Add(banner);
        await _db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Banner created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var banner = await _db.Banners
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (banner is null)
        {
            return NotFound();
        }

        string? currentImageUrl = null;
        if (banner.MediaAssetId.HasValue)
        {
            var media = await _db.MediaAssets
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == banner.MediaAssetId.Value, cancellationToken);

            if (media is not null)
            {
                currentImageUrl = _urlService.GetUrl(media.ObjectKey);
            }
        }

        var model = new BannerEditViewModel
        {
            Id = banner.Id,
            MediaAssetId = banner.MediaAssetId ?? Guid.Empty,
            CurrentImageUrl = currentImageUrl,
            Title = banner.Title,
            Subtitle = banner.Subtitle,
            CtaText = banner.CtaText,
            CtaUrl = banner.CtaUrl,
            DisplayOrder = banner.DisplayOrder,
            IsActive = banner.IsActive,
            IsPublished = banner.IsPublished,
            StartDateUtc = banner.StartDateUtc,
            EndDateUtc = banner.EndDateUtc
        };

        return View(model);
    }

    [HttpPost("Edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        Guid id,
        [FromForm] BannerEditViewModel model,
        CancellationToken cancellationToken)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var banner = await _db.Banners
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (banner is null)
        {
            return NotFound();
        }

        banner.Update(
            model.Title,
            model.Subtitle,
            model.CtaText,
            model.CtaUrl,
            model.DisplayOrder,
            model.StartDateUtc,
            model.EndDateUtc,
            model.MediaAssetId == Guid.Empty ? null : model.MediaAssetId,
            CurrentUserId);

        if (model.IsPublished)
        {
            banner.Publish(CurrentUserId);
        }
        else
        {
            banner.Unpublish(CurrentUserId);
        }

        if (model.IsActive)
        {
            banner.Activate();
        }
        else
        {
            banner.Deactivate();
        }

        await _db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Banner updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Delete/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var banner = await _db.Banners
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (banner is null)
        {
            return NotFound();
        }

        _db.Banners.Remove(banner);
        await _db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Banner removed successfully.";
        return RedirectToAction(nameof(Index));
    }
}
