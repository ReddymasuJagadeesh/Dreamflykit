using System.Text;
using System.Text.Encodings.Web;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Route("Admin/[controller]")]
public sealed class MfaController : AdminBaseController
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UrlEncoder _urlEncoder;
    private readonly ILogger<MfaController> _logger;

    private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

    public MfaController(
        UserManager<ApplicationUser> userManager,
        UrlEncoder urlEncoder,
        ILogger<MfaController> logger)
    {
        _userManager = userManager;
        _urlEncoder = urlEncoder;
        _logger = logger;
    }

    [HttpGet("Setup")]
    public async Task<IActionResult> Setup(CancellationToken ct)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var model = await BuildMfaSetupViewModelAsync(user);
        return View(model);
    }

    [HttpPost("Setup")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Setup(MfaSetupViewModel model, CancellationToken ct)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        if (!ModelState.IsValid)
        {
            var reloadModel = await BuildMfaSetupViewModelAsync(user);
            reloadModel.VerificationCode = model.VerificationCode;
            return View(reloadModel);
        }

        // Clean user input
        var verificationCode = model.VerificationCode
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty)
            .Trim();

        var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
            user,
            _userManager.Options.Tokens.AuthenticatorTokenProvider,
            verificationCode);

        if (!is2faTokenValid)
        {
            ModelState.AddModelError(
                nameof(model.VerificationCode),
                "Verification code is invalid or has expired. Please check your authenticator clock and try again.");

            var reloadModel = await BuildMfaSetupViewModelAsync(user);
            return View(reloadModel);
        }

        await _userManager.SetTwoFactorEnabledAsync(user, true);
        _logger.LogInformation("Administrator '{UserId}' successfully enrolled and enabled TOTP MFA.", user.Id);

        var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
        return View("RecoveryCodes", new MfaRecoveryCodesViewModel
        {
            RecoveryCodes = recoveryCodes?.ToArray() ?? Array.Empty<string>()
        });
    }

    [HttpGet("Status")]
    public async Task<IActionResult> Status(CancellationToken ct)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var count = await _userManager.CountRecoveryCodesAsync(user);
        return View(new MfaStatusViewModel
        {
            Is2faEnabled = user.TwoFactorEnabled,
            RecoveryCodesLeft = count
        });
    }

    private async Task<MfaSetupViewModel> BuildMfaSetupViewModelAsync(ApplicationUser user)
    {
        var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
        if (string.IsNullOrEmpty(unformattedKey))
        {
            await _userManager.ResetAuthenticatorKeyAsync(user);
            unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
        }

        var email = await _userManager.GetEmailAsync(user) ?? user.UserName ?? "admin";
        var sharedKey = FormatKey(unformattedKey!);
        var authenticatorUri = GenerateQrCodeUri(email, unformattedKey!);

        return new MfaSetupViewModel
        {
            SharedKey = sharedKey,
            AuthenticatorUri = authenticatorUri
        };
    }

    private static string FormatKey(string unformattedKey)
    {
        var result = new StringBuilder();
        var currentPosition = 0;
        while (currentPosition + 4 < unformattedKey.Length)
        {
            result.Append(unformattedKey.AsSpan(currentPosition, 4)).Append(' ');
            currentPosition += 4;
        }
        if (currentPosition < unformattedKey.Length)
        {
            result.Append(unformattedKey.AsSpan(currentPosition));
        }
        return result.ToString().ToLowerInvariant();
    }

    private string GenerateQrCodeUri(string email, string unformattedKey)
    {
        return string.Format(
            AuthenticatorUriFormat,
            _urlEncoder.Encode("Dreamflykit"),
            _urlEncoder.Encode(email),
            unformattedKey);
    }
}

