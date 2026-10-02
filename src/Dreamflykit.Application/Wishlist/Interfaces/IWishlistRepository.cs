namespace Dreamflykit.Application.Wishlist.Interfaces;

public interface IWishlistRepository
{
    Task<Domain.Wishlist.Wishlist?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task SaveAsync(
        Domain.Wishlist.Wishlist wishlist,
        CancellationToken cancellationToken);
}
