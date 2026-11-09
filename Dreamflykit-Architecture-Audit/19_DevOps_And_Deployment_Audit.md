# 19. DEVOPS, DOCKER & HOSTINGER VPS DEPLOYMENT AUDIT: DREAMFLYKIT

**Objective:** Audit of Docker multi-stage builds, compose service topologies, persistent storage volumes, and Hostinger VPS deployment readiness.

---

## 1. Dockerfile Production Architecture Audit

### Production Strengths Verified:
- Multi-stage build (`mcr.microsoft.com/dotnet/sdk:8.0` build stage -> `mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim` final stage).
- Non-root execution: runs under built-in unprivileged user `app` (`UID 1654`).
- Healthcheck probe configured: `curl -f http://localhost:8080/health || exit 1`.
- Listening on standard non-root port `8080`.

### CRITICAL DOCKER DEFECT: Missing Volume for Hub Flat Files
In `Dockerfile`:
```dockerfile
# Line 64:
RUN mkdir -p /app/App_Data/MediaCache /app/wwwroot/uploads \
    && chown -R $APP_UID:$APP_UID /app
```
In `docker-compose.yml`:
```yaml
volumes:
  - media_cache:/app/App_Data/MediaCache
```
**The Flaw:**
`/app/wwwroot/data` (which stores the 15 category hub `.json` files) is **NOT mounted as a persistent Docker volume!**
When Dreamflykit is deployed on Hostinger VPS using Docker, any changes made by administrators in the Admin Hubs (banner updates, category names, featured items) are written directly into the ephemeral container filesystem.
**As soon as a new Docker image is deployed or the container is recreated, ALL ADMIN MODIFICATIONS TO HUBS ARE PERMANENTLY LOST!**

---

## 2. Docker Compose & Environment Topology

### PostgreSQL Version Inconsistency:
- In `docker-compose.yml`: `image: postgres:16-alpine`.
- In Development / Local machine: `PostgreSQL 18.6`.
- **Risk:** Features or collation differences between PostgreSQL 16 and PostgreSQL 18 can lead to migration failures during production deployment.

### Missing Production Services in `docker-compose.yml`:
1. **No Redis Service:** Required for shared session state and distributed hub caching.
2. **No Nginx / Reverse Proxy:** The web container exposes port 8080 directly via HTTP. There is no automated SSL certificate provisioning (Let's Encrypt / Certbot) or gzip/brotli static asset compression proxy.

---

## 3. CI/CD & Build Pipeline Audit

- **GitHub Actions (`.github/workflows`):** **COMPLETELY MISSING.**
- There is no automated pull-request validation, no automated `dotnet test` execution, and no continuous deployment pipeline configured for Hostinger VPS.
