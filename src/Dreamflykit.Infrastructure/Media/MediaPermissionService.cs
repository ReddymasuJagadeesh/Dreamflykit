using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Media;

/// <summary>
/// Server-side media permission enforcement.
/// Depends on ASP.NET Core Identity role membership to detect SuperAdmin.
/// </summary>
public sealed class MediaPermissionService : IMediaPermissionService
{
    private readonly DreamflykitDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public MediaPermissionService(
        DreamflykitDbContext db,
        UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<bool> HasPermissionAsync(
        Guid userId,
        MediaType mediaType,
        MediaPermissionType permissionType,
        CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
        {
            return false;
        }

        // SuperAdmin always has all permissions
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return false;
        }

        if (await _userManager.IsInRoleAsync(user, "SuperAdmin"))
        {
            return true;
        }

        // Check explicit permission record for Admin users
        return await _db.MediaPermissions
            .AnyAsync(p =>
                p.UserId == userId &&
                p.MediaType == mediaType &&
                p.PermissionType == permissionType &&
                p.IsGranted,
                cancellationToken);
    }

    /// <inheritdoc />
    public async Task GrantOrRevokeAsync(
        Guid targetUserId,
        MediaType mediaType,
        MediaPermissionType permissionType,
        bool isGranted,
        Guid grantedBy,
        CancellationToken cancellationToken = default)
    {
        var existing = await _db.MediaPermissions
            .FirstOrDefaultAsync(p =>
                p.UserId == targetUserId &&
                p.MediaType == mediaType &&
                p.PermissionType == permissionType,
                cancellationToken);

        if (existing is not null)
        {
            existing.Update(isGranted, grantedBy);
        }
        else
        {
            var permission = new MediaPermission(
                targetUserId,
                mediaType,
                permissionType,
                isGranted,
                grantedBy);

            _db.MediaPermissions.Add(permission);
        }

        // Audit log
        var auditLog = new MediaAuditLog(
            userId: grantedBy,
            action: isGranted ? "PermissionGranted" : "PermissionRevoked",
            mediaAssetId: null,
            resourceType: "MediaPermission",
            resourceId: targetUserId,
            oldValue: null,
            newValue: $"{mediaType}:{permissionType}={isGranted}",
            notes: null);

        _db.MediaAuditLogs.Add(auditLog);

        await _db.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<MediaPermissionEntry>> GetUserPermissionsAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _db.MediaPermissions
            .Where(p => p.UserId == userId)
            .Select(p => new MediaPermissionEntry
            {
                MediaType = p.MediaType,
                PermissionType = p.PermissionType,
                IsGranted = p.IsGranted,
                GrantedAt = p.GrantedAt
            })
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<AdminPermissionSummary>> GetAllAdminPermissionsAsync(
        CancellationToken cancellationToken = default)
    {
        // Get all users in Admin role
        var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");

        var allPermissions = await _db.MediaPermissions
            .ToListAsync(cancellationToken);

        return adminUsers
            .Select(u =>
            {
                var userPermissions = allPermissions
                    .Where(p => p.UserId == u.Id)
                    .Select(p => new MediaPermissionEntry
                    {
                        MediaType = p.MediaType,
                        PermissionType = p.PermissionType,
                        IsGranted = p.IsGranted,
                        GrantedAt = p.GrantedAt
                    })
                    .ToList();

                return new AdminPermissionSummary
                {
                    UserId = u.Id,
                    DisplayName = $"{u.FirstName} {u.LastName}".Trim(),
                    Email = u.Email ?? string.Empty,
                    Permissions = userPermissions
                };
            })
            .ToList();
    }
}
