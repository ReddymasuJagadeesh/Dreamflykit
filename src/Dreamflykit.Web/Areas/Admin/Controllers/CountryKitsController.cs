using Dreamflykit.Application.CountryKits.Interfaces;
using Dreamflykit.Application.CountryKits.Models;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
[Route("Admin/[controller]")]
public sealed class CountryKitsController : AdminBaseController
{
    private readonly ICountryKitsHubService _countryKitsHubService;
    private readonly IMediaService _mediaService;
    private readonly IMediaUrlService _urlService;
    private readonly ILogger<CountryKitsController> _logger;

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp", ".svg"
    };

    private const long MaxFileSizeBytes = 8 * 1024 * 1024; // 8 MB

    public CountryKitsController(
        ICountryKitsHubService countryKitsHubService,
        IMediaService mediaService,
        IMediaUrlService urlService,
        ILogger<CountryKitsController> logger)
    {
        _countryKitsHubService = countryKitsHubService;
        _mediaService = mediaService;
        _urlService = urlService;
        _logger = logger;
    }

    [HttpGet("")]
    [HttpGet("Index")]
    public async Task<IActionResult> Index([FromQuery] string? countrySlug = null, CancellationToken cancellationToken = default)
    {
        ViewData["Title"] = "Country Kits Management";
        var hub = await _countryKitsHubService.GetAllProfilesAsync(cancellationToken);

        var slug = string.IsNullOrWhiteSpace(countrySlug) ? "usa" : CountryKitsHubDto.NormalizeSlug(countrySlug);
        var selectedProfile = hub.GetCountryProfile(slug);

        var viewModel = new CountryKitAdminViewModel
        {
            HubData = hub,
            SelectedProfile = selectedProfile,
            SelectedSlug = slug,
            Countries = hub.Countries.Values.OrderBy(c => c.DisplayOrder).ThenBy(c => c.CountryName).ToList()
        };

        return View(viewModel);
    }

    [HttpPost("Save")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(
        [FromBody] CountryKitProfileDto? jsonModel,
        [FromForm] CountryKitProfileDto? formModel,
        CancellationToken cancellationToken)
    {
        var model = jsonModel ?? formModel;
        if (model == null || string.IsNullOrWhiteSpace(model.CountrySlug))
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
            var saved = await _countryKitsHubService.SaveCountryProfileAsync(model, cancellationToken);
            if (saved)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" || Request.ContentType?.Contains("application/json") == true)
                {
                    return Json(new { success = true, message = $"Country profile for '{model.CountryName}' saved successfully!" });
                }

                TempData["SuccessMessage"] = $"Country profile for '{model.CountryName}' saved successfully!";
                return RedirectToAction(nameof(Index), new { countrySlug = model.CountrySlug });
            }

            throw new Exception("Failed to save country profile.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update Country Kit profile for {Slug}", model.CountrySlug);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" || Request.ContentType?.Contains("application/json") == true)
            {
                return Json(new { success = false, message = "Failed to save: " + ex.Message });
            }
            TempData["ErrorMessage"] = "Failed to save: " + ex.Message;
            return RedirectToAction(nameof(Index), new { countrySlug = model.CountrySlug });
        }
    }

    [HttpPost("AddCountry")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCountry(
        [FromBody] CountryKitProfileDto? jsonModel,
        [FromForm] CountryKitProfileDto? formModel,
        CancellationToken cancellationToken)
    {
        var model = jsonModel ?? formModel;
        if (model == null || string.IsNullOrWhiteSpace(model.CountryName))
        {
            return Json(new { success = false, message = "Country name is required." });
        }

        var normalizedSlug = CountryKitsHubDto.NormalizeSlug(string.IsNullOrWhiteSpace(model.CountrySlug) ? model.CountryName : model.CountrySlug);
        model.CountrySlug = normalizedSlug;

        if (string.IsNullOrWhiteSpace(model.HeroTitle))
        {
            model.HeroTitle = $"{model.CountryName} Student Kits";
        }

        if (string.IsNullOrWhiteSpace(model.HeroSubtitle))
        {
            model.HeroSubtitle = $"Everything you need for a comfortable and successful study abroad journey in {model.CountryName}.";
        }

        if (string.IsNullOrWhiteSpace(model.Tagline))
        {
            model.Tagline = $"Study in {model.CountryName}";
        }

        if (model.Kits == null || model.Kits.Count == 0)
        {
            model.Kits = new List<CountryKitItemDto>
            {
                new()
                {
                    Id = $"{normalizedSlug}-small-kit",
                    CountrySlug = normalizedSlug,
                    Name = $"{model.CountryName} Small Kit",
                    Slug = "small-kit",
                    Size = "small",
                    Badge = "Best Seller",
                    Description = $"Essential items for a hassle-free start in {model.CountryName}.",
                    SellingPrice = 4999,
                    Mrp = 6499,
                    DiscountPercentage = 23,
                    Rating = 4.7,
                    ReviewCount = 1200,
                    IsUploadingSoon = true,
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = $"{normalizedSlug}-medium-kit",
                    CountrySlug = normalizedSlug,
                    Name = $"{model.CountryName} Medium Kit",
                    Slug = "medium-kit",
                    Size = "medium",
                    Badge = "Most Popular",
                    Description = $"All-rounder kit for the perfect balance of needs in {model.CountryName}.",
                    SellingPrice = 9999,
                    Mrp = 13499,
                    DiscountPercentage = 26,
                    Rating = 4.8,
                    ReviewCount = 1560,
                    IsUploadingSoon = true,
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = $"{normalizedSlug}-large-kit",
                    CountrySlug = normalizedSlug,
                    Name = $"{model.CountryName} Large Kit",
                    Slug = "large-kit",
                    Size = "large",
                    Badge = "Best Value",
                    Description = $"Complete kit with everything you might need in {model.CountryName}.",
                    SellingPrice = 16999,
                    Mrp = 22999,
                    DiscountPercentage = 26,
                    Rating = 4.9,
                    ReviewCount = 2134,
                    IsUploadingSoon = true,
                    DisplayOrder = 3,
                    IsActive = true
                }
            };
        }

        try
        {
            var saved = await _countryKitsHubService.SaveCountryProfileAsync(model, cancellationToken);
            if (saved)
            {
                return Json(new { success = true, slug = model.CountrySlug, message = $"Country '{model.CountryName}' added successfully!" });
            }
            return Json(new { success = false, message = "Could not save new country." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add country {CountryName}", model.CountryName);
            return Json(new { success = false, message = "Error: " + ex.Message });
        }
    }

    [HttpPost("DeleteCountry")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCountry([FromForm] string countrySlug, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(countrySlug))
        {
            return Json(new { success = false, message = "Country slug is required." });
        }

        try
        {
            var deleted = await _countryKitsHubService.DeleteCountryProfileAsync(countrySlug, cancellationToken);
            if (deleted)
            {
                return Json(new { success = true, message = $"Country '{countrySlug}' removed successfully!" });
            }
            return Json(new { success = false, message = $"Failed to delete '{countrySlug}'. Country not found or default protected." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting country {Slug}", countrySlug);
            return Json(new { success = false, message = "Error: " + ex.Message });
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
                AltText = "Country Kit Asset",
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
            _logger.LogError(ex, "Failed to upload image for Country Kits.");
            return Json(new { success = false, message = "Upload failed: " + ex.Message });
        }
    }

    [HttpPost("ResetDefaults")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetDefaults([FromForm] string? countrySlug, CancellationToken cancellationToken)
    {
        try
        {
            await _countryKitsHubService.ResetDefaultsAsync(countrySlug, cancellationToken);
            TempData["SuccessMessage"] = string.IsNullOrWhiteSpace(countrySlug)
                ? "All Country Kits have been restored to defaults."
                : $"Country '{countrySlug}' has been restored to defaults.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset Country Kits defaults.");
            TempData["ErrorMessage"] = "Failed to reset defaults: " + ex.Message;
        }

        return RedirectToAction(nameof(Index), new { countrySlug });
    }
}

