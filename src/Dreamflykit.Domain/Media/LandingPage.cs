using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Media;

/// <summary>
/// A configurable landing page with hero media, sections, and CTA.
/// Images are managed through the Media module — no binaries stored here.
/// </summary>
public sealed class LandingPage : AuditableEntity
{
    private LandingPage()
    {
    }

    public LandingPage(
        string title,
        string slug,
        Guid createdBy)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Landing page title is required.");
        }

        if (title.Length > 200)
        {
            throw new DomainException("Landing page title cannot exceed 200 characters.");
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new DomainException("Landing page slug is required.");
        }

        if (createdBy == Guid.Empty)
        {
            throw new DomainException("Created by user ID is required.");
        }

        Title = title.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        CreatedBy = createdBy;
        Status = LandingPageStatus.Draft;
    }

    // ----------------------------------------------------------------
    // Content
    // ----------------------------------------------------------------

    public string Title { get; private set; } = string.Empty;

    public string Slug { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    // ----------------------------------------------------------------
    // Media
    // ----------------------------------------------------------------

    /// <summary>Hero section media asset reference.</summary>
    public Guid? HeroMediaAssetId { get; private set; }

    // ----------------------------------------------------------------
    // SEO
    // ----------------------------------------------------------------

    public string? MetaTitle { get; private set; }

    public string? MetaDescription { get; private set; }

    // ----------------------------------------------------------------
    // Scheduling & Status
    // ----------------------------------------------------------------

    public LandingPageStatus Status { get; private set; }

    public DateTime? StartDateUtc { get; private set; }

    public DateTime? EndDateUtc { get; private set; }

    // ----------------------------------------------------------------
    // Audit
    // ----------------------------------------------------------------

    public Guid CreatedBy { get; private set; }

    public Guid? UpdatedBy { get; private set; }

    // ----------------------------------------------------------------
    // Domain Methods
    // ----------------------------------------------------------------

    public void Update(
        string title,
        string slug,
        string? description,
        Guid? heroMediaAssetId,
        string? metaTitle,
        string? metaDescription,
        DateTime? startDateUtc,
        DateTime? endDateUtc,
        Guid updatedBy)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Landing page title is required.");
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new DomainException("Landing page slug is required.");
        }

        if (startDateUtc.HasValue && endDateUtc.HasValue
            && endDateUtc.Value <= startDateUtc.Value)
        {
            throw new DomainException("End date must be after start date.");
        }

        Title = title.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        Description = description?.Trim();
        HeroMediaAssetId = heroMediaAssetId;
        MetaTitle = metaTitle?.Trim();
        MetaDescription = metaDescription?.Trim();
        StartDateUtc = startDateUtc;
        EndDateUtc = endDateUtc;
        UpdatedBy = updatedBy;
        MarkUpdated();
    }

    public void Publish(Guid publishedBy)
    {
        if (Status == LandingPageStatus.Published)
        {
            return;
        }

        Status = LandingPageStatus.Published;
        UpdatedBy = publishedBy;
        MarkUpdated();
    }

    public void Archive(Guid archivedBy)
    {
        Status = LandingPageStatus.Archived;
        UpdatedBy = archivedBy;
        MarkUpdated();
    }

    public void Unpublish(Guid unpublishedBy)
    {
        Status = LandingPageStatus.Draft;
        UpdatedBy = unpublishedBy;
        MarkUpdated();
    }

    public bool IsCurrentlyActive()
    {
        if (Status != LandingPageStatus.Published)
        {
            return false;
        }

        var now = DateTime.UtcNow;

        if (StartDateUtc.HasValue && now < StartDateUtc.Value)
        {
            return false;
        }

        if (EndDateUtc.HasValue && now > EndDateUtc.Value)
        {
            return false;
        }

        return true;
    }
}

/// <summary>Lifecycle status of a landing page.</summary>
public enum LandingPageStatus
{
    Draft = 0,
    Published = 1,
    Archived = 2
}
