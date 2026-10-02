namespace Dreamflykit.Application.Payments.Models;

public sealed class PaymentSessionRequest
{
    public Guid OrderId { get; init; }
    public string OrderNumber { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string Currency { get; init; } = "INR";
    public Guid CustomerId { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public string CustomerEmail { get; init; } = string.Empty;
    public string CustomerPhone { get; init; } = string.Empty;
    public string ReturnUrl { get; init; } = string.Empty;
}

public sealed class PaymentSessionResult
{
    public bool Success { get; init; }
    public string? PaymentSessionId { get; init; }
    public string? PaymentOrderId { get; init; }
    public string? PaymentUrl { get; init; }
    public string? ErrorMessage { get; init; }

    public static PaymentSessionResult Failed(string error) =>
        new() { Success = false, ErrorMessage = error };

    public static PaymentSessionResult Succeeded(string sessionId, string paymentOrderId, string paymentUrl) =>
        new() { Success = true, PaymentSessionId = sessionId, PaymentOrderId = paymentOrderId, PaymentUrl = paymentUrl };
}

public sealed class PaymentVerificationRequest
{
    public string OrderNumber { get; init; } = string.Empty;
    public string PaymentOrderId { get; init; } = string.Empty;
    public string PaymentId { get; init; } = string.Empty;
    public string? Signature { get; init; }
}

public sealed class PaymentVerificationResult
{
    public bool IsPaid { get; init; }
    public string OrderNumber { get; init; } = string.Empty;
    public string PaymentId { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string Status { get; init; } = string.Empty;
    public string? ErrorMessage { get; init; }

    public static PaymentVerificationResult Successful(string orderNumber, string paymentId, decimal amount, string status = "SUCCESS") =>
        new() { IsPaid = true, OrderNumber = orderNumber, PaymentId = paymentId, Amount = amount, Status = status };

    public static PaymentVerificationResult Failed(string orderNumber, string error) =>
        new() { IsPaid = false, OrderNumber = orderNumber, ErrorMessage = error, Status = "FAILED" };
}
