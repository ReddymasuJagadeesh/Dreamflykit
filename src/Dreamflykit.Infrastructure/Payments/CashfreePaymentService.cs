using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Dreamflykit.Application.Payments.Interfaces;
using Dreamflykit.Application.Payments.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dreamflykit.Infrastructure.Payments;

public sealed class CashfreePaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;
    private readonly CashfreePaymentOptions _options;
    private readonly ILogger<CashfreePaymentService> _logger;

    public CashfreePaymentService(
        HttpClient httpClient,
        IOptions<CashfreePaymentOptions> options,
        ILogger<CashfreePaymentService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<PaymentSessionResult> CreatePaymentSessionAsync(
        PaymentSessionRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Simulation Mode (Local development or when Cashfree credentials are not configured)
        if (string.Equals(_options.Mode, "Simulation", StringComparison.OrdinalIgnoreCase) ||
            string.IsNullOrWhiteSpace(_options.AppId) ||
            string.IsNullOrWhiteSpace(_options.SecretKey))
        {
            _logger.LogInformation("Using Payment Simulation for Order {OrderNumber}", request.OrderNumber);
            var simulatedSessionId = $"sim_sess_{Guid.NewGuid():N}";
            var simulatedOrderId = $"CF_SIM_{request.OrderNumber}_{DateTime.UtcNow.Ticks}";
            var paymentUrl = $"/Checkout/Payment?orderNumber={Uri.EscapeDataString(request.OrderNumber)}&sessionId={simulatedSessionId}";

            return PaymentSessionResult.Succeeded(simulatedSessionId, simulatedOrderId, paymentUrl);
        }

        // Live / Sandbox Cashfree API integration
        try
        {
            var baseUrl = string.Equals(_options.Mode, "Production", StringComparison.OrdinalIgnoreCase)
                ? _options.ProductionBaseUrl
                : _options.SandboxBaseUrl;

            var endpoint = $"{baseUrl.TrimEnd('/')}/orders";

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, endpoint);
            httpRequest.Headers.Add("x-client-id", _options.AppId);
            httpRequest.Headers.Add("x-client-secret", _options.SecretKey);
            httpRequest.Headers.Add("x-api-version", _options.ApiVersion);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var payload = new
            {
                order_id = $"DFK_{request.OrderNumber}",
                order_amount = request.Amount,
                order_currency = request.Currency,
                customer_details = new
                {
                    customer_id = request.CustomerId.ToString(),
                    customer_name = string.IsNullOrWhiteSpace(request.CustomerName) ? "Dreamflykit Customer" : request.CustomerName,
                    customer_email = request.CustomerEmail,
                    customer_phone = request.CustomerPhone.Length >= 10 ? request.CustomerPhone[^10..] : "9999999999"
                },
                order_meta = new
                {
                    return_url = request.ReturnUrl
                }
            };

            httpRequest.Content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Cashfree order creation failed with status {StatusCode}: {Response}", response.StatusCode, responseContent);
                return PaymentSessionResult.Failed($"Cashfree payment initiation error ({response.StatusCode}).");
            }

            using var doc = JsonDocument.Parse(responseContent);
            var root = doc.RootElement;
            var paymentSessionId = root.TryGetProperty("payment_session_id", out var sid) ? sid.GetString() : null;
            var paymentOrderId = root.TryGetProperty("order_id", out var oid) ? oid.GetString() : null;

            if (string.IsNullOrWhiteSpace(paymentSessionId))
            {
                return PaymentSessionResult.Failed("Cashfree did not return a valid session ID.");
            }

            var paymentUrl = $"/Checkout/Payment?orderNumber={Uri.EscapeDataString(request.OrderNumber)}&sessionId={paymentSessionId}";
            return PaymentSessionResult.Succeeded(paymentSessionId, paymentOrderId ?? request.OrderNumber, paymentUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error communicating with Cashfree API");
            return PaymentSessionResult.Failed("Unable to connect to payment provider. Please try again.");
        }
    }

    public async Task<PaymentVerificationResult> VerifyPaymentAsync(
        PaymentVerificationRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Simulation Mode
        if (string.Equals(_options.Mode, "Simulation", StringComparison.OrdinalIgnoreCase) ||
            string.IsNullOrWhiteSpace(_options.AppId))
        {
            var simulatedPaymentId = string.IsNullOrWhiteSpace(request.PaymentId)
                ? $"PAY_SIM_{Guid.NewGuid():N}"
                : request.PaymentId;

            return PaymentVerificationResult.Successful(request.OrderNumber, simulatedPaymentId, 0m);
        }

        // Live / Sandbox API Verification
        try
        {
            var baseUrl = string.Equals(_options.Mode, "Production", StringComparison.OrdinalIgnoreCase)
                ? _options.ProductionBaseUrl
                : _options.SandboxBaseUrl;

            var endpoint = $"{baseUrl.TrimEnd('/')}/orders/DFK_{request.OrderNumber}/payments";

            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, endpoint);
            httpRequest.Headers.Add("x-client-id", _options.AppId);
            httpRequest.Headers.Add("x-client-secret", _options.SecretKey);
            httpRequest.Headers.Add("x-api-version", _options.ApiVersion);

            var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return PaymentVerificationResult.Failed(request.OrderNumber, "Payment verification request failed.");
            }

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            using var doc = JsonDocument.Parse(responseContent);

            if (doc.RootElement.ValueKind == JsonValueKind.Array && doc.RootElement.GetArrayLength() > 0)
            {
                var firstPayment = doc.RootElement[0];
                var status = firstPayment.TryGetProperty("payment_status", out var ps) ? ps.GetString() : "UNKNOWN";
                var cfPaymentId = firstPayment.TryGetProperty("cf_payment_id", out var cpid) ? cpid.GetInt64().ToString() : request.PaymentId;
                var amount = firstPayment.TryGetProperty("payment_amount", out var pa) ? pa.GetDecimal() : 0m;

                if (string.Equals(status, "SUCCESS", StringComparison.OrdinalIgnoreCase))
                {
                    return PaymentVerificationResult.Successful(request.OrderNumber, cfPaymentId, amount, status ?? "SUCCESS");
                }

                return PaymentVerificationResult.Failed(request.OrderNumber, $"Payment status is {status}.");
            }

            return PaymentVerificationResult.Failed(request.OrderNumber, "No payments recorded for this order.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error verifying Cashfree payment for order {OrderNumber}", request.OrderNumber);
            return PaymentVerificationResult.Failed(request.OrderNumber, "Exception during payment verification.");
        }
    }

    public bool VerifyWebhookSignature(string rawPayload, string signature, string timestamp)
    {
        if (string.IsNullOrWhiteSpace(_options.SecretKey) || string.IsNullOrWhiteSpace(signature))
        {
            return true; // If in simulation/testing without keys
        }

        try
        {
            var dataToSign = timestamp + rawPayload;
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_options.SecretKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
            var computedSignature = Convert.ToBase64String(hash);

            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(computedSignature),
                Encoding.UTF8.GetBytes(signature));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking Cashfree webhook signature");
            return false;
        }
    }
}
