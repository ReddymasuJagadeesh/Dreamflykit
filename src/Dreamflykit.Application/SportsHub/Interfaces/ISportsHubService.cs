using Dreamflykit.Application.SportsHub.Models;

namespace Dreamflykit.Application.SportsHub.Interfaces;

public interface ISportsHubService
{
    Task<SportsHubDto> GetConfigAsync(CancellationToken ct = default);
    Task<SportsSubcategoryPageDto?> GetSubcategoryPageAsync(string subcategorySlug, CancellationToken ct = default);
    Task<bool> UpdateConfigAsync(SportsHubDto model, string? userId, CancellationToken ct = default);
    Task ResetToDefaultAsync(CancellationToken ct = default);
}
