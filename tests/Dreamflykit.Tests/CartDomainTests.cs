using Dreamflykit.Domain.Cart;
using Dreamflykit.Domain.Common;

namespace Dreamflykit.Tests;

public class CartDomainTests
{
    [Fact]
    public void AddItem_ValidInputs_AddsItemToCart()
    {
        // Arrange
        var cart = new Cart(Guid.NewGuid());
        var variantId = Guid.NewGuid();

        // Act
        cart.AddItem(
            variantId,
            "Notebook Pro 200 Pages",
            "A4 Ruled",
            "SKU-NOTE-01",
            "https://cdn.dreamflykit.com/notebook.png",
            unitPrice: 199,
            mrp: 299,
            quantity: 2);

        // Assert
        Assert.Single(cart.Items);
        Assert.Equal(2, cart.TotalItems);
        Assert.Equal(398, cart.Subtotal);
        Assert.Equal(598, cart.TotalMrp);
        Assert.Equal(200, cart.TotalDiscount);
    }

    [Fact]
    public void AddItem_DuplicateVariant_MergesQuantities()
    {
        // Arrange
        var cart = new Cart(Guid.NewGuid());
        var variantId = Guid.NewGuid();

        // Act
        cart.AddItem(variantId, "Scientific Calculator", "Black", "SKU-CALC-01", null, 1499, 1999, 1);
        cart.AddItem(variantId, "Scientific Calculator", "Black", "SKU-CALC-01", null, 1499, 1999, 3);

        // Assert
        Assert.Single(cart.Items);
        Assert.Equal(4, cart.TotalItems);
        Assert.Equal(4 * 1499, cart.Subtotal);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(100)]
    public void AddItem_InvalidQuantity_ThrowsDomainException(int invalidQty)
    {
        // Arrange
        var cart = new Cart(Guid.NewGuid());
        var variantId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<DomainException>(() =>
            cart.AddItem(variantId, "Desk Lamp", "LED", "SKU-LAMP", null, 500, 700, invalidQty));
    }

    [Fact]
    public void AddItem_SellingPriceGreaterThanMrp_ThrowsDomainException()
    {
        // Arrange
        var cart = new Cart(Guid.NewGuid());
        var variantId = Guid.NewGuid();

        // Act & Assert: selling price 1200 > MRP 1000
        Assert.Throws<DomainException>(() =>
            cart.AddItem(variantId, "Travel Bag", "Blue", "SKU-BAG", null, unitPrice: 1200, mrp: 1000, quantity: 1));
    }

    [Fact]
    public void UpdateItemQuantity_SetToZero_RemovesLineItem()
    {
        // Arrange
        var cart = new Cart(Guid.NewGuid());
        var variantId = Guid.NewGuid();
        cart.AddItem(variantId, "Pen Pack", "Blue", "SKU-PEN", null, 99, 150, 3);

        // Act
        cart.UpdateItemQuantity(variantId, 0);

        // Assert
        Assert.Empty(cart.Items);
        Assert.Equal(0, cart.TotalItems);
        Assert.Equal(0, cart.Subtotal);
    }
}
