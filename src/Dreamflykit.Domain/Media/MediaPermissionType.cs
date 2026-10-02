namespace Dreamflykit.Domain.Media;

/// <summary>
/// Granular media permission types that Super Admin can grant/revoke per Admin user.
/// </summary>
public enum MediaPermissionType
{
    View = 1,
    Upload = 2,
    Update = 3,
    Delete = 4,
    Reorder = 5,
    Publish = 6,
    Unpublish = 7,
    Replace = 8,
    BulkUpload = 9
}
