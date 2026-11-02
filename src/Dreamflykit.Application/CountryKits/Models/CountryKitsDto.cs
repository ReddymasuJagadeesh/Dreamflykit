namespace Dreamflykit.Application.CountryKits.Models;

public sealed class CountryKitItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string CountrySlug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Size { get; set; } = "medium"; // "small", "medium", "large"
    public string Badge { get; set; } = string.Empty; // "Best Seller", "Most Popular", "Best Value", "Premium"
    public string Description { get; set; } = string.Empty;
    public decimal SellingPrice { get; set; }
    public decimal Mrp { get; set; }
    public int DiscountPercentage { get; set; }
    public double Rating { get; set; } = 4.8;
    public int ReviewCount { get; set; } = 850;
    public List<string> IncludedCategories { get; set; } = new()
    {
        "Study Essentials",
        "Electronics",
        "Travel Essentials",
        "Room & Living",
        "Kitchen & Dining"
    };
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public bool IsFeatured { get; set; } = true;
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
}

public sealed class CountryKitProfileDto
{
    public string CountrySlug { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string FlagEmoji { get; set; } = "🇺🇸";
    public string Tagline { get; set; } = "Study in the Land of Opportunities";
    public string HeroTitle { get; set; } = string.Empty;
    public string HeroSubtitle { get; set; } = string.Empty;
    public string LandmarkImageUrl { get; set; } = "/images/countries/usa.webp";
    public bool IsLandmarkUploadingSoon { get; set; } = false;
    public decimal StartingPrice { get; set; } = 4999;
    public string Feature1 { get; set; } = "Curated for Local Lifestyle";
    public string Feature2 { get; set; } = "Essential Items for Every Student";
    public string Feature3 { get; set; } = "Premium Quality Products";
    public string Feature4 { get; set; } = "Value for Your Money";
    public List<CountryKitItemDto> Kits { get; set; } = new();
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; } = 1;
}

public sealed class CountryKitsHubDto
{
    public Dictionary<string, CountryKitProfileDto> Countries { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    public CountryKitProfileDto GetCountryProfile(string? countrySlug)
    {
        if (string.IsNullOrWhiteSpace(countrySlug)) countrySlug = "usa";
        countrySlug = NormalizeSlug(countrySlug);

        if (Countries.TryGetValue(countrySlug, out var profile) && profile != null)
        {
            return profile;
        }

        // Return a dynamic fallback profile if not explicitly registered
        var formattedName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(countrySlug.Replace("-", " "));
        return new CountryKitProfileDto
        {
            CountrySlug = countrySlug,
            CountryName = formattedName,
            FlagEmoji = "🌍",
            Tagline = $"Your Future in {formattedName}, Our Essentials",
            HeroTitle = $"{formattedName} Student Kits",
            HeroSubtitle = $"Everything you need for a comfortable and successful study abroad journey in {formattedName}.",
            LandmarkImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
            IsLandmarkUploadingSoon = true,
            StartingPrice = 4999,
            Feature1 = $"Curated for {formattedName} Lifestyle",
            Feature2 = "Essential Items for Every Student",
            Feature3 = "Premium Quality Products",
            Feature4 = "Value for Your Money",
            Kits = CreateDefaultKitsFor(countrySlug, formattedName)
        };
    }

    public static string NormalizeSlug(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return "usa";
        var cleaned = input.Trim().ToLowerInvariant();
        return cleaned switch
        {
            "united states" or "united-states" or "us" => "usa",
            "united kingdom" or "united-kingdom" or "great britain" => "uk",
            _ => cleaned
        };
    }

    public static CountryKitsHubDto CreateDefault()
    {
        var hub = new CountryKitsHubDto();

        // 1. USA (Matches Screenshot 1 Exactly)
        hub.Countries["usa"] = new CountryKitProfileDto
        {
            CountrySlug = "usa",
            CountryName = "USA",
            FlagEmoji = "🇺🇸",
            Tagline = "Study in the Land of Opportunities",
            HeroTitle = "USA Student Kits",
            HeroSubtitle = "Everything you need for a comfortable and successful study abroad journey in the USA.",
            LandmarkImageUrl = "/images/countries/usa.webp",
            IsLandmarkUploadingSoon = false,
            StartingPrice = 4999,
            Feature1 = "Curated for American Lifestyle",
            Feature2 = "Essential Items for Every Student",
            Feature3 = "Premium Quality Products",
            Feature4 = "Value for Your Money",
            DisplayOrder = 1,
            Kits = new List<CountryKitItemDto>
            {
                new()
                {
                    Id = "usa_k1",
                    CountrySlug = "usa",
                    Name = "USA Small Kit",
                    Slug = "usa-small-kit",
                    Size = "small",
                    Badge = "Best Seller",
                    Description = "Essential items for a hassle-free start in the USA.",
                    SellingPrice = 4999,
                    Mrp = 6499,
                    DiscountPercentage = 23,
                    Rating = 4.7,
                    ReviewCount = 1210,
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    DisplayOrder = 1
                },
                new()
                {
                    Id = "usa_k2",
                    CountrySlug = "usa",
                    Name = "USA Small Kit - Premium",
                    Slug = "usa-small-kit-premium",
                    Size = "small",
                    Badge = "",
                    Description = "Upgraded essentials for extra comfort and convenience.",
                    SellingPrice = 6999,
                    Mrp = 8999,
                    DiscountPercentage = 22,
                    Rating = 4.8,
                    ReviewCount = 860,
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    DisplayOrder = 2
                },
                new()
                {
                    Id = "usa_k3",
                    CountrySlug = "usa",
                    Name = "USA Medium Kit",
                    Slug = "usa-medium-kit",
                    Size = "medium",
                    Badge = "Most Popular",
                    Description = "All-rounder kit for the perfect balance of needs.",
                    SellingPrice = 9999,
                    Mrp = 13499,
                    DiscountPercentage = 26,
                    Rating = 4.8,
                    ReviewCount = 1560,
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    DisplayOrder = 3
                },
                new()
                {
                    Id = "usa_k4",
                    CountrySlug = "usa",
                    Name = "USA Medium Kit - Premium",
                    Slug = "usa-medium-kit-premium",
                    Size = "medium",
                    Badge = "",
                    Description = "Enhanced quality items for a better experience.",
                    SellingPrice = 12999,
                    Mrp = 16999,
                    DiscountPercentage = 24,
                    Rating = 4.9,
                    ReviewCount = 940,
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    DisplayOrder = 4
                },
                new()
                {
                    Id = "usa_k5",
                    CountrySlug = "usa",
                    Name = "USA Large Kit",
                    Slug = "usa-large-kit",
                    Size = "large",
                    Badge = "Best Value",
                    Description = "Complete kit with everything you might need.",
                    SellingPrice = 16999,
                    Mrp = 22999,
                    DiscountPercentage = 26,
                    Rating = 4.9,
                    ReviewCount = 2134,
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    DisplayOrder = 5
                },
                new()
                {
                    Id = "usa_k6",
                    CountrySlug = "usa",
                    Name = "USA Large Kit - Premium",
                    Slug = "usa-large-kit-premium",
                    Size = "large",
                    Badge = "",
                    Description = "Ultimate comfort with premium quality products.",
                    SellingPrice = 21999,
                    Mrp = 28999,
                    DiscountPercentage = 24,
                    Rating = 4.9,
                    ReviewCount = 1230,
                    ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                    IsUploadingSoon = true,
                    DisplayOrder = 6
                }
            }
        };

        // 2. UK
        hub.Countries["uk"] = new CountryKitProfileDto
        {
            CountrySlug = "uk",
            CountryName = "UK",
            FlagEmoji = "🇬🇧",
            Tagline = "Experience World-Class Education",
            HeroTitle = "UK Student Kits",
            HeroSubtitle = "Specially adapted for British weather, voltage, university dorms, and lifestyle.",
            LandmarkImageUrl = "/images/countries/uk.webp",
            IsLandmarkUploadingSoon = false,
            StartingPrice = 4499,
            Feature1 = "Adapted for UK Weather & Dorms",
            Feature2 = "Type G UK Voltage Ready",
            Feature3 = "British University Approved",
            Feature4 = "Comprehensive Starter Kit",
            DisplayOrder = 2,
            Kits = CreateDefaultKitsFor("uk", "UK", 4499)
        };

        // 3. CANADA
        hub.Countries["canada"] = new CountryKitProfileDto
        {
            CountrySlug = "canada",
            CountryName = "Canada",
            FlagEmoji = "🇨🇦",
            Tagline = "Your Future, Our Essentials",
            HeroTitle = "Canada Student Kits",
            HeroSubtitle = "Built to handle Canadian winter climates, sub-zero protection, and dorm room living.",
            LandmarkImageUrl = "/images/countries/canada.webp",
            IsLandmarkUploadingSoon = false,
            StartingPrice = 4499,
            Feature1 = "Winter & Sub-Zero Protected",
            Feature2 = "Essential Canadian Campus Gear",
            Feature3 = "100% Verified Quality",
            Feature4 = "Dorm & Living Ready",
            DisplayOrder = 3,
            Kits = CreateDefaultKitsFor("canada", "Canada", 4499)
        };

        // 4. AUSTRALIA
        hub.Countries["australia"] = new CountryKitProfileDto
        {
            CountrySlug = "australia",
            CountryName = "Australia",
            FlagEmoji = "🇦🇺",
            Tagline = "Discover. Learn. Grow Abroad.",
            HeroTitle = "Australia Student Kits",
            HeroSubtitle = "Curated for Australian campuses, coastal climates, and student accommodation needs.",
            LandmarkImageUrl = "/images/countries/australia.webp",
            IsLandmarkUploadingSoon = false,
            StartingPrice = 4999,
            Feature1 = "Type I Australian Power Ready",
            Feature2 = "Sun & Coastal Weather Adapted",
            Feature3 = "All Australian Universities",
            Feature4 = "Lightweight & Baggage Optimized",
            DisplayOrder = 4,
            Kits = CreateDefaultKitsFor("australia", "Australia", 4999)
        };

        // 5. GERMANY
        hub.Countries["germany"] = new CountryKitProfileDto
        {
            CountrySlug = "germany",
            CountryName = "Germany",
            FlagEmoji = "🇩🇪",
            Tagline = "Excellence in Education Awaits",
            HeroTitle = "Germany Student Kits",
            HeroSubtitle = "Curated for WG living (Wohngemeinschaft), German standards, and European academic life.",
            LandmarkImageUrl = "/images/countries/germany.webp",
            IsLandmarkUploadingSoon = false,
            StartingPrice = 4999,
            Feature1 = "WG & Dorm Accommodation Ready",
            Feature2 = "European Type C/F Voltage Tested",
            Feature3 = "Scholarly Precision & Durability",
            Feature4 = "Seamless Arrival Experience",
            DisplayOrder = 5,
            Kits = CreateDefaultKitsFor("germany", "Germany", 4999)
        };

        // 6. IRELAND
        hub.Countries["ireland"] = new CountryKitProfileDto
        {
            CountrySlug = "ireland",
            CountryName = "Ireland",
            FlagEmoji = "🇮🇪",
            Tagline = "Your Gateway to Global Success",
            HeroTitle = "Ireland Student Kits",
            HeroSubtitle = "Designed for Irish collegiate life, rainy-day weather gear, and student residencies.",
            LandmarkImageUrl = "/images/countries/ireland.webp",
            IsLandmarkUploadingSoon = false,
            StartingPrice = 4499,
            Feature1 = "Irish Weather-Proof Protection",
            Feature2 = "Type G Dual-Voltage Compliant",
            Feature3 = "Trinity & UCD Dorm Compatible",
            Feature4 = "High-Quality Value Guaranteed",
            DisplayOrder = 6,
            Kits = CreateDefaultKitsFor("ireland", "Ireland", 4499)
        };

        // 7. NEW ZEALAND
        hub.Countries["new-zealand"] = new CountryKitProfileDto
        {
            CountrySlug = "new-zealand",
            CountryName = "New Zealand",
            FlagEmoji = "🇳🇿",
            Tagline = "Kiwi Adventure Meets World-Class Study",
            HeroTitle = "New Zealand Student Kits",
            HeroSubtitle = "Outdoor ready, cozy dorm essentials, and academic supplies for studying in Aotearoa.",
            LandmarkImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
            IsLandmarkUploadingSoon = true,
            StartingPrice = 4999,
            Feature1 = "All-Season Kiwi Climate Ready",
            Feature2 = "Type I NZ Power Compatible",
            Feature3 = "Student Village & Flat Ready",
            Feature4 = "Eco-Friendly Sustainable Essentials",
            DisplayOrder = 7,
            Kits = CreateDefaultKitsFor("new-zealand", "New Zealand", 4999)
        };

        // 8. FRANCE
        hub.Countries["france"] = new CountryKitProfileDto
        {
            CountrySlug = "france",
            CountryName = "France",
            FlagEmoji = "🇫🇷",
            Tagline = "Culture, Innovation and Academic Prestige",
            HeroTitle = "France Student Kits",
            HeroSubtitle = "Tailored for studio living (CROUS), French university culture, and European convenience.",
            LandmarkImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
            IsLandmarkUploadingSoon = true,
            StartingPrice = 4799,
            Feature1 = "CROUS & Studio Apartment Ready",
            Feature2 = "European Standard Certified",
            Feature3 = "Compact & High Utility",
            Feature4 = "Baggage Weight Optimized",
            DisplayOrder = 8,
            Kits = CreateDefaultKitsFor("france", "France", 4799)
        };

        return hub;
    }

    public static List<CountryKitItemDto> CreateDefaultKitsFor(string slug, string name, decimal basePrice = 4999)
    {
        return new List<CountryKitItemDto>
        {
            new()
            {
                Id = $"{slug}_k1",
                CountrySlug = slug,
                Name = $"{name} Small Kit",
                Slug = $"{slug}-small-kit",
                Size = "small",
                Badge = "Best Seller",
                Description = $"Essential items for a hassle-free start in {name}.",
                SellingPrice = basePrice,
                Mrp = Math.Round(basePrice * 1.3m, 0),
                DiscountPercentage = 23,
                Rating = 4.7,
                ReviewCount = 890,
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                DisplayOrder = 1
            },
            new()
            {
                Id = $"{slug}_k2",
                CountrySlug = slug,
                Name = $"{name} Small Kit - Premium",
                Slug = $"{slug}-small-kit-premium",
                Size = "small",
                Badge = "",
                Description = $"Upgraded essentials for extra comfort and convenience in {name}.",
                SellingPrice = basePrice + 2000,
                Mrp = Math.Round((basePrice + 2000) * 1.28m, 0),
                DiscountPercentage = 22,
                Rating = 4.8,
                ReviewCount = 640,
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                DisplayOrder = 2
            },
            new()
            {
                Id = $"{slug}_k3",
                CountrySlug = slug,
                Name = $"{name} Medium Kit",
                Slug = $"{slug}-medium-kit",
                Size = "medium",
                Badge = "Most Popular",
                Description = $"All-rounder kit for the perfect balance of needs in {name}.",
                SellingPrice = basePrice + 5000,
                Mrp = Math.Round((basePrice + 5000) * 1.35m, 0),
                DiscountPercentage = 26,
                Rating = 4.8,
                ReviewCount = 1420,
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                DisplayOrder = 3
            },
            new()
            {
                Id = $"{slug}_k4",
                CountrySlug = slug,
                Name = $"{name} Medium Kit - Premium",
                Slug = $"{slug}-medium-kit-premium",
                Size = "medium",
                Badge = "",
                Description = $"Enhanced quality items for an elevated campus experience in {name}.",
                SellingPrice = basePrice + 8000,
                Mrp = Math.Round((basePrice + 8000) * 1.32m, 0),
                DiscountPercentage = 24,
                Rating = 4.9,
                ReviewCount = 780,
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                DisplayOrder = 4
            },
            new()
            {
                Id = $"{slug}_k5",
                CountrySlug = slug,
                Name = $"{name} Large Kit",
                Slug = $"{slug}-large-kit",
                Size = "large",
                Badge = "Best Value",
                Description = $"Complete kit with everything you might need for study in {name}.",
                SellingPrice = basePrice + 12000,
                Mrp = Math.Round((basePrice + 12000) * 1.35m, 0),
                DiscountPercentage = 26,
                Rating = 4.9,
                ReviewCount = 1890,
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                DisplayOrder = 5
            },
            new()
            {
                Id = $"{slug}_k6",
                CountrySlug = slug,
                Name = $"{name} Large Kit - Premium",
                Slug = $"{slug}-large-kit-premium",
                Size = "large",
                Badge = "",
                Description = $"Ultimate comfort with premium curated quality products for {name}.",
                SellingPrice = basePrice + 17000,
                Mrp = Math.Round((basePrice + 17000) * 1.32m, 0),
                DiscountPercentage = 24,
                Rating = 4.9,
                ReviewCount = 1050,
                ImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                IsUploadingSoon = true,
                DisplayOrder = 6
            }
        };
    }
}

