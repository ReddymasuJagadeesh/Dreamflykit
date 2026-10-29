using Dreamflykit.Application.TravelHub.Models;

namespace Dreamflykit.Application.TravelHub.Interfaces;

public interface ITravelHubService
{
    Task<TravelHubDto> GetConfigAsync(CancellationToken cancellationToken = default);
    Task UpdateConfigAsync(TravelHubDto dto, string? updatedBy = null, CancellationToken cancellationToken = default);
}

