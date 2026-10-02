using Dreamflykit.Domain.Media;

namespace Dreamflykit.Application.Media.Interfaces;

/// <summary>
/// Server-side media permission enforcement.
/// All media operations must call HasPermissionAsync before executing.
/// </summary>
public interface IMediaPermissionService
{
    /// <summary>
    /// Returns true if the given user is allowed to perform the operation on the given media type.
    /// - SuperAdmin: always returns true.
    /// - Admin: checks MediaPermissions table for an explicit grant.
    /// - Other roles: always returns false.
    /// </summary>
    Task<bool> HasPermissionAsync(
        Guid userId,
        MediaType mediaType,
        MediaPermissionType permissionType,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Grants or revokes a permission. Can only be called by SuperAdmin.
    /// </summary>
    Task GrantOrRevokeAsync(
        Guid targetUserId,
        MediaType mediaType,
        MediaPermissionType permissionType,
        bool isGranted,
        Guid grantedBy,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns all permissions for a given user.
    /// </summary>
    Task<IReadOnlyList<MediaPermissionEntry>> GetUserPermissionsAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a permission matrix for all admins (SuperAdmin use only).
    /// </summary>
    Task<IReadOnlyList<AdminPermissionSummary>> GetAllAdminPermissionsAsync(
        CancellationToken cancellationToken = default);
}

public sealed class MediaPermissionEntry
{
    public MediaType MediaType { get; init; }

    public MediaPermissionType PermissionType { get; init; }

    public bool IsGranted { get; init; }

    public DateTime GrantedAt { get; init; }
}

public sealed class AdminPermissionSummary
{
    public Guid UserId { get; init; }

    public string DisplayName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public IReadOnlyList<MediaPermissionEntry> Permissions { get; init; }
        = Array.Empty<MediaPermissionEntry>();
}
