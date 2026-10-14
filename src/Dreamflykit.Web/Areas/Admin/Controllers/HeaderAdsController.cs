using System.Text.Json;
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

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp", ".svg", ".gif"
    };

    private static readonly HashSet<string> AllowedMimeTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg", "image/png", "image/webp", "image/svg+xml", "image/gif"
    };

    private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

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
        // Find existing HeaderAdSpace or legacy slot 1
        var section = await _db.HomepageSections
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SectionKey == HomepageSectionKey.HeaderAdSpace, cancellationToken);

        if (section == null)
        {
            section = await _db.HomepageSections
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SectionKey == HomepageSectionKey.HeaderAdSlot1, cancellationToken);
        }

        string? imgUrl = null;
        if (section?.MediaAssetId.HasValue == true)
        {
            var asset = await _db.MediaAssets
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == section.MediaAssetId.Value, cancellationToken);
            if (asset != null)
            {
                imgUrl = _urlService.GetUrl(asset.ObjectKey);
            }
        }

        var subtitle = section?.Subtitle;
        var badgeText = "SPECIAL OFFER";
        var displayMode = "Hybrid";
        var openInNewTab = false;

        if (!string.IsNullOrWhiteSpace(subtitle) && subtitle.TrimStart().StartsWith("{"))
        {
            try
            {
                using var doc = JsonDocument.Parse(subtitle);
                var root = doc.RootElement;
                if (root.TryGetProperty("sub", out var sProp)) subtitle = sProp.GetString();
                if (root.TryGetProperty("badge", out var bProp)) badgeText = bProp.GetString() ?? "SPECIAL OFFER";
                if (root.TryGetProperty("mode", out var mProp)) displayMode = mProp.GetString() ?? "Hybrid";
                if (root.TryGetProperty("blank", out var blProp)) openInNewTab = blProp.GetBoolean();
                if (root.TryGetProperty("img", out var imgProp) && string.IsNullOrWhiteSpace(imgUrl))
                {
                    imgUrl = imgProp.GetString();
                }
            }
            catch
            {
                // Fallback to raw subtitle
            }
        }
        else if (!string.IsNullOrWhiteSpace(subtitle) && subtitle.StartsWith("/uploads/", StringComparison.OrdinalIgnoreCase))
        {
            imgUrl = subtitle;
            subtitle = null;
        }

        var model = new HeaderAdSpaceViewModel
        {
            Id = section?.Id,
            MediaAssetId = section?.MediaAssetId,
            CurrentImageUrl = imgUrl,
            Title = string.IsNullOrWhiteSpace(section?.Title) ? "Dreamflykit Special Offers" : section.Title,
            Subtitle = subtitle ?? "Exclusive student travel deals & partner perks",
            BadgeText = badgeText,
            RedirectUrl = string.IsNullOrWhiteSpace(section?.LinkUrl) ? "/SpecialOffer" : section.LinkUrl,
            DisplayMode = displayMode,
            OpenInNewTab = openInNewTab,
            IsActive = section?.IsActive ?? true
        };

        return View(model);
    }

    [HttpPost("Save")]
    [HttpPost("SaveSlot")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(
        [FromForm] HeaderAdSpaceViewModel model,
        CancellationToken cancellationToken)
    {
        // 1. Sanitize & validate redirect URL for Open Redirect & XSS security
        var cleanUrl = SanitizeUrl(model.RedirectUrl);
        if (string.IsNullOrWhiteSpace(cleanUrl))
        {
            ModelState.AddModelError(nameof(model.RedirectUrl), "Please enter a valid, safe URL (e.g. /SpecialOffer or https://...).");
        }

        // 2. Validate file upload if provided
        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            if (model.ImageFile.Length > MaxFileSizeBytes)
            {
                ModelState.AddModelError(nameof(model.ImageFile), "Image file size exceeds the 5MB limit.");
            }

            var extension = Path.GetExtension(model.ImageFile.FileName);
            if (string.IsNullOrEmpty(extension) || !AllowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(nameof(model.ImageFile), "Only image files (.jpg, .jpeg, .png, .webp, .svg, .gif) are allowed.");
            }

            if (!AllowedMimeTypes.Contains(model.ImageFile.ContentType))
            {
                ModelState.AddModelError(nameof(model.ImageFile), "Invalid image MIME type.");
            }

            if (!await ValidateFileHeaderAsync(model.ImageFile, extension, cancellationToken))
            {
                ModelState.AddModelError(nameof(model.ImageFile), "File header validation failed. The file is not a valid image.");
            }
        }

        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        // 3. Purge legacy 2-slot records from database if any exist
        var legacySlots = await _db.HomepageSections
            .Where(s => s.SectionKey == HomepageSectionKey.HeaderAdSlot1 || s.SectionKey == HomepageSectionKey.HeaderAdSlot2)
            .ToListAsync(cancellationToken);
        if (legacySlots.Count > 0)
        {
            _db.HomepageSections.RemoveRange(legacySlots);
        }

        // 4. Find or create the HeaderAdSpace section
        var section = await _db.HomepageSections
            .FirstOrDefaultAsync(s => s.SectionKey == HomepageSectionKey.HeaderAdSpace, cancellationToken);

        Guid? uploadedAssetId = model.RemoveExistingImage ? null : model.MediaAssetId;
        string? localSavedImagePath = null;

        // 5. Handle Image Upload securely
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
                else
                {
                    localSavedImagePath = await SaveLocalImageFallbackAsync(model.ImageFile, cancellationToken);
                }
            }
            catch
            {
                localSavedImagePath = await SaveLocalImageFallbackAsync(model.ImageFile, cancellationToken);
            }
        }

        // 6. Encode structured metadata in JSON format
        var metadataObj = new
        {
            sub = model.Subtitle?.Trim(),
            badge = string.IsNullOrWhiteSpace(model.BadgeText) ? "SPECIAL OFFER" : model.BadgeText.Trim(),
            mode = string.IsNullOrWhiteSpace(model.DisplayMode) ? "Hybrid" : model.DisplayMode.Trim(),
            blank = model.OpenInNewTab,
            img = localSavedImagePath
        };
        var encodedMetadata = JsonSerializer.Serialize(metadataObj);

        var finalRedirect = cleanUrl ?? "/SpecialOffer";

        if (section == null)
        {
            section = new HomepageSection(
                HomepageSectionKey.HeaderAdSpace,
                model.Title,
                8,
                CurrentUserId);

            section.Update(
                model.Title,
                encodedMetadata,
                finalRedirect,
                8,
                uploadedAssetId,
                null,
                null,
                CurrentUserId);

            section.Publish(CurrentUserId);
            if (!model.IsActive) section.Deactivate();

            _db.HomepageSections.Add(section);
        }
        else
        {
            var assetIdToUse = model.RemoveExistingImage ? null : (uploadedAssetId ?? section.MediaAssetId);

            section.Update(
                model.Title,
                encodedMetadata,
                finalRedirect,
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

        TempData["SuccessMessage"] = "Header Ad Space has been updated and published successfully!";
        return RedirectToAction(nameof(Index));
    }

    private async Task<string> SaveLocalImageFallbackAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "ads");
        Directory.CreateDirectory(uploadsDir);

        var safeFileName = $"ad-space-{Guid.NewGuid():N}{Path.GetExtension(file.FileName).ToLowerInvariant()}";
        var filePath = Path.Combine(uploadsDir, safeFileName);

        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        await file.CopyToAsync(fileStream, cancellationToken);

        return $"/uploads/ads/{safeFileName}";
    }

    private static async Task<bool> ValidateFileHeaderAsync(IFormFile file, string extension, CancellationToken cancellationToken)
    {
        var ext = extension.ToLowerInvariant();
        if (ext == ".svg")
        {
            // For SVG verify it starts with XML declaration or <svg
            using var reader = new StreamReader(file.OpenReadStream());
            var buffer = new char[512];
            var read = await reader.ReadAsync(buffer, 0, buffer.Length);
            var content = new string(buffer, 0, read).Trim();
            return content.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase) ||
                   content.Contains("<svg", StringComparison.OrdinalIgnoreCase);
        }

        byte[] header = new byte[12];
        await using (var stream = file.OpenReadStream())
        {
            var read = await stream.ReadAsync(header, 0, header.Length, cancellationToken);
            if (read < 4) return false;
        }

        // JPEG: FF D8 FF
        if (ext is ".jpg" or ".jpeg")
        {
            return header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF;
        }

        // PNG: 89 50 4E 47
        if (ext == ".png")
        {
            return header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47;
        }

        // GIF: 47 49 46 38
        if (ext == ".gif")
        {
            return header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38;
        }

        // WEBP: RIFF .... WEBP
        if (ext == ".webp")
        {
            return header[0] == 0x52 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x46 &&
                   header[8] == 0x57 && header[9] == 0x45 && header[10] == 0x42 && header[11] == 0x50;
        }

        return true;
    }

    private static string? SanitizeUrl(string? rawUrl)
    {
        if (string.IsNullOrWhiteSpace(rawUrl)) return null;

        var trimmed = rawUrl.Trim();

        // Disallow dangerous schemes
        if (trimmed.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("data:", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("vbscript:", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        // Relative URLs starting with single / and not protocol-relative // or /\
        if (trimmed.StartsWith('/') && !trimmed.StartsWith("//") && !trimmed.StartsWith("/\\"))
        {
            return trimmed;
        }

        // Absolute safe URLs
        if (Uri.TryCreate(trimmed, UriKind.Absolute, out var uri) &&
            (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
        {
            return trimmed;
        }

        // Auto-fix www. URLs
        if (trimmed.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
        {
            return "https://" + trimmed;
        }

        return null;
    }
}
