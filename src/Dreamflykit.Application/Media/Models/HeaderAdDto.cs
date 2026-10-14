namespace Dreamflykit.Application.Media.Models;

public sealed class HeaderAdDto
{
    public int SlotNumber { get; init; } = 1;
    public string Title { get; init; } = string.Empty;
    public string? Subtitle { get; init; }
    public string? BadgeText { get; init; }
    public string? ImageUrl { get; init; }
    public string RedirectUrl { get; init; } = "/SpecialOffer";
    public bool OpenInNewTab { get; init; }
    public string DisplayMode { get; init; } = "Hybrid"; // "Hybrid", "ImageOnly", "TextOnly"
    public bool IsActive { get; init; }
}
