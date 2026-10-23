using Dreamflykit.Application.FashionHub.Models;

namespace Dreamflykit.Application.FashionHub.Interfaces;

public interface IFashionHubService
{
    Task<FashionHubDto> GetConfigAsync(CancellationToken cancellationToken = default);
    Task UpdateConfigAsync(FashionHubDto dto, Guid updatedBy, CancellationToken cancellationToken = default);
    Task ResetToDefaultAsync(CancellationToken cancellationToken = default);
}
