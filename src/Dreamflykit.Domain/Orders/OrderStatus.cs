namespace Dreamflykit.Domain.Orders;

/// <summary>
/// Order lifecycle states. Transitions are enforced in the Order aggregate.
/// Cancellation is only allowed while status is Pending, Confirmed, or Processing.
/// </summary>
public enum OrderStatus
{
    Pending = 1,
    Confirmed = 2,
    Processing = 3,
    Packed = 4,
    Shipped = 5,
    OutForDelivery = 6,
    Delivered = 7,
    Cancelled = 8,
    ReturnRequested = 9,
    ReturnApproved = 10,
    Refunded = 11
}
