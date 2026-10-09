namespace Dreamflykit.Web.Models.Categories;

public sealed class CategoryDetailsViewModel
{
    public Guid CategoryId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string? ImageUrl { get; init; }

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

    public string? ImageUrl { get; init; }
}

public sealed class SubcategoryDetailsViewModel
{
    public Guid SubcategoryId { get; init; }

    public Guid CategoryId { get; init; }

    public string CategoryName { get; init; } = string.Empty;

    public string CategorySlug { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string? ImageUrl { get; init; }

    public int ProductCount { get; init; }

    public IReadOnlyList<Dreamflykit.Application.Catalog.Models.ProductListItem> Products { get; init; }
        = Array.Empty<Dreamflykit.Application.Catalog.Models.ProductListItem>();
}