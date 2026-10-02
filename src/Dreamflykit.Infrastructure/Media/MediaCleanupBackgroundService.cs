using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.Media;

/// <summary>
/// Background maintenance worker that periodically cleans up:
/// 1. Soft-deleted media assets from AWS S3 storage.
/// 2. Stale 'Draft' uploads older than 24 hours that were never confirmed.
/// </summary>
public sealed class MediaCleanupBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MediaCleanupBackgroundService> _logger;
    private static readonly TimeSpan Interval = TimeSpan.FromHours(12);

    public MediaCleanupBackgroundService(
        IServiceProvider serviceProvider,
        ILogger<MediaCleanupBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Media cleanup background service started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CleanOrphanedAssetsAsync(stoppingToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(ex, "An error occurred during scheduled media cleanup.");
            }

            await Task.Delay(Interval, stoppingToken);
        }
    }

    private async Task CleanOrphanedAssetsAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DreamflykitDbContext>();
        var storage = scope.ServiceProvider.GetRequiredService<IMediaStorageService>();

        var cutoffDraft = DateTime.UtcNow.AddHours(-24);

        // 1. Stale unconfirmed Draft uploads
        var staleDrafts = await db.MediaAssets
            .Where(a => a.Status == MediaAssetStatus.Draft && a.CreatedAtUtc < cutoffDraft)
            .Take(50)
            .ToListAsync(cancellationToken);

        foreach (var draft in staleDrafts)
        {
            try
            {
                await storage.DeleteObjectAsync(draft.ObjectKey, cancellationToken);
                db.MediaAssets.Remove(draft);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to clean stale draft asset {Id}", draft.Id);
            }
        }

        if (staleDrafts.Count > 0)
        {
            await db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Purged {Count} stale draft media assets.", staleDrafts.Count);
        }
    }
}
