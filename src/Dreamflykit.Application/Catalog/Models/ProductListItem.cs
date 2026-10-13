namespace Dreamflykit.Application.Catalog.Models;

public sealed class ProductListItem
{
    public Guid ProductId { get; init; }

    public Guid DefaultVariantId { get; init; }

    public Guid CategoryId { get; init; }

    public Guid? SubcategoryId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string? ImageUrl { get; init; }

    public string? PrimaryImageUrl { get; init; }

    public decimal MRP { get; init; }

    public decimal Mrp => MRP;

    public decimal SellingPrice { get; init; }

    public decimal DiscountAmount =>
        MRP - SellingPrice;

    public decimal DiscountPercentage =>
        MRP <= 0
            ? 0
            : Math.Round(
                ((MRP - SellingPrice) / MRP) * 100,
                2);

    public bool IsActive { get; init; }

    public bool IsFeatured { get; init; }

    public bool IsPromoted { get; init; }

    public Dreamflykit.Domain.Catalog.ProductApprovalStatus ApprovalStatus { get; init; } = Dreamflykit.Domain.Catalog.ProductApprovalStatus.Approved;

    public Guid? SellerId { get; init; }

    public bool IsInStock { get; init; }
}