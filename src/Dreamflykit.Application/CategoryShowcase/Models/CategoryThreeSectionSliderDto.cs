namespace Dreamflykit.Application.CategoryShowcase.Models;

public sealed class CategorySliderSlideDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string CategorySlug { get; set; } = string.Empty;
    public string BadgeText { get; set; } = "FEATURED SPECIAL";
    public string Rating { get; set; } = "4.8";
    public string Title { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string PriceText { get; set; } = "From ₹1,999*";
    public string OriginalPriceText { get; set; } = "₹3,999";
    public string EmiBadgeText { get; set; } = "0% EMI ₹499/m";
    public string OfferDisclaimer { get; set; } = "*Incl. Bank Offer";
    public string ThemeGlowColor { get; set; } = "#f97316";
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public string TargetUrl { get; set; } = "#";
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
}

public sealed class CategoryThreeSectionConfigDto
{
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
    public bool AutoPlay { get; set; } = true;
    public int SlideIntervalSeconds { get; set; } = 4;
    public int TransitionDurationMs { get; set; } = 650;
    public List<CategorySliderSlideDto> Slides { get; set; } = new();
}

public sealed class CategoryThreeSectionHubDto
{
    public Dictionary<string, CategoryThreeSectionConfigDto> Categories { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    public CategoryThreeSectionConfigDto GetConfigForCategory(string categorySlug)
    {
        if (string.IsNullOrWhiteSpace(categorySlug)) categorySlug = "electronics";
        categorySlug = categorySlug.Trim().ToLowerInvariant();

        if (Categories.TryGetValue(categorySlug, out var config) && config != null)
        {
            return config;
        }

        // Check if pre-configured default exists in CreateDefault
        var defaults = CreateDefault();
        if (defaults.Categories.TryGetValue(categorySlug, out var defConfig) && defConfig != null)
        {
            Categories[categorySlug] = defConfig;
            return defConfig;
        }

        // Return a sensible default if not explicitly registered
        var name = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(categorySlug.Replace("-", " "));
        var fallback = new CategoryThreeSectionConfigDto
        {
            CategorySlug = categorySlug,
            CategoryName = name,
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = CreateDefaultSlidesFor(categorySlug, name)
        };
        Categories[categorySlug] = fallback;
        return fallback;
    }

    public static CategoryThreeSectionHubDto CreateDefault()
    {
        var hub = new CategoryThreeSectionHubDto();

        // 1. ELECTRONICS
        hub.Categories["electronics"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "electronics",
            CategoryName = "Electronics",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "elec_1",
                    CategorySlug = "electronics",
                    BadgeText = "AI ULTRABOOK",
                    Rating = "4.8",
                    Title = "MacBook Air M3 15\"",
                    Tagline = "Apple M3 Chip & Liquid Retina",
                    PriceText = "From ₹1,14,900*",
                    OriginalPriceText = "₹1,34,900",
                    EmiBadgeText = "0% EMI ₹9,575/m",
                    OfferDisclaimer = "*Incl. Student Offer",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/electronics/laptops-macbooks",
                    DisplayOrder = 1
                },
                new()
                {
                    Id = "elec_2",
                    CategorySlug = "electronics",
                    BadgeText = "ANC FLAGSHIP",
                    Rating = "4.7",
                    Title = "Sony WH-1000XM5",
                    Tagline = "Industry-Leading 30h Noise Cancel",
                    PriceText = "From ₹26,990*",
                    OriginalPriceText = "₹34,990",
                    EmiBadgeText = "0% EMI ₹2,249/m",
                    OfferDisclaimer = "*Incl. Bank Discount",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/electronics/noise-cancelling-headphones",
                    DisplayOrder = 2
                },
                new()
                {
                    Id = "elec_3",
                    CategorySlug = "electronics",
                    BadgeText = "CREATIVE PRO",
                    Rating = "4.9",
                    Title = "iPad Air M2 11\"",
                    Tagline = "Liquid Retina & Apple Pencil Pro",
                    PriceText = "From ₹54,900*",
                    OriginalPriceText = "₹59,900",
                    EmiBadgeText = "0% EMI ₹4,575/m",
                    OfferDisclaimer = "*Incl. Campus ID Deal",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/electronics",
                    DisplayOrder = 3
                },
                new()
                {
                    Id = "elec_4",
                    CategorySlug = "electronics",
                    BadgeText = "INFINITY OLED",
                    Rating = "4.6",
                    Title = "Dell XPS 14 OLED",
                    Tagline = "Intel Core Ultra 7 & CNC Aluminum",
                    PriceText = "From ₹1,49,990*",
                    OriginalPriceText = "₹1,74,990",
                    EmiBadgeText = "0% EMI ₹12,499/m",
                    OfferDisclaimer = "*Incl. Instant Bank Off",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/electronics/laptops-macbooks",
                    DisplayOrder = 4
                },
                new()
                {
                    Id = "elec_5",
                    CategorySlug = "electronics",
                    BadgeText = "GLOBAL TRAVEL",
                    Rating = "4.8",
                    Title = "Universal 65W GaN Travel Adapter",
                    Tagline = "All-in-One US/UK/EU/AU Fast Charge",
                    PriceText = "From ₹2,499*",
                    OriginalPriceText = "₹4,999",
                    EmiBadgeText = "0% EMI ₹416/m",
                    OfferDisclaimer = "*Worldwide Compatible",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/electronics/universal-travel-adapters",
                    DisplayOrder = 5
                },
                new()
                {
                    Id = "elec_6",
                    CategorySlug = "electronics",
                    BadgeText = "AUDIO BEAST",
                    Rating = "4.8",
                    Title = "Bose QuietComfort Ultra",
                    Tagline = "Spatial Audio & World-Class Silence",
                    PriceText = "From ₹32,900*",
                    OriginalPriceText = "₹39,900",
                    EmiBadgeText = "0% EMI ₹2,741/m",
                    OfferDisclaimer = "*Special Campus Offer",
                    ThemeGlowColor = "#ec4899",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/electronics/noise-cancelling-headphones",
                    DisplayOrder = 6
                }
            }
        };

        // 2. APPLIANCES
        hub.Categories["appliances"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "appliances",
            CategoryName = "Appliances",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "app_1",
                    CategorySlug = "appliances",
                    BadgeText = "SMART LIVING",
                    Rating = "4.8",
                    Title = "LG AI DirectDrive Washer",
                    Tagline = "Intelligent Fabric Care & Steam",
                    PriceText = "From ₹34,990*",
                    OriginalPriceText = "₹48,990",
                    EmiBadgeText = "0% EMI ₹2,916/m",
                    OfferDisclaimer = "*Free Standard Install",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/appliances/laundry",
                    DisplayOrder = 1
                },
                new()
                {
                    Id = "app_2",
                    CategorySlug = "appliances",
                    BadgeText = "CAMPUS CHILL",
                    Rating = "4.7",
                    Title = "Samsung 236L Digital Inverter",
                    Tagline = "Curd Maestro & Stabilizer Free",
                    PriceText = "From ₹22,490*",
                    OriginalPriceText = "₹28,990",
                    EmiBadgeText = "0% EMI ₹1,874/m",
                    OfferDisclaimer = "*Incl. Exchange Bonus",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/appliances/fridges",
                    DisplayOrder = 2
                },
                new()
                {
                    Id = "app_3",
                    CategorySlug = "appliances",
                    BadgeText = "4K CINEMA",
                    Rating = "4.9",
                    Title = "Sony Bravia XR 55\" OLED",
                    Tagline = "Cognitive Processor & Acoustic Surface",
                    PriceText = "From ₹89,990*",
                    OriginalPriceText = "₹1,29,990",
                    EmiBadgeText = "0% EMI ₹7,499/m",
                    OfferDisclaimer = "*Incl. Bank Offer",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/appliances/television",
                    DisplayOrder = 3
                },
                new()
                {
                    Id = "app_4",
                    CategorySlug = "appliances",
                    BadgeText = "INSTANT CHEF",
                    Rating = "4.6",
                    Title = "Philips Digital Airfryer XXL",
                    Tagline = "Rapid CombiAir Tech & 90% Less Fat",
                    PriceText = "From ₹9,999*",
                    OriginalPriceText = "₹14,999",
                    EmiBadgeText = "0% EMI ₹833/m",
                    OfferDisclaimer = "*Free Recipe Guide Included",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/appliances/microwaves",
                    DisplayOrder = 4
                },
                new()
                {
                    Id = "app_5",
                    CategorySlug = "appliances",
                    BadgeText = "INVERTER AC",
                    Rating = "4.7",
                    Title = "Daikin 1.5 Ton 5-Star Split AC",
                    Tagline = "Triple Display & PM 2.5 Filter",
                    PriceText = "From ₹37,990*",
                    OriginalPriceText = "₹52,990",
                    EmiBadgeText = "0% EMI ₹3,165/m",
                    OfferDisclaimer = "*10-Yr Compressor Warranty",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/appliances/acs",
                    DisplayOrder = 5
                },
                new()
                {
                    Id = "app_6",
                    CategorySlug = "appliances",
                    BadgeText = "AERO COMFORT",
                    Rating = "4.8",
                    Title = "Havells Stealth Air BLDC Fan",
                    Tagline = "Whisper Quiet 60% Energy Saver",
                    PriceText = "From ₹3,499*",
                    OriginalPriceText = "₹5,499",
                    EmiBadgeText = "0% EMI ₹291/m",
                    OfferDisclaimer = "*5-Star Energy Certified",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/appliances/fans",
                    DisplayOrder = 6
                }
            }
        };

        // 3. FASHION
        hub.Categories["fashion"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "fashion",
            CategoryName = "Fashion",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "fash_1",
                    CategorySlug = "fashion",
                    BadgeText = "WINTER ESSENTIAL",
                    Rating = "4.8",
                    Title = "Thermal Arctic Parka Jacket",
                    Tagline = "-20°C Weatherproof & Faux Fur Hood",
                    PriceText = "From ₹4,999*",
                    OriginalPriceText = "₹8,999",
                    EmiBadgeText = "0% EMI ₹416/m",
                    OfferDisclaimer = "*International Campus Ready",
                    ThemeGlowColor = "#f97316",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/men-clothing",
                    DisplayOrder = 1
                },
                new()
                {
                    Id = "fash_2",
                    CategorySlug = "fashion",
                    BadgeText = "CAMPUS CHIC",
                    Rating = "4.7",
                    Title = "Merino Wool Trench Overcoat",
                    Tagline = "Italian Cut Tailored Warmth",
                    PriceText = "From ₹3,799*",
                    OriginalPriceText = "₹6,999",
                    EmiBadgeText = "0% EMI ₹316/m",
                    OfferDisclaimer = "*Premium Autumn/Winter Edition",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-clothing",
                    DisplayOrder = 2
                },
                new()
                {
                    Id = "fash_3",
                    CategorySlug = "fashion",
                    BadgeText = "HERITAGE COMFORT",
                    Rating = "4.9",
                    Title = "Handcrafted Full-Grain Oxford Shoes",
                    Tagline = "Memory Cushion Sole & Goodyear Welt",
                    PriceText = "From ₹2,999*",
                    OriginalPriceText = "₹5,499",
                    EmiBadgeText = "0% EMI ₹250/m",
                    OfferDisclaimer = "*Formal & Presentation Ready",
                    ThemeGlowColor = "#eab308",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion",
                    DisplayOrder = 3
                },
                new()
                {
                    Id = "fash_4",
                    CategorySlug = "fashion",
                    BadgeText = "STREETWEAR ICON",
                    Rating = "4.6",
                    Title = "Heavyweight 450GSM Fleece Hoodie",
                    Tagline = "Pre-Shrunk Organic Cotton Relaxed Fit",
                    PriceText = "From ₹1,899*",
                    OriginalPriceText = "₹3,499",
                    EmiBadgeText = "0% EMI ₹158/m",
                    OfferDisclaimer = "*Dorm & Lecture Favorite",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/hoodies",
                    DisplayOrder = 4
                },
                new()
                {
                    Id = "fash_5",
                    CategorySlug = "fashion",
                    BadgeText = "TIMELESS ACCESSORY",
                    Rating = "4.8",
                    Title = "Sapphire Crystal Chronograph Watch",
                    Tagline = "Stainless Steel & Japanese Movement",
                    PriceText = "From ₹5,499*",
                    OriginalPriceText = "₹9,999",
                    EmiBadgeText = "0% EMI ₹458/m",
                    OfferDisclaimer = "*Water Resistant 50M",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion",
                    DisplayOrder = 5
                },
                new()
                {
                    Id = "fash_6",
                    CategorySlug = "fashion",
                    BadgeText = "DAILY LUXURY",
                    Rating = "4.7",
                    Title = "Vegan Leather Campus Tote Bag",
                    Tagline = "Padded 15.6\" Laptop Sleeve & Zipper",
                    PriceText = "From ₹1,999*",
                    OriginalPriceText = "₹3,799",
                    EmiBadgeText = "0% EMI ₹166/m",
                    OfferDisclaimer = "*Spill-Proof Fabric Lining",
                    ThemeGlowColor = "#ec4899",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion",
                    DisplayOrder = 6
                }
            }
        };

        // 4. STUDY ESSENTIALS
        hub.Categories["study-essentials"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "study-essentials",
            CategoryName = "Study Essentials",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "study_1",
                    CategorySlug = "study-essentials",
                    BadgeText = "CAMPUS ERGONOMIC",
                    Rating = "4.9",
                    Title = "Sihoo Pro Ergonomic Mesh Chair",
                    Tagline = "Adaptive Lumbar & 3D Armrest Support",
                    PriceText = "From ₹12,999*",
                    OriginalPriceText = "₹18,999",
                    EmiBadgeText = "0% EMI ₹1,083/m",
                    OfferDisclaimer = "*Student Posture Certified",
                    ThemeGlowColor = "#2563eb",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/study-essentials",
                    DisplayOrder = 1
                },
                new()
                {
                    Id = "study_2",
                    CategorySlug = "study-essentials",
                    BadgeText = "EXAM GEAR",
                    Rating = "4.8",
                    Title = "Texas Instruments TI-Nspire CX II",
                    Tagline = "Color CAS Graphing & SAT/ACT Approved",
                    PriceText = "From ₹13,490*",
                    OriginalPriceText = "₹16,990",
                    EmiBadgeText = "0% EMI ₹1,124/m",
                    OfferDisclaimer = "*Includes Student Software",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/study-essentials",
                    DisplayOrder = 2
                },
                new()
                {
                    Id = "study_3",
                    CategorySlug = "study-essentials",
                    BadgeText = "SMART LIGHTING",
                    Rating = "4.7",
                    Title = "BenQ WiT e-Reading LED Desk Lamp",
                    Tagline = "Auto-Dimming Eye-Care Dual Light",
                    PriceText = "From ₹8,990*",
                    OriginalPriceText = "₹12,990",
                    EmiBadgeText = "0% EMI ₹749/m",
                    OfferDisclaimer = "*Zero Screen Glare",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/study-essentials",
                    DisplayOrder = 3
                },
                new()
                {
                    Id = "study_4",
                    CategorySlug = "study-essentials",
                    BadgeText = "DUAL SCREEN",
                    Rating = "4.8",
                    Title = "Arzopa 15.6\" FHD Portable Monitor",
                    Tagline = "Ultra-Slim USB-C Plug & Play",
                    PriceText = "From ₹7,999*",
                    OriginalPriceText = "₹12,499",
                    EmiBadgeText = "0% EMI ₹666/m",
                    OfferDisclaimer = "*Works with Mac, Windows & Phones",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/study-essentials",
                    DisplayOrder = 4
                },
                new()
                {
                    Id = "study_5",
                    CategorySlug = "study-essentials",
                    BadgeText = "ALL-DAY POWER",
                    Rating = "4.9",
                    Title = "Anker Prime 20,000mAh 200W Power Bank",
                    Tagline = "Ultra-Fast Laptop & Phone Multi-Charge",
                    PriceText = "From ₹8,499*",
                    OriginalPriceText = "₹11,999",
                    EmiBadgeText = "0% EMI ₹708/m",
                    OfferDisclaimer = "*TSA Airline Approved",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/study-essentials",
                    DisplayOrder = 5
                },
                new()
                {
                    Id = "study_6",
                    CategorySlug = "study-essentials",
                    BadgeText = "FOCUS RETREAT",
                    Rating = "4.7",
                    Title = "White Noise Sound Machine with Warm Light",
                    Tagline = "24 Soothing Natural Sounds for Deep Sleep",
                    PriceText = "From ₹1,799*",
                    OriginalPriceText = "₹3,299",
                    EmiBadgeText = "0% EMI ₹149/m",
                    OfferDisclaimer = "*Compact Dorm Friendly",
                    ThemeGlowColor = "#ec4899",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/study-essentials",
                    DisplayOrder = 6
                }
            }
        };

        // 5. TRAVEL GEAR / LUGGAGE
        hub.Categories["travel-gear"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "travel-gear",
            CategoryName = "Travel Gear",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "trav_1",
                    CategorySlug = "travel-gear",
                    BadgeText = "AIRPORT CRUSHPROOF",
                    Rating = "4.9",
                    Title = "Samsonite Proxis 30\" Hardside Spinner",
                    Tagline = "Roxkin™ Polypropylene & Built-In TSA Lock",
                    PriceText = "From ₹28,990*",
                    OriginalPriceText = "₹39,990",
                    EmiBadgeText = "0% EMI ₹2,415/m",
                    OfferDisclaimer = "*10-Yr Global Warranty",
                    ThemeGlowColor = "#0284c7",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/travel-gear",
                    DisplayOrder = 1
                },
                new()
                {
                    Id = "trav_2",
                    CategorySlug = "travel-gear",
                    BadgeText = "STUDENT CARRY-ON",
                    Rating = "4.8",
                    Title = "Nomatic Travel Pack 40L Expandable",
                    Tagline = "Waterproof Tarpaulin & RFID Safe Pocket",
                    PriceText = "From ₹14,990*",
                    OriginalPriceText = "₹19,990",
                    EmiBadgeText = "0% EMI ₹1,249/m",
                    OfferDisclaimer = "*Flight Overhead Bin Compliant",
                    ThemeGlowColor = "#f97316",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/travel-gear",
                    DisplayOrder = 2
                },
                new()
                {
                    Id = "trav_3",
                    CategorySlug = "travel-gear",
                    BadgeText = "FLIGHT ERGONOMIC",
                    Rating = "4.7",
                    Title = "Cabeau Evolution S3 Neck Pillow",
                    Tagline = "360° Memory Foam with Seat Strap Anchor",
                    PriceText = "From ₹2,499*",
                    OriginalPriceText = "₹4,299",
                    EmiBadgeText = "0% EMI ₹208/m",
                    OfferDisclaimer = "*Zero Head Dropping Guaranteed",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/travel-gear",
                    DisplayOrder = 3
                },
                new()
                {
                    Id = "trav_4",
                    CategorySlug = "travel-gear",
                    BadgeText = "ORGANIZATION PRO",
                    Rating = "4.8",
                    Title = "Level8 Compression Packing Cubes 6-Set",
                    Tagline = "Heavy-Duty Ripstop & Double Compression",
                    PriceText = "From ₹1,899*",
                    OriginalPriceText = "₹3,499",
                    EmiBadgeText = "0% EMI ₹158/m",
                    OfferDisclaimer = "*Saves 60% Luggage Volume",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/travel-gear",
                    DisplayOrder = 4
                },
                new()
                {
                    Id = "trav_5",
                    CategorySlug = "travel-gear",
                    BadgeText = "GLOBAL TRACKER",
                    Rating = "4.9",
                    Title = "Apple AirTag 4-Pack Luggage Kit",
                    Tagline = "Ultra-Wideband Precision Finding Worldwide",
                    PriceText = "From ₹9,900*",
                    OriginalPriceText = "₹11,900",
                    EmiBadgeText = "0% EMI ₹825/m",
                    OfferDisclaimer = "*Never Lose Your Bags Abroad",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/travel-gear",
                    DisplayOrder = 5
                },
                new()
                {
                    Id = "trav_6",
                    CategorySlug = "travel-gear",
                    BadgeText = "PORTABLE SCALE",
                    Rating = "4.6",
                    Title = "Digital Luggage Scale with Thermometer",
                    Tagline = "50kg Capacity Backlit LCD Precision Scale",
                    PriceText = "From ₹699*",
                    OriginalPriceText = "₹1,499",
                    EmiBadgeText = "0% EMI ₹58/m",
                    OfferDisclaimer = "*Avoid Airport Overweight Fees",
                    ThemeGlowColor = "#ec4899",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/travel-gear",
                    DisplayOrder = 6
                }
            }
        };

        // 6. BEAUTY
        hub.Categories["beauty"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "beauty",
            CategoryName = "Beauty",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = CreateDefaultSlidesFor("beauty", "Beauty")
        };

        // 7. HEALTH CARE
        hub.Categories["health-care"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "health-care",
            CategoryName = "Health Care",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = CreateDefaultSlidesFor("health-care", "Health Care")
        };

        // 8. FOOD
        hub.Categories["food"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "food",
            CategoryName = "Food",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = CreateDefaultSlidesFor("food", "Food")
        };

        // 9. MOBILES
        hub.Categories["mobiles"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "mobiles",
            CategoryName = "Mobiles",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "mob_1",
                    CategorySlug = "mobiles",
                    BadgeText = "FLAGSHIP SPECIAL",
                    Rating = "4.6",
                    Title = "Galaxy S25 FE",
                    Tagline = "Unrivalled Nightography",
                    PriceText = "From ₹44,999*",
                    OriginalPriceText = "₹89,999",
                    EmiBadgeText = "0% EMI ₹3,750/m",
                    OfferDisclaimer = "*Incl. All Offers",
                    ThemeGlowColor = "#f97316",
                    ImageUrl = "/images/mobiles-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/mobiles",
                    DisplayOrder = 1
                },
                new()
                {
                    Id = "mob_2",
                    CategorySlug = "mobiles",
                    BadgeText = "GAMING BLAST",
                    Rating = "4.3",
                    Title = "POCO X8 Pro Max 5G",
                    Tagline = "Dimensity 9500s Flagship Speed",
                    PriceText = "From ₹42,999*",
                    OriginalPriceText = "₹57,999",
                    EmiBadgeText = "0% EMI ₹3,583/m",
                    OfferDisclaimer = "*Incl. All Offers",
                    ThemeGlowColor = "#eab308",
                    ImageUrl = "/images/mobiles-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/mobiles",
                    DisplayOrder = 2
                },
                new()
                {
                    Id = "mob_3",
                    CategorySlug = "mobiles",
                    BadgeText = "DESIGN ICON",
                    Rating = "4.4",
                    Title = "Nothing Phone (4b)",
                    Tagline = "Glyph Matrix 2.0 & Transparent",
                    PriceText = "From ₹25,999*",
                    OriginalPriceText = "₹54,999",
                    EmiBadgeText = "0% EMI ₹2,166/m",
                    OfferDisclaimer = "*Incl. All Offers",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/mobiles-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/mobiles",
                    DisplayOrder = 3
                },
                new()
                {
                    Id = "mob_4",
                    CategorySlug = "mobiles",
                    BadgeText = "TITANIUM PRO",
                    Rating = "4.9",
                    Title = "iPhone 16 Pro Max",
                    Tagline = "A18 Pro & Ceramic Shield",
                    PriceText = "From ₹1,29,900*",
                    OriginalPriceText = "₹1,44,900",
                    EmiBadgeText = "0% EMI ₹10,825/m",
                    OfferDisclaimer = "*Incl. Bank Offer",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/mobiles-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/mobiles",
                    DisplayOrder = 4
                },
                new()
                {
                    Id = "mob_5",
                    CategorySlug = "mobiles",
                    BadgeText = "SUPER CHARGE",
                    Rating = "4.5",
                    Title = "Realme GT 7 Pro",
                    Tagline = "Underwater Mode & 120W SuperCharge",
                    PriceText = "From ₹54,999*",
                    OriginalPriceText = "₹64,999",
                    EmiBadgeText = "0% EMI ₹4,583/m",
                    OfferDisclaimer = "*Incl. Special Offers",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/mobiles-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/mobiles",
                    DisplayOrder = 5
                },
                new()
                {
                    Id = "mob_6",
                    CategorySlug = "mobiles",
                    BadgeText = "NEXT-GEN FLIP",
                    Rating = "4.6",
                    Title = "Motorola Razr 50 Ultra",
                    Tagline = "Largest 4.0\" External Screen Fold",
                    PriceText = "From ₹89,999*",
                    OriginalPriceText = "₹1,09,999",
                    EmiBadgeText = "0% EMI ₹7,499/m",
                    OfferDisclaimer = "*Incl. Bank Offer",
                    ThemeGlowColor = "#ec4899",
                    ImageUrl = "/images/mobiles-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/mobiles",
                    DisplayOrder = 6
                }
            }
        };

        // 10. STORY BOOKS
        hub.Categories["story-books"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "story-books",
            CategoryName = "Story Books",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = CreateDefaultSlidesFor("story-books", "Story Books")
        };

        // 11. KITCHEN APPLIANCES
        hub.Categories["kitchen-appliances"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "kitchen-appliances",
            CategoryName = "Kitchen Appliances",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = CreateDefaultSlidesFor("kitchen-appliances", "Kitchen Appliances")
        };

        // 12. ROOM & LIVING
        hub.Categories["room-living"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "room-living",
            CategoryName = "Room & Living",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = CreateDefaultSlidesFor("room-living", "Room & Living")
        };

        // 13. GAMING HUB
        hub.Categories["gaming-hub"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "gaming-hub",
            CategoryName = "Gaming Hub",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "gaming_1",
                    CategorySlug = "gaming-hub",
                    BadgeText = "FLAGSHIP CONSOLE",
                    Rating = "4.9",
                    Title = "Sony PlayStation 5 Pro (2TB SSD)",
                    Tagline = "Advanced Ray Tracing & 120 FPS 4K Gaming",
                    PriceText = "₹1,69,999",
                    OriginalPriceText = "₹1,84,999",
                    EmiBadgeText = "0% EMI ₹14,166/m",
                    OfferDisclaimer = "*Incl. DualSense Wireless",
                    ThemeGlowColor = "#00439c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/gaming-hub/playstation",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "gaming_2",
                    CategorySlug = "gaming-hub",
                    BadgeText = "MICROSOFT POWER",
                    Rating = "4.8",
                    Title = "Xbox Series X (1TB Carbon Black)",
                    Tagline = "12 Teraflops GPU & 100+ Game Pass Titles",
                    PriceText = "₹49,990",
                    OriginalPriceText = "₹55,990",
                    EmiBadgeText = "0% EMI ₹4,165/m",
                    OfferDisclaimer = "*Incl. 3-Month Game Pass",
                    ThemeGlowColor = "#107c10",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/gaming-hub/xbox",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "gaming_3",
                    CategorySlug = "gaming-hub",
                    BadgeText = "HANDHELD CHAMPION",
                    Rating = "4.9",
                    Title = "Steam Deck OLED (1TB HDR)",
                    Tagline = "90Hz Vivid OLED & Custom AMD Zen 2 APU",
                    PriceText = "₹64,999",
                    OriginalPriceText = "₹72,990",
                    EmiBadgeText = "0% EMI ₹5,416/m",
                    OfferDisclaimer = "*Incl. Hard Carrying Case",
                    ThemeGlowColor = "#1a9fff",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/gaming-hub/steam-deck",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "gaming_4",
                    CategorySlug = "gaming-hub",
                    BadgeText = "NEXT-GEN VR",
                    Rating = "4.8",
                    Title = "PlayStation VR2 Sense Horizon Bundle",
                    Tagline = "4K HDR OLED & Intelligent Eye Tracking",
                    PriceText = "₹54,990",
                    OriginalPriceText = "₹61,999",
                    EmiBadgeText = "0% EMI ₹4,582/m",
                    OfferDisclaimer = "*Haptic Feedback Triggers",
                    ThemeGlowColor = "#d84e55",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/gaming-hub/vr-gaming",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "gaming_5",
                    CategorySlug = "gaming-hub",
                    BadgeText = "HYBRID MARVEL",
                    Rating = "4.8",
                    Title = "Nintendo Switch OLED (White Edition)",
                    Tagline = "7-inch Vivid OLED & Enhanced Audio Dock",
                    PriceText = "₹28,999",
                    OriginalPriceText = "₹34,990",
                    EmiBadgeText = "0% EMI ₹2,416/m",
                    OfferDisclaimer = "*64GB Internal Storage",
                    ThemeGlowColor = "#e60012",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/gaming-hub/nintendo",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "gaming_6",
                    CategorySlug = "gaming-hub",
                    BadgeText = "EXTREME RACING",
                    Rating = "4.7",
                    Title = "Logitech G29 Driving Force Racing Wheel",
                    Tagline = "Dual-Motor Force Feedback & Stainless Shifters",
                    PriceText = "₹26,490",
                    OriginalPriceText = "₹36,995",
                    EmiBadgeText = "0% EMI ₹2,207/m",
                    OfferDisclaimer = "*900-degree Lock-to-Lock",
                    ThemeGlowColor = "#00bcd4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/gaming-hub/steering-wheels",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };

        // 14 FASHION CATEGORIES (SHOWCASE BELOW "SHOP BY CATEGORY")
        AddMenClothingDefaults(hub);
        AddWomenClothingDefaults(hub);
        AddMenFootwearDefaults(hub);
        AddWomenFootwearDefaults(hub);
        AddWatchesDefaults(hub);
        AddSunglassesDefaults(hub);
        AddCapsHatsDefaults(hub);
        AddBeltsWalletsDefaults(hub);
        AddSocksDefaults(hub);
        AddAccessoriesDefaults(hub);
        AddHoodiesDefaults(hub);
        AddSweatshirtsDefaults(hub);
        AddEthnicWearDefaults(hub);
        AddThermalsDefaults(hub);

        return hub;
    }

    private static void AddMenClothingDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["men-clothing"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "men-clothing",
            CategoryName = "Men Clothing",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "men_cloth_1",
                    CategorySlug = "men-clothing",
                    BadgeText = "TAILORED WARMTH",
                    Rating = "4.8",
                    Title = "Men's Italian Wool Overcoat",
                    Tagline = "Tailored structured cut & premium inner lining",
                    PriceText = "From ₹3,799*",
                    OriginalPriceText = "₹6,999",
                    EmiBadgeText = "0% EMI ₹316/m",
                    OfferDisclaimer = "*Winter Campus Special",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/men-clothing",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "men_cloth_2",
                    CategorySlug = "men-clothing",
                    BadgeText = "STREETWEAR ICON",
                    Rating = "4.9",
                    Title = "450GSM Heavyweight Fleece Hoodie",
                    Tagline = "Pre-shrunk organic cotton & double-lined hood",
                    PriceText = "From ₹1,899*",
                    OriginalPriceText = "₹3,499",
                    EmiBadgeText = "0% EMI ₹158/m",
                    OfferDisclaimer = "*Lecture & Travel Favorite",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/hoodies",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "men_cloth_3",
                    CategorySlug = "men-clothing",
                    BadgeText = "CAMPUS FORMAL",
                    Rating = "4.7",
                    Title = "Supima Cotton Slim Business Shirt",
                    Tagline = "Wrinkle-resistant luxury weave for presentations",
                    PriceText = "From ₹1,499*",
                    OriginalPriceText = "₹2,799",
                    EmiBadgeText = "0% EMI ₹125/m",
                    OfferDisclaimer = "*100% Long-Staple Cotton",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/men-clothing",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "men_cloth_4",
                    CategorySlug = "men-clothing",
                    BadgeText = "ALL-DAY COMFORT",
                    Rating = "4.8",
                    Title = "Italian Fit Stretch Chino Trousers",
                    Tagline = "4-way flexible mobility with reinforced seams",
                    PriceText = "From ₹1,699*",
                    OriginalPriceText = "₹2,999",
                    EmiBadgeText = "0% EMI ₹141/m",
                    OfferDisclaimer = "*Dorm to Class Versatility",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/men-clothing",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "men_cloth_5",
                    CategorySlug = "men-clothing",
                    BadgeText = "RETRO ATHLETIC",
                    Rating = "4.7",
                    Title = "Suede Trim Varsity Bomber Jacket",
                    Tagline = "Snap button front with thermal ribbed collar",
                    PriceText = "From ₹2,799*",
                    OriginalPriceText = "₹4,999",
                    EmiBadgeText = "0% EMI ₹233/m",
                    OfferDisclaimer = "*Includes Student Pass Deal",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/men-clothing",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "men_cloth_6",
                    CategorySlug = "men-clothing",
                    BadgeText = "CAMPUS LOUNGE",
                    Rating = "4.8",
                    Title = "Tapered French Terry Joggers",
                    Tagline = "Moisture-wicking breathable gym & study pants",
                    PriceText = "From ₹1,199*",
                    OriginalPriceText = "₹2,199",
                    EmiBadgeText = "0% EMI ₹100/m",
                    OfferDisclaimer = "*Dual Zipper Pockets",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/men-clothing",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddWomenClothingDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["women-clothing"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "women-clothing",
            CategoryName = "Women Clothing",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "wom_cloth_1",
                    CategorySlug = "women-clothing",
                    BadgeText = "PARISIAN CHIC",
                    Rating = "4.9",
                    Title = "Double-Breasted Wool Trench Coat",
                    Tagline = "Storm flap collar with adjustable waist belt",
                    PriceText = "From ₹3,999*",
                    OriginalPriceText = "₹7,499",
                    EmiBadgeText = "0% EMI ₹333/m",
                    OfferDisclaimer = "*Autumn / Winter Edition",
                    ThemeGlowColor = "#e11d48",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-clothing",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "wom_cloth_2",
                    CategorySlug = "women-clothing",
                    BadgeText = "WARM KNIT",
                    Rating = "4.8",
                    Title = "Cashmere High-Neck Ribbed Sweater",
                    Tagline = "Featherlight warmth for chilly lecture halls",
                    PriceText = "From ₹2,299*",
                    OriginalPriceText = "₹3,999",
                    EmiBadgeText = "0% EMI ₹191/m",
                    OfferDisclaimer = "*100% Anti-Pilling Blend",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-clothing",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "wom_cloth_3",
                    CategorySlug = "women-clothing",
                    BadgeText = "EFFORTLESS ELEGANCE",
                    Rating = "4.8",
                    Title = "Floral Tiered Chiffon Midi Dress",
                    Tagline = "Breathable lining & flattering cinched waist",
                    PriceText = "From ₹1,899*",
                    OriginalPriceText = "₹3,299",
                    EmiBadgeText = "0% EMI ₹158/m",
                    OfferDisclaimer = "*Day-to-Evening Versatility",
                    ThemeGlowColor = "#ec4899",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-clothing",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "wom_cloth_4",
                    CategorySlug = "women-clothing",
                    BadgeText = "CAMPUS DENIM",
                    Rating = "4.7",
                    Title = "High-Rise Relaxed Straight Jeans",
                    Tagline = "Vintage wash denim with comfortable stretch",
                    PriceText = "From ₹1,599*",
                    OriginalPriceText = "₹2,899",
                    EmiBadgeText = "0% EMI ₹133/m",
                    OfferDisclaimer = "*Reinforced Riveted Pockets",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-clothing",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "wom_cloth_5",
                    CategorySlug = "women-clothing",
                    BadgeText = "POWER SUITING",
                    Rating = "4.8",
                    Title = "Tailored Oversized Boyfriend Blazer",
                    Tagline = "Padded shoulders with sleek interior pocket",
                    PriceText = "From ₹2,999*",
                    OriginalPriceText = "₹5,299",
                    EmiBadgeText = "0% EMI ₹250/m",
                    OfferDisclaimer = "*Internship & Presentation Fit",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-clothing",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "wom_cloth_6",
                    CategorySlug = "women-clothing",
                    BadgeText = "LOUNGE CHIC",
                    Rating = "4.7",
                    Title = "Organic Cotton Waffle Co-ord Set",
                    Tagline = "Relaxed drop-shoulder top with matching bottoms",
                    PriceText = "From ₹1,699*",
                    OriginalPriceText = "₹2,999",
                    EmiBadgeText = "0% EMI ₹141/m",
                    OfferDisclaimer = "*Ultra-Soft Cloud Fabric",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-clothing",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddMenFootwearDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["men-footwear"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "men-footwear",
            CategoryName = "Men Footwear",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "men_foot_1",
                    CategorySlug = "men-footwear",
                    BadgeText = "HERITAGE COMFORT",
                    Rating = "4.9",
                    Title = "Handcrafted Full-Grain Oxford Shoes",
                    Tagline = "Goodyear welted sole with memory foam footbed",
                    PriceText = "From ₹2,999*",
                    OriginalPriceText = "₹5,499",
                    EmiBadgeText = "0% EMI ₹250/m",
                    OfferDisclaimer = "*Formal & Interview Ready",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/men-footwear",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "men_foot_2",
                    CategorySlug = "men-footwear",
                    BadgeText = "ALL-DAY SNEAKER",
                    Rating = "4.8",
                    Title = "Air-Cushioned Campus Running Sneakers",
                    Tagline = "Breathable mesh upper with shock-absorbing foam",
                    PriceText = "From ₹2,299*",
                    OriginalPriceText = "₹3,999",
                    EmiBadgeText = "0% EMI ₹191/m",
                    OfferDisclaimer = "*Tested for 20,000+ Steps/Day",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/men-footwear",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "men_foot_3",
                    CategorySlug = "men-footwear",
                    BadgeText = "WINTER BOOTS",
                    Rating = "4.8",
                    Title = "Waterproof Chelsea Suede Boots",
                    Tagline = "Elastic side gussets & lugged non-slip rubber sole",
                    PriceText = "From ₹3,499*",
                    OriginalPriceText = "₹6,299",
                    EmiBadgeText = "0% EMI ₹291/m",
                    OfferDisclaimer = "*All-Weather Treated Leather",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/men-footwear",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "men_foot_4",
                    CategorySlug = "men-footwear",
                    BadgeText = "EASY SLIP-ON",
                    Rating = "4.7",
                    Title = "Suede Leather Penny Loafers",
                    Tagline = "Hand-stitched moc toe with arch support",
                    PriceText = "From ₹2,499*",
                    OriginalPriceText = "₹4,299",
                    EmiBadgeText = "0% EMI ₹208/m",
                    OfferDisclaimer = "*Smart-Casual Campus Essential",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/men-footwear",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "men_foot_5",
                    CategorySlug = "men-footwear",
                    BadgeText = "OUTDOOR TRAIL",
                    Rating = "4.9",
                    Title = "High-Traction All-Terrain Trekker Boots",
                    Tagline = "Ankle support cage with waterproof membrane",
                    PriceText = "From ₹3,799*",
                    OriginalPriceText = "₹6,999",
                    EmiBadgeText = "0% EMI ₹316/m",
                    OfferDisclaimer = "*Weekend Hiking & Snow Ready",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/men-footwear",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "men_foot_6",
                    CategorySlug = "men-footwear",
                    BadgeText = "RETRO VINTAGE",
                    Rating = "4.7",
                    Title = "Classic White Leather Court Sneakers",
                    Tagline = "Clean minimalist silhouette with padded collar",
                    PriceText = "From ₹1,899*",
                    OriginalPriceText = "₹3,499",
                    EmiBadgeText = "0% EMI ₹158/m",
                    OfferDisclaimer = "*Matches Every Campus Outfit",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/men-footwear",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddWomenFootwearDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["women-footwear"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "women-footwear",
            CategoryName = "Women Footwear",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "wom_foot_1",
                    CategorySlug = "women-footwear",
                    BadgeText = "ERGONOMIC HEELS",
                    Rating = "4.8",
                    Title = "Memory-Foam Pointed Block Pumps",
                    Tagline = "50mm stable block heel with anti-blister padding",
                    PriceText = "From ₹2,499*",
                    OriginalPriceText = "₹4,499",
                    EmiBadgeText = "0% EMI ₹208/m",
                    OfferDisclaimer = "*Presentation & Formal Events",
                    ThemeGlowColor = "#e11d48",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-footwear",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "wom_foot_2",
                    CategorySlug = "women-footwear",
                    BadgeText = "CAMPUS TREND",
                    Rating = "4.9",
                    Title = "Chunky Platform Lightweight Sneakers",
                    Tagline = "Ultra-lightweight EVA sole with cloud cushion",
                    PriceText = "From ₹2,199*",
                    OriginalPriceText = "₹3,799",
                    EmiBadgeText = "0% EMI ₹183/m",
                    OfferDisclaimer = "*Top Pick Among Students",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-footwear",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "wom_foot_3",
                    CategorySlug = "women-footwear",
                    BadgeText = "COLD WEATHER",
                    Rating = "4.8",
                    Title = "Waterproof Chelsea Leather Ankle Boots",
                    Tagline = "Deep grip lug tread & insulated microfiber lining",
                    PriceText = "From ₹3,299*",
                    OriginalPriceText = "₹5,899",
                    EmiBadgeText = "0% EMI ₹274/m",
                    OfferDisclaimer = "*Rain & Snow Protected",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-footwear",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "wom_foot_4",
                    CategorySlug = "women-footwear",
                    BadgeText = "ELEGANT STRAP",
                    Rating = "4.7",
                    Title = "Square-Toe Mid Block-Heel Sandals",
                    Tagline = "Ankle buckle strap with cushioned latex insole",
                    PriceText = "From ₹1,899*",
                    OriginalPriceText = "₹3,299",
                    EmiBadgeText = "0% EMI ₹158/m",
                    OfferDisclaimer = "*Farewell & Dinner Ready",
                    ThemeGlowColor = "#ec4899",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-footwear",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "wom_foot_5",
                    CategorySlug = "women-footwear",
                    BadgeText = "EVERYDAY COMFORT",
                    Rating = "4.8",
                    Title = "Foldable Leather Ballet Flats",
                    Tagline = "Flexible skid-proof sole & soft arch padding",
                    PriceText = "From ₹1,499*",
                    OriginalPriceText = "₹2,699",
                    EmiBadgeText = "0% EMI ₹125/m",
                    OfferDisclaimer = "*Packs Flat in Bag",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-footwear",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "wom_foot_6",
                    CategorySlug = "women-footwear",
                    BadgeText = "LUXURY SLIP-ON",
                    Rating = "4.7",
                    Title = "Horsebit Leather Driving Loafers",
                    Tagline = "Buttery soft grain leather with metallic hardware",
                    PriceText = "From ₹2,599*",
                    OriginalPriceText = "₹4,699",
                    EmiBadgeText = "0% EMI ₹216/m",
                    OfferDisclaimer = "*Campus to City Commute",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/women-footwear",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddWatchesDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["watches"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "watches",
            CategoryName = "Watches",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "wat_1",
                    CategorySlug = "watches",
                    BadgeText = "CHRONO MASTER",
                    Rating = "4.9",
                    Title = "Sapphire Crystal Chronograph Watch",
                    Tagline = "Precision Japanese movement & 316L stainless steel",
                    PriceText = "From ₹4,999*",
                    OriginalPriceText = "₹8,999",
                    EmiBadgeText = "0% EMI ₹416/m",
                    OfferDisclaimer = "*Water Resistant 50M",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/watches",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "wat_2",
                    CategorySlug = "watches",
                    BadgeText = "MECHANICAL ART",
                    Rating = "4.8",
                    Title = "Automatic Skeleton Exhibition Watch",
                    Tagline = "Self-winding mechanical caliber & glass caseback",
                    PriceText = "From ₹6,499*",
                    OriginalPriceText = "₹11,999",
                    EmiBadgeText = "0% EMI ₹541/m",
                    OfferDisclaimer = "*Zero Battery Needed",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/watches",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "wat_3",
                    CategorySlug = "watches",
                    BadgeText = "SMART GPS",
                    Rating = "4.8",
                    Title = "AMOLED All-Day Outdoor Smartwatch",
                    Tagline = "Health tracking, GPS routes, and 12-day battery",
                    PriceText = "From ₹3,999*",
                    OriginalPriceText = "₹6,999",
                    EmiBadgeText = "0% EMI ₹333/m",
                    OfferDisclaimer = "*iOS & Android Compatible",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/watches",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "wat_4",
                    CategorySlug = "watches",
                    BadgeText = "MINIMALIST LUXE",
                    Rating = "4.7",
                    Title = "Ultra-Slim Stainless Mesh Watch",
                    Tagline = "6.8mm profile case with scratch-proof mineral glass",
                    PriceText = "From ₹2,799*",
                    OriginalPriceText = "₹4,899",
                    EmiBadgeText = "0% EMI ₹233/m",
                    OfferDisclaimer = "*Magnetic Quick-Clasp",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/watches",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "wat_5",
                    CategorySlug = "watches",
                    BadgeText = "DEEP DIVER",
                    Rating = "4.8",
                    Title = "200M Professional Stainless Diver Watch",
                    Tagline = "Unidirectional ceramic bezel with luminous hands",
                    PriceText = "From ₹5,499*",
                    OriginalPriceText = "₹9,499",
                    EmiBadgeText = "0% EMI ₹458/m",
                    OfferDisclaimer = "*Screw-Down Crown",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/watches",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "wat_6",
                    CategorySlug = "watches",
                    BadgeText = "RETRO DIGITAL",
                    Rating = "4.7",
                    Title = "Vintage Gold-Tone Illuminator Watch",
                    Tagline = "Stopwatch, daily alarm, and retro electro backlight",
                    PriceText = "From ₹1,699*",
                    OriginalPriceText = "₹2,999",
                    EmiBadgeText = "0% EMI ₹141/m",
                    OfferDisclaimer = "*Iconic 90s Heritage",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/watches",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddSunglassesDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["sunglasses"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "sunglasses",
            CategoryName = "Sunglasses",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "sun_1",
                    CategorySlug = "sunglasses",
                    BadgeText = "POLARIZED TITANIUM",
                    Rating = "4.9",
                    Title = "Ultra-Lightweight Titanium Aviator",
                    Tagline = "100% UV400 polarized lenses with anti-glare coat",
                    PriceText = "From ₹2,499*",
                    OriginalPriceText = "₹4,499",
                    EmiBadgeText = "0% EMI ₹208/m",
                    OfferDisclaimer = "*Includes Hard Case & Cloth",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/sunglasses",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "sun_2",
                    CategorySlug = "sunglasses",
                    BadgeText = "TIMELESS SHADES",
                    Rating = "4.8",
                    Title = "Classic Matte Black Wayfarer Shades",
                    Tagline = "Impact-resistant polycarbonate frame & TAC lenses",
                    PriceText = "From ₹1,499*",
                    OriginalPriceText = "₹2,699",
                    EmiBadgeText = "0% EMI ₹125/m",
                    OfferDisclaimer = "*Everyday Campus Staple",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/sunglasses",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "sun_3",
                    CategorySlug = "sunglasses",
                    BadgeText = "RETRO CIRCULAR",
                    Rating = "4.7",
                    Title = "Round Vintage Wireframe Sunglasses",
                    Tagline = "Gold wire temples with green gradient tint",
                    PriceText = "From ₹1,799*",
                    OriginalPriceText = "₹3,199",
                    EmiBadgeText = "0% EMI ₹150/m",
                    OfferDisclaimer = "*Bohemian Festival Style",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/sunglasses",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "sun_4",
                    CategorySlug = "sunglasses",
                    BadgeText = "HALF-RIM CLUB",
                    Rating = "4.8",
                    Title = "Clubmaster Browline Sunglasses",
                    Tagline = "Handmade acetate upper with silver metal rim",
                    PriceText = "From ₹1,999*",
                    OriginalPriceText = "₹3,499",
                    EmiBadgeText = "0% EMI ₹166/m",
                    OfferDisclaimer = "*Intellectual Classic",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/sunglasses",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "sun_5",
                    CategorySlug = "sunglasses",
                    BadgeText = "HIGH FASHION",
                    Rating = "4.8",
                    Title = "Modern Sculpted Cat-Eye Shades",
                    Tagline = "Gradient UV400 lenses with premium acetate finish",
                    PriceText = "From ₹2,199*",
                    OriginalPriceText = "₹3,899",
                    EmiBadgeText = "0% EMI ₹183/m",
                    OfferDisclaimer = "*Runway Inspired Silhouette",
                    ThemeGlowColor = "#ec4899",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/sunglasses",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "sun_6",
                    CategorySlug = "sunglasses",
                    BadgeText = "SPORT PERFORMANCE",
                    Rating = "4.7",
                    Title = "Wraparound Shield Running Sunglasses",
                    Tagline = "Ventilated anti-fog lens with rubberized grip arms",
                    PriceText = "From ₹1,899*",
                    OriginalPriceText = "₹3,299",
                    EmiBadgeText = "0% EMI ₹158/m",
                    OfferDisclaimer = "*Cycling & Sports Ready",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/sunglasses",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddCapsHatsDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["caps-hats"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "caps-hats",
            CategoryName = "Caps & Hats",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "cap_1",
                    CategorySlug = "caps-hats",
                    BadgeText = "VINTAGE WASH",
                    Rating = "4.8",
                    Title = "6-Panel Washed Cotton Dad Cap",
                    Tagline = "Unstructured crown with pre-curved visor & brass clasp",
                    PriceText = "From ₹699*",
                    OriginalPriceText = "₹1,299",
                    EmiBadgeText = "0% EMI ₹58/m",
                    OfferDisclaimer = "*100% Breathable Cotton",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/caps-hats",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "cap_2",
                    CategorySlug = "caps-hats",
                    BadgeText = "WINTER WARMTH",
                    Rating = "4.9",
                    Title = "Merino Wool Ribbed Cuffed Beanie",
                    Tagline = "Double-layer thermal knit with zero itchy feel",
                    PriceText = "From ₹899*",
                    OriginalPriceText = "₹1,599",
                    EmiBadgeText = "0% EMI ₹75/m",
                    OfferDisclaimer = "*Essential Cold Weather Gear",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/caps-hats",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "cap_3",
                    CategorySlug = "caps-hats",
                    BadgeText = "RETRO CORDUROY",
                    Rating = "4.7",
                    Title = "Vintage Corduroy 5-Panel Snapback",
                    Tagline = "Soft wide-wale corduroy with embroidered crest",
                    PriceText = "From ₹799*",
                    OriginalPriceText = "₹1,499",
                    EmiBadgeText = "0% EMI ₹66/m",
                    OfferDisclaimer = "*Streetwear Daily Driver",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/caps-hats",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "cap_4",
                    CategorySlug = "caps-hats",
                    BadgeText = "REVERSIBLE SUN",
                    Rating = "4.8",
                    Title = "Reversible Heavy Cotton Bucket Hat",
                    Tagline = "Solid neutral on one side, graphic print on reverse",
                    PriceText = "From ₹849*",
                    OriginalPriceText = "₹1,499",
                    EmiBadgeText = "0% EMI ₹70/m",
                    OfferDisclaimer = "*UPF 50+ Sun Protection",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/caps-hats",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "cap_5",
                    CategorySlug = "caps-hats",
                    BadgeText = "AERO VISOR",
                    Rating = "4.7",
                    Title = "Moisture-Wicking Athletic Visor",
                    Tagline = "Quick-dry sweatband for running and tennis",
                    PriceText = "From ₹599*",
                    OriginalPriceText = "₹999",
                    EmiBadgeText = "0% EMI ₹50/m",
                    OfferDisclaimer = "*Ultra-Lightweight 42g",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/caps-hats",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "cap_6",
                    CategorySlug = "caps-hats",
                    BadgeText = "URBAN FLAT PEAK",
                    Rating = "4.8",
                    Title = "Structured Wool-Blend Snapback Cap",
                    Tagline = "High-crown 6-panel fit with contrast under-visor",
                    PriceText = "From ₹999*",
                    OriginalPriceText = "₹1,799",
                    EmiBadgeText = "0% EMI ₹83/m",
                    OfferDisclaimer = "*Pro-Fit Streetwear",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/caps-hats",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddBeltsWalletsDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["belts-wallets"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "belts-wallets",
            CategoryName = "Belts & Wallets",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "bw_1",
                    CategorySlug = "belts-wallets",
                    BadgeText = "DUAL STYLE",
                    Rating = "4.9",
                    Title = "Full-Grain Reversible Italian Leather Belt",
                    Tagline = "Twist-lock buckle rotates between Black & Rich Brown",
                    PriceText = "From ₹1,299*",
                    OriginalPriceText = "₹2,499",
                    EmiBadgeText = "0% EMI ₹108/m",
                    OfferDisclaimer = "*100% Genuine Bovine Leather",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/belts-wallets",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "bw_2",
                    CategorySlug = "belts-wallets",
                    BadgeText = "RFID PROTECTED",
                    Rating = "4.8",
                    Title = "Slim RFID-Blocking Bifold Wallet",
                    Tagline = "8 card slots, billfold, and pull-tab quick access",
                    PriceText = "From ₹999*",
                    OriginalPriceText = "₹1,899",
                    EmiBadgeText = "0% EMI ₹83/m",
                    OfferDisclaimer = "*Blocks 13.56 MHz RFID Scanners",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/belts-wallets",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "bw_3",
                    CategorySlug = "belts-wallets",
                    BadgeText = "AEROSPACE GRADE",
                    Rating = "4.8",
                    Title = "Minimalist Carbon Fiber Cardholder",
                    Tagline = "Holds up to 12 cards with integrated cash strap",
                    PriceText = "From ₹1,499*",
                    OriginalPriceText = "₹2,699",
                    EmiBadgeText = "0% EMI ₹125/m",
                    OfferDisclaimer = "*Weighs Only 45 Grams",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/belts-wallets",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "bw_4",
                    CategorySlug = "belts-wallets",
                    BadgeText = "MICRO ADJUST",
                    Rating = "4.7",
                    Title = "Automatic Click-Ratchet Leather Belt",
                    Tagline = "32 micro-adjust track positions for exact comfort",
                    PriceText = "From ₹1,199*",
                    OriginalPriceText = "₹2,199",
                    EmiBadgeText = "0% EMI ₹100/m",
                    OfferDisclaimer = "*No Holes, Never Stretches",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/belts-wallets",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "bw_5",
                    CategorySlug = "belts-wallets",
                    BadgeText = "TRAVEL PASSPORT",
                    Rating = "4.9",
                    Title = "All-in-One Zippered Passport Wallet",
                    Tagline = "Holds passport, boarding pass, currencies & SIM cards",
                    PriceText = "From ₹1,599*",
                    OriginalPriceText = "₹2,999",
                    EmiBadgeText = "0% EMI ₹133/m",
                    OfferDisclaimer = "*International Flight Ready",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/belts-wallets",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "bw_6",
                    CategorySlug = "belts-wallets",
                    BadgeText = "VINTAGE PATINA",
                    Rating = "4.7",
                    Title = "Distressed Hunter Leather Bifold",
                    Tagline = "Develops rich unique leather patina with daily use",
                    PriceText = "From ₹1,199*",
                    OriginalPriceText = "₹2,299",
                    EmiBadgeText = "0% EMI ₹100/m",
                    OfferDisclaimer = "*Handcrafted Saddle Stitch",
                    ThemeGlowColor = "#ec4899",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/belts-wallets",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddSocksDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["socks"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "socks",
            CategoryName = "Socks",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "soc_1",
                    CategorySlug = "socks",
                    BadgeText = "ANTI-SLIP LINER",
                    Rating = "4.9",
                    Title = "Bamboo Anti-Odor No-Show Loafer Socks (3-Pack)",
                    Tagline = "Silicone heel grip prevents slipping into shoes",
                    PriceText = "From ₹499*",
                    OriginalPriceText = "₹899",
                    EmiBadgeText = "0% EMI ₹41/m",
                    OfferDisclaimer = "*Naturally Antibacterial Bamboo",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/socks",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "soc_2",
                    CategorySlug = "socks",
                    BadgeText = "SUB-ZERO WARMTH",
                    Rating = "4.8",
                    Title = "Merino Wool Thermal Winter Crew Socks (2-Pack)",
                    Tagline = "Cushioned footbed & temperature regulating wool",
                    PriceText = "From ₹899*",
                    OriginalPriceText = "₹1,599",
                    EmiBadgeText = "0% EMI ₹75/m",
                    OfferDisclaimer = "*Tested for Below -10°C",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/socks",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "soc_3",
                    CategorySlug = "socks",
                    BadgeText = "CLASSIC ARGYLE",
                    Rating = "4.7",
                    Title = "Combed Cotton Argyle Business Socks (3-Pack)",
                    Tagline = "Seamless hand-linked toe with non-binding cuff",
                    PriceText = "From ₹599*",
                    OriginalPriceText = "₹1,099",
                    EmiBadgeText = "0% EMI ₹50/m",
                    OfferDisclaimer = "*Interview & Formal Ready",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/socks",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "soc_4",
                    CategorySlug = "socks",
                    BadgeText = "COMPRESSION SUPPORT",
                    Rating = "4.8",
                    Title = "Graduated Arch Compression Athletic Socks (3-Pack)",
                    Tagline = "Reduces foot fatigue during long days of walking",
                    PriceText = "From ₹699*",
                    OriginalPriceText = "₹1,299",
                    EmiBadgeText = "0% EMI ₹58/m",
                    OfferDisclaimer = "*15-20 mmHg Targeted Arch Band",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/socks",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "soc_5",
                    CategorySlug = "socks",
                    BadgeText = "PLUSH RUNNER",
                    Rating = "4.7",
                    Title = "Cushioned Terry Ankle Sports Socks (4-Pack)",
                    Tagline = "Reinforced heel and toe with breathable instep mesh",
                    PriceText = "From ₹499*",
                    OriginalPriceText = "₹999",
                    EmiBadgeText = "0% EMI ₹41/m",
                    OfferDisclaimer = "*High-Wear Durability",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/socks",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "soc_6",
                    CategorySlug = "socks",
                    BadgeText = "ORGANIC ESSENTIAL",
                    Rating = "4.8",
                    Title = "Ribbed Organic Cotton Everyday Crew (5-Pack)",
                    Tagline = "Soft breathable pure combed cotton in earth tones",
                    PriceText = "From ₹799*",
                    OriginalPriceText = "₹1,499",
                    EmiBadgeText = "0% EMI ₹66/m",
                    OfferDisclaimer = "*Dorm Laundry Certified",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/socks",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddAccessoriesDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["accessories"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "accessories",
            CategoryName = "Accessories",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "acc_1",
                    CategorySlug = "accessories",
                    BadgeText = "WEATHERPROOF COMMUTE",
                    Rating = "4.9",
                    Title = "Water-Resistant Campus Tech Backpack",
                    Tagline = "Dedicated 16\" laptop compartment & USB charging port",
                    PriceText = "From ₹2,499*",
                    OriginalPriceText = "₹4,499",
                    EmiBadgeText = "0% EMI ₹208/m",
                    OfferDisclaimer = "*YKK Zippers & Ergonomic Straps",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/accessories",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "acc_2",
                    CategorySlug = "accessories",
                    BadgeText = "100% SILK",
                    Rating = "4.8",
                    Title = "Pure Mulberry Silk Tie & Pocket Square Set",
                    Tagline = "Hand-rolled edges with matching cufflinks & gift box",
                    PriceText = "From ₹1,499*",
                    OriginalPriceText = "₹2,799",
                    EmiBadgeText = "0% EMI ₹125/m",
                    OfferDisclaimer = "*Formal Presentation Polish",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/accessories",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "acc_3",
                    CategorySlug = "accessories",
                    BadgeText = "MONOGRAM STEEL",
                    Rating = "4.7",
                    Title = "Brushed Stainless Steel Cufflinks & Tie Clip",
                    Tagline = "Laser-etched geometric design with bullet-back clasp",
                    PriceText = "From ₹999*",
                    OriginalPriceText = "₹1,799",
                    EmiBadgeText = "0% EMI ₹83/m",
                    OfferDisclaimer = "*Tarnish-Free 316L Steel",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/accessories",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "acc_4",
                    CategorySlug = "accessories",
                    BadgeText = "CASHMERE TOUCH",
                    Rating = "4.8",
                    Title = "Ultra-Soft Brushed Fringe Winter Scarf",
                    Tagline = "Generous 180cm wrap with insulating warmth",
                    PriceText = "From ₹1,199*",
                    OriginalPriceText = "₹2,199",
                    EmiBadgeText = "0% EMI ₹100/m",
                    OfferDisclaimer = "*Unisex Campus Styling",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/accessories",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "acc_5",
                    CategorySlug = "accessories",
                    BadgeText = "HEAVY DUTY",
                    Rating = "4.7",
                    Title = "Tactical Metal Key Organizer & Carabiner",
                    Tagline = "Holds 8 keys securely without jingling or scratches",
                    PriceText = "From ₹699*",
                    OriginalPriceText = "₹1,299",
                    EmiBadgeText = "0% EMI ₹58/m",
                    OfferDisclaimer = "*Titanium Alloy Clip",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/accessories",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "acc_6",
                    CategorySlug = "accessories",
                    BadgeText = "STORM PROOF",
                    Rating = "4.8",
                    Title = "Windproof Teflon Compact Automatic Umbrella",
                    Tagline = "9-rib fiberglass frame withstands 60 mph gusts",
                    PriceText = "From ₹899*",
                    OriginalPriceText = "₹1,599",
                    EmiBadgeText = "0% EMI ₹75/m",
                    OfferDisclaimer = "*Auto Open/Close Pushbutton",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/accessories",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddHoodiesDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["hoodies"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "hoodies",
            CategoryName = "Hoodies",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "hod_1",
                    CategorySlug = "hoodies",
                    BadgeText = "HEAVYWEIGHT 450GSM",
                    Rating = "4.9",
                    Title = "Custom Fit Heavyweight Fleece Hoodie",
                    Tagline = "Pre-shrunk organic cotton with dense brushed interior",
                    PriceText = "From ₹1,999*",
                    OriginalPriceText = "₹3,799",
                    EmiBadgeText = "0% EMI ₹166/m",
                    OfferDisclaimer = "*Campus Cold Weather Essential",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/hoodies",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "hod_2",
                    CategorySlug = "hoodies",
                    BadgeText = "STREETWEAR DROP",
                    Rating = "4.8",
                    Title = "Acid-Washed Oversized Drop-Shoulder Hoodie",
                    Tagline = "Vintage enzyme wash with distressed seam details",
                    PriceText = "From ₹1,899*",
                    OriginalPriceText = "₹3,499",
                    EmiBadgeText = "0% EMI ₹158/m",
                    OfferDisclaimer = "*Relaxed Boxy Silhouette",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/hoodies",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "hod_3",
                    CategorySlug = "hoodies",
                    BadgeText = "SHERPA LINED",
                    Rating = "4.9",
                    Title = "Thermal Sherpa-Lined Full Zip Hoodie",
                    Tagline = "Ultra-warm faux fur fleece body with metal YKK zipper",
                    PriceText = "From ₹2,499*",
                    OriginalPriceText = "₹4,499",
                    EmiBadgeText = "0% EMI ₹208/m",
                    OfferDisclaimer = "*Replaces Light Winter Jacket",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/hoodies",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "hod_4",
                    CategorySlug = "hoodies",
                    BadgeText = "ACTIVE PERFORMANCE",
                    Rating = "4.7",
                    Title = "Moisture-Wicking Tech Fleece Hoodie",
                    Tagline = "4-way athletic stretch with hidden zip phone pouch",
                    PriceText = "From ₹1,799*",
                    OriginalPriceText = "₹3,199",
                    EmiBadgeText = "0% EMI ₹150/m",
                    OfferDisclaimer = "*Gym & Early Morning Runs",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/hoodies",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "hod_5",
                    CategorySlug = "hoodies",
                    BadgeText = "MINIMAL EMBROIDERY",
                    Rating = "4.8",
                    Title = "Subtle Tone-on-Tone Embroidered Hoodie",
                    Tagline = "Clean tonal chest branding & ribbed side gussets",
                    PriceText = "From ₹1,699*",
                    OriginalPriceText = "₹2,999",
                    EmiBadgeText = "0% EMI ₹141/m",
                    OfferDisclaimer = "*Lecture Hall Minimalist",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/hoodies",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "hod_6",
                    CategorySlug = "hoodies",
                    BadgeText = "PIGMENT DYED",
                    Rating = "4.7",
                    Title = "Vintage Pigment-Dyed Pullover Hoodie",
                    Tagline = "Soft washed aesthetic that gets better with every wash",
                    PriceText = "From ₹1,749*",
                    OriginalPriceText = "₹3,199",
                    EmiBadgeText = "0% EMI ₹145/m",
                    OfferDisclaimer = "*100% Ring-Spun Cotton",
                    ThemeGlowColor = "#ec4899",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/hoodies",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddSweatshirtsDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["sweatshirts"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "sweatshirts",
            CategoryName = "Sweatshirts",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "swt_1",
                    CategorySlug = "sweatshirts",
                    BadgeText = "CAMPUS CLASSIC",
                    Rating = "4.8",
                    Title = "Brushed Fleece Crewneck Sweatshirt",
                    Tagline = "380GSM comfortable daily cotton-poly fleece",
                    PriceText = "From ₹1,399*",
                    OriginalPriceText = "₹2,499",
                    EmiBadgeText = "0% EMI ₹116/m",
                    OfferDisclaimer = "*Everyday Lecture Staple",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/sweatshirts",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "swt_2",
                    CategorySlug = "sweatshirts",
                    BadgeText = "VARSITY HERITAGE",
                    Rating = "4.9",
                    Title = "Chenille Patch Letterman Sweatshirt",
                    Tagline = "Vintage collegiate lettering with ribbed stripe cuffs",
                    PriceText = "From ₹1,699*",
                    OriginalPriceText = "₹2,999",
                    EmiBadgeText = "0% EMI ₹141/m",
                    OfferDisclaimer = "*Authentic Campus Spirit",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/sweatshirts",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "swt_3",
                    CategorySlug = "sweatshirts",
                    BadgeText = "QUARTER ZIP",
                    Rating = "4.8",
                    Title = "French Terry Quarter-Zip Pullover",
                    Tagline = "Stand-up collar with brushed brass pull for easy layering",
                    PriceText = "From ₹1,799*",
                    OriginalPriceText = "₹3,299",
                    EmiBadgeText = "0% EMI ₹150/m",
                    OfferDisclaimer = "*Smart-Casual Presentation Look",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/sweatshirts",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "swt_4",
                    CategorySlug = "sweatshirts",
                    BadgeText = "RAW EDGE",
                    Rating = "4.7",
                    Title = "Raw-Hem Relaxed Boxy Sweatshirt",
                    Tagline = "Drop-shoulder cut with subtle rolled unfinished hem",
                    PriceText = "From ₹1,499*",
                    OriginalPriceText = "₹2,699",
                    EmiBadgeText = "0% EMI ₹125/m",
                    OfferDisclaimer = "*Streetwear Aesthetic",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/sweatshirts",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "swt_5",
                    CategorySlug = "sweatshirts",
                    BadgeText = "THERMAL WAFFLE",
                    Rating = "4.8",
                    Title = "Heavy Waffle-Knit Layering Crew",
                    Tagline = "Textured honeycomb weave traps body heat effectively",
                    PriceText = "From ₹1,599*",
                    OriginalPriceText = "₹2,799",
                    EmiBadgeText = "0% EMI ₹133/m",
                    OfferDisclaimer = "*Breathable Base Layer",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/sweatshirts",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "swt_6",
                    CategorySlug = "sweatshirts",
                    BadgeText = "MOCK NECK",
                    Rating = "4.7",
                    Title = "Structured Mock-Neck Minimalist Pullover",
                    Tagline = "Clean architectural neck silhouette in dense fleece",
                    PriceText = "From ₹1,649*",
                    OriginalPriceText = "₹2,999",
                    EmiBadgeText = "0% EMI ₹137/m",
                    OfferDisclaimer = "*Modern European Campus Fit",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/sweatshirts",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddEthnicWearDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["ethnic-wear"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "ethnic-wear",
            CategoryName = "Ethnic Wear",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "eth_1",
                    CategorySlug = "ethnic-wear",
                    BadgeText = "FESTIVE ROYAL",
                    Rating = "4.9",
                    Title = "Pure Silk Blend Embroidered Kurta Set",
                    Tagline = "Intricate neckline embroidery with matching churidar",
                    PriceText = "From ₹2,999*",
                    OriginalPriceText = "₹5,499",
                    EmiBadgeText = "0% EMI ₹250/m",
                    OfferDisclaimer = "*Diwali & Campus Gala Ready",
                    ThemeGlowColor = "#f59e0b",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/ethnic-wear",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "eth_2",
                    CategorySlug = "ethnic-wear",
                    BadgeText = "NEHRU STYLING",
                    Rating = "4.8",
                    Title = "Designer Jacquard Bandhgala Waistcoat",
                    Tagline = "Mandarin collar with engraved brass buttons",
                    PriceText = "From ₹1,999*",
                    OriginalPriceText = "₹3,799",
                    EmiBadgeText = "0% EMI ₹166/m",
                    OfferDisclaimer = "*Pairs with Any Kurta or Shirt",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/ethnic-wear",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "eth_3",
                    CategorySlug = "ethnic-wear",
                    BadgeText = "HANDLOOM COTTON",
                    Rating = "4.8",
                    Title = "Breathable Daily Handloom Cotton Kurta",
                    Tagline = "Lightweight textured khadi weave with side pockets",
                    PriceText = "From ₹1,299*",
                    OriginalPriceText = "₹2,299",
                    EmiBadgeText = "0% EMI ₹108/m",
                    OfferDisclaimer = "*All-Day Campus Comfort",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/ethnic-wear",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "eth_4",
                    CategorySlug = "ethnic-wear",
                    BadgeText = "TRADITIONAL ENSEMBLE",
                    Rating = "4.7",
                    Title = "Silk Blend Dhoti Kurta Two-Piece Set",
                    Tagline = "Pre-pleated ready-to-wear dhoti with woven border",
                    PriceText = "From ₹2,499*",
                    OriginalPriceText = "₹4,499",
                    EmiBadgeText = "0% EMI ₹208/m",
                    OfferDisclaimer = "*Hassle-Free Traditional Draping",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/ethnic-wear",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "eth_5",
                    CategorySlug = "ethnic-wear",
                    BadgeText = "MODERN INDOWESTERN",
                    Rating = "4.8",
                    Title = "Asymmetric Hem Indowestern Kurta",
                    Tagline = "Contemporary overlap styling with button placket",
                    PriceText = "From ₹1,899*",
                    OriginalPriceText = "₹3,499",
                    EmiBadgeText = "0% EMI ₹158/m",
                    OfferDisclaimer = "*Fusion Campus Celebrations",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/ethnic-wear",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "eth_6",
                    CategorySlug = "ethnic-wear",
                    BadgeText = "REGAL VELVET",
                    Rating = "4.9",
                    Title = "Velvet Zari Embroidered Sherwani Coat",
                    Tagline = "Rich micro-velvet with antique gold threadwork",
                    PriceText = "From ₹4,999*",
                    OriginalPriceText = "₹9,999",
                    EmiBadgeText = "0% EMI ₹416/m",
                    OfferDisclaimer = "*Graduation & Wedding Ceremonies",
                    ThemeGlowColor = "#ec4899",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/ethnic-wear",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    private static void AddThermalsDefaults(CategoryThreeSectionHubDto hub)
    {
        hub.Categories["thermals"] = new CategoryThreeSectionConfigDto
        {
            CategorySlug = "thermals",
            CategoryName = "Thermals",
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = new List<CategorySliderSlideDto>
            {
                new()
                {
                    Id = "thm_1",
                    CategorySlug = "thermals",
                    BadgeText = "MERINO WOOL",
                    Rating = "4.9",
                    Title = "100% Merino Wool Base Layer Thermal Top",
                    Tagline = "Ultra-fine 18.5 micron wool with natural odor resistance",
                    PriceText = "From ₹1,899*",
                    OriginalPriceText = "₹3,499",
                    EmiBadgeText = "0% EMI ₹158/m",
                    OfferDisclaimer = "*Active Body Heat Regulation",
                    ThemeGlowColor = "#3b82f6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/thermals",
                    DisplayOrder = 1,
                    IsActive = true
                },
                new()
                {
                    Id = "thm_2",
                    CategorySlug = "thermals",
                    BadgeText = "EXTREME WARMTH",
                    Rating = "4.9",
                    Title = "Sub-Zero Thermal Compression Leggings",
                    Tagline = "Dense fleece interior rated for down to -20°C temperatures",
                    PriceText = "From ₹1,499*",
                    OriginalPriceText = "₹2,799",
                    EmiBadgeText = "0% EMI ₹125/m",
                    OfferDisclaimer = "*Snow & Freezing Campus Commute",
                    ThemeGlowColor = "#ea580c",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/thermals",
                    DisplayOrder = 2,
                    IsActive = true
                },
                new()
                {
                    Id = "thm_3",
                    CategorySlug = "thermals",
                    BadgeText = "SEAMLESS COMFORT",
                    Rating = "4.8",
                    Title = "Seamless Second-Skin Thermal Inner Set",
                    Tagline = "Zero chafing flat-lock seams with 4-way stretch",
                    PriceText = "From ₹2,299*",
                    OriginalPriceText = "₹3,999",
                    EmiBadgeText = "0% EMI ₹191/m",
                    OfferDisclaimer = "*Top & Bottom Matching Pack",
                    ThemeGlowColor = "#8b5cf6",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/thermals",
                    DisplayOrder = 3,
                    IsActive = true
                },
                new()
                {
                    Id = "thm_4",
                    CategorySlug = "thermals",
                    BadgeText = "POLAR FLEECE",
                    Rating = "4.7",
                    Title = "Insulated Polar-Fleece Thermal Undershirt",
                    Tagline = "Traps micro-air pockets to preserve core body warmth",
                    PriceText = "From ₹1,199*",
                    OriginalPriceText = "₹2,199",
                    EmiBadgeText = "0% EMI ₹100/m",
                    OfferDisclaimer = "*Lightweight Under-Clothing Fit",
                    ThemeGlowColor = "#10b981",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/thermals",
                    DisplayOrder = 4,
                    IsActive = true
                },
                new()
                {
                    Id = "thm_5",
                    CategorySlug = "thermals",
                    BadgeText = "MICRO SILK BLEND",
                    Rating = "4.8",
                    Title = "Silk-Touch Ultra-Thin Warmth Camisole / Top",
                    Tagline = "Invisible under formal shirts and thin knits",
                    PriceText = "From ₹999*",
                    OriginalPriceText = "₹1,899",
                    EmiBadgeText = "0% EMI ₹83/m",
                    OfferDisclaimer = "*Featherlight 85g Layer",
                    ThemeGlowColor = "#ec4899",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/thermals",
                    DisplayOrder = 5,
                    IsActive = true
                },
                new()
                {
                    Id = "thm_6",
                    CategorySlug = "thermals",
                    BadgeText = "ARCTIC HEAVYWEIGHT",
                    Rating = "4.9",
                    Title = "Double-Knitted Thermal Long Johns",
                    Tagline = "Dual-layered ribbed knit with elasticized ankle cuffs",
                    PriceText = "From ₹1,399*",
                    OriginalPriceText = "₹2,599",
                    EmiBadgeText = "0% EMI ₹116/m",
                    OfferDisclaimer = "*Wind Chill Protected",
                    ThemeGlowColor = "#06b6d4",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    TargetUrl = "/category/fashion/thermals",
                    DisplayOrder = 6,
                    IsActive = true
                }
            }
        };
    }

    public static List<CategorySliderSlideDto> CreateDefaultSlidesFor(string slug, string name)
    {
        return new List<CategorySliderSlideDto>
        {
            new()
            {
                Id = $"{slug}_1",
                CategorySlug = slug,
                BadgeText = "CAMPUS BESTSELLER",
                Rating = "4.8",
                Title = $"{name} Signature Edition",
                Tagline = "Curated specifically for university life",
                PriceText = "From ₹1,499*",
                OriginalPriceText = "₹2,999",
                EmiBadgeText = "0% EMI ₹250/m",
                OfferDisclaimer = "*Verified Student Quality",
                ThemeGlowColor = "#f97316",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TargetUrl = $"/category/{slug}",
                DisplayOrder = 1
            },
            new()
            {
                Id = $"{slug}_2",
                CategorySlug = slug,
                BadgeText = "PREMIUM VALUE",
                Rating = "4.7",
                Title = $"{name} Deluxe Multi-Kit",
                Tagline = "Essential pack with comprehensive warranty",
                PriceText = "From ₹2,199*",
                OriginalPriceText = "₹3,799",
                EmiBadgeText = "0% EMI ₹366/m",
                OfferDisclaimer = "*Special Campus Offer",
                ThemeGlowColor = "#3b82f6",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TargetUrl = $"/category/{slug}",
                DisplayOrder = 2
            },
            new()
            {
                Id = $"{slug}_3",
                CategorySlug = slug,
                BadgeText = "LIMITED DEAL",
                Rating = "4.9",
                Title = $"{name} Ultra Pro Set",
                Tagline = "High-performance international grade",
                PriceText = "From ₹3,499*",
                OriginalPriceText = "₹5,999",
                EmiBadgeText = "0% EMI ₹583/m",
                OfferDisclaimer = "*Instant Bank Cashback",
                ThemeGlowColor = "#10b981",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TargetUrl = $"/category/{slug}",
                DisplayOrder = 3
            },
            new()
            {
                Id = $"{slug}_4",
                CategorySlug = slug,
                BadgeText = "STUDENT CHOICE",
                Rating = "4.6",
                Title = $"{name} Essential Starter",
                Tagline = "Compact, durable and easy to carry",
                PriceText = "From ₹999*",
                OriginalPriceText = "₹1,899",
                EmiBadgeText = "0% EMI ₹166/m",
                OfferDisclaimer = "*Loved by 10k+ Students",
                ThemeGlowColor = "#8b5cf6",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TargetUrl = $"/category/{slug}",
                DisplayOrder = 4
            },
            new()
            {
                Id = $"{slug}_5",
                CategorySlug = slug,
                BadgeText = "TOP RATED",
                Rating = "4.8",
                Title = $"{name} Executive Series",
                Tagline = "Advanced reliability and modern design",
                PriceText = "From ₹4,299*",
                OriginalPriceText = "₹6,999",
                EmiBadgeText = "0% EMI ₹716/m",
                OfferDisclaimer = "*Includes Free Support",
                ThemeGlowColor = "#f59e0b",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TargetUrl = $"/category/{slug}",
                DisplayOrder = 5
            },
            new()
            {
                Id = $"{slug}_6",
                CategorySlug = slug,
                BadgeText = "SMART EDITION",
                Rating = "4.7",
                Title = $"{name} Next-Gen Edition",
                Tagline = "Next-generation student technology",
                PriceText = "From ₹2,799*",
                OriginalPriceText = "₹4,499",
                EmiBadgeText = "0% EMI ₹466/m",
                OfferDisclaimer = "*Limited Semester Stock",
                ThemeGlowColor = "#ec4899",
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                TargetUrl = $"/category/{slug}",
                DisplayOrder = 6
            }
        };
    }
}

