namespace Dreamflykit.Application.ElectronicsHub.Models;

public sealed class ElectronicsHubDto
{
    public ElectronicsThemeSettingsDto ThemeSettings { get; set; } = new();
    public ElectronicsHeroBannerDto HeroBanner { get; set; } = new();
    public ElectronicsCategoriesSectionDto CategoriesSection { get; set; } = new();
    public ElectronicsCuratedProductsSectionDto CuratedProductsSection { get; set; } = new();
    public ElectronicsTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();
    public ElectronicsBottomBannerDto BottomBanner { get; set; } = new();

    public static ElectronicsHubDto CreateDefault()
    {
        return new ElectronicsHubDto
        {
            ThemeSettings = new ElectronicsThemeSettingsDto
            {
                StoreTitle = "Dreamflykit Tech • Electronics Store",
                AccentColor = "#0284c7",
                PrimaryColor = "#0f172a",
                FestivalThemeEnabled = false,
                FestivalThemeName = "Student Tech Fest 2026",
                FestivalBannerUrl = ""
            },
            HeroBanner = new ElectronicsHeroBannerDto
            {
                IsEnabled = true,
                Title = "Student Electronics for",
                TitleHighlight = "Every Global Campus",
                Subtitle = "Certified MacBooks, noise-cancelling audio, universal adapters, and tech power for university life.",
                ImageUrl = "/images/hero/hero-electronics.webp",
                IsUploadingSoon = true,
                BannerHeight = 440,
                TitleColor = "#063f91",
                TitleHighlightColor = "#0284c7",
                SubtitleColor = "#334155",
                BadgeBgColor = "rgba(255, 255, 255, 0.94)",
                BadgeTextColor = "#0f172a",
                OverlayGradient = "linear-gradient(90deg, rgba(255, 255, 255, 0.85) 0%, rgba(255, 255, 255, 0.45) 45%, rgba(255, 255, 255, 0.15) 70%, rgba(255, 255, 255, 0.65) 100%)",
                SlideIntervalSeconds = 5,
                Slides = new List<ElectronicsHeroSlideDto>
                {
                    new()
                    {
                        Id = "s1",
                        Title = "Next-Gen Laptops &",
                        TitleHighlight = "MacBook Power",
                        Subtitle = "Universal 110V–240V chargers, long battery life, and global campus warranties.",
                        ImageUrl = "/images/hero/hero-electronics.webp",
                        IsUploadingSoon = true,
                        ButtonText = "Shop Laptops",
                        ButtonUrl = "/category/electronics?q=laptop",
                        DisplayOrder = 1,
                        IsActive = true
                    },
                    new()
                    {
                        Id = "s2",
                        Title = "Active Noise-Cancelling",
                        TitleHighlight = "Study Silence Audio",
                        Subtitle = "Bose, Sony, Apple & Boat headphones for undisturbed library sessions & long-haul flights.",
                        ImageUrl = "/images/hero/hero-electronics.webp",
                        IsUploadingSoon = true,
                        ButtonText = "Explore Audio",
                        ButtonUrl = "/category/electronics?q=headphone",
                        DisplayOrder = 2,
                        IsActive = true
                    },
                    new()
                    {
                        Id = "s3",
                        Title = "Worldwide 150+ Country",
                        TitleHighlight = "Universal Adapters",
                        Subtitle = "Surge-protected multi-plug GaN fast chargers ready for USA, UK, Canada & Europe.",
                        ImageUrl = "/images/hero/hero-electronics.webp",
                        IsUploadingSoon = true,
                        ButtonText = "Shop Adapters",
                        ButtonUrl = "/category/electronics?q=adapter",
                        DisplayOrder = 3,
                        IsActive = true
                    }
                },
                Badges = new List<ElectronicsHeroBadgeDto>
                {
                    new() { Id = "b1", Label = "110V–240V Universal Power", Icon = "zap" },
                    new() { Id = "b2", Label = "100% Genuine Certified Tech", Icon = "shield" },
                    new() { Id = "b3", Label = "International Student Warranty", Icon = "award" },
                    new() { Id = "b4", Label = "Loved by 10K+ Students", Icon = "users" }
                },
                RightCard = new ElectronicsHeroRightCardDto
                {
                    Title = "Smart Tech.",
                    TitleHighlight = "Campuses Abroad.",
                    Features = new List<ElectronicsHeroFeatureDto>
                    {
                        new() { Id = "f1", Title = "Worldwide Voltage Tested", Icon = "zap" },
                        new() { Id = "f2", Title = "Lightweight & Carry-On Ready", Icon = "feather" },
                        new() { Id = "f3", Title = "Pre-Activated Student SIMs", Icon = "sim" }
                    }
                }
            },
            CategoriesSection = new ElectronicsCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All Categories →",
                ViewAllUrl = "/products?category=electronics",
                Categories = new List<ElectronicsCategoryItemDto>
                {
                    new() { Id = "c1", Name = "Laptops & MacBooks", Slug = "laptops-macbooks", Emoji = "💻", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c2", Name = "Noise-Cancelling Headphones", Slug = "headphones-audio", Emoji = "🎧", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c3", Name = "Universal Travel Adapters", Slug = "travel-adapters", Emoji = "🔌", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c4", Name = "Power Banks & Chargers", Slug = "power-banks-chargers", Emoji = "🔋", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c5", Name = "International Student SIMs", Slug = "student-sims", Emoji = "📶", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c6", Name = "Storage & Portable SSDs", Slug = "storage-ssds", Emoji = "💾", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c7", Name = "Smartwatches & Wearables", Slug = "smartwatches-wearables", Emoji = "⌚", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 7, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c8", Name = "Cables, Hubs & Docks", Slug = "cables-hubs-docks", Emoji = "🔗", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 8, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c9", Name = "Monitors & Displays", Slug = "monitors-displays", Emoji = "🖥️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 9, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c10", Name = "Calculators & Tech Tools", Slug = "calculators-tech-tools", Emoji = "🔢", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 10, IsUploadingSoon = true, IsActive = true }
                }
            },
            CuratedProductsSection = new ElectronicsCuratedProductsSectionDto
            {
                IsEnabled = true,
                Title = "Top Tech Picks for Students",
                ViewAllText = "View All Tech Essentials →",
                ViewAllUrl = "/products?category=electronics",
                Products = new List<ElectronicsProductItemDto>
                {
                    new()
                    {
                        Id = "p1",
                        Name = "All-in-One Universal Travel Adapter with USB-C PD",
                        Slug = "universal-travel-adapter-pd",
                        CategoryName = "Universal Travel Adapters",
                        ProductUrl = "/category/electronics?q=adapter",
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        Price = 1499,
                        OriginalPrice = 2499,
                        DiscountPercent = 40,
                        Rating = 4.9,
                        ReviewCount = 384,
                        IsActive = true,
                        InStock = true,
                        Badge = "BESTSELLER"
                    },
                    new()
                    {
                        Id = "p2",
                        Name = "Active Noise-Cancelling Wireless Headphones (40h Battery)",
                        Slug = "anc-wireless-headphones",
                        CategoryName = "Noise-Cancelling Headphones",
                        ProductUrl = "/category/electronics?q=headphone",
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        Price = 5999,
                        OriginalPrice = 9999,
                        DiscountPercent = 40,
                        Rating = 4.8,
                        ReviewCount = 512,
                        IsActive = true,
                        InStock = true,
                        Badge = "STUDENT CHOICE"
                    },
                    new()
                    {
                        Id = "p3",
                        Name = "65W GaN 3-Port Fast Charger (Laptop + Mobile)",
                        Slug = "65w-gan-fast-charger",
                        CategoryName = "Power Banks & Chargers",
                        ProductUrl = "/category/electronics?q=charger",
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        Price = 1999,
                        OriginalPrice = 3499,
                        DiscountPercent = 43,
                        Rating = 4.9,
                        ReviewCount = 280,
                        IsActive = true,
                        InStock = true,
                        Badge = "MUST HAVE"
                    },
                    new()
                    {
                        Id = "p4",
                        Name = "1TB Portable High-Speed USB-C External SSD",
                        Slug = "1tb-portable-external-ssd",
                        CategoryName = "Storage & Portable SSDs",
                        ProductUrl = "/category/electronics?q=ssd",
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        Price = 7499,
                        OriginalPrice = 11999,
                        DiscountPercent = 37,
                        Rating = 4.9,
                        ReviewCount = 194,
                        IsActive = true,
                        InStock = true,
                        Badge = "HIGH SPEED"
                    },
                    new()
                    {
                        Id = "p5",
                        Name = "USA & Canada Pre-Activated Student 5G SIM Card (30-Day)",
                        Slug = "usa-canada-student-sim",
                        CategoryName = "International Student SIMs",
                        ProductUrl = "/category/electronics?q=sim",
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        Price = 2499,
                        OriginalPrice = 3999,
                        DiscountPercent = 38,
                        Rating = 4.8,
                        ReviewCount = 420,
                        IsActive = true,
                        InStock = true,
                        Badge = "INSTANT CONNECT"
                    }
                }
            },
            TrustBadgesSection = new ElectronicsTrustBadgesSectionDto
            {
                IsEnabled = true,
                Badges = new List<ElectronicsTrustBadgeItemDto>
                {
                    new() { Id = "tb1", Title = "100% Genuine Certified Tech", Subtitle = "Direct from brand-authorized distributors", Icon = "shield" },
                    new() { Id = "tb2", Title = "Worldwide Voltage 110V–240V", Subtitle = "Safe for US, UK, Canada & European sockets", Icon = "zap" },
                    new() { Id = "tb3", Title = "Express Student Dispatch", Subtitle = "Fast delivery right before your flight departure", Icon = "truck" },
                    new() { Id = "tb4", Title = "Student Warranty Support", Subtitle = "Hassle-free replacement & international coverage", Icon = "award" }
                }
            },
            BottomBanner = new ElectronicsBottomBannerDto
            {
                IsEnabled = true,
                Title = "Need Complete Tech Packing for Your Destination?",
                Subtitle = "Explore curated tech bundles tested and approved by students in 50+ universities worldwide.",
                ButtonText = "Browse Tech Catalog",
                ButtonUrl = "/products?category=electronics",
                ImageUrl = "/images/hero/hero-electronics.webp",
                IsUploadingSoon = true
            }
        };
    }
}

public sealed class ElectronicsThemeSettingsDto
{
    public string StoreTitle { get; set; } = "Dreamflykit Tech • Electronics Store";
    public string AccentColor { get; set; } = "#0284c7";
    public string PrimaryColor { get; set; } = "#0f172a";
    public bool FestivalThemeEnabled { get; set; } = false;
    public string FestivalThemeName { get; set; } = "Student Tech Fest 2026";
    public string FestivalBannerUrl { get; set; } = "";
}

public sealed class ElectronicsHeroSlideDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public string ButtonText { get; set; } = "Shop Now";
    public string ButtonUrl { get; set; } = "/category/electronics";
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
}

public sealed class ElectronicsHeroBadgeDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Label { get; set; } = string.Empty;
    public string Icon { get; set; } = "zap";
    public bool IsActive { get; set; } = true;
}

public sealed class ElectronicsHeroFeatureDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = "zap";
    public bool IsActive { get; set; } = true;
}

public sealed class ElectronicsHeroRightCardDto
{
    public string Title { get; set; } = "Smart Tech.";
    public string TitleHighlight { get; set; } = "Campuses Abroad.";
    public List<ElectronicsHeroFeatureDto> Features { get; set; } = new();
}

public sealed class ElectronicsHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Student Electronics for";
    public string TitleHighlight { get; set; } = "Every Global Campus";
    public string Subtitle { get; set; } = "Certified MacBooks, noise-cancelling audio, universal adapters, and tech power for university life.";
    public string ImageUrl { get; set; } = "/images/hero/hero-electronics.webp";
    public bool IsUploadingSoon { get; set; } = true;
    public int BannerHeight { get; set; } = 440;
    public string TitleColor { get; set; } = "#063f91";
    public string TitleHighlightColor { get; set; } = "#0284c7";
    public string SubtitleColor { get; set; } = "#334155";
    public string BadgeBgColor { get; set; } = "rgba(255, 255, 255, 0.94)";
    public string BadgeTextColor { get; set; } = "#0f172a";
    public string OverlayGradient { get; set; } = "linear-gradient(90deg, rgba(255, 255, 255, 0.85) 0%, rgba(255, 255, 255, 0.45) 45%, rgba(255, 255, 255, 0.15) 70%, rgba(255, 255, 255, 0.65) 100%)";
    public int SlideIntervalSeconds { get; set; } = 5;
    public List<ElectronicsHeroSlideDto> Slides { get; set; } = new();
    public List<ElectronicsHeroBadgeDto> Badges { get; set; } = new();
    public ElectronicsHeroRightCardDto? RightCard { get; set; } = new();
}

public sealed class ElectronicsCategoryItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Emoji { get; set; } = "⚡";
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
    public bool IsUploadingSoon { get; set; } = true;
}

public sealed class ElectronicsCategoriesSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Shop by Category";
    public string ViewAllText { get; set; } = "View All Categories →";
    public string ViewAllUrl { get; set; } = "/products?category=electronics";
    public List<ElectronicsCategoryItemDto> Categories { get; set; } = new();
}

public sealed class ElectronicsProductItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string ProductUrl { get; set; } = "/category/electronics";
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public decimal Price { get; set; } = 0;
    public decimal OriginalPrice { get; set; } = 0;
    public int DiscountPercent { get; set; } = 0;
    public double Rating { get; set; } = 4.8;
    public int ReviewCount { get; set; } = 120;
    public bool IsActive { get; set; } = true;
    public bool InStock { get; set; } = true;
    public string Badge { get; set; } = "POPULAR";
}

public sealed class ElectronicsCuratedProductsSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Top Tech Picks for Students";
    public string ViewAllText { get; set; } = "View All Tech Essentials →";
    public string ViewAllUrl { get; set; } = "/products?category=electronics";
    public List<ElectronicsProductItemDto> Products { get; set; } = new();
}

public sealed class ElectronicsTrustBadgeItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Icon { get; set; } = "shield";
}

public sealed class ElectronicsTrustBadgesSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public List<ElectronicsTrustBadgeItemDto> Badges { get; set; } = new();
}

public sealed class ElectronicsBottomBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ButtonText { get; set; } = "Explore More";
    public string ButtonUrl { get; set; } = "/products?category=electronics";
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
}

