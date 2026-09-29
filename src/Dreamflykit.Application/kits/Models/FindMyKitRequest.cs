namespace Dreamflykit.Application.Kits.Models;

public sealed class FindMyKitRequest
{
    public string Name { get; init; } = string.Empty;

    public Guid DestinationCountryId { get; init; }

    public DateOnly ArrivalDate { get; init; }

    public string Phone { get; init; } = string.Empty;
}