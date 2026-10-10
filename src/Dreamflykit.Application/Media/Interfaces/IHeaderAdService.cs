using Dreamflykit.Application.Media.Models;

namespace Dreamflykit.Application.Media.Interfaces;

public interface IHeaderAdService
{
    Task<IReadOnlyList<HeaderAdDto>> GetActiveHeaderAdsAsync(CancellationToken cancellationToken = default);
}
