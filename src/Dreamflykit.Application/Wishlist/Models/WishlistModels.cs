namespace Dreamflykit.Application.Wishlist.Models;

public sealed class WishlistSummary
{
    public int TotalItems { get; init; }

    public bool IsEmpty => TotalItems == 0;

    public IReadOnlyList<WishlistItemDto> Items { get; init; } = Array.Empty<WishlistItemDto>();
}

public sealed class WishlistItemDto
{
    public Guid ProductVariantId { get; init; }

    public string ProductName { get; init; } = string.Empty;

    public string VariantName { get; init; } = string.Empty;

    public string Sku { get; init; } = string.Empty;

    public string? ImageUrl { get; init; }

    public decimal UnitPrice { get; init; }

    public decimal Mrp { get; init; }

    public int DiscountPercentage { get; init; }
}

public sealed class ToggleWishlistResult
{
    public bool IsInWishlist { get; init; }

    public int TotalWishlistItems { get; init; }

    public string Message { get; init; } = string.Empty;
}
