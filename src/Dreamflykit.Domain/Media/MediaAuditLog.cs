using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Media;

/// <summary>
/// Immutable audit record of every significant media operation.
/// Records are insert-only — never updated or deleted.
/// </summary>
public sealed class MediaAuditLog : Entity
{
    private MediaAuditLog()
    {
    }

    public MediaAuditLog(
        Guid userId,
        string action,
        Guid? mediaAssetId,
        string resourceType,
        Guid? resourceId,
        string? oldValue,
        string? newValue,
        string? notes)
    {
        if (userId == Guid.Empty)
        {
            throw new DomainException("User ID is required for audit log.");
        }

        if (string.IsNullOrWhiteSpace(action))
        {
            throw new DomainException("Action is required for audit log.");
        }

        if (string.IsNullOrWhiteSpace(resourceType))
        {
            throw new DomainException("Resource type is required for audit log.");
        }

        UserId = userId;
        Action = action.Trim();
        MediaAssetId = mediaAssetId;
        ResourceType = resourceType.Trim();
        ResourceId = resourceId;
        OldValue = oldValue?.Trim();
        NewValue = newValue?.Trim();
        Notes = notes?.Trim();
        OccurredAtUtc = DateTime.UtcNow;
    }

    /// <summary>The user who performed the action.</summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// Human-readable action name, e.g. "Uploaded", "Deleted", "Published", "PermissionGranted".
    /// </summary>
    public string Action { get; private set; } = string.Empty;

    /// <summary>The media asset this log entry relates to (if applicable).</summary>
    public Guid? MediaAssetId { get; private set; }

    /// <summary>
    /// The type of the resource the action was performed on
    /// (e.g., "ProductMedia", "Banner", "HomepageSection").
    /// </summary>
    public string ResourceType { get; private set; } = string.Empty;

    /// <summary>The ID of the resource (e.g., ProductId, BannerId).</summary>
    public Guid? ResourceId { get; private set; }

    /// <summary>Serialized previous state (JSON or plain value).</summary>
    public string? OldValue { get; private set; }

    /// <summary>Serialized new state (JSON or plain value).</summary>
    public string? NewValue { get; private set; }

    public string? Notes { get; private set; }

    public DateTime OccurredAtUtc { get; private set; }
}
