using System.Globalization;
using Dreamflykit.Application.ElectronicsHub.Models;

namespace Dreamflykit.Application.ElectronicsHub.Services;

public static class ElectronicsBrandCatalogService
{
    public static ElectronicsBrandShowcaseData GetBrandShowcase(
        string subcategorySlug,
        string brandSlug,
        ElectronicsHubDto? hubConfig = null)
    {
        var cleanSub = (subcategorySlug ?? string.Empty).Trim().ToLowerInvariant();
        var cleanBrand = (brandSlug ?? string.Empty).Trim().ToLowerInvariant();

        var defaultPages = ElectronicsHubDto.CreateDefaultSubcategoryPages();
        var allPages = hubConfig?.SubcategoryPages != null && hubConfig.SubcategoryPages.Count > 0
            ? hubConfig.SubcategoryPages
            : defaultPages;

        var subPage = allPages.FirstOrDefault(p =>
            string.Equals(p.CategorySlug, cleanSub, StringComparison.OrdinalIgnoreCase) ||
            (cleanSub.Contains("laptop") && p.CategorySlug.Contains("laptop")) ||
            (cleanSub.Contains("headphone") && p.CategorySlug.Contains("headphone")) ||
            (cleanSub.Contains("adapter") && p.CategorySlug.Contains("adapter")) ||
            (cleanSub.Contains("power") && p.CategorySlug.Contains("power")) ||
            (cleanSub.Contains("sim") && p.CategorySlug.Contains("sim")) ||
            (cleanSub.Contains("storage") && p.CategorySlug.Contains("storage")) ||
            (cleanSub.Contains("watch") && p.CategorySlug.Contains("watch")) ||
            (cleanSub.Contains("cable") && p.CategorySlug.Contains("cable")) ||
            (cleanSub.Contains("monitor") && p.CategorySlug.Contains("monitor")) ||
            (cleanSub.Contains("calculator") && p.CategorySlug.Contains("calculator")))
            ?? allPages.First();

        var brands = subPage.ShopByBrands?.Brands ?? new List<ElectronicsBrandItemDto>();
        var brandItem = brands.FirstOrDefault(b =>
            string.Equals(b.Slug, cleanBrand, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(b.Name, cleanBrand, StringComparison.OrdinalIgnoreCase) ||
            b.Slug.Contains(cleanBrand) ||
            cleanBrand.Contains(b.Slug));

        if (brandItem == null)
        {
            var formattedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cleanBrand.Replace("-", " "));
            brandItem = new ElectronicsBrandItemDto
            {
                Id = "b_" + cleanBrand,
                Name = string.IsNullOrWhiteSpace(formattedName) ? "Official Brand" : formattedName,
                Slug = cleanBrand,
                LogoUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                DisplayOrder = 1,
                IsActive = true
            };
        }

        var brandName = brandItem.Name;
        var subName = subPage.CategoryName;

        var result = new ElectronicsBrandShowcaseData
        {
            SubcategorySlug = subPage.CategorySlug,
            SubcategoryName = subPage.CategoryName,
            SubcategoryEmoji = subPage.Emoji,
            BrandSlug = brandItem.Slug,
            BrandName = brandName,
            BrandItem = brandItem,
            HeroBanner = new ElectronicsBrandHeroBannerDto
            {
                IsEnabled = true,
                Title = brandName + " " + subName,
                TitleHighlight = "Official Campus Tech Store",
                Subtitle = $"Explore genuine {brandName} {subName.ToLowerInvariant()} tailored for university studies, coursework, and global campus life with international student warranty and 110V-240V universal power.",
                ImageUrl = brandItem.HeroBanner != null && !string.IsNullOrWhiteSpace(brandItem.HeroBanner.ImageUrl)
                    ? brandItem.HeroBanner.ImageUrl
                    : "/images/hero/hero-electronics.webp",
                IsUploadingSoon = brandItem.HeroBanner?.IsUploadingSoon ?? true,
                BannerHeight = 360,
                TitleColor = "#063f91",
                TitleHighlightColor = "#0284c7",
                SubtitleColor = "#334155",
                Badges = new List<ElectronicsHeroBadgeDto>
                {
                    new() { Id = "bb1", Label = "110V–240V Universal Power Tested", Icon = "zap" },
                    new() { Id = "bb2", Label = "Official Campus Warranty Supported", Icon = "shield" },
                    new() { Id = "bb3", Label = "Express Pre-Departure Campus Delivery", Icon = "truck" }
                }
            }
        };

        BuildFacetedFiltersAndProducts(result, subPage.CategorySlug, brandItem.Slug, brandName, subName);

        return result;
    }

    private static void BuildFacetedFiltersAndProducts(
        ElectronicsBrandShowcaseData data,
        string categorySlug,
        string brandSlug,
        string brandName,
        string categoryName)
    {
        var cleanCat = categorySlug.ToLowerInvariant();
        var cleanBrand = brandSlug.ToLowerInvariant();
        var isLaptop = cleanCat.Contains("laptop") || cleanCat.Contains("macbook");
        var isAudio = cleanCat.Contains("headphone") || cleanCat.Contains("audio");
        var isAdapter = cleanCat.Contains("adapter");
        var isPower = cleanCat.Contains("power") || cleanCat.Contains("charger");
        var isSim = cleanCat.Contains("sim");
        var isStorage = cleanCat.Contains("storage") || cleanCat.Contains("ssd");
        var isWatch = cleanCat.Contains("watch") || cleanCat.Contains("wearable");
        var isCable = cleanCat.Contains("cable") || cleanCat.Contains("hub");
        var isMonitor = cleanCat.Contains("monitor") || cleanCat.Contains("display");

        var prods = new List<ElectronicsBrandProductDto>();
        var filters = new List<ElectronicsBrandFilterGroupDto>();

        if (isLaptop)
        {
            BuildLaptopShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isAudio)
        {
            BuildAudioShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isAdapter)
        {
            BuildAdapterShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isPower)
        {
            BuildPowerShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isSim)
        {
            BuildSimShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isStorage)
        {
            BuildStorageShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isWatch)
        {
            BuildWatchShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isCable)
        {
            BuildCableShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isMonitor)
        {
            BuildMonitorShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else
        {
            BuildGenericShowcase(data.SubcategorySlug, cleanBrand, brandName, categoryName, prods, filters);
        }

        data.Products = prods;
        data.FilterGroups = filters;
    }

    private static void BuildLaptopShowcase(
        string brand,
        string brandName,
        string catName,
        List<ElectronicsBrandProductDto> prods,
        List<ElectronicsBrandFilterGroupDto> filters)
    {
        var isApple = brand.Contains("apple");

        if (isApple)
        {
            prods.Add(new ElectronicsBrandProductDto
            {
                Id = "p_mac_air13_m3",
                Name = "Apple MacBook Air 13.6-inch (M3 Chip, 16GB Unified RAM, 512GB SSD) - Space Grey",
                Slug = "apple-macbook-air-13-m3-16gb-512gb",
                BrandName = "Apple",
                BrandSlug = "apple",
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 114900,
                OriginalPrice = 134900,
                DiscountPercent = 15,
                Rating = 4.9,
                ReviewCount = 142,
                Processor = "Apple M3",
                Ram = "16 GB",
                Storage = "512 GB SSD",
                ScreenSize = "13.6\"",
                Series = "MacBook Air",
                OperatingSystem = "macOS",
                KeySpecs = new() { "M3 8-Core CPU", "16GB Unified RAM", "512GB SSD", "13.6\" Liquid Retina", "18h Battery" },
                CampusPerk = "1 Year Official AppleCare+ Campus Support • 110V-240V Universal MagSafe"
            });

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = "p_mac_pro14_m3pro",
                Name = "Apple MacBook Pro 14.2-inch (M3 Pro, 18GB Unified RAM, 512GB SSD) - Space Black",
                Slug = "apple-macbook-pro-14-m3-pro-18gb-512gb",
                BrandName = "Apple",
                BrandSlug = "apple",
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 179900,
                OriginalPrice = 199900,
                DiscountPercent = 10,
                Rating = 4.9,
                ReviewCount = 89,
                Processor = "Apple M3 Pro",
                Ram = "18 GB",
                Storage = "512 GB SSD",
                ScreenSize = "14.2\"",
                Series = "MacBook Pro",
                OperatingSystem = "macOS",
                KeySpecs = new() { "M3 Pro 11-Core", "18GB RAM", "512GB SSD", "Liquid Retina XDR 120Hz", "HDMI & SD Slot" },
                CampusPerk = "Pro Creative & Coding Suite • 110V-240V Universal Power"
            });

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = "p_mac_air15_m3",
                Name = "Apple MacBook Air 15.3-inch (M3 Chip, 16GB Unified RAM, 512GB SSD) - Midnight",
                Slug = "apple-macbook-air-15-m3-16gb-512gb",
                BrandName = "Apple",
                BrandSlug = "apple",
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 134900,
                OriginalPrice = 154900,
                DiscountPercent = 13,
                Rating = 4.8,
                ReviewCount = 67,
                Processor = "Apple M3",
                Ram = "16 GB",
                Storage = "512 GB SSD",
                ScreenSize = "15.3\"",
                Series = "MacBook Air",
                OperatingSystem = "macOS",
                KeySpecs = new() { "M3 8-Core", "16GB RAM", "512GB SSD", "15.3\" Liquid Retina", "MagSafe 3" },
                CampusPerk = "Large Workspace & Dual Monitor Support • Universal Power"
            });

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = "p_mac_pro16_m3max",
                Name = "Apple MacBook Pro 16.2-inch (M3 Max, 36GB Unified RAM, 1TB SSD) - Silver",
                Slug = "apple-macbook-pro-16-m3-max-36gb-1tb",
                BrandName = "Apple",
                BrandSlug = "apple",
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 299900,
                OriginalPrice = 349900,
                DiscountPercent = 14,
                Rating = 5.0,
                ReviewCount = 45,
                Processor = "Apple M3 Max",
                Ram = "36 GB",
                Storage = "1 TB SSD",
                ScreenSize = "16.2\"",
                Series = "MacBook Pro",
                OperatingSystem = "macOS",
                KeySpecs = new() { "M3 Max 14-Core", "36GB RAM", "1TB SSD", "Liquid Retina XDR", "22h Battery" },
                CampusPerk = "AI Machine Learning & 3D Engineering Grade • Worldwide Warranty"
            });

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = "p_mac_air13_m2",
                Name = "Apple MacBook Air 13.6-inch (M2 Chip, 8GB Unified RAM, 256GB SSD) - Starlight",
                Slug = "apple-macbook-air-13-m2-8gb-256gb",
                BrandName = "Apple",
                BrandSlug = "apple",
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 84900,
                OriginalPrice = 99900,
                DiscountPercent = 15,
                Rating = 4.8,
                ReviewCount = 210,
                Processor = "Apple M2",
                Ram = "8 GB",
                Storage = "256 GB SSD",
                ScreenSize = "13.6\"",
                Series = "MacBook Air",
                OperatingSystem = "macOS",
                KeySpecs = new() { "M2 8-Core", "8GB RAM", "256GB SSD", "13.6\" Liquid Retina", "1080p FaceTime HD" },
                CampusPerk = "Budget Campus Essential • Ultra Lightweight 1.24kg"
            });

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = "p_mac_mini_m2pro",
                Name = "Apple Mac Mini Desktop (M2 Pro, 16GB Unified RAM, 512GB SSD)",
                Slug = "apple-mac-mini-m2-pro-16gb-512gb",
                BrandName = "Apple",
                BrandSlug = "apple",
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 119900,
                OriginalPrice = 129900,
                DiscountPercent = 8,
                Rating = 4.9,
                ReviewCount = 52,
                Processor = "Apple M2 Pro",
                Ram = "16 GB",
                Storage = "512 GB SSD",
                ScreenSize = "Compact Desktop",
                Series = "Mac Mini",
                OperatingSystem = "macOS",
                KeySpecs = new() { "M2 Pro 10-Core", "16GB RAM", "512GB SSD", "4x Thunderbolt 4", "10Gb Ethernet" },
                CampusPerk = "Dorm Desk Heavy Computing • Multi-Display 8K Support"
            });

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = "p_magic_keyboard",
                Name = "Apple Magic Keyboard with Touch ID and Numeric Keypad - White Keys",
                Slug = "apple-magic-keyboard-touch-id-numeric",
                BrandName = "Apple",
                BrandSlug = "apple",
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 17500,
                OriginalPrice = 19500,
                DiscountPercent = 10,
                Rating = 4.7,
                ReviewCount = 78,
                Processor = "Wireless Accessories",
                Ram = "N/A",
                Storage = "N/A",
                ScreenSize = "Full Layout",
                Series = "Accessories",
                OperatingSystem = "macOS / iOS",
                KeySpecs = new() { "Touch ID Biometrics", "Numeric Keypad", "Rechargeable Battery", "Bluetooth Multi-Device" },
                CampusPerk = "Ergonomic Study Productivity • USB-C Woven Cable Included"
            });

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = "p_magic_mouse",
                Name = "Apple Magic Mouse (Wireless, Rechargeable) - Black Multi-Touch Surface",
                Slug = "apple-magic-mouse-wireless-black",
                BrandName = "Apple",
                BrandSlug = "apple",
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 8900,
                OriginalPrice = 9900,
                DiscountPercent = 10,
                Rating = 4.6,
                ReviewCount = 95,
                Processor = "Wireless Accessories",
                Ram = "N/A",
                Storage = "N/A",
                ScreenSize = "Ergonomic",
                Series = "Accessories",
                OperatingSystem = "macOS / iPadOS",
                KeySpecs = new() { "Multi-Touch Gesture Surface", "Bluetooth Auto-Pair", "USB-C Fast Recharge", "Smooth Gliding Foot" },
                CampusPerk = "Seamless Mac Gesture Navigation • Long Battery Life"
            });
        }
        else
        {
            var series1 = brand switch
            {
                "dell" => "XPS",
                "hp" => "Spectre x360",
                "lenovo" => "ThinkPad X1",
                "asus" => "ZenBook OLED",
                "acer" => "Swift Go OLED",
                "microsoft" => "Surface Laptop 7",
                "samsung" => "Galaxy Book4 Pro",
                _ => "Flagship Pro"
            };

            var series2 = brand switch
            {
                "dell" => "Inspiron Plus",
                "hp" => "Envy x360",
                "lenovo" => "Yoga 2-in-1",
                "asus" => "Vivobook 15",
                "acer" => "Aspire 5 Slim",
                "microsoft" => "Surface Pro 11",
                "samsung" => "Galaxy Book4 360",
                _ => "Campus Essential"
            };

            var series3 = brand switch
            {
                "dell" => "Alienware",
                "hp" => "OMEN Gaming",
                "lenovo" => "Legion Gaming",
                "asus" => "ROG Zephyrus",
                "acer" => "Predator Helios",
                "microsoft" => "Surface Studio 2",
                "samsung" => "Galaxy Book4 Ultra",
                _ => "High Performance"
            };

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = $"p_{brand}_flagship",
                Name = $"{brandName} {series1} 14 (Intel Core Ultra 7 155H, 16GB LPDDR5X, 1TB SSD, 14\" 2.8K OLED)",
                Slug = $"{brand}-flagship-14-ultra7-16gb-1tb",
                BrandName = brandName,
                BrandSlug = brand,
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 119990,
                OriginalPrice = 139990,
                DiscountPercent = 14,
                Rating = 4.8,
                ReviewCount = 112,
                Processor = "Intel Core Ultra 7",
                Ram = "16 GB",
                Storage = "1 TB SSD",
                ScreenSize = "14.0\"",
                Series = series1,
                OperatingSystem = "Windows 11 Home",
                KeySpecs = new() { "Core Ultra 7 (16 Cores)", "16GB LPDDR5X", "1TB Gen4 SSD", "14\" 2.8K OLED 120Hz", "Intel Arc Graphics" },
                CampusPerk = "Intel AI Boost NPU • 110V-240V Universal Fast Charger"
            });

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = $"p_{brand}_essential",
                Name = $"{brandName} {series2} (Intel Core Ultra 5 125H, 16GB RAM, 512GB SSD, 14\" 2.2K Display)",
                Slug = $"{brand}-essential-14-ultra5-16gb-512gb",
                BrandName = brandName,
                BrandSlug = brand,
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 74990,
                OriginalPrice = 89990,
                DiscountPercent = 17,
                Rating = 4.7,
                ReviewCount = 98,
                Processor = "Intel Core Ultra 5",
                Ram = "16 GB",
                Storage = "512 GB SSD",
                ScreenSize = "14.0\"",
                Series = series2,
                OperatingSystem = "Windows 11 Home",
                KeySpecs = new() { "Core Ultra 5 125H", "16GB RAM", "512GB SSD", "14\" 2.2K IPS Anti-Glare", "16h Battery" },
                CampusPerk = "Student Best Seller • Backlit Keyboard & Fingerprint Reader"
            });

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = $"p_{brand}_gaming",
                Name = $"{brandName} {series3} (Intel Core Ultra 9, 32GB RAM, 1TB SSD, RTX 4070 8GB Graphics)",
                Slug = $"{brand}-power-ultra9-32gb-1tb-rtx4070",
                BrandName = brandName,
                BrandSlug = brand,
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 189990,
                OriginalPrice = 219990,
                DiscountPercent = 14,
                Rating = 4.9,
                ReviewCount = 76,
                Processor = "Intel Core Ultra 9",
                Ram = "32 GB",
                Storage = "1 TB SSD",
                ScreenSize = "16.0\"",
                Series = series3,
                OperatingSystem = "Windows 11 Pro",
                KeySpecs = new() { "Core Ultra 9 185H", "32GB DDR5", "1TB NVMe", "NVIDIA RTX 4070", "16\" QHD 240Hz" },
                CampusPerk = "Heavy Engineering & Data Science Ready • Vapor Chamber Cooling"
            });

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = $"p_{brand}_budget",
                Name = $"{brandName} Campus Edition 15.6\" (Intel Core i5 13th Gen, 16GB RAM, 512GB SSD, FHD)",
                Slug = $"{brand}-campus-15-i5-16gb-512gb",
                BrandName = brandName,
                BrandSlug = brand,
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 49990,
                OriginalPrice = 62990,
                DiscountPercent = 21,
                Rating = 4.6,
                ReviewCount = 184,
                Processor = "Intel Core i5",
                Ram = "16 GB",
                Storage = "512 GB SSD",
                ScreenSize = "15.6\"",
                Series = series2,
                OperatingSystem = "Windows 11 Home",
                KeySpecs = new() { "Core i5-1335U", "16GB RAM", "512GB SSD", "15.6\" FHD Micro-Edge", "Fast Charge 50% in 45m" },
                CampusPerk = "Affordable International Student Workhorse • 1 Year Onsite Warranty"
            });

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = $"p_{brand}_ryzen",
                Name = $"{brandName} Performance Slim (AMD Ryzen 7 7840HS, 16GB LPDDR5, 512GB SSD, 14\" 2.8K OLED)",
                Slug = $"{brand}-ryzen7-16gb-512gb-oled",
                BrandName = brandName,
                BrandSlug = brand,
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 82990,
                OriginalPrice = 98990,
                DiscountPercent = 16,
                Rating = 4.8,
                ReviewCount = 63,
                Processor = "AMD Ryzen 7",
                Ram = "16 GB",
                Storage = "512 GB SSD",
                ScreenSize = "14.0\"",
                Series = series1,
                OperatingSystem = "Windows 11 Home",
                KeySpecs = new() { "Ryzen 7 7840HS", "16GB RAM", "512GB SSD", "Radeon 780M Graphics", "14\" 2.8K OLED" },
                CampusPerk = "Extreme Battery Efficiency & Thermal Management • Universal Power"
            });

            prods.Add(new ElectronicsBrandProductDto
            {
                Id = $"p_{brand}_pro32",
                Name = $"{brandName} Pro Workstation 16\" (Intel Core Ultra 7, 32GB RAM, 2TB SSD, RTX 4060, Windows 11 Pro)",
                Slug = $"{brand}-pro-workstation-ultra7-32gb-2tb",
                BrandName = brandName,
                BrandSlug = brand,
                CategorySlug = "laptops-macbooks",
                CategoryName = catName,
                Price = 149990,
                OriginalPrice = 179990,
                DiscountPercent = 17,
                Rating = 4.9,
                ReviewCount = 41,
                Processor = "Intel Core Ultra 7",
                Ram = "32 GB",
                Storage = "2 TB SSD",
                ScreenSize = "16.0\"",
                Series = series1,
                OperatingSystem = "Windows 11 Pro",
                KeySpecs = new() { "Core Ultra 7", "32GB RAM", "2TB SSD", "RTX 4060 8GB", "16\" 3.2K 120Hz IPS" },
                CampusPerk = "Software Architecture & CAD Certified • International Warranty"
            });
        }

        // Build Filters
        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Processor",
            Key = "processor",
            Options = isApple
                ? new()
                {
                    new() { Label = "Apple M3 Max (Pro / CAD)", Value = "m3 max", Count = 1 },
                    new() { Label = "Apple M3 Pro (High Perf)", Value = "m3 pro", Count = 1 },
                    new() { Label = "Apple M3 (Next-Gen)", Value = "m3", Count = 2 },
                    new() { Label = "Apple M2 (Fast & Cool)", Value = "m2", Count = 2 }
                }
                : new()
                {
                    new() { Label = "Intel Core Ultra 9 (AI NPU)", Value = "ultra 9", Count = 1 },
                    new() { Label = "Intel Core Ultra 7 (Fast AI)", Value = "ultra 7", Count = 2 },
                    new() { Label = "Intel Core Ultra 5 (Everyday)", Value = "ultra 5", Count = 1 },
                    new() { Label = "Intel Core i5 (13th Gen)", Value = "i5", Count = 1 },
                    new() { Label = "AMD Ryzen 7 (High Battery)", Value = "ryzen", Count = 1 }
                }
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "RAM / Memory",
            Key = "ram",
            Options = new()
            {
                new() { Label = "8 GB", Value = "8gb", Count = prods.Count(p => p.Ram.Contains("8")) },
                new() { Label = "16 GB (Recommended)", Value = "16gb", Count = prods.Count(p => p.Ram.Contains("16")) },
                new() { Label = "18 GB - 36 GB (Pro)", Value = "32gb", Count = prods.Count(p => p.Ram.Contains("18") || p.Ram.Contains("32") || p.Ram.Contains("36")) }
            }
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Storage / SSD",
            Key = "storage",
            Options = new()
            {
                new() { Label = "256 GB SSD", Value = "256gb", Count = prods.Count(p => p.Storage.Contains("256")) },
                new() { Label = "512 GB SSD (Sweetspot)", Value = "512gb", Count = prods.Count(p => p.Storage.Contains("512")) },
                new() { Label = "1 TB SSD & Above", Value = "1tb", Count = prods.Count(p => p.Storage.Contains("1 TB") || p.Storage.Contains("2 TB")) }
            }
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Screen Size",
            Key = "screensize",
            Options = new()
            {
                new() { Label = "13\" - 13.6\" (Compact Dorm/Air)", Value = "13", Count = prods.Count(p => p.ScreenSize.Contains("13")) },
                new() { Label = "14\" - 14.5\" (Ideal Student)", Value = "14", Count = prods.Count(p => p.ScreenSize.Contains("14")) },
                new() { Label = "15\" - 16\"+ (Large Workspace)", Value = "16", Count = prods.Count(p => p.ScreenSize.Contains("15") || p.ScreenSize.Contains("16")) }
            }
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Price Range",
            Key = "price",
            Options = new()
            {
                new() { Label = "Under ₹60,000", Value = "under60k", Count = prods.Count(p => p.Price < 60000) },
                new() { Label = "₹60,000 - ₹90,000", Value = "60k-90k", Count = prods.Count(p => p.Price >= 60000 && p.Price <= 90000) },
                new() { Label = "₹90,000 - ₹1,40,000", Value = "90k-140k", Count = prods.Count(p => p.Price > 90000 && p.Price <= 140000) },
                new() { Label = "Above ₹1,40,000", Value = "above140k", Count = prods.Count(p => p.Price > 140000) }
            }
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Student Discount",
            Key = "discount",
            Options = new()
            {
                new() { Label = "10% Off & Above", Value = "10", Count = prods.Count(p => p.DiscountPercent >= 10) },
                new() { Label = "15% Off & Above", Value = "15", Count = prods.Count(p => p.DiscountPercent >= 15) },
                new() { Label = "20% Off & Above", Value = "20", Count = prods.Count(p => p.DiscountPercent >= 20) }
            }
        });
    }

    private static void BuildAudioShowcase(
        string brand,
        string brandName,
        string catName,
        List<ElectronicsBrandProductDto> prods,
        List<ElectronicsBrandFilterGroupDto> filters)
    {
        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_anc_flagship",
            Name = $"{brandName} Premium Wireless Noise-Cancelling Headphones (Adaptive ANC, 40h Battery, Multi-Device)",
            Slug = $"{brand}-premium-wireless-anc-headphones",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "headphones-audio",
            CategoryName = catName,
            Price = 24990,
            OriginalPrice = 29990,
            DiscountPercent = 17,
            Rating = 4.9,
            ReviewCount = 215,
            Processor = "Active Noise Cancellation",
            Ram = "40 Hours Battery",
            Storage = "Over-Ear Wireless",
            ScreenSize = "Hi-Res Audio LDAC",
            Series = "ANC Over-Ear",
            KeySpecs = new() { "Industry-Leading ANC", "40h Playtime", "Multipoint Bluetooth 5.3", "Foldable Travel Case", "Touch Gestures" },
            CampusPerk = "Quiet Study in Crowded Libraries • Long-Haul Flight Ear Relief"
        });

        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_tws_anc",
            Name = $"{brandName} Active Noise-Cancelling True Wireless Earbuds (Transparency Mode, IPX4, 32h Playtime)",
            Slug = $"{brand}-tws-anc-earbuds-ipx4",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "headphones-audio",
            CategoryName = catName,
            Price = 14990,
            OriginalPrice = 18990,
            DiscountPercent = 21,
            Rating = 4.8,
            ReviewCount = 180,
            Processor = "Active Noise Cancellation",
            Ram = "32 Hours with Case",
            Storage = "In-Ear TWS",
            ScreenSize = "Spatial Audio",
            Series = "TWS Earbuds",
            KeySpecs = new() { "Smart ANC & Ambient Sound", "Dual Beamforming Mics", "Fast Wireless Qi Charging", "IPX4 Sweat Resistant" },
            CampusPerk = "Daily Lectures & Commutes • Clear Zoom Call Voice"
        });

        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_student_value",
            Name = $"{brandName} Campus Everyday Wireless Over-Ear Headphones (Deep Bass, 50h Battery, Lightweight)",
            Slug = $"{brand}-campus-wireless-over-ear-headphones",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "headphones-audio",
            CategoryName = catName,
            Price = 4990,
            OriginalPrice = 6990,
            DiscountPercent = 29,
            Rating = 4.6,
            ReviewCount = 340,
            Processor = "Passive Isolation",
            Ram = "50 Hours Battery",
            Storage = "Over-Ear Wireless",
            ScreenSize = "Bass Boost",
            Series = "Everyday Wireless",
            KeySpecs = new() { "50h Mega Battery", "Dual Equalizer Modes", "USB-C Fast Charge", "Soft Memory Foam Cushions" },
            CampusPerk = "Budget-Friendly Campus Audio • 1 Year Warranty"
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Headphone Type",
            Key = "type",
            Options = new()
            {
                new() { Label = "Over-Ear Wireless (Library/Travel)", Value = "overear", Count = 2 },
                new() { Label = "In-Ear True Wireless (TWS)", Value = "tws", Count = 1 }
            }
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Noise Cancellation",
            Key = "anc",
            Options = new()
            {
                new() { Label = "Active Noise Cancellation (ANC)", Value = "anc", Count = 2 },
                new() { Label = "Passive Noise Isolation", Value = "passive", Count = 1 }
            }
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Price Range",
            Key = "price",
            Options = new()
            {
                new() { Label = "Under ₹6,000", Value = "under6k", Count = 1 },
                new() { Label = "₹6,000 - ₹16,000", Value = "6k-16k", Count = 1 },
                new() { Label = "Above ₹16,000", Value = "above16k", Count = 1 }
            }
        });
    }

    private static void BuildAdapterShowcase(
        string brand,
        string brandName,
        string catName,
        List<ElectronicsBrandProductDto> prods,
        List<ElectronicsBrandFilterGroupDto> filters)
    {
        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_adp_65w",
            Name = $"{brandName} All-in-One 65W GaN Universal Travel Adapter (USA, UK, EU, AU Plugs, 3x USB-C + 2x USB-A)",
            Slug = $"{brand}-65w-gan-universal-travel-adapter",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "travel-adapters",
            CategoryName = catName,
            Price = 2899,
            OriginalPrice = 3999,
            DiscountPercent = 28,
            Rating = 4.9,
            ReviewCount = 512,
            Processor = "65W GaN Fast Charge",
            Ram = "3x USB-C, 2x USB-A",
            Storage = "150+ Countries",
            ScreenSize = "Auto-Reset Fuse",
            Series = "GaN Pro Adapters",
            KeySpecs = new() { "65W Power Delivery", "Charges Laptop & Phone", "USA/UK/EU/AU Slide Plugs", "Auto-Reset 10A Fuse" },
            CampusPerk = "Works in 150+ Countries • Eliminates Laptop Power Bricks"
        });

        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_adp_30w",
            Name = $"{brandName} Universal World Travel Plug Adapter with Dual USB-C 30W Fast Charge",
            Slug = $"{brand}-30w-universal-travel-adapter",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "travel-adapters",
            CategoryName = catName,
            Price = 1899,
            OriginalPrice = 2499,
            DiscountPercent = 24,
            Rating = 4.8,
            ReviewCount = 380,
            Processor = "30W Fast Charge",
            Ram = "Dual USB-C",
            Storage = "Worldwide Fit",
            ScreenSize = "Surge Protection",
            Series = "Compact Adapters",
            KeySpecs = new() { "30W USB-C PD", "Child Safety Shutter", "Compact Lightweight", "Universal AC Outlet" },
            CampusPerk = "Surge Protected for Flight & Dorm Sockets • 1 Year Warranty"
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Power Wattage",
            Key = "wattage",
            Options = new()
            {
                new() { Label = "65W GaN (Laptop + Phone)", Value = "65w", Count = 1 },
                new() { Label = "30W Fast Charge (Phone/Tablet)", Value = "30w", Count = 1 }
            }
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Country Compatibility",
            Key = "region",
            Options = new()
            {
                new() { Label = "Worldwide 150+ Countries (US/UK/EU/AU)", Value = "worldwide", Count = 2 }
            }
        });
    }

    private static void BuildPowerShowcase(
        string brand,
        string brandName,
        string catName,
        List<ElectronicsBrandProductDto> prods,
        List<ElectronicsBrandFilterGroupDto> filters)
    {
        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_pb_20k_65w",
            Name = $"{brandName} 20,000mAh 65W Laptop Power Bank (Dual USB-C PD, Airline Approved 74Wh)",
            Slug = $"{brand}-20000mah-65w-laptop-power-bank",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "power-banks-chargers",
            CategoryName = catName,
            Price = 4499,
            OriginalPrice = 5999,
            DiscountPercent = 25,
            Rating = 4.9,
            ReviewCount = 420,
            Processor = "65W Power Delivery",
            Ram = "20,000 mAh",
            Storage = "Airline Approved",
            ScreenSize = "Digital LED Display",
            Series = "Laptop Power Banks",
            KeySpecs = new() { "Charges MacBook/Laptops at 65W", "20000mAh High Capacity", "TSA Flight Safe (74Wh)", "Digital Battery % Screen" },
            CampusPerk = "Power Laptops in Library & 14h Flights • 18 Months Warranty"
        });

        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_pb_10k_mag",
            Name = $"{brandName} 10,000mAh Magnetic Wireless Power Bank (Foldable Stand, 20W PD Fast Charge)",
            Slug = $"{brand}-10000mah-magnetic-power-bank",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "power-banks-chargers",
            CategoryName = catName,
            Price = 2999,
            OriginalPrice = 3999,
            DiscountPercent = 25,
            Rating = 4.7,
            ReviewCount = 280,
            Processor = "20W Fast Charge",
            Ram = "10,000 mAh",
            Storage = "MagSafe Wireless",
            ScreenSize = "Foldable Kickstand",
            Series = "Pocket Wireless",
            KeySpecs = new() { "Strong Magnetic Snap", "10,000mAh Pocket Size", "Pass-Through Charging", "Hands-Free Lecture Viewing" },
            CampusPerk = "Slim Backpack Daily Essential • Certified Safe Battery"
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Battery Capacity",
            Key = "capacity",
            Options = new()
            {
                new() { Label = "20,000 mAh (Laptop + Phone)", Value = "20000mah", Count = 1 },
                new() { Label = "10,000 mAh (Pocket Daily)", Value = "10000mah", Count = 1 }
            }
        });
    }

    private static void BuildSimShowcase(
        string brand,
        string brandName,
        string catName,
        List<ElectronicsBrandProductDto> prods,
        List<ElectronicsBrandFilterGroupDto> filters)
    {
        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_sim_usa30",
            Name = $"{brandName} USA 30-Day Unlimited 5G eSIM / Physical SIM (Unlimited Calling + 50GB High Speed Data)",
            Slug = $"{brand}-usa-30-day-unlimited-5g-sim",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "student-sims",
            CategoryName = catName,
            Price = 2999,
            OriginalPrice = 3999,
            DiscountPercent = 25,
            Rating = 4.8,
            ReviewCount = 310,
            Processor = "Instant eSIM / Physical",
            Ram = "50GB 5G Data",
            Storage = "USA Coverage",
            ScreenSize = "Unlimited USA & India Calls",
            Series = "Student Plans",
            KeySpecs = new() { "Instant QR Activation", "Free Incoming from India", "Hotspot Tethering Included", "Zero Roaming Charges" },
            CampusPerk = "Activated Before Boarding Flight • 24/7 Student WhatsApp Support"
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Destination Region",
            Key = "region",
            Options = new()
            {
                new() { Label = "USA & North America", Value = "usa", Count = 1 },
                new() { Label = "UK & Europe (34 Countries)", Value = "uk-eu", Count = 1 }
            }
        });
    }

    private static void BuildStorageShowcase(
        string brand,
        string brandName,
        string catName,
        List<ElectronicsBrandProductDto> prods,
        List<ElectronicsBrandFilterGroupDto> filters)
    {
        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_ssd_1tb",
            Name = $"{brandName} Extreme 1TB Portable SSD (Up to 1,050 MB/s NVMe, IP55 Water & Dust Resistant)",
            Slug = $"{brand}-extreme-1tb-portable-ssd",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "storage-ssds",
            CategoryName = catName,
            Price = 8499,
            OriginalPrice = 11999,
            DiscountPercent = 29,
            Rating = 4.9,
            ReviewCount = 612,
            Processor = "1,050 MB/s NVMe",
            Ram = "1 TB Capacity",
            Storage = "1 TB NVMe",
            ScreenSize = "Drop-Resistant IP55",
            Series = "Rugged Portable SSD",
            KeySpecs = new() { "1050 MB/s Read Speed", "2-Meter Drop Protection", "Carabiner Loop for Backpacks", "256-Bit Hardware Encryption" },
            CampusPerk = "Safe Assignment & Project Backups • Mac & Windows Universal"
        });

        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_ssd_2tb",
            Name = $"{brandName} Extreme 2TB High-Speed Portable SSD (Up to 2,000 MB/s Extreme NVMe)",
            Slug = $"{brand}-extreme-2tb-portable-ssd",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "storage-ssds",
            CategoryName = catName,
            Price = 15999,
            OriginalPrice = 21999,
            DiscountPercent = 27,
            Rating = 4.9,
            ReviewCount = 380,
            Processor = "2,000 MB/s Extreme",
            Ram = "2 TB Capacity",
            Storage = "2 TB NVMe",
            ScreenSize = "Aluminum Core Heat Sink",
            Series = "Pro Extreme",
            KeySpecs = new() { "2000 MB/s Ultra Fast", "Forged Aluminum Chassis", "IP55 Rating", "5 Years Global Warranty" },
            CampusPerk = "Large 4K Video & Coding Datasets • 5 Years Campus Protection"
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Capacity",
            Key = "capacity",
            Options = new()
            {
                new() { Label = "1 TB SSD (Student Sweetspot)", Value = "1tb", Count = 1 },
                new() { Label = "2 TB SSD (Large Datasets)", Value = "2tb", Count = 1 }
            }
        });
    }

    private static void BuildWatchShowcase(
        string brand,
        string brandName,
        string catName,
        List<ElectronicsBrandProductDto> prods,
        List<ElectronicsBrandFilterGroupDto> filters)
    {
        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_watch_smart",
            Name = $"{brandName} Smartwatch (Always-On AMOLED, Dual GPS, Bluetooth Calling, 7-Day Battery)",
            Slug = $"{brand}-smartwatch-amoled-gps-calling",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "smartwatches-wearables",
            CategoryName = catName,
            Price = 8999,
            OriginalPrice = 12999,
            DiscountPercent = 30,
            Rating = 4.8,
            ReviewCount = 290,
            Processor = "Dual GPS & Sensor Suite",
            Ram = "7-Day Battery",
            Storage = "AMOLED Display",
            ScreenSize = "1.43\" AMOLED",
            Series = "Active Wearables",
            KeySpecs = new() { "Always-On AMOLED", "Bluetooth Calling & Mic", "Class Timetable & Alarm Alert", "5ATM Swimproof" },
            CampusPerk = "Morning Lecture Alarms & Daily Activity Tracker"
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Display Type",
            Key = "display",
            Options = new()
            {
                new() { Label = "AMOLED Ultra-Bright", Value = "amoled", Count = 1 }
            }
        });
    }

    private static void BuildCableShowcase(
        string brand,
        string brandName,
        string catName,
        List<ElectronicsBrandProductDto> prods,
        List<ElectronicsBrandFilterGroupDto> filters)
    {
        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_hub_8in1",
            Name = $"{brandName} 8-in-1 USB-C Hub & Dock (4K HDMI, 100W PD Pass-Through, Gigabit Ethernet, SD Reader)",
            Slug = $"{brand}-8in1-usbc-hub-dock-100w",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "cables-hubs-docks",
            CategoryName = catName,
            Price = 3499,
            OriginalPrice = 4999,
            DiscountPercent = 30,
            Rating = 4.8,
            ReviewCount = 420,
            Processor = "100W PD Pass-through",
            Ram = "8-in-1 Ports",
            Storage = "Gigabit Ethernet",
            ScreenSize = "4K @ 60Hz HDMI",
            Series = "Multiport Docks",
            KeySpecs = new() { "4K 60Hz HDMI", "100W Power Delivery", "Gigabit LAN Port", "Dual USB-A 3.2 Ports" },
            CampusPerk = "Turns Laptop into Complete Dorm Desktop Setup"
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Hub Configuration",
            Key = "ports",
            Options = new()
            {
                new() { Label = "8-in-1 Multiport with Ethernet", Value = "8in1", Count = 1 }
            }
        });
    }

    private static void BuildMonitorShowcase(
        string brand,
        string brandName,
        string catName,
        List<ElectronicsBrandProductDto> prods,
        List<ElectronicsBrandFilterGroupDto> filters)
    {
        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_mon_27_4k",
            Name = $"{brandName} 27-inch 4K UHD IPS USB-C Study & Coding Monitor (USB-C 65W PD Charging, Ergonomic Stand)",
            Slug = $"{brand}-27-inch-4k-usbc-monitor",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = "monitors-displays",
            CategoryName = catName,
            Price = 24990,
            OriginalPrice = 32990,
            DiscountPercent = 24,
            Rating = 4.9,
            ReviewCount = 185,
            Processor = "65W USB-C One Cable",
            Ram = "IPS 99% sRGB",
            Storage = "Height & Pivot Stand",
            ScreenSize = "27.0\"",
            Series = "Pro Monitors",
            KeySpecs = new() { "3840x2160 4K UHD", "One-Cable USB-C 65W Power", "Eye-Care Flicker Free", "Height & Pivot Adjustable" },
            CampusPerk = "Dual-Window Study & Coding Workspace • Worldwide Power 110V-240V"
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Screen Size & Resolution",
            Key = "resolution",
            Options = new()
            {
                new() { Label = "27\" 4K UHD (USB-C Hub)", Value = "27-4k", Count = 1 }
            }
        });
    }

    private static void BuildGenericShowcase(
        string subSlug,
        string brand,
        string brandName,
        string catName,
        List<ElectronicsBrandProductDto> prods,
        List<ElectronicsBrandFilterGroupDto> filters)
    {
        prods.Add(new ElectronicsBrandProductDto
        {
            Id = $"p_{brand}_generic_1",
            Name = $"{brandName} Official {catName} Student Edition (110V–240V Universal Power Certified)",
            Slug = $"{brand}-official-student-edition",
            BrandName = brandName,
            BrandSlug = brand,
            CategorySlug = subSlug,
            CategoryName = catName,
            Price = 2499,
            OriginalPrice = 3499,
            DiscountPercent = 28,
            Rating = 4.8,
            ReviewCount = 120,
            Processor = "Official Certified",
            Ram = "Campus Grade",
            Storage = "Worldwide Fit",
            ScreenSize = "Compact",
            Series = "Official Hardware",
            KeySpecs = new() { "100% Genuine Hardware", "International Warranty", "Campus Tested", "Express Dispatch" },
            CampusPerk = "Tested for International Academic Life • 1 Year Warranty"
        });

        filters.Add(new ElectronicsBrandFilterGroupDto
        {
            Title = "Product Edition",
            Key = "edition",
            Options = new()
            {
                new() { Label = "Campus Verified Student Edition", Value = "campus", Count = 1 }
            }
        });
    }
}

