using System.ComponentModel.DataAnnotations;
using Dreamflykit.Application.Common;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Domain.Media;

namespace Dreamflykit.Web.Areas.Admin.Models;

public sealed class MediaLibraryViewModel
{
    public PagedResult<MediaAssetDto> Assets { get; set; } = new();
    public MediaType? MediaType { get; set; }
    public MediaAssetStatus? Status { get; set; }
    public string? Search { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = true;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 24;
}

public sealed class MediaUploadViewModel
{
    [Required(ErrorMessage = "Please select a file to upload")]
    public IFormFile? File { get; set; }

    [Required]
    public MediaType MediaType { get; set; } = MediaType.Product;

    public Guid OwnerId { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Alt text is required for accessibility & SEO")]
    [MaxLength(250)]
    public string AltText { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(500)]
    public string? Caption { get; set; }
}

public sealed class MediaDetailViewModel
{
    public MediaAssetDto Asset { get; set; } = default!;
    public List<MediaUsageItem> Usages { get; set; } = [];
}

public sealed class MediaUsageItem
{
    public string ResourceType { get; set; } = string.Empty;
    public string ResourceTitle { get; set; } = string.Empty;
    public string ResourceUrl { get; set; } = string.Empty;
}

public sealed class MediaEditViewModel
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string AltText { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(500)]
    public string? Caption { get; set; }
}

public sealed class BannerListViewModel
{
    public IReadOnlyList<BannerItemDto> Banners { get; set; } = [];
    public int MaxLiveSlides { get; set; } = 12;
    public int ActiveLiveCount { get; set; }
    public int SlideDurationSeconds { get; set; } = 5;
    public bool AutoPlay { get; set; } = true;
}

public sealed class BannerItemDto
{
    public Guid Id { get; set; }
    public Guid MediaAssetId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string? CtaText { get; set; }
    public string? CtaUrl { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? StartDateUtc { get; set; }
    public DateTime? EndDateUtc { get; set; }
    public bool IsLiveInHero { get; set; }
}

public sealed class BannerEditViewModel
{
    public Guid? Id { get; set; }

    public Guid MediaAssetId { get; set; }

    public IFormFile? ImageFile { get; set; }

    public string? CurrentImageUrl { get; set; }

    [Required(ErrorMessage = "Title / Headline is required")]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(250)]
    public string? Subtitle { get; set; }

    [MaxLength(50)]
    public string? CtaText { get; set; }

    [MaxLength(300)]
    public string? CtaUrl { get; set; }

    public int DisplayOrder { get; set; } = 1;

    public bool IsActive { get; set; } = true;
    public bool IsPublished { get; set; } = true;

    public DateTime? StartDateUtc { get; set; }
    public DateTime? EndDateUtc { get; set; }
}

public sealed class HomepageSectionListViewModel
{
    public IReadOnlyList<HomepageSectionItemDto> Sections { get; set; } = [];
}

public sealed class HomepageSectionItemDto
{
    public Guid Id { get; set; }
    public string SectionKey { get; set; } = string.Empty;
    public Guid? MediaAssetId { get; set; }
    public string? ImageUrl { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public bool IsPublished { get; set; }
}

public sealed class HomepageSectionEditViewModel
{
    public Guid Id { get; set; }
    public string SectionKey { get; set; } = string.Empty;
    public Guid? MediaAssetId { get; set; }
    public string? CurrentImageUrl { get; set; }

    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(250)]
    public string? Subtitle { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public bool IsPublished { get; set; }
}

public sealed class LandingPageListViewModel
{
    public IReadOnlyList<LandingPageItemDto> LandingPages { get; set; } = [];
}

public sealed class LandingPageItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public Guid? HeroMediaAssetId { get; set; }
    public string? HeroImageUrl { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? StartDateUtc { get; set; }
    public DateTime? EndDateUtc { get; set; }
}

public sealed class LandingPageEditViewModel
{
    public Guid? Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Slug { get; set; } = string.Empty;

    public Guid? HeroMediaAssetId { get; set; }
    public string? CurrentHeroImageUrl { get; set; }

    public string Status { get; set; } = "Draft";
    public DateTime? StartDateUtc { get; set; }
    public DateTime? EndDateUtc { get; set; }
}

public sealed class MediaPermissionMatrixViewModel
{
    public IReadOnlyList<UserPermissionSummaryDto> Users { get; set; } = [];
    public IReadOnlyList<MediaType> MediaTypes { get; set; } = [];
    public IReadOnlyList<MediaPermissionType> PermissionTypes { get; set; } = [];
}

public sealed class UserPermissionSummaryDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public Dictionary<string, bool> GrantedPermissions { get; set; } = [];
}

public sealed class UpdateUserPermissionRequest
{
    public Guid UserId { get; set; }
    public MediaType MediaType { get; set; }
    public MediaPermissionType PermissionType { get; set; }
    public bool IsGranted { get; set; }
}

public sealed class ProductMediaManagementViewModel
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductSlug { get; set; } = string.Empty;
    public List<ProductMediaItemViewModel> MediaItems { get; set; } = [];
}

public sealed class ProductMediaItemViewModel
{
    public Guid ProductMediaId { get; set; }
    public Guid? MediaAssetId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string AltText { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsPrimary { get; set; }
}

public sealed class AssignProductMediaRequest
{
    public Guid ProductId { get; set; }
    public Guid MediaAssetId { get; set; }
    public bool IsPrimary { get; set; }
    public int DisplayOrder { get; set; }
}
