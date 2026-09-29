using Dreamflykit.Application.Kits.Interfaces;
using Dreamflykit.Application.Kits.Models;

namespace Dreamflykit.Application.Kits.Services;

public sealed class KitService : IKitService
{
    private readonly IKitRepository _repository;

    public KitService(IKitRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<KitListItem>>
        GetCountryKitsAsync(
            string countrySlug,
            CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(countrySlug))
        {
            throw new ArgumentException(
                "Country is required.",
                nameof(countrySlug));
        }

        return await _repository
            .GetActiveKitsByCountryNameAsync(
                countrySlug,
                cancellationToken);
    }

    public async Task<KitDetails?>
        GetDetailsAsync(
            string kitSlug,
            CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(kitSlug))
        {
            throw new ArgumentException(
                "Kit slug is required.",
                nameof(kitSlug));
        }

        return await _repository
            .GetActiveKitBySlugAsync(
                kitSlug,
                cancellationToken);
    }
}