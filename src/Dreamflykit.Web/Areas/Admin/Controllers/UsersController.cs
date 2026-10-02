using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
[Route("Admin/[controller]")]
public sealed class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public UsersController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(
        [FromQuery] string? search,
        CancellationToken cancellationToken)
    {
        var query = _userManager.Users.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLowerInvariant();
            query = query.Where(u =>
                (u.Email != null && u.Email.ToLower().Contains(term)) ||
                u.FirstName.ToLower().Contains(term) ||
                u.LastName.ToLower().Contains(term) ||
                (u.PhoneNumber != null && u.PhoneNumber.Contains(term)));
        }

        var usersRaw = await query
            .OrderBy(u => u.FirstName)
            .Take(100)
            .ToListAsync(cancellationToken);

        var list = new List<AdminUserListItemViewModel>();

        foreach (var u in usersRaw)
        {
            var roles = await _userManager.GetRolesAsync(u);
            var isLocked = await _userManager.IsLockedOutAsync(u);
            var lockoutEnd = u.LockoutEnd?.UtcDateTime;

            list.Add(new AdminUserListItemViewModel
            {
                UserId = u.Id,
                Email = u.Email ?? "N/A",
                FullName = $"{u.FirstName} {u.LastName}".Trim(),
                PhoneNumber = u.PhoneNumber ?? "N/A",
                Roles = roles.ToList(),
                IsLockedOut = isLocked,
                IsActive = u.IsActive,
                LockoutEnd = lockoutEnd
            });
        }

        return View(list);
    }

    [HttpPost("AssignRole")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignRole(
        [FromForm] Guid userId,
        [FromForm] string roleName,
        [FromForm] bool assign)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return NotFound();
        }

        if (user.Email == "superadmin@dreamflykit.com" && roleName == "SuperAdmin" && !assign)
        {
            TempData["ErrorMessage"] = "Cannot revoke SuperAdmin role from root super administrator account.";
            return RedirectToAction("Index");
        }

        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            TempData["ErrorMessage"] = $"Role '{roleName}' does not exist.";
            return RedirectToAction("Index");
        }

        if (assign)
        {
            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
                TempData["SuccessMessage"] = $"Role '{roleName}' assigned to {user.Email}.";
            }
        }
        else
        {
            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
                TempData["SuccessMessage"] = $"Role '{roleName}' removed from {user.Email}.";
            }
        }

        return RedirectToAction("Index");
    }

    [HttpPost("ToggleLock")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleLock([FromForm] Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return NotFound();
        }

        if (user.Email == "superadmin@dreamflykit.com")
        {
            TempData["ErrorMessage"] = "Root super administrator account cannot be locked.";
            return RedirectToAction("Index");
        }

        var isLocked = await _userManager.IsLockedOutAsync(user);
        if (isLocked)
        {
            await _userManager.SetLockoutEndDateAsync(user, null);
            TempData["SuccessMessage"] = $"Account {user.Email} unlocked.";
        }
        else
        {
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddYears(100));
            TempData["SuccessMessage"] = $"Account {user.Email} locked.";
        }

        return RedirectToAction("Index");
    }
}
