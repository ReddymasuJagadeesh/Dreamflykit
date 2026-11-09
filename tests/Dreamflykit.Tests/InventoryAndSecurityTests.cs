using Dreamflykit.Application.Inventory.Interfaces;
using Dreamflykit.Application.Inventory.Services;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Domain.Common;
using Dreamflykit.Domain.Inventory;
using Dreamflykit.Domain.Orders;
using Dreamflykit.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Dreamflykit.Tests;

public class InventoryAndSecurityTests
{
    [Fact]
    public void InventoryItem_ReserveStock_SucceedsWhenStockSufficient()
    {
        // Arrange
        var item = new InventoryItem(Guid.NewGuid(), Guid.NewGuid(), availableQuantity: 10, reservedQuantity: 0);

        // Act
        item.ReserveStock(3);

        // Assert
        Assert.Equal(7, item.AvailableQuantity);
        Assert.Equal(3, item.ReservedQuantity);
        Assert.Equal(10, item.TotalQuantity);
        Assert.Equal(7, item.SellableQuantity);
    }

    [Fact]
    public void InventoryItem_ReserveStock_ThrowsWhenStockInsufficient()
    {
        // Arrange
        var item = new InventoryItem(Guid.NewGuid(), Guid.NewGuid(), availableQuantity: 2, reservedQuantity: 0);

        // Act & Assert
        Assert.Throws<DomainException>(() => item.ReserveStock(5));
    }

    [Fact]
    public void InventoryItem_ReleaseReservation_RestoresAvailableQuantity()
    {
        // Arrange
        var item = new InventoryItem(Guid.NewGuid(), Guid.NewGuid(), availableQuantity: 5, reservedQuantity: 5);

        // Act
        item.ReleaseReservation(2);

        // Assert
        Assert.Equal(7, item.AvailableQuantity);
        Assert.Equal(3, item.ReservedQuantity);
    }

    [Theory]
    [InlineData("../appsettings.json")]
    [InlineData("../../etc/passwd")]
    [InlineData("..\\..\\Windows\\System32\\cmd.exe")]
    [InlineData("C:/secret/passwords.txt")]
    [InlineData("media/../../../Program.cs")]
    public async Task MediaDeliveryController_PathTraversalAttempt_ReturnsBadRequest(string maliciousPath)
    {
        // Arrange
        var storageMock = new Mock<IMediaStorageService>();
        var envMock = new Mock<IWebHostEnvironment>();
        envMock.Setup(e => e.ContentRootPath).Returns(AppContext.BaseDirectory);
        var loggerMock = new Mock<ILogger<MediaDeliveryController>>();

        var controller = new MediaDeliveryController(storageMock.Object, envMock.Object, loggerMock.Object);

        // Act
        var result = await controller.GetMedia(maliciousPath, CancellationToken.None);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Order_RecordPaymentSuccess_IsIdempotent()
    {
        // Arrange
        var addr = new OrderAddress("John Doe", "9999999999", "123 Main St", null, "Bengaluru", "Karnataka", "560001", "India");
        var item = new OrderItem(Guid.NewGuid(), "Sample Product", "Standard", "SKU123", null, 1000m, 1200m, 1);
        var order = new Order(Guid.NewGuid(), "DFK-TEST-1234", addr, addr, new[] { item });

        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Null(order.PaymentId);

        // Act 1: Initial payment notification (e.g. from Webhook)
        order.RecordPaymentSuccess("PAY_12345");

        // Assert 1
        Assert.Equal(OrderStatus.Confirmed, order.Status);
        Assert.Equal("PAY_12345", order.PaymentId);

        // Act 2: Duplicate payment notification (e.g. from browser redirect or duplicate webhook)
        var exception = Record.Exception(() => order.RecordPaymentSuccess("PAY_12345"));

        // Assert 2: Must be idempotent and never throw DomainException
        Assert.Null(exception);
        Assert.Equal(OrderStatus.Confirmed, order.Status);
        Assert.Equal("PAY_12345", order.PaymentId);
    }

    [Fact]
    public async Task InventoryService_ReserveStockAsync_ThrowsWhenItemNotProvisioned()
    {
        // Arrange: Repository returns empty list for requested variant (no inventory item exists)
        var invRepoMock = new Mock<IInventoryRepository>();
        invRepoMock.Setup(r => r.GetByVariantIdsAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<InventoryItem>());

        var warehouse = new Warehouse("Main Warehouse", "BLR-01", "123 Indiranagar", "Bengaluru", "Karnataka", "560038", "India");
        invRepoMock.Setup(r => r.GetOrCreateDefaultWarehouseAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(warehouse);

        var service = new InventoryService(invRepoMock.Object);
        var variantId = Guid.NewGuid();

        // Act & Assert: Must throw DomainException and NOT auto-create 100 units
        var ex = await Assert.ThrowsAsync<DomainException>(() =>
            service.ReserveStockAsync(new[] { (variantId, 1) }, "DFK-TEST-001", CancellationToken.None));

        Assert.Contains("Inventory record not found", ex.Message);
        Assert.Contains("unprovisioned", ex.Message);
    }
}
