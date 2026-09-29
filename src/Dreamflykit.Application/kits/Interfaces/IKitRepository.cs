using Dreamflykit.Application.Kits.Models;

namespace Dreamflykit.Application.Kits.Interfaces;

public interface IKitRepository
{
    Task<IReadOnlyList<KitListItem>>
        GetActiveKitsByCountryNameAsync(
            string countryName,
            CancellationToken cancellationToken);

    Task<KitDetails?>
        GetActiveKitBySlugAsync(
            string kitSlug,
            CancellationToken cancellationToken);
}