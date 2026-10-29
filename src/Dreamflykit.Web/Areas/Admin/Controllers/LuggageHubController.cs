using Dreamflykit.Application.LuggageHub.Interfaces;
using Dreamflykit.Application.LuggageHub.Models;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
[Route("Admin/[controller]")]
public sealed class LuggageHubController : AdminBaseController
{
    private readonly ILuggageHubService _luggageHubService;
    private readonly IMediaService _mediaService;
    private readonly IMediaUrlService _urlService;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<LuggageHubController> _logger;

    private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".svg", ".gif" };
    private const long MaxImageSizeBytes = 5 * 1024 * 1024; // 5MB

    public LuggageHubController(
        ILuggageHubService luggageHubService,
        IMediaService mediaService,
        IMediaUrlService urlService,
        IWebHostEnvironment env,
        ILogger<LuggageHubController> logger)
    {
        _luggageHubService = luggageHubService;
        _mediaService = mediaService;
        _urlService = urlService;
        _env = env;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Luggage & Bags Category Hub Management";
        var config = await _luggageHubService.GetConfigAsync(cancellationToken);
        return View(config);
    }

    [HttpPost("Save")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(
        [FromBody] LuggageHubDto? jsonModel,
        [FromForm] LuggageHubDto? formModel,
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
            await _luggageHubService.UpdateConfigAsync(model, CurrentUserId.ToString(), cancellationToken);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" || Request.ContentType?.Contains("application/json") == true)
            {
                return Json(new { success = true, message = "Luggage & Bags Category Hub settings saved successfully!" });
            }

            TempData["SuccessMessage"] = "Luggage & Bags Category Hub settings saved successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update Luggage Hub configuration.");
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" || Request.ContentType?.Contains("application/json") == true)
            {
                return Json(new { success = false, message = "Failed to save configuration: " + ex.Message });
            }
            TempData["ErrorMessage"] = "Failed to save configuration: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost("ResetToDefault")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetToDefault(CancellationToken cancellationToken)
    {
        try
        {
            var def = LuggageHubDto.CreateDefault();
            await _luggageHubService.UpdateConfigAsync(def, CurrentUserId.ToString(), cancellationToken);
            TempData["SuccessMessage"] = "Luggage & Bags Hub has been reset to default configuration!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset Luggage Hub configuration.");
            TempData["ErrorMessage"] = "Failed to reset configuration: " + ex.Message;
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
            return Json(new { success = false, message = "No file was selected for upload." });
        }

        if (file.Length > MaxImageSizeBytes)
        {
            return Json(new { success = false, message = $"Image size exceeds limit of {MaxImageSizeBytes / (1024 * 1024)} MB." });
        }

        var ext = Path.GetExtension(file.FileName);
        if (string.IsNullOrWhiteSpace(ext) || !AllowedImageExtensions.Contains(ext.ToLowerInvariant()))
        {
            return Json(new { success = false, message = "Invalid file type. Allowed formats: JPG, PNG, WEBP, SVG, GIF." });
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
                AltText = "Luggage Hub Asset",
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
                    message = "Image uploaded successfully via Media Library!"
                });
            }

            // Fallback to local storage in wwwroot/images/luggage-hub
            var rootPath = !string.IsNullOrWhiteSpace(_env.WebRootPath)
                ? _env.WebRootPath
                : Path.Combine(AppContext.BaseDirectory, "wwwroot");

            var uploadsDir = Path.Combine(rootPath, "images", "luggage-hub");
            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            var safeFileName = $"luggage_{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}{ext.ToLowerInvariant()}";
            var fullPath = Path.Combine(uploadsDir, safeFileName);

            await using var outStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            stream.Position = 0;
            await stream.CopyToAsync(outStream, cancellationToken);

            var relativeUrl = $"/images/luggage-hub/{safeFileName}";
            return Json(new
            {
                success = true,
                url = relativeUrl,
                targetField = targetField,
                message = "Image uploaded successfully to Luggage Hub storage!"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload Luggage Hub image.");
            return Json(new { success = false, message = "Upload failed: " + ex.Message });
        }
    }
}

