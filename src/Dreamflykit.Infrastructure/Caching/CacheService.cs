using System.Collections.Concurrent;
using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.Caching.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Dreamflykit.Infrastructure.Caching;

/// <summary>
/// Production multi-tier cache service (L1 Memory + L2 Redis/Distributed)
/// with single-flight stampede prevention, cluster invalidation, and graceful failover.
/// </summary>
public sealed class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IMemoryCache _memoryCache;
    private readonly IConnectionMultiplexer? _redisMultiplexer;
    private readonly ILogger<CacheService> _logger;

    private static readonly TimeSpan DefaultL2Ttl = TimeSpan.FromMinutes(30);
    private static readonly TimeSpan DefaultL1Ttl = TimeSpan.FromMinutes(5);

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = false
    };

    // Tracks active keys for prefix-based invalidation in local L1
    private readonly ConcurrentDictionary<string, byte> _knownKeys = new(StringComparer.OrdinalIgnoreCase);

    // Single-flight locks to prevent cache stampedes under high concurrency
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new(StringComparer.OrdinalIgnoreCase);

    public CacheService(
        IDistributedCache distributedCache,
        IMemoryCache memoryCache,
        ILogger<CacheService> logger,
        IConnectionMultiplexer? redisMultiplexer = null)
    {
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _redisMultiplexer = redisMultiplexer;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(key)) return default;

        // 1. Try L1 (Local In-Memory Cache)
        if (_memoryCache.TryGetValue(key, out T? localValue) && localValue != null)
        {
            return localValue;
        }

        // 2. Try L2 (Distributed / Redis Cache)
        try
        {
            var bytes = await _distributedCache.GetAsync(key, ct);
            if (bytes != null && bytes.Length > 0)
            {
                var deserialized = JsonSerializer.Deserialize<T>(bytes, JsonOptions);
                if (deserialized != null)
                {
                    // Backfill L1 for fast subsequent local reads
                    _memoryCache.Set(key, deserialized, DefaultL1Ttl);
                    _knownKeys.TryAdd(key, 0);
                    return deserialized;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Distributed cache read failed for key '{Key}'. Degrading gracefully.", key);
        }

        return default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(key) || value == null) return;

        var l2Ttl = expiration ?? DefaultL2Ttl;
        var l1Ttl = l2Ttl < DefaultL1Ttl ? l2Ttl : DefaultL1Ttl;

        // 1. Set L1
        _memoryCache.Set(key, value, l1Ttl);
        _knownKeys.TryAdd(key, 0);

        // 2. Set L2
        try
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes(value, JsonOptions);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = l2Ttl
            };

            await _distributedCache.SetAsync(key, bytes, options, ct);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Distributed cache write failed for key '{Key}'. L1 cache remains active.", key);
        }
    }

    public async Task RemoveAsync(string key, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(key)) return;

        // 1. Evict from L1
        _memoryCache.Remove(key);
        _knownKeys.TryRemove(key, out _);

        // 2. Evict from L2
        try
        {
            await _distributedCache.RemoveAsync(key, ct);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Distributed cache eviction failed for key '{Key}'.", key);
        }
    }

    public async Task RemoveByPrefixAsync(string prefix, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(prefix)) return;

        // 1. Evict from local L1 memory cache and known distributed keys
        var matchingKeys = _knownKeys.Keys
            .Where(k => k.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            .ToList();

        foreach (var key in matchingKeys)
        {
            _memoryCache.Remove(key);
            _knownKeys.TryRemove(key, out _);

            try
            {
                await _distributedCache.RemoveAsync(key, ct);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to remove key '{Key}' from distributed cache during prefix eviction.", key);
            }
        }

        // 2. Evict from Redis L2 cluster if connected
        if (_redisMultiplexer != null && _redisMultiplexer.IsConnected)
        {
            try
            {
                var endpoints = _redisMultiplexer.GetEndPoints();
                var redisDb = _redisMultiplexer.GetDatabase();

                foreach (var endpoint in endpoints)
                {
                    var server = _redisMultiplexer.GetServer(endpoint);
                    if (server.IsConnected)
                    {
                        var keys = server.Keys(pattern: $"*{prefix}*").ToArray();
                        if (keys.Length > 0)
                        {
                            await redisDb.KeyDeleteAsync(keys);
                            _logger.LogInformation("Evicted {Count} Redis keys matching prefix '{Prefix}'", keys.Length, prefix);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Redis cluster prefix eviction failed for prefix '{Prefix}'.", prefix);
            }
        }
    }

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return await factory(ct);
        }

        // 1. Fast path: check L1 and L2 before locking
        var existing = await GetAsync<T>(key, ct);
        if (existing != null)
        {
            return existing;
        }

        // 2. Stampede Protection: acquire per-key single-flight lock
        var keyLock = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        await keyLock.WaitAsync(ct);

        try
        {
            // Double check after lock acquisition
            existing = await GetAsync<T>(key, ct);
            if (existing != null)
            {
                return existing;
            }

            // 3. Execute factory
            var created = await factory(ct);

            // 4. Cache in both tiers
            if (created != null)
            {
                await SetAsync(key, created, expiration, ct);
            }

            return created;
        }
        finally
        {
            keyLock.Release();

            // Clean up unused semaphore to prevent dictionary growth
            if (keyLock.CurrentCount == 1)
            {
                _locks.TryRemove(key, out _);
            }
        }
    }
}
