namespace Dreamflykit.Application.Geography.Models;

public sealed class CountryKitListItem
{
    public Guid CountryId { get; init; }

    public string CountryName { get; init; } = string.Empty;
}