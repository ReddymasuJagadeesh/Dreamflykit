namespace Dreamflykit.Web.Models.Kits;

public sealed class FindMyKitResultsViewModel
{
    public string Name { get; init; } = string.Empty;

    public string CountryName { get; init; } = string.Empty;

    public string SeasonName { get; init; } = string.Empty;

    public DateOnly ArrivalDate { get; init; }

    public IReadOnlyList<FindMyKitResultItemViewModel> Kits { get; init; }
        = Array.Empty<FindMyKitResultItemViewModel>();
}

public sealed class FindMyKitResultItemViewModel
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