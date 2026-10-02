using Dreamflykit.Application.Cart.Models;
using Dreamflykit.Application.Orders.Models;

namespace Dreamflykit.Application.Orders.Interfaces;

public interface IOrderService
{
    /// <summary>
    /// Creates and persists a new order from the user's current cart.
    /// Validates cart is non-empty, addresses exist, clears cart on success.
    /// Returns the new order number for redirect.
    /// </summary>
    Task<string> PlaceOrderAsync(
        PlaceOrderCommand command,
        CancellationToken cancellationToken);

    Task<OrderDetails?> GetOrderAsync(
        string orderNumber,
        Guid userId,
        CancellationToken cancellationToken);

    Task<OrderListResult> GetOrdersAsync(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    /// <summary>
    /// Customer self-cancellation. Only succeeds if order is in a
    /// cancellable state (Pending, Confirmed, Processing).
    /// </summary>
    Task CancelOrderAsync(
        string orderNumber,
        Guid userId,
        string reason,
        CancellationToken cancellationToken);

    /// <summary>
    /// Customer return request. Only succeeds if order was Delivered
    /// within the last 3 days.
    /// </summary>
    Task RequestReturnAsync(
        string orderNumber,
        Guid userId,
        string reason,
        CancellationToken cancellationToken);

    /// <summary>
    /// Updates the order's payment session / order ID from payment gateway.
    /// </summary>
    Task SetPaymentOrderIdAsync(
        string orderNumber,
        string paymentOrderId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Verifies and transitions order to Confirmed upon successful payment.
    /// </summary>
    Task MarkPaymentCompletedAsync(
        string orderNumber,
        string paymentId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Admin return approval after customer returns items.
    /// </summary>
    Task ApproveReturnAsync(
        string orderNumber,
        CancellationToken cancellationToken);

    /// <summary>
    /// Admin refund processing and inventory restock.
    /// </summary>
    Task ProcessRefundAsync(
        string orderNumber,
        CancellationToken cancellationToken);
}
