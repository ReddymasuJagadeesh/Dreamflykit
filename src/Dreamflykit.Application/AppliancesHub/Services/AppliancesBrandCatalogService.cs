using System.Globalization;
using Dreamflykit.Application.AppliancesHub.Models;

namespace Dreamflykit.Application.AppliancesHub.Services;

public static class AppliancesBrandCatalogService
{
    public static AppliancesBrandShowcaseData GetBrandShowcase(
        string subcategorySlug,
        string brandSlug,
        AppliancesHubDto? hubConfig = null)
    {
        var cleanSub = (subcategorySlug ?? string.Empty).Trim().ToLowerInvariant();
        var cleanBrand = (brandSlug ?? string.Empty).Trim().ToLowerInvariant();

        var defaultPages = AppliancesHubDto.CreateDefaultSubcategoryPages();
        var allPages = hubConfig?.SubcategoryPages != null && hubConfig.SubcategoryPages.Count > 0
            ? hubConfig.SubcategoryPages
            : defaultPages;

        var subPage = allPages.FirstOrDefault(p =>
            string.Equals(p.CategorySlug, cleanSub, StringComparison.OrdinalIgnoreCase) ||
            (cleanSub.Contains("tv") && p.CategorySlug.Contains("television")) ||
            (cleanSub.Contains("telev") && p.CategorySlug.Contains("television")) ||
            (cleanSub.Contains("laund") && p.CategorySlug.Contains("laundry")) ||
            (cleanSub.Contains("wash") && p.CategorySlug.Contains("laundry")) ||
            (cleanSub.Contains("ac") && p.CategorySlug.Contains("air-conditioners")) ||
            (cleanSub.Contains("fan") && p.CategorySlug.Contains("fans")) ||
            (cleanSub.Contains("micro") && p.CategorySlug.Contains("microwaves")) ||
            (cleanSub.Contains("365") && p.CategorySlug.Contains("value-365")) ||
            (cleanSub.Contains("small") && p.CategorySlug.Contains("small-appliances")) ||
            (cleanSub.Contains("geys") && p.CategorySlug.Contains("geysers")) ||
            (cleanSub.Contains("stab") && p.CategorySlug.Contains("stabilizers")) ||
            (cleanSub.Contains("kitch") && p.CategorySlug.Contains("kitchen-appliances")) ||
            (cleanSub.Contains("fridg") && p.CategorySlug.Contains("refrigerators")) ||
            (cleanSub.Contains("refrig") && p.CategorySlug.Contains("refrigerators")) ||
            (cleanSub.Contains("home") && p.CategorySlug.Contains("home-appliances")) ||
            (cleanSub.Contains("break") && p.CategorySlug.Contains("breakfast-appliances")) ||
            (cleanSub.Contains("top") && p.CategorySlug.Contains("top-rated")) ||
            ((cleanSub.Contains("invert") || cleanSub.Contains("backup")) && p.CategorySlug.Contains("inverters")) ||
            (cleanSub.Contains("dish") && p.CategorySlug.Contains("dishwashers")))
            ?? allPages.First();

        var brands = subPage.ShopByBrands?.Brands ?? new List<AppliancesBrandItemDto>();
        var brandItem = brands.FirstOrDefault(b =>
            string.Equals(b.Slug, cleanBrand, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(b.Name, cleanBrand, StringComparison.OrdinalIgnoreCase) ||
            b.Slug.Contains(cleanBrand) ||
            cleanBrand.Contains(b.Slug));

        if (brandItem == null)
        {
            var formattedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cleanBrand.Replace("-", " "));
            brandItem = new AppliancesBrandItemDto
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

        var result = new AppliancesBrandShowcaseData
        {
            SubcategorySlug = subPage.CategorySlug,
            SubcategoryName = subPage.CategoryName,
            SubcategoryEmoji = subPage.Emoji,
            BrandSlug = brandItem.Slug,
            BrandName = brandName,
            BrandItem = brandItem,
            HeroBanner = new AppliancesBrandHeroBannerDto
            {
                IsEnabled = true,
                Title = brandName + " " + subName,
                TitleHighlight = "Official Store & Student Privilege Deals",
                Subtitle = $"Explore genuine {brandName} {subName.ToLowerInvariant()} with student warranty, 110V-240V dual voltage testing, 5-Star energy efficiency, and free on-site campus delivery & installation.",
                TagBadge = "⚡ OFFICIAL APPLIANCES PARTNER • 100% GENUINE",
                ImageUrl = brandItem.HeroBanner != null && !string.IsNullOrWhiteSpace(brandItem.HeroBanner.ImageUrl)
                    ? brandItem.HeroBanner.ImageUrl
                    : "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = brandItem.HeroBanner?.IsUploadingSoon ?? true,
                BannerHeight = 360,
                TitleColor = "#ffffff",
                TitleHighlightColor = "#fbbf24",
                SubtitleColor = "#fef08a",
                Badges = new List<AppliancesHeroBadgeDto>
                {
                    new() { Id = "ab1", Label = "Official Brand In-Home Warranty", Icon = "shield" },
                    new() { Id = "ab2", Label = "110V–240V Dual Voltage Safe", Icon = "zap" },
                    new() { Id = "ab3", Label = "Free Student Dorm Delivery & Demo", Icon = "truck" }
                }
            }
        };

        BuildFacetedFiltersAndProducts(result, subPage.CategorySlug, brandItem.Slug, brandName, subName);

        return result;
    }

    private static void BuildFacetedFiltersAndProducts(
        AppliancesBrandShowcaseData data,
        string categorySlug,
        string brandSlug,
        string brandName,
        string categoryName)
    {
        var cleanCat = categorySlug.ToLowerInvariant();
        var cleanBrand = brandSlug.ToLowerInvariant();

        var isTv = cleanCat.Contains("television");
        var isLaundry = cleanCat.Contains("laundry");
        var isAc = cleanCat.Contains("air-conditioners");
        var isFridge = cleanCat.Contains("refrigerator");
        var isMicro = cleanCat.Contains("microwave");
        var isFan = cleanCat.Contains("fan");
        var isGeyser = cleanCat.Contains("geyser");
        var isKitchen = cleanCat.Contains("kitchen");
        var isDish = cleanCat.Contains("dishwasher");
        var isPower = cleanCat.Contains("stabilizer") || cleanCat.Contains("inverter");

        var prods = new List<AppliancesBrandProductDto>();
        var filters = new List<AppliancesBrandFilterGroupDto>();

        if (isTv)
        {
            BuildTelevisionShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isLaundry)
        {
            BuildLaundryShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isAc)
        {
            BuildAcShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isFridge)
        {
            BuildRefrigeratorShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isMicro)
        {
            BuildMicrowaveShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isFan)
        {
            BuildFanShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isGeyser)
        {
            BuildGeyserShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isKitchen)
        {
            BuildKitchenShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isDish)
        {
            BuildDishwasherShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else if (isPower)
        {
            BuildPowerBackupShowcase(cleanBrand, brandName, categoryName, prods, filters);
        }
        else
        {
            BuildGenericApplianceShowcase(data.SubcategorySlug, cleanBrand, brandName, categoryName, prods, filters);
        }

        data.Products = prods;
        data.FilterGroups = filters;
    }

    // =========================================================================
    // 1. TELEVISION
    // =========================================================================
    private static void BuildTelevisionShowcase(
        string brandSlug, string brandName, string catName,
        List<AppliancesBrandProductDto> prods, List<AppliancesBrandFilterGroupDto> filters)
    {
        filters.Add(new()
        {
            Key = "screensize",
            Title = "Screen Size",
            Options = new()
            {
                new() { Label = "32 Inch", Value = "32-inch", Count = 1 },
                new() { Label = "43 Inch", Value = "43-inch", Count = 2 },
                new() { Label = "50 Inch", Value = "50-inch", Count = 2 },
                new() { Label = "55 Inch", Value = "55-inch", Count = 2 },
                new() { Label = "65 Inch", Value = "65-inch", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "displaytech",
            Title = "Display Technology",
            Options = new()
            {
                new() { Label = "4K Ultra HD", Value = "4k-uhd", Count = 4 },
                new() { Label = "QLED", Value = "qled", Count = 2 },
                new() { Label = "OLED", Value = "oled", Count = 1 },
                new() { Label = "Full HD LED", Value = "full-hd", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "smarttvos",
            Title = "Smart TV Operating System",
            Options = new List<AppliancesBrandFilterOptionDto>
            {
                new() { Label = "Google TV", Value = "google-tv", Count = 3 },
                new() { Label = "Tizen OS (Samsung)", Value = "tizen", Count = brandSlug.Contains("samsung") ? 4 : 0 },
                new() { Label = "WebOS (LG)", Value = "webos", Count = brandSlug.Contains("lg") ? 4 : 0 },
                new() { Label = "Android TV", Value = "android", Count = 2 }
            }.Where(o => o.Count > 0 || !o.Label.Contains("(")).ToList()
        });

        filters.Add(new()
        {
            Key = "refreshrate",
            Title = "Refresh Rate",
            Options = new()
            {
                new() { Label = "60 Hz Standard", Value = "60hz", Count = 4 },
                new() { Label = "120 Hz Gaming", Value = "120hz", Count = 2 }
            }
        });

        filters.Add(new()
        {
            Key = "energystar",
            Title = "Energy Rating",
            Options = new()
            {
                new() { Label = "5 Star", Value = "5-star", Count = 3 },
                new() { Label = "4 Star", Value = "4-star", Count = 2 },
                new() { Label = "3 Star", Value = "3-star", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "price",
            Title = "Price Range",
            Options = new()
            {
                new() { Label = "Under ₹25,000", Value = "under-25k", Count = 1 },
                new() { Label = "₹25,000 – ₹50,000", Value = "25k-50k", Count = 3 },
                new() { Label = "Above ₹50,000", Value = "above-50k", Count = 2 }
            }
        });

        prods.Add(new()
        {
            Id = "p_tv_" + brandSlug + "_1",
            Name = $"{brandName} 43-Inch Crystal 4K UHD Smart AI TV",
            Slug = $"{brandSlug}-43-crystal-4k-uhd",
            Brand = brandName,
            Price = 28990,
            OriginalPrice = 44990,
            DiscountPercent = 35,
            Rating = 4.8,
            ReviewCount = 420,
            KeySpecs = new() { "43-Inch 4K UHD", "HDR 10+", "Dolby Audio 20W", "Google / Smart OS", "60 Hz" },
            EnergyStar = "5 Star",
            WarrantyBadge = "1-Year Comprehensive + 2-Year Panel",
            CampusPerk = "Free On-Site Campus Wall-Mounting & Setup",
            ScreenSize = "43-inch",
            DisplayTech = "4k-uhd",
            SmartTvOs = brandSlug.Contains("samsung") ? "tizen" : (brandSlug.Contains("lg") ? "webos" : "google-tv"),
            RefreshRate = "60hz"
        });

        prods.Add(new()
        {
            Id = "p_tv_" + brandSlug + "_2",
            Name = $"{brandName} 55-Inch Quantum QLED 4K Cinema Vision Smart TV",
            Slug = $"{brandSlug}-55-quantum-qled-4k",
            Brand = brandName,
            Price = 47990,
            OriginalPrice = 79990,
            DiscountPercent = 40,
            Rating = 4.9,
            ReviewCount = 310,
            KeySpecs = new() { "55-Inch QLED 4K", "120Hz Refresh Rate", "Dolby Atmos 40W", "AirPlay 2", "ALLM Game Mode" },
            EnergyStar = "5 Star",
            WarrantyBadge = "3-Year Official Brand Warranty",
            CampusPerk = "Student Festival Free Soundbar Upgrade Promo",
            ScreenSize = "55-inch",
            DisplayTech = "qled",
            SmartTvOs = brandSlug.Contains("samsung") ? "tizen" : (brandSlug.Contains("lg") ? "webos" : "google-tv"),
            RefreshRate = "120hz"
        });

        prods.Add(new()
        {
            Id = "p_tv_" + brandSlug + "_3",
            Name = $"{brandName} 32-Inch Bezel-less HD Smart TV (Student Dorm Pick)",
            Slug = $"{brandSlug}-32-bezel-less-smart-tv",
            Brand = brandName,
            Price = 13990,
            OriginalPrice = 21990,
            DiscountPercent = 36,
            Rating = 4.6,
            ReviewCount = 590,
            KeySpecs = new() { "32-Inch Vivid HD", "Chromecast Built-in", "DTS Virtual:X", "Quad Core AI", "60 Hz" },
            EnergyStar = "4 Star",
            WarrantyBadge = "1-Year On-Site Brand Warranty",
            CampusPerk = "Compact Size Fits Standard University Dorm Desks",
            ScreenSize = "32-inch",
            DisplayTech = "full-hd",
            SmartTvOs = brandSlug.Contains("samsung") ? "tizen" : (brandSlug.Contains("lg") ? "webos" : "google-tv"),
            RefreshRate = "60hz"
        });

        prods.Add(new()
        {
            Id = "p_tv_" + brandSlug + "_4",
            Name = $"{brandName} 50-Inch 4K HDR Ultra Slim Bezel Smart Cinema TV",
            Slug = $"{brandSlug}-50-4k-hdr-smart-tv",
            Brand = brandName,
            Price = 36990,
            OriginalPrice = 54990,
            DiscountPercent = 32,
            Rating = 4.7,
            ReviewCount = 280,
            KeySpecs = new() { "50-Inch 4K HDR", "MEMC Smooth Motion", "Google Assistant Voice Remote", "3x HDMI 2.1" },
            EnergyStar = "5 Star",
            WarrantyBadge = "2-Year Comprehensive Warranty",
            CampusPerk = "Zero-Cost 12 Months EMI for Student IDs",
            ScreenSize = "50-inch",
            DisplayTech = "4k-uhd",
            SmartTvOs = "google-tv",
            RefreshRate = "60hz"
        });

        prods.Add(new()
        {
            Id = "p_tv_" + brandSlug + "_5",
            Name = $"{brandName} 65-Inch OLED 4K Pro Master Cinema Reference Display",
            Slug = $"{brandSlug}-65-oled-4k-pro-master",
            Brand = brandName,
            Price = 98990,
            OriginalPrice = 159990,
            DiscountPercent = 38,
            Rating = 4.9,
            ReviewCount = 145,
            KeySpecs = new() { "65-Inch Self-Lit OLED", "Infinite Contrast", "120Hz VRR G-Sync", "60W Dolby Atmos 3D" },
            EnergyStar = "4 Star",
            WarrantyBadge = "3-Year Official Panel & Motherboard Warranty",
            CampusPerk = "White-Glove Delivery & Calibration Included",
            ScreenSize = "65-inch",
            DisplayTech = "oled",
            SmartTvOs = brandSlug.Contains("samsung") ? "tizen" : (brandSlug.Contains("lg") ? "webos" : "google-tv"),
            RefreshRate = "120hz"
        });
    }

    // =========================================================================
    // 2. LAUNDRY / WASHING MACHINES
    // =========================================================================
    private static void BuildLaundryShowcase(
        string brandSlug, string brandName, string catName,
        List<AppliancesBrandProductDto> prods, List<AppliancesBrandFilterGroupDto> filters)
    {
        filters.Add(new()
        {
            Key = "type",
            Title = "Washing Machine Type",
            Options = new()
            {
                new() { Label = "Front Load Fully-Automatic", Value = "front-load", Count = 3 },
                new() { Label = "Top Load Fully-Automatic", Value = "top-load", Count = 2 },
                new() { Label = "Semi-Automatic", Value = "semi-automatic", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "capacity",
            Title = "Capacity (Kg)",
            Options = new()
            {
                new() { Label = "6.0 – 7.0 Kg (1–2 Students)", Value = "7kg", Count = 2 },
                new() { Label = "8.0 Kg (Shared Dorm Apartment)", Value = "8kg", Count = 3 },
                new() { Label = "9.0+ Kg (Heavy Duty)", Value = "9kg", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "energystar",
            Title = "Energy Rating",
            Options = new()
            {
                new() { Label = "5 Star Inverter", Value = "5-star", Count = 5 },
                new() { Label = "4 Star", Value = "4-star", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "features",
            Title = "Key Technology",
            Options = new()
            {
                new() { Label = "Hygiene Steam Allergy Wash", Value = "steam", Count = 3 },
                new() { Label = "Inverter Direct Drive Motor", Value = "inverter", Count = 4 },
                new() { Label = "15-Min Quick Wash Cycle", Value = "quickwash", Count = 4 },
                new() { Label = "Wi-Fi Smart IoT App Control", Value = "wifi", Count = 2 }
            }
        });

        prods.Add(new()
        {
            Id = "p_ldy_" + brandSlug + "_1",
            Name = $"{brandName} 8 Kg 5 Star AI Inverter Front Load Washing Machine",
            Slug = $"{brandSlug}-8kg-ai-inverter-front-load",
            Brand = brandName,
            Price = 34990,
            OriginalPrice = 49990,
            DiscountPercent = 30,
            Rating = 4.8,
            ReviewCount = 540,
            KeySpecs = new() { "8 Kg Capacity", "5 Star Energy", "AI Direct Drive", "Hygiene Steam 99.9%", "1400 RPM Spin" },
            EnergyStar = "5 Star",
            WarrantyBadge = "2-Year Comprehensive + 10-Year Motor Warranty",
            CampusPerk = "Free Dorm Inlet-Outlet Plumbing Kit Included",
            ApplianceType = "front-load",
            Capacity = "8kg",
            Features = "steam,inverter,quickwash,wifi"
        });

        prods.Add(new()
        {
            Id = "p_ldy_" + brandSlug + "_2",
            Name = $"{brandName} 7 Kg 5 Star Smart Inverter Top Load Washer",
            Slug = $"{brandSlug}-7kg-smart-inverter-top-load",
            Brand = brandName,
            Price = 18990,
            OriginalPrice = 27990,
            DiscountPercent = 32,
            Rating = 4.7,
            ReviewCount = 390,
            KeySpecs = new() { "7 Kg Capacity", "5 Star Rated", "Smart Motion 3-Way", "TurboDrum Wash", "780 RPM Spin" },
            EnergyStar = "5 Star",
            WarrantyBadge = "10-Year Motor Warranty",
            CampusPerk = "Compact Footprint for Student Balconies",
            ApplianceType = "top-load",
            Capacity = "7kg",
            Features = "inverter,quickwash"
        });

        prods.Add(new()
        {
            Id = "p_ldy_" + brandSlug + "_3",
            Name = $"{brandName} 9 Kg 5 Star Steam-Wash EcoSilence Front Load",
            Slug = $"{brandSlug}-9kg-steam-wash-front-load",
            Brand = brandName,
            Price = 42990,
            OriginalPrice = 64990,
            DiscountPercent = 33,
            Rating = 4.9,
            ReviewCount = 210,
            KeySpecs = new() { "9 Kg Heavy Load", "Anti-Tangle Wash", "SpeedPerfect 65% Faster", "ActiveWater Plus" },
            EnergyStar = "5 Star",
            WarrantyBadge = "3-Year Complete + 12-Year Motor Warranty",
            CampusPerk = "Silent Operation Tested Under 50dB For Night Study",
            ApplianceType = "front-load",
            Capacity = "9kg",
            Features = "steam,inverter,quickwash,wifi"
        });
    }

    // =========================================================================
    // 3. AIR CONDITIONERS (ACs)
    // =========================================================================
    private static void BuildAcShowcase(
        string brandSlug, string brandName, string catName,
        List<AppliancesBrandProductDto> prods, List<AppliancesBrandFilterGroupDto> filters)
    {
        filters.Add(new()
        {
            Key = "tonnage",
            Title = "Capacity (Tonnage)",
            Options = new()
            {
                new() { Label = "1.0 Ton (Dorm / Small Room)", Value = "1-ton", Count = 2 },
                new() { Label = "1.5 Ton (Standard Room)", Value = "1.5-ton", Count = 3 },
                new() { Label = "2.0 Ton (Large Hall)", Value = "2-ton", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "energystar",
            Title = "Energy Rating",
            Options = new()
            {
                new() { Label = "5 Star Super Inverter", Value = "5-star", Count = 4 },
                new() { Label = "3 Star Inverter", Value = "3-star", Count = 2 }
            }
        });

        filters.Add(new()
        {
            Key = "features",
            Title = "Special Features",
            Options = new()
            {
                new() { Label = "Convertible 6-in-1 Modes", Value = "convertible", Count = 4 },
                new() { Label = "100% Copper Condenser", Value = "copper", Count = 5 },
                new() { Label = "PM 2.5 Air Purification Filter", Value = "air-filter", Count = 3 },
                new() { Label = "Wi-Fi Voice Control (Alexa/Google)", Value = "wifi", Count = 2 }
            }
        });

        prods.Add(new()
        {
            Id = "p_ac_" + brandSlug + "_1",
            Name = $"{brandName} 1.5 Ton 5 Star Dual Inverter Split AC",
            Slug = $"{brandSlug}-1.5-ton-5-star-dual-inverter",
            Brand = brandName,
            Price = 37990,
            OriginalPrice = 66990,
            DiscountPercent = 43,
            Rating = 4.8,
            ReviewCount = 760,
            KeySpecs = new() { "1.5 Ton", "5 Star BEE 2024", "Dual Inverter", "6-in-1 Convertible", "Copper Condenser" },
            EnergyStar = "5 Star",
            WarrantyBadge = "1-Year Product + 10-Year Compressor Warranty",
            CampusPerk = "Free Standard In-Dorm Installation Promo",
            Capacity = "1.5-ton",
            Features = "convertible,copper,air-filter,wifi"
        });

        prods.Add(new()
        {
            Id = "p_ac_" + brandSlug + "_2",
            Name = $"{brandName} 1.0 Ton 5 Star AI Inverter Split AC (Dorm Pick)",
            Slug = $"{brandSlug}-1-ton-5-star-ai-inverter",
            Brand = brandName,
            Price = 31990,
            OriginalPrice = 52990,
            DiscountPercent = 39,
            Rating = 4.7,
            ReviewCount = 410,
            KeySpecs = new() { "1.0 Ton", "Ultra Silent 21dB", "100% Grooved Copper", "Anti-Corrosion Blue Fin" },
            EnergyStar = "5 Star",
            WarrantyBadge = "10-Year Compressor Warranty",
            CampusPerk = "Lowest Electricity Consumption for Shared Hostels",
            Capacity = "1-ton",
            Features = "convertible,copper,air-filter"
        });
    }

    // =========================================================================
    // 4. REFRIGERATORS / FRIDGES
    // =========================================================================
    private static void BuildRefrigeratorShowcase(
        string brandSlug, string brandName, string catName,
        List<AppliancesBrandProductDto> prods, List<AppliancesBrandFilterGroupDto> filters)
    {
        filters.Add(new()
        {
            Key = "type",
            Title = "Refrigerator Type",
            Options = new()
            {
                new() { Label = "Double Door Frost Free", Value = "double-door", Count = 3 },
                new() { Label = "Single Door Direct Cool", Value = "single-door", Count = 2 },
                new() { Label = "Side-by-Side Multi-Door", Value = "side-by-side", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "capacity",
            Title = "Capacity",
            Options = new()
            {
                new() { Label = "Under 200 Litres (Single/Dorm)", Value = "under-200l", Count = 2 },
                new() { Label = "200 to 300 Litres (Shared)", Value = "200-300l", Count = 3 },
                new() { Label = "300+ Litres", Value = "above-300l", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "energystar",
            Title = "Energy Rating",
            Options = new()
            {
                new() { Label = "5 Star", Value = "5-star", Count = 2 },
                new() { Label = "3 Star Inverter", Value = "3-star", Count = 4 }
            }
        });

        filters.Add(new()
        {
            Key = "features",
            Title = "Special Features",
            Options = new()
            {
                new() { Label = "Convertible 5-in-1", Value = "convertible", Count = 3 },
                new() { Label = "Digital Inverter Compressor", Value = "inverter", Count = 5 },
                new() { Label = "Stabilizer Free Operation", Value = "stabilizer-free", Count = 5 }
            }
        });

        prods.Add(new()
        {
            Id = "p_ref_" + brandSlug + "_1",
            Name = $"{brandName} 256 Litres 3 Star Inverter Frost-Free Double Door Refrigerator",
            Slug = $"{brandSlug}-256l-frost-free-double-door",
            Brand = brandName,
            Price = 24990,
            OriginalPrice = 36990,
            DiscountPercent = 32,
            Rating = 4.8,
            ReviewCount = 890,
            KeySpecs = new() { "256 Litres", "Frost Free", "Convertible 5-in-1", "Toughened Glass Shelves", "All-Round Cooling" },
            EnergyStar = "3 Star",
            WarrantyBadge = "1-Year Comprehensive + 20-Year Compressor Warranty",
            CampusPerk = "Works on Home Inverter / Generator During Outages",
            ApplianceType = "double-door",
            Capacity = "200-300l",
            Features = "convertible,inverter,stabilizer-free"
        });

        prods.Add(new()
        {
            Id = "p_ref_" + brandSlug + "_2",
            Name = $"{brandName} 185 Litres 5 Star Direct-Cool Single Door Refrigerator",
            Slug = $"{brandSlug}-185l-5-star-single-door",
            Brand = brandName,
            Price = 16490,
            OriginalPrice = 22990,
            DiscountPercent = 28,
            Rating = 4.7,
            ReviewCount = 610,
            KeySpecs = new() { "185 Litres", "5 Star Energy", "Base Stand with Drawer", "Fast Ice Making In 1 Hour" },
            EnergyStar = "5 Star",
            WarrantyBadge = "10-Year Digital Inverter Compressor Warranty",
            CampusPerk = "Ultra Low Energy Bill (Under 130 Units / Year)",
            ApplianceType = "single-door",
            Capacity = "under-200l",
            Features = "inverter,stabilizer-free"
        });
    }

    // =========================================================================
    // 5. MICROWAVES
    // =========================================================================
    private static void BuildMicrowaveShowcase(
        string brandSlug, string brandName, string catName,
        List<AppliancesBrandProductDto> prods, List<AppliancesBrandFilterGroupDto> filters)
    {
        filters.Add(new()
        {
            Key = "type",
            Title = "Microwave Type",
            Options = new()
            {
                new() { Label = "Convection (Baking & Roasting)", Value = "convection", Count = 2 },
                new() { Label = "Solo (Reheating & Cooking)", Value = "solo", Count = 2 },
                new() { Label = "Grill", Value = "grill", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "capacity",
            Title = "Capacity",
            Options = new()
            {
                new() { Label = "20 Litres (Compact Student Pick)", Value = "20l", Count = 2 },
                new() { Label = "28 Litres (All-in-One)", Value = "28l", Count = 2 }
            }
        });

        filters.Add(new()
        {
            Key = "features",
            Title = "Features",
            Options = new()
            {
                new() { Label = "Oil-Free Diet Fry", Value = "diet-fry", Count = 2 },
                new() { Label = "Ceramic Enamel Cavity", Value = "ceramic", Count = 2 },
                new() { Label = "Auto-Cook Menus (Indian/Continental)", Value = "autocook", Count = 4 }
            }
        });

        prods.Add(new()
        {
            Id = "p_micro_" + brandSlug + "_1",
            Name = $"{brandName} 28 Litres Convection Microwave Oven with Diet Fry",
            Slug = $"{brandSlug}-28l-convection-microwave",
            Brand = brandName,
            Price = 14490,
            OriginalPrice = 21990,
            DiscountPercent = 34,
            Rating = 4.8,
            ReviewCount = 430,
            KeySpecs = new() { "28L Convection", "Tandoor & Diet Fry", "Ceramic Cavity 10-Yr", "Fermentation & Dough" },
            WarrantyBadge = "1-Year Product + 5-Year Magnetron Warranty",
            CampusPerk = "Cook Pizza, Reheat Campus Meals & Make Tea Instantly",
            ApplianceType = "convection",
            Capacity = "28l",
            Features = "diet-fry,ceramic,autocook"
        });

        prods.Add(new()
        {
            Id = "p_micro_" + brandSlug + "_2",
            Name = $"{brandName} 20 Litres Solo Microwave Oven for Fast Reheating",
            Slug = $"{brandSlug}-20l-solo-microwave",
            Brand = brandName,
            Price = 6490,
            OriginalPrice = 8990,
            DiscountPercent = 27,
            Rating = 4.6,
            ReviewCount = 520,
            KeySpecs = new() { "20L Solo", "Compact Countertop", "Quick Defrost", "Easy Clean Coating", "5 Power Levels" },
            WarrantyBadge = "1-Year Comprehensive Warranty",
            CampusPerk = "Pocket-Friendly Student Reheating Essential",
            ApplianceType = "solo",
            Capacity = "20l",
            Features = "autocook"
        });
    }

    // =========================================================================
    // 6. FANS
    // =========================================================================
    private static void BuildFanShowcase(
        string brandSlug, string brandName, string catName,
        List<AppliancesBrandProductDto> prods, List<AppliancesBrandFilterGroupDto> filters)
    {
        filters.Add(new()
        {
            Key = "type",
            Title = "Fan Type",
            Options = new()
            {
                new() { Label = "BLDC Energy Saver Ceiling Fan", Value = "bldc-ceiling", Count = 2 },
                new() { Label = "Pedestal / Stand Fan", Value = "pedestal", Count = 1 },
                new() { Label = "Tower Fan (Space-Saver)", Value = "tower", Count = 1 },
                new() { Label = "Table / Desk Fan", Value = "desk", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "features",
            Title = "Key Technology",
            Options = new()
            {
                new() { Label = "Remote Control Operation", Value = "remote", Count = 3 },
                new() { Label = "Super Energy Saving 28W Motor", Value = "28w-motor", Count = 2 },
                new() { Label = "Ultra Silent Airflow", Value = "silent", Count = 3 }
            }
        });

        prods.Add(new()
        {
            Id = "p_fan_" + brandSlug + "_1",
            Name = $"{brandName} 1200mm BLDC Smart Energy Saving Ceiling Fan with Remote",
            Slug = $"{brandSlug}-1200mm-bldc-ceiling-fan",
            Brand = brandName,
            Price = 3499,
            OriginalPrice = 5499,
            DiscountPercent = 36,
            Rating = 4.8,
            ReviewCount = 780,
            KeySpecs = new() { "1200mm Sweep", "28W BLDC Motor", "Saves Up to 65% Power", "Smart Remote with Sleep Timer" },
            WarrantyBadge = "3-Year On-Site Manufacturer Warranty",
            CampusPerk = "Whisper-Quiet Operation for Concentration & Sleep",
            ApplianceType = "bldc-ceiling",
            Capacity = "1200mm",
            Features = "remote,28w-motor,silent"
        });

        prods.Add(new()
        {
            Id = "p_fan_" + brandSlug + "_2",
            Name = $"{brandName} High-Airflow Compact Oscillating Table/Desk Fan",
            Slug = $"{brandSlug}-compact-oscillating-table-fan",
            Brand = brandName,
            Price = 1799,
            OriginalPrice = 2799,
            DiscountPercent = 35,
            Rating = 4.6,
            ReviewCount = 340,
            KeySpecs = new() { "400mm Sweep", "100% Copper Motor", "3-Speed Dial", "Jerk-Free Oscillation" },
            WarrantyBadge = "2-Year Motor Warranty",
            CampusPerk = "Lightweight Portable Design Fits Any Study Table",
            ApplianceType = "desk",
            Capacity = "400mm",
            Features = "silent"
        });
    }

    // =========================================================================
    // 7. GEYSERS & WATER HEATERS
    // =========================================================================
    private static void BuildGeyserShowcase(
        string brandSlug, string brandName, string catName,
        List<AppliancesBrandProductDto> prods, List<AppliancesBrandFilterGroupDto> filters)
    {
        filters.Add(new()
        {
            Key = "type",
            Title = "Geyser Type",
            Options = new()
            {
                new() { Label = "Storage Geyser", Value = "storage", Count = 2 },
                new() { Label = "Instant Water Geyser", Value = "instant", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "capacity",
            Title = "Capacity",
            Options = new()
            {
                new() { Label = "3 Litres Instant", Value = "3l", Count = 1 },
                new() { Label = "15 Litres Storage", Value = "15l", Count = 2 },
                new() { Label = "25 Litres Storage", Value = "25l", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "energystar",
            Title = "BEE Star Rating",
            Options = new()
            {
                new() { Label = "5 Star Energy Rated", Value = "5-star", Count = 3 }
            }
        });

        prods.Add(new()
        {
            Id = "p_gey_" + brandSlug + "_1",
            Name = $"{brandName} 15 Litres 5 Star Glass-Lined Storage Geyser",
            Slug = $"{brandSlug}-15l-5-star-storage-geyser",
            Brand = brandName,
            Price = 6990,
            OriginalPrice = 11990,
            DiscountPercent = 41,
            Rating = 4.8,
            ReviewCount = 590,
            KeySpecs = new() { "15L Storage", "5 Star Rated", "Blue Diamond Glass Lined", "8 Bar Multi-Storey Safe" },
            EnergyStar = "5 Star",
            WarrantyBadge = "7-Year Inner Tank + 2-Year Comprehensive Warranty",
            CampusPerk = "Safe for High-Rise Student Apartments & Hard Water",
            ApplianceType = "storage",
            Capacity = "15l",
            Features = "glass-lined,8-bar"
        });

        prods.Add(new()
        {
            Id = "p_gey_" + brandSlug + "_2",
            Name = $"{brandName} 3 Litres 3kW Instant High-Speed Geyser",
            Slug = $"{brandSlug}-3l-instant-geyser",
            Brand = brandName,
            Price = 2890,
            OriginalPrice = 4490,
            DiscountPercent = 35,
            Rating = 4.7,
            ReviewCount = 420,
            KeySpecs = new() { "3L Instant", "3000W Copper Element", "Rust-Proof Polymer Body", "Instant Hot Water in 10s" },
            EnergyStar = "5 Star",
            WarrantyBadge = "5-Year Tank Warranty",
            CampusPerk = "Compact Wall-Mounted Size for Small Dorm Washrooms",
            ApplianceType = "instant",
            Capacity = "3l",
            Features = "instant,shock-proof"
        });
    }

    // =========================================================================
    // 8. KITCHEN APPLIANCES
    // =========================================================================
    private static void BuildKitchenShowcase(
        string brandSlug, string brandName, string catName,
        List<AppliancesBrandProductDto> prods, List<AppliancesBrandFilterGroupDto> filters)
    {
        filters.Add(new()
        {
            Key = "type",
            Title = "Appliance Type",
            Options = new()
            {
                new() { Label = "Air Fryer (Healthy Dorm Cooking)", Value = "air-fryer", Count = 1 },
                new() { Label = "Induction Cooktop 2000W", Value = "induction", Count = 1 },
                new() { Label = "Electric Kettle 1.5L", Value = "kettle", Count = 1 },
                new() { Label = "Mixer Grinder 750W", Value = "mixer-grinder", Count = 1 },
                new() { Label = "Sandwich & Panini Maker", Value = "sandwich-maker", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "features",
            Title = "Safety & Build",
            Options = new()
            {
                new() { Label = "Auto Shut-Off Overheat Protection", Value = "auto-shutoff", Count = 3 },
                new() { Label = "Food Grade 304 Stainless Steel", Value = "ss304", Count = 3 },
                new() { Label = "Non-Stick Easy Clean Coating", Value = "non-stick", Count = 2 }
            }
        });

        prods.Add(new()
        {
            Id = "p_kitch_" + brandSlug + "_1",
            Name = $"{brandName} 4.2 Litres Digital Rapid Air Fryer (90% Less Oil)",
            Slug = $"{brandSlug}-4.2l-digital-air-fryer",
            Brand = brandName,
            Price = 4999,
            OriginalPrice = 8999,
            DiscountPercent = 44,
            Rating = 4.9,
            ReviewCount = 890,
            KeySpecs = new() { "4.2L Capacity", "Rapid Air 360°", "Touch Screen 8 Presets", "Non-Stick Dishwasher Safe Basket" },
            WarrantyBadge = "2-Year International Brand Warranty",
            CampusPerk = "Cook Crispy Fries, Paneer Tikka & Chicken Fast in Dorms",
            ApplianceType = "air-fryer",
            Features = "auto-shutoff,non-stick"
        });

        prods.Add(new()
        {
            Id = "p_kitch_" + brandSlug + "_2",
            Name = $"{brandName} 2000W Smart Induction Cooktop with Indian Menu Presets",
            Slug = $"{brandSlug}-2000w-smart-induction-cooktop",
            Brand = brandName,
            Price = 2499,
            OriginalPrice = 3999,
            DiscountPercent = 37,
            Rating = 4.7,
            ReviewCount = 730,
            KeySpecs = new() { "2000W Fast Heating", "Feather Touch Panel", "Voltage Surge Protector", "Flame-Free & Dorm Safe" },
            WarrantyBadge = "1-Year Replacement Warranty",
            CampusPerk = "Approved for Student Dorms with Flame Restrictions",
            ApplianceType = "induction",
            Features = "auto-shutoff"
        });

        prods.Add(new()
        {
            Id = "p_kitch_" + brandSlug + "_3",
            Name = $"{brandName} 1.8 Litres Double-Wall Cool-Touch Electric Kettle",
            Slug = $"{brandSlug}-1.8l-electric-kettle",
            Brand = brandName,
            Price = 999,
            OriginalPrice = 1799,
            DiscountPercent = 44,
            Rating = 4.8,
            ReviewCount = 1200,
            KeySpecs = new() { "1.8L Fast Boil", "304 Stainless Steel Inner", "Cool Touch Exterior", "Boil-Dry Auto Protection" },
            WarrantyBadge = "1-Year Warranty",
            CampusPerk = "Late-Night Maggi, Coffee & Green Tea Campus Essential",
            ApplianceType = "kettle",
            Features = "auto-shutoff,ss304"
        });
    }

    // =========================================================================
    // 9. DISHWASHERS
    // =========================================================================
    private static void BuildDishwasherShowcase(
        string brandSlug, string brandName, string catName,
        List<AppliancesBrandProductDto> prods, List<AppliancesBrandFilterGroupDto> filters)
    {
        filters.Add(new()
        {
            Key = "placesettings",
            Title = "Place Settings",
            Options = new()
            {
                new() { Label = "13–14 Place Settings (Standard)", Value = "14-place", Count = 2 },
                new() { Label = "12 Place Settings", Value = "12-place", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "features",
            Title = "Specialized Programs",
            Options = new()
            {
                new() { Label = "Intensive Kadhai 70°C Wash", Value = "kadhai-wash", Count = 2 },
                new() { Label = "HygienePlus 99.9% Bacteria Free", Value = "hygiene-plus", Count = 2 },
                new() { Label = "EcoSilence Drive Motor", Value = "inverter", Count = 2 }
            }
        });

        prods.Add(new()
        {
            Id = "p_dish_" + brandSlug + "_1",
            Name = $"{brandName} 14 Place Settings Inverter Fully-Automatic Dishwasher",
            Slug = $"{brandSlug}-14-place-dishwasher",
            Brand = brandName,
            Price = 38990,
            OriginalPrice = 54990,
            DiscountPercent = 29,
            Rating = 4.8,
            ReviewCount = 310,
            KeySpecs = new() { "14 Place Settings", "Intensive Kadhai 70°", "Half Load Option", "Low Water 9.5L Cycle" },
            WarrantyBadge = "2-Year Comprehensive + 10-Year Rust-Through Warranty",
            CampusPerk = "No More Shared Washing Sink Disputes in Student Apartments",
            ApplianceType = "14-place",
            Features = "kadhai-wash,hygiene-plus,inverter"
        });
    }

    // =========================================================================
    // 10. STABILIZERS & POWER BACKUP
    // =========================================================================
    private static void BuildPowerBackupShowcase(
        string brandSlug, string brandName, string catName,
        List<AppliancesBrandProductDto> prods, List<AppliancesBrandFilterGroupDto> filters)
    {
        filters.Add(new()
        {
            Key = "compatibility",
            Title = "Appliance Compatibility",
            Options = new()
            {
                new() { Label = "For Inverter AC (Up to 1.5 Ton)", Value = "for-ac", Count = 2 },
                new() { Label = "For Refrigerator & Smart TV", Value = "for-fridge-tv", Count = 1 },
                new() { Label = "Home & Dorm Inverter UPS", Value = "inverter-ups", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "features",
            Title = "Protection Features",
            Options = new()
            {
                new() { Label = "Digital Seven-Segment Display", Value = "digital-display", Count = 2 },
                new() { Label = "High-Low Voltage Cutoff", Value = "voltage-cutoff", Count = 3 },
                new() { Label = "Initial Time Delay System (ITDS)", Value = "time-delay", Count = 2 }
            }
        });

        prods.Add(new()
        {
            Id = "p_stab_" + brandSlug + "_1",
            Name = $"{brandName} Smart Voltage Stabilizer for Inverter AC (Up to 1.5 Ton)",
            Slug = $"{brandSlug}-smart-ac-voltage-stabilizer",
            Brand = brandName,
            Price = 2490,
            OriginalPrice = 3690,
            DiscountPercent = 32,
            Rating = 4.8,
            ReviewCount = 940,
            KeySpecs = new() { "90V to 290V Wide Range", "Digital Seven Segment", "Wall Mountable", "Thermal Overload Protection" },
            WarrantyBadge = "3-Year Replacement Guarantee",
            CampusPerk = "Protects Expensive Student Appliances from Campus Grid Spikes",
            ApplianceType = "for-ac",
            Features = "digital-display,voltage-cutoff,time-delay"
        });
    }

    // =========================================================================
    // 11. GENERIC APPLIANCE FALLBACK
    // =========================================================================
    private static void BuildGenericApplianceShowcase(
        string subcategorySlug, string brandSlug, string brandName, string catName,
        List<AppliancesBrandProductDto> prods, List<AppliancesBrandFilterGroupDto> filters)
    {
        filters.Add(new()
        {
            Key = "type",
            Title = $"{catName} Type",
            Options = new()
            {
                new() { Label = $"Essential {brandName} Model", Value = "essential", Count = 2 },
                new() { Label = $"Pro Performance {brandName}", Value = "pro", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "energystar",
            Title = "Energy Rating",
            Options = new()
            {
                new() { Label = "5 Star Energy Efficient", Value = "5-star", Count = 2 },
                new() { Label = "4 Star", Value = "4-star", Count = 1 }
            }
        });

        filters.Add(new()
        {
            Key = "price",
            Title = "Price Range",
            Options = new()
            {
                new() { Label = "Under ₹5,000", Value = "under-5k", Count = 1 },
                new() { Label = "₹5,000 – ₹15,000", Value = "5k-15k", Count = 2 }
            }
        });

        prods.Add(new()
        {
            Id = $"p_{brandSlug}_gen_1",
            Name = $"{brandName} Verified Smart {catName} (Student Edition)",
            Slug = $"{brandSlug}-verified-student-{subcategorySlug}",
            Brand = brandName,
            Price = 4999,
            OriginalPrice = 7999,
            DiscountPercent = 37,
            Rating = 4.8,
            ReviewCount = 210,
            KeySpecs = new() { "110V-240V Compatible", "Energy Certified", "Dorm Size Compact", "Fast Operation" },
            EnergyStar = "5 Star",
            WarrantyBadge = "2-Year Official Brand Warranty",
            CampusPerk = "Student Privilege Free Express Delivery",
            ApplianceType = "essential",
            Features = "smart,compact"
        });

        prods.Add(new()
        {
            Id = $"p_{brandSlug}_gen_2",
            Name = $"{brandName} Turbo Pro Multi-Function {catName}",
            Slug = $"{brandSlug}-turbo-pro-{subcategorySlug}",
            Brand = brandName,
            Price = 8499,
            OriginalPrice = 12999,
            DiscountPercent = 34,
            Rating = 4.9,
            ReviewCount = 160,
            KeySpecs = new() { "High Efficiency Motor", "Digital Controls", "Quiet Operation", "BPA Free Material" },
            EnergyStar = "5 Star",
            WarrantyBadge = "3-Year Comprehensive Warranty",
            CampusPerk = "Campus Extended Warranty Included",
            ApplianceType = "pro",
            Features = "smart,turbo"
        });
    }
}
