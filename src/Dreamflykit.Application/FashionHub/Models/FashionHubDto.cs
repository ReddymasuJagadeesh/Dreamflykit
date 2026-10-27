namespace Dreamflykit.Application.FashionHub.Models;

public sealed class FashionHubDto
{
    public FashionHeroBannerDto HeroBanner { get; set; } = new();
    public FashionCategoriesSectionDto CategoriesSection { get; set; } = new();
    public FashionTopPicksSectionDto TopPicksSection { get; set; } = new();
    public FashionTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();
    public FashionBottomBannerDto BottomBanner { get; set; } = new();
    public FashionMenClothingDto MenClothing { get; set; } = FashionMenClothingDto.CreateDefault();
    public FashionWomenClothingDto WomenClothing { get; set; } = FashionWomenClothingDto.CreateDefault();
    public FashionHoodiesHubDto Hoodies { get; set; } = FashionHoodiesHubDto.CreateDefault();
    public FashionSweatshirtsHubDto Sweatshirts { get; set; } = FashionSweatshirtsHubDto.CreateDefault();
    public FashionEthnicWearDto EthnicWear { get; set; } = FashionEthnicWearDto.CreateDefault();
    public FashionThermalsDto Thermals { get; set; } = FashionThermalsDto.CreateDefault();

    public static FashionHubDto CreateDefault()
    {
        return new FashionHubDto
        {
            MenClothing = FashionMenClothingDto.CreateDefault(),
            WomenClothing = FashionWomenClothingDto.CreateDefault(),
            Hoodies = FashionHoodiesHubDto.CreateDefault(),
            Sweatshirts = FashionSweatshirtsHubDto.CreateDefault(),
            EthnicWear = FashionEthnicWearDto.CreateDefault(),
            Thermals = FashionThermalsDto.CreateDefault(),
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Fashion for",
                TitleHighlight = "Every Journey",
                Subtitle = "Stylish, comfortable & travel-ready fashion for students and explorers.",
                ImageUrl = "/images/fashion-hub/fashion_hero_banner.jpg",
                IsUploadingSoon = false,
                SlideIntervalSeconds = 5,
                Slides = new List<FashionHeroSlideDto>
                {
                    new()
                    {
                        Id = "s1",
                        Title = "Fashion for",
                        TitleHighlight = "Every Journey",
                        Subtitle = "Stylish, comfortable & travel-ready fashion for students and explorers.",
                        ImageUrl = "/images/fashion-hub/fashion_hero_banner.jpg",
                        IsUploadingSoon = false,
                        ButtonText = "Shop Women Clothing",
                        ButtonUrl = "/category/fashion/women-clothing",
                        DisplayOrder = 1,
                        IsActive = true
                    },
                    new()
                    {
                        Id = "s2",
                        Title = "Premium Comfort",
                        TitleHighlight = "Hoodies & Sweatshirts",
                        Subtitle = "Engineered lightweight and cozy fits crafted for modern university life.",
                        ImageUrl = "/images/fashion-hub/hoodies_hero.jpg",
                        IsUploadingSoon = false,
                        ButtonText = "Explore Hoodies",
                        ButtonUrl = "/category/fashion/hoodies",
                        DisplayOrder = 2,
                        IsActive = true
                    },
                    new()
                    {
                        Id = "s3",
                        Title = "All-Weather Outfits",
                        TitleHighlight = "Men & Women Collection",
                        Subtitle = "Durable, easy-to-pack essentials curated with 100% genuine student trust.",
                        ImageUrl = "/images/fashion-hub/sweatshirts_hero.jpg",
                        IsUploadingSoon = false,
                        ButtonText = "Shop Sweatshirts",
                        ButtonUrl = "/category/fashion/sweatshirts",
                        DisplayOrder = 3,
                        IsActive = true
                    }
                },
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
                    new() { Id = "p1", Name = "Men's Hoodie", CategoryName = "Men Clothing", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 128, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/hoodies", IsUploadingSoon = true },
                    new() { Id = "p2", Name = "Men's Bomber Jacket", CategoryName = "Men Clothing", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 96, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing", IsUploadingSoon = true },
                    new() { Id = "p3", Name = "Men's Casual Shirt", CategoryName = "Men Clothing", Price = 749, OriginalPrice = 1499, Rating = 4.7m, ReviewsCount = 87, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing", IsUploadingSoon = true },
                    new() { Id = "p4", Name = "Men's Joggers", CategoryName = "Men Clothing", Price = 799, OriginalPrice = 1599, Rating = 4.8m, ReviewsCount = 76, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing", IsUploadingSoon = true },
                    new() { Id = "p5", Name = "Women's Sweatshirt", CategoryName = "Women Clothing", Price = 899, OriginalPrice = 1799, Rating = 4.9m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sweatshirts", IsUploadingSoon = true },
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
    public string? Description { get; set; }
    public string? ButtonText { get; set; }
    public string? ButtonUrl { get; set; }
    public string? ImageUrl { get; set; } = "/images/fashion-hub/fashion_hero_banner.jpg";
    public bool IsUploadingSoon { get; set; } = false;

    // Full Width Slides Carousel configuration
    public int SlideIntervalSeconds { get; set; } = 5;
    public List<FashionHeroSlideDto> Slides { get; set; } = new();

    // Custom Color and Appearance controls for Admin
    public string TitleColor { get; set; } = "#063f91";
    public string TitleHighlightColor { get; set; } = "#ea580c";
    public string SubtitleColor { get; set; } = "#334155";
    public string BadgeBgColor { get; set; } = "rgba(255, 255, 255, 0.94)";
    public string BadgeTextColor { get; set; } = "#0f172a";
    public string OverlayGradient { get; set; } = "linear-gradient(90deg, rgba(255, 255, 255, 0.94) 0%, rgba(255, 255, 255, 0.78) 42%, rgba(255, 255, 255, 0.2) 68%, rgba(255, 255, 255, 0.85) 100%)";
    public int BannerHeight { get; set; } = 440;

    public List<FashionHeroBadgeDto> Badges { get; set; } = new();
    public FashionHeroRightCardDto RightCard { get; set; } = new();
}

public sealed class FashionHeroSlideDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = "Fashion for";
    public string TitleHighlight { get; set; } = "Every Journey";
    public string Subtitle { get; set; } = "Stylish, comfortable & travel-ready fashion for students and explorers.";
    public string? ImageUrl { get; set; } = "/images/fashion-hub/fashion_hero_banner.jpg";
    public bool IsUploadingSoon { get; set; } = false;
    public string? ButtonText { get; set; } = "Shop Now";
    public string? ButtonUrl { get; set; } = "/products?category=fashion";
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
}

public sealed class FashionHeroBadgeDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Label { get; set; } = string.Empty;
    public string Title { get => Label; set => Label = value; }
    public string Subtitle { get; set; } = string.Empty;
    public string? Description { get; set; }
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

// -------------------------------------------------------------
// MEN CLOTHING DTO
// -------------------------------------------------------------
public sealed class FashionMenClothingDto
{
    public bool IsEnabled { get; set; } = true;
    public FashionHeroBannerDto HeroBanner { get; set; } = new();
    public FashionCategoriesSectionDto CategoriesSection { get; set; } = new();
    public FashionTopPicksSectionDto TopPicksSection { get; set; } = new();
    public FashionTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();
    public FashionBottomBannerDto BottomBanner { get; set; } = new();

    public static FashionMenClothingDto CreateDefault()
    {
        return new FashionMenClothingDto
        {
            IsEnabled = true,
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Men Clothing",
                TitleHighlight = "Wherever You Go.",
                Subtitle = "Style. Comfort.",
                Description = "Trendy, durable & travel-ready outfits for every student and explorer.",
                ImageUrl = "/images/fashion-hub/men_clothing_hero.jpg",
                IsUploadingSoon = false,
                TitleColor = "#063f91",
                TitleHighlightColor = "#ea580c",
                SubtitleColor = "#334155",
                BannerHeight = 440,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "mb1", Label = "Trendy Styles", Icon = "shirt" },
                    new() { Id = "mb2", Label = "Premium Quality", Icon = "shield" },
                    new() { Id = "mb3", Label = "Affordable Prices", Icon = "tag" },
                    new() { Id = "mb4", Label = "Loved by 10K+ Students", Icon = "users" }
                },
                RightCard = new FashionHeroRightCardDto
                {
                    Title = "Everyday",
                    TitleHighlight = "Comfort",
                    Features = new List<FashionHeroFeatureDto>
                    {
                        new() { Id = "mf1", Title = "Everyday Comfort", Icon = "shirt" },
                        new() { Id = "mf2", Title = "Easy to Pack", Icon = "luggage" },
                        new() { Id = "mf3", Title = "All-Season Wear", Icon = "clock" },
                        new() { Id = "mf4", Title = "Perfect for Travel", Icon = "plane" }
                    }
                }
            },
            CategoriesSection = new FashionCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Categories →",
                ViewAllUrl = "/category/fashion/men-clothing",
                Categories = new List<FashionCategoryItemDto>
                {
                    new() { Id = "mc1", Name = "1. Ethnic Wear", Slug = "ethnic-wear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = false },
                    new() { Id = "mc2", Name = "2. Thermals", Slug = "thermals", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = false },
                    new() { Id = "mc3", Name = "3. T-Shirts", Slug = "t-shirts", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = false },
                    new() { Id = "mc4", Name = "4. Shirts", Slug = "shirts", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = false },
                    new() { Id = "mc5", Name = "5. Jackets & Coats", Slug = "jackets-coats", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = false },
                    new() { Id = "mc6", Name = "6. Hoodies", Slug = "hoodies", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = false },
                    new() { Id = "mc7", Name = "7. Sweatshirts", Slug = "sweatshirts", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 7, IsUploadingSoon = false },
                    new() { Id = "mc8", Name = "8. Jeans", Slug = "jeans", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 8, IsUploadingSoon = false },
                    new() { Id = "mc9", Name = "9. Trousers", Slug = "trousers", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 9, IsUploadingSoon = false },
                    new() { Id = "mc10", Name = "10. Pants", Slug = "pants", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 10, IsUploadingSoon = false },
                    new() { Id = "mc11", Name = "11. Innerwear", Slug = "innerwear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 11, IsUploadingSoon = false },
                    new() { Id = "mc12", Name = "12. Sleepwear", Slug = "sleepwear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 12, IsUploadingSoon = false },
                    new() { Id = "mc13", Name = "13. Shorts", Slug = "shorts", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 13, IsUploadingSoon = false },
                    new() { Id = "mc14", Name = "14. Socks", Slug = "socks", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 14, IsUploadingSoon = false },
                    new() { Id = "mc15", Name = "15. Caps", Slug = "caps", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 15, IsUploadingSoon = false },
                    new() { Id = "mc16", Name = "16. Towels", Slug = "towels", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 16, IsUploadingSoon = false },
                    new() { Id = "mc17", Name = "17. Belts", Slug = "belts", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 17, IsUploadingSoon = false }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks for Men",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/category/fashion/men-clothing",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "mp1", Name = "Men's Polo T-Shirt", CategoryName = "Men Clothing", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 128, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing?subcategory=t-shirts", IsUploadingSoon = false },
                    new() { Id = "mp2", Name = "Men's Casual Shirt", CategoryName = "Men Clothing", Price = 849, OriginalPrice = 1699, Rating = 4.7m, ReviewsCount = 96, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing?subcategory=shirts", IsUploadingSoon = false },
                    new() { Id = "mp3", Name = "Men's Hoodie", CategoryName = "Men Clothing", Price = 899, OriginalPrice = 1799, Rating = 4.8m, ReviewsCount = 87, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/hoodies", IsUploadingSoon = false },
                    new() { Id = "mp4", Name = "Men's Slim Fit Jeans", CategoryName = "Men Clothing", Price = 1199, OriginalPrice = 2399, Rating = 4.9m, ReviewsCount = 76, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing?subcategory=jeans", IsUploadingSoon = false },
                    new() { Id = "mp5", Name = "Men's Bomber Jacket", CategoryName = "Men Clothing", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing?subcategory=jackets-coats", IsUploadingSoon = false },
                    new() { Id = "mp6", Name = "Men's Chino Pants", CategoryName = "Men Clothing", Price = 999, OriginalPrice = 1999, Rating = 4.7m, ReviewsCount = 84, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing?subcategory=pants", IsUploadingSoon = false },
                    new() { Id = "mp7", Name = "Men's Dry Fit T-Shirt", CategoryName = "Men Clothing", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 65, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing?subcategory=t-shirts", IsUploadingSoon = false },
                    new() { Id = "mp8", Name = "Men's Checked Shirt", CategoryName = "Men Clothing", Price = 749, OriginalPrice = 1499, Rating = 4.7m, ReviewsCount = 78, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-clothing?subcategory=shirts", IsUploadingSoon = false }
                }
            },
            TrustBadgesSection = new FashionTrustBadgesSectionDto
            {
                IsEnabled = true,
                Badges = new List<FashionTrustBadgeDto>
                {
                    new() { Id = "mt1", Title = "Curated for Students", Subtitle = "Handpicked styles for every student's journey", Icon = "box" },
                    new() { Id = "mt2", Title = "Quality You Can Trust", Subtitle = "100% genuine & premium quality", Icon = "shield" },
                    new() { Id = "mt3", Title = "Affordable for Everyone", Subtitle = "Best value fashion for students", Icon = "tag" },
                    new() { Id = "mt4", Title = "Fast & Reliable Delivery", Subtitle = "On-time delivery worldwide", Icon = "truck" },
                    new() { Id = "mt5", Title = "Easy Returns", Subtitle = "Hassle-free returns within 7 days", Icon = "return" },
                    new() { Id = "mt6", Title = "24/7 Customer Support", Subtitle = "We're always here to help you", Icon = "headset" }
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

// -------------------------------------------------------------
// WOMEN CLOTHING DTO (MATCHES SCREENSHOT 2)
// -------------------------------------------------------------
public sealed class FashionWomenClothingDto
{
    public bool IsEnabled { get; set; } = true;
    public FashionHeroBannerDto HeroBanner { get; set; } = new();
    public FashionCategoriesSectionDto CategoriesSection { get; set; } = new();
    public FashionTopPicksSectionDto TopPicksSection { get; set; } = new();
    public FashionTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();
    public FashionBottomBannerDto BottomBanner { get; set; } = new();

    public static FashionWomenClothingDto CreateDefault()
    {
        return new FashionWomenClothingDto
        {
            IsEnabled = true,
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Women Clothing",
                TitleHighlight = "Stylish. Comfortable. Travel-ready.",
                Subtitle = "Discover trendy and comfortable outfits perfect for every journey and season.",
                Description = "Curated collections with lightweight breathable fabrics designed for international campus life.",
                ImageUrl = "/images/fashion-hub/women_clothing_hero.jpg",
                IsUploadingSoon = false,
                TitleColor = "#063f91",
                TitleHighlightColor = "#ea580c",
                SubtitleColor = "#334155",
                BannerHeight = 440,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "wb1", Label = "Trendy Styles", Icon = "shirt" },
                    new() { Id = "wb2", Label = "Premium Quality", Icon = "shield" },
                    new() { Id = "wb3", Label = "Affordable Prices", Icon = "tag" },
                    new() { Id = "wb4", Label = "Loved by 10K+ Students", Icon = "users" }
                },
                RightCard = new FashionHeroRightCardDto
                {
                    Title = "Comfort",
                    TitleHighlight = "First",
                    Features = new List<FashionHeroFeatureDto>
                    {
                        new() { Id = "wf1", Title = "Comfort First", Icon = "feather" },
                        new() { Id = "wf2", Title = "Easy to Pack", Icon = "luggage" },
                        new() { Id = "wf3", Title = "All-Season Wear", Icon = "sun" },
                        new() { Id = "wf4", Title = "Perfect for Travel", Icon = "plane" }
                    }
                }
            },
            CategoriesSection = new FashionCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Categories →",
                ViewAllUrl = "/category/fashion/women-clothing",
                Categories = new List<FashionCategoryItemDto>
                {
                    new() { Id = "wc1", Name = "Tops & T-Shirts", Slug = "tops-t-shirts", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },
                    new() { Id = "wc2", Name = "Shirts & Blouses", Slug = "shirts-blouses", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },
                    new() { Id = "wc3", Name = "Dresses", Slug = "dresses", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },
                    new() { Id = "wc4", Name = "Hoodies & Sweatshirts", Slug = "hoodies-sweatshirts", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },
                    new() { Id = "wc5", Name = "Jackets & Coats", Slug = "jackets-coats", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true },
                    new() { Id = "wc6", Name = "Bottom Wear", Slug = "bottom-wear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true },
                    new() { Id = "wc7", Name = "Activewear", Slug = "activewear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 7, IsUploadingSoon = true },
                    new() { Id = "wc8", Name = "Innerwear & Sleepwear", Slug = "innerwear-sleepwear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 8, IsUploadingSoon = true },
                    new() { Id = "wc9", Name = "Ethnic Wear", Slug = "ethnic-wear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 9, IsUploadingSoon = true },
                    new() { Id = "wc10", Name = "Accessories", Slug = "accessories", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 10, IsUploadingSoon = true }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks for You",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/category/fashion/women-clothing",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "wp1", Name = "Oversized Cotton T-Shirt", CategoryName = "Women Clothing", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 128, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing", IsUploadingSoon = true },
                    new() { Id = "wp2", Name = "Floral Maxi Dress", CategoryName = "Women Clothing", Price = 1299, OriginalPrice = 2499, Rating = 4.9m, ReviewsCount = 96, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing", IsUploadingSoon = true },
                    new() { Id = "wp3", Name = "Women's Linen Shirt", CategoryName = "Women Clothing", Price = 849, OriginalPrice = 1699, Rating = 4.8m, ReviewsCount = 87, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing", IsUploadingSoon = true },
                    new() { Id = "wp4", Name = "High Waist Leggings", CategoryName = "Women Clothing", Price = 599, OriginalPrice = 1199, Rating = 4.7m, ReviewsCount = 76, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing", IsUploadingSoon = true },
                    new() { Id = "wp5", Name = "Women's Hoodie", CategoryName = "Women Clothing", Price = 899, OriginalPrice = 1799, Rating = 4.8m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/hoodies", IsUploadingSoon = true },
                    new() { Id = "wp6", Name = "Mom Fit Jeans", CategoryName = "Women Clothing", Price = 1199, OriginalPrice = 2399, Rating = 4.9m, ReviewsCount = 84, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing", IsUploadingSoon = true },
                    new() { Id = "wp7", Name = "Trench Coat", CategoryName = "Women Clothing", Price = 1799, OriginalPrice = 3499, Rating = 4.9m, ReviewsCount = 65, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing", IsUploadingSoon = true },
                    new() { Id = "wp8", Name = "Cotton Night Suit", CategoryName = "Women Clothing", Price = 699, OriginalPrice = 1399, Rating = 4.7m, ReviewsCount = 78, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing", IsUploadingSoon = true }
                }
            },
            TrustBadgesSection = new FashionTrustBadgesSectionDto
            {
                IsEnabled = true,
                Badges = new List<FashionTrustBadgeDto>
                {
                    new() { Id = "wt1", Title = "Curated for Students", Subtitle = "Handpicked styles for every student's journey", Icon = "box" },
                    new() { Id = "wt2", Title = "Quality You Can Trust", Subtitle = "100% genuine & premium quality", Icon = "shield" },
                    new() { Id = "wt3", Title = "Affordable for Everyone", Subtitle = "Best value fashion for students", Icon = "tag" },
                    new() { Id = "wt4", Title = "Fast & Reliable Delivery", Subtitle = "On-time delivery worldwide", Icon = "truck" },
                    new() { Id = "wt5", Title = "Easy Returns", Subtitle = "Hassle-free returns within 7 days", Icon = "return" },
                    new() { Id = "wt6", Title = "24/7 Customer Support", Subtitle = "We're always here to help you", Icon = "headset" }
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

// -------------------------------------------------------------
// HOODIES TEMPLATE DTO (MATCHES SCREENSHOT 3)
// -------------------------------------------------------------
public sealed class FashionHoodiesHubDto
{
    public bool IsEnabled { get; set; } = true;
    public string Tag { get; set; } = "NEW COLLECTION";
    public string Title { get; set; } = "HOODIES";
    public string TitleHighlight { get; set; } = "MADE FOR EVERY DAY";
    public string Subtitle { get; set; } = "Modern fits. Premium comfort. Effortless style.";
    public string PrimaryButtonText { get; set; } = "SHOP ALL HOODIES";
    public string PrimaryButtonUrl { get; set; } = "/products?q=hoodie";
    public string SecondaryButtonText { get; set; } = "NEW ARRIVALS";
    public string SecondaryButtonUrl { get; set; } = "/products?q=hoodie&sort=Newest";
    public string? HeroImageUrl { get; set; } = "/images/fashion-hub/hoodies_hero.jpg";
    public bool IsUploadingSoon { get; set; } = false;

    // Aliases for template bindings
    public string ShopAllButtonText { get => PrimaryButtonText; set => PrimaryButtonText = value; }
    public string ShopAllButtonUrl { get => PrimaryButtonUrl; set => PrimaryButtonUrl = value; }
    public string NewArrivalsButtonText { get => SecondaryButtonText; set => SecondaryButtonText = value; }
    public string NewArrivalsButtonUrl { get => SecondaryButtonUrl; set => SecondaryButtonUrl = value; }
    public bool IsHeroUploadingSoon { get => IsUploadingSoon; set => IsUploadingSoon = value; }

    public List<FashionPromoCardDto> PromoCards { get; set; } = new();
    public FashionFitSectionDto FitSection { get; set; } = new();
    public FashionTopPicksSectionDto TopPicksSection { get; set; } = new();
    public FashionTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();

    public static FashionHoodiesHubDto CreateDefault()
    {
        return new FashionHoodiesHubDto
        {
            IsEnabled = true,
            Tag = "NEW COLLECTION",
            Title = "HOODIES",
            TitleHighlight = "MADE FOR EVERY DAY",
            Subtitle = "Modern fits. Premium comfort. Effortless style.",
            PrimaryButtonText = "SHOP ALL HOODIES",
            PrimaryButtonUrl = "/products?q=hoodie",
            SecondaryButtonText = "NEW ARRIVALS",
            SecondaryButtonUrl = "/products?q=hoodie&sort=Newest",
            HeroImageUrl = "/images/fashion-hub/hoodies_hero.jpg",
            IsUploadingSoon = false,
            PromoCards = new List<FashionPromoCardDto>
            {
                new()
                {
                    Id = "hp1",
                    Title = "MINIMAL ESSENTIALS",
                    Subtitle = "Clean looks. Everyday versatility.",
                    ActionText = "EXPLORE COLLECTION →",
                    ActionUrl = "/products?q=minimal+hoodie",
                    ImageUrl = "/images/fashion-hub/hoodie_minimal.jpg",
                    IsUploadingSoon = false
                },
                new()
                {
                    Id = "hp2",
                    Title = "GRAPHIC STATEMENT",
                    Subtitle = "Bold prints. Stand out your way.",
                    ActionText = "EXPLORE COLLECTION →",
                    ActionUrl = "/products?q=graphic+hoodie",
                    ImageUrl = "/images/fashion-hub/hoodie_graphic.jpg",
                    IsUploadingSoon = false
                },
                new()
                {
                    Id = "hp3",
                    Title = "PREMIUM COMFORT",
                    Subtitle = "Superior fabrics. All-day ease.",
                    ActionText = "EXPLORE COLLECTION →",
                    ActionUrl = "/products?q=comfort+hoodie",
                    ImageUrl = "/images/fashion-hub/hoodie_comfort.jpg",
                    IsUploadingSoon = false
                }
            },
            FitSection = new FashionFitSectionDto
            {
                IsEnabled = true,
                Title = "SHOP BY FIT",
                ViewAllText = "VIEW ALL",
                ViewAllUrl = "/products?q=hoodie",
                Items = new List<FashionFitItemDto>
                {
                    new() { Id = "hf1", Name = "Oversized Fit", Slug = "oversized-fit", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },
                    new() { Id = "hf2", Name = "Regular Fit", Slug = "regular-fit", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },
                    new() { Id = "hf3", Name = "Relaxed Fit", Slug = "relaxed-fit", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },
                    new() { Id = "hf4", Name = "Zip-Up Hoodies", Slug = "zip-up-hoodies", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },
                    new() { Id = "hf5", Name = "Drop Shoulder", Slug = "drop-shoulder", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true },
                    new() { Id = "hf6", Name = "Sleeveless Hoodies", Slug = "sleeveless-hoodies", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Popular Hoodies for Students",
                ViewAllText = "View All Hoodies →",
                ViewAllUrl = "/products?q=hoodie",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "hpk1", Name = "Heavyweight Fleece Hoodie", CategoryName = "Hoodies", Price = 1299, OriginalPrice = 2499, Rating = 4.9m, ReviewsCount = 142, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/products?q=hoodie", IsUploadingSoon = true },
                    new() { Id = "hpk2", Name = "Classic Oversized Pullover", CategoryName = "Hoodies", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/products?q=hoodie", IsUploadingSoon = true },
                    new() { Id = "hpk3", Name = "Zip-Up Thermal Campus Hoodie", CategoryName = "Hoodies", Price = 1149, OriginalPrice = 2299, Rating = 4.8m, ReviewsCount = 86, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/products?q=hoodie", IsUploadingSoon = true },
                    new() { Id = "hpk4", Name = "Streetwear Graphic Hoodie", CategoryName = "Hoodies", Price = 1099, OriginalPrice = 2199, Rating = 4.7m, ReviewsCount = 64, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/products?q=hoodie", IsUploadingSoon = true }
                }
            },
            TrustBadgesSection = new FashionTrustBadgesSectionDto
            {
                IsEnabled = true,
                Badges = new List<FashionTrustBadgeDto>
                {
                    new() { Id = "ht1", Title = "100% Organic Cotton", Subtitle = "Breathable, plush, anti-pilling", Icon = "shield" },
                    new() { Id = "ht2", Title = "Double-Layer Hood", Subtitle = "Structured warm protection", Icon = "box" },
                    new() { Id = "ht3", Title = "Thermal Lined", Subtitle = "Engineered for winter travel", Icon = "sun" },
                    new() { Id = "ht4", Title = "Easy Care Machine Wash", Subtitle = "Pre-shrunk durable stitching", Icon = "check" }
                }
            }
        };
    }
}

// -------------------------------------------------------------
// SWEATSHIRTS TEMPLATE DTO (MATCHES SCREENSHOT 4)
// -------------------------------------------------------------
public sealed class FashionSweatshirtsHubDto
{
    public bool IsEnabled { get; set; } = true;
    public string OutletTag { get; set; } = "DREAMFLYKIT OUTLET";
    public string Tag { get; set; } = "MEN'S COLLECTION";
    public string Title { get; set; } = "SWEATSHIRTS";
    public string Subtitle { get; set; } = "Soft. Stylish. Made for everyday comfort.";
    public string PrimaryButtonText { get; set; } = "SHOP ALL SWEATSHIRTS";
    public string PrimaryButtonUrl { get; set; } = "/products?q=sweatshirt";
    public string SecondaryButtonText { get; set; } = "NEW ARRIVALS";
    public string SecondaryButtonUrl { get; set; } = "/products?q=sweatshirt&sort=Newest";
    public string? HeroImageUrl { get; set; } = "/images/fashion-hub/sweatshirts_hero.jpg";
    public bool IsUploadingSoon { get; set; } = false;

    // Aliases for template bindings
    public string CollectionTag { get => Tag; set => Tag = value; }
    public string ShopAllButtonText { get => PrimaryButtonText; set => PrimaryButtonText = value; }
    public string ShopAllButtonUrl { get => PrimaryButtonUrl; set => PrimaryButtonUrl = value; }
    public string NewArrivalsButtonText { get => SecondaryButtonText; set => SecondaryButtonText = value; }
    public string NewArrivalsButtonUrl { get => SecondaryButtonUrl; set => SecondaryButtonUrl = value; }
    public bool IsHeroUploadingSoon { get => IsUploadingSoon; set => IsUploadingSoon = value; }
    public List<FashionHeroBadgeDto> HeroBadges { get => Badges; set => Badges = value; }
    public List<FashionTrustBadgeDto> TrustBadges { get => TrustBadgesSection.Badges; set => TrustBadgesSection.Badges = value; }

    public List<FashionHeroBadgeDto> Badges { get; set; } = new();
    public List<FashionPromoCardDto> PromoCards { get; set; } = new();
    public FashionStyleSectionDto StyleSection { get; set; } = new();
    public FashionTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();

    public static FashionSweatshirtsHubDto CreateDefault()
    {
        return new FashionSweatshirtsHubDto
        {
            IsEnabled = true,
            OutletTag = "DREAMFLYKIT OUTLET",
            Tag = "MEN'S COLLECTION",
            Title = "SWEATSHIRTS",
            Subtitle = "Soft. Stylish. Made for everyday comfort.",
            PrimaryButtonText = "SHOP ALL SWEATSHIRTS",
            PrimaryButtonUrl = "/products?q=sweatshirt",
            SecondaryButtonText = "NEW ARRIVALS",
            SecondaryButtonUrl = "/products?q=sweatshirt&sort=Newest",
            HeroImageUrl = "/images/fashion-hub/sweatshirts_hero.jpg",
            IsUploadingSoon = false,
            Badges = new List<FashionHeroBadgeDto>
            {
                new() { Id = "sb1", Label = "Soft & Comfortable", Icon = "comfort" },
                new() { Id = "sb2", Label = "Premium Quality", Icon = "shield" },
                new() { Id = "sb3", Label = "Best Prices Everyday", Icon = "tag" },
                new() { Id = "sb4", Label = "Easy Care Fabric", Icon = "fabric" }
            },
            PromoCards = new List<FashionPromoCardDto>
            {
                new()
                {
                    Id = "sp1",
                    Title = "EVERYDAY ESSENTIALS",
                    Subtitle = "Timeless styles, made to move.",
                    ActionText = "EXPLORE COLLECTION →",
                    ActionUrl = "/products?q=everyday+sweatshirt",
                    ImageUrl = "/images/fashion-hub/sweatshirt_essentials.jpg",
                    IsUploadingSoon = false
                },
                new()
                {
                    Id = "sp2",
                    Title = "GRAPHIC SWEATSHIRTS",
                    Subtitle = "Bold prints. Stand out in every crowd.",
                    ActionText = "EXPLORE COLLECTION →",
                    ActionUrl = "/products?q=graphic+sweatshirt",
                    ImageUrl = "/images/fashion-hub/sweatshirt_graphic.jpg",
                    IsUploadingSoon = false
                },
                new()
                {
                    Id = "sp3",
                    Title = "PREMIUM COMFORT",
                    Subtitle = "Superior fabrics. All-day ease.",
                    ActionText = "EXPLORE COLLECTION →",
                    ActionUrl = "/products?q=premium+sweatshirt",
                    ImageUrl = "/images/fashion-hub/sweatshirt_comfort.jpg",
                    IsUploadingSoon = false
                }
            },
            StyleSection = new FashionStyleSectionDto
            {
                IsEnabled = true,
                Title = "SHOP BY STYLE",
                ViewAllText = "VIEW ALL",
                ViewAllUrl = "/products?q=sweatshirt",
                Items = new List<FashionStyleItemDto>
                {
                    new() { Id = "st1", Name = "Crew Neck", Slug = "crew-neck", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },
                    new() { Id = "st2", Name = "Hoodies", Slug = "hoodies", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },
                    new() { Id = "st3", Name = "Oversized", Slug = "oversized", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },
                    new() { Id = "st4", Name = "Zip-Up", Slug = "zip-up", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },
                    new() { Id = "st5", Name = "Printed", Slug = "printed", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true },
                    new() { Id = "st6", Name = "Solid", Slug = "solid", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true },
                    new() { Id = "st7", Name = "Half Zip", Slug = "half-zip", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 7, IsUploadingSoon = true },
                    new() { Id = "st8", Name = "Fleece", Slug = "fleece", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 8, IsUploadingSoon = true },
                    new() { Id = "st9", Name = "Colorblock", Slug = "colorblock", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 9, IsUploadingSoon = true },
                    new() { Id = "st10", Name = "Washed", Slug = "washed", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 10, IsUploadingSoon = true }
                }
            },
            TrustBadgesSection = new FashionTrustBadgesSectionDto
            {
                IsEnabled = true,
                Badges = new List<FashionTrustBadgeDto>
                {
                    new() { Id = "stb1", Title = "Affordable Prices", Subtitle = "Best fashion, always", Icon = "tag" },
                    new() { Id = "stb2", Title = "Premium Quality", Subtitle = "Built to last", Icon = "shield" },
                    new() { Id = "stb3", Title = "Fast Delivery", Subtitle = "On-time, every time", Icon = "truck" },
                    new() { Id = "stb4", Title = "Easy Returns", Subtitle = "Hassle-free returns", Icon = "return" }
                }
            }
        };
    }
}

// -------------------------------------------------------------
// HELPER SECTION & CARD DTOS
// -------------------------------------------------------------
public sealed class FashionOutletBadgeDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public sealed class FashionPromoCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ActionText { get; set; } = "EXPLORE COLLECTION →";
    public string ActionUrl { get; set; } = "/products?category=fashion";
    public string? ImageUrl { get; set; }
    public bool IsUploadingSoon { get; set; } = false;
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
}

public sealed class FashionFitSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "SHOP BY FIT";
    public string ViewAllText { get; set; } = "VIEW ALL";
    public string ViewAllUrl { get; set; } = "/products?category=fashion";
    public List<FashionFitItemDto> Items { get; set; } = new();
}

public sealed class FashionFitItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int DisplayOrder { get; set; } = 1;
    public bool IsUploadingSoon { get; set; } = true;
    public bool IsActive { get; set; } = true;
}

public sealed class FashionStyleSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "SHOP BY STYLE";
    public string ViewAllText { get; set; } = "VIEW ALL";
    public string ViewAllUrl { get; set; } = "/products?category=fashion";
    public List<FashionStyleItemDto> Items { get; set; } = new();
}

public sealed class FashionStyleItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int DisplayOrder { get; set; } = 1;
    public bool IsUploadingSoon { get; set; } = true;
    public bool IsActive { get; set; } = true;
}

// -------------------------------------------------------------
// ETHNIC WEAR DTO (MATCHES IMAGE 3)
// -------------------------------------------------------------
public sealed class FashionEthnicWearDto
{
    public bool IsEnabled { get; set; } = true;
    public FashionHeroBannerDto HeroBanner { get; set; } = new();
    public FashionCategoriesSectionDto CategoriesSection { get; set; } = new();
    public FashionTopPicksSectionDto TopPicksSection { get; set; } = new();
    public FashionTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();
    public FashionBottomBannerDto BottomBanner { get; set; } = new();

    public static FashionEthnicWearDto CreateDefault()
    {
        return new FashionEthnicWearDto
        {
            IsEnabled = true,
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Ethnic Wear",
                TitleHighlight = "For Every Celebration.",
                Subtitle = "Style. Tradition.",
                Description = "Traditional and contemporary styles for every student, explorer, and occassion.",
                ImageUrl = "/images/fashion-hub/ethnic_wear_hero.jpg",
                IsUploadingSoon = false,
                TitleColor = "#ffffff",
                TitleHighlightColor = "#f59e0b",
                SubtitleColor = "#e2e8f0",
                BannerHeight = 440,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "eb1", Label = "Handcrafted Traditions", Icon = "sparkles" },
                    new() { Id = "eb2", Label = "Premium Fabrics", Icon = "shield" },
                    new() { Id = "eb3", Label = "Affordable Elegance", Icon = "tag" },
                    new() { Id = "eb4", Label = "Loved for Special Days", Icon = "heart" }
                },
                RightCard = new FashionHeroRightCardDto
                {
                    Title = "Festive",
                    TitleHighlight = "Elegance",
                    Features = new List<FashionHeroFeatureDto>
                    {
                        new() { Id = "ef1", Title = "Authentic Craftsmanship", Icon = "star" },
                        new() { Id = "ef2", Title = "Festive & Wedding Ready", Icon = "gift" },
                        new() { Id = "ef3", Title = "Comfortable Fit", Icon = "shirt" },
                        new() { Id = "ef4", Title = "Global Delivery", Icon = "plane" }
                    }
                }
            },
            CategoriesSection = new FashionCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/category/fashion/ethnic-wear",
                Categories = new List<FashionCategoryItemDto>
                {
                    new() { Id = "ec1", Name = "1. Sherwanis", Slug = "sherwanis", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = false },
                    new() { Id = "ec2", Name = "2. Nehru Jackets", Slug = "nehru-jackets", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = false },
                    new() { Id = "ec3", Name = "3. Bandhgala Suits", Slug = "bandhgala-suits", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = false },
                    new() { Id = "ec4", Name = "4. Traditional Dhotis", Slug = "traditional-dhotis", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = false },
                    new() { Id = "ec5", Name = "5. Pajamas", Slug = "pajamas", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = false },
                    new() { Id = "ec6", Name = "6. Jodhpuri Suits", Slug = "jodhpuri-suits", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = false },
                    new() { Id = "ec7", Name = "7. Kurta Sets", Slug = "kurta-sets", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 7, IsUploadingSoon = false },
                    new() { Id = "ec8", Name = "8. Ethnic Footwear", Slug = "ethnic-footwear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 8, IsUploadingSoon = false },
                    new() { Id = "ec9", Name = "9. Brooches & Aura Pins", Slug = "brooches-aura-pins", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 9, IsUploadingSoon = false },
                    new() { Id = "ec10", Name = "10. Sherwani Sets", Slug = "sherwani-sets", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 10, IsUploadingSoon = false },
                    new() { Id = "ec11", Name = "11. Turbans (Pagris)", Slug = "turbans-pagris", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 11, IsUploadingSoon = false },
                    new() { Id = "ec12", Name = "12. Traditional Shawls", Slug = "traditional-shawls", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 12, IsUploadingSoon = false },
                    new() { Id = "ec13", Name = "13. Ethnic Pocket Squares", Slug = "ethnic-pocket-squares", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 13, IsUploadingSoon = false },
                    new() { Id = "ec14", Name = "14. Traditional Accessories", Slug = "traditional-accessories", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 14, IsUploadingSoon = false },
                    new() { Id = "ec15", Name = "15. Festive Wear Gifting", Slug = "festive-wear-gifting", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 15, IsUploadingSoon = false }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks for Men",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/category/fashion/ethnic-wear",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "ep1", Name = "Silk Kurta Set (Red)", CategoryName = "Ethnic Wear", Price = 1499, OriginalPrice = 2999, Rating = 4.8m, ReviewsCount = 128, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/ethnic-wear", IsUploadingSoon = false },
                    new() { Id = "ep2", Name = "Silk Kurta Set (Gold)", CategoryName = "Ethnic Wear", Price = 1499, OriginalPrice = 2999, Rating = 4.8m, ReviewsCount = 96, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/ethnic-wear", IsUploadingSoon = false },
                    new() { Id = "ep3", Name = "Brocade Nehru Jacket", CategoryName = "Ethnic Wear", Price = 1599, OriginalPrice = 3199, Rating = 4.9m, ReviewsCount = 187, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/ethnic-wear", IsUploadingSoon = false },
                    new() { Id = "ep4", Name = "Bandhgala Suit", CategoryName = "Ethnic Wear", Price = 2999, OriginalPrice = 5999, Rating = 4.8m, ReviewsCount = 76, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/ethnic-wear", IsUploadingSoon = false },
                    new() { Id = "ep5", Name = "Premium Sherwani", CategoryName = "Ethnic Wear", Price = 4999, OriginalPrice = 9999, Rating = 4.9m, ReviewsCount = 398, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/ethnic-wear", IsUploadingSoon = false },
                    new() { Id = "ep6", Name = "Embroidered Mojari", CategoryName = "Ethnic Wear", Price = 999, OriginalPrice = 1999, Rating = 4.7m, ReviewsCount = 84, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/ethnic-wear", IsUploadingSoon = false },
                    new() { Id = "ep7", Name = "Traditional Shawl", CategoryName = "Ethnic Wear", Price = 1299, OriginalPrice = 2599, Rating = 4.9m, ReviewsCount = 128, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/ethnic-wear", IsUploadingSoon = false },
                    new() { Id = "ep8", Name = "Elegant Brooch", CategoryName = "Ethnic Wear", Price = 799, OriginalPrice = 1599, Rating = 4.8m, ReviewsCount = 78, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/ethnic-wear", IsUploadingSoon = false }
                }
            },
            TrustBadgesSection = new FashionTrustBadgesSectionDto
            {
                IsEnabled = true,
                Badges = new List<FashionTrustBadgeDto>
                {
                    new() { Id = "et1", Title = "Curated for Students", Subtitle = "Handpicked styles for every student's journey", Icon = "box" },
                    new() { Id = "et2", Title = "Quality You Can Trust", Subtitle = "100% genuine & premium quality", Icon = "shield" },
                    new() { Id = "et3", Title = "Affordable for Everyone", Subtitle = "Best value fashion for students", Icon = "tag" },
                    new() { Id = "et4", Title = "Fast & Reliable Delivery", Subtitle = "On-time delivery worldwide", Icon = "truck" },
                    new() { Id = "et5", Title = "Easy Returns", Subtitle = "Hassle-free returns within 7 days", Icon = "return" },
                    new() { Id = "et6", Title = "24/7 Customer Support", Subtitle = "We're always here to help you", Icon = "headset" }
                }
            }
        };
    }
}

// -------------------------------------------------------------
// THERMALS DTO (MATCHES IMAGE 4)
// -------------------------------------------------------------
public sealed class FashionThermalsDto
{
    public bool IsEnabled { get; set; } = true;
    public List<string> SubNavItems { get; set; } = new() { "NEW ARRIVALS", "THERMALS", "WINTER ESSENTIALS", "CLOTHING", "FOOTWEAR", "ACCESSORIES", "SALE" };
    public FashionHeroBannerDto HeroBanner { get; set; } = new();
    public List<ThermalsCircleItemDto> ShopThermals { get; set; } = new();
    public List<ThermalsCircleItemDto> CircleItems { get => ShopThermals; set => ShopThermals = value; }
    public List<ThermalsCardItemDto> WinterEssentials { get; set; } = new();
    public List<ThermalsCardItemDto> WinterAccessories { get; set; } = new();
    public List<ThermalsWarmthLevelDto> WarmthLevels { get; set; } = new();
    public List<ThermalsCollectionDto> FeaturedCollections { get; set; } = new();
    public List<FashionProductItemDto> BestSellingProducts { get; set; } = new();
    public FashionTopPicksSectionDto TopPicksSection { get; set; } = new();
    public List<FashionTrustBadgeDto> FeaturesBar { get; set; } = new();

    public static FashionThermalsDto CreateDefault()
    {
        return new FashionThermalsDto
        {
            IsEnabled = true,
            SubNavItems = new List<string> { "NEW ARRIVALS", "THERMALS", "WINTER ESSENTIALS", "CLOTHING", "FOOTWEAR", "ACCESSORIES", "SALE" },
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "THERMALS",
                TitleHighlight = "Designed for everyday comfort.",
                Subtitle = "Built for warmth.",
                ButtonText = "SHOP THERMALS →",
                ButtonUrl = "#shop-thermals",
                ImageUrl = "/images/fashion-hub/thermals_hero.jpg",
                IsUploadingSoon = false,
                BannerHeight = 460,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "tb1", Label = "LOCKS HEAT — Keeps you warm in cold weather", Icon = "thermometer" },
                    new() { Id = "tb2", Label = "BREATHABLE — Moisture-wicking for all-day comfort", Icon = "wind" },
                    new() { Id = "tb3", Label = "LIGHTWEIGHT — Easy layering without bulk", Icon = "feather" }
                }
            },
            ShopThermals = new List<ThermalsCircleItemDto>
            {
                new() { Id = "st1", Title = "THERMAL TOPS", Subtitle = "Stay warm, stay active", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/thermals?sub=tops" },
                new() { Id = "st2", Title = "THERMAL BOTTOMS", Subtitle = "Warmth from within", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/thermals?sub=bottoms" },
                new() { Id = "st3", Title = "THERMAL SETS", Subtitle = "Complete warmth", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/thermals?sub=sets" },
                new() { Id = "st4", Title = "SLEEVELESS THERMALS", Subtitle = "For extra comfort", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/thermals?sub=sleeveless" }
            },
            WinterEssentials = new List<ThermalsCardItemDto>
            {
                new() { Id = "we1", Title = "JACKETS & COATS", Subtitle = "Shield yourself from the cold", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/men-clothing?subcategory=jackets-coats", Icon = "jacket" },
                new() { Id = "we2", Title = "HOODIES", Subtitle = "Comfort meets warmth", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/hoodies", Icon = "hoodie" },
                new() { Id = "we3", Title = "SWEATSHIRTS", Subtitle = "Everyday warmth you'll love", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/sweatshirts", Icon = "sweatshirt" },
                new() { Id = "we4", Title = "SWEATERS", Subtitle = "Classic warmth for winters", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/men-clothing?subcategory=sweaters", Icon = "sweater" }
            },
            WinterAccessories = new List<ThermalsCardItemDto>
            {
                new() { Id = "wa1", Title = "GLOVES", Subtitle = "Keep your hands warm", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/men-clothing?subcategory=gloves", Icon = "hand" },
                new() { Id = "wa2", Title = "BEANIES & CAPS", Subtitle = "Warmth for your head", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/men-clothing?subcategory=caps", Icon = "cap" },
                new() { Id = "wa3", Title = "SCARVES", Subtitle = "Wrap up in warmth", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/men-clothing?subcategory=scarves", Icon = "scarf" },
                new() { Id = "wa4", Title = "WARM SOCKS", Subtitle = "Cozy comfort all day", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/men-clothing?subcategory=socks", Icon = "socks" }
            },
            WarmthLevels = new List<ThermalsWarmthLevelDto>
            {
                new() { Id = "wl1", Level = "LIGHT WARMTH", Description = "For everyday comfort & mild cold", ButtonText = "SHOP NOW →", Url = "/category/fashion/thermals?level=light" },
                new() { Id = "wl2", Level = "COLD WEATHER", Description = "For colder days & travel", ButtonText = "SHOP NOW →", Url = "/category/fashion/thermals?level=cold" },
                new() { Id = "wl3", Level = "EXTREME COLD", Description = "Extra protection for harsh weather", ButtonText = "SHOP NOW →", Url = "/category/fashion/thermals?level=extreme" }
            },
            FeaturedCollections = new List<ThermalsCollectionDto>
            {
                new() { Id = "fc1", Title = "EVERYDAY THERMALS", Subtitle = "Soft, comfortable & perfect for daily wear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/thermals" },
                new() { Id = "fc2", Title = "TRAVEL & WINTER TRIPS", Subtitle = "Pack warmth wherever you go", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/thermals" },
                new() { Id = "fc3", Title = "OUTDOOR ESSENTIALS", Subtitle = "Made for colder outdoor conditions", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", Url = "/category/fashion/thermals" }
            },
            BestSellingProducts = new List<FashionProductItemDto>
            {
                new() { Id = "tp1", Name = "Striped Thermal Top", CategoryName = "Thermals", Price = 699, OriginalPrice = 1399, Rating = 4.6m, ReviewsCount = 120, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/thermals", IsUploadingSoon = false },
                new() { Id = "tp2", Name = "Solid Thermal Bottom", CategoryName = "Thermals", Price = 599, OriginalPrice = 1199, Rating = 4.7m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/thermals", IsUploadingSoon = false },
                new() { Id = "tp3", Name = "Premium Thermal Set", CategoryName = "Thermals", Price = 1199, OriginalPrice = 2399, Rating = 4.8m, ReviewsCount = 154, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/thermals", IsUploadingSoon = false },
                new() { Id = "tp4", Name = "Sleeveless Thermal Vest", CategoryName = "Thermals", Price = 449, OriginalPrice = 899, Rating = 4.5m, ReviewsCount = 76, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/thermals", IsUploadingSoon = false },
                new() { Id = "tp5", Name = "Waffle Thermal Top", CategoryName = "Thermals", Price = 799, OriginalPrice = 1599, Rating = 4.6m, ReviewsCount = 63, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/thermals", IsUploadingSoon = false },
                new() { Id = "tp6", Name = "Brushed Thermal Bottom", CategoryName = "Thermals", Price = 649, OriginalPrice = 1299, Rating = 4.7m, ReviewsCount = 82, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/thermals", IsUploadingSoon = false }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "BEST SELLING THERMALS",
                ViewAllText = "VIEW ALL →",
                ViewAllUrl = "/category/fashion/thermals",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "tp1", Name = "Striped Thermal Top", CategoryName = "Thermals", Price = 699, OriginalPrice = 1399, Rating = 4.6m, ReviewsCount = 120, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/thermals", IsUploadingSoon = false },
                    new() { Id = "tp2", Name = "Solid Thermal Bottom", CategoryName = "Thermals", Price = 599, OriginalPrice = 1199, Rating = 4.7m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/thermals", IsUploadingSoon = false },
                    new() { Id = "tp3", Name = "Premium Thermal Set", CategoryName = "Thermals", Price = 1199, OriginalPrice = 2399, Rating = 4.8m, ReviewsCount = 154, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/thermals", IsUploadingSoon = false },
                    new() { Id = "tp4", Name = "Sleeveless Thermal Vest", CategoryName = "Thermals", Price = 449, OriginalPrice = 899, Rating = 4.5m, ReviewsCount = 76, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/thermals", IsUploadingSoon = false },
                    new() { Id = "tp5", Name = "Waffle Thermal Top", CategoryName = "Thermals", Price = 799, OriginalPrice = 1599, Rating = 4.6m, ReviewsCount = 63, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/thermals", IsUploadingSoon = false },
                    new() { Id = "tp6", Name = "Brushed Thermal Bottom", CategoryName = "Thermals", Price = 649, OriginalPrice = 1299, Rating = 4.7m, ReviewsCount = 82, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/thermals", IsUploadingSoon = false }
                }
            },
            FeaturesBar = new List<FashionTrustBadgeDto>
            {
                new() { Id = "tf1", Title = "PREMIUM QUALITY", Subtitle = "High quality fabric built to last", Icon = "shield" },
                new() { Id = "tf2", Title = "BREATHABLE FABRIC", Subtitle = "Keeps you warm yet let your skin breathe", Icon = "wind" },
                new() { Id = "tf3", Title = "LIGHTWEIGHT & FLEXIBLE", Subtitle = "Move freely with comfort all day", Icon = "feather" },
                new() { Id = "tf4", Title = "EASY CARE", Subtitle = "Machine washable & easy to maintain", Icon = "refresh" }
            }
        };
    }
}

public sealed class ThermalsCircleItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public sealed class ThermalsCardItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public sealed class ThermalsWarmthLevelDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Level { get => Title; set => Title = value; }
    public string Subtitle { get; set; } = string.Empty;
    public string Description { get => Subtitle; set => Subtitle = value; }
    public string BackgroundColor { get; set; } = "#1e293b";
    public string ButtonText { get; set; } = "SHOP NOW →";
    public string ButtonUrl { get; set; } = "/category/fashion/thermals";
    public string Url { get => ButtonUrl; set => ButtonUrl = value; }
    public bool IsActive { get; set; } = true;
}

public sealed class ThermalsCollectionDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ButtonUrl { get; set; } = "/category/fashion/thermals";
    public string Url { get => ButtonUrl; set => ButtonUrl = value; }
    public bool IsActive { get; set; } = true;
}


