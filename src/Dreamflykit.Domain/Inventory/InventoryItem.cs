using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Inventory;

public sealed class InventoryItem : AuditableEntity
{
    private InventoryItem()
    {
    }

    public InventoryItem(
        Guid warehouseId,
        Guid productVariantId,
        int availableQuantity = 0,
        int reservedQuantity = 0)
    {
        if (warehouseId == Guid.Empty)
        {
            throw new DomainException(
                "Warehouse ID is required.");
        }

        if (productVariantId == Guid.Empty)
        {
            throw new DomainException(
                "Product variant ID is required.");
        }

        if (availableQuantity < 0)
        {
            throw new DomainException(
                "Available quantity cannot be negative.");
        }

        if (reservedQuantity < 0)
        {
            throw new DomainException(
                "Reserved quantity cannot be negative.");
        }

        WarehouseId = warehouseId;
        ProductVariantId = productVariantId;
        AvailableQuantity = availableQuantity;
        ReservedQuantity = reservedQuantity;
    }

    public Guid WarehouseId { get; private set; }

    public Guid ProductVariantId { get; private set; }

    public int AvailableQuantity { get; private set; }

    public int ReservedQuantity { get; private set; }

    public int TotalQuantity =>
        AvailableQuantity + ReservedQuantity;

    public int SellableQuantity =>
        AvailableQuantity;

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new DomainException(
                "Stock quantity must be greater than zero.");
        }

        AvailableQuantity += quantity;

        MarkUpdated();
    }

    public void ReserveStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new DomainException(
                "Reservation quantity must be greater than zero.");
        }

        if (AvailableQuantity < quantity)
        {
            throw new DomainException(
                "Insufficient available stock.");
        }

        AvailableQuantity -= quantity;
        ReservedQuantity += quantity;

        MarkUpdated();
    }

    public void ReleaseReservation(int quantity)
    {
        if (quantity <= 0)
        {
            throw new DomainException(
                "Release quantity must be greater than zero.");
        }

        if (ReservedQuantity < quantity)
        {
            throw new DomainException(
                "Reserved stock is insufficient.");
        }

        ReservedQuantity -= quantity;
        AvailableQuantity += quantity;

        MarkUpdated();
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new DomainException(
                "Stock quantity must be greater than zero.");
        }

        if (AvailableQuantity < quantity)
        {
            throw new DomainException(
                "Insufficient available stock.");
        }

        AvailableQuantity -= quantity;

        MarkUpdated();
    }
}