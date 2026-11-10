namespace Dreamflykit.Application.Email.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(
        string toEmail,
        string subject,
        string htmlContent,
        string? plainTextContent = null,
        CancellationToken cancellationToken = default);

    Task SendPasswordResetEmailAsync(
        string toEmail,
        string resetLink,
        CancellationToken cancellationToken = default);

    Task SendOrderConfirmationEmailAsync(
        string toEmail,
        string orderNumber,
        decimal totalAmount,
        CancellationToken cancellationToken = default);
}

