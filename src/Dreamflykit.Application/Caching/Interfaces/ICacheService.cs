namespace Dreamflykit.Application.Caching.Interfaces;

/// <summary>
/// Distributed caching abstraction supporting multi-tier (L1 In-Memory + L2 Distributed Redis) caching,
/// stampede prevention via single-flight execution, and distributed cluster invalidation.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Gets a cached item by key from L1 (Memory) or L2 (Distributed/Redis).
    /// Returns default(T) on cache miss or deserialization failure.
    /// </summary>
    Task<T?> GetAsync<T>(string key, CancellationToken ct = default);

    /// <summary>
    /// Sets an item in both L1 (Memory) and L2 (Distributed/Redis) caches with specified expiration.
    /// </summary>
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken ct = default);

    /// <summary>
    /// Removes an item from both L1 and L2 caches across the cluster.
    /// </summary>
    Task RemoveAsync(string key, CancellationToken ct = default);

    /// <summary>
    /// Removes all cached items matching a key prefix from both L1 and L2 caches.
    /// </summary>
    Task RemoveByPrefixAsync(string prefix, CancellationToken ct = default);

    /// <summary>
    /// Atomically gets or creates a cached item using a single-flight mutex to prevent cache stampedes.
    /// </summary>
    Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken ct = default);
}

