# 15. PERFORMANCE & SCALABILITY AUDIT: DREAMFLYKIT

**Objective:** Performance profiling across application layers, memory utilization, caching strategies, and concurrency scaling scenarios.

---

## 1. Concurrency Traffic Scenario Modeling

| Concurrent Active Users | Peak Requests / Sec | Bottleneck Analysis with Current Architecture | Required Infrastructure Architecture |
| :--- | :--- | :--- | :--- |
| **100 Users** | ~25 RPS | In-memory cache handles read traffic; DB handles low checkout volume. Current setup functions. | Single VPS (4 vCPU, 8 GB RAM), PostgreSQL 18 |
| **500 Users** | ~120 RPS | File system JSON serialization locks on admin saves; DB connection pool spikes. | Single VPS (8 vCPU, 16 GB RAM), Redis caching |
| **1,000 Users** | ~300 RPS | Memory pressure from 14 in-memory DTO trees; checkout concurrency conflicts spike. | 2 Web Nodes + Load Balancer, Dedicated DB, Redis |
| **5,000 Users** | ~1,500 RPS | Severe database contention on catalogue queries; file system flat files fail completely. | 4+ Web Nodes, PostgreSQL Primary + Read Replica, CDN |
| **10,000 Users** | ~3,500 RPS | Connection pool starvation; order queue lockups without async messaging. | Autoscaling Web Nodes, PgBouncer, RabbitMQ/Outbox |

---

## 2. In-Memory Caching vs. Distributed Redis Architecture

### Current Implementation:
```csharp
// InfrastructureServiceCollectionExtensions.cs
services.AddMemoryCache();
```
All 14 Hub services cache their DTOs in `IMemoryCache` on the local web server process for 4 hours:
- `_cache.Set(CacheKey, dto, TimeSpan.FromHours(4));`

### Architectural Hazards of Local `IMemoryCache`:
1. **Multi-Instance Inconsistency:** In a scaled deployment with 2 or more web instances behind a reverse proxy (e.g., Hostinger VPS or Docker Swarm), an update made by an admin on Node A invalidates Node A's cache, but Node B continues serving stale cached data for up to 4 hours.
2. **Process Restart Eviction:** Any deployment or container restart dumps the entire in-memory cache, causing "cache stampede" where concurrent user requests hit the disk to parse multi-thousand-line JSON files simultaneously.
3. **Target Solution:** Replace `IMemoryCache` with **Redis Distributed Caching** (`AddStackExchangeRedisCache`).

---

## 3. Media & Static File Delivery Architecture

### Current Implementation:
- Hybrid storage: AWS S3 storage service (`S3MediaStorageService.cs`) and local disk uploads (`/wwwroot/uploads`).
- Static CSS / JS: Unbundled, unminified files (`bootstrap.css` 11,221 lines, `jquery.js` 10,881 lines, `app.css` 10,560 lines).
- Images: Over 100 images stored locally in `wwwroot/images/` including large JPEGs (>1 MB each).

### Performance Recommendations:
1. **AWS CloudFront CDN:** Serve all product media and category banners via CloudFront with HTTP/3 and WebP automatic conversion.
2. **Asset Bundling & Minification:** Configure Vite or Tailwind CLI minification to compress CSS from 30+ MB uncompressed down to < 150 KB gzipped.
