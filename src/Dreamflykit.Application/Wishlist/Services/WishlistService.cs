using Dreamflykit.Application.Cart.Interfaces;
using Dreamflykit.Application.Cart.Models;
using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Wishlist.Interfaces;
using Dreamflykit.Application.Wishlist.Models;
using Dreamflykit.Domain.Common;

namespace Dreamflykit.Application.Wishlist.Services;

public sealed class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly ICartService _cartService;
    private readonly IProductRepository _productRepository;

    public WishlistService(
        IWishlistRepository wishlistRepository,
        ICartService cartService,
        IProductRepository productRepository)
    {
        _wishlistRepository = wishlistRepository;
        _cartService = cartService;
        _productRepository = productRepository;
    }

    public async Task<WishlistSummary> GetWishlistAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
            return new WishlistSummary();

        var wishlist = await _wishlistRepository.GetByUserIdAsync(userId, cancellationToken);
        if (wishlist is null || wishlist.Items.Count == 0)
        {
            return new WishlistSummary();
        }

        var items = wishlist.Items.Select(i => new WishlistItemDto
        {
            ProductVariantId = i.ProductVariantId,
            ProductName = i.ProductName,
            VariantName = i.VariantName,
            Sku = i.Sku,
            ImageUrl = i.ImageUrl,
            UnitPrice = i.UnitPrice,
            Mrp = i.Mrp,
            DiscountPercentage = i.DiscountPercentage
        }).ToList();

        return new WishlistSummary
        {
            TotalItems = wishlist.TotalItems,
            Items = items
        };
    }

    public async Task<int> GetWishlistItemCountAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
            return 0;

        var wishlist = await _wishlistRepository.GetByUserIdAsync(userId, cancellationToken);
        return wishlist?.TotalItems ?? 0;
    }

    public async Task<ToggleWishlistResult> ToggleWishlistItemAsync(
        Guid userId,
        Guid productVariantId,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
            throw new DomainException("Sign in required to manage your wishlist.");

        var variant = await _productRepository.GetActiveVariantAsync(productVariantId, cancellationToken);
        if (variant is null)
            throw new DomainException("Product variant not found or no longer available.");

        var wishlist = await _wishlistRepository.GetByUserIdAsync(userId, cancellationToken)
            ?? new Domain.Wishlist.Wishlist(userId);

        bool isInWishlist;
        string message;

        if (wishlist.ContainsItem(productVariantId))
        {
            wishlist.RemoveItem(productVariantId);
            isInWishlist = false;
            message = "Removed from your Wishlist.";
        }
        else
        {
            wishlist.AddItem(
                variant.ProductVariantId,
                variant.ProductName,
                variant.VariantName,
                variant.Sku,
                variant.PrimaryImageUrl,
                variant.SellingPrice,
                variant.Mrp);
            isInWishlist = true;
            message = "Added to your Wishlist!";
        }

        await _wishlistRepository.SaveAsync(wishlist, cancellationToken);

        return new ToggleWishlistResult
        {
            IsInWishlist = isInWishlist,
            TotalWishlistItems = wishlist.TotalItems,
            Message = message
        };
    }

    public async Task RemoveItemAsync(
        Guid userId,
        Guid productVariantId,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
            return;

        var wishlist = await _wishlistRepository.GetByUserIdAsync(userId, cancellationToken);
        if (wishlist is null)
            return;

        wishlist.RemoveItem(productVariantId);
        await _wishlistRepository.SaveAsync(wishlist, cancellationToken);
    }

    public async Task MoveToCartAsync(
        Guid userId,
        Guid productVariantId,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
            throw new DomainException("Sign in required to move item to cart.");

        var wishlist = await _wishlistRepository.GetByUserIdAsync(userId, cancellationToken);
        if (wishlist is null || !wishlist.ContainsItem(productVariantId))
            throw new DomainException("Item not found in wishlist.");

        // 1. Add to cart
        await _cartService.AddItemAsync(
            userId,
            new AddToCartCommand { ProductVariantId = productVariantId, Quantity = 1 },
            cancellationToken);

        // 2. Remove from wishlist
        wishlist.RemoveItem(productVariantId);
        await _wishlistRepository.SaveAsync(wishlist, cancellationToken);
    }
}
