using Dreamflykit.Application.Kits.Interfaces;
using Dreamflykit.Application.Kits.Models;
using Dreamflykit.Domain.Kits;

namespace Dreamflykit.Application.Kits.Services;

public sealed class FindMyKitService : IFindMyKitService
{
    private readonly IFindMyKitRepository _repository;

    public FindMyKitService(
        IFindMyKitRepository repository)
    {
        _repository = repository;
    }

    public async Task<FindMyKitResult> FindAsync(
        FindMyKitRequest request,
        CancellationToken cancellationToken)
    {
        ValidateRequest(request);

        var country =
            await _repository.GetActiveCountryAsync(
                request.DestinationCountryId,
                cancellationToken);

        if (country is null)
        {
            throw new InvalidOperationException(
                "The selected destination country is not available.");
        }

        var seasons =
            await _repository.GetActiveSeasonsAsync(
                cancellationToken);

        var matchingSeason =
            seasons.FirstOrDefault(
                season => season.ContainsDate(request.ArrivalDate));

        if (matchingSeason is null)
        {
            throw new InvalidOperationException(
                "No active season is configured for the selected arrival date.");
        }

        var kits =
            await _repository.GetActiveKitsAsync(
                country.Id,
                matchingSeason.Id,
                cancellationToken);

        var selectedKits = kits
            .OrderBy(x => x.Size)
            .ThenBy(x => x.DisplayOrder)
            .Take(3)
            .Select(x => new FindMyKitItem
            {
                KitId = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description,
                Size = (int)x.Size,
                SizeName = x.Size.ToString(),
                Mrp = x.Mrp,
                SellingPrice = x.SellingPrice,
                DiscountAmount = x.DiscountAmount,
                DiscountPercentage = x.DiscountPercentage,
                IsFeatured = x.IsFeatured,
                DisplayOrder = x.DisplayOrder
            })
            .ToList();

        return new FindMyKitResult
        {
            Name = request.Name.Trim(),
            CountryId = country.Id,
            CountryName = country.Name,
            SeasonId = matchingSeason.Id,
            SeasonName = matchingSeason.Name,
            Kits = selectedKits
        };
    }

    private static void ValidateRequest(
        FindMyKitRequest request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException(
                "Name is required.",
                nameof(request.Name));
        }

        if (request.DestinationCountryId == Guid.Empty)
        {
            throw new ArgumentException(
                "Destination country is required.",
                nameof(request.DestinationCountryId));
        }

        if (request.ArrivalDate == default)
        {
            throw new ArgumentException(
                "Arrival date is required.",
                nameof(request.ArrivalDate));
        }

        if (string.IsNullOrWhiteSpace(request.Phone))
        {
            throw new ArgumentException(
                "Phone number is required.",
                nameof(request.Phone));
        }
    }
}