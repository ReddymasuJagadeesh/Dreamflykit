using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dreamflykit.Web.Models.Kits;

public sealed class FindMyKitViewModel
{
    [Required]
    [Display(Name = "Name")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "Please enter your name.")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Destination Country")]
    public Guid? DestinationCountryId { get; set; }

    [Required]
    [Display(Name = "Arrival Date")]
    [DataType(DataType.Date)]
    public DateOnly? ArrivalDate { get; set; }

    [Required]
    [Display(Name = "Phone Number")]
    [Phone]
    [StringLength(
        20,
        ErrorMessage = "Please enter a valid phone number.")]
    public string Phone { get; set; } = string.Empty;

    public IReadOnlyList<SelectListItem> Countries { get; set; }
        = Array.Empty<SelectListItem>();
}