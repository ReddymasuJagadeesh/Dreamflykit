namespace Dreamflykit.Application.Theme;

public sealed record FontDefinition(
    string Name,
    string CssFamily,
    string? GoogleFontUrl,
    string Description,
    string Category = "Sans-Serif");

public static class TypographyCatalog
{
    public const string DefaultFontName = "Plus Jakarta Sans";
    public const double DefaultFontScale = 1.0;
    public const int DefaultBaseFontSizePx = 15;

    public static readonly IReadOnlyList<FontDefinition> SupportedFonts = new List<FontDefinition>
    {
        new("Plus Jakarta Sans",
            "'Plus Jakarta Sans', system-ui, -apple-system, sans-serif",
            "https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@400;500;600;700;800&display=swap",
            "Modern, crisp & clean e-commerce standard (Recommended Default)",
            "Geometric Sans"),

        new("Inter",
            "'Inter', system-ui, -apple-system, sans-serif",
            "https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&display=swap",
            "Neutral, highly legible, world-class interface font",
            "Neo-Grotesque"),

        new("Outfit",
            "'Outfit', system-ui, -apple-system, sans-serif",
            "https://fonts.googleapis.com/css2?family=Outfit:wght@400;500;600;700;800&display=swap",
            "Friendly geometric proportions with premium luxury feel",
            "Geometric Sans"),

        new("Roboto",
            "'Roboto', system-ui, -apple-system, sans-serif",
            "https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;700&display=swap",
            "Google classic, balanced natural reading rhythm",
            "Neo-Grotesque"),

        new("Poppins",
            "'Poppins', system-ui, -apple-system, sans-serif",
            "https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600;700&display=swap",
            "Youthful, circular curves, popular in international travel & lifestyle",
            "Geometric Sans"),

        new("Montserrat",
            "'Montserrat', system-ui, -apple-system, sans-serif",
            "https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700;800&display=swap",
            "Editorial flair with strong visual personality",
            "Geometric Sans"),

        new("Open Sans",
            "'Open Sans', system-ui, -apple-system, sans-serif",
            "https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;500;600;700&display=swap",
            "Optimized for universal print and screen legibility",
            "Humanist Sans"),

        new("System UI",
            "system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif",
            null,
            "Native device OS system font (Fastest, zero webfont payload)",
            "System Native")
    };

    public static FontDefinition GetFontOrDefault(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return SupportedFonts[0];
        }

        return SupportedFonts.FirstOrDefault(f => string.Equals(f.Name, name.Trim(), StringComparison.OrdinalIgnoreCase))
               ?? SupportedFonts[0];
    }

    public static bool IsSupported(string? name)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;
        return SupportedFonts.Any(f => string.Equals(f.Name, name.Trim(), StringComparison.OrdinalIgnoreCase));
    }
}
