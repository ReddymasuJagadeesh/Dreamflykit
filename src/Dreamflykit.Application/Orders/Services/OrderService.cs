using Dreamflykit.Application.Addressing.Interfaces;
using Dreamflykit.Application.Cart.Interfaces;
using Dreamflykit.Application.Inventory.Interfaces;
using Dreamflykit.Application.Orders.Interfaces;
using Dreamflykit.Application.Orders.Models;
using Dreamflykit.Domain.Common;
using Dreamflykit.Domain.Orders;

namespace Dreamflykit.Application.Orders.Services;

public sealed class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IInventoryService _inventoryService;

    public OrderService(
        IOrderRepository orderRepository,
        ICartRepository cartRepository,
        IAddressRepository addressRepository,
        IInventoryService inventoryService)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _addressRepository = addressRepository;
        _inventoryService = inventoryService;
    }

    public async Task<string> PlaceOrderAsync(
        PlaceOrderCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (command.UserId == Guid.Empty)
            throw new DomainException("Valid user ID is required to place an order.");

        // 1. Fetch user's cart
        var cart = await _cartRepository.GetByUserIdAsync(command.UserId, cancellationToken);
        if (cart is null || cart.Items.Count == 0)
        {
            throw new DomainException("Cannot place order: cart is empty.");
        }

        // 2. Fetch shipping and billing addresses scoped to user (prevent IDOR)
        var shippingAddr = await _addressRepository.GetByIdAsync(
            command.ShippingAddressId, command.UserId, cancellationToken);

        if (shippingAddr is null)
            throw new DomainException("Shipping address not found or invalid.");

        var billingAddr = command.BillingAddressId == command.ShippingAddressId
            ? shippingAddr
            : await _addressRepository.GetByIdAsync(
                command.BillingAddressId, command.UserId, cancellationToken);

        if (billingAddr is null)
            throw new DomainException("Billing address not found or invalid.");

        // 3. Snapshot addresses into immutable OrderAddress
        var shippingSnapshot = new OrderAddress(
            shippingAddr.FullName,
            shippingAddr.Phone,
            shippingAddr.AddressLine1,
            shippingAddr.AddressLine2,
            shippingAddr.City,
            shippingAddr.State,
            shippingAddr.PostalCode,
            shippingAddr.Country);

        var billingSnapshot = new OrderAddress(
            billingAddr.FullName,
            billingAddr.Phone,
            billingAddr.AddressLine1,
            billingAddr.AddressLine2,
            billingAddr.City,
            billingAddr.State,
            billingAddr.PostalCode,
            billingAddr.Country);

        // 4. Snapshot order items from cart items
        var orderItems = cart.Items.Select(ci => new OrderItem(
            ci.ProductVariantId,
            ci.ProductName,
            ci.VariantName,
            ci.Sku,
            ci.ImageUrl,
            ci.UnitPrice,
            ci.Mrp,
            ci.Quantity)).ToList();

        // 5. Generate atomic order number
        var orderNumber = await _orderRepository.GenerateOrderNumberAsync(cancellationToken);

        // 6. Atomically reserve inventory to prevent overselling
        var itemsToReserve = cart.Items.Select(ci => (ci.ProductVariantId, ci.Quantity)).ToList();
        await _inventoryService.ReserveStockAsync(itemsToReserve, orderNumber, cancellationToken);

        // 7. Create Order aggregate
        var order = new Order(
            command.UserId,
            orderNumber,
            shippingSnapshot,
            billingSnapshot,
            orderItems);

        // 8. Persist order
        await _orderRepository.AddAsync(order, cancellationToken);

        // 9. Clear user's cart
        cart.Clear();
        await _cartRepository.SaveAsync(cart, cancellationToken);

        return order.OrderNumber;
    }

    public async Task<OrderDetails?> GetOrderAsync(
        string orderNumber,
        Guid userId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(orderNumber) || userId == Guid.Empty)
            return null;

        var order = await _orderRepository.GetByOrderNumberAsync(orderNumber.Trim(), userId, cancellationToken);
        if (order is null)
            return null;

        return MapToDetails(order);
    }

    public async Task<OrderListResult> GetOrdersAsync(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
            return new OrderListResult();

        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 50);

        var totalCount = await _orderRepository.CountByUserIdAsync(userId, cancellationToken);
        var orders = await _orderRepository.GetByUserIdAsync(userId, page, pageSize, cancellationToken);

        var summaries = orders.Select(o => new OrderSummary
        {
            OrderId = o.Id,
            OrderNumber = o.OrderNumber,
            Status = o.Status,
            StatusDisplay = GetStatusDisplay(o.Status),
            PlacedAtUtc = o.PlacedAtUtc,
            ItemCount = o.Items.Sum(i => i.Quantity),
            GrandTotal = o.GrandTotal,
            CanCancel = o.Status is OrderStatus.Pending or OrderStatus.Confirmed or OrderStatus.Processing
        }).ToList();

        return new OrderListResult
        {
            Orders = summaries,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task CancelOrderAsync(
        string orderNumber,
        Guid userId,
        string reason,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(orderNumber) || userId == Guid.Empty)
            throw new DomainException("Order not found.");

        var order = await _orderRepository.GetByOrderNumberAsync(orderNumber.Trim(), userId, cancellationToken);
        if (order is null)
            throw new DomainException("Order not found.");

        order.Cancel(reason);
        await _orderRepository.UpdateAsync(order, cancellationToken);

        // Atomically release reserved stock back into available inventory
        var itemsToRelease = order.Items.Select(i => (i.ProductVariantId, i.Quantity)).ToList();
        await _inventoryService.ReleaseReservedStockAsync(itemsToRelease, order.OrderNumber, reason, cancellationToken);
    }

    public async Task RequestReturnAsync(
        string orderNumber,
        Guid userId,
        string reason,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(orderNumber) || userId == Guid.Empty)
            throw new DomainException("Order not found.");

        var order = await _orderRepository.GetByOrderNumberAsync(orderNumber.Trim(), userId, cancellationToken);
        if (order is null)
            throw new DomainException("Order not found.");

        order.RequestReturn(reason);
        await _orderRepository.UpdateAsync(order, cancellationToken);
    }

    private static OrderDetails MapToDetails(Order order)
    {
        var canCancel = order.Status is OrderStatus.Pending or OrderStatus.Confirmed or OrderStatus.Processing;
        var canReturn = order.Status == OrderStatus.Delivered &&
            (!order.DeliveredAtUtc.HasValue || DateTime.UtcNow <= order.DeliveredAtUtc.Value.AddDays(3));

        return new OrderDetails
        {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            Status = order.Status,
            StatusDisplay = GetStatusDisplay(order.Status),
            PlacedAtUtc = order.PlacedAtUtc,
            DeliveredAtUtc = order.DeliveredAtUtc,
            CanCancel = canCancel,
            CanReturn = canReturn,
            ShippingAddress = MapAddress(order.ShippingAddress),
            BillingAddress = MapAddress(order.BillingAddress),
            Items = order.Items.Select(i => new OrderItemDto
            {
                ProductVariantId = i.ProductVariantId,
                ProductName = i.ProductName,
                VariantName = i.VariantName,
                Sku = i.Sku,
                ImageUrl = i.ImageUrl,
                UnitPrice = i.UnitPrice,
                Mrp = i.Mrp,
                Quantity = i.Quantity,
                LineTotal = i.LineTotal
            }).ToList(),
            Subtotal = order.Subtotal,
            TotalMrp = order.TotalMrp,
            TotalDiscount = order.TotalDiscount,
            DeliveryCharge = order.DeliveryCharge,
            GrandTotal = order.GrandTotal
        };
    }

    private static OrderAddressDto MapAddress(OrderAddress a) => new()
    {
        FullName = a.FullName,
        Phone = a.Phone,
        AddressLine1 = a.AddressLine1,
        AddressLine2 = a.AddressLine2,
        City = a.City,
        State = a.State,
        PostalCode = a.PostalCode,
        Country = a.Country,
        DisplayString = a.ToDisplayString()
    };

    public async Task SetPaymentOrderIdAsync(
        string orderNumber,
        string paymentOrderId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            throw new DomainException("Order number is required.");

        var order = await _orderRepository.GetByOrderNumberAsync(orderNumber, cancellationToken);
        if (order is null)
            throw new DomainException($"Order {orderNumber} not found.");

        order.SetPaymentOrderId(paymentOrderId);
        await _orderRepository.UpdateAsync(order, cancellationToken);
    }

    public async Task MarkPaymentCompletedAsync(
        string orderNumber,
        string paymentId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            throw new DomainException("Order number is required.");

        var order = await _orderRepository.GetByOrderNumberAsync(orderNumber, cancellationToken);
        if (order is null)
            throw new DomainException($"Order {orderNumber} not found.");

        order.RecordPaymentSuccess(paymentId);
        await _orderRepository.UpdateAsync(order, cancellationToken);

        // Commit reserved stock into confirmed inventory state
        var itemsToCommit = order.Items.Select(i => (i.ProductVariantId, i.Quantity)).ToList();
        await _inventoryService.CommitReservedStockAsync(itemsToCommit, order.OrderNumber, cancellationToken);
    }

    public async Task ApproveReturnAsync(
        string orderNumber,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            throw new DomainException("Order number is required.");

        var order = await _orderRepository.GetByOrderNumberAsync(orderNumber.Trim(), cancellationToken);
        if (order is null)
            throw new DomainException($"Order {orderNumber} not found.");

        order.ApproveReturn();
        await _orderRepository.UpdateAsync(order, cancellationToken);
    }

    public async Task ProcessRefundAsync(
        string orderNumber,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            throw new DomainException("Order number is required.");

        var order = await _orderRepository.GetByOrderNumberAsync(orderNumber.Trim(), cancellationToken);
        if (order is null)
            throw new DomainException($"Order {orderNumber} not found.");

        order.MarkRefunded();
        await _orderRepository.UpdateAsync(order, cancellationToken);

        // Restock returned items back to available inventory
        var itemsToRestock = order.Items.Select(i => (i.ProductVariantId, i.Quantity)).ToList();
        await _inventoryService.RestockReturnedItemsAsync(
            itemsToRestock,
            order.OrderNumber,
            order.CancellationReason ?? "Customer Return Processed",
            cancellationToken);
    }

    private static string GetStatusDisplay(OrderStatus status) => status switch
    {
        OrderStatus.Pending => "Pending",
        OrderStatus.Confirmed => "Confirmed",
        OrderStatus.Processing => "Processing",
        OrderStatus.Packed => "Packed",
        OrderStatus.Shipped => "Shipped",
        OrderStatus.OutForDelivery => "Out for Delivery",
        OrderStatus.Delivered => "Delivered",
        OrderStatus.Cancelled => "Cancelled",
        OrderStatus.ReturnRequested => "Return Requested",
        OrderStatus.ReturnApproved => "Return Approved",
        OrderStatus.Refunded => "Refunded",
        _ => status.ToString()
    };
}
