using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
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
    private readonly IMediaService _mediaService;
    private readonly IWebHostEnvironment _env;

    public BannersController(
        DreamflykitDbContext db,
        IMediaUrlService urlService,
        IMediaService mediaService,
        IWebHostEnvironment env)
    {
        _db = db;
        _urlService = urlService;
        _mediaService = mediaService;
        _env = env;
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

        var now = DateTime.UtcNow;
        int liveHeroCounter = 0;

        var dtoList = banners.Select(b =>
        {
            var isEligible = b.IsActive && b.IsPublished &&
                             (!b.StartDateUtc.HasValue || b.StartDateUtc.Value <= now) &&
                             (!b.EndDateUtc.HasValue || b.EndDateUtc.Value >= now) &&
                             b.MediaAssetId.HasValue;

            bool isLiveInHero = false;
            if (isEligible && liveHeroCounter < 12)
            {
                isLiveInHero = true;
                liveHeroCounter++;
            }

            return new BannerItemDto
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
                EndDateUtc = b.EndDateUtc,
                IsLiveInHero = isLiveInHero
            };
        }).ToList();

        var (duration, autoPlay) = GetSliderSettings();

        return View(new BannerListViewModel
        {
            Banners = dtoList,
            MaxLiveSlides = 12,
            ActiveLiveCount = liveHeroCounter,
            SlideDurationSeconds = duration,
            AutoPlay = autoPlay
        });
    }

    [HttpPost("UpdateTimeframe")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateTimeframe([FromForm] int slideDurationSeconds, [FromForm] bool autoPlay)
    {
        var clamped = Math.Clamp(slideDurationSeconds, 2, 30);
        SaveSliderSettings(clamped, autoPlay);
        TempData["SuccessMessage"] = $"Homepage hero slide timeframe set to {clamped} seconds (Auto-play: {(autoPlay ? "Enabled" : "Disabled")}).";
        return RedirectToAction(nameof(Index));
    }

    private string GetSettingsFilePath()
    {
        return Path.Combine(_env.WebRootPath, "data", "hero_slider_settings.json");
    }

    private (int Duration, bool AutoPlay) GetSliderSettings()
    {
        try
        {
            var path = GetSettingsFilePath();
            if (System.IO.File.Exists(path))
            {
                var json = System.IO.File.ReadAllText(path);
                using var doc = System.Text.Json.JsonDocument.Parse(json);
                var duration = doc.RootElement.TryGetProperty("SlideDurationSeconds", out var dProp) ? dProp.GetInt32() : 5;
                var autoPlay = doc.RootElement.TryGetProperty("AutoPlay", out var aProp) ? aProp.GetBoolean() : true;
                return (Math.Clamp(duration, 2, 30), autoPlay);
            }
        }
        catch
        {
            // fallback
        }
        return (5, true);
    }

    private void SaveSliderSettings(int duration, bool autoPlay)
    {
        try
        {
            var path = GetSettingsFilePath();
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var data = new
            {
                SlideDurationSeconds = Math.Clamp(duration, 2, 30),
                AutoPlay = autoPlay,
                UpdatedAtUtc = DateTime.UtcNow
            };
            var json = System.Text.Json.JsonSerializer.Serialize(data, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(path, json);
        }
        catch
        {
            // ignore
        }
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var maxOrder = await _db.Banners.MaxAsync(b => (int?)b.DisplayOrder, cancellationToken) ?? 0;
        return View(new BannerEditViewModel
        {
            DisplayOrder = maxOrder + 1,
            IsActive = true,
            IsPublished = true,
            CtaText = "Shop Now",
            CtaUrl = "/Kits/FindMyKit"
        });
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [FromForm] BannerEditViewModel model,
        CancellationToken cancellationToken)
    {
        // 1. Direct Image File Upload if provided
        if (model.ImageFile is { Length: > 0 })
        {
            await using var stream = model.ImageFile.OpenReadStream();
            var uploadRequest = new MediaUploadRequest
            {
                FileStream = stream,
                OriginalFileName = model.ImageFile.FileName,
                ContentType = model.ImageFile.ContentType,
                MediaType = MediaType.Banner,
                OwnerId = Guid.NewGuid(),
                AltText = model.Title,
                UploadedBy = CurrentUserId
            };

            var uploadResult = await _mediaService.UploadAsync(uploadRequest, cancellationToken);
            if (!uploadResult.IsSuccess)
            {
                ModelState.AddModelError(nameof(model.ImageFile), uploadResult.ErrorMessage ?? "Image upload failed.");
                return View(model);
            }

            model.MediaAssetId = uploadResult.Asset!.Id;
        }

        if (model.MediaAssetId == Guid.Empty)
        {
            ModelState.AddModelError(nameof(model.ImageFile), "Please upload a slide banner image or choose one from the Media Library.");
        }

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

        TempData["SuccessMessage"] = "Homepage slide created successfully.";
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

        var banner = await _db.Banners
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (banner is null)
        {
            return NotFound();
        }

        // Direct Image File Upload if a new file was provided
        if (model.ImageFile is { Length: > 0 })
        {
            await using var stream = model.ImageFile.OpenReadStream();
            var uploadRequest = new MediaUploadRequest
            {
                FileStream = stream,
                OriginalFileName = model.ImageFile.FileName,
                ContentType = model.ImageFile.ContentType,
                MediaType = MediaType.Banner,
                OwnerId = banner.Id,
                AltText = model.Title,
                UploadedBy = CurrentUserId
            };

            var uploadResult = await _mediaService.UploadAsync(uploadRequest, cancellationToken);
            if (!uploadResult.IsSuccess)
            {
                ModelState.AddModelError(nameof(model.ImageFile), uploadResult.ErrorMessage ?? "Image upload failed.");
                return View(model);
            }

            model.MediaAssetId = uploadResult.Asset!.Id;
        }
        else if (model.MediaAssetId == Guid.Empty && banner.MediaAssetId.HasValue)
        {
            // Retain previous image
            model.MediaAssetId = banner.MediaAssetId.Value;
        }

        if (model.MediaAssetId == Guid.Empty)
        {
            ModelState.AddModelError(nameof(model.ImageFile), "Please upload an image or choose one from the Media Library.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
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

        TempData["SuccessMessage"] = "Homepage slide updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("ToggleActive/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(Guid id, CancellationToken cancellationToken)
    {
        var banner = await _db.Banners.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        if (banner is null) return NotFound();

        if (banner.IsActive)
        {
            banner.Deactivate();
        }
        else
        {
            banner.Activate();
        }

        await _db.SaveChangesAsync(cancellationToken);
        TempData["SuccessMessage"] = $"Slide '{banner.Title}' is now {(banner.IsActive ? "Active" : "Inactive")}.";
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

        TempData["SuccessMessage"] = "Homepage slide removed successfully.";
        return RedirectToAction(nameof(Index));
    }
}
