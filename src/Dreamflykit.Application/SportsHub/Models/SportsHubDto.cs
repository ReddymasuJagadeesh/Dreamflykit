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
            new() { Id = "spc1", Name = "Cricket", Slug = "cricket", Subtitle = "Bats, Balls, Pads & Gloves", IconEmoji = "🏏", ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = true, IsActive = true },
            new() { Id = "spc2", Name = "Football", Slug = "football", Subtitle = "Match Balls, Shin Guards & Pumps", IconEmoji = "⚽", ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = true, IsActive = true },
            new() { Id = "spc3", Name = "Badminton", Slug = "badminton", Subtitle = "Rackets, Shuttlecocks & Grips", IconEmoji = "🏸", ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = true, IsActive = true },
            new() { Id = "spc4", Name = "Gym & Fitness", Slug = "gym-fitness", Subtitle = "Dumbbells, Bands & Jump Ropes", IconEmoji = "🏋️", ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = true, IsActive = true },
            new() { Id = "spc5", Name = "Basketball & Volleyball", Slug = "basketball-volleyball", Subtitle = "Official Size Balls & Nets", IconEmoji = "🏀", ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = true, IsActive = true },
            new() { Id = "spc6", Name = "Tennis & Table Tennis", Slug = "tennis-table-tennis", Subtitle = "Paddles, Balls & Rackets", IconEmoji = "🏓", ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = true, IsActive = true },
            new() { Id = "spc7", Name = "Sports Footwear", Slug = "sports-footwear", Subtitle = "Running, Cleats & Court Shoes", IconEmoji = "👟", ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", DisplayOrder = 7, IsUploadingSoon = true, IsActive = true },
            new() { Id = "spc8", Name = "Athletic Wear", Slug = "athletic-wear", Subtitle = "Jerseys, Shorts & Track Pants", IconEmoji = "🎽", ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", DisplayOrder = 8, IsUploadingSoon = true, IsActive = true },
            new() { Id = "spc9", Name = "Swimming", Slug = "swimming", Subtitle = "Goggles, Caps & Anti-Fog Gear", IconEmoji = "🏊", ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", DisplayOrder = 9, IsUploadingSoon = true, IsActive = true },
            new() { Id = "spc10", Name = "Sports & Kit Bags", Slug = "sports-bags", Subtitle = "Duffels, Racket Bags & Backpacks", IconEmoji = "🎒", ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", DisplayOrder = 10, IsUploadingSoon = true, IsActive = true },
            new() { Id = "spc11", Name = "Yoga & Recovery", Slug = "yoga-recovery", Subtitle = "Eco Mats, Foam Rollers & Straps", IconEmoji = "🧘", ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", DisplayOrder = 11, IsUploadingSoon = true, IsActive = true },
            new() { Id = "spc12", Name = "Outdoor & Trekking", Slug = "outdoor-trekking", Subtitle = "Hydration Packs, Compasses & Trek Sticks", IconEmoji = "🏕️", ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", DisplayOrder = 12, IsUploadingSoon = true, IsActive = true }
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
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Cricket",
                    TitleHighlight = "PRO TOURNAMENT SPEC • ENGLISH & KASHMIR WILLOW",
                    Subtitle = "Handcrafted cricket bats, lightweight batting pads, ventilated gloves, and leather match balls designed for college leagues.",
                    IsUploadingSoon = true,
                    ThemeColor = "#059669"
                },
                Products = new List<SportsProductItemDto>
                {
                    new() { Id = "cr1", Name = "Handcrafted Kashmir Willow Power Cricket Bat", CategoryName = "Cricket", CategorySlug = "cricket", Price = 1799, OriginalPrice = 3599, Rating = 4.8m, ReviewsCount = 142, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Short Handle", "Size 6" }, Colors = new List<string> { "Natural Wood" } },
                    new() { Id = "cr2", Name = "High-Density Foam Lightweight Batting Legguards", CategoryName = "Cricket", CategorySlug = "cricket", Price = 1499, OriginalPrice = 2999, Rating = 4.9m, ReviewsCount = 98, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Impact Shield", Sizes = new List<string> { "Mens Adult", "Youth" }, Colors = new List<string> { "White", "Navy" } },
                    new() { Id = "cr3", Name = "Ventilated Leather Batting Gloves (Pair)", CategoryName = "Cricket", CategorySlug = "cricket", Price = 899, OriginalPrice = 1799, Rating = 4.7m, ReviewsCount = 120, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Flex Grip", Sizes = new List<string> { "Adult", "Youth" }, Colors = new List<string> { "White/Green", "White/Blue" } },
                    new() { Id = "cr4", Name = "Four-Piece Alum-Tanned Leather Match Cricket Ball", CategoryName = "Cricket", CategorySlug = "cricket", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 165, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Match Ready", Sizes = new List<string> { "Standard 156g" }, Colors = new List<string> { "Red", "White", "Pink" } }
                }
            },
            // 2. Football
            new()
            {
                CategorySlug = "football",
                CategoryName = "Football",
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
                    new() { Id = "fb1", Name = "Thermal-Bonded All-Weather Match Football (Size 5)", CategoryName = "Football", CategorySlug = "football", Price = 1299, OriginalPrice = 2599, Rating = 4.9m, ReviewsCount = 210, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Size 5", "Size 4" }, Colors = new List<string> { "White/Blue", "Hi-Vis Yellow" } },
                    new() { Id = "fb2", Name = "Ergonomic Breathable Ankle Shin Guards", CategoryName = "Football", CategorySlug = "football", Price = 449, OriginalPrice = 899, Rating = 4.7m, ReviewsCount = 135, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Lightweight", Sizes = new List<string> { "S", "M", "L" }, Colors = new List<string> { "Black", "Red", "Blue" } },
                    new() { Id = "fb3", Name = "Latex Grip Pro Goalkeeper Gloves with Finger Spines", CategoryName = "Football", CategorySlug = "football", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 95, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Pro Grip", Sizes = new List<string> { "Size 8", "Size 9", "Size 10" }, Colors = new List<string> { "Neon Orange", "Black" } },
                    new() { Id = "fb4", Name = "Dual-Action Compact Ball Pump with Pressure Gauge", CategoryName = "Football", CategorySlug = "football", Price = 349, OriginalPrice = 699, Rating = 4.8m, ReviewsCount = 170, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Essential", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Black", "Blue" } }
                }
            },
            // 3. Badminton
            new()
            {
                CategorySlug = "badminton",
                CategoryName = "Badminton",
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
                    new() { Id = "bm1", Name = "Carbon-Graphite High-Tension Badminton Racket", CategoryName = "Badminton", CategorySlug = "badminton", Price = 1899, OriginalPrice = 3799, Rating = 4.9m, ReviewsCount = 280, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "4U / G5", "3U / G4" }, Colors = new List<string> { "Matte Black", "Ruby Red" } },
                    new() { Id = "bm2", Name = "Tournament Goose Feather Shuttlecocks (Tube of 12)", CategoryName = "Badminton", CategorySlug = "badminton", Price = 1099, OriginalPrice = 2199, Rating = 4.8m, ReviewsCount = 190, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "BWF Standard", Sizes = new List<string> { "Speed 77", "Speed 78" }, Colors = new List<string> { "White" } },
                    new() { Id = "bm3", Name = "Durable Synthetic Nylon Shuttlecocks (Pack of 6)", CategoryName = "Badminton", CategorySlug = "badminton", Price = 499, OriginalPrice = 999, Rating = 4.7m, ReviewsCount = 310, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Durable Play", Sizes = new List<string> { "Medium Speed" }, Colors = new List<string> { "Neon Yellow", "White" } },
                    new() { Id = "bm4", Name = "Anti-Slip Tacky Overgrips (Pack of 5)", CategoryName = "Badminton", CategorySlug = "badminton", Price = 299, OriginalPrice = 599, Rating = 4.9m, ReviewsCount = 240, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Sweat Absorbent", Sizes = new List<string> { "Universal" }, Colors = new List<string> { "Assorted", "Black", "White" } }
                }
            },
            // 4. Gym & Fitness
            new()
            {
                CategorySlug = "gym-fitness",
                CategoryName = "Gym & Fitness",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Gym & Fitness",
                    TitleHighlight = "COMPACT WORKOUT • MAXIMUM GAINS",
                    Subtitle = "Stackable resistance tubes, high-speed ball-bearing jump ropes, door pull-up bars, and push-up stands for dorm workouts.",
                    IsUploadingSoon = true,
                    ThemeColor = "#ea580c"
                },
                Products = new List<SportsProductItemDto>
                {
                    new() { Id = "gf1", Name = "11-Piece Stackable Resistance Bands Gym Set", CategoryName = "Gym & Fitness", CategorySlug = "gym-fitness", Price = 899, OriginalPrice = 1799, Rating = 4.9m, ReviewsCount = 380, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Up to 150 lbs" }, Colors = new List<string> { "Multi-Color" } },
                    new() { Id = "gf2", Name = "High-Speed 360° Ball-Bearing Aluminum Jump Rope", CategoryName = "Gym & Fitness", CategorySlug = "gym-fitness", Price = 449, OriginalPrice = 899, Rating = 4.8m, ReviewsCount = 210, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Cardio Pro", Sizes = new List<string> { "Adjustable 3m" }, Colors = new List<string> { "Black", "Red", "Blue" } },
                    new() { Id = "gf3", Name = "Heavy-Duty No-Screw Doorway Pull-Up Bar", CategoryName = "Gym & Fitness", CategorySlug = "gym-fitness", Price = 1399, OriginalPrice = 2799, Rating = 4.8m, ReviewsCount = 150, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Dorm Friendly", Sizes = new List<string> { "Adjustable 65-100cm" }, Colors = new List<string> { "Black/Red" } },
                    new() { Id = "gf4", Name = "Ergonomic Rotating Push-Up Handles with Non-Slip Base", CategoryName = "Gym & Fitness", CategorySlug = "gym-fitness", Price = 649, OriginalPrice = 1299, Rating = 4.7m, ReviewsCount = 120, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Wrist Safe", Sizes = new List<string> { "Pair" }, Colors = new List<string> { "Black" } }
                }
            },
            // 5. Basketball & Volleyball
            new()
            {
                CategorySlug = "basketball-volleyball",
                CategoryName = "Basketball & Volleyball",
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
                    new() { Id = "bv1", Name = "Deep-Channel Composite Leather Basketball (Size 7)", CategoryName = "Basketball & Volleyball", CategorySlug = "basketball-volleyball", Price = 1199, OriginalPrice = 2399, Rating = 4.9m, ReviewsCount = 180, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Size 7", "Size 6" }, Colors = new List<string> { "Classic Orange", "Black/Gold" } },
                    new() { Id = "bv2", Name = "Soft-Touch Waterproof 18-Panel Volleyball", CategoryName = "Basketball & Volleyball", CategorySlug = "basketball-volleyball", Price = 849, OriginalPrice = 1699, Rating = 4.8m, ReviewsCount = 130, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Soft Cushion", Sizes = new List<string> { "Size 5 Standard" }, Colors = new List<string> { "Blue/Yellow/White" } },
                    new() { Id = "bv3", Name = "Heavy-Duty All-Weather Basketball Net (Set of 2)", CategoryName = "Basketball & Volleyball", CategorySlug = "basketball-volleyball", Price = 299, OriginalPrice = 599, Rating = 4.7m, ReviewsCount = 95, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Weatherproof", Sizes = new List<string> { "12 Loops" }, Colors = new List<string> { "Red/White/Blue", "White" } },
                    new() { Id = "bv4", Name = "Padded Knee Pads for Volleyball & Court Sports", CategoryName = "Basketball & Volleyball", CategorySlug = "basketball-volleyball", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 110, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Impact Cushion", Sizes = new List<string> { "M", "L", "XL" }, Colors = new List<string> { "Black", "White" } }
                }
            },
            // 6. Tennis & Table Tennis
            new()
            {
                CategorySlug = "tennis-table-tennis",
                CategoryName = "Tennis & Table Tennis",
                HeroBanner = new SportsSubcategoryHeroDto
                {
                    Title = "Tennis & Table Tennis",
                    TitleHighlight = "SPIN & CONTROL • ITTF COMPLIANT",
                    Subtitle = "5-ply wood table tennis blades with inverted pimpled rubber, pressurized tennis balls, and aluminum composite court rackets.",
                    IsUploadingSoon = true,
                    ThemeColor = "#10b981"
                },
                Products = new List<SportsProductItemDto>
                {
                    new() { Id = "tt1", Name = "Tournament 5-Ply Table Tennis Racket with ITTF Rubber", CategoryName = "Tennis & Table Tennis", CategorySlug = "tennis-table-tennis", Price = 999, OriginalPrice = 1999, Rating = 4.8m, ReviewsCount = 145, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Flared Handle" }, Colors = new List<string> { "Red/Black" } },
                    new() { Id = "tt2", Name = "3-Star ITTF Approved Table Tennis Balls (Pack of 12)", CategoryName = "Tennis & Table Tennis", CategorySlug = "tennis-table-tennis", Price = 449, OriginalPrice = 899, Rating = 4.9m, ReviewsCount = 190, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "3-Star Pro", Sizes = new List<string> { "40mm+" }, Colors = new List<string> { "White", "Orange" } },
                    new() { Id = "tt3", Name = "Pressurized Championship Extra-Duty Tennis Balls (Can of 3)", CategoryName = "Tennis & Table Tennis", CategorySlug = "tennis-table-tennis", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 135, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "All Court", Sizes = new List<string> { "Can of 3" }, Colors = new List<string> { "Optic Yellow" } },
                    new() { Id = "tt4", Name = "Lightweight Graphite Composite Lawn Tennis Racket", CategoryName = "Tennis & Table Tennis", CategorySlug = "tennis-table-tennis", Price = 2499, OriginalPrice = 4999, Rating = 4.7m, ReviewsCount = 88, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Power Frame", Sizes = new List<string> { "Grip 2 (4 1/4)", "Grip 3 (4 3/8)" }, Colors = new List<string> { "Blue/Black" } }
                }
            },
            // 7. Sports Footwear
            new()
            {
                CategorySlug = "sports-footwear",
                CategoryName = "Sports Footwear",
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
                    new() { Id = "sf1", Name = "All-Weather High-Traction Multi-Ground Cleats", CategoryName = "Sports Footwear", CategorySlug = "sports-footwear", Price = 2199, OriginalPrice = 4399, Rating = 4.9m, ReviewsCount = 185, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "Neon/Black", "White/Gold" } },
                    new() { Id = "sf2", Name = "Non-Marking Gum Rubber Badminton Court Shoes", CategoryName = "Sports Footwear", CategorySlug = "sports-footwear", Price = 1799, OriginalPrice = 3499, Rating = 4.8m, ReviewsCount = 140, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Non-Marking", Sizes = new List<string> { "UK 6", "UK 7", "UK 8", "UK 9", "UK 10" }, Colors = new List<string> { "White/Blue", "Navy/Orange" } },
                    new() { Id = "sf3", Name = "Air-Cushioned Ultra-Lightweight Campus Running Shoes", CategoryName = "Sports Footwear", CategorySlug = "sports-footwear", Price = 1599, OriginalPrice = 3199, Rating = 4.8m, ReviewsCount = 220, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Cloud Cushion", Sizes = new List<string> { "UK 7", "UK 8", "UK 9", "UK 10", "UK 11", "UK 12" }, Colors = new List<string> { "All-Black", "Grey/Lime" } },
                    new() { Id = "sf4", Name = "Padded High-Top Ankle Support Basketball Shoes", CategoryName = "Sports Footwear", CategorySlug = "sports-footwear", Price = 2499, OriginalPrice = 4999, Rating = 4.7m, ReviewsCount = 95, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Ankle Lock", Sizes = new List<string> { "UK 8", "UK 9", "UK 10", "UK 11" }, Colors = new List<string> { "Red/Black", "White/Teal" } }
                }
            },
            // 8. Athletic Wear
            new()
            {
                CategorySlug = "athletic-wear",
                CategoryName = "Athletic Wear",
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
                    new() { Id = "aw1", Name = "Quick-Dry Moisture-Wicking Athletic Team Jersey", CategoryName = "Athletic Wear", CategorySlug = "athletic-wear", Price = 599, OriginalPrice = 1199, Rating = 4.9m, ReviewsCount = 230, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "S", "M", "L", "XL", "XXL" }, Colors = new List<string> { "Navy", "Black", "Royal Blue" } },
                    new() { Id = "aw2", Name = "Breathable Mesh Running Shorts with Zipper Pockets", CategoryName = "Athletic Wear", CategorySlug = "athletic-wear", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 175, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "With Pockets", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Black", "Charcoal", "Navy" } },
                    new() { Id = "aw3", Name = "Full-Sleeve Thermal Athletic Compression Top", CategoryName = "Athletic Wear", CategorySlug = "athletic-wear", Price = 749, OriginalPrice = 1499, Rating = 4.9m, ReviewsCount = 140, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Muscle Flex", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Black", "White" } },
                    new() { Id = "aw4", Name = "Tapered Ankle Lightweight Track Pants", CategoryName = "Athletic Wear", CategorySlug = "athletic-wear", Price = 849, OriginalPrice = 1699, Rating = 4.7m, ReviewsCount = 115, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Slim Fit", Sizes = new List<string> { "S", "M", "L", "XL" }, Colors = new List<string> { "Dark Grey", "Black" } }
                }
            },
            // 9. Swimming
            new()
            {
                CategorySlug = "swimming",
                CategoryName = "Swimming",
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
                    new() { Id = "sw1", Name = "Wide-Vision Mirrored Anti-Fog Swim Goggles", CategoryName = "Swimming", CategorySlug = "swimming", Price = 599, OriginalPrice = 1199, Rating = 4.9m, ReviewsCount = 195, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Adjustable Strap" }, Colors = new List<string> { "Mirror Black", "Blue Mirror" } },
                    new() { Id = "sw2", Name = "100% Silicone Ergonomic Waterproof Swim Cap", CategoryName = "Swimming", CategorySlug = "swimming", Price = 299, OriginalPrice = 599, Rating = 4.8m, ReviewsCount = 150, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Ear Protection", Sizes = new List<string> { "Universal Adult" }, Colors = new List<string> { "Black", "Navy", "Silver" } },
                    new() { Id = "sw3", Name = "Ultra-Compact Quick-Dry Microfiber Beach & Pool Towel", CategoryName = "Swimming", CategorySlug = "swimming", Price = 499, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 135, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Fast Dry", Sizes = new List<string> { "140 x 70 cm" }, Colors = new List<string> { "Ocean Blue", "Teal", "Grey" } },
                    new() { Id = "sw4", Name = "Soft Silicone Waterproof Earplugs & Nose Clip Case Set", CategoryName = "Swimming", CategorySlug = "swimming", Price = 249, OriginalPrice = 499, Rating = 4.6m, ReviewsCount = 88, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "With Case", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Clear Blue", "Black" } }
                }
            },
            // 10. Sports Bags
            new()
            {
                CategorySlug = "sports-bags",
                CategoryName = "Sports & Kit Bags",
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
                    new() { Id = "sb1", Name = "45L Water-Resistant Gym Duffel with Shoe Compartment", CategoryName = "Sports & Kit Bags", CategorySlug = "sports-bags", Price = 1199, OriginalPrice = 2399, Rating = 4.9m, ReviewsCount = 210, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "45 Litres" }, Colors = new List<string> { "Matte Black", "Army Green" } },
                    new() { Id = "sb2", Name = "Dual-Racket Badminton & Tennis Padded Backpack", CategoryName = "Sports & Kit Bags", CategorySlug = "sports-bags", Price = 1399, OriginalPrice = 2799, Rating = 4.8m, ReviewsCount = 140, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Racket Holder", Sizes = new List<string> { "35 Litres" }, Colors = new List<string> { "Navy/Red", "Black" } },
                    new() { Id = "sb3", Name = "Heavy-Duty Full Cricket Kit Bag with Wheels", CategoryName = "Sports & Kit Bags", CategorySlug = "sports-bags", Price = 2499, OriginalPrice = 4999, Rating = 4.9m, ReviewsCount = 92, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Tractor Wheels", Sizes = new List<string> { "Full Team Size" }, Colors = new List<string> { "Blue/Black" } },
                    new() { Id = "sb4", Name = "Waterproof Drawstring Sports Gym Sackpack", CategoryName = "Sports & Kit Bags", CategorySlug = "sports-bags", Price = 349, OriginalPrice = 699, Rating = 4.7m, ReviewsCount = 165, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Lightweight", Sizes = new List<string> { "Standard" }, Colors = new List<string> { "Black", "Royal Blue" } }
                }
            },
            // 11. Yoga & Recovery
            new()
            {
                CategorySlug = "yoga-recovery",
                CategoryName = "Yoga & Recovery",
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
                    new() { Id = "yr1", Name = "6mm Dual-Color Anti-Skid Eco TPE Yoga Mat with Carry Strap", CategoryName = "Yoga & Recovery", CategorySlug = "yoga-recovery", Price = 899, OriginalPrice = 1799, Rating = 4.9m, ReviewsCount = 260, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "6mm Extra Thick" }, Colors = new List<string> { "Teal/Grey", "Purple/Pink", "Blue/Black" } },
                    new() { Id = "yr2", Name = "High-Density Trigger Point Deep Tissue Foam Roller", CategoryName = "Yoga & Recovery", CategorySlug = "yoga-recovery", Price = 699, OriginalPrice = 1399, Rating = 4.8m, ReviewsCount = 180, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Muscle Recovery", Sizes = new List<string> { "33cm Standard" }, Colors = new List<string> { "Black", "Blue", "Orange" } },
                    new() { Id = "yr3", Name = "Firm Lacrosse Mobility Massage Ball for Back & Glutes", CategoryName = "Yoga & Recovery", CategorySlug = "yoga-recovery", Price = 249, OriginalPrice = 499, Rating = 4.7m, ReviewsCount = 110, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Targeted Relief", Sizes = new List<string> { "6.3cm" }, Colors = new List<string> { "Red", "Black" } },
                    new() { Id = "yr4", Name = "Multi-Loop Non-Elastic Physical Therapy Stretch Strap", CategoryName = "Yoga & Recovery", CategorySlug = "yoga-recovery", Price = 349, OriginalPrice = 699, Rating = 4.8m, ReviewsCount = 95, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Flexibility", Sizes = new List<string> { "12 Loops" }, Colors = new List<string> { "Black/Green" } }
                }
            },
            // 12. Outdoor & Trekking
            new()
            {
                CategorySlug = "outdoor-trekking",
                CategoryName = "Outdoor & Trekking",
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
                    new() { Id = "ot1", Name = "Aluminum Alloy Telescopic Shock-Absorbing Trekking Poles (Pair)", CategoryName = "Outdoor & Trekking", CategorySlug = "outdoor-trekking", Price = 1199, OriginalPrice = 2399, Rating = 4.8m, ReviewsCount = 145, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Best Seller", Sizes = new List<string> { "Adjustable 65-135cm" }, Colors = new List<string> { "Matte Black", "Cobalt Blue" } },
                    new() { Id = "ot2", Name = "2L BPA-Free Leakproof Hydration Pack Running Backpack", CategoryName = "Outdoor & Trekking", CategorySlug = "outdoor-trekking", Price = 999, OriginalPrice = 1999, Rating = 4.9m, ReviewsCount = 160, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Waterproof", Sizes = new List<string> { "2 Litre Bladder" }, Colors = new List<string> { "Black", "Orange" } },
                    new() { Id = "ot3", Name = "Rechargeable 800-Lumen Waterproof LED Headlamp", CategoryName = "Outdoor & Trekking", CategorySlug = "outdoor-trekking", Price = 599, OriginalPrice = 1199, Rating = 4.8m, ReviewsCount = 120, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Night Trail", Sizes = new List<string> { "Type-C USB" }, Colors = new List<string> { "Black" } },
                    new() { Id = "ot4", Name = "Thermal Emergency Mylar Foil Space Blankets (Pack of 4)", CategoryName = "Outdoor & Trekking", CategorySlug = "outdoor-trekking", Price = 299, OriginalPrice = 599, Rating = 4.7m, ReviewsCount = 85, ImageUrl = "/images/sports-hub/uploading-soon-placeholder.svg", IsUploadingSoon = true, Badge = "Safety Shield", Sizes = new List<string> { "210 x 130 cm" }, Colors = new List<string> { "Silver/Gold" } }
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
}

public sealed class SportsSubcategoryPageDto
{
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = "🏅";
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
