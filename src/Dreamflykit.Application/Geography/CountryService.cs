using Dreamflykit.Application.Geography.Models;

namespace Dreamflykit.Application.Geography;

public sealed class CountryService : ICountryService
{
    private readonly ICountryRepository _repository;

    public CountryService(
        ICountryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CountryListItem>>
        GetActiveCountriesAsync(
            CancellationToken cancellationToken)
    {
        var countries =
            await _repository.GetActiveCountriesAsync(
                cancellationToken);

        return countries
            .Select(country =>
                new CountryListItem
                {
                    Id = country.Id,
                    Name = country.Name
                })
            .ToList();
    }

    public async Task<IReadOnlyList<CountryKitListItem>>
        GetActiveCountriesWithKitsAsync(
            CancellationToken cancellationToken)
    {
        return await _repository
            .GetActiveCountriesWithKitsAsync(
                cancellationToken);
    }
}