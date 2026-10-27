namespace Dreamflykit.Application.AppliancesHub.Models;

public sealed class AppliancesHubDto
{
    public AppliancesThemeSettingsDto ThemeSettings { get; set; } = new();
    public AppliancesHeroBannerDto HeroBanner { get; set; } = new();
    public AppliancesBankOfferDto BankOffer { get; set; } = new();
    public AppliancesSubCategoriesDto SubCategories { get; set; } = new();
    public AppliancesBrandSpotlightDto BrandSpotlight { get; set; } = new();
    public AppliancesCreditPromoDto CreditPromos { get; set; } = new();
    public AppliancesLimitedDealsDto LimitedTimeDeals { get; set; } = new();
    public AppliancesGoldenHighlightsDto GoldenHighlights { get; set; } = new();
    public AppliancesTabbedDealsDto HomeAppliancesDeals { get; set; } = new();
    public AppliancesTabbedDealsDto KitchenAppliancesDeals { get; set; } = new();
    public AppliancesEmiSectionDto NoCostEmiSection { get; set; } = new();
    public AppliancesEverydayLivingDto EverydayLivingSection { get; set; } = new();

    public static AppliancesHubDto CreateDefault()
    {
        return new AppliancesHubDto
        {
            ThemeSettings = new AppliancesThemeSettingsDto
            {
                ThemeName = "Big Bang Diwali / Festival Dhamaka",
                ThemeTagline = "SALE IS LIVE • LOWEST PRICES GUARANTEED*",
                BadgeText = "⚡ FESTIVE DHAMAKA DEALS",
                PrimaryBgGradient = "linear-gradient(135deg, #7c1212 0%, #b91c1c 45%, #ea580c 100%)",
                AccentGold = "#fbbf24",
                IsFestiveEnabled = true,
                NoticeBarText = "🎆 Big Bang Festival is LIVE! Extra 10% instant bank discounts + No Cost EMI up to 24 months on all major appliances."
            },

            HeroBanner = new AppliancesHeroBannerDto
            {
                IsEnabled = true,
                AutoPlay = true,
                IntervalSeconds = 4,
                Slides = new List<AppliancesHeroSlideDto>
                {
                    new()
                    {
                        Id = "app-slide-1",
                        TagBadge = "LIMITED HOUR DEALS",
                        Title = "Price Drop on Appliances",
                        PriceHighlight = "Starting Under ₹5,990*",
                        Subtitle = "Up to 65% Off on Front-Load Washing Machines, Smart Microwaves & Geysers.",
                        ImageUrl = "/images/appliances/hero-slide-1.jpg",
                        TargetUrl = "/category/appliances?sub=washing-machines",
                        ButtonText = "Grab Deals →",
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Id = "app-slide-2",
                        TagBadge = "BIG BANG DIWALI",
                        Title = "65\" 4K QLED & OLED TVs",
                        PriceHighlight = "From ₹38,999*",
                        Subtitle = "The big league screen experience. Dolby Vision Atmos, 120Hz gaming & Google TV.",
                        ImageUrl = "/images/appliances/hero-slide-2.jpg",
                        TargetUrl = "/category/appliances?sub=television",
                        ButtonText = "Explore 4K TVs →",
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Id = "app-slide-3",
                        TagBadge = "SALE IS LIVE",
                        Title = "Mega Kitchen & Home Fest",
                        PriceHighlight = "Lowest Prices Guaranteed*",
                        Subtitle = "Convertible Multi-Door Fridges, Inverter ACs, Dishwashers with 100% Exchange Bonus.",
                        ImageUrl = "/images/appliances/hero-slide-3.jpg",
                        TargetUrl = "/category/kitchen-appliances",
                        ButtonText = "Shop Mega Sale →",
                        IsUploadingSoon = false
                    }
                }
            },

            BankOffer = new AppliancesBankOfferDto
            {
                IsEnabled = true,
                BankName = "SBI Card & SBI Debit Card",
                BankLogoUrl = "",
                OfferText = "10% Instant Discount*",
                OfferSubtext = "on Debit/Credit Cards & Credit Card EMI | Min spend ₹5,000",
                TermsText = "*T&C Apply. Valid across all Home & Kitchen Appliances."
            },

            SubCategories = new AppliancesSubCategoriesDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                Items = new List<AppliancesSubCategoryItemDto>
                {
                    new() { Title = "Television", Icon = "📺", Slug = "television", TargetUrl = "/category/appliances?sub=television" },
                    new() { Title = "Laundry", Icon = "🧺", Slug = "laundry", TargetUrl = "/category/appliances?sub=laundry" },
                    new() { Title = "ACs", Icon = "❄️", Slug = "air-conditioners", TargetUrl = "/category/appliances?sub=air-conditioners" },
                    new() { Title = "Fans", Icon = "🌀", Slug = "fans", TargetUrl = "/category/appliances?sub=fans" },
                    new() { Title = "Microwaves", Icon = "🍲", Slug = "microwaves", TargetUrl = "/category/appliances?sub=microwaves" },
                    new() { Title = "Value 365", Icon = "🏷️", Slug = "value-365", TargetUrl = "/category/appliances?sub=value-365" },
                    new() { Title = "Appliances", Icon = "🔌", Slug = "small-appliances", TargetUrl = "/category/appliances?sub=small-appliances" },
                    new() { Title = "Geysers", Icon = "🚿", Slug = "geysers", TargetUrl = "/category/appliances?sub=geysers" },
                    new() { Title = "Stabilizers", Icon = "⚡", Slug = "stabilizers", TargetUrl = "/category/appliances?sub=stabilizers" },
                    new() { Title = "Kitchen", Icon = "🍳", Slug = "kitchen-appliances", TargetUrl = "/category/kitchen-appliances" },
                    new() { Title = "Fridges", Icon = "🧊", Slug = "refrigerators", TargetUrl = "/category/appliances?sub=refrigerators" },
                    new() { Title = "Home", Icon = "🏠", Slug = "home-appliances", TargetUrl = "/category/appliances?sub=home-appliances" },
                    new() { Title = "Breakfast", Icon = "☕", Slug = "breakfast-appliances", TargetUrl = "/category/kitchen-appliances?sub=breakfast" },
                    new() { Title = "FK Originals", Icon = "⭐", Slug = "top-rated", TargetUrl = "/category/appliances?sub=top-rated" },
                    new() { Title = "Power Backup", Icon = "🔋", Slug = "inverters", TargetUrl = "/category/appliances?sub=inverters" },
                    new() { Title = "Dishwashers", Icon = "🍽️", Slug = "dishwashers", TargetUrl = "/category/appliances?sub=dishwashers" }
                }
            },

            BrandSpotlight = new AppliancesBrandSpotlightDto
            {
                IsEnabled = true,
                BrandName = "TCL",
                BrandTagline = "Master the Moment",
                ProductTitle = "85P8L Ai QLED TV",
                PriceHighlight = "Just ₹1,20,500*",
                Subtitle = "85-inch Massive Screen • 144Hz VRR • AiPQ Processor Pro • Onkyo 2.1 Sound",
                ImageUrl = "/images/appliances/spotlight-tcl.jpg",
                TargetUrl = "/category/appliances?sub=television",
                IsUploadingSoon = false
            },

            CreditPromos = new AppliancesCreditPromoDto
            {
                IsEnabled = true,
                Card1Title = "Dreamflykit Axis Bank Card",
                Card1Badge = "₹500 ZERO joining fee",
                Card1Sub = "5% Unlimited Cashback on All Appliance Orders",
                Card1Link = "#apply-card",
                Card2Title = "Instant Appliance EMI",
                Card2Badge = "Credit Up to ₹2L",
                Card2Sub = "+ No Cost EMI* on 3, 6, 9 & 12 Months",
                Card2Link = "#check-emi"
            },

            LimitedTimeDeals = new AppliancesLimitedDealsDto
            {
                IsEnabled = true,
                Title = "Limited time deal",
                Subtitle = "Grab unmissable price drops before the timer resets!",
                Slots = new List<AppliancesTimeSlotDealDto>
                {
                    new()
                    {
                        TimeSlot = "DEAL LIVE NOW",
                        ItemTitle = "Samsung 43\" 4K Smart TV",
                        PriceText = "Just ₹19,999*",
                        SubText = "Bezel-less Crystal 4K UHD",
                        ImageUrl = "/images/appliances/deal-samsung-tv.jpg",
                        IsLocked = false,
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        TimeSlot = "10 AM TO 11 AM",
                        ItemTitle = "Havells & more Geysers",
                        PriceText = "From ₹369",
                        SubText = "Storage & Instant Water Heaters",
                        ImageUrl = "/images/appliances/deal-geyser.jpg",
                        IsLocked = true,
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        TimeSlot = "1 PM TO 2 PM",
                        ItemTitle = "Mixers & Grinders Fest",
                        PriceText = "From ₹889",
                        SubText = "750W 3-Jar Heavy Duty Motor",
                        ImageUrl = "/images/appliances/deal-mixer.jpg",
                        IsLocked = true,
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        TimeSlot = "3 PM TO 4 PM",
                        ItemTitle = "Sandwich Grill & Toasters",
                        PriceText = "From ₹494",
                        SubText = "Non-stick Automatic Cut-off",
                        ImageUrl = "/images/appliances/deal-toaster.jpg",
                        IsLocked = true,
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        TimeSlot = "5 PM TO 6 PM",
                        ItemTitle = "Voltas & more Coolers",
                        PriceText = "From ₹431",
                        SubText = "Desert & Tower Coolers with Honeycomb",
                        ImageUrl = "/images/appliances/deal-cooler.jpg",
                        IsLocked = true,
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        TimeSlot = "8 PM TO 9 PM",
                        ItemTitle = "LG, IFB & more Washers",
                        PriceText = "From ₹6,929*",
                        SubText = "Smart Inverter & Steam Wash",
                        ImageUrl = "/images/appliances/deal-washer.jpg",
                        IsLocked = true,
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        TimeSlot = "7 PM TO 8 PM",
                        ItemTitle = "Godrej & Whirlpool Fridges",
                        PriceText = "Extra ₹1,000 Off",
                        SubText = "Double Door Frost-Free",
                        ImageUrl = "/images/appliances/deal-fridge.jpg",
                        IsLocked = true,
                        IsUploadingSoon = false
                    }
                }
            },

            GoldenHighlights = new AppliancesGoldenHighlightsDto
            {
                IsEnabled = true,
                Title = "Big Bang Blockbuster Deals",
                Subtitle = "Handpicked top-rated appliances with maximum festive savings & 5-star ratings.",
                Items = new List<AppliancesGoldenItemDto>
                {
                    new()
                    {
                        Title = "Samsung 43\" 4K Crystal UHD",
                        PriceText = "Just ₹24,499*",
                        OriginalPrice = "₹39,990",
                        DiscountBadge = "38% OFF",
                        Subtitle = "HDR 10+ • PurColor • Voice Assistant",
                        ImageUrl = "/images/appliances/gold-samsung-tv.jpg",
                        TargetUrl = "/category/appliances?sub=television",
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Title = "Samsung 9kg Bespoke AI Laundry",
                        PriceText = "From ₹33,240*",
                        OriginalPrice = "₹52,990",
                        DiscountBadge = "37% OFF",
                        Subtitle = "AI Ecobubble™ • 1400 RPM • WiFi SmartThings",
                        ImageUrl = "/images/appliances/gold-samsung-washer.jpg",
                        TargetUrl = "/category/appliances?sub=laundry",
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Title = "Samsung 653L Convertible 5-in-1",
                        PriceText = "Just ₹61,990*",
                        OriginalPrice = "₹99,990",
                        DiscountBadge = "38% OFF",
                        Subtitle = "Side-by-Side Fridge • Smart Inverter AI",
                        ImageUrl = "/images/appliances/gold-samsung-fridge.jpg",
                        TargetUrl = "/category/appliances?sub=refrigerators",
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Title = "Exide Inverter 220 Ah Battery",
                        PriceText = "Just ₹19,990",
                        OriginalPrice = "₹28,500",
                        DiscountBadge = "30% OFF",
                        Subtitle = "Tubular InvaMaster • 36 Months Warranty",
                        ImageUrl = "/images/appliances/gold-inverter.jpg",
                        TargetUrl = "/category/appliances?sub=inverters",
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Title = "Ecovacs N30 Robot Vacuum & Mop",
                        PriceText = "Just ₹20,999*",
                        OriginalPrice = "₹35,999",
                        DiscountBadge = "42% OFF",
                        Subtitle = "LiDAR Navigation • 5000Pa Strong Suction",
                        ImageUrl = "/images/appliances/gold-ecovacs.jpg",
                        TargetUrl = "/category/appliances?sub=vacuum",
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Title = "Voltas 1.42 Ton 5-Star Split AC",
                        PriceText = "Just ₹30,490*",
                        OriginalPrice = "₹58,990",
                        DiscountBadge = "48% OFF",
                        Subtitle = "Adjustable Inverter • 100% Copper Condenser",
                        ImageUrl = "/images/appliances/gold-voltas-ac.jpg",
                        TargetUrl = "/category/appliances?sub=air-conditioners",
                        IsUploadingSoon = false
                    }
                }
            },

            HomeAppliancesDeals = new AppliancesTabbedDealsDto
            {
                IsEnabled = true,
                SectionHeader = "Deals on Home Appliances",
                Tabs = new List<string> { "Best picks", "Television", "Washing machine", "Water purifier & more" },
                Items = new List<AppliancesDealItemDto>
                {
                    new() { TabCategory = "Best picks", Title = "TCL 55\" QLED 4K TV", PriceText = "Just ₹31,499*", Subtitle = "120Hz DLG • Dolby Vision", ImageUrl = "/images/appliances/home-tcl-tv.jpg", TargetUrl = "/category/appliances?sub=television", IsUploadingSoon = false },
                    new() { TabCategory = "Best picks", Title = "Washing Machine 7.5kg", PriceText = "From ₹7,490*", Subtitle = "Semi-Automatic Magic Filter", ImageUrl = "/images/appliances/home-washer.jpg", TargetUrl = "/category/appliances?sub=laundry", IsUploadingSoon = false },
                    new() { TabCategory = "Best picks", Title = "M3 Pro Smart RO Purifier", PriceText = "Just ₹24,999", Subtitle = "3 Year Filter Life • TDS Controller", ImageUrl = "/images/appliances/home-purifier.jpg", TargetUrl = "/category/appliances?sub=water-purifier", IsUploadingSoon = false },
                    new() { TabCategory = "Best picks", Title = "Bajaj 15L Storage Geyser", PriceText = "Just ₹6,499", Subtitle = "Titanium Armour • 8 Bar Pressure", ImageUrl = "/images/appliances/home-geyser.jpg", TargetUrl = "/category/appliances?sub=geysers", IsUploadingSoon = false },
                    new() { TabCategory = "Best picks", Title = "Livguard Inverter Battery", PriceText = "Just ₹11,499", Subtitle = "Fast Charging Super Tuff", ImageUrl = "/images/appliances/home-inverter.jpg", TargetUrl = "/category/appliances?sub=inverters", IsUploadingSoon = false }
                }
            },

            KitchenAppliancesDeals = new AppliancesTabbedDealsDto
            {
                IsEnabled = true,
                SectionHeader = "Deals on Kitchen Appliances",
                Tabs = new List<string> { "Best picks", "Refrigerator", "Mixer & more", "Chimney" },
                Items = new List<AppliancesDealItemDto>
                {
                    new() { TabCategory = "Best picks", Title = "12L Digital Oven Air Fryer", PriceText = "Just ₹7,999", Subtitle = "360° Rapid Air Rotisserie", ImageUrl = "/images/appliances/kitchen-airfryer.jpg", TargetUrl = "/category/kitchen-appliances?sub=air-fryer", IsUploadingSoon = false },
                    new() { TabCategory = "Best picks", Title = "BLDC Motion Sensor Chimney", PriceText = "Just ₹12,799", Subtitle = "India's 1st Plug 'N' Play Filterless", ImageUrl = "/images/appliances/kitchen-chimney.jpg", TargetUrl = "/category/kitchen-appliances?sub=chimney", IsUploadingSoon = false },
                    new() { TabCategory = "Best picks", Title = "750W Powerful Mixer Grinder", PriceText = "Just ₹1,699", Subtitle = "3 Stainless Steel Heavy Jars", ImageUrl = "/images/appliances/kitchen-mixer.jpg", TargetUrl = "/category/kitchen-appliances?sub=mixer", IsUploadingSoon = false },
                    new() { TabCategory = "Best picks", Title = "Bosch 14 Place Dishwasher", PriceText = "From ₹13,749*", Subtitle = "Indian Kitchen Intensive Kadhai Clean", ImageUrl = "/images/appliances/kitchen-dishwasher.jpg", TargetUrl = "/category/kitchen-appliances?sub=dishwashers", IsUploadingSoon = false }
                }
            },

            NoCostEmiSection = new AppliancesEmiSectionDto
            {
                IsEnabled = true,
                Title = "No cost EMI",
                Subtitle = "Zero down payment and zero interest installments on verified bank cards.",
                Items = new List<AppliancesEmiItemDto>
                {
                    new() { Title = "55\" 4K TVs", EmiText = "From ₹4,726/m*", Subtitle = "Sony, Samsung, LG", ImageUrl = "/images/appliances/emi-tv.jpg", TargetUrl = "/category/appliances?sub=television", IsUploadingSoon = false },
                    new() { Title = "Value Washers", EmiText = "Starting ₹699*/m", Subtitle = "Top Load & Front Load", ImageUrl = "/images/appliances/emi-washer.jpg", TargetUrl = "/category/appliances?sub=laundry", IsUploadingSoon = false },
                    new() { Title = "Fridges & Double Door", EmiText = "Under ₹3,999/m*", Subtitle = "Whirlpool, Samsung", ImageUrl = "/images/appliances/emi-fridge.jpg", TargetUrl = "/category/appliances?sub=refrigerators", IsUploadingSoon = false },
                    new() { Title = "Mixer Grinders", EmiText = "From ₹1,077/m", Subtitle = "Philips, Preethi, Sujata", ImageUrl = "/images/appliances/emi-mixer.jpg", TargetUrl = "/category/kitchen-appliances?sub=mixer", IsUploadingSoon = false },
                    new() { Title = "2 Ton Inverter ACs", EmiText = "From ₹2,989/m*", Subtitle = "Daikin, Voltas, Lloyd", ImageUrl = "/images/appliances/emi-ac.jpg", TargetUrl = "/category/appliances?sub=air-conditioners", IsUploadingSoon = false },
                    new() { Title = "8L Smart Purifiers", EmiText = "From ₹1,750/m*", Subtitle = "Kent, Aquaguard", ImageUrl = "/images/appliances/emi-purifier.jpg", TargetUrl = "/category/appliances?sub=water-purifier", IsUploadingSoon = false },
                    new() { Title = "Smart BLDC Fans", EmiText = "From ₹599/m", Subtitle = "Atomberg, Crompton", ImageUrl = "/images/appliances/emi-fan.jpg", TargetUrl = "/category/appliances?sub=fans", IsUploadingSoon = false }
                }
            },

            EverydayLivingSection = new AppliancesEverydayLivingDto
            {
                IsEnabled = true,
                Title = "For everyday living!",
                Subtitle = "Smart small appliances for effortless morning routine and quick cooking.",
                Items = new List<AppliancesEverydayItemDto>
                {
                    new() { Title = "Electric Kettle 1.8L", Subtitle = "Instant Auto Cut-off", PriceText = "Just ₹649", ImageUrl = "/images/appliances/everyday-kettle.jpg", TargetUrl = "/category/kitchen-appliances?sub=kettle", IsUploadingSoon = false },
                    new() { Title = "Non-Stick Steam Iron", Subtitle = "Super Crease Removal", PriceText = "Just ₹599", ImageUrl = "/images/appliances/everyday-iron.jpg", TargetUrl = "/category/appliances?sub=iron", IsUploadingSoon = false },
                    new() { Title = "Sandwich & Panini Maker", Subtitle = "Jumbo Non-Stick Grill", PriceText = "Just ₹849", ImageUrl = "/images/appliances/everyday-grill.jpg", TargetUrl = "/category/kitchen-appliances?sub=grill", IsUploadingSoon = false },
                    new() { Title = "Electric Vegetable Chopper", Subtitle = "Stainless Steel 4-Blade", PriceText = "Just ₹499", ImageUrl = "/images/appliances/everyday-chopper.jpg", TargetUrl = "/category/kitchen-appliances?sub=chopper", IsUploadingSoon = false },
                    new() { Title = "Compact 3-Jar Mixer Set", Subtitle = "Multi-Speed Blender", PriceText = "Just ₹1,299", ImageUrl = "/images/appliances/everyday-mixer.jpg", TargetUrl = "/category/kitchen-appliances?sub=mixer", IsUploadingSoon = false }
                }
            }
        };
    }
}

public sealed class AppliancesThemeSettingsDto
{
    public string ThemeName { get; set; } = "Big Bang Diwali / Festival Dhamaka";
    public string ThemeTagline { get; set; } = "SALE IS LIVE • LOWEST PRICES GUARANTEED*";
    public string BadgeText { get; set; } = "⚡ FESTIVE DHAMAKA DEALS";
    public string PrimaryBgGradient { get; set; } = "linear-gradient(135deg, #7c1212 0%, #b91c1c 45%, #ea580c 100%)";
    public string AccentGold { get; set; } = "#fbbf24";
    public bool IsFestiveEnabled { get; set; } = true;
    public string NoticeBarText { get; set; } = "🎆 Big Bang Festival is LIVE! Extra 10% instant bank discounts + No Cost EMI up to 24 months.";
}

public sealed class AppliancesHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public bool AutoPlay { get; set; } = true;
    public int IntervalSeconds { get; set; } = 4;
    public List<AppliancesHeroSlideDto> Slides { get; set; } = new();
}

public sealed class AppliancesHeroSlideDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string TagBadge { get; set; } = "LIMITED HOUR DEALS";
    public string Title { get; set; } = "Price Drop on Appliances";
    public string PriceHighlight { get; set; } = "Starting Under ₹5,990*";
    public string Subtitle { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public string TargetUrl { get; set; } = "/category/appliances";
    public string ButtonText { get; set; } = "Shop Now →";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class AppliancesBankOfferDto
{
    public bool IsEnabled { get; set; } = true;
    public string BankName { get; set; } = "SBI Card & SBI Debit Card";
    public string BankLogoUrl { get; set; } = "";
    public string OfferText { get; set; } = "10% Instant Discount*";
    public string OfferSubtext { get; set; } = "on Debit/Credit Cards & Credit Card EMI";
    public string TermsText { get; set; } = "*T&C Apply. Applicable on selected transactions.";
}

public sealed class AppliancesSubCategoriesDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Shop by Category";
    public List<AppliancesSubCategoryItemDto> Items { get; set; } = new();
}

public sealed class AppliancesSubCategoryItemDto
{
    public string Title { get; set; } = "";
    public string Icon { get; set; } = "🔌";
    public string IconImageUrl { get; set; } = "";
    public string ImageUrl { get => IconImageUrl; set => IconImageUrl = value; }
    public string Badge { get; set; } = "";
    public string Slug { get; set; } = "";
    public string TargetUrl { get; set; } = "/category/appliances";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class AppliancesBrandSpotlightDto
{
    public bool IsEnabled { get; set; } = true;
    public string BrandName { get; set; } = "TCL";
    public string BrandTagline { get; set; } = "Master the Moment";
    public string ProductTitle { get; set; } = "85P8L Ai QLED TV";
    public string PriceHighlight { get; set; } = "Just ₹1,20,500*";
    public string Subtitle { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public string TargetUrl { get; set; } = "/category/appliances";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class AppliancesCreditPromoDto
{
    public bool IsEnabled { get; set; } = true;
    public string Card1Title { get; set; } = "Flipkart Axis Bank Credit Card";
    public string Card1Badge { get; set; } = "₹500 ZERO joining fee";
    public string Card1Sub { get; set; } = "Unlimited 5% Cashback on Store";
    public string Card1Link { get; set; } = "#apply-card";
    public string Card2Title = "Flipkart EMI";
    public string Card2Badge { get; set; } = "Credit Up to ₹2L";
    public string Card2Sub { get; set; } = "+ No Cost EMI* Check Limit >";
    public string Card2Link { get; set; } = "#check-emi";
}

public sealed class AppliancesLimitedDealsDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Limited time deal";
    public string Subtitle { get; set; } = "Lightning deals with time slots";
    public List<AppliancesTimeSlotDealDto> Slots { get; set; } = new();
}

public sealed class AppliancesTimeSlotDealDto
{
    public string TimeSlot { get; set; } = "10 AM TO 11 AM";
    public string ItemTitle { get; set; } = "";
    public string PriceText { get; set; } = "From ₹369";
    public string SubText { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public bool IsLocked { get; set; } = true;
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class AppliancesGoldenHighlightsDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Big Bang Deals";
    public string Subtitle { get; set; } = "";
    public List<AppliancesGoldenItemDto> Items { get; set; } = new();
}

public sealed class AppliancesGoldenItemDto
{
    public string Title { get; set; } = "";
    public string PriceText { get; set; } = "Just ₹24,499*";
    public string OriginalPrice { get; set; } = "";
    public string DiscountBadge { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public string TargetUrl { get; set; } = "/category/appliances";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class AppliancesTabbedDealsDto
{
    public bool IsEnabled { get; set; } = true;
    public string SectionHeader { get; set; } = "Deals on Appliances";
    public List<string> Tabs { get; set; } = new();
    public List<AppliancesDealItemDto> Items { get; set; } = new();
}

public sealed class AppliancesDealItemDto
{
    public string TabCategory { get; set; } = "Best picks";
    public string Title { get; set; } = "";
    public string PriceText { get; set; } = "Just ₹31,499*";
    public string Subtitle { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public string TargetUrl { get; set; } = "/category/appliances";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class AppliancesEmiSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "No cost EMI";
    public string Subtitle { get; set; } = "";
    public List<AppliancesEmiItemDto> Items { get; set; } = new();
}

public sealed class AppliancesEmiItemDto
{
    public string Title { get; set; } = "";
    public string EmiText { get; set; } = "From ₹4,726/m*";
    public string Subtitle { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public string TargetUrl { get; set; } = "/category/appliances";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class AppliancesEverydayLivingDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "For everyday living!";
    public string Subtitle { get; set; } = "";
    public List<AppliancesEverydayItemDto> Items { get; set; } = new();
}

public sealed class AppliancesEverydayItemDto
{
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string PriceText { get; set; } = "Just ₹649";
    public string ImageUrl { get; set; } = "";
    public string TargetUrl { get; set; } = "/category/kitchen-appliances";
    public bool IsUploadingSoon { get; set; } = false;
}
