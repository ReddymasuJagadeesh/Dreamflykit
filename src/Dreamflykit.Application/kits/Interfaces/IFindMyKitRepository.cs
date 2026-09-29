using Dreamflykit.Domain.Geography;
using Dreamflykit.Domain.Kits;

namespace Dreamflykit.Application.Kits.Interfaces;

public interface IFindMyKitRepository
{
    Task<Country?> GetActiveCountryAsync(
        Guid countryId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Season>> GetActiveSeasonsAsync(
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Kit>> GetActiveKitsAsync(
        Guid countryId,
        Guid seasonId,
        CancellationToken cancellationToken);
}