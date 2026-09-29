using Dreamflykit.Application.Geography.Models;
using Dreamflykit.Domain.Geography;

namespace Dreamflykit.Application.Geography;

public interface ICountryRepository
{
    Task<IReadOnlyList<Country>>
        GetActiveCountriesAsync(
            CancellationToken cancellationToken);

    Task<IReadOnlyList<CountryKitListItem>>
        GetActiveCountriesWithKitsAsync(
            CancellationToken cancellationToken);
}