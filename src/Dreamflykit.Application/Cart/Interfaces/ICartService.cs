using Dreamflykit.Application.Cart.Models;

namespace Dreamflykit.Application.Cart.Interfaces;

public interface ICartService
{
    /// <summary>
    /// Returns the current cart summary for a user.
    /// Returns an empty cart summary if no cart exists yet.
    /// </summary>
    Task<CartSummary> GetCartAsync(
        Guid userId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Returns just the total item count for the mini-cart badge.
    /// Efficient — does not load full cart items.
    /// </summary>
    Task<int> GetCartItemCountAsync(
        Guid userId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Adds a product variant to the cart.
    /// Creates a new cart if one does not yet exist.
    /// Validates that the variant is active before adding.
    /// </summary>
    Task AddItemAsync(
        Guid userId,
        AddToCartCommand command,
        CancellationToken cancellationToken);

    /// <summary>
    /// Updates the quantity of a cart line.
    /// Quantity = 0 removes the item.
    /// </summary>
    Task UpdateItemQuantityAsync(
        Guid userId,
        Guid productVariantId,
        int quantity,
        CancellationToken cancellationToken);

    /// <summary>
    /// Removes a line item entirely from the cart.
    /// </summary>
    Task RemoveItemAsync(
        Guid userId,
        Guid productVariantId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Clears all items from the cart.
    /// </summary>
    Task ClearCartAsync(
        Guid userId,
        CancellationToken cancellationToken);
}
