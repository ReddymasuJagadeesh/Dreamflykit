using Dreamflykit.Application.Inventory.Interfaces;
using Dreamflykit.Domain.Inventory;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Inventory;

public sealed class InventoryRepository : IInventoryRepository
{
    private readonly DreamflykitDbContext _dbContext;

    public InventoryRepository(DreamflykitDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<InventoryItem?> GetByVariantIdAsync(
        Guid productVariantId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.InventoryItems
            .FirstOrDefaultAsync(i => i.ProductVariantId == productVariantId, cancellationToken);
    }

    public async Task<List<InventoryItem>> GetByVariantIdsAsync(
        IEnumerable<Guid> productVariantIds,
        CancellationToken cancellationToken)
    {
        var idList = productVariantIds.Distinct().ToList();
        return await _dbContext.InventoryItems
            .Where(i => idList.Contains(i.ProductVariantId))
            .ToListAsync(cancellationToken);
    }

    public async Task SaveAsync(
        InventoryItem item,
        CancellationToken cancellationToken)
    {
        var exists = await _dbContext.InventoryItems
            .AnyAsync(i => i.Id == item.Id, cancellationToken);

        if (!exists)
        {
            await _dbContext.InventoryItems.AddAsync(item, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveRangeAsync(
        IEnumerable<InventoryItem> items,
        CancellationToken cancellationToken)
    {
        var itemList = items.ToList();
        var existingIds = await _dbContext.InventoryItems
            .Where(i => itemList.Select(x => x.Id).Contains(i.Id))
            .Select(i => i.Id)
            .ToListAsync(cancellationToken);

        var newItems = itemList.Where(i => !existingIds.Contains(i.Id)).ToList();
        if (newItems.Count > 0)
        {
            await _dbContext.InventoryItems.AddRangeAsync(newItems, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddStockMovementAsync(
        StockMovement movement,
        CancellationToken cancellationToken)
    {
        await _dbContext.StockMovements.AddAsync(movement, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Warehouse> GetOrCreateDefaultWarehouseAsync(
        CancellationToken cancellationToken)
    {
        var warehouse = await _dbContext.Warehouses
            .FirstOrDefaultAsync(w => w.IsActive, cancellationToken);

        if (warehouse is null)
        {
            warehouse = new Warehouse(
                name: "Dreamflykit Central Fulfillment Center",
                code: "DFK-HYD-01",
                addressLine1: "Plot 42, Aero Logistics Park, Shamshabad",
                city: "Hyderabad",
                state: "Telangana",
                postalCode: "500409",
                country: "India");

            await _dbContext.Warehouses.AddAsync(warehouse, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return warehouse;
    }
}
