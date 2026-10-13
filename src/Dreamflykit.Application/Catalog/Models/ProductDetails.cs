namespace Dreamflykit.Application.Catalog.Models;

public sealed class ProductDetails
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

    public IReadOnlyList<ProductVariantItem> Variants { get; init; }
        = Array.Empty<ProductVariantItem>();

    public IReadOnlyList<ProductMediaItem> Media { get; init; }
        = Array.Empty<ProductMediaItem>();

    public IReadOnlyList<ProductAttributeItem> Attributes { get; init; }
        = Array.Empty<ProductAttributeItem>();

    public bool IsActive { get; init; }

    public bool IsFeatured { get; init; }

    public bool IsPromoted { get; init; }

    public Dreamflykit.Domain.Catalog.ProductApprovalStatus ApprovalStatus { get; init; } = Dreamflykit.Domain.Catalog.ProductApprovalStatus.Approved;

    public Guid? SellerId { get; init; }
}

public sealed class ProductVariantItem
{
    public Guid VariantId { get; init; }

    public string Sku { get; init; } = string.Empty;

    public decimal Mrp { get; init; }

    public decimal SellingPrice { get; init; }

    public bool IsActive { get; init; }

    public bool IsInStock { get; init; }
}

public sealed class ProductMediaItem
{
    public Guid MediaId { get; init; }

    public string Url { get; init; } = string.Empty;

    public string? ThumbnailUrl { get; init; }

    public string? CardUrl { get; init; }

    public string? AltText { get; init; }

    public int DisplayOrder { get; init; }

    public bool IsPrimary { get; init; }
}

public sealed class ProductAttributeItem
{
    public Guid AttributeId { get; init; }

    public string AttributeName { get; init; } = string.Empty;

    public string Value { get; init; } = string.Empty;
}