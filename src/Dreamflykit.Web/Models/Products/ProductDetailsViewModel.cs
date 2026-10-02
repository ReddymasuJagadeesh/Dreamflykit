using Dreamflykit.Application.Catalog.Models;

namespace Dreamflykit.Web.Models.Products;

public sealed class ProductDetailsViewModel
{
    public Guid ProductId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string? ShortDescription { get; init; }

    public string CategoryName { get; init; } = string.Empty;

    public string CategorySlug { get; init; } = string.Empty;

    public string? SubcategoryName { get; init; }

    public string? SubcategorySlug { get; init; }

    public IReadOnlyList<ProductVariantViewModel> Variants { get; init; }
        = Array.Empty<ProductVariantViewModel>();

    public IReadOnlyList<ProductMediaViewModel> Media { get; init; }
        = Array.Empty<ProductMediaViewModel>();

    public IReadOnlyList<ProductAttributeViewModel> Attributes { get; init; }
        = Array.Empty<ProductAttributeViewModel>();

    public bool IsActive { get; init; }

    public bool IsFeatured { get; init; }
}

public sealed class ProductVariantViewModel
{
    public Guid VariantId { get; init; }

    public string Sku { get; init; } = string.Empty;

    public decimal Mrp { get; init; }

    public decimal SellingPrice { get; init; }

    public bool IsActive { get; init; }

    public bool IsInStock { get; init; }

    public decimal DiscountPercentage =>
        Mrp <= 0
            ? 0
            : Math.Round(
                ((Mrp - SellingPrice) / Mrp) * 100,
                2);
}

public sealed class ProductMediaViewModel
{
    public Guid MediaId { get; init; }

    public string Url { get; init; } = string.Empty;

    public string? AltText { get; init; }

    public int DisplayOrder { get; init; }

    public bool IsPrimary { get; init; }
}

public sealed class ProductAttributeViewModel
{
    public Guid AttributeId { get; init; }

    public string AttributeName { get; init; } = string.Empty;

    public string Value { get; init; } = string.Empty;
}