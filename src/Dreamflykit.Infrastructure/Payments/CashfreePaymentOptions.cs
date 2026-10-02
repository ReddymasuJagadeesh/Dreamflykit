namespace Dreamflykit.Infrastructure.Payments;

public sealed class CashfreePaymentOptions
{
    public const string SectionName = "Cashfree";

    public string AppId { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string ApiVersion { get; set; } = "2023-08-01";
    /// <summary>
    /// Supported modes: "Sandbox", "Production", or "Simulation" (for offline/local testing).
    /// </summary>
    public string Mode { get; set; } = "Simulation";
    public string SandboxBaseUrl { get; set; } = "https://sandbox.cashfree.com/pg";
    public string ProductionBaseUrl { get; set; } = "https://api.cashfree.com/pg";
}
