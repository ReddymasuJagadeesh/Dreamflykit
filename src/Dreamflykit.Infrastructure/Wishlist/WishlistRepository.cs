using Dreamflykit.Application.Wishlist.Interfaces;
using Dreamflykit.Domain.Wishlist;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Wishlist;

public sealed class WishlistRepository : IWishlistRepository
{
    private readonly DreamflykitDbContext _dbContext;

    public WishlistRepository(DreamflykitDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Wishlist.Wishlist?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Wishlists
            .Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.UserId == userId, cancellationToken);
    }

    public async Task SaveAsync(
        Domain.Wishlist.Wishlist wishlist,
        CancellationToken cancellationToken)
    {
        var exists = await _dbContext.Wishlists
            .AnyAsync(w => w.Id == wishlist.Id, cancellationToken);

        if (!exists)
        {
            await _dbContext.Wishlists.AddAsync(wishlist, cancellationToken);
        }
        else
        {
            _dbContext.Wishlists.Update(wishlist);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
