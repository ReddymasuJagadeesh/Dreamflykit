namespace Dreamflykit.Application.Inventory.Interfaces;

public interface IInventoryService
{
    /// <summary>
    /// Atomically reserves stock for items during order checkout.
    /// Uses concurrency controls to prevent overselling.
    /// Throws DomainException if stock is insufficient.
    /// </summary>
    Task ReserveStockAsync(
        IEnumerable<(Guid ProductVariantId, int Quantity)> items,
        string orderNumber,
        CancellationToken cancellationToken);

    /// <summary>
    /// Commits reserved stock upon order payment/confirmation.
    /// </summary>
    Task CommitReservedStockAsync(
        IEnumerable<(Guid ProductVariantId, int Quantity)> items,
        string orderNumber,
        CancellationToken cancellationToken);

    /// <summary>
    /// Releases reserved stock back to available inventory on order cancellation or checkout timeout.
    /// </summary>
    Task ReleaseReservedStockAsync(
        IEnumerable<(Guid ProductVariantId, int Quantity)> items,
        string orderNumber,
        string reason,
        CancellationToken cancellationToken);

    /// <summary>
    /// Restocks returned inventory items when return request is approved and items are received.
    /// </summary>
    Task RestockReturnedItemsAsync(
        IEnumerable<(Guid ProductVariantId, int Quantity)> items,
        string orderNumber,
        string reason,
        CancellationToken cancellationToken);

    /// <summary>
    /// Checks if a variant has sufficient available stock.
    /// </summary>
    Task<bool> CheckStockAvailabilityAsync(
        Guid productVariantId,
        int quantity,
        CancellationToken cancellationToken);
}
