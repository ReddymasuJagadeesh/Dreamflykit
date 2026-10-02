using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Controllers;

[Route("pages")]
public sealed class PagesController : Controller
{
    private readonly DreamflykitDbContext _db;
    private readonly IMediaUrlService _urlService;

    public PagesController(
        DreamflykitDbContext db,
        IMediaUrlService urlService)
    {
        _db = db;
        _urlService = urlService;
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> Detail(string slug, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return NotFound();
        }

        var normalizedSlug = slug.Trim().ToLowerInvariant();

        var page = await _db.LandingPages
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Slug == normalizedSlug, cancellationToken);

        if (page is null || page.Status != LandingPageStatus.Published)
        {
            return NotFound();
        }

        string? heroImageUrl = null;
        if (page.HeroMediaAssetId.HasValue)
        {
            var media = await _db.MediaAssets
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == page.HeroMediaAssetId.Value, cancellationToken);

            if (media is not null)
            {
                heroImageUrl = _urlService.GetUrl(media.ObjectKey);
            }
        }

        ViewData["Title"] = page.Title;
        ViewData["HeroImageUrl"] = heroImageUrl;

        return View(page);
    }
}
