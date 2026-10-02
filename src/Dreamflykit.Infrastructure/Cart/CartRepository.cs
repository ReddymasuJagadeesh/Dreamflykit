using Dreamflykit.Application.Cart.Interfaces;
using Dreamflykit.Application.Cart.Models;
using Dreamflykit.Domain.Cart;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Cart;

/// <summary>
/// EF Core repository for the Cart aggregate.
/// Loads the full aggregate (cart + items) in a single query.
/// Uses SaveChanges for both insert and update.
/// </summary>
public sealed class CartRepository : ICartRepository
{
    private readonly DreamflykitDbContext _dbContext;

    public CartRepository(DreamflykitDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Cart.Cart?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Carts
            .Include(cart => cart.Items)
            .FirstOrDefaultAsync(
                cart => cart.UserId == userId,
                cancellationToken);
    }

    public async Task SaveAsync(
        Domain.Cart.Cart cart,
        CancellationToken cancellationToken)
    {
        var isNew = _dbContext.Entry(cart).State
            == EntityState.Detached;

        if (isNew)
        {
            _dbContext.Carts.Add(cart);
        }
        else
        {
            _dbContext.Carts.Update(cart);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
