# ==============================================================================
# Dreamflykit Production Dockerfile (.NET 8 MVC / Architecture Standard)
# 
# Security & Best-Practice Highlights:
# 1. Multi-stage build (Separate .NET SDK build stage and minimal runtime)
# 2. Strict non-root execution (Runs as built-in non-root user 'app', UID 1654)
# 3. Layer cache optimization (Restores NuGet packages prior to copying full source)
# 4. Container Health Check probe configured against /health endpoint
# 5. Diagnostic and telemetry ports disabled for reduced attack surface
# 6. Preserves complete standalone static assets in wwwroot (CSS, JS, images)
# 7. Pre-configures writable directories for cache/uploads with non-root ownership
# ==============================================================================

# ------------------------------------------------------------------------------
# Stage 1: Build & Publish .NET application
# ------------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /src

# Disable telemetry and first-run experience during docker build
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1 \
    NUGET_XMLDOC_MODE=skip

# Copy project files first to leverage Docker layer caching for NuGet restore
COPY src/Dreamflykit.Domain/Dreamflykit.Domain.csproj src/Dreamflykit.Domain/
COPY src/Dreamflykit.Contracts/Dreamflykit.Contracts.csproj src/Dreamflykit.Contracts/
COPY src/Dreamflykit.Application/Dreamflykit.Application.csproj src/Dreamflykit.Application/
COPY src/Dreamflykit.Infrastructure/Dreamflykit.Infrastructure.csproj src/Dreamflykit.Infrastructure/
COPY src/Dreamflykit.Web/Dreamflykit.Web.csproj src/Dreamflykit.Web/

# Restore dependencies
RUN dotnet restore src/Dreamflykit.Web/Dreamflykit.Web.csproj

# Copy remaining source code (including authentic wwwroot styles, scripts, and images)
COPY src/Dreamflykit.Domain/ src/Dreamflykit.Domain/
COPY src/Dreamflykit.Contracts/ src/Dreamflykit.Contracts/
COPY src/Dreamflykit.Application/ src/Dreamflykit.Application/
COPY src/Dreamflykit.Infrastructure/ src/Dreamflykit.Infrastructure/
COPY src/Dreamflykit.Web/ src/Dreamflykit.Web/

# Publish compiled binaries in Release configuration
RUN dotnet publish src/Dreamflykit.Web/Dreamflykit.Web.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# ------------------------------------------------------------------------------
# Stage 2: Minimal, Hardened Production Runtime
# ------------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS final
WORKDIR /app

# Install curl for container health check probe; clean apt caches in same layer
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

# Copy published application artifacts
COPY --from=build /app/publish .

# Create writable runtime directories for media cache and uploads with non-root ownership
RUN mkdir -p /app/App_Data/MediaCache /app/wwwroot/uploads \
    && chown -R $APP_UID:$APP_UID /app

# Configure production environment variables
ENV ASPNETCORE_HTTP_PORTS=8080 \
    ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_EnableDiagnostics=0 \
    DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

# Container listening port (.NET 8 non-root standard)
EXPOSE 8080

# Health check probe against ASP.NET Core health check endpoint
HEALTHCHECK --interval=30s --timeout=5s --start-period=15s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

# Drop privileges to official built-in non-root user ($APP_UID = 1654)
USER $APP_UID

# Start the application
ENTRYPOINT ["dotnet", "Dreamflykit.Web.dll"]
