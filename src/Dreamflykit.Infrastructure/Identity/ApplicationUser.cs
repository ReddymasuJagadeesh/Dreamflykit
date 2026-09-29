using Microsoft.AspNetCore.Identity;

namespace Dreamflykit.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Country { get; set; }

    public string? ProfileImageUrl { get; set; }

    public bool IsActive { get; set; } = true;
}