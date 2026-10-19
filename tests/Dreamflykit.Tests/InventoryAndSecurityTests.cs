using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Domain.Common;
using Dreamflykit.Domain.Inventory;
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
}
