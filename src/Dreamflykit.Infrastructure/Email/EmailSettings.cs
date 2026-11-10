namespace Dreamflykit.Infrastructure.Email;

public sealed class EmailSettings
{
    public const string SectionName = "EmailSettings";

    public string? SmtpHost { get; set; }
    public int SmtpPort { get; set; } = 587;
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool EnableSsl { get; set; } = true;
    public string FromEmail { get; set; } = "noreply@dreamflykit.com";
    public string FromName { get; set; } = "Dreamflykit";
}

