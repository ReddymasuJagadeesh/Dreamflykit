using Dreamflykit.Application.Addressing.Models;
using Dreamflykit.Application.Cart.Models;
using Dreamflykit.Application.Orders.Models;
using System.ComponentModel.DataAnnotations;

namespace Dreamflykit.Web.Models.Orders;

public sealed class CheckoutViewModel
{
    public IReadOnlyList<AddressSummary> Addresses { get; set; } = Array.Empty<AddressSummary>();

    [Required(ErrorMessage = "Please select a delivery address.")]
    public Guid SelectedShippingAddressId { get; set; }

    public Guid? SelectedBillingAddressId { get; set; }

    public bool SameAsShipping { get; set; } = true;

    [Required(ErrorMessage = "Please select a payment method.")]
    public string PaymentMethod { get; set; } = "COD"; // COD or ONLINE

    public CartSummary Cart { get; set; } = new();
}

public sealed class OrderConfirmationViewModel
{
    public OrderDetails Order { get; set; } = null!;
}

public sealed class CancelOrderRequest
{
    [Required]
    public string OrderNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please specify a reason for cancellation.")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "Reason must be between 5 and 500 characters.")]
    public string Reason { get; set; } = string.Empty;
}

public sealed class ReturnOrderRequest
{
    [Required]
    public string OrderNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please specify a reason for returning the kit.")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "Reason must be between 5 and 500 characters.")]
    public string Reason { get; set; } = string.Empty;
}
