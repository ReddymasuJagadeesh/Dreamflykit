using Dreamflykit.Application.CategoryShowcase.Models;

namespace Dreamflykit.Application.CategoryShowcase.Interfaces;

public interface ICategoryThreeSectionSliderService
{
    Task<CategoryThreeSectionConfigDto> GetCategorySliderAsync(string categorySlug, CancellationToken ct = default);
    Task<CategoryThreeSectionHubDto> GetAllSlidersAsync(CancellationToken ct = default);
    Task<bool> UpdateCategorySliderAsync(CategoryThreeSectionConfigDto config, CancellationToken ct = default);
    Task<bool> ResetDefaultsAsync(string? categorySlug = null, CancellationToken ct = default);
}

