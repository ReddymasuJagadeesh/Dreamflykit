using Dreamflykit.Application.Cart.Models;

namespace Dreamflykit.Application.Cart.Interfaces;

public interface ICartRepository
{
    /// <summary>Gets the user's cart including all items. Returns null if no cart exists.</summary>
    Task<Domain.Cart.Cart?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken);

    /// <summary>Persists a new or existing cart.</summary>
    Task SaveAsync(
        Domain.Cart.Cart cart,
        CancellationToken cancellationToken);
}
