using Dreamflykit.Application.Kits.Interfaces;
using Dreamflykit.Domain.Geography;
using Dreamflykit.Domain.Kits;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Kits;

public sealed class FindMyKitRepository : IFindMyKitRepository
{
    private readonly DreamflykitDbContext _dbContext;

    public FindMyKitRepository(
        DreamflykitDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Country?> GetActiveCountryAsync(
        Guid countryId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Countries
            .AsNoTracking()
            .FirstOrDefaultAsync(
                country =>
                    country.Id == countryId &&
                    country.IsActive,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Season>> GetActiveSeasonsAsync(
        CancellationToken cancellationToken)
    {
        var seasons = await _dbContext.Seasons
            .AsNoTracking()
            .Where(season => season.IsActive)
            .OrderBy(season => season.StartMonth)
            .ThenBy(season => season.StartDay)
            .ToListAsync(cancellationToken);

        return seasons;
    }

    public async Task<IReadOnlyList<Kit>> GetActiveKitsAsync(
        Guid countryId,
        Guid seasonId,
        CancellationToken cancellationToken)
    {
        var kits = await _dbContext.Kits
            .AsNoTracking()
            .Where(
                kit =>
                    kit.CountryId == countryId &&
                    kit.SeasonId == seasonId &&
                    kit.IsActive)
            .OrderBy(kit => kit.Size)
            .ThenBy(kit => kit.DisplayOrder)
            .ToListAsync(cancellationToken);

        return kits;
    }
}