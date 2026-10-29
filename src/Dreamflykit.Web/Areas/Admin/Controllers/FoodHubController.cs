using Dreamflykit.Application.FoodHub.Interfaces;
using Dreamflykit.Application.FoodHub.Models;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
[Route("Admin/[controller]")]
public sealed class FoodHubController : AdminBaseController
{
    private readonly IFoodHubService _foodHubService;
    private readonly IMediaService _mediaService;
    private readonly IMediaUrlService _urlService;
    private readonly ILogger<FoodHubController> _logger;
    private readonly IWebHostEnvironment _env;

    private static readonly HashSet<string> AllowedImageExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp", ".svg", ".gif"
    };

    private const long MaxImageSizeBytes = 10 * 1024 * 1024; // 10 MB

    public FoodHubController(
        IFoodHubService foodHubService,
        IMediaService mediaService,
        IMediaUrlService urlService,
        IWebHostEnvironment env,
        ILogger<FoodHubController> logger)
    {
        _foodHubService = foodHubService;
        _mediaService = mediaService;
        _urlService = urlService;
        _env = env;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Food & Grocery Category Hub Management";
        var config = await _foodHubService.GetConfigAsync(cancellationToken);
        return View(config);
    }

    [HttpPost("Save")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(
        [FromBody] FoodHubDto? jsonModel,
        [FromForm] FoodHubDto? formModel,
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
            await _foodHubService.UpdateConfigAsync(model, CurrentUserId.ToString(), cancellationToken);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" || Request.ContentType?.Contains("application/json") == true)
            {
                return Json(new { success = true, message = "Food & Grocery Category Hub settings saved successfully!" });
            }

            TempData["SuccessMessage"] = "Food & Grocery Category Hub settings saved successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update Food Hub configuration.");
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
            var def = FoodHubDto.CreateDefault();
            await _foodHubService.UpdateConfigAsync(def, CurrentUserId.ToString(), cancellationToken);
            TempData["SuccessMessage"] = "Food Hub configuration reset to official FreshBasket defaults successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset Food Hub configuration.");
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
        if (string.IsNullOrWhiteSpace(ext) || !AllowedImageExtensions.Contains(ext))
        {
            return Json(new { success = false, message = "Invalid file type. Allowed formats: JPG, PNG, WEBP, SVG, GIF." });
        }

        try
        {
            // Attempt standard MediaService upload first
            await using var stream = file.OpenReadStream();
            var uploadRequest = new MediaUploadRequest
            {
                FileStream = stream,
                OriginalFileName = file.FileName,
                ContentType = file.ContentType,
                MediaType = Dreamflykit.Domain.Media.MediaType.Banner,
                OwnerId = CurrentUserId,
                AltText = "Food Hub Asset",
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

            // Fallback to local wwwroot/images/food-hub folder
            var rootPath = !string.IsNullOrWhiteSpace(_env.WebRootPath)
                ? _env.WebRootPath
                : Path.Combine(AppContext.BaseDirectory, "wwwroot");

            var uploadsDir = Path.Combine(rootPath, "images", "food-hub");
            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            var safeFileName = $"food_{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(uploadsDir, safeFileName);

            await using var outStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            stream.Position = 0;
            await stream.CopyToAsync(outStream, cancellationToken);

            var relativeUrl = $"/images/food-hub/{safeFileName}";
            return Json(new
            {
                success = true,
                url = relativeUrl,
                targetField = targetField,
                message = "Image uploaded successfully to Food Hub storage!"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload Food Hub image.");
            return Json(new { success = false, message = "Upload failed: " + ex.Message });
        }
    }
}

