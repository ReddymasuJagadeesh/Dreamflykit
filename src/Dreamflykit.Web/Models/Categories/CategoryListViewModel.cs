namespace Dreamflykit.Web.Models.Categories;

public sealed class CategoryListViewModel
{
    public IReadOnlyList<CategoryListItemViewModel> Categories { get; init; }
        = Array.Empty<CategoryListItemViewModel>();
}

public sealed class CategoryListItemViewModel
{
    public Guid CategoryId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public int DisplayOrder { get; init; }

    public int ProductCount { get; init; }

    public string? ImageUrl { get; init; }
}