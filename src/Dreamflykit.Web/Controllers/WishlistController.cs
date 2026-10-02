using Dreamflykit.Application.Wishlist.Interfaces;
using Dreamflykit.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dreamflykit.Web.Controllers;

[Authorize]
[Route("[controller]")]
public sealed class WishlistController : Controller
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var summary = await _wishlistService.GetWishlistAsync(userId, cancellationToken);
        return View(summary);
    }

    [HttpGet("count")]
    public async Task<IActionResult> Count(CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var count = await _wishlistService.GetWishlistItemCountAsync(userId, cancellationToken);
        return Json(new { count });
    }

    [HttpPost("toggle")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(
        [FromForm] Guid productVariantId,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        try
        {
            var result = await _wishlistService.ToggleWishlistItemAsync(
                userId, productVariantId, cancellationToken);

            return Json(new
            {
                success = true,
                isInWishlist = result.IsInWishlist,
                count = result.TotalWishlistItems,
                message = result.Message
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    [HttpPost("remove")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(
        [FromForm] Guid productVariantId,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        await _wishlistService.RemoveItemAsync(userId, productVariantId, cancellationToken);
        TempData["SuccessMessage"] = "Item removed from wishlist.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("move-to-cart")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MoveToCart(
        [FromForm] Guid productVariantId,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        try
        {
            await _wishlistService.MoveToCartAsync(userId, productVariantId, cancellationToken);
            TempData["SuccessMessage"] = "Item moved to your cart!";
            return RedirectToAction("Index", "Cart");
        }
        catch (DomainException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    private Guid GetCurrentUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId))
        {
            throw new InvalidOperationException("User ID claim is missing or invalid.");
        }
        return userId;
    }
}
