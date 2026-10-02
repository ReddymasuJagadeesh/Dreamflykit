using Dreamflykit.Application.Payments.Models;

namespace Dreamflykit.Application.Payments.Interfaces;

public interface IPaymentService
{
    Task<PaymentSessionResult> CreatePaymentSessionAsync(
        PaymentSessionRequest request,
        CancellationToken cancellationToken);

    Task<PaymentVerificationResult> VerifyPaymentAsync(
        PaymentVerificationRequest request,
        CancellationToken cancellationToken);

    bool VerifyWebhookSignature(
        string rawPayload,
        string signature,
        string timestamp);
}
