using Dreamflykit.Domain.Audit;
using Dreamflykit.Domain.Common;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Dreamflykit.Tests;

public sealed class DatabaseArchitectureAndAuditTests
{
    private static DreamflykitDbContext CreateTestDbContext()
    {
        var options = new DbContextOptionsBuilder<DreamflykitDbContext>()
            .UseNpgsql("Host=localhost;Database=dreamflykit_test_metadata;Username=postgres;Password=postgres")
            .Options;

        return new DreamflykitDbContext(options);
    }

    [Fact]
    public void AuditLog_WithValidInputs_InstantiatesWithUtcTimestampAndProperties()
    {
        var userId = Guid.NewGuid();
        var traceId = "req-test-12345";
        var log = new AuditLog(
            userId: userId,
            userName: "admin@dreamflykit.com",
            actorType: "Admin",
            action: "UPDATE_PRICE",
            entityName: "ProductVariant",
            tableName: "ProductVariants",
            recordId: Guid.NewGuid().ToString(),
            oldValues: "{\"SellingPrice\": 999.00}",
            newValues: "{\"SellingPrice\": 849.00}",
            affectedColumns: "SellingPrice",
            ipAddress: "192.168.1.100",
            userAgent: "Mozilla/5.0 (Windows NT 10.0; Win64; x64)",
            traceId: traceId,
            isSuccess: true,
            errorMessage: null,
            notes: "Festive discount promotion approved");

        Assert.Equal(userId, log.UserId);
        Assert.Equal("admin@dreamflykit.com", log.UserName);
        Assert.Equal("Admin", log.ActorType);
        Assert.Equal("UPDATE_PRICE", log.Action);
        Assert.Equal("ProductVariant", log.EntityName);
        Assert.Equal("ProductVariants", log.TableName);
        Assert.NotEmpty(log.RecordId);
        Assert.Contains("999.00", log.OldValues);
        Assert.Contains("849.00", log.NewValues);
        Assert.Equal("SellingPrice", log.AffectedColumns);
        Assert.Equal("192.168.1.100", log.IpAddress);
        Assert.Equal(traceId, log.TraceId);
        Assert.True(log.IsSuccess);
        Assert.Null(log.ErrorMessage);
        Assert.Equal("Festive discount promotion approved", log.Notes);
        Assert.True((DateTime.UtcNow - log.OccurredAtUtc).TotalSeconds < 5);
    }

    [Theory]
    [InlineData("", "INSERT", "Order", "Orders", "123")]
    [InlineData("User", "", "Order", "Orders", "123")]
    [InlineData("User", "INSERT", "", "Orders", "123")]
    [InlineData("User", "INSERT", "Order", "", "123")]
    [InlineData("User", "INSERT", "Order", "Orders", "")]
    public void AuditLog_WithMissingRequiredFields_ThrowsDomainException(
        string actorType, string action, string entityName, string tableName, string recordId)
    {
        Assert.Throws<DomainException>(() => new AuditLog(
            userId: Guid.NewGuid(),
            userName: "user@test.com",
            actorType: actorType,
            action: action,
            entityName: entityName,
            tableName: tableName,
            recordId: recordId,
            oldValues: null,
            newValues: null,
            affectedColumns: null,
            ipAddress: null,
            userAgent: null,
            traceId: null,
            isSuccess: true,
            errorMessage: null,
            notes: null));
    }

    [Fact]
    public void DbContext_ModelBuilder_ConfiguresAuditLogsAndCheckConstraints()
    {
        using var dbContext = CreateTestDbContext();
        var designModel = Microsoft.EntityFrameworkCore.Infrastructure.AccessorExtensions.GetService<Microsoft.EntityFrameworkCore.Metadata.IDesignTimeModel>(dbContext).Model;

        // Verify AuditLog entity is registered in EF model
        var auditEntity = designModel.FindEntityType(typeof(AuditLog));
        Assert.NotNull(auditEntity);
        Assert.Equal("AuditLogs", auditEntity.GetTableName());

        // Verify ProductVariant check constraints
        var productVariantEntity = designModel.FindEntityType(typeof(Dreamflykit.Domain.Catalog.ProductVariant));
        Assert.NotNull(productVariantEntity);
        var pvConstraints = productVariantEntity.GetCheckConstraints().Select(c => c.Name).ToList();
        Assert.Contains("CK_ProductVariants_SellingPrice_NonNegative", pvConstraints);
        Assert.Contains("CK_ProductVariants_Mrp_NonNegative", pvConstraints);
        Assert.Contains("CK_ProductVariants_SellingPrice_Lte_Mrp", pvConstraints);

        // Verify InventoryItem check constraints
        var inventoryEntity = designModel.FindEntityType(typeof(Dreamflykit.Domain.Inventory.InventoryItem));
        Assert.NotNull(inventoryEntity);
        var invConstraints = inventoryEntity.GetCheckConstraints().Select(c => c.Name).ToList();
        Assert.Contains("CK_InventoryItems_AvailableQuantity_NonNegative", invConstraints);
        Assert.Contains("CK_InventoryItems_ReservedQuantity_NonNegative", invConstraints);

        // Verify OrderItem check constraints
        var orderItemEntity = designModel.FindEntityType(typeof(Dreamflykit.Domain.Orders.OrderItem));
        Assert.NotNull(orderItemEntity);
        var oiConstraints = orderItemEntity.GetCheckConstraints().Select(c => c.Name).ToList();
        Assert.Contains("CK_OrderItems_Quantity_Positive", oiConstraints);
        Assert.Contains("CK_OrderItems_UnitPrice_NonNegative", oiConstraints);
        Assert.Contains("CK_OrderItems_Mrp_NonNegative", oiConstraints);

        // Verify CartItem check constraints
        var cartItemEntity = designModel.FindEntityType(typeof(Dreamflykit.Domain.Cart.CartItem));
        Assert.NotNull(cartItemEntity);
        var ciConstraints = cartItemEntity.GetCheckConstraints().Select(c => c.Name).ToList();
        Assert.Contains("CK_CartItems_Quantity_Positive", ciConstraints);
        Assert.Contains("CK_CartItems_UnitPrice_NonNegative", ciConstraints);
    }
}
