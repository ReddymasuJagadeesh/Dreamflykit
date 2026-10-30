using Dreamflykit.Application.ElectronicsHub.Models;

namespace Dreamflykit.Application.ElectronicsHub.Interfaces;

public interface IElectronicsHubService
{
    Task<ElectronicsHubDto> GetConfigAsync(CancellationToken cancellationToken = default);

    Task UpdateConfigAsync(ElectronicsHubDto dto, Guid updatedBy, CancellationToken cancellationToken = default);

    Task ResetToDefaultAsync(CancellationToken cancellationToken = default);
}

