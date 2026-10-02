using Dreamflykit.Application.Cart.Interfaces;
using Dreamflykit.Application.Cart.Models;
using Dreamflykit.Application.Catalog.Interfaces;

namespace Dreamflykit.Application.Cart.Services;

/// <summary>
/// Orchestrates all cart operations. Validates that variants exist and
/// are active before mutating the cart. All business rules enforced
/// in the Cart aggregate — this service does not duplicate them.
/// </summary>
public sealed class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(
        ICartRepository cartRepository,
        IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    // ============================================================
    // GET CART
    // ============================================================

    public async Task<CartSummary> GetCartAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByUserIdAsync(
            userId, cancellationToken);

        return cart is null
            ? new CartSummary()
            : MapToSummary(cart);
    }

    public async Task<int> GetCartItemCountAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByUserIdAsync(
            userId, cancellationToken);

        return cart?.TotalItems ?? 0;
    }

    // ============================================================
    // ADD ITEM
    // ============================================================

    public async Task AddItemAsync(
        Guid userId,
        AddToCartCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (userId == Guid.Empty)
        {
            throw new ArgumentException(
                "User ID is required.", nameof(userId));
        }

        // Validate the variant is active and get pricing
        var variant = await _productRepository
            .GetActiveVariantAsync(
                command.ProductVariantId,
                cancellationToken);

        if (variant is null)
        {
            throw new InvalidOperationException(
                "Product variant not found or is not available.");
        }

        // Get or create cart
        var cart = await _cartRepository.GetByUserIdAsync(
            userId, cancellationToken)
            ?? new Domain.Cart.Cart(userId);

        cart.AddItem(
            productVariantId: variant.ProductVariantId,
            productName: variant.ProductName,
            variantName: variant.VariantName,
            sku: variant.Sku,
            imageUrl: variant.PrimaryImageUrl,
            unitPrice: variant.SellingPrice,
            mrp: variant.Mrp,
            quantity: command.Quantity);

        await _cartRepository.SaveAsync(cart, cancellationToken);
    }

    // ============================================================
    // UPDATE QUANTITY
    // ============================================================

    public async Task UpdateItemQuantityAsync(
        Guid userId,
        Guid productVariantId,
        int quantity,
        CancellationToken cancellationToken)
    {
        var cart = await GetRequiredCartAsync(userId, cancellationToken);

        cart.UpdateItemQuantity(productVariantId, quantity);

        await _cartRepository.SaveAsync(cart, cancellationToken);
    }

    // ============================================================
    // REMOVE ITEM
    // ============================================================

    public async Task RemoveItemAsync(
        Guid userId,
        Guid productVariantId,
        CancellationToken cancellationToken)
    {
        var cart = await GetRequiredCartAsync(userId, cancellationToken);

        cart.RemoveItem(productVariantId);

        await _cartRepository.SaveAsync(cart, cancellationToken);
    }

    // ============================================================
    // CLEAR CART
    // ============================================================

    public async Task ClearCartAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByUserIdAsync(
            userId, cancellationToken);

        if (cart is null)
        {
            return; // Nothing to clear
        }

        cart.Clear();

        await _cartRepository.SaveAsync(cart, cancellationToken);
    }

    // ============================================================
    // PRIVATE HELPERS
    // ============================================================

    private async Task<Domain.Cart.Cart> GetRequiredCartAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByUserIdAsync(
            userId, cancellationToken);

        if (cart is null)
        {
            throw new InvalidOperationException(
                "Cart not found.");
        }

        return cart;
    }

    private static CartSummary MapToSummary(Domain.Cart.Cart cart)
    {
        return new CartSummary
        {
            CartId = cart.Id,
            Items = cart.Items
                .Select(item => new CartLineItem
                {
                    ProductVariantId = item.ProductVariantId,
                    ProductName = item.ProductName,
                    VariantName = item.VariantName,
                    Sku = item.Sku,
                    ImageUrl = item.ImageUrl,
                    UnitPrice = item.UnitPrice,
                    Mrp = item.Mrp,
                    Quantity = item.Quantity,
                    LineTotal = item.LineTotal,
                    LineMrp = item.LineMrp,
                    LineDiscount = item.LineDiscount,
                    DiscountPercentage = item.DiscountPercentage
                })
                .ToList(),
            TotalItems = cart.TotalItems,
            Subtotal = cart.Subtotal,
            TotalMrp = cart.TotalMrp,
            TotalDiscount = cart.TotalDiscount
        };
    }
}
