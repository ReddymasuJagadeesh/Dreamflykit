using System.Net;
using System.Net.Mail;
using Dreamflykit.Application.Email.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dreamflykit.Infrastructure.Email;

public sealed class SmtpEmailSender : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly ILogger<SmtpEmailSender> _logger;

    public SmtpEmailSender(
        IOptions<EmailSettings> settings,
        ILogger<SmtpEmailSender> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(
        string toEmail,
        string subject,
        string htmlContent,
        string? plainTextContent = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
        {
            throw new ArgumentException("Recipient email cannot be empty.", nameof(toEmail));
        }

        // If SMTP server is not configured, log email contents safely for local development and testing
        if (string.IsNullOrWhiteSpace(_settings.SmtpHost))
        {
            _logger.LogInformation(
                "[DEVELOPMENT/MOCK EMAIL] To: {ToEmail} | Subject: {Subject}\nBody: {HtmlContent}",
                toEmail,
                subject,
                htmlContent);
            return;
        }

        try
        {
            using var message = new MailMessage
            {
                From = new MailAddress(_settings.FromEmail, _settings.FromName),
                Subject = subject,
                Body = htmlContent,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(toEmail));

            if (!string.IsNullOrWhiteSpace(plainTextContent))
            {
                var plainView = AlternateView.CreateAlternateViewFromString(plainTextContent, null, "text/plain");
                var htmlView = AlternateView.CreateAlternateViewFromString(htmlContent, null, "text/html");
                message.AlternateViews.Add(plainView);
                message.AlternateViews.Add(htmlView);
            }

            using var client = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
            {
                EnableSsl = _settings.EnableSsl,
                Timeout = 10000
            };

            if (!string.IsNullOrWhiteSpace(_settings.Username) && !string.IsNullOrWhiteSpace(_settings.Password))
            {
                client.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
            }

            await client.SendMailAsync(message, cancellationToken);
            _logger.LogInformation("Email successfully dispatched to {ToEmail} with subject '{Subject}'.", toEmail, subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {ToEmail} with subject '{Subject}'.", toEmail, subject);
            // In development, do not crash flow if SMTP network is down
            if (string.IsNullOrWhiteSpace(_settings.Username))
            {
                _logger.LogWarning("Continuing flow despite email error as SMTP username is unconfigured.");
                return;
            }
            throw;
        }
    }

    public async Task SendPasswordResetEmailAsync(
        string toEmail,
        string resetLink,
        CancellationToken cancellationToken = default)
    {
        var subject = "Reset your Dreamflykit password";
        var htmlContent = $@"
<div style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 24px; border: 1px solid #e2e8f0; border-radius: 8px; background-color: #ffffff;"">
    <div style=""text-align: center; margin-bottom: 24px;"">
        <h2 style=""color: #0f172a; margin: 0; font-size: 24px; font-weight: 700;"">Dreamflykit</h2>
        <p style=""color: #64748b; font-size: 14px; margin-top: 4px;"">Your Journey. Our Kit.</p>
    </div>
    <div style=""padding: 24px 0; border-top: 1px solid #f1f5f9; border-bottom: 1px solid #f1f5f9;"">
        <h3 style=""color: #1e293b; font-size: 18px; margin-top: 0;"">Password Reset Request</h3>
        <p style=""color: #475569; font-size: 14px; line-height: 1.6;"">
            We received a request to reset the password for your Dreamflykit account. Click the button below to choose a new password:
        </p>
        <div style=""text-align: center; margin: 32px 0;"">
            <a href=""{resetLink}"" style=""background-color: #2563eb; color: #ffffff; padding: 12px 28px; border-radius: 6px; font-weight: 600; text-decoration: none; display: inline-block;"">Reset My Password</a>
        </div>
        <p style=""color: #64748b; font-size: 13px; line-height: 1.5;"">
            If the button doesn't work, copy and paste this link into your browser:<br/>
            <a href=""{resetLink}"" style=""color: #2563eb; word-break: break-all;"">{resetLink}</a>
        </p>
        <p style=""color: #94a3b8; font-size: 12px; margin-top: 24px; line-height: 1.4;"">
            If you did not make this request, you can safely ignore this email. Your password will remain unchanged.
        </p>
    </div>
    <div style=""text-align: center; margin-top: 24px; color: #94a3b8; font-size: 12px;"">
        &copy; {DateTime.UtcNow.Year} Dreamflykit. All rights reserved.
    </div>
</div>";

        var plainTextContent = $@"Dreamflykit - Password Reset Request

We received a request to reset your password. Use the link below to set a new password:

{resetLink}

If you did not request this, you can safely ignore this message.";

        await SendEmailAsync(toEmail, subject, htmlContent, plainTextContent, cancellationToken);
    }

    public async Task SendOrderConfirmationEmailAsync(
        string toEmail,
        string orderNumber,
        decimal totalAmount,
        CancellationToken cancellationToken = default)
    {
        var subject = $"Order Confirmation - #{orderNumber} | Dreamflykit";
        var htmlContent = $@"
<div style=""font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 24px; border: 1px solid #e2e8f0; border-radius: 8px; background-color: #ffffff;"">
    <div style=""text-align: center; margin-bottom: 24px;"">
        <h2 style=""color: #0f172a; margin: 0; font-size: 24px; font-weight: 700;"">Dreamflykit</h2>
        <p style=""color: #64748b; font-size: 14px; margin-top: 4px;"">Your Journey. Our Kit.</p>
    </div>
    <div style=""padding: 24px 0; border-top: 1px solid #f1f5f9; border-bottom: 1px solid #f1f5f9;"">
        <h3 style=""color: #1e293b; font-size: 18px; margin-top: 0;"">Thank you for your order!</h3>
        <p style=""color: #475569; font-size: 14px; line-height: 1.6;"">
            Your order <strong>#{orderNumber}</strong> has been confirmed. Total Amount: <strong>&#8377;{totalAmount:N2}</strong>.
        </p>
        <p style=""color: #475569; font-size: 14px; line-height: 1.6;"">
            We are preparing your items and will notify you with tracking information as soon as your package ships.
        </p>
    </div>
    <div style=""text-align: center; margin-top: 24px; color: #94a3b8; font-size: 12px;"">
        &copy; {DateTime.UtcNow.Year} Dreamflykit. All rights reserved.
    </div>
</div>";

        await SendEmailAsync(toEmail, subject, htmlContent, null, cancellationToken);
    }
}

