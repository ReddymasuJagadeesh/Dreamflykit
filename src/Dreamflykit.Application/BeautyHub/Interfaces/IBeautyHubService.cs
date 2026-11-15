using Dreamflykit.Application.BeautyHub.Models;

namespace Dreamflykit.Application.BeautyHub.Interfaces;

public interface IBeautyHubService
{
    Task<BeautyHubDto> GetConfigAsync(CancellationToken cancellationToken = default);
    Task<BeautyCategoryPageDto?> GetCategoryPageAsync(string categorySlug, CancellationToken cancellationToken = default);
    Task<BeautySubcategoryPageDto?> GetSubcategoryPageAsync(string categorySlug, string? subcategorySlug = null, CancellationToken cancellationToken = default);
    Task UpdateConfigAsync(BeautyHubDto dto, string? updatedBy = null, CancellationToken cancellationToken = default);
    Task ResetToDefaultAsync(CancellationToken cancellationToken = default);
}
