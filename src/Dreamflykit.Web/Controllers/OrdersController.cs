using Dreamflykit.Application.Orders.Interfaces;
using Dreamflykit.Domain.Common;
using Dreamflykit.Web.Models.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dreamflykit.Web.Controllers;

[Authorize]
[Route("[controller]")]
public sealed class OrdersController : Controller
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(
        [FromQuery] int page = 1,
        CancellationToken cancellationToken = default)
    {
        var userId = GetCurrentUserId();
        var result = await _orderService.GetOrdersAsync(userId, page, 10, cancellationToken);
        return View(result);
    }

    [HttpGet("details/{orderNumber}")]
    public async Task<IActionResult> Details(
        [FromRoute] string orderNumber,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var order = await _orderService.GetOrderAsync(orderNumber, userId, cancellationToken);

        if (order is null)
        {
            return NotFound();
        }

        return View(order);
    }

    [HttpGet("confirmation")]
    public async Task<IActionResult> Confirmation(
        [FromQuery] string orderNumber,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var order = await _orderService.GetOrderAsync(orderNumber, userId, cancellationToken);

        if (order is null)
        {
            return RedirectToAction(nameof(Index));
        }

        var vm = new OrderConfirmationViewModel { Order = order };
        return View(vm);
    }

    [HttpPost("cancel")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(
        [FromForm] CancelOrderRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Please provide a valid cancellation reason.";
            return RedirectToAction(nameof(Details), new { orderNumber = request.OrderNumber });
        }

        var userId = GetCurrentUserId();

        try
        {
            await _orderService.CancelOrderAsync(request.OrderNumber, userId, request.Reason, cancellationToken);
            TempData["SuccessMessage"] = "Order cancelled successfully.";
        }
        catch (DomainException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { orderNumber = request.OrderNumber });
    }

    [HttpPost("return")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Return(
        [FromForm] ReturnOrderRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Please provide a valid return reason.";
            return RedirectToAction(nameof(Details), new { orderNumber = request.OrderNumber });
        }

        var userId = GetCurrentUserId();

        try
        {
            await _orderService.RequestReturnAsync(request.OrderNumber, userId, request.Reason, cancellationToken);
            TempData["SuccessMessage"] = "Return request submitted successfully. Our team will verify and initiate pickup.";
        }
        catch (DomainException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Details), new { orderNumber = request.OrderNumber });
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
