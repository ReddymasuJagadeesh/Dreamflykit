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
    public FashionMenFootwearDto MenFootwear { get; set; } = FashionMenFootwearDto.CreateDefault();

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
            MenFootwear = FashionMenFootwearDto.CreateDefault(),

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

    public string? CategorySlug { get; set; }

    public decimal Price { get; set; }

    public decimal? OriginalPrice { get; set; }

    public decimal Rating { get; set; } = 4.8m;

    public int ReviewsCount { get; set; } = 50;

    public string? ImageUrl { get; set; }

    public string ProductUrl { get; set; } = "/products?category=fashion";

    public bool IsUploadingSoon { get; set; } = true;

    public bool IsActive { get; set; } = true;

    public string? Badge { get; set; }

    public List<string> Sizes { get; set; } = new();

    public List<string> Colors { get; set; } = new();

    public bool InStock { get; set; } = true;

    public string? Fit { get; set; }

    public string? Style { get; set; }

    public string? Fabric { get; set; }

}



public sealed class FashionFestivalThemeDto

{

    public bool IsEnabled { get; set; } = true;

    public string FestivalName { get; set; } = "Festive Season Edit";

    public string AnnouncementText { get; set; } = "✨ Campus Festive Collection: Travel-ready styles with special student discounts!";

    public string BadgeText { get; set; } = "FESTIVAL SPECIAL";

    public string BannerHighlight { get; set; } = "Up to 50% Off";

    public string BgGradient { get; set; } = "linear-gradient(135deg, #e11d48 0%, #ea580c 100%)";

}



public sealed class FashionWomenSubcategoryPageDto

{

    public string CategorySlug { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public FashionWomenSubcategoryHeroDto HeroBanner { get; set; } = new();

    public List<FashionProductItemDto> Products { get; set; } = new();

}



public sealed class FashionWomenSubcategoryHeroDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public string ThemeColor { get; set; } = "#e11d48";
    public string BadgeText { get; set; } = "STUDENT ESSENTIALS • 100% GENUINE";
}

public sealed class FashionMenFootwearSubcategoryPageDto
{
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public FashionMenFootwearSubcategoryHeroDto HeroBanner { get; set; } = new();
    public List<FashionProductItemDto> Products { get; set; } = new();
}

public sealed class FashionMenFootwearSubcategoryHeroDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public string ThemeColor { get; set; } = "#0284c7";
    public string BadgeText { get; set; } = "STUDENT ESSENTIALS • 100% GENUINE";
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


public sealed class FashionMenClothingSubcategoryPageDto
{
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string Emoji { get; set; } = "👕";
    public FashionMenClothingSubcategoryHeroDto HeroBanner { get; set; } = new();
    public FashionShopByStyleSectionDto ShopByStyleSection { get; set; } = new();
    public List<string> StyleTypes { get; set; } = new();
    public List<FashionProductItemDto> Products { get; set; } = new();
}

public sealed class FashionMenClothingSubcategoryHeroDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = "STUDENT ESSENTIALS • 100% GENUINE";
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public int BannerHeight { get; set; } = 400;
    public string ThemeColor { get; set; } = "#0284c7";
    public string BadgeText { get; set; } = "CAMPUS FASHION";
    public List<string> Badges { get; set; } = new();
}

public sealed class FashionShopByStyleSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Shop by Category";
    public string Subtitle { get; set; } = "Select your occasion and style — Casual, Formal, Party Wear, Streetwear & more.";
    public List<FashionStyleItemDto> Styles { get; set; } = new();
}




public sealed class FashionMenClothingDto

{

    public bool IsEnabled { get; set; } = true;

    public FashionHeroBannerDto HeroBanner { get; set; } = new();

    public FashionCategoriesSectionDto CategoriesSection { get; set; } = new();

    public FashionTopPicksSectionDto TopPicksSection { get; set; } = new();

    public FashionTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();

    public FashionBottomBannerDto BottomBanner { get; set; } = new();

    public FashionFestivalThemeDto FestivalTheme { get; set; } = new();

    public List<FashionMenClothingSubcategoryPageDto> SubcategoryPages { get; set; } = new();




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

,
            SubcategoryPages = CreateDefaultSubcategoryPages()
        };

    }

    public static List<FashionMenClothingSubcategoryPageDto> CreateDefaultSubcategoryPages()
    {
        return new List<FashionMenClothingSubcategoryPageDto>
        {
            new()
            {
                CategorySlug = "ethnic-wear",
                CategoryName = "Ethnic Wear",
                Emoji = "🥻",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Ethnic Wear",
                    TitleHighlight = "TRADITIONAL CHARM • FESTIVE READY",
                    Subtitle = "Authentic kurtas, elegant Nehru jackets, and festive Indo-Western sets crafted for university cultural fests, celebrations, and formal ceremonies abroad.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "100% Breathable Cotton", "Festive Approved", "Wrinkle Resistant", "Lightweight Fabric" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated ethnic wear styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "ethnic-wear_s1", Name = "Casual Kurtas", Slug = "casual-kurtas", Emoji = "👘", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "ethnic-wear_s2", Name = "Formal Nehru Jackets", Slug = "formal-nehru-jackets", Emoji = "🦺", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "ethnic-wear_s3", Name = "Party Indo-Western", Slug = "party-indo-western", Emoji = "✨", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "ethnic-wear_s4", Name = "Festive Sherwanis", Slug = "festive-sherwanis", Emoji = "👑", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "ethnic-wear_s5", Name = "Cotton Kurta Sets", Slug = "cotton-kurta-sets", Emoji = "👔", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "ethnic-wear_s6", Name = "Festive Dupattas & Stoles", Slug = "festive-dupattas", Emoji = "🧣", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "ethnic-wear_p1",
                        Name = "Pure Cotton Mandarin Collar Long Kurta",
                        CategoryName = "Ethnic Wear",
                        CategorySlug = "ethnic-wear",
                        Price = 1199,
                        OriginalPrice = 2499,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/ethnic-wear",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "White", "Navy", "Maroon", "Mustard" }
                    },
                    new()
                    {
                        Id = "ethnic-wear_p2",
                        Name = "Jacquard Silk Blend Tailored Nehru Jacket",
                        CategoryName = "Ethnic Wear",
                        CategorySlug = "ethnic-wear",
                        Price = 1899,
                        OriginalPrice = 3999,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/ethnic-wear",
                        IsUploadingSoon = true,
                        Badge = "FESTIVE READY",
                        Sizes = new List<string> { "38", "40", "42", "44" },
                        Colors = new List<string> { "Black", "Royal Blue", "Gold", "Maroon" }
                    },
                    new()
                    {
                        Id = "ethnic-wear_p3",
                        Name = "Modern Asymmetric Hem Indo-Western Kurta",
                        CategoryName = "Ethnic Wear",
                        CategorySlug = "ethnic-wear",
                        Price = 1699,
                        OriginalPrice = 3499,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/ethnic-wear",
                        IsUploadingSoon = true,
                        Badge = "TRENDING",
                        Sizes = new List<string> { "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Olive", "Charcoal", "Wine" }
                    },
                    new()
                    {
                        Id = "ethnic-wear_p4",
                        Name = "Classic Chikankari Embroidered Festive Kurta",
                        CategoryName = "Ethnic Wear",
                        CategorySlug = "ethnic-wear",
                        Price = 2299,
                        OriginalPrice = 4599,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/ethnic-wear",
                        IsUploadingSoon = true,
                        Badge = "HERITAGE",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Ivory", "Powder Blue", "Peach" }
                    }
                }
            },
            new()
            {
                CategorySlug = "thermals",
                CategoryName = "Thermals",
                Emoji = "❄️",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Thermals",
                    TitleHighlight = "SUB-ZERO HEAT RETENTION • MERINO WOOL",
                    Subtitle = "Ultra-warm, moisture-wicking baselayers and merino fleece thermals designed for extreme winter study destinations in the USA, UK, Canada & Europe.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "Merino Wool Infused", "Sub-Zero Rated (-15°C)", "Anti-Odor Technology", "Ultra-Lightweight" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated thermals styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "thermals_s1", Name = "Casual Daily Baselayers", Slug = "casual-baselayers", Emoji = "👕", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "thermals_s2", Name = "Heavy Winter Thermal Sets", Slug = "heavy-winter-sets", Emoji = "❄️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "thermals_s3", Name = "Merino Wool Baselayers", Slug = "merino-wool", Emoji = "🐑", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "thermals_s4", Name = "Thermal Tops", Slug = "thermal-tops", Emoji = "👔", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "thermals_s5", Name = "Thermal Bottoms / Spats", Slug = "thermal-bottoms", Emoji = "👖", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "thermals_s6", Name = "Extreme Arctic Cold Suits", Slug = "extreme-arctic", Emoji = "🏔️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "thermals_p1",
                        Name = "Merino Wool Sub-Zero Warmth Thermal Set (Top + Bottom)",
                        CategoryName = "Thermals",
                        CategorySlug = "thermals",
                        Price = 1899,
                        OriginalPrice = 3999,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/thermals",
                        IsUploadingSoon = true,
                        Badge = "ARCTIC RATED",
                        Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Charcoal", "Black", "Off-White" }
                    },
                    new()
                    {
                        Id = "thermals_p2",
                        Name = "Ultra-Light Microfleece Long Sleeve Baselayer Top",
                        CategoryName = "Thermals",
                        CategorySlug = "thermals",
                        Price = 999,
                        OriginalPrice = 1999,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/thermals",
                        IsUploadingSoon = true,
                        Badge = "CAMPUS VALUE",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Black", "Navy", "Grey" }
                    },
                    new()
                    {
                        Id = "thermals_p3",
                        Name = "Stretch Comfort Thermal Long Johns Leggings",
                        CategoryName = "Thermals",
                        CategorySlug = "thermals",
                        Price = 899,
                        OriginalPrice = 1799,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/thermals",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Dark Grey", "Black" }
                    },
                    new()
                    {
                        Id = "thermals_p4",
                        Name = "Heavyweight Dual-Layer Thermal Fleece Body Warmer",
                        CategoryName = "Thermals",
                        CategorySlug = "thermals",
                        Price = 1399,
                        OriginalPrice = 2799,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/thermals",
                        IsUploadingSoon = true,
                        Badge = "TRAVEL MUST-HAVE",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Navy", "Charcoal" }
                    }
                }
            },
            new()
            {
                CategorySlug = "t-shirts",
                CategoryName = "T-Shirts",
                Emoji = "👕",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's T-Shirts",
                    TitleHighlight = "100% COMBED COTTON • ALL-DAY COMFORT",
                    Subtitle = "Premium crew necks, breathable pique polos, and oversized streetwear tees designed for campus lectures, study groups, and weekend hangs.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "220 GSM Bio-Washed", "Pre-Shrunk Cotton", "Shape Retention", "Color Fast Guarantee" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated t-shirts styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "t-shirts_s1", Name = "Casual Crew Necks", Slug = "casual-crew-necks", Emoji = "👕", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "t-shirts_s2", Name = "Formal Smart Polos", Slug = "formal-smart-polos", Emoji = "👔", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "t-shirts_s3", Name = "Party Graphic Tees", Slug = "party-graphic-tees", Emoji = "🎨", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "t-shirts_s4", Name = "Oversized Streetwear", Slug = "oversized-streetwear", Emoji = "🛹", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "t-shirts_s5", Name = "Gym & Workout Tees", Slug = "gym-workout-tees", Emoji = "🏃", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "t-shirts_s6", Name = "Vintage Henley Tees", Slug = "vintage-henley-tees", Emoji = "🔘", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "t-shirts_p1",
                        Name = "Heavyweight 240 GSM Oversized Streetwear Tee",
                        CategoryName = "T-Shirts",
                        CategorySlug = "t-shirts",
                        Price = 799,
                        OriginalPrice = 1599,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/t-shirts",
                        IsUploadingSoon = true,
                        Badge = "CAMPUS FAV",
                        Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Onyx Black", "Sage Green", "Off-White", "Mocha" }
                    },
                    new()
                    {
                        Id = "t-shirts_p2",
                        Name = "Classic Cotton Pique Slim Fit Polo Shirt",
                        CategoryName = "T-Shirts",
                        CategorySlug = "t-shirts",
                        Price = 999,
                        OriginalPrice = 1999,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/t-shirts",
                        IsUploadingSoon = true,
                        Badge = "SMART CASUAL",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Navy", "Burgundy", "White", "Forest Green" }
                    },
                    new()
                    {
                        Id = "t-shirts_p3",
                        Name = "Everyday Essential Bio-Washed Crew Neck T-Shirt (Pack of 3)",
                        CategoryName = "T-Shirts",
                        CategorySlug = "t-shirts",
                        Price = 1299,
                        OriginalPrice = 2599,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/t-shirts",
                        IsUploadingSoon = true,
                        Badge = "VALUE PACK",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Black/White/Grey", "Navy/Olive/Charcoal" }
                    },
                    new()
                    {
                        Id = "t-shirts_p4",
                        Name = "Vintage 3-Button Waffle Knit Long Sleeve Henley",
                        CategoryName = "T-Shirts",
                        CategorySlug = "t-shirts",
                        Price = 899,
                        OriginalPrice = 1799,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/t-shirts",
                        IsUploadingSoon = true,
                        Badge = "TRENDING",
                        Sizes = new List<string> { "M", "L", "XL" },
                        Colors = new List<string> { "Navy", "Olive", "Heather Grey" }
                    }
                }
            },
            new()
            {
                CategorySlug = "shirts",
                CategoryName = "Shirts",
                Emoji = "👔",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Shirts",
                    TitleHighlight = "PREMIUM OXFORD & LINEN • WRINKLE-RESISTANT",
                    Subtitle = "From crisp formal button-downs for university presentations to relaxed casual linens and party printed shirts.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "100% Cotton Oxford", "Easy Iron Finish", "Button-Down Collar", "Reinforced Seams" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated shirts styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "shirts_s1", Name = "Casual Shirts", Slug = "casual-shirts", Emoji = "👕", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "shirts_s2", Name = "Formal Dress Shirts", Slug = "formal-dress-shirts", Emoji = "👔", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "shirts_s3", Name = "Party Wear & Printed", Slug = "party-wear-printed", Emoji = "✨", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "shirts_s4", Name = "Denim & Chambray", Slug = "denim-chambray", Emoji = "👖", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "shirts_s5", Name = "Linen Summer Shirts", Slug = "linen-summer-shirts", Emoji = "🌿", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "shirts_s6", Name = "Oxford Button-Downs", Slug = "oxford-button-downs", Emoji = "🎓", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "shirts_p1",
                        Name = "Classic Oxford Cotton Button-Down Shirt",
                        CategoryName = "Shirts",
                        CategorySlug = "shirts",
                        Price = 1299,
                        OriginalPrice = 2599,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/shirts",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "38", "39", "40", "42", "44" },
                        Colors = new List<string> { "Sky Blue", "Classic White", "Pale Pink", "Navy" }
                    },
                    new()
                    {
                        Id = "shirts_p2",
                        Name = "Non-Iron Luxury Formal Dress Shirt",
                        CategoryName = "Shirts",
                        CategorySlug = "shirts",
                        Price = 1499,
                        OriginalPrice = 2999,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/shirts",
                        IsUploadingSoon = true,
                        Badge = "INTERVIEW READY",
                        Sizes = new List<string> { "38", "39", "40", "42", "44" },
                        Colors = new List<string> { "Crisp White", "Light Blue", "Charcoal" }
                    },
                    new()
                    {
                        Id = "shirts_p3",
                        Name = "Pure Breathable Linen Casual Resort Shirt",
                        CategoryName = "Shirts",
                        CategorySlug = "shirts",
                        Price = 1699,
                        OriginalPrice = 3499,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/shirts",
                        IsUploadingSoon = true,
                        Badge = "SUMMER VIBES",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Natural Beige", "Olive", "White", "Teal" }
                    },
                    new()
                    {
                        Id = "shirts_p4",
                        Name = "Washed Indigo Denim Casual Shirt with Snap Buttons",
                        CategoryName = "Shirts",
                        CategorySlug = "shirts",
                        Price = 1399,
                        OriginalPrice = 2799,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/shirts",
                        IsUploadingSoon = true,
                        Badge = "RUGGED STYLE",
                        Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Light Wash", "Mid Indigo", "Dark Stone" }
                    }
                }
            },
            new()
            {
                CategorySlug = "jackets-coats",
                CategoryName = "Jackets & Coats",
                Emoji = "🧥",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Jackets & Coats",
                    TitleHighlight = "ALL-WEATHER PROTECTION • WINDPROOF & INSULATED",
                    Subtitle = "Heavy duty puffers, classic wool blazers, rugged denim jackets, and travel-ready windbreakers built for changing global climates.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "Water-Resistant Shell", "Thermal Insulation", "Internal Gadget Pockets", "YKK Zippers" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated jackets & coats styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "jackets-coats_s1", Name = "Casual Denim & Windbreakers", Slug = "casual-denim-windbreakers", Emoji = "🧥", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "jackets-coats_s2", Name = "Formal Blazers & Overcoats", Slug = "formal-blazers-overcoats", Emoji = "👔", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "jackets-coats_s3", Name = "Party Leather & Suede", Slug = "party-leather-suede", Emoji = "✨", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "jackets-coats_s4", Name = "Puffer Winter Coats", Slug = "puffer-winter-coats", Emoji = "❄️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "jackets-coats_s5", Name = "Bomber & Varsity Jackets", Slug = "bomber-varsity-jackets", Emoji = "🏈", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "jackets-coats_s6", Name = "Fleece & Travel Layers", Slug = "fleece-travel-layers", Emoji = "🎒", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "jackets-coats_p1",
                        Name = "Ultra-Light Packable Down Puffer Winter Jacket",
                        CategoryName = "Jackets & Coats",
                        CategorySlug = "jackets-coats",
                        Price = 2499,
                        OriginalPrice = 4999,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/jackets-coats",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Matte Black", "Navy", "Dark Olive", "Burgundy" }
                    },
                    new()
                    {
                        Id = "jackets-coats_p2",
                        Name = "Classic Wool Blend Structured Overcoat",
                        CategoryName = "Jackets & Coats",
                        CategorySlug = "jackets-coats",
                        Price = 3999,
                        OriginalPrice = 7999,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/jackets-coats",
                        IsUploadingSoon = true,
                        Badge = "CAMPUS ICON",
                        Sizes = new List<string> { "38", "40", "42", "44" },
                        Colors = new List<string> { "Camel", "Charcoal Grey", "Black" }
                    },
                    new()
                    {
                        Id = "jackets-coats_p3",
                        Name = "Rugged Sherpa-Lined Heavy Trucker Denim Jacket",
                        CategoryName = "Jackets & Coats",
                        CategorySlug = "jackets-coats",
                        Price = 2799,
                        OriginalPrice = 5499,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/jackets-coats",
                        IsUploadingSoon = true,
                        Badge = "VINTAGE WARMTH",
                        Sizes = new List<string> { "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Vintage Blue", "Washed Black" }
                    },
                    new()
                    {
                        Id = "jackets-coats_p4",
                        Name = "All-Weather Waterproof Commuter Windbreaker",
                        CategoryName = "Jackets & Coats",
                        CategorySlug = "jackets-coats",
                        Price = 1899,
                        OriginalPrice = 3799,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/jackets-coats",
                        IsUploadingSoon = true,
                        Badge = "COMMUTE PRO",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Black", "Electric Blue", "Olive" }
                    }
                }
            },
            new()
            {
                CategorySlug = "hoodies",
                CategoryName = "Hoodies",
                Emoji = "🧥",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Hoodies",
                    TitleHighlight = "CAMPUS COMFORT • HEAVYWEIGHT FLEECE",
                    Subtitle = "Ultra-soft brushed fleece hoodies with double-lined hoods and spacious kangaroo pockets for cozy library marathons and frosty morning walks.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "360 GSM Heavyweight Terry", "Double-Lined Hood", "Preshrunk Organic Cotton", "Spacious Kangaroo Pocket" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated hoodies styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "hoodies_s1", Name = "Casual Pullover Hoodies", Slug = "casual-pullover-hoodies", Emoji = "🧥", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "hoodies_s2", Name = "Full-Zip Casual Hoodies", Slug = "full-zip-hoodies", Emoji = "🤐", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "hoodies_s3", Name = "Streetwear Oversized", Slug = "streetwear-oversized", Emoji = "🛹", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "hoodies_s4", Name = "Heavyweight Terry Fleece", Slug = "heavyweight-terry", Emoji = "🧶", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "hoodies_s5", Name = "Gym Performance Hoodies", Slug = "gym-performance-hoodies", Emoji = "🏃", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "hoodies_s6", Name = "Minimalist Drop-Shoulder", Slug = "minimalist-drop-shoulder", Emoji = "✨", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "hoodies_p1",
                        Name = "Premium 380 GSM Heavyweight Boxy Pullover Hoodie",
                        CategoryName = "Hoodies",
                        CategorySlug = "hoodies",
                        Price = 1499,
                        OriginalPrice = 2999,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/hoodies",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Charcoal", "Mocha Brown", "Off-White", "Pitch Black" }
                    },
                    new()
                    {
                        Id = "hoodies_p2",
                        Name = "Full-Zip Thermal Lined Travel Campus Hoodie",
                        CategoryName = "Hoodies",
                        CategorySlug = "hoodies",
                        Price = 1699,
                        OriginalPrice = 3299,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/hoodies",
                        IsUploadingSoon = true,
                        Badge = "TRAVEL READY",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Navy", "Heather Grey", "Black" }
                    },
                    new()
                    {
                        Id = "hoodies_p3",
                        Name = "Vintage Washed Minimalist Raglan Sleeve Hoodie",
                        CategoryName = "Hoodies",
                        CategorySlug = "hoodies",
                        Price = 1399,
                        OriginalPrice = 2799,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/hoodies",
                        IsUploadingSoon = true,
                        Badge = "TRENDING",
                        Sizes = new List<string> { "M", "L", "XL" },
                        Colors = new List<string> { "Washed Olive", "Washed Maroon", "Washed Grey" }
                    },
                    new()
                    {
                        Id = "hoodies_p4",
                        Name = "Athletic Training Moisture-Wicking Performance Hoodie",
                        CategoryName = "Hoodies",
                        CategorySlug = "hoodies",
                        Price = 1299,
                        OriginalPrice = 2499,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/hoodies",
                        IsUploadingSoon = true,
                        Badge = "WORKOUT ESSENTIAL",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Black/Reflective", "Gunmetal" }
                    }
                }
            },
            new()
            {
                CategorySlug = "sweatshirts",
                CategoryName = "Sweatshirts",
                Emoji = "🧶",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Sweatshirts",
                    TitleHighlight = "BREATHABLE FLEECE • CLASSIC CREWNECKS",
                    Subtitle = "Timeless crewneck pullovers, collegiate graphics, and drop-shoulder silhouettes made from sustainable cotton blends.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "100% Combed Cotton Terry", "Anti-Pilling Finish", "Ribbed Crew Collar", "All-Season Breathability" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated sweatshirts styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "sweatshirts_s1", Name = "Casual Crewneck Sweaters", Slug = "casual-crewneck-sweaters", Emoji = "🧶", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "sweatshirts_s2", Name = "Minimalist Solid Pullovers", Slug = "minimalist-solid-pullovers", Emoji = "✨", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "sweatshirts_s3", Name = "Party Graphic Sweatshirts", Slug = "party-graphic-sweatshirts", Emoji = "🎨", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "sweatshirts_s4", Name = "Varsity Collegiate Sweaters", Slug = "varsity-collegiate", Emoji = "🎓", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "sweatshirts_s5", Name = "Heavy French Terry", Slug = "heavy-french-terry", Emoji = "🧥", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "sweatshirts_s6", Name = "Travel Warmth Sweaters", Slug = "travel-warmth-sweaters", Emoji = "🎒", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "sweatshirts_p1",
                        Name = "Collegiate Heavy Terry Crewneck Sweatshirt",
                        CategoryName = "Sweatshirts",
                        CategorySlug = "sweatshirts",
                        Price = 1199,
                        OriginalPrice = 2399,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/sweatshirts",
                        IsUploadingSoon = true,
                        Badge = "CAMPUS FAV",
                        Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Navy", "Forest Green", "Heather Grey", "Burgundy" }
                    },
                    new()
                    {
                        Id = "sweatshirts_p2",
                        Name = "Essential Minimalist Drop-Shoulder Crew Pullover",
                        CategoryName = "Sweatshirts",
                        CategorySlug = "sweatshirts",
                        Price = 999,
                        OriginalPrice = 1999,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/sweatshirts",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Sand Beige", "Onyx Black", "Sage Green" }
                    },
                    new()
                    {
                        Id = "sweatshirts_p3",
                        Name = "Varsity Stripe Contrast Neck Fleece Sweatshirt",
                        CategoryName = "Sweatshirts",
                        CategorySlug = "sweatshirts",
                        Price = 1299,
                        OriginalPrice = 2599,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/sweatshirts",
                        IsUploadingSoon = true,
                        Badge = "RETRO STYLE",
                        Sizes = new List<string> { "M", "L", "XL" },
                        Colors = new List<string> { "Navy/White", "Maroon/Gold", "Black/Grey" }
                    },
                    new()
                    {
                        Id = "sweatshirts_p4",
                        Name = "Raglan Sleeve Active Breathable Sweatshirt",
                        CategoryName = "Sweatshirts",
                        CategorySlug = "sweatshirts",
                        Price = 1099,
                        OriginalPrice = 2199,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/sweatshirts",
                        IsUploadingSoon = true,
                        Badge = "EVERYDAY COMFORT",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Dark Grey", "Charcoal", "Olive" }
                    }
                }
            },
            new()
            {
                CategorySlug = "jeans",
                CategoryName = "Jeans",
                Emoji = "👖",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Jeans",
                    TitleHighlight = "DURABLE STRETCH DENIM • SHAPE RETENTION",
                    Subtitle = "Engineered with 4-way stretch denim and reinforced stitching for maximum mobility across campus.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "Power Stretch Denim", "Reinforced Rivets", "Fade-Resistant Wash", "YKK Heavy Zipper" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated jeans styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "jeans_s1", Name = "Casual Slim Fit", Slug = "casual-slim-fit", Emoji = "👖", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "jeans_s2", Name = "Regular Straight Leg", Slug = "regular-straight-leg", Emoji = "👖", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "jeans_s3", Name = "Party Distressed & Ripped", Slug = "party-distressed", Emoji = "✨", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "jeans_s4", Name = "Relaxed Baggy Fit", Slug = "relaxed-baggy", Emoji = "🛹", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "jeans_s5", Name = "Tapered Ankle Fit", Slug = "tapered-ankle", Emoji = "👟", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "jeans_s6", Name = "Dark Wash Smart Denims", Slug = "dark-wash-smart", Emoji = "👔", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "jeans_p1",
                        Name = "Motion-Flex 4-Way Stretch Slim Tapered Jeans",
                        CategoryName = "Jeans",
                        CategorySlug = "jeans",
                        Price = 1499,
                        OriginalPrice = 2999,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/jeans",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "30", "32", "34", "36", "38" },
                        Colors = new List<string> { "Dark Indigo", "Vintage Blue", "Jet Black" }
                    },
                    new()
                    {
                        Id = "jeans_p2",
                        Name = "Classic Regular Fit Heavyweight Denim Jeans",
                        CategoryName = "Jeans",
                        CategorySlug = "jeans",
                        Price = 1399,
                        OriginalPrice = 2799,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/jeans",
                        IsUploadingSoon = true,
                        Badge = "EVERYDAY DURABLE",
                        Sizes = new List<string> { "30", "32", "34", "36", "38" },
                        Colors = new List<string> { "Mid Blue", "Stone Wash", "Raw Indigo" }
                    },
                    new()
                    {
                        Id = "jeans_p3",
                        Name = "Modern Relaxed Baggy Skate Denim Jeans",
                        CategoryName = "Jeans",
                        CategorySlug = "jeans",
                        Price = 1699,
                        OriginalPrice = 3499,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/jeans",
                        IsUploadingSoon = true,
                        Badge = "STREETWEAR",
                        Sizes = new List<string> { "30", "32", "34", "36" },
                        Colors = new List<string> { "Light Blue Acid", "Washed Black" }
                    },
                    new()
                    {
                        Id = "jeans_p4",
                        Name = "Clean Dark Wash Smart Casual Presentation Jeans",
                        CategoryName = "Jeans",
                        CategorySlug = "jeans",
                        Price = 1599,
                        OriginalPrice = 3199,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/jeans",
                        IsUploadingSoon = true,
                        Badge = "OFFICE READY",
                        Sizes = new List<string> { "30", "32", "34", "36", "38" },
                        Colors = new List<string> { "Inky Black", "Deep Blue Navy" }
                    }
                }
            },
            new()
            {
                CategorySlug = "trousers",
                CategoryName = "Trousers",
                Emoji = "👖",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Trousers",
                    TitleHighlight = "SMART CASUAL & OFFICE CHINOS • WRINKLE-FREE",
                    Subtitle = "Tailored stretch chinos, lightweight linen trousers, and pleated smart slacks designed for presentations, networking events, and casual Fridays.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "Wrinkle-Resistant Twill", "Hidden Stretch Waistband", "Tailored Ankle Crop", "Breathable Cotton Blend" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated trousers styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "trousers_s1", Name = "Casual Stretch Chinos", Slug = "casual-stretch-chinos", Emoji = "👖", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "trousers_s2", Name = "Formal Office Trousers", Slug = "formal-office-trousers", Emoji = "👔", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "trousers_s3", Name = "Linen Vacation Trousers", Slug = "linen-vacation-trousers", Emoji = "🌿", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "trousers_s4", Name = "Pleated Dress Pants", Slug = "pleated-dress-pants", Emoji = "🎩", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "trousers_s5", Name = "Slim Fit Smart Trousers", Slug = "slim-smart-trousers", Emoji = "✨", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "trousers_s6", Name = "Flat-Front Commuter Pants", Slug = "flat-front-commuter", Emoji = "🎒", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "trousers_p1",
                        Name = "Everyday Comfort Stretch Cotton Chino Trousers",
                        CategoryName = "Trousers",
                        CategorySlug = "trousers",
                        Price = 1299,
                        OriginalPrice = 2599,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/trousers",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "30", "32", "34", "36", "38" },
                        Colors = new List<string> { "Khaki", "Navy Blue", "Olive Green", "Charcoal" }
                    },
                    new()
                    {
                        Id = "trousers_p2",
                        Name = "Formal Slim Fit Poly-Viscose Formal Trousers",
                        CategoryName = "Trousers",
                        CategorySlug = "trousers",
                        Price = 1399,
                        OriginalPrice = 2799,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/trousers",
                        IsUploadingSoon = true,
                        Badge = "INTERVIEW ESSENTIAL",
                        Sizes = new List<string> { "30", "32", "34", "36", "38" },
                        Colors = new List<string> { "Charcoal Grey", "Jet Black", "Navy" }
                    },
                    new()
                    {
                        Id = "trousers_p3",
                        Name = "Pleated Wide-Leg Vintage Smart Casual Trousers",
                        CategoryName = "Trousers",
                        CategorySlug = "trousers",
                        Price = 1699,
                        OriginalPrice = 3399,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/trousers",
                        IsUploadingSoon = true,
                        Badge = "TRENDING",
                        Sizes = new List<string> { "30", "32", "34", "36" },
                        Colors = new List<string> { "Taupe", "Espresso Brown", "Slate Grey" }
                    },
                    new()
                    {
                        Id = "trousers_p4",
                        Name = "Breathable Cotton Linen Relaxed Drawstring Trousers",
                        CategoryName = "Trousers",
                        CategorySlug = "trousers",
                        Price = 1499,
                        OriginalPrice = 2999,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/trousers",
                        IsUploadingSoon = true,
                        Badge = "SUMMER TRAVEL",
                        Sizes = new List<string> { "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Beige", "White", "Navy" }
                    }
                }
            },
            new()
            {
                CategorySlug = "pants",
                CategoryName = "Pants",
                Emoji = "👖",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Pants",
                    TitleHighlight = "MULTIPLE POCKETS • RUGGED CAMPUS UTILITY",
                    Subtitle = "Heavy-duty cargo pants, relaxed utility joggers, and technical track pants equipped with deep secure pockets for your passport and gadgets.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "Ripstop Cotton Twill", "6-8 Secure Pockets", "Gusseted Crotch", "Elastic Cuffed Ankle" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated pants styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "pants_s1", Name = "Casual Cargo Pants", Slug = "casual-cargo-pants", Emoji = "👖", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "pants_s2", Name = "Streetwear Joggers", Slug = "streetwear-joggers", Emoji = "🛹", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "pants_s3", Name = "Formal Work Slacks", Slug = "formal-work-slacks", Emoji = "👔", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "pants_s4", Name = "Active Track Pants", Slug = "active-track-pants", Emoji = "🏃", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "pants_s5", Name = "Techwear Multi-Pocket", Slug = "techwear-multi-pocket", Emoji = "⚙️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "pants_s6", Name = "Relaxed Utility Trousers", Slug = "relaxed-utility", Emoji = "🎒", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "pants_p1",
                        Name = "Military Spec 8-Pocket Heavy Duty Cargo Pants",
                        CategoryName = "Pants",
                        CategorySlug = "pants",
                        Price = 1599,
                        OriginalPrice = 3199,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/pants",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "30", "32", "34", "36", "38" },
                        Colors = new List<string> { "Army Green", "Desert Sand", "Tactical Black" }
                    },
                    new()
                    {
                        Id = "pants_p2",
                        Name = "Heavyweight French Terry Slim Fit Jogger Pants",
                        CategoryName = "Pants",
                        CategorySlug = "pants",
                        Price = 1199,
                        OriginalPrice = 2399,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/pants",
                        IsUploadingSoon = true,
                        Badge = "CAMPUS COMFORT",
                        Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Black", "Heather Grey", "Olive" }
                    },
                    new()
                    {
                        Id = "pants_p3",
                        Name = "Tactical Water-Resistant Multi-Pocket Techwear Pants",
                        CategoryName = "Pants",
                        CategorySlug = "pants",
                        Price = 1799,
                        OriginalPrice = 3599,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/pants",
                        IsUploadingSoon = true,
                        Badge = "TRAVEL TECH",
                        Sizes = new List<string> { "30", "32", "34", "36" },
                        Colors = new List<string> { "Stealth Black", "Gunmetal Grey" }
                    },
                    new()
                    {
                        Id = "pants_p4",
                        Name = "Cotton Stretch Relaxed Commuter Drawstring Pants",
                        CategoryName = "Pants",
                        CategorySlug = "pants",
                        Price = 1299,
                        OriginalPrice = 2599,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/pants",
                        IsUploadingSoon = true,
                        Badge = "EVERYDAY EASE",
                        Sizes = new List<string> { "M", "L", "XL" },
                        Colors = new List<string> { "Navy", "Charcoal", "Khaki" }
                    }
                }
            },
            new()
            {
                CategorySlug = "innerwear",
                CategoryName = "Innerwear",
                Emoji = "🩲",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Innerwear",
                    TitleHighlight = "100% ORGANIC COTTON • ANTI-CHAFE & ODOR-FREE",
                    Subtitle = "Ultra-breathable modal trunks, pure cotton boxers, and seamless athletic briefs designed with anti-roll waistbands for zero irritation.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "Micro-Modal Ultra Soft", "Anti-Bacterial Coating", "Non-Roll Microfiber Waistband", "No-Chafe Flatlock Seams" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated innerwear styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "innerwear_s1", Name = "100% Cotton Boxers", Slug = "cotton-boxers", Emoji = "🩳", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "innerwear_s2", Name = "Stretch Trunks", Slug = "stretch-trunks", Emoji = "🩲", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "innerwear_s3", Name = "Active Moisture Briefs", Slug = "active-briefs", Emoji = "🏃", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "innerwear_s4", Name = "Undershirts & Vests", Slug = "undershirts-vests", Emoji = "👕", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "innerwear_s5", Name = "Seamless Microfiber", Slug = "seamless-microfiber", Emoji = "✨", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "innerwear_s6", Name = "Multipack Basics", Slug = "multipack-basics", Emoji = "📦", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "innerwear_p1",
                        Name = "Micro-Modal Anti-Roll Comfort Trunks (Pack of 3)",
                        CategoryName = "Innerwear",
                        CategorySlug = "innerwear",
                        Price = 899,
                        OriginalPrice = 1799,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/innerwear",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Black/Navy/Grey", "Charcoal/Burgundy/Blue" }
                    },
                    new()
                    {
                        Id = "innerwear_p2",
                        Name = "100% Pure Woven Cotton Button-Fly Boxers (Pack of 2)",
                        CategoryName = "Innerwear",
                        CategorySlug = "innerwear",
                        Price = 699,
                        OriginalPrice = 1399,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/innerwear",
                        IsUploadingSoon = true,
                        Badge = "EVERYDAY VALUE",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Classic Plaid", "Stripe Assorted" }
                    },
                    new()
                    {
                        Id = "innerwear_p3",
                        Name = "Seamless Active Performance Athletic Briefs (Pack of 3)",
                        CategoryName = "Innerwear",
                        CategorySlug = "innerwear",
                        Price = 799,
                        OriginalPrice = 1599,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/innerwear",
                        IsUploadingSoon = true,
                        Badge = "WORKOUT READY",
                        Sizes = new List<string> { "M", "L", "XL" },
                        Colors = new List<string> { "All Black", "Navy/Grey/Black" }
                    },
                    new()
                    {
                        Id = "innerwear_p4",
                        Name = "100% Ribbed Combed Cotton Sleeveless Undershirt Vests (Pack of 3)",
                        CategoryName = "Innerwear",
                        CategorySlug = "innerwear",
                        Price = 599,
                        OriginalPrice = 1199,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/innerwear",
                        IsUploadingSoon = true,
                        Badge = "STUDENT ESSENTIAL",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Pure White", "Black/Grey/White" }
                    }
                }
            },
            new()
            {
                CategorySlug = "sleepwear",
                CategoryName = "Sleepwear",
                Emoji = "🛋️",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Sleepwear",
                    TitleHighlight = "BREATHABLE MODAL • DORM ROOM COMFORT",
                    Subtitle = "Featherlight pajama sets, relaxed flannel lounge pants, and ultra-soft night tees designed for deep restorative sleep after long study sessions.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "100% Brushed Cotton", "Breathable Modal Blend", "Deep Pockets & Drawcord", "Tag-Free Neckband" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated sleepwear styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "sleepwear_s1", Name = "Casual Lounge Pants", Slug = "casual-lounge-pants", Emoji = "👖", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "sleepwear_s2", Name = "Plaid Flannel Pajamas", Slug = "plaid-flannel-pajamas", Emoji = "🛋️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "sleepwear_s3", Name = "Sleepwear Sets (Top + Bottom)", Slug = "sleepwear-sets", Emoji = "👕", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "sleepwear_s4", Name = "Relaxed Night Shorts", Slug = "relaxed-night-shorts", Emoji = "🩳", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "sleepwear_s5", Name = "Modal Sleep Tees", Slug = "modal-sleep-tees", Emoji = "✨", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "sleepwear_s6", Name = "Cozy Dorm Robes", Slug = "dorm-robes", Emoji = "🧖", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "sleepwear_p1",
                        Name = "Ultra-Cozy Plaid Brushed Cotton Flannel Pajama Pants",
                        CategoryName = "Sleepwear",
                        CategorySlug = "sleepwear",
                        Price = 899,
                        OriginalPrice = 1799,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/sleepwear",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Red Plaid", "Navy Check", "Green Tartan" }
                    },
                    new()
                    {
                        Id = "sleepwear_p2",
                        Name = "Modal Cotton Breathable Short Sleeve Sleepwear Set",
                        CategoryName = "Sleepwear",
                        CategorySlug = "sleepwear",
                        Price = 1299,
                        OriginalPrice = 2599,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/sleepwear",
                        IsUploadingSoon = true,
                        Badge = "TOP RATED",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Navy/Charcoal", "Slate Blue/Grey" }
                    },
                    new()
                    {
                        Id = "sleepwear_p3",
                        Name = "Relaxed Knit Cotton Sleep Lounge Shorts (Pack of 2)",
                        CategoryName = "Sleepwear",
                        CategorySlug = "sleepwear",
                        Price = 749,
                        OriginalPrice = 1499,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/sleepwear",
                        IsUploadingSoon = true,
                        Badge = "VALUE PACK",
                        Sizes = new List<string> { "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Grey/Black", "Navy/Olive" }
                    },
                    new()
                    {
                        Id = "sleepwear_p4",
                        Name = "Plush Fleece Dorm Lounging Robe with Belt",
                        CategoryName = "Sleepwear",
                        CategorySlug = "sleepwear",
                        Price = 1499,
                        OriginalPrice = 2999,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/sleepwear",
                        IsUploadingSoon = true,
                        Badge = "WINTER COZY",
                        Sizes = new List<string> { "Free Size / M-XL" },
                        Colors = new List<string> { "Navy Blue", "Dark Charcoal" }
                    }
                }
            },
            new()
            {
                CategorySlug = "shorts",
                CategoryName = "Shorts",
                Emoji = "🩳",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Shorts",
                    TitleHighlight = "LIGHTWEIGHT & QUICK-DRY • MAXIMUM MOBILITY",
                    Subtitle = "Versatile chino shorts, moisture-wicking gym shorts, and utility cargo shorts built for summer semesters, gym workouts, and weekend trips.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "Stretch Cotton Twill", "Water-Repellent Coating", "7-Inch Inseam Modern Cut", "Reinforced Pockets" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated shorts styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "shorts_s1", Name = "Casual Chino Shorts", Slug = "casual-chino-shorts", Emoji = "🩳", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "shorts_s2", Name = "Gym Workout Shorts", Slug = "gym-workout-shorts", Emoji = "🏃", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "shorts_s3", Name = "Utility Cargo Shorts", Slug = "utility-cargo-shorts", Emoji = "🎒", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "shorts_s4", Name = "Denim Summer Shorts", Slug = "denim-summer-shorts", Emoji = "👖", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "shorts_s5", Name = "Sweatshorts Loungewear", Slug = "sweatshorts", Emoji = "🛋️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "shorts_s6", Name = "Quick-Dry Boardshorts", Slug = "quick-dry-boardshorts", Emoji = "🏖️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "shorts_p1",
                        Name = "Everyday 7-Inch Inseam Stretch Chino Shorts",
                        CategoryName = "Shorts",
                        CategorySlug = "shorts",
                        Price = 899,
                        OriginalPrice = 1799,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/shorts",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "30", "32", "34", "36", "38" },
                        Colors = new List<string> { "Navy", "Khaki Tan", "Olive", "Light Grey" }
                    },
                    new()
                    {
                        Id = "shorts_p2",
                        Name = "Dual-Layer 2-in-1 Athletic Gym Running Shorts",
                        CategoryName = "Shorts",
                        CategorySlug = "shorts",
                        Price = 999,
                        OriginalPrice = 1999,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/shorts",
                        IsUploadingSoon = true,
                        Badge = "WORKOUT FAV",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Matte Black", "Charcoal", "Army Green" }
                    },
                    new()
                    {
                        Id = "shorts_p3",
                        Name = "Ripstop Multi-Pocket Utility Cargo Shorts",
                        CategoryName = "Shorts",
                        CategorySlug = "shorts",
                        Price = 1099,
                        OriginalPrice = 2199,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/shorts",
                        IsUploadingSoon = true,
                        Badge = "TRAVEL ESSENTIAL",
                        Sizes = new List<string> { "30", "32", "34", "36" },
                        Colors = new List<string> { "Khaki", "Dark Olive", "Black" }
                    },
                    new()
                    {
                        Id = "shorts_p4",
                        Name = "Ultra-Soft Heavy French Terry Lounge Sweatshorts",
                        CategoryName = "Shorts",
                        CategorySlug = "shorts",
                        Price = 799,
                        OriginalPrice = 1599,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/shorts",
                        IsUploadingSoon = true,
                        Badge = "DORM LOUNGE",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Heather Grey", "Black", "Sand" }
                    }
                }
            },
            new()
            {
                CategorySlug = "socks",
                CategoryName = "Socks",
                Emoji = "🧦",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Socks",
                    TitleHighlight = "ARCH SUPPORT & CUSHIONED SOLE • ANTI-ODOR",
                    Subtitle = "Reinforced heel-and-toe cotton crew socks, invisible loafer socks, and merino wool winter socks engineered to keep your feet dry and comfortable.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "Cushioned Terry Sole", "Arch Compression Band", "Silver Ion Anti-Odor", "Seamless Toe Closure" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated socks styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "socks_s1", Name = "Casual Ankle Socks", Slug = "casual-ankle-socks", Emoji = "🧦", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "socks_s2", Name = "Formal Crew Dress Socks", Slug = "formal-crew-dress-socks", Emoji = "👔", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "socks_s3", Name = "Athletic Cushion Socks", Slug = "athletic-cushion-socks", Emoji = "🏃", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "socks_s4", Name = "Winter Merino Wool Socks", Slug = "winter-merino-wool-socks", Emoji = "❄️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "socks_s5", Name = "Invisible Loafer Socks", Slug = "invisible-loafer-socks", Emoji = "👟", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "socks_s6", Name = "Compression Travel Socks", Slug = "compression-travel-socks", Emoji = "✈️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "socks_p1",
                        Name = "Cushioned Athletic Ankle Socks (Pack of 5)",
                        CategoryName = "Socks",
                        CategorySlug = "socks",
                        Price = 599,
                        OriginalPrice = 1199,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/socks",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "Free Size (UK 6-11)" },
                        Colors = new List<string> { "All Black", "All White", "Assorted Grey/Black/White" }
                    },
                    new()
                    {
                        Id = "socks_p2",
                        Name = "Fine Mercerized Cotton Formal Crew Socks (Pack of 3)",
                        CategoryName = "Socks",
                        CategorySlug = "socks",
                        Price = 499,
                        OriginalPrice = 999,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/socks",
                        IsUploadingSoon = true,
                        Badge = "FORMAL ESSENTIAL",
                        Sizes = new List<string> { "Free Size (UK 7-11)" },
                        Colors = new List<string> { "Navy/Charcoal/Black" }
                    },
                    new()
                    {
                        Id = "socks_p3",
                        Name = "Sub-Zero Thermal Merino Wool Winter Socks (Pack of 2)",
                        CategoryName = "Socks",
                        CategorySlug = "socks",
                        Price = 799,
                        OriginalPrice = 1599,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/socks",
                        IsUploadingSoon = true,
                        Badge = "WINTER MUST-HAVE",
                        Sizes = new List<string> { "Free Size (UK 7-11)" },
                        Colors = new List<string> { "Charcoal/Navy", "Black/Brown" }
                    },
                    new()
                    {
                        Id = "socks_p4",
                        Name = "Non-Slip Silicone Grip Invisible Loafer Socks (Pack of 4)",
                        CategoryName = "Socks",
                        CategorySlug = "socks",
                        Price = 449,
                        OriginalPrice = 899,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/socks",
                        IsUploadingSoon = true,
                        Badge = "NO-SHOW FIT",
                        Sizes = new List<string> { "Free Size (UK 6-10)" },
                        Colors = new List<string> { "Black/Grey/Beige/White" }
                    }
                }
            },
            new()
            {
                CategorySlug = "caps",
                CategoryName = "Caps",
                Emoji = "🧢",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Caps",
                    TitleHighlight = "UV SUN PROTECTION • ADJUSTABLE STRAPBACK",
                    Subtitle = "Structured baseball caps, retro snapbacks, breathable running caps, and winter beanies designed to shield you from sun, wind, and cold.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "100% Cotton Twill", "UPF 50+ Sun Protection", "Metal Clasp Adjustable Strap", "Moisture-Wicking Sweatband" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated caps styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "caps_s1", Name = "Casual Baseball Caps", Slug = "casual-baseball-caps", Emoji = "🧢", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "caps_s2", Name = "Streetwear Snapbacks", Slug = "streetwear-snapbacks", Emoji = "🛹", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "caps_s3", Name = "Winter Knit Beanies", Slug = "winter-knit-beanies", Emoji = "❄️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "caps_s4", Name = "Outdoor Bucket Hats", Slug = "outdoor-bucket-hats", Emoji = "🌿", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "caps_s5", Name = "Athletic Running Caps", Slug = "athletic-running-caps", Emoji = "🏃", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "caps_s6", Name = "Trucker Mesh Caps", Slug = "trucker-mesh-caps", Emoji = "🚚", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "caps_p1",
                        Name = "Classic 6-Panel Washed Cotton Dad Cap",
                        CategoryName = "Caps",
                        CategorySlug = "caps",
                        Price = 599,
                        OriginalPrice = 1199,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/caps",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "One Size (Adjustable)" },
                        Colors = new List<string> { "Washed Black", "Vintage Navy", "Olive Green", "Beige" }
                    },
                    new()
                    {
                        Id = "caps_p2",
                        Name = "Chunky Ribbed Knit Winter Thermal Beanie",
                        CategoryName = "Caps",
                        CategorySlug = "caps",
                        Price = 499,
                        OriginalPrice = 999,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/caps",
                        IsUploadingSoon = true,
                        Badge = "WINTER ESSENTIAL",
                        Sizes = new List<string> { "One Size (Universal Fit)" },
                        Colors = new List<string> { "Charcoal", "Black", "Burgundy", "Mustard" }
                    },
                    new()
                    {
                        Id = "caps_p3",
                        Name = "Lightweight Laser-Perforated Athletic Running Cap",
                        CategoryName = "Caps",
                        CategorySlug = "caps",
                        Price = 699,
                        OriginalPrice = 1399,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/caps",
                        IsUploadingSoon = true,
                        Badge = "WORKOUT FAV",
                        Sizes = new List<string> { "One Size (Adjustable Strap)" },
                        Colors = new List<string> { "Matte Black", "White", "Navy" }
                    },
                    new()
                    {
                        Id = "caps_p4",
                        Name = "Reversible 100% Cotton Outdoor Bucket Hat",
                        CategoryName = "Caps",
                        CategorySlug = "caps",
                        Price = 649,
                        OriginalPrice = 1299,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/caps",
                        IsUploadingSoon = true,
                        Badge = "TRENDING",
                        Sizes = new List<string> { "M/L", "L/XL" },
                        Colors = new List<string> { "Black/Beige", "Navy/Olive" }
                    }
                }
            },
            new()
            {
                CategorySlug = "towels",
                CategoryName = "Towels",
                Emoji = "🧖",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Towels",
                    TitleHighlight = "HIGH ABSORBENCY • QUICK-DRY MICROFIBER",
                    Subtitle = "Ultra-compact microfiber travel towels, plush 600 GSM bath sheets, and anti-microbial gym towels designed for easy dorm packing and quick drying.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "Fast Dry in 30 Mins", "Ultra Compact Packing", "Anti-Bacterial Silver Treated", "Super Soft Zero Twist" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated towels styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "towels_s1", Name = "Quick-Dry Travel Towels", Slug = "quick-dry-travel-towels", Emoji = "🎒", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "towels_s2", Name = "Luxury Plush Bath Sheets", Slug = "luxury-plush-bath-sheets", Emoji = "🧖", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "towels_s3", Name = "Compact Gym Workout Towels", Slug = "compact-gym-towels", Emoji = "🏃", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "towels_s4", Name = "Anti-Bacterial Campus Towels", Slug = "anti-bacterial-towels", Emoji = "🛡️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "towels_s5", Name = "Hand & Face Towels Pack", Slug = "hand-face-towels", Emoji = "🧼", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "towels_s6", Name = "Beach & Pool Extra Large", Slug = "beach-pool-towels", Emoji = "🏖️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "towels_p1",
                        Name = "Ultra-Compact Fast-Drying Microfiber Travel Towel (XL 150x80cm)",
                        CategoryName = "Towels",
                        CategorySlug = "towels",
                        Price = 699,
                        OriginalPrice = 1399,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/towels",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "XL (150x80 cm)" },
                        Colors = new List<string> { "Navy Blue", "Teal", "Charcoal Grey", "Olive" }
                    },
                    new()
                    {
                        Id = "towels_p2",
                        Name = "100% Turkish Combed Cotton 600 GSM Plush Bath Towel",
                        CategoryName = "Towels",
                        CategorySlug = "towels",
                        Price = 899,
                        OriginalPrice = 1799,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/towels",
                        IsUploadingSoon = true,
                        Badge = "DORM LUXURY",
                        Sizes = new List<string> { "Large (140x70 cm)" },
                        Colors = new List<string> { "Classic White", "Charcoal", "Ocean Blue", "Sand" }
                    },
                    new()
                    {
                        Id = "towels_p3",
                        Name = "Magnetic Grip Anti-Microbial Gym Workout Towel",
                        CategoryName = "Towels",
                        CategorySlug = "towels",
                        Price = 499,
                        OriginalPrice = 999,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/towels",
                        IsUploadingSoon = true,
                        Badge = "GYM ESSENTIAL",
                        Sizes = new List<string> { "Gym Size (90x40 cm)" },
                        Colors = new List<string> { "Black", "Gunmetal Grey" }
                    },
                    new()
                    {
                        Id = "towels_p4",
                        Name = "100% Cotton Waffle Weave Quick-Drying Hand Towels (Pack of 3)",
                        CategoryName = "Towels",
                        CategorySlug = "towels",
                        Price = 549,
                        OriginalPrice = 1099,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/towels",
                        IsUploadingSoon = true,
                        Badge = "VALUE PACK",
                        Sizes = new List<string> { "Hand Size (60x40 cm)" },
                        Colors = new List<string> { "White/Grey/Charcoal" }
                    }
                }
            },
            new()
            {
                CategorySlug = "belts",
                CategoryName = "Belts",
                Emoji = "🥋",
                HeroBanner = new FashionMenClothingSubcategoryHeroDto
                {
                    Title = "Men's Belts",
                    TitleHighlight = "100% GENUINE LEATHER • DURABLE ALLOY BUCKLE",
                    Subtitle = "Classic formal leather dress belts, reversible dual-tone belts, and rugged casual canvas webbing belts built to last your entire degree.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    ThemeColor = "#0284c7",
                    BadgeText = "CAMPUS FASHION",
                    Badges = new List<string> { "100% Full-Grain Leather", "Scratch-Resistant Zinc Buckle", "Reversible Dual-Color", "5 Custom Sizing Holes" }
                },
                ShopByStyleSection = new FashionShopByStyleSectionDto
                {
                    IsEnabled = true,
                    Title = "Shop by Category",
                    Subtitle = "Curated belts styles for everyday campus, formal meetings, and weekend outings.",
                    Styles = new List<FashionStyleItemDto>
                    {
                        new() { Id = "belts_s1", Name = "Formal Leather Belts", Slug = "formal-leather-belts", Emoji = "👔", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "belts_s2", Name = "Casual Canvas Webbing", Slug = "casual-canvas-webbing", Emoji = "👖", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "belts_s3", Name = "Party Reversible Belts", Slug = "party-reversible-belts", Emoji = "✨", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "belts_s4", Name = "Automatic Ratchet Belts", Slug = "automatic-ratchet-belts", Emoji = "⚙️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "belts_s5", Name = "Braided Stretch Belts", Slug = "braided-stretch-belts", Emoji = "👟", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "belts_s6", Name = "Heavy-Duty Tactical Belts", Slug = "heavy-duty-tactical", Emoji = "🎒", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
                    }
                },
                StyleTypes = new List<string> { "Casual Wear", "Formal Wear", "Party Wear", "Streetwear", "Gym & Active", "Lounge & Sleep" },
                Products = new List<FashionProductItemDto>
                {
                    new()
                    {
                        Id = "belts_p1",
                        Name = "100% Full-Grain Leather Reversible Dual-Color Belt (Black / Brown)",
                        CategoryName = "Belts",
                        CategorySlug = "belts",
                        Price = 899,
                        OriginalPrice = 1799,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/belts",
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER",
                        Sizes = new List<string> { "30-34", "36-40", "42-46" },
                        Colors = new List<string> { "Black/Brown" }
                    },
                    new()
                    {
                        Id = "belts_p2",
                        Name = "Automatic Click-Ratchet Exact Fit Leather Belt",
                        CategoryName = "Belts",
                        CategorySlug = "belts",
                        Price = 999,
                        OriginalPrice = 1999,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/belts",
                        IsUploadingSoon = true,
                        Badge = "MICRO-ADJUSTABLE",
                        Sizes = new List<string> { "Custom Cut to Fit (28-44)" },
                        Colors = new List<string> { "Matte Black", "Cognac Brown" }
                    },
                    new()
                    {
                        Id = "belts_p3",
                        Name = "Heavy-Duty Military Nylon Canvas Webbing Belt",
                        CategoryName = "Belts",
                        CategorySlug = "belts",
                        Price = 499,
                        OriginalPrice = 999,
                        Rating = 4.8m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/belts",
                        IsUploadingSoon = true,
                        Badge = "CASUAL VALUE",
                        Sizes = new List<string> { "Free Size (Up to 44 Inches)" },
                        Colors = new List<string> { "Tactical Black", "Army Green", "Khaki Tan" }
                    },
                    new()
                    {
                        Id = "belts_p4",
                        Name = "Braided Elastic Stretch Woven Belt with Leather Trim",
                        CategoryName = "Belts",
                        CategorySlug = "belts",
                        Price = 649,
                        OriginalPrice = 1299,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        ProductUrl = "/category/fashion/men-clothing/belts",
                        IsUploadingSoon = true,
                        Badge = "COMFORT STRETCH",
                        Sizes = new List<string> { "M (30-34)", "L (36-40)" },
                        Colors = new List<string> { "Navy/Tan", "Black/Grey", "All Black" }
                    }
                }
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

    public FashionFestivalThemeDto FestivalTheme { get; set; } = new();

    public FashionHeroBannerDto HeroBanner { get; set; } = new();

    public FashionCategoriesSectionDto CategoriesSection { get; set; } = new();

    public FashionTopPicksSectionDto TopPicksSection { get; set; } = new();

    public FashionTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();

    public FashionBottomBannerDto BottomBanner { get; set; } = new();

    public List<FashionWomenSubcategoryPageDto> SubcategoryPages { get; set; } = new();



    public static FashionWomenClothingDto CreateDefault()

    {

        return new FashionWomenClothingDto

        {

            IsEnabled = true,

            FestivalTheme = new FashionFestivalThemeDto

            {

                IsEnabled = true,

                FestivalName = "Festive Season Edit",

                AnnouncementText = "✨ Campus Festive Collection: Travel-ready styles with special student discounts!",

                BadgeText = "FESTIVAL SPECIAL",

                BannerHighlight = "Up to 50% Off",

                BgGradient = "linear-gradient(135deg, #e11d48 0%, #ea580c 100%)"

            },

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

                    new() { Id = "wc1", Name = "T-Shirts & Tops", Slug = "t-shirts-tops", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },

                    new() { Id = "wc2", Name = "Shirts & Blouses", Slug = "shirts-blouses", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },

                    new() { Id = "wc3", Name = "Jeans", Slug = "jeans", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },

                    new() { Id = "wc4", Name = "Dresses", Slug = "dresses", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },

                    new() { Id = "wc5", Name = "Leggings", Slug = "leggings", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true },

                    new() { Id = "wc6", Name = "Trousers & Pants", Slug = "trousers-pants", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true },

                    new() { Id = "wc7", Name = "Thermals", Slug = "thermals", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 7, IsUploadingSoon = true },

                    new() { Id = "wc8", Name = "Hoodies & Sweatshirts", Slug = "hoodies-sweatshirts", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 8, IsUploadingSoon = true },

                    new() { Id = "wc9", Name = "Jackets & Coats", Slug = "jackets-coats", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 9, IsUploadingSoon = true },

                    new() { Id = "wc10", Name = "Nightwear", Slug = "nightwear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 10, IsUploadingSoon = true },

                    new() { Id = "wc11", Name = "Innerwear", Slug = "innerwear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 11, IsUploadingSoon = true },

                    new() { Id = "wc12", Name = "Activewear", Slug = "activewear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 12, IsUploadingSoon = true },

                    new() { Id = "wc13", Name = "Formal Wear", Slug = "formal-wear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 13, IsUploadingSoon = true },

                    new() { Id = "wc14", Name = "Gym Wear & Sportswear", Slug = "gym-wear-sportswear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 14, IsUploadingSoon = true }

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

                    new() { Id = "wp1", Name = "Oversized Cotton Graphic T-Shirt", CategoryName = "Women Clothing", CategorySlug = "t-shirts-tops", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 128, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/t-shirts-tops", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "XS", "S", "M", "L", "XL" }, Colors = new List<string> { "White", "Black", "Beige" } },

                    new() { Id = "wp2", Name = "Floral Tiered Midi Campus Dress", CategoryName = "Women Clothing", CategorySlug = "dresses", Price = 1299, OriginalPrice = 2499, Rating = 4.9m, ReviewsCount = 96, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/dresses", IsUploadingSoon = true, Badge = "Trending", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Pink", "Navy" } },

                    new() { Id = "wp3", Name = "Relaxed Linen Blend Casual Shirt", CategoryName = "Women Clothing", CategorySlug = "shirts-blouses", Price = 849, OriginalPrice = 1699, Rating = 4.8m, ReviewsCount = 87, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/shirts-blouses", IsUploadingSoon = true, Badge = "Student Pick", Sizes = new List<string> { "XS", "S", "M", "L" }, Colors = new List<string> { "Beige", "Olive", "White" } },

                    new() { Id = "wp4", Name = "High Waist Butter-Soft Leggings", CategoryName = "Women Clothing", CategorySlug = "leggings", Price = 599, OriginalPrice = 1199, Rating = 4.7m, ReviewsCount = 76, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/leggings", IsUploadingSoon = true, Badge = "Comfort Fit", Sizes = new List<string> { "XS", "S", "M", "L", "XL" }, Colors = new List<string> { "Black", "Navy", "Charcoal" } },

                    new() { Id = "wp5", Name = "Fleece Oversized College Hoodie", CategoryName = "Women Clothing", CategorySlug = "hoodies-sweatshirts", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/hoodies-sweatshirts", IsUploadingSoon = true, Badge = "Warm & Cozy", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Heather Grey", "Pink", "Black" } },

                    new() { Id = "wp6", Name = "High-Rise Wide Leg Baggy Jeans", CategoryName = "Women Clothing", CategorySlug = "jeans", Price = 1299, OriginalPrice = 2599, Rating = 4.9m, ReviewsCount = 84, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/jeans", IsUploadingSoon = true, Badge = "Top Rated", Sizes = new List<string> { "28", "30", "32", "34" }, Colors = new List<string> { "Light Blue", "Deep Blue" } },

                    new() { Id = "wp7", Name = "Classic Double-Breasted Trench Coat", CategoryName = "Women Clothing", CategorySlug = "jackets-coats", Price = 2199, OriginalPrice = 4399, Rating = 4.9m, ReviewsCount = 65, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/jackets-coats", IsUploadingSoon = true, Badge = "Travel Luxe", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Beige", "Black" } },

                    new() { Id = "wp8", Name = "Premium Satin Pajama Notch Set", CategoryName = "Women Clothing", CategorySlug = "nightwear", Price = 999, OriginalPrice = 1999, Rating = 4.7m, ReviewsCount = 78, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/nightwear", IsUploadingSoon = true, Badge = "Sleep Comfort", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Pink", "Emerald", "Navy" } }

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

            },

            SubcategoryPages = CreateDefaultSubcategoryPages()

        };

    }



    public static List<FashionWomenSubcategoryPageDto> CreateDefaultSubcategoryPages()

    {

        return new List<FashionWomenSubcategoryPageDto>

        {

            new()

            {

                CategorySlug = "t-shirts-tops",

                CategoryName = "T-Shirts & Tops",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "T-Shirts & Tops",

                    TitleHighlight = "CASUAL ESSENTIALS • 100% COTTON",

                    Subtitle = "Explore comfortable oversized tees, crop tops, casual henleys, and breathable daily basics tailored for student campus living.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "tt1", Name = "Oversized Drop-Shoulder Graphic T-Shirt", CategoryName = "T-Shirts & Tops", CategorySlug = "t-shirts-tops", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 142, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/t-shirts-tops", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "XS", "S", "M", "L", "XL" }, Colors = new List<string> { "White", "Black", "Beige" } },

                    new() { Id = "tt2", Name = "Ribbed Slim-Fit Cotton Crop Top", CategoryName = "T-Shirts & Tops", CategorySlug = "t-shirts-tops", Price = 449, OriginalPrice = 899, Rating = 4.7m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/t-shirts-tops", IsUploadingSoon = true, Badge = "Trending", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Pink", "Black", "White" } },

                    new() { Id = "tt3", Name = "Relaxed Crew Neck Breathable Tee", CategoryName = "T-Shirts & Tops", CategorySlug = "t-shirts-tops", Price = 499, OriginalPrice = 999, Rating = 4.9m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/t-shirts-tops", IsUploadingSoon = true, Badge = "Student Pick", Sizes = new List<string> { "XS", "S", "M", "L", "XL", "XXL" }, Colors = new List<string> { "Navy", "White", "Olive" } },

                    new() { Id = "tt4", Name = "Striped Boxy Campus T-Shirt", CategoryName = "T-Shirts & Tops", CategorySlug = "t-shirts-tops", Price = 549, OriginalPrice = 1099, Rating = 4.6m, ReviewsCount = 85, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/t-shirts-tops", IsUploadingSoon = true, Badge = "New Arrival", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Navy", "Beige" } },

                    new() { Id = "tt5", Name = "Long Sleeve Thermal Henley Top", CategoryName = "T-Shirts & Tops", CategorySlug = "t-shirts-tops", Price = 699, OriginalPrice = 1399, Rating = 4.8m, ReviewsCount = 67, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/t-shirts-tops", IsUploadingSoon = true, Badge = "Warm Layer", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Maroon", "Grey", "Black" } }

                }

            },

            new()

            {

                CategorySlug = "shirts-blouses",

                CategoryName = "Shirts & Blouses",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Shirts & Blouses",

                    TitleHighlight = "ELEGANT & CRISP • ALL-SEASON",

                    Subtitle = "Smart button-downs, floral blouses, and breezy linen shirts for lectures, presentations, and social meetups.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "sb1", Name = "Classic Oxford Cotton Button-Down Shirt", CategoryName = "Shirts & Blouses", CategorySlug = "shirts-blouses", Price = 899, OriginalPrice = 1799, Rating = 4.9m, ReviewsCount = 115, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/shirts-blouses", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "White", "Light Blue" } },

                    new() { Id = "sb2", Name = "Relaxed Linen Blend Casual Shirt", CategoryName = "Shirts & Blouses", CategorySlug = "shirts-blouses", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 92, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/shirts-blouses", IsUploadingSoon = true, Badge = "Student Pick", Sizes = new List<string> { "XS", "S", "M", "L" }, Colors = new List<string> { "Beige", "Olive", "White" } },

                    new() { Id = "sb3", Name = "Floral Print Chiffon Blouse", CategoryName = "Shirts & Blouses", CategorySlug = "shirts-blouses", Price = 799, OriginalPrice = 1599, Rating = 4.7m, ReviewsCount = 74, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/shirts-blouses", IsUploadingSoon = true, Badge = "Trending", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Pink", "Navy" } },

                    new() { Id = "sb4", Name = "Satin Finish Formal Presentation Blouse", CategoryName = "Shirts & Blouses", CategorySlug = "shirts-blouses", Price = 1099, OriginalPrice = 2199, Rating = 4.9m, ReviewsCount = 88, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/shirts-blouses", IsUploadingSoon = true, Badge = "Formal Star", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Black", "Beige" } }

                }

            },

            new()

            {

                CategorySlug = "jeans",

                CategoryName = "Jeans",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Jeans",

                    TitleHighlight = "DURABLE DENIM • PERFECT FIT",

                    Subtitle = "From relaxed mom fits to wide-leg street styles and stretch skinnies — premium durable denim built for campus adventures.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "jn1", Name = "High-Rise Wide Leg Baggy Jeans", CategoryName = "Jeans", CategorySlug = "jeans", Price = 1299, OriginalPrice = 2599, Rating = 4.9m, ReviewsCount = 186, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/jeans", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "28", "30", "32", "34" }, Colors = new List<string> { "Light Blue", "Deep Blue" } },

                    new() { Id = "jn2", Name = "Vintage 90s Straight Fit Denim", CategoryName = "Jeans", CategorySlug = "jeans", Price = 1399, OriginalPrice = 2799, Rating = 4.8m, ReviewsCount = 104, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/jeans", IsUploadingSoon = true, Badge = "Retro Style", Sizes = new List<string> { "28", "30", "32" }, Colors = new List<string> { "Medium Blue", "Charcoal" } },

                    new() { Id = "jn3", Name = "Ankle-Length Super Stretch Skinny Jeans", CategoryName = "Jeans", CategorySlug = "jeans", Price = 1099, OriginalPrice = 2199, Rating = 4.7m, ReviewsCount = 129, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/jeans", IsUploadingSoon = true, Badge = "Super Stretch", Sizes = new List<string> { "26", "28", "30", "32" }, Colors = new List<string> { "Dark Navy", "Black" } },

                    new() { Id = "jn4", Name = "Relaxed High-Waist Mom Fit Jeans", CategoryName = "Jeans", CategorySlug = "jeans", Price = 1249, OriginalPrice = 2499, Rating = 4.9m, ReviewsCount = 94, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/jeans", IsUploadingSoon = true, Badge = "Trending", Sizes = new List<string> { "28", "30", "32", "34" }, Colors = new List<string> { "Ice Blue", "Black" } }

                }

            },

            new()

            {

                CategorySlug = "dresses",

                CategoryName = "Dresses",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Dresses",

                    TitleHighlight = "CHIC & EFFORTLESS • TRAVEL-READY",

                    Subtitle = "Flowy midis, elegant shirt dresses, and versatile summer frocks designed for international travel and effortless styling.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "dr1", Name = "Floral Tiered Midi Campus Dress", CategoryName = "Dresses", CategorySlug = "dresses", Price = 1299, OriginalPrice = 2499, Rating = 4.9m, ReviewsCount = 162, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/dresses", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Pink", "Navy" } },

                    new() { Id = "dr2", Name = "Ribbed Bodycon Casual Day Dress", CategoryName = "Dresses", CategorySlug = "dresses", Price = 899, OriginalPrice = 1799, Rating = 4.7m, ReviewsCount = 95, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/dresses", IsUploadingSoon = true, Badge = "Daily Wear", Sizes = new List<string> { "XS", "S", "M", "L" }, Colors = new List<string> { "Black", "Olive", "Maroon" } },

                    new() { Id = "dr3", Name = "A-Line Cotton Summer Sundress", CategoryName = "Dresses", CategorySlug = "dresses", Price = 1099, OriginalPrice = 2199, Rating = 4.8m, ReviewsCount = 88, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/dresses", IsUploadingSoon = true, Badge = "Summer Glow", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Beige", "White" } },

                    new() { Id = "dr4", Name = "Elegant Tie-Belt Linen Shirt Dress", CategoryName = "Dresses", CategorySlug = "dresses", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 72, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/dresses", IsUploadingSoon = true, Badge = "Travel Luxe", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Olive", "Navy" } }

                }

            },

            new()

            {

                CategorySlug = "leggings",

                CategoryName = "Leggings",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Leggings",

                    TitleHighlight = "BUTTER-SOFT • 4-WAY STRETCH",

                    Subtitle = "High-waisted compression tights, pocket leggings, and fleece-lined winter options for everyday campus comfort.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "lg1", Name = "High-Waist Seamless Butter-Soft Leggings", CategoryName = "Leggings", CategorySlug = "leggings", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 205, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/leggings", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "XS", "S", "M", "L", "XL" }, Colors = new List<string> { "Black", "Charcoal", "Navy" } },

                    new() { Id = "lg2", Name = "Dual-Pocket Ankle-Length Yoga Leggings", CategoryName = "Leggings", CategorySlug = "leggings", Price = 699, OriginalPrice = 1399, Rating = 4.9m, ReviewsCount = 140, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/leggings", IsUploadingSoon = true, Badge = "With Pockets", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Olive", "Maroon", "Black" } },

                    new() { Id = "lg3", Name = "Fleece-Lined Winter Thermal Leggings", CategoryName = "Leggings", CategorySlug = "leggings", Price = 799, OriginalPrice = 1599, Rating = 4.9m, ReviewsCount = 178, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/leggings", IsUploadingSoon = true, Badge = "Winter Warm", Sizes = new List<string> { "Free Size", "S", "M", "L" }, Colors = new List<string> { "Black", "Dark Grey" } },

                    new() { Id = "lg4", Name = "4-Way Stretch High Compression Tights", CategoryName = "Leggings", CategorySlug = "leggings", Price = 849, OriginalPrice = 1699, Rating = 4.7m, ReviewsCount = 89, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/leggings", IsUploadingSoon = true, Badge = "Active Flex", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Black", "Navy" } }

                }

            },

            new()

            {

                CategorySlug = "trousers-pants",

                CategoryName = "Trousers & Pants",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Trousers & Pants",

                    TitleHighlight = "STRUCTURED & RELAXED • VERSATILE",

                    Subtitle = "Tailored pleated trousers, flowy palazzos, and street-smart utility cargos made for presentation days and weekend hangouts.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "tp1", Name = "High-Waist Pleated Formal Trousers", CategoryName = "Trousers & Pants", CategorySlug = "trousers-pants", Price = 1199, OriginalPrice = 2399, Rating = 4.9m, ReviewsCount = 132, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/trousers-pants", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "28", "30", "32", "34" }, Colors = new List<string> { "Beige", "Black", "Charcoal" } },

                    new() { Id = "tp2", Name = "Wide-Leg Flowy Linen Palazzos", CategoryName = "Trousers & Pants", CategorySlug = "trousers-pants", Price = 899, OriginalPrice = 1799, Rating = 4.8m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/trousers-pants", IsUploadingSoon = true, Badge = "Breezy Fit", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "White", "Beige", "Olive" } },

                    new() { Id = "tp3", Name = "Multi-Pocket Utility Street Cargo Pants", CategoryName = "Trousers & Pants", CategorySlug = "trousers-pants", Price = 1299, OriginalPrice = 2599, Rating = 4.8m, ReviewsCount = 112, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/trousers-pants", IsUploadingSoon = true, Badge = "Street Style", Sizes = new List<string> { "28", "30", "32" }, Colors = new List<string> { "Olive", "Khaki", "Black" } },

                    new() { Id = "tp4", Name = "Straight Fit Ankle Cigarette Pants", CategoryName = "Trousers & Pants", CategorySlug = "trousers-pants", Price = 999, OriginalPrice = 1999, Rating = 4.7m, ReviewsCount = 76, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/trousers-pants", IsUploadingSoon = true, Badge = "Smart Casual", Sizes = new List<string> { "28", "30", "32" }, Colors = new List<string> { "Navy", "Black" } }

                }

            },

            new()

            {

                CategorySlug = "thermals",

                CategoryName = "Thermals",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Thermals",

                    TitleHighlight = "SUB-ZERO PROTECTION • MERINO BLEND",

                    Subtitle = "Stay comfortably warm abroad with ultra-thin, temperature-regulating base layers, thermal tops, and bottom sets.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "th1", Name = "Women's Ultra-Warm Merino Wool Top", CategoryName = "Thermals", CategorySlug = "thermals", Price = 899, OriginalPrice = 1799, Rating = 4.9m, ReviewsCount = 164, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/thermals", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Charcoal", "Off-White" } },

                    new() { Id = "th2", Name = "Seamless Body-Hugging Thermal Bottom", CategoryName = "Thermals", CategorySlug = "thermals", Price = 799, OriginalPrice = 1599, Rating = 4.8m, ReviewsCount = 120, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/thermals", IsUploadingSoon = true, Badge = "Essential", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Charcoal", "Black" } },

                    new() { Id = "th3", Name = "Double-Layer Thermal Suit Set (Top + Bottom)", CategoryName = "Thermals", CategorySlug = "thermals", Price = 1499, OriginalPrice = 2999, Rating = 5.0m, ReviewsCount = 210, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/thermals", IsUploadingSoon = true, Badge = "Full Set Value", Sizes = new List<string> { "S", "M", "L", "XL", "XXL" }, Colors = new List<string> { "Off-White", "Grey" } },

                    new() { Id = "th4", Name = "Lightweight Base Layer Thermal Tee", CategoryName = "Thermals", CategorySlug = "thermals", Price = 649, OriginalPrice = 1299, Rating = 4.7m, ReviewsCount = 85, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/thermals", IsUploadingSoon = true, Badge = "Ultra Thin", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Beige", "Black" } }

                }

            },

            new()

            {

                CategorySlug = "hoodies-sweatshirts",

                CategoryName = "Hoodies & Sweatshirts",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Hoodies & Sweatshirts",

                    TitleHighlight = "OVERSIZED FLEECE • CAMPUS COZY",

                    Subtitle = "Soft fleece pullovers, zip-up terry hoodies, and vintage college crewnecks engineered for cold library study sessions.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "hs1", Name = "Fleece Oversized College Graphic Hoodie", CategoryName = "Hoodies & Sweatshirts", CategorySlug = "hoodies-sweatshirts", Price = 999, OriginalPrice = 1999, Rating = 4.9m, ReviewsCount = 230, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/hoodies-sweatshirts", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Heather Grey", "Pink", "Black" } },

                    new() { Id = "hs2", Name = "Relaxed Crewneck Campus Sweatshirt", CategoryName = "Hoodies & Sweatshirts", CategorySlug = "hoodies-sweatshirts", Price = 849, OriginalPrice = 1699, Rating = 4.8m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/hoodies-sweatshirts", IsUploadingSoon = true, Badge = "Student Pick", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Beige", "Navy" } },

                    new() { Id = "hs3", Name = "Cropped Zip-Up Terry Cloth Hoodie", CategoryName = "Hoodies & Sweatshirts", CategorySlug = "hoodies-sweatshirts", Price = 949, OriginalPrice = 1899, Rating = 4.7m, ReviewsCount = 88, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/hoodies-sweatshirts", IsUploadingSoon = true, Badge = "Trending", Sizes = new List<string> { "XS", "S", "M", "L" }, Colors = new List<string> { "Olive", "Charcoal" } },

                    new() { Id = "hs4", Name = "Heavyweight Kangaroo Pocket Warm Hoodie", CategoryName = "Hoodies & Sweatshirts", CategorySlug = "hoodies-sweatshirts", Price = 1199, OriginalPrice = 2399, Rating = 4.9m, ReviewsCount = 175, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/hoodies-sweatshirts", IsUploadingSoon = true, Badge = "Ultra Warm", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Olive", "Black" } }

                }

            },

            new()

            {

                CategorySlug = "jackets-coats",

                CategoryName = "Jackets & Coats",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Jackets & Coats",

                    TitleHighlight = "OUTERWEAR SHIELD • WEATHER-PROOF",

                    Subtitle = "Trench coats, sherpa corduroys, lightweight packable puffers, and rugged denim jackets to conquer any weather abroad.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "jc1", Name = "Classic Double-Breasted Trench Coat", CategoryName = "Jackets & Coats", CategorySlug = "jackets-coats", Price = 2199, OriginalPrice = 4399, Rating = 4.9m, ReviewsCount = 158, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/jackets-coats", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Beige", "Black" } },

                    new() { Id = "jc2", Name = "Sherpa-Lined Warm Corduroy Jacket", CategoryName = "Jackets & Coats", CategorySlug = "jackets-coats", Price = 1799, OriginalPrice = 3599, Rating = 4.8m, ReviewsCount = 114, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/jackets-coats", IsUploadingSoon = true, Badge = "Winter Trend", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Beige", "Olive" } },

                    new() { Id = "jc3", Name = "Lightweight Packable Down Puffer Jacket", CategoryName = "Jackets & Coats", CategorySlug = "jackets-coats", Price = 1899, OriginalPrice = 3799, Rating = 4.9m, ReviewsCount = 192, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/jackets-coats", IsUploadingSoon = true, Badge = "Travel Essential", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Black", "Maroon" } },

                    new() { Id = "jc4", Name = "Vintage Wash Trucker Denim Jacket", CategoryName = "Jackets & Coats", CategorySlug = "jackets-coats", Price = 1599, OriginalPrice = 3199, Rating = 4.7m, ReviewsCount = 86, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/jackets-coats", IsUploadingSoon = true, Badge = "Timeless", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Light Blue", "Deep Blue" } }

                }

            },

            new()

            {

                CategorySlug = "nightwear",

                CategoryName = "Nightwear",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Nightwear",

                    TitleHighlight = "REST & RECHARGE • SILKY SOFT",

                    Subtitle = "Satin lounge sets, cozy flannel pajamas, and breathable sleep tees for restorative sleep after busy university days.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "nw1", Name = "Premium Satin Pajama Notch Collar Set", CategoryName = "Nightwear", CategorySlug = "nightwear", Price = 999, OriginalPrice = 1999, Rating = 4.9m, ReviewsCount = 138, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/nightwear", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Pink", "Navy", "Beige" } },

                    new() { Id = "nw2", Name = "100% Breathable Cotton Sleep Tee & Shorts", CategoryName = "Nightwear", CategorySlug = "nightwear", Price = 649, OriginalPrice = 1299, Rating = 4.8m, ReviewsCount = 104, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/nightwear", IsUploadingSoon = true, Badge = "Daily Comfort", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Pink", "White" } },

                    new() { Id = "nw3", Name = "Cozy Flannel Plaid Winter Sleepwear Set", CategoryName = "Nightwear", CategorySlug = "nightwear", Price = 1099, OriginalPrice = 2199, Rating = 4.8m, ReviewsCount = 92, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/nightwear", IsUploadingSoon = true, Badge = "Flannel Warmth", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Red", "Navy" } },

                    new() { Id = "nw4", Name = "Rayon Soft Floral Nightdress / Loungewear", CategoryName = "Nightwear", CategorySlug = "nightwear", Price = 699, OriginalPrice = 1399, Rating = 4.7m, ReviewsCount = 68, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/nightwear", IsUploadingSoon = true, Badge = "Ultra Soft", Sizes = new List<string> { "Free Size", "M", "L" }, Colors = new List<string> { "Olive", "Pink" } }

                }

            },

            new()

            {

                CategorySlug = "innerwear",

                CategoryName = "Innerwear",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Innerwear",

                    TitleHighlight = "EVERYDAY FOUNDATION • SEAMLESS",

                    Subtitle = "Seamless T-shirt bras, breathable cotton multipacks, and wire-free lounge bralettes designed for all-day campus comfort.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "in1", Name = "Seamless T-Shirt Bra (Everyday Comfort)", CategoryName = "Innerwear", CategorySlug = "innerwear", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 192, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/innerwear", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "32B", "34B", "36B", "38C" }, Colors = new List<string> { "Beige", "Black", "White" } },

                    new() { Id = "in2", Name = "Cotton Stretch Bikini Panties (Pack of 3)", CategoryName = "Innerwear", CategorySlug = "innerwear", Price = 449, OriginalPrice = 899, Rating = 4.9m, ReviewsCount = 220, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/innerwear", IsUploadingSoon = true, Badge = "Multipack Value", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Pink", "Beige", "White" } },

                    new() { Id = "in3", Name = "Wire-Free Ribbed Lounge Bralette", CategoryName = "Innerwear", CategorySlug = "innerwear", Price = 399, OriginalPrice = 799, Rating = 4.7m, ReviewsCount = 114, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/innerwear", IsUploadingSoon = true, Badge = "Zero Wire", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Beige", "Black", "Grey" } },

                    new() { Id = "in4", Name = "High-Rise Cotton Boy-Shorts (Pack of 2)", CategoryName = "Innerwear", CategorySlug = "innerwear", Price = 429, OriginalPrice = 859, Rating = 4.8m, ReviewsCount = 96, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/innerwear", IsUploadingSoon = true, Badge = "Full Cover", Sizes = new List<string> { "M", "L", "XL" }, Colors = new List<string> { "Black", "Grey" } }

                }

            },

            new()

            {

                CategorySlug = "activewear",

                CategoryName = "Activewear",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Activewear",

                    TitleHighlight = "HIGH IMPACT • QUICK-DRY PERFORMANCE",

                    Subtitle = "Racerback sports bras, active tees, biker shorts, and running jackets built to support your daily campus fitness routine.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "aw1", Name = "Seamless High-Impact Racerback Sports Bra", CategoryName = "Activewear", CategorySlug = "activewear", Price = 699, OriginalPrice = 1399, Rating = 4.9m, ReviewsCount = 168, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/activewear", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Black", "Pink", "Navy" } },

                    new() { Id = "aw2", Name = "Quick-Dry Breathable Active Training Tee", CategoryName = "Activewear", CategorySlug = "activewear", Price = 549, OriginalPrice = 1099, Rating = 4.8m, ReviewsCount = 124, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/activewear", IsUploadingSoon = true, Badge = "Quick Dry", Sizes = new List<string> { "XS", "S", "M", "L", "XL" }, Colors = new List<string> { "White", "Olive", "Black" } },

                    new() { Id = "aw3", Name = "Active High-Waist Biker Shorts with Pocket", CategoryName = "Activewear", CategorySlug = "activewear", Price = 599, OriginalPrice = 1199, Rating = 4.7m, ReviewsCount = 142, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/activewear", IsUploadingSoon = true, Badge = "Trending", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Black", "Maroon" } },

                    new() { Id = "aw4", Name = "Full-Zip Lightweight Running Track Jacket", CategoryName = "Activewear", CategorySlug = "activewear", Price = 1199, OriginalPrice = 2399, Rating = 4.8m, ReviewsCount = 89, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/activewear", IsUploadingSoon = true, Badge = "Performance", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Charcoal", "Pink" } }

                }

            },

            new()

            {

                CategorySlug = "formal-wear",

                CategoryName = "Formal Wear",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Formal Wear",

                    TitleHighlight = "PROFESSIONAL EXCELLENCE • TAILORED",

                    Subtitle = "Tailored office blazers, pencil skirts, crisp poplin shirts, and sharp trousers tailored for interviews, internships, and conferences.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "fw1", Name = "Single-Breasted Tailored Office Blazer", CategoryName = "Formal Wear", CategorySlug = "formal-wear", Price = 1899, OriginalPrice = 3799, Rating = 4.9m, ReviewsCount = 148, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/formal-wear", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Black", "Navy", "Beige" } },

                    new() { Id = "fw2", Name = "Straight Formal Pantsuit Trousers", CategoryName = "Formal Wear", CategorySlug = "formal-wear", Price = 1199, OriginalPrice = 2399, Rating = 4.8m, ReviewsCount = 112, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/formal-wear", IsUploadingSoon = true, Badge = "Sharp Cut", Sizes = new List<string> { "28", "30", "32", "34" }, Colors = new List<string> { "Black", "Navy" } },

                    new() { Id = "fw3", Name = "Collared Crisp Poplin Presentation Shirt", CategoryName = "Formal Wear", CategorySlug = "formal-wear", Price = 949, OriginalPrice = 1899, Rating = 4.9m, ReviewsCount = 95, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/formal-wear", IsUploadingSoon = true, Badge = "Crisp Finish", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "White", "Light Blue" } },

                    new() { Id = "fw4", Name = "Formal Pencil Skirt with Back Vent", CategoryName = "Formal Wear", CategorySlug = "formal-wear", Price = 899, OriginalPrice = 1799, Rating = 4.7m, ReviewsCount = 68, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/formal-wear", IsUploadingSoon = true, Badge = "Classic", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Charcoal", "Black" } }

                }

            },

            new()

            {

                CategorySlug = "gym-wear-sportswear",

                CategoryName = "Gym Wear & Sportswear",

                HeroBanner = new FashionWomenSubcategoryHeroDto

                {

                    Title = "Gym Wear & Sportswear",

                    TitleHighlight = "WORKOUT READY • MAXIMUM FLEX",

                    Subtitle = "2-piece matching ribbed workout sets, moisture-wicking muscle tanks, phone-pocket gym tights, and athletic joggers.",

                    IsUploadingSoon = true

                },

                Products = new List<FashionProductItemDto>

                {

                    new() { Id = "gw1", Name = "Matching 2-Piece Ribbed Workout Gym Set", CategoryName = "Gym Wear & Sportswear", CategorySlug = "gym-wear-sportswear", Price = 1399, OriginalPrice = 2799, Rating = 5.0m, ReviewsCount = 182, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/gym-wear-sportswear", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Beige", "Olive", "Black" } },

                    new() { Id = "gw2", Name = "Moisture-Wicking Athletic Muscle Tank Top", CategoryName = "Gym Wear & Sportswear", CategorySlug = "gym-wear-sportswear", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/gym-wear-sportswear", IsUploadingSoon = true, Badge = "Ultra Light", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "White", "Grey", "Black" } },

                    new() { Id = "gw3", Name = "Gym Compression Leggings with Phone Pocket", CategoryName = "Gym Wear & Sportswear", CategorySlug = "gym-wear-sportswear", Price = 799, OriginalPrice = 1599, Rating = 4.9m, ReviewsCount = 156, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/gym-wear-sportswear", IsUploadingSoon = true, Badge = "Top Pick", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Black", "Navy" } },

                    new() { Id = "gw4", Name = "Drawstring Soft Fleece Athletic Joggers", CategoryName = "Gym Wear & Sportswear", CategorySlug = "gym-wear-sportswear", Price = 899, OriginalPrice = 1799, Rating = 4.7m, ReviewsCount = 120, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-clothing/gym-wear-sportswear", IsUploadingSoon = true, Badge = "All-Day Cozy", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Olive", "Grey", "Black" } }

                }

            }

        };

    }

}



// -------------------------------------------------------------

// HOODIES TEMPLATE DTO (MATCHES SCREENSHOT 3)

// -------------------------------------------------------------


// -------------------------------------------------------------
// MEN FOOTWEAR DTO (MATCHES SCREENSHOT & USER SPEC)
// -------------------------------------------------------------
public sealed class FashionMenFootwearDto
{
    public bool IsEnabled { get; set; } = true;
    public FashionFestivalThemeDto FestivalTheme { get; set; } = new();
    public FashionHeroBannerDto HeroBanner { get; set; } = new();
    public FashionCategoriesSectionDto CategoriesSection { get; set; } = new();
    public FashionTopPicksSectionDto TopPicksSection { get; set; } = new();
    public FashionTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();
    public FashionBottomBannerDto BottomBanner { get; set; } = new();
    public List<FashionMenFootwearSubcategoryPageDto> SubcategoryPages { get; set; } = new();

    public static FashionMenFootwearDto CreateDefault()
    {
        return new FashionMenFootwearDto
        {
            IsEnabled = true,
            FestivalTheme = new FashionFestivalThemeDto
            {
                IsEnabled = true,
                FestivalName = "Campus Walk & Performance Edit",
                AnnouncementText = "👟 Step Up Your Journey: Premium campus footwear with shock absorption & special student discounts!",
                BadgeText = "FOOTWEAR SPECIAL",
                BannerHighlight = "Up to 50% Off",
                BgGradient = "linear-gradient(135deg, #0284c7 0%, #0369a1 100%)"
            },
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Men's Footwear",
                TitleHighlight = "Step into Comfort & Performance",
                Subtitle = "Engineered for international students — all-day campus walking, workouts, and formal presentations.",
                Description = "Lightweight memory-foam cushioning, high-abrasion rubber soles, and breathable weather-ready designs.",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TitleColor = "#0369a1",
                TitleHighlightColor = "#ea580c",
                SubtitleColor = "#334155",
                BannerHeight = 440,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "mfb1", Label = "Shock Absorption", Icon = "shield" },
                    new() { Id = "mfb2", Label = "All-Weather Grip", Icon = "compass" },
                    new() { Id = "mfb3", Label = "Lightweight & Breathable", Icon = "feather" },
                    new() { Id = "mfb4", Label = "Loved by 15K+ Students", Icon = "users" }
                },
                RightCard = new FashionHeroRightCardDto
                {
                    Title = "Walk Far.",
                    TitleHighlight = "Stay Agile.",
                    Features = new List<FashionHeroFeatureDto>
                    {
                        new() { Id = "mff1", Title = "Shock Absorption", Icon = "shield" },
                        new() { Id = "mff2", Title = "All-Weather Grip", Icon = "compass" },
                        new() { Id = "mff3", Title = "Breathable Mesh", Icon = "wind" },
                        new() { Id = "mff4", Title = "All-Day Comfort", Icon = "feather" }
                    }
                }
            },
            CategoriesSection = new FashionCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Categories →",
                ViewAllUrl = "/category/fashion/men-footwear",
                Categories = new List<FashionCategoryItemDto>
                {
                    new() { Id = "mfc1", Name = "Casual Shoes", Slug = "casual-shoes", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },
                    new() { Id = "mfc2", Name = "Sports Shoes", Slug = "sports-shoes", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },
                    new() { Id = "mfc3", Name = "Formal Shoes", Slug = "formal-shoes", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },
                    new() { Id = "mfc4", Name = "Sneakers", Slug = "sneakers", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },
                    new() { Id = "mfc5", Name = "Sandals & Floaters", Slug = "sandals-floaters", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true },
                    new() { Id = "mfc6", Name = "Flip Flops", Slug = "flip-flops", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true },
                    new() { Id = "mfc7", Name = "Socks", Slug = "socks", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 7, IsUploadingSoon = true }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks for You",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/category/fashion/men-footwear",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "mfp1", Name = "Urban Street Low-Top Leather Sneakers", CategoryName = "Men Footwear", CategorySlug = "sneakers", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 210, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sneakers", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "White", "Black", "Grey" } },
                    new() { Id = "mfp2", Name = "Air-Cushion Campus Running Sports Shoes", CategoryName = "Men Footwear", CategorySlug = "sports-shoes", Price = 1799, OriginalPrice = 3499, Rating = 4.8m, ReviewsCount = 185, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sports-shoes", IsUploadingSoon = true, Badge = "Trending", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10", "UK 11", "UK 12" }, Colors = new List<string> { "Black", "Navy", "Neon" } },
                    new() { Id = "mfp3", Name = "Classic Handcrafted Leather Oxford Formal Shoes", CategoryName = "Men Footwear", CategorySlug = "formal-shoes", Price = 2199, OriginalPrice = 4299, Rating = 4.9m, ReviewsCount = 142, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/formal-shoes", IsUploadingSoon = true, Badge = "Formal Star", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "Black", "Dark Tan" } },
                    new() { Id = "mfp4", Name = "Memory Foam Slip-On Casual Canvas Loafers", CategoryName = "Men Footwear", CategorySlug = "casual-shoes", Price = 999, OriginalPrice = 1999, Rating = 4.7m, ReviewsCount = 164, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/casual-shoes", IsUploadingSoon = true, Badge = "Campus Pick", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Navy", "Charcoal", "Beige" } },
                    new() { Id = "mfp5", Name = "All-Terrain Adventure Grip Floaters", CategoryName = "Men Footwear", CategorySlug = "sandals-floaters", Price = 899, OriginalPrice = 1799, Rating = 4.8m, ReviewsCount = 118, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sandals-floaters", IsUploadingSoon = true, Badge = "Rugged Grip", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Olive", "Black" } },
                    new() { Id = "mfp6", Name = "Anti-Skid Ultra-Light Comfort Flip Flops", CategoryName = "Men Footwear", CategorySlug = "flip-flops", Price = 449, OriginalPrice = 899, Rating = 4.6m, ReviewsCount = 95, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/flip-flops", IsUploadingSoon = true, Badge = "Waterproof", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Black", "Blue" } },
                    new() { Id = "mfp7", Name = "5-Pack Anti-Odor Breathable Cotton Ankle Socks", CategoryName = "Men Footwear", CategorySlug = "socks", Price = 399, OriginalPrice = 799, Rating = 4.9m, ReviewsCount = 230, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/socks", IsUploadingSoon = true, Badge = "Value Pack", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Black", "White", "Grey" } }
                }
            },
            TrustBadgesSection = new FashionTrustBadgesSectionDto
            {
                IsEnabled = true,
                Badges = new List<FashionTrustBadgeDto>
                {
                    new() { Id = "mft1", Title = "Curated for Students", Subtitle = "All-day comfort designed for campus walking", Icon = "box" },
                    new() { Id = "mft2", Title = "100% Genuine Quality", Subtitle = "Durable materials & high-traction soles", Icon = "shield" },
                    new() { Id = "mft3", Title = "Affordable Value", Subtitle = "Unmatched student price guarantee", Icon = "tag" },
                    new() { Id = "mft4", Title = "Fast Campus Delivery", Subtitle = "Delivered right to your doorstep", Icon = "truck" },
                    new() { Id = "mft5", Title = "7-Day Easy Returns", Subtitle = "Hassle-free size replacement", Icon = "return" },
                    new() { Id = "mft6", Title = "24/7 Student Support", Subtitle = "Dedicated assistance whenever needed", Icon = "headset" }
                }
            },
            BottomBanner = new FashionBottomBannerDto
            {
                IsEnabled = true,
                QuizTitle = "Not Sure Which Size or Style?",
                QuizSubtitle = "Take our 30-second shoe finder quiz to find your ideal fit",
                QuizButtonText = "Find My Shoe Size",
                QuizButtonUrl = "/find-my-kit",
                DestinationsTitle = "Weather-Ready Footwear for Top Destinations",
                DestinationsSubtitle = "USA, UK, Canada, Australia & Europe",
                BulkTitle = "Group Orders for Student Clubs",
                BulkSubtitle = "Special bulk pricing for university sports teams",
                BulkButtonText = "Contact Us",
                BulkButtonUrl = "/contact"
            },
            SubcategoryPages = CreateDefaultSubcategoryPages()
        };
    }

    public static List<FashionMenFootwearSubcategoryPageDto> CreateDefaultSubcategoryPages()
    {
        return new List<FashionMenFootwearSubcategoryPageDto>
        {
            // 1. Casual Shoes
            new()
            {
                CategorySlug = "casual-shoes",
                CategoryName = "Casual Shoes",
                HeroBanner = new FashionMenFootwearSubcategoryHeroDto
                {
                    Title = "Casual Shoes",
                    TitleHighlight = "EVERYDAY COMFORT • MEMORY FOAM",
                    Subtitle = "Breathable canvas slip-ons, lightweight knit loafers, and casual boat shoes built for lectures and campus strolls.",
                    IsUploadingSoon = true
                },
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "cs1", Name = "Memory Foam Slip-On Canvas Loafers", CategoryName = "Casual Shoes", CategorySlug = "casual-shoes", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/casual-shoes", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Navy", "Charcoal", "Beige" } },
                    new() { Id = "cs2", Name = "Breathable Knit Flexible Walking Shoes", CategoryName = "Casual Shoes", CategorySlug = "casual-shoes", Price = 1199, OriginalPrice = 2399, Rating = 4.7m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/casual-shoes", IsUploadingSoon = true, Badge = "Trending", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "Black", "Grey" } },
                    new() { Id = "cs3", Name = "Casual Suede Handcrafted Driving Loafers", CategoryName = "Casual Shoes", CategorySlug = "casual-shoes", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 112, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/casual-shoes", IsUploadingSoon = true, Badge = "Premium Suede", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Tan", "Brown", "Navy" } },
                    new() { Id = "cs4", Name = "Classic Deck Boat Shoes with Anti-Slip Sole", CategoryName = "Casual Shoes", CategorySlug = "casual-shoes", Price = 1299, OriginalPrice = 2599, Rating = 4.6m, ReviewsCount = 80, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/casual-shoes", IsUploadingSoon = true, Badge = "All-Day Grip", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Brown", "Navy" } }
                }
            },
            // 2. Sports Shoes
            new()
            {
                CategorySlug = "sports-shoes",
                CategoryName = "Sports Shoes",
                HeroBanner = new FashionMenFootwearSubcategoryHeroDto
                {
                    Title = "Sports Shoes",
                    TitleHighlight = "HIGH PERFORMANCE • SHOCK ABSORPTION",
                    Subtitle = "Air-cushioned running trainers, gym trainers, and durable turf turf shoes built for maximum speed and shock protection.",
                    IsUploadingSoon = true
                },
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "ss1", Name = "Air-Cushion Campus Running Sports Shoes", CategoryName = "Sports Shoes", CategorySlug = "sports-shoes", Price = 1799, OriginalPrice = 3499, Rating = 4.9m, ReviewsCount = 220, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sports-shoes", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10", "UK 11", "UK 12" }, Colors = new List<string> { "Black", "Navy", "Neon" } },
                    new() { Id = "ss2", Name = "Ultra-Lightweight Gym Mesh Training Trainers", CategoryName = "Sports Shoes", CategorySlug = "sports-shoes", Price = 1399, OriginalPrice = 2799, Rating = 4.8m, ReviewsCount = 135, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sports-shoes", IsUploadingSoon = true, Badge = "Workout Pro", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "Grey", "Black", "Red" } },
                    new() { Id = "ss3", Name = "High-Traction All-Weather Marathon Runners", CategoryName = "Sports Shoes", CategorySlug = "sports-shoes", Price = 2199, OriginalPrice = 4399, Rating = 5.0m, ReviewsCount = 94, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sports-shoes", IsUploadingSoon = true, Badge = "Pro Runner", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "Blue", "Black" } },
                    new() { Id = "ss4", Name = "Cross-Training Court & Badminton Shoes", CategoryName = "Sports Shoes", CategorySlug = "sports-shoes", Price = 1599, OriginalPrice = 3199, Rating = 4.7m, ReviewsCount = 88, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sports-shoes", IsUploadingSoon = true, Badge = "Indoor Court", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "White", "Neon Orange" } }
                }
            },
            // 3. Formal Shoes
            new()
            {
                CategorySlug = "formal-shoes",
                CategoryName = "Formal Shoes",
                HeroBanner = new FashionMenFootwearSubcategoryHeroDto
                {
                    Title = "Formal Shoes",
                    TitleHighlight = "TIMELESS ELEGANCE • GENUINE LEATHER",
                    Subtitle = "Sharp Oxford brogues, polished patent Derbys, and slip-on monk straps engineered for presentations, interviews, and formal banquets.",
                    IsUploadingSoon = true
                },
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "fs1", Name = "Classic Handcrafted Leather Oxford Formal Shoes", CategoryName = "Formal Shoes", CategorySlug = "formal-shoes", Price = 2199, OriginalPrice = 4299, Rating = 4.9m, ReviewsCount = 168, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/formal-shoes", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "Black", "Dark Tan" } },
                    new() { Id = "fs2", Name = "Glossy Patent Derby Formal Lace-Up Shoes", CategoryName = "Formal Shoes", CategorySlug = "formal-shoes", Price = 1899, OriginalPrice = 3799, Rating = 4.8m, ReviewsCount = 114, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/formal-shoes", IsUploadingSoon = true, Badge = "Sharp Look", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Black" } },
                    new() { Id = "fs3", Name = "Double Monk Strap Italian Style Leather Shoes", CategoryName = "Formal Shoes", CategorySlug = "formal-shoes", Price = 2499, OriginalPrice = 4999, Rating = 4.9m, ReviewsCount = 92, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/formal-shoes", IsUploadingSoon = true, Badge = "Italian Style", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Burgundy", "Cognac Brown" } },
                    new() { Id = "fs4", Name = "Cushioned Slip-On Formal Penny Loafers", CategoryName = "Formal Shoes", CategorySlug = "formal-shoes", Price = 1699, OriginalPrice = 3399, Rating = 4.7m, ReviewsCount = 85, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/formal-shoes", IsUploadingSoon = true, Badge = "Comfort Sole", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "Black", "Dark Brown" } }
                }
            },
            // 4. Sneakers
            new()
            {
                CategorySlug = "sneakers",
                CategoryName = "Sneakers",
                HeroBanner = new FashionMenFootwearSubcategoryHeroDto
                {
                    Title = "Sneakers",
                    TitleHighlight = "STREET CULTURE • ICONIC VIBE",
                    Subtitle = "Retro low-tops, chunky platform kicks, and minimalist court sneakers tailored for your unique everyday campus aesthetic.",
                    IsUploadingSoon = true
                },
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "sn1", Name = "Urban Street Low-Top Leather Sneakers", CategoryName = "Sneakers", CategorySlug = "sneakers", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 245, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sneakers", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "White", "Black", "Grey" } },
                    new() { Id = "sn2", Name = "Chunky Retro 90s Platform Streetwear Sneakers", CategoryName = "Sneakers", CategorySlug = "sneakers", Price = 1899, OriginalPrice = 3799, Rating = 4.8m, ReviewsCount = 175, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sneakers", IsUploadingSoon = true, Badge = "Trending", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "White", "Multi-Color" } },
                    new() { Id = "sn3", Name = "High-Top Canvas Streetwear Skate Sneakers", CategoryName = "Sneakers", CategorySlug = "sneakers", Price = 1299, OriginalPrice = 2599, Rating = 4.7m, ReviewsCount = 130, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sneakers", IsUploadingSoon = true, Badge = "Skate Classic", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Black", "Red", "Navy" } },
                    new() { Id = "sn4", Name = "Minimalist Court All-White Daily Sneakers", CategoryName = "Sneakers", CategorySlug = "sneakers", Price = 1599, OriginalPrice = 3199, Rating = 4.9m, ReviewsCount = 190, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sneakers", IsUploadingSoon = true, Badge = "Campus Clean", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10", "UK 11", "UK 12" }, Colors = new List<string> { "All-White" } }
                }
            },
            // 5. Sandals & Floaters
            new()
            {
                CategorySlug = "sandals-floaters",
                CategoryName = "Sandals & Floaters",
                HeroBanner = new FashionMenFootwearSubcategoryHeroDto
                {
                    Title = "Sandals & Floaters",
                    TitleHighlight = "ALL-TERRAIN GRIP • WEATHERPROOF",
                    Subtitle = "Multi-strap ergonomic sandals, water-resistant hiking floaters, and cushioned comfort slides for tropical days and casual trips.",
                    IsUploadingSoon = true
                },
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "sf1", Name = "All-Terrain Adventure Grip Floaters", CategoryName = "Sandals & Floaters", CategorySlug = "sandals-floaters", Price = 899, OriginalPrice = 1799, Rating = 4.8m, ReviewsCount = 152, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sandals-floaters", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Olive", "Black" } },
                    new() { Id = "sf2", Name = "Ergonomic Dual-Strap Adjustable Sandals", CategoryName = "Sandals & Floaters", CategorySlug = "sandals-floaters", Price = 999, OriginalPrice = 1999, Rating = 4.7m, ReviewsCount = 108, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sandals-floaters", IsUploadingSoon = true, Badge = "Ergonomic", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Brown", "Tan" } },
                    new() { Id = "sf3", Name = "Water-Resistant Quick-Dry Trail Floaters", CategoryName = "Sandals & Floaters", CategorySlug = "sandals-floaters", Price = 1199, OriginalPrice = 2399, Rating = 4.9m, ReviewsCount = 96, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sandals-floaters", IsUploadingSoon = true, Badge = "Waterproof", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "Black", "Navy" } },
                    new() { Id = "sf4", Name = "Soft-Footbed Comfort Casual Slides", CategoryName = "Sandals & Floaters", CategorySlug = "sandals-floaters", Price = 749, OriginalPrice = 1499, Rating = 4.6m, ReviewsCount = 74, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/sandals-floaters", IsUploadingSoon = true, Badge = "Soft Bed", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Charcoal", "Navy" } }
                }
            },
            // 6. Flip Flops
            new()
            {
                CategorySlug = "flip-flops",
                CategoryName = "Flip Flops",
                HeroBanner = new FashionMenFootwearSubcategoryHeroDto
                {
                    Title = "Flip Flops",
                    TitleHighlight = "FEATHER-LIGHT EVA • ANTI-SKID",
                    Subtitle = "Soft EVA foam flip flops, beach thongs, and dormitory shower slippers engineered with anti-slip textured footbeds.",
                    IsUploadingSoon = true
                },
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "ff1", Name = "Anti-Skid Ultra-Light Comfort Flip Flops", CategoryName = "Flip Flops", CategorySlug = "flip-flops", Price = 449, OriginalPrice = 899, Rating = 4.7m, ReviewsCount = 186, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/flip-flops", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Black", "Blue" } },
                    new() { Id = "ff2", Name = "Lightweight EVA Foam Campus Chappals", CategoryName = "Flip Flops", CategorySlug = "flip-flops", Price = 349, OriginalPrice = 699, Rating = 4.6m, ReviewsCount = 120, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/flip-flops", IsUploadingSoon = true, Badge = "Super Light", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Grey", "Black" } },
                    new() { Id = "ff3", Name = "Dormitory Shower Anti-Slip Waterproof Slides", CategoryName = "Flip Flops", CategorySlug = "flip-flops", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 142, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/flip-flops", IsUploadingSoon = true, Badge = "Dorm Essential", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "Navy", "White" } },
                    new() { Id = "ff4", Name = "Cushioned Wave-Grip Beach Thong Slippers", CategoryName = "Flip Flops", CategorySlug = "flip-flops", Price = 399, OriginalPrice = 799, Rating = 4.5m, ReviewsCount = 92, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/flip-flops", IsUploadingSoon = true, Badge = "Beach Ready", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "Orange", "Black" } }
                }
            },
            // 7. Socks
            new()
            {
                CategorySlug = "socks",
                CategoryName = "Socks",
                HeroBanner = new FashionMenFootwearSubcategoryHeroDto
                {
                    Title = "Socks",
                    TitleHighlight = "ANTI-ODOR BAMBOO • REINFORCED HEEL",
                    Subtitle = "Multipack breathable cotton ankle socks, winter merino thermal crew socks, and sports compression athletic socks.",
                    IsUploadingSoon = true
                },
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "sk1", Name = "5-Pack Anti-Odor Breathable Cotton Ankle Socks", CategoryName = "Socks", CategorySlug = "socks", Price = 399, OriginalPrice = 799, Rating = 4.9m, ReviewsCount = 280, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/socks", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Black", "White", "Grey" } },
                    new() { Id = "sk2", Name = "3-Pack Winter Merino Thermal Crew Warm Socks", CategoryName = "Socks", CategorySlug = "socks", Price = 599, OriginalPrice = 1199, Rating = 4.9m, ReviewsCount = 195, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/socks", IsUploadingSoon = true, Badge = "Winter Warm", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Charcoal", "Navy" } },
                    new() { Id = "sk3", Name = "4-Pack Athletic Arch Compression Sports Socks", CategoryName = "Socks", CategorySlug = "socks", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 140, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/socks", IsUploadingSoon = true, Badge = "Compression", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "White", "Black" } },
                    new() { Id = "sk4", Name = "5-Pack Formal Antibacterial Bamboo Dress Socks", CategoryName = "Socks", CategorySlug = "socks", Price = 549, OriginalPrice = 1099, Rating = 4.8m, ReviewsCount = 112, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/men-footwear/socks", IsUploadingSoon = true, Badge = "Formal Pack", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Black", "Navy", "Dark Grey" } }
                }
            }
        };
    }
}


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
    public string? Subtitle { get; set; }
    public string Emoji { get; set; } = "👔";

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





