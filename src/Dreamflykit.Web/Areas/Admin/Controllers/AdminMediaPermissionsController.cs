using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
[Route("Admin/MediaPermissions")]
public sealed class AdminMediaPermissionsController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediaPermissionService _permissionService;
    private readonly DreamflykitDbContext _db;

    public AdminMediaPermissionsController(
        UserManager<ApplicationUser> userManager,
        IMediaPermissionService permissionService,
        DreamflykitDbContext db)
    {
        _userManager = userManager;
        _permissionService = permissionService;
        _db = db;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var users = await _userManager.Users
            .AsNoTracking()
            .OrderBy(u => u.FirstName)
            .ToListAsync(cancellationToken);

        var permissions = await _db.MediaPermissions
            .AsNoTracking()
            .Where(p => p.IsGranted)
            .ToListAsync(cancellationToken);

        var userSummaries = new List<UserPermissionSummaryDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var isSuperAdmin = roles.Contains("SuperAdmin");
            var roleName = isSuperAdmin ? "SuperAdmin" : (roles.FirstOrDefault() ?? "Staff");

            var userPerms = permissions
                .Where(p => p.UserId == user.Id)
                .ToDictionary(
                    p => $"{p.MediaType}_{p.PermissionType}",
                    p => p.IsGranted);

            userSummaries.Add(new UserPermissionSummaryDto
            {
                UserId = user.Id,
                Email = user.Email ?? "Unknown",
                Role = roleName,
                GrantedPermissions = userPerms
            });
        }

        var model = new MediaPermissionMatrixViewModel
        {
            Users = userSummaries,
            MediaTypes = Enum.GetValues<MediaType>(),
            PermissionTypes = Enum.GetValues<MediaPermissionType>()
        };

        return View("~/Areas/Admin/Views/MediaPermissions/Index.cshtml", model);
    }

    [HttpPost("Toggle")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(
        [FromForm] UpdateUserPermissionRequest request,
        CancellationToken cancellationToken)
    {
        var currentUserIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        _ = Guid.TryParse(currentUserIdClaim, out var grantedBy);

        await _permissionService.GrantOrRevokeAsync(
            request.UserId,
            request.MediaType,
            request.PermissionType,
            request.IsGranted,
            grantedBy,
            cancellationToken);

        return Json(new { success = true });
    }
}
