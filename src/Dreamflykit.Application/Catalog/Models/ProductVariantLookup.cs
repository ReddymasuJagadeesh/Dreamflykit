namespace Dreamflykit.Application.Catalog.Models;

/// <summary>
/// Variant lookup result used by CartService to validate and snapshot pricing.
/// </summary>
public sealed class ProductVariantLookup
{
    public Guid ProductVariantId { get; init; }

    public Guid ProductId { get; init; }

    public string ProductName { get; init; } = string.Empty;

    public string VariantName { get; init; } = string.Empty;

    public string Sku { get; init; } = string.Empty;

    public string? PrimaryImageUrl { get; init; }

    public decimal SellingPrice { get; init; }

    public decimal Mrp { get; init; }

    public bool IsActive { get; init; }
}
