using System.Security.Claims;
using System.Text.Json;
using Dreamflykit.Application.Addressing.Interfaces;
using Dreamflykit.Application.Cart.Interfaces;
using Dreamflykit.Application.Orders.Interfaces;
using Dreamflykit.Application.Orders.Models;
using Dreamflykit.Application.Payments.Interfaces;
using Dreamflykit.Application.Payments.Models;
using Dreamflykit.Domain.Common;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Web.Models.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

[Authorize]
[Route("[controller]")]
public sealed class CheckoutController : Controller
{
    private readonly ICartService _cartService;
    private readonly IAddressService _addressService;
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;
    private readonly UserManager<ApplicationUser> _userManager;

    public CheckoutController(
        ICartService cartService,
        IAddressService addressService,
        IOrderService orderService,
        IPaymentService paymentService,
        UserManager<ApplicationUser> userManager)
    {
        _cartService = cartService;
        _addressService = addressService;
        _orderService = orderService;
        _paymentService = paymentService;
        _userManager = userManager;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var cart = await _cartService.GetCartAsync(userId, cancellationToken);
        if (cart.IsEmpty)
        {
            return RedirectToAction("Index", "Cart");
        }

        var addresses = await _addressService.GetAddressesAsync(userId, cancellationToken);
        var defaultAddress = addresses.FirstOrDefault(a => a.IsDefault) ?? addresses.FirstOrDefault();

        var vm = new CheckoutViewModel
        {
            Addresses = addresses,
            SelectedShippingAddressId = defaultAddress?.AddressId ?? Guid.Empty,
            Cart = cart,
            PaymentMethod = "COD"
        };

        return View(vm);
    }

    [HttpPost("place-order")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PlaceOrder(
        [FromForm] CheckoutViewModel model,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var cart = await _cartService.GetCartAsync(userId, cancellationToken);
        if (cart.IsEmpty)
        {
            return RedirectToAction("Index", "Cart");
        }

        if (model.SelectedShippingAddressId == Guid.Empty)
        {
            ModelState.AddModelError(nameof(model.SelectedShippingAddressId), "Please select a delivery address.");
        }

        if (!ModelState.IsValid)
        {
            model.Addresses = await _addressService.GetAddressesAsync(userId, cancellationToken);
            model.Cart = cart;
            return View("Index", model);
        }

        var billingAddressId = model.SameAsShipping || !model.SelectedBillingAddressId.HasValue
            ? model.SelectedShippingAddressId
            : model.SelectedBillingAddressId.Value;

        var command = new PlaceOrderCommand
        {
            UserId = userId,
            ShippingAddressId = model.SelectedShippingAddressId,
            BillingAddressId = billingAddressId
        };

        try
        {
            var cartSubtotal = cart.Subtotal;
            var orderNumber = await _orderService.PlaceOrderAsync(command, cancellationToken);

            // Online Payment Flow (Cashfree / UPI / Cards)
            if (string.Equals(model.PaymentMethod, "ONLINE", StringComparison.OrdinalIgnoreCase))
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                var returnUrl = Url.Action("Payment", "Checkout", new { orderNumber }, Request.Scheme) ?? "";

                var sessionResult = await _paymentService.CreatePaymentSessionAsync(new PaymentSessionRequest
                {
                    OrderId = Guid.NewGuid(),
                    OrderNumber = orderNumber,
                    Amount = cartSubtotal,
                    Currency = "INR",
                    CustomerId = userId,
                    CustomerName = user != null ? $"{user.FirstName} {user.LastName}".Trim() : "Customer",
                    CustomerEmail = user?.Email ?? "",
                    CustomerPhone = user?.PhoneNumber ?? "",
                    ReturnUrl = returnUrl
                }, cancellationToken);

                if (sessionResult.Success && !string.IsNullOrWhiteSpace(sessionResult.PaymentOrderId))
                {
                    await _orderService.SetPaymentOrderIdAsync(orderNumber, sessionResult.PaymentOrderId, cancellationToken);
                }

                return RedirectToAction("Payment", new { orderNumber, sessionId = sessionResult.PaymentSessionId });
            }

            // Cash on Delivery Flow
            return RedirectToAction("Confirmation", "Orders", new { orderNumber });
        }
        catch (DomainException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            model.Addresses = await _addressService.GetAddressesAsync(userId, cancellationToken);
            model.Cart = cart;
            return View("Index", model);
        }
    }

    [HttpGet("Payment")]
    public async Task<IActionResult> Payment(
        [FromQuery] string orderNumber,
        [FromQuery] string? sessionId,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var order = await _orderService.GetOrderAsync(orderNumber, userId, cancellationToken);
        if (order is null)
        {
            return NotFound();
        }

        var vm = new PaymentGatewayViewModel
        {
            OrderNumber = order.OrderNumber,
            GrandTotal = order.GrandTotal,
            Subtotal = order.Subtotal,
            PaymentSessionId = sessionId ?? "",
            PaymentOrderId = order.OrderNumber,
            CustomerEmail = User.FindFirstValue(ClaimTypes.Email) ?? "",
            ItemCount = order.Items.Sum(i => i.Quantity)
        };

        return View(vm);
    }

    [HttpPost("VerifyPayment")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> VerifyPayment(
        [FromForm] string orderNumber,
        [FromForm] string? paymentMethod,
        [FromForm] string? transactionRef,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var order = await _orderService.GetOrderAsync(orderNumber, userId, cancellationToken);
        if (order is null)
        {
            return NotFound();
        }

        var verification = await _paymentService.VerifyPaymentAsync(new PaymentVerificationRequest
        {
            OrderNumber = orderNumber,
            PaymentId = string.IsNullOrWhiteSpace(transactionRef) ? $"DFK_PAY_{Guid.NewGuid():N}" : transactionRef,
            PaymentOrderId = orderNumber
        }, cancellationToken);

        if (verification.IsPaid)
        {
            await _orderService.MarkPaymentCompletedAsync(orderNumber, verification.PaymentId, cancellationToken);
            return RedirectToAction("Confirmation", "Orders", new { orderNumber });
        }

        TempData["PaymentError"] = verification.ErrorMessage ?? "Payment authorization failed. Please try again.";
        return RedirectToAction("Payment", new { orderNumber });
    }

    [HttpPost("webhook/cashfree")]
    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> CashfreeWebhook(CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(Request.Body);
        var rawPayload = await reader.ReadToEndAsync(cancellationToken);

        var signature = Request.Headers["x-webhook-signature"].ToString();
        var timestamp = Request.Headers["x-webhook-timestamp"].ToString();

        if (!_paymentService.VerifyWebhookSignature(rawPayload, signature, timestamp))
        {
            return Unauthorized();
        }

        try
        {
            using var doc = JsonDocument.Parse(rawPayload);
            if (doc.RootElement.TryGetProperty("data", out var data))
            {
                var orderId = data.GetProperty("order").GetProperty("order_id").GetString();
                var paymentStatus = data.GetProperty("payment").GetProperty("payment_status").GetString();
                var paymentId = data.GetProperty("payment").GetProperty("cf_payment_id").GetInt64().ToString();

                if (string.Equals(paymentStatus, "SUCCESS", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(orderId))
                {
                    var cleanOrderNumber = orderId.StartsWith("DFK_") ? orderId[4..] : orderId;
                    await _orderService.MarkPaymentCompletedAsync(cleanOrderNumber, paymentId, cancellationToken);
                }
            }

            return Ok();
        }
        catch
        {
            return BadRequest();
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
