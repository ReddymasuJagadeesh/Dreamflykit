using Dreamflykit.Application.Audit.Interfaces;
using Dreamflykit.Domain.Audit;
using Dreamflykit.Infrastructure.Persistence;

namespace Dreamflykit.Infrastructure.Audit;

public sealed class AuditLogService : IAuditLogService
{
    private readonly DreamflykitDbContext _dbContext;

    public AuditLogService(DreamflykitDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task LogAsync(
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
        CancellationToken cancellationToken = default)
    {
        var audit = new AuditLog(
            userId: userId,
            userName: userName,
            actorType: actorType,
            action: action,
            entityName: entityName,
            tableName: tableName,
            recordId: recordId,
            oldValues: oldValues,
            newValues: newValues,
            affectedColumns: affectedColumns,
            ipAddress: ipAddress,
            userAgent: userAgent,
            traceId: traceId,
            isSuccess: isSuccess,
            errorMessage: errorMessage,
            notes: notes);

        await _dbContext.AuditLogs.AddAsync(audit, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
