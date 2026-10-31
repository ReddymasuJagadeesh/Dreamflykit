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
    public List<AppliancesSubcategoryPageDto> SubcategoryPages { get; set; } = new();

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
                        TargetUrl = "/category/appliances/television",
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
                        TargetUrl = "/category/appliances/kitchen-appliances",
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
                    new() { Title = "Laundry", Icon = "🧺", Slug = "laundry", TargetUrl = "/category/appliances/laundry" },
                    new() { Title = "ACs", Icon = "❄️", Slug = "air-conditioners", TargetUrl = "/category/appliances/air-conditioners" },
                    new() { Title = "Fans", Icon = "🌀", Slug = "fans", TargetUrl = "/category/appliances/fans" },
                    new() { Title = "Microwaves", Icon = "🍲", Slug = "microwaves", TargetUrl = "/category/appliances/microwaves" },
                    new() { Title = "Value 365", Icon = "🏷️", Slug = "value-365", TargetUrl = "/category/appliances/value-365" },
                    new() { Title = "Appliances", Icon = "🔌", Slug = "small-appliances", TargetUrl = "/category/appliances/small-appliances" },
                    new() { Title = "Geysers", Icon = "🚿", Slug = "geysers", TargetUrl = "/category/appliances/geysers" },
                    new() { Title = "Stabilizers", Icon = "⚡", Slug = "stabilizers", TargetUrl = "/category/appliances/stabilizers" },
                    new() { Title = "Kitchen", Icon = "🍳", Slug = "kitchen-appliances", TargetUrl = "/category/kitchen-appliances" },
                    new() { Title = "Fridges", Icon = "🧊", Slug = "refrigerators", TargetUrl = "/category/appliances/refrigerators" },
                    new() { Title = "Home", Icon = "🏠", Slug = "home-appliances", TargetUrl = "/category/appliances/home-appliances" },
                    new() { Title = "Breakfast", Icon = "☕", Slug = "breakfast-appliances", TargetUrl = "/category/appliances/breakfast-appliances" },
                    new() { Title = "FK Originals", Icon = "⭐", Slug = "top-rated", TargetUrl = "/category/appliances/top-rated" },
                    new() { Title = "Power Backup", Icon = "🔋", Slug = "inverters", TargetUrl = "/category/appliances/inverters" },
                    new() { Title = "Dishwashers", Icon = "🍽️", Slug = "dishwashers", TargetUrl = "/category/appliances/dishwashers" }
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

            SubcategoryPages = CreateDefaultSubcategoryPages(),
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

    public static List<AppliancesSubcategoryPageDto> CreateDefaultSubcategoryPages()
    {
        return new List<AppliancesSubcategoryPageDto>
        {
            // 1. Television
            new()
            {
                Id = "app_sub_tv",
                CategorySlug = "television",
                CategoryName = "Television",
                Emoji = "📺",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "4K Ultra HD & Smart TVs for",
                    TitleHighlight = "Campus Entertainment & Cinema",
                    Subtitle = "Dolby Vision Atmos, Google TV, 120Hz refresh rate, 110V-240V dual voltage tested with student exchange bonuses.",
                    TagBadge = "⚡ 4K QLED & OLED ENTERTAINMENT • 100% GENUINE",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "4K Ultra HD & Dolby Atmos", "Google TV & AirPlay 2", "110V–240V Dual Voltage", "Up to 3-Year Warranty" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Top certified TV manufacturers with genuine panels and official on-site warranties.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_tv_1", Name = "Samsung", Slug = "samsung", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_tv_2", Name = "LG", Slug = "lg", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_tv_3", Name = "Sony", Slug = "sony", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_tv_4", Name = "TCL", Slug = "tcl", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_tv_5", Name = "Xiaomi", Slug = "xiaomi", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_tv_6", Name = "Hisense", Slug = "hisense", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_tv_7", Name = "OnePlus", Slug = "oneplus", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_tv_8", Name = "Panasonic", Slug = "panasonic", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_tv_1", Name = "Samsung 43\" Crystal 4K Vivid Pro Smart TV", Slug = "samsung-43-crystal-4k", Brand = "Samsung", Price = 28990, OriginalPrice = 44900, DiscountBadge = "35% OFF", Rating = 4.8, RatingCount = 420, KeySpecs = "4K Ultra HD • PurColor • HDR 10+ • Q-Symphony Sound" },
                    new() { Id = "p_tv_2", Name = "LG 55\" 4K Nanocell AI ThinQ Smart WebOS TV", Slug = "lg-55-nanocell-4k", Brand = "LG", Price = 42990, OriginalPrice = 79990, DiscountBadge = "46% OFF", Rating = 4.7, RatingCount = 310, KeySpecs = "Nanocell Color • Alpha 5 Gen6 AI • Apple AirPlay • Magic Remote" },
                    new() { Id = "p_tv_3", Name = "Sony Bravia 50\" Google TV with Dolby Audio", Slug = "sony-bravia-50-google-tv", Brand = "Sony", Price = 54990, OriginalPrice = 85900, DiscountBadge = "36% OFF", Rating = 4.9, RatingCount = 680, KeySpecs = "X1 4K Processor • Google TV • Motionflow XR • Open Baffle Speaker" }
                }
            },

            // 2. Laundry
            new()
            {
                Id = "app_sub_laundry",
                CategorySlug = "laundry",
                CategoryName = "Laundry",
                Emoji = "🧺",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Smart Inverter Washing Machines",
                    TitleHighlight = "Gentle Fabric Care for University Life",
                    Subtitle = "Front load & top load energy-efficient washers with hygiene steam, quick 15-min wash cycles, and silent inverter motors.",
                    TagBadge = "⚡ 5-STAR ENERGY RATED • SILENT INVERTER",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "Hygiene Steam Allergy Care", "AI Direct Drive Motor", "15-Min Quick Wash", "Free In-Home Installation" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Premium laundry appliances offering high efficiency and deep fabric protection.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_ldy_1", Name = "LG", Slug = "lg", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_ldy_2", Name = "Samsung", Slug = "samsung", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_ldy_3", Name = "Bosch", Slug = "bosch", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_ldy_4", Name = "Whirlpool", Slug = "whirlpool", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_ldy_5", Name = "IFB", Slug = "ifb", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_ldy_6", Name = "Godrej", Slug = "godrej", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_ldy_7", Name = "Haier", Slug = "haier", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_ldy_8", Name = "Panasonic", Slug = "panasonic", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_ldy_1", Name = "LG 8 Kg 5 Star AI Direct Drive Front Load Washer", Slug = "lg-8kg-front-load", Brand = "LG", Price = 34990, OriginalPrice = 49990, DiscountBadge = "30% OFF", Rating = 4.8, RatingCount = 540, KeySpecs = "Steam Allergy Care • TurboWash • 6 Motion DD • Wi-Fi ThinQ" },
                    new() { Id = "p_ldy_2", Name = "Bosch 7 Kg 5 Star Fully-Automatic Inverter Front Load", Slug = "bosch-7kg-front-load", Brand = "Bosch", Price = 31490, OriginalPrice = 43990, DiscountBadge = "28% OFF", Rating = 4.9, RatingCount = 380, KeySpecs = "Anti-Tangle • Anti-Vibration Design • 1200 RPM • SpeedPerfect" }
                }
            },

            // 3. ACs
            new()
            {
                Id = "app_sub_acs",
                CategorySlug = "air-conditioners",
                CategoryName = "ACs",
                Emoji = "❄️",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Energy Efficient Split & Inverter ACs",
                    TitleHighlight = "Rapid Turbo Cooling & Dust Protection",
                    Subtitle = "5-star AI convertible air conditioners with copper condenser, anti-bacterial PM 2.5 filters, and stabilizer-free operation.",
                    TagBadge = "⚡ AI CONVERTIBLE 6-IN-1 • COPPER CONDENSER",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "Rapid 10-Second Turbo Cooling", "PM 2.5 Anti-Bacterial Filter", "Stabilizer-Free 140V–290V", "10-Year Compressor Warranty" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Market leaders in Japanese, European and Indian heavy-duty cooling technologies.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_ac_1", Name = "Daikin", Slug = "daikin", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_ac_2", Name = "Voltas", Slug = "voltas", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_ac_3", Name = "Blue Star", Slug = "blue-star", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_ac_4", Name = "LG", Slug = "lg", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_ac_5", Name = "Panasonic", Slug = "panasonic", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_ac_6", Name = "Lloyd", Slug = "lloyd", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_ac_7", Name = "Carrier", Slug = "carrier", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_ac_8", Name = "Hitachi", Slug = "hitachi", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_ac_1", Name = "Daikin 1.5 Ton 5 Star Inverter Split AC (Copper)", Slug = "daikin-15ton-5star-ac", Brand = "Daikin", Price = 44990, OriginalPrice = 67200, DiscountBadge = "33% OFF", Rating = 4.8, RatingCount = 760, KeySpecs = "PM 2.5 Filter • Dew Clean Tech • 3D Airflow • Triple Display" },
                    new() { Id = "p_ac_2", Name = "Voltas 1.5 Ton 3 Star Inverter Split AC", Slug = "voltas-15ton-3star-ac", Brand = "Voltas", Price = 32990, OriginalPrice = 62990, DiscountBadge = "47% OFF", Rating = 4.6, RatingCount = 920, KeySpecs = "4-in-1 Adjustable Mode • Anti-Dust Filter • Dual Temp Display" }
                }
            },

            // 4. Fans
            new()
            {
                Id = "app_sub_fans",
                CategorySlug = "fans",
                CategoryName = "Fans",
                Emoji = "🌀",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "BLDC Energy-Saving Ceiling & Table Fans",
                    TitleHighlight = "Silent Whisper High Air Delivery",
                    Subtitle = "Brushless DC motor fans saving up to 65% electricity with RF remote control, timer modes, and aerodynamic aerofoil blades.",
                    TagBadge = "⚡ 28W BLDC LOW POWER • 5-STAR SAVINGS",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "65% Electricity Savings", "Smart Remote Control & Timer", "Silent High-Speed 380 RPM", "Rust-Proof Aluminum Blades" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Top energy-efficient BLDC and high-speed ceiling, pedestal, and exhaust fan brands.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_fan_1", Name = "Atomberg", Slug = "atomberg", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_fan_2", Name = "Havells", Slug = "havells", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_fan_3", Name = "Crompton", Slug = "crompton", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_fan_4", Name = "Orient Electric", Slug = "orient-electric", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_fan_5", Name = "Usha", Slug = "usha", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_fan_6", Name = "Bajaj", Slug = "bajaj", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_fan_7", Name = "Luminous", Slug = "luminous", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_fan_8", Name = "Polycab", Slug = "polycab", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_fan_1", Name = "Atomberg Renesa 1200mm BLDC Motor Ceiling Fan with Remote", Slug = "atomberg-renesa-1200mm", Brand = "Atomberg", Price = 3699, OriginalPrice = 5290, DiscountBadge = "30% OFF", Rating = 4.8, RatingCount = 1200, KeySpecs = "28W Power Consumption • RF Remote with Sleep Timer • LED Speed Indicators" }
                }
            },

            // 5. Microwaves
            new()
            {
                Id = "app_sub_micro",
                CategorySlug = "microwaves",
                CategoryName = "Microwaves",
                Emoji = "🍲",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Convection & Solo Smart Microwaves",
                    TitleHighlight = "Quick Healthy Cooking & Reheating",
                    Subtitle = "Auto-cook student recipes, oil-free air fry baking, express defrost presets, and anti-bacterial easy-clean ceramic enamel cavity.",
                    TagBadge = "⚡ OIL-FREE AIR FRY & BAKE • 300+ PRESETS",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "Convection Bake & Grill", "Auto-Cook Student Menu", "Ceramic Enamel Easy Clean", "Express 30s Reheat" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Versatile culinary microwave ovens crafted for instant cooking and effortless baking.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_mw_1", Name = "LG", Slug = "lg", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_mw_2", Name = "Samsung", Slug = "samsung", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_mw_3", Name = "IFB", Slug = "ifb", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_mw_4", Name = "Panasonic", Slug = "panasonic", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_mw_5", Name = "Morphy Richards", Slug = "morphy-richards", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_mw_6", Name = "Bajaj", Slug = "bajaj", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_mw_7", Name = "Haier", Slug = "haier", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_mw_8", Name = "Whirlpool", Slug = "whirlpool", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_mw_1", Name = "Samsung 28L Convection Microwave with Slim Fry", Slug = "samsung-28l-slim-fry", Brand = "Samsung", Price = 11990, OriginalPrice = 17990, DiscountBadge = "33% OFF", Rating = 4.7, RatingCount = 480, KeySpecs = "Slim Fry No Oil • Tandoor & Curd Making • Ceramic Cavity 10-Yr Warranty" }
                }
            },

            // 6. Value 365
            new()
            {
                Id = "app_sub_val365",
                CategorySlug = "value-365",
                CategoryName = "Value 365",
                Emoji = "🏷️",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Everyday Budget Appliances",
                    TitleHighlight = "Unbeatable Value 365 Days a Year",
                    Subtitle = "High durability, essential student dorm appliances, steam irons, sandwich makers, and kettles at best-in-class everyday prices.",
                    TagBadge = "⚡ LOWEST PRICE GUARANTEE • VALUE 365",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "Affordable Student Pricing", "Zero Compromise on Quality", "1-Year Doorstep Warranty", "Free Fast Shipping" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Verified high-value brands delivering reliable performance on student budgets.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_v365_1", Name = "Godrej", Slug = "godrej", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_v365_2", Name = "Voltas Beko", Slug = "voltas-beko", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_v365_3", Name = "Thomson", Slug = "thomson", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_v365_4", Name = "Sansui", Slug = "sansui", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_v365_5", Name = "Lifelong", Slug = "lifelong", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_v365_6", Name = "Prestige", Slug = "prestige", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_v365_7", Name = "Croma", Slug = "croma", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_v365_8", Name = "Candes", Slug = "candes", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_v365_1", Name = "Lifelong 1000W Dry Iron with Teflon Coating", Slug = "lifelong-1000w-iron", Brand = "Lifelong", Price = 499, OriginalPrice = 999, DiscountBadge = "50% OFF", Rating = 4.4, RatingCount = 180, KeySpecs = "Non-Stick Soleplate • 360 Swivel Cord • Thermal Fuse Protection" }
                }
            },

            // 7. Appliances (Small Appliances)
            new()
            {
                Id = "app_sub_small",
                CategorySlug = "small-appliances",
                CategoryName = "Appliances",
                Emoji = "🔌",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Smart Small Living Appliances",
                    TitleHighlight = "Compact Convenience for Student Rooms",
                    Subtitle = "Handheld garment steamers, cordless stick vacuums, room heaters, water purifiers, and air fryers designed for modern living.",
                    TagBadge = "⚡ COMPACT DORM ESSENTIALS • 100% GENUINE",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "Compact & Foldable Storage", "Quick 1000W+ Heating", "Dual Voltage Friendly", "Student Campus Approved" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Compact living technology engineered for dorm rooms and compact apartments.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_sml_1", Name = "Philips", Slug = "philips", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_sml_2", Name = "Bajaj", Slug = "bajaj", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_sml_3", Name = "Morphy Richards", Slug = "morphy-richards", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_sml_4", Name = "Usha", Slug = "usha", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_sml_5", Name = "Prestige", Slug = "prestige", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_sml_6", Name = "Kent", Slug = "kent", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_sml_7", Name = "Crompton", Slug = "crompton", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_sml_8", Name = "Wonderchef", Slug = "wonderchef", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_sml_1", Name = "Philips Handheld Garment Steamer 1000 Series", Slug = "philips-garment-steamer", Brand = "Philips", Price = 2499, OriginalPrice = 3995, DiscountBadge = "37% OFF", Rating = 4.6, RatingCount = 610, KeySpecs = "Continuous Steam 20g/min • Kills 99.9% Bacteria • Safe on All Fabrics" }
                }
            },

            // 8. Geysers
            new()
            {
                Id = "app_sub_geysers",
                CategorySlug = "geysers",
                CategoryName = "Geysers",
                Emoji = "🚿",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Instant & Storage Water Geysers",
                    TitleHighlight = "Instant Hot Showers in Chilly Winters",
                    Subtitle = "5-star energy rated glass-lined titanium tanks, magnesium anode rod, shock-proof bodies, and variable temperature dials.",
                    TagBadge = "⚡ 5-STAR RATED • GLASS-LINED TANK",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "Instant 3-Sec Quick Heat", "High Pressure High-Rise Ready", "Shock-Proof Waterproof Body", "8-Year Tank Warranty" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Trusted water heater brands with high energy-efficiency and rust-proof protection.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_gys_1", Name = "AO Smith", Slug = "ao-smith", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_gys_2", Name = "Racold", Slug = "racold", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_gys_3", Name = "Havells", Slug = "havells", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_gys_4", Name = "Crompton", Slug = "crompton", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_gys_5", Name = "Bajaj", Slug = "bajaj", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_gys_6", Name = "V-Guard", Slug = "v-guard", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_gys_7", Name = "Venus", Slug = "venus", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_gys_8", Name = "Orient", Slug = "orient", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_gys_1", Name = "AO Smith HSE-VAS-X-015 Storage 15 Litre 5 Star Geyser", Slug = "ao-smith-15l-geyser", Brand = "AO Smith", Price = 7499, OriginalPrice = 11800, DiscountBadge = "36% OFF", Rating = 4.8, RatingCount = 590, KeySpecs = "Blue Diamond Glass Lining • 8 Bar High Rise Ready • Anode Rod" }
                }
            },

            // 9. Stabilizers
            new()
            {
                Id = "app_sub_stab",
                CategorySlug = "stabilizers",
                CategoryName = "Stabilizers",
                Emoji = "⚡",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Mainline & Appliance Voltage Stabilizers",
                    TitleHighlight = "Surge Protection for Valuable Equipment",
                    Subtitle = "Digital micro-controlled copper wound stabilizers for ACs, TVs, and Refrigerators with high-low voltage cutoff and thermal overload protection.",
                    TagBadge = "⚡ MICRO-CONTROLLED COPPER • SURGE SAFE",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "High/Low Voltage Auto Cutoff", "100% Pure Copper Winding", "Digital LED Status Display", "3-Year Replacement Guarantee" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Reliable surge suppression and automatic voltage regulation brands.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_stb_1", Name = "V-Guard", Slug = "v-guard", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_stb_2", Name = "Microtek", Slug = "microtek", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_stb_3", Name = "Luminous", Slug = "luminous", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_stb_4", Name = "Everest", Slug = "everest", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_stb_5", Name = "Syska", Slug = "syska", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_stb_6", Name = "Bluebird", Slug = "bluebird", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_stb_7", Name = "Livguard", Slug = "livguard", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_stb_8", Name = "Servokon", Slug = "servokon", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_stb_1", Name = "V-Guard VG 400 for 1.5 Ton Inverter AC (130V-280V)", Slug = "vguard-vg400-ac-stabilizer", Brand = "V-Guard", Price = 2399, OriginalPrice = 3290, DiscountBadge = "27% OFF", Rating = 4.8, RatingCount = 1420, KeySpecs = "Intelligent Time Delay System • Thermal Overload Protection • Wall Mount" }
                }
            },

            // 10. Kitchen
            new()
            {
                Id = "app_sub_kitchen",
                CategorySlug = "kitchen-appliances",
                CategoryName = "Kitchen",
                Emoji = "🍳",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Smart Kitchen & Cooking Appliances",
                    TitleHighlight = "Nutritious Meals in Minutes",
                    Subtitle = "High-speed mixer grinders, multi-cookers, induction cooktops, and air fryers with student-safe automatic shutoff.",
                    TagBadge = "⚡ QUICK STUDENT COOKING • FOOD GRADE",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "Induction Friendly & Auto Shutoff", "750W Heavy Duty Copper Motors", "Dishwasher Safe Stainless Steel", "Pre-Programmed Menus" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "India and Europe's premier smart kitchen, culinary and cookware makers.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_ktc_1", Name = "Philips", Slug = "philips", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_ktc_2", Name = "Prestige", Slug = "prestige", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_ktc_3", Name = "Bajaj", Slug = "bajaj", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_ktc_4", Name = "Wonderchef", Slug = "wonderchef", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_ktc_5", Name = "Butterfly", Slug = "butterfly", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_ktc_6", Name = "Morphy Richards", Slug = "morphy-richards", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_ktc_7", Name = "Bosch", Slug = "bosch", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_ktc_8", Name = "Sujata", Slug = "sujata", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_ktc_1", Name = "Philips HL7756/00 750 Watt Mixer Grinder (3 Jars)", Slug = "philips-750w-mixer-grinder", Brand = "Philips", Price = 3499, OriginalPrice = 5295, DiscountBadge = "34% OFF", Rating = 4.7, RatingCount = 890, KeySpecs = "750W Turbo Motor • Advanced Air Ventilation • Leak-proof Jars" }
                }
            },

            // 11. Fridges
            new()
            {
                Id = "app_sub_fridges",
                CategorySlug = "refrigerators",
                CategoryName = "Fridges",
                Emoji = "🧊",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Frost-Free & Single-Door Refrigerators",
                    TitleHighlight = "Fresh Groceries & Meal Preps Stored Right",
                    Subtitle = "Convertible multi-door refrigerators, mini dorm fridges, digital inverter compressors, and toughened glass spill-proof shelves.",
                    TagBadge = "⚡ DIGITAL INVERTER • 10-YEAR WARRANTY",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "Multi-Air Flow Even Cooling", "Convertible Freezer-to-Fridge", "Stabilizer-Free Operation", "Door Cooling+ Technology" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Top refrigeration brands renowned for food freshness and minimal electricity usage.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_frg_1", Name = "Samsung", Slug = "samsung", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_frg_2", Name = "LG", Slug = "lg", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_frg_3", Name = "Whirlpool", Slug = "whirlpool", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_frg_4", Name = "Godrej", Slug = "godrej", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_frg_5", Name = "Haier", Slug = "haier", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_frg_6", Name = "Bosch", Slug = "bosch", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_frg_7", Name = "Liebherr", Slug = "liebherr", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_frg_8", Name = "Panasonic", Slug = "panasonic", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_frg_1", Name = "Samsung 236L 3 Star Inverter Double Door Refrigerator", Slug = "samsung-236l-double-door", Brand = "Samsung", Price = 24990, OriginalPrice = 37990, DiscountBadge = "34% OFF", Rating = 4.8, RatingCount = 430, KeySpecs = "Digital Inverter • MoistFresh Zone • Toughened Glass • All Round Cooling" }
                }
            },

            // 12. Home
            new()
            {
                Id = "app_sub_home",
                CategorySlug = "home-appliances",
                CategoryName = "Home",
                Emoji = "🏠",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Complete Home Comfort & Clean Living",
                    TitleHighlight = "Smart Climate & Air Purification",
                    Subtitle = "True HEPA air purifiers, robotic vacuum cleaners, dehumidifiers, and garment care for a fresh, allergen-free student apartment.",
                    TagBadge = "⚡ SMART IOT ENABLED • HEPA FILTRATION",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "True HEPA H13 Filtration", "Laser Smart Room Mapping", "Ultra-Low Noise Sleep Mode", "Mobile App & Voice Control" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Pioneering smart home cleaning and environment purification systems.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_hm_1", Name = "Dyson", Slug = "dyson", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_hm_2", Name = "Eureka Forbes", Slug = "eureka-forbes", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_hm_3", Name = "Philips", Slug = "philips", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_hm_4", Name = "Havells", Slug = "havells", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_hm_5", Name = "Kent", Slug = "kent", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_hm_6", Name = "Xiaomi", Slug = "xiaomi", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_hm_7", Name = "Black+Decker", Slug = "black-decker", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_hm_8", Name = "AGARO", Slug = "agaro", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_hm_1", Name = "Eureka Forbes Robo Vac n Mop Robotic Cleaner with LiDAR", Slug = "eureka-forbes-robo-vac", Brand = "Eureka Forbes", Price = 17990, OriginalPrice = 29990, DiscountBadge = "40% OFF", Rating = 4.6, RatingCount = 260, KeySpecs = "LiDAR Navigation • 2-in-1 Mopping & Vacuum • 2700Pa Suction" }
                }
            },

            // 13. Breakfast
            new()
            {
                Id = "app_sub_breakfast",
                CategorySlug = "breakfast-appliances",
                CategoryName = "Breakfast",
                Emoji = "☕",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Quick Morning Breakfast Appliances",
                    TitleHighlight = "Fast Campus Mornings Made Easy",
                    Subtitle = "Pop-up toasters, electric kettles, egg boilers, sandwich grillers, and drip coffee makers for energetic student mornings.",
                    TagBadge = "⚡ 2-MINUTE BREAKFAST • EASY CLEAN",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "Rapid 1500W Water Boiling", "Non-Stick Grill Plates", "Auto Shut-off Dry Boil Protection", "Cool Touch Exterior" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Instant breakfast makers designed for quick dorm routines and healthy morning starts.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_bkf_1", Name = "Morphy Richards", Slug = "morphy-richards", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_bkf_2", Name = "Philips", Slug = "philips", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_bkf_3", Name = "Prestige", Slug = "prestige", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_bkf_4", Name = "Kent", Slug = "kent", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_bkf_5", Name = "Wonderchef", Slug = "wonderchef", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_bkf_6", Name = "AGARO", Slug = "agaro", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_bkf_7", Name = "Borosil", Slug = "borosil", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_bkf_8", Name = "Pigeon", Slug = "pigeon", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_bkf_1", Name = "Prestige PKOSS 1.5L Stainless Steel Electric Kettle", Slug = "prestige-electric-kettle", Brand = "Prestige", Price = 749, OriginalPrice = 1295, DiscountBadge = "42% OFF", Rating = 4.7, RatingCount = 2100, KeySpecs = "1500W Fast Boil • 360 Swivel Base • Auto Shut-off Indicator" }
                }
            },

            // 14. FK Originals
            new()
            {
                Id = "app_sub_originals",
                CategorySlug = "top-rated",
                CategoryName = "FK Originals",
                Emoji = "⭐",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Top-Rated Flipkart & Brand Certified Tech",
                    TitleHighlight = "Direct Factory Tested & Highly Rated",
                    Subtitle = "Customer-certified bestsellers with verified 4.5+ star ratings, express door delivery, and extended multi-year peace of mind.",
                    TagBadge = "⚡ VERIFIED 4.5+ STAR BESTSELLERS",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "Top Customer Satisfaction Score", "Express Pre-Flight Dispatch", "Strict Quality Certification", "100% Genuine Replacement" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "Exclusive top-rated brands vetted for maximum reliability and value.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_orig_1", Name = "MarQ", Slug = "marq", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_orig_2", Name = "Motorola", Slug = "motorola", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_orig_3", Name = "Nokia", Slug = "nokia", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_orig_4", Name = "Samsung", Slug = "samsung", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_orig_5", Name = "LG", Slug = "lg", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_orig_6", Name = "Whirlpool", Slug = "whirlpool", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_orig_7", Name = "Voltas", Slug = "voltas", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_orig_8", Name = "Philips", Slug = "philips", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_orig_1", Name = "MarQ by Flipkart 7.5 Kg Semi-Automatic Washing Machine", Slug = "marq-75kg-washer", Brand = "MarQ", Price = 7990, OriginalPrice = 14999, DiscountBadge = "46% OFF", Rating = 4.5, RatingCount = 890, KeySpecs = "5-Star Energy • Deep Clean Pulsator • Rust-Proof Body" }
                }
            },

            // 15. Power Backup
            new()
            {
                Id = "app_sub_backup",
                CategorySlug = "inverters",
                CategoryName = "Power Backup",
                Emoji = "🔋",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Sine Wave Home Inverters & Batteries",
                    TitleHighlight = "Uninterrupted Power for Study & Work",
                    Subtitle = "Pure sine wave inverter UPS with heavy-duty tubular batteries, fast adaptive charging, and zero switchover lag for sensitive electronics.",
                    TagBadge = "⚡ PURE SINE WAVE • HEAVY-DUTY TUBULAR",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "Zero Lag UPS Mode for PCs", "Heavy Duty Long-Life Tubular", "Smart Adaptive Battery Charging", "36-Month Battery Warranty" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "India's highest rated pure sine wave and heavy duty battery power solutions.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_pwr_1", Name = "Luminous", Slug = "luminous", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_pwr_2", Name = "Microtek", Slug = "microtek", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_pwr_3", Name = "Exide", Slug = "exide", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_pwr_4", Name = "V-Guard", Slug = "v-guard", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_pwr_5", Name = "Livguard", Slug = "livguard", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_pwr_6", Name = "Amaron", Slug = "amaron", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_pwr_7", Name = "Sukam", Slug = "sukam", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_pwr_8", Name = "Okaya", Slug = "okaya", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_pwr_1", Name = "Luminous Zelio+ 1100 Pure Sine Wave Inverter 900VA", Slug = "luminous-zelio-1100", Brand = "Luminous", Price = 5999, OriginalPrice = 8500, DiscountBadge = "29% OFF", Rating = 4.8, RatingCount = 1150, KeySpecs = "LED Display Hours & Mins • MCB Protection • Bypass Switch" }
                }
            },

            // 16. Dishwashers
            new()
            {
                Id = "app_sub_dishwashers",
                CategorySlug = "dishwashers",
                CategoryName = "Dishwashers",
                Emoji = "🍽️",
                HeroBanner = new AppliancesSubcategoryHeroBannerDto
                {
                    IsEnabled = true,
                    Title = "Hygienic Smart Kitchen Dishwashers",
                    TitleHighlight = "Zero Scrubbing, 99.9% Germ-Free Cleaning",
                    Subtitle = "Intensive 70°C Kadhai wash programs, water-saving eco cycles, flexible cutlery racks, and silent BLDC inverter pumps.",
                    TagBadge = "⚡ 70°C HYGIENE INTENSIVE • SILENT BLDC",
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    FeaturePills = new List<string> { "70°C Intensive Stain Removal", "Saves Up to 80% Water", "Silent Inverter BLDC Motor", "Adjustable Cutlery Basket" }
                },
                ShopByBrands = new AppliancesShopByBrandsDto
                {
                    IsEnabled = true,
                    Title = "Shop by Brands",
                    Subtitle = "World-class kitchen hygiene dishwashers designed for tough stains and heavy cookware.",
                    Brands = new List<AppliancesBrandItemDto>
                    {
                        new() { Id = "b_dsh_1", Name = "Bosch", Slug = "bosch", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 1 },
                        new() { Id = "b_dsh_2", Name = "LG", Slug = "lg", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 2 },
                        new() { Id = "b_dsh_3", Name = "IFB", Slug = "ifb", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 3 },
                        new() { Id = "b_dsh_4", Name = "Siemens", Slug = "siemens", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 4 },
                        new() { Id = "b_dsh_5", Name = "Voltas Beko", Slug = "voltas-beko", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 5 },
                        new() { Id = "b_dsh_6", Name = "Faber", Slug = "faber", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 6 },
                        new() { Id = "b_dsh_7", Name = "Midea", Slug = "midea", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 7 },
                        new() { Id = "b_dsh_8", Name = "Godrej", Slug = "godrej", LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, DisplayOrder = 8 }
                    }
                },
                CuratedProducts = new List<AppliancesCuratedProductDto>
                {
                    new() { Id = "p_dsh_1", Name = "Bosch 13 Place Settings Free-Standing Dishwasher (Intensive Kadhai)", Slug = "bosch-13-place-dishwasher", Brand = "Bosch", Price = 38990, OriginalPrice = 52990, DiscountBadge = "26% OFF", Rating = 4.9, RatingCount = 420, KeySpecs = "Intensive Kadhai 70°C • EcoSilence Drive • DosageAssist • VarioSpeed" }
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



public sealed class AppliancesBrandItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public string TargetUrl { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
}

public sealed class AppliancesShopByBrandsDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Shop by Brands";
    public string Subtitle { get; set; } = "Verified manufacturers with authorized pan-India service and student warranty";
    public List<AppliancesBrandItemDto> Brands { get; set; } = new();
}

public sealed class AppliancesSubcategoryHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string TagBadge { get; set; } = "⚡ STUDENT CERTIFIED • 100% GENUINE";
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public string TargetUrl { get; set; } = string.Empty;
    public string ButtonText { get; set; } = "Shop Now →";
    public List<string> FeaturePills { get; set; } = new();
}

public sealed class AppliancesCuratedProductDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public string DiscountBadge { get; set; } = string.Empty;
    public double Rating { get; set; } = 4.5;
    public int RatingCount { get; set; } = 120;
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public string KeySpecs { get; set; } = string.Empty;
}

public sealed class AppliancesSubcategoryPageDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string Emoji { get; set; } = "🔌";
    public AppliancesSubcategoryHeroBannerDto HeroBanner { get; set; } = new();
    public AppliancesShopByBrandsDto ShopByBrands { get; set; } = new();
    public List<AppliancesCuratedProductDto> CuratedProducts { get; set; } = new();
}
