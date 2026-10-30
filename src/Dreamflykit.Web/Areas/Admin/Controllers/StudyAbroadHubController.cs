using System.Diagnostics;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Application.StudyHub.Interfaces;
using Dreamflykit.Application.StudyHub.Models;
using Dreamflykit.Domain.Media;
using Dreamflykit.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<StudyAbroadHubController> _logger;

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp", ".svg", ".mp4", ".webm", ".gif"
    };

    private const long MaxFileSizeBytes = 50 * 1024 * 1024; // 50 MB

    public StudyAbroadHubController(
        IStudyHubService studyHubService,
        IMediaService mediaService,
        IMediaUrlService urlService,
        IWebHostEnvironment env,
        ILogger<StudyAbroadHubController> logger)
    {
        _studyHubService = studyHubService;
        _mediaService = mediaService;
        _urlService = urlService;
        _env = env;
        _logger = logger;
    }

    [HttpGet("")]
    [HttpGet("Index")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var config = await _studyHubService.GetConfigAsync(cancellationToken);
        var abroadProfile = config.AbroadProfile ?? StudyHubDto.CreateDefaultAbroadProfile();
        abroadProfile.EducationLoansPage = config.EducationLoansPage ?? StudyHubDto.CreateDefaultEducationLoansPage();
        abroadProfile.EducationLoansPage.EnsureDefaults();
        abroadProfile.ConsultancyDetailsPage = config.ConsultancyDetailsPage ?? abroadProfile.ConsultancyDetailsPage ?? StudyHubDto.CreateDefaultConsultancyDetailsPage();
        return View(abroadProfile);
    }

    [HttpPost("SaveConfig")]
    [IgnoreAntiforgeryToken]
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
            if (payload.EducationLoansPage != null)
            {
                config.EducationLoansPage = payload.EducationLoansPage;
            }
            if (payload.ConsultancyDetailsPage != null)
            {
                config.ConsultancyDetailsPage = payload.ConsultancyDetailsPage;
            }

            await _studyHubService.UpdateConfigAsync(config, CurrentUserId, cancellationToken);
            sw.Stop();

            _logger.LogInformation("Study Abroad Hub Profile & Consultancy Details updated successfully by {UserId} in {ElapsedMs}ms",
                CurrentUserId, sw.ElapsedMilliseconds);

            return Json(new
            {
                success = true,
                message = "Study Abroad Hub Profile, Education Loans & Consultancy Portal settings saved successfully!",
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
    [IgnoreAntiforgeryToken]
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

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
        {
            return Json(new { success = false, message = "Invalid file type. Allowed: JPG, PNG, WEBP, SVG, MP4, WEBM." });
        }

        try
        {
            // 1. Primary: Save directly to local wwwroot/uploads/study-hub for instant, 100% reliable local/production availability
            var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadDir = Path.Combine(webRoot, "uploads", "study-hub");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            var uniqueFileName = $"hub-{DateTime.UtcNow:yyyyMMddHHmmssfff}-{Guid.NewGuid():N[..6]}{ext}";
            var destinationPath = Path.Combine(uploadDir, uniqueFileName);

            await using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await file.CopyToAsync(fileStream, cancellationToken);
            }

            var relativeUrl = $"/uploads/study-hub/{uniqueFileName}";
            sw.Stop();

            _logger.LogInformation("Image saved locally to {Url} in {ElapsedMs}ms", relativeUrl, sw.ElapsedMilliseconds);

            // 2. Best-effort centralized media registration (does not block local delivery)
            try
            {
                if (file.Length > 0)
                {
                    await using var stream = file.OpenReadStream();
                    var isVideo = ext.Equals(".mp4", StringComparison.OrdinalIgnoreCase) || ext.Equals(".webm", StringComparison.OrdinalIgnoreCase);
                    var uploadRequest = new MediaUploadRequest
                    {
                        FileStream = stream,
                        OriginalFileName = file.FileName,
                        ContentType = file.ContentType,
                        MediaType = MediaType.Banner,
                        OwnerId = CurrentUserId != Guid.Empty ? CurrentUserId : Guid.NewGuid(),
                        AltText = isVideo ? "Study Abroad Hub Video Visual" : "Study Abroad Hub Slide Asset",
                        UploadedBy = CurrentUserId != Guid.Empty ? CurrentUserId : Guid.NewGuid()
                    };
                    await _mediaService.UploadAsync(uploadRequest, cancellationToken);
                }
            }
            catch (Exception s3Ex)
            {
                _logger.LogDebug(s3Ex, "Secondary cloud sync skipped or deferred; local file is ready at {Url}", relativeUrl);
            }

            return Json(new
            {
                success = true,
                url = relativeUrl,
                targetCardId = targetCardId,
                elapsedMs = sw.ElapsedMilliseconds,
                message = $"Image uploaded successfully in {sw.ElapsedMilliseconds}ms!"
            });
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
