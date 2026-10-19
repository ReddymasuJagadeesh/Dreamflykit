using Dreamflykit.Infrastructure.Persistence;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Dreamflykit.Web.Health;

public sealed class DatabaseHealthCheck : IHealthCheck
{
    private readonly DreamflykitDbContext _dbContext;

    public DatabaseHealthCheck(DreamflykitDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var canConnect = await _dbContext.Database.CanConnectAsync(cancellationToken);
            return canConnect
                ? HealthCheckResult.Healthy("Database connection successful.")
                : HealthCheckResult.Unhealthy("Cannot connect to PostgreSQL database.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Database health probe encountered an exception.", ex);
        }
    }
}
