namespace Dreamflykit.Domain.Orders;

/// <summary>
/// An immutable snapshot of a delivery/billing address captured at order
/// placement time. Changing the customer's address book later must never
/// alter a historical order's address.
/// Stored as owned entity (no separate table FK) within the Orders table.
/// </summary>
public sealed class OrderAddress
{
    // EF Core private constructor
    private OrderAddress()
    {
    }

    public OrderAddress(
        string fullName,
        string phone,
        string addressLine1,
        string? addressLine2,
        string city,
        string state,
        string postalCode,
        string country)
    {
        FullName = fullName;
        Phone = phone;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

    public string FullName { get; private set; } = string.Empty;

    public string Phone { get; private set; } = string.Empty;

    public string AddressLine1 { get; private set; } = string.Empty;

    public string? AddressLine2 { get; private set; }

    public string City { get; private set; } = string.Empty;

    public string State { get; private set; } = string.Empty;

    public string PostalCode { get; private set; } = string.Empty;

    public string Country { get; private set; } = string.Empty;

    /// <summary>Returns a formatted multi-line address string.</summary>
    public string ToDisplayString() =>
        string.Join(", ",
            new[] { AddressLine1, AddressLine2, City, State, PostalCode, Country }
            .Where(s => !string.IsNullOrWhiteSpace(s)));
}
