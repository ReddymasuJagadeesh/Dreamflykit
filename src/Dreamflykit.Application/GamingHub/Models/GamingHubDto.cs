namespace Dreamflykit.Application.GamingHub.Models;

public sealed class GamingHubDto
{
    public GamingHeroBannerDto HeroBanner { get; set; } = new();
    public GamingShopByCategorySectionDto ShopByCategory { get; set; } = new();
    public List<GamingSubcategoryPageDto> SubcategoryPages { get; set; } = new();
    public GamingTopPicksSectionDto TopPicks { get; set; } = new();
    public string ThreeSectionSliderSlug { get; set; } = "gaming-hub";

    public static GamingHubDto CreateDefault()
    {
        var dto = new GamingHubDto
        {
            HeroBanner = new GamingHeroBannerDto
            {
                IsEnabled = true,
                AutoPlay = true,
                SlideIntervalSeconds = 4,
                BannerHeight = 440,
                Slides = new List<GamingHeroSlideDto>
                {
                    new()
                    {
                        Id = "gaming-slide-1",
                        BadgeText = "NEXT-GEN CONSOLES • UP TO 40% OFF",
                        Title = "Power Your Play,",
                        TitleHighlight = "PlayStation 5 & Pro Ecosystem.",
                        Subtitle = "Experience lightning-fast loading with an ultra-high speed SSD, deeper immersion with haptic feedback & 4K 120FPS gaming.",
                        ImageUrl = "/images/gaming-hub/hero-slide-1.webp",
                        TargetUrl = "/category/gaming-hub/playstation",
                        ButtonText = "Explore PlayStation →",
                        DisplayOrder = 1,
                        IsActive = true,
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "gaming-slide-2",
                        BadgeText = "IMMERSIVE REALITY & RACING",
                        Title = "Next-Level VR Gaming &",
                        TitleHighlight = "Force Feedback Racing.",
                        Subtitle = "Step beyond reality with PlayStation VR2 & Meta Quest 3, plus direct-drive steering wheels for precision track dominance.",
                        ImageUrl = "/images/gaming-hub/hero-slide-2.webp",
                        TargetUrl = "/category/gaming-hub/vr-gaming",
                        ButtonText = "Discover VR & Simulators →",
                        DisplayOrder = 2,
                        IsActive = true,
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "gaming-slide-3",
                        BadgeText = "HANDHELD PC POWER • PLAY ANYWHERE",
                        Title = "Steam Deck OLED &",
                        TitleHighlight = "Asus ROG Ally Combos.",
                        Subtitle = "Take your entire Steam & Game Pass library on the go. High-refresh OLED displays, custom AMD APUs & ergonomic grips.",
                        ImageUrl = "/images/gaming-hub/hero-slide-3.webp",
                        TargetUrl = "/category/gaming-hub/steam-deck",
                        ButtonText = "Shop Handheld Gaming →",
                        DisplayOrder = 3,
                        IsActive = true,
                        IsUploadingSoon = true
                    }
                }
            },
            ShopByCategory = new GamingShopByCategorySectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                Subtitle = "Explore premier gaming consoles, controllers, retro arcades, VR and streaming gear",
                Items = CreateDefaultCategoryItems()
            },
            SubcategoryPages = CreateDefaultSubcategoryPages(),
            TopPicks = new GamingTopPicksSectionDto
            {
                IsEnabled = true,
                Title = "Top Gaming Picks for Students & Pros",
                Subtitle = "Verified 100% genuine consoles, certified open-box deals & gaming gear",
                Products = CreateDefaultTopProducts()
            }
        };

        return dto;
    }

    public static List<GamingCategoryItemDto> CreateDefaultCategoryItems()
    {
        // 22 Categories directly from user's Image 2 (EXCLUDING Mobile, Television, Camera)
        return new List<GamingCategoryItemDto>
        {
            new() { Id = "cat-ps", Name = "PlayStation", Slug = "playstation", Subtitle = "Consoles, Games & DualSense", IconEmoji = "🎮", ImageUrl = "/images/gaming-hub/cats/playstation.webp", IsUploadingSoon = true, DisplayOrder = 1 },
            new() { Id = "cat-xbox", Name = "Xbox", Slug = "xbox", Subtitle = "Series X|S & Game Pass", IconEmoji = "🟢", ImageUrl = "/images/gaming-hub/cats/xbox.webp", IsUploadingSoon = true, DisplayOrder = 2 },
            new() { Id = "cat-nintendo", Name = "Nintendo", Slug = "nintendo", Subtitle = "Switch OLED & Joy-Cons", IconEmoji = "🔴", ImageUrl = "/images/gaming-hub/cats/nintendo.webp", IsUploadingSoon = true, DisplayOrder = 3 },
            new() { Id = "cat-steamdeck", Name = "Steam Deck", Slug = "steam-deck", Subtitle = "Handheld PCs & Docks", IconEmoji = "🕹️", ImageUrl = "/images/gaming-hub/cats/steam-deck.webp", IsUploadingSoon = true, DisplayOrder = 4 },
            new() { Id = "cat-vr", Name = "VR Gaming", Slug = "vr-gaming", Subtitle = "PSVR2 & Meta Quest 3", IconEmoji = "🥽", ImageUrl = "/images/gaming-hub/cats/vr-gaming.webp", IsUploadingSoon = true, DisplayOrder = 5 },
            new() { Id = "cat-steering", Name = "Steering Wheel", Slug = "steering-wheels", Subtitle = "Racing Wheels & Pedals", IconEmoji = "🏎️", ImageUrl = "/images/gaming-hub/cats/steering-wheels.webp", IsUploadingSoon = true, DisplayOrder = 6 },
            new() { Id = "cat-preload", Name = "PreLoaded", Slug = "preloaded-consoles", Subtitle = "Ready-to-Play Classic Games", IconEmoji = "💾", ImageUrl = "/images/gaming-hub/cats/preloaded.webp", IsUploadingSoon = true, DisplayOrder = 7 },
            new() { Id = "cat-asus", Name = "Asus", Slug = "asus-rog", Subtitle = "ROG Ally & Laptops", IconEmoji = "⚡", ImageUrl = "/images/gaming-hub/cats/asus.webp", IsUploadingSoon = true, DisplayOrder = 8 },
            new() { Id = "cat-lenovo", Name = "Lenovo", Slug = "lenovo-legion", Subtitle = "Legion Go & Battlestations", IconEmoji = "💻", ImageUrl = "/images/gaming-hub/cats/lenovo.webp", IsUploadingSoon = true, DisplayOrder = 9 },
            new() { Id = "cat-retro", Name = "Retro Gaming & Arcade", Slug = "retro-gaming-arcade", Subtitle = "Cabinets & Vintage Sticks", IconEmoji = "👾", ImageUrl = "/images/gaming-hub/cats/retro-arcade.webp", IsUploadingSoon = true, DisplayOrder = 10 },
            new() { Id = "cat-projector", Name = "Game Projector", Slug = "game-projector", Subtitle = "Low-Latency 4K HDR", IconEmoji = "📽️", ImageUrl = "/images/gaming-hub/cats/game-projector.webp", IsUploadingSoon = true, DisplayOrder = 11 },
            new() { Id = "cat-openbox", Name = "Open Box", Slug = "open-box-gaming", Subtitle = "Certified Refurbished Deals", IconEmoji = "📦", ImageUrl = "/images/gaming-hub/cats/open-box.webp", IsUploadingSoon = true, DisplayOrder = 12 },
            new() { Id = "cat-drones", Name = "Drones", Slug = "drones", Subtitle = "FPV Racing & Controllers", IconEmoji = "🛸", ImageUrl = "/images/gaming-hub/cats/drones.webp", IsUploadingSoon = true, DisplayOrder = 13 },
            new() { Id = "cat-btspeaker", Name = "Bluetooth Speaker", Slug = "bluetooth-speaker", Subtitle = "RGB Soundbars & Bass", IconEmoji = "🔊", ImageUrl = "/images/gaming-hub/cats/speakers.webp", IsUploadingSoon = true, DisplayOrder = 14 },
            new() { Id = "cat-harddrive", Name = "Gaming Hard Drive", Slug = "gaming-hard-drive", Subtitle = "Gen4 NVMe & External SSDs", IconEmoji = "💽", ImageUrl = "/images/gaming-hub/cats/hard-drive.webp", IsUploadingSoon = true, DisplayOrder = 15 },
            new() { Id = "cat-motion", Name = "Motion Sensor", Slug = "motion-sensor", Subtitle = "Body Trackers & HD Cams", IconEmoji = "📡", ImageUrl = "/images/gaming-hub/cats/motion-sensor.webp", IsUploadingSoon = true, DisplayOrder = 16 },
            new() { Id = "cat-fitness", Name = "Fitness", Slug = "fitness-gaming", Subtitle = "Dance Mats & Sports Combos", IconEmoji = "🏃", ImageUrl = "/images/gaming-hub/cats/fitness.webp", IsUploadingSoon = true, DisplayOrder = 17 },
            new() { Id = "cat-mic", Name = "Microphone", Slug = "microphone", Subtitle = "Studio Stream & Podcasting", IconEmoji = "🎙️", ImageUrl = "/images/gaming-hub/cats/microphone.webp", IsUploadingSoon = true, DisplayOrder = 18 },
            new() { Id = "cat-controllers", Name = "Controllers and Gamepads", Slug = "controllers-and-gamepads", Subtitle = "Custom Grips & Wireless", IconEmoji = "🎮", ImageUrl = "/images/gaming-hub/cats/controllers.webp", IsUploadingSoon = true, DisplayOrder = 19 },
            new() { Id = "cat-psecosystem", Name = "Playstation Ecosystem", Slug = "playstation-ecosystem", Subtitle = "PSVR2, Headsets & Remotes", IconEmoji = "🎧", ImageUrl = "/images/gaming-hub/cats/ps-ecosystem.webp", IsUploadingSoon = true, DisplayOrder = 20 },
            new() { Id = "cat-xboxecosystem", Name = "Xbox Ecosystem", Slug = "xbox-ecosystem", Subtitle = "Wireless Adapters & Hubs", IconEmoji = "🔋", ImageUrl = "/images/gaming-hub/cats/xbox-ecosystem.webp", IsUploadingSoon = true, DisplayOrder = 21 },
            new() { Id = "cat-retrobudget", Name = "RETRO + BUDGET GAMING", Slug = "retro-budget-gaming", Subtitle = "Pocket Emulators & Combos", IconEmoji = "🕹️", ImageUrl = "/images/gaming-hub/cats/retro-budget.webp", IsUploadingSoon = true, DisplayOrder = 22 }
        };
    }

    public static List<GamingSubcategoryPageDto> CreateDefaultSubcategoryPages()
    {
        var items = CreateDefaultCategoryItems();
        var pages = new List<GamingSubcategoryPageDto>();

        foreach (var itm in items)
        {
            var page = new GamingSubcategoryPageDto
            {
                CategorySlug = itm.Slug,
                CategoryName = itm.Name,
                IconEmoji = itm.IconEmoji,
                HeroBanner = new GamingSubcategoryHeroDto
                {
                    IsEnabled = true,
                    Title = itm.Name,
                    TitleHighlight = "Gaming Store",
                    Subtitle = $"Discover the best {itm.Name} deals, certified genuine bundles, low-latency performance and student warranty.",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    BannerHeight = 260
                },
                FilterTags = new List<string> { "Condition", "Compatibility", "Brand", "Storage" },
                Products = GenerateSampleProductsForCategory(itm.Slug, itm.Name)
            };
            pages.Add(page);
        }

        return pages;
    }

    private static List<GamingProductItemDto> GenerateSampleProductsForCategory(string slug, string name)
    {
        if (slug == "playstation" || slug == "playstation-ecosystem")
        {
            return new List<GamingProductItemDto>
            {
                new()
                {
                    Id = "ps5-yotei",
                    Title = "Brand New Sony PlayStation 5 Ghost of Yotei Limited Edition Bundle (1TB SSD)",
                    Brand = "Sony",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹99,999",
                    OriginalPriceText = "₹1,06,380",
                    DiscountPercent = "6% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sony-ps5-ghost-of-yotei-edition"
                },
                new()
                {
                    Id = "ps4-sega",
                    Title = "Sega Mega Drive Classics For PS4 & PS5 Backward Compatible",
                    Brand = "Sega",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹1,180",
                    OriginalPriceText = "₹2,999",
                    DiscountPercent = "61% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sega-mega-drive-classics-ps4"
                },
                new()
                {
                    Id = "psvr2-openbox",
                    Title = "Open Box(New) • Sony PlayStation VR2 - PSVR2 Headset + Sense Controllers",
                    Brand = "Sony",
                    ConditionBadge = "Open Box",
                    AvailabilityStatus = "In stock",
                    Rating = "4.7",
                    PriceText = "₹41,990",
                    OriginalPriceText = "₹48,260",
                    DiscountPercent = "13% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sony-psvr2-open-box-certified"
                },
                new()
                {
                    Id = "ps5-pro-white",
                    Title = "Brand New Sony PlayStation 5 • PS5 Pro White 2TB Enhanced GPU Edition",
                    Brand = "Sony",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "5.0",
                    PriceText = "₹1,69,999",
                    OriginalPriceText = "₹1,84,999",
                    DiscountPercent = "8% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sony-ps5-pro-white-2tb"
                },
                new()
                {
                    Id = "ps5-dualsense",
                    Title = "Sony PlayStation DualSense Wireless Controller - Midnight Black Haptic",
                    Brand = "Sony",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹5,499",
                    OriginalPriceText = "₹6,490",
                    DiscountPercent = "15% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sony-dualsense-wireless-controller"
                },
                new()
                {
                    Id = "ps5-slim-digital",
                    Title = "Open Box Sony PlayStation 5 Slim Digital Edition (1TB SSD, 120Hz)",
                    Brand = "Sony",
                    ConditionBadge = "Open Box",
                    AvailabilityStatus = "In stock",
                    Rating = "4.7",
                    PriceText = "₹39,990",
                    OriginalPriceText = "₹44,990",
                    DiscountPercent = "11% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sony-ps5-slim-digital-openbox"
                }
            };
        }

        // Generic samples for other categories
        return new List<GamingProductItemDto>
        {
            new()
            {
                Id = $"{slug}-prod-1",
                Title = $"Brand New {name} Ultimate Edition Gaming Bundle",
                Brand = name,
                ConditionBadge = "Brand New",
                AvailabilityStatus = "In stock",
                Rating = "4.9",
                PriceText = "₹54,990",
                OriginalPriceText = "₹69,990",
                DiscountPercent = "21% off",
                ImageUrl = "",
                IsUploadingSoon = true,
                Slug = $"{slug}-ultimate-bundle"
            },
            new()
            {
                Id = $"{slug}-prod-2",
                Title = $"Open Box(New) {name} Pro Performance Edition",
                Brand = name,
                ConditionBadge = "Open Box",
                AvailabilityStatus = "In stock",
                Rating = "4.7",
                PriceText = "₹36,499",
                OriginalPriceText = "₹48,990",
                DiscountPercent = "25% off",
                ImageUrl = "",
                IsUploadingSoon = true,
                Slug = $"{slug}-pro-openbox"
            },
            new()
            {
                Id = $"{slug}-prod-3",
                Title = $"{name} High-Speed Wireless Expansion Kit",
                Brand = name,
                ConditionBadge = "Brand New",
                AvailabilityStatus = "In stock",
                Rating = "4.8",
                PriceText = "₹4,299",
                OriginalPriceText = "₹7,999",
                DiscountPercent = "46% off",
                ImageUrl = "",
                IsUploadingSoon = true,
                Slug = $"{slug}-expansion-kit"
            },
            new()
            {
                Id = $"{slug}-prod-4",
                Title = $"{name} Tournament Grade Dual Controller Pack",
                Brand = name,
                ConditionBadge = "Brand New",
                AvailabilityStatus = "In stock",
                Rating = "4.9",
                PriceText = "₹8,990",
                OriginalPriceText = "₹12,490",
                DiscountPercent = "28% off",
                ImageUrl = "",
                IsUploadingSoon = true,
                Slug = $"{slug}-dual-pack"
            }
        };
    }

    public static List<GamingProductItemDto> CreateDefaultTopProducts()
    {
        return GenerateSampleProductsForCategory("playstation", "PlayStation");
    }
}

public sealed class GamingHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public bool AutoPlay { get; set; } = true;
    public int SlideIntervalSeconds { get; set; } = 4;
    public int BannerHeight { get; set; } = 440;
    public List<GamingHeroSlideDto> Slides { get; set; } = new();
}

public sealed class GamingHeroSlideDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string BadgeText { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = string.Empty;
    public string ButtonText { get; set; } = "Shop Now →";
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
    public bool IsUploadingSoon { get; set; } = true;
}

public sealed class GamingShopByCategorySectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Shop by Category";
    public string Subtitle { get; set; } = "Consoles, VR, Racing Wheels, Arcade, Handheld PCs & Gaming Ecosystem";
    public List<GamingCategoryItemDto> Items { get; set; } = new();
}

public sealed class GamingCategoryItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = "🎮";
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
}

public sealed class GamingSubcategoryPageDto
{
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = "🎮";
    public GamingSubcategoryHeroDto HeroBanner { get; set; } = new();
    public List<string> FilterTags { get; set; } = new() { "Condition", "Compatibility", "Brand", "Storage" };
    public List<GamingProductItemDto> Products { get; set; } = new();
}

public sealed class GamingSubcategoryHeroDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public int BannerHeight { get; set; } = 260;
}

public sealed class GamingProductItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string ConditionBadge { get; set; } = "Brand New"; // "Brand New", "Open Box", "Refurbished"
    public string AvailabilityStatus { get; set; } = "In stock";
    public string Rating { get; set; } = "4.8";
    public string PriceText { get; set; } = string.Empty;
    public string OriginalPriceText { get; set; } = string.Empty;
    public string DiscountPercent { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public string Slug { get; set; } = string.Empty;
    public bool IsInStock => !string.Equals(AvailabilityStatus, "Out of Stock", StringComparison.OrdinalIgnoreCase);
}

public sealed class GamingTopPicksSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Top Gaming Picks for Students & Pros";
    public string Subtitle { get; set; } = "Verified 100% genuine consoles, certified open-box deals & gaming gear";
    public List<GamingProductItemDto> Products { get; set; } = new();
}

