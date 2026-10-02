namespace Dreamflykit.Application.Categories.Models;

public sealed class SubcategoryListItem
{
    public Guid SubcategoryId { get; init; }

    public Guid CategoryId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public int DisplayOrder { get; init; }

    public int ProductCount { get; init; }
}