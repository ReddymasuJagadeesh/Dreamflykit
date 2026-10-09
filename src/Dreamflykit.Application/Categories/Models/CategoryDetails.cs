namespace Dreamflykit.Application.Categories.Models;

public sealed class CategoryDetails
{
    public Guid CategoryId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string? ImageUrl { get; init; }

    public IReadOnlyList<SubcategoryListItem> Subcategories { get; init; }
        = Array.Empty<SubcategoryListItem>();
}