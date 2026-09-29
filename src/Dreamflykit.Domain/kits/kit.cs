using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Kits;

public sealed class Kit : AuditableEntity
{
    private Kit()
    {
    }

    public Kit(
        Guid countryId,
        Guid seasonId,
        string name,
        string slug,
        string description,
        KitSize size,
        decimal mrp,
        decimal sellingPrice,
        int displayOrder = 0)
    {
        Validate(
            countryId,
            seasonId,
            name,
            slug,
            description,
            size,
            mrp,
            sellingPrice,
            displayOrder);

        CountryId = countryId;
        SeasonId = seasonId;
        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        Description = description.Trim();
        Size = size;
        Mrp = mrp;
        SellingPrice = sellingPrice;
        DisplayOrder = displayOrder;

        IsActive = true;
        IsFeatured = false;
    }

    public Guid CountryId { get; private set; }

    public Guid SeasonId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Slug { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public KitSize Size { get; private set; }

    public decimal Mrp { get; private set; }

    public decimal SellingPrice { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsFeatured { get; private set; }

    public int DisplayOrder { get; private set; }

    public decimal DiscountAmount =>
        Mrp - SellingPrice;

    public decimal DiscountPercentage =>
        Mrp == 0
            ? 0
            : Math.Round(
                ((Mrp - SellingPrice) / Mrp) * 100,
                2);

    public void Update(
        Guid countryId,
        Guid seasonId,
        string name,
        string slug,
        string description,
        KitSize size,
        decimal mrp,
        decimal sellingPrice,
        int displayOrder)
    {
        Validate(
            countryId,
            seasonId,
            name,
            slug,
            description,
            size,
            mrp,
            sellingPrice,
            displayOrder);

        CountryId = countryId;
        SeasonId = seasonId;
        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        Description = description.Trim();
        Size = size;
        Mrp = mrp;
        SellingPrice = sellingPrice;
        DisplayOrder = displayOrder;

        MarkUpdated();
    }

    public void SetFeatured(bool isFeatured)
    {
        IsFeatured = isFeatured;
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

    private static void Validate(
        Guid countryId,
        Guid seasonId,
        string name,
        string slug,
        string description,
        KitSize size,
        decimal mrp,
        decimal sellingPrice,
        int displayOrder)
    {
        if (countryId == Guid.Empty)
            throw new DomainException(
                "Country ID is required.");

        if (seasonId == Guid.Empty)
            throw new DomainException(
                "Season ID is required.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(
                "Kit name is required.");

        if (string.IsNullOrWhiteSpace(slug))
            throw new DomainException(
                "Kit slug is required.");

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException(
                "Kit description is required.");

        if (!Enum.IsDefined(size))
            throw new DomainException(
                "Invalid kit size.");

        if (mrp <= 0)
            throw new DomainException(
                "Kit MRP must be greater than zero.");

        if (sellingPrice <= 0)
            throw new DomainException(
                "Kit selling price must be greater than zero.");

        if (sellingPrice > mrp)
            throw new DomainException(
                "Kit selling price cannot be greater than MRP.");

        if (displayOrder < 0)
            throw new DomainException(
                "Kit display order cannot be negative.");
    }
}