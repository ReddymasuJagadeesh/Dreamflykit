namespace Dreamflykit.Web.Models.Orders;

public sealed class PaymentGatewayViewModel
{
    public string OrderNumber { get; init; } = string.Empty;
    public decimal GrandTotal { get; init; }
    public decimal Subtotal { get; init; }
    public string PaymentSessionId { get; init; } = string.Empty;
    public string PaymentOrderId { get; init; } = string.Empty;
    public string CustomerEmail { get; init; } = string.Empty;
    public int ItemCount { get; init; }
}
