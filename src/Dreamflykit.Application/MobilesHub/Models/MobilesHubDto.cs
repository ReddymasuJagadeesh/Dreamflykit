namespace Dreamflykit.Application.MobilesHub.Models;

public sealed class MobilesHubDto
{
    public MobilesHeroBannerDto HeroBanner { get; set; } = new();
    public MobilesBrandsSectionDto BrandsSection { get; set; } = new();
    public MobilesHighlightBannerDto HighlightBanner { get; set; } = new();
    public MobilesTopDealsSectionDto TopDealsSection { get; set; } = new();
    public MobilesConfusedSectionDto ConfusedSection { get; set; } = new();
    public MobilesPromoBannerDto PromoBanner { get; set; } = new();
    public MobilesUnmissableDealsSectionDto UnmissableDealsSection { get; set; } = new();
    public string DefaultViewMode { get; set; } = "3D"; // "3D", "2D", "1D"

    public static MobilesHubDto CreateDefault()
    {
        return new MobilesHubDto
        {
            DefaultViewMode = "3D",
            HeroBanner = new MobilesHeroBannerDto
            {
                IsEnabled = true,
                EnableSlideshow = true,
                AutoPlay = true,
                SlideIntervalSeconds = 4,
                TransitionEffect = "slide",
                BannerHeight = 440,
                ThemeColor = "#2874f0",
                ThemeAccentColor = "#fb641b",
                Slides = new List<MobilesHeroSlideDto>
                {
                    new()
                    {
                        Id = "mob-slide-1",
                        DiscountBadge = "BIGGEST MOBILE FEST â€¢ UP TO 45% OFF",
                        Title = "Next-Gen Smartphones,",
                        TitleHighlight = "Unbeatable Deals.",
                        Subtitle = "Flagship Snapdragon & Bionic powerhouses, 200MP camera phones, 7200mAh battery monsters with 0% EMI.",
                        ImageUrl = "/images/mobiles-hub/hero-slide-1.svg",
                        TargetUrl = "/products?category=mobiles",
                        ButtonText = "Shop All Mobiles â†’",
                        DisplayOrder = 1,
                        IsActive = true,
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Id = "mob-slide-2",
                        DiscountBadge = "NEW LAUNCHES & 5G FLAGSHIPS",
                        Title = "Experience 3D Showcase,",
                        TitleHighlight = "Rotate & Inspect Live.",
                        Subtitle = "Explore smartphones in 360Â° 3D before buying. Inspect titanium frames, camera lenses & futuristic designs.",
                        ImageUrl = "/images/mobiles-hub/hero-slide-2.svg",
                        TargetUrl = "/products?category=mobiles",
                        ButtonText = "Explore 3D Deals â†’",
                        DisplayOrder = 2,
                        IsActive = true,
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Id = "mob-slide-3",
                        DiscountBadge = "STUDENT EXCLUSIVE â€¢ EXTRA â‚¹2,500 OFF",
                        Title = "Best Smartphones for Study,",
                        TitleHighlight = "Travel & Campus Life.",
                        Subtitle = "All-day battery, global 5G dual eSIM, crystal-clear display for notes, assignments and international travel.",
                        ImageUrl = "/images/mobiles-hub/hero-slide-3.svg",
                        TargetUrl = "/products?category=mobiles",
                        ButtonText = "Claim Student Offer â†’",
                        DisplayOrder = 3,
                        IsActive = true,
                        IsUploadingSoon = false
                    }
                }
            },
            BrandsSection = new MobilesBrandsSectionDto
            {
                IsEnabled = true,
                Title = "Explore Top Smartphone Brands",
                ViewAllUrl = "/products?category=mobiles",
                Brands = new List<MobileBrandItemDto>
                {
                    // Row 1
                    new() { Id = "brand-1", Name = "iPhone", Slug = "apple", IconUrl = "/images/mobiles-hub/brand-stages/apple-stage.svg", ProductUrl = "/products?category=mobiles&q=iPhone", DisplayOrder = 1 },
                    new() { Id = "brand-2", Name = "motorola", Slug = "motorola", IconUrl = "/images/mobiles-hub/brand-stages/motorola-stage.svg", ProductUrl = "/products?category=mobiles&q=motorola", DisplayOrder = 2 },
                    new() { Id = "brand-3", Name = "realme", Slug = "realme", IconUrl = "/images/mobiles-hub/brand-stages/realme-stage.svg", ProductUrl = "/products?category=mobiles&q=realme", DisplayOrder = 3 },
                    new() { Id = "brand-4", Name = "POCO", Slug = "poco", IconUrl = "/images/mobiles-hub/brand-stages/poco-stage.svg", ProductUrl = "/products?category=mobiles&q=POCO", DisplayOrder = 4 },
                    new() { Id = "brand-5", Name = "Google", Slug = "google-pixel", IconUrl = "/images/mobiles-hub/brand-stages/google-stage.svg", ProductUrl = "/products?category=mobiles&q=Google", DisplayOrder = 5 },
                    new() { Id = "brand-6", Name = "HMD", Slug = "hmd", IconUrl = "/images/mobiles-hub/brand-stages/hmd-stage.svg", ProductUrl = "/products?category=mobiles&q=HMD", DisplayOrder = 6 },
                    new() { Id = "brand-7", Name = "boltt", Slug = "boltt", IconUrl = "/images/mobiles-hub/brand-stages/boltt-stage.svg", ProductUrl = "/products?category=mobiles&q=boltt", DisplayOrder = 7 },
                    new() { Id = "brand-8", Name = "MIVI", Slug = "mivi", IconUrl = "/images/mobiles-hub/brand-stages/mivi-stage.svg", ProductUrl = "/products?category=mobiles&q=MIVI", DisplayOrder = 8 },
                    new() { Id = "brand-9", Name = "Tecno", Slug = "tecno", IconUrl = "/images/mobiles-hub/brand-stages/tecno-stage.svg", ProductUrl = "/products?category=mobiles&q=Tecno", DisplayOrder = 9 },

                    // Row 2
                    new() { Id = "brand-10", Name = "Samsung", Slug = "samsung", IconUrl = "/images/mobiles-hub/brand-stages/samsung-stage.svg", ProductUrl = "/products?category=mobiles&q=Samsung", DisplayOrder = 10 },
                    new() { Id = "brand-11", Name = "vivo", Slug = "vivo", IconUrl = "/images/mobiles-hub/brand-stages/vivo-stage.svg", ProductUrl = "/products?category=mobiles&q=vivo", DisplayOrder = 11 },
                    new() { Id = "brand-12", Name = "OPPO", Slug = "oppo", IconUrl = "/images/mobiles-hub/brand-stages/oppo-stage.svg", ProductUrl = "/products?category=mobiles&q=OPPO", DisplayOrder = 12 },
                    new() { Id = "brand-13", Name = "Ai+", Slug = "ai-plus", IconUrl = "/images/mobiles-hub/brand-stages/ai-stage.svg", ProductUrl = "/products?category=mobiles&q=Ai%2B", DisplayOrder = 13 },
                    new() { Id = "brand-14", Name = "Nothing", Slug = "nothing", IconUrl = "/images/mobiles-hub/brand-stages/nothing-stage.svg", ProductUrl = "/products?category=mobiles&q=Nothing", DisplayOrder = 14 },
                    new() { Id = "brand-15", Name = "Infinix", Slug = "infinix", IconUrl = "/images/mobiles-hub/brand-stages/infinix-stage.svg", ProductUrl = "/products?category=mobiles&q=Infinix", DisplayOrder = 15 },
                    new() { Id = "brand-16", Name = "LAVA", Slug = "lava", IconUrl = "/images/mobiles-hub/brand-stages/lava-stage.svg", ProductUrl = "/products?category=mobiles&q=LAVA", DisplayOrder = 16 },
                    new() { Id = "brand-17", Name = "REDMI", Slug = "redmi", IconUrl = "/images/mobiles-hub/brand-stages/redmi-stage.svg", ProductUrl = "/products?category=mobiles&q=REDMI", DisplayOrder = 17 },
                    new() { Id = "brand-18", Name = "OnePlus", Slug = "oneplus", IconUrl = "/images/mobiles-hub/brand-stages/oneplus-stage.svg", ProductUrl = "/products?category=mobiles&q=OnePlus", DisplayOrder = 18 }
                }
            },
            HighlightBanner = new MobilesHighlightBannerDto
            {
                IsEnabled = true,
                Title = "Galaxy S25 Ultra 5G",
                TitleHighlight = "From â‚¹58,999*",
                Subtitle = "Upgrade to Flagship â€¢ Snapdragon 8 Elite â€¢ Galaxy AI Built-in",
                ImageUrl = "/images/mobiles-hub/galaxy-s25-banner.svg",
                TargetUrl = "/products?category=mobiles&q=Galaxy+S25",
                ButtonText = "Buy Now with 0% EMI â†’"
            },
            TopDealsSection = new MobilesTopDealsSectionDto
            {
                IsEnabled = true,
                Title = "TOP DEALS",
                Subtitle = "Curated Smartphone Steals of the Week",
                Tagline = "Lightning Fast 5G â€¢ Mega Batteries â€¢ 100% Student Verified",
                ViewAllUrl = "/products?category=mobiles",
                Deals = new List<MobileDealItemDto>
                {
                    new()
                    {
                        Id = "deal-1",
                        Name = "Ai+ Nova 2 5G",
                        PriceText = "From â‚¹11,999*",
                        OriginalPriceText = "â‚¹16,999",
                        OfferTag = "Incl. All Offers",
                        ImageUrl = "/images/mobiles-hub/phones/nova2.svg",
                        ProductUrl = "/products?category=mobiles&q=Nova+2",
                        DisplayOrder = 1,
                        ViewMode = "3D",
                        ColorHex = "#6366f1",
                        Specifications = "6.7\" 120Hz AMOLED | 50MP Sony OIS | 5000mAh 67W"
                    },
                    new()
                    {
                        Id = "deal-2",
                        Name = "Phone (4b) Glyph 5G",
                        PriceText = "Just â‚¹25,999*",
                        OriginalPriceText = "â‚¹32,999",
                        OfferTag = "Incl. All Offers",
                        ImageUrl = "/images/mobiles-hub/phones/phone4b.svg",
                        ProductUrl = "/products?category=mobiles&q=Phone+4b",
                        DisplayOrder = 2,
                        ViewMode = "3D",
                        ColorHex = "#0284c7",
                        Specifications = "Transparent Glyph Matrix | Dimensity 8300 | 50MP Dual"
                    },
                    new()
                    {
                        Id = "deal-3",
                        Name = "moto g37 Power 7200mAh",
                        PriceText = "From â‚¹14,999",
                        OriginalPriceText = "â‚¹19,999",
                        OfferTag = "Incl. All Offers",
                        ImageUrl = "/images/mobiles-hub/phones/motog37.svg",
                        ProductUrl = "/products?category=mobiles&q=moto+g37",
                        DisplayOrder = 3,
                        ViewMode = "3D",
                        ColorHex = "#1e3a8a",
                        Specifications = "7200mAh 4-Day Battery | Gorilla Glass | Clean Android 15"
                    },
                    new()
                    {
                        Id = "deal-4",
                        Name = "OPPO K14 Plus 5G",
                        PriceText = "From â‚¹25,999*",
                        OriginalPriceText = "â‚¹31,999",
                        OfferTag = "Incl. All Offers",
                        ImageUrl = "/images/mobiles-hub/phones/oppok14.svg",
                        ProductUrl = "/products?category=mobiles&q=OPPO+K14",
                        DisplayOrder = 4,
                        ViewMode = "3D",
                        ColorHex = "#ea580c",
                        Specifications = "Curved Vision Display | 64MP Portrait Camera | 80W SuperVOOC"
                    },
                    new()
                    {
                        Id = "deal-5",
                        Name = "boltt Ace Pro 5G",
                        PriceText = "From â‚¹11,999",
                        OriginalPriceText = "â‚¹17,499",
                        OfferTag = "Incl. All Offers",
                        ImageUrl = "/images/mobiles-hub/phones/bolttace.svg",
                        ProductUrl = "/products?category=mobiles&q=boltt+Ace",
                        DisplayOrder = 5,
                        ViewMode = "3D",
                        ColorHex = "#be185d",
                        Specifications = "Titanium Finish Frame | 108MP Studio Sensor | Stereo Dolby"
                    },
                    new()
                    {
                        Id = "deal-6",
                        Name = "motorola edge 70 Fusion",
                        PriceText = "From â‚¹29,749*",
                        OriginalPriceText = "â‚¹36,999",
                        OfferTag = "Incl. All Offers",
                        ImageUrl = "/images/mobiles-hub/phones/edge70.svg",
                        ProductUrl = "/products?category=mobiles&q=edge+70",
                        DisplayOrder = 6,
                        ViewMode = "3D",
                        ColorHex = "#0d9488",
                        Specifications = "IP68 Underwater Proof | 144Hz pOLED | Sony LYTIA 700C"
                    },
                    new()
                    {
                        Id = "deal-7",
                        Name = "Vibe 2 5G (4|64GB)",
                        PriceText = "From â‚¹10,499*",
                        OriginalPriceText = "â‚¹14,999",
                        OfferTag = "Incl. All Offers",
                        ImageUrl = "/images/mobiles-hub/phones/vibe2.svg",
                        ProductUrl = "/products?category=mobiles&q=Vibe+2",
                        DisplayOrder = 7,
                        ViewMode = "3D",
                        ColorHex = "#db2777",
                        Specifications = "Pocket Budget King | 5G Ready | Expandable 1TB | 5000mAh"
                    },
                    new()
                    {
                        Id = "deal-8",
                        Name = "POCO X8 Power 5G",
                        PriceText = "From â‚¹28,999*",
                        OriginalPriceText = "â‚¹34,999",
                        OfferTag = "Incl. All Offers",
                        ImageUrl = "/images/mobiles-hub/phones/pocox8.svg",
                        ProductUrl = "/products?category=mobiles&q=POCO+X8",
                        DisplayOrder = 8,
                        ViewMode = "3D",
                        ColorHex = "#eab308",
                        Specifications = "Flagship Gaming Snapdragon 8s | Liquid Cooling | 120W HyperCharge"
                    },
                    new()
                    {
                        Id = "deal-9",
                        Name = "Nothing Phone (4a) 5G",
                        PriceText = "Just â‚¹31,999*",
                        OriginalPriceText = "â‚¹37,999",
                        OfferTag = "Incl. All Offers",
                        ImageUrl = "/images/mobiles-hub/phones/phone4a.svg",
                        ProductUrl = "/products?category=mobiles&q=Phone+4a",
                        DisplayOrder = 9,
                        ViewMode = "3D",
                        ColorHex = "#f8fafc",
                        Specifications = "Minimalist Icon | Transparent White Glass | Nothing OS 3.0"
                    }
                }
            },
            ConfusedSection = new MobilesConfusedSectionDto
            {
                IsEnabled = true,
                Title = "CONFUSED?",
                Subtitle = "Let us pick your perfect phone in 30 seconds",
                ButtonText = "EXPLORE NOW âž”",
                TargetUrl = "/products?category=mobiles"
            },
            PromoBanner = new MobilesPromoBannerDto
            {
                IsEnabled = true,
                Title = "Buy any Smartphone and Get a Chance to Win",
                Highlight = "GOOGLE PIXEL WATCH 5",
                ButtonText = "Shop Now âž”",
                ImageUrl = "/images/mobiles-hub/pixel-watch-promo.svg",
                TargetUrl = "/products?category=mobiles"
            },
            UnmissableDealsSection = new MobilesUnmissableDealsSectionDto
            {
                IsEnabled = true,
                Title = "UNMISSABLE DEALS",
                ViewAllUrl = "/products?category=mobiles",
                Deals = new List<MobileEmiDealItemDto>
                {
                    new()
                    {
                        Id = "emi-1",
                        Name = "Google Pixel 11 5G",
                        PriceText = "â‚¹71,999*",
                        OriginalPriceText = "â‚¹83,000",
                        EmiText = "Pay Only â‚¹8,000/m",
                        ImageUrl = "/images/mobiles-hub/phones/pixel11.svg",
                        ProductUrl = "/products?category=mobiles&q=Pixel+11",
                        DisplayOrder = 1,
                        ViewMode = "3D",
                        ColorHex = "#94a3b8"
                    },
                    new()
                    {
                        Id = "emi-2",
                        Name = "Galaxy S25 FE",
                        PriceText = "â‚¹44,999",
                        OriginalPriceText = "â‚¹58,000",
                        EmiText = "Pay Only â‚¹7,044/m",
                        ImageUrl = "/images/mobiles-hub/phones/s25fe.svg",
                        ProductUrl = "/products?category=mobiles&q=S25+FE",
                        DisplayOrder = 2,
                        ViewMode = "3D",
                        ColorHex = "#334155"
                    },
                    new()
                    {
                        Id = "emi-3",
                        Name = "moto g37 Power 5G",
                        PriceText = "â‚¹14,999",
                        OriginalPriceText = "â‚¹24,000",
                        EmiText = "Pay Only â‚¹2,500/m",
                        ImageUrl = "/images/mobiles-hub/phones/motog37-blue.svg",
                        ProductUrl = "/products?category=mobiles&q=moto+g37",
                        DisplayOrder = 3,
                        ViewMode = "3D",
                        ColorHex = "#1d4ed8"
                    },
                    new()
                    {
                        Id = "emi-4",
                        Name = "Mivi One 5G Pro",
                        PriceText = "â‚¹15,999*",
                        OriginalPriceText = "â‚¹25,000",
                        EmiText = "Pay Only â‚¹2,666/m",
                        ImageUrl = "/images/mobiles-hub/phones/mivi-one.svg",
                        ProductUrl = "/products?category=mobiles&q=Mivi+One",
                        DisplayOrder = 4,
                        ViewMode = "3D",
                        ColorHex = "#16a34a"
                    },
                    new()
                    {
                        Id = "emi-5",
                        Name = "HMD Vibe 5G Minimal",
                        PriceText = "â‚¹11,999*",
                        OriginalPriceText = "â‚¹20,000",
                        EmiText = "Pay Only â‚¹1,999/m",
                        ImageUrl = "/images/mobiles-hub/phones/hmd-vibe.svg",
                        ProductUrl = "/products?category=mobiles&q=HMD+Vibe",
                        DisplayOrder = 5,
                        ViewMode = "3D",
                        ColorHex = "#e2e8f0"
                    }
                }
            }
        };
    }
}

public sealed class MobilesHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public bool EnableSlideshow { get; set; } = true;
    public bool AutoPlay { get; set; } = true;
    public int SlideIntervalSeconds { get; set; } = 4;
    public string TransitionEffect { get; set; } = "slide";
    public int BannerHeight { get; set; } = 440;
    public string ThemeColor { get; set; } = "#2874f0";
    public string ThemeAccentColor { get; set; } = "#fb641b";
    public List<MobilesHeroSlideDto> Slides { get; set; } = new();
}

public sealed class MobilesHeroSlideDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string DiscountBadge { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = string.Empty;
    public string ButtonText { get; set; } = "Explore Now";
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class MobilesBrandsSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Shop by Brand";
    public string ViewAllUrl { get; set; } = "/products?category=mobiles";
    public List<MobileBrandItemDto> Brands { get; set; } = new();
}

public sealed class MobileBrandItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public string ProductUrl { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 1;
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class MobilesHighlightBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = string.Empty;
    public string ButtonText { get; set; } = "Shop Now";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class MobilesTopDealsSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "TOP DEALS";
    public string Subtitle { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string ViewAllUrl { get; set; } = "/products?category=mobiles";
    public List<MobileDealItemDto> Deals { get; set; } = new();
}

public sealed class MobileDealItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string PriceText { get; set; } = string.Empty;
    public string OriginalPriceText { get; set; } = string.Empty;
    public string OfferTag { get; set; } = "Incl. All Offers";
    public string ImageUrl { get; set; } = string.Empty;
    public string ProductUrl { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 1;
    public bool IsUploadingSoon { get; set; } = false;

    /// <summary>
    /// Visualization mode decided by Admin: "3D", "2D", or "1D"
    /// </summary>
    public string ViewMode { get; set; } = "3D";

    /// <summary>
    /// Optional .glb / .gltf 3D model URL or built-in model identifier
    /// </summary>
    public string? Model3dUrl { get; set; }

    /// <summary>
    /// Hex color for 3D body rendering (e.g. #0284c7)
    /// </summary>
    public string ColorHex { get; set; } = "#2563eb";

    /// <summary>
    /// Key specs bullet summary (RAM, Storage, Camera, Battery)
    /// </summary>
    public string Specifications { get; set; } = string.Empty;
}

public sealed class MobilesConfusedSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "CONFUSED?";
    public string Subtitle { get; set; } = "Let us pick your perfect phone in 30 seconds";
    public string ButtonText { get; set; } = "EXPLORE NOW âž”";
    public string TargetUrl { get; set; } = "/products?category=mobiles";
}

public sealed class MobilesPromoBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string Highlight { get; set; } = string.Empty;
    public string ButtonText { get; set; } = "Shop Now";
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class MobilesUnmissableDealsSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "UNMISSABLE DEALS";
    public string ViewAllUrl { get; set; } = "/products?category=mobiles";
    public List<MobileEmiDealItemDto> Deals { get; set; } = new();
}

public sealed class MobileEmiDealItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string PriceText { get; set; } = string.Empty;
    public string OriginalPriceText { get; set; } = string.Empty;
    public string EmiText { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ProductUrl { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 1;
    public bool IsUploadingSoon { get; set; } = false;
    public string ViewMode { get; set; } = "3D";
    public string? Model3dUrl { get; set; }
    public string ColorHex { get; set; } = "#3b82f6";
}
