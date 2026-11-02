using Dreamflykit.Application.CountryKits.Models;

namespace Dreamflykit.Application.CountryKits.Interfaces;

public interface ICountryKitsHubService
{
    Task<CountryKitsHubDto> GetAllProfilesAsync(CancellationToken ct = default);
    Task<CountryKitProfileDto> GetCountryProfileAsync(string countrySlug, CancellationToken ct = default);
    Task<bool> SaveCountryProfileAsync(CountryKitProfileDto profile, CancellationToken ct = default);
    Task<bool> DeleteCountryProfileAsync(string countrySlug, CancellationToken ct = default);
    Task<bool> ResetDefaultsAsync(string? countrySlug = null, CancellationToken ct = default);
}

