using Dreamflykit.Application.Cart.Interfaces;
using Dreamflykit.Application.Cart.Models;
using Dreamflykit.Web.Models.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dreamflykit.Web.Controllers;

/// <summary>
/// Handles all shopping cart operations.
/// All actions require authentication — guests are redirected to login.
/// All POST actions use anti-forgery tokens to prevent CSRF.
/// </summary>
[Authorize]
public sealed class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    // ============================================================
    // CART PAGE
    // GET /cart
    // ============================================================

    [HttpGet("/cart")]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var summary = await _cartService.GetCartAsync(
            userId, cancellationToken);

        var model = MapToViewModel(summary);

        return View(model);
    }

    // ============================================================
    // CART ITEM COUNT (for mini-cart badge)
    // GET /cart/count
    // ============================================================

    [HttpGet("/cart/count")]
    public async Task<IActionResult> Count(
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var count = await _cartService.GetCartItemCountAsync(
            userId, cancellationToken);

        return Json(new { count });
    }

    // ============================================================
    // ADD TO CART
    // POST /cart/add
    // ============================================================

    [HttpPost("/cart/add")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(
        AddToCartFormModel form,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["CartError"] = "Invalid request.";
            return RedirectToAction("Index");
        }

        var userId = GetCurrentUserId();

        try
        {
            await _cartService.AddItemAsync(
                userId,
                new AddToCartCommand
                {
                    ProductVariantId = form.ProductVariantId,
                    Quantity = form.Quantity
                },
                cancellationToken);

            TempData["CartSuccess"] = "Item added to cart.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["CartError"] = ex.Message;
        }
        catch (Domain.Common.DomainException ex)
        {
            TempData["CartError"] = ex.Message;
        }

        // If an explicit return URL was provided, go back there
        if (!string.IsNullOrWhiteSpace(form.ReturnUrl) &&
            Url.IsLocalUrl(form.ReturnUrl))
        {
            return Redirect(form.ReturnUrl);
        }

        return RedirectToAction("Index");
    }

    // ============================================================
    // UPDATE QUANTITY
    // POST /cart/update
    // ============================================================

    [HttpPost("/cart/update")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateQuantity(
        UpdateCartItemFormModel form,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["CartError"] = "Invalid request.";
            return RedirectToAction("Index");
        }

        var userId = GetCurrentUserId();

        try
        {
            await _cartService.UpdateItemQuantityAsync(
                userId,
                form.ProductVariantId,
                form.Quantity,
                cancellationToken);
        }
        catch (Domain.Common.DomainException ex)
        {
            TempData["CartError"] = ex.Message;
        }

        return RedirectToAction("Index");
    }

    // ============================================================
    // REMOVE ITEM
    // POST /cart/remove
    // ============================================================

    [HttpPost("/cart/remove")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveItem(
        RemoveCartItemFormModel form,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["CartError"] = "Invalid request.";
            return RedirectToAction("Index");
        }

        var userId = GetCurrentUserId();

        try
        {
            await _cartService.RemoveItemAsync(
                userId,
                form.ProductVariantId,
                cancellationToken);
        }
        catch (Domain.Common.DomainException ex)
        {
            TempData["CartError"] = ex.Message;
        }

        return RedirectToAction("Index");
    }

    // ============================================================
    // CLEAR CART
    // POST /cart/clear
    // ============================================================

    [HttpPost("/cart/clear")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ClearCart(
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        await _cartService.ClearCartAsync(userId, cancellationToken);

        return RedirectToAction("Index");
    }

    // ============================================================
    // PRIVATE HELPERS
    // ============================================================

    private Guid GetCurrentUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(claim, out var userId))
        {
            throw new InvalidOperationException(
                "Authenticated user has no valid ID claim.");
        }

        return userId;
    }

    private static CartViewModel MapToViewModel(
        CartSummary summary)
    {
        return new CartViewModel
        {
            IsEmpty = summary.IsEmpty,
            TotalItems = summary.TotalItems,
            Subtotal = summary.Subtotal,
            TotalMrp = summary.TotalMrp,
            TotalDiscount = summary.TotalDiscount,
            Items = summary.Items
                .Select(item => new CartItemViewModel
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
                .ToList()
        };
    }
}
