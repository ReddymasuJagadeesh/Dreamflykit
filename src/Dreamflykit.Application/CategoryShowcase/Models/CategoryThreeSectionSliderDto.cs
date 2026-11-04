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

        // Return a sensible default if not explicitly registered
        return new CategoryThreeSectionConfigDto
        {
            CategorySlug = categorySlug,
            CategoryName = categorySlug.Replace("-", " "),
            IsEnabled = true,
            AutoPlay = true,
            SlideIntervalSeconds = 4,
            TransitionDurationMs = 650,
            Slides = CreateDefaultSlidesFor(categorySlug, categorySlug.Replace("-", " "))
        };
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

        return hub;
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

