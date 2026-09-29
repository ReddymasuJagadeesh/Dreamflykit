using Dreamflykit.Application.Kits.Interfaces;
using Dreamflykit.Application.Kits.Models;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Kits;

public sealed class KitRepository : IKitRepository
{
    private readonly DreamflykitDbContext _dbContext;

    public KitRepository(
        DreamflykitDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<KitListItem>>
        GetActiveKitsByCountryNameAsync(
            string countryName,
            CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(countryName))
        {
            return Array.Empty<KitListItem>();
        }

        var normalizedCountryName =
            countryName
                .Trim()
                .Replace("-", " ")
                .ToLowerInvariant();

        return await (
            from kit in _dbContext.Kits.AsNoTracking()

            join country in _dbContext.Countries.AsNoTracking()
                on kit.CountryId equals country.Id

            join season in _dbContext.Seasons.AsNoTracking()
                on kit.SeasonId equals season.Id

            where
                kit.IsActive &&
                country.IsActive &&
                country.Name.ToLower() == normalizedCountryName

            orderby
                kit.Size,
                kit.DisplayOrder

            select new KitListItem
            {
                KitId = kit.Id,

                CountryId = country.Id,

                CountryName = country.Name,

                SeasonId = season.Id,

                SeasonName = season.Name,

                Name = kit.Name,

                Slug = kit.Slug,

                Description = kit.Description,

                Size = (int)kit.Size,

                SizeName = kit.Size.ToString(),

                Mrp = kit.Mrp,

                SellingPrice = kit.SellingPrice,

                DiscountAmount =
                    kit.DiscountAmount,

                DiscountPercentage =
                    kit.DiscountPercentage,

                IsFeatured =
                    kit.IsFeatured,

                DisplayOrder =
                    kit.DisplayOrder
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<KitDetails?>
        GetActiveKitBySlugAsync(
            string kitSlug,
            CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(kitSlug))
        {
            return null;
        }

        var normalizedKitSlug =
            kitSlug
                .Trim()
                .ToLowerInvariant();

        return await (
            from kit in _dbContext.Kits.AsNoTracking()

            join country in _dbContext.Countries.AsNoTracking()
                on kit.CountryId equals country.Id

            join season in _dbContext.Seasons.AsNoTracking()
                on kit.SeasonId equals season.Id

            where
                kit.IsActive &&
                kit.Slug == normalizedKitSlug &&
                country.IsActive &&
                season.IsActive

            select new KitDetails
            {
                KitId = kit.Id,

                CountryId = country.Id,

                CountryName = country.Name,

                SeasonId = season.Id,

                SeasonName = season.Name,

                Name = kit.Name,

                Slug = kit.Slug,

                Description = kit.Description,

                Size = (int)kit.Size,

                SizeName = kit.Size.ToString(),

                Mrp = kit.Mrp,

                SellingPrice = kit.SellingPrice,

                DiscountAmount =
                    kit.DiscountAmount,

                DiscountPercentage =
                    kit.DiscountPercentage,

                IsFeatured =
                    kit.IsFeatured
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}