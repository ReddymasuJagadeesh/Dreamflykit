using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Inventory;

public sealed class StockMovement : AuditableEntity
{
    private StockMovement()
    {
    }

    public StockMovement(
        Guid warehouseId,
        Guid productVariantId,
        StockMovementType movementType,
        int quantity,
        string reason)
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

        if (quantity <= 0)
        {
            throw new DomainException(
                "Movement quantity must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new DomainException(
                "Movement reason is required.");
        }

        WarehouseId = warehouseId;
        ProductVariantId = productVariantId;
        MovementType = movementType;
        Quantity = quantity;
        Reason = reason.Trim();
        OccurredAtUtc = DateTime.UtcNow;
    }

    public Guid WarehouseId { get; private set; }

    public Guid ProductVariantId { get; private set; }

    public StockMovementType MovementType { get; private set; }

    public int Quantity { get; private set; }

    public string Reason { get; private set; } =
        string.Empty;

    public DateTime OccurredAtUtc { get; private set; }
}

public enum StockMovementType
{
    StockIn = 1,
    StockOut = 2,
    Reservation = 3,
    ReservationRelease = 4,
    Adjustment = 5,
    Return = 6
}