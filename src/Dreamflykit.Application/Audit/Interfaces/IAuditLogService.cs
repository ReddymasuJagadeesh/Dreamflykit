namespace Dreamflykit.Application.Audit.Interfaces;

public interface IAuditLogService
{
    Task LogAsync(
        Guid? userId,
        string? userName,
        string actorType,
        string action,
        string entityName,
        string tableName,
        string recordId,
        string? oldValues = null,
        string? newValues = null,
        string? affectedColumns = null,
        string? ipAddress = null,
        string? userAgent = null,
        string? traceId = null,
        bool isSuccess = true,
        string? errorMessage = null,
        string? notes = null,
        CancellationToken cancellationToken = default);
}
