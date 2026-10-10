using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Dreamflykit.Web.Areas.Admin.Models;

public sealed class HeaderAdsManagementViewModel
{
    public HeaderAdEditItem Slot1 { get; set; } = new() { SlotNumber = 1 };
    public HeaderAdEditItem Slot2 { get; set; } = new() { SlotNumber = 2 };
}

public sealed class HeaderAdEditItem
{
    public int SlotNumber { get; set; }

    public Guid? Id { get; set; }

    public Guid? MediaAssetId { get; set; }

    public string? CurrentImageUrl { get; set; }

    [Required]
    [MaxLength(150)]
    [Display(Name = "Ad Title / Headline")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(200)]
    [Display(Name = "Subtitle / Promo Badge")]
    public string? Subtitle { get; set; }

    [Required]
    [MaxLength(500)]
    [Display(Name = "Redirect Website URL")]
    public string RedirectUrl { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    [Display(Name = "Upload New Image")]
    public IFormFile? ImageFile { get; set; }
}
