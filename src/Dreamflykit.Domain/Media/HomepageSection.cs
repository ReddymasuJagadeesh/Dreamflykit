using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Media;

/// <summary>
/// A configurable section of the homepage that can display managed media.
/// Each section is identified by a unique key. Admins can update images
/// without touching application code.
/// </summary>
public sealed class HomepageSection : AuditableEntity
{
    private HomepageSection()
    {
    }

    public HomepageSection(
        HomepageSectionKey sectionKey,
        string title,
        int displayOrder,
        Guid createdBy)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Section title is required.");
        }

        if (title.Length > 200)
        {
            throw new DomainException("Section title cannot exceed 200 characters.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException("Display order cannot be negative.");
        }

        if (createdBy == Guid.Empty)
        {
            throw new DomainException("Created by user ID is required.");
        }

        SectionKey = sectionKey;
        Title = title.Trim();
        DisplayOrder = displayOrder;
        CreatedBy = createdBy;
        IsActive = true;
        IsPublished = false;
    }

    // ----------------------------------------------------------------
    // Identity
    // ----------------------------------------------------------------

    /// <summary>
    /// Uniquely identifies this section on the homepage.
    /// Used by the customer website to load the correct section data.
    /// </summary>
    public HomepageSectionKey SectionKey { get; private set; }

    // ----------------------------------------------------------------
    // Media
    // ----------------------------------------------------------------

    /// <summary>Reference to the centralized MediaAsset for this section.</summary>
    public Guid? MediaAssetId { get; private set; }

    // ----------------------------------------------------------------
    // Content
    // ----------------------------------------------------------------

    public string Title { get; private set; } = string.Empty;

    public string? Subtitle { get; private set; }

    public string? LinkUrl { get; private set; }

    // ----------------------------------------------------------------
    // Display
    // ----------------------------------------------------------------

    public int DisplayOrder { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsPublished { get; private set; }

    // ----------------------------------------------------------------
    // Scheduling
    // ----------------------------------------------------------------

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
        string? subtitle,
        string? linkUrl,
        int displayOrder,
        Guid? mediaAssetId,
        DateTime? startDateUtc,
        DateTime? endDateUtc,
        Guid updatedBy)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Section title is required.");
        }

        if (title.Length > 200)
        {
            throw new DomainException("Section title cannot exceed 200 characters.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException("Display order cannot be negative.");
        }

        if (startDateUtc.HasValue && endDateUtc.HasValue
            && endDateUtc.Value <= startDateUtc.Value)
        {
            throw new DomainException("End date must be after start date.");
        }

        Title = title.Trim();
        Subtitle = subtitle?.Trim();
        LinkUrl = linkUrl?.Trim();
        DisplayOrder = displayOrder;
        MediaAssetId = mediaAssetId;
        StartDateUtc = startDateUtc;
        EndDateUtc = endDateUtc;
        UpdatedBy = updatedBy;
        MarkUpdated();
    }

    public void Publish(Guid publishedBy)
    {
        IsPublished = true;
        UpdatedBy = publishedBy;
        MarkUpdated();
    }

    public void Unpublish(Guid unpublishedBy)
    {
        IsPublished = false;
        UpdatedBy = unpublishedBy;
        MarkUpdated();
    }

    public void Activate()
    {
        IsActive = true;
        MarkUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkUpdated();
    }

    /// <summary>
    /// Returns true if this section should be visible to customers right now.
    /// </summary>
    public bool IsCurrentlyVisible()
    {
        if (!IsActive || !IsPublished)
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
