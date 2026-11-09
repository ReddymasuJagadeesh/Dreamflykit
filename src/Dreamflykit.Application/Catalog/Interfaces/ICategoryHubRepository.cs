using Dreamflykit.Domain.Catalog;

namespace Dreamflykit.Application.Catalog.Interfaces;

/// <summary>
/// Repository abstraction for retrieving and persisting category hub landing configurations in PostgreSQL.
/// Eliminates split-brain file persistence by storing state directly in the relational database.
/// </summary>
public interface ICategoryHubRepository
{
    Task<CategoryHubConfiguration?> GetBySlugAsync(string hubSlug, CancellationToken ct = default);

    Task<T?> GetPayloadAsync<T>(string hubSlug, string fallbackFileName, CancellationToken ct = default) where T : class;

    Task SavePayloadAsync<T>(string hubSlug, string title, string fallbackFileName, T payload, CancellationToken ct = default) where T : class;

    Task<List<CategoryHubConfiguration>> GetAllActiveAsync(CancellationToken ct = default);
}

