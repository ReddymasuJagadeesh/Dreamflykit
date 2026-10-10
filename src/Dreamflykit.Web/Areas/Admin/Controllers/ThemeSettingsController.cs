using Dreamflykit.Application.Theme;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
[Route("Admin/ThemeSettings")]
public sealed class ThemeSettingsController : Controller
{
    private readonly IThemeSettingService _themeService;
    private readonly ILogger<ThemeSettingsController> _logger;

    public ThemeSettingsController(
        IThemeSettingService themeService,
        ILogger<ThemeSettingsController> logger)
    {
        _themeService = themeService;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var settings = await _themeService.GetThemeSettingsAsync(cancellationToken);

        var viewModel = new ThemeSettingsViewModel
        {
            FontFamily = settings.FontFamily,
            FontScale = settings.FontScale,
            FontSizeBasePx = settings.FontSizeBasePx,
            HeadingFontFamily = settings.HeadingFontFamily,
            UpdatedAtUtc = settings.UpdatedAtUtc,
            SupportedFonts = TypographyCatalog.SupportedFonts
        };

        return View(viewModel);
    }

    [HttpPost("")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(
        [FromForm] ThemeSettingsViewModel model,
        CancellationToken cancellationToken)
    {
        model.SupportedFonts = TypographyCatalog.SupportedFonts;

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var currentUserIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        _ = Guid.TryParse(currentUserIdClaim, out var superAdminId);

        var command = new UpdateThemeSettingsCommand
        {
            FontFamily = model.FontFamily,
            FontScale = model.FontScale,
            FontSizeBasePx = model.FontSizeBasePx,
            HeadingFontFamily = model.HeadingFontFamily
        };

        var (success, errorMessage) = await _themeService.UpdateThemeSettingsAsync(
            command,
            superAdminId,
            cancellationToken);

        if (!success)
        {
            ModelState.AddModelError(string.Empty, errorMessage ?? "Failed to update theme settings.");
            return View(model);
        }

        TempData["SuccessMessage"] = $"Typography updated to '{model.FontFamily}' (Scale: {model.FontScale:P0}, Base: {model.FontSizeBasePx}px). Applied instantly across the entire application.";
        return RedirectToAction(nameof(Index));
    }
}
