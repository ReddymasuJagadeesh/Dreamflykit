using Dreamflykit.Domain.Addressing;
using System.ComponentModel.DataAnnotations;

namespace Dreamflykit.Application.Addressing.Models;

/// <summary>Summary DTO returned to the UI.</summary>
public sealed class AddressSummary
{
    public Guid AddressId { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string AddressLine1 { get; init; } = string.Empty;
    public string? AddressLine2 { get; init; }
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public AddressType AddressType { get; init; }
    public bool IsDefault { get; init; }
    public string DisplayString { get; init; } = string.Empty;
}

/// <summary>Command used for both Add and Update operations.</summary>
public sealed class SaveAddressCommand
{
    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100)]
    public string FullName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone]
    public string Phone { get; init; } = string.Empty;

    [Required(ErrorMessage = "Address line 1 is required.")]
    [StringLength(250)]
    public string AddressLine1 { get; init; } = string.Empty;

    [StringLength(250)]
    public string? AddressLine2 { get; init; }

    [Required(ErrorMessage = "City is required.")]
    [StringLength(100)]
    public string City { get; init; } = string.Empty;

    [Required(ErrorMessage = "State / Province is required.")]
    [StringLength(100)]
    public string State { get; init; } = string.Empty;

    [Required(ErrorMessage = "Postal code is required.")]
    [StringLength(20)]
    public string PostalCode { get; init; } = string.Empty;

    [Required(ErrorMessage = "Country is required.")]
    [StringLength(100)]
    public string Country { get; init; } = string.Empty;

    public AddressType AddressType { get; init; } = AddressType.Shipping;

    public bool SetAsDefault { get; init; }
}
