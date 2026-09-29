using Dreamflykit.Application.Kits.Models;

namespace Dreamflykit.Application.Kits.Interfaces;

public interface IKitService
{
    Task<IReadOnlyList<KitListItem>>
        GetCountryKitsAsync(
            string countrySlug,
            CancellationToken cancellationToken);

    Task<KitDetails?>
        GetDetailsAsync(
            string kitSlug,
            CancellationToken cancellationToken);
}