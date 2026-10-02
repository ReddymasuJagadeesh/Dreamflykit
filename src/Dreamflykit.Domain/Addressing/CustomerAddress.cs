using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Addressing;

/// <summary>
/// Type of address. Shipping = where goods are sent.
/// Billing = for invoice/GST purposes.
/// </summary>
public enum AddressType
{
    Shipping = 1,
    Billing = 2
}

/// <summary>
/// A saved address in a customer's address book.
/// A customer may have many addresses but only one default per type.
/// All validation is enforced here — no controller may bypass it.
/// </summary>
public sealed class CustomerAddress : AuditableEntity
{
    // EF Core private constructor
    private CustomerAddress()
    {
    }

    public CustomerAddress(
        Guid userId,
        string fullName,
        string phone,
        string addressLine1,
        string? addressLine2,
        string city,
        string state,
        string postalCode,
        string country,
        AddressType addressType)
    {
        Validate(fullName, phone, addressLine1, city, state, postalCode, country);

        UserId = userId;
        FullName = fullName.Trim();
        Phone = phone.Trim();
        AddressLine1 = addressLine1.Trim();
        AddressLine2 = addressLine2?.Trim();
        City = city.Trim();
        State = state.Trim();
        PostalCode = postalCode.Trim();
        Country = country.Trim();
        AddressType = addressType;
        IsDefault = false;
    }

    // ----------------------------------------------------------
    // Properties
    // ----------------------------------------------------------

    public Guid UserId { get; private set; }

    public string FullName { get; private set; } = string.Empty;

    public string Phone { get; private set; } = string.Empty;

    public string AddressLine1 { get; private set; } = string.Empty;

    public string? AddressLine2 { get; private set; }

    public string City { get; private set; } = string.Empty;

    public string State { get; private set; } = string.Empty;

    public string PostalCode { get; private set; } = string.Empty;

    public string Country { get; private set; } = string.Empty;

    public AddressType AddressType { get; private set; }

    /// <summary>Whether this is the customer's default for this address type.</summary>
    public bool IsDefault { get; private set; }

    // ----------------------------------------------------------
    // Domain operations
    // ----------------------------------------------------------

    public void Update(
        string fullName,
        string phone,
        string addressLine1,
        string? addressLine2,
        string city,
        string state,
        string postalCode,
        string country)
    {
        Validate(fullName, phone, addressLine1, city, state, postalCode, country);

        FullName = fullName.Trim();
        Phone = phone.Trim();
        AddressLine1 = addressLine1.Trim();
        AddressLine2 = addressLine2?.Trim();
        City = city.Trim();
        State = state.Trim();
        PostalCode = postalCode.Trim();
        Country = country.Trim();

        MarkUpdated();
    }

    public void SetAsDefault()
    {
        IsDefault = true;
        MarkUpdated();
    }

    public void ClearDefault()
    {
        IsDefault = false;
        MarkUpdated();
    }

    // ----------------------------------------------------------
    // Helpers
    // ----------------------------------------------------------

    /// <summary>Returns a single-line formatted address string.</summary>
    public string ToDisplayString() =>
        string.Join(", ",
            new[] { AddressLine1, AddressLine2, City, State, PostalCode, Country }
            .Where(s => !string.IsNullOrWhiteSpace(s)));

    private static void Validate(
        string fullName,
        string phone,
        string addressLine1,
        string city,
        string state,
        string postalCode,
        string country)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new DomainException("Full name is required.");

        if (string.IsNullOrWhiteSpace(phone))
            throw new DomainException("Phone number is required.");

        if (string.IsNullOrWhiteSpace(addressLine1))
            throw new DomainException("Address line 1 is required.");

        if (string.IsNullOrWhiteSpace(city))
            throw new DomainException("City is required.");

        if (string.IsNullOrWhiteSpace(state))
            throw new DomainException("State / Province is required.");

        if (string.IsNullOrWhiteSpace(postalCode))
            throw new DomainException("Postal code is required.");

        if (string.IsNullOrWhiteSpace(country))
            throw new DomainException("Country is required.");
    }
}
