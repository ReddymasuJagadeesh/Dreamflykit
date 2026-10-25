using System.Security.Claims;
using Dreamflykit.Application.Logo;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
[Route("Admin/[controller]")]
public sealed class LogoSettingsController : AdminBaseController
{
    private readonly ILogoSettingService _logoService;
    private readonly ILogger<LogoSettingsController> _logger;

    public LogoSettingsController(
        ILogoSettingService logoService,
        ILogger<LogoSettingsController> logger)
    {
        _logoService = logoService;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Header & Logo Branding";
        var settings = await _logoService.GetLogoSettingsAsync(cancellationToken);

        var vm = new LogoSettingsViewModel
        {
            Section1DLogoUrl = settings.Section1DLogoUrl,
            Section1UploadingSoon = settings.Section1UploadingSoon,
            Section2BrandLogoUrl = settings.Section2BrandLogoUrl,
            Section2UploadingSoon = settings.Section2UploadingSoon,
            SiteTitle = settings.SiteTitle,
            Tagline = settings.Tagline,
            BadgeSubtitle = settings.BadgeSubtitle,
            ShowBrandTagline = settings.ShowBrandTagline,
            UpdatedAtUtc = settings.UpdatedAtUtc
        };

        return View(vm);
    }

    [HttpPost("Save")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(
        [FromForm] LogoSettingsViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _ = Guid.TryParse(currentUserIdClaim, out var adminId);

        // Upload Section 1 ('D' Logo)
        var s1File = model.Section1File ?? model.ScrolledLogoFile;
        if (s1File != null && s1File.Length > 0)
        {
            using var stream = s1File.OpenReadStream();
            var uploadRes = await _logoService.UploadLogoFileAsync(
                stream,
                s1File.FileName,
                s1File.ContentType,
                isScrolledIcon: true,
                adminId,
                cancellationToken);

            if (!uploadRes.Success)
            {
                ModelState.AddModelError(nameof(model.Section1File), uploadRes.ErrorMessage ?? "Failed to upload Section 1 'D' logo.");
                return View("Index", model);
            }

            model.Section1DLogoUrl = uploadRes.FileUrl!;
            model.Section1UploadingSoon = false;
        }

        // Upload Section 2 (Dreamflykit Brand Logo)
        var s2File = model.Section2File ?? model.MainLogoFile;
        if (s2File != null && s2File.Length > 0)
        {
            using var stream = s2File.OpenReadStream();
            var uploadRes = await _logoService.UploadLogoFileAsync(
                stream,
                s2File.FileName,
                s2File.ContentType,
                isScrolledIcon: false,
                adminId,
                cancellationToken);

            if (!uploadRes.Success)
            {
                ModelState.AddModelError(nameof(model.Section2File), uploadRes.ErrorMessage ?? "Failed to upload Section 2 brand logo.");
                return View("Index", model);
            }

            model.Section2BrandLogoUrl = uploadRes.FileUrl!;
            model.Section2UploadingSoon = false;
        }

        var dto = new LogoSettingsDto
        {
            Section1DLogoUrl = model.Section1DLogoUrl,
            Section1UploadingSoon = model.Section1UploadingSoon,
            Section2BrandLogoUrl = model.Section2BrandLogoUrl,
            Section2UploadingSoon = model.Section2UploadingSoon,
            SiteTitle = model.SiteTitle.Trim(),
            Tagline = model.Tagline?.Trim() ?? "Your Journey. Our Kit.",
            BadgeSubtitle = model.BadgeSubtitle?.Trim(),
            ShowBrandTagline = model.ShowBrandTagline
        };

        var (success, errorMsg) = await _logoService.UpdateLogoSettingsAsync(dto, adminId, cancellationToken);
        if (!success)
        {
            ModelState.AddModelError(string.Empty, errorMsg ?? "Failed to update branding settings.");
            return View("Index", model);
        }

        TempData["SuccessMessage"] = "Header logo sections and branding updated successfully! Changes are live across Dreamflykit.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Upload")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(
        IFormFile? file,
        [FromForm] bool isSection1,
        CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
        {
            return Json(new { success = false, message = "No file was selected for upload." });
        }

        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _ = Guid.TryParse(currentUserIdClaim, out var adminId);

        using var stream = file.OpenReadStream();
        var result = await _logoService.UploadLogoFileAsync(
            stream,
            file.FileName,
            file.ContentType,
            isScrolledIcon: isSection1,
            adminId,
            cancellationToken);

        if (!result.Success)
        {
            return Json(new { success = false, message = result.ErrorMessage ?? "Upload failed." });
        }

        return Json(new
        {
            success = true,
            fileUrl = result.FileUrl,
            isSection1 = isSection1,
            message = isSection1
                ? "Section 1 'D' Logo updated successfully!"
                : "Section 2 'Dreamflykit' Brand Logo updated successfully!"
        });
    }

    [HttpPost("Reset")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reset(
        [FromForm] string logoType,
        CancellationToken cancellationToken)
    {
        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _ = Guid.TryParse(currentUserIdClaim, out var adminId);

        await _logoService.ResetToDefaultAsync(logoType, adminId, cancellationToken);
        TempData["SuccessMessage"] = $"Reset {logoType} logo back to default brand asset.";
        return RedirectToAction(nameof(Index));
    }
}
