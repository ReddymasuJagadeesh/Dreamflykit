using Dreamflykit.Application.Wishlist.Models;

namespace Dreamflykit.Application.Wishlist.Interfaces;

public interface IWishlistService
{
    Task<WishlistSummary> GetWishlistAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<int> GetWishlistItemCountAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<ToggleWishlistResult> ToggleWishlistItemAsync(
        Guid userId,
        Guid productVariantId,
        CancellationToken cancellationToken);

    Task RemoveItemAsync(
        Guid userId,
        Guid productVariantId,
        CancellationToken cancellationToken);

    Task MoveToCartAsync(
        Guid userId,
        Guid productVariantId,
        CancellationToken cancellationToken);
}
