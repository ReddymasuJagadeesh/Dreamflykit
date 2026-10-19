using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Audit;

/// <summary>
/// Immutable enterprise audit trail capturing who, what, when, where, and why
/// for critical domain mutations, financial transactions, inventory changes, and administrative actions.
/// </summary>
public sealed class AuditLog : Entity
{
    private AuditLog()
    {
    }

    public AuditLog(
        Guid? userId,
        string? userName,
        string actorType,
        string action,
        string entityName,
        string tableName,
        string recordId,
        string? oldValues,
        string? newValues,
        string? affectedColumns,
        string? ipAddress,
        string? userAgent,
        string? traceId,
        bool isSuccess,
        string? errorMessage,
        string? notes)
    {
        if (string.IsNullOrWhiteSpace(actorType))
            throw new DomainException("Actor type is required for audit log.");

        if (string.IsNullOrWhiteSpace(action))
            throw new DomainException("Action is required for audit log.");

        if (string.IsNullOrWhiteSpace(entityName))
            throw new DomainException("Entity name is required for audit log.");

        if (string.IsNullOrWhiteSpace(tableName))
            throw new DomainException("Table name is required for audit log.");

        if (string.IsNullOrWhiteSpace(recordId))
            throw new DomainException("Record ID is required for audit log.");

        UserId = userId;
        UserName = userName?.Trim();
        ActorType = actorType.Trim();
        Action = action.Trim().ToUpperInvariant();
        EntityName = entityName.Trim();
        TableName = tableName.Trim();
        RecordId = recordId.Trim();
        OldValues = oldValues;
        NewValues = newValues;
        AffectedColumns = affectedColumns?.Trim();
        IpAddress = ipAddress?.Trim();
        UserAgent = userAgent?.Trim();
        TraceId = traceId?.Trim();
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage?.Trim();
        Notes = notes?.Trim();
        OccurredAtUtc = DateTime.UtcNow;
    }

    /// <summary>The ID of the user who performed the action (null if system, background job, or anonymous webhook).</summary>
    public Guid? UserId { get; private set; }

    /// <summary>Username or email of the actor at the time of the event.</summary>
    public string? UserName { get; private set; }

    /// <summary>Category of actor: User, Admin, Seller, System, BackgroundJob, PaymentWebhook.</summary>
    public string ActorType { get; private set; } = "User";

    /// <summary>The operation performed (e.g., INSERT, UPDATE, DELETE, ORDER_PLACED, STATUS_CHANGE).</summary>
    public string Action { get; private set; } = string.Empty;

    /// <summary>Name of the domain entity (e.g., Order, Product, InventoryItem).</summary>
    public string EntityName { get; private set; } = string.Empty;

    /// <summary>Name of the relational database table affected.</summary>
    public string TableName { get; private set; } = string.Empty;

    /// <summary>Primary key of the affected record as a string.</summary>
    public string RecordId { get; private set; } = string.Empty;

    /// <summary>JSON snapshot of state prior to change (null for insert operations).</summary>
    public string? OldValues { get; private set; }

    /// <summary>JSON snapshot of state after change (null for delete operations).</summary>
    public string? NewValues { get; private set; }

    /// <summary>Comma-separated list or JSON array of specific properties modified.</summary>
    public string? AffectedColumns { get; private set; }

    /// <summary>IP address from which the HTTP request originated.</summary>
    public string? IpAddress { get; private set; }

    /// <summary>User-Agent string of the calling client or service.</summary>
    public string? UserAgent { get; private set; }

    /// <summary>Correlation / Trace ID for distributed tracing across logs and metrics.</summary>
    public string? TraceId { get; private set; }

    /// <summary>Indicates whether the audited business operation succeeded.</summary>
    public bool IsSuccess { get; private set; } = true;

    /// <summary>Error message or failure reason if IsSuccess is false.</summary>
    public string? ErrorMessage { get; private set; }

    /// <summary>Business reason, notes, or justification for administrative action.</summary>
    public string? Notes { get; private set; }

    /// <summary>Exact UTC timestamp when the event occurred.</summary>
    public DateTime OccurredAtUtc { get; private set; }
}
