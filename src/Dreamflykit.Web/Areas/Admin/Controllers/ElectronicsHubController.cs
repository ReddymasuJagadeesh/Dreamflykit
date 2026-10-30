using Dreamflykit.Application.ElectronicsHub.Interfaces;
using Dreamflykit.Application.ElectronicsHub.Models;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
[Route("Admin/[controller]")]
public sealed class ElectronicsHubController : AdminBaseController
{
    private readonly IElectronicsHubService _electronicsHubService;
    private readonly IMediaService _mediaService;
    private readonly IMediaUrlService _urlService;
    private readonly ILogger<ElectronicsHubController> _logger;

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp", ".svg"
    };

    private const long MaxFileSizeBytes = 8 * 1024 * 1024; // 8 MB

    public ElectronicsHubController(
        IElectronicsHubService electronicsHubService,
        IMediaService mediaService,
        IMediaUrlService urlService,
        ILogger<ElectronicsHubController> logger)
    {
        _electronicsHubService = electronicsHubService;
        _mediaService = mediaService;
        _urlService = urlService;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Electronics Category Hub Management";
        var config = await _electronicsHubService.GetConfigAsync(cancellationToken);
        return View(config);
    }

    [HttpPost("Save")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(
        [FromBody] ElectronicsHubDto? jsonModel,
        [FromForm] ElectronicsHubDto? formModel,
        CancellationToken cancellationToken)
    {
        var model = jsonModel ?? formModel;
        if (model == null)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" || Request.ContentType?.Contains("application/json") == true)
            {
                return Json(new { success = false, message = "Invalid data payload received." });
            }
            TempData["ErrorMessage"] = "Invalid data payload received.";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            await _electronicsHubService.UpdateConfigAsync(model, CurrentUserId, cancellationToken);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" || Request.ContentType?.Contains("application/json") == true)
            {
                return Json(new { success = true, message = "Electronics Category Hub settings saved successfully!" });
            }

            TempData["SuccessMessage"] = "Electronics Category Hub settings saved successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update Electronics Category Hub configuration.");
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" || Request.ContentType?.Contains("application/json") == true)
            {
                return Json(new { success = false, message = "Failed to save configuration: " + ex.Message });
            }
            TempData["ErrorMessage"] = "Failed to save configuration: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost("UploadImage")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadImage(
        IFormFile? file,
        [FromForm] string? targetField,
        CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
        {
            return Json(new { success = false, message = "No image file provided." });
        }

        if (file.Length > MaxFileSizeBytes)
        {
            return Json(new { success = false, message = "Image exceeds maximum allowed size of 8 MB." });
        }

        var ext = Path.GetExtension(file.FileName);
        if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
        {
            return Json(new { success = false, message = "Invalid image type. Allowed: JPG, PNG, WEBP, SVG." });
        }

        try
        {
            await using var stream = file.OpenReadStream();
            var uploadRequest = new MediaUploadRequest
            {
                FileStream = stream,
                OriginalFileName = file.FileName,
                ContentType = file.ContentType,
                MediaType = Dreamflykit.Domain.Media.MediaType.Banner,
                OwnerId = CurrentUserId,
                AltText = "Electronics Hub Asset",
                UploadedBy = CurrentUserId
            };

            var uploadResult = await _mediaService.UploadAsync(uploadRequest, cancellationToken);
            if (uploadResult.IsSuccess && uploadResult.Asset != null)
            {
                var url = _urlService.GetUrl(uploadResult.Asset.ObjectKey);
                return Json(new
                {
                    success = true,
                    url = url,
                    mediaId = uploadResult.Asset.Id,
                    targetField = targetField,
                    message = "Image uploaded successfully!"
                });
            }

            return Json(new { success = false, message = uploadResult.ErrorMessage ?? "Failed to upload image." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload image for Electronics Hub.");
            return Json(new { success = false, message = "Upload failed: " + ex.Message });
        }
    }

    [HttpPost("ResetDefaults")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetDefaults(CancellationToken cancellationToken)
    {
        try
        {
            await _electronicsHubService.ResetToDefaultAsync(cancellationToken);
            TempData["SuccessMessage"] = "Electronics Category Hub restored to default template.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset Electronics Hub to defaults.");
            TempData["ErrorMessage"] = "Failed to reset defaults: " + ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}

