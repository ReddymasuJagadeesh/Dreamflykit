namespace Dreamflykit.Application.Kits.Models;

public sealed class FindMyKitResult
{
    public string Name { get; init; } = string.Empty;

    public Guid CountryId { get; init; }

    public string CountryName { get; init; } = string.Empty;

    public Guid SeasonId { get; init; }

    public string SeasonName { get; init; } = string.Empty;

    public IReadOnlyList<FindMyKitItem> Kits { get; init; }
        = Array.Empty<FindMyKitItem>();
}