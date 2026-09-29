using Dreamflykit.Application.Geography;
using Dreamflykit.Application.Geography.Models;
using Dreamflykit.Domain.Geography;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Geography;

public sealed class CountryRepository : ICountryRepository
{
    private readonly DreamflykitDbContext _dbContext;

    public CountryRepository(
        DreamflykitDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Country>>
        GetActiveCountriesAsync(
            CancellationToken cancellationToken)
    {
        return await _dbContext.Countries
            .AsNoTracking()
            .Where(country => country.IsActive)
            .OrderBy(country => country.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<CountryKitListItem>>
        GetActiveCountriesWithKitsAsync(
            CancellationToken cancellationToken)
    {
        return await (
            from country in _dbContext.Countries.AsNoTracking()

            join kit in _dbContext.Kits.AsNoTracking()
                on country.Id equals kit.CountryId

            where
                country.IsActive &&
                kit.IsActive

            select new CountryKitListItem
            {
                CountryId = country.Id,

                CountryName = country.Name
            })
            .Distinct()
            .OrderBy(country => country.CountryName)
            .ToListAsync(cancellationToken);
    }
}