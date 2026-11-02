using Dreamflykit.Application.CategoryShowcase.Interfaces;
using Dreamflykit.Application.CategoryShowcase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class CategorySlidersController : Controller
{
    private readonly ICategoryThreeSectionSliderService _sliderService;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<CategorySlidersController> _logger;

    public CategorySlidersController(
        ICategoryThreeSectionSliderService sliderService,
        IWebHostEnvironment env,
        ILogger<CategorySlidersController> logger)
    {
        _sliderService = sliderService;
        _env = env;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? categorySlug = null, CancellationToken ct = default)
    {
        var allSliders = await _sliderService.GetAllSlidersAsync(ct);
        var activeSlug = string.IsNullOrWhiteSpace(categorySlug) ? "electronics" : categorySlug.Trim().ToLowerInvariant();

        ViewBag.ActiveSlug = activeSlug;
        ViewBag.AllSliders = allSliders;

        var activeConfig = allSliders.GetConfigForCategory(activeSlug);
        return View(activeConfig);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save([FromBody] CategoryThreeSectionConfigDto config, CancellationToken ct = default)
    {
        if (config == null || string.IsNullOrWhiteSpace(config.CategorySlug))
        {
            return Json(new { success = false, message = "Invalid category slider payload." });
        }

        try
        {
            var success = await _sliderService.UpdateCategorySliderAsync(config, ct);
            if (success)
            {
                return Json(new { success = true, message = $"3-Section Slider for '{config.CategoryName}' updated successfully!" });
            }
            return Json(new { success = false, message = "Failed to persist slider configuration." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving category slider for {Slug}", config.CategorySlug);
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Json(new { success = false, message = "No file selected." });
        }

        var allowedExts = new[] { ".jpg", ".jpeg", ".png", ".webp", ".svg", ".gif" };
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExts.Contains(ext))
        {
            return Json(new { success = false, message = "Unsupported image format. Use JPG, PNG, WEBP, or SVG." });
        }

        try
        {
            var uploadDir = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "images", "category-sliders");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            var fileName = $"slider_{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}{ext}";
            var filePath = Path.Combine(uploadDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var publicUrl = $"/images/category-sliders/{fileName}";
            return Json(new { success = true, url = publicUrl });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload category slider image");
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetDefaults(string? categorySlug = null, CancellationToken ct = default)
    {
        await _sliderService.ResetDefaultsAsync(categorySlug, ct);
        TempData["SuccessMessage"] = $"Defaults reset successfully for {(string.IsNullOrWhiteSpace(categorySlug) ? "all categories" : categorySlug)}.";
        return RedirectToAction(nameof(Index), new { categorySlug });
    }
}

