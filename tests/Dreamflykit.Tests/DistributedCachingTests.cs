using System.Text;
using Dreamflykit.Application.Caching.Interfaces;
using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Domain.Catalog;
using Dreamflykit.Infrastructure.Caching;
using Dreamflykit.Infrastructure.Catalog;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Dreamflykit.Tests;

public class DistributedCachingTests
{
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _distributedCache;
    private readonly CacheService _cacheService;

    public DistributedCachingTests()
    {
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        _distributedCache = new MemoryDistributedCache(
            Microsoft.Extensions.Options.Options.Create(new MemoryDistributedCacheOptions()));
        _cacheService = new CacheService(
            _distributedCache,
            _memoryCache,
            NullLogger<CacheService>.Instance,
            redisMultiplexer: null);
    }

    private class SampleCacheItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    [Fact]
    public async Task GetAsync_WhenKeyDoesNotExist_ReturnsNull()
    {
        // Act
        var result = await _cacheService.GetAsync<SampleCacheItem>("non_existent_key");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SetAsync_And_GetAsync_RoundTripsSuccessfully()
    {
        // Arrange
        var item = new SampleCacheItem { Id = 42, Name = "Laptop Bag", Price = 1299m };
        var key = "test:item:42";

        // Act
        await _cacheService.SetAsync(key, item, TimeSpan.FromMinutes(10));
        var retrieved = await _cacheService.GetAsync<SampleCacheItem>(key);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal(42, retrieved.Id);
        Assert.Equal("Laptop Bag", retrieved.Name);
        Assert.Equal(1299m, retrieved.Price);
    }

    [Fact]
    public async Task RemoveAsync_EvictsFromBothTiers()
    {
        // Arrange
        var item = new SampleCacheItem { Id = 10, Name = "Headphones", Price = 2499m };
        var key = "test:item:10";
        await _cacheService.SetAsync(key, item, TimeSpan.FromMinutes(10));

        // Act
        await _cacheService.RemoveAsync(key);
        var retrieved = await _cacheService.GetAsync<SampleCacheItem>(key);

        // Assert
        Assert.Null(retrieved);
    }

    [Fact]
    public async Task RemoveByPrefixAsync_EvictsAllMatchingKeys()
    {
        // Arrange
        await _cacheService.SetAsync("prefix:item1", new SampleCacheItem { Id = 1, Name = "Item 1" }, TimeSpan.FromMinutes(5));
        await _cacheService.SetAsync("prefix:item2", new SampleCacheItem { Id = 2, Name = "Item 2" }, TimeSpan.FromMinutes(5));
        await _cacheService.SetAsync("other:item3", new SampleCacheItem { Id = 3, Name = "Item 3" }, TimeSpan.FromMinutes(5));

        // Act
        await _cacheService.RemoveByPrefixAsync("prefix:");

        // Assert
        Assert.Null(await _cacheService.GetAsync<SampleCacheItem>("prefix:item1"));
        Assert.Null(await _cacheService.GetAsync<SampleCacheItem>("prefix:item2"));
        Assert.NotNull(await _cacheService.GetAsync<SampleCacheItem>("other:item3"));
    }

    [Fact]
    public async Task GetOrCreateAsync_ExecutesFactoryOnlyOnCacheMiss()
    {
        // Arrange
        var key = "test:get_or_create";
        var callCount = 0;

        Task<SampleCacheItem> Factory(CancellationToken ct)
        {
            Interlocked.Increment(ref callCount);
            return Task.FromResult(new SampleCacheItem { Id = 100, Name = "Created Once" });
        }

        // Act
        var first = await _cacheService.GetOrCreateAsync(key, Factory, TimeSpan.FromMinutes(10));
        var second = await _cacheService.GetOrCreateAsync(key, Factory, TimeSpan.FromMinutes(10));

        // Assert
        Assert.Equal(1, callCount);
        Assert.Equal("Created Once", first.Name);
        Assert.Equal("Created Once", second.Name);
    }

    [Fact]
    public async Task GetOrCreateAsync_UnderHighConcurrency_PreventsCacheStampede()
    {
        // Arrange
        var key = "stampede:hot_key";
        var factoryExecutionCount = 0;

        async Task<SampleCacheItem> SlowFactory(CancellationToken ct)
        {
            Interlocked.Increment(ref factoryExecutionCount);
            await Task.Delay(100, ct); // simulate slow database query
            return new SampleCacheItem { Id = 999, Name = "Expensive Data" };
        }

        // Act - Simulate 20 concurrent threads requesting the exact same expired key simultaneously
        var tasks = Enumerable.Range(0, 20)
            .Select(_ => _cacheService.GetOrCreateAsync(key, SlowFactory, TimeSpan.FromMinutes(10)))
            .ToList();

        var results = await Task.WhenAll(tasks);

        // Assert - Only 1 factory execution must have occurred despite 20 concurrent requests
        Assert.Equal(1, factoryExecutionCount);
        Assert.Equal(20, results.Length);
        Assert.All(results, r => Assert.Equal("Expensive Data", r.Name));
    }

    [Fact]
    public async Task GetAsync_BackfillsL1_WhenFoundInL2DistributedCache()
    {
        // Arrange
        var key = "test:l2_backfill";
        var item = new SampleCacheItem { Id = 77, Name = "Distributed Only", Price = 500m };

        // Write directly to L2 distributed cache bypassing L1
        var json = System.Text.Json.JsonSerializer.Serialize(item);
        await _distributedCache.SetStringAsync(key, json);

        // Verify L1 does not have it yet
        Assert.False(_memoryCache.TryGetValue(key, out _));

        // Act - GetAsync should read from L2 and populate L1
        var result = await _cacheService.GetAsync<SampleCacheItem>(key);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(77, result.Id);
        Assert.True(_memoryCache.TryGetValue(key, out SampleCacheItem? l1Value));
        Assert.Equal(77, l1Value!.Id);
    }

    [Fact]
    public async Task CacheService_DegradesGracefully_WhenDistributedCacheThrows()
    {
        // Arrange
        var failingDistributedCacheMock = new Mock<IDistributedCache>();
        failingDistributedCacheMock
            .Setup(d => d.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Redis connection timed out"));

        var resilientCache = new CacheService(
            failingDistributedCacheMock.Object,
            _memoryCache,
            NullLogger<CacheService>.Instance);

        // Act & Assert - Should not throw exception, should return default
        var result = await resilientCache.GetAsync<SampleCacheItem>("failing:key");
        Assert.Null(result);
    }

    [Fact]
    public async Task CategoryHubRepository_WithCacheService_CachesAndInvalidates()
    {
        // Arrange
        var envMock = new Mock<IWebHostEnvironment>();
        envMock.Setup(e => e.WebRootPath).Returns(Path.GetTempPath());

        var repo = new CategoryHubRepository(
            context: null,
            cache: _memoryCache,
            env: envMock.Object,
            logger: NullLogger<CategoryHubRepository>.Instance,
            cacheService: _cacheService);

        var payload = new SampleCacheItem { Id = 5, Name = "Fashion Category Dto", Price = 100m };

        // Act - Save payload
        await repo.SavePayloadAsync("fashion", "Fashion", "fashion_hub.json", payload);

        // Verify it was cached in CacheService
        var cached = await _cacheService.GetAsync<SampleCacheItem>("category_hub_payload_fashion");
        Assert.NotNull(cached);
        Assert.Equal("Fashion Category Dto", cached.Name);

        // Act - Invalidate
        await repo.InvalidateHubCacheAsync("fashion");

        // Verify evicted
        var evicted = await _cacheService.GetAsync<SampleCacheItem>("category_hub_payload_fashion");
        Assert.Null(evicted);
    }
}

