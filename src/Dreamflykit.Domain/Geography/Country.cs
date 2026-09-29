using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Geography;

public sealed class Country : AuditableEntity
{
    private Country()
    {
    }

    public Country(
        string name,
        string code,
        int displayOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Country name is required.");
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new DomainException(
                "Country code is required.");
        }

        if (code.Length > 10)
        {
            throw new DomainException(
                "Country code cannot exceed 10 characters.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Country display order cannot be negative.");
        }

        Name = name.Trim();
        Code = code.Trim().ToUpperInvariant();
        DisplayOrder = displayOrder;
        IsActive = true;
    }

    public string Name { get; private set; } = string.Empty;

    public string Code { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    public int DisplayOrder { get; private set; }

    public void Update(
        string name,
        string code,
        int displayOrder)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Country name is required.");
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new DomainException(
                "Country code is required.");
        }

        if (code.Length > 10)
        {
            throw new DomainException(
                "Country code cannot exceed 10 characters.");
        }

        if (displayOrder < 0)
        {
            throw new DomainException(
                "Country display order cannot be negative.");
        }

        Name = name.Trim();
        Code = code.Trim().ToUpperInvariant();
        DisplayOrder = displayOrder;

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