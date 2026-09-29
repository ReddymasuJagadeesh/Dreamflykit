namespace Dreamflykit.Application.Kits.Models;

public sealed class FindMyKitItem
{
    public Guid KitId { get; init; }

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