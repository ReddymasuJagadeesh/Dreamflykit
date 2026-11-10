using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Catalog;

/// <summary>
/// Persistent relational configuration for Category Hub landing pages (Fashion, Mobiles, Electronics, etc.).
/// Guarantees durability across Docker container restarts by persisting in PostgreSQL rather than flat files.
/// </summary>
public sealed class CategoryHubConfiguration : AuditableEntity
{
    private CategoryHubConfiguration()
    {
    }

    public CategoryHubConfiguration(
        string hubSlug,
        string title,
        string? subtitle,
        string? heroBadge,
        string? heroBannerUrl,
        string? heroActionText,
        string? heroActionUrl,
        string configPayloadJson,
        bool isActive = true)
    {
        if (string.IsNullOrWhiteSpace(hubSlug))
        {
            throw new DomainException("Hub slug is required.");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Hub title is required.");
        }

        HubSlug = hubSlug.Trim().ToLowerInvariant();
        Title = title.Trim();
        Subtitle = subtitle?.Trim();
        HeroBadge = heroBadge?.Trim();
        HeroBannerUrl = heroBannerUrl?.Trim();
        HeroActionText = heroActionText?.Trim();
        HeroActionUrl = heroActionUrl?.Trim();
        ConfigPayloadJson = configPayloadJson ?? "{}";
        IsActive = isActive;
    }

    public string HubSlug { get; private set; } = string.Empty;

    public string Title { get; private set; } = string.Empty;

    public string? Subtitle { get; private set; }

    public string? HeroBadge { get; private set; }

    public string? HeroBannerUrl { get; private set; }

    public string? HeroActionText { get; private set; }

    public string? HeroActionUrl { get; private set; }

    /// <summary>
    /// Stored as JSONB in PostgreSQL. Contains full hub model, subcategories, sliders, and featured cards.
    /// </summary>
    public string ConfigPayloadJson { get; private set; } = "{}";

    public bool IsActive { get; private set; }

    public void UpdateContent(
        string title,
        string? subtitle,
        string? heroBadge,
        string? heroBannerUrl,
        string? heroActionText,
        string? heroActionUrl,
        string configPayloadJson,
        bool isActive = true)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Hub title is required.");
        }

        Title = title.Trim();
        Subtitle = subtitle?.Trim();
        HeroBadge = heroBadge?.Trim();
        HeroBannerUrl = heroBannerUrl?.Trim();
        HeroActionText = heroActionText?.Trim();
        HeroActionUrl = heroActionUrl?.Trim();
        ConfigPayloadJson = configPayloadJson ?? "{}";
        IsActive = isActive;
        MarkUpdated();
    }
}

