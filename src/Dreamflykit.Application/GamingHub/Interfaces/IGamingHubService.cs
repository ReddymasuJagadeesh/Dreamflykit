using Dreamflykit.Application.GamingHub.Models;

namespace Dreamflykit.Application.GamingHub.Interfaces;

public interface IGamingHubService
{
    Task<GamingHubDto> GetConfigAsync(CancellationToken ct = default);
    Task<bool> UpdateConfigAsync(GamingHubDto model, string? userId, CancellationToken ct = default);
    Task<GamingSubcategoryPageDto> GetSubcategoryPageAsync(string subcategorySlug, CancellationToken ct = default);
}

