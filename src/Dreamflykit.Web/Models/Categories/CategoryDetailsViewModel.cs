namespace Dreamflykit.Web.Models.Categories;

public sealed class CategoryDetailsViewModel
{
    public Guid CategoryId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public IReadOnlyList<SubcategoryListItemViewModel> Subcategories { get; init; }
        = Array.Empty<SubcategoryListItemViewModel>();
}

public sealed class SubcategoryListItemViewModel
{
    public Guid SubcategoryId { get; init; }

    public Guid CategoryId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public int DisplayOrder { get; init; }

    public int ProductCount { get; init; }
}