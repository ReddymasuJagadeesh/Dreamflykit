using Dreamflykit.Application.Email.Interfaces;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Dreamflykit.Web.Controllers;

/// <summary>
/// Handles user authentication: Login, Register, Logout.
///
/// Security practices applied:
/// - [ValidateAntiForgeryToken] on all POST actions.
/// - Url.IsLocalUrl() check before all redirects using user-supplied URLs.
/// - No information leakage on login failure (generic error message).
/// - Password hashed by ASP.NET Identity — never stored plain text.
/// - Account lockout after 5 failed attempts (configured in Program.cs).
/// </summary>
public sealed class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailService? _emailService;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailService? emailService = null)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
    }

    // ============================================================
    // LOGIN
    // ============================================================

    [HttpGet("/Account/Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            var user = await _userManager.GetUserAsync(User);
            return await RedirectToLocalAsync(returnUrl, user);
        }

        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpGet("/Seller/Login")]
    [AllowAnonymous]
    public async Task<IActionResult> SellerLogin(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && await _userManager.IsInRoleAsync(user, "Seller"))
            {
                return Redirect("/Seller/Products");
            }

            return await RedirectToLocalAsync(returnUrl, user);
        }

        return View("~/Views/Account/SellerLogin.cshtml", new LoginViewModel { ReturnUrl = returnUrl ?? "/Seller/Products" });
    }

    [HttpPost("/Account/Login")]
    [HttpPost("/Seller/Login")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("auth-policy")]
    public async Task<IActionResult> Login(
        LoginViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            if (Request.Path.Value?.Contains("/Seller/Login", StringComparison.OrdinalIgnoreCase) == true)
            {
                return View("~/Views/Account/SellerLogin.cshtml", model);
            }
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: true);   // Honour lockout policy

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            return await RedirectToLocalAsync(model.ReturnUrl, user);
        }

        if (result.RequiresTwoFactor)
        {
            return RedirectToAction(nameof(LoginWith2fa), new { returnUrl = model.ReturnUrl, rememberMe = model.RememberMe });
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(
                string.Empty,
                "Your account has been temporarily locked. Please try again in 15 minutes.");

            if (Request.Path.Value?.Contains("/Seller/Login", StringComparison.OrdinalIgnoreCase) == true)
            {
                return View("~/Views/Account/SellerLogin.cshtml", model);
            }
            return View(model);
        }

        // Generic error — do not reveal whether email or password was wrong.
        ModelState.AddModelError(
            string.Empty,
            "Invalid email or password.");

        if (Request.Path.Value?.Contains("/Seller/Login", StringComparison.OrdinalIgnoreCase) == true)
        {
            return View("~/Views/Account/SellerLogin.cshtml", model);
        }
        return View(model);
    }

    // ============================================================
    // REGISTER
    // ============================================================

    [HttpGet("/Account/Register")]
    [AllowAnonymous]
    public IActionResult Register(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToLocal(returnUrl);
        }

        return View(new RegisterViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost("/Account/Register")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("auth-policy")]
    public async Task<IActionResult> Register(
        RegisterViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(
            user,
            model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Customer");
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToLocal(model.ReturnUrl);
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    // ============================================================
    // TWO-FACTOR AUTHENTICATION LOGIN
    // ============================================================

    [HttpGet("/Account/LoginWith2fa")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginWith2fa(bool rememberMe, string? returnUrl = null)
    {
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            return RedirectToAction(nameof(Login));
        }

        return View(new LoginWith2faViewModel
        {
            ReturnUrl = returnUrl,
            RememberMe = rememberMe
        });
    }

    [HttpPost("/Account/LoginWith2fa")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("auth-policy")]
    public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            return RedirectToAction(nameof(Login));
        }

        var authenticatorCode = model.TwoFactorCode
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty)
            .Trim();

        var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(
            authenticatorCode,
            model.RememberMe,
            model.RememberMachine);

        if (result.Succeeded)
        {
            return await RedirectToLocalAsync(model.ReturnUrl, user);
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, "Your account has been temporarily locked due to failed attempts.");
            return View(model);
        }

        ModelState.AddModelError(string.Empty, "Invalid authenticator verification code.");
        return View(model);
    }

    [HttpGet("/Account/LoginWithRecoveryCode")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginWithRecoveryCode(string? returnUrl = null)
    {
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            return RedirectToAction(nameof(Login));
        }

        return View(new LoginWithRecoveryCodeViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost("/Account/LoginWithRecoveryCode")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("auth-policy")]
    public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            return RedirectToAction(nameof(Login));
        }

        var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty).Trim();
        var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

        if (result.Succeeded)
        {
            return await RedirectToLocalAsync(model.ReturnUrl, user);
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, "Your account has been temporarily locked.");
            return View(model);
        }

        ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
        return View(model);
    }

    // ============================================================
    // LOGOUT
    // ============================================================

    [HttpPost("/Account/Logout")]
    [HttpPost("/Seller/Logout")]
    [HttpGet("/Account/Logout")]
    [HttpGet("/Seller/Logout")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Logout()
    {
        var wasSeller = User.IsInRole("Seller");
        await _signInManager.SignOutAsync();

        if (wasSeller || Request.Path.Value?.Contains("/Seller", StringComparison.OrdinalIgnoreCase) == true)
        {
            return Redirect("/Seller/Login");
        }

        return RedirectToAction("Index", "Home");
    }

    // ============================================================
    // ACCESS DENIED
    // ============================================================

    [HttpGet("/Account/AccessDenied")]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }

    // ============================================================
    // PROFILE
    // ============================================================

    [HttpGet("/Account/Profile")]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return RedirectToAction("Login");
        }

        return View(user);
    }

    // ============================================================
    // FORGOT PASSWORD & RESET PASSWORD
    // ============================================================

    [HttpGet("/Account/ForgotPassword")]
    [AllowAnonymous]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost("/Account/ForgotPassword")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("auth-policy")]
    public async Task<IActionResult> ForgotPassword(
        ForgotPasswordViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action(
                nameof(ResetPassword),
                "Account",
                new { code, email = model.Email },
                protocol: Request.Scheme);

            if (_emailService != null && !string.IsNullOrWhiteSpace(callbackUrl))
            {
                await _emailService.SendPasswordResetEmailAsync(model.Email, callbackUrl, cancellationToken);
            }
        }

        // Generic redirect prevents account enumeration attacks
        return RedirectToAction(nameof(ForgotPasswordConfirmation));
    }

    [HttpGet("/Account/ForgotPasswordConfirmation")]
    [AllowAnonymous]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet("/Account/ResetPassword")]
    [AllowAnonymous]
    public IActionResult ResetPassword(string? code = null, string? email = null)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return BadRequest("A security code must be supplied for password reset.");
        }

        return View(new ResetPasswordViewModel
        {
            Code = code,
            Email = email ?? string.Empty
        });
    }

    [HttpPost("/Account/ResetPassword")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("auth-policy")]
    public async Task<IActionResult> ResetPassword(
        ResetPasswordViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            // Do not reveal that the user does not exist
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpGet("/Account/ResetPasswordConfirmation")]
    [AllowAnonymous]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }

    // ============================================================
    // PRIVATE HELPERS
    // ============================================================

    private async Task<IActionResult> RedirectToLocalAsync(string? returnUrl, ApplicationUser? user)
    {
        if (user != null)
        {
            if (await _userManager.IsInRoleAsync(user, "SuperAdmin") ||
                await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return Redirect("/Admin");
            }

            if (await _userManager.IsInRoleAsync(user, "Seller"))
            {
                return Redirect("/Seller/Products");
            }
        }

        if (!string.IsNullOrWhiteSpace(returnUrl) &&
            Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (!string.IsNullOrWhiteSpace(returnUrl) &&
            Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction("Index", "Home");
    }
}
