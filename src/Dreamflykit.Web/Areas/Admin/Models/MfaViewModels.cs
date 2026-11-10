using System.ComponentModel.DataAnnotations;

namespace Dreamflykit.Web.Areas.Admin.Models;

public sealed class MfaSetupViewModel
{
    public string SharedKey { get; set; } = string.Empty;

    public string AuthenticatorUri { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter the 6-digit verification code from your authenticator app.")]
    [StringLength(7, MinimumLength = 6, ErrorMessage = "The verification code must be 6 digits.")]
    [DataType(DataType.Text)]
    [Display(Name = "Verification Code")]
    public string VerificationCode { get; set; } = string.Empty;
}

public sealed class MfaRecoveryCodesViewModel
{
    public IEnumerable<string> RecoveryCodes { get; set; } = Array.Empty<string>();
}

public sealed class MfaStatusViewModel
{
    public bool Is2faEnabled { get; set; }

    public int RecoveryCodesLeft { get; set; }
}

