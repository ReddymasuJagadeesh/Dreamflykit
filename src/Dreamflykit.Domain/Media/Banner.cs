using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Media;

/// <summary>
/// A promotional or campaign banner with associated media, scheduling, and call-to-action.
/// </summary>
public sealed class Banner : AuditableEntity
{
    private Banner()
    {
    }

    public Banner(
        string title,
        Guid? mediaAssetId,
        int displayOrder,
        Guid createdBy)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Banner title is required.");
        }

        if (title.Length > 200)
        {
            throw new DomainException("Banner title cannot exceed 200 characters.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException("Display order cannot be negative.");
        }

        if (createdBy == Guid.Empty)
        {
            throw new DomainException("Created by user ID is required.");
        }

        Title = title.Trim();
        MediaAssetId = mediaAssetId;
        DisplayOrder = displayOrder;
        CreatedBy = createdBy;
        IsActive = true;
        IsPublished = false;
    }

    // ----------------------------------------------------------------
    // Media
    // ----------------------------------------------------------------

    /// <summary>Reference to the centralized MediaAsset.</summary>
    public Guid? MediaAssetId { get; private set; }

    // ----------------------------------------------------------------
    // Content
    // ----------------------------------------------------------------

    public string Title { get; private set; } = string.Empty;

    public string? Subtitle { get; private set; }

    public string? CtaText { get; private set; }

    public string? CtaUrl { get; private set; }

    // ----------------------------------------------------------------
    // Display
    // ----------------------------------------------------------------

    public int DisplayOrder { get; private set; }

    // ----------------------------------------------------------------
    // Scheduling & Publishing
    // ----------------------------------------------------------------

    public DateTime? StartDateUtc { get; private set; }

    public DateTime? EndDateUtc { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsPublished { get; private set; }

    // ----------------------------------------------------------------
    // Audit
    // ----------------------------------------------------------------

    public Guid CreatedBy { get; private set; }

    public Guid? UpdatedBy { get; private set; }

    // ----------------------------------------------------------------
    // Domain Methods
    // ----------------------------------------------------------------

    /// <summary>
    /// Returns true if the banner should be displayed right now
    /// based on its publish state and scheduling dates.
    /// </summary>
    public bool IsCurrentlyActive()
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

    public void Update(
        string title,
        string? subtitle,
        string? ctaText,
        string? ctaUrl,
        int displayOrder,
        DateTime? startDateUtc,
        DateTime? endDateUtc,
        Guid? mediaAssetId,
        Guid updatedBy)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Banner title is required.");
        }

        if (title.Length > 200)
        {
            throw new DomainException("Banner title cannot exceed 200 characters.");
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
        CtaText = ctaText?.Trim();
        CtaUrl = ctaUrl?.Trim();
        DisplayOrder = displayOrder;
        StartDateUtc = startDateUtc;
        EndDateUtc = endDateUtc;
        MediaAssetId = mediaAssetId;
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
}
