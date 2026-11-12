using Dreamflykit.Application.MobilesHub.Models;

namespace Dreamflykit.Application.MobilesHub.Services;

/// <summary>
/// Researched brand catalog provider for all 18 smartphone brands under Mobiles Hub.
/// Each brand features bespoke hero theming, domain-accurate left-sidebar filters (with More Filters),
/// and verified campus-ready smartphone listings.
/// </summary>
public static class MobileBrandCatalogService
{
    public static string NormalizeBrandSlug(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return string.Empty;
        var norm = raw.Trim().ToLowerInvariant().Replace("_", "-").Replace(" ", "-");
        return norm switch
        {
            "iphone" or "apple" => "apple",
            "google" or "pixel" or "google-pixel" => "google-pixel",
            "ai" or "ai+" or "ai-plus" or "aiplus" => "ai-plus",
            "moto" or "motorola" => "motorola",
            "mi" or "redmi" => "redmi",
            "fire-boltt" or "fireboltt" or "boltt" => "boltt",
            _ => norm
        };
    }

    public static List<MobileBrandPageDto> CreateDefaultBrandPages()
    {
        var slugs = new[]
        {
            "apple", "motorola", "realme", "poco", "google-pixel", "hmd",
            "boltt", "mivi", "tecno", "samsung", "vivo", "oppo",
            "ai-plus", "nothing", "infinix", "lava", "redmi", "oneplus"
        };

        return slugs.Select(BuildBrandPage).ToList();
    }

    public static MobileBrandShowcaseData GetBrandShowcase(string brandSlug, MobilesHubDto? config = null)
    {
        var normSlug = NormalizeBrandSlug(brandSlug);
        var hub = config ?? MobilesHubDto.CreateDefault();
        var allBrands = hub.BrandsSection?.Brands ?? MobilesHubDto.CreateDefault().BrandsSection.Brands;

        var brandItem = allBrands.FirstOrDefault(b =>
            string.Equals(NormalizeBrandSlug(b.Slug), normSlug, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(NormalizeBrandSlug(b.Name), normSlug, StringComparison.OrdinalIgnoreCase));

        var customPage = hub.BrandPages?.FirstOrDefault(p =>
            string.Equals(NormalizeBrandSlug(p.BrandSlug), normSlug, StringComparison.OrdinalIgnoreCase));

        var page = customPage ?? BuildBrandPage(normSlug);
        if (brandItem != null && string.IsNullOrWhiteSpace(page.BrandName))
        {
            page.BrandName = brandItem.Name;
        }

        // Ensure filters and products are populated if a partial/legacy record existed
        if (page.FilterGroups == null || page.FilterGroups.Count == 0)
        {
            var fallback = BuildBrandPage(normSlug);
            page.FilterGroups = fallback.FilterGroups;
        }
        if (page.Products == null || page.Products.Count == 0)
        {
            var fallback = BuildBrandPage(normSlug);
            page.Products = fallback.Products;
        }

        return new MobileBrandShowcaseData
        {
            BrandSlug = page.BrandSlug,
            BrandName = page.BrandName,
            BrandItem = brandItem,
            Page = page,
            AllBrands = allBrands
        };
    }

    private static MobileBrandPageDto BuildBrandPage(string slug)
    {
        return slug switch
        {
            "apple" => BuildApplePage(),
            "samsung" => BuildSamsungPage(),
            "google-pixel" => BuildGooglePage(),
            "oneplus" => BuildOnePlusPage(),
            "motorola" => BuildMotorolaPage(),
            "realme" => BuildRealmePage(),
            "poco" => BuildPocoPage(),
            "vivo" => BuildVivoPage(),
            "oppo" => BuildOppoPage(),
            "nothing" => BuildNothingPage(),
            "redmi" => BuildRedmiPage(),
            "infinix" => BuildInfinixPage(),
            "tecno" => BuildTecnoPage(),
            "lava" => BuildLavaPage(),
            "hmd" => BuildHmdPage(),
            "boltt" => BuildBolttPage(),
            "mivi" => BuildMiviPage(),
            "ai-plus" => BuildAiPlusPage(),
            _ => BuildGenericBrandPage(slug)
        };
    }

    private static MobileBrandPageDto BuildApplePage() => new()
    {
        BrandSlug = "apple",
        BrandName = "iPhone",
        CategoryTagline = "Apple Silicon Flagships & Student Ecosystem",
        LogoEmoji = "🍎",
        ThemePrimaryColor = "#1d4ed8",
        ThemeAccentColor = "#6366f1",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "OFFICIAL APPLE STORE • APPLE INTELLIGENCE READY",
            Title = "iPhone 16 Pro & 16 Series",
            TitleHighlight = "Built for Apple Intelligence & Studio Creators.",
            Subtitle = "Grade 5 Titanium, A18 Pro 3nm silicon, Camera Control, Global 5G eSIM support & special campus trade-in / 0% EMI.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #090d16 0%, #172554 55%, #1e1b4b 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#60a5fa",
            SubtitleColor = "#cbd5e1",
            FilterAccentColor = "#2563eb"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("iPhone Series Lineup", "series", false,
                Opt("iPhone 16 Pro / Pro Max", "16-pro", 2),
                Opt("iPhone 16 / 16 Plus", "16-std", 1),
                Opt("iPhone 15 / 15 Pro", "15-series", 1),
                Opt("iPhone 14 & 13 Student Value", "14-13", 1)),
            Group("Apple Silicon Chipset", "chipset", false,
                Opt("A18 Pro Chip (3nm)", "a18-pro", 2),
                Opt("A18 Bionic Chip", "a18", 1),
                Opt("A16 / A15 Bionic", "a16", 2)),
            Group("Storage Capacity", "storage", false,
                Opt("128 GB", "128gb", 2),
                Opt("256 GB", "256gb", 2),
                Opt("512 GB / 1 TB", "512gb", 1)),
            Group("Display & Refresh Rate", "display", false,
                Opt("120Hz ProMotion LTPO OLED", "120hz", 2),
                Opt("Super Retina XDR OLED (60Hz)", "60hz", 3)),
            Group("Apple Intelligence & Build", "features", true,
                Opt("Apple Intelligence Ready", "apple-ai", 4),
                Opt("Grade 5 Titanium Frame", "titanium", 2),
                Opt("Camera Control + Action Button", "cam-ctrl", 3)),
            Group("Condition & Campus Pricing", "price", true,
                Opt("Under ₹60,000", "under-60k", 1),
                Opt("₹60,000 – ₹1,00,000", "60k-100k", 2),
                Opt("Above ₹1,00,000", "above-100k", 2),
                Opt("0% Student EMI Eligible", "0-emi", 5))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("app-16pm", "Apple iPhone 16 Pro Max (256 GB, Desert Titanium)", "apple", "iPhone", 144900, 149900, 4.9, 642,
                new[] { "A18 Pro", "256 GB", "6.9\" ProMotion", "Titanium" },
                new[] { "16-pro", "a18-pro", "256gb", "120hz", "apple-ai", "titanium", "cam-ctrl", "above-100k", "0-emi" },
                "iPhone 16 Pro", "A18 Pro", "8GB", "256 GB", "6.9\" 120Hz ProMotion"),
            Prod("app-16p", "Apple iPhone 16 Pro (128 GB, Natural Titanium)", "apple", "iPhone", 119900, 124900, 4.9, 418,
                new[] { "A18 Pro", "128 GB", "6.3\" ProMotion", "4K120 Dolby" },
                new[] { "16-pro", "a18-pro", "128gb", "120hz", "apple-ai", "titanium", "cam-ctrl", "above-100k", "0-emi" },
                "iPhone 16 Pro", "A18 Pro", "8GB", "128 GB", "6.3\" 120Hz ProMotion"),
            Prod("app-16", "Apple iPhone 16 (128 GB, Ultramarine)", "apple", "iPhone", 76900, 79900, 4.8, 520,
                new[] { "A18 Bionic", "128 GB", "6.1\" OLED", "Camera Control" },
                new[] { "16-std", "a18", "128gb", "60hz", "apple-ai", "cam-ctrl", "60k-100k", "0-emi" },
                "iPhone 16", "A18", "8GB", "128 GB", "6.1\" Super Retina XDR"),
            Prod("app-15", "Apple iPhone 15 (256 GB, Blue • USB-C)", "apple", "iPhone", 69900, 79900, 4.8, 890,
                new[] { "A16 Bionic", "256 GB", "48MP Main", "Dynamic Island" },
                new[] { "15-series", "a16", "256gb", "60hz", "apple-ai", "60k-100k", "0-emi" },
                "iPhone 15", "A16 Bionic", "6GB", "256 GB", "6.1\" Dynamic Island"),
            Prod("app-14", "Apple iPhone 14 (512 GB, Starlight • Student Pick)", "apple", "iPhone", 58900, 79900, 4.7, 1120,
                new[] { "A15 Bionic", "512 GB", "Crash Detection", "eSIM Global" },
                new[] { "14-13", "a16", "512gb", "60hz", "under-60k", "0-emi" },
                "iPhone 14", "A15 Bionic", "6GB", "512 GB", "6.1\" Super Retina XDR")
        }
    };

    private static MobileBrandPageDto BuildSamsungPage() => new()
    {
        BrandSlug = "samsung",
        BrandName = "Samsung",
        CategoryTagline = "Galaxy AI Flagships, Foldables & 5G Powerhouses",
        LogoEmoji = "🌌",
        ThemePrimaryColor = "#1d4ed8",
        ThemeAccentColor = "#0284c7",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "GALAXY AI OFFICIAL STORE • 7 YEARS OS UPGRADES",
            Title = "Samsung Galaxy S25 Ultra & Z Series",
            TitleHighlight = "Galaxy AI Built-In. 200MP Nightography.",
            Subtitle = "Experience Snapdragon 8 Elite performance, built-in S Pen, Titanium craft & Dynamic AMOLED 2X 120Hz displays.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #061229 0%, #0f2857 55%, #0369a1 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#38bdf8",
            SubtitleColor = "#e0f2fe",
            FilterAccentColor = "#0284c7"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("Galaxy Series Lineup", "series", false,
                Opt("Galaxy S25 Ultra / S25 Series", "s25", 2),
                Opt("Galaxy Z Fold & Z Flip", "z-fold", 1),
                Opt("Galaxy A55 / A35 5G", "a-series", 1),
                Opt("Galaxy M & F Value 5G", "m-series", 1)),
            Group("Processor Platform", "chipset", false,
                Opt("Snapdragon 8 Elite / Gen 3", "sd-elite", 3),
                Opt("Exynos 1480 / 1380", "exynos", 2)),
            Group("Galaxy AI & Pro Features", "features", false,
                Opt("Galaxy AI + Circle to Search", "galaxy-ai", 4),
                Opt("200MP Quad Telephoto + S Pen", "200mp-spen", 1),
                Opt("IP68 Water Resistant", "ip68", 4)),
            Group("RAM & Storage", "storage", false,
                Opt("12 GB / 512 GB", "12-512", 2),
                Opt("12 GB / 256 GB", "12-256", 1),
                Opt("8 GB / 128-256 GB", "8-256", 2)),
            Group("Display & Refresh Rate", "display", true,
                Opt("Dynamic AMOLED 2X 120Hz", "amoled-120", 4),
                Opt("Foldable LTPO Infinity", "foldable", 1)),
            Group("Battery & Campus Price", "price", true,
                Opt("Under ₹25,000", "under-25k", 1),
                Opt("₹25,000 – ₹60,000", "25k-60k", 2),
                Opt("Above ₹60,000", "above-60k", 2))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("sam-s25u", "Samsung Galaxy S25 Ultra 5G (12GB/512GB, Titanium Gray)", "samsung", "Samsung", 129999, 139999, 4.9, 490,
                new[] { "SD 8 Elite", "200MP", "S Pen", "Galaxy AI" },
                new[] { "s25", "sd-elite", "galaxy-ai", "200mp-spen", "ip68", "12-512", "amoled-120", "above-60k" },
                "Galaxy S25 Ultra", "Snapdragon 8 Elite", "12GB", "512 GB", "6.9\" QHD+ AMOLED 2X"),
            Prod("sam-s25fe", "Samsung Galaxy S25 FE 5G (8GB/256GB, Graphite)", "samsung", "Samsung", 44999, 59999, 4.7, 380,
                new[] { "Galaxy AI", "Nightography", "IP68", "45W Charge" },
                new[] { "s25", "sd-elite", "galaxy-ai", "ip68", "8-256", "amoled-120", "25k-60k" },
                "Galaxy S25 FE", "Flagship Chipset", "8GB", "256 GB", "6.7\" FHD+ 120Hz"),
            Prod("sam-zflip6", "Samsung Galaxy Z Flip6 5G (12GB/256GB, Mint)", "samsung", "Samsung", 89999, 109999, 4.8, 210,
                new[] { "SD 8 Gen 3", "FlexCam 50MP", "Galaxy AI", "Foldable" },
                new[] { "z-fold", "sd-elite", "galaxy-ai", "ip68", "12-256", "foldable", "above-60k" },
                "Galaxy Z Flip6", "Snapdragon 8 Gen 3", "12GB", "256 GB", "6.7\" Foldable AMOLED"),
            Prod("sam-a55", "Samsung Galaxy A55 5G (8GB/256GB, Awesome Navy)", "samsung", "Samsung", 35999, 42999, 4.6, 512,
                new[] { "Exynos 1480", "Metal Frame", "IP67/IP68", "50MP OIS" },
                new[] { "a-series", "exynos", "galaxy-ai", "ip68", "8-256", "amoled-120", "25k-60k" },
                "Galaxy A55", "Exynos 1480", "8GB", "256 GB", "6.6\" Super AMOLED"),
            Prod("sam-m35", "Samsung Galaxy M35 5G (8GB/128GB, Moonlight Blue)", "samsung", "Samsung", 18999, 24999, 4.6, 740,
                new[] { "6000mAh Battery", "120Hz sAMOLED", "Knox Vault", "50MP OIS" },
                new[] { "m-series", "exynos", "8-256", "amoled-120", "under-25k" },
                "Galaxy M35", "Exynos 1380", "8GB", "128 GB", "6.6\" 120Hz sAMOLED")
        }
    };

    private static MobileBrandPageDto BuildGooglePage() => new()
    {
        BrandSlug = "google-pixel",
        BrandName = "Google",
        CategoryTagline = "Google Tensor AI & Computational Photography",
        LogoEmoji = "🤖",
        ThemePrimaryColor = "#0f766e",
        ThemeAccentColor = "#2563eb",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "OFFICIAL GOOGLE PIXEL • GEMINI AI BUILT-IN",
            Title = "Google Pixel 9 Pro & 8a Series",
            TitleHighlight = "Pure Google AI. Iconic Pixel Camera.",
            Subtitle = "Powered by Google Tensor G4, Gemini Nano, Magic Editor, Super Actua Displays & 7 years of OS & Pixel Drops.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #062825 0%, #0f766e 55%, #1d4ed8 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#5eead4",
            SubtitleColor = "#ccfbf1",
            FilterAccentColor = "#0d9488"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("Pixel Generation", "series", false,
                Opt("Pixel 9 Pro / 9 Pro XL", "pixel-9-pro", 2),
                Opt("Pixel 9 Standard", "pixel-9", 1),
                Opt("Pixel 8a Student Favorite", "pixel-8a", 1),
                Opt("Pixel 8 / 7 Pro", "pixel-8", 1)),
            Group("Tensor Silicon & AI", "chipset", false,
                Opt("Google Tensor G4 + Gemini", "tensor-g4", 3),
                Opt("Google Tensor G3", "tensor-g3", 2),
                Opt("7 Years Pixel Updates", "7yr-updates", 5)),
            Group("Camera & Computational AI", "camera", false,
                Opt("Triple Pro 50MP + 5x Telephoto", "triple-pro", 2),
                Opt("Best Take + Add Me + Audio Eraser", "magic-ai", 5)),
            Group("Storage & Display", "storage", true,
                Opt("128 GB", "128gb", 2),
                Opt("256 GB / 512 GB", "256gb", 3),
                Opt("Super Actua 3000-nit LTPO", "super-actua", 2)),
            Group("Budget Range", "price", true,
                Opt("Under ₹45,000", "under-45k", 1),
                Opt("₹45,000 – ₹80,000", "45k-80k", 2),
                Opt("Above ₹80,000", "above-80k", 2))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("pix-9pxl", "Google Pixel 9 Pro XL 5G (16GB/256GB, Obsidian)", "google-pixel", "Google", 114999, 124999, 4.9, 310,
                new[] { "Tensor G4", "Gemini AI", "50MP+48MP+48MP", "6.8\" Super Actua" },
                new[] { "pixel-9-pro", "tensor-g4", "7yr-updates", "triple-pro", "magic-ai", "256gb", "super-actua", "above-80k" },
                "Pixel 9 Pro XL", "Google Tensor G4", "16GB", "256 GB", "6.8\" Super Actua OLED"),
            Prod("pix-9p", "Google Pixel 9 Pro 5G (16GB/256GB, Hazel)", "google-pixel", "Google", 99999, 109999, 4.9, 275,
                new[] { "Tensor G4", "5x Telephoto", "6.3\" Pro Actua", "Satellite SOS" },
                new[] { "pixel-9-pro", "tensor-g4", "7yr-updates", "triple-pro", "magic-ai", "256gb", "super-actua", "above-80k" },
                "Pixel 9 Pro", "Google Tensor G4", "16GB", "256 GB", "6.3\" Super Actua OLED"),
            Prod("pix-9", "Google Pixel 9 5G (12GB/256GB, Peony)", "google-pixel", "Google", 74999, 79999, 4.8, 390,
                new[] { "Tensor G4", "12GB RAM", "Add Me AI", "IP68 Rated" },
                new[] { "pixel-9", "tensor-g4", "7yr-updates", "magic-ai", "256gb", "45k-80k" },
                "Pixel 9", "Google Tensor G4", "12GB", "256 GB", "6.3\" Actua Display"),
            Prod("pix-8a", "Google Pixel 8a 5G (8GB/128GB, Aloe • Campus Hit)", "google-pixel", "Google", 39999, 52999, 4.8, 620,
                new[] { "Tensor G3", "64MP OIS", "7 Years OS", "Magic Eraser" },
                new[] { "pixel-8a", "tensor-g3", "7yr-updates", "magic-ai", "128gb", "under-45k" },
                "Pixel 8a", "Google Tensor G3", "8GB", "128 GB", "6.1\" 120Hz Actua"),
            Prod("pix-8", "Google Pixel 8 5G (8GB/128GB, Obsidian)", "google-pixel", "Google", 52999, 75999, 4.7, 450,
                new[] { "Tensor G3", "Macro Focus", "Live Translate", "IP68" },
                new[] { "pixel-8", "tensor-g3", "7yr-updates", "magic-ai", "128gb", "45k-80k" },
                "Pixel 8", "Google Tensor G3", "8GB", "128 GB", "6.2\" 120Hz Actua")
        }
    };

    private static MobileBrandPageDto BuildOnePlusPage() => new()
    {
        BrandSlug = "oneplus",
        BrandName = "OnePlus",
        CategoryTagline = "Never Settle • Hasselblad Camera & 100W SuperVOOC",
        LogoEmoji = "⚡",
        ThemePrimaryColor = "#dc2626",
        ThemeAccentColor = "#991b1b",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "ONEPLUS OFFICIAL STORE • NEVER SETTLE",
            Title = "OnePlus 13 & Nord 5G Lineup",
            TitleHighlight = "Snapdragon 8 Elite & 6000mAh Glacier Battery.",
            Subtitle = "Equipped with Hasselblad Master Camera, 100W SuperVOOC rapid charging, Aqua Touch 2.0 & OxygenOS 15.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #1a0606 0%, #7f1d1d 55%, #dc2626 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#fca5a5",
            SubtitleColor = "#fee2e2",
            FilterAccentColor = "#dc2626"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("OnePlus Series", "series", false,
                Opt("OnePlus 13 / 13R Flagship", "op-13", 2),
                Opt("OnePlus 12 / 12R", "op-12", 1),
                Opt("OnePlus Nord 4 / Nord CE4", "op-nord", 2)),
            Group("Snapdragon Silicon", "chipset", false,
                Opt("Snapdragon 8 Elite", "sd-elite", 1),
                Opt("Snapdragon 8 Gen 3 / Gen 2", "sd-8gen", 2),
                Opt("Snapdragon 7+ Gen 3 / 7 Gen 3", "sd-7gen", 2)),
            Group("SuperVOOC & Signature Features", "features", false,
                Opt("100W SuperVOOC Fast Charge", "100w", 4),
                Opt("Hasselblad Camera System", "hasselblad", 2),
                Opt("Alert Slider + Aqua Touch", "alert-slider", 5)),
            Group("RAM & Storage", "storage", true,
                Opt("16 GB + 512 GB", "16-512", 1),
                Opt("12 GB + 256 GB", "12-256", 2),
                Opt("8 GB + 128/256 GB", "8-256", 2)),
            Group("Price Bracket", "price", true,
                Opt("Under ₹25,000", "under-25k", 1),
                Opt("₹25,000 – ₹45,000", "25k-45k", 2),
                Opt("Above ₹45,000", "above-45k", 2))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("op-13", "OnePlus 13 5G (16GB/512GB, Midnight Ocean • Hasselblad)", "oneplus", "OnePlus", 69999, 76999, 4.9, 410,
                new[] { "SD 8 Elite", "6000mAh", "100W Charge", "IP68/IP69" },
                new[] { "op-13", "sd-elite", "100w", "hasselblad", "alert-slider", "16-512", "above-45k" },
                "OnePlus 13", "Snapdragon 8 Elite", "16GB", "512 GB", "6.82\" 2K ProXDR"),
            Prod("op-13r", "OnePlus 13R 5G (12GB/256GB, Astral Trail)", "oneplus", "OnePlus", 42999, 46999, 4.8, 360,
                new[] { "SD 8 Gen 3", "6000mAh", "80W SuperVOOC", "Flat 120Hz" },
                new[] { "op-13", "sd-8gen", "alert-slider", "12-256", "25k-45k" },
                "OnePlus 13R", "Snapdragon 8 Gen 3", "12GB", "256 GB", "6.78\" 1.5K LTPO"),
            Prod("op-12", "OnePlus 12 5G (12GB/256GB, Flowy Emerald)", "oneplus", "OnePlus", 58999, 64999, 4.9, 540,
                new[] { "SD 8 Gen 3", "Hasselblad", "50W Wireless", "100W Wired" },
                new[] { "op-12", "sd-8gen", "100w", "hasselblad", "alert-slider", "12-256", "above-45k" },
                "OnePlus 12", "Snapdragon 8 Gen 3", "12GB", "256 GB", "6.82\" QHD+ 120Hz"),
            Prod("op-nord4", "OnePlus Nord 4 5G (8GB/256GB, Obsidian Midnight)", "oneplus", "OnePlus", 29999, 32999, 4.7, 720,
                new[] { "SD 7+ Gen 3", "Metal Unibody", "100W Charge", "4Y OS" },
                new[] { "op-nord", "sd-7gen", "100w", "alert-slider", "8-256", "25k-45k" },
                "OnePlus Nord 4", "Snapdragon 7+ Gen 3", "8GB", "256 GB", "6.74\" 120Hz AMOLED"),
            Prod("op-nordce4", "OnePlus Nord CE4 5G (8GB/128GB, Celadon Marble)", "oneplus", "OnePlus", 22999, 24999, 4.7, 850,
                new[] { "SD 7 Gen 3", "5500mAh", "100W SuperVOOC", "Sony LYT-600" },
                new[] { "op-nord", "sd-7gen", "100w", "alert-slider", "8-256", "under-25k" },
                "OnePlus Nord CE4", "Snapdragon 7 Gen 3", "8GB", "128 GB", "6.7\" FHD+ AMOLED")
        }
    };

    private static MobileBrandPageDto BuildMotorolaPage() => new()
    {
        BrandSlug = "motorola",
        BrandName = "motorola",
        CategoryTagline = "Pantone Curated Color, Clean Hello UI & 144Hz pOLED",
        LogoEmoji = "🦋",
        ThemePrimaryColor = "#2563eb",
        ThemeAccentColor = "#0ea5e9",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "MOTOROLA OFFICIAL STORE • PANTONE VALIDATED",
            Title = "motorola edge 50, razr & moto g 5G",
            TitleHighlight = "Bloat-Free Hello UI. Vegan Leather Design.",
            Subtitle = "Clean Android experience with Pantone validated colors, 144Hz curved pOLED displays, IP68 water protection & TurboPower charging.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #091326 0%, #1e3a8a 55%, #0284c7 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#7dd3fc",
            SubtitleColor = "#e0f2fe",
            FilterAccentColor = "#2563eb"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("Motorola Family", "series", false,
                Opt("razr Flip Foldables", "razr", 1),
                Opt("edge 50 Ultra / Pro / Fusion", "edge", 2),
                Opt("moto g85 / g64 / g45 5G", "moto-g", 2)),
            Group("Display & Craftsmanship", "display", false,
                Opt("144Hz 3D Curved pOLED", "144hz", 2),
                Opt("Vegan Leather / Wood Back", "vegan", 4),
                Opt("Pantone Validated Camera", "pantone", 3)),
            Group("Durability & Clean OS", "features", false,
                Opt("Clean Hello UI (Zero Bloat)", "clean-os", 5),
                Opt("IP68 Underwater Protection", "ip68", 2),
                Opt("MIL-STD-810H Certified", "mil-std", 3)),
            Group("TurboPower & Price", "price", true,
                Opt("125W / 68W TurboPower Charge", "turbo", 3),
                Opt("Under ₹15,000", "under-15k", 1),
                Opt("₹15,000 – ₹30,000", "15k-30k", 2),
                Opt("Above ₹30,000", "above-30k", 2))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("moto-razr50u", "motorola razr 50 ultra (12GB/512GB, Peach Fuzz • Moto AI)", "motorola", "motorola", 84999, 99999, 4.8, 185,
                new[] { "4\" Cover pOLED", "SD 8s Gen 3", "Pantone", "IPX8" },
                new[] { "razr", "144hz", "vegan", "pantone", "clean-os", "ip68", "above-30k" },
                "razr 50 ultra", "Snapdragon 8s Gen 3", "12GB", "512 GB", "6.9\" + 4.0\" Dual pOLED"),
            Prod("moto-edge50pro", "motorola edge 50 pro 5G (12GB/256GB, Luxe Lavender)", "motorola", "motorola", 29999, 41999, 4.7, 540,
                new[] { "144Hz 1.5K pOLED", "125W Charge", "50MP f/1.4", "IP68" },
                new[] { "edge", "144hz", "vegan", "pantone", "clean-os", "ip68", "turbo", "15k-30k" },
                "edge 50 pro", "Snapdragon 7 Gen 3", "12GB", "256 GB", "6.7\" 144Hz Curved pOLED"),
            Prod("moto-edge50fus", "motorola edge 50 fusion 5G (8GB/128GB, Hot Pink)", "motorola", "motorola", 21999, 25999, 4.7, 680,
                new[] { "SD 7s Gen 2", "144Hz pOLED", "68W Turbo", "Sony LYT-700C" },
                new[] { "edge", "144hz", "vegan", "pantone", "clean-os", "mil-std", "turbo", "15k-30k" },
                "edge 50 fusion", "Snapdragon 7s Gen 2", "8GB", "128 GB", "6.7\" 144Hz pOLED"),
            Prod("moto-g85", "moto g85 5G (8GB/128GB, Cobalt Blue)", "motorola", "motorola", 16999, 20999, 4.6, 920,
                new[] { "Endless Edge pOLED", "50MP Sony LYT-600", "Gorilla Glass 5", "SD 6s Gen 3" },
                new[] { "moto-g", "vegan", "clean-os", "mil-std", "15k-30k" },
                "moto g85", "Snapdragon 6s Gen 3", "8GB", "128 GB", "6.67\" 120Hz Curved pOLED"),
            Prod("moto-g45", "moto g45 5G (8GB/128GB, Brilliant Green)", "motorola", "motorola", 11999, 14999, 4.5, 1150,
                new[] { "SD 6s Gen 3", "Vegan Leather", "5000mAh", "13 5G Bands" },
                new[] { "moto-g", "vegan", "clean-os", "under-15k" },
                "moto g45", "Snapdragon 6s Gen 3", "8GB", "128 GB", "6.5\" 120Hz Punch Hole")
        }
    };

    private static MobileBrandPageDto BuildRealmePage() => new()
    {
        BrandSlug = "realme",
        BrandName = "realme",
        CategoryTagline = "GT Performance Flagships & Periscope Portrait Pros",
        LogoEmoji = "⚡",
        ThemePrimaryColor = "#eab308",
        ThemeAccentColor = "#f59e0b",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "REALME OFFICIAL STORE • MAKE IT REAL",
            Title = "realme GT 7 Pro & 14 Pro+ Series",
            TitleHighlight = "Snapdragon 8 Elite & IP69 Underwater Photography.",
            Subtitle = "Industry-leading 6000 nits HyperDisplay, 120W UltraDart charging, Periscope Zoom & MagicGlow portraits.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #1c1917 0%, #713f12 55%, #ca8a04 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#fde047",
            SubtitleColor = "#fef9c3",
            FilterAccentColor = "#d97706"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("realme Series", "series", false,
                Opt("GT Flagship Series (GT 7 Pro / 6T)", "gt", 2),
                Opt("Number Pro Series (14 Pro+ / 13 Pro+)", "num-pro", 1),
                Opt("Narzo & P Performance Series", "narzo-p", 2)),
            Group("Processor & Speed", "chipset", false,
                Opt("Snapdragon 8 Elite / 7+ Gen 3", "sd-flag", 2),
                Opt("Snapdragon 7s Gen 2 / Gen 3", "sd-7s", 1),
                Opt("Dimensity 7300 Energy Gaming", "dim-7300", 2)),
            Group("Camera & Protection", "features", false,
                Opt("Periscope Telephoto (120x Zoom)", "periscope", 2),
                Opt("IP69 Dust & Water Resistance", "ip69", 2),
                Opt("120W UltraCharge / 6500mAh", "120w", 3)),
            Group("RAM & Student Budget", "price", true,
                Opt("12 GB + 256/512 GB", "12-256", 2),
                Opt("Under ₹18,000", "under-18k", 2),
                Opt("₹18,000 – ₹35,000", "18k-35k", 1),
                Opt("Above ₹35,000", "above-35k", 1))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("rlm-gt7p", "realme GT 7 Pro 5G (12GB/256GB, Mars Orange)", "realme", "realme", 54999, 59999, 4.9, 320,
                new[] { "SD 8 Elite", "IP69 Underwater", "120W Charge", "6500mAh" },
                new[] { "gt", "sd-flag", "periscope", "ip69", "120w", "12-256", "above-35k" },
                "realme GT 7 Pro", "Snapdragon 8 Elite", "12GB", "256 GB", "6.78\" Eco² OLED Plus"),
            Prod("rlm-gt6t", "realme GT 6T 5G (8GB/256GB, Fluid Silver)", "realme", "realme", 28999, 35999, 4.7, 610,
                new[] { "6000 nits Display", "SD 7+ Gen 3", "120W Charge", "VC Cooling" },
                new[] { "gt", "sd-flag", "120w", "18k-35k" },
                "realme GT 6T", "Snapdragon 7+ Gen 3", "8GB", "256 GB", "6.78\" 6000-nit LTPO"),
            Prod("rlm-14pp", "realme 14 Pro+ 5G (12GB/256GB, Pearl White • Color Changing)", "realme", "realme", 31999, 35999, 4.8, 290,
                new[] { "Sony IMX896 Periscope", "MagicGlow", "IP69", "6000mAh" },
                new[] { "num-pro", "sd-7s", "periscope", "ip69", "12-256", "18k-35k" },
                "realme 14 Pro+", "Snapdragon 7s Gen 3", "12GB", "256 GB", "6.83\" Quad-Curved AMOLED"),
            Prod("rlm-n70t", "realme NARZO 70 Turbo 5G (8GB/128GB, Turbo Yellow)", "realme", "realme", 16499, 19999, 4.7, 840,
                new[] { "Dimensity 7300E", "Motorsport Design", "120Hz OLED", "Vapor Chamber" },
                new[] { "narzo-p", "dim-7300", "under-18k" },
                "NARZO 70 Turbo", "Dimensity 7300 Energy", "8GB", "128 GB", "6.67\" 120Hz OLED"),
            Prod("rlm-p1", "realme P1 5G (6GB/128GB, Phoenix Red)", "realme", "realme", 14499, 19999, 4.6, 1340,
                new[] { "120Hz AMOLED", "Dimensity 7050", "45W SuperVOOC", "Rainwater Touch" },
                new[] { "narzo-p", "dim-7300", "under-18k" },
                "realme P1 5G", "Dimensity 7050", "6GB", "128 GB", "6.67\" 120Hz AMOLED")
        }
    };

    private static MobileBrandPageDto BuildPocoPage() => new()
    {
        BrandSlug = "poco",
        BrandName = "POCO",
        CategoryTagline = "Everything You Need, Nothing You Don't • Esports Champions",
        LogoEmoji = "🔥",
        ThemePrimaryColor = "#f59e0b",
        ThemeAccentColor = "#111827",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "POCO ESPORTS ARENA • PURE GAMING SPEED",
            Title = "POCO F6, X7 Pro & M-Series 5G",
            TitleHighlight = "WildBoost 3.0 & Flagship Gaming AnTuTu Scores.",
            Subtitle = "Built for mobile esports with LiquidCool IceLoop systems, 1.5K CrystalRes 120Hz AMOLED & Turbo HyperCharge.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #09090b 0%, #422006 55%, #b45309 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#facc15",
            SubtitleColor = "#fef3c7",
            FilterAccentColor = "#d97706"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("POCO Series", "series", false,
                Opt("F-Series (Flagship Killer)", "f-series", 1),
                Opt("X-Series (Esports Power)", "x-series", 2),
                Opt("M & C Series (Student Value)", "m-series", 1)),
            Group("Gaming Silicon", "chipset", false,
                Opt("Snapdragon 8s Gen 3", "sd-8s", 1),
                Opt("Dimensity 8400 / 8300 Ultra", "dim-8400", 2),
                Opt("Snapdragon 4 / Dimensity 6000", "entry-5g", 1)),
            Group("Esports Cooling & Display", "features", false,
                Opt("1.5K 120Hz CrystalRes AMOLED", "1.5k", 3),
                Opt("90W / 67W HyperCharge + 6000mAh+", "hypercharge", 3),
                Opt("LiquidCool Vapor Chamber", "liquidcool", 3)),
            Group("RAM & Price", "price", true,
                Opt("12 GB LPDDR5X RAM", "12gb", 2),
                Opt("Under ₹15,000", "under-15k", 1),
                Opt("₹15,000 – ₹35,000", "15k-35k", 3))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("poco-f6", "POCO F6 5G (12GB/256GB, Black • Snapdragon 8s Gen 3)", "poco", "POCO", 26999, 33999, 4.8, 720,
                new[] { "SD 8s Gen 3", "90W Turbo", "1.5K 120Hz", "IceLoop Cooling" },
                new[] { "f-series", "sd-8s", "1.5k", "hypercharge", "liquidcool", "12gb", "15k-35k" },
                "POCO F6", "Snapdragon 8s Gen 3", "12GB", "256 GB", "6.67\" 1.5K Flow AMOLED"),
            Prod("poco-x7p", "POCO X7 Pro 5G (12GB/256GB, POCO Yellow)", "poco", "POCO", 27999, 31999, 4.8, 410,
                new[] { "Dimensity 8400-U", "6550mAh", "90W Charge", "IP68" },
                new[] { "x-series", "dim-8400", "1.5k", "hypercharge", "liquidcool", "12gb", "15k-35k" },
                "POCO X7 Pro", "Dimensity 8400-Ultra", "12GB", "256 GB", "6.67\" 1.5K CrystalRes"),
            Prod("poco-x6p", "POCO X6 Pro 5G (8GB/256GB, Spectre Black)", "poco", "POCO", 21999, 28999, 4.7, 940,
                new[] { "Dimensity 8300-U", "67W Charge", "HyperOS", "WildBoost 2.0" },
                new[] { "x-series", "dim-8400", "1.5k", "hypercharge", "liquidcool", "15k-35k" },
                "POCO X6 Pro", "Dimensity 8300-Ultra", "8GB", "256 GB", "6.67\" 1.5K AMOLED"),
            Prod("poco-m6p", "POCO M6 Pro 5G (6GB/128GB, Forest Green)", "poco", "POCO", 10499, 15999, 4.5, 1650,
                new[] { "SD 4 Gen 2", "5000mAh", "90Hz FHD+", "Glass Back" },
                new[] { "m-series", "entry-5g", "under-15k" },
                "POCO M6 Pro", "Snapdragon 4 Gen 2", "6GB", "128 GB", "6.79\" FHD+ 90Hz")
        }
    };

    private static MobileBrandPageDto BuildVivoPage() => new()
    {
        BrandSlug = "vivo",
        BrandName = "vivo",
        CategoryTagline = "Co-Engineered with ZEISS • Studio Aura Light Portraits",
        LogoEmoji = "📸",
        ThemePrimaryColor = "#2563eb",
        ThemeAccentColor = "#38bdf8",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "VIVO X ZEISS • STUDIO PORTRAIT FLAGSHIPS",
            Title = "vivo X200 Pro & V40 Series",
            TitleHighlight = "200MP ZEISS APO Telephoto & Aura Light Portrait.",
            Subtitle = "Capture cinematic studio bokeh with co-engineered ZEISS optics, V3+ Imaging Chip & ultra-slim 5500mAh BlueVolt batteries.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #091532 0%, #1e40af 55%, #0284c7 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#93c5fd",
            SubtitleColor = "#dbeafe",
            FilterAccentColor = "#2563eb"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("vivo Series Lineup", "series", false,
                Opt("X-Series ZEISS Flagships (X200 Pro)", "x-series", 1),
                Opt("V-Series Portrait Masters (V40 Pro)", "v-series", 2),
                Opt("T-Series Turbo Performance", "t-series", 1)),
            Group("Camera & Imaging Tech", "camera", false,
                Opt("ZEISS Optics + Multifocal Portrait", "zeiss", 3),
                Opt("Studio Aura Light Ring", "aura-light", 2),
                Opt("V3+ / V2 Dedicated Imaging Chip", "v-chip", 1)),
            Group("Battery & Protection", "features", true,
                Opt("5500mAh+ BlueVolt Slim Battery", "bluevolt", 4),
                Opt("IP68 / IP69 Dust & Water Proof", "ip68", 3),
                Opt("80W FlashCharge Included", "80w", 3)),
            Group("Budget Tier", "price", true,
                Opt("Under ₹25,000", "under-25k", 1),
                Opt("₹25,000 – ₹50,000", "25k-50k", 2),
                Opt("Above ₹50,000", "above-50k", 1))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("vivo-x200p", "vivo X200 Pro 5G (16GB/512GB, Titanium Grey • ZEISS 200MP)", "vivo", "vivo", 94999, 101999, 4.9, 260,
                new[] { "Dimensity 9400", "200MP ZEISS APO", "6000mAh", "V3+ Chip" },
                new[] { "x-series", "zeiss", "v-chip", "bluevolt", "ip68", "80w", "above-50k" },
                "vivo X200 Pro", "Dimensity 9400", "16GB", "512 GB", "6.78\" 8T LTPO AMOLED"),
            Prod("vivo-v40p", "vivo V40 Pro 5G (8GB/256GB, Ganges Blue • ZEISS Optics)", "vivo", "vivo", 48999, 54999, 4.8, 390,
                new[] { "Dimensity 9200+", "50MP ZEISS Trio", "Aura Light", "5500mAh" },
                new[] { "v-series", "zeiss", "aura-light", "bluevolt", "ip68", "80w", "25k-50k" },
                "vivo V40 Pro", "Dimensity 9200+", "8GB", "256 GB", "6.78\" 120Hz 3D Curved"),
            Prod("vivo-v40", "vivo V40 5G (8GB/128GB, Lotus Purple)", "vivo", "vivo", 34999, 39999, 4.7, 510,
                new[] { "SD 7 Gen 3", "50MP ZEISS OIS", "5500mAh Slim", "IP68" },
                new[] { "v-series", "zeiss", "aura-light", "bluevolt", "ip68", "80w", "25k-50k" },
                "vivo V40", "Snapdragon 7 Gen 3", "8GB", "128 GB", "6.78\" 120Hz AMOLED"),
            Prod("vivo-t3p", "vivo T3 Pro 5G (8GB/128GB, Sandstone Orange Vegan Leather)", "vivo", "vivo", 22999, 26999, 4.7, 680,
                new[] { "SD 7 Gen 3", "5500mAh", "4500-nit AMOLED", "Sony IMX882" },
                new[] { "t-series", "bluevolt", "under-25k" },
                "vivo T3 Pro", "Snapdragon 7 Gen 3", "8GB", "128 GB", "6.77\" 120Hz 3D Curved")
        }
    };

    private static MobileBrandPageDto BuildOppoPage() => new()
    {
        BrandSlug = "oppo",
        BrandName = "OPPO",
        CategoryTagline = "OPPO AI Portrait Experts & 360° Armour Durability",
        LogoEmoji = "🌿",
        ThemePrimaryColor = "#059669",
        ThemeAccentColor = "#10b981",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "OPPO OFFICIAL STORE • OPPO AI & ARMOUR BODY",
            Title = "OPPO Find X8 Pro, Reno13 & F27 Pro+",
            TitleHighlight = "AI Eraser 2.0, Hasselblad Zoom & IP69 Waterproof.",
            Subtitle = "Crafted with 360-degree Damage-Proof Armour Body, Splash Touch displays, and ultra-long life 5600mAh batteries.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #042f2e 0%, #065f46 55%, #10b981 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#6ee7b7",
            SubtitleColor = "#d1fae5",
            FilterAccentColor = "#059669"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("OPPO Series", "series", false,
                Opt("Find X8 Flagship Series", "find", 1),
                Opt("Reno 13 / 12 Portrait Series", "reno", 2),
                Opt("F27 Pro+ / K12x Armour Series", "armour", 1)),
            Group("OPPO AI & Durability", "features", false,
                Opt("AI Eraser 2.0 + AI Studio", "oppo-ai", 4),
                Opt("IP69 Waterproof + MIL-STD Armour", "ip69", 3),
                Opt("Hasselblad Portrait Master", "hasselblad", 1)),
            Group("Charging & Display", "charging", true,
                Opt("80W / 45W SuperVOOC", "supervooc", 4),
                Opt("120Hz Splash Touch AMOLED", "amoled", 4)),
            Group("Price Segment", "price", true,
                Opt("Under ₹25,000", "under-25k", 1),
                Opt("₹25,000 – ₹45,000", "25k-45k", 2),
                Opt("Above ₹45,000", "above-45k", 1))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("oppo-fx8p", "OPPO Find X8 Pro 5G (16GB/512GB, Space Black • Dual Periscope)", "oppo", "OPPO", 94999, 99999, 4.9, 195,
                new[] { "Dimensity 9400", "Hasselblad Quad 50MP", "5910mAh", "Quick Button" },
                new[] { "find", "oppo-ai", "ip69", "hasselblad", "supervooc", "amoled", "above-45k" },
                "Find X8 Pro", "Dimensity 9400", "16GB", "512 GB", "6.78\" Quad-Curved LTPO"),
            Prod("oppo-r13p", "OPPO Reno13 Pro 5G (12GB/256GB, Graphite Grey • AI Livephoto)", "oppo", "OPPO", 47999, 54999, 4.8, 280,
                new[] { "Dimensity 8350", "3.5x Telephoto", "IP69 Underwater", "80W" },
                new[] { "reno", "oppo-ai", "ip69", "supervooc", "amoled", "25k-45k" },
                "Reno13 Pro", "Dimensity 8350", "12GB", "256 GB", "6.83\" 1.5K 120Hz"),
            Prod("oppo-r12", "OPPO Reno12 5G (8GB/256GB, Sunset Peach)", "oppo", "OPPO", 29999, 36999, 4.7, 420,
                new[] { "AI Eraser 2.0", "BeaconLink Offline Call", "80W SuperVOOC", "50MP OIS" },
                new[] { "reno", "oppo-ai", "supervooc", "amoled", "25k-45k" },
                "Reno12 5G", "Dimensity 7300-Energy", "8GB", "256 GB", "6.7\" 120Hz Curved AMOLED"),
            Prod("oppo-f27p", "OPPO F27 Pro+ 5G (8GB/128GB, Midnight Navy • India's 1st IP69)", "oppo", "OPPO", 24999, 32999, 4.6, 590,
                new[] { "IP69 Waterproof", "360° Armour Body", "67W SuperVOOC", "Vegan Leather" },
                new[] { "armour", "oppo-ai", "ip69", "supervooc", "amoled", "under-25k" },
                "F27 Pro+", "Dimensity 7050", "8GB", "128 GB", "6.7\" 120Hz 3D Curved")
        }
    };

    private static MobileBrandPageDto BuildNothingPage() => new()
    {
        BrandSlug = "nothing",
        BrandName = "Nothing",
        CategoryTagline = "Transparent Design, Iconic Glyph Interface & Pure Nothing OS",
        LogoEmoji = "⚪",
        ThemePrimaryColor = "#111827",
        ThemeAccentColor = "#dc2626",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "NOTHING OFFICIAL STORE • PURE & TRANSPARENT",
            Title = "Nothing Phone (2), (2a) Plus & CMF",
            TitleHighlight = "Bespoke Glyph Interface & Zero-Bloat Nothing OS 3.0.",
            Subtitle = "Designed in London with transparent rear glass, interactive Glyph LED lighting, ChatGPT OS integration & symmetrical bezels.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #111827 0%, #1f2937 60%, #374151 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#f87171",
            SubtitleColor = "#e5e7eb",
            FilterAccentColor = "#111827"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("Nothing & CMF Models", "series", false,
                Opt("Phone (2) Flagship", "phone-2", 1),
                Opt("Phone (2a) Plus / (2a) 5G", "phone-2a", 2),
                Opt("CMF Phone 1 (Modular Case)", "cmf", 1)),
            Group("Signature Engineering", "features", false,
                Opt("Glyph LED Light Interface", "glyph", 3),
                Opt("Nothing OS 3.0 (Zero Bloatware)", "nothing-os", 4),
                Opt("Interchangeable Modular Back", "modular", 1)),
            Group("Display & Camera", "display", true,
                Opt("120Hz Flexible AMOLED", "120hz", 4),
                Opt("50MP Front + Dual 50MP OIS", "50mp", 3)),
            Group("Price Range", "price", true,
                Opt("Under ₹18,000", "under-18k", 1),
                Opt("₹18,000 – ₹30,000", "18k-30k", 2),
                Opt("Above ₹30,000", "above-30k", 1))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("nth-p2", "Nothing Phone (2) 5G (12GB/256GB, Dark Grey)", "nothing", "Nothing", 36999, 49999, 4.8, 690,
                new[] { "SD 8+ Gen 1", "LTPO 120Hz", "Glyph Interface", "50MP OIS" },
                new[] { "phone-2", "glyph", "nothing-os", "120hz", "50mp", "above-30k" },
                "Phone (2)", "Snapdragon 8+ Gen 1", "12GB", "256 GB", "6.7\" LTPO OLED"),
            Prod("nth-p2ap", "Nothing Phone (2a) Plus 5G (8GB/256GB, Metallic Grey)", "nothing", "Nothing", 26999, 29999, 4.8, 430,
                new[] { "Dimensity 7350 Pro", "50MP Selfie", "Glyph Lights", "50W Fast" },
                new[] { "phone-2a", "glyph", "nothing-os", "120hz", "50mp", "18k-30k" },
                "Phone (2a) Plus", "Dimensity 7350 Pro", "8GB", "256 GB", "6.7\" 120Hz AMOLED"),
            Prod("nth-p2a", "Nothing Phone (2a) 5G (8GB/128GB, Milk White)", "nothing", "Nothing", 22999, 25999, 4.7, 980,
                new[] { "Dimensity 7200 Pro", "5000mAh", "Dual 50MP OIS", "Glyph" },
                new[] { "phone-2a", "glyph", "nothing-os", "120hz", "50mp", "18k-30k" },
                "Phone (2a)", "Dimensity 7200 Pro", "8GB", "128 GB", "6.7\" Flexible AMOLED"),
            Prod("nth-cmf1", "CMF by Nothing Phone 1 5G (8GB/128GB, Orange • Modular)", "nothing", "Nothing", 15999, 19999, 4.7, 1120,
                new[] { "Dimensity 7300", "Custom Backplates", "Super AMOLED", "50MP Sony" },
                new[] { "cmf", "nothing-os", "modular", "120hz", "under-18k" },
                "CMF Phone 1", "Dimensity 7300 5G", "8GB", "128 GB", "6.67\" Super AMOLED")
        }
    };

    private static MobileBrandPageDto BuildRedmiPage() => new()
    {
        BrandSlug = "redmi",
        BrandName = "REDMI",
        CategoryTagline = "India's Most Loved Note Series • 200MP Pro+ & HyperCharge",
        LogoEmoji = "⚡",
        ThemePrimaryColor = "#e11d48",
        ThemeAccentColor = "#f43f5e",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "REDMI OFFICIAL STORE • HYPEROS POWERED",
            Title = "REDMI Note 14 Pro+ & 14 Series 5G",
            TitleHighlight = "200MP Flagship Camera, Gorilla Glass Victus 2 & 120W.",
            Subtitle = "Flagship IP68 durability, curved 1.5K AMOLED screens with Wet Touch control & HyperOS smooth optimization.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #270712 0%, #881337 55%, #e11d48 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#fda4af",
            SubtitleColor = "#ffe4e6",
            FilterAccentColor = "#e11d48"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("REDMI Lineup", "series", false,
                Opt("Note 14 Pro+ / Note 14 Pro 5G", "note-pro", 2),
                Opt("Note 14 / 13 5G", "note-std", 1),
                Opt("REDMI 13 / A-Series Budget", "budget", 1)),
            Group("Camera & Durability", "features", false,
                Opt("200MP OIS / 50MP Telephoto", "200mp", 2),
                Opt("IP68 + Gorilla Glass Victus 2", "victus", 2),
                Opt("120W / 45W HyperCharge", "hyper", 3)),
            Group("Display & Audio Extras", "display", true,
                Opt("1.5K 120Hz Curved AMOLED", "1.5k", 2),
                Opt("IR Blaster + 3.5mm Jack", "ir-jack", 4)),
            Group("Student Budget", "price", true,
                Opt("Under ₹15,000", "under-15k", 1),
                Opt("₹15,000 – ₹25,000", "15k-25k", 1),
                Opt("₹25,000 – ₹35,000", "25k-35k", 2))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("red-n14pp", "REDMI Note 14 Pro+ 5G (12GB/256GB, Spectre Blue)", "redmi", "REDMI", 30999, 34999, 4.8, 490,
                new[] { "SD 7s Gen 3", "6200mAh Si-C", "90W HyperCharge", "IP68 + IP69" },
                new[] { "note-pro", "200mp", "victus", "hyper", "1.5k", "ir-jack", "25k-35k" },
                "Note 14 Pro+", "Snapdragon 7s Gen 3", "12GB", "256 GB", "6.67\" 1.5K Curved AMOLED"),
            Prod("red-n14p", "REDMI Note 14 Pro 5G (8GB/128GB, Ivy Green)", "redmi", "REDMI", 25999, 28999, 4.7, 560,
                new[] { "Dimensity 7300-U", "Victus 2 Glass", "IP68", "Sony LYT-600" },
                new[] { "note-pro", "victus", "hyper", "1.5k", "ir-jack", "25k-35k" },
                "Note 14 Pro", "Dimensity 7300-Ultra", "8GB", "128 GB", "6.67\" 1.5K Curved AMOLED"),
            Prod("red-n14", "REDMI Note 14 5G (8GB/128GB, Mystique White)", "redmi", "REDMI", 18999, 21999, 4.7, 810,
                new[] { "50MP Sony OIS", "5110mAh", "120Hz AMOLED", "3.5mm Jack" },
                new[] { "note-std", "hyper", "ir-jack", "15k-25k" },
                "Note 14 5G", "Dimensity 7025-Ultra", "8GB", "128 GB", "6.67\" 120Hz AMOLED"),
            Prod("red-13", "REDMI 13 5G (6GB/128GB, Hawaiian Blue • Crystal Glass)", "redmi", "REDMI", 12999, 17999, 4.6, 1420,
                new[] { "SD 4 Gen 2 AE", "108MP Pro Camera", "5030mAh", "33W Included" },
                new[] { "budget", "200mp", "ir-jack", "under-15k" },
                "REDMI 13 5G", "Snapdragon 4 Gen 2 AE", "6GB", "128 GB", "6.79\" 120Hz Gorilla Glass")
        }
    };

    private static MobileBrandPageDto BuildInfinixPage() => new()
    {
        BrandSlug = "infinix",
        BrandName = "Infinix",
        CategoryTagline = "Cyber Mecha Esports, JBL Audio & MagCharge 2.0",
        LogoEmoji = "🎮",
        ThemePrimaryColor = "#6d28d9",
        ThemeAccentColor = "#10b981",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "INFINIX OFFICIAL STORE • BYPASS GAMING & MAGCHARGE",
            Title = "Infinix GT 20 Pro & Note 40 Pro 5G",
            TitleHighlight = "144Hz Esports AMOLED + Dedicated Gaming Display Chip.",
            Subtitle = "Built for BGMI & mobile esports with Cyber Mecha RGB Halo, Sound by JBL, Bypass Charging & Magnetic Wireless MagKit.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #130b29 0%, #3b0764 55%, #059669 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#34d399",
            SubtitleColor = "#e9d5ff",
            FilterAccentColor = "#7c3aed"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("Infinix Lineup", "series", false,
                Opt("GT 20 Pro (Esports Mecha)", "gt-pro", 1),
                Opt("Note 40 Pro / 40X (MagCharge)", "note-40", 2),
                Opt("Hot 50 / Zero 40 Series", "hot-zero", 1)),
            Group("Gaming & Charging Innovation", "features", false,
                Opt("Pixelworks 144Hz Gaming Chip", "gaming-chip", 1),
                Opt("Wireless MagCharge + Bypass Mode", "magcharge", 2),
                Opt("Sound by JBL Dual Stereo", "jbl", 3)),
            Group("Memory & Budget", "price", true,
                Opt("24 GB Virtual Extended RAM", "24gb-ram", 2),
                Opt("Under ₹15,000", "under-15k", 2),
                Opt("₹15,000 – ₹25,000", "15k-25k", 2))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("inf-gt20", "Infinix GT 20 Pro 5G (12GB/256GB, Mecha Blue • 144Hz Esports)", "infinix", "Infinix", 24999, 31999, 4.8, 520,
                new[] { "Dimensity 8200U", "Pixelworks X5 Chip", "144Hz AMOLED", "Cyber RGB" },
                new[] { "gt-pro", "gaming-chip", "jbl", "24gb-ram", "15k-25k" },
                "GT 20 Pro", "Dimensity 8200 Ultimate", "12GB", "256 GB", "6.78\" 144Hz Esports AMOLED"),
            Prod("inf-n40p", "Infinix Note 40 Pro 5G (8GB/256GB, Vintage Green • Free MagCase)", "infinix", "Infinix", 19999, 27999, 4.7, 640,
                new[] { "20W MagCharge", "108MP OIS", "Curved AMOLED", "JBL Audio" },
                new[] { "note-40", "magcharge", "jbl", "15k-25k" },
                "Note 40 Pro", "Dimensity 7020 5G", "8GB", "256 GB", "6.78\" 120Hz Curved AMOLED"),
            Prod("inf-n40x", "Infinix Note 40X 5G (12GB/256GB, Palm Blue)", "infinix", "Infinix", 13999, 17999, 4.6, 480,
                new[] { "108MP Camera", "256GB Storage", "Dynamic Bar", "DTS Sound" },
                new[] { "note-40", "24gb-ram", "under-15k" },
                "Note 40X 5G", "Dimensity 6300", "12GB", "256 GB", "6.78\" 120Hz FHD+"),
            Prod("inf-h50", "Infinix Hot 50 5G (8GB/128GB, Vibrant Blue • Ultra-Slim)", "infinix", "Infinix", 10499, 14999, 4.6, 710,
                new[] { "7.8mm Slim", "Dimensity 6300", "5 Year TUV Smooth", "Wet Touch" },
                new[] { "hot-zero", "jbl", "under-15k" },
                "Hot 50 5G", "Dimensity 6300", "8GB", "128 GB", "6.7\" 120Hz HD+")
        }
    };

    private static MobileBrandPageDto BuildTecnoPage() => new()
    {
        BrandSlug = "tecno",
        BrandName = "Tecno",
        CategoryTagline = "PHANTOM Foldables, CAMON 30 LYTIA & POVA 6000mAh",
        LogoEmoji = "🌠",
        ThemePrimaryColor = "#0284c7",
        ThemeAccentColor = "#4f46e5",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "TECNO OFFICIAL STORE • STOP AT NOTHING",
            Title = "Tecno PHANTOM Fold, CAMON 30 & POVA 6",
            TitleHighlight = "6000mAh Gaming Power & Flagship Sony LYTIA Optics.",
            Subtitle = "Cutting-edge ultra-slim foldables, PolarAce AI imaging chips, 70W UltraCharge & Dolby Atmos audio.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #091326 0%, #1e3a8a 55%, #4338ca 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#60a5fa",
            SubtitleColor = "#e0e7ff",
            FilterAccentColor = "#2563eb"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("Tecno Series", "series", false,
                Opt("PHANTOM V Fold2 / Flip2", "phantom", 1),
                Opt("CAMON 30 Series (Camera)", "camon", 1),
                Opt("POVA 6 Pro (6000mAh Gaming)", "pova", 1),
                Opt("SPARK 5G Student Series", "spark", 1)),
            Group("Hardware Highlights", "features", false,
                Opt("6000mAh+ Arc Light Battery", "6000mah", 1),
                Opt("Sony LYTIA + Dual OIS Camera", "lytia", 2),
                Opt("512GB / 256GB Large Storage", "256gb", 3)),
            Group("Price Range", "price", true,
                Opt("Under ₹15,000", "under-15k", 1),
                Opt("₹15,000 – ₹30,000", "15k-30k", 2),
                Opt("Above ₹30,000 (Foldables)", "above-30k", 1))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("tec-pvfold2", "TECNO PHANTOM V Fold2 5G (12GB/512GB, Karst Green • Ultra Slim Fold)", "tecno", "Tecno", 79999, 99999, 4.8, 145,
                new[] { "7.85\" 2K+ AMOLED", "5750mAh", "70W Charge", "PHANTOM V Pen" },
                new[] { "phantom", "lytia", "256gb", "above-30k" },
                "PHANTOM V Fold2", "Dimensity 9000+", "12GB", "512 GB", "7.85\" + 6.42\" Dual LTPO"),
            Prod("tec-cam30", "TECNO CAMON 30 5G (12GB/512GB, Uyuni Salt White • Marble)", "tecno", "Tecno", 22999, 27999, 4.7, 320,
                new[] { "50MP OIS + 50MP Selfie", "512GB Storage", "70W Charge", "Dolby Atmos" },
                new[] { "camon", "lytia", "256gb", "15k-30k" },
                "CAMON 30 5G", "Dimensity 7020", "12GB", "512 GB", "6.78\" 120Hz AMOLED"),
            Prod("tec-pova6p", "TECNO POVA 6 Pro 5G (8GB/256GB, Comet Green • Arc Lights)", "tecno", "Tecno", 18999, 22999, 4.7, 610,
                new[] { "6000mAh Battery", "70W UltraCharge", "210+ Dynamic LEDs", "108MP" },
                new[] { "pova", "6000mah", "256gb", "15k-30k" },
                "POVA 6 Pro", "Dimensity 6080", "8GB", "256 GB", "6.78\" 120Hz AMOLED"),
            Prod("tec-spark30", "TECNO SPARK 30C 5G (8GB/128GB, Midnight Shadow)", "tecno", "Tecno", 10499, 13999, 4.5, 480,
                new[] { "48MP Sony IMX582", "120Hz Display", "IP54", "Infrared Remote" },
                new[] { "spark", "under-15k" },
                "SPARK 30C 5G", "Dimensity 6300", "8GB", "128 GB", "6.67\" 120Hz Hole Display")
        }
    };

    private static MobileBrandPageDto BuildLavaPage() => new()
    {
        BrandSlug = "lava",
        BrandName = "LAVA",
        CategoryTagline = "Proudly Designed in India • Clean Stock Android & Home Service",
        LogoEmoji = "🇮🇳",
        ThemePrimaryColor = "#ea580c",
        ThemeAccentColor = "#c2410c",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "LAVA OFFICIAL STORE • CLEAN STOCK ANDROID + HOME SERVICE",
            Title = "LAVA Agni 3 & Blaze 5G Series",
            TitleHighlight = "India's First Dual-AMOLED 5G Smartphone + Action Key.",
            Subtitle = "Zero bloatware, zero ads, clean Android updates & complimentary at-home doorstep warranty service.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #270e05 0%, #7c2d12 55%, #ea580c 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#fdba74",
            SubtitleColor = "#ffedd5",
            FilterAccentColor = "#ea580c"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("LAVA Lineup", "series", false,
                Opt("Agni 3 Dual-Screen Flagship", "agni", 1),
                Opt("Blaze X / Blaze Duo 5G", "blaze", 2),
                Opt("Yuva 5G Value Series", "yuva", 1)),
            Group("LAVA Signature Benefits", "features", false,
                Opt("Rear Secondary Mini AMOLED", "dual-screen", 2),
                Opt("Zero-Ads Clean Stock Android", "clean-android", 4),
                Opt("Free Home Doorstep Service", "home-service", 4)),
            Group("Price Range", "price", true,
                Opt("Under ₹10,000", "under-10k", 1),
                Opt("₹10,000 – ₹18,000", "10k-18k", 2),
                Opt("₹18,000 – ₹25,000", "18k-25k", 1))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("lava-agni3", "LAVA Agni 3 5G (8GB/256GB, Heather Glass • Dual AMOLED + Action Key)", "lava", "LAVA", 22999, 25999, 4.8, 390,
                new[] { "1.74\" Rear AMOLED", "Dimensity 7300X", "50MP OIS + Telephoto", "66W" },
                new[] { "agni", "dual-screen", "clean-android", "home-service", "18k-25k" },
                "Agni 3 5G", "Dimensity 7300X", "8GB", "256 GB", "6.78\" 1.5K Curved + 1.74\" Rear"),
            Prod("lava-blazex", "LAVA Blaze X 5G (8GB/128GB, Titanium Grey • Curved AMOLED)", "lava", "LAVA", 15999, 18999, 4.7, 510,
                new[] { "120Hz Curved AMOLED", "64MP Sony OIS", "Dimensity 6300", "33W" },
                new[] { "blaze", "clean-android", "home-service", "10k-18k" },
                "Blaze X 5G", "Dimensity 6300", "8GB", "128 GB", "6.67\" 120Hz Curved AMOLED"),
            Prod("lava-blazed", "LAVA Blaze Duo 5G (6GB/128GB, Arctic White • Dual Screen)", "lava", "LAVA", 16999, 18999, 4.7, 290,
                new[] { "Secondary 1.58\" AMOLED", "Dimensity 7025", "Clean OS", "5000mAh" },
                new[] { "blaze", "dual-screen", "clean-android", "home-service", "10k-18k" },
                "Blaze Duo 5G", "Dimensity 7025", "6GB", "128 GB", "6.67\" + 1.58\" Dual AMOLED"),
            Prod("lava-yuva5g", "LAVA Yuva 5G (4GB/128GB, Mystic Blue • Student Essential)", "lava", "LAVA", 9499, 11499, 4.5, 620,
                new[] { "Clean Android", "50MP AI Camera", "5000mAh", "Side Fingerprint" },
                new[] { "yuva", "clean-android", "home-service", "under-10k" },
                "Yuva 5G", "Unisoc T750 5G", "4GB", "128 GB", "6.53\" 90Hz Punch Hole")
        }
    };

    private static MobileBrandPageDto BuildHmdPage() => new()
    {
        BrandSlug = "hmd",
        BrandName = "HMD",
        CategoryTagline = "Human Mobile Devices • Gen2 Self-Repairable & Detox Mode",
        LogoEmoji = "🛠️",
        ThemePrimaryColor = "#be185d",
        ThemeAccentColor = "#0f172a",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "HMD OFFICIAL STORE • GEN2 SELF-REPAIRABLE TECH",
            Title = "HMD Skyline, Crest & Fusion",
            TitleHighlight = "Replaceable Screens & Batteries in Minutes + Detox Mode.",
            Subtitle = "European craftsmanship featuring Gen2 iFixit self-repairability, Digital Detox Mode for exam focus, and Nokia classic durability.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #1f0a18 0%, #831843 55%, #be185d 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#f9a8d4",
            SubtitleColor = "#fce7f3",
            FilterAccentColor = "#be185d"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("HMD Models", "series", false,
                Opt("HMD Skyline (Flagship Repairable)", "skyline", 1),
                Opt("HMD Fusion (Modular Smart Outfits)", "fusion", 1),
                Opt("HMD Crest / Crest Max 5G", "crest", 1),
                Opt("Classic Keypads & UPI Phones", "classic", 1)),
            Group("Unique HMD Tech", "features", false,
                Opt("Gen2 Self-Repairable Screen/Port", "repairable", 3),
                Opt("Digital Detox Mode (Focus Study)", "detox", 3),
                Opt("Qi2 Magnetic Wireless Charge", "qi2", 1)),
            Group("Price Bracket", "price", true,
                Opt("Under ₹5,000 (Keypads)", "under-5k", 1),
                Opt("₹10,000 – ₹25,000", "10k-25k", 2),
                Opt("Above ₹25,000", "above-25k", 1))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("hmd-skyline", "HMD Skyline 5G (12GB/256GB, Neon Pink • Gen2 Repairable)", "hmd", "HMD", 35999, 41999, 4.8, 220,
                new[] { "108MP OIS + 50MP AF Selfie", "Gen2 Easy Repair", "Qi2 Magnetic", "SD 7s Gen 2" },
                new[] { "skyline", "repairable", "detox", "qi2", "above-25k" },
                "HMD Skyline", "Snapdragon 7s Gen 2", "12GB", "256 GB", "6.55\" 144Hz pOLED"),
            Prod("hmd-fusion", "HMD Fusion 5G (8GB/256GB, Noir • Modular Smart Outfits)", "hmd", "HMD", 17999, 21999, 4.7, 190,
                new[] { "Modular Smart Pins", "108MP + 50MP", "Gen2 Repairable", "Detox Mode" },
                new[] { "fusion", "repairable", "detox", "10k-25k" },
                "HMD Fusion", "Snapdragon 4 Gen 2", "8GB", "256 GB", "6.56\" 90Hz HD+"),
            Prod("hmd-crest", "HMD Crest Max 5G (8GB/256GB, Aqua Green)", "hmd", "HMD", 14999, 17999, 4.6, 340,
                new[] { "64MP OIS + 50MP Selfie", "OLED Display", "Gen1 Easy Repair", "5000mAh" },
                new[] { "crest", "repairable", "detox", "10k-25k" },
                "HMD Crest Max", "Unisoc T760 5G", "8GB", "256 GB", "6.67\" FHD+ OLED"),
            Prod("hmd-105", "HMD 105 4G Keypad Phone with In-Built UPI Scan & Pay", "hmd", "HMD", 2199, 2999, 4.6, 890,
                new[] { "In-Built UPI Payments", "4G VoLTE Crystal", "1450mAh", "MP3 & FM" },
                new[] { "classic", "under-5k" },
                "HMD 105 4G", "4G Embedded OS", "Dual SIM", "Expandable 32GB", "2.4\" Sunlight Readable")
        }
    };

    private static MobileBrandPageDto BuildBolttPage() => new()
    {
        BrandSlug = "boltt",
        BrandName = "boltt",
        CategoryTagline = "Android 4G SIM Wristphones & Connected Wearable Phones",
        LogoEmoji = "⌚",
        ThemePrimaryColor = "#dc2626",
        ThemeAccentColor = "#f97316",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "FIRE-BOLTT OFFICIAL • WRISTPHONE REVOLUTION",
            Title = "boltt Dream & Snapp 4G Wristphones",
            TitleHighlight = "Full Android OS + 4G Nano-SIM Right on Your Wrist.",
            Subtitle = "Leave bulky phones behind on runs or quick campus hops—make 4G LTE calls, use Google Maps, WhatsApp & Spotify directly on your wrist.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #1f0808 0%, #7f1d1d 55%, #ea580c 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#fdba74",
            SubtitleColor = "#fee2e2",
            FilterAccentColor = "#dc2626"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("Wristphone Models", "series", false,
                Opt("Dream Android 4G Wristphone", "dream", 2),
                Opt("Snapp Selfie Camera Wristphone", "snapp", 1),
                Opt("BT Calling Smart Watches", "bt-watch", 1)),
            Group("Connectivity & OS", "features", false,
                Opt("Standalone 4G LTE Nano-SIM", "4g-sim", 3),
                Opt("Full Android OS + Play Store", "android-os", 3),
                Opt("Built-in Camera & GPS", "gps-cam", 3)),
            Group("Budget Range", "price", true,
                Opt("Under ₹4,000", "under-4k", 1),
                Opt("₹4,000 – ₹8,000", "4k-8k", 3))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("blt-dream", "Fire-Boltt Dream 4G LTE Android Wristphone (2GB/16GB, Denim Steel)", "boltt", "boltt", 5999, 24999, 4.6, 820,
                new[] { "4G Nano-SIM", "Android OS + Play Store", "2.02\" AMOLED", "Wi-Fi + GPS" },
                new[] { "dream", "4g-sim", "android-os", "gps-cam", "4k-8k" },
                "Dream Wristphone", "Quad-Core Cortex", "2GB", "16 GB", "2.02\" CrystalView"),
            Prod("blt-snapp", "Fire-Boltt Snapp 4G Wristphone with Flip HD Selfie Camera (4GB/64GB)", "boltt", "boltt", 6499, 19999, 4.7, 340,
                new[] { "4GB+64GB Memory", "Flip HD Camera", "4G LTE SIM", "AMOLED Display" },
                new[] { "snapp", "4g-sim", "android-os", "gps-cam", "4k-8k" },
                "Snapp 4G", "Quad-Core 4G SoC", "4GB", "64 GB", "2.13\" HD AMOLED"),
            Prod("blt-dream-sp", "Fire-Boltt Dream Midnight Sports Edition 4G Wristphone", "boltt", "boltt", 5499, 19999, 4.5, 410,
                new[] { "WhatsApp + Maps", "4G LTE Calling", "800mAh Battery", "Play Store" },
                new[] { "dream", "4g-sim", "android-os", "gps-cam", "4k-8k" },
                "Dream Sports", "Quad-Core SoC", "2GB", "16 GB", "2.02\" HD Screen"),
            Prod("blt-inv", "Fire-Boltt Invincible Plus AMOLED BT Calling Watch (4GB TWS Storage)", "boltt", "boltt", 3499, 21000, 4.6, 1520,
                new[] { "1.43\" AMOLED", "4GB Local Music", "TWS Connect", "300 Sports Modes" },
                new[] { "bt-watch", "under-4k" },
                "Invincible Plus", "BT 5.2 + Audio", "128MB", "4 GB Storage", "1.43\" Always-On AMOLED")
        }
    };

    private static MobileBrandPageDto BuildMiviPage() => new()
    {
        BrandSlug = "mivi",
        BrandName = "MIVI",
        CategoryTagline = "Made-in-India Mobile Companions: Hi-Res TWS, GaN Chargers & Audio",
        LogoEmoji = "🎧",
        ThemePrimaryColor = "#16a34a",
        ThemeAccentColor = "#15803d",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "MIVI OFFICIAL • SMARTPHONE AUDIO & FAST POWER COMPANIONS",
            Title = "MIVI SuperPods, Duopods & 65W GaN Adapters",
            TitleHighlight = "3D Spatial Audio + Ultra-Fast Smartphone Companions.",
            Subtitle = "Complete your smartphone setup with LDAC Hi-Res TWS earbuds, 45dB ANC, 35ms gaming latency & high-speed GaN chargers.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #052e16 0%, #14532d 55%, #16a34a 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#86efac",
            SubtitleColor = "#dcfce7",
            FilterAccentColor = "#16a34a"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("Companion Category", "series", false,
                Opt("SuperPods Hi-Res TWS Audio", "superpods", 2),
                Opt("DuoPods Esports Gaming Earbuds", "duopods", 1),
                Opt("65W GaN Smartphone Chargers", "chargers", 1)),
            Group("Audio & Power Specs", "features", false,
                Opt("3D Spatial Audio + LDAC Hi-Res", "spatial", 2),
                Opt("ANC Noise Cancellation", "anc", 2),
                Opt("35ms Ultra-Low Gaming Latency", "low-latency", 3)),
            Group("Price Range", "price", true,
                Opt("Under ₹1,500", "under-1500", 2),
                Opt("₹1,500 – ₹3,500", "1500-3500", 2))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("mivi-spconc", "MIVI SuperPods Concerto Wireless Earbuds (Hi-Res LDAC + 3D Spatial)", "mivi", "MIVI", 2999, 7999, 4.7, 680,
                new[] { "LDAC Hi-Res", "3D Spatial", "Dual Drivers", "60H Playtime" },
                new[] { "superpods", "spatial", "anc", "1500-3500" },
                "SuperPods Concerto", "Bluetooth 5.4 LDAC", "13mm + 6mm", "60H Battery", "USB-C Fast Charge"),
            Prod("mivi-spimm", "MIVI SuperPods Immersio with Dolby Audio & 45dB ANC", "mivi", "MIVI", 2199, 5999, 4.6, 520,
                new[] { "Dolby Audio", "45dB ANC", "Multi-Point Connect", "50ms Gaming" },
                new[] { "superpods", "spatial", "anc", "low-latency", "1500-3500" },
                "SuperPods Immersio", "BT 5.3 + Dolby", "13mm", "50H Playtime", "IPX4 Sweat Proof"),
            Prod("mivi-dpd3", "MIVI DuoPods i7 Esports Edition (35ms Ultra Low Latency)", "mivi", "MIVI", 999, 2999, 4.5, 1950,
                new[] { "35ms Gaming Mode", "Quad Mic AI ENC", "Made in India", "45H Battery" },
                new[] { "duopods", "low-latency", "under-1500" },
                "DuoPods i7", "Bluetooth 5.3", "13mm Drivers", "45H Playtime", "Type-C Fast Charge"),
            Prod("mivi-gan65", "MIVI Volt 65W GaN 3-Port Fast PD Charger for Phones & Laptops", "mivi", "MIVI", 1399, 3499, 4.8, 410,
                new[] { "65W GaN Tech", "2x USB-C + 1x USB-A", "PPS Fast Charge", "Surge Guard" },
                new[] { "chargers", "low-latency", "under-1500" },
                "Volt 65W GaN", "GaN III Chipset", "3 Ports", "PD 3.0 + PPS", "Compact Travel Plug")
        }
    };

    private static MobileBrandPageDto BuildAiPlusPage() => new()
    {
        BrandSlug = "ai-plus",
        BrandName = "Ai+",
        CategoryTagline = "AI-Native Campus Companions • Lecture Transcription & Dual eSIM",
        LogoEmoji = "🧠",
        ThemePrimaryColor = "#7c3aed",
        ThemeAccentColor = "#2563eb",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            BadgeText = "Ai+ STUDENT LAB • NEXT-GEN ON-DEVICE NPU",
            Title = "Ai+ Pulse, Nova Pro & Lite 5G",
            TitleHighlight = "Real-Time Lecture Transcription & Live Translation.",
            Subtitle = "Engineered specifically for university students with privacy-first on-device NPU AI, Dual Global eSIM & 6000mAh all-day power.",
            IsUploadingSoon = true,
            ThemeBgGradient = "linear-gradient(135deg, #130b2e 0%, #4c1d95 55%, #2563eb 100%)",
            TitleColor = "#ffffff",
            TitleHighlightColor = "#c4b5fd",
            SubtitleColor = "#ede9fe",
            FilterAccentColor = "#7c3aed"
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("Ai+ Lineup", "series", false,
                Opt("Ai+ Nova Pro 5G (Flagship NPU)", "nova", 1),
                Opt("Ai+ Pulse 5G (Student Favorite)", "pulse", 2),
                Opt("Ai+ Lite 5G", "lite", 1)),
            Group("Campus AI Capabilities", "features", false,
                Opt("Live Lecture Voice-to-Notes AI", "lecture-ai", 4),
                Opt("Offline Multi-Language Translator", "translate", 4),
                Opt("Dual Global eSIM + 5G Bands", "esim", 3)),
            Group("Price Range", "price", true,
                Opt("Under ₹15,000", "under-15k", 1),
                Opt("₹15,000 – ₹28,000", "15k-28k", 3))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod("ai-novap", "Ai+ Nova Pro 5G (12GB/256GB, Quantum Violet • 45 TOPS NPU)", "ai-plus", "Ai+", 24999, 29999, 4.9, 240,
                new[] { "On-Device NPU", "Lecture Summary", "6000mAh", "Dual eSIM" },
                new[] { "nova", "lecture-ai", "translate", "esim", "15k-28k" },
                "Ai+ Nova Pro", "Ai+ Neural Core NPU", "12GB", "256 GB", "6.78\" 120Hz AMOLED"),
            Prod("ai-pulse2", "Ai+ Pulse 5G Student Edition (8GB/256GB, Midnight Indigo)", "ai-plus", "Ai+", 18999, 22999, 4.8, 390,
                new[] { "Note Transcriber", "Live Translate", "Dual eSIM", "67W Charge" },
                new[] { "pulse", "lecture-ai", "translate", "esim", "15k-28k" },
                "Ai+ Pulse 5G", "Ai+ Smart Octa-Core", "8GB", "256 GB", "6.67\" 120Hz AMOLED"),
            Prod("ai-pulse1", "Ai+ Pulse SE 5G (8GB/128GB, Frosted Silver)", "ai-plus", "Ai+", 15999, 19999, 4.7, 290,
                new[] { "Voice-to-PDF AI", "5500mAh", "50MP OIS", "eSIM Ready" },
                new[] { "pulse", "lecture-ai", "translate", "esim", "15k-28k" },
                "Ai+ Pulse SE", "Ai+ Smart Core", "8GB", "128 GB", "6.67\" FHD+ 120Hz"),
            Prod("ai-lite", "Ai+ Lite 5G (6GB/128GB, Slate Blue)", "ai-plus", "Ai+", 12999, 15999, 4.6, 460,
                new[] { "Lecture Notes AI", "Offline Translate", "5000mAh", "Clean UI" },
                new[] { "lite", "lecture-ai", "translate", "under-15k" },
                "Ai+ Lite 5G", "5G AI Efficiency SoC", "6GB", "128 GB", "6.55\" 120Hz LCD")
        }
    };

    private static MobileBrandPageDto BuildGenericBrandPage(string slug) => new()
    {
        BrandSlug = slug,
        BrandName = slug.ToUpperInvariant(),
        CategoryTagline = $"Official {slug} Smartphones & Campus Deals",
        LogoEmoji = "📱",
        HeroBanner = new MobileBrandHeroBannerDto
        {
            Title = $"{slug.ToUpperInvariant()} Official Store",
            TitleHighlight = "Verified 5G Smartphones & Student Offers.",
            Subtitle = "Explore official brand smartphones with global 5G bands, verified campus warranty & 0% EMI.",
            IsUploadingSoon = true
        },
        FilterGroups = new List<MobileBrandFilterGroupDto>
        {
            Group("Series & Performance", "series", false, Opt("Flagship 5G Series", "flagship", 2), Opt("Student Value 5G", "value", 2)),
            Group("Price Range", "price", true, Opt("Under ₹20,000", "under-20k", 2), Opt("₹20,000 & Above", "above-20k", 2))
        },
        Products = new List<MobileBrandProductDto>
        {
            Prod($"{slug}-1", $"{slug.ToUpperInvariant()} Pro Max 5G Edition (8GB/256GB)", slug, slug, 24999, 29999, 4.8, 120,
                new[] { "5G Global", "120Hz AMOLED", "5000mAh", "50MP OIS" },
                new[] { "flagship", "above-20k" },
                "Pro Series", "5G Octa-Core", "8GB", "256 GB", "6.7\" 120Hz AMOLED"),
            Prod($"{slug}-2", $"{slug.ToUpperInvariant()} Student 5G Essential (6GB/128GB)", slug, slug, 14999, 18999, 4.7, 210,
                new[] { "5G Dual SIM", "All-Day Battery", "Fast Charge", "1Y Warranty" },
                new[] { "value", "under-20k" },
                "Essential", "5G Efficiency", "6GB", "128 GB", "6.6\" FHD+ 120Hz")
        }
    };

    private static MobileBrandFilterGroupDto Group(string title, string key, bool isMoreFilter, params MobileBrandFilterOptionDto[] options)
        => new()
        {
            Title = title,
            Key = key,
            IsMoreFilter = isMoreFilter,
            Options = options.ToList()
        };

    private static MobileBrandFilterOptionDto Opt(string label, string value, int count)
        => new() { Label = label, Value = value, Count = count };

    private static MobileBrandProductDto Prod(
        string id, string name, string brandSlug, string brandName,
        decimal price, decimal origPrice, double rating, int reviews,
        string[] specs, string[] tags,
        string series, string chipset, string ram, string storage, string display)
    {
        var discount = origPrice > price && origPrice > 0
            ? (int)Math.Round(((origPrice - price) / origPrice) * 100)
            : 0;

        return new MobileBrandProductDto
        {
            Id = id,
            Name = name,
            Slug = id,
            BrandSlug = brandSlug,
            BrandName = brandName,
            ImageUrl = "",
            IsUploadingSoon = true,
            Price = price,
            OriginalPrice = origPrice,
            DiscountPercent = discount,
            Rating = rating,
            ReviewCount = reviews,
            KeySpecs = specs.ToList(),
            FilterTags = tags.ToList(),
            Series = series,
            Chipset = chipset,
            Ram = ram,
            Storage = storage,
            Display = display,
            CampusPerk = "Global 5G Dual SIM • Official Warranty • 0% No-Cost Student EMI",
            InStock = true
        };
    }
}

