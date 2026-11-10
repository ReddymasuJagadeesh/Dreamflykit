using Dreamflykit.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dreamflykit.Web.Areas.Admin.Filters;

/// <summary>
/// Enforces Multi-Factor Authentication (TOTP) for all users with Admin or SuperAdmin roles.
/// Redirects administrators who have not yet enrolled in 2FA to the mandatory setup workflow.
/// </summary>
public sealed class AdminMfaFilter : IAsyncActionFilter
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminMfaFilter(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userPrincipal = context.HttpContext.User;

        if (userPrincipal?.Identity?.IsAuthenticated == true &&
            (userPrincipal.IsInRole("Admin") || userPrincipal.IsInRole("SuperAdmin")))
        {
            var path = context.HttpContext.Request.Path.Value ?? string.Empty;

            // Allow MFA enrollment, challenge, and logout paths without intercepting
            if (path.StartsWith("/Admin/Mfa", StringComparison.OrdinalIgnoreCase) ||
                path.StartsWith("/Account/Logout", StringComparison.OrdinalIgnoreCase))
            {
                await next();
                return;
            }

            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user != null && !user.TwoFactorEnabled)
            {
                context.Result = new RedirectToActionResult("Setup", "Mfa", new { area = "Admin" });
                return;
            }
        }

        await next();
    }
}

