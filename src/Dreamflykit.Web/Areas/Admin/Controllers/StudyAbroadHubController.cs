using System.Diagnostics;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Application.StudyHub.Interfaces;
using Dreamflykit.Application.StudyHub.Models;
using Dreamflykit.Domain.Media;
using Dreamflykit.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/[controller]")]
[Authorize(Roles = "SuperAdmin,Admin")]
public sealed class StudyAbroadHubController : AdminBaseController
{
    private readonly IStudyHubService _studyHubService;
    private readonly IMediaService _mediaService;
    private readonly IMediaUrlService _urlService;
    private readonly ILogger<StudyAbroadHubController> _logger;

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp", ".svg", ".mp4", ".webm"
    };

    private const long MaxFileSizeBytes = 50 * 1024 * 1024; // 50 MB

    public StudyAbroadHubController(
        IStudyHubService studyHubService,
        IMediaService mediaService,
        IMediaUrlService urlService,
        ILogger<StudyAbroadHubController> logger)
    {
        _studyHubService = studyHubService;
        _mediaService = mediaService;
        _urlService = urlService;
        _logger = logger;
    }

    [HttpGet("")]
    [HttpGet("Index")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var config = await _studyHubService.GetConfigAsync(cancellationToken);
        var abroadProfile = config.AbroadProfile ?? StudyHubDto.CreateDefaultAbroadProfile();
        return View(abroadProfile);
    }

    [HttpPost("SaveConfig")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveConfig(
        [FromBody] StudyAbroadHubProfileDto payload,
        CancellationToken cancellationToken)
    {
        if (payload == null)
        {
            return Json(new { success = false, message = "Payload cannot be empty." });
        }

        try
        {
            var sw = Stopwatch.StartNew();
            var config = await _studyHubService.GetConfigAsync(cancellationToken);
            config.AbroadProfile = payload;

            await _studyHubService.UpdateConfigAsync(config, CurrentUserId, cancellationToken);
            sw.Stop();

            _logger.LogInformation("Study Abroad Hub Profile updated successfully by {UserId} in {ElapsedMs}ms",
                CurrentUserId, sw.ElapsedMilliseconds);

            return Json(new
            {
                success = true,
                message = "Study Abroad Hub Profile saved successfully!",
                elapsedMs = sw.ElapsedMilliseconds
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save Study Abroad Hub Profile configuration.");
            return Json(new { success = false, message = "Failed to save: " + ex.Message });
        }
    }

    [HttpPost("UploadImage")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadImage(
        IFormFile? file,
        [FromForm] string? targetCardId,
        CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();

        if (file == null || file.Length == 0)
        {
            return Json(new { success = false, message = "No file was selected for upload." });
        }

        if (file.Length > MaxFileSizeBytes)
        {
            return Json(new { success = false, message = $"File exceeds maximum allowed size of {MaxFileSizeBytes / 1024 / 1024} MB." });
        }

        var ext = Path.GetExtension(file.FileName);
        if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
        {
            return Json(new { success = false, message = "Invalid file type. Allowed: JPG, PNG, WEBP, SVG, MP4, WEBM." });
        }

        try
        {
            await using var stream = file.OpenReadStream();
            var isVideo = ext.Equals(".mp4", StringComparison.OrdinalIgnoreCase) || ext.Equals(".webm", StringComparison.OrdinalIgnoreCase);

            var uploadRequest = new MediaUploadRequest
            {
                FileStream = stream,
                OriginalFileName = file.FileName,
                ContentType = file.ContentType,
                MediaType = Dreamflykit.Domain.Media.MediaType.Banner,
                OwnerId = CurrentUserId != Guid.Empty ? CurrentUserId : Guid.NewGuid(),
                AltText = isVideo ? "Study Abroad Hub Video Visual" : "Study Abroad Hub Slide Asset",
                UploadedBy = CurrentUserId != Guid.Empty ? CurrentUserId : Guid.NewGuid()
            };

            var uploadResult = await _mediaService.UploadAsync(uploadRequest, cancellationToken);
            sw.Stop();

            if (uploadResult.IsSuccess && uploadResult.Asset != null)
            {
                var url = _urlService.GetUrl(uploadResult.Asset.ObjectKey);
                _logger.LogInformation("Fast media upload succeeded for {FileName} ({Bytes} bytes) to {ObjectKey} in {ElapsedMs}ms",
                    file.FileName, file.Length, uploadResult.Asset.ObjectKey, sw.ElapsedMilliseconds);

                return Json(new
                {
                    success = true,
                    url = url,
                    targetCardId = targetCardId,
                    mediaId = uploadResult.Asset.Id,
                    elapsedMs = sw.ElapsedMilliseconds,
                    message = $"Image uploaded successfully in {sw.ElapsedMilliseconds}ms!"
                });
            }

            _logger.LogWarning("Centralized media upload failed: {ErrorMessage}", uploadResult.ErrorMessage);
            return Json(new { success = false, message = uploadResult.ErrorMessage ?? "Failed to upload image." });
        }
        catch (Exception ex)
        {
            sw.Stop();
            _logger.LogError(ex, "Failed to upload image for Study Abroad Hub after {ElapsedMs}ms", sw.ElapsedMilliseconds);
            return Json(new { success = false, message = "Upload failed: " + ex.Message });
        }
    }

    [HttpPost("ResetDefaults")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetDefaults(CancellationToken cancellationToken)
    {
        try
        {
            var config = await _studyHubService.GetConfigAsync(cancellationToken);
            config.AbroadProfile = StudyHubDto.CreateDefaultAbroadProfile();

            await _studyHubService.UpdateConfigAsync(config, CurrentUserId, cancellationToken);
            TempData["SuccessMessage"] = "Study Abroad Hub Profile restored to default template.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset Study Abroad Hub Profile defaults.");
            TempData["ErrorMessage"] = "Failed to reset: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
