using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Media;

/// <summary>
/// Granular media permission granted or explicitly revoked by Super Admin for a specific Admin user.
/// Super Admin has all permissions implicitly — this table only governs Admin users.
/// </summary>
public sealed class MediaPermission : AuditableEntity
{
    private MediaPermission()
    {
    }

    public MediaPermission(
        Guid userId,
        MediaType mediaType,
        MediaPermissionType permissionType,
        bool isGranted,
        Guid grantedBy)
    {
        if (userId == Guid.Empty)
        {
            throw new DomainException("User ID is required.");
        }

        if (grantedBy == Guid.Empty)
        {
            throw new DomainException("Granted-by user ID is required.");
        }

        UserId = userId;
        MediaType = mediaType;
        PermissionType = permissionType;
        IsGranted = isGranted;
        GrantedBy = grantedBy;
        GrantedAt = DateTime.UtcNow;
    }

    /// <summary>The Admin user this permission record applies to.</summary>
    public Guid UserId { get; private set; }

    /// <summary>The media category this permission applies to (e.g., Product, Banner).</summary>
    public MediaType MediaType { get; private set; }

    /// <summary>The specific operation (e.g., Upload, Delete, Publish).</summary>
    public MediaPermissionType PermissionType { get; private set; }

    /// <summary>
    /// True = permission is granted. False = permission is explicitly revoked.
    /// Absence of a record = no permission (deny by default).
    /// </summary>
    public bool IsGranted { get; private set; }

    /// <summary>The Super Admin user who granted or revoked this permission.</summary>
    public Guid GrantedBy { get; private set; }

    public DateTime GrantedAt { get; private set; }

    public void Update(bool isGranted, Guid updatedBy)
    {
        if (updatedBy == Guid.Empty)
        {
            throw new DomainException("Updated-by user ID is required.");
        }

        IsGranted = isGranted;
        GrantedBy = updatedBy;
        GrantedAt = DateTime.UtcNow;
        MarkUpdated();
    }
}
