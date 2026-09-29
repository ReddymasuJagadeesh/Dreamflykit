namespace Dreamflykit.Web.Models.Kits;

public sealed class CountryKitsViewModel
{
    public string CountryName { get; init; } = string.Empty;

    public IReadOnlyList<CountryKitItemViewModel> Kits { get; init; }
        = Array.Empty<CountryKitItemViewModel>();
}

public sealed class CountryKitItemViewModel
{
    public Guid KitId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string SizeName { get; init; } = string.Empty;

    public decimal Mrp { get; init; }

    public decimal SellingPrice { get; init; }

    public decimal DiscountPercentage { get; init; }

    public bool IsFeatured { get; init; }
}