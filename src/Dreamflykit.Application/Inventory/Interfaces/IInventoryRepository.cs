using Dreamflykit.Domain.Inventory;

namespace Dreamflykit.Application.Inventory.Interfaces;

public interface IInventoryRepository
{
    Task<InventoryItem?> GetByVariantIdAsync(Guid productVariantId, CancellationToken cancellationToken);

    Task<List<InventoryItem>> GetByVariantIdsAsync(IEnumerable<Guid> productVariantIds, CancellationToken cancellationToken);

    Task SaveAsync(InventoryItem item, CancellationToken cancellationToken);

    Task SaveRangeAsync(IEnumerable<InventoryItem> items, CancellationToken cancellationToken);

    Task AddStockMovementAsync(StockMovement movement, CancellationToken cancellationToken);

    Task<Warehouse> GetOrCreateDefaultWarehouseAsync(CancellationToken cancellationToken);
}
