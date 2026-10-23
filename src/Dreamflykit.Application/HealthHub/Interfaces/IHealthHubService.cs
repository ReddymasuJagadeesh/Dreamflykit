using Dreamflykit.Application.HealthHub.Models;

namespace Dreamflykit.Application.HealthHub.Interfaces;

public interface IHealthHubService
{
    Task<HealthHubDto> GetConfigAsync(CancellationToken cancellationToken = default);
    Task UpdateConfigAsync(HealthHubDto dto, Guid modifiedByUserId, CancellationToken cancellationToken = default);
    Task ResetToDefaultAsync(CancellationToken cancellationToken = default);
}
