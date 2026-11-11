namespace Dreamflykit.Application.SportsHub.Models;

public sealed class SportsHubDto
{
    public SportsFestivalThemeDto FestivalTheme { get; set; } = new();
    public SportsHeroBannerDto HeroBanner { get; set; } = new();
    public SportsShopByCategorySectionDto ShopByCategory { get; set; } = new();
    public List<SportsSubcategoryPageDto> SubcategoryPages { get; set; } = new();
    public SportsTopPicksSectionDto TopPicks { get; set; } = new();
    public SportsTrustBadgesSectionDto TrustBadges { get; set; } = new();
    public SportsBottomBannerDto BottomBanner { get; set; } = new();
    public string ThreeSectionSliderSlug { get; set; } = "sports-hub";

    public static SportsHubDto CreateDefault()
    {
        return new SportsHubDto
        {
            FestivalTheme = new SportsFestivalThemeDto
            {
                IsEnabled = true,
                FestivalName = "Inter-College Tournament & Fitness Season",
                AnnouncementText = "🏆 Campus Sports League Special: Pro-grade sports equipment & verified team kits with student discounts!",
                BadgeText = "SPORTS SPECIAL",
                BannerHighlight = "Up to 50% Off",
                BgGradient = "linear-gradient(135deg, #059669 0%, #0d9488 50%, #0284c7 100%)"
            },
            HeroBanner = new SportsHeroBannerDto
            {
                IsEnabled = true,
                AutoPlay = true,
                SlideIntervalSeconds = 5,
                BannerHeight = 440,
                Slides = new List<SportsHeroSlideDto>
                {
                    new()
                    {
                        Id = "sp-slide-1",
                        BadgeText = "TOURNAMENT READY • UP TO 45% OFF",
                        Title = "Gear Up to Win,",
                        TitleHighlight = "Pro Sports & Campus Kits.",
                        Subtitle = "Engineered for international students & university athletes — high-performance bats, match balls, tournament rackets, and fitness equipment.",
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        TargetUrl = "/category/sports-hub/cricket",
                        ButtonText = "Explore Cricket Gear",
                        DisplayOrder = 1,
                        IsActive = true
                    },
                    new()
                    {
                        Id = "sp-slide-2",
                        BadgeText = "FIFA & BWF COMPLIANT",
                        Title = "Speed, Power & Precision,",
                        TitleHighlight = "Badminton & Football Essentials.",
                        Subtitle = "Lightweight carbon-graphite rackets, thermal-bonded footballs, and high-traction turf cleats for peak athletic performance.",
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        TargetUrl = "/category/sports-hub/badminton",
                        ButtonText = "Shop Badminton Rackets",
                        DisplayOrder = 2,
                        IsActive = true
                    },
                    new()
                    {
                        Id = "sp-slide-3",
                        BadgeText = "DORM & CAMPUS FITNESS",
                        Title = "Strength & Conditioning,",
                        TitleHighlight = "Anytime, Anywhere Training.",
                        Subtitle = "Heavy-duty resistance bands, adjustable dumb-bells, pull-up bars, and gym apparel designed for active student living.",
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        TargetUrl = "/category/sports-hub/gym-fitness",
                        ButtonText = "Browse Fitness Gear",
                        DisplayOrder = 3,
                        IsActive = true
                    }
                }
            },
            ShopByCategory = new SportsShopByCategorySectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                Subtitle = "Explore verified university tournament sports gear, workout equipment, and athletic apparel",
                Items = CreateDefaultCategoryItems()
            },
            SubcategoryPages = CreateDefaultSubcategoryPages(),
            TopPicks = new SportsTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Picks in Sports Hub",
                Subtitle = "Most popular athletic equipment loved by over 20,000 international student athletes",
                Products = new List<SportsProductItemDto>
                {
                    new() { Id = "spp1", Name = "Pro Grade English Willow Cricket Bat", CategoryName = "Cricket", CategorySlug = "cricket", Price = 2999, OriginalPrice = 5999, Rating = 4.9m, ReviewsCount = 280, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Size 6", "Full Size SH" }, Colors = new List<string> { "Natural Wood" } },
                    new() { Id = "spp2", Name = "High-Modulus Carbon Fiber Badminton Racket", CategoryName = "Badminton", CategorySlug = "badminton", Price = 1899, OriginalPrice = 3799, Rating = 4.9m, ReviewsCount = 310, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Student Pick", Sizes = new List<string> { "3U / G4", "4U / G5" }, Colors = new List<string> { "Matte Black", "Cobalt Blue" } },
                    new() { Id = "spp3", Name = "FIFA Certified Match Football (Size 5)", CategoryName = "Football", CategorySlug = "football", Price = 1299, OriginalPrice = 2499, Rating = 4.8m, ReviewsCount = 195, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Tournament Spec", Sizes = new List<string> { "Size 5" }, Colors = new List<string> { "White/Blue", "Hi-Vis Yellow" } },
                    new() { Id = "spp4", Name = "11-Piece Heavy-Duty Workout Resistance Bands Set", CategoryName = "Gym & Fitness", CategorySlug = "gym-fitness", Price = 899, OriginalPrice = 1799, Rating = 4.8m, ReviewsCount = 420, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Dorm Essential", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Multi-Color" } },
                    new() { Id = "spp5", Name = "All-Weather High-Traction Multi-Ground Cleats", CategoryName = "Sports Footwear", CategorySlug = "sports-footwear", Price = 2199, OriginalPrice = 4399, Rating = 4.7m, ReviewsCount = 160, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Pro Grip", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "Neon/Black", "White/Gold" } },
                    new() { Id = "spp6", Name = "Quick-Dry Moisture-Wicking Athletic Team Jersey", CategoryName = "Athletic Wear", CategorySlug = "athletic-wear", Price = 599, OriginalPrice = 1199, Rating = 4.9m, ReviewsCount = 210, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Breathable", Sizes = new List<string> { "S", "M", "L", "XL", "XXL" }, Colors = new List<string> { "Navy", "Black", "Royal Blue" } }
                }
            },
            TrustBadges = new SportsTrustBadgesSectionDto
            {
                IsEnabled = true,
                Badges = new List<SportsTrustBadgeDto>
                {
                    new() { Id = "spt1", Title = "100% Genuine Gear", Subtitle = "Certified authentic equipment from top sports brands", Icon = "shield" },
                    new() { Id = "spt2", Title = "Campus Delivery Ready", Subtitle = "Fast & safe shipping direct to your university or dorm", Icon = "truck" },
                    new() { Id = "spt3", Title = "Pro Durability Tested", Subtitle = "Built to withstand intense daily practice and match play", Icon = "award" },
                    new() { Id = "spt4", Title = "Student Price Match", Subtitle = "Best value sports gear guaranteed for students", Icon = "tag" },
                    new() { Id = "spt5", Title = "7-Day Easy Returns", Subtitle = "Hassle-free replacement for sizing and specifications", Icon = "return" },
                    new() { Id = "spt6", Title = "24/7 Player Support", Subtitle = "Expert guidance on choosing the right racket, bat & gear", Icon = "headset" }
                }
            },
            BottomBanner = new SportsBottomBannerDto
            {
                IsEnabled = true,
                QuizTitle = "Not Sure Which Gear Matches Your Game?",
                QuizSubtitle = "Take our 30-second Sports Kit Quiz to find your ideal racket tension, bat weight & gear fit",
                QuizButtonText = "Find My Sports Kit",
                QuizButtonUrl = "/find-my-kit",
                DestinationsTitle = "Weather-Ready Sports Gear for International Campuses",
                DestinationsSubtitle = "Indoor and outdoor kits tuned for USA, UK, Canada, Australia & Europe university tournaments",
                BulkTitle = "Group Orders for University Teams & Sports Clubs",
                BulkSubtitle = "Equip your full university cricket, football, or badminton squad with custom team printing & discounts",
                BulkButtonText = "Request Team Quote",
                BulkButtonUrl = "/contact"
            }
        };
    }

    public static List<SportsCategoryItemDto> CreateDefaultCategoryItems()
    {
        return new List<SportsCategoryItemDto>
        {
            new()
            {
                Id = "spc1",
                Name = "Cricket",
                Slug = "cricket",
                Subtitle = "Bats, Balls, Pads & Full Match Kits",
                IconEmoji = "🏏",
                ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                DisplayOrder = 1,
                IsUploadingSoon = true,
                IsActive = true,
                Subcategories = new List<string>
                {
                    "Cricket Bats (English & Kashmir Willow)",
                    "Leather & Tennis Match Balls",
                    "Batting Legguards & Pads",
                    "Batting & Wicket-keeping Gloves",
                    "Helmets & Protective Guards",
                    "Complete Cricket Kits & Wheelie Bags",
                    "Cricket Shoes & Rubber Spikes",
                    "Stumps, Bails & Boundary Markers"
                },
                ImportantNote = "All cricket bats and match balls are verified to MCC/Tournament regulations. Select Kashmir Willow for club practice and English Willow for university tournaments."
            },
            new()
            {
                Id = "spc2",
                Name = "Football",
                Slug = "football",
                Subtitle = "FIFA Certified Balls, Cleats & Shin Guards",
                IconEmoji = "⚽",
                ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                DisplayOrder = 2,
                IsUploadingSoon = true,
                IsActive = true,
                Subcategories = new List<string>
                {
                    "Match & Training Footballs (FIFA Spec)",
                    "Football Boots & Cleats (Turf & FG)",
                    "Shin Guards & Ankle Support",
                    "Goalkeeper Gloves & Padded Gear",
                    "Team Jerseys & Training Bibs",
                    "Agility Cones, Ladders & Hurdles",
                    "Football Pumps, Valves & Pressure Gauges",
                    "Football Kit Bags & Backpacks"
                },
                ImportantNote = "Match balls feature thermal-bonded seamless PU shells with butyl bladders for optimal trajectory in all weather conditions."
            },
            new()
            {
                Id = "spc3",
                Name = "Badminton",
                Slug = "badminton",
                Subtitle = "Carbon Rackets, Shuttlecocks & Grips",
                IconEmoji = "🏸",
                ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                DisplayOrder = 3,
                IsUploadingSoon = true,
                IsActive = true,
                Subcategories = new List<string>
                {
                    "Carbon-Graphite Rackets",
                    "Feather & Nylon Shuttlecocks",
                    "Badminton Racket Kit Bags & Backpacks",
                    "Non-Slip Indoor Court Shoes",
                    "Overgrips & High-Tension Strings",
                    "Badminton Nets & Portable Posts"
                },
                ImportantNote = "Tournament rackets are pre-strung with high-durability multifilament strings at 24-28 lbs tension for powerful smashes and touch control."
            },
            new()
            {
                Id = "spc4",
                Name = "Gym & Fitness",
                Slug = "gym-fitness",
                Subtitle = "Resistance Bands, Dumbbells & Mats",
                IconEmoji = "🏋️",
                ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                DisplayOrder = 4,
                IsUploadingSoon = true,
                IsActive = true,
                Subcategories = new List<string>
                {
                    "Stackable Resistance Bands & Tubes",
                    "Hex Dumbbells & Kettlebells",
                    "Doorway Pull-Up & Chin-Up Bars",
                    "High-Speed Ball-Bearing Jump Ropes",
                    "Ab Rollers & Push-Up Stands",
                    "Weightlifting Gloves & Wrist Wraps"
                },
                ImportantNote = "Dorm & apartment friendly equipment designed with non-marking rubber pads and compact storage dimensions."
            },
            new()
            {
                Id = "spc5",
                Name = "Basketball & Volleyball",
                Slug = "basketball-volleyball",
                Subtitle = "Match Balls, Nets & Training Sets",
                IconEmoji = "🏀",
                ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                DisplayOrder = 5,
                IsUploadingSoon = true,
                IsActive = true,
                Subcategories = new List<string>
                {
                    "Composite Leather Basketballs (Size 7 & 6)",
                    "Soft-Touch Match Volleyballs",
                    "Heavy-Duty Basketball Nets & Rims",
                    "Padded Knee & Elbow Guards",
                    "Dual-Action Ball Pumps & Needles",
                    "Portable Volleyball Nets & Accessories"
                },
                ImportantNote = "Basketballs are engineered with deep-channel composite leather for maximum grip on indoor wood and outdoor asphalt courts."
            },
            new()
            {
                Id = "spc6",
                Name = "Tennis & Table Tennis",
                Slug = "tennis-table-tennis",
                Subtitle = "Tournament Rackets, Paddles & Balls",
                IconEmoji = "🏓",
                ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                DisplayOrder = 6,
                IsUploadingSoon = true,
                IsActive = true,
                Subcategories = new List<string>
                {
                    "Tournament Tennis Rackets & Covers",
                    "Pressurized Match Tennis Balls",
                    "ITTF 5-Ply Table Tennis Paddles",
                    "3-Star Table Tennis Match Balls",
                    "Retractable Table Tennis Nets & Clamps",
                    "Racket Grips, Dampeners & Accessories"
                },
                ImportantNote = "Table tennis blades are certified by ITTF with inverted 2.0mm offensive rubbers for high spin and control."
            },
            new()
            {
                Id = "spc7",
                Name = "Sports Footwear",
                Slug = "sports-footwear",
                Subtitle = "Running, Cleats & Court Shoes",
                IconEmoji = "👟",
                ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                DisplayOrder = 7,
                IsUploadingSoon = true,
                IsActive = true,
                Subcategories = new List<string>
                {
                    "Multi-Ground Football Cleats & Turf Shoes",
                    "Non-Marking Indoor Court Shoes",
                    "High-Mileage Running & Training Shoes",
                    "Spikes & Track Footwear",
                    "Anti-Blister Athletic Performance Socks"
                },
                ImportantNote = "All court shoes feature non-marking pure gum rubber outsoles required by university sports complexes and campus gymnasiums."
            },
            new()
            {
                Id = "spc8",
                Name = "Athletic Wear",
                Slug = "athletic-wear",
                Subtitle = "Jerseys, Shorts & Track Pants",
                IconEmoji = "🎽",
                ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                DisplayOrder = 8,
                IsUploadingSoon = true,
                IsActive = true,
                Subcategories = new List<string>
                {
                    "Moisture-Wicking Dry-Fit Jerseys",
                    "Breathable Running & Training Shorts",
                    "Compression Base Layers & Tights",
                    "Lightweight Athletic Tracksuits",
                    "Sweatbands, Headbands & Cooling Towels"
                },
                ImportantNote = "Fabrics are engineered with 4-way stretch, anti-odor silver ion technology and quick-dry microfibers for heavy training sessions."
            },
            new()
            {
                Id = "spc9",
                Name = "Swimming",
                Slug = "swimming",
                Subtitle = "Goggles, Caps & Anti-Fog Gear",
                IconEmoji = "🏊",
                ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                DisplayOrder = 9,
                IsUploadingSoon = true,
                IsActive = true,
                Subcategories = new List<string>
                {
                    "Anti-Fog Mirrored Swim Goggles",
                    "100% Silicone Ergonomic Swim Caps",
                    "Quick-Dry Microfiber Sports Towels",
                    "Waterproof Earplugs & Nose Clip Sets",
                    "Kickboards & Training Pull Buoys"
                },
                ImportantNote = "Swim goggles include UV400 protection and hydrophobic anti-fog coating. Caps are made from 100% tear-resistant soft silicone."
            },
            new()
            {
                Id = "spc10",
                Name = "Sports & Kit Bags",
                Slug = "sports-bags",
                Subtitle = "Duffels, Racket Bags & Backpacks",
                IconEmoji = "🎒",
                ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                DisplayOrder = 10,
                IsUploadingSoon = true,
                IsActive = true,
                Subcategories = new List<string>
                {
                    "High-Capacity Duffel Bags with Shoe Tunnel",
                    "Dual Racket Backpacks with Padded Straps",
                    "Heavy-Duty Cricket Wheelie Kit Bags",
                    "Waterproof Drawstring Sackpacks",
                    "Gym Waist Packs & Running Belts"
                },
                ImportantNote = "Equipped with dedicated ventilated shoe compartments and water-repellent heavy oxford fabric to keep your books and gear separated."
            },
            new()
            {
                Id = "spc11",
                Name = "Yoga & Recovery",
                Slug = "yoga-recovery",
                Subtitle = "Eco Mats, Foam Rollers & Straps",
                IconEmoji = "🧘",
                ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                DisplayOrder = 11,
                IsUploadingSoon = true,
                IsActive = true,
                Subcategories = new List<string>
                {
                    "Non-Slip Dual-Layer Eco TPE Yoga Mats",
                    "Trigger Point Deep Tissue Foam Rollers",
                    "Firm Mobility & Massage Lacrosse Balls",
                    "Multi-Loop Physical Therapy Stretch Straps",
                    "High-Density EVA Yoga Blocks"
                },
                ImportantNote = "6mm high-density eco-friendly TPE mats with alignment markers provide superior joint cushioning on hard dorm floors."
            },
            new()
            {
                Id = "spc12",
                Name = "Outdoor & Trekking",
                Slug = "outdoor-trekking",
                Subtitle = "Hydration Packs, Compasses & Trek Sticks",
                IconEmoji = "🏕️",
                ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                DisplayOrder = 12,
                IsUploadingSoon = true,
                IsActive = true,
                Subcategories = new List<string>
                {
                    "Shock-Absorbing Telescopic Trekking Poles",
                    "Leak-Proof 2L Hydration Bladder Backpacks",
                    "Rechargeable Waterproof LED Headlamps",
                    "Thermal Emergency Space Blankets & Compasses",
                    "Camping Multi-Tools & First Aid Kits"
                },
                ImportantNote = "Telescopic poles are constructed from aerospace-grade aluminum 7075 with quick-lock flick mechanisms and anti-shock springs."
            }
        };
    }

    public static List<SportsSubcategoryPageDto> CreateDefaultSubcategoryPages()
    {
        return new List<SportsSubcategoryPageDto>
        {
            // 1. Cricket
            new()
            {
                CategorySlug = "cricket",
                CategoryName = "Cricket",
                IconEmoji = "🏏",
                Subcategories = new List<string>
                {
                    "Cricket Bats (English & Kashmir Willow)",
                    "Leather & Tennis Match Balls",
                    "Batting Legguards & Pads",
                    "Batting & Wicket-keeping Gloves",
                    "Helmets & Protective Guards",
                    "Complete Cricket Kits & Wheelie Bags",
                    "Cricket Shoes & Rubber Spikes",
                    "Stumps, Bails & Boundary Markers"
                },
                ImportantNote = "All cricket bats and match balls are verified to MCC/Tournament regulations. Select Kashmir Willow for club practice and English Willow for university tournaments.",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Cricket",
                    TitleHighlight = "PRO TOURNAMENT SPEC • ENGLISH & KASHMIR WILLOW",
                    Subtitle = "Handcrafted cricket bats, lightweight batting pads, ventilated gloves, and leather match balls designed for university leagues.",
                    IsUploadingSoon = true,
                    ThemeColor = "#059669"
                },
                Products = new List<SportsProductItemDto>
                {
                    new()
                    {
                        Id = "cr1",
                        Name = "Handcrafted Kashmir Willow Power Cricket Bat",
                        CategoryName = "Cricket",
                        CategorySlug = "cricket",
                        SubcategoryTag = "Cricket Bats (English & Kashmir Willow)",
                        Description = "Pre-knocked Kashmir Willow bat with thick 38mm edges, enlarged sweet spot, natural cane handle and protective toe guard.",
                        Price = 1799,
                        OriginalPrice = 3599,
                        Rating = 4.8m,
                        ReviewsCount = 142,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Best Seller",
                        Sizes = new List<string> { "Short Handle", "Size 6" },
                        Colors = new List<string> { "Natural Wood" },
                        Brand = "SS Ton"
                    },
                    new()
                    {
                        Id = "cr2",
                        Name = "Grade 1 English Willow Tournament Match Bat",
                        CategoryName = "Cricket",
                        CategorySlug = "cricket",
                        SubcategoryTag = "Cricket Bats (English & Kashmir Willow)",
                        Description = "Hand-selected Grade 1 English Willow with 7-9 straight grains, feather-light balanced pickup, and premium chevron grip.",
                        Price = 4499,
                        OriginalPrice = 8999,
                        Rating = 4.9m,
                        ReviewsCount = 88,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Tournament Spec",
                        Sizes = new List<string> { "Short Handle Adult" },
                        Colors = new List<string> { "Natural Wood / Silver" },
                        Brand = "SG"
                    },
                    new()
                    {
                        Id = "cr3",
                        Name = "Four-Piece Alum-Tanned Leather Match Cricket Ball",
                        CategoryName = "Cricket",
                        CategorySlug = "cricket",
                        SubcategoryTag = "Leather & Tennis Match Balls",
                        Description = "Tournament spec 156g 4-piece alum tanned leather ball with cork core, hand-stitched 80-stitch seam for 50-over match play.",
                        Price = 499,
                        OriginalPrice = 999,
                        Rating = 4.8m,
                        ReviewsCount = 165,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Match Ready",
                        Sizes = new List<string> { "Standard 156g" },
                        Colors = new List<string> { "Red", "White", "Pink" },
                        Brand = "Kookaburra"
                    },
                    new()
                    {
                        Id = "cr4",
                        Name = "High-Density Foam Lightweight Batting Legguards",
                        CategoryName = "Cricket",
                        CategorySlug = "cricket",
                        SubcategoryTag = "Batting Legguards & Pads",
                        Description = "Molded ultra-lightweight high-density foam pads with reinforced cane vertical bolsters, instep protection, and sweat-absorbing toweling.",
                        Price = 1499,
                        OriginalPrice = 2999,
                        Rating = 4.9m,
                        ReviewsCount = 98,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Impact Shield",
                        Sizes = new List<string> { "Mens Adult", "Youth" },
                        Colors = new List<string> { "White", "Navy" },
                        Brand = "DSC"
                    },
                    new()
                    {
                        Id = "cr5",
                        Name = "Ventilated Sheepskin Leather Batting Gloves (Pair)",
                        CategoryName = "Cricket",
                        CategorySlug = "cricket",
                        SubcategoryTag = "Batting & Wicket-keeping Gloves",
                        Description = "Supple sheepskin leather palm with multi-flex ergonomic finger cuts, high-density fiber inserts, and dual-sided sweatbands.",
                        Price = 899,
                        OriginalPrice = 1799,
                        Rating = 4.7m,
                        ReviewsCount = 120,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Flex Grip",
                        Sizes = new List<string> { "Adult", "Youth" },
                        Colors = new List<string> { "White/Green", "White/Blue" },
                        Brand = "SG"
                    },
                    new()
                    {
                        Id = "cr6",
                        Name = "High-Impact Cricket Helmet with Steel Visor",
                        CategoryName = "Cricket",
                        CategorySlug = "cricket",
                        SubcategoryTag = "Helmets & Protective Guards",
                        Description = "BSI certified cricket helmet with shock-absorbing EPS liner, adjustable powder-coated steel grill, and dial-fit adjustment strap.",
                        Price = 1399,
                        OriginalPrice = 2799,
                        Rating = 4.9m,
                        ReviewsCount = 110,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Safety Certified",
                        Sizes = new List<string> { "M (56-58cm)", "L (59-61cm)" },
                        Colors = new List<string> { "Navy", "Black", "Maroon" },
                        Brand = "Shrey"
                    },
                    new()
                    {
                        Id = "cr7",
                        Name = "Full-Size Cricket Kit Bag with Heavy-Duty Tractor Wheels",
                        CategoryName = "Cricket",
                        CategorySlug = "cricket",
                        SubcategoryTag = "Complete Cricket Kits & Wheelie Bags",
                        Description = "1680D ballistic nylon wheelie kit bag with 2 integrated bat pockets, footwear tunnel, dual padded haul handles, and tractor wheels.",
                        Price = 2499,
                        OriginalPrice = 4999,
                        Rating = 4.9m,
                        ReviewsCount = 92,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Pro Capacity",
                        Sizes = new List<string> { "90 x 35 x 35 cm" },
                        Colors = new List<string> { "Black/Cobalt", "Charcoal/Lime" },
                        Brand = "SS Ton"
                    },
                    new()
                    {
                        Id = "cr8",
                        Name = "Solid Wood Cricket Stumps Set with Bails & Heavy Metal Base",
                        CategoryName = "Cricket",
                        CategorySlug = "cricket",
                        SubcategoryTag = "Stumps, Bails & Boundary Markers",
                        Description = "Tournament height seasoned hardwood stumps with precision turned bails, cast-iron spring-free heavy base for indoor and turf nets.",
                        Price = 899,
                        OriginalPrice = 1799,
                        Rating = 4.7m,
                        ReviewsCount = 74,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "All Ground",
                        Sizes = new List<string> { "28 Inch Standard" },
                        Colors = new List<string> { "Natural Polish" },
                        Brand = "Cosco"
                    }
                }
            },
            // 2. Football
            new()
            {
                CategorySlug = "football",
                CategoryName = "Football",
                IconEmoji = "⚽",
                Subcategories = new List<string>
                {
                    "Match & Training Footballs (FIFA Spec)",
                    "Football Boots & Cleats (Turf & FG)",
                    "Shin Guards & Ankle Support",
                    "Goalkeeper Gloves & Padded Gear",
                    "Team Jerseys & Training Bibs",
                    "Agility Cones, Ladders & Hurdles",
                    "Football Pumps, Valves & Pressure Gauges",
                    "Football Kit Bags & Backpacks"
                },
                ImportantNote = "Match balls feature thermal-bonded seamless PU shells with butyl bladders for optimal trajectory in all weather conditions.",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Football",
                    TitleHighlight = "FIFA STANDARD • THERMAL BONDED",
                    Subtitle = "Official size 5 match footballs, ergonomic shin guards, dual-action ball pumps, and goalkeeper grip gloves.",
                    IsUploadingSoon = true,
                    ThemeColor = "#0284c7"
                },
                Products = new List<SportsProductItemDto>
                {
                    new()
                    {
                        Id = "fb1",
                        Name = "Thermal-Bonded All-Weather Match Football (Size 5)",
                        CategoryName = "Football",
                        CategorySlug = "football",
                        SubcategoryTag = "Match & Training Footballs (FIFA Spec)",
                        Description = "FIFA Quality spec thermal-bonded football with seamless textured polyurethane casing, reinforced foam backing and zero water uptake.",
                        Price = 1299,
                        OriginalPrice = 2599,
                        Rating = 4.9m,
                        ReviewsCount = 210,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Best Seller",
                        Sizes = new List<string> { "Size 5", "Size 4" },
                        Colors = new List<string> { "White/Blue", "Hi-Vis Yellow" },
                        Brand = "Nivia"
                    },
                    new()
                    {
                        Id = "fb2",
                        Name = "Multi-Ground TPU Molded Football Cleats",
                        CategoryName = "Football",
                        CategorySlug = "football",
                        SubcategoryTag = "Football Boots & Cleats (Turf & FG)",
                        Description = "Conical molded stud configuration engineered for explosive acceleration on natural turf and artificial grass. Lightweight micro-textured upper.",
                        Price = 2199,
                        OriginalPrice = 4399,
                        Rating = 4.8m,
                        ReviewsCount = 185,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Pro Traction",
                        Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" },
                        Colors = new List<string> { "Neon/Black", "White/Gold" },
                        Brand = "Nivia"
                    },
                    new()
                    {
                        Id = "fb3",
                        Name = "Ergonomic Breathable Ankle Shin Guards",
                        CategoryName = "Football",
                        CategorySlug = "football",
                        SubcategoryTag = "Shin Guards & Ankle Support",
                        Description = "Anatomically molded PP outer shell with dense EVA shock-absorbing foam lining, integrated ankle guard cup, and elastic stirrup.",
                        Price = 449,
                        OriginalPrice = 899,
                        Rating = 4.7m,
                        ReviewsCount = 135,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Lightweight",
                        Sizes = new List<string> { "S", "M", "L" },
                        Colors = new List<string> { "Black", "Red", "Blue" },
                        Brand = "Cosco"
                    },
                    new()
                    {
                        Id = "fb4",
                        Name = "4mm German Latex Contact Pro Goalkeeper Gloves",
                        CategoryName = "Football",
                        CategorySlug = "football",
                        SubcategoryTag = "Goalkeeper Gloves & Padded Gear",
                        Description = "Professional 4mm German Contact Latex with negative cut fingers, removable finger spine saves, and full elastic bandage wrist closure.",
                        Price = 1199,
                        OriginalPrice = 2399,
                        Rating = 4.8m,
                        ReviewsCount = 95,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Pro Grip",
                        Sizes = new List<string> { "Size 8", "Size 9", "Size 10" },
                        Colors = new List<string> { "Neon Orange", "Black" },
                        Brand = "Nivia"
                    },
                    new()
                    {
                        Id = "fb5",
                        Name = "Breathable Numbered Team Training Bibs (Set of 10)",
                        CategoryName = "Football",
                        CategorySlug = "football",
                        SubcategoryTag = "Team Jerseys & Training Bibs",
                        Description = "Pack of 10 numbered scrimmage vests made from lightweight tear-resistant open-mesh polyester. Quick-drying and machine washable.",
                        Price = 899,
                        OriginalPrice = 1799,
                        Rating = 4.8m,
                        ReviewsCount = 112,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Squad Pack",
                        Sizes = new List<string> { "Adult Free Size" },
                        Colors = new List<string> { "Fluorescent Yellow", "Electric Blue" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "fb6",
                        Name = "Speed & Agility Footwork Training Ladder Kit",
                        CategoryName = "Football",
                        CategorySlug = "football",
                        SubcategoryTag = "Agility Cones, Ladders & Hurdles",
                        Description = "6-meter 12-rung adjustable agility ladder with 10 high-visibility disc cones, ground pegs, and durable carry pouch.",
                        Price = 699,
                        OriginalPrice = 1399,
                        Rating = 4.9m,
                        ReviewsCount = 140,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Cardio Drill",
                        Sizes = new List<string> { "6 Meter Ladder" },
                        Colors = new List<string> { "Yellow/Black" },
                        Brand = "Nivia"
                    },
                    new()
                    {
                        Id = "fb7",
                        Name = "Dual-Action Compact Ball Pump with Pressure Gauge",
                        CategoryName = "Football",
                        CategorySlug = "football",
                        SubcategoryTag = "Football Pumps, Valves & Pressure Gauges",
                        Description = "Inflates on both push and pull strokes. Built-in dial barometer gauge displays PSI/BAR ratings. Includes 3 spare steel pins.",
                        Price = 349,
                        OriginalPrice = 699,
                        Rating = 4.8m,
                        ReviewsCount = 170,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Essential",
                        Sizes = new List<string> { "Standard Pocket" },
                        Colors = new List<string> { "Black", "Blue" },
                        Brand = "Cosco"
                    },
                    new()
                    {
                        Id = "fb8",
                        Name = "Water-Resistant Ball & Cleat Football Backpack",
                        CategoryName = "Football",
                        CategorySlug = "football",
                        SubcategoryTag = "Football Kit Bags & Backpacks",
                        Description = "High-density 600D water-resistant backpack featuring front mesh ball holder, ventilated bottom cleat tunnel, and padded laptop sleeve.",
                        Price = 1199,
                        OriginalPrice = 2399,
                        Rating = 4.9m,
                        ReviewsCount = 158,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Campus Pick",
                        Sizes = new List<string> { "35 Litres" },
                        Colors = new List<string> { "Matte Black", "Navy Blue" },
                        Brand = "Nivia"
                    }
                }
            },
            // 3. Badminton
            new()
            {
                CategorySlug = "badminton",
                CategoryName = "Badminton",
                IconEmoji = "🏸",
                Subcategories = new List<string>
                {
                    "Carbon-Graphite Rackets",
                    "Feather & Nylon Shuttlecocks",
                    "Badminton Racket Kit Bags & Backpacks",
                    "Non-Slip Indoor Court Shoes",
                    "Overgrips & High-Tension Strings",
                    "Badminton Nets & Portable Posts"
                },
                ImportantNote = "Tournament rackets are pre-strung with high-durability multifilament strings at 24-28 lbs tension for powerful smashes and touch control.",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Badminton",
                    TitleHighlight = "HIGH-MODULUS GRAPHITE • ULTRA-LIGHT",
                    Subtitle = "Head-heavy smasher rackets, precision control frames, tournament goose feather shuttles, and non-slip absorbent grips.",
                    IsUploadingSoon = true,
                    ThemeColor = "#7c3aed"
                },
                Products = new List<SportsProductItemDto>
                {
                    new()
                    {
                        Id = "bm1",
                        Name = "Carbon-Graphite High-Tension Badminton Racket",
                        CategoryName = "Badminton",
                        CategorySlug = "badminton",
                        SubcategoryTag = "Carbon-Graphite Rackets",
                        Description = "High modulus carbon fiber frame supporting up to 30 lbs string tension. Isometric head shape expands the sweet spot for effortless clears.",
                        Price = 1899,
                        OriginalPrice = 3799,
                        Rating = 4.9m,
                        ReviewsCount = 280,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Best Seller",
                        Sizes = new List<string> { "4U / G5", "3U / G4" },
                        Colors = new List<string> { "Matte Black", "Ruby Red" },
                        Brand = "Yonex"
                    },
                    new()
                    {
                        Id = "bm2",
                        Name = "Even-Balance All-Round Tournament Racket",
                        CategoryName = "Badminton",
                        CategorySlug = "badminton",
                        SubcategoryTag = "Carbon-Graphite Rackets",
                        Description = "Aero-box high rigidity frame delivers lightning quick racket handling for defensive blocks and sharp front-court net kills.",
                        Price = 2499,
                        OriginalPrice = 4999,
                        Rating = 4.8m,
                        ReviewsCount = 135,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Pro Control",
                        Sizes = new List<string> { "4U / G5" },
                        Colors = new List<string> { "Midnight Navy", "Neon Lime" },
                        Brand = "Li-Ning"
                    },
                    new()
                    {
                        Id = "bm3",
                        Name = "Tournament Goose Feather Shuttlecocks (Tube of 12)",
                        CategoryName = "Badminton",
                        CategorySlug = "badminton",
                        SubcategoryTag = "Feather & Nylon Shuttlecocks",
                        Description = "BWF tournament grade natural goose feather shuttlecocks with dual-density Portuguese cork base for consistent flight and spin stability.",
                        Price = 1099,
                        OriginalPrice = 2199,
                        Rating = 4.8m,
                        ReviewsCount = 190,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "BWF Standard",
                        Sizes = new List<string> { "Speed 77", "Speed 78" },
                        Colors = new List<string> { "White" },
                        Brand = "Yonex"
                    },
                    new()
                    {
                        Id = "bm4",
                        Name = "Durable Synthetic Nylon Shuttlecocks (Pack of 6)",
                        CategoryName = "Badminton",
                        CategorySlug = "badminton",
                        SubcategoryTag = "Feather & Nylon Shuttlecocks",
                        Description = "Engineered precision nylon skirt with solid foam base, lasting up to 4 times longer than standard shuttles. High visibility optic yellow.",
                        Price = 499,
                        OriginalPrice = 999,
                        Rating = 4.7m,
                        ReviewsCount = 310,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Durable Play",
                        Sizes = new List<string> { "Medium Speed" },
                        Colors = new List<string> { "Neon Yellow", "White" },
                        Brand = "Yonex"
                    },
                    new()
                    {
                        Id = "bm5",
                        Name = "6-Racket Thermo-Guard Badminton Kit Bag",
                        CategoryName = "Badminton",
                        CategorySlug = "badminton",
                        SubcategoryTag = "Badminton Racket Kit Bags & Backpacks",
                        Description = "Dual full-length zippered compartments with thermal insulation foil lining to protect string tension from hot and cold temperature swings.",
                        Price = 1399,
                        OriginalPrice = 2799,
                        Rating = 4.9m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Thermal Guard",
                        Sizes = new List<string> { "6 Rackets" },
                        Colors = new List<string> { "Black/Red", "Royal Blue" },
                        Brand = "Yonex"
                    },
                    new()
                    {
                        Id = "bm6",
                        Name = "Non-Marking Gum Rubber Badminton Court Shoes",
                        CategoryName = "Badminton",
                        CategorySlug = "badminton",
                        SubcategoryTag = "Non-Slip Indoor Court Shoes",
                        Description = "High traction hexagrip gum rubber outsole, TruCushion EVA shock absorption in heel, and lateral claw support for rapid court pivots.",
                        Price = 1799,
                        OriginalPrice = 3499,
                        Rating = 4.8m,
                        ReviewsCount = 140,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Non-Marking",
                        Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10" },
                        Colors = new List<string> { "White/Blue", "Navy/Orange" },
                        Brand = "Yonex"
                    },
                    new()
                    {
                        Id = "bm7",
                        Name = "Anti-Slip Tacky Overgrips (Pack of 5)",
                        CategoryName = "Badminton",
                        CategorySlug = "badminton",
                        SubcategoryTag = "Overgrips & High-Tension Strings",
                        Description = "0.6mm ultra-thin high-tack polyurethane overgrips with moisture-wicking micro-perforations to prevent sweaty palm slip during games.",
                        Price = 299,
                        OriginalPrice = 599,
                        Rating = 4.9m,
                        ReviewsCount = 240,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Sweat Absorbent",
                        Sizes = new List<string> { "Universal 110cm" },
                        Colors = new List<string> { "Assorted", "Black", "White" },
                        Brand = "Yonex"
                    },
                    new()
                    {
                        Id = "bm8",
                        Name = "Official Tournament Badminton Net with Steel Cable",
                        CategoryName = "Badminton",
                        CategorySlug = "badminton",
                        SubcategoryTag = "Badminton Nets & Portable Posts",
                        Description = "Heavy-duty knotted nylon mesh with 2-inch double-stitched white canvas headband and vinyl-coated aircraft steel tension wire.",
                        Price = 749,
                        OriginalPrice = 1499,
                        Rating = 4.7m,
                        ReviewsCount = 88,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Tournament Net",
                        Sizes = new List<string> { "20 x 2.5 Feet" },
                        Colors = new List<string> { "Maroon/White" },
                        Brand = "Cosco"
                    }
                }
            },
            // 4. Gym & Fitness
            new()
            {
                CategorySlug = "gym-fitness",
                CategoryName = "Gym & Fitness",
                IconEmoji = "🏋️",
                Subcategories = new List<string>
                {
                    "Stackable Resistance Bands & Tubes",
                    "Hex Dumbbells & Kettlebells",
                    "Doorway Pull-Up & Chin-Up Bars",
                    "High-Speed Ball-Bearing Jump Ropes",
                    "Ab Rollers & Push-Up Stands",
                    "Weightlifting Gloves & Wrist Wraps"
                },
                ImportantNote = "Dorm & apartment friendly equipment designed with non-marking rubber pads and compact storage dimensions.",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Gym & Fitness",
                    TitleHighlight = "COMPACT WORKOUT • MAXIMUM GAINS",
                    Subtitle = "Stackable resistance tubes, high-speed ball-bearing jump ropes, doorway pull-up bars, and push-up stands for dorm workouts.",
                    IsUploadingSoon = true,
                    ThemeColor = "#ea580c"
                },
                Products = new List<SportsProductItemDto>
                {
                    new()
                    {
                        Id = "gf1",
                        Name = "11-Piece Stackable Resistance Bands Gym Set",
                        CategoryName = "Gym & Fitness",
                        CategorySlug = "gym-fitness",
                        SubcategoryTag = "Stackable Resistance Bands & Tubes",
                        Description = "100% natural Malaysian latex tubes (10 to 50 lbs, total 150 lbs) with cushioned handles, reinforced door anchor, ankle straps, and pouch.",
                        Price = 899,
                        OriginalPrice = 1799,
                        Rating = 4.9m,
                        ReviewsCount = 380,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Best Seller",
                        Sizes = new List<string> { "Up to 150 lbs" },
                        Colors = new List<string> { "Multi-Color" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "gf2",
                        Name = "Cast Iron Rubber-Encased Hex Dumbbells (Pair of 5kg)",
                        CategoryName = "Gym & Fitness",
                        CategorySlug = "gym-fitness",
                        SubcategoryTag = "Hex Dumbbells & Kettlebells",
                        Description = "Heavy cast iron core with odorless natural rubber outer coating to prevent floor scuffing and noise in dorm rooms. Ergonomic chrome handle.",
                        Price = 1499,
                        OriginalPrice = 2999,
                        Rating = 4.8m,
                        ReviewsCount = 165,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Floor Safe",
                        Sizes = new List<string> { "5kg x 2", "7.5kg x 2" },
                        Colors = new List<string> { "Black/Chrome" },
                        Brand = "Kobo"
                    },
                    new()
                    {
                        Id = "gf3",
                        Name = "Heavy-Duty No-Screw Locking Doorway Pull-Up Bar",
                        CategoryName = "Gym & Fitness",
                        CategorySlug = "gym-fitness",
                        SubcategoryTag = "Doorway Pull-Up & Chin-Up Bars",
                        Description = "Diamond gear locking mechanism with thickened non-slip honeycomb silicone pads. Holds up to 200kg without drilling or door damage.",
                        Price = 1399,
                        OriginalPrice = 2799,
                        Rating = 4.8m,
                        ReviewsCount = 150,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Dorm Friendly",
                        Sizes = new List<string> { "Adjustable 65-100cm" },
                        Colors = new List<string> { "Black/Red" },
                        Brand = "Kobo"
                    },
                    new()
                    {
                        Id = "gf4",
                        Name = "High-Speed 360° Ball-Bearing Aluminum Jump Rope",
                        CategoryName = "Gym & Fitness",
                        CategorySlug = "gym-fitness",
                        SubcategoryTag = "High-Speed Ball-Bearing Jump Ropes",
                        Description = "Dual ball-bearing swivel handles with knurled aluminum grip and 3m PVC-coated steel cable for smooth high-cadence double unders.",
                        Price = 449,
                        OriginalPrice = 899,
                        Rating = 4.8m,
                        ReviewsCount = 210,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Cardio Pro",
                        Sizes = new List<string> { "Adjustable 3m" },
                        Colors = new List<string> { "Black", "Red", "Blue" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "gf5",
                        Name = "Dual-Wheel Ultra-Wide Ab Roller Wheel with Knee Mat",
                        CategoryName = "Gym & Fitness",
                        CategorySlug = "gym-fitness",
                        SubcategoryTag = "Ab Rollers & Push-Up Stands",
                        Description = "Wide curved rubber tread wheel for smooth multi-angle core carving. Features internal kinetic carbon spring assist and dense knee cushion pad.",
                        Price = 799,
                        OriginalPrice = 1599,
                        Rating = 4.8m,
                        ReviewsCount = 185,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Core Master",
                        Sizes = new List<string> { "Standard with Mat" },
                        Colors = new List<string> { "Black/Red" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "gf6",
                        Name = "Padded Leather Weightlifting Gloves with 18 Inch Wrist Wraps",
                        CategoryName = "Gym & Fitness",
                        CategorySlug = "gym-fitness",
                        SubcategoryTag = "Weightlifting Gloves & Wrist Wraps",
                        Description = "Full leather palm with silicone honeycomb grip, ventilated mesh backing, and heavy-duty elastic wrist wrap stabilizers.",
                        Price = 549,
                        OriginalPrice = 1099,
                        Rating = 4.7m,
                        ReviewsCount = 130,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Wrist Lock",
                        Sizes = new List<string> { "M", "L", "XL" },
                        Colors = new List<string> { "Black" },
                        Brand = "Kobo"
                    }
                }
            },
            // 5. Basketball & Volleyball
            new()
            {
                CategorySlug = "basketball-volleyball",
                CategoryName = "Basketball & Volleyball",
                IconEmoji = "🏀",
                Subcategories = new List<string>
                {
                    "Composite Leather Basketballs (Size 7 & 6)",
                    "Soft-Touch Match Volleyballs",
                    "Heavy-Duty Basketball Nets & Rims",
                    "Padded Knee & Elbow Guards",
                    "Dual-Action Ball Pumps & Needles",
                    "Portable Volleyball Nets & Accessories"
                },
                ImportantNote = "Basketballs are engineered with deep-channel composite leather for maximum grip on indoor wood and outdoor asphalt courts.",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Basketball & Volleyball",
                    TitleHighlight = "HIGH TRACTION • OFFICIAL SIZE & WEIGHT",
                    Subtitle = "Composite leather outdoor/indoor basketballs, soft-touch volleyballs, and portable tournament net systems.",
                    IsUploadingSoon = true,
                    ThemeColor = "#f59e0b"
                },
                Products = new List<SportsProductItemDto>
                {
                    new()
                    {
                        Id = "bv1",
                        Name = "Deep-Channel Composite Leather Basketball (Size 7)",
                        CategoryName = "Basketball & Volleyball",
                        CategorySlug = "basketball-volleyball",
                        SubcategoryTag = "Composite Leather Basketballs (Size 7 & 6)",
                        Description = "Official size 7 basketball crafted from premium composite microfiber leather with pebble grain surface and deep channels for supreme fingertip feel.",
                        Price = 1199,
                        OriginalPrice = 2399,
                        Rating = 4.9m,
                        ReviewsCount = 180,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Best Seller",
                        Sizes = new List<string> { "Size 7", "Size 6" },
                        Colors = new List<string> { "Classic Orange", "Black/Gold" },
                        Brand = "Spalding"
                    },
                    new()
                    {
                        Id = "bv2",
                        Name = "Soft-Touch Waterproof 18-Panel Match Volleyball",
                        CategoryName = "Basketball & Volleyball",
                        CategorySlug = "basketball-volleyball",
                        SubcategoryTag = "Soft-Touch Match Volleyballs",
                        Description = "Official Size 5 volleyball with soft-cushion composite PU covering, reinforced butyl bladder, and minimal sting technology on forearm passes.",
                        Price = 849,
                        OriginalPrice = 1699,
                        Rating = 4.8m,
                        ReviewsCount = 130,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Soft Cushion",
                        Sizes = new List<string> { "Size 5 Standard" },
                        Colors = new List<string> { "Blue/Yellow/White" },
                        Brand = "Mikasa"
                    },
                    new()
                    {
                        Id = "bv3",
                        Name = "Heavy-Duty All-Weather Basketball Net (Set of 2)",
                        CategoryName = "Basketball & Volleyball",
                        CategorySlug = "basketball-volleyball",
                        SubcategoryTag = "Heavy-Duty Basketball Nets & Rims",
                        Description = "12-loop standard braided polyester nets designed to withstand outdoor sun and rain without rotting or tangling.",
                        Price = 299,
                        OriginalPrice = 599,
                        Rating = 4.7m,
                        ReviewsCount = 95,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Weatherproof",
                        Sizes = new List<string> { "12 Loops" },
                        Colors = new List<string> { "Red/White/Blue", "White" },
                        Brand = "Cosco"
                    },
                    new()
                    {
                        Id = "bv4",
                        Name = "Padded Knee Pads for Volleyball & Court Sports",
                        CategoryName = "Basketball & Volleyball",
                        CategorySlug = "basketball-volleyball",
                        SubcategoryTag = "Padded Knee & Elbow Guards",
                        Description = "High-density EVA molded knee shield with breathable knitted sleeve and anti-slip silicone top hem for dive recovery and floor impact.",
                        Price = 499,
                        OriginalPrice = 999,
                        Rating = 4.8m,
                        ReviewsCount = 110,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Impact Cushion",
                        Sizes = new List<string> { "M", "L", "XL" },
                        Colors = new List<string> { "Black", "White" },
                        Brand = "Nivia"
                    },
                    new()
                    {
                        Id = "bv5",
                        Name = "Steel Spring-Loaded Breakaway Basketball Rim",
                        CategoryName = "Basketball & Volleyball",
                        CategorySlug = "basketball-volleyball",
                        SubcategoryTag = "Heavy-Duty Basketball Nets & Rims",
                        Description = "18-inch standard solid steel rim with enclosed spring rebound return, universal bolt mounting plate, and anti-rust orange powder coating.",
                        Price = 1899,
                        OriginalPrice = 3799,
                        Rating = 4.8m,
                        ReviewsCount = 76,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Heavy Duty",
                        Sizes = new List<string> { "18 Inch Standard" },
                        Colors = new List<string> { "Orange" },
                        Brand = "Cosco"
                    },
                    new()
                    {
                        Id = "bv6",
                        Name = "Heavy-Duty Portable Volleyball Net System",
                        CategoryName = "Basketball & Volleyball",
                        CategorySlug = "basketball-volleyball",
                        SubcategoryTag = "Portable Volleyball Nets & Accessories",
                        Description = "Regulation 32ft x 3ft weather-resistant polyethylene net with top steel cable, heavy boundary line markers, and zippered storage carry bag.",
                        Price = 1499,
                        OriginalPrice = 2999,
                        Rating = 4.8m,
                        ReviewsCount = 82,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Tournament Net",
                        Sizes = new List<string> { "32 x 3 Feet" },
                        Colors = new List<string> { "Black/White" },
                        Brand = "Cosco"
                    }
                }
            },
            // 6. Tennis & Table Tennis
            new()
            {
                CategorySlug = "tennis-table-tennis",
                CategoryName = "Tennis & Table Tennis",
                IconEmoji = "🏓",
                Subcategories = new List<string>
                {
                    "Tournament Tennis Rackets & Covers",
                    "Pressurized Match Tennis Balls",
                    "ITTF 5-Ply Table Tennis Paddles",
                    "3-Star Table Tennis Match Balls",
                    "Retractable Table Tennis Nets & Clamps",
                    "Racket Grips, Dampeners & Accessories"
                },
                ImportantNote = "Table tennis blades are certified by ITTF with inverted 2.0mm offensive rubbers for high spin and control.",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Tennis & Table Tennis",
                    TitleHighlight = "SPIN & CONTROL • ITTF COMPLIANT",
                    Subtitle = "5-ply wood table tennis blades with inverted pimpled rubber, pressurized tennis balls, and graphite composite court rackets.",
                    IsUploadingSoon = true,
                    ThemeColor = "#10b981"
                },
                Products = new List<SportsProductItemDto>
                {
                    new()
                    {
                        Id = "tt1",
                        Name = "Tournament 5-Ply Table Tennis Racket with ITTF Rubber",
                        CategoryName = "Tennis & Table Tennis",
                        CategorySlug = "tennis-table-tennis",
                        SubcategoryTag = "ITTF 5-Ply Table Tennis Paddles",
                        Description = "5-ply Nordic wood blade layered with 2.0mm ITTF tournament sponge and tacky inverted topsheet. Flared ergonomic handle for swift counter-loops.",
                        Price = 999,
                        OriginalPrice = 1999,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Best Seller",
                        Sizes = new List<string> { "Flared Handle" },
                        Colors = new List<string> { "Red/Black" },
                        Brand = "Stag"
                    },
                    new()
                    {
                        Id = "tt2",
                        Name = "3-Star ITTF Approved Table Tennis Balls (Pack of 12)",
                        CategoryName = "Tennis & Table Tennis",
                        CategorySlug = "tennis-table-tennis",
                        SubcategoryTag = "3-Star Table Tennis Match Balls",
                        Description = "Seamless 40+ non-celluloid poly balls calibrated for uniform roundness, reliable high bounce, and consistent tournament spin.",
                        Price = 449,
                        OriginalPrice = 899,
                        Rating = 4.9m,
                        ReviewsCount = 190,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "3-Star Pro",
                        Sizes = new List<string> { "40mm+" },
                        Colors = new List<string> { "White", "Orange" },
                        Brand = "Stag"
                    },
                    new()
                    {
                        Id = "tt3",
                        Name = "Pressurized Championship Extra-Duty Tennis Balls (Can of 3)",
                        CategoryName = "Tennis & Table Tennis",
                        CategorySlug = "tennis-table-tennis",
                        SubcategoryTag = "Pressurized Match Tennis Balls",
                        Description = "Pressurized gas core wrapped in high-interlock woven felt for maximum durability and playability on hard campus tennis courts.",
                        Price = 499,
                        OriginalPrice = 999,
                        Rating = 4.8m,
                        ReviewsCount = 135,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "All Court",
                        Sizes = new List<string> { "Can of 3" },
                        Colors = new List<string> { "Optic Yellow" },
                        Brand = "Wilson"
                    },
                    new()
                    {
                        Id = "tt4",
                        Name = "Lightweight Graphite Composite Lawn Tennis Racket",
                        CategoryName = "Tennis & Table Tennis",
                        CategorySlug = "tennis-table-tennis",
                        SubcategoryTag = "Tournament Tennis Rackets & Covers",
                        Description = "100 sq in head size with open 16x19 string pattern for easy spin generation. Pre-strung with dampener and full zippered shoulder cover.",
                        Price = 2499,
                        OriginalPrice = 4999,
                        Rating = 4.7m,
                        ReviewsCount = 88,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Power Frame",
                        Sizes = new List<string> { "Grip 2 (4 1/4)", "Grip 3 (4 3/8)" },
                        Colors = new List<string> { "Blue/Black" },
                        Brand = "Wilson"
                    },
                    new()
                    {
                        Id = "tt5",
                        Name = "Retractable Table Tennis Net with Spring Clamps",
                        CategoryName = "Tennis & Table Tennis",
                        CategorySlug = "tennis-table-tennis",
                        SubcategoryTag = "Retractable Table Tennis Nets & Clamps",
                        Description = "Pulls open up to 6 feet to instantly convert any dining or study table into a ping pong arena. Rubber-padded spring clamp prevents scratches.",
                        Price = 599,
                        OriginalPrice = 1199,
                        Rating = 4.9m,
                        ReviewsCount = 160,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Dorm Pick",
                        Sizes = new List<string> { "Extends up to 190cm" },
                        Colors = new List<string> { "Black/Red" },
                        Brand = "Stag"
                    },
                    new()
                    {
                        Id = "tt6",
                        Name = "Silicone Vibration Dampeners for Tennis Rackets (Pack of 4)",
                        CategoryName = "Tennis & Table Tennis",
                        CategorySlug = "tennis-table-tennis",
                        SubcategoryTag = "Racket Grips, Dampeners & Accessories",
                        Description = "Grooved silicone shock absorbers that clip securely between bottom main strings to dampen frame buzz and arm strain.",
                        Price = 249,
                        OriginalPrice = 499,
                        Rating = 4.8m,
                        ReviewsCount = 98,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Elbow Guard",
                        Sizes = new List<string> { "Universal" },
                        Colors = new List<string> { "Assorted Neon" },
                        Brand = "Wilson"
                    }
                }
            },
            // 7. Sports Footwear
            new()
            {
                CategorySlug = "sports-footwear",
                CategoryName = "Sports Footwear",
                IconEmoji = "👟",
                Subcategories = new List<string>
                {
                    "Multi-Ground Football Cleats & Turf Shoes",
                    "Non-Marking Indoor Court Shoes",
                    "High-Mileage Running & Training Shoes",
                    "Spikes & Track Footwear",
                    "Anti-Blister Athletic Performance Socks"
                },
                ImportantNote = "All court shoes feature non-marking pure gum rubber outsoles required by university sports complexes and campus gymnasiums.",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Sports Footwear",
                    TitleHighlight = "HIGH TRACTION • CUSHIONED STABILITY",
                    Subtitle = "Multi-ground studs, non-marking indoor court shoes, lightweight sprint spikes, and high-mileage road running trainers.",
                    IsUploadingSoon = true,
                    ThemeColor = "#0284c7"
                },
                Products = new List<SportsProductItemDto>
                {
                    new()
                    {
                        Id = "sf1",
                        Name = "All-Weather High-Traction Multi-Ground Cleats",
                        CategoryName = "Sports Footwear",
                        CategorySlug = "sports-footwear",
                        SubcategoryTag = "Multi-Ground Football Cleats & Turf Shoes",
                        Description = "Molded dual-density TPU studs for quick deceleration and lateral traction on grass and artificial fields. Synthetic upper with micro-ribbing.",
                        Price = 2199,
                        OriginalPrice = 4399,
                        Rating = 4.9m,
                        ReviewsCount = 185,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Best Seller",
                        Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" },
                        Colors = new List<string> { "Neon/Black", "White/Gold" },
                        Brand = "Nivia"
                    },
                    new()
                    {
                        Id = "sf2",
                        Name = "Non-Marking Gum Rubber Court Shoes",
                        CategoryName = "Sports Footwear",
                        CategorySlug = "sports-footwear",
                        SubcategoryTag = "Non-Slip Indoor Court Shoes",
                        Description = "Designed for badminton, squash, and volleyball courts. 100% non-marking natural gum outsole with anti-rollover TPU stabilizer bridge.",
                        Price = 1799,
                        OriginalPrice = 3499,
                        Rating = 4.8m,
                        ReviewsCount = 140,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Non-Marking",
                        Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10" },
                        Colors = new List<string> { "White/Blue", "Navy/Orange" },
                        Brand = "Yonex"
                    },
                    new()
                    {
                        Id = "sf3",
                        Name = "Air-Cushioned Ultra-Lightweight Campus Running Shoes",
                        CategoryName = "Sports Footwear",
                        CategorySlug = "sports-footwear",
                        SubcategoryTag = "High-Mileage Running & Training Shoes",
                        Description = "Breathable jacquard mesh upper with full-length cloud EVA midsole and responsive heel air bubble. High-mileage carbon rubber tread.",
                        Price = 1599,
                        OriginalPrice = 3199,
                        Rating = 4.8m,
                        ReviewsCount = 220,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Cloud Cushion",
                        Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11", "UK 12" },
                        Colors = new List<string> { "All-Black", "Grey/Lime" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "sf4",
                        Name = "Track & Field 7-Pin Sprint Spikes with Wrench",
                        CategoryName = "Sports Footwear",
                        CategorySlug = "sports-footwear",
                        SubcategoryTag = "Spikes & Track Footwear",
                        Description = "Pebax spike plate with 7 replaceable metallic pins for sprint events from 100m to 400m. Ultra-snug synthetic aerodynamic upper.",
                        Price = 2299,
                        OriginalPrice = 4599,
                        Rating = 4.7m,
                        ReviewsCount = 85,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Sprint Master",
                        Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10" },
                        Colors = new List<string> { "Cyan/Black", "White/Red" },
                        Brand = "Nivia"
                    },
                    new()
                    {
                        Id = "sf5",
                        Name = "Anti-Blister Cushioned Athletic Quarter Socks (3 Pairs)",
                        CategoryName = "Sports Footwear",
                        CategorySlug = "sports-footwear",
                        SubcategoryTag = "Anti-Blister Athletic Performance Socks",
                        Description = "CoolMax technical fibers with zoned terry padding at heel and ball of foot, dynamic arch compression band, and seamless toe closure.",
                        Price = 399,
                        OriginalPrice = 799,
                        Rating = 4.9m,
                        ReviewsCount = 310,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Anti Blister",
                        Sizes = new List<string> { "Free Size (UK 7-11)" },
                        Colors = new List<string> { "White/Grey", "Black" },
                        Brand = "Decathlon"
                    }
                }
            },
            // 8. Athletic Wear
            new()
            {
                CategorySlug = "athletic-wear",
                CategoryName = "Athletic Wear",
                IconEmoji = "🎽",
                Subcategories = new List<string>
                {
                    "Moisture-Wicking Dry-Fit Jerseys",
                    "Breathable Running & Training Shorts",
                    "Compression Base Layers & Tights",
                    "Lightweight Athletic Tracksuits",
                    "Sweatbands, Headbands & Cooling Towels"
                },
                ImportantNote = "Fabrics are engineered with 4-way stretch, anti-odor silver ion technology and quick-dry microfibers for heavy training sessions.",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Athletic Wear",
                    TitleHighlight = "DRI-FIT BREATHABLE • 4-WAY STRETCH",
                    Subtitle = "Moisture-wicking dry-fit sports tees, quick-dry football shorts, athletic compression base layers, and zippered track jackets.",
                    IsUploadingSoon = true,
                    ThemeColor = "#06b6d4"
                },
                Products = new List<SportsProductItemDto>
                {
                    new()
                    {
                        Id = "aw1",
                        Name = "Quick-Dry Moisture-Wicking Athletic Team Jersey",
                        CategoryName = "Athletic Wear",
                        CategorySlug = "athletic-wear",
                        SubcategoryTag = "Moisture-Wicking Dry-Fit Jerseys",
                        Description = "Featherweight polyester micro-mesh pulls sweat to the outer surface for rapid evaporation. Raglan sleeves ensure unrestricted arm range.",
                        Price = 599,
                        OriginalPrice = 1199,
                        Rating = 4.9m,
                        ReviewsCount = 230,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Best Seller",
                        Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                        Colors = new List<string> { "Navy", "Black", "Royal Blue" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "aw2",
                        Name = "Breathable 2-in-1 Running Shorts with Phone Pocket",
                        CategoryName = "Athletic Wear",
                        CategorySlug = "athletic-wear",
                        SubcategoryTag = "Breathable Running & Training Shorts",
                        Description = "Lightweight outer shell with integrated anti-chafe compression liner. Includes bounce-free interior phone pocket and zippered key pocket.",
                        Price = 649,
                        OriginalPrice = 1299,
                        Rating = 4.8m,
                        ReviewsCount = 175,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "2-in-1 Pocket",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Black", "Charcoal", "Army Green" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "aw3",
                        Name = "Full-Length Thermal Athletic Compression Tights",
                        CategoryName = "Athletic Wear",
                        CategorySlug = "athletic-wear",
                        SubcategoryTag = "Compression Base Layers & Tights",
                        Description = "Graduated compression enhances venous return and stabilizes muscle vibrations during sprint and leg sessions. Flatlock stitching prevents chafing.",
                        Price = 749,
                        OriginalPrice = 1499,
                        Rating = 4.9m,
                        ReviewsCount = 140,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Muscle Flex",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Black", "Dark Grey" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "aw4",
                        Name = "Tapered Ankle Lightweight Track Pants",
                        CategoryName = "Athletic Wear",
                        CategorySlug = "athletic-wear",
                        SubcategoryTag = "Lightweight Athletic Tracksuits",
                        Description = "4-way stretch poly-spandex knit with zippered ankle cuffs, elastic waistband with drawcord, and deep zippered hip pockets.",
                        Price = 849,
                        OriginalPrice = 1699,
                        Rating = 4.7m,
                        ReviewsCount = 115,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Slim Fit",
                        Sizes = new List<string> { "S", "M", "L", "XL" },
                        Colors = new List<string> { "Dark Grey", "Black" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "aw5",
                        Name = "Anti-Slip Headband & Wrist Sweatbands Set",
                        CategoryName = "Athletic Wear",
                        CategorySlug = "athletic-wear",
                        SubcategoryTag = "Sweatbands, Headbands & Cooling Towels",
                        Description = "Thick terry cloth cotton blend with internal silicone grip line to halt sweat drips into eyes during intense sets.",
                        Price = 249,
                        OriginalPrice = 499,
                        Rating = 4.8m,
                        ReviewsCount = 190,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Sweat Stopper",
                        Sizes = new List<string> { "Universal Free Size" },
                        Colors = new List<string> { "Black", "White", "Navy" },
                        Brand = "Decathlon"
                    }
                }
            },
            // 9. Swimming
            new()
            {
                CategorySlug = "swimming",
                CategoryName = "Swimming",
                IconEmoji = "🏊",
                Subcategories = new List<string>
                {
                    "Anti-Fog Mirrored Swim Goggles",
                    "100% Silicone Ergonomic Swim Caps",
                    "Quick-Dry Microfiber Sports Towels",
                    "Waterproof Earplugs & Nose Clip Sets",
                    "Kickboards & Training Pull Buoys"
                },
                ImportantNote = "Swim goggles include UV400 protection and hydrophobic anti-fog coating. Caps are made from 100% tear-resistant soft silicone.",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Swimming",
                    TitleHighlight = "ANTI-FOG UV PROTECTION • 100% SILICONE",
                    Subtitle = "Wide-vision mirrored swim goggles, waterproof silicone swim caps, microfiber quick-dry towels, and ear & nose clips.",
                    IsUploadingSoon = true,
                    ThemeColor = "#0284c7"
                },
                Products = new List<SportsProductItemDto>
                {
                    new()
                    {
                        Id = "sw1",
                        Name = "Wide-Vision Mirrored Anti-Fog Swim Goggles",
                        CategoryName = "Swimming",
                        CategorySlug = "swimming",
                        SubcategoryTag = "Anti-Fog Mirrored Swim Goggles",
                        Description = "Curved polycarbonate lenses with UV400 shield and anti-fog film. Ultra-soft 3D memory silicone gaskets prevent eye suction marks.",
                        Price = 599,
                        OriginalPrice = 1199,
                        Rating = 4.9m,
                        ReviewsCount = 195,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Best Seller",
                        Sizes = new List<string> { "3 Nose Bridge Sizes" },
                        Colors = new List<string> { "Mirror Black", "Blue Mirror" },
                        Brand = "Speedo"
                    },
                    new()
                    {
                        Id = "sw2",
                        Name = "100% Silicone Ergonomic Waterproof Swim Cap",
                        CategoryName = "Swimming",
                        CategorySlug = "swimming",
                        SubcategoryTag = "100% Silicone Ergonomic Swim Caps",
                        Description = "Contoured ear pockets relieve pressure buildup and keep pool water out. Snag-free tear-resistant silicone protects hair from chlorine.",
                        Price = 299,
                        OriginalPrice = 599,
                        Rating = 4.8m,
                        ReviewsCount = 150,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Ear Protection",
                        Sizes = new List<string> { "Universal Adult" },
                        Colors = new List<string> { "Black", "Navy", "Silver" },
                        Brand = "Speedo"
                    },
                    new()
                    {
                        Id = "sw3",
                        Name = "Ultra-Compact Quick-Dry Microfiber Beach & Pool Towel",
                        CategoryName = "Swimming",
                        CategorySlug = "swimming",
                        SubcategoryTag = "Quick-Dry Microfiber Sports Towels",
                        Description = "Absorbs 5x its weight in moisture and dries 3x faster than cotton. Rolls up into a breathable mesh pouch no bigger than a beverage can.",
                        Price = 499,
                        OriginalPrice = 999,
                        Rating = 4.8m,
                        ReviewsCount = 135,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Fast Dry",
                        Sizes = new List<string> { "140 x 70 cm" },
                        Colors = new List<string> { "Ocean Blue", "Teal", "Grey" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "sw4",
                        Name = "Soft Silicone Waterproof Earplugs & Nose Clip Set",
                        CategoryName = "Swimming",
                        CategorySlug = "swimming",
                        SubcategoryTag = "Waterproof Earplugs & Nose Clip Sets",
                        Description = "Multi-flange Christmas tree profile earplugs provide airtight seal without pushing into canal. Flexible silicone bridge nose clip with travel case.",
                        Price = 249,
                        OriginalPrice = 499,
                        Rating = 4.6m,
                        ReviewsCount = 88,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "With Case",
                        Sizes = new List<string> { "Standard" },
                        Colors = new List<string> { "Clear Blue", "Black" },
                        Brand = "Speedo"
                    },
                    new()
                    {
                        Id = "sw5",
                        Name = "EVA Foam Ergonomic Swim Kickboard",
                        CategoryName = "Swimming",
                        CategorySlug = "swimming",
                        SubcategoryTag = "Kickboards & Training Pull Buoys",
                        Description = "High buoyancy closed-cell EVA foam kickboard with dual molded handle grip cutouts for lap drills and lower body kick endurance.",
                        Price = 549,
                        OriginalPrice = 1099,
                        Rating = 4.8m,
                        ReviewsCount = 76,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Lap Trainer",
                        Sizes = new List<string> { "45 x 30 cm" },
                        Colors = new List<string> { "Royal Blue", "Bright Yellow" },
                        Brand = "Speedo"
                    }
                }
            },
            // 10. Sports Bags
            new()
            {
                CategorySlug = "sports-bags",
                CategoryName = "Sports & Kit Bags",
                IconEmoji = "🎒",
                Subcategories = new List<string>
                {
                    "High-Capacity Duffel Bags with Shoe Tunnel",
                    "Dual Racket Backpacks with Padded Straps",
                    "Heavy-Duty Cricket Wheelie Kit Bags",
                    "Waterproof Drawstring Sackpacks",
                    "Gym Waist Packs & Running Belts"
                },
                ImportantNote = "Equipped with dedicated ventilated shoe compartments and water-repellent heavy oxford fabric to keep your books and gear separated.",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Sports & Kit Bags",
                    TitleHighlight = "WATER-RESISTANT • SEPARATE SHOE COMPARTMENT",
                    Subtitle = "High-capacity gym duffels with ventilated shoe tunnels, racket backpacks with padded straps, and water-resistant kit bags.",
                    IsUploadingSoon = true,
                    ThemeColor = "#4f46e5"
                },
                Products = new List<SportsProductItemDto>
                {
                    new()
                    {
                        Id = "sb1",
                        Name = "45L Water-Resistant Gym Duffel with Shoe Compartment",
                        CategoryName = "Sports & Kit Bags",
                        CategorySlug = "sports-bags",
                        SubcategoryTag = "High-Capacity Duffel Bags with Shoe Tunnel",
                        Description = "Tough 900D polyester duffel with waterproof base, side ventilated sneaker tunnel, wet towel PVC compartment, and padded shoulder strap.",
                        Price = 1199,
                        OriginalPrice = 2399,
                        Rating = 4.9m,
                        ReviewsCount = 210,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Best Seller",
                        Sizes = new List<string> { "45 Litres" },
                        Colors = new List<string> { "Matte Black", "Army Green" },
                        Brand = "Wildcraft"
                    },
                    new()
                    {
                        Id = "sb2",
                        Name = "Dual-Racket Badminton & Tennis Padded Backpack",
                        CategoryName = "Sports & Kit Bags",
                        CategorySlug = "sports-bags",
                        SubcategoryTag = "Dual Racket Backpacks with Padded Straps",
                        Description = "Dedicated back zipper compartment holds up to 2 rackets with grip covers. Separate main cavity holds clothing, water bottle, and campus tech.",
                        Price = 1399,
                        OriginalPrice = 2799,
                        Rating = 4.8m,
                        ReviewsCount = 140,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Racket Holder",
                        Sizes = new List<string> { "35 Litres" },
                        Colors = new List<string> { "Navy/Red", "Black" },
                        Brand = "Yonex"
                    },
                    new()
                    {
                        Id = "sb3",
                        Name = "Waterproof Drawstring Sports Gym Sackpack",
                        CategoryName = "Sports & Kit Bags",
                        CategorySlug = "sports-bags",
                        SubcategoryTag = "Waterproof Drawstring Sackpacks",
                        Description = "Reinforced high-density oxford nylon with thick tubular shoulder cords, front zippered phone pocket, and earphone cable access port.",
                        Price = 349,
                        OriginalPrice = 699,
                        Rating = 4.7m,
                        ReviewsCount = 165,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Lightweight",
                        Sizes = new List<string> { "Standard" },
                        Colors = new List<string> { "Black", "Royal Blue" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "sb4",
                        Name = "Bounce-Free Slim Running Belt Waist Pack",
                        CategoryName = "Sports & Kit Bags",
                        CategorySlug = "sports-bags",
                        SubcategoryTag = "Gym Waist Packs & Running Belts",
                        Description = "Ultra-slim expandable spandex pouch fits up to 6.8-inch smartphones without bouncing. Reflective night perimeter trim and key clip.",
                        Price = 399,
                        OriginalPrice = 799,
                        Rating = 4.8m,
                        ReviewsCount = 125,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "No Bounce",
                        Sizes = new List<string> { "Waist 26-42 Inch" },
                        Colors = new List<string> { "Black", "Neon Green" },
                        Brand = "Decathlon"
                    }
                }
            },
            // 11. Yoga & Recovery
            new()
            {
                CategorySlug = "yoga-recovery",
                CategoryName = "Yoga & Recovery",
                IconEmoji = "🧘",
                Subcategories = new List<string>
                {
                    "Non-Slip Dual-Layer Eco TPE Yoga Mats",
                    "Trigger Point Deep Tissue Foam Rollers",
                    "Firm Mobility & Massage Lacrosse Balls",
                    "Multi-Loop Physical Therapy Stretch Straps",
                    "High-Density EVA Yoga Blocks"
                },
                ImportantNote = "6mm high-density eco-friendly TPE mats with alignment markers provide superior joint cushioning on hard dorm floors.",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Yoga & Recovery",
                    TitleHighlight = "HIGH DENSITY • ECO-FRIENDLY TPE",
                    Subtitle = "6mm anti-skid dual-layer yoga mats, deep-tissue trigger-point foam rollers, massage lacrosse balls, and stretch straps.",
                    IsUploadingSoon = true,
                    ThemeColor = "#059669"
                },
                Products = new List<SportsProductItemDto>
                {
                    new()
                    {
                        Id = "yr1",
                        Name = "6mm Dual-Color Anti-Skid Eco TPE Yoga Mat with Carry Strap",
                        CategoryName = "Yoga & Recovery",
                        CategorySlug = "yoga-recovery",
                        SubcategoryTag = "Non-Slip Dual-Layer Eco TPE Yoga Mats",
                        Description = "Biodegradable odorless TPE material with laser alignment guides, wavy anti-slip tire bottom grip, and premium elastic carry harness.",
                        Price = 899,
                        OriginalPrice = 1799,
                        Rating = 4.9m,
                        ReviewsCount = 260,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Best Seller",
                        Sizes = new List<string> { "6mm Extra Thick (183x61cm)" },
                        Colors = new List<string> { "Teal/Grey", "Purple/Pink", "Blue/Black" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "yr2",
                        Name = "High-Density Trigger Point Deep Tissue Foam Roller",
                        CategoryName = "Yoga & Recovery",
                        CategorySlug = "yoga-recovery",
                        SubcategoryTag = "Trigger Point Deep Tissue Foam Rollers",
                        Description = "Molded EVA 3D matrix ridges mimic fingertip and palm pressure to release lactic acid and tight myofascial knots in quads, calves, and lats.",
                        Price = 699,
                        OriginalPrice = 1399,
                        Rating = 4.8m,
                        ReviewsCount = 180,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Muscle Recovery",
                        Sizes = new List<string> { "33cm Standard" },
                        Colors = new List<string> { "Black", "Blue", "Orange" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "yr3",
                        Name = "Firm Lacrosse Mobility Massage Ball for Back & Glutes",
                        CategoryName = "Yoga & Recovery",
                        CategorySlug = "yoga-recovery",
                        SubcategoryTag = "Firm Mobility & Massage Lacrosse Balls",
                        Description = "100% solid dense natural rubber ball isolates hard-to-reach trigger points around shoulders, spine, and piriformis without flattening.",
                        Price = 249,
                        OriginalPrice = 499,
                        Rating = 4.7m,
                        ReviewsCount = 110,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Targeted Relief",
                        Sizes = new List<string> { "6.3cm Standard" },
                        Colors = new List<string> { "Red", "Black" },
                        Brand = "Kobo"
                    },
                    new()
                    {
                        Id = "yr4",
                        Name = "Multi-Loop Non-Elastic Physical Therapy Stretch Strap",
                        CategoryName = "Yoga & Recovery",
                        CategorySlug = "yoga-recovery",
                        SubcategoryTag = "Multi-Loop Physical Therapy Stretch Straps",
                        Description = "12 individual numbered hand/foot loops allow progressive stretching of hamstrings, calves, and shoulders without overstraining joints.",
                        Price = 349,
                        OriginalPrice = 699,
                        Rating = 4.8m,
                        ReviewsCount = 95,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Flexibility",
                        Sizes = new List<string> { "12 Loops (2.5m)" },
                        Colors = new List<string> { "Black/Green" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "yr5",
                        Name = "High-Density Beveled-Edge EVA Foam Yoga Blocks (Pair)",
                        CategoryName = "Yoga & Recovery",
                        CategorySlug = "yoga-recovery",
                        SubcategoryTag = "High-Density EVA Yoga Blocks",
                        Description = "Supportive firm foam blocks with beveled comfort edges to provide stable height elevation and proper posture alignment in challenging poses.",
                        Price = 499,
                        OriginalPrice = 999,
                        Rating = 4.8m,
                        ReviewsCount = 88,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Posture Support",
                        Sizes = new List<string> { "Pack of 2" },
                        Colors = new List<string> { "Purple", "Slate Blue" },
                        Brand = "Decathlon"
                    }
                }
            },
            // 12. Outdoor & Trekking
            new()
            {
                CategorySlug = "outdoor-trekking",
                CategoryName = "Outdoor & Trekking",
                IconEmoji = "🏕️",
                Subcategories = new List<string>
                {
                    "Shock-Absorbing Telescopic Trekking Poles",
                    "Leak-Proof 2L Hydration Bladder Backpacks",
                    "Rechargeable Waterproof LED Headlamps",
                    "Thermal Emergency Space Blankets & Compasses",
                    "Camping Multi-Tools & First Aid Kits"
                },
                ImportantNote = "Telescopic poles are constructed from aerospace-grade aluminum 7075 with quick-lock flick mechanisms and anti-shock springs.",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Outdoor & Trekking",
                    TitleHighlight = "ADVENTURE READY • WEATHERPROOF RUGGED",
                    Subtitle = "Shock-absorbing telescopic trekking poles, 2L leak-proof hydration backpacks, tactical LED headlamps, and outdoor survival gear.",
                    IsUploadingSoon = true,
                    ThemeColor = "#84cc16"
                },
                Products = new List<SportsProductItemDto>
                {
                    new()
                    {
                        Id = "ot1",
                        Name = "Aluminum Alloy Telescopic Shock-Absorbing Trekking Poles (Pair)",
                        CategoryName = "Outdoor & Trekking",
                        CategorySlug = "outdoor-trekking",
                        SubcategoryTag = "Shock-Absorbing Telescopic Trekking Poles",
                        Description = "Aircraft aluminum 7075 poles with fast-flick cam lock, internal shock dampening springs, natural cork sweat-wicking grips, and mud baskets.",
                        Price = 1199,
                        OriginalPrice = 2399,
                        Rating = 4.8m,
                        ReviewsCount = 145,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Best Seller",
                        Sizes = new List<string> { "Adjustable 65-135cm" },
                        Colors = new List<string> { "Matte Black", "Cobalt Blue" },
                        Brand = "Wildcraft"
                    },
                    new()
                    {
                        Id = "ot2",
                        Name = "2L BPA-Free Leakproof Hydration Pack Running Backpack",
                        CategoryName = "Outdoor & Trekking",
                        CategorySlug = "outdoor-trekking",
                        SubcategoryTag = "Leak-Proof 2L Hydration Bladder Backpacks",
                        Description = "Includes 2L anti-microbial water reservoir with insulated neoprene hose and silicone bite valve. Breathable honeycomb mesh shoulder harness.",
                        Price = 999,
                        OriginalPrice = 1999,
                        Rating = 4.9m,
                        ReviewsCount = 160,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Waterproof",
                        Sizes = new List<string> { "2 Litre Bladder" },
                        Colors = new List<string> { "Black", "Orange" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "ot3",
                        Name = "Rechargeable 800-Lumen Waterproof LED Headlamp",
                        CategoryName = "Outdoor & Trekking",
                        CategorySlug = "outdoor-trekking",
                        SubcategoryTag = "Rechargeable Waterproof LED Headlamps",
                        Description = "Type-C fast rechargeable 1200mAh battery with smart hand-wave motion sensor, 5 light modes including red night safety strobe, and IPX5 rainproof rating.",
                        Price = 599,
                        OriginalPrice = 1199,
                        Rating = 4.8m,
                        ReviewsCount = 120,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Night Trail",
                        Sizes = new List<string> { "Type-C USB" },
                        Colors = new List<string> { "Black" },
                        Brand = "Decathlon"
                    },
                    new()
                    {
                        Id = "ot4",
                        Name = "Thermal Emergency Mylar Foil Space Blankets (Pack of 4)",
                        CategoryName = "Outdoor & Trekking",
                        CategorySlug = "outdoor-trekking",
                        SubcategoryTag = "Thermal Emergency Space Blankets & Compasses",
                        Description = "Military grade aluminized Mylar reflects up to 90% of body heat. Windproof, waterproof, pocket-sized emergency hypothermia protection.",
                        Price = 299,
                        OriginalPrice = 599,
                        Rating = 4.7m,
                        ReviewsCount = 85,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Safety Shield",
                        Sizes = new List<string> { "210 x 130 cm" },
                        Colors = new List<string> { "Silver/Gold" },
                        Brand = "Wildcraft"
                    },
                    new()
                    {
                        Id = "ot5",
                        Name = "Compact 120-Piece Waterproof Outdoor Hiking First Aid Kit",
                        CategoryName = "Outdoor & Trekking",
                        CategorySlug = "outdoor-trekking",
                        SubcategoryTag = "Camping Multi-Tools & First Aid Kits",
                        Description = "Essential hospital grade medical supplies packed in a rugged waterproof red nylon pouch with Molle attachment straps for backpacks.",
                        Price = 699,
                        OriginalPrice = 1399,
                        Rating = 4.9m,
                        ReviewsCount = 110,
                        ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true,
                        Badge = "Emergency Kit",
                        Sizes = new List<string> { "120 Pieces" },
                        Colors = new List<string> { "Emergency Red" },
                        Brand = "Wildcraft"
                    }
                }
            }
        };
    }
}

public sealed class SportsFestivalThemeDto
{
    public bool IsEnabled { get; set; } = true;
    public string FestivalName { get; set; } = string.Empty;
    public string AnnouncementText { get; set; } = string.Empty;
    public string BadgeText { get; set; } = string.Empty;
    public string BannerHighlight { get; set; } = string.Empty;
    public string BgGradient { get; set; } = "linear-gradient(135deg, #059669 0%, #0d9488 50%, #0284c7 100%)";
}

public sealed class SportsHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public bool AutoPlay { get; set; } = true;
    public int SlideIntervalSeconds { get; set; } = 5;
    public int BannerHeight { get; set; } = 440;
    public List<SportsHeroSlideDto> Slides { get; set; } = new();
}

public sealed class SportsHeroSlideDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string BadgeText { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public string TargetUrl { get; set; } = string.Empty;
    public string ButtonText { get; set; } = "Shop Now";
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
}

public sealed class SportsShopByCategorySectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Shop by Category";
    public string Subtitle { get; set; } = "Explore tournament gear, fitness equipment & athletic wear";
    public List<SportsCategoryItemDto> Items { get; set; } = new();
}

public sealed class SportsCategoryItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = "⚽";
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
    public List<string> Subcategories { get; set; } = new();
    public string? ImportantNote { get; set; }
}

public sealed class SportsSubcategoryPageDto
{
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = "🏅";
    public List<string> Subcategories { get; set; } = new();
    public string? ImportantNote { get; set; }
    public List<string> FilterTags { get; set; } = new();
    public SportsSubcategoryHeroDto HeroBanner { get; set; } = new();
    public List<SportsProductItemDto> Products { get; set; } = new();
}

public sealed class SportsSubcategoryHeroDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public string ThemeColor { get; set; } = "#059669";
    public string BadgeText { get; set; } = "STUDENT ESSENTIALS • 100% GENUINE";
    public int BannerHeight { get; set; } = 280;
}

public sealed class SportsProductItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string CategorySlug { get; set; } = string.Empty;
    public string SubcategoryTag { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public decimal Rating { get; set; } = 4.8m;
    public int ReviewsCount { get; set; } = 100;
    public string ImageUrl { get; set; } = string.Empty;
    public string ProductUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public string Badge { get; set; } = string.Empty;
    public List<string> Sizes { get; set; } = new();
    public List<string> Colors { get; set; } = new();
    public string Brand { get; set; } = string.Empty;
    public bool InStock { get; set; } = true;
    public bool IsActive { get; set; } = true;
}

public sealed class SportsTopPicksSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Top Picks for You";
    public string Subtitle { get; set; } = "Most popular athletic equipment loved by international student athletes";
    public List<SportsProductItemDto> Products { get; set; } = new();
}

public sealed class SportsTrustBadgesSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public List<SportsTrustBadgeDto> Badges { get; set; } = new();
}

public sealed class SportsTrustBadgeDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = "shield";
    public string IconEmoji { get; set; } = "🛡️";
}

public sealed class SportsBottomBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Join the University Sports Club";
    public string Subtitle { get; set; } = "Get exclusive university tournament kit deals, free team jersey prints and student warranty.";
    public string ButtonText { get; set; } = "Explore Campus Sports";
    public string TargetUrl { get; set; } = "/category/sports-hub/cricket";
    public string QuizTitle { get; set; } = string.Empty;
    public string QuizSubtitle { get; set; } = string.Empty;
    public string QuizButtonText { get; set; } = string.Empty;
    public string QuizButtonUrl { get; set; } = string.Empty;
    public string DestinationsTitle { get; set; } = string.Empty;
    public string DestinationsSubtitle { get; set; } = string.Empty;
    public string BulkTitle { get; set; } = string.Empty;
    public string BulkSubtitle { get; set; } = string.Empty;
    public string BulkButtonText { get; set; } = string.Empty;
    public string BulkButtonUrl { get; set; } = string.Empty;
}
