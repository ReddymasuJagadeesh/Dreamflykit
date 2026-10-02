using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Route("Admin/[controller]")]
public sealed class HomepageSectionsController : AdminBaseController
{
    private readonly DreamflykitDbContext _db;
    private readonly IMediaUrlService _urlService;

    public HomepageSectionsController(
        DreamflykitDbContext db,
        IMediaUrlService urlService)
    {
        _db = db;
        _urlService = urlService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var sections = await _db.HomepageSections
            .AsNoTracking()
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync(cancellationToken);

        var mediaIds = sections.Where(s => s.MediaAssetId.HasValue)
            .Select(s => s.MediaAssetId!.Value)
            .Distinct()
            .ToList();

        var mediaMap = await _db.MediaAssets
            .AsNoTracking()
            .Where(m => mediaIds.Contains(m.Id))
            .ToDictionaryAsync(m => m.Id, m => _urlService.GetUrl(m.ObjectKey), cancellationToken);

        var dtoList = sections.Select(s => new HomepageSectionItemDto
        {
            Id = s.Id,
            SectionKey = s.SectionKey.ToString(),
            MediaAssetId = s.MediaAssetId,
            ImageUrl = s.MediaAssetId.HasValue && mediaMap.TryGetValue(s.MediaAssetId.Value, out var url) ? url : null,
            Title = s.Title,
            Subtitle = s.Subtitle,
            DisplayOrder = s.DisplayOrder,
            IsActive = s.IsActive,
            IsPublished = s.IsPublished
        }).ToList();

        return View(new HomepageSectionListViewModel { Sections = dtoList });
    }

    [HttpGet("Edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var section = await _db.HomepageSections
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (section is null)
        {
            return NotFound();
        }

        string? currentImageUrl = null;
        if (section.MediaAssetId.HasValue)
        {
            var media = await _db.MediaAssets
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == section.MediaAssetId.Value, cancellationToken);

            if (media is not null)
            {
                currentImageUrl = _urlService.GetUrl(media.ObjectKey);
            }
        }

        var model = new HomepageSectionEditViewModel
        {
            Id = section.Id,
            SectionKey = section.SectionKey.ToString(),
            MediaAssetId = section.MediaAssetId,
            CurrentImageUrl = currentImageUrl,
            Title = section.Title,
            Subtitle = section.Subtitle,
            DisplayOrder = section.DisplayOrder,
            IsActive = section.IsActive,
            IsPublished = section.IsPublished
        };

        return View(model);
    }

    [HttpPost("Edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        Guid id,
        [FromForm] HomepageSectionEditViewModel model,
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

        var section = await _db.HomepageSections
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (section is null)
        {
            return NotFound();
        }

        section.Update(
            model.Title,
            model.Subtitle,
            null,
            model.DisplayOrder,
            model.MediaAssetId == Guid.Empty ? null : model.MediaAssetId,
            null,
            null,
            CurrentUserId);

        if (model.IsPublished)
        {
            section.Publish(CurrentUserId);
        }
        else
        {
            section.Unpublish(CurrentUserId);
        }

        if (model.IsActive)
        {
            section.Activate();
        }
        else
        {
            section.Deactivate();
        }

        await _db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Homepage section updated successfully.";
        return RedirectToAction(nameof(Index));
    }
}
