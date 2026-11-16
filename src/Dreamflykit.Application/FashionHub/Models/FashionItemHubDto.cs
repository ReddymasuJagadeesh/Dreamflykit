namespace Dreamflykit.Application.FashionHub.Models;

public sealed class FashionSubcategoryHeroDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public string ThemeColor { get; set; } = "#ea580c";
    public string BadgeText { get; set; } = "STUDENT ESSENTIALS • 100% GENUINE";
}

public sealed class FashionSubcategoryPageDto
{
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public FashionSubcategoryHeroDto HeroBanner { get; set; } = new();
    public List<FashionProductItemDto> Products { get; set; } = new();
}

public sealed class FashionItemHubDto
{
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
    public FashionFestivalThemeDto FestivalTheme { get; set; } = new();
    public FashionHeroBannerDto HeroBanner { get; set; } = new();
    public FashionCategoriesSectionDto CategoriesSection { get; set; } = new();
    public FashionTopPicksSectionDto TopPicksSection { get; set; } = new();
    public FashionTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();
    public FashionBottomBannerDto BottomBanner { get; set; } = new();
    public List<FashionSubcategoryPageDto> SubcategoryPages { get; set; } = new();

    #region Factory: Women Footwear
    public static FashionItemHubDto CreateDefaultWomenFootwear()
    {
        return new FashionItemHubDto
        {
            CategorySlug = "women-footwear",
            CategoryName = "Women Footwear",
            IsEnabled = true,
            FestivalTheme = new FashionFestivalThemeDto
            {
                IsEnabled = true,
                FestivalName = "Campus Chic & Step Edit",
                AnnouncementText = "👠 Step into Comfort: Designer campus heels, sneakers & flats with memory foam cushioning!",
                BadgeText = "WOMEN FOOTWEAR SPECIAL",
                BannerHighlight = "Up to 50% Off",
                BgGradient = "linear-gradient(135deg, #e11d48 0%, #db2777 100%)"
            },
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Women's Footwear",
                TitleHighlight = "Elegance, Comfort & Performance",
                Subtitle = "Curated for international university life — all-day walking comfort, presentation stilettos, and everyday casual flats.",
                Description = "High-density memory foam cushioning, flexible anti-skid soles, and breathable weather-ready designs.",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TitleColor = "#e11d48",
                TitleHighlightColor = "#ea580c",
                SubtitleColor = "#334155",
                BannerHeight = 440,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "wfb1", Label = "Memory Foam Cushion", Icon = "feather" },
                    new() { Id = "wfb2", Label = "Anti-Skid Rubber Soles", Icon = "shield" },
                    new() { Id = "wfb3", Label = "Lightweight & Breathable", Icon = "wind" },
                    new() { Id = "wfb4", Label = "Loved by 12K+ Students", Icon = "users" }
                },
                RightCard = new FashionHeroRightCardDto
                {
                    Title = "Walk With Grace.",
                    TitleHighlight = "Campus To Evening.",
                    Features = new List<FashionHeroFeatureDto>
                    {
                        new() { Id = "wff1", Title = "Memory Foam Comfort", Icon = "feather" },
                        new() { Id = "wff2", Title = "Anti-Skid Traction", Icon = "shield" },
                        new() { Id = "wff3", Title = "Lightweight Build", Icon = "wind" },
                        new() { Id = "wff4", Title = "All-Day Support", Icon = "compass" }
                    }
                }
            },
            CategoriesSection = new FashionCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Categories →",
                ViewAllUrl = "/category/fashion/women-footwear",
                Categories = new List<FashionCategoryItemDto>
                {
                    new() { Id = "wfc1", Name = "Flats & Bellies", Slug = "flats-bellies", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },
                    new() { Id = "wfc2", Name = "Heels & Pumps", Slug = "heels-pumps", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },
                    new() { Id = "wfc3", Name = "Sneakers & Trainers", Slug = "sneakers-trainers", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },
                    new() { Id = "wfc4", Name = "Casual Loafers", Slug = "casual-loafers", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },
                    new() { Id = "wfc5", Name = "Wedge Sandals", Slug = "wedge-sandals", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true },
                    new() { Id = "wfc6", Name = "Boots & Booties", Slug = "boots-booties", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true },
                    new() { Id = "wfc7", Name = "Flip Flops & Slides", Slug = "flipflops-slides", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 7, IsUploadingSoon = true }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks in Women Footwear",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/category/fashion/women-footwear",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "wfp1", Name = "Cloud-Comfort Pointed Toe Ballet Flats", CategoryName = "Women Footwear", CategorySlug = "flats-bellies", Price = 899, OriginalPrice = 1799, Rating = 4.9m, ReviewsCount = 198, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/flats-bellies", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 3", "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Blush Pink", "Black", "Beige" } },
                    new() { Id = "wfp2", Name = "Block Heel Formal Presentation Pumps (2.5 Inch)", CategoryName = "Women Footwear", CategorySlug = "heels-pumps", Price = 1599, OriginalPrice = 3199, Rating = 4.8m, ReviewsCount = 142, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/heels-pumps", IsUploadingSoon = true, Badge = "Formal Star", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Nude", "Classic Black" } },
                    new() { Id = "wfp3", Name = "Chunky Retro Campus Platform Sneakers", CategoryName = "Women Footwear", CategorySlug = "sneakers-trainers", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 260, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/sneakers-trainers", IsUploadingSoon = true, Badge = "Trending", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "White", "Pastel Multi" } },
                    new() { Id = "wfp4", Name = "Soft Leather Tassel Driving Loafers", CategoryName = "Women Footwear", CategorySlug = "casual-loafers", Price = 1199, OriginalPrice = 2399, Rating = 4.7m, ReviewsCount = 115, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/casual-loafers", IsUploadingSoon = true, Badge = "Campus Pick", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7" }, Colors = new List<string> { "Tan", "Navy", "Olive" } },
                    new() { Id = "wfp5", Name = "Espadrille Ankle-Strap Summer Wedges", CategoryName = "Women Footwear", CategorySlug = "wedge-sandals", Price = 1299, OriginalPrice = 2599, Rating = 4.8m, ReviewsCount = 89, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/wedge-sandals", IsUploadingSoon = true, Badge = "Summer Chic", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Cream", "Camel" } },
                    new() { Id = "wfp6", Name = "Water-Resistant Chelsea Ankle Rain Booties", CategoryName = "Women Footwear", CategorySlug = "boots-booties", Price = 1899, OriginalPrice = 3799, Rating = 4.9m, ReviewsCount = 175, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/boots-booties", IsUploadingSoon = true, Badge = "Weatherproof", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Matte Black", "Dark Brown" } },
                    new() { Id = "wfp7", Name = "Ultra-Soft Cloud Pillow Slide Slippers", CategoryName = "Women Footwear", CategorySlug = "flipflops-slides", Price = 499, OriginalPrice = 999, Rating = 4.7m, ReviewsCount = 310, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/flipflops-slides", IsUploadingSoon = true, Badge = "Dorm Essential", Sizes = new List<string> { "UK 3", "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Lilac", "Mint", "Beige" } }
                }
            },
            TrustBadgesSection = CreateDefaultTrustBadges(),
            BottomBanner = CreateDefaultBottomBanner("Find My Shoe Fit", "/find-my-kit"),
            SubcategoryPages = new List<FashionSubcategoryPageDto>
            {
                new()
                {
                    CategorySlug = "flats-bellies",
                    CategoryName = "Flats & Bellies",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Flats & Bellies",
                        TitleHighlight = "ALL-DAY COMFORT • ELEGANT SILHOUETTES",
                        Subtitle = "Foldable pointed ballerinas, round-toe quilted flats, and cushioned slip-ons tailored for endless library and campus sessions.",
                        ThemeColor = "#e11d48",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "fb1", Name = "Cloud-Comfort Pointed Toe Ballet Flats", CategoryName = "Flats & Bellies", CategorySlug = "flats-bellies", Price = 899, OriginalPrice = 1799, Rating = 4.9m, ReviewsCount = 198, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/flats-bellies", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 3", "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Blush Pink", "Black", "Beige" } },
                        new() { Id = "fb2", Name = "Quilted Bow-Accent Cushioned Ballerina", CategoryName = "Flats & Bellies", CategorySlug = "flats-bellies", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/flats-bellies", IsUploadingSoon = true, Badge = "Chic Classic", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7" }, Colors = new List<string> { "Black", "Burgundy" } },
                        new() { Id = "fb3", Name = "Breathable Knit Flexible Walking Skimmer Flats", CategoryName = "Flats & Bellies", CategorySlug = "flats-bellies", Price = 1199, OriginalPrice = 2399, Rating = 4.8m, ReviewsCount = 112, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/flats-bellies", IsUploadingSoon = true, Badge = "Knit Comfort", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Grey", "Navy" } },
                        new() { Id = "fb4", Name = "Soft Suede Square-Toe Slip-On Mules", CategoryName = "Flats & Bellies", CategorySlug = "flats-bellies", Price = 1299, OriginalPrice = 2599, Rating = 4.7m, ReviewsCount = 88, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/flats-bellies", IsUploadingSoon = true, Badge = "Suede Luxe", Sizes = new List<string> { "UK 3", "UK 4", "UK 5", "UK 6", "UK 7" }, Colors = new List<string> { "Camel", "Black" } }
                    }
                },
                new()
                {
                    CategorySlug = "heels-pumps",
                    CategoryName = "Heels & Pumps",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Heels & Pumps",
                        TitleHighlight = "CONFIDENCE & ELEVATION • STABLE STEPS",
                        Subtitle = "Engineered block heels, kitten heel presentation pumps, and slingbacks with arch support for formal college banquets and interviews.",
                        ThemeColor = "#e11d48",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "hp1", Name = "Block Heel Formal Presentation Pumps (2.5 Inch)", CategoryName = "Heels & Pumps", CategorySlug = "heels-pumps", Price = 1599, OriginalPrice = 3199, Rating = 4.8m, ReviewsCount = 142, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/heels-pumps", IsUploadingSoon = true, Badge = "Formal Star", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Nude", "Classic Black" } },
                        new() { Id = "hp2", Name = "Kitten Heel Slingback Evening Pointed Pumps", CategoryName = "Heels & Pumps", CategorySlug = "heels-pumps", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 118, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/heels-pumps", IsUploadingSoon = true, Badge = "Kitten Chic", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7" }, Colors = new List<string> { "Black", "Red" } },
                        new() { Id = "hp3", Name = "Comfort Chunky Flared Heeled Sandals", CategoryName = "Heels & Pumps", CategorySlug = "heels-pumps", Price = 1399, OriginalPrice = 2799, Rating = 4.7m, ReviewsCount = 95, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/heels-pumps", IsUploadingSoon = true, Badge = "Chunky Stable", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Gold", "Silver", "Black" } },
                        new() { Id = "hp4", Name = "Patent Gloss Stiletto Formal Banqueting Heels", CategoryName = "Heels & Pumps", CategorySlug = "heels-pumps", Price = 1799, OriginalPrice = 3599, Rating = 4.8m, ReviewsCount = 84, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/heels-pumps", IsUploadingSoon = true, Badge = "High Glamour", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7" }, Colors = new List<string> { "Burgundy Gloss", "Black Gloss" } }
                    }
                },
                new()
                {
                    CategorySlug = "sneakers-trainers",
                    CategoryName = "Sneakers & Trainers",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Sneakers & Trainers",
                        TitleHighlight = "URBAN EDGE • ALL-DAY MILEAGE",
                        Subtitle = "Chunky platform lifestyle kicks, minimalist court trainers, and shock-absorbing gym runners built for fast campus transits.",
                        ThemeColor = "#e11d48",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "st1", Name = "Chunky Retro Campus Platform Sneakers", CategoryName = "Sneakers & Trainers", CategorySlug = "sneakers-trainers", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 260, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/sneakers-trainers", IsUploadingSoon = true, Badge = "Trending", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "White", "Pastel Multi" } },
                        new() { Id = "st2", Name = "Clean-Cut Minimalist All-White Court Sneakers", CategoryName = "Sneakers & Trainers", CategorySlug = "sneakers-trainers", Price = 1399, OriginalPrice = 2799, Rating = 4.8m, ReviewsCount = 188, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/sneakers-trainers", IsUploadingSoon = true, Badge = "Wardrobe Hero", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "All White" } },
                        new() { Id = "st3", Name = "Air-Cushioned Lightweight Gym Running Trainers", CategoryName = "Sneakers & Trainers", CategorySlug = "sneakers-trainers", Price = 1699, OriginalPrice = 3399, Rating = 4.9m, ReviewsCount = 140, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/sneakers-trainers", IsUploadingSoon = true, Badge = "Workout Pro", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7" }, Colors = new List<string> { "Lavender", "Black" } },
                        new() { Id = "st4", Name = "High-Top Canvas Streetwear Skate Kicks", CategoryName = "Sneakers & Trainers", CategorySlug = "sneakers-trainers", Price = 1299, OriginalPrice = 2599, Rating = 4.7m, ReviewsCount = 125, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/sneakers-trainers", IsUploadingSoon = true, Badge = "Canvas Classic", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Black & White", "Pink" } }
                    }
                },
                new()
                {
                    CategorySlug = "casual-loafers",
                    CategoryName = "Casual Loafers",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Casual Loafers",
                        TitleHighlight = "CLASSIC PREPPY • TIMELESS POLISH",
                        Subtitle = "Lug-sole penny loafers, horsebit moccasins, and supple driving shoes combining smart-casual collegiate style with plush insoles.",
                        ThemeColor = "#e11d48",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "cl1", Name = "Soft Leather Tassel Driving Loafers", CategoryName = "Casual Loafers", CategorySlug = "casual-loafers", Price = 1199, OriginalPrice = 2399, Rating = 4.7m, ReviewsCount = 115, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/casual-loafers", IsUploadingSoon = true, Badge = "Campus Pick", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7" }, Colors = new List<string> { "Tan", "Navy", "Olive" } },
                        new() { Id = "cl2", Name = "Chunky Lug-Sole Preppy Penny Loafers", CategoryName = "Casual Loafers", CategorySlug = "casual-loafers", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 168, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/casual-loafers", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Gloss Black", "Oxblood" } },
                        new() { Id = "cl3", Name = "Gold Horsebit Buckle Tailored Moccasins", CategoryName = "Casual Loafers", CategorySlug = "casual-loafers", Price = 1399, OriginalPrice = 2799, Rating = 4.8m, ReviewsCount = 92, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/casual-loafers", IsUploadingSoon = true, Badge = "Luxury Accent", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7" }, Colors = new List<string> { "Beige", "Black" } },
                        new() { Id = "cl4", Name = "Flexible Perforated Summer Slip-On Loafers", CategoryName = "Casual Loafers", CategorySlug = "casual-loafers", Price = 1099, OriginalPrice = 2199, Rating = 4.6m, ReviewsCount = 74, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/casual-loafers", IsUploadingSoon = true, Badge = "Ultra Light", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "White", "Tan" } }
                    }
                },
                new()
                {
                    CategorySlug = "wedge-sandals",
                    CategoryName = "Wedge Sandals",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Wedge Sandals",
                        TitleHighlight = "EFFORTLESS LIFT • SUMMER BREEZE",
                        Subtitle = "Espadrille jute-wrapped wedges, cork footbed slides, and cross-strap platforms that give flattering height without sacrificing stability.",
                        ThemeColor = "#e11d48",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "ws1", Name = "Espadrille Ankle-Strap Summer Wedges", CategoryName = "Wedge Sandals", CategorySlug = "wedge-sandals", Price = 1299, OriginalPrice = 2599, Rating = 4.8m, ReviewsCount = 89, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/wedge-sandals", IsUploadingSoon = true, Badge = "Summer Chic", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Cream", "Camel" } },
                        new() { Id = "ws2", Name = "Cork Footbed Ergonomic Comfort Wedge Slides", CategoryName = "Wedge Sandals", CategorySlug = "wedge-sandals", Price = 1199, OriginalPrice = 2399, Rating = 4.7m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/wedge-sandals", IsUploadingSoon = true, Badge = "All Day Wear", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7" }, Colors = new List<string> { "Black", "Tan" } },
                        new() { Id = "ws3", Name = "Cross-Strap Metallic Evening Party Wedges", CategoryName = "Wedge Sandals", CategorySlug = "wedge-sandals", Price = 1399, OriginalPrice = 2799, Rating = 4.8m, ReviewsCount = 76, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/wedge-sandals", IsUploadingSoon = true, Badge = "Party Star", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Rose Gold", "Silver" } },
                        new() { Id = "ws4", Name = "Low-Wedge Peep-Toe Casual Slingback Sandal", CategoryName = "Wedge Sandals", CategorySlug = "wedge-sandals", Price = 999, OriginalPrice = 1999, Rating = 4.6m, ReviewsCount = 68, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/wedge-sandals", IsUploadingSoon = true, Badge = "Light Wedge", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7" }, Colors = new List<string> { "Navy", "Beige" } }
                    }
                },
                new()
                {
                    CategorySlug = "boots-booties",
                    CategoryName = "Boots & Booties",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Boots & Booties",
                        TitleHighlight = "ALL-WEATHER SHIELD • RUGGED CHIC",
                        Subtitle = "Elasticated Chelsea booties, lace-up combat boots, and warm fleece-lined winter walkers prepared for chilly abroad semesters.",
                        ThemeColor = "#e11d48",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "bb1", Name = "Water-Resistant Chelsea Ankle Rain Booties", CategoryName = "Boots & Booties", CategorySlug = "boots-booties", Price = 1899, OriginalPrice = 3799, Rating = 4.9m, ReviewsCount = 175, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/boots-booties", IsUploadingSoon = true, Badge = "Weatherproof", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Matte Black", "Dark Brown" } },
                        new() { Id = "bb2", Name = "Lace-Up Chunky Lug Sole Combat Boots", CategoryName = "Boots & Booties", CategorySlug = "boots-booties", Price = 2199, OriginalPrice = 4399, Rating = 4.9m, ReviewsCount = 210, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/boots-booties", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Black", "Burgundy" } },
                        new() { Id = "bb3", Name = "Thermal Fleece Lined Winter Campus Boots", CategoryName = "Boots & Booties", CategorySlug = "boots-booties", Price = 2499, OriginalPrice = 4999, Rating = 4.8m, ReviewsCount = 132, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/boots-booties", IsUploadingSoon = true, Badge = "Winter Shield", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Chestnut", "Black" } },
                        new() { Id = "bb4", Name = "Heeled Pointed Ankle Sock Boots", CategoryName = "Boots & Booties", CategorySlug = "boots-booties", Price = 1999, OriginalPrice = 3999, Rating = 4.7m, ReviewsCount = 94, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/boots-booties", IsUploadingSoon = true, Badge = "Sleek Look", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7" }, Colors = new List<string> { "Black Knit", "Taupe" } }
                    }
                },
                new()
                {
                    CategorySlug = "flipflops-slides",
                    CategoryName = "Flip Flops & Slides",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Flip Flops & Slides",
                        TitleHighlight = "FEATHER-LIGHT CLOUD EVA • ANTI-SLIP",
                        Subtitle = "Ergonomic arch-support cloud slides, quick-dry shower slippers, and metallic beach thongs made for hostel dorms and leisure weekends.",
                        ThemeColor = "#e11d48",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "ff1", Name = "Ultra-Soft Cloud Pillow Slide Slippers", CategoryName = "Flip Flops & Slides", CategorySlug = "flipflops-slides", Price = 499, OriginalPrice = 999, Rating = 4.7m, ReviewsCount = 310, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/flipflops-slides", IsUploadingSoon = true, Badge = "Dorm Essential", Sizes = new List<string> { "UK 3", "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Lilac", "Mint", "Beige" } },
                        new() { Id = "ff2", Name = "Waterproof Non-Slip Dormitory Shower Slides", CategoryName = "Flip Flops & Slides", CategorySlug = "flipflops-slides", Price = 399, OriginalPrice = 799, Rating = 4.8m, ReviewsCount = 180, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/flipflops-slides", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Pink", "White", "Grey" } },
                        new() { Id = "ff3", Name = "Glitter Metallic Strap Ergonomic Flip Flops", CategoryName = "Flip Flops & Slides", CategorySlug = "flipflops-slides", Price = 449, OriginalPrice = 899, Rating = 4.6m, ReviewsCount = 112, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/flipflops-slides", IsUploadingSoon = true, Badge = "Sparkle", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7" }, Colors = new List<string> { "Rose Gold", "Silver" } },
                        new() { Id = "ff4", Name = "Molded Footbed Dual-Buckle Lightweight Slides", CategoryName = "Flip Flops & Slides", CategorySlug = "flipflops-slides", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/women-footwear/flipflops-slides", IsUploadingSoon = true, Badge = "Molded Sole", Sizes = new List<string> { "UK 4", "UK 5", "UK 6", "UK 7", "UK 8" }, Colors = new List<string> { "Olive", "Black", "White" } }
                    }
                }
            }
        };
    }
    #endregion

    #region Factory: Watches
    public static FashionItemHubDto CreateDefaultWatches()
    {
        return new FashionItemHubDto
        {
            CategorySlug = "watches",
            CategoryName = "Watches",
            IsEnabled = true,
            FestivalTheme = new FashionFestivalThemeDto
            {
                IsEnabled = true,
                FestivalName = "Horology & Precision Showcase",
                AnnouncementText = "⌚ Master Every Second: Japanese Quartz chronographs & smart fitness watches with student warranties!",
                BadgeText = "WATCH SPECIAL",
                BannerHighlight = "Up to 55% Off",
                BgGradient = "linear-gradient(135deg, #0f172a 0%, #1e293b 100%)"
            },
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Watches Collection",
                TitleHighlight = "Timeless Precision & Modern Smart Tech",
                Subtitle = "From lecture hall chronographs to minimalist evening mesh styles and health-tracking smartwatches.",
                Description = "Japanese quartz movements, scratch-resistant mineral glass, 50M water resistance, and durable bands.",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TitleColor = "#0f172a",
                TitleHighlightColor = "#ea580c",
                SubtitleColor = "#334155",
                BannerHeight = 440,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "wb1", Label = "Quartz Precision", Icon = "compass" },
                    new() { Id = "wb2", Label = "50M Water Resistance", Icon = "shield" },
                    new() { Id = "wb3", Label = "Scratch-Resistant Glass", Icon = "feather" },
                    new() { Id = "wb4", Label = "2-Year Student Warranty", Icon = "users" }
                },
                RightCard = new FashionHeroRightCardDto
                {
                    Title = "Master Every Second.",
                    TitleHighlight = "Design Meets Tech.",
                    Features = new List<FashionHeroFeatureDto>
                    {
                        new() { Id = "wf1", Title = "Japanese Movement", Icon = "compass" },
                        new() { Id = "wf2", Title = "50M Water Resistance", Icon = "shield" },
                        new() { Id = "wf3", Title = "Scratch-Proof Glass", Icon = "feather" },
                        new() { Id = "wf4", Title = "2-Year Warranty", Icon = "tag" }
                    }
                }
            },
            CategoriesSection = new FashionCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Categories →",
                ViewAllUrl = "/category/fashion/watches",
                Categories = new List<FashionCategoryItemDto>
                {
                    new() { Id = "wc1", Name = "Analog Classic Watches", Slug = "analog-classic", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },
                    new() { Id = "wc2", Name = "Chronograph & Sport", Slug = "chronograph-sport", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },
                    new() { Id = "wc3", Name = "Smartwatches & Bands", Slug = "smartwatches", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },
                    new() { Id = "wc4", Name = "Formal Leather Strap", Slug = "leather-strap", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },
                    new() { Id = "wc5", Name = "Luxury Stainless Steel", Slug = "stainless-steel", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true },
                    new() { Id = "wc6", Name = "Digital & Retro Watches", Slug = "digital-retro", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks in Watches",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/category/fashion/watches",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "wtp1", Name = "Minimalist Ultra-Slim Analog Quartz Watch", CategoryName = "Watches", CategorySlug = "analog-classic", Price = 1299, OriginalPrice = 2599, Rating = 4.9m, ReviewsCount = 240, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/analog-classic", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "40mm Dial", "36mm Dial" }, Colors = new List<string> { "Black", "Rose Gold", "Silver" } },
                    new() { Id = "wtp2", Name = "Tachymeter Multi-Dial Chronograph Sport Watch", CategoryName = "Watches", CategorySlug = "chronograph-sport", Price = 2199, OriginalPrice = 4399, Rating = 4.8m, ReviewsCount = 180, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/chronograph-sport", IsUploadingSoon = true, Badge = "High Precision", Sizes = new List<string> { "44mm Dial" }, Colors = new List<string> { "Matte Black", "Ocean Blue" } },
                    new() { Id = "wtp3", Name = "AMOLED Display Bluetooth Calling Smartwatch", CategoryName = "Watches", CategorySlug = "smartwatches", Price = 2499, OriginalPrice = 4999, Rating = 4.8m, ReviewsCount = 310, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/smartwatches", IsUploadingSoon = true, Badge = "Smart Tech", Sizes = new List<string> { "1.43\" AMOLED" }, Colors = new List<string> { "Charcoal", "Deep Green", "Silver" } },
                    new() { Id = "wtp4", Name = "Italian Full-Grain Leather Roman Dial Dress Watch", CategoryName = "Watches", CategorySlug = "leather-strap", Price = 1599, OriginalPrice = 3199, Rating = 4.9m, ReviewsCount = 150, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/leather-strap", IsUploadingSoon = true, Badge = "Formal Star", Sizes = new List<string> { "42mm Dial" }, Colors = new List<string> { "Dark Brown", "Tan Leather", "Black" } },
                    new() { Id = "wtp5", Name = "Heavy Stainless Steel Link Bracelet Luxury Watch", CategoryName = "Watches", CategorySlug = "stainless-steel", Price = 2799, OriginalPrice = 5599, Rating = 4.9m, ReviewsCount = 120, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/stainless-steel", IsUploadingSoon = true, Badge = "Luxury Accent", Sizes = new List<string> { "41mm Dial" }, Colors = new List<string> { "Silver & Gold", "All Silver" } },
                    new() { Id = "wtp6", Name = "Vintage 80s Gold-Tone Digital Illuminator Watch", CategoryName = "Watches", CategorySlug = "digital-retro", Price = 999, OriginalPrice = 1999, Rating = 4.7m, ReviewsCount = 205, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/digital-retro", IsUploadingSoon = true, Badge = "Vintage Hit", Sizes = new List<string> { "Standard Digital" }, Colors = new List<string> { "Gold", "Gunmetal", "Silver" } }
                }
            },
            TrustBadgesSection = CreateDefaultTrustBadges(),
            BottomBanner = CreateDefaultBottomBanner("Watch Finder Guide", "/find-my-kit"),
            SubcategoryPages = new List<FashionSubcategoryPageDto>
            {
                new()
                {
                    CategorySlug = "analog-classic",
                    CategoryName = "Analog Classic Watches",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Analog Classic Watches",
                        TitleHighlight = "TIMELESS PURITY • MINIMALIST DIALS",
                        Subtitle = "Clean index markings, scratch-resistant mineral crystals, and ultra-thin profiles made for academic poise and everyday refinement.",
                        ThemeColor = "#0f172a",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "acw1", Name = "Minimalist Ultra-Slim Analog Quartz Watch", CategoryName = "Analog Classic Watches", CategorySlug = "analog-classic", Price = 1299, OriginalPrice = 2599, Rating = 4.9m, ReviewsCount = 240, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/analog-classic", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "40mm", "36mm" }, Colors = new List<string> { "Black", "Rose Gold", "Silver" } },
                        new() { Id = "acw2", Name = "Bauhaus Clean Dial Mesh Bracelet Watch", CategoryName = "Analog Classic Watches", CategorySlug = "analog-classic", Price = 1499, OriginalPrice = 2999, Rating = 4.8m, ReviewsCount = 135, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/analog-classic", IsUploadingSoon = true, Badge = "Bauhaus Style", Sizes = new List<string> { "38mm" }, Colors = new List<string> { "Silver Mesh", "Black Mesh" } },
                        new() { Id = "acw3", Name = "Sunray Blue Dial Classic Stainless Steel Watch", CategoryName = "Analog Classic Watches", CategorySlug = "analog-classic", Price = 1699, OriginalPrice = 3399, Rating = 4.9m, ReviewsCount = 160, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/analog-classic", IsUploadingSoon = true, Badge = "Sunray Dial", Sizes = new List<string> { "40mm" }, Colors = new List<string> { "Royal Blue", "Emerald Green" } },
                        new() { Id = "acw4", Name = "Square Vintage Frame Analog Leather Watch", CategoryName = "Analog Classic Watches", CategorySlug = "analog-classic", Price = 1399, OriginalPrice = 2799, Rating = 4.7m, ReviewsCount = 88, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/analog-classic", IsUploadingSoon = true, Badge = "Square Classic", Sizes = new List<string> { "34mm Square" }, Colors = new List<string> { "Tan", "Black" } }
                    }
                },
                new()
                {
                    CategorySlug = "chronograph-sport",
                    CategoryName = "Chronograph & Sport",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Chronograph & Sport",
                        TitleHighlight = "PRECISION STOPWATCH • TACHYMETER BEZEL",
                        Subtitle = "Engineered triple subdials, split-second timing, and high-impact water resistance crafted for fitness, workouts, and travel precision.",
                        ThemeColor = "#0f172a",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "csp1", Name = "Tachymeter Multi-Dial Chronograph Sport Watch", CategoryName = "Chronograph & Sport", CategorySlug = "chronograph-sport", Price = 2199, OriginalPrice = 4399, Rating = 4.8m, ReviewsCount = 180, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/chronograph-sport", IsUploadingSoon = true, Badge = "High Precision", Sizes = new List<string> { "44mm Dial" }, Colors = new List<string> { "Matte Black", "Ocean Blue" } },
                        new() { Id = "csp2", Name = "Aerospace Dual-Display Chronograph Diver Watch", CategoryName = "Chronograph & Sport", CategorySlug = "chronograph-sport", Price = 2499, OriginalPrice = 4999, Rating = 4.9m, ReviewsCount = 142, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/chronograph-sport", IsUploadingSoon = true, Badge = "100M Water-Resist", Sizes = new List<string> { "45mm Dial" }, Colors = new List<string> { "Black & Orange", "Gunmetal" } },
                        new() { Id = "csp3", Name = "Silicone Strap Lightweight Field Sport Watch", CategoryName = "Chronograph & Sport", CategorySlug = "chronograph-sport", Price = 1799, OriginalPrice = 3599, Rating = 4.7m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/chronograph-sport", IsUploadingSoon = true, Badge = "Sport Strap", Sizes = new List<string> { "42mm Dial" }, Colors = new List<string> { "Olive Drab", "Stealth Black" } },
                        new() { Id = "csp4", Name = "Racer Panda Dial Contrast Chronograph", CategoryName = "Chronograph & Sport", CategorySlug = "chronograph-sport", Price = 2399, OriginalPrice = 4799, Rating = 4.8m, ReviewsCount = 115, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/chronograph-sport", IsUploadingSoon = true, Badge = "Panda Dial", Sizes = new List<string> { "41mm Dial" }, Colors = new List<string> { "White & Black", "Blue & Silver" } }
                    }
                },
                new()
                {
                    CategorySlug = "smartwatches",
                    CategoryName = "Smartwatches & Bands",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Smartwatches & Bands",
                        TitleHighlight = "AMOLED DISPLAY • BLUETOOTH CALLING",
                        Subtitle = "Stay connected with notifications, heart rate monitoring, sleep analysis, and 7-day battery life engineered for student routines.",
                        ThemeColor = "#0f172a",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "smw1", Name = "AMOLED Display Bluetooth Calling Smartwatch", CategoryName = "Smartwatches & Bands", CategorySlug = "smartwatches", Price = 2499, OriginalPrice = 4999, Rating = 4.8m, ReviewsCount = 310, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/smartwatches", IsUploadingSoon = true, Badge = "Smart Tech", Sizes = new List<string> { "1.43\" AMOLED" }, Colors = new List<string> { "Charcoal", "Deep Green", "Silver" } },
                        new() { Id = "smw2", Name = "Slim Fitness Tracker Band with SpO2 & Sleep Tracking", CategoryName = "Smartwatches & Bands", CategorySlug = "smartwatches", Price = 1699, OriginalPrice = 3399, Rating = 4.7m, ReviewsCount = 220, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/smartwatches", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "1.1\" OLED" }, Colors = new List<string> { "Black", "Pink", "Navy" } },
                        new() { Id = "smw3", Name = "Rugged Military Outdoor GPS Smartwatch", CategoryName = "Smartwatches & Bands", CategorySlug = "smartwatches", Price = 3499, OriginalPrice = 6999, Rating = 4.9m, ReviewsCount = 150, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/smartwatches", IsUploadingSoon = true, Badge = "Rugged Armor", Sizes = new List<string> { "1.5\" Display" }, Colors = new List<string> { "Camo Green", "Desert Sand" } },
                        new() { Id = "smw4", Name = "Metal Mesh Strap Ultra-Slim Women's Smartwatch", CategoryName = "Smartwatches & Bands", CategorySlug = "smartwatches", Price = 2799, OriginalPrice = 5599, Rating = 4.8m, ReviewsCount = 175, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/smartwatches", IsUploadingSoon = true, Badge = "Chic Smart", Sizes = new List<string> { "1.2\" AMOLED" }, Colors = new List<string> { "Rose Gold Mesh", "Silver Mesh" } }
                    }
                },
                new()
                {
                    CategorySlug = "leather-strap",
                    CategoryName = "Formal Leather Strap",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Formal Leather Strap",
                        TitleHighlight = "GENUINE LEATHER • EXECUTIVE FINISH",
                        Subtitle = "Hand-stitched Italian leather straps, elegant Roman numerals, and slim rose-gold or silver cases built for presentations and job interviews.",
                        ThemeColor = "#0f172a",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "flw1", Name = "Italian Full-Grain Leather Roman Dial Dress Watch", CategoryName = "Formal Leather Strap", CategorySlug = "leather-strap", Price = 1599, OriginalPrice = 3199, Rating = 4.9m, ReviewsCount = 150, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/leather-strap", IsUploadingSoon = true, Badge = "Formal Star", Sizes = new List<string> { "42mm" }, Colors = new List<string> { "Dark Brown", "Tan Leather", "Black" } },
                        new() { Id = "flw2", Name = "Ultra-Thin Gold Case Crocodile Embossed Leather Watch", CategoryName = "Formal Leather Strap", CategorySlug = "leather-strap", Price = 1799, OriginalPrice = 3599, Rating = 4.8m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/leather-strap", IsUploadingSoon = true, Badge = "Executive", Sizes = new List<string> { "40mm" }, Colors = new List<string> { "Chestnut Croc", "Black Croc" } },
                        new() { Id = "flw3", Name = "Suede Leather Band Minimalist Automatic Watch", CategoryName = "Formal Leather Strap", CategorySlug = "leather-strap", Price = 2999, OriginalPrice = 5999, Rating = 4.9m, ReviewsCount = 85, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/leather-strap", IsUploadingSoon = true, Badge = "Automatic Movement", Sizes = new List<string> { "40mm" }, Colors = new List<string> { "Grey Suede", "Tan Suede" } },
                        new() { Id = "flw4", Name = "Subsecond Subdial Classic White Leather Watch", CategoryName = "Formal Leather Strap", CategorySlug = "leather-strap", Price = 1499, OriginalPrice = 2999, Rating = 4.7m, ReviewsCount = 92, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/leather-strap", IsUploadingSoon = true, Badge = "Classic Clean", Sizes = new List<string> { "39mm" }, Colors = new List<string> { "Dark Brown", "Midnight Blue" } }
                    }
                },
                new()
                {
                    CategorySlug = "stainless-steel",
                    CategoryName = "Luxury Stainless Steel",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Luxury Stainless Steel",
                        TitleHighlight = "316L SURGICAL STEEL • SOLID LINK BRACELETS",
                        Subtitle = "Scratch-resistant sapphire coating, butterfly deployment clasp, and lustrous polished accents that last your entire college and career journey.",
                        ThemeColor = "#0f172a",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "ssw1", Name = "Heavy Stainless Steel Link Bracelet Luxury Watch", CategoryName = "Luxury Stainless Steel", CategorySlug = "stainless-steel", Price = 2799, OriginalPrice = 5599, Rating = 4.9m, ReviewsCount = 120, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/stainless-steel", IsUploadingSoon = true, Badge = "Luxury Accent", Sizes = new List<string> { "41mm" }, Colors = new List<string> { "Silver & Gold", "All Silver" } },
                        new() { Id = "ssw2", Name = "Black Ion-Plated Textured Dial Steel Watch", CategoryName = "Luxury Stainless Steel", CategorySlug = "stainless-steel", Price = 2499, OriginalPrice = 4999, Rating = 4.8m, ReviewsCount = 140, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/stainless-steel", IsUploadingSoon = true, Badge = "Stealth Black", Sizes = new List<string> { "42mm" }, Colors = new List<string> { "Matte Black", "Gunmetal" } },
                        new() { Id = "ssw3", Name = "Jubilee Bracelet Sunburst Emerald Green Dial Watch", CategoryName = "Luxury Stainless Steel", CategorySlug = "stainless-steel", Price = 2999, OriginalPrice = 5999, Rating = 4.9m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/stainless-steel", IsUploadingSoon = true, Badge = "Emerald Dial", Sizes = new List<string> { "40mm" }, Colors = new List<string> { "Emerald & Silver", "Cobalt & Silver" } },
                        new() { Id = "ssw4", Name = "Presidential Fluted Bezel Steel Dress Watch", CategoryName = "Luxury Stainless Steel", CategorySlug = "stainless-steel", Price = 2699, OriginalPrice = 5399, Rating = 4.8m, ReviewsCount = 105, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/stainless-steel", IsUploadingSoon = true, Badge = "Fluted Bezel", Sizes = new List<string> { "39mm" }, Colors = new List<string> { "Silver Tone", "Champagne Gold" } }
                    }
                },
                new()
                {
                    CategorySlug = "digital-retro",
                    CategoryName = "Digital & Retro Watches",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Digital & Retro Watches",
                        TitleHighlight = "80s RETRO REVIVAL • DAILY FUNCTIONALITY",
                        Subtitle = "Vintage electro-luminescent displays, daily alarm, hourly chime, and indestructible construction for dependable campus timekeeping.",
                        ThemeColor = "#0f172a",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "drw1", Name = "Vintage 80s Gold-Tone Digital Illuminator Watch", CategoryName = "Digital & Retro Watches", CategorySlug = "digital-retro", Price = 999, OriginalPrice = 1999, Rating = 4.7m, ReviewsCount = 205, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/digital-retro", IsUploadingSoon = true, Badge = "Vintage Hit", Sizes = new List<string> { "Standard Digital" }, Colors = new List<string> { "Gold", "Gunmetal", "Silver" } },
                        new() { Id = "drw2", Name = "Shock-Resistant Outdoor Digital Sports Watch", CategoryName = "Digital & Retro Watches", CategorySlug = "digital-retro", Price = 1199, OriginalPrice = 2399, Rating = 4.9m, ReviewsCount = 190, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/digital-retro", IsUploadingSoon = true, Badge = "Heavy Shock-Proof", Sizes = new List<string> { "48mm Dial" }, Colors = new List<string> { "Matte Black", "Army Green" } },
                        new() { Id = "drw3", Name = "Ultra-Thin Stainless Steel Retro Digital Watch", CategoryName = "Digital & Retro Watches", CategorySlug = "digital-retro", Price = 1299, OriginalPrice = 2599, Rating = 4.8m, ReviewsCount = 115, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/digital-retro", IsUploadingSoon = true, Badge = "Slim Steel", Sizes = new List<string> { "Standard Digital" }, Colors = new List<string> { "Silver", "Rose Gold" } },
                        new() { Id = "drw4", Name = "Dual-Time World Clock Calculator Retro Watch", CategoryName = "Digital & Retro Watches", CategorySlug = "digital-retro", Price = 1099, OriginalPrice = 2199, Rating = 4.6m, ReviewsCount = 82, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/watches/digital-retro", IsUploadingSoon = true, Badge = "Dual Time", Sizes = new List<string> { "Standard Digital" }, Colors = new List<string> { "Black Resin" } }
                    }
                }
            }
        };
    }
    #endregion

    #region Factory: Sunglasses
    public static FashionItemHubDto CreateDefaultSunglasses()
    {
        return new FashionItemHubDto
        {
            CategorySlug = "sunglasses",
            CategoryName = "Sunglasses",
            IsEnabled = true,
            FestivalTheme = new FashionFestivalThemeDto
            {
                IsEnabled = true,
                FestivalName = "Sun & Campus Protection Edit",
                AnnouncementText = "🕶️ Visionary Style: 100% UV400 & polarized anti-glare shades for sunny lecture walks and travel trips!",
                BadgeText = "SHADES SPECIAL",
                BannerHighlight = "Up to 50% Off",
                BgGradient = "linear-gradient(135deg, #0284c7 0%, #0f172a 100%)"
            },
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Sunglasses & Shades",
                TitleHighlight = "100% UV400 Protection & Iconic Frames",
                Subtitle = "Protect your vision in style with polarized anti-glare TAC lenses and featherlight TR90 frames made for campus and travel.",
                Description = "High-definition optical clarity, shatterproof TAC lenses, and timeless acetate and alloy frames.",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TitleColor = "#0284c7",
                TitleHighlightColor = "#ea580c",
                SubtitleColor = "#334155",
                BannerHeight = 440,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "sgb1", Label = "100% UV400 Protection", Icon = "shield" },
                    new() { Id = "sgb2", Label = "Polarized Anti-Glare", Icon = "compass" },
                    new() { Id = "sgb3", Label = "Shatterproof TAC Lenses", Icon = "feather" },
                    new() { Id = "sgb4", Label = "Lightweight TR90 Frames", Icon = "wind" }
                },
                RightCard = new FashionHeroRightCardDto
                {
                    Title = "Clear Vision.",
                    TitleHighlight = "Iconic Frames.",
                    Features = new List<FashionHeroFeatureDto>
                    {
                        new() { Id = "sgf1", Title = "100% UV400 Protection", Icon = "shield" },
                        new() { Id = "sgf2", Title = "Polarized Anti-Glare", Icon = "compass" },
                        new() { Id = "sgf3", Title = "TR90 Lightweight", Icon = "wind" },
                        new() { Id = "sgf4", Title = "Shatterproof Lenses", Icon = "feather" }
                    }
                }
            },
            CategoriesSection = new FashionCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Categories →",
                ViewAllUrl = "/category/fashion/sunglasses",
                Categories = new List<FashionCategoryItemDto>
                {
                    new() { Id = "sgc1", Name = "Aviator Sunglasses", Slug = "aviator", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },
                    new() { Id = "sgc2", Name = "Wayfarer Classics", Slug = "wayfarer", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },
                    new() { Id = "sgc3", Name = "Round Retro Shades", Slug = "round-retro", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },
                    new() { Id = "sgc4", Name = "Clubmaster & Browline", Slug = "clubmaster", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },
                    new() { Id = "sgc5", Name = "Cat Eye Shades", Slug = "cat-eye", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true },
                    new() { Id = "sgc6", Name = "Polarized Sports Wraps", Slug = "polarized-sports", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks in Sunglasses",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/category/fashion/sunglasses",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "sgp1", Name = "Classic Teardrop Polarized Aviator Sunglasses", CategoryName = "Sunglasses", CategorySlug = "aviator", Price = 799, OriginalPrice = 1599, Rating = 4.9m, ReviewsCount = 280, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/aviator", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Medium", "Large" }, Colors = new List<string> { "Gold Frame / Green Lens", "Silver / Grey", "All Black" } },
                    new() { Id = "sgp2", Name = "Iconic Matte Black Acetate Wayfarer Shades", CategoryName = "Sunglasses", CategorySlug = "wayfarer", Price = 899, OriginalPrice = 1799, Rating = 4.8m, ReviewsCount = 210, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/wayfarer", IsUploadingSoon = true, Badge = "Iconic Classic", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Matte Black", "Tortoise Shell" } },
                    new() { Id = "sgp3", Name = "John Lennon Vintage Circular Wire Shades", CategoryName = "Sunglasses", CategorySlug = "round-retro", Price = 699, OriginalPrice = 1399, Rating = 4.7m, ReviewsCount = 165, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/round-retro", IsUploadingSoon = true, Badge = "Retro Vibe", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Gold / Dark Green", "Black / Grey" } },
                    new() { Id = "sgp4", Name = "Half-Rim Browline Clubmaster Gradient Sunglasses", CategoryName = "Sunglasses", CategorySlug = "clubmaster", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/clubmaster", IsUploadingSoon = true, Badge = "Preppy Look", Sizes = new List<string> { "Medium" }, Colors = new List<string> { "Black & Gold", "Tortoise" } },
                    new() { Id = "sgp5", Name = "Exaggerated Winged Vintage Cat Eye Sunglasses", CategoryName = "Sunglasses", CategorySlug = "cat-eye", Price = 749, OriginalPrice = 1499, Rating = 4.8m, ReviewsCount = 190, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/cat-eye", IsUploadingSoon = true, Badge = "Chic Winged", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Gloss Black", "Milky White", "Red" } },
                    new() { Id = "sgp6", Name = "High-Impact Aerodynamic UV400 Sports Wraps", CategoryName = "Sunglasses", CategorySlug = "polarized-sports", Price = 1099, OriginalPrice = 2199, Rating = 4.9m, ReviewsCount = 130, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/polarized-sports", IsUploadingSoon = true, Badge = "Performance Grip", Sizes = new List<string> { "Sport Fit" }, Colors = new List<string> { "Neon Blue Mirror", "Fire Red Mirror" } }
                }
            },
            TrustBadgesSection = CreateDefaultTrustBadges(),
            BottomBanner = CreateDefaultBottomBanner("Face Shape & Shades Quiz", "/find-my-kit"),
            SubcategoryPages = new List<FashionSubcategoryPageDto>
            {
                new()
                {
                    CategorySlug = "aviator",
                    CategoryName = "Aviator Sunglasses",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Aviator Sunglasses",
                        TitleHighlight = "TIMELESS DOUBLE-BRIDGE • 100% POLARIZED",
                        Subtitle = "The timeless military pilot silhouette with spring hinges, adjustable silicone nose pads, and 100% UV400 protection.",
                        ThemeColor = "#0284c7",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "av1", Name = "Classic Teardrop Polarized Aviator Sunglasses", CategoryName = "Aviator Sunglasses", CategorySlug = "aviator", Price = 799, OriginalPrice = 1599, Rating = 4.9m, ReviewsCount = 280, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/aviator", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "58mm Medium", "62mm Large" }, Colors = new List<string> { "Gold / G-15 Green", "Silver / Grey", "All Black" } },
                        new() { Id = "av2", Name = "Flat-Top Contemporary Metal Aviator Shades", CategoryName = "Aviator Sunglasses", CategorySlug = "aviator", Price = 899, OriginalPrice = 1799, Rating = 4.8m, ReviewsCount = 130, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/aviator", IsUploadingSoon = true, Badge = "Modern Top", Sizes = new List<string> { "Medium" }, Colors = new List<string> { "Gunmetal", "Matte Gold" } },
                        new() { Id = "av3", Name = "Gradient Amber Lens Driving Aviators", CategoryName = "Aviator Sunglasses", CategorySlug = "aviator", Price = 949, OriginalPrice = 1899, Rating = 4.7m, ReviewsCount = 95, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/aviator", IsUploadingSoon = true, Badge = "High Contrast", Sizes = new List<string> { "58mm" }, Colors = new List<string> { "Bronze / Amber", "Gold / Brown" } },
                        new() { Id = "av4", Name = "Mirrored Flash Pilot Sunglasses with Spring Hinges", CategoryName = "Aviator Sunglasses", CategorySlug = "aviator", Price = 849, OriginalPrice = 1699, Rating = 4.8m, ReviewsCount = 112, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/aviator", IsUploadingSoon = true, Badge = "Silver Mirror", Sizes = new List<string> { "60mm" }, Colors = new List<string> { "Silver Mirror", "Blue Mirror" } }
                    }
                },
                new()
                {
                    CategorySlug = "wayfarer",
                    CategoryName = "Wayfarer Classics",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Wayfarer Classics",
                        TitleHighlight = "BOLD TRAPEZOID • TIMELESS ROCK VIBE",
                        Subtitle = "Durable molded acetate frames with metal rivet hinges and dark polarized lenses that fit almost every face structure.",
                        ThemeColor = "#0284c7",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "wf1", Name = "Iconic Matte Black Acetate Wayfarer Shades", CategoryName = "Wayfarer Classics", CategorySlug = "wayfarer", Price = 899, OriginalPrice = 1799, Rating = 4.8m, ReviewsCount = 210, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/wayfarer", IsUploadingSoon = true, Badge = "Iconic Classic", Sizes = new List<string> { "50mm", "54mm" }, Colors = new List<string> { "Matte Black", "Tortoise Shell" } },
                        new() { Id = "wf2", Name = "Gloss Black Dark Polarized Campus Wayfarers", CategoryName = "Wayfarer Classics", CategorySlug = "wayfarer", Price = 799, OriginalPrice = 1599, Rating = 4.9m, ReviewsCount = 170, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/wayfarer", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "52mm" }, Colors = new List<string> { "Gloss Black" } },
                        new() { Id = "wf3", Name = "Vintage Tortoise Shell Warm Tint Shades", CategoryName = "Wayfarer Classics", CategorySlug = "wayfarer", Price = 949, OriginalPrice = 1899, Rating = 4.7m, ReviewsCount = 88, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/wayfarer", IsUploadingSoon = true, Badge = "Havana Vibe", Sizes = new List<string> { "50mm" }, Colors = new List<string> { "Dark Havana", "Honey Tortoise" } },
                        new() { Id = "wf4", Name = "Translucent Crystal Frame Gradient Sunglasses", CategoryName = "Wayfarer Classics", CategorySlug = "wayfarer", Price = 849, OriginalPrice = 1699, Rating = 4.8m, ReviewsCount = 104, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/wayfarer", IsUploadingSoon = true, Badge = "Crystal Frame", Sizes = new List<string> { "52mm" }, Colors = new List<string> { "Clear Grey", "Champagne Clear" } }
                    }
                },
                new()
                {
                    CategorySlug = "round-retro",
                    CategoryName = "Round Retro Shades",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Round Retro Shades",
                        TitleHighlight = "ARTISTIC CIRCULAR • WIRE FRAME RETRO",
                        Subtitle = "Vintage 60s inspired slender circular metal frames, dark tint lenses, and tortoise ear tips for academic and creative vibes.",
                        ThemeColor = "#0284c7",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "rr1", Name = "John Lennon Vintage Circular Wire Shades", CategoryName = "Round Retro Shades", CategorySlug = "round-retro", Price = 699, OriginalPrice = 1399, Rating = 4.7m, ReviewsCount = 165, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/round-retro", IsUploadingSoon = true, Badge = "Retro Vibe", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Gold / Dark Green", "Black / Grey" } },
                        new() { Id = "rr2", Name = "Steampunk Side-Shield Round Polarized Glasses", CategoryName = "Round Retro Shades", CategorySlug = "round-retro", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/round-retro", IsUploadingSoon = true, Badge = "Side Shield", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Black & Gold", "Silver" } },
                        new() { Id = "rr3", Name = "Thick Rim Acetate Retro Round Sunglasses", CategoryName = "Round Retro Shades", CategorySlug = "round-retro", Price = 849, OriginalPrice = 1699, Rating = 4.8m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/round-retro", IsUploadingSoon = true, Badge = "Acetate Round", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Black", "Tortoise" } },
                        new() { Id = "rr4", Name = "Rose Gold Tinted Rimless Round Shades", CategoryName = "Round Retro Shades", CategorySlug = "round-retro", Price = 799, OriginalPrice = 1599, Rating = 4.6m, ReviewsCount = 85, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/round-retro", IsUploadingSoon = true, Badge = "Rimless Chic", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Rose Tint", "Ocean Blue Tint" } }
                    }
                },
                new()
                {
                    CategorySlug = "clubmaster",
                    CategoryName = "Clubmaster & Browline",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Clubmaster & Browline",
                        TitleHighlight = "HALF-RIM CHARM • INTELLECTUAL DISTINCTION",
                        Subtitle = "Pronounced upper browline acetate with gleaming lower wire rims, delivering quintessential retro-modern collegiate style.",
                        ThemeColor = "#0284c7",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "cm1", Name = "Half-Rim Browline Clubmaster Gradient Sunglasses", CategoryName = "Clubmaster & Browline", CategorySlug = "clubmaster", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/clubmaster", IsUploadingSoon = true, Badge = "Preppy Look", Sizes = new List<string> { "49mm", "51mm" }, Colors = new List<string> { "Black & Gold", "Tortoise" } },
                        new() { Id = "cm2", Name = "Matte Black Minimalist Browline Polarized Shades", CategoryName = "Clubmaster & Browline", CategorySlug = "clubmaster", Price = 899, OriginalPrice = 1799, Rating = 4.9m, ReviewsCount = 120, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/clubmaster", IsUploadingSoon = true, Badge = "Stealth Browline", Sizes = new List<string> { "Medium" }, Colors = new List<string> { "Matte Black & Silver" } },
                        new() { Id = "cm3", Name = "Wood-Grain Texture Clubmaster Sunglasses", CategoryName = "Clubmaster & Browline", CategorySlug = "clubmaster", Price = 1199, OriginalPrice = 2399, Rating = 4.8m, ReviewsCount = 76, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/clubmaster", IsUploadingSoon = true, Badge = "Wood Texture", Sizes = new List<string> { "Medium" }, Colors = new List<string> { "Walnut Brown", "Bamboo Tan" } },
                        new() { Id = "cm4", Name = "Blue Light Blocking Browline Computer Glasses", CategoryName = "Clubmaster & Browline", CategorySlug = "clubmaster", Price = 799, OriginalPrice = 1599, Rating = 4.7m, ReviewsCount = 160, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/clubmaster", IsUploadingSoon = true, Badge = "Study Friendly", Sizes = new List<string> { "Medium" }, Colors = new List<string> { "Black", "Clear" } }
                    }
                },
                new()
                {
                    CategorySlug = "cat-eye",
                    CategoryName = "Cat Eye Shades",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Cat Eye Shades",
                        TitleHighlight = "DRAMATIC WINGED • ICONIC FEMININITY",
                        Subtitle = "Sculpted upswept corners, thick glossy temples, and UV400 dark lenses that make a bold, unforgettable campus statement.",
                        ThemeColor = "#0284c7",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "ce1", Name = "Exaggerated Winged Vintage Cat Eye Sunglasses", CategoryName = "Cat Eye Shades", CategorySlug = "cat-eye", Price = 749, OriginalPrice = 1499, Rating = 4.8m, ReviewsCount = 190, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/cat-eye", IsUploadingSoon = true, Badge = "Chic Winged", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Gloss Black", "Milky White", "Red" } },
                        new() { Id = "ce2", Name = "Slim Narrow 90s Matrix Cat Eye Sunglasses", CategoryName = "Cat Eye Shades", CategorySlug = "cat-eye", Price = 699, OriginalPrice = 1399, Rating = 4.7m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/cat-eye", IsUploadingSoon = true, Badge = "90s Matrix", Sizes = new List<string> { "Narrow" }, Colors = new List<string> { "Black", "Neon Lime" } },
                        new() { Id = "ce3", Name = "Oversized Dramatic Diva Cat Eye Shades", CategoryName = "Cat Eye Shades", CategorySlug = "cat-eye", Price = 899, OriginalPrice = 1799, Rating = 4.9m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/cat-eye", IsUploadingSoon = true, Badge = "Oversized", Sizes = new List<string> { "Oversized" }, Colors = new List<string> { "Tortoise", "Burgundy" } },
                        new() { Id = "ce4", Name = "Metal Temple Accent Pointed Cat Eye Glasses", CategoryName = "Cat Eye Shades", CategorySlug = "cat-eye", Price = 799, OriginalPrice = 1599, Rating = 4.8m, ReviewsCount = 92, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/cat-eye", IsUploadingSoon = true, Badge = "Gold Accent", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Black & Gold", "Nude" } }
                    }
                },
                new()
                {
                    CategorySlug = "polarized-sports",
                    CategoryName = "Polarized Sports Wraps",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Polarized Sports Wraps",
                        TitleHighlight = "AERODYNAMIC SHIELD • SHATTERPROOF TAC",
                        Subtitle = "Wraparound peripheral vision protection, non-slip rubber ear grips, and anti-fog ventilation slits for cycling, running, and cricket.",
                        ThemeColor = "#0284c7",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "ps1", Name = "High-Impact Aerodynamic UV400 Sports Wraps", CategoryName = "Polarized Sports Wraps", CategorySlug = "polarized-sports", Price = 1099, OriginalPrice = 2199, Rating = 4.9m, ReviewsCount = 130, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/polarized-sports", IsUploadingSoon = true, Badge = "Performance Grip", Sizes = new List<string> { "Sport Fit" }, Colors = new List<string> { "Neon Blue Mirror", "Fire Red Mirror" } },
                        new() { Id = "ps2", Name = "Ultra-Light TR90 Cycling & Running Sunglasses", CategoryName = "Polarized Sports Wraps", CategorySlug = "polarized-sports", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 115, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/polarized-sports", IsUploadingSoon = true, Badge = "Feather TR90", Sizes = new List<string> { "Sport Fit" }, Colors = new List<string> { "Black & Lime", "White & Blue" } },
                        new() { Id = "ps3", Name = "5-Lens Interchangeable Outdoor Athletic Goggles", CategoryName = "Polarized Sports Wraps", CategorySlug = "polarized-sports", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/polarized-sports", IsUploadingSoon = true, Badge = "5-Lens Kit", Sizes = new List<string> { "Universal Kit" }, Colors = new List<string> { "Matte Black Kit" } },
                        new() { Id = "ps4", Name = "Floating Polarized Water Sports Beach Glasses", CategoryName = "Polarized Sports Wraps", CategorySlug = "polarized-sports", Price = 1199, OriginalPrice = 2399, Rating = 4.7m, ReviewsCount = 68, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/sunglasses/polarized-sports", IsUploadingSoon = true, Badge = "Floats in Water", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Orange Mirror", "Aqua Blue" } }
                    }
                }
            }
        };
    }
    #endregion

    #region Factory: Caps & Hats
    public static FashionItemHubDto CreateDefaultCapsHats()
    {
        return new FashionItemHubDto
        {
            CategorySlug = "caps-hats",
            CategoryName = "Caps & Hats",
            IsEnabled = true,
            FestivalTheme = new FashionFestivalThemeDto
            {
                IsEnabled = true,
                FestivalName = "Streetwear & Sun Protection Headwear",
                AnnouncementText = "🧢 Top Off Your Look: Washed cotton dad caps, bucket hats & winter beanies with student discounts!",
                BadgeText = "HEADWEAR SPECIAL",
                BannerHighlight = "Up to 45% Off",
                BgGradient = "linear-gradient(135deg, #3b82f6 0%, #1d4ed8 100%)"
            },
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Caps & Hats",
                TitleHighlight = "Streetwear Edge & Sun Protection",
                Subtitle = "Breathable 100% cotton dad caps, trendy bucket hats, and thermal knit beanies engineered for active days and travel adventures.",
                Description = "Pure washed cotton twill, internal moisture-wicking sweatbands, and durable adjustable metal buckle closures.",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TitleColor = "#1d4ed8",
                TitleHighlightColor = "#ea580c",
                SubtitleColor = "#334155",
                BannerHeight = 440,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "chb1", Label = "100% Washed Cotton Twill", Icon = "feather" },
                    new() { Id = "chb2", Label = "Moisture-Wicking Sweatband", Icon = "wind" },
                    new() { Id = "chb3", Label = "UV Sun Shield UPF 50+", Icon = "shield" },
                    new() { Id = "chb4", Label = "Adjustable Universal Fit", Icon = "compass" }
                },
                RightCard = new FashionHeroRightCardDto
                {
                    Title = "Top Off Your Look.",
                    TitleHighlight = "Everyday Street Style.",
                    Features = new List<FashionHeroFeatureDto>
                    {
                        new() { Id = "chf1", Title = "100% Washed Cotton", Icon = "feather" },
                        new() { Id = "chf2", Title = "Anti-Sweat Band", Icon = "wind" },
                        new() { Id = "chf3", Title = "UPF 50+ Sun Block", Icon = "shield" },
                        new() { Id = "chf4", Title = "Strapback Adjuster", Icon = "compass" }
                    }
                }
            },
            CategoriesSection = new FashionCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Categories →",
                ViewAllUrl = "/category/fashion/caps-hats",
                Categories = new List<FashionCategoryItemDto>
                {
                    new() { Id = "chc1", Name = "Baseball & Dad Caps", Slug = "baseball-caps", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },
                    new() { Id = "chc2", Name = "Snapbacks & Flat Brim", Slug = "snapbacks", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },
                    new() { Id = "chc3", Name = "Beanies & Winter Knits", Slug = "beanies", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },
                    new() { Id = "chc4", Name = "Streetwear Bucket Hats", Slug = "bucket-hats", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },
                    new() { Id = "chc5", Name = "Visors & Sport Caps", Slug = "visors-sport", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true },
                    new() { Id = "chc6", Name = "Fedora & Travel Sun Hats", Slug = "sun-hats-fedora", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks in Caps & Hats",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/category/fashion/caps-hats",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "chp1", Name = "Vintage Washed Cotton 6-Panel Dad Cap", CategoryName = "Caps & Hats", CategorySlug = "baseball-caps", Price = 399, OriginalPrice = 799, Rating = 4.9m, ReviewsCount = 295, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/baseball-caps", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Adjustable Free Size" }, Colors = new List<string> { "Faded Navy", "Washed Black", "Olive" } },
                    new() { Id = "chp2", Name = "Embroidered 3D Letter Flat Brim Snapback", CategoryName = "Caps & Hats", CategorySlug = "snapbacks", Price = 549, OriginalPrice = 1099, Rating = 4.8m, ReviewsCount = 180, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/snapbacks", IsUploadingSoon = true, Badge = "Street King", Sizes = new List<string> { "Snapback Universal" }, Colors = new List<string> { "Black / Red", "Grey / Black" } },
                    new() { Id = "chp3", Name = "Warm Ribbed Fold-Over Cuff Winter Beanie", CategoryName = "Caps & Hats", CategorySlug = "beanies", Price = 349, OriginalPrice = 699, Rating = 4.9m, ReviewsCount = 240, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/beanies", IsUploadingSoon = true, Badge = "Winter Warmth", Sizes = new List<string> { "Stretch Free Size" }, Colors = new List<string> { "Charcoal", "Mustard", "Burgundy" } },
                    new() { Id = "chp4", Name = "Reversible 100% Heavy Canvas Bucket Hat", CategoryName = "Caps & Hats", CategorySlug = "bucket-hats", Price = 499, OriginalPrice = 999, Rating = 4.7m, ReviewsCount = 135, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/bucket-hats", IsUploadingSoon = true, Badge = "2-in-1 Reversible", Sizes = new List<string> { "M (56-58cm)", "L (58-60cm)" }, Colors = new List<string> { "Beige & Black", "Navy & White" } },
                    new() { Id = "chp5", Name = "Ultra-Light Breathable Mesh Running Cap", CategoryName = "Caps & Hats", CategorySlug = "visors-sport", Price = 449, OriginalPrice = 899, Rating = 4.8m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/visors-sport", IsUploadingSoon = true, Badge = "Quick Dry", Sizes = new List<string> { "Adjustable" }, Colors = new List<string> { "Pure White", "Jet Black", "Neon Lime" } },
                    new() { Id = "chp6", Name = "Packable Wide-Brim UV Sun Protection Travel Hat", CategoryName = "Caps & Hats", CategorySlug = "sun-hats-fedora", Price = 699, OriginalPrice = 1399, Rating = 4.7m, ReviewsCount = 92, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/sun-hats-fedora", IsUploadingSoon = true, Badge = "Packable", Sizes = new List<string> { "Free Size with Chin Cord" }, Colors = new List<string> { "Khaki", "Dark Grey" } }
                }
            },
            TrustBadgesSection = CreateDefaultTrustBadges(),
            BottomBanner = CreateDefaultBottomBanner("Cap Style Quiz", "/find-my-kit"),
            SubcategoryPages = new List<FashionSubcategoryPageDto>
            {
                new()
                {
                    CategorySlug = "baseball-caps",
                    CategoryName = "Baseball & Dad Caps",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Baseball & Dad Caps",
                        TitleHighlight = "6-PANEL CURVED VISOR • VINTAGE WASHED",
                        Subtitle = "Unstructured relaxed crown, embroidered eyelets for airflow, and antique brass buckle closure for effortless casual style.",
                        ThemeColor = "#1d4ed8",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "bc1", Name = "Vintage Washed Cotton 6-Panel Dad Cap", CategoryName = "Baseball & Dad Caps", CategorySlug = "baseball-caps", Price = 399, OriginalPrice = 799, Rating = 4.9m, ReviewsCount = 295, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/baseball-caps", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Adjustable" }, Colors = new List<string> { "Faded Navy", "Washed Black", "Olive" } },
                        new() { Id = "bc2", Name = "Collegiate University Embroidered Baseball Cap", CategoryName = "Baseball & Dad Caps", CategorySlug = "baseball-caps", Price = 449, OriginalPrice = 899, Rating = 4.8m, ReviewsCount = 175, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/baseball-caps", IsUploadingSoon = true, Badge = "Campus Icon", Sizes = new List<string> { "Adjustable" }, Colors = new List<string> { "Maroon", "Forest Green" } },
                        new() { Id = "bc3", Name = "Minimalist Monogram Curved Visor Cap", CategoryName = "Baseball & Dad Caps", CategorySlug = "baseball-caps", Price = 429, OriginalPrice = 859, Rating = 4.7m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/baseball-caps", IsUploadingSoon = true, Badge = "Clean Look", Sizes = new List<string> { "Adjustable" }, Colors = new List<string> { "Sand Beige", "All White" } },
                        new() { Id = "bc4", Name = "Distressed Raw Edge Streetwear Dad Hat", CategoryName = "Baseball & Dad Caps", CategorySlug = "baseball-caps", Price = 469, OriginalPrice = 939, Rating = 4.8m, ReviewsCount = 88, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/baseball-caps", IsUploadingSoon = true, Badge = "Distressed", Sizes = new List<string> { "Adjustable" }, Colors = new List<string> { "Charcoal Distressed", "Khaki" } }
                    }
                },
                new()
                {
                    CategorySlug = "snapbacks",
                    CategoryName = "Snapbacks & Flat Brim",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Snapbacks & Flat Brim",
                        TitleHighlight = "STRUCTURED HIGH CROWN • FLAT VISOR",
                        Subtitle = "Buckram-reinforced front panels, classic green under-visor, and durable plastic snap adjuster designed for streetwear culture.",
                        ThemeColor = "#1d4ed8",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "sb1", Name = "Embroidered 3D Letter Flat Brim Snapback", CategoryName = "Snapbacks & Flat Brim", CategorySlug = "snapbacks", Price = 549, OriginalPrice = 1099, Rating = 4.8m, ReviewsCount = 180, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/snapbacks", IsUploadingSoon = true, Badge = "Street King", Sizes = new List<string> { "Universal" }, Colors = new List<string> { "Black / Red", "Grey / Black" } },
                        new() { Id = "sb2", Name = "Two-Tone Retro Contrast Visor Snapback", CategoryName = "Snapbacks & Flat Brim", CategorySlug = "snapbacks", Price = 499, OriginalPrice = 999, Rating = 4.7m, ReviewsCount = 130, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/snapbacks", IsUploadingSoon = true, Badge = "Two Tone", Sizes = new List<string> { "Universal" }, Colors = new List<string> { "Navy / Grey", "Burgundy / Gold" } },
                        new() { Id = "sb3", Name = "Reflective Cyber Logo Flat Brim Cap", CategoryName = "Snapbacks & Flat Brim", CategorySlug = "snapbacks", Price = 599, OriginalPrice = 1199, Rating = 4.9m, ReviewsCount = 95, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/snapbacks", IsUploadingSoon = true, Badge = "Reflective", Sizes = new List<string> { "Universal" }, Colors = new List<string> { "Stealth Black", "Titanium Grey" } },
                        new() { Id = "sb4", Name = "Suede Visor Structured High-Crown Snapback", CategoryName = "Snapbacks & Flat Brim", CategorySlug = "snapbacks", Price = 649, OriginalPrice = 1299, Rating = 4.8m, ReviewsCount = 74, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/snapbacks", IsUploadingSoon = true, Badge = "Suede Peak", Sizes = new List<string> { "Universal" }, Colors = new List<string> { "Tan Suede & Black" } }
                    }
                },
                new()
                {
                    CategorySlug = "beanies",
                    CategoryName = "Beanies & Winter Knits",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Beanies & Winter Knits",
                        TitleHighlight = "THERMAL ACRYLIC KNIT • FOLD-OVER CUFF",
                        Subtitle = "Chunky ribbed weave, snug thermal stretch, and anti-itch soft yarns built to trap body heat on frosty walks to early morning lectures.",
                        ThemeColor = "#1d4ed8",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "bn1", Name = "Warm Ribbed Fold-Over Cuff Winter Beanie", CategoryName = "Beanies & Winter Knits", CategorySlug = "beanies", Price = 349, OriginalPrice = 699, Rating = 4.9m, ReviewsCount = 240, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/beanies", IsUploadingSoon = true, Badge = "Winter Warmth", Sizes = new List<string> { "Stretch Free Size" }, Colors = new List<string> { "Charcoal", "Mustard", "Burgundy" } },
                        new() { Id = "bn2", Name = "Fleece-Lined Heavy Winter Fisherman Beanie", CategoryName = "Beanies & Winter Knits", CategorySlug = "beanies", Price = 449, OriginalPrice = 899, Rating = 4.9m, ReviewsCount = 185, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/beanies", IsUploadingSoon = true, Badge = "Sub-Zero Warm", Sizes = new List<string> { "Stretch Free Size" }, Colors = new List<string> { "Black", "Navy", "Oatmeal" } },
                        new() { Id = "bn3", Name = "Slouchy Hipster Oversized Knit Skull Cap", CategoryName = "Beanies & Winter Knits", CategorySlug = "beanies", Price = 379, OriginalPrice = 759, Rating = 4.7m, ReviewsCount = 112, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/beanies", IsUploadingSoon = true, Badge = "Slouchy Style", Sizes = new List<string> { "Stretch Free Size" }, Colors = new List<string> { "Grey Melange", "Olive" } },
                        new() { Id = "bn4", Name = "Faux Fur Pom-Pom Cable Knit Beanie", CategoryName = "Beanies & Winter Knits", CategorySlug = "beanies", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/beanies", IsUploadingSoon = true, Badge = "Cute Pom-Pom", Sizes = new List<string> { "Stretch Free Size" }, Colors = new List<string> { "Cream White", "Dusty Rose" } }
                    }
                },
                new()
                {
                    CategorySlug = "bucket-hats",
                    CategoryName = "Streetwear Bucket Hats",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Streetwear Bucket Hats",
                        TitleHighlight = "360° ALL-ROUND BRIM • Y2K NOSTALGIA",
                        Subtitle = "Sturdy cotton canvas, downward-slanting brim, and packable crushable design that easily stows in any student backpack.",
                        ThemeColor = "#1d4ed8",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "bh1", Name = "Reversible 100% Heavy Canvas Bucket Hat", CategoryName = "Streetwear Bucket Hats", CategorySlug = "bucket-hats", Price = 499, OriginalPrice = 999, Rating = 4.7m, ReviewsCount = 135, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/bucket-hats", IsUploadingSoon = true, Badge = "2-in-1 Reversible", Sizes = new List<string> { "M (56-58cm)", "L (58-60cm)" }, Colors = new List<string> { "Beige & Black", "Navy & White" } },
                        new() { Id = "bh2", Name = "Washed Corduroy Vintage Ribbed Bucket Hat", CategoryName = "Streetwear Bucket Hats", CategorySlug = "bucket-hats", Price = 549, OriginalPrice = 1099, Rating = 4.8m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/bucket-hats", IsUploadingSoon = true, Badge = "Corduroy Luxe", Sizes = new List<string> { "Universal" }, Colors = new List<string> { "Caramel Tan", "Hunter Green" } },
                        new() { Id = "bh3", Name = "Tie-Dye Festival Aesthetic Cotton Bucket Hat", CategoryName = "Streetwear Bucket Hats", CategorySlug = "bucket-hats", Price = 449, OriginalPrice = 899, Rating = 4.6m, ReviewsCount = 94, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/bucket-hats", IsUploadingSoon = true, Badge = "Festival Hit", Sizes = new List<string> { "Universal" }, Colors = new List<string> { "Pastel Pastel Tie-Dye", "Blue Wave" } },
                        new() { Id = "bh4", Name = "Waterproof Quick-Dry Trail Hiking Bucket Hat", CategoryName = "Streetwear Bucket Hats", CategorySlug = "bucket-hats", Price = 599, OriginalPrice = 1199, Rating = 4.9m, ReviewsCount = 120, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/bucket-hats", IsUploadingSoon = true, Badge = "Waterproof", Sizes = new List<string> { "Universal with Toggle" }, Colors = new List<string> { "Charcoal", "Army Green" } }
                    }
                },
                new()
                {
                    CategorySlug = "visors-sport",
                    CategoryName = "Visors & Sport Caps",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Visors & Sport Caps",
                        TitleHighlight = "FEATHER-LIGHT MESH • OPEN-CROWN AIRFLOW",
                        Subtitle = "Engineered moisture management, anti-glare dark under-bills, and reflective safety accents for runners and outdoor athletes.",
                        ThemeColor = "#1d4ed8",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "vs1", Name = "Ultra-Light Breathable Mesh Running Cap", CategoryName = "Visors & Sport Caps", CategorySlug = "visors-sport", Price = 449, OriginalPrice = 899, Rating = 4.8m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/visors-sport", IsUploadingSoon = true, Badge = "Quick Dry", Sizes = new List<string> { "Adjustable" }, Colors = new List<string> { "Pure White", "Jet Black", "Neon Lime" } },
                        new() { Id = "vs2", Name = "Open-Top Athletic Tennis & Golf Sun Visor", CategoryName = "Visors & Sport Caps", CategorySlug = "visors-sport", Price = 379, OriginalPrice = 759, Rating = 4.7m, ReviewsCount = 85, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/visors-sport", IsUploadingSoon = true, Badge = "Sun Visor", Sizes = new List<string> { "Adjustable" }, Colors = new List<string> { "White", "Navy", "Pink" } },
                        new() { Id = "vs3", Name = "Reflective Night-Running Featherweight Cap", CategoryName = "Visors & Sport Caps", CategorySlug = "visors-sport", Price = 499, OriginalPrice = 999, Rating = 4.9m, ReviewsCount = 92, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/visors-sport", IsUploadingSoon = true, Badge = "Night Reflective", Sizes = new List<string> { "Adjustable" }, Colors = new List<string> { "Silver Grey", "High-Vis Yellow" } },
                        new() { Id = "vs4", Name = "Perforated Laser-Cut Breathable Training Cap", CategoryName = "Visors & Sport Caps", CategorySlug = "visors-sport", Price = 479, OriginalPrice = 959, Rating = 4.8m, ReviewsCount = 78, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/visors-sport", IsUploadingSoon = true, Badge = "Laser Cut", Sizes = new List<string> { "Adjustable" }, Colors = new List<string> { "Black", "Royal Blue" } }
                    }
                },
                new()
                {
                    CategorySlug = "sun-hats-fedora",
                    CategoryName = "Fedora & Travel Sun Hats",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Fedora & Travel Sun Hats",
                        TitleHighlight = "PACKABLE STRAW & FELT • UPF 50+ BROAD BRIM",
                        Subtitle = "Crushable paper straw, teardrop pinched crown, and adjustable wind lanyards for campus sunny days, beach trips, and study abroad tours.",
                        ThemeColor = "#1d4ed8",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "sh1", Name = "Packable Wide-Brim UV Sun Protection Travel Hat", CategoryName = "Fedora & Travel Sun Hats", CategorySlug = "sun-hats-fedora", Price = 699, OriginalPrice = 1399, Rating = 4.7m, ReviewsCount = 92, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/sun-hats-fedora", IsUploadingSoon = true, Badge = "Packable", Sizes = new List<string> { "Free Size with Chin Cord" }, Colors = new List<string> { "Khaki", "Dark Grey" } },
                        new() { Id = "sh2", Name = "Classic Summer Paper Straw Panama Fedora Hat", CategoryName = "Fedora & Travel Sun Hats", CategorySlug = "sun-hats-fedora", Price = 799, OriginalPrice = 1599, Rating = 4.8m, ReviewsCount = 115, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/sun-hats-fedora", IsUploadingSoon = true, Badge = "Panama Style", Sizes = new List<string> { "Medium (57cm)", "Large (59cm)" }, Colors = new List<string> { "Natural Straw", "Ivory" } },
                        new() { Id = "sh3", Name = "Wool Felt Pinched Crown Vintage Fedora", CategoryName = "Fedora & Travel Sun Hats", CategorySlug = "sun-hats-fedora", Price = 999, OriginalPrice = 1999, Rating = 4.9m, ReviewsCount = 80, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/sun-hats-fedora", IsUploadingSoon = true, Badge = "Pure Wool Felt", Sizes = new List<string> { "58cm" }, Colors = new List<string> { "Black", "Charcoal", "Camel" } },
                        new() { Id = "sh4", Name = "Floppy Oversized Ribbon Sun Hat", CategoryName = "Fedora & Travel Sun Hats", CategorySlug = "sun-hats-fedora", Price = 649, OriginalPrice = 1299, Rating = 4.6m, ReviewsCount = 65, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/caps-hats/sun-hats-fedora", IsUploadingSoon = true, Badge = "Sun Diva", Sizes = new List<string> { "Wide Brim" }, Colors = new List<string> { "Tan / Black Ribbon", "Cream" } }
                    }
                }
            }
        };
    }
    #endregion

    #region Factory: Belts & Wallets
    public static FashionItemHubDto CreateDefaultBeltsWallets()
    {
        return new FashionItemHubDto
        {
            CategorySlug = "belts-wallets",
            CategoryName = "Belts & Wallets",
            IsEnabled = true,
            FestivalTheme = new FashionFestivalThemeDto
            {
                IsEnabled = true,
                FestivalName = "Handcrafted Leather & Security Edit",
                AnnouncementText = "👛 Everyday Security: Genuine full-grain leather belts and military-grade RFID skimmer-proof slim wallets!",
                BadgeText = "LEATHER SPECIAL",
                BannerHighlight = "Up to 50% Off",
                BgGradient = "linear-gradient(135deg, #78350f 0%, #451a03 100%)"
            },
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Belts & Wallets",
                TitleHighlight = "Genuine Leather & RFID Protection",
                Subtitle = "Handcrafted formal leather belts, reversible dual-tone buckles, and ultra-thin RFID-blocking travel wallets.",
                Description = "100% full-grain leather, military-grade RFID skimmer protection, heavy-duty alloy hardware, and slim pocket profiles.",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TitleColor = "#78350f",
                TitleHighlightColor = "#ea580c",
                SubtitleColor = "#334155",
                BannerHeight = 440,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "bwb1", Label = "100% Genuine Full Grain", Icon = "shield" },
                    new() { Id = "bwb2", Label = "RFID Skimmer Protection", Icon = "compass" },
                    new() { Id = "bwb3", Label = "Solid Zinc Alloy Buckle", Icon = "feather" },
                    new() { Id = "bwb4", Label = "Slim Front-Pocket Profile", Icon = "wind" }
                },
                RightCard = new FashionHeroRightCardDto
                {
                    Title = "Built To Last.",
                    TitleHighlight = "Everyday Security.",
                    Features = new List<FashionHeroFeatureDto>
                    {
                        new() { Id = "bwf1", Title = "Full Grain Leather", Icon = "shield" },
                        new() { Id = "bwf2", Title = "Military RFID Shield", Icon = "compass" },
                        new() { Id = "bwf3", Title = "Zinc Alloy Buckles", Icon = "feather" },
                        new() { Id = "bwf4", Title = "Slim Ergonomic Design", Icon = "tag" }
                    }
                }
            },
            CategoriesSection = new FashionCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Categories →",
                ViewAllUrl = "/category/fashion/belts-wallets",
                Categories = new List<FashionCategoryItemDto>
                {
                    new() { Id = "bwc1", Name = "Genuine Leather Formal Belts", Slug = "formal-belts", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },
                    new() { Id = "bwc2", Name = "Casual & Reversible Belts", Slug = "casual-reversible", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },
                    new() { Id = "bwc3", Name = "Slim RFID-Blocking Wallets", Slug = "rfid-wallets", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },
                    new() { Id = "bwc4", Name = "Classic Bi-Fold Wallets", Slug = "bifold-wallets", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },
                    new() { Id = "bwc5", Name = "Cardholders & Money Clips", Slug = "cardholders", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true },
                    new() { Id = "bwc6", Name = "Travel Passport Wallets", Slug = "travel-wallets", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks in Belts & Wallets",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/category/fashion/belts-wallets",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "bwp1", Name = "Handcrafted Full-Grain Formal Leather Belt (35mm)", CategoryName = "Belts & Wallets", CategorySlug = "formal-belts", Price = 699, OriginalPrice = 1399, Rating = 4.9m, ReviewsCount = 265, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/formal-belts", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "28-32\"", "32-36\"", "36-40\"", "40-44\"" }, Colors = new List<string> { "Black", "Dark Brown", "Tan" } },
                    new() { Id = "bwp2", Name = "Dual-Color Twist Reversible Casual Leather Belt", CategoryName = "Belts & Wallets", CategorySlug = "casual-reversible", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 210, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/casual-reversible", IsUploadingSoon = true, Badge = "2-in-1 Value", Sizes = new List<string> { "30-34\"", "34-38\"", "38-42\"" }, Colors = new List<string> { "Black / Brown Reversible" } },
                    new() { Id = "bwp3", Name = "Ultra-Slim RFID Blocking Carbon Fiber Cardholder", CategoryName = "Belts & Wallets", CategorySlug = "rfid-wallets", Price = 499, OriginalPrice = 999, Rating = 4.9m, ReviewsCount = 310, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/rfid-wallets", IsUploadingSoon = true, Badge = "RFID Shield", Sizes = new List<string> { "Slim 8-Card Capacity" }, Colors = new List<string> { "Carbon Fiber Black", "Gunmetal" } },
                    new() { Id = "bwp4", Name = "Vintage Hunter Distressed Leather Bi-Fold Wallet", CategoryName = "Belts & Wallets", CategorySlug = "bifold-wallets", Price = 649, OriginalPrice = 1299, Rating = 4.8m, ReviewsCount = 180, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/bifold-wallets", IsUploadingSoon = true, Badge = "Hunter Leather", Sizes = new List<string> { "Standard Bi-Fold" }, Colors = new List<string> { "Distressed Tan", "Dark Coffee" } },
                    new() { Id = "bwp5", Name = "Minimalist Front Pocket Metal Money Clip Wallet", CategoryName = "Belts & Wallets", CategorySlug = "cardholders", Price = 399, OriginalPrice = 799, Rating = 4.7m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/cardholders", IsUploadingSoon = true, Badge = "Minimalist", Sizes = new List<string> { "Compact 6-Card" }, Colors = new List<string> { "Matte Black", "Silver Steel" } },
                    new() { Id = "bwp6", Name = "All-in-One Zipper Travel Passport & Boarding Pass Wallet", CategoryName = "Belts & Wallets", CategorySlug = "travel-wallets", Price = 799, OriginalPrice = 1599, Rating = 4.9m, ReviewsCount = 195, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/travel-wallets", IsUploadingSoon = true, Badge = "Abroad Ready", Sizes = new List<string> { "Passport Size" }, Colors = new List<string> { "Midnight Blue", "Espresso Brown" } }
                }
            },
            TrustBadgesSection = CreateDefaultTrustBadges(),
            BottomBanner = CreateDefaultBottomBanner("Belt Sizing & Wallet Guide", "/find-my-kit"),
            SubcategoryPages = new List<FashionSubcategoryPageDto>
            {
                new()
                {
                    CategorySlug = "formal-belts",
                    CategoryName = "Genuine Leather Formal Belts",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Genuine Leather Formal Belts",
                        TitleHighlight = "FULL-GRAIN HIDE • BRUSHED PIN BUCKLE",
                        Subtitle = "Feathered beveled edges, polished gunmetal hardware, and 100% genuine hide designed to match university presentation suits and dress trousers.",
                        ThemeColor = "#78350f",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "flb1", Name = "Handcrafted Full-Grain Formal Leather Belt (35mm)", CategoryName = "Genuine Leather Formal Belts", CategorySlug = "formal-belts", Price = 699, OriginalPrice = 1399, Rating = 4.9m, ReviewsCount = 265, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/formal-belts", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "30-34\"", "34-38\"", "38-42\"" }, Colors = new List<string> { "Black", "Dark Brown", "Tan" } },
                        new() { Id = "flb2", Name = "Italian Saffiano Textured Formal Dress Belt", CategoryName = "Genuine Leather Formal Belts", CategorySlug = "formal-belts", Price = 799, OriginalPrice = 1599, Rating = 4.8m, ReviewsCount = 140, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/formal-belts", IsUploadingSoon = true, Badge = "Saffiano Scratch-Proof", Sizes = new List<string> { "30-34\"", "34-38\"" }, Colors = new List<string> { "Black", "Deep Navy" } },
                        new() { Id = "flb3", Name = "Automatic Ratchet Track Click Belt (No Holes)", CategoryName = "Genuine Leather Formal Belts", CategorySlug = "formal-belts", Price = 849, OriginalPrice = 1699, Rating = 4.9m, ReviewsCount = 185, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/formal-belts", IsUploadingSoon = true, Badge = "Auto Click Track", Sizes = new List<string> { "Micro-Adjustable Universal" }, Colors = new List<string> { "All Black", "Brown" } },
                        new() { Id = "flb4", Name = "Classic Stitch-Detail Semi-Formal Leather Belt", CategoryName = "Genuine Leather Formal Belts", CategorySlug = "formal-belts", Price = 649, OriginalPrice = 1299, Rating = 4.7m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/formal-belts", IsUploadingSoon = true, Badge = "Double Stitch", Sizes = new List<string> { "32-36\"", "36-40\"" }, Colors = new List<string> { "Cognac Tan", "Black" } }
                    }
                },
                new()
                {
                    CategorySlug = "casual-reversible",
                    CategoryName = "Casual & Reversible Belts",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Casual & Reversible Belts",
                        TitleHighlight = "SWIVEL 360° HARDWARE • BLACK & BROWN IN ONE",
                        Subtitle = "Effortlessly twist the buckle to swap between versatile black and rich brown leather, maximizing luggage space and wardrobe combinations.",
                        ThemeColor = "#78350f",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "crb1", Name = "Dual-Color Twist Reversible Casual Leather Belt", CategoryName = "Casual & Reversible Belts", CategorySlug = "casual-reversible", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 210, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/casual-reversible", IsUploadingSoon = true, Badge = "2-in-1 Value", Sizes = new List<string> { "30-34\"", "34-38\"", "38-42\"" }, Colors = new List<string> { "Black / Brown Reversible" } },
                        new() { Id = "crb2", Name = "Rugged Canvas Military Webbing Belt with D-Ring", CategoryName = "Casual & Reversible Belts", CategorySlug = "casual-reversible", Price = 349, OriginalPrice = 699, Rating = 4.7m, ReviewsCount = 160, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/casual-reversible", IsUploadingSoon = true, Badge = "Rugged Canvas", Sizes = new List<string> { "Free Size 48\" (Cut-to-Fit)" }, Colors = new List<string> { "Army Green", "Black", "Khaki" } },
                        new() { Id = "crb3", Name = "Vintage Distressed Jean Belt with Antique Roller Buckle", CategoryName = "Casual & Reversible Belts", CategorySlug = "casual-reversible", Price = 649, OriginalPrice = 1299, Rating = 4.9m, ReviewsCount = 135, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/casual-reversible", IsUploadingSoon = true, Badge = "Denim Companion", Sizes = new List<string> { "32-36\"", "36-40\"" }, Colors = new List<string> { "Distressed Brown", "Raw Black" } },
                        new() { Id = "crb4", Name = "Braided Stretch Elastic Woven Comfort Belt", CategoryName = "Casual & Reversible Belts", CategorySlug = "casual-reversible", Price = 449, OriginalPrice = 899, Rating = 4.8m, ReviewsCount = 98, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/casual-reversible", IsUploadingSoon = true, Badge = "All-Way Stretch", Sizes = new List<string> { "Stretch Free Size" }, Colors = new List<string> { "Navy & White", "Black" } }
                    }
                },
                new()
                {
                    CategorySlug = "rfid-wallets",
                    CategoryName = "Slim RFID-Blocking Wallets",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Slim RFID-Blocking Wallets",
                        TitleHighlight = "MILITARY RFID SHIELD • FRONT POCKET COMFORT",
                        Subtitle = "Embedded Faraday barrier blocks 13.56 MHz RFID scanners to protect student IDs, metro cards, and bank credentials from digital pickpocketing.",
                        ThemeColor = "#78350f",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "rfw1", Name = "Ultra-Slim RFID Blocking Carbon Fiber Cardholder", CategoryName = "Slim RFID-Blocking Wallets", CategorySlug = "rfid-wallets", Price = 499, OriginalPrice = 999, Rating = 4.9m, ReviewsCount = 310, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/rfid-wallets", IsUploadingSoon = true, Badge = "RFID Shield", Sizes = new List<string> { "8-Card Capacity" }, Colors = new List<string> { "Carbon Fiber Black", "Gunmetal" } },
                        new() { Id = "rfw2", Name = "Quick-Eject Pop-Up Card RFID Aluminum Wallet", CategoryName = "Slim RFID-Blocking Wallets", CategorySlug = "rfid-wallets", Price = 699, OriginalPrice = 1399, Rating = 4.9m, ReviewsCount = 220, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/rfid-wallets", IsUploadingSoon = true, Badge = "Pop-Up Trigger", Sizes = new List<string> { "6-Card Pop-Up + Cash Strap" }, Colors = new List<string> { "Matte Black", "Space Grey" } },
                        new() { Id = "rfw3", Name = "Slim Genuine Leather Pull-Tab RFID Bifold", CategoryName = "Slim RFID-Blocking Wallets", CategorySlug = "rfid-wallets", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 165, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/rfid-wallets", IsUploadingSoon = true, Badge = "Pull-Tab Slot", Sizes = new List<string> { "Ultra Slim" }, Colors = new List<string> { "Vintage Tan", "Black" } },
                        new() { Id = "rfw4", Name = "AirTag Integrated RFID Leather Wallet", CategoryName = "Slim RFID-Blocking Wallets", CategorySlug = "rfid-wallets", Price = 749, OriginalPrice = 1499, Rating = 4.8m, ReviewsCount = 130, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/rfid-wallets", IsUploadingSoon = true, Badge = "AirTag Holder", Sizes = new List<string> { "AirTag Built-In Slot" }, Colors = new List<string> { "Espresso Brown", "Midnight Blue" } }
                    }
                },
                new()
                {
                    CategorySlug = "bifold-wallets",
                    CategoryName = "Classic Bi-Fold Wallets",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Classic Bi-Fold Wallets",
                        TitleHighlight = "TIMELESS FOLD • DUAL CURRENCY COMPARTMENT",
                        Subtitle = "Full-length bills compartment, clear flip-out student ID window, dedicated coin snap pocket, and 8 card slots in handcrafted genuine leather.",
                        ThemeColor = "#78350f",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "bfw1", Name = "Vintage Hunter Distressed Leather Bi-Fold Wallet", CategoryName = "Classic Bi-Fold Wallets", CategorySlug = "bifold-wallets", Price = 649, OriginalPrice = 1299, Rating = 4.8m, ReviewsCount = 180, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/bifold-wallets", IsUploadingSoon = true, Badge = "Hunter Leather", Sizes = new List<string> { "Standard Bi-Fold" }, Colors = new List<string> { "Distressed Tan", "Dark Coffee" } },
                        new() { Id = "bfw2", Name = "Dual Currency Partition Nappa Leather Bi-Fold", CategoryName = "Classic Bi-Fold Wallets", CategorySlug = "bifold-wallets", Price = 699, OriginalPrice = 1399, Rating = 4.9m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/bifold-wallets", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Standard Bi-Fold" }, Colors = new List<string> { "Black", "Chocolate" } },
                        new() { Id = "bfw3", Name = "Bi-Fold with Snap Coin Pocket & Flip ID Window", CategoryName = "Classic Bi-Fold Wallets", CategorySlug = "bifold-wallets", Price = 599, OriginalPrice = 1199, Rating = 4.7m, ReviewsCount = 125, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/bifold-wallets", IsUploadingSoon = true, Badge = "Coin Pocket", Sizes = new List<string> { "Standard Bi-Fold" }, Colors = new List<string> { "Tan", "Navy" } },
                        new() { Id = "bfw4", Name = "Slim Vegan Microfiber Leather Bi-Fold Wallet", CategoryName = "Classic Bi-Fold Wallets", CategorySlug = "bifold-wallets", Price = 449, OriginalPrice = 899, Rating = 4.6m, ReviewsCount = 90, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/bifold-wallets", IsUploadingSoon = true, Badge = "100% Vegan", Sizes = new List<string> { "Slim Fold" }, Colors = new List<string> { "Olive", "Charcoal" } }
                    }
                },
                new()
                {
                    CategorySlug = "cardholders",
                    CategoryName = "Cardholders & Money Clips",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Cardholders & Money Clips",
                        TitleHighlight = "FEATHER-LIGHT MINIMALISM • FRONT POCKET",
                        Subtitle = "Engineered spring-steel cash clips, central stash pocket for folded currency, and quick thumb-access card slots for student quick-taps.",
                        ThemeColor = "#78350f",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "chw1", Name = "Minimalist Front Pocket Metal Money Clip Wallet", CategoryName = "Cardholders & Money Clips", CategorySlug = "cardholders", Price = 399, OriginalPrice = 799, Rating = 4.7m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/cardholders", IsUploadingSoon = true, Badge = "Minimalist", Sizes = new List<string> { "6-Card" }, Colors = new List<string> { "Matte Black", "Silver Steel" } },
                        new() { Id = "chw2", Name = "Ultra-Thin Saffiano Leather 6-Slot Card Sleeve", CategoryName = "Cardholders & Money Clips", CategorySlug = "cardholders", Price = 349, OriginalPrice = 699, Rating = 4.8m, ReviewsCount = 195, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/cardholders", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "6-Slot Ultra Thin" }, Colors = new List<string> { "Black", "Burgundy", "Navy" } },
                        new() { Id = "chw3", Name = "Titanium Anodized Tactical Money Clip Cardholder", CategoryName = "Cardholders & Money Clips", CategorySlug = "cardholders", Price = 549, OriginalPrice = 1099, Rating = 4.9m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/cardholders", IsUploadingSoon = true, Badge = "Titanium Stiff", Sizes = new List<string> { "12-Card Expandable" }, Colors = new List<string> { "Gunmetal", "Burnt Titanium" } },
                        new() { Id = "chw4", Name = "Dual-Sided Clear ID Badge & Travel Card Sleeve", CategoryName = "Cardholders & Money Clips", CategorySlug = "cardholders", Price = 299, OriginalPrice = 599, Rating = 4.7m, ReviewsCount = 88, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/cardholders", IsUploadingSoon = true, Badge = "ID Window", Sizes = new List<string> { "Compact" }, Colors = new List<string> { "Tan", "Black" } }
                    }
                },
                new()
                {
                    CategorySlug = "travel-wallets",
                    CategoryName = "Travel Passport Wallets",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Travel Passport Wallets",
                        TitleHighlight = "AIRPORT READY • BOARDING PASS & PASSPORT ORGANIZER",
                        Subtitle = "Full perimeter zipper, dedicated SIM card & ejector pin slots, pen loop, and water-resistant leather designed for international journeys.",
                        ThemeColor = "#78350f",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "tpw1", Name = "All-in-One Zipper Travel Passport & Boarding Pass Wallet", CategoryName = "Travel Passport Wallets", CategorySlug = "travel-wallets", Price = 799, OriginalPrice = 1599, Rating = 4.9m, ReviewsCount = 195, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/travel-wallets", IsUploadingSoon = true, Badge = "Abroad Ready", Sizes = new List<string> { "Passport Organizer" }, Colors = new List<string> { "Midnight Blue", "Espresso Brown" } },
                        new() { Id = "tpw2", Name = "Slim RFID Passport Cover Sleeve with Card Slots", CategoryName = "Travel Passport Wallets", CategorySlug = "travel-wallets", Price = 449, OriginalPrice = 899, Rating = 4.8m, ReviewsCount = 160, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/travel-wallets", IsUploadingSoon = true, Badge = "Slim Cover", Sizes = new List<string> { "Single Passport" }, Colors = new List<string> { "Gold Foil Navy", "Black", "Rose Gold" } },
                        new() { Id = "tpw3", Name = "Family / Duo Dual-Passport Travel Organizer", CategoryName = "Travel Passport Wallets", CategorySlug = "travel-wallets", Price = 899, OriginalPrice = 1799, Rating = 4.8m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/travel-wallets", IsUploadingSoon = true, Badge = "Dual Passport", Sizes = new List<string> { "2-Passport Capacity" }, Colors = new List<string> { "Charcoal", "Dark Brown" } },
                        new() { Id = "tpw4", Name = "Neck Stash Hidden Travel Wallet with RFID Blocking", CategoryName = "Travel Passport Wallets", CategorySlug = "travel-wallets", Price = 499, OriginalPrice = 999, Rating = 4.7m, ReviewsCount = 85, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/belts-wallets/travel-wallets", IsUploadingSoon = true, Badge = "Anti-Theft Neck", Sizes = new List<string> { "Neck Pouch" }, Colors = new List<string> { "Beige", "Black" } }
                    }
                }
            }
        };
    }
    #endregion

    #region Factory: Socks
    public static FashionItemHubDto CreateDefaultSocks()
    {
        return new FashionItemHubDto
        {
            CategorySlug = "socks",
            CategoryName = "Socks",
            IsEnabled = true,
            FestivalTheme = new FashionFestivalThemeDto
            {
                IsEnabled = true,
                FestivalName = "Fresh Step & Anti-Odor Footing Edit",
                AnnouncementText = "🧦 Fresh Steps Every Day: Antibacterial bamboo & cushioned ankle socks with multi-pack student savings!",
                BadgeText = "SOCKS SPECIAL",
                BannerHighlight = "Buy 2 Get 1 Free / Up to 50% Off",
                BgGradient = "linear-gradient(135deg, #059669 0%, #047857 100%)"
            },
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Socks & Foot Liners",
                TitleHighlight = "Anti-Odor Bamboo & Arch Cushion Tech",
                Subtitle = "Multipack soft combed cotton ankle socks, invisible loafer liners, and warm winter thermal socks designed for endless campus walking.",
                Description = "Pure combed cotton, naturally anti-microbial bamboo yarns, seamless anti-blister toes, and stay-put silicone grips.",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TitleColor = "#047857",
                TitleHighlightColor = "#ea580c",
                SubtitleColor = "#334155",
                BannerHeight = 440,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "skb1", Label = "Anti-Odor Bamboo Fiber", Icon = "feather" },
                    new() { Id = "skb2", Label = "Reinforced Heel & Toe", Icon = "shield" },
                    new() { Id = "skb3", Label = "Seamless Anti-Blister Toe", Icon = "wind" },
                    new() { Id = "skb4", Label = "High-Breathability Mesh", Icon = "compass" }
                },
                RightCard = new FashionHeroRightCardDto
                {
                    Title = "Fresh Steps.",
                    TitleHighlight = "Zero Blisters.",
                    Features = new List<FashionHeroFeatureDto>
                    {
                        new() { Id = "skf1", Title = "Bamboo Anti-Odor Tech", Icon = "feather" },
                        new() { Id = "skf2", Title = "Arch Support Band", Icon = "shield" },
                        new() { Id = "skf3", Title = "Reinforced High-Stress Zones", Icon = "compass" },
                        new() { Id = "skf4", Title = "Seamless Toe Comfort", Icon = "tag" }
                    }
                }
            },
            CategoriesSection = new FashionCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Categories →",
                ViewAllUrl = "/category/fashion/socks",
                Categories = new List<FashionCategoryItemDto>
                {
                    new() { Id = "skc1", Name = "Ankle Athletic Socks", Slug = "ankle-socks", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },
                    new() { Id = "skc2", Name = "Formal & Casual Crew Socks", Slug = "crew-socks", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },
                    new() { Id = "skc3", Name = "No-Show Loafer Liners", Slug = "no-show-liners", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },
                    new() { Id = "skc4", Name = "Thermal Woolen Winter Socks", Slug = "thermal-winter", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },
                    new() { Id = "skc5", Name = "Compression & Sports Socks", Slug = "compression-sports", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks in Socks",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/category/fashion/socks",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "skp1", Name = "5-Pack Anti-Odor Breathable Combed Cotton Ankle Socks", CategoryName = "Socks", CategorySlug = "ankle-socks", Price = 399, OriginalPrice = 799, Rating = 4.9m, ReviewsCount = 340, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/ankle-socks", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Free Size (UK 6-11)" }, Colors = new List<string> { "Black, White & Grey Multi-Pack" } },
                    new() { Id = "skp2", Name = "3-Pack Antibacterial Bamboo Formal Dress Crew Socks", CategoryName = "Socks", CategorySlug = "crew-socks", Price = 449, OriginalPrice = 899, Rating = 4.8m, ReviewsCount = 210, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/crew-socks", IsUploadingSoon = true, Badge = "Bamboo Luxe", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Solid Black", "Dark Navy", "Charcoal" } },
                    new() { Id = "skp3", Name = "6-Pack Non-Slip Silicone Grip Invisible Loafer Liners", CategoryName = "Socks", CategorySlug = "no-show-liners", Price = 349, OriginalPrice = 699, Rating = 4.9m, ReviewsCount = 290, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/no-show-liners", IsUploadingSoon = true, Badge = "Zero Slip", Sizes = new List<string> { "Free Size (UK 5-10)" }, Colors = new List<string> { "Nude, White, Black" } },
                    new() { Id = "skp4", Name = "3-Pack Heavy Merino Wool Freeze-Proof Winter Thermal Socks", CategoryName = "Socks", CategorySlug = "thermal-winter", Price = 599, OriginalPrice = 1199, Rating = 4.9m, ReviewsCount = 185, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/thermal-winter", IsUploadingSoon = true, Badge = "Sub-Zero Warm", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Nordic Pattern Multi", "Solid Charcoal" } },
                    new() { Id = "skp5", Name = "2-Pack Graduated Calf Arch Compression Running Socks", CategoryName = "Socks", CategorySlug = "compression-sports", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 140, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/compression-sports", IsUploadingSoon = true, Badge = "Recovery Pro", Sizes = new List<string> { "S/M", "L/XL" }, Colors = new List<string> { "Black & Neon Green", "White & Blue" } }
                }
            },
            TrustBadgesSection = CreateDefaultTrustBadges(),
            BottomBanner = CreateDefaultBottomBanner("Sock Length Finder", "/find-my-kit"),
            SubcategoryPages = new List<FashionSubcategoryPageDto>
            {
                new()
                {
                    CategorySlug = "ankle-socks",
                    CategoryName = "Ankle Athletic Socks",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Ankle Athletic Socks",
                        TitleHighlight = "LOW-PROFILE CUFF • TERRY CUSHION SOLE",
                        Subtitle = "Elasticated arch ribbon, breathable instep mesh panel, and reinforced high-wear heel zones crafted for sneakers and gym trainers.",
                        ThemeColor = "#047857",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "as1", Name = "5-Pack Anti-Odor Breathable Combed Cotton Ankle Socks", CategoryName = "Ankle Athletic Socks", CategorySlug = "ankle-socks", Price = 399, OriginalPrice = 799, Rating = 4.9m, ReviewsCount = 340, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/ankle-socks", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Black, White, Grey" } },
                        new() { Id = "as2", Name = "3-Pack Arch Support High-Impact Cushion Ankle Socks", CategoryName = "Ankle Athletic Socks", CategorySlug = "ankle-socks", Price = 349, OriginalPrice = 699, Rating = 4.8m, ReviewsCount = 180, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/ankle-socks", IsUploadingSoon = true, Badge = "Pillow Cushion", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "All Black", "All White" } },
                        new() { Id = "as3", Name = "4-Pack Quarter Ankle Campus Walking Socks", CategoryName = "Ankle Athletic Socks", CategorySlug = "ankle-socks", Price = 379, OriginalPrice = 759, Rating = 4.7m, ReviewsCount = 120, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/ankle-socks", IsUploadingSoon = true, Badge = "Quarter Length", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Pastel Assorted" } },
                        new() { Id = "as4", Name = "3-Pack Seamless Anti-Blister Marathon Low-Cut Socks", CategoryName = "Ankle Athletic Socks", CategorySlug = "ankle-socks", Price = 429, OriginalPrice = 859, Rating = 4.9m, ReviewsCount = 95, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/ankle-socks", IsUploadingSoon = true, Badge = "Marathon Spec", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Neon Accent / Black" } }
                    }
                },
                new()
                {
                    CategorySlug = "crew-socks",
                    CategoryName = "Formal & Casual Crew Socks",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Formal & Casual Crew Socks",
                        TitleHighlight = "MID-CALF STAY-UP FIT • PREMIUM COMBED COTTON",
                        Subtitle = "Fine-gauge knit that stays up all day without pinching, crafted in boardroom formal tones and collegiate retro athletic stripes.",
                        ThemeColor = "#047857",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "cs1", Name = "3-Pack Antibacterial Bamboo Formal Dress Crew Socks", CategoryName = "Formal & Casual Crew Socks", CategorySlug = "crew-socks", Price = 449, OriginalPrice = 899, Rating = 4.8m, ReviewsCount = 210, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/crew-socks", IsUploadingSoon = true, Badge = "Bamboo Luxe", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Solid Black", "Navy", "Charcoal" } },
                        new() { Id = "cs2", Name = "3-Pack Retro Varsity Double-Stripe Athletic Crew Socks", CategoryName = "Formal & Casual Crew Socks", CategorySlug = "crew-socks", Price = 399, OriginalPrice = 799, Rating = 4.9m, ReviewsCount = 190, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/crew-socks", IsUploadingSoon = true, Badge = "Varsity Retro", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "White with Red/Blue Stripes" } },
                        new() { Id = "cs3", Name = "4-Pack Mercersed Cotton Argyle Pattern Dress Socks", CategoryName = "Formal & Casual Crew Socks", CategorySlug = "crew-socks", Price = 499, OriginalPrice = 999, Rating = 4.7m, ReviewsCount = 115, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/crew-socks", IsUploadingSoon = true, Badge = "Argyle Heritage", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Argyle Assorted" } },
                        new() { Id = "cs4", Name = "3-Pack Thick Heavy-Rib Campus Crew Boot Socks", CategoryName = "Formal & Casual Crew Socks", CategorySlug = "crew-socks", Price = 449, OriginalPrice = 899, Rating = 4.8m, ReviewsCount = 88, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/crew-socks", IsUploadingSoon = true, Badge = "Boot Socks", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Oatmeal", "Olive", "Brown" } }
                    }
                },
                new()
                {
                    CategorySlug = "no-show-liners",
                    CategoryName = "No-Show Loafer Liners",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "No-Show Loafer Liners",
                        TitleHighlight = "100% INVISIBLE CUT • QUAD-SILICONE HEEL GRIP",
                        Subtitle = "Ultra-low scooped neckline stays hidden inside loafers, boat shoes, and court sneakers with zero slipping or bunching under the foot.",
                        ThemeColor = "#047857",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "ns1", Name = "6-Pack Non-Slip Silicone Grip Invisible Loafer Liners", CategoryName = "No-Show Loafer Liners", CategorySlug = "no-show-liners", Price = 349, OriginalPrice = 699, Rating = 4.9m, ReviewsCount = 290, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/no-show-liners", IsUploadingSoon = true, Badge = "Zero Slip", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Nude, White, Black" } },
                        new() { Id = "ns2", Name = "4-Pack Ice Silk Ultra-Thin Breathable Loafer Liners", CategoryName = "No-Show Loafer Liners", CategorySlug = "no-show-liners", Price = 399, OriginalPrice = 799, Rating = 4.8m, ReviewsCount = 170, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/no-show-liners", IsUploadingSoon = true, Badge = "Ice Silk Cool", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Beige", "Black", "Grey" } },
                        new() { Id = "ns3", Name = "3-Pack Cushioned Sole No-Show Sneaker Liners", CategoryName = "No-Show Loafer Liners", CategorySlug = "no-show-liners", Price = 329, OriginalPrice = 659, Rating = 4.7m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/no-show-liners", IsUploadingSoon = true, Badge = "Cushioned Base", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Black", "White" } },
                        new() { Id = "ns4", Name = "5-Pack Antibacterial Bamboo Invisible Loafer Socks", CategoryName = "No-Show Loafer Liners", CategorySlug = "no-show-liners", Price = 429, OriginalPrice = 859, Rating = 4.8m, ReviewsCount = 95, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/no-show-liners", IsUploadingSoon = true, Badge = "Bamboo Fresh", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Grey", "Black" } }
                    }
                },
                new()
                {
                    CategorySlug = "thermal-winter",
                    CategoryName = "Thermal Woolen Winter Socks",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Thermal Woolen Winter Socks",
                        TitleHighlight = "MERINO BLEND • HIGH BRUSHED INTERIOR TERRY",
                        Subtitle = "Engineered for snow, sleet, and freezing campuses abroad, trapping body warmth with a 2.3 TOG temperature insulation rating.",
                        ThemeColor = "#047857",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "tw1", Name = "3-Pack Heavy Merino Wool Freeze-Proof Winter Thermal Socks", CategoryName = "Thermal Woolen Winter Socks", CategorySlug = "thermal-winter", Price = 599, OriginalPrice = 1199, Rating = 4.9m, ReviewsCount = 185, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/thermal-winter", IsUploadingSoon = true, Badge = "Sub-Zero Warm", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Nordic Multi", "Charcoal" } },
                        new() { Id = "tw2", Name = "2-Pack Extreme Cold -20°C Thermal Fleece Boot Socks", CategoryName = "Thermal Woolen Winter Socks", CategorySlug = "thermal-winter", Price = 649, OriginalPrice = 1299, Rating = 5.0m, ReviewsCount = 140, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/thermal-winter", IsUploadingSoon = true, Badge = "Freeze Proof", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Black", "Navy" } },
                        new() { Id = "tw3", Name = "3-Pack Vintage Fair Isle Jacquard Pattern Wool Socks", CategoryName = "Thermal Woolen Winter Socks", CategorySlug = "thermal-winter", Price = 549, OriginalPrice = 1099, Rating = 4.8m, ReviewsCount = 95, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/thermal-winter", IsUploadingSoon = true, Badge = "Fair Isle Pattern", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Rustic Pattern Mix" } },
                        new() { Id = "tw4", Name = "Cozy Dormitory Bedtime Fluffy Fuzzy Socks (2-Pack)", CategoryName = "Thermal Woolen Winter Socks", CategorySlug = "thermal-winter", Price = 349, OriginalPrice = 699, Rating = 4.7m, ReviewsCount = 120, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/thermal-winter", IsUploadingSoon = true, Badge = "Cozy Fluff", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "Pastel Pink", "Mint Grey" } }
                    }
                },
                new()
                {
                    CategorySlug = "compression-sports",
                    CategoryName = "Compression & Sports Socks",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Compression & Sports Socks",
                        TitleHighlight = "15-20 mmHg GRADUATED COMPRESSION • SPEED RECOVERY",
                        Subtitle = "Reduces muscle fatigue during long flights, standing lectures, and campus sports with medical-grade graduated pressure.",
                        ThemeColor = "#047857",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "sp1", Name = "2-Pack Graduated Calf Arch Compression Running Socks", CategoryName = "Compression & Sports Socks", CategorySlug = "compression-sports", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 140, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/compression-sports", IsUploadingSoon = true, Badge = "Recovery Pro", Sizes = new List<string> { "S/M", "L/XL" }, Colors = new List<string> { "Black & Neon", "White & Blue" } },
                        new() { Id = "sp2", Name = "Flight Travel Anti-Swelling High Compression Socks", CategoryName = "Compression & Sports Socks", CategorySlug = "compression-sports", Price = 449, OriginalPrice = 899, Rating = 4.9m, ReviewsCount = 165, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/compression-sports", IsUploadingSoon = true, Badge = "Flight Essential", Sizes = new List<string> { "Universal" }, Colors = new List<string> { "Solid Black", "Nude" } },
                        new() { Id = "sp3", Name = "Anti-Skid Gripper Football & Court Sports Grip Socks", CategoryName = "Compression & Sports Socks", CategorySlug = "compression-sports", Price = 399, OriginalPrice = 799, Rating = 4.8m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/compression-sports", IsUploadingSoon = true, Badge = "Rubber Grippers", Sizes = new List<string> { "Free Size" }, Colors = new List<string> { "White", "Black", "Red" } },
                        new() { Id = "sp4", Name = "Plantar Fasciitis Arch Relief Targeted Ankle Sleeve", CategoryName = "Compression & Sports Socks", CategorySlug = "compression-sports", Price = 349, OriginalPrice = 699, Rating = 4.7m, ReviewsCount = 75, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/socks/compression-sports", IsUploadingSoon = true, Badge = "Arch Relief", Sizes = new List<string> { "S/M", "L/XL" }, Colors = new List<string> { "Black & Copper" } }
                    }
                }
            }
        };
    }
    #endregion

    #region Factory: Accessories
    public static FashionItemHubDto CreateDefaultAccessories()
    {
        return new FashionItemHubDto
        {
            CategorySlug = "accessories",
            CategoryName = "Accessories",
            IsEnabled = true,
            FestivalTheme = new FashionFestivalThemeDto
            {
                IsEnabled = true,
                FestivalName = "Refined Accents & Student Campus Gear",
                AnnouncementText = "🎒 Student Essentials & Formal Finishes: Water-resistant backpacks, silk ties, cufflinks & windproof travel umbrellas!",
                BadgeText = "ACCESSORIES SPECIAL",
                BannerHighlight = "Up to 50% Off",
                BgGradient = "linear-gradient(135deg, #4f46e5 0%, #4338ca 100%)"
            },
            HeroBanner = new FashionHeroBannerDto
            {
                IsEnabled = true,
                Title = "Fashion Accessories",
                TitleHighlight = "Campus Practicality & Refined Accents",
                Subtitle = "Complete your student look with water-resistant commuter backpacks, silk presentation ties, cufflinks, winter mufflers, and windproof compact umbrellas.",
                Description = "High-tensile nylon, microfiber silk, stainless steel hardware, and weather-resistant finishes designed for academic and professional success.",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TitleColor = "#4338ca",
                TitleHighlightColor = "#ea580c",
                SubtitleColor = "#334155",
                BannerHeight = 440,
                Badges = new List<FashionHeroBadgeDto>
                {
                    new() { Id = "acb1", Label = "Water-Resistant Commuter Gear", Icon = "shield" },
                    new() { Id = "acb2", Label = "Airport-Friendly Designs", Icon = "compass" },
                    new() { Id = "acb3", Label = "Premium Stainless Steel & Silk", Icon = "feather" },
                    new() { Id = "acb4", Label = "Loved by 8K+ Students", Icon = "users" }
                },
                RightCard = new FashionHeroRightCardDto
                {
                    Title = "Every Detail Matters.",
                    TitleHighlight = "Travel & Campus Ready.",
                    Features = new List<FashionHeroFeatureDto>
                    {
                        new() { Id = "acf1", Title = "Water-Repellent Fabrics", Icon = "shield" },
                        new() { Id = "acf2", Title = "Airport-Friendly Design", Icon = "compass" },
                        new() { Id = "acf3", Title = "Stainless Steel & Zinc", Icon = "feather" },
                        new() { Id = "acf4", Title = "Reinforced Stitching", Icon = "tag" }
                    }
                }
            },
            CategoriesSection = new FashionCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Categories →",
                ViewAllUrl = "/category/fashion/accessories",
                Categories = new List<FashionCategoryItemDto>
                {
                    new() { Id = "acc1", Name = "Backpacks & Bags", Slug = "bags-backpacks", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true },
                    new() { Id = "acc2", Name = "Ties, Bowties & Squares", Slug = "ties-pocket-squares", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true },
                    new() { Id = "acc3", Name = "Cufflinks & Brooches", Slug = "cufflinks-brooches", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true },
                    new() { Id = "acc4", Name = "Scarves & Mufflers", Slug = "scarves-mufflers", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true },
                    new() { Id = "acc5", Name = "Keychains & AirTag Holders", Slug = "keychains-holders", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true },
                    new() { Id = "acc6", Name = "Compact Travel Umbrellas", Slug = "umbrellas-gear", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true }
                }
            },
            TopPicksSection = new FashionTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks in Accessories",
                ViewAllText = "View All Products →",
                ViewAllUrl = "/category/fashion/accessories",
                Products = new List<FashionProductItemDto>
                {
                    new() { Id = "acp1", Name = "Water-Resistant 15.6\" Laptop Commuter Campus Backpack", CategoryName = "Accessories", CategorySlug = "bags-backpacks", Price = 1299, OriginalPrice = 2599, Rating = 4.9m, ReviewsCount = 280, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/bags-backpacks", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "25 Liters Capacity" }, Colors = new List<string> { "Matte Black", "Heather Grey", "Navy" } },
                    new() { Id = "acp2", Name = "Luxury 3-Piece Silk Tie, Pocket Square & Cufflink Gift Box", CategoryName = "Accessories", CategorySlug = "ties-pocket-squares", Price = 799, OriginalPrice = 1599, Rating = 4.9m, ReviewsCount = 210, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/ties-pocket-squares", IsUploadingSoon = true, Badge = "Formal Star", Sizes = new List<string> { "Standard 3-Piece Set" }, Colors = new List<string> { "Royal Navy Paisley", "Burgundy Floral", "Black Jacquard" } },
                    new() { Id = "acp3", Name = "Brushed Stainless Steel Monogram Executive Cufflinks", CategoryName = "Accessories", CategorySlug = "cufflinks-brooches", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/cufflinks-brooches", IsUploadingSoon = true, Badge = "Steel Luxury", Sizes = new List<string> { "Standard Pair" }, Colors = new List<string> { "Silver & Onyx", "Gold & Pearl" } },
                    new() { Id = "acp4", Name = "Ultra-Soft Cashmere-Feel Plaid Winter Campus Scarf", CategoryName = "Accessories", CategorySlug = "scarves-mufflers", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 190, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/scarves-mufflers", IsUploadingSoon = true, Badge = "Winter Essential", Sizes = new List<string> { "180cm x 30cm" }, Colors = new List<string> { "Classic Camel Plaid", "Grey Herringbone" } },
                    new() { Id = "acp5", Name = "Heavy Duty Solid Brass Quick-Release Keychain & Key Organizer", CategoryName = "Accessories", CategorySlug = "keychains-holders", Price = 349, OriginalPrice = 699, Rating = 4.7m, ReviewsCount = 160, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/keychains-holders", IsUploadingSoon = true, Badge = "Durable Brass", Sizes = new List<string> { "Universal" }, Colors = new List<string> { "Raw Brass", "Titanium Black" } },
                    new() { Id = "acp6", Name = "10-Rib Windproof Teflon Coated Auto Open/Close Umbrella", CategoryName = "Accessories", CategorySlug = "umbrellas-gear", Price = 599, OriginalPrice = 1199, Rating = 4.9m, ReviewsCount = 225, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/umbrellas-gear", IsUploadingSoon = true, Badge = "Windproof Storm", Sizes = new List<string> { "Compact 42\" Canopy" }, Colors = new List<string> { "Midnight Black", "Deep Navy" } }
                }
            },
            TrustBadgesSection = CreateDefaultTrustBadges(),
            BottomBanner = CreateDefaultBottomBanner("Accessories Styling Guide", "/find-my-kit"),
            SubcategoryPages = new List<FashionSubcategoryPageDto>
            {
                new()
                {
                    CategorySlug = "bags-backpacks",
                    CategoryName = "Backpacks & Bags",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Backpacks & Bags",
                        TitleHighlight = "WATER-REPELLENT • DEDICATED LAPTOP SLEEVE",
                        Subtitle = "Engineered ergonomics, luggage trolley strap passthrough, hidden anti-theft back pocket, and USB charging port for campus commuting.",
                        ThemeColor = "#4338ca",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "bbp1", Name = "Water-Resistant 15.6\" Laptop Commuter Campus Backpack", CategoryName = "Backpacks & Bags", CategorySlug = "bags-backpacks", Price = 1299, OriginalPrice = 2599, Rating = 4.9m, ReviewsCount = 280, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/bags-backpacks", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "25L" }, Colors = new List<string> { "Matte Black", "Heather Grey", "Navy" } },
                        new() { Id = "bbp2", Name = "Compact Crossbody Sling Chest Bag with Anti-Theft Lock", CategoryName = "Backpacks & Bags", CategorySlug = "bags-backpacks", Price = 749, OriginalPrice = 1499, Rating = 4.8m, ReviewsCount = 190, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/bags-backpacks", IsUploadingSoon = true, Badge = "Anti-Theft Sling", Sizes = new List<string> { "Compact 8L" }, Colors = new List<string> { "Black", "Charcoal" } },
                        new() { Id = "bbp3", Name = "Heavy Duty Canvas Messenger Shoulder Bag (14\" Laptop)", CategoryName = "Backpacks & Bags", CategorySlug = "bags-backpacks", Price = 1199, OriginalPrice = 2399, Rating = 4.8m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/bags-backpacks", IsUploadingSoon = true, Badge = "Canvas Vintage", Sizes = new List<string> { "18L" }, Colors = new List<string> { "Army Khaki", "Coffee Brown" } },
                        new() { Id = "bbp4", Name = "Foldable Packable Ultralight Daypack (20L)", CategoryName = "Backpacks & Bags", CategorySlug = "bags-backpacks", Price = 499, OriginalPrice = 999, Rating = 4.7m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/bags-backpacks", IsUploadingSoon = true, Badge = "Packable", Sizes = new List<string> { "20L Packable" }, Colors = new List<string> { "Black", "Blue" } }
                    }
                },
                new()
                {
                    CategorySlug = "ties-pocket-squares",
                    CategoryName = "Ties, Bowties & Squares",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Ties, Bowties & Squares",
                        TitleHighlight = "1200-NEEDLE JACQUARD • CRISP PRESENTATION POLISH",
                        Subtitle = "High-density microfiber silk, wrinkle-resistant wool blend interlining, and hand-rolled pocket squares for interviews, banquets, and defense presentations.",
                        ThemeColor = "#4338ca",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "tps1", Name = "Luxury 3-Piece Silk Tie, Pocket Square & Cufflink Gift Box", CategoryName = "Ties, Bowties & Squares", CategorySlug = "ties-pocket-squares", Price = 799, OriginalPrice = 1599, Rating = 4.9m, ReviewsCount = 210, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/ties-pocket-squares", IsUploadingSoon = true, Badge = "Formal Star", Sizes = new List<string> { "3-Piece Box Set" }, Colors = new List<string> { "Navy Paisley", "Burgundy Floral", "Black Jacquard" } },
                        new() { Id = "tps2", Name = "Classic 3-Inch Satin Finish Solid Silk Necktie", CategoryName = "Ties, Bowties & Squares", CategorySlug = "ties-pocket-squares", Price = 449, OriginalPrice = 899, Rating = 4.8m, ReviewsCount = 175, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/ties-pocket-squares", IsUploadingSoon = true, Badge = "Solid Clean", Sizes = new List<string> { "3\" Width" }, Colors = new List<string> { "Navy Blue", "Deep Red", "Black", "Emerald" } },
                        new() { Id = "tps3", Name = "Pre-Tied Adjustable Satin Formal Tuxedo Bowtie", CategoryName = "Ties, Bowties & Squares", CategorySlug = "ties-pocket-squares", Price = 349, OriginalPrice = 699, Rating = 4.7m, ReviewsCount = 130, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/ties-pocket-squares", IsUploadingSoon = true, Badge = "Tuxedo Ready", Sizes = new List<string> { "Adjustable Neck" }, Colors = new List<string> { "Classic Black", "Burgundy", "Royal Blue" } },
                        new() { Id = "tps4", Name = "3-Pack Hand-Rolled Pure White Linen Pocket Squares", CategoryName = "Ties, Bowties & Squares", CategorySlug = "ties-pocket-squares", Price = 399, OriginalPrice = 799, Rating = 4.8m, ReviewsCount = 95, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/ties-pocket-squares", IsUploadingSoon = true, Badge = "Pure Linen", Sizes = new List<string> { "33cm x 33cm" }, Colors = new List<string> { "Pure White with Contrast Edges" } }
                    }
                },
                new()
                {
                    CategorySlug = "cufflinks-brooches",
                    CategoryName = "Cufflinks & Brooches",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Cufflinks & Brooches",
                        TitleHighlight = "316L SURGICAL HARDWARE • BULLET-BACK CLOSURE",
                        Subtitle = "Hand-polished mother-of-pearl, onyx inlays, and sleek lapel lapel pins engineered to elevate any French cuff dress shirt.",
                        ThemeColor = "#4338ca",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "cb1", Name = "Brushed Stainless Steel Monogram Executive Cufflinks", CategoryName = "Cufflinks & Brooches", CategorySlug = "cufflinks-brooches", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/cufflinks-brooches", IsUploadingSoon = true, Badge = "Steel Luxury", Sizes = new List<string> { "Pair in Presentation Box" }, Colors = new List<string> { "Silver & Onyx", "Gold & Pearl" } },
                        new() { Id = "cb2", Name = "Onyx Stone & Crystal Formal Black Tie Cufflink Stud Set", CategoryName = "Cufflinks & Brooches", CategorySlug = "cufflinks-brooches", Price = 699, OriginalPrice = 1399, Rating = 4.9m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/cufflinks-brooches", IsUploadingSoon = true, Badge = "Tuxedo Studs Set", Sizes = new List<string> { "2 Cufflinks + 4 Studs" }, Colors = new List<string> { "Silver & Black Onyx" } },
                        new() { Id = "cb3", Name = "Sleek Modern Minimalist Metal Tie Bar Clip", CategoryName = "Cufflinks & Brooches", CategorySlug = "cufflinks-brooches", Price = 299, OriginalPrice = 599, Rating = 4.8m, ReviewsCount = 180, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/cufflinks-brooches", IsUploadingSoon = true, Badge = "Tie Clip", Sizes = new List<string> { "2-Inch Bar" }, Colors = new List<string> { "Brushed Silver", "Matte Black", "Gold" } },
                        new() { Id = "cb4", Name = "Vintage Lion Emblem Chain Suit Lapel Brooch Pin", CategoryName = "Cufflinks & Brooches", CategorySlug = "cufflinks-brooches", Price = 349, OriginalPrice = 699, Rating = 4.7m, ReviewsCount = 85, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/cufflinks-brooches", IsUploadingSoon = true, Badge = "Vintage Pin", Sizes = new List<string> { "Lapel Pin" }, Colors = new List<string> { "Antique Gold", "Antique Silver" } }
                    }
                },
                new()
                {
                    CategorySlug = "scarves-mufflers",
                    CategoryName = "Scarves & Mufflers",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Scarves & Mufflers",
                        TitleHighlight = "CASHMERE-FEEL WEAVE • FRINGED TASSEL EDGES",
                        Subtitle = "Feather-soft thermal yarns, classic tartan and solid jacquards that shield against biting winter drafts while complementing coats and trench outfits.",
                        ThemeColor = "#4338ca",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "sm1", Name = "Ultra-Soft Cashmere-Feel Plaid Winter Campus Scarf", CategoryName = "Scarves & Mufflers", CategorySlug = "scarves-mufflers", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 190, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/scarves-mufflers", IsUploadingSoon = true, Badge = "Winter Essential", Sizes = new List<string> { "180cm x 30cm" }, Colors = new List<string> { "Classic Camel Plaid", "Grey Herringbone" } },
                        new() { Id = "sm2", Name = "Solid Ribbed Heavy Knit Winter Muffler", CategoryName = "Scarves & Mufflers", CategorySlug = "scarves-mufflers", Price = 549, OriginalPrice = 1099, Rating = 4.9m, ReviewsCount = 145, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/scarves-mufflers", IsUploadingSoon = true, Badge = "Chunky Knit", Sizes = new List<string> { "200cm Long" }, Colors = new List<string> { "Charcoal Black", "Navy", "Burgundy" } },
                        new() { Id = "sm3", Name = "Lightweight Modal Summer Sun & Dust Scarf", CategoryName = "Scarves & Mufflers", CategorySlug = "scarves-mufflers", Price = 399, OriginalPrice = 799, Rating = 4.7m, ReviewsCount = 110, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/scarves-mufflers", IsUploadingSoon = true, Badge = "All Season", Sizes = new List<string> { "Large Wrap" }, Colors = new List<string> { "Ivory", "Pastel Blue", "Olive" } },
                        new() { Id = "sm4", Name = "Reversible Dual-Tone Brushed Fleece Muffler", CategoryName = "Scarves & Mufflers", CategorySlug = "scarves-mufflers", Price = 649, OriginalPrice = 1299, Rating = 4.8m, ReviewsCount = 88, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/scarves-mufflers", IsUploadingSoon = true, Badge = "Reversible", Sizes = new List<string> { "190cm x 32cm" }, Colors = new List<string> { "Grey / Charcoal", "Navy / Camel" } }
                    }
                },
                new()
                {
                    CategorySlug = "keychains-holders",
                    CategoryName = "Keychains & AirTag Holders",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Keychains & AirTag Holders",
                        TitleHighlight = "DURABLE SOLID BRASS & LEATHER • ANTI-LOSS",
                        Subtitle = "Spring carabiner clips, full-grain leather key fobs, and snug protective AirTag cases to keep your hostel keys and campus ID safe.",
                        ThemeColor = "#4338ca",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "kh1", Name = "Heavy Duty Solid Brass Quick-Release Keychain & Key Organizer", CategoryName = "Keychains & AirTag Holders", CategorySlug = "keychains-holders", Price = 349, OriginalPrice = 699, Rating = 4.7m, ReviewsCount = 160, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/keychains-holders", IsUploadingSoon = true, Badge = "Durable Brass", Sizes = new List<string> { "Heavy Duty" }, Colors = new List<string> { "Raw Brass", "Titanium Black" } },
                        new() { Id = "kh2", Name = "Genuine Leather Protective AirTag Keychain Case", CategoryName = "Keychains & AirTag Holders", CategorySlug = "keychains-holders", Price = 299, OriginalPrice = 599, Rating = 4.9m, ReviewsCount = 220, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/keychains-holders", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "AirTag Compatible" }, Colors = new List<string> { "Saddle Brown", "Midnight Black" } },
                        new() { Id = "kh3", Name = "Titanium Carabiner Belt Loop Multi-Key Organizer Clip", CategoryName = "Keychains & AirTag Holders", CategorySlug = "keychains-holders", Price = 399, OriginalPrice = 799, Rating = 4.8m, ReviewsCount = 135, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/keychains-holders", IsUploadingSoon = true, Badge = "Carabiner Clip", Sizes = new List<string> { "Ultra Light" }, Colors = new List<string> { "Gunmetal", "Matte Silver" } },
                        new() { Id = "kh4", Name = "Braided Paracord Tactical Survival Keychain with Whistle", CategoryName = "Keychains & AirTag Holders", CategorySlug = "keychains-holders", Price = 249, OriginalPrice = 499, Rating = 4.6m, ReviewsCount = 95, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/keychains-holders", IsUploadingSoon = true, Badge = "Survival Cord", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Army Green", "Black" } }
                    }
                },
                new()
                {
                    CategorySlug = "umbrellas-gear",
                    CategoryName = "Compact Travel Umbrellas",
                    HeroBanner = new FashionSubcategoryHeroDto
                    {
                        Title = "Compact Travel Umbrellas",
                        TitleHighlight = "10-RIB WINDPROOF • TEFLON WATER-REPELLENT",
                        Subtitle = "One-button auto open and collapse, reinforced fiberglass ribs that invert without snapping in gale winds, and pocket-sized folded length.",
                        ThemeColor = "#4338ca",
                        IsUploadingSoon = true
                    },
                    Products = new List<FashionProductItemDto>
                    {
                        new() { Id = "ug1", Name = "10-Rib Windproof Teflon Coated Auto Open/Close Umbrella", CategoryName = "Compact Travel Umbrellas", CategorySlug = "umbrellas-gear", Price = 599, OriginalPrice = 1199, Rating = 4.9m, ReviewsCount = 225, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/umbrellas-gear", IsUploadingSoon = true, Badge = "Windproof Storm", Sizes = new List<string> { "42\" Canopy (Folded 12\")" }, Colors = new List<string> { "Midnight Black", "Deep Navy" } },
                        new() { Id = "ug2", Name = "Mini Featherweight Ultra-Compact Pocket Umbrella (6-Rib)", CategoryName = "Compact Travel Umbrellas", CategorySlug = "umbrellas-gear", Price = 449, OriginalPrice = 899, Rating = 4.7m, ReviewsCount = 180, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/umbrellas-gear", IsUploadingSoon = true, Badge = "Pocket Sized", Sizes = new List<string> { "38\" Canopy (Folded 7\")" }, Colors = new List<string> { "Pastel Pink", "Mint", "Charcoal" } },
                        new() { Id = "ug3", Name = "UPF 50+ UV Black-Coated Dual Sun & Rain Umbrella", CategoryName = "Compact Travel Umbrellas", CategorySlug = "umbrellas-gear", Price = 549, OriginalPrice = 1099, Rating = 4.8m, ReviewsCount = 140, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/umbrellas-gear", IsUploadingSoon = true, Badge = "Sun & Rain 2-in-1", Sizes = new List<string> { "40\" Canopy" }, Colors = new List<string> { "Black Inner / Sky Blue Outer" } },
                        new() { Id = "ug4", Name = "Inverted Reverse Folding Drip-Free Car Umbrella", CategoryName = "Compact Travel Umbrellas", CategorySlug = "umbrellas-gear", Price = 699, OriginalPrice = 1399, Rating = 4.9m, ReviewsCount = 115, ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", ProductUrl = "/category/fashion/accessories/umbrellas-gear", IsUploadingSoon = true, Badge = "No-Drip Inverted", Sizes = new List<string> { "44\" Large Canopy" }, Colors = new List<string> { "Reflective Rim Black", "Navy" } }
                    }
                }
            }
        };
    }
    #endregion

    #region Helper Methods
    private static FashionTrustBadgesSectionDto CreateDefaultTrustBadges()
    {
        return new FashionTrustBadgesSectionDto
        {
            IsEnabled = true,
            Badges = new List<FashionTrustBadgeDto>
            {
                new() { Id = Guid.NewGuid().ToString("N"), Title = "Curated for Students", Subtitle = "Tailored for campus life and international journeys", Icon = "box" },
                new() { Id = Guid.NewGuid().ToString("N"), Title = "100% Genuine Quality", Subtitle = "Rigorous materials inspection on all items", Icon = "shield" },
                new() { Id = Guid.NewGuid().ToString("N"), Title = "Affordable Value", Subtitle = "Best student pricing and transparent costs", Icon = "tag" },
                new() { Id = Guid.NewGuid().ToString("N"), Title = "Fast Campus Delivery", Subtitle = "Delivered directly to your hostel or dorm", Icon = "truck" },
                new() { Id = Guid.NewGuid().ToString("N"), Title = "7-Day Easy Returns", Subtitle = "Hassle-free size and product replacements", Icon = "return" },
                new() { Id = Guid.NewGuid().ToString("N"), Title = "24/7 Student Support", Subtitle = "Friendly assistance whenever you need it", Icon = "headset" }
            }
        };
    }

    private static FashionBottomBannerDto CreateDefaultBottomBanner(string quizTitle, string quizUrl)
    {
        return new FashionBottomBannerDto
        {
            IsEnabled = true,
            QuizTitle = quizTitle,
            QuizSubtitle = "Take our 30-second finder quiz to discover your ideal match",
            QuizButtonText = "Find My Style",
            QuizButtonUrl = quizUrl,
            DestinationsTitle = "Weather-Ready Fashion for Global Destinations",
            DestinationsSubtitle = "USA, UK, Canada, Australia & Europe",
            BulkTitle = "Group Orders for Student Societies",
            BulkSubtitle = "Special bulk pricing and customizations for university clubs",
            BulkButtonText = "Contact Us",
            BulkButtonUrl = "/contact"
        };
    }
    #endregion
}

