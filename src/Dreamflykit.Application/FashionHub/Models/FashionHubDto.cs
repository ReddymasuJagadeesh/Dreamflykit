namespace Dreamflykit.Application.FashionHub.Models;

public sealed class FashionHubDto
{
    public FashionHeroBannerDto HeroBanner { get; set; } = new();
    public FashionCategoriesSectionDto CategoriesSection { get; set; } = new();
    public FashionTopPicksSectionDto TopPicksSection { get; set; } = new();
    public FashionTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();
    public FashionBottomBannerDto BottomBanner { get; set; } = new();

    public static FashionHubDto CreateDefault()
    {
        return new FashionHubDto
        {
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Fashion for",
                TitleHighlight = "Every Journey",
                Subtitle = "Stylish, comfortable & travel-ready fashion for students and explorers.",
                ImageUrl = "/images/fashion-hub/fashion_hero_banner.jpg",
                IsUploadingSoon = false,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "b1", Label = "Trendy & Comfortable", Icon = "shirt" },
                    new() { Id = "b2", Label = "Premium Quality", Icon = "shield" },
                    new() { Id = "b3", Label = "Affordable Prices", Icon = "tag" },
                    new() { Id = "b4", Label = "Loved by 10K+ Students", Icon = "users" }
                },
                RightCard = new FashionHeroRightCardDto
                {
                    Title = "Look Good.",
                    TitleHighlight = "Travel Smart.",
                    Features = new List<FashionHeroFeatureDto>
                    {
                        new() { Id = "f1", Title = "Travel Friendly", Icon = "luggage" },
                        new() { Id = "f2", Title = "Lightweight & Easy", Icon = "feather" },
                        new() { Id = "f3", Title = "All-Season Wear", Icon = "grid" }
                    }
                }
            },
            CategoriesSection = new FashionCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Categories →",
                ViewAllUrl = "/products?category=fashion",
                Categories = new List<FashionCategoryItemDto>
                {
                    new() { Id = "c1", Name = "Men Clothing", Slug = "men-clothing", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },
                    new() { Id = "c2", Name = "Women Clothing", Slug = "women-clothing", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },
                    new() { Id = "c3", Name = "Men Footwear", Slug = "men-footwear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },
                    new() { Id = "c4", Name = "Women Footwear", Slug = "women-footwear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },
                    new() { Id = "c5", Name = "Watches", Slug = "watches", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true },
                    new() { Id = "c6", Name = "Sunglasses", Slug = "sunglasses", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true },
                    new() { Id = "c7", Name = "Caps & Hats", Slug = "caps-hats", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 7, IsUploadingSoon = true },
                    new() { Id = "c8", Name = "Belts & Wallets", Slug = "belts-wallets", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 8, IsUploadingSoon = true },
                    new() { Id = "c9", Name = "Socks", Slug = "socks", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 9, IsUploadingSoon = true },
                    new() { Id = "c10", Name = "Accessories", Slug = "accessories", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 10, IsUploadingSoon = true }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks for Men & Women",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/products?category=fashion",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "p1", Name = "Men's Hoodie", CategoryName = "Men Clothing", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 128, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing", IsUploadingSoon = true },
                    new() { Id = "p2", Name = "Men's Bomber Jacket", CategoryName = "Men Clothing", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 96, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing", IsUploadingSoon = true },
                    new() { Id = "p3", Name = "Men's Casual Shirt", CategoryName = "Men Clothing", Price = 749, OriginalPrice = 1499, Rating = 4.7m, ReviewsCount = 87, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing", IsUploadingSoon = true },
                    new() { Id = "p4", Name = "Men's Joggers", CategoryName = "Men Clothing", Price = 799, OriginalPrice = 1599, Rating = 4.8m, ReviewsCount = 76, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing", IsUploadingSoon = true },
                    new() { Id = "p5", Name = "Women's Sweatshirt", CategoryName = "Women Clothing", Price = 899, OriginalPrice = 1799, Rating = 4.9m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing", IsUploadingSoon = true },
                    new() { Id = "p6", Name = "Women's Leggings", CategoryName = "Women Clothing", Price = 599, OriginalPrice = 1199, Rating = 4.7m, ReviewsCount = 84, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing", IsUploadingSoon = true },
                    new() { Id = "p7", Name = "Women's Casual Dress", CategoryName = "Women Clothing", Price = 1099, OriginalPrice = 2199, Rating = 4.9m, ReviewsCount = 65, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing", IsUploadingSoon = true },
                    new() { Id = "p8", Name = "Women's Denim Jacket", CategoryName = "Women Clothing", Price = 1299, OriginalPrice = 2499, Rating = 4.8m, ReviewsCount = 78, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing", IsUploadingSoon = true }
                }
            },
            TrustBadgesSection = new FashionTrustBadgesSectionDto
            {
                IsEnabled = true,
                Badges = new List<FashionTrustBadgeDto>
                {
                    new() { Id = "t1", Title = "Curated for Students", Subtitle = "Handpicked fashion for every journey", Icon = "box" },
                    new() { Id = "t2", Title = "Trendy & Comfortable", Subtitle = "Stay stylish with all-day comfort", Icon = "shield" },
                    new() { Id = "t3", Title = "Affordable for Everyone", Subtitle = "Best value fashion for students", Icon = "tag" },
                    new() { Id = "t4", Title = "Fast & Reliable Delivery", Subtitle = "On-time delivery worldwide", Icon = "truck" },
                    new() { Id = "t5", Title = "Easy Returns", Subtitle = "Hassle-free returns within 7 days", Icon = "return" },
                    new() { Id = "t6", Title = "24/7 Customer Support", Subtitle = "We're always here to help you", Icon = "headset" }
                }
            },
            BottomBanner = new FashionBottomBannerDto
            {
                IsEnabled = true,
                QuizTitle = "Not Sure What You Need?",
                QuizSubtitle = "Take our quick quiz and find your perfect kit",
                QuizButtonText = "Find My Kit",
                QuizButtonUrl = "/find-my-kit",
                DestinationsTitle = "Kits for Top Destinations",
                DestinationsSubtitle = "USA, UK, Canada, Australia & More",
                BulkTitle = "Bulk Orders for Universities",
                BulkSubtitle = "Get special discounts for groups",
                BulkButtonText = "Contact Us",
                BulkButtonUrl = "/contact"
            }
        };
    }
}

public sealed class FashionHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Fashion for";
    public string TitleHighlight { get; set; } = "Every Journey";
    public string Subtitle { get; set; } = "Stylish, comfortable & travel-ready fashion for students and explorers.";
    public string? ImageUrl { get; set; } = "/images/fashion-hub/fashion_hero_banner.jpg";
    public bool IsUploadingSoon { get; set; } = false;

    // Custom Color and Appearance controls for Admin
    public string TitleColor { get; set; } = "#063f91";
    public string TitleHighlightColor { get; set; } = "#ea580c";
    public string SubtitleColor { get; set; } = "#334155";
    public string BadgeBgColor { get; set; } = "rgba(255, 255, 255, 0.94)";
    public string BadgeTextColor { get; set; } = "#0f172a";
    public string OverlayGradient { get; set; } = "linear-gradient(90deg, rgba(255, 255, 255, 0.94) 0%, rgba(255, 255, 255, 0.78) 42%, rgba(255, 255, 255, 0.2) 68%, rgba(255, 255, 255, 0.85) 100%)";
    public int BannerHeight { get; set; } = 420;

    public List<FashionHeroBadgeDto> Badges { get; set; } = new();
    public FashionHeroRightCardDto RightCard { get; set; } = new();
}

public sealed class FashionHeroBadgeDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Label { get; set; } = string.Empty;
    public string Icon { get; set; } = "check";
    public bool IsActive { get; set; } = true;
}

public sealed class FashionHeroRightCardDto
{
    public string Title { get; set; } = "Look Good.";
    public string TitleHighlight { get; set; } = "Travel Smart.";
    public List<FashionHeroFeatureDto> Features { get; set; } = new();
}

public sealed class FashionHeroFeatureDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = "star";
    public bool IsActive { get; set; } = true;
}

public sealed class FashionCategoriesSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Shop by Category";
    public string ViewAllText { get; set; } = "View All Categories →";
    public string ViewAllUrl { get; set; } = "/products?category=fashion";
    public List<FashionCategoryItemDto> Categories { get; set; } = new();
}

public sealed class FashionCategoryItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsUploadingSoon { get; set; } = true;
    public bool IsActive { get; set; } = true;
}

public sealed class FashionTopPicksSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Top Picks for Men & Women";
    public string ViewAllText { get; set; } = "View All Products →";
    public string ViewAllUrl { get; set; } = "/products?category=fashion";
    public List<FashionProductItemDto> Products { get; set; } = new();
}

public sealed class FashionProductItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public decimal Rating { get; set; } = 4.8m;
    public int ReviewsCount { get; set; } = 50;
    public string? ImageUrl { get; set; }
    public string ProductUrl { get; set; } = "/products?category=fashion";
    public bool IsUploadingSoon { get; set; } = true;
    public bool IsActive { get; set; } = true;
}

public sealed class FashionTrustBadgesSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public List<FashionTrustBadgeDto> Badges { get; set; } = new();
}

public sealed class FashionTrustBadgeDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Icon { get; set; } = "check";
    public bool IsActive { get; set; } = true;
}

public sealed class FashionBottomBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string QuizTitle { get; set; } = "Not Sure What You Need?";
    public string QuizSubtitle { get; set; } = "Take our quick quiz and find your perfect kit";
    public string QuizButtonText { get; set; } = "Find My Kit";
    public string QuizButtonUrl { get; set; } = "/find-my-kit";
    public string DestinationsTitle { get; set; } = "Kits for Top Destinations";
    public string DestinationsSubtitle { get; set; } = "USA, UK, Canada, Australia & More";
    public string BulkTitle { get; set; } = "Bulk Orders for Universities";
    public string BulkSubtitle { get; set; } = "Get special discounts for groups";
    public string BulkButtonText { get; set; } = "Contact Us";
    public string BulkButtonUrl { get; set; } = "/contact";
}
