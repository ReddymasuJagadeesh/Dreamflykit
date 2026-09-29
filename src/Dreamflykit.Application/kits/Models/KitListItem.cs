namespace Dreamflykit.Application.Kits.Models;

public sealed class KitListItem
{
    public Guid KitId { get; init; }

    public Guid CountryId { get; init; }

    public string CountryName { get; init; } = string.Empty;

    public Guid SeasonId { get; init; }

    public string SeasonName { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public int Size { get; init; }

    public string SizeName { get; init; } = string.Empty;

    public decimal Mrp { get; init; }

    public decimal SellingPrice { get; init; }

    public decimal DiscountAmount { get; init; }

    public decimal DiscountPercentage { get; init; }

    public bool IsFeatured { get; init; }

    public int DisplayOrder { get; init; }
}