namespace Dreamflykit.Application.LuggageHub.Models;

public sealed class LuggageHubDto
{
    public LuggageFestivalThemeDto FestivalTheme { get; set; } = new();
    public LuggageHeroBannerDto HeroBanner { get; set; } = new();
    public LuggageReassuranceBarDto ReassuranceBar { get; set; } = new();
    public LuggageShopByCategoryDto ShopByCategory { get; set; } = new();
    public LuggagePromoBannersDto PromoBanners { get; set; } = new();

    public static LuggageHubDto CreateDefault()
    {
        return new LuggageHubDto
        {
            FestivalTheme = new LuggageFestivalThemeDto
            {
                IsFestivalActive = true,
                FestivalName = "Flight Season Special",
                FestivalTag = "FESTIVAL THEME",
                FestivalBannerText = "✈️ International Student Travel Sale • Extra 15% Off with Code FLYSMART",
                ThemePreset = "warm-sunset",
                PrimaryNavy = "#0f2b48",
                AccentOrange = "#f97316",
                HeroBackground = "linear-gradient(135deg, #fdf4ec 0%, #fae8d8 35%, #f1f5f9 70%, #e2e8f0 100%)",
                CardBorderRadius = "14px"
            },

            HeroBanner = new LuggageHeroBannerDto
            {
                IsEnabled = true,
                HeadingLine1 = "Pack your dreams.",
                HeadingLine2 = "Carry your tomorrow.",
                SubtitleLine1 = "Every journey begins with the right luggage.",
                SubtitleLine2 = "Built to go the distance. Made to move with you.",
                ButtonText = "Explore Collection →",
                ButtonUrl = "#luggage-products-section",
                ImageUrl = "/images/luggage-hub/hero-luggage-collection.webp",
                IsUploadingSoon = true,
                Badges = new List<LuggageBadgeDto>
                {
                    new()
                    {
                        Id = "badge-strong",
                        Icon = "shield",
                        Title = "Strong Outside",
                        Subtitle = "Built to last"
                    },
                    new()
                    {
                        Id = "badge-light",
                        Icon = "feather",
                        Title = "Light Inside",
                        Subtitle = "Travel with ease"
                    },
                    new()
                    {
                        Id = "badge-safe",
                        Icon = "lock",
                        Title = "Safe All The Way",
                        Subtitle = "Secure TSA Locks"
                    }
                },
                OfferBox = new LuggageOfferBoxDto
                {
                    IsEnabled = true,
                    TopTag = "TRAVEL FAR. TRAVEL SMART.",
                    DiscountTitle = "UP TO 15% OFF",
                    DiscountSubtitle = "On All Luggage",
                    ButtonText = "Shop Now →",
                    ButtonUrl = "#luggage-products-section"
                }
            },

            ReassuranceBar = new LuggageReassuranceBarDto
            {
                IsEnabled = true,
                Items = new List<LuggageReassuranceItemDto>
                {
                    new()
                    {
                        Icon = "truck",
                        Title = "Free Delivery",
                        Subtitle = "On orders above ₹999"
                    },
                    new()
                    {
                        Icon = "rotate-ccw",
                        Title = "Easy Returns",
                        Subtitle = "7 day hassle-free returns"
                    },
                    new()
                    {
                        Icon = "credit-card",
                        Title = "Secure Payments",
                        Subtitle = "100% secure checkout"
                    },
                    new()
                    {
                        Icon = "headphones",
                        Title = "24/7 Support",
                        Subtitle = "We're here to help"
                    },
                    new()
                    {
                        Icon = "award",
                        Title = "Best Price Guarantee",
                        Subtitle = "Get the best deals"
                    }
                }
            },

            ShopByCategory = new LuggageShopByCategoryDto
            {
                Heading = "Shop by Category",
                Subtitle = "Find the perfect luggage for your next adventure",
                Cards = new List<LuggageCategoryCardDto>
                {
                    new()
                    {
                        Id = "cat-suitcases",
                        Title = "Suitcases",
                        Subtitle = "Hard & Soft Case",
                        Slug = "suitcases",
                        ImageUrl = "/images/luggage-hub/cat-suitcases.webp",
                        TargetUrl = "/category/travel-gear?subcategory=luggage-suitcases&type=suitcases",
                        IsUploadingSoon = true,
                        DisplayOrder = 1,
                        IsActive = true,
                        Icon = "luggage"
                    },
                    new()
                    {
                        Id = "cat-backpacks",
                        Title = "Backpacks",
                        Subtitle = "For Travel & Daily Use",
                        Slug = "backpacks",
                        ImageUrl = "/images/luggage-hub/cat-backpacks.webp",
                        TargetUrl = "/category/travel-gear?subcategory=packing-organizers&type=backpacks",
                        IsUploadingSoon = true,
                        DisplayOrder = 2,
                        IsActive = true,
                        Icon = "backpack"
                    },
                    new()
                    {
                        Id = "cat-duffel",
                        Title = "Duffel Bags",
                        Subtitle = "Spacious & Stylish",
                        Slug = "duffel-bags",
                        ImageUrl = "/images/luggage-hub/cat-duffel.webp",
                        TargetUrl = "/category/travel-gear?subcategory=luggage-suitcases&type=duffels",
                        IsUploadingSoon = true,
                        DisplayOrder = 3,
                        IsActive = true,
                        Icon = "briefcase"
                    },
                    new()
                    {
                        Id = "cat-cabin",
                        Title = "Cabin Luggage",
                        Subtitle = "Travel Light",
                        Slug = "cabin-luggage",
                        ImageUrl = "/images/luggage-hub/cat-cabin.webp",
                        TargetUrl = "/category/travel-gear?subcategory=luggage-suitcases&type=cabin",
                        IsUploadingSoon = true,
                        DisplayOrder = 4,
                        IsActive = true,
                        Icon = "compass"
                    },
                    new()
                    {
                        Id = "cat-sets",
                        Title = "Luggage Sets",
                        Subtitle = "All-in-One Sets",
                        Slug = "luggage-sets",
                        ImageUrl = "/images/luggage-hub/cat-sets.webp",
                        TargetUrl = "/category/travel-gear?subcategory=luggage-suitcases&type=sets",
                        IsUploadingSoon = true,
                        DisplayOrder = 5,
                        IsActive = true,
                        Icon = "layers"
                    },
                    new()
                    {
                        Id = "cat-laptop-bags",
                        Title = "Laptop Bags",
                        Subtitle = "Work & Travel",
                        Slug = "laptop-bags",
                        ImageUrl = "/images/luggage-hub/cat-laptop-bags.webp",
                        TargetUrl = "/category/travel-gear?subcategory=tech-accessories&type=laptop-bags",
                        IsUploadingSoon = true,
                        DisplayOrder = 6,
                        IsActive = true,
                        Icon = "folder"
                    },
                    new()
                    {
                        Id = "cat-kids",
                        Title = "Kids Luggage",
                        Subtitle = "Fun & Durable",
                        Slug = "kids-luggage",
                        ImageUrl = "/images/luggage-hub/cat-kids.webp",
                        TargetUrl = "/category/travel-gear?subcategory=luggage-suitcases&type=kids",
                        IsUploadingSoon = true,
                        DisplayOrder = 7,
                        IsActive = true,
                        Icon = "smile"
                    },
                    new()
                    {
                        Id = "cat-accessories",
                        Title = "Travel Accessories",
                        Subtitle = "Must-Have Add-ons",
                        Slug = "travel-accessories",
                        ImageUrl = "/images/luggage-hub/cat-accessories.webp",
                        TargetUrl = "/category/travel-gear?subcategory=comfort-essentials",
                        IsUploadingSoon = true,
                        DisplayOrder = 8,
                        IsActive = true,
                        Icon = "tag"
                    }
                }
            },

            PromoBanners = new LuggagePromoBannersDto
            {
                IsEnabled = true,
                Cards = new List<LuggagePromoCardDto>
                {
                    new()
                    {
                        Id = "promo-new-arrivals",
                        Title = "New Arrivals",
                        Subtitle = "Explore the latest in luggage",
                        ButtonText = "Shop Now →",
                        ButtonUrl = "/category/travel-gear?subcategory=luggage-suitcases&filter=new",
                        ImageUrl = "/images/luggage-hub/promo-new-arrivals.webp",
                        Gradient = "linear-gradient(135deg, #e0f2fe 0%, #bae6fd 100%)",
                        AccentColor = "#0284c7",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "promo-top-brands",
                        Title = "Top Brands",
                        Subtitle = "Trusted by thousands of travelers",
                        ButtonText = "Explore Brands →",
                        ButtonUrl = "/category/travel-gear?subcategory=luggage-suitcases&filter=brands",
                        ImageUrl = "/images/luggage-hub/promo-top-brands.webp",
                        Gradient = "linear-gradient(135deg, #fef3c7 0%, #fde68a 100%)",
                        AccentColor = "#d97706",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "promo-student-special",
                        Title = "Student Special",
                        Subtitle = "Extra discounts for students",
                        ButtonText = "Verify Now →",
                        ButtonUrl = "/SpecialOffer",
                        ImageUrl = "/images/luggage-hub/promo-student-special.webp",
                        Gradient = "linear-gradient(135deg, #fae8ff 0%, #f5d0fe 100%)",
                        AccentColor = "#a855f7",
                        IsUploadingSoon = true
                    }
                }
            }
        };
    }
}

public sealed class LuggageFestivalThemeDto
{
    public bool IsFestivalActive { get; set; } = true;
    public string FestivalName { get; set; } = "Flight Season Special";
    public string FestivalTag { get; set; } = "FESTIVAL THEME";
    public string FestivalBannerText { get; set; } = "✈️ International Student Travel Sale • Extra 15% Off with Code FLYSMART";
    public string ThemePreset { get; set; } = "warm-sunset";
    public string PrimaryNavy { get; set; } = "#0f2b48";
    public string AccentOrange { get; set; } = "#f97316";
    public string HeroBackground { get; set; } = "linear-gradient(135deg, #fdf4ec 0%, #fae8d8 35%, #f1f5f9 70%, #e2e8f0 100%)";
    public string CardBorderRadius { get; set; } = "14px";
}

public sealed class LuggageHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string HeadingLine1 { get; set; } = "Pack your dreams.";
    public string HeadingLine2 { get; set; } = "Carry your tomorrow.";
    public string SubtitleLine1 { get; set; } = "Every journey begins with the right luggage.";
    public string SubtitleLine2 { get; set; } = "Built to go the distance. Made to move with you.";
    public string ButtonText { get; set; } = "Explore Collection →";
    public string ButtonUrl { get; set; } = "#luggage-products-section";
    public string ImageUrl { get; set; } = "/images/luggage-hub/hero-luggage-collection.webp";
    public bool IsUploadingSoon { get; set; } = true;
    public List<LuggageBadgeDto> Badges { get; set; } = new();
    public LuggageOfferBoxDto OfferBox { get; set; } = new();
}

public sealed class LuggageBadgeDto
{
    public string Id { get; set; } = "";
    public string Icon { get; set; } = "";
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
}

public sealed class LuggageOfferBoxDto
{
    public bool IsEnabled { get; set; } = true;
    public string TopTag { get; set; } = "TRAVEL FAR. TRAVEL SMART.";
    public string DiscountTitle { get; set; } = "UP TO 15% OFF";
    public string DiscountSubtitle { get; set; } = "On All Luggage";
    public string ButtonText { get; set; } = "Shop Now →";
    public string ButtonUrl { get; set; } = "#luggage-products-section";
}

public sealed class LuggageReassuranceBarDto
{
    public bool IsEnabled { get; set; } = true;
    public List<LuggageReassuranceItemDto> Items { get; set; } = new();
}

public sealed class LuggageReassuranceItemDto
{
    public string Icon { get; set; } = "";
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
}

public sealed class LuggageShopByCategoryDto
{
    public string Heading { get; set; } = "Shop by Category";
    public string Subtitle { get; set; } = "Find the perfect luggage for your next adventure";
    public List<LuggageCategoryCardDto> Cards { get; set; } = new();
}

public sealed class LuggageCategoryCardDto
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

public sealed class LuggagePromoBannersDto
{
    public bool IsEnabled { get; set; } = true;
    public List<LuggagePromoCardDto> Cards { get; set; } = new();
}

public sealed class LuggagePromoCardDto
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string ButtonText { get; set; } = "";
    public string ButtonUrl { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public string Gradient { get; set; } = "";
    public string AccentColor { get; set; } = "";
    public bool IsUploadingSoon { get; set; } = true;
}

