using Dreamflykit.Application.Catalog.Models;

namespace Dreamflykit.Web.Models.Products;

public sealed class ProductListViewModel
{
    public IReadOnlyList<ProductListItem> Products { get; init; }
        = Array.Empty<ProductListItem>();

    public int Page { get; init; }

    public int PageSize { get; init; }

    public int TotalCount { get; init; }

    public int TotalPages { get; init; }

    public bool HasPreviousPage { get; init; }

    public bool HasNextPage { get; init; }

    public string? SearchTerm { get; init; }

    public Guid? CategoryId { get; init; }

    public Guid? SubcategoryId { get; init; }

    public decimal? MinPrice { get; init; }

    public decimal? MaxPrice { get; init; }

    public ProductSortOption Sort { get; init; }
        = ProductSortOption.Relevance;

    public string? CategorySlug { get; init; }

    public string? CategoryName { get; init; }

    public string? CategoryImageUrl { get; init; }

    public string? CategoryDescription { get; init; }

    public string? CurrentSubcategoryName { get; init; }

    public string? CurrentSubcategorySlug { get; init; }

    public IReadOnlyList<Dreamflykit.Application.Categories.Models.SubcategoryListItem> Subcategories { get; init; }
        = Array.Empty<Dreamflykit.Application.Categories.Models.SubcategoryListItem>();

    public bool? InStockOnly { get; init; }

    public int? MinDiscount { get; init; }

    public IReadOnlyList<CategoryFacet> Categories { get; init; }
        = Array.Empty<CategoryFacet>();

    public IReadOnlyList<PriceFacetRange> PriceRanges { get; init; }
        = Array.Empty<PriceFacetRange>();

    public IReadOnlyList<DiscountFacet> Discounts { get; init; }
        = Array.Empty<DiscountFacet>();

    public Dreamflykit.Application.StudyHub.Models.StudyHubDto? StudyHub { get; init; }

    public Dreamflykit.Application.FashionHub.Models.FashionHubDto? FashionHub { get; init; }

    public Dreamflykit.Application.HealthHub.Models.HealthHubDto? HealthHub { get; init; }

    public Dreamflykit.Application.MobilesHub.Models.MobilesHubDto? MobilesHub { get; init; }

    public Dreamflykit.Application.AppliancesHub.Models.AppliancesHubDto? AppliancesHub { get; init; }
    public Dreamflykit.Application.BeautyHub.Models.BeautyHubDto? BeautyHub { get; init; }

    public string? ViewMode { get; init; }

    public bool IsStudyEssentialsHub =>
        string.Equals(CategorySlug, "study-essentials", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "study_essentials", StringComparison.OrdinalIgnoreCase);

    public bool IsMenClothing =>
        string.Equals(ViewMode, "men-clothing", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "men-clothing", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "men_clothing", StringComparison.OrdinalIgnoreCase) ||
        (string.Equals(CategorySlug, "fashion", StringComparison.OrdinalIgnoreCase) &&
         (string.Equals(CurrentSubcategorySlug, "men-clothing", StringComparison.OrdinalIgnoreCase) ||
          string.Equals(CurrentSubcategorySlug, "men_clothing", StringComparison.OrdinalIgnoreCase)));

    public bool IsWomenClothing =>
        string.Equals(ViewMode, "women-clothing", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "women-clothing", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "women_clothing", StringComparison.OrdinalIgnoreCase) ||
        (string.Equals(CategorySlug, "fashion", StringComparison.OrdinalIgnoreCase) &&
         (string.Equals(CurrentSubcategorySlug, "women-clothing", StringComparison.OrdinalIgnoreCase) ||
          string.Equals(CurrentSubcategorySlug, "women_clothing", StringComparison.OrdinalIgnoreCase)));

    public bool IsHoodiesHub =>
        string.Equals(ViewMode, "hoodies", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "hoodies", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "hoodie", StringComparison.OrdinalIgnoreCase) ||
        (string.Equals(CategorySlug, "fashion", StringComparison.OrdinalIgnoreCase) &&
         (string.Equals(CurrentSubcategorySlug, "hoodies", StringComparison.OrdinalIgnoreCase) ||
          string.Equals(CurrentSubcategorySlug, "hoodie", StringComparison.OrdinalIgnoreCase) ||
          string.Equals(CurrentSubcategorySlug, "hoodies-sweatshirts", StringComparison.OrdinalIgnoreCase) ||
          string.Equals(CurrentSubcategorySlug, "mens-hoodies", StringComparison.OrdinalIgnoreCase)));

    public bool IsSweatshirtsHub =>
        string.Equals(ViewMode, "sweatshirts", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "sweatshirts", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "sweatshirt", StringComparison.OrdinalIgnoreCase) ||
        (string.Equals(CategorySlug, "fashion", StringComparison.OrdinalIgnoreCase) &&
         (string.Equals(CurrentSubcategorySlug, "sweatshirts", StringComparison.OrdinalIgnoreCase) ||
          string.Equals(CurrentSubcategorySlug, "sweatshirt", StringComparison.OrdinalIgnoreCase) ||
          string.Equals(CurrentSubcategorySlug, "mens-sweatshirts", StringComparison.OrdinalIgnoreCase) ||
          string.Equals(CurrentSubcategorySlug, "mens-sweatshirt", StringComparison.OrdinalIgnoreCase)));

    public bool IsEthnicWearHub =>
        string.Equals(ViewMode, "ethnic-wear", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "ethnic-wear", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "ethnic", StringComparison.OrdinalIgnoreCase) ||
        (string.Equals(CategorySlug, "fashion", StringComparison.OrdinalIgnoreCase) &&
         (string.Equals(CurrentSubcategorySlug, "ethnic-wear", StringComparison.OrdinalIgnoreCase) ||
          string.Equals(CurrentSubcategorySlug, "ethnic", StringComparison.OrdinalIgnoreCase)));

    public bool IsThermalsHub =>
        string.Equals(ViewMode, "thermals", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "thermals", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "thermal", StringComparison.OrdinalIgnoreCase) ||
        (string.Equals(CategorySlug, "fashion", StringComparison.OrdinalIgnoreCase) &&
         (string.Equals(CurrentSubcategorySlug, "thermals", StringComparison.OrdinalIgnoreCase) ||
          string.Equals(CurrentSubcategorySlug, "thermal", StringComparison.OrdinalIgnoreCase)));

    public bool IsFashionHub =>
        string.Equals(CategorySlug, "fashion", StringComparison.OrdinalIgnoreCase) &&
        !IsMenClothing && !IsWomenClothing && !IsHoodiesHub && !IsSweatshirtsHub && !IsEthnicWearHub && !IsThermalsHub;

    public bool IsHealthCareHub =>
        string.Equals(CategorySlug, "health-care", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "healthcare", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "health_care", StringComparison.OrdinalIgnoreCase);

    public bool IsMobilesHub =>
        string.Equals(CategorySlug, "mobiles", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "mobile", StringComparison.OrdinalIgnoreCase);

    public bool IsAppliancesHub =>
        string.Equals(CategorySlug, "appliances", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "appliance", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "kitchen-appliances", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "kitchen_appliances", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "tv-and-appliances", StringComparison.OrdinalIgnoreCase);

    public bool IsElectronicsCategory =>
        string.Equals(CategorySlug, "electronics", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "electronic", StringComparison.OrdinalIgnoreCase);

    public bool IsBeautyHub =>
        string.Equals(CategorySlug, "beauty", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "beauty-and-personal-care", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "beauty-personal-care", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(CategorySlug, "bpc", StringComparison.OrdinalIgnoreCase);

    public bool HasActiveFilters =>
        !string.IsNullOrWhiteSpace(SearchTerm) ||
        SubcategoryId.HasValue ||
        MinPrice.HasValue ||
        MaxPrice.HasValue ||
        (MinDiscount.HasValue && MinDiscount.Value > 0) ||
        (InStockOnly.HasValue && InStockOnly.Value);
}