using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Route("Admin/[controller]")]
public sealed class HeaderAdsController : AdminBaseController
{
    private readonly DreamflykitDbContext _db;
    private readonly IMediaService _mediaService;
    private readonly IMediaUrlService _urlService;
    private readonly IWebHostEnvironment _env;

    public HeaderAdsController(
        DreamflykitDbContext db,
        IMediaService mediaService,
        IMediaUrlService urlService,
        IWebHostEnvironment env)
    {
        _db = db;
        _mediaService = mediaService;
        _urlService = urlService;
        _env = env;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var sections = await _db.HomepageSections
            .AsNoTracking()
            .Where(s => s.SectionKey == HomepageSectionKey.HeaderAdSlot1 || s.SectionKey == HomepageSectionKey.HeaderAdSlot2)
            .ToListAsync(cancellationToken);

        var mediaIds = sections
            .Where(s => s.MediaAssetId.HasValue)
            .Select(s => s.MediaAssetId!.Value)
            .Distinct()
            .ToList();

        var mediaMap = mediaIds.Count > 0
            ? await _db.MediaAssets
                .AsNoTracking()
                .Where(m => mediaIds.Contains(m.Id))
                .ToDictionaryAsync(m => m.Id, m => _urlService.GetUrl(m.ObjectKey), cancellationToken)
            : new Dictionary<Guid, string>();

        var s1 = sections.FirstOrDefault(s => s.SectionKey == HomepageSectionKey.HeaderAdSlot1);
        var s2 = sections.FirstOrDefault(s => s.SectionKey == HomepageSectionKey.HeaderAdSlot2);

        var model = new HeaderAdsManagementViewModel
        {
            Slot1 = new HeaderAdEditItem
            {
                SlotNumber = 1,
                Id = s1?.Id,
                MediaAssetId = s1?.MediaAssetId,
                CurrentImageUrl = s1?.MediaAssetId.HasValue == true && mediaMap.TryGetValue(s1.MediaAssetId.Value, out var u1)
                    ? u1
                    : (!string.IsNullOrWhiteSpace(s1?.Subtitle) && s1.Subtitle.StartsWith("http") ? s1.Subtitle : null),
                Title = s1?.Title ?? "Flight & Travel Kit Specials",
                Subtitle = s1?.Subtitle ?? "Up to 30% Off Study Bundles",
                RedirectUrl = s1?.LinkUrl ?? "/Kits/FindMyKit",
                IsActive = s1?.IsActive ?? true
            },
            Slot2 = new HeaderAdEditItem
            {
                SlotNumber = 2,
                Id = s2?.Id,
                MediaAssetId = s2?.MediaAssetId,
                CurrentImageUrl = s2?.MediaAssetId.HasValue == true && mediaMap.TryGetValue(s2.MediaAssetId.Value, out var u2)
                    ? u2
                    : (!string.IsNullOrWhiteSpace(s2?.Subtitle) && s2.Subtitle.StartsWith("http") ? s2.Subtitle : null),
                Title = s2?.Title ?? "International SIM & Adapters",
                Subtitle = s2?.Subtitle ?? "Pre-activated 5G abroad SIM",
                RedirectUrl = s2?.LinkUrl ?? "/category/adapters-sim",
                IsActive = s2?.IsActive ?? true
            }
        };

        return View(model);
    }

    [HttpPost("SaveSlot")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveSlot(
        [FromForm] HeaderAdEditItem model,
        CancellationToken cancellationToken)
    {
        if (model.SlotNumber is not 1 and not 2)
        {
            return BadRequest("Invalid slot number.");
        }

        // Validate redirect URL for security
        var cleanUrl = SanitizeUrl(model.RedirectUrl);
        if (string.IsNullOrWhiteSpace(cleanUrl))
        {
            ModelState.AddModelError(nameof(model.RedirectUrl), "Please enter a valid URL (starting with https://, http://, or /).");
        }

        var key = model.SlotNumber == 1
            ? HomepageSectionKey.HeaderAdSlot1
            : HomepageSectionKey.HeaderAdSlot2;

        var section = await _db.HomepageSections
            .FirstOrDefaultAsync(s => s.SectionKey == key, cancellationToken);

        Guid? uploadedAssetId = null;

        // Handle Image File Upload if provided
        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            try
            {
                await using var stream = model.ImageFile.OpenReadStream();
                var uploadRequest = new MediaUploadRequest
                {
                    FileStream = stream,
                    OriginalFileName = model.ImageFile.FileName,
                    ContentType = model.ImageFile.ContentType,
                    MediaType = MediaType.Banner,
                    OwnerId = section?.Id ?? Guid.NewGuid(),
                    AltText = model.Title,
                    UploadedBy = CurrentUserId
                };

                var uploadResult = await _mediaService.UploadAsync(uploadRequest, cancellationToken);
                if (uploadResult.IsSuccess && uploadResult.Asset != null)
                {
                    uploadedAssetId = uploadResult.Asset.Id;
                }
            }
            catch
            {
                // Fallback: save to local wwwroot/uploads/banners if S3 is unconfigured locally
                var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "banners");
                Directory.CreateDirectory(uploadsDir);
                var safeFileName = $"ad-slot-{model.SlotNumber}-{Guid.NewGuid():N}{Path.GetExtension(model.ImageFile.FileName)}";
                var filePath = Path.Combine(uploadsDir, safeFileName);
                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await model.ImageFile.CopyToAsync(fileStream, cancellationToken);
                model.Subtitle = $"/uploads/banners/{safeFileName}";
            }
        }

        if (section == null)
        {
            section = new HomepageSection(
                key,
                model.Title,
                model.SlotNumber + 7,
                CurrentUserId);

            section.Update(
                model.Title,
                model.Subtitle,
                cleanUrl,
                model.SlotNumber + 7,
                uploadedAssetId ?? model.MediaAssetId,
                null,
                null,
                CurrentUserId);

            section.Publish(CurrentUserId);
            if (!model.IsActive) section.Deactivate();

            _db.HomepageSections.Add(section);
        }
        else
        {
            var assetIdToUse = uploadedAssetId ?? (model.MediaAssetId == Guid.Empty ? null : model.MediaAssetId) ?? section.MediaAssetId;

            section.Update(
                model.Title,
                model.Subtitle,
                cleanUrl,
                section.DisplayOrder,
                assetIdToUse,
                null,
                null,
                CurrentUserId);

            section.Publish(CurrentUserId);

            if (model.IsActive)
                section.Activate();
            else
                section.Deactivate();
        }

        await _db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Header Ad Slot {model.SlotNumber} has been updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    private static string? SanitizeUrl(string? rawUrl)
    {
        if (string.IsNullOrWhiteSpace(rawUrl)) return null;

        var trimmed = rawUrl.Trim();

        // Disallow dangerous schemes
        if (trimmed.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("data:", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("vbscript:", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (trimmed.StartsWith('/') && !trimmed.StartsWith("//"))
        {
            return trimmed;
        }

        if (Uri.TryCreate(trimmed, UriKind.Absolute, out var uri) &&
            (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
        {
            return trimmed;
        }

        // If user typed 'www.example.com', prefix https://
        if (trimmed.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
        {
            return "https://" + trimmed;
        }

        return null;
    }
}
