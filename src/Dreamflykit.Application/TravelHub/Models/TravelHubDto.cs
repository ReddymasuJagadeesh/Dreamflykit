namespace Dreamflykit.Application.TravelHub.Models;

public sealed class TravelHubDto
{
    public TravelThemeSettingsDto ThemeSettings { get; set; } = new();
    public TravelTopNoticeBarDto TopNoticeBar { get; set; } = new();
    public TravelHeroBannerDto HeroBanner { get; set; } = new();
    public TravelSidebarDto Sidebar { get; set; } = new();
    public TravelShopByCategoriesDto ShopByCategories { get; set; } = new();
    public TravelBottomTrustBarDto BottomTrustBar { get; set; } = new();

    public static TravelHubDto CreateDefault()
    {
        return new TravelHubDto
        {
            ThemeSettings = new TravelThemeSettingsDto
            {
                StoreTitle = "Dreamflykit • Travel Gear & Luggage",
                PrimaryColor = "#0f2b48",
                AccentColor = "#0284c7",
                SoftBackground = "#f8fafc",
                HeroBackground = "linear-gradient(135deg, #e0f2fe 0%, #f0f9ff 50%, #ffffff 100%)",
                CardBorderRadius = "12px"
            },

            TopNoticeBar = new TravelTopNoticeBarDto
            {
                IsEnabled = true,
                Item1Text = "Delivering Across USA, UK, Canada, Australia & More",
                Item2Text = "100% Genuine Products",
                Item3Text = "Student Trusted",
                SupportPhone = "+91 91234 56789",
                TrackOrderUrl = "/Orders/Track"
            },

            HeroBanner = new TravelHeroBannerDto
            {
                IsEnabled = true,
                Heading = "Travel Gear",
                HeadingBadge = "✈️",
                Subtitle = "Everything you need for a smooth, comfortable and stress-free journey.",
                ImageUrl = "/images/travel-hub/hero-travel-banner.webp",
                IsUploadingSoon = true,
                Badges = new List<TravelBadgeDto>
                {
                    new()
                    {
                        Id = "badge-1",
                        Icon = "shield-check",
                        Title = "Curated for Every Traveler",
                        Subtitle = "Students, Tourists, Professionals & Honeymooners"
                    },
                    new()
                    {
                        Id = "badge-2",
                        Icon = "award",
                        Title = "Premium Quality",
                        Subtitle = "Durable, reliable & long-lasting products"
                    },
                    new()
                    {
                        Id = "badge-3",
                        Icon = "layers",
                        Title = "Smart & Compact",
                        Subtitle = "Space-saving & travel-friendly essentials"
                    },
                    new()
                    {
                        Id = "badge-4",
                        Icon = "globe",
                        Title = "Ready for Any Destination",
                        Subtitle = "From visa to boarding, we've got you covered"
                    }
                },
                PerfectJourneyTitle = "Perfect for Every Journey",
                PerfectJourneys = new List<TravelPersonaItemDto>
                {
                    new()
                    {
                        Id = "journey-1",
                        Icon = "graduation-cap",
                        Title = "MS / Study Abroad",
                        Subtitle = "Long-term stay essentials",
                        TargetUrl = "/category/travel-gear?subcategory=packing-organizers"
                    },
                    new()
                    {
                        Id = "journey-2",
                        Icon = "heart",
                        Title = "Honeymoon Trips",
                        Subtitle = "Romantic & comfortable travel",
                        TargetUrl = "/category/travel-gear?subcategory=comfort-essentials"
                    },
                    new()
                    {
                        Id = "journey-3",
                        Icon = "users",
                        Title = "Family Vacations",
                        Subtitle = "Safe & convenient packing",
                        TargetUrl = "/category/travel-gear?subcategory=luggage-suitcases"
                    },
                    new()
                    {
                        Id = "journey-4",
                        Icon = "briefcase",
                        Title = "Business Trips",
                        Subtitle = "Stay organized, travel smart",
                        TargetUrl = "/category/travel-gear?subcategory=tech-accessories"
                    },
                    new()
                    {
                        Id = "journey-5",
                        Icon = "backpack",
                        Title = "Weekend Getaways",
                        Subtitle = "Light, compact & hassle-free",
                        TargetUrl = "/category/travel-gear?subcategory=toiletry-hygiene"
                    }
                }
            },

            Sidebar = new TravelSidebarDto
            {
                TravelForTitle = "Travel For",
                TravelForItems = new List<TravelForPersonaItemDto>
                {
                    new()
                    {
                        Id = "tf-1",
                        Title = "MS / Study Abroad Students",
                        Subtitle = "Long stay essentials",
                        TargetUrl = "/category/travel-gear?subcategory=packing-organizers",
                        Icon = "graduation-cap"
                    },
                    new()
                    {
                        Id = "tf-2",
                        Title = "Honeymoon Couples",
                        Subtitle = "Romantic travel must-haves",
                        TargetUrl = "/category/travel-gear?subcategory=comfort-essentials",
                        Icon = "heart"
                    },
                    new()
                    {
                        Id = "tf-3",
                        Title = "Family Travelers",
                        Subtitle = "Comfort & convenience",
                        TargetUrl = "/category/travel-gear?subcategory=luggage-suitcases",
                        Icon = "users"
                    },
                    new()
                    {
                        Id = "tf-4",
                        Title = "Business Travelers",
                        Subtitle = "Smart & professional",
                        TargetUrl = "/category/travel-gear?subcategory=tech-accessories",
                        Icon = "briefcase"
                    },
                    new()
                    {
                        Id = "tf-5",
                        Title = "Weekend Getaways",
                        Subtitle = "Light & compact travel",
                        TargetUrl = "/category/travel-gear?subcategory=toiletry-hygiene",
                        Icon = "backpack"
                    }
                },
                QuizCard = new TravelQuizCardDto
                {
                    IsEnabled = true,
                    Title = "Not Sure What You Need?",
                    Subtitle = "Take our quick Travel Kit Quiz and get personalized picks!",
                    ButtonText = "Take Quiz Now →",
                    ButtonUrl = "/Kits/FindMyKit",
                    ImageUrl = "/images/travel-hub/quiz-travel-box.webp",
                    IsUploadingSoon = true
                }
            },

            ShopByCategories = new TravelShopByCategoriesDto
            {
                Heading = "Everything You Need, All in One Place",
                SortLabel = "Sort By: Popular",
                Cards = new List<TravelCategoryCardDto>
                {
                    new()
                    {
                        Id = "cat-luggage-suitcases",
                        Title = "Luggage & Suitcases",
                        Subtitle = "Trolleys, backpacks, duffels & travel bags",
                        Slug = "luggage-suitcases",
                        ImageUrl = "/images/travel-hub/cat-luggage-suitcases.webp",
                        TargetUrl = "/category/luggage-bags",
                        IsUploadingSoon = true,
                        DisplayOrder = 1,
                        IsActive = true,
                        Icon = "luggage"
                    },
                    new()
                    {
                        Id = "cat-packing-organizers",
                        Title = "Packing Organizers",
                        Subtitle = "Packing cubes, pouches, socks & laundry bags",
                        Slug = "packing-organizers",
                        ImageUrl = "/images/travel-hub/cat-packing-organizers.webp",
                        TargetUrl = "/category/travel-gear?subcategory=packing-organizers",
                        IsUploadingSoon = true,
                        DisplayOrder = 2,
                        IsActive = true,
                        Icon = "boxes"
                    },
                    new()
                    {
                        Id = "cat-comfort-essentials",
                        Title = "Comfort Essentials",
                        Subtitle = "Neck pillows, eye masks, earplugs & more",
                        Slug = "comfort-essentials",
                        ImageUrl = "/images/travel-hub/cat-comfort-essentials.webp",
                        TargetUrl = "/category/travel-gear?subcategory=comfort-essentials",
                        IsUploadingSoon = true,
                        DisplayOrder = 3,
                        IsActive = true,
                        Icon = "smile"
                    },
                    new()
                    {
                        Id = "cat-adapters-converters",
                        Title = "Adapters & Converters",
                        Subtitle = "Universal adapters, voltage converters & plugs",
                        Slug = "adapters-converters",
                        ImageUrl = "/images/travel-hub/cat-adapters-converters.webp",
                        TargetUrl = "/category/travel-gear?subcategory=adapters-converters",
                        IsUploadingSoon = true,
                        DisplayOrder = 4,
                        IsActive = true,
                        Icon = "zap"
                    },
                    new()
                    {
                        Id = "cat-bottles-hydration",
                        Title = "Bottles & Hydration",
                        Subtitle = "Water bottles, flasks, mugs & tumblers",
                        Slug = "bottles-hydration",
                        ImageUrl = "/images/travel-hub/cat-bottles-hydration.webp",
                        TargetUrl = "/category/travel-gear?subcategory=bottles-hydration",
                        IsUploadingSoon = true,
                        DisplayOrder = 5,
                        IsActive = true,
                        Icon = "coffee"
                    },
                    new()
                    {
                        Id = "cat-toiletry-hygiene",
                        Title = "Toiletry & Hygiene",
                        Subtitle = "Toiletry bags, travel bottles, essentials & personal care",
                        Slug = "toiletry-hygiene",
                        ImageUrl = "/images/travel-hub/cat-toiletry-hygiene.webp",
                        TargetUrl = "/category/travel-gear?subcategory=toiletry-hygiene",
                        IsUploadingSoon = true,
                        DisplayOrder = 6,
                        IsActive = true,
                        Icon = "droplet"
                    },
                    new()
                    {
                        Id = "cat-travel-documents",
                        Title = "Travel Documents",
                        Subtitle = "Passport holders, wallet, ID holders & card cases",
                        Slug = "travel-documents",
                        ImageUrl = "/images/travel-hub/cat-travel-documents.webp",
                        TargetUrl = "/category/travel-gear?subcategory=travel-documents",
                        IsUploadingSoon = true,
                        DisplayOrder = 7,
                        IsActive = true,
                        Icon = "file-text"
                    },
                    new()
                    {
                        Id = "cat-sim-connectivity",
                        Title = "SIM & Connectivity",
                        Subtitle = "Travel SIMs, WiFi devices, data cards & more",
                        Slug = "sim-connectivity",
                        ImageUrl = "/images/travel-hub/cat-sim-connectivity.webp",
                        TargetUrl = "/category/travel-gear?subcategory=sim-connectivity",
                        IsUploadingSoon = true,
                        DisplayOrder = 8,
                        IsActive = true,
                        Icon = "wifi"
                    },
                    new()
                    {
                        Id = "cat-travel-insurance",
                        Title = "Travel Insurance",
                        Subtitle = "Stay protected on your journey anywhere",
                        Slug = "travel-insurance",
                        ImageUrl = "/images/travel-hub/cat-travel-insurance.webp",
                        TargetUrl = "/category/travel-gear?subcategory=travel-insurance",
                        IsUploadingSoon = true,
                        DisplayOrder = 9,
                        IsActive = true,
                        Icon = "shield"
                    },
                    new()
                    {
                        Id = "cat-locks-security",
                        Title = "Locks & Security",
                        Subtitle = "TSA locks, luggage straps, anti-theft gear",
                        Slug = "locks-security",
                        ImageUrl = "/images/travel-hub/cat-locks-security.webp",
                        TargetUrl = "/category/travel-gear?subcategory=locks-security",
                        IsUploadingSoon = true,
                        DisplayOrder = 10,
                        IsActive = true,
                        Icon = "lock"
                    },
                    new()
                    {
                        Id = "cat-tech-accessories",
                        Title = "Tech Accessories",
                        Subtitle = "Power banks, cables, chargers & more",
                        Slug = "tech-accessories",
                        ImageUrl = "/images/travel-hub/cat-tech-accessories.webp",
                        TargetUrl = "/category/travel-gear?subcategory=tech-accessories",
                        IsUploadingSoon = true,
                        DisplayOrder = 11,
                        IsActive = true,
                        Icon = "battery-charging"
                    },
                    new()
                    {
                        Id = "cat-travel-entertainment",
                        Title = "Travel Entertainment",
                        Subtitle = "Headphones, stands, reading lights & more",
                        Slug = "travel-entertainment",
                        ImageUrl = "/images/travel-hub/cat-travel-entertainment.webp",
                        TargetUrl = "/category/travel-gear?subcategory=travel-entertainment",
                        IsUploadingSoon = true,
                        DisplayOrder = 12,
                        IsActive = true,
                        Icon = "headphones"
                    }
                }
            },

            BottomTrustBar = new TravelBottomTrustBarDto
            {
                IsEnabled = true,
                Badges = new List<TravelTrustBadgeDto>
                {
                    new()
                    {
                        Icon = "truck",
                        Title = "Fast & Reliable Delivery",
                        Subtitle = "On-time delivery worldwide"
                    },
                    new()
                    {
                        Icon = "shield-check",
                        Title = "100% Secure Payments",
                        Subtitle = "Multiple payment options"
                    },
                    new()
                    {
                        Icon = "check-circle",
                        Title = "Premium Quality Products",
                        Subtitle = "Genuine & trusted"
                    },
                    new()
                    {
                        Icon = "headphones",
                        Title = "24/7 Customer Support",
                        Subtitle = "We're here to help"
                    },
                    new()
                    {
                        Icon = "rotate-ccw",
                        Title = "Easy Returns",
                        Subtitle = "Hassle-free returns"
                    }
                }
            }
        };
    }
}

public sealed class TravelThemeSettingsDto
{
    public string StoreTitle { get; set; } = "Dreamflykit • Travel Gear & Luggage";
    public string PrimaryColor { get; set; } = "#0f2b48";
    public string AccentColor { get; set; } = "#0284c7";
    public string SoftBackground { get; set; } = "#f8fafc";
    public string HeroBackground { get; set; } = "linear-gradient(135deg, #e0f2fe 0%, #f0f9ff 50%, #ffffff 100%)";
    public string CardBorderRadius { get; set; } = "12px";
}

public sealed class TravelTopNoticeBarDto
{
    public bool IsEnabled { get; set; } = true;
    public string Item1Text { get; set; } = "Delivering Across USA, UK, Canada, Australia & More";
    public string Item2Text { get; set; } = "100% Genuine Products";
    public string Item3Text { get; set; } = "Student Trusted";
    public string SupportPhone { get; set; } = "+91 91234 56789";
    public string TrackOrderUrl { get; set; } = "/Orders/Track";
}

public sealed class TravelHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Heading { get; set; } = "Travel Gear";
    public string HeadingBadge { get; set; } = "✈️";
    public string Subtitle { get; set; } = "Everything you need for a smooth, comfortable and stress-free journey.";
    public string ImageUrl { get; set; } = "/images/travel-hub/hero-travel-banner.webp";
    public bool IsUploadingSoon { get; set; } = true;
    public List<TravelBadgeDto> Badges { get; set; } = new();
    public string PerfectJourneyTitle { get; set; } = "Perfect for Every Journey";
    public List<TravelPersonaItemDto> PerfectJourneys { get; set; } = new();
}

public sealed class TravelBadgeDto
{
    public string Id { get; set; } = "";
    public string Icon { get; set; } = "";
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
}

public sealed class TravelPersonaItemDto
{
    public string Id { get; set; } = "";
    public string Icon { get; set; } = "";
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string TargetUrl { get; set; } = "/category/travel-gear";
}

public sealed class TravelSidebarDto
{
    public string TravelForTitle { get; set; } = "Travel For";
    public List<TravelForPersonaItemDto> TravelForItems { get; set; } = new();
    public TravelQuizCardDto QuizCard { get; set; } = new();
}

public sealed class TravelForPersonaItemDto
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string TargetUrl { get; set; } = "/category/travel-gear";
    public string Icon { get; set; } = "";
}

public sealed class TravelQuizCardDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Not Sure What You Need?";
    public string Subtitle { get; set; } = "Take our quick Travel Kit Quiz and get personalized picks!";
    public string ButtonText { get; set; } = "Take Quiz Now →";
    public string ButtonUrl { get; set; } = "/Kits/FindMyKit";
    public string ImageUrl { get; set; } = "/images/travel-hub/quiz-travel-box.webp";
    public bool IsUploadingSoon { get; set; } = true;
}

public sealed class TravelShopByCategoriesDto
{
    public string Heading { get; set; } = "Everything You Need, All in One Place";
    public string SortLabel { get; set; } = "Sort By: Popular";
    public List<TravelCategoryCardDto> Cards { get; set; } = new();
}

public sealed class TravelCategoryCardDto
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string Slug { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public string TargetUrl { get; set; } = "";
    public bool IsUploadingSoon { get; set; } = true;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public string Icon { get; set; } = "";
}

public sealed class TravelBottomTrustBarDto
{
    public bool IsEnabled { get; set; } = true;
    public List<TravelTrustBadgeDto> Badges { get; set; } = new();
}

public sealed class TravelTrustBadgeDto
{
    public string Icon { get; set; } = "";
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
}

