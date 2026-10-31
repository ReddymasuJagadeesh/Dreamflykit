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
                        ButtonUrl = "/category/electronics/laptops-macbooks",
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
                        ButtonUrl = "/category/electronics/headphones-audio",
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
                        ButtonUrl = "/category/electronics/travel-adapters",
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
                        ProductUrl = "/category/electronics/travel-adapters",
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
                        ProductUrl = "/category/electronics/headphones-audio",
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
                        ProductUrl = "/category/electronics/power-banks-chargers",
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
                        ProductUrl = "/category/electronics/storage-ssds",
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
                        ProductUrl = "/category/electronics/student-sims",
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
            },
            SubcategoryPages = CreateDefaultSubcategoryPages()
        };
    }

    public static List<ElectronicsSubcategoryPageDto> CreateDefaultSubcategoryPages()
    {
        return new List<ElectronicsSubcategoryPageDto>
        {
            // 1. Laptops & MacBooks / Laptop & Mouses
            new()
            {
                Id = "sub_laptops",
                CategorySlug = "laptops-macbooks",
                CategoryName = "Laptops & MacBooks",
                Emoji = "💻",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Engineered for Global Campuses",
                    TitleHighlight = "Laptops & Computing",
                    Subtitle = "Certified MacBooks, ThinkPads, and student setups with worldwide 110V-240V power and international student warranty.",
                    ImageUrl = "/images/hero/hero-electronics.webp",
                    IsUploadingSoon = true,
                    BannerHeight = 380,
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
                        new() { Id = "b_lap_3", Name = "Lenovo", Slug = "lenovo", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_lap_4", Name = "HP", Slug = "hp", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_lap_5", Name = "Asus", Slug = "asus", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_lap_6", Name = "Acer", Slug = "acer", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_lap_7", Name = "Microsoft", Slug = "microsoft", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_lap_8", Name = "Samsung", Slug = "samsung", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                }
            },

            // 2. Noise-Cancelling Headphones
            new()
            {
                Id = "sub_headphones",
                CategorySlug = "headphones-audio",
                CategoryName = "Noise-Cancelling Headphones",
                Emoji = "🎧",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Distraction-Free Focus & Study",
                    TitleHighlight = "Noise-Cancelling Audio",
                    Subtitle = "Immerse in uninterrupted library study sessions and quiet long-haul flights with industry-leading Active Noise Cancellation.",
                    ImageUrl = "/images/hero/hero-electronics.webp",
                    IsUploadingSoon = true,
                    BannerHeight = 380,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155"
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Premium audio brands certified for high-fidelity acoustic performance and ANC comfort.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_aud_1", Name = "Sony", Slug = "sony", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_aud_2", Name = "Bose", Slug = "bose", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_aud_3", Name = "Apple", Slug = "apple", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_aud_4", Name = "Boat", Slug = "boat", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_aud_5", Name = "JBL", Slug = "jbl", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_aud_6", Name = "Sennheiser", Slug = "sennheiser", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_aud_7", Name = "Marshall", Slug = "marshall", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_aud_8", Name = "Skullcandy", Slug = "skullcandy", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                }
            },

            // 3. Universal Travel Adapters
            new()
            {
                Id = "sub_adapters",
                CategorySlug = "travel-adapters",
                CategoryName = "Universal Travel Adapters",
                Emoji = "🔌",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Safe Power Worldwide",
                    TitleHighlight = "Universal Travel Adapters",
                    Subtitle = "Surge-protected international plugs with fast USB-C PD ports compatible with USA, UK, EU, Canada & Australia sockets.",
                    ImageUrl = "/images/hero/hero-electronics.webp",
                    IsUploadingSoon = true,
                    BannerHeight = 380,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155"
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Safety-certified international power plug brands covering 150+ countries.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_adp_1", Name = "Epicka", Slug = "epicka", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_adp_2", Name = "Belkin", Slug = "belkin", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_adp_3", Name = "Anker", Slug = "anker", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_adp_4", Name = "Skross", Slug = "skross", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_adp_5", Name = "Oreva", Slug = "oreva", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_adp_6", Name = "Portronics", Slug = "portronics", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_adp_7", Name = "Syska", Slug = "syska", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_adp_8", Name = "Philips", Slug = "philips", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                }
            },

            // 4. Power Banks & Chargers
            new()
            {
                Id = "sub_power",
                CategorySlug = "power-banks-chargers",
                CategoryName = "Power Banks & Chargers",
                Emoji = "🔋",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "All-Day Campus Power",
                    TitleHighlight = "GaN Chargers & Power Banks",
                    Subtitle = "Flight-approved portable battery packs and multi-port 65W/100W GaN chargers to power laptops, tablets, and phones simultaneously.",
                    ImageUrl = "/images/hero/hero-electronics.webp",
                    IsUploadingSoon = true,
                    BannerHeight = 380,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155"
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Top high-efficiency GaN charging and portable power technology brands.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_pwr_1", Name = "Anker", Slug = "anker", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_pwr_2", Name = "Ambrane", Slug = "ambrane", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_pwr_3", Name = "Xiaomi", Slug = "xiaomi", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_pwr_4", Name = "Boat", Slug = "boat", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_pwr_5", Name = "Belkin", Slug = "belkin", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_pwr_6", Name = "Spigen", Slug = "spigen", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_pwr_7", Name = "Ugreen", Slug = "ugreen", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_pwr_8", Name = "CMF", Slug = "cmf", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                }
            },

            // 5. International Student SIMs
            new()
            {
                Id = "sub_sims",
                CategorySlug = "student-sims",
                CategoryName = "International Student SIMs",
                Emoji = "📶",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Stay Connected from Touchdown",
                    TitleHighlight = "International Student SIMs & eSIM",
                    Subtitle = "Pre-activated physical SIM cards and instant digital eSIMs with unlimited calling, high-speed 5G data, and zero roaming fees.",
                    ImageUrl = "/images/hero/hero-electronics.webp",
                    IsUploadingSoon = true,
                    BannerHeight = 380,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155"
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Authorized global telecom networks in USA, UK, Canada, Australia & Europe.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_sim_1", Name = "T-Mobile", Slug = "t-mobile", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_sim_2", Name = "Vodafone", Slug = "vodafone", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_sim_3", Name = "Lyca Mobile", Slug = "lyca", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_sim_4", Name = "AT&T", Slug = "att", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_sim_5", Name = "O2", Slug = "o2", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_sim_6", Name = "EE", Slug = "ee", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_sim_7", Name = "Mint Mobile", Slug = "mint", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_sim_8", Name = "Three", Slug = "three", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                }
            },

            // 6. Storage & Portable SSDs
            new()
            {
                Id = "sub_storage",
                CategorySlug = "storage-ssds",
                CategoryName = "Storage & Portable SSDs",
                Emoji = "💾",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Assignment & Project Backups",
                    TitleHighlight = "Ultra-Fast Portable SSDs",
                    Subtitle = "Shock-resistant, ultra-compact NVMe portable solid state drives up to 2TB for ultra-fast transfers and assignments safety.",
                    ImageUrl = "/images/hero/hero-electronics.webp",
                    IsUploadingSoon = true,
                    BannerHeight = 380,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155"
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "World-class solid-state memory and encrypted backup drive manufacturers.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_ssd_1", Name = "SanDisk", Slug = "sandisk", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_ssd_2", Name = "Samsung", Slug = "samsung", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_ssd_3", Name = "Western Digital", Slug = "wd", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_ssd_4", Name = "Seagate", Slug = "seagate", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_ssd_5", Name = "Crucial", Slug = "crucial", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_ssd_6", Name = "Kingston", Slug = "kingston", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_ssd_7", Name = "Transcend", Slug = "transcend", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true }
                    }
                }
            },

            // 7. Smartwatches & Wearables
            new()
            {
                Id = "sub_wearables",
                CategorySlug = "smartwatches-wearables",
                CategoryName = "Smartwatches & Wearables",
                Emoji = "⌚",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Timetables, Alarms & Health",
                    TitleHighlight = "Smartwatches & Wearables",
                    Subtitle = "Stay punctual for morning lectures, track your daily wellness, and manage incoming campus notifications on your wrist.",
                    ImageUrl = "/images/hero/hero-electronics.webp",
                    IsUploadingSoon = true,
                    BannerHeight = 380,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155"
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Industry-leading smartwatch brands with battery endurance and health analytics.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_wat_1", Name = "Apple", Slug = "apple", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_wat_2", Name = "Samsung Galaxy", Slug = "samsung", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_wat_3", Name = "Garmin", Slug = "garmin", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_wat_4", Name = "Fitbit", Slug = "fitbit", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_wat_5", Name = "Noise", Slug = "noise", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_wat_6", Name = "Fire-Boltt", Slug = "fire-boltt", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_wat_7", Name = "Amazfit", Slug = "amazfit", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_wat_8", Name = "Fastrack", Slug = "fastrack", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                }
            },

            // 8. Cables, Hubs & Docks
            new()
            {
                Id = "sub_cables",
                CategorySlug = "cables-hubs-docks",
                CategoryName = "Cables, Hubs & Docks",
                Emoji = "🔗",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Seamless Multi-Port Connectivity",
                    TitleHighlight = "USB-C Hubs, Docks & Cables",
                    Subtitle = "Multi-port 4K HDMI adapters, SD card slots, and military-grade braided cables engineered to withstand backpack wear.",
                    ImageUrl = "/images/hero/hero-electronics.webp",
                    IsUploadingSoon = true,
                    BannerHeight = 380,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155"
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Durable connectivity and docking station brands for MacBook and Windows.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_cbl_1", Name = "Anker", Slug = "anker", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_cbl_2", Name = "Ugreen", Slug = "ugreen", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_cbl_3", Name = "Baseus", Slug = "baseus", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_cbl_4", Name = "Satechi", Slug = "satechi", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_cbl_5", Name = "Belkin", Slug = "belkin", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_cbl_6", Name = "Portronics", Slug = "portronics", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_cbl_7", Name = "Honeywell", Slug = "honeywell", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true }
                    }
                }
            },

            // 9. Monitors & Displays
            new()
            {
                Id = "sub_monitors",
                CategorySlug = "monitors-displays",
                CategoryName = "Monitors & Displays",
                Emoji = "🖥️",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Expanded Desktop Productivity",
                    TitleHighlight = "Monitors & Portable Displays",
                    Subtitle = "Eye-care blue light filtered study displays and ultra-portable USB-C external screens for dorm rooms and library booths.",
                    ImageUrl = "/images/hero/hero-electronics.webp",
                    IsUploadingSoon = true,
                    BannerHeight = 380,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155"
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Certified ergonomic eye-care monitors from top global display brands.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_mon_1", Name = "LG", Slug = "lg", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_mon_2", Name = "Dell", Slug = "dell", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_mon_3", Name = "Samsung", Slug = "samsung", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_mon_4", Name = "BenQ", Slug = "benq", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_mon_5", Name = "Acer", Slug = "acer", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_mon_6", Name = "Asus", Slug = "asus", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true },
                        new() { Id = "b_mon_7", Name = "ViewSonic", Slug = "viewsonic", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7, IsActive = true },
                        new() { Id = "b_mon_8", Name = "MSI", Slug = "msi", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8, IsActive = true }
                    }
                }
            },

            // 10. Calculators & Tech Tools
            new()
            {
                Id = "sub_calculators",
                CategorySlug = "calculators-tech-tools",
                CategoryName = "Calculators & Tech Tools",
                Emoji = "🔢",
                HeroBanner = new ElectronicsHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Exam-Approved Academic Accuracy",
                    TitleHighlight = "Calculators & Tech Tools",
                    Subtitle = "University and examination-board compliant scientific and financial calculators, digital styluses, and screen detail kits.",
                    ImageUrl = "/images/hero/hero-electronics.webp",
                    IsUploadingSoon = true,
                    BannerHeight = 380,
                    TitleColor = "#063f91",
                    TitleHighlightColor = "#0284c7",
                    SubtitleColor = "#334155"
                },
                ShopByBrands = new ElectronicsShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Global examination-certified calculation and STEM academic tools.",
                    Brands = new List<ElectronicsBrandItemDto>
                    {
                        new() { Id = "b_cal_1", Name = "Casio", Slug = "casio", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1, IsActive = true },
                        new() { Id = "b_cal_2", Name = "Texas Instruments", Slug = "ti", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2, IsActive = true },
                        new() { Id = "b_cal_3", Name = "Sharp", Slug = "sharp", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3, IsActive = true },
                        new() { Id = "b_cal_4", Name = "Citizen", Slug = "citizen", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4, IsActive = true },
                        new() { Id = "b_cal_5", Name = "HP", Slug = "hp", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5, IsActive = true },
                        new() { Id = "b_cal_6", Name = "Orpat", Slug = "orpat", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6, IsActive = true }
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
