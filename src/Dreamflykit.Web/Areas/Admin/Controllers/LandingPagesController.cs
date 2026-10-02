using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Route("Admin/[controller]")]
public sealed class LandingPagesController : AdminBaseController
{
    private readonly DreamflykitDbContext _db;
    private readonly IMediaUrlService _urlService;

    public LandingPagesController(
        DreamflykitDbContext db,
        IMediaUrlService urlService)
    {
        _db = db;
        _urlService = urlService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var pages = await _db.LandingPages
            .AsNoTracking()
            .OrderByDescending(p => p.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        var mediaIds = pages.Where(p => p.HeroMediaAssetId.HasValue)
            .Select(p => p.HeroMediaAssetId!.Value)
            .Distinct()
            .ToList();

        var mediaMap = await _db.MediaAssets
            .AsNoTracking()
            .Where(m => mediaIds.Contains(m.Id))
            .ToDictionaryAsync(m => m.Id, m => _urlService.GetUrl(m.ObjectKey), cancellationToken);

        var dtoList = pages.Select(p => new LandingPageItemDto
        {
            Id = p.Id,
            Title = p.Title,
            Slug = p.Slug,
            HeroMediaAssetId = p.HeroMediaAssetId,
            HeroImageUrl = p.HeroMediaAssetId.HasValue && mediaMap.TryGetValue(p.HeroMediaAssetId.Value, out var url) ? url : null,
            Status = p.Status.ToString(),
            StartDateUtc = p.StartDateUtc,
            EndDateUtc = p.EndDateUtc
        }).ToList();

        return View(new LandingPageListViewModel { LandingPages = dtoList });
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View(new LandingPageEditViewModel());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [FromForm] LandingPageEditViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var slugExists = await _db.LandingPages
            .AnyAsync(lp => lp.Slug == model.Slug.Trim().ToLowerInvariant(), cancellationToken);

        if (slugExists)
        {
            ModelState.AddModelError(nameof(model.Slug), "A landing page with this slug already exists.");
            return View(model);
        }

        var landingPage = new LandingPage(
            model.Title,
            model.Slug,
            CurrentUserId);

        landingPage.Update(
            model.Title,
            model.Slug,
            null,
            model.HeroMediaAssetId == Guid.Empty ? null : model.HeroMediaAssetId,
            null,
            null,
            model.StartDateUtc,
            model.EndDateUtc,
            CurrentUserId);

        if (model.Status == "Published")
        {
            landingPage.Publish(CurrentUserId);
        }

        _db.LandingPages.Add(landingPage);
        await _db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Landing page created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var page = await _db.LandingPages
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (page is null)
        {
            return NotFound();
        }

        string? heroUrl = null;
        if (page.HeroMediaAssetId.HasValue)
        {
            var media = await _db.MediaAssets
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == page.HeroMediaAssetId.Value, cancellationToken);

            if (media is not null)
            {
                heroUrl = _urlService.GetUrl(media.ObjectKey);
            }
        }

        var model = new LandingPageEditViewModel
        {
            Id = page.Id,
            Title = page.Title,
            Slug = page.Slug,
            HeroMediaAssetId = page.HeroMediaAssetId,
            CurrentHeroImageUrl = heroUrl,
            Status = page.Status.ToString(),
            StartDateUtc = page.StartDateUtc,
            EndDateUtc = page.EndDateUtc
        };

        return View(model);
    }

    [HttpPost("Edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        Guid id,
        [FromForm] LandingPageEditViewModel model,
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

        var page = await _db.LandingPages
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (page is null)
        {
            return NotFound();
        }

        var slugExists = await _db.LandingPages
            .AnyAsync(lp => lp.Slug == model.Slug.Trim().ToLowerInvariant() && lp.Id != id, cancellationToken);

        if (slugExists)
        {
            ModelState.AddModelError(nameof(model.Slug), "Another landing page with this slug already exists.");
            return View(model);
        }

        page.Update(
            model.Title,
            model.Slug,
            null,
            model.HeroMediaAssetId == Guid.Empty ? null : model.HeroMediaAssetId,
            null,
            null,
            model.StartDateUtc,
            model.EndDateUtc,
            CurrentUserId);

        if (model.Status == "Published")
        {
            page.Publish(CurrentUserId);
        }
        else if (model.Status == "Archived")
        {
            page.Archive(CurrentUserId);
        }
        else
        {
            page.Unpublish(CurrentUserId);
        }

        await _db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Landing page updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Delete/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var page = await _db.LandingPages
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (page is null)
        {
            return NotFound();
        }

        _db.LandingPages.Remove(page);
        await _db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Landing page deleted.";
        return RedirectToAction(nameof(Index));
    }
}
