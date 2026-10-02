using Dreamflykit.Application.Addressing.Models;
using Dreamflykit.Domain.Addressing;
using System.ComponentModel.DataAnnotations;

namespace Dreamflykit.Web.Models.Addressing;

public sealed class AddressFormViewModel
{
    public Guid? AddressId { get; set; }

    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter a valid 10-digit mobile number.")]
    [Display(Name = "10-digit Mobile Number")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address (House No, Building, Street, Area) is required.")]
    [StringLength(200, ErrorMessage = "Address line 1 cannot exceed 200 characters.")]
    [Display(Name = "Address (House No, Building, Street, Area)")]
    public string AddressLine1 { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "Address line 2 cannot exceed 200 characters.")]
    [Display(Name = "Locality / Landmark (Optional)")]
    public string? AddressLine2 { get; set; }

    [Required(ErrorMessage = "City / District / Town is required.")]
    [StringLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
    [Display(Name = "City / District / Town")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "State is required.")]
    [StringLength(100, ErrorMessage = "State cannot exceed 100 characters.")]
    [Display(Name = "State")]
    public string State { get; set; } = string.Empty;

    [Required(ErrorMessage = "6-digit PIN code is required.")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "Please enter a valid 6-digit PIN code.")]
    [Display(Name = "PIN Code")]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Country is required.")]
    [StringLength(100)]
    public string Country { get; set; } = "India";

    [Display(Name = "Address Type")]
    public AddressType AddressType { get; set; } = AddressType.Shipping;

    [Display(Name = "Make this my default address")]
    public bool SetAsDefault { get; set; }

    public string? ReturnUrl { get; set; }
}

public sealed class AddressListViewModel
{
    public IReadOnlyList<AddressSummary> Addresses { get; set; } = Array.Empty<AddressSummary>();
    public string? ReturnUrl { get; set; }
}
