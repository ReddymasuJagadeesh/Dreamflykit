using System.ComponentModel.DataAnnotations;

namespace Dreamflykit.Web.Models.Info;

public sealed class ContactFormViewModel
{
    [Required(ErrorMessage = "Your name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(120, ErrorMessage = "Email cannot exceed 120 characters.")]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select or enter a subject.")]
    [StringLength(150, ErrorMessage = "Subject cannot exceed 150 characters.")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Message is required.")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 2000 characters.")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Honeypot trap to block automated spambots.
    /// Real users will leave this blank.
    /// </summary>
    public string? WebsiteUrlHoneypot { get; set; }
}

public sealed class ConsultancyBookingViewModel
{
    [Required(ErrorMessage = "Student name is required.")]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "WhatsApp / Contact number is required.")]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select your destination country.")]
    public string DestinationCountry { get; set; } = string.Empty;

    [Required(ErrorMessage = "Target university / city is required.")]
    [StringLength(150)]
    public string TargetUniversity { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select your intake.")]
    public string IntakeTerm { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a preferred date for the call.")]
    [DataType(DataType.Date)]
    public DateTime? PreferredDate { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }

    public string? Honeypot { get; set; }
}
