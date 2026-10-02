using Dreamflykit.Application.Inventory.Interfaces;
using Dreamflykit.Domain.Common;
using Dreamflykit.Domain.Inventory;

namespace Dreamflykit.Application.Inventory.Services;

public sealed class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryService(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task ReserveStockAsync(
        IEnumerable<(Guid ProductVariantId, int Quantity)> items,
        string orderNumber,
        CancellationToken cancellationToken)
    {
        var itemList = items.ToList();
        if (itemList.Count == 0) return;

        var variantIds = itemList.Select(i => i.ProductVariantId).Distinct().ToList();
        var inventoryItems = await _inventoryRepository.GetByVariantIdsAsync(variantIds, cancellationToken);
        var warehouse = await _inventoryRepository.GetOrCreateDefaultWarehouseAsync(cancellationToken);

        var itemsToSave = new List<InventoryItem>();
        var movements = new List<StockMovement>();

        foreach (var (variantId, quantity) in itemList)
        {
            var invItem = inventoryItems.FirstOrDefault(i => i.ProductVariantId == variantId);
            if (invItem is null)
            {
                // Auto-provision initial stock for seeded catalog items (100 units)
                invItem = new InventoryItem(warehouse.Id, variantId, availableQuantity: 100, reservedQuantity: 0);
                inventoryItems.Add(invItem);
            }

            if (invItem.AvailableQuantity < quantity)
            {
                throw new DomainException(
                    $"Insufficient stock for item. Requested: {quantity}, Available: {invItem.AvailableQuantity}.");
            }

            invItem.ReserveStock(quantity);
            itemsToSave.Add(invItem);

            movements.Add(new StockMovement(
                warehouse.Id,
                variantId,
                StockMovementType.Reservation,
                quantity,
                $"Reserved for order {orderNumber}"));
        }

        await _inventoryRepository.SaveRangeAsync(itemsToSave, cancellationToken);
        foreach (var m in movements)
        {
            await _inventoryRepository.AddStockMovementAsync(m, cancellationToken);
        }
    }

    public async Task CommitReservedStockAsync(
        IEnumerable<(Guid ProductVariantId, int Quantity)> items,
        string orderNumber,
        CancellationToken cancellationToken)
    {
        var itemList = items.ToList();
        if (itemList.Count == 0) return;

        var variantIds = itemList.Select(i => i.ProductVariantId).Distinct().ToList();
        var inventoryItems = await _inventoryRepository.GetByVariantIdsAsync(variantIds, cancellationToken);
        var warehouse = await _inventoryRepository.GetOrCreateDefaultWarehouseAsync(cancellationToken);

        var itemsToSave = new List<InventoryItem>();
        var movements = new List<StockMovement>();

        foreach (var (variantId, quantity) in itemList)
        {
            var invItem = inventoryItems.FirstOrDefault(i => i.ProductVariantId == variantId);
            if (invItem is null) continue;

            if (invItem.ReservedQuantity >= quantity)
            {
                invItem.CommitReservation(quantity);
            }
            else if (invItem.AvailableQuantity >= quantity)
            {
                invItem.RemoveStock(quantity);
            }

            itemsToSave.Add(invItem);

            movements.Add(new StockMovement(
                warehouse.Id,
                variantId,
                StockMovementType.StockOut,
                quantity,
                $"Committed for confirmed order {orderNumber}"));
        }

        await _inventoryRepository.SaveRangeAsync(itemsToSave, cancellationToken);
        foreach (var m in movements)
        {
            await _inventoryRepository.AddStockMovementAsync(m, cancellationToken);
        }
    }

    public async Task ReleaseReservedStockAsync(
        IEnumerable<(Guid ProductVariantId, int Quantity)> items,
        string orderNumber,
        string reason,
        CancellationToken cancellationToken)
    {
        var itemList = items.ToList();
        if (itemList.Count == 0) return;

        var variantIds = itemList.Select(i => i.ProductVariantId).Distinct().ToList();
        var inventoryItems = await _inventoryRepository.GetByVariantIdsAsync(variantIds, cancellationToken);
        var warehouse = await _inventoryRepository.GetOrCreateDefaultWarehouseAsync(cancellationToken);

        var itemsToSave = new List<InventoryItem>();
        var movements = new List<StockMovement>();

        foreach (var (variantId, quantity) in itemList)
        {
            var invItem = inventoryItems.FirstOrDefault(i => i.ProductVariantId == variantId);
            if (invItem is null) continue;

            var toRelease = Math.Min(quantity, invItem.ReservedQuantity);
            if (toRelease > 0)
            {
                invItem.ReleaseReservation(toRelease);
                itemsToSave.Add(invItem);

                movements.Add(new StockMovement(
                    warehouse.Id,
                    variantId,
                    StockMovementType.ReservationRelease,
                    toRelease,
                    $"Released from order {orderNumber}. Reason: {reason}"));
            }
        }

        if (itemsToSave.Count > 0)
        {
            await _inventoryRepository.SaveRangeAsync(itemsToSave, cancellationToken);
            foreach (var m in movements)
            {
                await _inventoryRepository.AddStockMovementAsync(m, cancellationToken);
            }
        }
    }

    public async Task RestockReturnedItemsAsync(
        IEnumerable<(Guid ProductVariantId, int Quantity)> items,
        string orderNumber,
        string reason,
        CancellationToken cancellationToken)
    {
        var itemList = items.ToList();
        if (itemList.Count == 0) return;

        var variantIds = itemList.Select(i => i.ProductVariantId).Distinct().ToList();
        var inventoryItems = await _inventoryRepository.GetByVariantIdsAsync(variantIds, cancellationToken);
        var warehouse = await _inventoryRepository.GetOrCreateDefaultWarehouseAsync(cancellationToken);

        var itemsToSave = new List<InventoryItem>();
        var movements = new List<StockMovement>();

        foreach (var (variantId, quantity) in itemList)
        {
            var invItem = inventoryItems.FirstOrDefault(i => i.ProductVariantId == variantId);
            if (invItem is null)
            {
                invItem = new InventoryItem(warehouse.Id, variantId, availableQuantity: quantity, reservedQuantity: 0);
                inventoryItems.Add(invItem);
            }
            else
            {
                invItem.AddStock(quantity);
            }

            itemsToSave.Add(invItem);

            movements.Add(new StockMovement(
                warehouse.Id,
                variantId,
                StockMovementType.Return,
                quantity,
                $"Restocked from return for order {orderNumber}. Reason: {reason}"));
        }

        await _inventoryRepository.SaveRangeAsync(itemsToSave, cancellationToken);
        foreach (var m in movements)
        {
            await _inventoryRepository.AddStockMovementAsync(m, cancellationToken);
        }
    }

    public async Task<bool> CheckStockAvailabilityAsync(
        Guid productVariantId,
        int quantity,
        CancellationToken cancellationToken)
    {
        if (quantity <= 0) return false;
        var item = await _inventoryRepository.GetByVariantIdAsync(productVariantId, cancellationToken);
        return item is null || item.AvailableQuantity >= quantity;
    }
}
