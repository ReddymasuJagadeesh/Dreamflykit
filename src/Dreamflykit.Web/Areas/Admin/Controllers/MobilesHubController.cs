using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Application.MobilesHub.Interfaces;
using Dreamflykit.Application.MobilesHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
[Route("Admin/[controller]")]
public sealed class MobilesHubController : AdminBaseController
{
    private readonly IMobilesHubService _mobilesHubService;
    private readonly IMediaService _mediaService;
    private readonly IMediaUrlService _urlService;
    private readonly ILogger<MobilesHubController> _logger;

    private static readonly HashSet<string> AllowedImageExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp", ".svg"
    };

    private static readonly HashSet<string> Allowed3dExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".glb", ".gltf", ".bin"
    };

    private const long MaxImageSizeBytes = 8 * 1024 * 1024; // 8 MB
    private const long Max3dSizeBytes = 30 * 1024 * 1024; // 30 MB for 3D GLB/GLTF models

    public MobilesHubController(
        IMobilesHubService mobilesHubService,
        IMediaService mediaService,
        IMediaUrlService urlService,
        ILogger<MobilesHubController> logger)
    {
        _mobilesHubService = mobilesHubService;
        _mediaService = mediaService;
        _urlService = urlService;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Mobiles Flipkart Template & 3D Visualizer Management";
        var config = await _mobilesHubService.GetConfigAsync(cancellationToken);
        return View(config);
    }

    [HttpPost("Save")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(
        [FromBody] MobilesHubDto? jsonModel,
        [FromForm] MobilesHubDto? formModel,
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
            await _mobilesHubService.UpdateConfigAsync(model, CurrentUserId, cancellationToken);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" || Request.ContentType?.Contains("application/json") == true)
            {
                return Json(new { success = true, message = "Mobiles Hub template & 3D settings saved successfully!" });
            }

            TempData["SuccessMessage"] = "Mobiles Hub template & 3D settings saved successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update Mobiles Hub configuration.");
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

        if (file.Length > MaxImageSizeBytes)
        {
            return Json(new { success = false, message = "Image exceeds maximum allowed size of 8 MB." });
        }

        var ext = Path.GetExtension(file.FileName);
        if (string.IsNullOrEmpty(ext) || !AllowedImageExtensions.Contains(ext))
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
                AltText = "Mobiles Hub Asset",
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
            _logger.LogError(ex, "Failed to upload image for Mobiles Hub.");
            return Json(new { success = false, message = "Upload failed: " + ex.Message });
        }
    }

    [HttpPost("Upload3DModel")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload3DModel(
        IFormFile? file,
        [FromForm] string? targetField,
        CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
        {
            return Json(new { success = false, message = "No 3D model file provided." });
        }

        if (file.Length > Max3dSizeBytes)
        {
            return Json(new { success = false, message = "3D model exceeds maximum allowed size of 30 MB." });
        }

        var ext = Path.GetExtension(file.FileName);
        if (string.IsNullOrEmpty(ext) || !Allowed3dExtensions.Contains(ext))
        {
            return Json(new { success = false, message = "Invalid 3D model type. Allowed: .GLB, .GLTF." });
        }

        try
        {
            await using var stream = file.OpenReadStream();
            var uploadRequest = new MediaUploadRequest
            {
                FileStream = stream,
                OriginalFileName = file.FileName,
                ContentType = file.ContentType ?? "model/gltf-binary",
                MediaType = Dreamflykit.Domain.Media.MediaType.Product,
                OwnerId = CurrentUserId,
                AltText = "3D Smartphone Model",
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
                    message = "3D model uploaded successfully!"
                });
            }

            return Json(new { success = false, message = uploadResult.ErrorMessage ?? "Failed to upload 3D model." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload 3D model for Mobiles Hub.");
            return Json(new { success = false, message = "Upload failed: " + ex.Message });
        }
    }

    [HttpPost("ResetDefaults")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetDefaults(CancellationToken cancellationToken)
    {
        try
        {
            await _mobilesHubService.ResetToDefaultAsync(cancellationToken);
            TempData["SuccessMessage"] = "Mobiles Hub restored to default Flipkart template with 3D models.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset Mobiles Hub to defaults.");
            TempData["ErrorMessage"] = "Failed to reset Mobiles Hub: " + ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
