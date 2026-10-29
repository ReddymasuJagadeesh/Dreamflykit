using Dreamflykit.Application.FoodHub.Models;

namespace Dreamflykit.Application.FoodHub.Interfaces;

public interface IFoodHubService
{
    Task<FoodHubDto> GetConfigAsync(CancellationToken cancellationToken = default);
    Task UpdateConfigAsync(FoodHubDto dto, string? updatedBy = null, CancellationToken cancellationToken = default);
}

