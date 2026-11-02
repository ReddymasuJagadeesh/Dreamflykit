using System.Text.Json;
using Dreamflykit.Application.CountryKits.Interfaces;
using Dreamflykit.Application.CountryKits.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.CountryKits;

public sealed class CountryKitsHubService : ICountryKitsHubService
{
    private const string CacheKey = "CountryKitsHub_Data";
    private static readonly SemaphoreSlim Semaphore = new(1, 1);
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<CountryKitsHubService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public CountryKitsHubService(
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<CountryKitsHubService> logger)
    {
        _cache = cache;
        _env = env;
        _logger = logger;
        _filePath = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "data", "country_kits_hub.json");
    }

    public async Task<CountryKitsHubDto> GetAllProfilesAsync(CancellationToken ct = default)
    {
        if (_cache.TryGetValue(CacheKey, out CountryKitsHubDto? cached) && cached != null)
        {
            return cached;
        }

        await Semaphore.WaitAsync(ct);
        try
        {
            if (_cache.TryGetValue(CacheKey, out cached) && cached != null)
            {
                return cached;
            }

            CountryKitsHubDto? data = null;

            if (File.Exists(_filePath))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(_filePath, ct);
                    data = JsonSerializer.Deserialize<CountryKitsHubDto>(json, JsonOptions);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to deserialize CountryKitsHub from file {Path}", _filePath);
                }
            }

            if (data == null || data.Countries == null || data.Countries.Count == 0)
            {
                data = CountryKitsHubDto.CreateDefault();
                await SaveToFileAsync(data, ct);
            }

            _cache.Set(CacheKey, data, TimeSpan.FromMinutes(30));
            return data;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public async Task<CountryKitProfileDto> GetCountryProfileAsync(string countrySlug, CancellationToken ct = default)
    {
        var all = await GetAllProfilesAsync(ct);
        return all.GetCountryProfile(countrySlug);
    }

    public async Task<bool> SaveCountryProfileAsync(CountryKitProfileDto profile, CancellationToken ct = default)
    {
        if (profile == null || string.IsNullOrWhiteSpace(profile.CountrySlug)) return false;

        await Semaphore.WaitAsync(ct);
        try
        {
            var all = await GetAllProfilesAsync(ct);
            var slug = CountryKitsHubDto.NormalizeSlug(profile.CountrySlug);

            profile.CountrySlug = slug;
            all.Countries[slug] = profile;

            var success = await SaveToFileAsync(all, ct);
            if (success)
            {
                _cache.Set(CacheKey, all, TimeSpan.FromMinutes(30));
                _logger.LogInformation("Saved Country Kits profile for '{Slug}'", slug);
            }
            return success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving Country Kits profile for '{Slug}'", profile.CountrySlug);
            return false;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public async Task<bool> DeleteCountryProfileAsync(string countrySlug, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(countrySlug)) return false;

        await Semaphore.WaitAsync(ct);
        try
        {
            var all = await GetAllProfilesAsync(ct);
            var slug = CountryKitsHubDto.NormalizeSlug(countrySlug);

            if (all.Countries.Remove(slug))
            {
                var success = await SaveToFileAsync(all, ct);
                if (success)
                {
                    _cache.Set(CacheKey, all, TimeSpan.FromMinutes(30));
                    _logger.LogInformation("Deleted Country Kits profile for '{Slug}'", slug);
                }
                return success;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting Country Kits profile for '{Slug}'", countrySlug);
            return false;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public async Task<bool> ResetDefaultsAsync(string? countrySlug = null, CancellationToken ct = default)
    {
        await Semaphore.WaitAsync(ct);
        try
        {
            var defaults = CountryKitsHubDto.CreateDefault();

            if (string.IsNullOrWhiteSpace(countrySlug))
            {
                var success = await SaveToFileAsync(defaults, ct);
                if (success)
                {
                    _cache.Set(CacheKey, defaults, TimeSpan.FromMinutes(30));
                }
                return success;
            }
            else
            {
                var all = await GetAllProfilesAsync(ct);
                var slug = CountryKitsHubDto.NormalizeSlug(countrySlug);

                if (defaults.Countries.TryGetValue(slug, out var defaultProfile))
                {
                    all.Countries[slug] = defaultProfile;
                }
                else
                {
                    all.Countries[slug] = defaults.GetCountryProfile(slug);
                }

                var success = await SaveToFileAsync(all, ct);
                if (success)
                {
                    _cache.Set(CacheKey, all, TimeSpan.FromMinutes(30));
                }
                return success;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error resetting defaults for CountryKitsHub");
            return false;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    private async Task<bool> SaveToFileAsync(CountryKitsHubDto data, CancellationToken ct)
    {
        try
        {
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var json = JsonSerializer.Serialize(data, JsonOptions);
            await File.WriteAllTextAsync(_filePath, json, ct);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save CountryKitsHub to {Path}", _filePath);
            return false;
        }
    }
}

