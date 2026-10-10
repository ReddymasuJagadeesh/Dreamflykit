namespace Dreamflykit.Application.Media.Models;

public sealed class HeaderAdDto
{
    public int SlotNumber { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Subtitle { get; init; }
    public string? ImageUrl { get; init; }
    public string RedirectUrl { get; init; } = "#";
    public bool IsActive { get; init; }
}
