namespace Dreamflykit.Application.ElectronicsHub.Models;

public sealed class ElectronicsHubDto
{
    public ElectronicsThemeSettingsDto ThemeSettings { get; set; } = new();
    public ElectronicsHeroBannerDto HeroBanner { get; set; } = new();
    public ElectronicsCategoriesSectionDto CategoriesSection { get; set; } = new();
    public ElectronicsCuratedProductsSectionDto CuratedProductsSection { get; set; } = new();
    public ElectronicsTrustBadgesSectionDto TrustBadgesSection { get; set; } = new();
    public ElectronicsBottomBannerDto BottomBanner { get; set; } = new();
    public List<ElectronicsSubcategoryPageDto> SubcategoryPages { get; set; } = new();

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
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
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
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        ButtonText = "Shop Laptops",
                        ButtonUrl = "/category/electronics/laptops",
                        DisplayOrder = 1,
                        IsActive = true
                    },
                    new()
                    {
                        Id = "s2",
                        Title = "Active Noise-Cancelling",
                        TitleHighlight = "Study Silence Audio",
                        Subtitle = "Bose, Sony, Apple & Boat earphones for undisturbed library sessions & long-haul flights.",
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        ButtonText = "Explore Earphones",
                        ButtonUrl = "/category/electronics/earphones",
                        DisplayOrder = 2,
                        IsActive = true
                    },
                    new()
                    {
                        Id = "s3",
                        Title = "Worldwide Voltage Tested",
                        TitleHighlight = "Power & Cables",
                        Subtitle = "Surge-protected multi-plug GaN fast chargers and high-speed storage ready for USA, UK, Canada & Europe.",
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        ButtonText = "Shop Chargers",
                        ButtonUrl = "/category/electronics/chargers-cables",
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
                    new() { Id = "c1", Name = "Laptops", Slug = "laptops", Emoji = "💻", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c2", Name = "Earphones", Slug = "earphones", Emoji = "🎧", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c3", Name = "Grooming", Slug = "grooming", Emoji = "🪒", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c4", Name = "Mobile Cases", Slug = "mobile-cases", Emoji = "📱", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c5", Name = "Storage Devices", Slug = "storage-devices", Emoji = "💾", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c6", Name = "Chargers & Cables", Slug = "chargers-cables", Emoji = "🔌", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c7", Name = "Power Banks", Slug = "power-banks", Emoji = "🔋", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 7, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c8", Name = "Watches", Slug = "watches", Emoji = "⌚", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 8, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c9", Name = "Computer Accessories", Slug = "computer-accessories", Emoji = "🖱️", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 9, IsUploadingSoon = true, IsActive = true },
                    new() { Id = "c10", Name = "Speakers", Slug = "speakers", Emoji = "🔊", ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", DisplayOrder = 10, IsUploadingSoon = true, IsActive = true }
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
                        Name = "MacBook Air 13.6-inch M2 / M3 (16GB RAM, 512GB SSD)",
                        Slug = "macbook-air-m3-16gb",
                        CategoryName = "Laptops",
                        Brand = "Apple",
                        ProductType = "Ultrabook",
                        Specs = "16GB Unified RAM • 512GB SSD • MagSafe 3",
                        ProductUrl = "/category/electronics/laptops",
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        Price = 99900,
                        OriginalPrice = 114900,
                        DiscountPercent = 13,
                        Rating = 4.9,
                        ReviewCount = 420,
                        IsActive = true,
                        InStock = true,
                        IsUploadingSoon = true,
                        Badge = "BESTSELLER"
                    },
                    new()
                    {
                        Id = "p2",
                        Name = "Sony WH-1000XM5 Wireless Noise-Cancelling Headphones",
                        Slug = "sony-wh1000xm5-anc",
                        CategoryName = "Earphones",
                        Brand = "Sony",
                        ProductType = "Over-Ear ANC",
                        Specs = "30h Battery • Industry Leading ANC • Hi-Res LDAC",
                        ProductUrl = "/category/electronics/earphones",
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        Price = 26990,
                        OriginalPrice = 34990,
                        DiscountPercent = 23,
                        Rating = 4.9,
                        ReviewCount = 512,
                        IsActive = true,
                        InStock = true,
                        IsUploadingSoon = true,
                        Badge = "STUDENT CHOICE"
                    },
                    new()
                    {
                        Id = "p3",
                        Name = "Philips Series 7000 Multi-Grooming Kit (14-in-1, 110V-240V)",
                        Slug = "philips-series-7000-groomer",
                        CategoryName = "Grooming",
                        Brand = "Philips",
                        ProductType = "Multi-Grooming Kit",
                        Specs = "DualCut Blades • 120min Runtime • Worldwide Voltage",
                        ProductUrl = "/category/electronics/grooming",
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        Price = 3899,
                        OriginalPrice = 5499,
                        DiscountPercent = 29,
                        Rating = 4.8,
                        ReviewCount = 310,
                        IsActive = true,
                        InStock = true,
                        IsUploadingSoon = true,
                        Badge = "TRAVEL READY"
                    },
                    new()
                    {
                        Id = "p4",
                        Name = "Samsung T7 Shield 1TB Rugged Portable NVMe SSD",
                        Slug = "samsung-t7-shield-1tb",
                        CategoryName = "Storage Devices",
                        Brand = "Samsung",
                        ProductType = "Portable NVMe SSD",
                        Specs = "1050 MB/s • IP65 Water & Dust Resistant • Drop Proof",
                        ProductUrl = "/category/electronics/storage-devices",
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        Price = 8499,
                        OriginalPrice = 12999,
                        DiscountPercent = 35,
                        Rating = 4.9,
                        ReviewCount = 280,
                        IsActive = true,
                        InStock = true,
                        IsUploadingSoon = true,
                        Badge = "HIGH SPEED"
                    },
                    new()
                    {
                        Id = "p5",
                        Name = "Anker 737 Power Bank 24,000mAh (140W Two-Way Fast Charge)",
                        Slug = "anker-737-power-bank-24k",
                        CategoryName = "Power Banks",
                        Brand = "Anker",
                        ProductType = "20,000mAh+ Laptop PD",
                        Specs = "140W PD 3.1 • Smart Digital Display • TSA Flight Safe",
                        ProductUrl = "/category/electronics/power-banks",
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        Price = 11999,
                        OriginalPrice = 15999,
                        DiscountPercent = 25,
                        Rating = 4.9,
                        ReviewCount = 195,
                        IsActive = true,
                        InStock = true,
                        IsUploadingSoon = true,
                        Badge = "TOP RATED"
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
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true
            },
            SubcategoryPages = CreateDefaultSubcategoryPages()
        };
    }

    public static List<ElectronicsSubcategoryPageDto> CreateDefaultSubcategoryPages()
    {
        return new List<ElectronicsSubcategoryPageDto>
        {
            // 1. Laptops
            new()
            {
                Id = "sub_laptops",
                CategorySlug = "laptops",
                CategoryName = "Laptops",
                Emoji = "💻",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Engineered for Global Campuses",
                    TitleHighlight = "Laptops & Computing",
                    Subtitle = "Certified MacBooks, ThinkPads, and student setups with worldwide 110V-240V power and international student warranty.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155",
                    Badges = new List<ElectronicsHeroBadgeDto>
                    {
                        new() { Id = "sb1", Label = "110V–240V Universal Power", Icon = "zap" },
                        new() { Id = "sb2", Label = "Official Campus Warranty", Icon = "shield" },
                        new() { Id = "sb3", Label = "Long Battery Life", Icon = "award" }
                    }
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Top authorized campus computing brands with student discounts & global support.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_lap_1", Name = "Apple", Slug = "apple", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_lap_2", Name = "Dell", Slug = "dell", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_lap_3", Name = "HP", Slug = "hp", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_lap_4", Name = "Lenovo", Slug = "lenovo", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_lap_5", Name = "Asus", Slug = "asus", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_lap_6", Name = "Acer", Slug = "acer", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_lap_7", Name = "Microsoft", Slug = "microsoft", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_lap_8", Name = "Samsung", Slug = "samsung", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                },
                EquipmentTypes = new List<string> { "Ultrabook", "MacBook", "Gaming Laptop", "Student Thin & Light", "2-in-1 Touchscreen", "16GB+ RAM" },
                CuratedProducts = new List<ElectronicsProductItemDto>
                {
                    new()
                    {
                        Id = "lap_p1",
                        Name = "Apple MacBook Air 13.6-inch M2 (16GB RAM, 512GB SSD)",
                        Slug = "apple-macbook-air-m2-16gb",
                        CategoryName = "Laptops",
                        Brand = "Apple",
                        ProductType = "MacBook",
                        Specs = "16GB Unified RAM • 512GB SSD • 18h Battery • MagSafe 3",
                        Price = 99900,
                        OriginalPrice = 114900,
                        DiscountPercent = 13,
                        Rating = 4.9,
                        ReviewCount = 380,
                        Badge = "BESTSELLER",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "lap_p2",
                        Name = "Dell XPS 13 Evo Intel Core i7 (16GB RAM, 512GB NVMe SSD)",
                        Slug = "dell-xps-13-evo-i7",
                        CategoryName = "Laptops",
                        Brand = "Dell",
                        ProductType = "Ultrabook",
                        Specs = "Intel Core i7 13th Gen • 16GB LPDDR5 • 512GB NVMe • Worldwide Support",
                        Price = 89990,
                        OriginalPrice = 109990,
                        DiscountPercent = 18,
                        Rating = 4.8,
                        ReviewCount = 240,
                        Badge = "CAMPUS CHOICE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "lap_p3",
                        Name = "Lenovo ThinkPad T14 Gen 4 AMD Ryzen 7 PRO (16GB RAM)",
                        Slug = "lenovo-thinkpad-t14-gen4",
                        CategoryName = "Laptops",
                        Brand = "Lenovo",
                        ProductType = "Student Thin & Light",
                        Specs = "Ryzen 7 PRO • 16GB RAM • 512GB SSD • Military-Grade Durability",
                        Price = 78990,
                        OriginalPrice = 96990,
                        DiscountPercent = 19,
                        Rating = 4.9,
                        ReviewCount = 185,
                        Badge = "DURABLE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "lap_p4",
                        Name = "HP Envy x360 2-in-1 Convertible Laptop (Intel Core i5 13th Gen)",
                        Slug = "hp-envy-x360-convertible",
                        CategoryName = "Laptops",
                        Brand = "HP",
                        ProductType = "2-in-1 Touchscreen",
                        Specs = "Touchscreen 15.6-inch • Stylus Pen Included • 16GB RAM • 512GB SSD",
                        Price = 72990,
                        OriginalPrice = 88990,
                        DiscountPercent = 18,
                        Rating = 4.7,
                        ReviewCount = 150,
                        Badge = "TOUCHSCREEN",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "lap_p5",
                        Name = "Asus ROG Zephyrus G14 Gaming & STEM Laptop (Ryzen 9, RTX 4060)",
                        Slug = "asus-rog-zephyrus-g14",
                        CategoryName = "Laptops",
                        Brand = "Asus",
                        ProductType = "Gaming Laptop",
                        Specs = "Ryzen 9 7940HS • RTX 4060 8GB • 16GB DDR5 • 1TB SSD • 165Hz ROG Nebula",
                        Price = 129990,
                        OriginalPrice = 154990,
                        DiscountPercent = 16,
                        Rating = 4.9,
                        ReviewCount = 210,
                        Badge = "HIGH POWER",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "lap_p6",
                        Name = "Acer Swift Go 14 AI Laptop (Intel Core Ultra 5, 16GB LPDDR5X)",
                        Slug = "acer-swift-go-14-ai",
                        CategoryName = "Laptops",
                        Brand = "Acer",
                        ProductType = "Ultrabook",
                        Specs = "Intel Core Ultra 5 • 16GB RAM • 512GB Gen4 SSD • OLED 2.8K Display",
                        Price = 64990,
                        OriginalPrice = 79990,
                        DiscountPercent = 19,
                        Rating = 4.7,
                        ReviewCount = 115,
                        Badge = "OLED DISPLAY",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    }
                }
            },

            // 2. Earphones
            new()
            {
                Id = "sub_earphones",
                CategorySlug = "earphones",
                CategoryName = "Earphones",
                Emoji = "🎧",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Distraction-Free Focus & Study",
                    TitleHighlight = "Earphones & Audio",
                    Subtitle = "Immerse in uninterrupted library study sessions and quiet long-haul flights with industry-leading Active Noise Cancellation.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155",
                    Badges = new List<ElectronicsHeroBadgeDto>
                    {
                        new() { Id = "eb1", Label = "Industry Leading ANC", Icon = "zap" },
                        new() { Id = "eb2", Label = "40h+ Extended Battery", Icon = "award" },
                        new() { Id = "eb3", Label = "Lossless Audio Certified", Icon = "shield" }
                    }
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Premium audio brands certified for high-fidelity acoustic performance and ANC comfort.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_aud_1", Name = "Sony", Slug = "sony", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_aud_2", Name = "Apple", Slug = "apple", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_aud_3", Name = "Bose", Slug = "bose", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_aud_4", Name = "Sennheiser", Slug = "sennheiser", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_aud_5", Name = "JBL", Slug = "jbl", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_aud_6", Name = "Boat", Slug = "boat", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_aud_7", Name = "OnePlus", Slug = "oneplus", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_aud_8", Name = "Marshall", Slug = "marshall", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                },
                EquipmentTypes = new List<string> { "TWS Earbuds", "Over-Ear ANC", "Neckband", "Wired Studio", "Spatial Audio", "40h+ Battery" },
                CuratedProducts = new List<ElectronicsProductItemDto>
                {
                    new()
                    {
                        Id = "ear_p1",
                        Name = "Sony WH-1000XM5 Wireless Noise-Cancelling Headphones",
                        Slug = "sony-wh-1000xm5-black",
                        CategoryName = "Earphones",
                        Brand = "Sony",
                        ProductType = "Over-Ear ANC",
                        Specs = "30h Battery • Multi-Point Connection • Speak-to-Chat • 8 Mics ANC",
                        Price = 26990,
                        OriginalPrice = 34990,
                        DiscountPercent = 23,
                        Rating = 4.9,
                        ReviewCount = 480,
                        Badge = "BESTSELLER",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "ear_p2",
                        Name = "Apple AirPods Pro (2nd Generation, MagSafe USB-C Case)",
                        Slug = "apple-airpods-pro-2-usbc",
                        CategoryName = "Earphones",
                        Brand = "Apple",
                        ProductType = "TWS Earbuds",
                        Specs = "H2 Chip • 2x More ANC • Adaptive Audio • Transparency Mode",
                        Price = 21990,
                        OriginalPrice = 24900,
                        DiscountPercent = 12,
                        Rating = 4.9,
                        ReviewCount = 620,
                        Badge = "STUDENT TOP PICK",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "ear_p3",
                        Name = "Bose QuietComfort 45 Bluetooth Wireless Over-Ear Headphones",
                        Slug = "bose-quietcomfort-45-anc",
                        CategoryName = "Earphones",
                        Brand = "Bose",
                        ProductType = "Over-Ear ANC",
                        Specs = "Quiet & Aware Modes • High-Fidelity Audio • 24h Playtime",
                        Price = 24990,
                        OriginalPrice = 29900,
                        DiscountPercent = 16,
                        Rating = 4.8,
                        ReviewCount = 310,
                        Badge = "LEGENDARY SILENCE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "ear_p4",
                        Name = "Sennheiser Momentum True Wireless 4 Noise Cancelling Earbuds",
                        Slug = "sennheiser-momentum-tw4",
                        CategoryName = "Earphones",
                        Brand = "Sennheiser",
                        ProductType = "TWS Earbuds",
                        Specs = "TrueResponse Transducer • Lossless Sound • Adaptive ANC 2.0 • 30h Play",
                        Price = 24990,
                        OriginalPrice = 29990,
                        DiscountPercent = 17,
                        Rating = 4.8,
                        ReviewCount = 145,
                        Badge = "AUDIOPHILE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "ear_p5",
                        Name = "JBL Tune 770NC Wireless Over-Ear Active Noise-Cancelling",
                        Slug = "jbl-tune-770nc-wireless",
                        CategoryName = "Earphones",
                        Brand = "JBL",
                        ProductType = "Over-Ear ANC",
                        Specs = "JBL Pure Bass Sound • 70h Battery • Speed Charge 5min=3h",
                        Price = 5999,
                        OriginalPrice = 9999,
                        DiscountPercent = 40,
                        Rating = 4.7,
                        ReviewCount = 280,
                        Badge = "VALUE CHAMP",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "ear_p6",
                        Name = "Boat Airdopes 141 ANC True Wireless Earbuds with 32dB ANC",
                        Slug = "boat-airdopes-141-anc",
                        CategoryName = "Earphones",
                        Brand = "Boat",
                        ProductType = "TWS Earbuds",
                        Specs = "32dB Hybrid ANC • 42h Total Playtime • ENx Tech 4-Mic Quad System",
                        Price = 1499,
                        OriginalPrice = 3990,
                        DiscountPercent = 62,
                        Rating = 4.6,
                        ReviewCount = 940,
                        Badge = "MEGA VALUE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    }
                }
            },

            // 3. Grooming
            new()
            {
                Id = "sub_grooming",
                CategorySlug = "grooming",
                CategoryName = "Grooming",
                Emoji = "🪒",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Personal Grooming & Care",
                    TitleHighlight = "110V-240V Travel Approved",
                    Subtitle = "Universal voltage cordless beard trimmers, electric shavers, and high-speed ionic hair dryers certified safe for worldwide travel and student dorms.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155",
                    Badges = new List<ElectronicsHeroBadgeDto>
                    {
                        new() { Id = "gb1", Label = "Worldwide Voltage 110V-240V", Icon = "zap" },
                        new() { Id = "gb2", Label = "Cordless Long Runtime", Icon = "award" },
                        new() { Id = "gb3", Label = "IPX7 Waterproof Washable", Icon = "shield" }
                    }
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Global personal care leaders known for precision stainless steel blades and dual-voltage safety.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_grm_1", Name = "Philips", Slug = "philips", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_grm_2", Name = "Braun", Slug = "braun", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_grm_3", Name = "Panasonic", Slug = "panasonic", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_grm_4", Name = "Havells", Slug = "havells", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_grm_5", Name = "Dyson", Slug = "dyson", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_grm_6", Name = "Wahl", Slug = "wahl", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_grm_7", Name = "Xiaomi", Slug = "xiaomi", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_grm_8", Name = "Syska", Slug = "syska", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                },
                EquipmentTypes = new List<string> { "Beard Trimmer", "Electric Shaver", "Hair Dryer", "Nose & Ear Trimmer", "Multi-Grooming Kit", "Fast Charge" },
                CuratedProducts = new List<ElectronicsProductItemDto>
                {
                    new()
                    {
                        Id = "grm_p1",
                        Name = "Philips Series 7000 Multi-Grooming Kit (14-in-1, 110V-240V)",
                        Slug = "philips-series-7000-kit",
                        CategoryName = "Grooming",
                        Brand = "Philips",
                        ProductType = "Multi-Grooming Kit",
                        Specs = "DualCut Self-Sharpening • 120min Runtime • 14 Attachments",
                        Price = 3899,
                        OriginalPrice = 5499,
                        DiscountPercent = 29,
                        Rating = 4.8,
                        ReviewCount = 520,
                        Badge = "BESTSELLER",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "grm_p2",
                        Name = "Braun Series 9 Pro Electric Foil Shaver with Precision Trimmer",
                        Slug = "braun-series-9-pro-shaver",
                        CategoryName = "Grooming",
                        Brand = "Braun",
                        ProductType = "Electric Shaver",
                        Specs = "SyncroSonic Tech • 40,000 Cutting Actions • Wet & Dry • Universal Voltage",
                        Price = 18990,
                        OriginalPrice = 24990,
                        DiscountPercent = 24,
                        Rating = 4.9,
                        ReviewCount = 180,
                        Badge = "PREMIUM",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "grm_p3",
                        Name = "Panasonic Arc5 Wet/Dry 5-Blade Electric Shaver for Men",
                        Slug = "panasonic-arc5-wet-dry",
                        CategoryName = "Grooming",
                        Brand = "Panasonic",
                        ProductType = "Electric Shaver",
                        Specs = "14,000 CPM Linear Motor • 30° Nano Polished Blades • Travel Lock",
                        Price = 14490,
                        OriginalPrice = 18990,
                        DiscountPercent = 24,
                        Rating = 4.8,
                        ReviewCount = 130,
                        Badge = "ULTRA CLOSE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "grm_p4",
                        Name = "Havells Quick-Charge Cordless Beard Trimmer (90min Runtime)",
                        Slug = "havells-quick-charge-trimmer",
                        CategoryName = "Grooming",
                        Brand = "Havells",
                        ProductType = "Beard Trimmer",
                        Specs = "Titanium Coated Blades • 0.5mm Precision • USB Fast Charging",
                        Price = 1499,
                        OriginalPrice = 2395,
                        DiscountPercent = 37,
                        Rating = 4.6,
                        ReviewCount = 310,
                        Badge = "STUDENT VALUE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "grm_p5",
                        Name = "Dyson Supersonic Hair Dryer (Intelligent Heat Control)",
                        Slug = "dyson-supersonic-dryer",
                        CategoryName = "Grooming",
                        Brand = "Dyson",
                        ProductType = "Hair Dryer",
                        Specs = "V9 Digital Motor • Air Multiplier Tech • 5 Styling Attachments",
                        Price = 32900,
                        OriginalPrice = 37900,
                        DiscountPercent = 13,
                        Rating = 4.9,
                        ReviewCount = 220,
                        Badge = "FLAGSHIP",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "grm_p6",
                        Name = "Wahl Professional Cordless Detailer Trimmer (Zero-Overlap)",
                        Slug = "wahl-cordless-detailer",
                        CategoryName = "Grooming",
                        Brand = "Wahl",
                        ProductType = "Beard Trimmer",
                        Specs = "Rotary Motor • T-Wide Blade • 100min Runtime • Dual Voltage",
                        Price = 6490,
                        OriginalPrice = 8500,
                        DiscountPercent = 24,
                        Rating = 4.8,
                        ReviewCount = 160,
                        Badge = "BARBER GRADE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    }
                }
            },

            // 4. Mobile Cases
            new()
            {
                Id = "sub_mobile_cases",
                CategorySlug = "mobile-cases",
                CategoryName = "Mobile Cases",
                Emoji = "📱",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Drop-Tested Mobile Protection",
                    TitleHighlight = "Military-Grade Armor & MagSafe",
                    Subtitle = "Shock-absorbing armor cases, magnetic MagSafe covers, and slim crystal-clear protection designed for travel durability and everyday student life.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155",
                    Badges = new List<ElectronicsHeroBadgeDto>
                    {
                        new() { Id = "mb1", Label = "Military-Grade Drop Certified", Icon = "shield" },
                        new() { Id = "mb2", Label = "Strong MagSafe Ring", Icon = "zap" },
                        new() { Id = "mb3", Label = "Anti-Yellowing UV Protection", Icon = "award" }
                    }
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Top drop-tested protective accessory brands built to safeguard student smartphones.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_cas_1", Name = "Spigen", Slug = "spigen", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_cas_2", Name = "OtterBox", Slug = "otterbox", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_cas_3", Name = "Apple", Slug = "apple", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_cas_4", Name = "Ringke", Slug = "ringke", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_cas_5", Name = "UAG", Slug = "uag", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_cas_6", Name = "ESR", Slug = "esr", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_cas_7", Name = "DailyObjects", Slug = "dailyobjects", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_cas_8", Name = "Caseology", Slug = "caseology", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                },
                EquipmentTypes = new List<string> { "MagSafe Compatible", "Shockproof Armor", "Slim Clear Case", "Leather Wallet Case", "Kickstand Case", "Anti-Scratch" },
                CuratedProducts = new List<ElectronicsProductItemDto>
                {
                    new()
                    {
                        Id = "cas_p1",
                        Name = "Spigen Ultra Hybrid MagSafe Case for iPhone 15 / 16",
                        Slug = "spigen-ultra-hybrid-magsafe",
                        CategoryName = "Mobile Cases",
                        Brand = "Spigen",
                        ProductType = "MagSafe Compatible",
                        Specs = "Air Cushion Tech • Anti-Yellowing Clear Back • Strong Magnets",
                        Price = 1899,
                        OriginalPrice = 2999,
                        DiscountPercent = 37,
                        Rating = 4.8,
                        ReviewCount = 610,
                        Badge = "BESTSELLER",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "cas_p2",
                        Name = "OtterBox Defender Series Rugged Drop-Proof Case",
                        Slug = "otterbox-defender-rugged",
                        CategoryName = "Mobile Cases",
                        Brand = "OtterBox",
                        ProductType = "Shockproof Armor",
                        Specs = "4X Drop+ Standard • Multi-Layer Outer Cover • Port Plugs",
                        Price = 3499,
                        OriginalPrice = 4999,
                        DiscountPercent = 30,
                        Rating = 4.9,
                        ReviewCount = 280,
                        Badge = "MAX DEFENSE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "cas_p3",
                        Name = "Ringke Fusion-X Camo Protective Case (Shock Absorbing TPU)",
                        Slug = "ringke-fusion-x-camo",
                        CategoryName = "Mobile Cases",
                        Brand = "Ringke",
                        ProductType = "Shockproof Armor",
                        Specs = "Corner Bumper Protection • Dual Easy QuickCatch Lanyard Holes",
                        Price = 1299,
                        OriginalPrice = 1999,
                        DiscountPercent = 35,
                        Rating = 4.7,
                        ReviewCount = 420,
                        Badge = "TACTICAL",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "cas_p4",
                        Name = "UAG Urban Armor Gear Monarch Rugged Case (5-Layer Armor)",
                        Slug = "uag-monarch-rugged-case",
                        CategoryName = "Mobile Cases",
                        Brand = "UAG",
                        ProductType = "Shockproof Armor",
                        Specs = "20ft Drop Protection • Carbon Fiber & Alloy Metal Hardware",
                        Price = 4299,
                        OriginalPrice = 5999,
                        DiscountPercent = 28,
                        Rating = 4.9,
                        ReviewCount = 190,
                        Badge = "20FT TESTED",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "cas_p5",
                        Name = "ESR Classic Hybrid Case with HaloLock Magnetic Kickstand",
                        Slug = "esr-classic-hybrid-kickstand",
                        CategoryName = "Mobile Cases",
                        Brand = "ESR",
                        ProductType = "Kickstand Case",
                        Specs = "Adjustable Camera Ring Stand • Military Drop Protection • 1500g Magnetic Hold",
                        Price = 1699,
                        OriginalPrice = 2499,
                        DiscountPercent = 32,
                        Rating = 4.8,
                        ReviewCount = 310,
                        Badge = "HANDS FREE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "cas_p6",
                        Name = "Apple Official Silicone Case with MagSafe (Silky Soft-Touch)",
                        Slug = "apple-official-silicone-magsafe",
                        CategoryName = "Mobile Cases",
                        Brand = "Apple",
                        ProductType = "MagSafe Compatible",
                        Specs = "Microfiber Lining • Perfect Alignment Magnets • Silky Grip",
                        Price = 4499,
                        OriginalPrice = 4900,
                        DiscountPercent = 8,
                        Rating = 4.8,
                        ReviewCount = 520,
                        Badge = "OFFICIAL APPLE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    }
                }
            },

            // 5. Storage Devices
            new()
            {
                Id = "sub_storage",
                CategorySlug = "storage-devices",
                CategoryName = "Storage Devices",
                Emoji = "💾",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "High-Speed Storage & Portable SSDs",
                    TitleHighlight = "Ultra-Fast NVMe • Up to 2000 MB/s",
                    Subtitle = "Pocket-sized external SSDs, high-capacity portable hard drives, and rugged USB-C flash drives to back up research papers, code repositories, and semester files.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155",
                    Badges = new List<ElectronicsHeroBadgeDto>
                    {
                        new() { Id = "stb1", Label = "Up to 2000 MB/s Read & Write", Icon = "zap" },
                        new() { Id = "stb2", Label = "Shock & Drop Resistant", Icon = "shield" },
                        new() { Id = "stb3", Label = "Hardware 256-Bit Encryption", Icon = "award" }
                    }
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "World-class digital storage manufacturers safeguarding student research data worldwide.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_sto_1", Name = "Samsung", Slug = "samsung", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_sto_2", Name = "SanDisk", Slug = "sandisk", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_sto_3", Name = "Western Digital", Slug = "wd", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_sto_4", Name = "Crucial", Slug = "crucial", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_sto_5", Name = "Kingston", Slug = "kingston", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_sto_6", Name = "Seagate", Slug = "seagate", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_sto_7", Name = "Transcend", Slug = "transcend", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_sto_8", Name = "LaCie", Slug = "lacie", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                },
                EquipmentTypes = new List<string> { "Portable NVMe SSD", "Rugged External HDD", "Type-C Flash Drive", "High Endurance microSD", "Hardware Encrypted", "2TB+ Storage" },
                CuratedProducts = new List<ElectronicsProductItemDto>
                {
                    new()
                    {
                        Id = "sto_p1",
                        Name = "Samsung T7 Shield 1TB Rugged Portable NVMe SSD",
                        Slug = "samsung-t7-shield-1tb-ssd",
                        CategoryName = "Storage Devices",
                        Brand = "Samsung",
                        ProductType = "Portable NVMe SSD",
                        Specs = "1050 MB/s Transfer • IP65 Water & Dust Resistant • 3m Drop Proof",
                        Price = 8499,
                        OriginalPrice = 12999,
                        DiscountPercent = 35,
                        Rating = 4.9,
                        ReviewCount = 420,
                        Badge = "BESTSELLER",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "sto_p2",
                        Name = "SanDisk Extreme Portable SSD 1TB (USB 3.2 Gen 2, 1050MB/s)",
                        Slug = "sandisk-extreme-portable-ssd-1tb",
                        CategoryName = "Storage Devices",
                        Brand = "SanDisk",
                        ProductType = "Portable NVMe SSD",
                        Specs = "Handy Carabiner Loop • 2-Meter Drop Protection • AES 256-Bit Encryption",
                        Price = 8999,
                        OriginalPrice = 13999,
                        DiscountPercent = 36,
                        Rating = 4.8,
                        ReviewCount = 510,
                        Badge = "CAMPUS FAVORITE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "sto_p3",
                        Name = "Western Digital My Passport 2TB Portable External Hard Drive",
                        Slug = "wd-my-passport-2tb",
                        CategoryName = "Storage Devices",
                        Brand = "Western Digital",
                        ProductType = "Rugged External HDD",
                        Specs = "SuperSpeed USB-C • Automatic Backup Software • Password Protection",
                        Price = 5499,
                        OriginalPrice = 7999,
                        DiscountPercent = 31,
                        Rating = 4.7,
                        ReviewCount = 360,
                        Badge = "HIGH CAPACITY",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "sto_p4",
                        Name = "Crucial X9 Pro 1TB Portable SSD (2100 MB/s Read & Write)",
                        Slug = "crucial-x9-pro-1tb",
                        CategoryName = "Storage Devices",
                        Brand = "Crucial",
                        ProductType = "Portable NVMe SSD",
                        Specs = "Compact 65x50mm • Anodized Aluminum with Soft-Touch Base • Plug-and-Play",
                        Price = 9499,
                        OriginalPrice = 14999,
                        DiscountPercent = 37,
                        Rating = 4.9,
                        ReviewCount = 190,
                        Badge = "PRO SPEED",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "sto_p5",
                        Name = "Kingston DataTraveler Max 256GB USB 3.2 Gen 2 Type-C Flash Drive",
                        Slug = "kingston-datatraveler-max-256gb",
                        CategoryName = "Storage Devices",
                        Brand = "Kingston",
                        ProductType = "Type-C Flash Drive",
                        Specs = "1000 MB/s Read Speed • Ridged Casing with Keyring Loop • LED Indicator",
                        Price = 3299,
                        OriginalPrice = 4999,
                        DiscountPercent = 34,
                        Rating = 4.8,
                        ReviewCount = 220,
                        Badge = "POCKET SPEED",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "sto_p6",
                        Name = "Seagate One Touch 2TB Slim External HDD with Password Protection",
                        Slug = "seagate-one-touch-2tb",
                        CategoryName = "Storage Devices",
                        Brand = "Seagate",
                        ProductType = "Rugged External HDD",
                        Specs = "Brushed Metal Enclosure • Rescue Data Recovery Services Included",
                        Price = 5299,
                        OriginalPrice = 7499,
                        DiscountPercent = 29,
                        Rating = 4.7,
                        ReviewCount = 275,
                        Badge = "DATA RESCUE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    }
                }
            },

            // 6. Chargers & Cables
            new()
            {
                Id = "sub_chargers_cables",
                CategorySlug = "chargers-cables",
                CategoryName = "Chargers & Cables",
                Emoji = "🔌",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Fast Chargers & Heavy-Duty Cables",
                    TitleHighlight = "GaN Technology • 65W to 140W Power",
                    Subtitle = "Surge-protected multi-port GaN fast wall chargers, durable braided 100W USB-C cables, and multi-port travel hubs tested for worldwide electrical standards.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155",
                    Badges = new List<ElectronicsHeroBadgeDto>
                    {
                        new() { Id = "cb1", Label = "GaN III High Efficiency", Icon = "zap" },
                        new() { Id = "cb2", Label = "Surge & Over-Heat Safe", Icon = "shield" },
                        new() { Id = "cb3", Label = "Braided 20,000+ Bend Tested", Icon = "award" }
                    }
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Certified fast power and charging accessories built for high wattage and global travel.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_chg_1", Name = "Anker", Slug = "anker", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_chg_2", Name = "Belkin", Slug = "belkin", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_chg_3", Name = "Ugreen", Slug = "ugreen", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_chg_4", Name = "Baseus", Slug = "baseus", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_chg_5", Name = "Portronics", Slug = "portronics", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_chg_6", Name = "Stuffcool", Slug = "stuffcool", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_chg_7", Name = "Ambrane", Slug = "ambrane", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_chg_8", Name = "Apple", Slug = "apple", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                },
                EquipmentTypes = new List<string> { "GaN Fast Charger", "100W USB-C Cable", "Multi-Port USB Hub", "Lightning Cable", "Braided Nylon", "Surge Protected" },
                CuratedProducts = new List<ElectronicsProductItemDto>
                {
                    new()
                    {
                        Id = "chg_p1",
                        Name = "Anker Prime 67W 3-Port GaN Wall Charger (Simultaneous Fast Charge)",
                        Slug = "anker-prime-67w-gan",
                        CategoryName = "Chargers & Cables",
                        Brand = "Anker",
                        ProductType = "GaN Fast Charger",
                        Specs = "2x USB-C + 1x USB-A • ActiveShield 2.0 Protection • Universal 100V-240V",
                        Price = 3999,
                        OriginalPrice = 5999,
                        DiscountPercent = 33,
                        Rating = 4.9,
                        ReviewCount = 480,
                        Badge = "BESTSELLER",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "chg_p2",
                        Name = "Belkin BoostCharge Pro 100W Dual USB-C GaN Wall Charger",
                        Slug = "belkin-boostcharge-pro-100w",
                        CategoryName = "Chargers & Cables",
                        Brand = "Belkin",
                        ProductType = "GaN Fast Charger",
                        Specs = "Power Delivery 3.0 • Intelligent Power Sharing • Dual Laptop Charging",
                        Price = 5499,
                        OriginalPrice = 7999,
                        DiscountPercent = 31,
                        Rating = 4.8,
                        ReviewCount = 290,
                        Badge = "PRO DUAL",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "chg_p3",
                        Name = "Ugreen Nexode 100W 4-Port GaN Desktop & Travel Fast Charger",
                        Slug = "ugreen-nexode-100w",
                        CategoryName = "Chargers & Cables",
                        Brand = "Ugreen",
                        ProductType = "GaN Fast Charger",
                        Specs = "3x USB-C + 1x USB-A • Thermal Guard Protection • 100W Single Port Max",
                        Price = 4999,
                        OriginalPrice = 6999,
                        DiscountPercent = 29,
                        Rating = 4.9,
                        ReviewCount = 340,
                        Badge = "4-PORT HUB",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "chg_p4",
                        Name = "Baseus 100W Fast Charging Braided Type-C to Type-C Cable 2-Meter",
                        Slug = "baseus-100w-braided-cable-2m",
                        CategoryName = "Chargers & Cables",
                        Brand = "Baseus",
                        ProductType = "100W USB-C Cable",
                        Specs = "E-Marker Smart Chip • 480 Mbps Data • High Density Braided Nylon",
                        Price = 799,
                        OriginalPrice = 1499,
                        DiscountPercent = 47,
                        Rating = 4.7,
                        ReviewCount = 610,
                        Badge = "HEAVY DUTY",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "chg_p5",
                        Name = "Stuffcool Neutron 33W Tiny GaN Dual Port Charger (Ultra Compact)",
                        Slug = "stuffcool-neutron-33w-gan",
                        CategoryName = "Chargers & Cables",
                        Brand = "Stuffcool",
                        ProductType = "GaN Fast Charger",
                        Specs = "Type-C PD + USB-A • 50% Smaller than standard adapter • Made in India",
                        Price = 1499,
                        OriginalPrice = 2499,
                        DiscountPercent = 40,
                        Rating = 4.7,
                        ReviewCount = 280,
                        Badge = "ULTRA TINY",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "chg_p6",
                        Name = "Portronics 7-in-1 USB-C Multiport Hub with 4K HDMI & 100W PD Pass-Through",
                        Slug = "portronics-7-in-1-usbc-hub",
                        CategoryName = "Chargers & Cables",
                        Brand = "Portronics",
                        ProductType = "Multi-Port USB Hub",
                        Specs = "4K 30Hz HDMI • 100W Power Delivery • SD/TF Card Slots • 3x USB 3.0",
                        Price = 1899,
                        OriginalPrice = 3499,
                        DiscountPercent = 46,
                        Rating = 4.6,
                        ReviewCount = 420,
                        Badge = "ALL-IN-ONE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    }
                }
            },

            // 7. Power Banks
            new()
            {
                Id = "sub_power_banks",
                CategorySlug = "power-banks",
                CategoryName = "Power Banks",
                Emoji = "🔋",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Flight-Approved Power Banks",
                    TitleHighlight = "TSA Flight Safe • High Capacity PD",
                    Subtitle = "Carry-on compliant 10,000mAh to 24,000mAh power banks with 65W to 140W USB-C Power Delivery to keep laptops, tablets, and phones charged across long-haul journeys.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155",
                    Badges = new List<ElectronicsHeroBadgeDto>
                    {
                        new() { Id = "pb1", Label = "Airline Carry-On Compliant (<100Wh)", Icon = "shield" },
                        new() { Id = "pb2", Label = "65W to 140W Laptop Fast Charging", Icon = "zap" },
                        new() { Id = "pb3", Label = "Smart Digital Battery Percentage", Icon = "award" }
                    }
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Trusted lithium-polymer battery brands certified for airline travel safety and laptop power.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_pb_1", Name = "Anker", Slug = "anker", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_pb_2", Name = "Xiaomi", Slug = "xiaomi", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_pb_3", Name = "Ambrane", Slug = "ambrane", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_pb_4", Name = "Baseus", Slug = "baseus", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_pb_5", Name = "Belkin", Slug = "belkin", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_pb_6", Name = "URBN", Slug = "urbn", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_pb_7", Name = "Portronics", Slug = "portronics", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_pb_8", Name = "Stuffcool", Slug = "stuffcool", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                },
                EquipmentTypes = new List<string> { "20,000mAh+ Laptop PD", "10,000mAh Slim Pocket", "MagSafe Wireless Magnetic", "65W Fast Charge", "LED Digital Display", "TSA Flight Safe" },
                CuratedProducts = new List<ElectronicsProductItemDto>
                {
                    new()
                    {
                        Id = "pb_p1",
                        Name = "Anker 737 Power Bank (PowerCore 24K, 140W Two-Way Fast Charge)",
                        Slug = "anker-737-powercore-24k",
                        CategoryName = "Power Banks",
                        Brand = "Anker",
                        ProductType = "20,000mAh+ Laptop PD",
                        Specs = "24,000mAh • 140W Output • Color Digital Display • TSA Flight Safe (86.4Wh)",
                        Price = 11999,
                        OriginalPrice = 15999,
                        DiscountPercent = 25,
                        Rating = 4.9,
                        ReviewCount = 380,
                        Badge = "BESTSELLER",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "pb_p2",
                        Name = "Xiaomi 20000mAh 50W HyperCharge Power Bank (Laptop & Phone Support)",
                        Slug = "xiaomi-20000mah-50w",
                        CategoryName = "Power Banks",
                        Brand = "Xiaomi",
                        ProductType = "20,000mAh+ Laptop PD",
                        Specs = "Type-C 50W MAX • Triple Output Ports • Low Current Mode for Earbuds",
                        Price = 3499,
                        OriginalPrice = 4999,
                        DiscountPercent = 30,
                        Rating = 4.8,
                        ReviewCount = 590,
                        Badge = "CAMPUS VALUE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "pb_p3",
                        Name = "URBN 20000mAh 65W Ultra Compact Fast Charging Power Bank",
                        Slug = "urbn-20000mah-65w-compact",
                        CategoryName = "Power Banks",
                        Brand = "URBN",
                        ProductType = "65W Fast Charge",
                        Specs = "65W Two-Way Fast Charging • Charges MacBook & Type-C Laptops • Compact Body",
                        Price = 2899,
                        OriginalPrice = 4499,
                        DiscountPercent = 36,
                        Rating = 4.7,
                        ReviewCount = 310,
                        Badge = "LAPTOP PD",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "pb_p4",
                        Name = "Baseus Blade 100W Ultra-Thin 20000mAh Laptop Power Bank",
                        Slug = "baseus-blade-100w-thin",
                        CategoryName = "Power Banks",
                        Brand = "Baseus",
                        ProductType = "20,000mAh+ Laptop PD",
                        Specs = "18mm Ultra-Slim Profile • Dual Type-C + Dual USB-A • Full Laptop Battery Boost",
                        Price = 5999,
                        OriginalPrice = 8999,
                        DiscountPercent = 33,
                        Rating = 4.8,
                        ReviewCount = 195,
                        Badge = "SLIM PROFILE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "pb_p5",
                        Name = "Ambrane 10000mAh MagSafe Magnetic Wireless Fast Charging Power Bank",
                        Slug = "ambrane-10000mah-magsafe",
                        CategoryName = "Power Banks",
                        Brand = "Ambrane",
                        ProductType = "MagSafe Wireless Magnetic",
                        Specs = "15W Wireless + 22.5W Wired PD • Foldable Kickstand • Strong Magnetic Lock",
                        Price = 1799,
                        OriginalPrice = 2999,
                        DiscountPercent = 40,
                        Rating = 4.7,
                        ReviewCount = 420,
                        Badge = "WIRELESS MAGSAFE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "pb_p6",
                        Name = "Belkin BoostCharge Magnetic Wireless Power Bank 5000mAh with Kickstand",
                        Slug = "belkin-boostcharge-magnetic-5k",
                        CategoryName = "Power Banks",
                        Brand = "Belkin",
                        ProductType = "MagSafe Wireless Magnetic",
                        Specs = "Pass-Through Power • Zinc Alloy Kickstand • Compact Travel Companion",
                        Price = 2999,
                        OriginalPrice = 4499,
                        DiscountPercent = 33,
                        Rating = 4.8,
                        ReviewCount = 180,
                        Badge = "OFFICIAL PARTNER",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    }
                }
            },

            // 8. Watches
            new()
            {
                Id = "sub_watches",
                CategorySlug = "watches",
                CategoryName = "Watches",
                Emoji = "⌚",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Smartwatches & Campus Wearables",
                    TitleHighlight = "Health Tracking • Dual Time Zone",
                    Subtitle = "Multi-time zone GPS smartwatches, fitness trackers, and university lifestyle wearables with sleep monitoring, heart rate analysis, and water resistance.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155",
                    Badges = new List<ElectronicsHeroBadgeDto>
                    {
                        new() { Id = "wb1", Label = "Multi-Time Zone Clocks", Icon = "zap" },
                        new() { Id = "wb2", Label = "14+ Day Battery Available", Icon = "award" },
                        new() { Id = "wb3", Label = "5ATM / 50M Water Resistance", Icon = "shield" }
                    }
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Leading wearable technology brands for student wellness, scheduling, and athletics.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_wat_1", Name = "Apple", Slug = "apple", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_wat_2", Name = "Samsung", Slug = "samsung", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_wat_3", Name = "Garmin", Slug = "garmin", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_wat_4", Name = "Amazfit", Slug = "amazfit", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_wat_5", Name = "Noise", Slug = "noise", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_wat_6", Name = "Fire-Boltt", Slug = "fire-boltt", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_wat_7", Name = "Fitbit", Slug = "fitbit", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_wat_8", Name = "Boat", Slug = "boat", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                },
                EquipmentTypes = new List<string> { "AMOLED Display", "Dual GPS", "10+ Days Battery", "5ATM Water Resistant", "Calling Smartwatch", "Health & ECG" },
                CuratedProducts = new List<ElectronicsProductItemDto>
                {
                    new()
                    {
                        Id = "wat_p1",
                        Name = "Apple Watch Series 9 GPS (Always-On Retina Display)",
                        Slug = "apple-watch-series-9",
                        CategoryName = "Watches",
                        Brand = "Apple",
                        ProductType = "AMOLED Display",
                        Specs = "S9 SiP • Double Tap Gesture • Blood Oxygen & ECG • Crash Detection",
                        Price = 41900,
                        OriginalPrice = 44900,
                        DiscountPercent = 7,
                        Rating = 4.9,
                        ReviewCount = 490,
                        Badge = "BESTSELLER",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "wat_p2",
                        Name = "Samsung Galaxy Watch6 (Sleep Coaching & Body Composition)",
                        Slug = "samsung-galaxy-watch6",
                        CategoryName = "Watches",
                        Brand = "Samsung",
                        ProductType = "AMOLED Display",
                        Specs = "Sapphire Crystal Glass • BioActive Sensor • Wear OS with Google Maps",
                        Price = 24999,
                        OriginalPrice = 33999,
                        DiscountPercent = 26,
                        Rating = 4.8,
                        ReviewCount = 280,
                        Badge = "HEALTH PRO",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "wat_p3",
                        Name = "Garmin Forerunner 55 GPS Running & Athletic Smartwatch",
                        Slug = "garmin-forerunner-55",
                        CategoryName = "Watches",
                        Brand = "Garmin",
                        ProductType = "Dual GPS",
                        Specs = "Up to 14 Days Battery • Built-In GPS • Daily Suggested Workouts • PacePro",
                        Price = 17990,
                        OriginalPrice = 22490,
                        DiscountPercent = 20,
                        Rating = 4.9,
                        ReviewCount = 190,
                        Badge = "RUNNERS CHOICE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "wat_p4",
                        Name = "Amazfit GTR 4 Smart Watch (Dual-Band Circularly-Polarized GPS)",
                        Slug = "amazfit-gtr-4",
                        CategoryName = "Watches",
                        Brand = "Amazfit",
                        ProductType = "10+ Days Battery",
                        Specs = "14-Day Battery Life • 1.43-inch AMOLED • Bluetooth Phone Calls • 150+ Sports",
                        Price = 14999,
                        OriginalPrice = 19999,
                        DiscountPercent = 25,
                        Rating = 4.8,
                        ReviewCount = 310,
                        Badge = "LONG BATTERY",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "wat_p5",
                        Name = "Noise ColorFit Pro 5 Max (1.96-inch AMOLED Display, BT Calling)",
                        Slug = "noise-colorfit-pro-5-max",
                        CategoryName = "Watches",
                        Brand = "Noise",
                        ProductType = "Calling Smartwatch",
                        Specs = "VO2 Max Measurement • Rapid SOS • Post-Flight Sleep Quality Score",
                        Price = 3999,
                        OriginalPrice = 7999,
                        DiscountPercent = 50,
                        Rating = 4.7,
                        ReviewCount = 580,
                        Badge = "STUDENT FAVORITE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "wat_p6",
                        Name = "Fire-Boltt Invincible Plus 1.43-inch AMOLED Smartwatch with 4GB Storage",
                        Slug = "fire-boltt-invincible-plus",
                        CategoryName = "Watches",
                        Brand = "Fire-Boltt",
                        ProductType = "Calling Smartwatch",
                        Specs = "4GB Offline Music Storage • Connect TWS directly • 300+ Sports Modes",
                        Price = 3499,
                        OriginalPrice = 8999,
                        DiscountPercent = 61,
                        Rating = 4.6,
                        ReviewCount = 420,
                        Badge = "MUSIC STORAGE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    }
                }
            },

            // 9. Computer Accessories
            new()
            {
                Id = "sub_comp_acc",
                CategorySlug = "computer-accessories",
                CategoryName = "Computer Accessories",
                Emoji = "🖱️",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Computer Accessories & Ergonomics",
                    TitleHighlight = "Ergonomic Comfort • Portable Productivity",
                    Subtitle = "Ergonomic wireless mice, multi-device Bluetooth keyboards, ventilated aluminum laptop stands, and protective sleeves engineered for library and dorm desks.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155",
                    Badges = new List<ElectronicsHeroBadgeDto>
                    {
                        new() { Id = "kab1", Label = "Multi-Device Switching", Icon = "zap" },
                        new() { Id = "kab2", Label = "Ergonomic Posture Approved", Icon = "award" },
                        new() { Id = "kab3", Label = "Lightweight Travel Aluminum", Icon = "shield" }
                    }
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Industry-standard workspace productivity brands trusted by students and engineers.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_acc_1", Name = "Logitech", Slug = "logitech", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_acc_2", Name = "Keychron", Slug = "keychron", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_acc_3", Name = "Microsoft", Slug = "microsoft", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_acc_4", Name = "Portronics", Slug = "portronics", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_acc_5", Name = "Tukzer", Slug = "tukzer", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_acc_6", Name = "Dell", Slug = "dell", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_acc_7", Name = "HP", Slug = "hp", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_acc_8", Name = "Zebronics", Slug = "zebronics", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                },
                EquipmentTypes = new List<string> { "Wireless Mouse", "Mechanical Keyboard", "Aluminum Laptop Stand", "Webcam & Mic", "Mouse Pad", "Protective Sleeve" },
                CuratedProducts = new List<ElectronicsProductItemDto>
                {
                    new()
                    {
                        Id = "acc_p1",
                        Name = "Logitech MX Master 3S Wireless Performance Mouse (8K DPI Quiet)",
                        Slug = "logitech-mx-master-3s",
                        CategoryName = "Computer Accessories",
                        Brand = "Logitech",
                        ProductType = "Wireless Mouse",
                        Specs = "MagSpeed Scrolling • 8000 DPI Glass Tracking • Ergonomic Thumb Rest",
                        Price = 8995,
                        OriginalPrice = 10995,
                        DiscountPercent = 18,
                        Rating = 4.9,
                        ReviewCount = 420,
                        Badge = "BESTSELLER",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "acc_p2",
                        Name = "Logitech K380 Multi-Device Bluetooth Wireless Keyboard",
                        Slug = "logitech-k380-multi-device",
                        CategoryName = "Computer Accessories",
                        Brand = "Logitech",
                        ProductType = "Mechanical Keyboard",
                        Specs = "Connects Up to 3 Devices • 2-Year Battery Life • Windows / Mac / iPad",
                        Price = 2895,
                        OriginalPrice = 3695,
                        DiscountPercent = 22,
                        Rating = 4.8,
                        ReviewCount = 680,
                        Badge = "PORTABLE HERO",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "acc_p3",
                        Name = "Keychron K2 Wireless Mechanical Keyboard (RGB Backlit, Gateron Red)",
                        Slug = "keychron-k2-wireless-mech",
                        CategoryName = "Computer Accessories",
                        Brand = "Keychron",
                        ProductType = "Mechanical Keyboard",
                        Specs = "Mac & Windows Keycaps • 4000mAh Battery • Hot-Swappable Switches",
                        Price = 7499,
                        OriginalPrice = 9999,
                        DiscountPercent = 25,
                        Rating = 4.9,
                        ReviewCount = 210,
                        Badge = "ENGINEER CHOICE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "acc_p4",
                        Name = "Portronics My Buddy K Foldable Aluminum Ergonomic Laptop Stand",
                        Slug = "portronics-my-buddy-k-stand",
                        CategoryName = "Computer Accessories",
                        Brand = "Portronics",
                        ProductType = "Aluminum Laptop Stand",
                        Specs = "7-Level Adjustable Height • Hollow Heat Dissipation • Fits Up to 17-inch",
                        Price = 899,
                        OriginalPrice = 1999,
                        DiscountPercent = 55,
                        Rating = 4.7,
                        ReviewCount = 890,
                        Badge = "POSTURE ESSENTIAL",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "acc_p5",
                        Name = "Logitech C920x HD Pro Webcam (Full HD 1080p, Stereo Mics)",
                        Slug = "logitech-c920x-pro-webcam",
                        CategoryName = "Computer Accessories",
                        Brand = "Logitech",
                        ProductType = "Webcam & Mic",
                        Specs = "Full HD 1080p 30fps • Auto Light Correction • Dual Mics for Lectures",
                        Price = 6495,
                        OriginalPrice = 8995,
                        DiscountPercent = 28,
                        Rating = 4.8,
                        ReviewCount = 340,
                        Badge = "CLEAR LECTURES",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "acc_p6",
                        Name = "Tukzer 360-Degree Protective Laptop Sleeve with Accessory Pocket",
                        Slug = "tukzer-360-laptop-sleeve",
                        CategoryName = "Computer Accessories",
                        Brand = "Tukzer",
                        ProductType = "Protective Sleeve",
                        Specs = "CornerArmor Protection • Water-Repellent Fabric • Front Organizer Pocket",
                        Price = 1199,
                        OriginalPrice = 1999,
                        DiscountPercent = 40,
                        Rating = 4.7,
                        ReviewCount = 450,
                        Badge = "TRAVEL SHIELD",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    }
                }
            },

            // 10. Speakers
            new()
            {
                Id = "sub_speakers",
                CategorySlug = "speakers",
                CategoryName = "Speakers",
                Emoji = "🔊",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Portable Speakers & Room Audio",
                    TitleHighlight = "360° Immersive Sound • IPX7 Waterproof",
                    Subtitle = "Compact Bluetooth speakers with rich bass, all-weather waterproof durability, and long battery life for student dorm rooms, study groups, and outdoor leisure.",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    BannerHeight = 400,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155",
                    Badges = new List<ElectronicsHeroBadgeDto>
                    {
                        new() { Id = "spk1", Label = "IPX7 Full Waterproof & Float", Icon = "shield" },
                        new() { Id = "spk2", Label = "True 360° Room-Filling Bass", Icon = "zap" },
                        new() { Id = "spk3", Label = "20h+ All-Day Playtime", Icon = "award" }
                    }
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Celebrated acoustics brands engineering portable Bluetooth speakers and dorm sound systems.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_spk_1", Name = "JBL", Slug = "jbl", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_spk_2", Name = "Sony", Slug = "sony", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_spk_3", Name = "Bose", Slug = "bose", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_spk_4", Name = "Marshall", Slug = "marshall", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_spk_5", Name = "Boat", Slug = "boat", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_spk_6", Name = "Ultimate Ears", Slug = "ultimate-ears", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_spk_7", Name = "Tribit", Slug = "tribit", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_spk_8", Name = "Harman Kardon", Slug = "harman-kardon", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                },
                EquipmentTypes = new List<string> { "Portable Bluetooth", "IPX7 Waterproof", "360° Sound", "Dorm Room Soundbar", "20h+ Playtime", "PartyBoost Pairable" },
                CuratedProducts = new List<ElectronicsProductItemDto>
                {
                    new()
                    {
                        Id = "spk_p1",
                        Name = "JBL Flip 6 Portable Waterproof Bluetooth Speaker (Bold Sound)",
                        Slug = "jbl-flip-6-bluetooth-speaker",
                        CategoryName = "Speakers",
                        Brand = "JBL",
                        ProductType = "Portable Bluetooth",
                        Specs = "2-Way Speaker System • IP67 Waterproof & Dustproof • 12h Battery • PartyBoost",
                        Price = 9999,
                        OriginalPrice = 13999,
                        DiscountPercent = 29,
                        Rating = 4.9,
                        ReviewCount = 580,
                        Badge = "BESTSELLER",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "spk_p2",
                        Name = "Bose SoundLink Flex Bluetooth Speaker (Rugged Waterproof Outdoor)",
                        Slug = "bose-soundlink-flex",
                        CategoryName = "Speakers",
                        Brand = "Bose",
                        ProductType = "Portable Bluetooth",
                        Specs = "PositionIQ Technology • IP67 Waterproof & Floats • 12h Battery Life",
                        Price = 14900,
                        OriginalPrice = 17900,
                        DiscountPercent = 17,
                        Rating = 4.9,
                        ReviewCount = 290,
                        Badge = "PREMIUM AUDIO",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "spk_p3",
                        Name = "Sony SRS-XB100 Wireless Ultra-Portable Speaker with Deep Bass",
                        Slug = "sony-srs-xb100-portable",
                        CategoryName = "Speakers",
                        Brand = "Sony",
                        ProductType = "Portable Bluetooth",
                        Specs = "Sound Diffusion Processor • IP67 Waterproof • 16h Battery • Multiway Strap",
                        Price = 3990,
                        OriginalPrice = 4990,
                        DiscountPercent = 20,
                        Rating = 4.8,
                        ReviewCount = 370,
                        Badge = "COMPACT PUNCH",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "spk_p4",
                        Name = "Marshall Emberton II Portable Bluetooth Speaker (True Stereophonic)",
                        Slug = "marshall-emberton-ii",
                        CategoryName = "Speakers",
                        Brand = "Marshall",
                        ProductType = "360° Sound",
                        Specs = "30+ Hours of Portable Playtime • Iconic Vintage Design • IP67 Dust & Water",
                        Price = 17499,
                        OriginalPrice = 19999,
                        DiscountPercent = 13,
                        Rating = 4.9,
                        ReviewCount = 220,
                        Badge = "ICONIC SOUND",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "spk_p5",
                        Name = "Ultimate Ears WONDERBOOM 3 Ultraportable Bluetooth Speaker",
                        Slug = "ultimate-ears-wonderboom-3",
                        CategoryName = "Speakers",
                        Brand = "Ultimate Ears",
                        ProductType = "IPX7 Waterproof",
                        Specs = "360° Bigger Sound • Outdoor Boost Button • IP67 Floats • 14h Battery",
                        Price = 6995,
                        OriginalPrice = 9995,
                        DiscountPercent = 30,
                        Rating = 4.8,
                        ReviewCount = 310,
                        Badge = "PARTY READY",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    },
                    new()
                    {
                        Id = "spk_p6",
                        Name = "Boat Stone 352 Bluetooth Speaker with 10W RMS Stereo Sound",
                        Slug = "boat-stone-352-speaker",
                        CategoryName = "Speakers",
                        Brand = "Boat",
                        ProductType = "Portable Bluetooth",
                        Specs = "10W RMS Audio • IPX7 Splash Resistance • 12h Playback • Type-C Charging",
                        Price = 1499,
                        OriginalPrice = 3490,
                        DiscountPercent = 57,
                        Rating = 4.6,
                        ReviewCount = 740,
                        Badge = "CAMPUS VALUE",
                        InStock = true,
                        IsActive = true,
                        IsUploadingSoon = true,
                        ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg"
                    }
                }
            }
        };
    }
}

public sealed class ElectronicsSubcategoryPageDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string Emoji { get; set; } = "⚡";
    public ElectronicsHeroBannerDto HeroBanner { get; set; } = new();
    public ElectronicsShopByBrandsDto ShopByBrands { get; set; } = new();
    public List<string> EquipmentTypes { get; set; } = new();
    public List<ElectronicsProductItemDto> CuratedProducts { get; set; } = new();
}

public sealed class ElectronicsShopByBrandsDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Shop by Brands";
    public string Subtitle { get; set; } = "Top authorized campus brands with international student warranty & support.";
    public List<ElectronicsBrandItemDto> Brands { get; set; } = new();
}

public sealed class ElectronicsBrandItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public string TargetUrl { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
    public string Tagline { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ElectronicsBrandHeroBannerDto? HeroBanner { get; set; }
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
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
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
    public string Brand { get; set; } = string.Empty;
    public string ProductType { get; set; } = string.Empty;
    public string Specs { get; set; } = string.Empty;
    public string ProductUrl { get; set; } = "/category/electronics";
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public decimal Price { get; set; } = 0;
    public decimal OriginalPrice { get; set; } = 0;
    public int DiscountPercent { get; set; } = 0;
    public double Rating { get; set; } = 4.8;
    public int ReviewCount { get; set; } = 120;
    public bool IsActive { get; set; } = true;
    public bool InStock { get; set; } = true;
    public bool IsUploadingSoon { get; set; } = true;
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
