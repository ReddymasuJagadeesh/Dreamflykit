using Dreamflykit.Application.Geography.Models;

namespace Dreamflykit.Application.Geography;

public interface ICountryService
{
    Task<IReadOnlyList<CountryListItem>>
        GetActiveCountriesAsync(
            CancellationToken cancellationToken);

    Task<IReadOnlyList<CountryKitListItem>>
        GetActiveCountriesWithKitsAsync(
            CancellationToken cancellationToken);
}