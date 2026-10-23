using Dreamflykit.Application.MobilesHub.Models;

namespace Dreamflykit.Application.MobilesHub.Interfaces;

public interface IMobilesHubService
{
    Task<MobilesHubDto> GetConfigAsync(CancellationToken cancellationToken = default);
    Task UpdateConfigAsync(MobilesHubDto config, Guid? updatedBy, CancellationToken cancellationToken = default);
    Task ResetToDefaultAsync(CancellationToken cancellationToken = default);
}
