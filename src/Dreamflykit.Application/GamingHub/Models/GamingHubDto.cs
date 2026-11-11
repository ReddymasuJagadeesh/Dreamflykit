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
        return new List<GamingCategoryItemDto>
        {
            new()
            {
                Id = "cat-ps",
                Name = "PlayStation",
                Slug = "playstation",
                Subtitle = "Consoles, Games & DualSense",
                IconEmoji = "🎮",
                ImageUrl = "/images/gaming-hub/cats/playstation.webp",
                IsUploadingSoon = true,
                DisplayOrder = 1,
                Subcategories = new List<string>
                {
                    "PS5 Consoles (Disc & Digital)",
                    "DualSense Wireless Controllers & Docks",
                    "PlayStation Exclusive Game Discs",
                    "PULSE 3D & Elite Wireless Headsets",
                    "PlayStation Portal Remote Player",
                    "PSVR2 & Sense Controllers",
                    "Console Covers & Custom Faceplates",
                    "Gen4 M.2 NVMe SSD Storage Expansion"
                },
                ImportantNote = "All PlayStation consoles and DualSense controllers are 100% genuine Indian/Global units with official warranty, verified serial numbers, and fast delivery to campus."
            },
            new()
            {
                Id = "cat-xbox",
                Name = "Xbox",
                Slug = "xbox",
                Subtitle = "Series X|S & Game Pass",
                IconEmoji = "🟢",
                ImageUrl = "/images/gaming-hub/cats/xbox.webp",
                IsUploadingSoon = true,
                DisplayOrder = 2,
                Subcategories = new List<string>
                {
                    "Xbox Series X & Series S Consoles",
                    "Xbox Wireless Controllers & Battery Packs",
                    "Xbox Game Pass Ultimate Cards",
                    "Seagate & WD_BLACK Storage Expansion Cards",
                    "Xbox Stereo & Wireless Gaming Headsets",
                    "Thumbstick Grips & Custom Shells"
                },
                ImportantNote = "Xbox Series X|S consoles support 120Hz gaming, Quick Resume, and Dolby Atmos. Official wireless controllers work across Xbox, Windows PC, iOS and Android."
            },
            new()
            {
                Id = "cat-nintendo",
                Name = "Nintendo",
                Slug = "nintendo",
                Subtitle = "Switch OLED & Joy-Cons",
                IconEmoji = "🔴",
                ImageUrl = "/images/gaming-hub/cats/nintendo.webp",
                IsUploadingSoon = true,
                DisplayOrder = 3,
                Subcategories = new List<string>
                {
                    "Nintendo Switch OLED & Lite Consoles",
                    "Joy-Con Controller Pairs & Charging Grips",
                    "Switch Pro Wireless Controllers",
                    "Physical Game Cartridges & Pokémon Editions",
                    "Hardshell Carrying Cases & Tempered Glass",
                    "High-Speed MicroSDXC Gaming Cards"
                },
                ImportantNote = "Switch OLED features a vibrant 7-inch OLED screen, 64GB internal storage, and enhanced audio. High-end MicroSD cards (UHS-I A2) are recommended for digital games."
            },
            new()
            {
                Id = "cat-steamdeck",
                Name = "Steam Deck",
                Slug = "steam-deck",
                Subtitle = "Handheld PCs & Docks",
                IconEmoji = "🕹️",
                ImageUrl = "/images/gaming-hub/cats/steam-deck.webp",
                IsUploadingSoon = true,
                DisplayOrder = 4,
                Subcategories = new List<string>
                {
                    "Steam Deck OLED & LCD Handhelds",
                    "Official USB-C Multi-Port Docking Stations",
                    "Protective Armor Cases, Skins & Grips",
                    "Anti-Glare Etched Screen Protectors",
                    "Fast 65W GaN Power Banks & Chargers",
                    "High-Speed 2230 NVMe Internal SSD Upgrades"
                },
                ImportantNote = "Steam Deck handhelds run SteamOS with verified badge compatibility for PC games. GaN 65W chargers ensure rapid charging during high-demand gameplay."
            },
            new()
            {
                Id = "cat-vr",
                Name = "VR Gaming",
                Slug = "vr-gaming",
                Subtitle = "PSVR2 & Meta Quest 3",
                IconEmoji = "🥽",
                ImageUrl = "/images/gaming-hub/cats/vr-gaming.webp",
                IsUploadingSoon = true,
                DisplayOrder = 5,
                Subcategories = new List<string>
                {
                    "Meta Quest 3 & 3S All-In-One VR Headsets",
                    "PlayStation VR2 Systems & PC Adapters",
                    "Magnetic VR Prescription Lens Inserts",
                    "Elite Head Straps with Extended Battery",
                    "Silicone Face Covers & Anti-Fog Lens Protectors",
                    "High-Speed Fiber-Optic Link Cables (5M)"
                },
                ImportantNote = "VR headsets feature pancake optics and room-scale spatial audio. Ensure adequate play area (2m x 2m) and clean lenses with microfiber cloths only."
            },
            new()
            {
                Id = "cat-steering",
                Name = "Steering Wheel",
                Slug = "steering-wheels",
                Subtitle = "Racing Wheels & Pedals",
                IconEmoji = "🏎️",
                ImageUrl = "/images/gaming-hub/cats/steering-wheels.webp",
                IsUploadingSoon = true,
                DisplayOrder = 6,
                Subcategories = new List<string>
                {
                    "Force Feedback Racing Wheels & 3-Pedal Sets",
                    "Direct Drive Wheel Bases (5Nm - 11Nm)",
                    "H-Pattern Gear Shifters & Progressive Handbrakes",
                    "Foldable Wheel Stands & Sim Cockpits",
                    "Heavy-Duty Desk Clamps & Mounting Brackets"
                },
                ImportantNote = "Dual-motor force feedback wheels simulate real tire traction and road surface. Compatible with PlayStation, PC, and Xbox sim racing titles."
            },
            new()
            {
                Id = "cat-preload",
                Name = "PreLoaded",
                Slug = "preloaded-consoles",
                Subtitle = "Ready-to-Play Classic Games",
                IconEmoji = "💾",
                ImageUrl = "/images/gaming-hub/cats/preloaded.webp",
                IsUploadingSoon = true,
                DisplayOrder = 7,
                Subcategories = new List<string>
                {
                    "Plug-and-Play 4K HDMI Retro Sticks",
                    "Preloaded Classic Handheld Consoles",
                    "Wireless 2.4G Dual Arcade Gamepad Packs",
                    "High-Speed Preconfigured Retro MicroSD Cards",
                    "Multi-System Pandora Box Arcade Cabinets"
                },
                ImportantNote = "Plug-and-play systems come pre-configured with 10,000+ classic arcade and retro titles. Connect directly to HDMI on TV, monitor, or projector."
            },
            new()
            {
                Id = "cat-asus",
                Name = "Asus",
                Slug = "asus-rog",
                Subtitle = "ROG Ally & Laptops",
                IconEmoji = "⚡",
                ImageUrl = "/images/gaming-hub/cats/asus.webp",
                IsUploadingSoon = true,
                DisplayOrder = 8,
                Subcategories = new List<string>
                {
                    "ROG Ally & Ally X Handheld Gaming PCs",
                    "ROG Strix & Zephyrus Gaming Laptops",
                    "ROG Chakram & Keris Wireless Gaming Mice",
                    "ROG Falchion 65% Mechanical Keyboards",
                    "ROG Delta S Hi-Res Gaming Headsets",
                    "ROG 140W GaN Travel Chargers & Hubs"
                },
                ImportantNote = "ROG Ally features AMD Ryzen Z1 Extreme processor and 120Hz FreeSync Premium display with full Windows 11 game compatibility."
            },
            new()
            {
                Id = "cat-lenovo",
                Name = "Lenovo",
                Slug = "lenovo-legion",
                Subtitle = "Legion Go & Battlestations",
                IconEmoji = "💻",
                ImageUrl = "/images/gaming-hub/cats/lenovo.webp",
                IsUploadingSoon = true,
                DisplayOrder = 9,
                Subcategories = new List<string>
                {
                    "Legion Go Handheld Gaming PCs",
                    "Legion Pro 5 & Pro 7 Gaming Battlestations",
                    "Detachable TrueStrike Wireless Controllers",
                    "Legion High-Performance Mechanical Keyboards",
                    "Legion True Wireless Low-Latency Gaming Earbuds",
                    "Legion Armor Backpacks & Protective Sleeves"
                },
                ImportantNote = "Legion Go boasts an 8.8-inch QHD+ 144Hz display with detachable FPS-mode controllers and dual USB4 high-speed connectivity."
            },
            new()
            {
                Id = "cat-retro",
                Name = "Retro Gaming & Arcade",
                Slug = "retro-gaming-arcade",
                Subtitle = "Cabinets & Vintage Sticks",
                IconEmoji = "👾",
                ImageUrl = "/images/gaming-hub/cats/retro-arcade.webp",
                IsUploadingSoon = true,
                DisplayOrder = 10,
                Subcategories = new List<string>
                {
                    "Bartop & 2-Player Full-Size Arcade Cabinets",
                    "NeoGeo & Mini Classic Nostalgia Consoles",
                    "Sanwa Arcade Joysticks & Sanwa Buttons",
                    "RetroTink & HDMI Video Upscalers",
                    "Vintage CRT-Compatible Composite Adapters"
                },
                ImportantNote = "Authentic micro-switches and Japanese Sanwa parts deliver genuine zero-latency arcade responsiveness for fighting games and shmups."
            },
            new()
            {
                Id = "cat-projector",
                Name = "Game Projector",
                Slug = "game-projector",
                Subtitle = "Low-Latency 4K HDR",
                IconEmoji = "📽️",
                ImageUrl = "/images/gaming-hub/cats/game-projector.webp",
                IsUploadingSoon = true,
                DisplayOrder = 11,
                Subcategories = new List<string>
                {
                    "Ultra-Low Input Lag 4K HDR Gaming Projectors",
                    "High-Refresh 1080p 240Hz Esports Projectors",
                    "Ambient Light Rejecting (ALR) Portable Screens",
                    "Ceiling Mount Brackets & Ultra-High Speed HDMI 2.1 Cables",
                    "Smart Portable LED Projectors with Built-in Android"
                },
                ImportantNote = "Gaming projectors offer sub-16ms latency at 4K 60Hz and 4ms at 1080p 240Hz, turning any dorm room wall into a 120-inch gaming arena."
            },
            new()
            {
                Id = "cat-openbox",
                Name = "Open Box",
                Slug = "open-box-gaming",
                Subtitle = "Certified Refurbished Deals",
                IconEmoji = "📦",
                ImageUrl = "/images/gaming-hub/cats/open-box.webp",
                IsUploadingSoon = true,
                DisplayOrder = 12,
                Subcategories = new List<string>
                {
                    "Certified Open-Box PS5 Disc & Digital Consoles",
                    "Certified Open-Box Xbox Series X & S Systems",
                    "Open-Box Nintendo Switch OLED Editions",
                    "Inspected Steam Deck & Handheld PCs",
                    "Open-Box DualSense & Elite Controllers"
                },
                ImportantNote = "Every Open-Box device undergoes a rigorous 42-point diagnostic check, comes in complete packaging with all original accessories and verified 1-year warranty."
            },
            new()
            {
                Id = "cat-drones",
                Name = "Drones",
                Slug = "drones",
                Subtitle = "FPV Racing & Controllers",
                IconEmoji = "🛸",
                ImageUrl = "/images/gaming-hub/cats/drones.webp",
                IsUploadingSoon = true,
                DisplayOrder = 13,
                Subcategories = new List<string>
                {
                    "FPV Racing Drones & High-Definition Goggles",
                    "Long-Range ELRS Radio Controllers & Transmitters",
                    "High-Discharge LiPo Batteries & Smart Balance Chargers",
                    "Carbon Fiber Drone Frames & Brushless Motors",
                    "Tri-Blade Propellers & Prop Guards"
                },
                ImportantNote = "Equipped with digital low-latency video transmission. Always check campus outdoor drone policies and fly in designated recreational zones."
            },
            new()
            {
                Id = "cat-btspeaker",
                Name = "Bluetooth Speaker",
                Slug = "bluetooth-speaker",
                Subtitle = "RGB Soundbars & Bass",
                IconEmoji = "🔊",
                ImageUrl = "/images/gaming-hub/cats/speakers.webp",
                IsUploadingSoon = true,
                DisplayOrder = 14,
                Subcategories = new List<string>
                {
                    "Desktop RGB Soundbars with Subwoofer",
                    "Portable Heavy-Bass Water-Resistant Party Speakers",
                    "Dual Wireless Stereo Pairing Speakers",
                    "Ultra-Low Latency Bluetooth 5.3 Audio Adapters",
                    "Magnetic Clamp Soundbars for Gaming Monitors"
                },
                ImportantNote = "Low-latency aptX Adaptive and DSP sound tuning ensure zero audio lag between gunfire/explosions and speaker playback."
            },
            new()
            {
                Id = "cat-harddrive",
                Name = "Gaming Hard Drive",
                Slug = "gaming-hard-drive",
                Subtitle = "Gen4 NVMe & External SSDs",
                IconEmoji = "💽",
                ImageUrl = "/images/gaming-hub/cats/hard-drive.webp",
                IsUploadingSoon = true,
                DisplayOrder = 15,
                Subcategories = new List<string>
                {
                    "PCIe 4.0 NVMe M.2 SSDs (7000+ MB/s Read)",
                    "Graphene & Aluminum SSD Heatsinks with Thermal Pads",
                    "Rugged USB 3.2 Gen2 External Portable SSDs",
                    "High-Capacity 2TB - 8TB External Console Backup Drives",
                    "Tool-Free Aluminum NVMe Enclosures (10Gbps)"
                },
                ImportantNote = "PS5 requires PCIe 4.0 M.2 NVMe SSDs with at least 5500 MB/s sequential read speed and an aluminum heatsink with max 11.25mm thickness."
            },
            new()
            {
                Id = "cat-motion",
                Name = "Motion Sensor",
                Slug = "motion-sensor",
                Subtitle = "Body Trackers & HD Cams",
                IconEmoji = "📡",
                ImageUrl = "/images/gaming-hub/cats/motion-sensor.webp",
                IsUploadingSoon = true,
                DisplayOrder = 16,
                Subcategories = new List<string>
                {
                    "Full-Body VR Tracking Pucks & Elastic Straps",
                    "Ultra-Wide Angle 1080p Streaming Webcams",
                    "SteamVR Room-Scale Lighthouse Base Stations 2.0",
                    "Gesture & Hand-Tracking Optical Sensor Modules",
                    "Motion Tracking Gloves with Haptic Feedback"
                },
                ImportantNote = "Lighthouse tracking systems require line-of-sight base stations mounted at corner heights for millimeter-accurate 360-degree tracking."
            },
            new()
            {
                Id = "cat-fitness",
                Name = "Fitness",
                Slug = "fitness-gaming",
                Subtitle = "Dance Mats & Sports Combos",
                IconEmoji = "🏃",
                ImageUrl = "/images/gaming-hub/cats/fitness.webp",
                IsUploadingSoon = true,
                DisplayOrder = 17,
                Subcategories = new List<string>
                {
                    "Resistance Fitness Rings & Leg Strap Sets",
                    "Thick Non-Slip Electronic USB Dance Mats",
                    "VR Weighted Controller Grips for Rhythm Games",
                    "Interactive Motion Sensor Boxing Gloves",
                    "Sweat-Resistant Silicone Controller Grip Covers"
                },
                ImportantNote = "Dorm-friendly high-density vibration dampening mats reduce floor impact and noise while gaming or training in college residences."
            },
            new()
            {
                Id = "cat-mic",
                Name = "Microphone",
                Slug = "microphone",
                Subtitle = "Studio Stream & Podcasting",
                IconEmoji = "🎙️",
                ImageUrl = "/images/gaming-hub/cats/microphone.webp",
                IsUploadingSoon = true,
                DisplayOrder = 18,
                Subcategories = new List<string>
                {
                    "Cardioid USB Condenser Studio Microphones",
                    "Dynamic Broadcast Streamer Mics (XLR/USB)",
                    "Heavy-Duty Desk Boom Arms with Cable Management",
                    "Dual-Layer Metal Mesh Pop Filters & Shock Mounts",
                    "Acoustic Foam Desktop Isolation Shields"
                },
                ImportantNote = "Cardioid pickup patterns isolate voice while suppressing mechanical keyboard clicks and PC fan noise during Discord chats and streaming."
            },
            new()
            {
                Id = "cat-controllers",
                Name = "Controllers and Gamepads",
                Slug = "controllers-and-gamepads",
                Subtitle = "Custom Grips & Wireless",
                IconEmoji = "🎮",
                ImageUrl = "/images/gaming-hub/cats/controllers.webp",
                IsUploadingSoon = true,
                DisplayOrder = 19,
                Subcategories = new List<string>
                {
                    "Pro Programmable Back-Paddle Wireless Controllers",
                    "Hall-Effect Magnetic Stick Controllers (Anti-Drift)",
                    "Multi-Platform Wireless Gamepads (PC/Switch/Android)",
                    "Arcade Fighting Game Hitbox & Leverless Controllers",
                    "Silicone Ergonomic Controller Grips & Thumb Caps"
                },
                ImportantNote = "Hall-Effect magnetic joysticks use contactless magnetic sensors to eliminate stick drift permanently and provide sub-millimeter precision."
            },
            new()
            {
                Id = "cat-psecosystem",
                Name = "Playstation Ecosystem",
                Slug = "playstation-ecosystem",
                Subtitle = "PSVR2, Headsets & Remotes",
                IconEmoji = "🎧",
                ImageUrl = "/images/gaming-hub/cats/ps-ecosystem.webp",
                IsUploadingSoon = true,
                DisplayOrder = 20,
                Subcategories = new List<string>
                {
                    "PlayStation Media Remotes & HD Streaming Cameras",
                    "Vertical Cooling Stands with Fast Dual Docks",
                    "Official PS5 Deluxe Travel Backpacks & Hard Cases",
                    "Pulse Elite Wireless Headsets with Planar Drivers",
                    "PlayStation Portal Handheld Protective Covers"
                },
                ImportantNote = "Engineered specifically for PlayStation 5 and PS4 ecosystem synergy with seamless console integration and native UI battery indicators."
            },
            new()
            {
                Id = "cat-xboxecosystem",
                Name = "Xbox Ecosystem",
                Slug = "xbox-ecosystem",
                Subtitle = "Wireless Adapters & Hubs",
                IconEmoji = "🔋",
                ImageUrl = "/images/gaming-hub/cats/xbox-ecosystem.webp",
                IsUploadingSoon = true,
                DisplayOrder = 21,
                Subcategories = new List<string>
                {
                    "Xbox Wireless USB Adapters for Windows PC",
                    "Play & Charge Kits with Braided USB-C Cables",
                    "Magnetic Quick-Charging Controller Stands",
                    "Official Xbox Series X Carrying Cases",
                    "Xbox Media Remotes & Controller Chatpads"
                },
                ImportantNote = "Xbox Wireless Adapter enables low-latency lossless wireless audio and multi-controller connections without Bluetooth lag on PC."
            },
            new()
            {
                Id = "cat-retrobudget",
                Name = "RETRO + BUDGET GAMING",
                Slug = "retro-budget-gaming",
                Subtitle = "Pocket Emulators & Combos",
                IconEmoji = "🕹️",
                ImageUrl = "/images/gaming-hub/cats/retro-budget.webp",
                IsUploadingSoon = true,
                DisplayOrder = 22,
                Subcategories = new List<string>
                {
                    "Pocket Linux Handheld Emulators (Anbernic/Miyoo)",
                    "4K HDMI Wireless Retro Sticks with Dual Pads",
                    "Classic USB PC/Raspberry Pi Gamepads",
                    "8-Bit & 16-Bit Nostalgia Kits & Cartridge Replicas",
                    "Protective EVA Zipper Travel Cases for Handhelds"
                },
                ImportantNote = "Budget-friendly handhelds run open-source Linux retro engines supporting thousands of games with bright IPS screens and long battery life."
            }
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
                Subcategories = new List<string>(itm.Subcategories),
                ImportantNote = itm.ImportantNote,
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
                Products = GenerateSampleProductsForCategory(itm.Slug, itm.Name, itm.Subcategories)
            };
            pages.Add(page);
        }

        return pages;
    }

    public static List<GamingProductItemDto> GenerateSampleProductsForCategory(string slug, string name, List<string>? subcategories = null)
    {
        var subs = subcategories ?? new List<string>();
        string GetSub(int index, string fallback) => subs.Count > index ? subs[index] : fallback;

        return slug switch
        {
            "playstation" => new List<GamingProductItemDto>
            {
                new()
                {
                    Id = "ps5-yotei",
                    Title = "Brand New Sony PlayStation 5 Ghost of Yotei Limited Edition Bundle (1TB SSD)",
                    Brand = "Sony",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "5.0",
                    PriceText = "₹99,999",
                    OriginalPriceText = "₹1,06,380",
                    DiscountPercent = "6% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sony-ps5-ghost-of-yotei-edition",
                    SubcategoryTag = GetSub(0, "PS5 Consoles (Disc & Digital)"),
                    Description = "Custom limited edition PS5 console with ultra-fast 1TB NVMe SSD, Ray Tracing, 4K 120Hz output and DualSense controller."
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
                    Slug = "sony-ps5-pro-white-2tb",
                    SubcategoryTag = GetSub(0, "PS5 Consoles (Disc & Digital)"),
                    Description = "Enhanced GPU architecture with advanced Ray Tracing, PlayStation Spectral Super Resolution (PSSR), and 2TB high-speed storage."
                },
                new()
                {
                    Id = "ps5-dualsense",
                    Title = "Sony PlayStation DualSense Wireless Controller - Midnight Black Haptic",
                    Brand = "Sony",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹5,499",
                    OriginalPriceText = "₹6,490",
                    DiscountPercent = "15% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sony-dualsense-wireless-controller",
                    SubcategoryTag = GetSub(1, "DualSense Wireless Controllers & Docks"),
                    Description = "Immersive haptic feedback, dynamic adaptive triggers, built-in microphone and motion sensor."
                },
                new()
                {
                    Id = "psvr2-openbox",
                    Title = "Open Box(New) • Sony PlayStation VR2 - PSVR2 Headset + Sense Controllers",
                    Brand = "Sony",
                    ConditionBadge = "Open Box",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹41,990",
                    OriginalPriceText = "₹48,260",
                    DiscountPercent = "13% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sony-psvr2-open-box-certified",
                    SubcategoryTag = GetSub(5, "PSVR2 & Sense Controllers"),
                    Description = "4K HDR OLED displays, 110-degree field of view, eye tracking, headset feedback, and 3D Audio immersion."
                },
                new()
                {
                    Id = "ps-portal",
                    Title = "PlayStation Portal Remote Player for PS5 Console",
                    Brand = "Sony",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹18,990",
                    OriginalPriceText = "₹21,990",
                    DiscountPercent = "14% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sony-playstation-portal-remote",
                    SubcategoryTag = GetSub(4, "PlayStation Portal Remote Player"),
                    Description = "Stream your PS5 games over home Wi-Fi on an 8-inch 1080p 60fps screen with integrated DualSense features."
                },
                new()
                {
                    Id = "ps-pulse-elite",
                    Title = "PULSE Elite Wireless Headset with Planar Magnetic Drivers",
                    Brand = "Sony",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹12,490",
                    OriginalPriceText = "₹14,990",
                    DiscountPercent = "17% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sony-pulse-elite-wireless-headset",
                    SubcategoryTag = GetSub(3, "PULSE 3D & Elite Wireless Headsets"),
                    Description = "Studio-inspired planar magnetic drivers, retractable boom mic with AI noise rejection, and PlayStation Link ultra-low latency."
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
                    Slug = "sega-mega-drive-classics-ps4",
                    SubcategoryTag = GetSub(2, "PlayStation Exclusive Game Discs"),
                    Description = "Over 50 retro classics optimized for PS4 & PS5 with save states, rewinds, and online multiplayer."
                },
                new()
                {
                    Id = "ps5-ssd-corsair",
                    Title = "Corsair MP600 PRO LPX 2TB Gen4 M.2 NVMe SSD for PS5",
                    Brand = "Corsair",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹14,990",
                    OriginalPriceText = "₹21,990",
                    DiscountPercent = "32% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "corsair-mp600-pro-lpx-2tb-ps5",
                    SubcategoryTag = GetSub(7, "Gen4 M.2 NVMe SSD Storage Expansion"),
                    Description = "Pre-installed low-profile aluminum heatsink, 7,100 MB/s sequential read speed exceeding Sony specifications."
                }
            },

            "xbox" => new List<GamingProductItemDto>
            {
                new()
                {
                    Id = "xbx-series-x",
                    Title = "Microsoft Xbox Series X 1TB Console (Carbon Black)",
                    Brand = "Microsoft",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹48,990",
                    OriginalPriceText = "₹55,990",
                    DiscountPercent = "13% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "xbox-series-x-1tb-console",
                    SubcategoryTag = GetSub(0, "Xbox Series X & Series S Consoles"),
                    Description = "True 4K gaming, 12 teraflops GPU processing power, DirectX Raytracing, and 1TB Custom NVMe SSD."
                },
                new()
                {
                    Id = "xbx-series-s",
                    Title = "Microsoft Xbox Series S 512GB All-Digital Console (Robot White)",
                    Brand = "Microsoft",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹29,990",
                    OriginalPriceText = "₹34,990",
                    DiscountPercent = "14% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "xbox-series-s-512gb-console",
                    SubcategoryTag = GetSub(0, "Xbox Series X & Series S Consoles"),
                    Description = "Next-gen performance in the smallest Xbox ever. 120FPS gaming, Quick Resume, and all-digital library."
                },
                new()
                {
                    Id = "xbx-controller-white",
                    Title = "Xbox Wireless Controller - Robot White with Textured Grip",
                    Brand = "Microsoft",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹4,990",
                    OriginalPriceText = "₹5,990",
                    DiscountPercent = "17% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "xbox-wireless-controller-white",
                    SubcategoryTag = GetSub(1, "Xbox Wireless Controllers & Battery Packs"),
                    Description = "Hybrid D-pad, textured grips on triggers and bumpers, seamless switching between Xbox, PC, and Mobile."
                },
                new()
                {
                    Id = "xbx-seagate-card",
                    Title = "Seagate 1TB Storage Expansion Card for Xbox Series X|S",
                    Brand = "Seagate",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹15,499",
                    OriginalPriceText = "₹20,999",
                    DiscountPercent = "26% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "seagate-1tb-expansion-card-xbox",
                    SubcategoryTag = GetSub(3, "Seagate & WD_BLACK Storage Expansion Cards"),
                    Description = "Seamlessly replicates Xbox Velocity Architecture performance with zero lag for Xbox Series X|S games."
                },
                new()
                {
                    Id = "xbx-game-pass",
                    Title = "Xbox Game Pass Ultimate 12-Month Membership Card",
                    Brand = "Microsoft",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "5.0",
                    PriceText = "₹6,499",
                    OriginalPriceText = "₹8,999",
                    DiscountPercent = "28% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "xbox-game-pass-ultimate-12m",
                    SubcategoryTag = GetSub(2, "Xbox Game Pass Ultimate Cards"),
                    Description = "Access 400+ high-quality console and PC games, EA Play membership, and online cloud gaming."
                },
                new()
                {
                    Id = "xbx-headset-wireless",
                    Title = "Official Xbox Wireless Headset with Spatial Sound & Dolby Atmos",
                    Brand = "Microsoft",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹8,990",
                    OriginalPriceText = "₹10,990",
                    DiscountPercent = "18% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "xbox-wireless-headset-official",
                    SubcategoryTag = GetSub(4, "Xbox Stereo & Wireless Gaming Headsets"),
                    Description = "Direct pairing without dongle, rotating earcup dials for volume and game/chat balance, Windows Sonic support."
                }
            },

            "nintendo" => new List<GamingProductItemDto>
            {
                new()
                {
                    Id = "nin-switch-oled",
                    Title = "Nintendo Switch OLED Model - Neon Blue & Neon Red",
                    Brand = "Nintendo",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹27,990",
                    OriginalPriceText = "₹32,990",
                    DiscountPercent = "15% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "nintendo-switch-oled-neon",
                    SubcategoryTag = GetSub(0, "Nintendo Switch OLED & Lite Consoles"),
                    Description = "7-inch vibrant OLED screen, wide adjustable stand, wired LAN port dock, and 64GB internal storage."
                },
                new()
                {
                    Id = "nin-switch-lite",
                    Title = "Nintendo Switch Lite Handheld Console - Coral Edition",
                    Brand = "Nintendo",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹16,490",
                    OriginalPriceText = "₹19,990",
                    DiscountPercent = "18% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "nintendo-switch-lite-coral",
                    SubcategoryTag = GetSub(0, "Nintendo Switch OLED & Lite Consoles"),
                    Description = "Compact, lightweight handheld-only system with integrated controls, ideal for dorm and campus travel."
                },
                new()
                {
                    Id = "nin-pro-controller",
                    Title = "Official Nintendo Switch Pro Controller (Wireless)",
                    Brand = "Nintendo",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹5,990",
                    OriginalPriceText = "₹7,490",
                    DiscountPercent = "20% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "nintendo-switch-pro-controller",
                    SubcategoryTag = GetSub(2, "Switch Pro Wireless Controllers"),
                    Description = "Premium grip, HD rumble, motion controls, built-in amiibo NFC, and 40-hour rechargeable battery."
                },
                new()
                {
                    Id = "nin-joy-con-pair",
                    Title = "Nintendo Joy-Con Controller Pair (Pastel Purple / Pastel Green)",
                    Brand = "Nintendo",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹6,990",
                    OriginalPriceText = "₹8,490",
                    DiscountPercent = "18% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "nintendo-joy-con-pair-pastel",
                    SubcategoryTag = GetSub(1, "Joy-Con Controller Pairs & Charging Grips"),
                    Description = "Independent versatile motion-sensing Joy-Cons with HD rumble, IR motion camera, and wrist straps."
                },
                new()
                {
                    Id = "nin-zelda-totk",
                    Title = "The Legend of Zelda: Tears of the Kingdom (Physical Cartridge)",
                    Brand = "Nintendo",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "5.0",
                    PriceText = "₹4,290",
                    OriginalPriceText = "₹4,999",
                    DiscountPercent = "14% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "zelda-tears-of-the-kingdom-switch",
                    SubcategoryTag = GetSub(3, "Physical Game Cartridges & Pokémon Editions"),
                    Description = "Epic adventure across the land and skies of Hyrule with groundbreaking physics crafting and exploration."
                },
                new()
                {
                    Id = "nin-microsd-sandisk",
                    Title = "SanDisk 256GB MicroSDXC Card for Nintendo Switch (100MB/s)",
                    Brand = "SanDisk",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹2,190",
                    OriginalPriceText = "₹3,999",
                    DiscountPercent = "45% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sandisk-256gb-microsd-switch",
                    SubcategoryTag = GetSub(5, "High-Speed MicroSDXC Gaming Cards"),
                    Description = "Officially licensed high-speed storage card for Nintendo Switch to store dozens of digital games and DLCs."
                }
            },

            "steam-deck" => new List<GamingProductItemDto>
            {
                new()
                {
                    Id = "sd-oled-512",
                    Title = "Valve Steam Deck OLED 512GB Handheld Gaming PC",
                    Brand = "Valve",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "5.0",
                    PriceText = "₹56,990",
                    OriginalPriceText = "₹64,990",
                    DiscountPercent = "12% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "steam-deck-oled-512gb",
                    SubcategoryTag = GetSub(0, "Steam Deck OLED & LCD Handhelds"),
                    Description = "7.4-inch 90Hz HDR OLED display, custom 6nm AMD APU, 50Wh battery, Wi-Fi 6E, and ultra-ergonomic controls."
                },
                new()
                {
                    Id = "sd-lcd-256",
                    Title = "Valve Steam Deck LCD 256GB Handheld Console",
                    Brand = "Valve",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹41,990",
                    OriginalPriceText = "₹49,990",
                    DiscountPercent = "16% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "steam-deck-lcd-256gb",
                    SubcategoryTag = GetSub(0, "Steam Deck OLED & LCD Handhelds"),
                    Description = "Crisp 7-inch display, 256GB NVMe SSD, optical trackpads, dual analog sticks, and full Steam library access."
                },
                new()
                {
                    Id = "sd-official-dock",
                    Title = "Official Steam Deck Docking Station (DisplayPort, HDMI 2.0, Gigabit LAN)",
                    Brand = "Valve",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹7,990",
                    OriginalPriceText = "₹9,990",
                    DiscountPercent = "20% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "official-steam-deck-docking-station",
                    SubcategoryTag = GetSub(1, "Official USB-C Multi-Port Docking Stations"),
                    Description = "Connect your Steam Deck to any TV or dual-monitor setup with Gigabit Ethernet, 3x USB-A 3.1, and 65W passthrough."
                },
                new()
                {
                    Id = "sd-modcase",
                    Title = "JSAUX ModCase Protective Armor Kit with Kickstand & Silicone Strap",
                    Brand = "JSAUX",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹2,790",
                    OriginalPriceText = "₹3,990",
                    DiscountPercent = "30% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "jsaux-modcase-steam-deck",
                    SubcategoryTag = GetSub(2, "Protective Armor Cases, Skins & Grips"),
                    Description = "Shockproof hard protective shell with detachable front cover and modular mounting bracket for power banks."
                },
                new()
                {
                    Id = "sd-baseus-65w",
                    Title = "Baseus 65W 20,000mAh Power Bank with Built-in Fast USB-C Cable",
                    Brand = "Baseus",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹4,490",
                    OriginalPriceText = "₹6,990",
                    DiscountPercent = "36% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "baseus-65w-power-bank-steam-deck",
                    SubcategoryTag = GetSub(4, "Fast 65W GaN Power Banks & Chargers"),
                    Description = "Power Delivery 65W fast-charging power bank capable of charging Steam Deck from 0% to 100% in 90 minutes."
                },
                new()
                {
                    Id = "sd-crucial-p310",
                    Title = "Crucial P310 1TB 2230 M.2 PCIe Gen4 NVMe Internal SSD for Steam Deck",
                    Brand = "Crucial",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹9,490",
                    OriginalPriceText = "₹13,990",
                    DiscountPercent = "32% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "crucial-p310-1tb-2230-ssd",
                    SubcategoryTag = GetSub(5, "High-Speed 2230 NVMe Internal SSD Upgrades"),
                    Description = "Compact 2230 form factor, up to 7,100 MB/s read speeds, low power draw for maximum handheld battery longevity."
                }
            },

            "vr-gaming" => new List<GamingProductItemDto>
            {
                new()
                {
                    Id = "vr-quest-3",
                    Title = "Meta Quest 3 512GB Breakthrough Mixed Reality VR Headset",
                    Brand = "Meta",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "5.0",
                    PriceText = "₹62,990",
                    OriginalPriceText = "₹72,990",
                    DiscountPercent = "14% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "meta-quest-3-512gb",
                    SubcategoryTag = GetSub(0, "Meta Quest 3 & 3S All-In-One VR Headsets"),
                    Description = "Next-gen Snapdragon XR2 Gen 2, 4K+ Infinite Display, color passthrough mixed reality, and Touch Plus controllers."
                },
                new()
                {
                    Id = "vr-quest-3s",
                    Title = "Meta Quest 3S 128GB All-In-One VR Headset Bundle",
                    Brand = "Meta",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹34,990",
                    OriginalPriceText = "₹39,990",
                    DiscountPercent = "13% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "meta-quest-3s-128gb",
                    SubcategoryTag = GetSub(0, "Meta Quest 3 & 3S All-In-One VR Headsets"),
                    Description = "Affordable VR with Quest 3 mixed reality power, Batman: Arkham Shadow bundle, and wireless freedom."
                },
                new()
                {
                    Id = "vr-psvr2-adapter",
                    Title = "Sony PlayStation VR2 PC Adapter (Official)",
                    Brand = "Sony",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹5,990",
                    OriginalPriceText = "₹7,490",
                    DiscountPercent = "20% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "sony-psvr2-pc-adapter",
                    SubcategoryTag = GetSub(1, "PlayStation VR2 Systems & PC Adapters"),
                    Description = "Unlock thousands of SteamVR titles on your gaming PC using your existing PSVR2 headset."
                },
                new()
                {
                    Id = "vr-bobovr-m3",
                    Title = "BOBOVR M3 Pro Battery Strap for Meta Quest 3",
                    Brand = "BOBOVR",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹4,990",
                    OriginalPriceText = "₹6,990",
                    DiscountPercent = "29% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "bobovr-m3-pro-battery-strap",
                    SubcategoryTag = GetSub(3, "Elite Head Straps with Extended Battery"),
                    Description = "Ergonomic halo design with hot-swappable 5200mAh magnetic battery pack extending playtime by 3 hours."
                },
                new()
                {
                    Id = "vr-kiwi-link",
                    Title = "KIWI design 5M High-Speed USB-C to USB-C VR Link Cable (5Gbps)",
                    Brand = "KIWI design",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹2,290",
                    OriginalPriceText = "₹3,490",
                    DiscountPercent = "34% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "kiwi-design-vr-link-cable-5m",
                    SubcategoryTag = GetSub(5, "High-Speed Fiber-Optic Link Cables (5M)"),
                    Description = "Flexible fiber-optic core with 90-degree connector and simultaneous fast charging during PC VR gaming."
                }
            },

            "steering-wheels" => new List<GamingProductItemDto>
            {
                new()
                {
                    Id = "st-logitech-g29",
                    Title = "Logitech G29 Driving Force Racing Wheel and Floor Pedals",
                    Brand = "Logitech G",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹28,990",
                    OriginalPriceText = "₹36,995",
                    DiscountPercent = "22% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "logitech-g29-driving-force",
                    SubcategoryTag = GetSub(0, "Force Feedback Racing Wheels & 3-Pedal Sets"),
                    Description = "Dual-motor force feedback, hand-stitched leather wheel, helical gearing, and responsive stainless steel pedals."
                },
                new()
                {
                    Id = "st-thrustmaster-t300",
                    Title = "Thrustmaster T300 RS GT Edition Racing Wheel (PS5 / PS4 / PC)",
                    Brand = "Thrustmaster",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹44,990",
                    OriginalPriceText = "₹52,990",
                    DiscountPercent = "15% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "thrustmaster-t300-rs-gt",
                    SubcategoryTag = GetSub(0, "Force Feedback Racing Wheels & 3-Pedal Sets"),
                    Description = "1080-degree dual-belt brushless force feedback system with HallEffect AccuRate technology and T3PA-GT pedal set."
                },
                new()
                {
                    Id = "st-fanatec-csl-dd",
                    Title = "Fanatec CSL DD Direct Drive Wheel Base (8Nm Boost Kit)",
                    Brand = "Fanatec",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "5.0",
                    PriceText = "₹59,990",
                    OriginalPriceText = "₹69,990",
                    DiscountPercent = "14% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "fanatec-csl-dd-8nm",
                    SubcategoryTag = GetSub(1, "Direct Drive Wheel Bases (5Nm - 11Nm)"),
                    Description = "Instant direct drive response without belts or gears, ultra-realistic road feel, and automotive-grade quick release."
                },
                new()
                {
                    Id = "st-logitech-shifter",
                    Title = "Logitech G Driving Force 6-Speed Shifter with Push-Down Reverse",
                    Brand = "Logitech G",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹4,990",
                    OriginalPriceText = "₹6,495",
                    DiscountPercent = "23% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "logitech-g-driving-force-shifter",
                    SubcategoryTag = GetSub(2, "H-Pattern Gear Shifters & Progressive Handbrakes"),
                    Description = "Solid steel gear shaft with leather boot and knob, built-in clamps for sturdy table mounting."
                },
                new()
                {
                    Id = "st-foldable-stand",
                    Title = "Next Level Racing Wheel Stand 2.0 (Foldable with Shifter Mount)",
                    Brand = "Next Level Racing",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹19,990",
                    OriginalPriceText = "₹24,990",
                    DiscountPercent = "20% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "next-level-racing-wheel-stand-2",
                    SubcategoryTag = GetSub(3, "Foldable Wheel Stands & Sim Cockpits"),
                    Description = "Heavy-duty carbon steel frame with gaming chair cradle plate and full height and angle adjustability."
                }
            },

            "preloaded-consoles" => new List<GamingProductItemDto>
            {
                new()
                {
                    Id = "pre-game-stick-4k",
                    Title = "Wireless 4K Ultra HD HDMI Retro Game Stick with 20,000+ Games & Dual 2.4G Pads",
                    Brand = "GameBox",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹2,499",
                    OriginalPriceText = "₹4,999",
                    DiscountPercent = "50% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "wireless-4k-hdmi-retro-stick",
                    SubcategoryTag = GetSub(0, "Plug-and-Play 4K HDMI Retro Sticks"),
                    Description = "Plug directly into TV HDMI. Preloaded with Arcade, PS1, SNES, Genesis, and GBA classics with save states."
                },
                new()
                {
                    Id = "pre-pandora-box",
                    Title = "Pandora Box 3D 2-Player Arcade Console (8,000 Built-in Arcade Games)",
                    Brand = "Pandora",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹13,990",
                    OriginalPriceText = "₹18,990",
                    DiscountPercent = "26% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "pandora-box-3d-arcade-console",
                    SubcategoryTag = GetSub(4, "Multi-System Pandora Box Arcade Cabinets"),
                    Description = "Heavy metal chassis with illuminated Sanwa-style joysticks, built-in speakers, and full HD HDMI output."
                },
                new()
                {
                    Id = "pre-handheld-retro",
                    Title = "R36S Open Source Linux Portable Retro Gaming Handheld (64GB)",
                    Brand = "RetroCraft",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹3,999",
                    OriginalPriceText = "₹5,999",
                    DiscountPercent = "33% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "r36s-portable-retro-handheld",
                    SubcategoryTag = GetSub(1, "Preloaded Classic Handheld Consoles"),
                    Description = "3.5-inch 640x480 IPS screen, dual Hall joysticks, 3500mAh battery, and ArkOS support for 20+ emulators."
                },
                new()
                {
                    Id = "pre-dual-pads-pack",
                    Title = "Dual 2.4G Wireless Arcade Gamepads Pack with USB Dongle",
                    Brand = "RetroGamer",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.7",
                    PriceText = "₹1,499",
                    OriginalPriceText = "₹2,499",
                    DiscountPercent = "40% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "dual-wireless-arcade-pads-pack",
                    SubcategoryTag = GetSub(2, "Wireless 2.4G Dual Arcade Gamepad Packs"),
                    Description = "Ergonomic SNES-style wireless controllers with zero-delay transmission for plug-and-play game sticks."
                }
            },

            "asus-rog" => new List<GamingProductItemDto>
            {
                new()
                {
                    Id = "asus-ally-x",
                    Title = "ASUS ROG Ally X Handheld Gaming PC (AMD Ryzen Z1 Extreme, 24GB RAM, 1TB SSD)",
                    Brand = "ASUS ROG",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "5.0",
                    PriceText = "₹89,990",
                    OriginalPriceText = "₹99,990",
                    DiscountPercent = "10% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "asus-rog-ally-x-1tb",
                    SubcategoryTag = GetSub(0, "ROG Ally & Ally X Handheld Gaming PCs"),
                    Description = "Upgraded 80Wh battery, dual USB-C ports with Thunderbolt 4, improved ergonomics, and 120Hz 7-inch FHD display."
                },
                new()
                {
                    Id = "asus-ally-z1e",
                    Title = "ASUS ROG Ally Handheld Gaming PC (AMD Ryzen Z1 Extreme, 512GB SSD)",
                    Brand = "ASUS ROG",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹54,990",
                    OriginalPriceText = "₹69,990",
                    DiscountPercent = "21% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "asus-rog-ally-z1-extreme",
                    SubcategoryTag = GetSub(0, "ROG Ally & Ally X Handheld Gaming PCs"),
                    Description = "Windows 11 handheld with AMD RDNA 3 graphics, Armoury Crate SE, and dual front-firing smart amp speakers."
                },
                new()
                {
                    Id = "asus-falchion",
                    Title = "ASUS ROG Falchion 65% Wireless RGB Mechanical Gaming Keyboard (Cherry MX Red)",
                    Brand = "ASUS ROG",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹9,990",
                    OriginalPriceText = "₹14,999",
                    DiscountPercent = "33% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "asus-rog-falchion-wireless",
                    SubcategoryTag = GetSub(3, "ROG Falchion 65% Mechanical Keyboards"),
                    Description = "Interactive touch panel, dual-mode connectivity, up to 450 hours battery life, and polycarbonate keyboard cover."
                },
                new()
                {
                    Id = "asus-chakram",
                    Title = "ASUS ROG Chakram X Wireless RGB Gaming Mouse (36,000 DPI, Programmable Joystick)",
                    Brand = "ASUS ROG",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹11,490",
                    OriginalPriceText = "₹15,999",
                    DiscountPercent = "28% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "asus-rog-chakram-x-mouse",
                    SubcategoryTag = GetSub(2, "ROG Chakram & Keris Wireless Gaming Mice"),
                    Description = "ROG AimPoint optical sensor, tri-mode connectivity, push-fit switch sockets, and built-in analog thumb joystick."
                }
            },

            "lenovo-legion" => new List<GamingProductItemDto>
            {
                new()
                {
                    Id = "len-legion-go",
                    Title = "Lenovo Legion Go 8.8-inch QHD+ 144Hz Handheld Gaming PC (AMD Ryzen Z1 Extreme, 16GB, 512GB)",
                    Brand = "Lenovo",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹69,990",
                    OriginalPriceText = "₹79,990",
                    DiscountPercent = "13% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "lenovo-legion-go-handheld",
                    SubcategoryTag = GetSub(0, "Legion Go Handheld Gaming PCs"),
                    Description = "Massive 8.8-inch 2560x1600 144Hz display, detachable TrueStrike controllers with FPS mouse mode and kickstand."
                },
                new()
                {
                    Id = "len-legion-pro5",
                    Title = "Lenovo Legion Pro 5 Gen 9 Battlestation Laptop (Intel Core i9, RTX 4070 8GB, 32GB RAM)",
                    Brand = "Lenovo",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "5.0",
                    PriceText = "₹1,54,990",
                    OriginalPriceText = "₹1,79,990",
                    DiscountPercent = "14% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "lenovo-legion-pro-5-rtx4070",
                    SubcategoryTag = GetSub(1, "Legion Pro 5 & Pro 7 Gaming Battlestations"),
                    Description = "Coldfront 5.0 vapor chamber cooling, 16-inch WQXGA 240Hz display with Legion AI Engine+ optimization."
                },
                new()
                {
                    Id = "len-legion-earbuds",
                    Title = "Lenovo Legion True Wireless Gaming Earbuds with Ultra-Low Latency USB-C Dongle",
                    Brand = "Lenovo",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹4,990",
                    OriginalPriceText = "₹7,990",
                    DiscountPercent = "38% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "lenovo-legion-wireless-earbuds",
                    SubcategoryTag = GetSub(4, "Legion True Wireless Low-Latency Gaming Earbuds"),
                    Description = "Dual-mode Bluetooth 5.3 & 2.4GHz wireless dongle delivering sub-25ms audio synchronization for esports."
                },
                new()
                {
                    Id = "len-legion-backpack",
                    Title = "Lenovo Legion Armored 17-inch Waterproof Gaming Backpack",
                    Brand = "Lenovo",
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹3,490",
                    OriginalPriceText = "₹5,490",
                    DiscountPercent = "36% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = "lenovo-legion-armored-backpack",
                    SubcategoryTag = GetSub(5, "Legion Armor Backpacks & Protective Sleeves"),
                    Description = "Molded EVA hard front shield, dedicated padded console/laptop compartments, and weather-resistant zippers."
                }
            },

            _ => new List<GamingProductItemDto>
            {
                new()
                {
                    Id = $"{slug}-prod-1",
                    Title = $"Pro Series {name} Tournament Hardware Edition",
                    Brand = name,
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹24,990",
                    OriginalPriceText = "₹29,990",
                    DiscountPercent = "17% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = $"{slug}-tournament-edition",
                    SubcategoryTag = GetSub(0, $"{name} Premium Gear"),
                    Description = $"Engineered for elite performance with genuine components, zero-latency responsiveness and campus warranty."
                },
                new()
                {
                    Id = $"{slug}-prod-2",
                    Title = $"Ultra Performance {name} Expansion & Accessory Kit",
                    Brand = name,
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹7,490",
                    OriginalPriceText = "₹9,990",
                    DiscountPercent = "25% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = $"{slug}-accessory-kit",
                    SubcategoryTag = GetSub(1, $"{name} Accessories"),
                    Description = $"High-durability accessory pack tailored for student dorm battlestations with seamless plug-and-play capability."
                },
                new()
                {
                    Id = $"{slug}-prod-3",
                    Title = $"Certified Open-Box {name} Pro Pack with Full Warranty",
                    Brand = name,
                    ConditionBadge = "Open Box",
                    AvailabilityStatus = "In stock",
                    Rating = "4.8",
                    PriceText = "₹16,990",
                    OriginalPriceText = "₹22,990",
                    DiscountPercent = "26% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = $"{slug}-openbox-pro",
                    SubcategoryTag = GetSub(2, $"{name} Refurbished & Open Box"),
                    Description = $"Inspected 42-point certified open-box deal in immaculate condition with complete original accessories."
                },
                new()
                {
                    Id = $"{slug}-prod-4",
                    Title = $"Essential {name} Student Gamer Bundle",
                    Brand = name,
                    ConditionBadge = "Brand New",
                    AvailabilityStatus = "In stock",
                    Rating = "4.9",
                    PriceText = "₹3,990",
                    OriginalPriceText = "₹5,490",
                    DiscountPercent = "27% off",
                    ImageUrl = "",
                    IsUploadingSoon = true,
                    Slug = $"{slug}-student-bundle",
                    SubcategoryTag = GetSub(0, $"{name} Student Kit"),
                    Description = $"Best value starter gear for university dorm rooms. Verified durable build and easy setup."
                }
            }
        };
    }

    public static List<GamingProductItemDto> CreateDefaultTopProducts()
    {
        return GenerateSampleProductsForCategory("playstation", "PlayStation", CreateDefaultCategoryItems().First().Subcategories);
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
    public List<string> Subcategories { get; set; } = new();
    public string? ImportantNote { get; set; }
}

public sealed class GamingSubcategoryPageDto
{
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = "🎮";
    public GamingSubcategoryHeroDto HeroBanner { get; set; } = new();
    public List<string> Subcategories { get; set; } = new();
    public string? ImportantNote { get; set; }
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
    public string SubcategoryTag { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsInStock => !string.Equals(AvailabilityStatus, "Out of Stock", StringComparison.OrdinalIgnoreCase);

    public decimal NumericPrice
    {
        get
        {
            if (string.IsNullOrWhiteSpace(PriceText)) return 0m;
            var clean = new string(PriceText.Where(char.IsDigit).ToArray());
            return decimal.TryParse(clean, out var val) ? val : 0m;
        }
    }

    public decimal NumericOriginalPrice
    {
        get
        {
            if (string.IsNullOrWhiteSpace(OriginalPriceText)) return NumericPrice > 0 ? Math.Round(NumericPrice * 1.25m, 0) : 0m;
            var clean = new string(OriginalPriceText.Where(char.IsDigit).ToArray());
            return decimal.TryParse(clean, out var val) ? val : (NumericPrice > 0 ? Math.Round(NumericPrice * 1.25m, 0) : 0m);
        }
    }

    public decimal NumericDiscount
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(DiscountPercent))
            {
                var clean = new string(DiscountPercent.Where(char.IsDigit).ToArray());
                if (decimal.TryParse(clean, out var d)) return d;
            }
            if (NumericOriginalPrice > NumericPrice && NumericOriginalPrice > 0)
            {
                return Math.Round(((NumericOriginalPrice - NumericPrice) / NumericOriginalPrice) * 100, 0);
            }
            return 0m;
        }
    }
}

public sealed class GamingTopPicksSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Top Gaming Picks for Students & Pros";
    public string Subtitle { get; set; } = "Verified 100% genuine consoles, certified open-box deals & gaming gear";
    public List<GamingProductItemDto> Products { get; set; } = new();
}
