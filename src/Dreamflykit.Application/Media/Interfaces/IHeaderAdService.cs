using Dreamflykit.Application.Media.Models;

namespace Dreamflykit.Application.Media.Interfaces;

public interface IHeaderAdService
{
    Task<HeaderAdDto?> GetActiveHeaderAdSpaceAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<HeaderAdDto>> GetActiveHeaderAdsAsync(CancellationToken cancellationToken = default);
}
