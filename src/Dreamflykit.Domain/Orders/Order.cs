using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Orders;

/// <summary>
/// Order aggregate root.
///
/// Design rules enforced here (never in controllers):
/// 1. Cancellation only allowed while status is Pending, Confirmed, or Processing.
/// 2. Return requests only allowed after Delivered.
/// 3. Status transitions follow a strict one-way state machine.
/// 4. Order number is a human-readable, prefixed identifier (e.g. DFK-20260930-0001).
/// 5. All prices are snapshotted at creation time.
/// </summary>
public sealed class Order : AuditableEntity
{
    private readonly List<OrderItem> _items = new();

    // EF Core private constructor
    private Order()
    {
    }

    public Order(
        Guid userId,
        string orderNumber,
        OrderAddress shippingAddress,
        OrderAddress billingAddress,
        IEnumerable<OrderItem> items)
    {
        ArgumentNullException.ThrowIfNull(shippingAddress);
        ArgumentNullException.ThrowIfNull(billingAddress);
        ArgumentNullException.ThrowIfNull(items);

        if (userId == Guid.Empty)
            throw new DomainException("User ID is required.");

        if (string.IsNullOrWhiteSpace(orderNumber))
            throw new DomainException("Order number is required.");

        UserId = userId;
        OrderNumber = orderNumber.Trim().ToUpperInvariant();
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Status = OrderStatus.Pending;
        PlacedAtUtc = DateTime.UtcNow;

        foreach (var item in items)
        {
            _items.Add(item);
        }

        if (_items.Count == 0)
            throw new DomainException("An order must contain at least one item.");
    }

    // ----------------------------------------------------------
    // Properties
    // ----------------------------------------------------------

    public Guid UserId { get; private set; }

    public string OrderNumber { get; private set; } = string.Empty;

    public OrderStatus Status { get; private set; }

    public OrderAddress ShippingAddress { get; private set; } = null!;

    public OrderAddress BillingAddress { get; private set; } = null!;

    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public DateTime PlacedAtUtc { get; private set; }

    public DateTime? ConfirmedAtUtc { get; private set; }

    public DateTime? ShippedAtUtc { get; private set; }

    public DateTime? DeliveredAtUtc { get; private set; }

    public DateTime? CancelledAtUtc { get; private set; }

    public string? CancellationReason { get; private set; }

    /// <summary>Cashfree order ID from payment provider (set after payment initiation).</summary>
    public string? PaymentOrderId { get; private set; }

    /// <summary>Cashfree payment ID (set after successful payment webhook).</summary>
    public string? PaymentId { get; private set; }

    // ----------------------------------------------------------
    // Computed pricing (derived from items — never stored separately)
    // ----------------------------------------------------------

    public decimal Subtotal => _items.Sum(i => i.LineTotal);

    public decimal TotalMrp => _items.Sum(i => i.LineMrp);

    public decimal TotalDiscount => TotalMrp - Subtotal;

    public decimal DeliveryCharge => 0m; // Free delivery (per business rules)

    public decimal GrandTotal => Subtotal + DeliveryCharge;

    // ----------------------------------------------------------
    // Status transition operations (all rules enforced here)
    // ----------------------------------------------------------

    public void Confirm()
    {
        EnsureStatus(OrderStatus.Pending, "Only pending orders can be confirmed.");
        Status = OrderStatus.Confirmed;
        ConfirmedAtUtc = DateTime.UtcNow;
        MarkUpdated();
    }

    public void StartProcessing()
    {
        EnsureStatus(OrderStatus.Confirmed, "Only confirmed orders can be moved to processing.");
        Status = OrderStatus.Processing;
        MarkUpdated();
    }

    public void Pack()
    {
        EnsureStatus(OrderStatus.Processing, "Only processing orders can be packed.");
        Status = OrderStatus.Packed;
        MarkUpdated();
    }

    public void Ship()
    {
        EnsureStatus(OrderStatus.Packed, "Only packed orders can be shipped.");
        Status = OrderStatus.Shipped;
        ShippedAtUtc = DateTime.UtcNow;
        MarkUpdated();
    }

    public void MarkOutForDelivery()
    {
        EnsureStatus(OrderStatus.Shipped, "Only shipped orders can be marked out for delivery.");
        Status = OrderStatus.OutForDelivery;
        MarkUpdated();
    }

    public void MarkDelivered()
    {
        EnsureStatus(OrderStatus.OutForDelivery, "Only out-for-delivery orders can be marked delivered.");
        Status = OrderStatus.Delivered;
        DeliveredAtUtc = DateTime.UtcNow;
        MarkUpdated();
    }

    /// <summary>
    /// Customer-initiated cancellation. Only allowed before Processing is complete.
    /// </summary>
    public void Cancel(string reason)
    {
        if (Status is not (OrderStatus.Pending or OrderStatus.Confirmed or OrderStatus.Processing))
        {
            throw new DomainException(
                $"Order cannot be cancelled in its current state ({Status}). " +
                "Cancellation is only allowed before the order is packed.");
        }

        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("A cancellation reason is required.");

        Status = OrderStatus.Cancelled;
        CancellationReason = reason.Trim();
        CancelledAtUtc = DateTime.UtcNow;
        MarkUpdated();
    }

    /// <summary>
    /// Return request — only allowed after delivery, within 3 days.
    /// </summary>
    public void RequestReturn(string reason)
    {
        if (Status != OrderStatus.Delivered)
        {
            throw new DomainException(
                "Returns can only be requested for delivered orders.");
        }

        if (DeliveredAtUtc.HasValue &&
            DateTime.UtcNow > DeliveredAtUtc.Value.AddDays(3))
        {
            throw new DomainException(
                "The 3-day return window has expired for this order.");
        }

        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("A return reason is required.");

        Status = OrderStatus.ReturnRequested;
        CancellationReason = reason.Trim(); // Reuse field for return reason
        MarkUpdated();
    }

    public void ApproveReturn()
    {
        EnsureStatus(OrderStatus.ReturnRequested, "Only return-requested orders can be approved.");
        Status = OrderStatus.ReturnApproved;
        MarkUpdated();
    }

    public void MarkRefunded()
    {
        EnsureStatus(OrderStatus.ReturnApproved, "Only return-approved orders can be marked refunded.");
        Status = OrderStatus.Refunded;
        MarkUpdated();
    }

    // ----------------------------------------------------------
    // Payment operations
    // ----------------------------------------------------------

    public void SetPaymentOrderId(string paymentOrderId)
    {
        if (string.IsNullOrWhiteSpace(paymentOrderId))
            throw new DomainException("Payment order ID cannot be empty.");

        PaymentOrderId = paymentOrderId.Trim();
        MarkUpdated();
    }

    /// <summary>
    /// Called only from the verified payment webhook — never from a browser redirect.
    /// </summary>
    public void RecordPaymentSuccess(string paymentId)
    {
        if (string.IsNullOrWhiteSpace(paymentId))
            throw new DomainException("Payment ID cannot be empty.");

        PaymentId = paymentId.Trim();
        Confirm(); // Transition to Confirmed on payment success
    }

    // ----------------------------------------------------------
    // Private helpers
    // ----------------------------------------------------------

    private void EnsureStatus(OrderStatus required, string message)
    {
        if (Status != required)
            throw new DomainException(message);
    }
}
