using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Inventory;

public sealed class Warehouse : AuditableEntity
{
    private Warehouse()
    {
    }

    public Warehouse(
        string name,
        string code,
        string addressLine1,
        string city,
        string state,
        string postalCode,
        string country)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Warehouse name is required.");
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new DomainException(
                "Warehouse code is required.");
        }

        if (string.IsNullOrWhiteSpace(addressLine1))
        {
            throw new DomainException(
                "Warehouse address is required.");
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            throw new DomainException(
                "Warehouse city is required.");
        }

        if (string.IsNullOrWhiteSpace(state))
        {
            throw new DomainException(
                "Warehouse state is required.");
        }

        if (string.IsNullOrWhiteSpace(postalCode))
        {
            throw new DomainException(
                "Warehouse postal code is required.");
        }

        if (string.IsNullOrWhiteSpace(country))
        {
            throw new DomainException(
                "Warehouse country is required.");
        }

        Name = name.Trim();
        Code = code.Trim().ToUpperInvariant();
        AddressLine1 = addressLine1.Trim();
        City = city.Trim();
        State = state.Trim();
        PostalCode = postalCode.Trim();
        Country = country.Trim();
        IsActive = true;
    }

    public string Name { get; private set; } =
        string.Empty;

    public string Code { get; private set; } =
        string.Empty;

    public string AddressLine1 { get; private set; } =
        string.Empty;

    public string? AddressLine2 { get; private set; }

    public string City { get; private set; } =
        string.Empty;

    public string State { get; private set; } =
        string.Empty;

    public string PostalCode { get; private set; } =
        string.Empty;

    public string Country { get; private set; } =
        string.Empty;

    public bool IsActive { get; private set; }

    public void UpdateAddress(
        string addressLine1,
        string? addressLine2,
        string city,
        string state,
        string postalCode,
        string country)
    {
        if (string.IsNullOrWhiteSpace(addressLine1))
        {
            throw new DomainException(
                "Warehouse address is required.");
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            throw new DomainException(
                "Warehouse city is required.");
        }

        if (string.IsNullOrWhiteSpace(state))
        {
            throw new DomainException(
                "Warehouse state is required.");
        }

        if (string.IsNullOrWhiteSpace(postalCode))
        {
            throw new DomainException(
                "Warehouse postal code is required.");
        }

        if (string.IsNullOrWhiteSpace(country))
        {
            throw new DomainException(
                "Warehouse country is required.");
        }

        AddressLine1 = addressLine1.Trim();
        AddressLine2 = addressLine2?.Trim();
        City = city.Trim();
        State = state.Trim();
        PostalCode = postalCode.Trim();
        Country = country.Trim();

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