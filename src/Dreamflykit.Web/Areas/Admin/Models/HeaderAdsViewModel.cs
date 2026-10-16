using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Dreamflykit.Web.Areas.Admin.Models;

public sealed class HeaderAdSpaceViewModel
{
    public Guid? Id { get; set; }

    public Guid? MediaAssetId { get; set; }

    public string? CurrentImageUrl { get; set; }

    [Required(ErrorMessage = "Ad headline / title is required.")]
    [MaxLength(150, ErrorMessage = "Headline cannot exceed 150 characters.")]
    [Display(Name = "Ad Title / Headline")]
    public string Title { get; set; } = "Dreamflykit Special Offers";

    [MaxLength(200, ErrorMessage = "Subtitle cannot exceed 200 characters.")]
    [Display(Name = "Subtitle / Offer Details")]
    public string? Subtitle { get; set; } = "Exclusive student travel deals & partner perks";

    [MaxLength(50, ErrorMessage = "Badge text cannot exceed 50 characters.")]
    [Display(Name = "Badge Tag (e.g. SPECIAL OFFER, STUDENT DEAL)")]
    public string? BadgeText { get; set; } = "SPECIAL OFFER";

    [Required(ErrorMessage = "Redirect URL is required.")]
    [MaxLength(500, ErrorMessage = "Redirect URL cannot exceed 500 characters.")]
    [Display(Name = "Click Redirect URL")]
    public string RedirectUrl { get; set; } = "/study-abroad-hub";

    [Display(Name = "Display Mode")]
    public string DisplayMode { get; set; } = "Hybrid"; // "Hybrid", "ImageOnly", "TextOnly"

    [Display(Name = "Open in New Tab")]
    public bool OpenInNewTab { get; set; }

    [Display(Name = "Ad Status (Active / Visible)")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Upload Logo or Banner Image")]
    public IFormFile? ImageFile { get; set; }

    public bool RemoveExistingImage { get; set; }
}
