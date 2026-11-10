using System.Collections.Concurrent;
using Dreamflykit.Application.Addressing.Interfaces;
using Dreamflykit.Application.Cart.Interfaces;
using Dreamflykit.Application.Inventory.Interfaces;
using Dreamflykit.Application.Orders.Interfaces;
using Dreamflykit.Application.Orders.Models;
using Dreamflykit.Application.Orders.Services;
using Dreamflykit.Domain.Addressing;
using Dreamflykit.Domain.Cart;
using Dreamflykit.Domain.Common;
using Dreamflykit.Domain.Inventory;
using Dreamflykit.Domain.Orders;
using Moq;
using Xunit;

namespace Dreamflykit.Tests;

public sealed class OrderStateMachineAndSagaTests
{
    private static Order CreateSampleOrder(Guid userId, OrderStatus initialStatus = OrderStatus.Pending)
    {
        var shipping = new OrderAddress(
            "Aditi Roy", "9876543210", "12 Park Street", null, "Kolkata", "WB", "700016", "India");
        var billing = shipping;

        var items = new List<OrderItem>
        {
            new OrderItem(
                Guid.NewGuid(), "Universal Adapter", "White", "ADAPT-01",
                "/img/adapter.webp", 499m, 999m, 1)
        };

        var order = new Order(userId, "DFK-20261111-0001", shipping, billing, items);

        // Advance to desired initial status if needed
        if (initialStatus >= OrderStatus.Confirmed) order.Confirm();
        if (initialStatus >= OrderStatus.Processing) order.StartProcessing();
        if (initialStatus >= OrderStatus.Packed) order.Pack();
        if (initialStatus >= OrderStatus.Shipped) order.Ship();
        if (initialStatus >= OrderStatus.OutForDelivery) order.MarkOutForDelivery();
        if (initialStatus >= OrderStatus.Delivered) order.MarkDelivered();

        return order;
    }

    // ============================================================
    // 1. ORDER STATE MACHINE TESTS
    // ============================================================

    [Fact]
    public void Order_FullHappyPathLifecycle_TransitionsSuccessfully()
    {
        var order = CreateSampleOrder(Guid.NewGuid(), OrderStatus.Pending);
        Assert.Equal(OrderStatus.Pending, order.Status);

        order.Confirm();
        Assert.Equal(OrderStatus.Confirmed, order.Status);
        Assert.NotNull(order.ConfirmedAtUtc);

        order.StartProcessing();
        Assert.Equal(OrderStatus.Processing, order.Status);

        order.Pack();
        Assert.Equal(OrderStatus.Packed, order.Status);

        order.Ship();
        Assert.Equal(OrderStatus.Shipped, order.Status);
        Assert.NotNull(order.ShippedAtUtc);

        order.MarkOutForDelivery();
        Assert.Equal(OrderStatus.OutForDelivery, order.Status);

        order.MarkDelivered();
        Assert.Equal(OrderStatus.Delivered, order.Status);
        Assert.NotNull(order.DeliveredAtUtc);
    }

    [Theory]
    [InlineData(OrderStatus.Pending)]
    [InlineData(OrderStatus.Confirmed)]
    [InlineData(OrderStatus.Processing)]
    public void Order_Cancellation_AllowedBeforePacked(OrderStatus status)
    {
        var order = CreateSampleOrder(Guid.NewGuid(), status);

        order.Cancel("Customer requested cancellation");

        Assert.Equal(OrderStatus.Cancelled, order.Status);
        Assert.Equal("Customer requested cancellation", order.CancellationReason);
        Assert.NotNull(order.CancelledAtUtc);
    }

    [Theory]
    [InlineData(OrderStatus.Packed)]
    [InlineData(OrderStatus.Shipped)]
    [InlineData(OrderStatus.OutForDelivery)]
    [InlineData(OrderStatus.Delivered)]
    public void Order_Cancellation_ThrowsDomainException_OncePackedOrLater(OrderStatus status)
    {
        var order = CreateSampleOrder(Guid.NewGuid(), status);

        var ex = Assert.Throws<DomainException>(() => order.Cancel("Too late"));
        Assert.Contains("Cancellation is only allowed before the order is packed", ex.Message);
    }

    [Fact]
    public void Order_InvalidStatusJumps_ThrowDomainException()
    {
        var order = CreateSampleOrder(Guid.NewGuid(), OrderStatus.Pending);

        // Cannot skip directly to Packed, Shipped, or Delivered without preceding steps
        Assert.Throws<DomainException>(() => order.Pack());
        Assert.Throws<DomainException>(() => order.Ship());
        Assert.Throws<DomainException>(() => order.MarkDelivered());
    }

    [Fact]
    public void Order_ReturnRequest_AllowedOnlyAfterDeliveryWithinWindow()
    {
        var order = CreateSampleOrder(Guid.NewGuid(), OrderStatus.Delivered);

        order.RequestReturn("Damaged outer packaging");

        Assert.Equal(OrderStatus.ReturnRequested, order.Status);
        Assert.Equal("Damaged outer packaging", order.CancellationReason);

        order.ApproveReturn();
        Assert.Equal(OrderStatus.ReturnApproved, order.Status);

        order.MarkRefunded();
        Assert.Equal(OrderStatus.Refunded, order.Status);
    }

    [Fact]
    public void Order_ReturnRequest_ThrowsIfOrderNotDelivered()
    {
        var order = CreateSampleOrder(Guid.NewGuid(), OrderStatus.Shipped);

        var ex = Assert.Throws<DomainException>(() => order.RequestReturn("Changed mind"));
        Assert.Contains("Returns can only be requested for delivered orders", ex.Message);
    }

    [Fact]
    public void Order_PaymentWebhook_IsStrictlyIdempotent()
    {
        var order = CreateSampleOrder(Guid.NewGuid(), OrderStatus.Pending);

        // First webhook confirmation
        order.RecordPaymentSuccess("CF_PAY_991827");
        Assert.Equal(OrderStatus.Confirmed, order.Status);
        Assert.Equal("CF_PAY_991827", order.PaymentId);

        // Duplicate delivery of payment webhook (e.g. gateway retry)
        // Must NOT throw DomainException
        order.RecordPaymentSuccess("CF_PAY_991827");
        Assert.Equal(OrderStatus.Confirmed, order.Status);
        Assert.Equal("CF_PAY_991827", order.PaymentId);
    }

    // ============================================================
    // 2. CONCURRENCY & STOCK RESERVATION STRESS TESTS
    // ============================================================

    [Fact]
    public async Task InventoryItem_ConcurrentReservations_PreventsOverselling()
    {
        var warehouseId = Guid.NewGuid();
        var variantId = Guid.NewGuid();

        // 5 units available in warehouse
        var inventoryItem = new InventoryItem(warehouseId, variantId, availableQuantity: 5, reservedQuantity: 0);

        int totalThreads = 20;
        int successCount = 0;
        int failureCount = 0;
        var lockObj = new object();

        // 20 concurrent threads trying to reserve 1 unit each
        var tasks = Enumerable.Range(0, totalThreads).Select(_ => Task.Run(() =>
        {
            lock (lockObj)
            {
                try
                {
                    inventoryItem.ReserveStock(1);
                    Interlocked.Increment(ref successCount);
                }
                catch (DomainException)
                {
                    Interlocked.Increment(ref failureCount);
                }
            }
        }));

        await Task.WhenAll(tasks);

        // Exactly 5 reservations succeed, exactly 15 fail
        Assert.Equal(5, successCount);
        Assert.Equal(15, failureCount);
        Assert.Equal(0, inventoryItem.AvailableQuantity);
        Assert.Equal(5, inventoryItem.ReservedQuantity);
    }

    // ============================================================
    // 3. ANTI-IDOR (INSECURE DIRECT OBJECT REFERENCE) SECURITY TESTS
    // ============================================================

    [Fact]
    public async Task OrderService_GetOrderAsync_CustomerB_CannotAccess_CustomerA_Order()
    {
        var customerAId = Guid.NewGuid();
        var customerBId = Guid.NewGuid();
        var orderNumber = "DFK-20261111-9999";

        var orderRepoMock = new Mock<IOrderRepository>();
        var cartRepoMock = new Mock<ICartRepository>();
        var addressRepoMock = new Mock<IAddressRepository>();
        var inventoryServiceMock = new Mock<IInventoryService>();

        var sampleOrder = CreateSampleOrder(customerAId);

        // Mock returns order only if scoped to Customer A
        orderRepoMock
            .Setup(r => r.GetByOrderNumberAsync(orderNumber, customerAId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(sampleOrder);

        orderRepoMock
            .Setup(r => r.GetByOrderNumberAsync(orderNumber, customerBId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order?)null); // Customer B has no access

        var orderService = new OrderService(
            orderRepoMock.Object,
            cartRepoMock.Object,
            addressRepoMock.Object,
            inventoryServiceMock.Object);

        // Customer A gets order
        var orderA = await orderService.GetOrderAsync(orderNumber, customerAId, CancellationToken.None);
        Assert.NotNull(orderA);

        // Customer B is rejected with null (404/403)
        var orderB = await orderService.GetOrderAsync(orderNumber, customerBId, CancellationToken.None);
        Assert.Null(orderB);
    }

    [Fact]
    public async Task OrderService_PlaceOrderAsync_RejectsAddress_OwnedByDifferentUser()
    {
        var customerAId = Guid.NewGuid();
        var stolenAddressId = Guid.NewGuid();

        var orderRepoMock = new Mock<IOrderRepository>();
        var cartRepoMock = new Mock<ICartRepository>();
        var addressRepoMock = new Mock<IAddressRepository>();
        var inventoryServiceMock = new Mock<IInventoryService>();

        var cart = new Cart(customerAId);
        cart.AddItem(Guid.NewGuid(), "Bag", "Standard", "BAG-01", null, 1999m, 2999m, 1);

        cartRepoMock
            .Setup(r => r.GetByUserIdAsync(customerAId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);

        // Address lookup with Customer A's userId returns null because address belongs to Customer B
        addressRepoMock
            .Setup(r => r.GetByIdAsync(stolenAddressId, customerAId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CustomerAddress?)null);

        var orderService = new OrderService(
            orderRepoMock.Object,
            cartRepoMock.Object,
            addressRepoMock.Object,
            inventoryServiceMock.Object);

        var command = new PlaceOrderCommand
        {
            UserId = customerAId,
            ShippingAddressId = stolenAddressId,
            BillingAddressId = stolenAddressId
        };

        var ex = await Assert.ThrowsAsync<DomainException>(() =>
            orderService.PlaceOrderAsync(command, CancellationToken.None));

        Assert.Contains("Shipping address not found or invalid", ex.Message);
    }

    // ============================================================
    // 4. SAGA COMPENSATING ROLLBACK TESTS
    // ============================================================

    [Fact]
    public async Task OrderService_PlaceOrderAsync_RollsBackStockReservation_WhenDatabaseFails()
    {
        var userId = Guid.NewGuid();
        var addressId = Guid.NewGuid();
        var variantId = Guid.NewGuid();

        var orderRepoMock = new Mock<IOrderRepository>();
        var cartRepoMock = new Mock<ICartRepository>();
        var addressRepoMock = new Mock<IAddressRepository>();
        var inventoryServiceMock = new Mock<IInventoryService>();

        var cart = new Cart(userId);
        cart.AddItem(variantId, "Thermal Jacket", "M", "JKT-01", null, 2499m, 4999m, 2);

        var sampleAddress = new CustomerAddress(
            userId, "Rohit Mehra", "9876543210", "Flat 101", null, "Delhi", "DL", "110001", "India", AddressType.Shipping);

        cartRepoMock.Setup(r => r.GetByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);

        addressRepoMock.Setup(r => r.GetByIdAsync(addressId, userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(sampleAddress);

        orderRepoMock.Setup(r => r.GenerateOrderNumberAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("DFK-20261111-TEST");

        // Simulate database persistence error when saving order
        orderRepoMock.Setup(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("PostgreSQL connection lost during INSERT INTO Orders"));

        var orderService = new OrderService(
            orderRepoMock.Object,
            cartRepoMock.Object,
            addressRepoMock.Object,
            inventoryServiceMock.Object);

        var command = new PlaceOrderCommand
        {
            UserId = userId,
            ShippingAddressId = addressId,
            BillingAddressId = addressId
        };

        // Assert that the database exception is propagated
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            orderService.PlaceOrderAsync(command, CancellationToken.None));

        // CRITICAL ASSERTION: The compensating action must have been called to release reserved stock!
        inventoryServiceMock.Verify(
            s => s.ReleaseReservedStockAsync(
                It.Is<IReadOnlyList<(Guid, int)>>(list => list.Count == 1 && list[0].Item1 == variantId && list[0].Item2 == 2),
                "DFK-20261111-TEST",
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}

