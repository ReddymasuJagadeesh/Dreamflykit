namespace Dreamflykit.Application.BeautyHub.Models;

public sealed class BeautyHubDto
{
    public BeautyThemeSettingsDto ThemeSettings { get; set; } = new();
    public BeautyHeroBannerDto HeroBanner { get; set; } = new();
    public BeautyPromoBannersDto PromoBanners { get; set; } = new();
    public BeautySubCategoriesDto SubCategories { get; set; } = new();
    public List<BeautyCategoryPageDto> CategoryPages { get; set; } = new();
    public List<BeautySubcategoryPageDto> SubcategoryPages { get; set; } = new();
    public BeautySpotlightBannerDto SpotlightBanner { get; set; } = new();
    public BeautyArchedSectionDto SpecialGiftsForHer { get; set; } = new();
    public BeautyArchedSectionDto GrandGiftsForHim { get; set; } = new();
    public BeautyDualBannersDto DualBanners { get; set; } = new();
    public BeautyPremiumGiftsDto PremiumGifts { get; set; } = new();
    public BeautyFestiveLooksDto FestiveLooks { get; set; } = new();
    public BeautyLimitedDealsRibbonDto LimitedDealsRibbon { get; set; } = new();
    public BeautyFlashDealsTimerDto FlashDealsTimer { get; set; } = new();
    public BeautyPicksUnder99Dto BeautyPicksUnder99 { get; set; } = new();
    public BeautyBiggestBrandsDto BiggestBrands { get; set; } = new();
    public BeautyPartnersDto BeautyPartners { get; set; } = new();
    public BeautyBrandNewDto BrandNewInBeauty { get; set; } = new();

    public static BeautyHubDto CreateDefault()
    {
        return new BeautyHubDto
        {
            ThemeSettings = new BeautyThemeSettingsDto
            {
                ThemeName = "BIG BANG DIWALI",
                ThemeTagline = "LOWEST PRICE OF 2026 • Beauty, food & more",
                BadgeText = "✨ FESTIVE GLOW DHAMAKA",
                NoticeBarText = "🎆 Festive Glow Dhamaka is LIVE! Extra 10% instant bank discount on SBI Card + Daily Flash Deals up to 80% Off on Skincare, Fragrances & Makeup.",
                IsFestiveEnabled = true,
                FrameDesignStyle = "FestiveArchedGolden",
                AccentGold = "#fbbf24",
                PrimaryBgGradient = "linear-gradient(135deg, #700b1a 0%, #a8122a 40%, #c2193b 70%, #d94612 100%)",
                DiyaDecorationEnabled = true
            },

            HeroBanner = new BeautyHeroBannerDto
            {
                IsEnabled = true,
                AutoPlay = true,
                IntervalSeconds = 4,
                Slides = new List<BeautyHeroSlideDto>
                {
                    new()
                    {
                        Id = "bpc-hero-1",
                        TagBadge = "BIG BANG DIWALI",
                        Title = "LOWEST PRICE OF 2026",
                        PriceHighlight = "Beauty, food & more",
                        Subtitle = "Mega Festive Glow Carnival • Luxury Fragrances, Serums & Lipsticks",
                        ImageUrl = "/images/beauty-hub/hero-diwali-banner.webp",
                        TargetUrl = "#beauty-products-catalog",
                        ButtonText = "Explore Deals →",
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Id = "bpc-hero-2",
                        TagBadge = "GLOBAL LUXE STORE",
                        Title = "The Global Luxe Store",
                        PriceHighlight = "Up to 60% Off*",
                        Subtitle = "Davidoff, Jaguar, L'Oréal Paris, Eucerin & K-Beauty Cult Classics",
                        ImageUrl = "/images/beauty-hub/hero-luxe-store.webp",
                        TargetUrl = "#beauty-products-catalog",
                        ButtonText = "Shop Luxury →",
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Id = "bpc-hero-3",
                        TagBadge = "FESTIVE GLAM MUST-HAVES",
                        Title = "Nail Every Festive Look",
                        PriceHighlight = "Starting Under ₹149*",
                        Subtitle = "Long-wear Foundations, Waterproof Mascaras & Festive Gift Sets",
                        ImageUrl = "/images/beauty-hub/hero-festive-glow.webp",
                        TargetUrl = "#beauty-products-catalog",
                        ButtonText = "Shop Festive Looks →",
                        IsUploadingSoon = false
                    }
                }
            },

            PromoBanners = new BeautyPromoBannersDto
            {
                IsEnabled = true,
                BankOfferHeader = "BANK OFFER REFRESHED DAILY",
                BankOfferSubtext = "10% Instant Discount* on SBI Debit & Credit Cards | Min Spend ₹1,500",
                Cards = new List<BeautyPromoCardDto>
                {
                    new()
                    {
                        Id = "promo-1",
                        TagBadge = "BIG BANG DIWALI",
                        BankOfferBadge = "BANK OFFER REFRESHED DAILY",
                        Title = "GEN-Z FAVES",
                        DiscountHighlight = "Up to 40% Off",
                        Subtitle = "Biestaa, nathabit & more",
                        BankOfferText = "10% Instant Discount* on SBI Card",
                        ImageUrl = "/images/beauty-hub/promo-genz-faves.webp",
                        TargetUrl = "#beauty-products-catalog",
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Id = "promo-2",
                        TagBadge = "BIG BANG DIWALI",
                        BankOfferBadge = "BANK OFFER REFRESHED DAILY",
                        Title = "Luscious locks",
                        DiscountHighlight = "Up to 75% Off",
                        Subtitle = "Pilgrim, Dove & more",
                        BankOfferText = "10% Instant Discount* on SBI Card",
                        ImageUrl = "/images/beauty-hub/promo-luscious-locks.webp",
                        TargetUrl = "#beauty-products-catalog",
                        IsUploadingSoon = false
                    },
                    new()
                    {
                        Id = "promo-3",
                        TagBadge = "BIG BANG DIWALI",
                        BankOfferBadge = "BANK OFFER REFRESHED DAILY",
                        Title = "Festive Glam Super Savings",
                        DiscountHighlight = "Up to 70% Off",
                        Subtitle = "Maybelline, Lakme & Swiss Beauty",
                        BankOfferText = "10% Instant Discount* on SBI Card",
                        ImageUrl = "/images/beauty-hub/promo-super-savings.webp",
                        TargetUrl = "#beauty-products-catalog",
                        IsUploadingSoon = false
                    }
                }
            },

            // 11 Core Beauty Categories (Shop by Category) from user spec
            SubCategories = new BeautySubCategoriesDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                Subtitle = "Explore verified beauty essentials, skincare rituals, salon haircare, and luxury fragrances",
                Items = CreateDefaultCategories()
            },

            CategoryPages = CreateDefaultCategoryPages(),
            SubcategoryPages = CreateDefaultSubcategoryPages(),

            SpotlightBanner = new BeautySpotlightBannerDto
            {
                IsEnabled = true,
                Title = "The Global Luxe Store",
                Subtitle = "Curated luxury fragrances, premium French serums & international beauty collections",
                ButtonText = "Explore now →",
                ImageUrl = "/images/beauty-hub/spotlight-global-luxe.webp",
                TargetUrl = "#beauty-products-catalog",
                IsUploadingSoon = false
            },

            SpecialGiftsForHer = new BeautyArchedSectionDto
            {
                IsEnabled = true,
                SectionTitle = "Special gifts for her",
                Subtitle = "Delightful makeup combos, luxury elixirs & glowing skincare picks",
                AccentColor = "#dc2626",
                BgGradient = "linear-gradient(135deg, #7f1d1d 0%, #b91c1c 50%, #991b1b 100%)",
                Cards = new List<BeautyArchedCardDto>
                {
                    new() { Id = "her-1", OfferTag = "Min. 40% Off", Title = "Bath combo", Subtitle = "Coffee scrubs & whipped butters", ImageUrl = "/images/beauty-hub/gift-her-bath-combo.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "her-2", OfferTag = "Under ₹199", Title = "Lip makeup", Subtitle = "Creamy matte & hydrating oils", ImageUrl = "/images/beauty-hub/gift-her-lip-makeup.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "her-3", OfferTag = "Up to 65% Off", Title = "Luxury fragrances", Subtitle = "Eau de Parfum for her", ImageUrl = "/images/beauty-hub/gift-her-fragrances.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "her-4", OfferTag = "Min. 40% Off", Title = "Hair care", Subtitle = "Nourishing masks & serums", ImageUrl = "/images/beauty-hub/gift-her-hair-care.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "her-5", OfferTag = "Under ₹199", Title = "Face makeup", Subtitle = "Flawless foundation & compacts", ImageUrl = "/images/beauty-hub/gift-her-face-makeup.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "her-6", OfferTag = "Under ₹249", Title = "Eye makeup", Subtitle = "Mesmer-eyes shadow palettes", ImageUrl = "/images/beauty-hub/gift-her-eye-makeup.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false }
                }
            },

            GrandGiftsForHim = new BeautyArchedSectionDto
            {
                IsEnabled = true,
                SectionTitle = "Grand gifts for him",
                Subtitle = "Sharp beard care, invigorating colognes & complete men's grooming hampers",
                AccentColor = "#ea580c",
                BgGradient = "linear-gradient(135deg, #7c2d12 0%, #c2410c 50%, #ea580c 100%)",
                Cards = new List<BeautyArchedCardDto>
                {
                    new() { Id = "him-1", OfferTag = "Min. 50% Off", Title = "Perfumes", Subtitle = "Bold woods & aquatic colognes", ImageUrl = "/images/beauty-hub/gift-him-perfumes.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "him-2", OfferTag = "Under ₹199", Title = "Beard care", Subtitle = "Beard oils, washes & balms", ImageUrl = "/images/beauty-hub/gift-him-beard-care.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "him-3", OfferTag = "Min. 50% Off", Title = "Grooming kits", Subtitle = "Head-to-toe luxury hampers", ImageUrl = "/images/beauty-hub/gift-him-grooming-kits.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "him-4", OfferTag = "Min. 50% Off", Title = "Skincare for men", Subtitle = "Oil-control washes & moisturizers", ImageUrl = "/images/beauty-hub/gift-him-skincare.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "him-5", OfferTag = "Up to 70% Off", Title = "Luxe fragrance", Subtitle = "Jaguar, Davidoff & Bella Vita", ImageUrl = "/images/beauty-hub/gift-him-luxe-fragrance.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "him-6", OfferTag = "Under ₹199", Title = "Men's styling", Subtitle = "Hair pomades, waxes & sprays", ImageUrl = "/images/beauty-hub/gift-him-styling.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false }
                }
            },

            DualBanners = new BeautyDualBannersDto
            {
                IsEnabled = true,
                LeftBanner = new BeautyDualBannerCardDto
                {
                    TagBadge = "Diwali celebration",
                    Title = "Beauty, food & more",
                    PriceHighlight = "From ₹99",
                    ImageUrl = "/images/beauty-hub/dual-banner-diwali.webp",
                    TargetUrl = "#beauty-products-catalog",
                    ButtonText = "→",
                    IsUploadingSoon = false
                },
                RightBanner = new BeautyDualBannerCardDto
                {
                    TagBadge = "Diwali celebration",
                    Title = "Festive Glow sets",
                    PriceHighlight = "Up to 60% Off",
                    ImageUrl = "/images/beauty-hub/dual-banner-sets.webp",
                    TargetUrl = "#beauty-products-catalog",
                    ButtonText = "→",
                    IsUploadingSoon = false
                }
            },

            PremiumGifts = new BeautyPremiumGiftsDto
            {
                IsEnabled = true,
                SectionTitle = "Premium festive gifts",
                Subtitle = "Luxury perfumes, French skincare elixirs & premium gift boxes",
                Brands = new List<BeautyBrandCardDto>
                {
                    new() { BrandName = "Eucerin", DiscountTag = "Min. 10% Off", ImageUrl = "/images/beauty-hub/brand-eucerin.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "Cetaphil", DiscountTag = "Up to 30% Off", ImageUrl = "/images/beauty-hub/brand-cetaphil.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "Minimalist", DiscountTag = "Flat 15% Off", ImageUrl = "/images/beauty-hub/brand-minimalist.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "The Ordinary", DiscountTag = "Special Value", ImageUrl = "/images/beauty-hub/brand-the-ordinary.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "L'Oréal Paris", DiscountTag = "Up to 40% Off", ImageUrl = "/images/beauty-hub/brand-loreal.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "DAVIDOFF", DiscountTag = "Min. 30% Off", ImageUrl = "/images/beauty-hub/brand-davidoff.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "Jaguar", DiscountTag = "Up to 50% Off", ImageUrl = "/images/beauty-hub/brand-jaguar.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "Bella Vita", DiscountTag = "Buy 1 Get 1", ImageUrl = "/images/beauty-hub/brand-bellavita.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "Maybelline", DiscountTag = "Up to 45% Off", ImageUrl = "/images/beauty-hub/brand-maybelline.webp", TargetUrl = "#beauty-products-catalog" }
                }
            },

            FestiveLooks = new BeautyFestiveLooksDto
            {
                IsEnabled = true,
                SectionTitle = "Festive looks",
                Subtitle = "Curated collections to ace every festival, wedding & Diwali gathering",
                Cards = new List<BeautyFestiveLookCardDto>
                {
                    new() { LookName = "Flawless finish", OfferTag = "Under ₹299", Subtitle = "High-coverage matte foundations", ImageUrl = "/images/beauty-hub/look-flawless.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { LookName = "Desi drama", OfferTag = "Starting ₹149", Subtitle = "Intense waterproof kajal & shadows", ImageUrl = "/images/beauty-hub/look-desi-drama.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { LookName = "Glow getter", OfferTag = "Up to 50% Off", Subtitle = "Illuminating strobe creams & drops", ImageUrl = "/images/beauty-hub/look-glow-getter.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { LookName = "Lip-sync ready", OfferTag = "Under ₹199", Subtitle = "Long-stay liquid transfer-proof mattes", ImageUrl = "/images/beauty-hub/look-lip-sync.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { LookName = "Glass skin ritual", OfferTag = "Min. 35% Off", Subtitle = "Korean essence & peptide serums", ImageUrl = "/images/beauty-hub/look-glass-skin.webp", TargetUrl = "#beauty-products-catalog" }
                }
            },

            LimitedDealsRibbon = new BeautyLimitedDealsRibbonDto
            {
                IsEnabled = true,
                Title = "LIMITED PERIOD DEALS",
                Tagline = "PRICES NEVER SEEN BEFORE",
                Badge = "⚡ FLASH SAVINGS",
                BgGradient = "linear-gradient(90deg, #b91c1c 0%, #dc2626 50%, #ea580c 100%)"
            },

            FlashDealsTimer = new BeautyFlashDealsTimerDto
            {
                IsEnabled = true,
                CountdownEndTimeUtc = DateTime.UtcNow.AddHours(28),
                Cards = new List<BeautyFlashDealCardDto>
                {
                    new() { DealTag = "FESTIVE DEAL", Title = "Liquid matte lipsticks", PriceHighlight = "Under ₹199", OriginalPrice = "₹499", ImageUrl = "/images/beauty-hub/flash-deal-1.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { DealTag = "TOP RATED", Title = "Hydrating face serums", PriceHighlight = "From ₹249", OriginalPrice = "₹699", ImageUrl = "/images/beauty-hub/flash-deal-2.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { DealTag = "LIMITED TIME", Title = "Eau de Parfum hampers", PriceHighlight = "Min. 50% Off", OriginalPrice = "₹1,999", ImageUrl = "/images/beauty-hub/flash-deal-3.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { DealTag = "HOT PICK", Title = "Salon hair spas & masks", PriceHighlight = "Under ₹299", OriginalPrice = "₹750", ImageUrl = "/images/beauty-hub/flash-deal-4.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { DealTag = "FESTIVE SPECIAL", Title = "Beard styling combo kits", PriceHighlight = "From ₹199", OriginalPrice = "₹599", ImageUrl = "/images/beauty-hub/flash-deal-5.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { DealTag = "MEGA DISCOUNT", Title = "Ultra matte sunscreens", PriceHighlight = "Flat 40% Off", OriginalPrice = "₹499", ImageUrl = "/images/beauty-hub/flash-deal-6.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { DealTag = "BESTSELLER", Title = "Gel eye kajal combos", PriceHighlight = "Under ₹149", OriginalPrice = "₹350", ImageUrl = "/images/beauty-hub/flash-deal-7.webp", TargetUrl = "#beauty-products-catalog" }
                }
            },

            BeautyPicksUnder99 = new BeautyPicksUnder99Dto
            {
                IsEnabled = true,
                SectionTitle = "Beauty picks under ₹99",
                Subtitle = "Student essentials & budget grooming must-haves",
                HighlightBadge = "BUDGET DHAMAKA",
                Cards = new List<BeautyArchedCardDto>
                {
                    new() { Id = "p99-1", OfferTag = "Under ₹99", Title = "Hydrating sheet mask", Subtitle = "Green tea & rice water", ImageUrl = "/images/beauty-hub/pick-sheet-mask.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Id = "p99-2", OfferTag = "Under ₹99", Title = "Lip balm with SPF 15", Subtitle = "Cherry moisture tint", ImageUrl = "/images/beauty-hub/pick-lip-balm.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Id = "p99-3", OfferTag = "Under ₹99", Title = "Charcoal face wash 50g", Subtitle = "Pollution defense wash", ImageUrl = "/images/beauty-hub/pick-face-wash.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Id = "p99-4", OfferTag = "Under ₹99", Title = "Pocket perfume spray", Subtitle = "Long-lasting travel mist", ImageUrl = "/images/beauty-hub/pick-pocket-perfume.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Id = "p99-5", OfferTag = "Under ₹99", Title = "Aloe vera soothing gel", Subtitle = "100% pure organic gel", ImageUrl = "/images/beauty-hub/pick-aloe-gel.webp", TargetUrl = "#beauty-products-catalog" }
                }
            },

            BiggestBrands = new BeautyBiggestBrandsDto
            {
                IsEnabled = true,
                SectionTitle = "Biggest brands on offer",
                Subtitle = "Official brand stores with authentic guarantee & fastest delivery",
                Brands = new List<BeautyBigBrandCardDto>
                {
                    new() { BrandName = "Maybelline New York", Tagline = "Make It Happen", DiscountHighlight = "Up to 45% Off", LogoUrl = "/images/beauty-hub/brand-logo-maybelline.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "Lakme", Tagline = "Reinvent Yourself", DiscountHighlight = "Up to 50% Off", LogoUrl = "/images/beauty-hub/brand-logo-lakme.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "L'Oréal Paris", Tagline = "Because You're Worth It", DiscountHighlight = "Up to 40% Off", LogoUrl = "/images/beauty-hub/brand-logo-loreal.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "Cetaphil", Tagline = "Gentle Power for Skin", DiscountHighlight = "Up to 25% Off", LogoUrl = "/images/beauty-hub/brand-logo-cetaphil.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "Minimalist", Tagline = "Hide Nothing", DiscountHighlight = "Flat 15% Off", LogoUrl = "/images/beauty-hub/brand-logo-minimalist.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "Bella Vita", Tagline = "Organic Luxury", DiscountHighlight = "Min. 50% Off", LogoUrl = "/images/beauty-hub/brand-logo-bellavita.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "Plum Goodness", Tagline = "100% Vegan Beauty", DiscountHighlight = "Up to 35% Off", LogoUrl = "/images/beauty-hub/brand-logo-plum.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { BrandName = "The Derma Co.", Tagline = "Designed by Dermatologists", DiscountHighlight = "Buy 1 Get 1", LogoUrl = "/images/beauty-hub/brand-logo-dermaco.webp", TargetUrl = "#beauty-products-catalog" }
                }
            },

            BeautyPartners = new BeautyPartnersDto
            {
                IsEnabled = true,
                Title = "Our Authorized Brand Partners",
                Items = new List<BeautyPartnerItemDto>
                {
                    new() { Name = "Maybelline", LogoUrl = "/images/beauty-hub/partner-maybelline.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Name = "Lakme", LogoUrl = "/images/beauty-hub/partner-lakme.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Name = "Cetaphil", LogoUrl = "/images/beauty-hub/partner-cetaphil.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Name = "L'Oréal", LogoUrl = "/images/beauty-hub/partner-loreal.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Name = "Minimalist", LogoUrl = "/images/beauty-hub/partner-minimalist.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Name = "The Ordinary", LogoUrl = "/images/beauty-hub/partner-ordinary.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Name = "Bella Vita", LogoUrl = "/images/beauty-hub/partner-bellavita.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Name = "Plum", LogoUrl = "/images/beauty-hub/partner-plum.webp", TargetUrl = "#beauty-products-catalog" }
                }
            },

            BrandNewInBeauty = new BeautyBrandNewDto
            {
                IsEnabled = true,
                SectionTitle = "Brand new in beauty",
                Subtitle = "Fresh launches, viral trends & newly imported cult favorites",
                Cards = new List<BeautyBrandNewCardDto>
                {
                    new() { Title = "Niacinamide Body Lotion", Brand = "Minimalist", PriceHighlight = "₹399", ImageUrl = "/images/beauty-hub/new-minimalist-body.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Title = "SuperStay Vinyl Ink", Brand = "Maybelline", PriceHighlight = "₹649", ImageUrl = "/images/beauty-hub/new-vinyl-ink.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Title = "Hyaluronic Sunscreen Aqua Gel", Brand = "The Derma Co.", PriceHighlight = "₹499", ImageUrl = "/images/beauty-hub/new-derma-sun.webp", TargetUrl = "#beauty-products-catalog" },
                    new() { Title = "Glutathione Radiance Serum", Brand = "Dot & Key", PriceHighlight = "₹595", ImageUrl = "/images/beauty-hub/new-dotkey-serum.webp", TargetUrl = "#beauty-products-catalog" }
                }
            }
        };
    }

    /// <summary>
    /// Creates the 9 Core Beauty Categories matching the user's specification image:
    /// Makeup, Skin, Fragrance, Hair, Bath & Body, Gifts, Trending Now, New Launches, Minis.
    /// </summary>
    public static List<BeautySubCategoryItemDto> CreateDefaultCategories()
    {
        return new List<BeautySubCategoryItemDto>
        {
            new()
            {
                Id = "bcat-1",
                Title = "Makeup",
                Slug = "makeup",
                Icon = "💄",
                Badge = "BESTSELLER",
                TargetUrl = "/category/beauty/makeup",
                ImageUrl = "/images/beauty-hub/categories/makeup.webp",
                IsUploadingSoon = true,
                Description = "Lipsticks, Foundations, Mascaras & Palettes",
                DisplayOrder = 1,
                IsActive = true,
                Subcategories = new List<string> { "Lips", "Face", "Eyes", "Nails", "Tools & Brushes", "Makeup Remover" }
            },
            new()
            {
                Id = "bcat-2",
                Title = "Skin",
                Slug = "skin",
                Icon = "🧴",
                Badge = "TRENDING",
                TargetUrl = "/category/beauty/skin",
                ImageUrl = "/images/beauty-hub/categories/skin.webp",
                IsUploadingSoon = true,
                Description = "Cleansers, Serums, Moisturizers & Sunscreens",
                DisplayOrder = 2,
                IsActive = true,
                Subcategories = new List<string> { "Cleansers", "Serums & Treatments", "Moisturizers", "Sunscreen", "Eye Care", "Masks & Peels" }
            },
            new()
            {
                Id = "bcat-3",
                Title = "Fragrance",
                Slug = "fragrance",
                Icon = "🌸",
                Badge = "LUXE SCENTS",
                TargetUrl = "/category/beauty/fragrance",
                ImageUrl = "/images/beauty-hub/categories/fragrance.webp",
                IsUploadingSoon = true,
                Description = "Luxury Perfumes, Body Mists, EDPs & Gift Sets",
                DisplayOrder = 3,
                IsActive = true,
                Subcategories = new List<string> { "Eau De Parfum", "Eau De Toilette", "Body Mists", "Perfume Sets", "Luxury Scents", "Roll-ons" }
            },
            new()
            {
                Id = "bcat-4",
                Title = "Hair",
                Slug = "hair",
                Icon = "💇‍♀️",
                Badge = "SALON PICK",
                TargetUrl = "/category/beauty/hair",
                ImageUrl = "/images/beauty-hub/categories/hair.webp",
                IsUploadingSoon = true,
                Description = "Shampoos, Conditioners, Hair Oils & Spa Masks",
                DisplayOrder = 4,
                IsActive = true,
                Subcategories = new List<string> { "Shampoo", "Conditioner", "Hair Oils & Tonics", "Hair Masks & Serums", "Hair Styling", "Hair Color" }
            },
            new()
            {
                Id = "bcat-5",
                Title = "Bath & Body",
                Slug = "bath-body",
                Icon = "🛁",
                Badge = "MUST HAVE",
                TargetUrl = "/category/beauty/bath-body",
                ImageUrl = "/images/beauty-hub/categories/bath-body.webp",
                IsUploadingSoon = true,
                Description = "Body Washes, Lotions, Scrubs & Hygiene Care",
                DisplayOrder = 5,
                IsActive = true,
                Subcategories = new List<string> { "Body Wash", "Body Lotions", "Body Scrubs", "Hand & Foot Care", "Bath Salts & Oils", "Deodorants" }
            },
            new()
            {
                Id = "bcat-6",
                Title = "Gifts",
                Slug = "gifts",
                Icon = "🎁",
                Badge = "FESTIVE READY",
                TargetUrl = "/category/beauty/gifts",
                ImageUrl = "/images/beauty-hub/categories/gifts.webp",
                IsUploadingSoon = true,
                Description = "Curated Beauty Hampers, Fragrance Coffrets & Luxe Boxes",
                DisplayOrder = 6,
                IsActive = true,
                Subcategories = new List<string> { "Skincare Gifts", "Makeup Kits", "Fragrance Boxes", "Spa Hampers", "Budget Gifts", "Luxe Sets" }
            },
            new()
            {
                Id = "bcat-7",
                Title = "Trending Now",
                Slug = "trending-now",
                Icon = "⚡",
                Badge = "VIRAL HITS",
                TargetUrl = "/category/beauty/trending-now",
                ImageUrl = "/images/beauty-hub/categories/trending-now.webp",
                IsUploadingSoon = true,
                Description = "Social Media Sensations, Viral K-Beauty & Glass Skin",
                DisplayOrder = 7,
                IsActive = true,
                Subcategories = new List<string> { "Viral K-Beauty", "Glass Skin", "Tinted Lip Balms", "Scalp Care", "Multi Sticks", "Lash Serums" }
            },
            new()
            {
                Id = "bcat-8",
                Title = "New Launches",
                Slug = "new-launches",
                Icon = "✨",
                Badge = "JUST DROPPED",
                TargetUrl = "/category/beauty/new-launches",
                ImageUrl = "/images/beauty-hub/categories/new-launches.webp",
                IsUploadingSoon = true,
                Description = "Fresh Arrivals, Newly Launched Formulations & Limited Drops",
                DisplayOrder = 8,
                IsActive = true,
                Subcategories = new List<string> { "Dropped Makeup", "Latest Skincare", "Fresh Fragrance", "Hair Innovations", "Body Formulas", "Festival Editions" }
            },
            new()
            {
                Id = "bcat-9",
                Title = "Minis",
                Slug = "minis",
                Icon = "🧴",
                Badge = "TRAVEL SIZE",
                TargetUrl = "/category/beauty/minis",
                ImageUrl = "/images/beauty-hub/categories/minis.webp",
                IsUploadingSoon = true,
                Description = "Pocket-Sized Must-Haves, TSA Friendly & Trial Pouches",
                DisplayOrder = 9,
                IsActive = true,
                Subcategories = new List<string> { "Mini Lipsticks", "Travel Skincare", "Mini Fragrances", "Mini Haircare", "Mini Body Washes", "Trial Kits" }
            }
        };
    }

    /// <summary>
    /// Creates the 11 Category Landing Pages (Level 1: Category -> Shows Hero Banner & Subcategories).
    /// </summary>
    public static List<BeautyCategoryPageDto> CreateDefaultCategoryPages()
    {
        return new List<BeautyCategoryPageDto>
        {
            new()
            {
                CategorySlug = "makeup",
                CategoryName = "Makeup",
                IconEmoji = "💄",
                Description = "Explore iconic lipsticks, high-coverage foundations, waterproof eyeliners, and radiant highlighters from world-renowned beauty houses.",
                HeroBanner = new BeautyCategoryHeroDto
                {
                    IsEnabled = true,
                    BadgeText = "VERIFIED BEAUTY & GLAM",
                    Title = "Glamour & Runway Makeup Studio",
                    TitleHighlight = "UP TO 60% OFF • 100% AUTHENTIC",
                    Subtitle = "Discover velvety lipsticks, weightless foundations, intensely pigmented palettes, and pro brushes curated for effortless everyday beauty.",
                    ImageUrl = "/images/beauty-hub/categories/hero-makeup.webp",
                    IsUploadingSoon = true,
                    ThemeColor = "#e11d48",
                    BannerHeight = 340
                },
                Subcategories = new List<BeautySubcategoryCardDto>
                {
                    new() { SubcategorySlug = "lips", SubcategoryName = "Lips", IconEmoji = "💋", Description = "Lipsticks, Liquid Matte, Lip Gloss, Liners & Tinted Balms", ItemCountText = "18+ Products", TargetUrl = "/category/beauty/makeup/lips", IsUploadingSoon = true, DisplayOrder = 1, SampleItems = new List<string> { "Matte Lipstick", "Liquid Lip Color", "High-Shine Gloss", "Lip Liner" } },
                    new() { SubcategorySlug = "face", SubcategoryName = "Face", IconEmoji = "✨", Description = "Foundations, Concealers, Compacts, Primers, Blushes & Setting Sprays", ItemCountText = "24+ Products", TargetUrl = "/category/beauty/makeup/face", IsUploadingSoon = true, DisplayOrder = 2, SampleItems = new List<string> { "Liquid Foundation", "Concealer", "Primer", "Compact Powder" } },
                    new() { SubcategorySlug = "eyes", SubcategoryName = "Eyes", IconEmoji = "👁️", Description = "Kajal, Precision Eyeliners, Volumizing Mascaras & Eyeshadow Palettes", ItemCountText = "20+ Products", TargetUrl = "/category/beauty/makeup/eyes", IsUploadingSoon = true, DisplayOrder = 3, SampleItems = new List<string> { "24HR Kajal", "Waterproof Liner", "Mascara", "Eyeshadows" } },
                    new() { SubcategorySlug = "nails", SubcategoryName = "Nails", IconEmoji = "💅", Description = "Gel Nail Lacquers, Matte Enamels, Nail Strengtheners & Polish Removers", ItemCountText = "15+ Products", TargetUrl = "/category/beauty/makeup/nails", IsUploadingSoon = true, DisplayOrder = 4, SampleItems = new List<string> { "Gel Lacquer", "Pastel Polish", "Nail Remover", "Cuticle Oil" } },
                    new() { SubcategorySlug = "tools-brushes", SubcategoryName = "Tools & Brushes", IconEmoji = "🖌️", Description = "Beauty Blenders, Foundation Brushes, Eyelash Curlers & Cleaning Mats", ItemCountText = "12+ Products", TargetUrl = "/category/beauty/makeup/tools-brushes", IsUploadingSoon = true, DisplayOrder = 5, SampleItems = new List<string> { "Makeup Sponge", "Brush Set", "Lash Curler", "Brush Cleanser" } },
                    new() { SubcategorySlug = "makeup-remover", SubcategoryName = "Makeup Remover", IconEmoji = "🫧", Description = "Micellar Cleansing Waters, Bi-Phase Removers & Gentle Makeup Wipes", ItemCountText = "10+ Products", TargetUrl = "/category/beauty/makeup/makeup-remover", IsUploadingSoon = true, DisplayOrder = 6, SampleItems = new List<string> { "Micellar Water", "Cleansing Balm", "Dual Phase", "Makeup Wipes" } }
                }
            },
            new()
            {
                CategorySlug = "skin",
                CategoryName = "Skin",
                IconEmoji = "🧴",
                Description = "Cleanse, treat, hydrate and protect with clinical actives, K-beauty essences, and dermatologist-tested skincare regimens.",
                HeroBanner = new BeautyCategoryHeroDto
                {
                    IsEnabled = true,
                    BadgeText = "DERMATOLOGIST VERIFIED",
                    Title = "Advanced Clinical & K-Beauty Skincare",
                    TitleHighlight = "RADIANT GLOW • DERMA FORMULAS",
                    Subtitle = "Target acne, dullness, fine lines, and hydration barriers with clinically proven serums, calming moisturizers, and invisible SPF protection.",
                    ImageUrl = "/images/beauty-hub/categories/hero-skin.webp",
                    IsUploadingSoon = true,
                    ThemeColor = "#0284c7",
                    BannerHeight = 340
                },
                Subcategories = new List<BeautySubcategoryCardDto>
                {
                    new() { SubcategorySlug = "cleansers", SubcategoryName = "Cleansers & Face Wash", IconEmoji = "🧼", Description = "Foaming Gel Washes, Gentle Hydrating Cleansers & Exfoliating Scrubs", ItemCountText = "22+ Products", TargetUrl = "/category/beauty/skin/cleansers", IsUploadingSoon = true, DisplayOrder = 1 },
                    new() { SubcategorySlug = "serums-treatments", SubcategoryName = "Serums & Treatments", IconEmoji = "🧪", Description = "Vitamin C, Salicylic Acid, Niacinamide, Hyaluronic & Retinol Elixirs", ItemCountText = "28+ Products", TargetUrl = "/category/beauty/skin/serums-treatments", IsUploadingSoon = true, DisplayOrder = 2 },
                    new() { SubcategorySlug = "moisturizers", SubcategoryName = "Moisturizers & Creams", IconEmoji = "💧", Description = "Ceramide Barrier Creams, Oil-Free Gel Hydrators & Rich Night Creams", ItemCountText = "20+ Products", TargetUrl = "/category/beauty/skin/moisturizers", IsUploadingSoon = true, DisplayOrder = 3 },
                    new() { SubcategorySlug = "sunscreen", SubcategoryName = "Sunscreen & Sun Care", IconEmoji = "☀️", Description = "Ultra Matte Gel SPF 50+, Clear Sunscreen Sticks & Mineral Fluid Screens", ItemCountText = "18+ Products", TargetUrl = "/category/beauty/skin/sunscreen", IsUploadingSoon = true, DisplayOrder = 4 },
                    new() { SubcategorySlug = "eye-lip-care", SubcategoryName = "Eye & Lip Care", IconEmoji = "👁️", Description = "Under Eye Dark Circle Gels, Peptide Rollers & Overnight Lip Sleeping Masks", ItemCountText = "14+ Products", TargetUrl = "/category/beauty/skin/eye-lip-care", IsUploadingSoon = true, DisplayOrder = 5 },
                    new() { SubcategorySlug = "masks-peels", SubcategoryName = "Masks & Peels", IconEmoji = "🎭", Description = "Korean Hydrating Sheet Masks, Clay Purifying Packs & Gentle AHA BHA Peels", ItemCountText = "16+ Products", TargetUrl = "/category/beauty/skin/masks-peels", IsUploadingSoon = true, DisplayOrder = 6 }
                }
            },
            new()
            {
                CategorySlug = "fragrance",
                CategoryName = "Fragrance",
                IconEmoji = "🌸",
                Description = "Indulge in luxurious Eau de Parfums, alluring body mists, smoky wood accords, and couples discovery gift boxes.",
                HeroBanner = new BeautyCategoryHeroDto
                {
                    IsEnabled = true,
                    BadgeText = "HAUTE PARFUMERIE",
                    Title = "Luxury Fragrances & Fine Scents",
                    TitleHighlight = "100% ORIGINAL IMPORTED OILS",
                    Subtitle = "Immerse in French vanilla, radiant citrus bouquets, rare amber oud, and refreshing everyday body mists.",
                    ImageUrl = "/images/beauty-hub/categories/hero-fragrance.webp",
                    IsUploadingSoon = true,
                    ThemeColor = "#9333ea",
                    BannerHeight = 340
                },
                Subcategories = new List<BeautySubcategoryCardDto>
                {
                    new() { SubcategorySlug = "edp", SubcategoryName = "Eau De Parfum (EDP)", IconEmoji = "💐", Description = "Long-Lasting Luxury EDPs, Sensual Florals & Warm Amber Accords", ItemCountText = "22+ Products", TargetUrl = "/category/beauty/fragrance/edp", IsUploadingSoon = true, DisplayOrder = 1 },
                    new() { SubcategorySlug = "edt", SubcategoryName = "Eau De Toilette (EDT)", IconEmoji = "🌲", Description = "Fresh Daily EDTs, Aquatic Citrus & Invigorating Sport Scents", ItemCountText = "18+ Products", TargetUrl = "/category/beauty/fragrance/edt", IsUploadingSoon = true, DisplayOrder = 2 },
                    new() { SubcategorySlug = "body-mists", SubcategoryName = "Body Mists & Sprays", IconEmoji = "✨", Description = "All-Day Fresh Fragrance Mists, Shimmer Body Sprays & Deos", ItemCountText = "16+ Products", TargetUrl = "/category/beauty/fragrance/body-mists", IsUploadingSoon = true, DisplayOrder = 3 },
                    new() { SubcategorySlug = "perfume-gift-sets", SubcategoryName = "Perfume Gift Sets", IconEmoji = "🎁", Description = "Miniature Discovery Coffrets, Couple Sets & Luxury Travel Boxes", ItemCountText = "14+ Products", TargetUrl = "/category/beauty/fragrance/perfume-gift-sets", IsUploadingSoon = true, DisplayOrder = 4 },
                    new() { SubcategorySlug = "luxury-perfumes", SubcategoryName = "Luxury & Niche Perfumes", IconEmoji = "👑", Description = "French Artisan Blends, Arabian Attars & Rare Oud Collections", ItemCountText = "12+ Products", TargetUrl = "/category/beauty/fragrance/luxury-perfumes", IsUploadingSoon = true, DisplayOrder = 5 }
                }
            },
            new()
            {
                CategorySlug = "hair",
                CategoryName = "Hair",
                IconEmoji = "💇‍♀️",
                Description = "Salon-grade shampoos, restorative keratin masks, scalp growth elixirs, and heat protectants for luscious campus locks.",
                HeroBanner = new BeautyCategoryHeroDto
                {
                    IsEnabled = true,
                    BadgeText = "SALON PROFESSIONAL CARE",
                    Title = "Haircare & Scalp Revival Hub",
                    TitleHighlight = "UP TO 50% OFF • PRO HAIR SOLUTIONS",
                    Subtitle = "Repair frizz, nourish roots, boost volume, and lock in color vibrancy with international salon formulations.",
                    ImageUrl = "/images/beauty-hub/categories/hero-hair.webp",
                    IsUploadingSoon = true,
                    ThemeColor = "#059669",
                    BannerHeight = 340
                },
                Subcategories = new List<BeautySubcategoryCardDto>
                {
                    new() { SubcategorySlug = "shampoo", SubcategoryName = "Shampoo", IconEmoji = "🫧", Description = "Anti-Dandruff, Color-Safe, Sulfate-Free & Volumizing Washes", ItemCountText = "20+ Products", TargetUrl = "/category/beauty/hair/shampoo", IsUploadingSoon = true, DisplayOrder = 1 },
                    new() { SubcategorySlug = "conditioners", SubcategoryName = "Conditioners", IconEmoji = "🧴", Description = "Deep Moisture Conditioners, Keratin Smooth & Weightless Leave-Ins", ItemCountText = "18+ Products", TargetUrl = "/category/beauty/hair/conditioners", IsUploadingSoon = true, DisplayOrder = 2 },
                    new() { SubcategorySlug = "hair-oils", SubcategoryName = "Hair Oils & Tonics", IconEmoji = "🌿", Description = "Rosemary Scalp Oils, Pure Moroccan Argan Serums & Onion Growth Blends", ItemCountText = "16+ Products", TargetUrl = "/category/beauty/hair/hair-oils", IsUploadingSoon = true, DisplayOrder = 3 },
                    new() { SubcategorySlug = "hair-masks-serums", SubcategoryName = "Hair Masks & Serums", IconEmoji = "💆‍♀️", Description = "Intensive Bond Repair Masques, Argan Hair Spas & Protein Treatments", ItemCountText = "14+ Products", TargetUrl = "/category/beauty/hair/hair-masks-serums", IsUploadingSoon = true, DisplayOrder = 4 },
                    new() { SubcategorySlug = "hair-styling-tools", SubcategoryName = "Hair Styling & Tools", IconEmoji = "✨", Description = "Heat Protectant Sprays, Volumizing Mousse, Styling Pomades & Hair Dryers", ItemCountText = "15+ Products", TargetUrl = "/category/beauty/hair/hair-styling-tools", IsUploadingSoon = true, DisplayOrder = 5 }
                }
            },
            new()
            {
                CategorySlug = "bath-body",
                CategoryName = "Bath & Body",
                IconEmoji = "🛁",
                Description = "Indulge in aromatic shower gels, whipped body butters, exfoliating coffee scrubs, and soothing hand and foot treatments.",
                HeroBanner = new BeautyCategoryHeroDto
                {
                    IsEnabled = true,
                    BadgeText = "LUXE SPA ESSENTIALS",
                    Title = "Bath, Body & Personal Wellness",
                    TitleHighlight = "HEAD-TO-TOE HYDRATION • UP TO 45% OFF",
                    Subtitle = "Transform daily showers into spa rituals with rich body butters, invigorating polishes, and pH-balanced intimate hygiene.",
                    ImageUrl = "/images/beauty-hub/categories/hero-bath-body.webp",
                    IsUploadingSoon = true,
                    ThemeColor = "#0d9488",
                    BannerHeight = 340
                },
                Subcategories = new List<BeautySubcategoryCardDto>
                {
                    new() { SubcategorySlug = "body-wash", SubcategoryName = "Shower Gels & Body Wash", IconEmoji = "🚿", Description = "Hydrating Shower Gels, Sulfate-Free Body Washes & Foaming Shower Oils", ItemCountText = "18+ Products", TargetUrl = "/category/beauty/bath-body/body-wash", IsUploadingSoon = true, DisplayOrder = 1 },
                    new() { SubcategorySlug = "body-lotions", SubcategoryName = "Body Lotions & Creams", IconEmoji = "🧴", Description = "Deep Moisture Shea Butters, Ceramide Lotions & Golden Shimmer Oils", ItemCountText = "20+ Products", TargetUrl = "/category/beauty/bath-body/body-lotions", IsUploadingSoon = true, DisplayOrder = 2 },
                    new() { SubcategorySlug = "body-scrubs", SubcategoryName = "Body Scrubs & Polishes", IconEmoji = "☕", Description = "Caffeine Body Polishes, Sugar Scrubs, Exfoliating Mitts & Peels", ItemCountText = "14+ Products", TargetUrl = "/category/beauty/bath-body/body-scrubs", IsUploadingSoon = true, DisplayOrder = 3 },
                    new() { SubcategorySlug = "hand-foot-care", SubcategoryName = "Hand & Foot Care", IconEmoji = "🤲", Description = "Shea Butter Hand Creams, Cracked Heel Repair Balms & Cuticle Oils", ItemCountText = "12+ Products", TargetUrl = "/category/beauty/bath-body/hand-foot-care", IsUploadingSoon = true, DisplayOrder = 4 },
                    new() { SubcategorySlug = "bath-salts-oils", SubcategoryName = "Bath Salts & Oils", IconEmoji = "🧂", Description = "Epsom Muscle Soak Salts, Shimmer Dry Body Oils & Relaxing Essences", ItemCountText = "10+ Products", TargetUrl = "/category/beauty/bath-body/bath-salts-oils", IsUploadingSoon = true, DisplayOrder = 5 }
                }
            },
            new()
            {
                CategorySlug = "gifts",
                CategoryName = "Gifts",
                IconEmoji = "🎁",
                Description = "Celebrate birthdays, festivals, and campus milestones with curated beauty hampers, perfume coffrets, and bespoke gift sets.",
                HeroBanner = new BeautyCategoryHeroDto
                {
                    IsEnabled = true,
                    BadgeText = "CELEBRATION & GIFTING",
                    Title = "Curated Beauty Hampers & Luxury Gift Boxes",
                    TitleHighlight = "FESTIVE READY • ELEGANT PACKAGING",
                    Subtitle = "Delight loved ones with bestselling skincare routines, vanity makeup chests, and luxury French fragrance gift sets.",
                    ImageUrl = "/images/beauty-hub/categories/hero-gifts.webp",
                    IsUploadingSoon = true,
                    ThemeColor = "#d97706",
                    BannerHeight = 340
                },
                Subcategories = new List<BeautySubcategoryCardDto>
                {
                    new() { SubcategorySlug = "skincare-gifts", SubcategoryName = "Skincare Gift Hampers", IconEmoji = "🧴", Description = "Complete Glow Kits, Vitamin C Trios & Korean Glass Skin Sets", ItemCountText = "16+ Products", TargetUrl = "/category/beauty/gifts/skincare-gifts", IsUploadingSoon = true, DisplayOrder = 1 },
                    new() { SubcategorySlug = "makeup-gifts", SubcategoryName = "Makeup Gift Kits", IconEmoji = "💄", Description = "Liquid Lipstick Wardrobe, Eye & Cheek Palettes & Full Glam Trunks", ItemCountText = "18+ Products", TargetUrl = "/category/beauty/gifts/makeup-gifts", IsUploadingSoon = true, DisplayOrder = 2 },
                    new() { SubcategorySlug = "fragrance-gifts", SubcategoryName = "Fragrance Gift Boxes", IconEmoji = "🌸", Description = "Luxury Discovery Coffrets, Scented Candle & EDP Combos", ItemCountText = "14+ Products", TargetUrl = "/category/beauty/gifts/fragrance-gifts", IsUploadingSoon = true, DisplayOrder = 3 },
                    new() { SubcategorySlug = "spa-gifts", SubcategoryName = "Bath & Spa Hampers", IconEmoji = "🛁", Description = "Relaxing Aromatherapy Sets, Turkish Rose Body Spa & Bath Bombs", ItemCountText = "12+ Products", TargetUrl = "/category/beauty/gifts/spa-gifts", IsUploadingSoon = true, DisplayOrder = 4 },
                    new() { SubcategorySlug = "budget-gifts", SubcategoryName = "Budget Gifts Under ₹499", IconEmoji = "🏷️", Description = "Student Pocket Favorites, Mini Perfume Packs & Sheet Mask Bouquets", ItemCountText = "20+ Products", TargetUrl = "/category/beauty/gifts/budget-gifts", IsUploadingSoon = true, DisplayOrder = 5 }
                }
            },
            new()
            {
                CategorySlug = "trending-now",
                CategoryName = "Trending Now",
                IconEmoji = "⚡",
                Description = "Discover the viral sensations taking social media by storm: Korean snail mucin, tinted peptide lip oils, and glass skin secrets.",
                HeroBanner = new BeautyCategoryHeroDto
                {
                    IsEnabled = true,
                    BadgeText = "VIRAL BEAUTY HITS",
                    Title = "Trending Now — The Viral Social Sensations",
                    TitleHighlight = "HOTTEST FINDS • COMMUNITY LOVED",
                    Subtitle = "Shop the exact beauty must-haves dominating reels and TikTok: plumping lip glazes, barrier balms, and dewy glow sticks.",
                    ImageUrl = "/images/beauty-hub/categories/hero-trending.webp",
                    IsUploadingSoon = true,
                    ThemeColor = "#ec4899",
                    BannerHeight = 340
                },
                Subcategories = new List<BeautySubcategoryCardDto>
                {
                    new() { SubcategorySlug = "viral-kbeauty", SubcategoryName = "Viral K-Beauty", IconEmoji = "🐌", Description = "96% Snail Mucin, Centella Calming Ampoules & Sun Sticks", ItemCountText = "18+ Products", TargetUrl = "/category/beauty/trending-now/viral-kbeauty", IsUploadingSoon = true, DisplayOrder = 1 },
                    new() { SubcategorySlug = "glass-skin", SubcategoryName = "Glass Skin Essentials", IconEmoji = "✨", Description = "Hydrating Milky Toners, Peptide Glazes & Barrier Hydration Creams", ItemCountText = "16+ Products", TargetUrl = "/category/beauty/trending-now/glass-skin", IsUploadingSoon = true, DisplayOrder = 2 },
                    new() { SubcategorySlug = "tinted-balms", SubcategoryName = "Tinted Lip Balms & Glazes", IconEmoji = "💋", Description = "Plumping Peptide Lip Glazes & pH Color-Changing Balms", ItemCountText = "15+ Products", TargetUrl = "/category/beauty/trending-now/tinted-balms", IsUploadingSoon = true, DisplayOrder = 3 },
                    new() { SubcategorySlug = "scalp-care", SubcategoryName = "Scalp Care & Serums", IconEmoji = "🌿", Description = "Rosemary Scalp Serums, Exfoliating Brushes & Growth Tonics", ItemCountText = "12+ Products", TargetUrl = "/category/beauty/trending-now/scalp-care", IsUploadingSoon = true, DisplayOrder = 4 },
                    new() { SubcategorySlug = "multi-sticks", SubcategoryName = "Multi-Use Sticks & Tints", IconEmoji = "🖍️", Description = "Lip & Cheek Cream Tints, Dewy Highlighting Sticks & Contour", ItemCountText = "14+ Products", TargetUrl = "/category/beauty/trending-now/multi-sticks", IsUploadingSoon = true, DisplayOrder = 5 }
                }
            },
            new()
            {
                CategorySlug = "new-launches",
                CategoryName = "New Launches",
                IconEmoji = "✨",
                Badge = "JUST DROPPED",
                Description = "Be the very first to experience brand-new shade drops, cutting-edge skincare breakthroughs, and seasonal limited editions.",
                HeroBanner = new BeautyCategoryHeroDto
                {
                    IsEnabled = true,
                    BadgeText = "FRESH ARRIVALS & DROPS",
                    Title = "New Launches — The Latest Beauty Breakthroughs",
                    TitleHighlight = "JUST LANDED • FIRST ACCESS",
                    Subtitle = "Experience newly formulated multi-ceramide moisturizers, transfer-proof vinyl inks, and niche seasonal perfumes.",
                    ImageUrl = "/images/beauty-hub/categories/hero-new-launches.webp",
                    IsUploadingSoon = true,
                    ThemeColor = "#6366f1",
                    BannerHeight = 340
                },
                Subcategories = new List<BeautySubcategoryCardDto>
                {
                    new() { SubcategorySlug = "dropped-makeup", SubcategoryName = "Just Dropped Makeup", IconEmoji = "💄", Description = "New Vinyl Lip Colors, Blur Setting Powders & Season Palettes", ItemCountText = "18+ Products", TargetUrl = "/category/beauty/new-launches/dropped-makeup", IsUploadingSoon = true, DisplayOrder = 1 },
                    new() { SubcategorySlug = "dropped-skincare", SubcategoryName = "Latest Skincare Formulations", IconEmoji = "🧪", Description = "Bio-Retinol Elixirs, Tri-Ceramide Barrier Creams & Water-Light SPF", ItemCountText = "20+ Products", TargetUrl = "/category/beauty/new-launches/dropped-skincare", IsUploadingSoon = true, DisplayOrder = 2 },
                    new() { SubcategorySlug = "dropped-fragrance", SubcategoryName = "Fresh Fragrance Releases", IconEmoji = "🌸", Description = "New Gourmand Vanilla EDPs, Tropical Mists & Travel Sprays", ItemCountText = "15+ Products", TargetUrl = "/category/beauty/new-launches/dropped-fragrance", IsUploadingSoon = true, DisplayOrder = 3 },
                    new() { SubcategorySlug = "dropped-hair", SubcategoryName = "Hair Innovations", IconEmoji = "💇‍♀️", Description = "Peptide Bond Repair Drops, Scalp Detox Scrubs & Heat Shields", ItemCountText = "14+ Products", TargetUrl = "/category/beauty/new-launches/dropped-hair", IsUploadingSoon = true, DisplayOrder = 4 },
                    new() { SubcategorySlug = "limited-editions", SubcategoryName = "Limited Festival Drops", IconEmoji = "🎆", Description = "Gold Foil Holiday Palettes, Collector Trunks & Gift Kits", ItemCountText = "12+ Products", TargetUrl = "/category/beauty/new-launches/limited-editions", IsUploadingSoon = true, DisplayOrder = 5 }
                }
            },
            new()
            {
                CategorySlug = "minis",
                CategoryName = "Minis",
                IconEmoji = "🧴",
                Badge = "TRAVEL SIZE",
                Description = "Discover travel-friendly beauty miniatures, pocket lipsticks, TSA-approved skincare, and trial-size discovery kits.",
                HeroBanner = new BeautyCategoryHeroDto
                {
                    IsEnabled = true,
                    BadgeText = "POCKET-FRIENDLY & TSA APPROVED",
                    Title = "Beauty Minis & Travel Essentials",
                    TitleHighlight = "LITTLE LUXURIES • BIG RESULTS",
                    Subtitle = "Never travel without your holy grail routines. Sized to fit dorm lockers, backpacks, and carry-on luggage perfectly.",
                    ImageUrl = "/images/beauty-hub/categories/hero-minis.webp",
                    IsUploadingSoon = true,
                    ThemeColor = "#8b5cf6",
                    BannerHeight = 340
                },
                Subcategories = new List<BeautySubcategoryCardDto>
                {
                    new() { SubcategorySlug = "mini-lips", SubcategoryName = "Mini Lipsticks & Tints", IconEmoji = "💄", Description = "Pocket Lipsticks (1.5g), Travel Liquid Mattes & Tinted Lip Butters", ItemCountText = "16+ Products", TargetUrl = "/category/beauty/minis/mini-lips", IsUploadingSoon = true, DisplayOrder = 1 },
                    new() { SubcategorySlug = "mini-skincare", SubcategoryName = "Travel-Size Skincare", IconEmoji = "🧴", Description = "Mini Cleansers 30ml, Trial Serums 10ml, Pocket Sunscreens & Creams", ItemCountText = "22+ Products", TargetUrl = "/category/beauty/minis/mini-skincare", IsUploadingSoon = true, DisplayOrder = 2 },
                    new() { SubcategorySlug = "mini-fragrances", SubcategoryName = "Mini Fragrances & Roll-ons", IconEmoji = "🌸", Description = "10ml Pocket Sprays, Travel Discovery Vials & Solid Balms", ItemCountText = "18+ Products", TargetUrl = "/category/beauty/minis/mini-fragrances", IsUploadingSoon = true, DisplayOrder = 3 },
                    new() { SubcategorySlug = "mini-haircare", SubcategoryName = "Mini Haircare Essentials", IconEmoji = "💇‍♀️", Description = "50ml Shampoos & Masks, Travel Argan Oils & Dry Shampoo Sprays", ItemCountText = "14+ Products", TargetUrl = "/category/beauty/minis/mini-haircare", IsUploadingSoon = true, DisplayOrder = 4 },
                    new() { SubcategorySlug = "trial-kits", SubcategoryName = "Trial Kits & Pouches", IconEmoji = "👝", Description = "5-Piece Complete Routine Pouches, Weekend Getaway Kits & Minis", ItemCountText = "15+ Products", TargetUrl = "/category/beauty/minis/trial-kits", IsUploadingSoon = true, DisplayOrder = 5 }
                }
            }
        };
    }

    /// <summary>
    /// Creates the complete Subcategory Pages with rich product catalogs, left-sidebar filter items, and hero banners.
    /// </summary>
    public static List<BeautySubcategoryPageDto> CreateDefaultSubcategoryPages()
    {
        var pages = new List<BeautySubcategoryPageDto>();

        // 1. MAKEUP SUBCATEGORIES
        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "makeup",
            CategoryName = "Makeup",
            SubcategorySlug = "lips",
            SubcategoryName = "Lips",
            IconEmoji = "💋",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "POUT PERFECTION",
                Title = "Lips — Lipsticks, Liquid Matte & Lip Glosses",
                TitleHighlight = "SMUDGE-PROOF & HYDRATING • 100% ORIGINAL",
                Subtitle = "Velvety matte bullets, high-shine plumping glosses, hydrating tinted balms, and waterproof precision liners.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-lips.webp",
                IsUploadingSoon = true,
                ThemeColor = "#be185d",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Lips",
                "Lipsticks (Matte & Satin)",
                "Liquid Matte Lipsticks",
                "High-Shine Lip Glosses",
                "Waterproof Lip Liners",
                "Hydrating Lip Balms & Tints"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-lip-1", Name = "SuperStay Matte Ink Liquid Lipstick (5ml)", Brand = "Maybelline New York", CategorySlug = "makeup", SubcategorySlug = "lips", SubcategoryTag = "Liquid Matte Lipsticks", Price = 489, OriginalPrice = 699, Rating = 4.9m, ReviewsCount = 1420, ImageUrl = "/images/beauty-hub/products/maybelline-matte-ink.webp", IsUploadingSoon = true, Badge = "Bestseller", Formulation = "Liquid", Finish = "Matte", SkinType = "All Skin Types", Preference = "Long-Lasting, Transfer-Proof" },
                new() { Id = "bprod-lip-2", Name = "Forever Matte Liquid Lip Colour with Vitamin E", Brand = "Lakme", CategorySlug = "makeup", SubcategorySlug = "lips", SubcategoryTag = "Liquid Matte Lipsticks", Price = 275, OriginalPrice = 399, Rating = 4.7m, ReviewsCount = 980, ImageUrl = "/images/beauty-hub/products/lakme-forever-matte.webp", IsUploadingSoon = true, Badge = "Campus Pick", Formulation = "Liquid", Finish = "Matte", SkinType = "All Skin Types", Preference = "Lightweight, Non-Drying" },
                new() { Id = "bprod-lip-3", Name = "Retro Matte Lipstick - Ruby Woo (3g)", Brand = "M.A.C", CategorySlug = "makeup", SubcategorySlug = "lips", SubcategoryTag = "Lipsticks (Matte & Satin)", Price = 1950, OriginalPrice = 2150, Rating = 5.0m, ReviewsCount = 3120, ImageUrl = "/images/beauty-hub/products/mac-ruby-woo.webp", IsUploadingSoon = true, Badge = "Luxury Icon", Formulation = "Bullet", Finish = "Matte", SkinType = "All Skin Types", Preference = "Iconic Shade, 8HR Wear" },
                new() { Id = "bprod-lip-4", Name = "Ultra Matte Luxe Bullet Lipstick with Jojoba Oil", Brand = "Nykaa Cosmetics", CategorySlug = "makeup", SubcategorySlug = "lips", SubcategoryTag = "Lipsticks (Matte & Satin)", Price = 349, OriginalPrice = 499, Rating = 4.6m, ReviewsCount = 740, ImageUrl = "/images/beauty-hub/products/nykaa-ultra-matte.webp", IsUploadingSoon = true, Badge = "Nykaa Choice", Formulation = "Bullet", Finish = "Matte", SkinType = "All Skin Types", Preference = "Enriched with Vitamin E" },
                new() { Id = "bprod-lip-5", Name = "Glow Gloss High-Shine Plumping Lip Glass (6ml)", Brand = "Swiss Beauty", CategorySlug = "makeup", SubcategorySlug = "lips", SubcategoryTag = "High-Shine Lip Glosses", Price = 229, OriginalPrice = 349, Rating = 4.7m, ReviewsCount = 560, ImageUrl = "/images/beauty-hub/products/swiss-beauty-gloss.webp", IsUploadingSoon = true, Badge = "Viral Trend", Formulation = "Gloss", Finish = "Glossy", SkinType = "All Skin Types", Preference = "Hydrating, Plumping" },
                new() { Id = "bprod-lip-6", Name = "Definer Precision Retractable Lip Liner Pencil", Brand = "Colorbar", CategorySlug = "makeup", SubcategorySlug = "lips", SubcategoryTag = "Waterproof Lip Liners", Price = 399, OriginalPrice = 550, Rating = 4.8m, ReviewsCount = 430, ImageUrl = "/images/beauty-hub/products/colorbar-lip-liner.webp", IsUploadingSoon = true, Badge = "Waterproof", Formulation = "Crayon", Finish = "Matte", SkinType = "All Skin Types", Preference = "Smudge-Proof, 12HR Wear" },
                new() { Id = "bprod-lip-7", Name = "Tinted Lip Butter with SPF 20 & Murumuru Butter", Brand = "Plum", CategorySlug = "makeup", SubcategorySlug = "lips", SubcategoryTag = "Hydrating Lip Balms & Tints", Price = 199, OriginalPrice = 295, Rating = 4.8m, ReviewsCount = 890, ImageUrl = "/images/beauty-hub/products/plum-lip-butter.webp", IsUploadingSoon = true, Badge = "100% Vegan", Formulation = "Balm", Finish = "Dewy", SkinType = "Dry / Normal", Preference = "Cruelty-Free, SPF 20" },
                new() { Id = "bprod-lip-8", Name = "Smudge Me Not Liquid Matte Mini Combo (Set of 3)", Brand = "SUGAR Cosmetics", CategorySlug = "makeup", SubcategorySlug = "lips", SubcategoryTag = "Liquid Matte Lipsticks", Price = 599, OriginalPrice = 799, Rating = 4.7m, ReviewsCount = 670, ImageUrl = "/images/beauty-hub/products/sugar-matte-mini.webp", IsUploadingSoon = true, Badge = "Value Combo", Formulation = "Liquid", Finish = "Matte", SkinType = "All Skin Types", Preference = "One-Coat Wonder" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "makeup",
            CategoryName = "Makeup",
            SubcategorySlug = "face",
            SubcategoryName = "Face",
            IconEmoji = "✨",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "FLAWLESS COMPLEXION",
                Title = "Face — Foundations, Concealers & Compacts",
                TitleHighlight = "PORELESS & WEIGHTLESS • 24HR WEAR",
                Subtitle = "Create a radiant base with oil-control foundations, creaseless concealers, blurring primers, and luminous blushes.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-face.webp",
                IsUploadingSoon = true,
                ThemeColor = "#e11d48",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Face",
                "Liquid Foundations",
                "Full Coverage Concealers",
                "Mattifying Compact Powders",
                "Hydrating Pore Blurring Primers",
                "Blush & Radiant Highlighters"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-face-1", Name = "Fit Me Matte + Poreless Liquid Foundation (30ml)", Brand = "Maybelline New York", CategorySlug = "makeup", SubcategorySlug = "face", SubcategoryTag = "Liquid Foundations", Price = 499, OriginalPrice = 649, Rating = 4.9m, ReviewsCount = 2890, ImageUrl = "/images/beauty-hub/products/maybelline-fit-me.webp", IsUploadingSoon = true, Badge = "Top Seller", Formulation = "Liquid", Finish = "Matte", SkinType = "Normal to Oily", Preference = "Dermatologist Tested, SPF 22" },
                new() { Id = "bprod-face-2", Name = "Infallible 24H Fresh Wear Foundation (30ml)", Brand = "L'Oréal Paris", CategorySlug = "makeup", SubcategorySlug = "face", SubcategoryTag = "Liquid Foundations", Price = 975, OriginalPrice = 1299, Rating = 4.8m, ReviewsCount = 1320, ImageUrl = "/images/beauty-hub/products/loreal-infallible.webp", IsUploadingSoon = true, Badge = "Waterproof", Formulation = "Liquid", Finish = "Natural Satin", SkinType = "All Skin Types", Preference = "Sweat-Proof, Breathable" },
                new() { Id = "bprod-face-3", Name = "Instant Age Rewind Multi-Use Eraser Concealer", Brand = "Maybelline New York", CategorySlug = "makeup", SubcategorySlug = "face", SubcategoryTag = "Full Coverage Concealers", Price = 599, OriginalPrice = 799, Rating = 4.9m, ReviewsCount = 2150, ImageUrl = "/images/beauty-hub/products/maybelline-age-rewind.webp", IsUploadingSoon = true, Badge = "Cult Favorite", Formulation = "Liquid", Finish = "Natural", SkinType = "All Skin Types", Preference = "Haloxyl Enriched, Dark Circles" },
                new() { Id = "bprod-face-4", Name = "9to5 Primer + Matte Powder Foundation Compact", Brand = "Lakme", CategorySlug = "makeup", SubcategorySlug = "face", SubcategoryTag = "Mattifying Compact Powders", Price = 385, OriginalPrice = 550, Rating = 4.7m, ReviewsCount = 890, ImageUrl = "/images/beauty-hub/products/lakme-compact.webp", IsUploadingSoon = true, Badge = "Campus Essential", Formulation = "Powder", Finish = "Matte", SkinType = "Oily / Normal", Preference = "Built-in Primer, Long Wear" },
                new() { Id = "bprod-face-5", Name = "Prep & Prime Pore Blurring Grip Gel Primer (30ml)", Brand = "Nykaa Cosmetics", CategorySlug = "makeup", SubcategorySlug = "face", SubcategoryTag = "Hydrating Pore Blurring Primers", Price = 425, OriginalPrice = 599, Rating = 4.8m, ReviewsCount = 640, ImageUrl = "/images/beauty-hub/products/nykaa-primer.webp", IsUploadingSoon = true, Badge = "Smooth Base", Formulation = "Gel", Finish = "Velvet Matte", SkinType = "All Skin Types", Preference = "Pore Minimizing, Oil Free" },
                new() { Id = "bprod-face-6", Name = "Baked Blusher & Highlighter Duo Palette (8g)", Brand = "Swiss Beauty", CategorySlug = "makeup", SubcategorySlug = "face", SubcategoryTag = "Blush & Radiant Highlighters", Price = 319, OriginalPrice = 449, Rating = 4.8m, ReviewsCount = 780, ImageUrl = "/images/beauty-hub/products/swiss-beauty-blush.webp", IsUploadingSoon = true, Badge = "Luminous Glow", Formulation = "Baked Powder", Finish = "Shimmer", SkinType = "All Skin Types", Preference = "Silk Micronized Pearls" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "makeup",
            CategoryName = "Makeup",
            SubcategorySlug = "eyes",
            SubcategoryName = "Eyes",
            IconEmoji = "👁️",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "ALL EYES ON YOU",
                Title = "Eyes — Kajals, Eyeliners & Mascaras",
                TitleHighlight = "WATERPROOF & SMUDGE-PROOF • INTENSE PIGMENT",
                Subtitle = "Create hypnotic cat eyes, sky-high dramatic lashes, and day-to-night smokey eyes with ophthalmologist-tested makeup.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-eyes.webp",
                IsUploadingSoon = true,
                ThemeColor = "#4338ca",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Eyes",
                "24HR Deep Black Kajals",
                "Precision Liquid & Felt-Tip Eyeliners",
                "Volumizing & Lengthening Mascaras",
                "Highly Pigmented Eyeshadow Palettes"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-eye-1", Name = "Colossal 24HR Smudge-Proof Kajal with Aloe Vera", Brand = "Maybelline New York", CategorySlug = "makeup", SubcategorySlug = "eyes", SubcategoryTag = "24HR Deep Black Kajals", Price = 169, OriginalPrice = 249, Rating = 4.9m, ReviewsCount = 3890, ImageUrl = "/images/beauty-hub/products/maybelline-colossal-kajal.webp", IsUploadingSoon = true, Badge = "India's #1 Kajal", Formulation = "Pencil", Finish = "Matte Black", SkinType = "Sensitive Eyes", Preference = "Contact Lens Safe, 24HR" },
                new() { Id = "bprod-eye-2", Name = "Eyeconic Liquid Black Liner - Waterproof (4.5ml)", Brand = "Lakme", CategorySlug = "makeup", SubcategorySlug = "eyes", SubcategoryTag = "Precision Liquid & Felt-Tip Eyeliners", Price = 239, OriginalPrice = 325, Rating = 4.8m, ReviewsCount = 1840, ImageUrl = "/images/beauty-hub/products/lakme-eyeconic-liner.webp", IsUploadingSoon = true, Badge = "Waterproof", Formulation = "Liquid", Finish = "Glossy Black", SkinType = "All Skin Types", Preference = "Fine Precision Tip" },
                new() { Id = "bprod-eye-3", Name = "Lash Sensational Sky High Waterproof Mascara", Brand = "Maybelline New York", CategorySlug = "makeup", SubcategorySlug = "eyes", SubcategoryTag = "Volumizing & Lengthening Mascaras", Price = 699, OriginalPrice = 899, Rating = 5.0m, ReviewsCount = 2950, ImageUrl = "/images/beauty-hub/products/maybelline-sky-high.webp", IsUploadingSoon = true, Badge = "Viral Bestseller", Formulation = "Waterproof Gel", Finish = "Intense Volume", SkinType = "All Skin Types", Preference = "Bamboo Extract Fiber" },
                new() { Id = "bprod-eye-4", Name = "Ultimate 9-in-1 Eyeshadow Palette (Warm Nudes)", Brand = "Swiss Beauty", CategorySlug = "makeup", SubcategorySlug = "eyes", SubcategoryTag = "Highly Pigmented Eyeshadow Palettes", Price = 269, OriginalPrice = 399, Rating = 4.7m, ReviewsCount = 940, ImageUrl = "/images/beauty-hub/products/swiss-beauty-palette.webp", IsUploadingSoon = true, Badge = "Festive Must Have", Formulation = "Pressed Powder", Finish = "Matte & Shimmer", SkinType = "All Skin Types", Preference = "Zero Fall Out, Blendable" }
            }
        });

        // 2. SKIN SUBCATEGORIES
        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "skin",
            CategoryName = "Skin",
            SubcategorySlug = "serums-treatments",
            SubcategoryName = "Serums & Treatments",
            IconEmoji = "🧪",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "CLINICAL GRADE ACTIVES",
                Title = "Serums & Treatments — Target & Transform",
                TitleHighlight = "HIGH CONCENTRATION • REAL RESULTS",
                Subtitle = "Brighten acne spots with 10% Niacinamide, fade pigmentation with 16% Vitamin C, and quench dehydrated skin with Multi-Molecular Hyaluronic Acid.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-serums.webp",
                IsUploadingSoon = true,
                ThemeColor = "#0284c7",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Serums",
                "Niacinamide (Blemish & Oil Control)",
                "Vitamin C (Brightening & Glow)",
                "Salicylic Acid (Blackheads & Acne)",
                "Hyaluronic Acid (Hydration & Plumping)",
                "Retinol (Anti-Aging & Texture)"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-serum-1", Name = "10% Niacinamide Face Serum with Zinc PCA (30ml)", Brand = "Minimalist", CategorySlug = "skin", SubcategorySlug = "serums-treatments", SubcategoryTag = "Niacinamide (Blemish & Oil Control)", Price = 569, OriginalPrice = 599, Rating = 4.9m, ReviewsCount = 3450, ImageUrl = "/images/beauty-hub/products/minimalist-niacinamide.webp", IsUploadingSoon = true, Badge = "Cult Cult Bestseller", Formulation = "Serum", Finish = "Lightweight", SkinType = "Oily & Acne Prone", Preference = "Fragrance Free, Clean" },
                new() { Id = "bprod-serum-2", Name = "10% Vitamin C Face Serum with 5% Niacinamide", Brand = "The Derma Co.", CategorySlug = "skin", SubcategorySlug = "serums-treatments", SubcategoryTag = "Vitamin C (Brightening & Glow)", Price = 549, OriginalPrice = 649, Rating = 4.8m, ReviewsCount = 2100, ImageUrl = "/images/beauty-hub/products/derma-vit-c.webp", IsUploadingSoon = true, Badge = "Dermatologist Pick", Formulation = "Serum", Finish = "Absorbent", SkinType = "All Skin Types", Preference = "Brightens in 2 Weeks" },
                new() { Id = "bprod-serum-3", Name = "Hyaluronic Acid 2% + B5 Hydration Serum (30ml)", Brand = "The Ordinary", CategorySlug = "skin", SubcategorySlug = "serums-treatments", SubcategoryTag = "Hyaluronic Acid (Hydration & Plumping)", Price = 750, OriginalPrice = 850, Rating = 4.9m, ReviewsCount = 4120, ImageUrl = "/images/beauty-hub/products/the-ordinary-hyaluronic.webp", IsUploadingSoon = true, Badge = "Global Favorite", Formulation = "Gel Serum", Finish = "Plump & Dewy", SkinType = "Dry & Dehydrated", Preference = "Multi-Depth Hydration" },
                new() { Id = "bprod-serum-4", Name = "2% Salicylic Acid Blemish Clarifying Serum (30ml)", Brand = "Minimalist", CategorySlug = "skin", SubcategorySlug = "serums-treatments", SubcategoryTag = "Salicylic Acid (Blackheads & Acne)", Price = 520, OriginalPrice = 549, Rating = 4.8m, ReviewsCount = 1870, ImageUrl = "/images/beauty-hub/products/minimalist-salicylic.webp", IsUploadingSoon = true, Badge = "Pore Clearing", Formulation = "Liquid Serum", Finish = "Fast Absorbing", SkinType = "Acne & Blackhead Prone", Preference = "Clears Pores, Reduces Redness" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "skin",
            CategoryName = "Skin",
            SubcategorySlug = "cleansers",
            SubcategoryName = "Cleansers & Face Wash",
            IconEmoji = "🧼",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "DEEP HYDRATING CLEANSE",
                Title = "Cleansers — Gentle, Barrier-Safe Washes",
                TitleHighlight = "pH 5.5 BALANCED • SULFATE FREE",
                Subtitle = "Wipe away dirt, stubborn city pollution, and excess sebum without stripping your natural moisture mantle.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-cleansers.webp",
                IsUploadingSoon = true,
                ThemeColor = "#0891b2",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Cleansers",
                "Gentle Skin Cleansers",
                "Foaming Face Washes",
                "Acne Defense Cleansers"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-clean-1", Name = "Gentle Skin Cleanser with Niacinamide & B5 (250ml)", Brand = "Cetaphil", CategorySlug = "skin", SubcategorySlug = "cleansers", SubcategoryTag = "Gentle Skin Cleansers", Price = 599, OriginalPrice = 699, Rating = 5.0m, ReviewsCount = 5200, ImageUrl = "/images/beauty-hub/products/cetaphil-cleanser.webp", IsUploadingSoon = true, Badge = "Derm Recommended", Formulation = "Lotion Wash", Finish = "Hydrated", SkinType = "Sensitive, Dry to Normal", Preference = "Soap Free, Hypoallergenic" },
                new() { Id = "bprod-clean-2", Name = "Green Tea Pore Cleansing Face Wash (120ml)", Brand = "Plum", CategorySlug = "skin", SubcategorySlug = "cleansers", SubcategoryTag = "Foaming Face Washes", Price = 299, OriginalPrice = 375, Rating = 4.8m, ReviewsCount = 2140, ImageUrl = "/images/beauty-hub/products/plum-green-tea-wash.webp", IsUploadingSoon = true, Badge = "100% Vegan", Formulation = "Gel with Beads", Finish = "Refreshed", SkinType = "Oily & Combination", Preference = "Glycolic Acid Enriched" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "skin",
            CategoryName = "Skin",
            SubcategorySlug = "sunscreen",
            SubcategoryName = "Sunscreen & Sun Care",
            IconEmoji = "☀️",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "BROAD SPECTRUM UV SHIELD",
                Title = "Sunscreen — SPF 50+ PA++++ Protection",
                TitleHighlight = "ZERO WHITE CAST • ULTRA LIGHTWEIGHT",
                Subtitle = "Protect against UVA/UVB rays and digital blue light with water-light aqua gels and touch-free sunscreen sticks.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-sunscreen.webp",
                IsUploadingSoon = true,
                ThemeColor = "#ea580c",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Sunscreens",
                "Ultra Matte Aqua Gels",
                "Sunscreen Sticks (Touch-Up)",
                "Mineral Sunscreens"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-sun-1", Name = "1% Hyaluronic Sunscreen Aqua Gel SPF 50 PA++++", Brand = "The Derma Co.", CategorySlug = "skin", SubcategorySlug = "sunscreen", SubcategoryTag = "Ultra Matte Aqua Gels", Price = 449, OriginalPrice = 499, Rating = 4.9m, ReviewsCount = 4120, ImageUrl = "/images/beauty-hub/products/derma-aqua-gel.webp", IsUploadingSoon = true, Badge = "Campus Favorite", Formulation = "Water Gel", Finish = "Matte Invisible", SkinType = "All Skin Types", Preference = "Zero White Cast, Non-Greasy" },
                new() { Id = "bprod-sun-2", Name = "SPF 50 Light Fluid Sunscreen with Multi-Vitamins", Brand = "Minimalist", CategorySlug = "skin", SubcategorySlug = "sunscreen", SubcategoryTag = "Ultra Matte Aqua Gels", Price = 380, OriginalPrice = 399, Rating = 4.8m, ReviewsCount = 2890, ImageUrl = "/images/beauty-hub/products/minimalist-sunscreen.webp", IsUploadingSoon = true, Badge = "Clinically Tested", Formulation = "Fluid", Finish = "Natural", SkinType = "All Skin Types", Preference = "Uvinul A Plus & T 150" }
            }
        });

        // 3. FRAGRANCE SUBCATEGORIES
        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "fragrance",
            CategoryName = "Fragrance",
            SubcategorySlug = "womens-perfume",
            SubcategoryName = "Women's Perfumes",
            IconEmoji = "💐",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "SIGNATURE PERFUMERY",
                Title = "Women's Perfumes — Eau De Parfum",
                TitleHighlight = "LONG LASTING • SENSUAL BOUQUETS",
                Subtitle = "Indulge in sweet vanilla orchids, sparkling bergamot, fresh jasmine sambac, and comforting warm amber dry-downs.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-perfume-women.webp",
                IsUploadingSoon = true,
                ThemeColor = "#a855f7",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Women's Scents",
                "Eau De Parfum (Long-Wear)",
                "Floral & Sweet Notes",
                "Discovery Sets & Coffrets"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-frag-1", Name = "Glam Woman Luxury Eau De Parfum (100ml)", Brand = "Bella Vita Organic", CategorySlug = "fragrance", SubcategorySlug = "womens-perfume", SubcategoryTag = "Eau De Parfum (Long-Wear)", Price = 599, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 1890, ImageUrl = "/images/beauty-hub/products/bellavita-glam.webp", IsUploadingSoon = true, Badge = "Top Pick", Formulation = "Spray", Finish = "EDP", SkinType = "All Types", Preference = "Jasmine, Citrus & Amber" },
                new() { Id = "bprod-frag-2", Name = "Cool Water Woman Eau De Toilette (100ml)", Brand = "DAVIDOFF", CategorySlug = "fragrance", SubcategorySlug = "womens-perfume", SubcategoryTag = "Eau De Parfum (Long-Wear)", Price = 3850, OriginalPrice = 5200, Rating = 4.9m, ReviewsCount = 1240, ImageUrl = "/images/beauty-hub/products/davidoff-cool-water-woman.webp", IsUploadingSoon = true, Badge = "Global Luxe", Formulation = "Spray", Finish = "EDT", SkinType = "All Types", Preference = "Aquatic Floral Breeze" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "fragrance",
            CategoryName = "Fragrance",
            SubcategorySlug = "mens-cologne",
            SubcategoryName = "Men's Colognes",
            IconEmoji = "🌲",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "MASCULINE ACCORDS",
                Title = "Men's Colognes & Eau De Parfum",
                TitleHighlight = "SMOKY WOODS & AQUATIC SPORT • ALL DAY SCENT",
                Subtitle = "Make an unmistakable impression with bold cedarwood, Italian bergamot, vetiver, and smoky leather notes.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-perfume-men.webp",
                IsUploadingSoon = true,
                ThemeColor = "#475569",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Men's Colognes",
                "Eau De Parfum (Woody & Amber)",
                "Fresh Aquatic & Citrus Colognes"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-frag-3", Name = "Classic Black Eau De Toilette (100ml)", Brand = "Jaguar", CategorySlug = "fragrance", SubcategorySlug = "mens-cologne", SubcategoryTag = "Eau De Parfum (Woody & Amber)", Price = 2899, OriginalPrice = 4200, Rating = 4.9m, ReviewsCount = 2100, ImageUrl = "/images/beauty-hub/products/jaguar-classic-black.webp", IsUploadingSoon = true, Badge = "Premium Bestseller", Formulation = "Spray", Finish = "EDT", SkinType = "All Types", Preference = "Woody Oriental Accords" },
                new() { Id = "bprod-frag-4", Name = "CEO Man Luxury Eau De Parfum (100ml)", Brand = "Bella Vita Organic", CategorySlug = "fragrance", SubcategorySlug = "mens-cologne", SubcategoryTag = "Eau De Parfum (Woody & Amber)", Price = 599, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 3450, ImageUrl = "/images/beauty-hub/products/bellavita-ceo.webp", IsUploadingSoon = true, Badge = "Viral Favorite", Formulation = "Spray", Finish = "EDP", SkinType = "All Types", Preference = "Lemon, Tonka & Amber" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "fragrance",
            CategoryName = "Fragrance",
            SubcategorySlug = "edp",
            SubcategoryName = "Eau De Parfum (EDP)",
            IconEmoji = "💐",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "HAUTE LUXE EDP",
                Title = "Eau De Parfum — Intense Long-Lasting Sillage",
                TitleHighlight = "20% PERFUME OIL • 12HR+ LONGEVITY",
                Subtitle = "Rich oriental florals, gourmand Madagascar vanilla, and exotic amber notes that linger through campus evenings.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-edp.webp",
                IsUploadingSoon = true,
                ThemeColor = "#9333ea",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All EDP Perfumes",
                "Floral & Sweet Gourmand",
                "Warm Amber & Woody",
                "Fresh Citrus Accords"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-edp-1", Name = "Glam Woman Luxury Eau De Parfum (100ml)", Brand = "Bella Vita Organic", CategorySlug = "fragrance", SubcategorySlug = "edp", SubcategoryTag = "Floral & Sweet Gourmand", Price = 599, OriginalPrice = 999, Rating = 4.8m, ReviewsCount = 2890, ImageUrl = "/images/beauty-hub/products/bellavita-glam.webp", IsUploadingSoon = true, Badge = "Top Pick", Formulation = "Spray", Finish = "EDP", SkinType = "All Types", Preference = "Jasmine, Citrus & Amber" },
                new() { Id = "bprod-edp-2", Name = "White Oud Unisex Luxury Eau De Parfum (100ml)", Brand = "Bella Vita Luxury", CategorySlug = "fragrance", SubcategorySlug = "edp", SubcategoryTag = "Warm Amber & Woody", Price = 649, OriginalPrice = 1099, Rating = 4.9m, ReviewsCount = 3120, ImageUrl = "/images/beauty-hub/products/white-oud.webp", IsUploadingSoon = true, Badge = "Bestseller", Formulation = "Spray", Finish = "EDP", SkinType = "All Types", Preference = "Rare Amber & Spices" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "fragrance",
            CategoryName = "Fragrance",
            SubcategorySlug = "edt",
            SubcategoryName = "Eau De Toilette (EDT)",
            IconEmoji = "🌲",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "FRESH DAILY SCENTS",
                Title = "Eau De Toilette — Crisp & Invigorating",
                TitleHighlight = "LIGHTWEIGHT & AIRY • PERFECT FOR DAYWEAR",
                Subtitle = "Energetic sparkling bergamot, sea salt breezes, and green cedar notes designed for classrooms and daily wear.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-edt.webp",
                IsUploadingSoon = true,
                ThemeColor = "#9333ea",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All EDT Scents",
                "Aquatic & Marine EDTs",
                "Fresh Citrus Sport",
                "Clean Green Woods"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-edt-1", Name = "Cool Water Man Eau De Toilette (125ml)", Brand = "DAVIDOFF", CategorySlug = "fragrance", SubcategorySlug = "edt", SubcategoryTag = "Aquatic & Marine EDTs", Price = 4250, OriginalPrice = 5800, Rating = 4.9m, ReviewsCount = 2450, ImageUrl = "/images/beauty-hub/products/davidoff-cool-water.webp", IsUploadingSoon = true, Badge = "Global Luxe", Formulation = "Spray", Finish = "EDT", SkinType = "All Types", Preference = "Marine Freshness" },
                new() { Id = "bprod-edt-2", Name = "Classic Blue Fresh Sport Eau De Toilette (100ml)", Brand = "Jaguar", CategorySlug = "fragrance", SubcategorySlug = "edt", SubcategoryTag = "Fresh Citrus Sport", Price = 2650, OriginalPrice = 3800, Rating = 4.8m, ReviewsCount = 1420, ImageUrl = "/images/beauty-hub/products/jaguar-blue.webp", IsUploadingSoon = true, Badge = "Sport Favorite", Formulation = "Spray", Finish = "EDT", SkinType = "All Types", Preference = "Citrus & Lavender" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "fragrance",
            CategoryName = "Fragrance",
            SubcategorySlug = "body-mists",
            SubcategoryName = "Body Mists & Sprays",
            IconEmoji = "✨",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "ALL-DAY MISTS",
                Title = "Body Mists & Sprays — Effortless Refreshment",
                TitleHighlight = "LIGHTWEIGHT • UNDER ₹399",
                Subtitle = "Spritz on refreshing floral and vanilla mists straight after showering or before heading to lectures.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-body-mists.webp",
                IsUploadingSoon = true,
                ThemeColor = "#9333ea",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Body Mists",
                "Vanilla & Warm Gourmand",
                "Floral Breeze Sprays",
                "Pocket Deodorant Mists"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-mist-1", Name = "Vanilla Vibes Fine Fragrance Body Mist (150ml)", Brand = "Plum", CategorySlug = "fragrance", SubcategorySlug = "body-mists", SubcategoryTag = "Vanilla & Warm Gourmand", Price = 349, OriginalPrice = 425, Rating = 4.9m, ReviewsCount = 3890, ImageUrl = "/images/beauty-hub/products/plum-vanilla-mist.webp", IsUploadingSoon = true, Badge = "Viral Hit", Formulation = "Fine Mist", Finish = "Sweet Vanilla", SkinType = "All Types", Preference = "100% Vegan, Aloe Enriched" },
                new() { Id = "bprod-mist-2", Name = "Hawaiian Rumba Shimmer Body Mist (150ml)", Brand = "Plum", CategorySlug = "fragrance", SubcategorySlug = "body-mists", SubcategoryTag = "Floral Breeze Sprays", Price = 375, OriginalPrice = 475, Rating = 4.8m, ReviewsCount = 2100, ImageUrl = "/images/beauty-hub/products/plum-hawaiian.webp", IsUploadingSoon = true, Badge = "Party Shimmer", Formulation = "Shimmer Mist", Finish = "Subtle Glow", SkinType = "All Types", Preference = "Tropical Beach Accords" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "fragrance",
            CategoryName = "Fragrance",
            SubcategorySlug = "perfume-gift-sets",
            SubcategoryName = "Perfume Gift Sets",
            IconEmoji = "🎁",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "DISCOVERY COFFRETS",
                Title = "Perfume Gift Sets — Curated Miniature Wardrobes",
                TitleHighlight = "LUXURY BOXES • IDEAL FESTIVE GIFTS",
                Subtitle = "Unbox multi-vial discovery sets featuring contrasting moods, notes, and pocket-friendly atomizers.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-perfume-gifts.webp",
                IsUploadingSoon = true,
                ThemeColor = "#9333ea",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Perfume Gift Sets",
                "Unisex Discovery Boxes",
                "Couples Luxury Hamper",
                "Mini Travel Sets"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-pbox-1", Name = "Luxury Perfume Gift Set for Men (4 x 20ml)", Brand = "Bella Vita Luxury", CategorySlug = "fragrance", SubcategorySlug = "perfume-gift-sets", SubcategoryTag = "Unisex Discovery Boxes", Price = 549, OriginalPrice = 849, Rating = 4.8m, ReviewsCount = 2840, ImageUrl = "/images/beauty-hub/products/bellavita-men-gift.webp", IsUploadingSoon = true, Badge = "Bestselling Box", Formulation = "Box of 4", Finish = "EDP", SkinType = "All Types", Preference = "Klub, CEO, OUD & Skai" },
                new() { Id = "bprod-pbox-2", Name = "Luxury Perfume Gift Set for Women (4 x 20ml)", Brand = "Bella Vita Luxury", CategorySlug = "fragrance", SubcategorySlug = "perfume-gift-sets", SubcategoryTag = "Unisex Discovery Boxes", Price = 549, OriginalPrice = 849, Rating = 4.8m, ReviewsCount = 3120, ImageUrl = "/images/beauty-hub/products/bellavita-women-gift.webp", IsUploadingSoon = true, Badge = "Top Gift Pick", Formulation = "Box of 4", Finish = "EDP", SkinType = "All Types", Preference = "Glam, Rose, Date & Senorita" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "fragrance",
            CategoryName = "Fragrance",
            SubcategorySlug = "luxury-perfumes",
            SubcategoryName = "Luxury & Niche Perfumes",
            IconEmoji = "👑",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "PRESTIGE PERFUMERY",
                Title = "Luxury & Niche Perfumes — Rare French & Oriental Blends",
                TitleHighlight = "ARTISAN CRAFTSMANSHIP • EXCLUSIVE DISTRIBUTION",
                Subtitle = "Experience masterfully compounded pure absolutes, rare smokey ouds, and golden amber resins.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-luxury-perfumes.webp",
                IsUploadingSoon = true,
                ThemeColor = "#9333ea",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Luxury Perfumes",
                "French Designer Scents",
                "Rare Arabian Ouds",
                "Niche Artisanal Blends"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-lux-1", Name = "Eros Eau De Parfum for Men (100ml)", Brand = "Versace", CategorySlug = "fragrance", SubcategorySlug = "luxury-perfumes", SubcategoryTag = "French Designer Scents", Price = 7450, OriginalPrice = 8900, Rating = 5.0m, ReviewsCount = 1890, ImageUrl = "/images/beauty-hub/products/versace-eros.webp", IsUploadingSoon = true, Badge = "Prestige Luxe", Formulation = "Spray", Finish = "EDP", SkinType = "All Types", Preference = "Italian Mint, Vanilla & Cedar" },
                new() { Id = "bprod-lux-2", Name = "Bright Crystal Absolu Eau De Parfum (90ml)", Brand = "Versace", CategorySlug = "fragrance", SubcategorySlug = "luxury-perfumes", SubcategoryTag = "French Designer Scents", Price = 8100, OriginalPrice = 9600, Rating = 5.0m, ReviewsCount = 1420, ImageUrl = "/images/beauty-hub/products/versace-crystal.webp", IsUploadingSoon = true, Badge = "Iconic Prestige", Formulation = "Spray", Finish = "EDP", SkinType = "All Types", Preference = "Pomegranate, Peony & Amber" }
            }
        });

        // 4. HAIR SUBCATEGORIES
        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "hair",
            CategoryName = "Hair",
            SubcategorySlug = "shampoo",
            SubcategoryName = "Shampoo",
            IconEmoji = "🫧",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "SALON HAIR CLEANSE",
                Title = "Shampoos — Anti-Hairfall & Sulfate-Free",
                TitleHighlight = "KERATIN & BIOTIN • GENTLE CARE",
                Subtitle = "Wash away dirt, sweat, and campus grime without stripping natural scalp moisture.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-shampoo.webp",
                IsUploadingSoon = true,
                ThemeColor = "#059669",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Shampoos",
                "Anti-Hairfall Shampoos",
                "Sulfate-Free Cleansers",
                "Dandruff Defense Washes"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-hair-1", Name = "Total Repair 5 Restoring Shampoo with Keratin (650ml)", Brand = "L'Oréal Paris", CategorySlug = "hair", SubcategorySlug = "shampoo", SubcategoryTag = "Anti-Hairfall Shampoos", Price = 499, OriginalPrice = 749, Rating = 4.8m, ReviewsCount = 3890, ImageUrl = "/images/beauty-hub/products/loreal-shampoo.webp", IsUploadingSoon = true, Badge = "Campus Favorite", Formulation = "Liquid", Finish = "Smooth Silk", SkinType = "Damaged / Dry", Preference = "Ceramide Restoring" },
                new() { Id = "bprod-hair-2", Name = "Rosemary Anti-Hairfall Shampoo with Biotin (250ml)", Brand = "Mamaearth", CategorySlug = "hair", SubcategorySlug = "shampoo", SubcategoryTag = "Sulfate-Free Cleansers", Price = 349, OriginalPrice = 399, Rating = 4.7m, ReviewsCount = 2100, ImageUrl = "/images/beauty-hub/products/mamaearth-rosemary.webp", IsUploadingSoon = true, Badge = "Toxin Free", Formulation = "Liquid", Finish = "Refreshed", SkinType = "All Scalp Types", Preference = "Sulfate & Paraben Free" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "hair",
            CategoryName = "Hair",
            SubcategorySlug = "conditioners",
            SubcategoryName = "Conditioners",
            IconEmoji = "🧴",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "DEEP HYDRATION & REPAIR",
                Title = "Conditioners — Keratin Smooth & Moisture Rich",
                TitleHighlight = "FRIZZ-FREE SHINE • WEIGHTLESS SILK",
                Subtitle = "Nourish dry locks with salon-grade conditioners, leave-ins, and protein-packed detangling formulas.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-conditioners.webp",
                IsUploadingSoon = true,
                ThemeColor = "#059669",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Conditioners",
                "Keratin Smooth Conditioners",
                "Deep Moisture Conditioners",
                "Weightless Leave-In Creams",
                "Color-Protect Conditioners"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-cond-1", Name = "Keratin Smooth Deep Moisture Conditioner with Argan Oil (300ml)", Brand = "TRESemmé", CategorySlug = "hair", SubcategorySlug = "conditioners", SubcategoryTag = "Keratin Smooth Conditioners", Price = 349, OriginalPrice = 499, Rating = 4.9m, ReviewsCount = 2840, ImageUrl = "/images/beauty-hub/products/tresemme-keratin.webp", IsUploadingSoon = true, Badge = "Bestseller", Formulation = "Rich Cream", Finish = "Gloss Silk", SkinType = "Frizzy / Dry Hair", Preference = "Salon Tested" },
                new() { Id = "bprod-cond-2", Name = "Total Repair 5 Restoring Conditioner with Ceramide (192ml)", Brand = "L'Oréal Paris", CategorySlug = "hair", SubcategorySlug = "conditioners", SubcategoryTag = "Deep Moisture Conditioners", Price = 225, OriginalPrice = 299, Rating = 4.8m, ReviewsCount = 1890, ImageUrl = "/images/beauty-hub/products/loreal-conditioner.webp", IsUploadingSoon = true, Badge = "Campus Favorite", Formulation = "Cream", Finish = "Soft Touch", SkinType = "Damaged Hair", Preference = "Ceramide-R" },
                new() { Id = "bprod-cond-3", Name = "Rosemary Anti-Hairfall Conditioner with Biotin (250ml)", Brand = "Mamaearth", CategorySlug = "hair", SubcategorySlug = "conditioners", SubcategoryTag = "Weightless Leave-In Creams", Price = 349, OriginalPrice = 399, Rating = 4.8m, ReviewsCount = 1420, ImageUrl = "/images/beauty-hub/products/mamaearth-cond.webp", IsUploadingSoon = true, Badge = "Toxin Free", Formulation = "Lotion", Finish = "Natural", SkinType = "All Hair Types", Preference = "Sulfate Free" },
                new() { Id = "bprod-cond-4", Name = "Intensive Repair Damage Therapy Conditioner (180ml)", Brand = "Dove", CategorySlug = "hair", SubcategorySlug = "conditioners", SubcategoryTag = "Deep Moisture Conditioners", Price = 199, OriginalPrice = 260, Rating = 4.7m, ReviewsCount = 2150, ImageUrl = "/images/beauty-hub/products/dove-cond.webp", IsUploadingSoon = true, Badge = "Budget Pick", Formulation = "Cream", Finish = "Hydrated", SkinType = "Dry / Rough", Preference = "Nutri-Keratin" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "hair",
            CategoryName = "Hair",
            SubcategorySlug = "hair-oils",
            SubcategoryName = "Hair Oils & Tonics",
            IconEmoji = "🌿",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "SCALP GROWTH & NOURISHMENT",
                Title = "Hair Oils & Scalp Tonics — Pure Herbal Elixirs",
                TitleHighlight = "ROSEMARY & REDENSYL • ROOT STRENGTH",
                Subtitle = "Revitalize dormant follicles with organic rosemary drops, cold-pressed argan oils, and ancient ayurvedic blends.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-hair-oils.webp",
                IsUploadingSoon = true,
                ThemeColor = "#059669",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Hair Oils",
                "Rosemary Scalp Oils",
                "Pure Moroccan Argan Serums",
                "Onion Growth Oils",
                "Cold-Pressed Almond & Coconut"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-oil-1", Name = "Rosemary Essential Oil for Scalp Growth (30ml)", Brand = "Soulflower", CategorySlug = "hair", SubcategorySlug = "hair-oils", SubcategoryTag = "Rosemary Scalp Oils", Price = 399, OriginalPrice = 500, Rating = 4.9m, ReviewsCount = 3890, ImageUrl = "/images/beauty-hub/products/soulflower-rosemary.webp", IsUploadingSoon = true, Badge = "Viral Bestseller", Formulation = "Pure Essential Oil", Finish = "Non-Sticky", SkinType = "All Scalp Types", Preference = "100% Pure & Natural" },
                new() { Id = "bprod-oil-2", Name = "Onion Hair Oil for Hair Regrowth with Redensyl (150ml)", Brand = "Mamaearth", CategorySlug = "hair", SubcategorySlug = "hair-oils", SubcategoryTag = "Onion Growth Oils", Price = 399, OriginalPrice = 499, Rating = 4.8m, ReviewsCount = 2950, ImageUrl = "/images/beauty-hub/products/mamaearth-onion-oil.webp", IsUploadingSoon = true, Badge = "Campus Pick", Formulation = "Oil Blend", Finish = "Lightweight", SkinType = "Thinning Hair", Preference = "Sulfur Rich" },
                new() { Id = "bprod-oil-3", Name = "Pure Moroccan Argan Treatment Hair Serum (100ml)", Brand = "Streax Professional", CategorySlug = "hair", SubcategorySlug = "hair-oils", SubcategoryTag = "Pure Moroccan Argan Serums", Price = 349, OriginalPrice = 450, Rating = 4.8m, ReviewsCount = 1840, ImageUrl = "/images/beauty-hub/products/streax-argan.webp", IsUploadingSoon = true, Badge = "Salon Icon", Formulation = "Oil Serum", Finish = "Gloss Shine", SkinType = "Dry / Dull Hair", Preference = "Vitamin E Enriched" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "hair",
            CategoryName = "Hair",
            SubcategorySlug = "hair-masks-serums",
            SubcategoryName = "Hair Masks & Serums",
            IconEmoji = "💆‍♀️",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "SALON HAIR SPA",
                Title = "Hair Masks & Spas — Deep Bond Repair",
                TitleHighlight = "INTENSIVE REPAIR • SALON RESULTS AT HOME",
                Subtitle = "Restore softness and heal heat damage with protein-infused hair butter masques and weightless smoothing serums.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-hair-masks.webp",
                IsUploadingSoon = true,
                ThemeColor = "#059669",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Hair Masks",
                "Bond Repair Masques",
                "Argan Hair Spa Creams",
                "Anti-Frizz Gloss Serums"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-mask-1", Name = "Damage Repair Keratin Hair Spa Masque (200g)", Brand = "Schwarzkopf Professional", CategorySlug = "hair", SubcategorySlug = "hair-masks-serums", SubcategoryTag = "Bond Repair Masques", Price = 599, OriginalPrice = 750, Rating = 4.9m, ReviewsCount = 2450, ImageUrl = "/images/beauty-hub/products/schwarzkopf-mask.webp", IsUploadingSoon = true, Badge = "Pro Spa", Formulation = "Rich Masque", Finish = "Smooth Silk", SkinType = "Over-Processed Hair", Preference = "Liquid Keratin" },
                new() { Id = "bprod-mask-2", Name = "Extraordinary Oil Serum with 6 Rare Floral Extracts (100ml)", Brand = "L'Oréal Paris", CategorySlug = "hair", SubcategorySlug = "hair-masks-serums", SubcategoryTag = "Anti-Frizz Gloss Serums", Price = 549, OriginalPrice = 699, Rating = 4.9m, ReviewsCount = 3890, ImageUrl = "/images/beauty-hub/products/loreal-extraordinary-serum.webp", IsUploadingSoon = true, Badge = "Cult Favorite", Formulation = "Serum", Finish = "High Gloss", SkinType = "All Hair Types", Preference = "UV & Heat Filter" }
            }
        });

        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "hair",
            CategoryName = "Hair",
            SubcategorySlug = "hair-styling-tools",
            SubcategoryName = "Hair Styling & Tools",
            IconEmoji = "✨",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "PRO STYLING & HEAT DEFENSE",
                Title = "Hair Styling & Tools — Protect & Perfect",
                TitleHighlight = "HEAT RESISTANT UP TO 230°C • MAXIMUM HOLD",
                Subtitle = "Shield strands against curling irons and blowouts with multi-active heat mists, pomades, and ionic dryers.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-hair-styling.webp",
                IsUploadingSoon = true,
                ThemeColor = "#059669",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Styling & Tools",
                "Heat Protectant Sprays",
                "Volumizing Mousse & Sprays",
                "Hair Dryers & Straighteners"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-tool-1", Name = "Hot Shot Heat Protection Mist up to 230°C (150ml)", Brand = "BBLUNT", CategorySlug = "hair", SubcategorySlug = "hair-styling-tools", SubcategoryTag = "Heat Protectant Sprays", Price = 449, OriginalPrice = 499, Rating = 4.8m, ReviewsCount = 1840, ImageUrl = "/images/beauty-hub/products/bblunt-heat-shot.webp", IsUploadingSoon = true, Badge = "Top Thermal Pick", Formulation = "Mist", Finish = "Weightless", SkinType = "All Types", Preference = "Grapeseed Oil & Provitamin B5" },
                new() { Id = "bprod-tool-2", Name = "Salon Dry Compact 1200W Lightweight Travel Hair Dryer", Brand = "Philips", CategorySlug = "hair", SubcategorySlug = "hair-styling-tools", SubcategoryTag = "Hair Dryers & Straighteners", Price = 899, OriginalPrice = 1195, Rating = 4.9m, ReviewsCount = 3120, ImageUrl = "/images/beauty-hub/products/philips-dryer.webp", IsUploadingSoon = true, Badge = "Campus Essential", Formulation = "Appliance", Finish = "Matte Black", SkinType = "All Types", Preference = "ThermoProtect Setting" }
            }
        });

        // 5. BATH & BODY SUBCATEGORIES
        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "bath-body",
            CategoryName = "Bath & Body",
            SubcategorySlug = "body-wash",
            SubcategoryName = "Shower Gels & Body Wash",
            IconEmoji = "🫧",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "SPA REFRESH",
                Title = "Shower Gels & Body Wash — Fragrant Glow",
                TitleHighlight = "AROMATHERAPY • HYDRATING FOAM",
                Subtitle = "Transform everyday showers into nourishing body spa sessions with creamy cocoa and citrus oils.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-body-wash.webp",
                IsUploadingSoon = true,
                ThemeColor = "#0d9488",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Body Washes",
                "Hydrating Shower Gels",
                "1% Salicylic Acid Acne Washes",
                "Aromatherapy Body Cleansers"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-body-1", Name = "Deep Moisture Nourishing Body Wash with NutriumMoisture (800ml)", Brand = "Dove", CategorySlug = "bath-body", SubcategorySlug = "body-wash", SubcategoryTag = "Hydrating Shower Gels", Price = 399, OriginalPrice = 525, Rating = 4.9m, ReviewsCount = 4210, ImageUrl = "/images/beauty-hub/products/dove-body-wash.webp", IsUploadingSoon = true, Badge = "Gentle Cleanse", Formulation = "Lotion Wash", Finish = "Baby Soft", SkinType = "Dry to Normal", Preference = "1/4th Moisturizing Cream" },
                new() { Id = "bprod-body-2", Name = "1% Salicylic Acid Body Wash for Body Acne (250ml)", Brand = "The Derma Co.", CategorySlug = "bath-body", SubcategorySlug = "body-wash", SubcategoryTag = "1% Salicylic Acid Acne Washes", Price = 319, OriginalPrice = 399, Rating = 4.8m, ReviewsCount = 1890, ImageUrl = "/images/beauty-hub/products/derma-body-wash.webp", IsUploadingSoon = true, Badge = "Acne Care", Formulation = "Gel", Finish = "Clear Skin", SkinType = "Acne Prone Body", Preference = "Salicylic Acid & Tea Tree" }
            }
        });

        // 6. GIFTS SUBCATEGORIES
        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "gifts",
            CategoryName = "Gifts",
            SubcategorySlug = "skincare-gifts",
            SubcategoryName = "Skincare Gift Hampers",
            IconEmoji = "🎁",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "FESTIVE GLOW GIFTING",
                Title = "Skincare Gift Hampers & Complete Routines",
                TitleHighlight = "CURATED HAMPERS • LUXURY BOXES",
                Subtitle = "Give the gift of radiant, healthy skin with dermatologist-selected serums, cleansers, and hydrating creams.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-gifts-skin.webp",
                IsUploadingSoon = true,
                ThemeColor = "#d97706",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Skincare Gifts",
                "Vitamin C Glow Kits",
                "Korean Glass Skin Hampers",
                "Budget Combos Under ₹499"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-gift-1", Name = "Festive Radiance Vitamin C 3-Step Routine Gift Box", Brand = "The Derma Co.", CategorySlug = "gifts", SubcategorySlug = "skincare-gifts", SubcategoryTag = "Vitamin C Glow Kits", Price = 999, OriginalPrice = 1499, Rating = 4.9m, ReviewsCount = 1250, ImageUrl = "/images/beauty-hub/products/derma-gift-box.webp", IsUploadingSoon = true, Badge = "Bestselling Hamper", Formulation = "Kit", Finish = "Luminous", SkinType = "All Skin Types", Preference = "Cleanser, Serum & Cream" },
                new() { Id = "bprod-gift-2", Name = "Pure Rose & Saffron Heritage Luxury Glow Hamper", Brand = "Forest Essentials", CategorySlug = "gifts", SubcategorySlug = "skincare-gifts", SubcategoryTag = "Korean Glass Skin Hampers", Price = 2850, OriginalPrice = 3400, Rating = 5.0m, ReviewsCount = 890, ImageUrl = "/images/beauty-hub/products/forest-gift-hamper.webp", IsUploadingSoon = true, Badge = "Royal Gifting", Formulation = "Luxury Trunk", Finish = "Velvet Glow", SkinType = "All Skin Types", Preference = "Handmade Ayurvedic" }
            }
        });

        // 7. TRENDING NOW SUBCATEGORIES
        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "trending-now",
            CategoryName = "Trending Now",
            SubcategorySlug = "viral-kbeauty",
            SubcategoryName = "Viral K-Beauty",
            IconEmoji = "🐌",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "SOCIAL MEDIA VIRAL SENSATIONS",
                Title = "Viral K-Beauty — Snail Mucin & Rice Toners",
                TitleHighlight = "HOTTEST FINDS • 100% KOREAN AUTHENTIC",
                Subtitle = "Discover the exact glass-skin serums and barrier-calming formulas breaking records on Instagram & TikTok.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-viral-kbeauty.webp",
                IsUploadingSoon = true,
                ThemeColor = "#ec4899",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Viral Hits",
                "Advanced Snail 96 Mucin",
                "Rice Water Hydrating Toners",
                "Calming Centella Sun Sticks"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-trend-1", Name = "Advanced Snail 96 Mucin Power Essence (100ml)", Brand = "COSRX", CategorySlug = "trending-now", SubcategorySlug = "viral-kbeauty", SubcategoryTag = "Advanced Snail 96 Mucin", Price = 1099, OriginalPrice = 1450, Rating = 5.0m, ReviewsCount = 5890, ImageUrl = "/images/beauty-hub/products/cosrx-snail-mucin.webp", IsUploadingSoon = true, Badge = "Global #1 Viral", Formulation = "Essence", Finish = "Glass Skin", SkinType = "All Skin Types", Preference = "96% Snail Secretion Filtrate" },
                new() { Id = "bprod-trend-2", Name = "Relief Sun : Rice + Probiotics SPF50+ PA++++ (50ml)", Brand = "Beauty of Joseon", CategorySlug = "trending-now", SubcategorySlug = "viral-kbeauty", SubcategoryTag = "Calming Centella Sun Sticks", Price = 1199, OriginalPrice = 1500, Rating = 4.9m, ReviewsCount = 3450, ImageUrl = "/images/beauty-hub/products/boj-rice-sunscreen.webp", IsUploadingSoon = true, Badge = "Cult Favorite", Formulation = "Lotion Gel", Finish = "Dewy Invisible", SkinType = "Sensitive / Normal", Preference = "Grain Ferment Extracts" }
            }
        });

        // 8. NEW LAUNCHES SUBCATEGORIES
        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "new-launches",
            CategoryName = "New Launches",
            SubcategorySlug = "dropped-makeup",
            SubcategoryName = "Just Dropped Makeup",
            IconEmoji = "✨",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "NEW ARRIVALS",
                Title = "Just Dropped Makeup — Fresh Formulations & Shades",
                TitleHighlight = "BRAND NEW • RUNWAY INSPIRATION",
                Subtitle = "Be the first on campus to rock new transfer-proof vinyl inks, ultra-blur powders, and sunset palettes.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-new-drops.webp",
                IsUploadingSoon = true,
                ThemeColor = "#6366f1",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Fresh Drops",
                "High-Shine Vinyl Inks",
                "Micro-Blurring Setting Powders",
                "Sunset Chrome Eyeshadows"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-new-1", Name = "SuperStay Vinyl Ink Longwear Liquid Lipcolor (4.2ml)", Brand = "Maybelline New York", CategorySlug = "new-launches", SubcategorySlug = "dropped-makeup", SubcategoryTag = "High-Shine Vinyl Inks", Price = 649, OriginalPrice = 849, Rating = 4.9m, ReviewsCount = 1420, ImageUrl = "/images/beauty-hub/products/maybelline-vinyl-ink.webp", IsUploadingSoon = true, Badge = "Just Launched", Formulation = "Vinyl Liquid", Finish = "High Gloss Matte-Wear", SkinType = "All Types", Preference = "16HR Shine & Wear" },
                new() { Id = "bprod-new-2", Name = "Airbrush Flawless Micro-Fine Setting Powder (9g)", Brand = "Nykaa Cosmetics", CategorySlug = "new-launches", SubcategorySlug = "dropped-makeup", SubcategoryTag = "Micro-Blurring Setting Powders", Price = 499, OriginalPrice = 699, Rating = 4.8m, ReviewsCount = 650, ImageUrl = "/images/beauty-hub/products/nykaa-airbrush-powder.webp", IsUploadingSoon = true, Badge = "Instant Filter", Formulation = "Translucent Powder", Finish = "Velvet Blur", SkinType = "Oily & Combo", Preference = "Silica & Rice Starch" }
            }
        });

        // 9. MINIS SUBCATEGORIES
        pages.Add(new BeautySubcategoryPageDto
        {
            CategorySlug = "minis",
            CategoryName = "Minis",
            SubcategorySlug = "mini-lips",
            SubcategoryName = "Mini Lipsticks & Tints",
            IconEmoji = "💄",
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = "TRAVEL POCKET SIZES",
                Title = "Beauty Minis — Travel Friendly & Pocket Sized",
                TitleHighlight = "TSA APPROVED • UNDER ₹299",
                Subtitle = "Tiny in size, massive in performance. Fit your daily essentials right inside small clutches, pouches, and dorm totes.",
                ImageUrl = "/images/beauty-hub/subcategories/hero-minis-lips.webp",
                IsUploadingSoon = true,
                ThemeColor = "#8b5cf6",
                BannerHeight = 280
            },
            SubcategoryItems = new List<string>
            {
                "All Beauty Minis",
                "Pocket Matte Lipsticks",
                "Travel Skincare Kits (30ml)",
                "Pocket Fragrance Sprays (10ml)"
            },
            Products = new List<BeautyProductItemDto>
            {
                new() { Id = "bprod-mini-1", Name = "Mini Liquid Matte Lipstick Pocket Trio", Brand = "SUGAR Cosmetics", CategorySlug = "minis", SubcategorySlug = "mini-lips", SubcategoryTag = "Pocket Matte Lipsticks", Price = 499, OriginalPrice = 699, Rating = 4.8m, ReviewsCount = 980, ImageUrl = "/images/beauty-hub/products/sugar-minis.webp", IsUploadingSoon = true, Badge = "Travel Essential", Formulation = "Liquid", Finish = "Matte", SkinType = "All Types", Preference = "1.5ml Each, Smudge-Proof" },
                new() { Id = "bprod-mini-2", Name = "Travel Cleanser & Moisturizer Discovery Duo (2x30ml)", Brand = "Cetaphil", CategorySlug = "minis", SubcategorySlug = "mini-lips", SubcategoryTag = "Travel Skincare Kits (30ml)", Price = 249, OriginalPrice = 325, Rating = 4.9m, ReviewsCount = 2150, ImageUrl = "/images/beauty-hub/products/cetaphil-duo.webp", IsUploadingSoon = true, Badge = "Hostel Favorite", Formulation = "Lotion & Wash", Finish = "Hydrated", SkinType = "Sensitive Skin", Preference = "TSA Carry-On Approved" }
            }
        });

        return pages;
    }

    /// <summary>
    /// Generates a rich, fully populated subcategory page dynamically for any beauty category and subcategory slug.
    /// Ensures that clicking any subcategory link never returns null or fails to render.
    /// </summary>
    public static BeautySubcategoryPageDto GenerateDynamicSubcategoryPage(string categorySlug, string subcategorySlug)
    {
        var catSlug = (categorySlug ?? "makeup").Trim().ToLowerInvariant();
        var subSlug = (subcategorySlug ?? "all").Trim().ToLowerInvariant();

        var defaultCategories = CreateDefaultCategories();
        var catItem = defaultCategories.FirstOrDefault(c => string.Equals(c.Slug, catSlug, StringComparison.OrdinalIgnoreCase))
                      ?? defaultCategories.FirstOrDefault();
        var catName = catItem?.Title ?? "Beauty";

        var defaultCatPages = CreateDefaultCategoryPages();
        var catPage = defaultCatPages.FirstOrDefault(p => string.Equals(p.CategorySlug, catSlug, StringComparison.OrdinalIgnoreCase));
        var subCard = catPage?.Subcategories?.FirstOrDefault(s => string.Equals(s.SubcategorySlug, subSlug, StringComparison.OrdinalIgnoreCase));

        var subName = subCard?.SubcategoryName ?? System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(subSlug.Replace('-', ' '));
        var emoji = subCard?.IconEmoji ?? catItem?.Icon ?? "✨";
        var themeColor = catPage?.HeroBanner?.ThemeColor ?? "#be123c";

        var filterItems = new List<string> { $"All {subName}" };
        if (subCard?.SampleItems != null && subCard.SampleItems.Count > 0)
        {
            filterItems.AddRange(subCard.SampleItems);
        }
        else
        {
            filterItems.Add($"Classic {subName}");
            filterItems.Add($"Pro {subName}");
            filterItems.Add($"Daily {subName}");
            filterItems.Add($"Intense Care {subName}");
        }

        var products = new List<BeautyProductItemDto>
        {
            new() { Id = $"bprod-{subSlug}-1", Name = $"Salon Pro {subName} with Botanical Actives", Brand = "L'Oréal Paris", CategorySlug = catSlug, SubcategorySlug = subSlug, SubcategoryTag = filterItems.Count > 1 ? filterItems[1] : subName, Price = 399, OriginalPrice = 599, Rating = 4.9m, ReviewsCount = 1840, IsUploadingSoon = true, Badge = "Bestseller", Formulation = "Rich Formula", Finish = "Natural Silk", SkinType = "All Types", Preference = "Derm Approved" },
            new() { Id = $"bprod-{subSlug}-2", Name = $"Ultra Hydrating & Nourishing {subName}", Brand = "Cetaphil", CategorySlug = catSlug, SubcategorySlug = subSlug, SubcategoryTag = filterItems.Count > 2 ? filterItems[2] : subName, Price = 499, OriginalPrice = 699, Rating = 4.8m, ReviewsCount = 1420, IsUploadingSoon = true, Badge = "Campus Pick", Formulation = "Gentle Care", Finish = "Hydrated", SkinType = "Sensitive / Normal", Preference = "Sulfate Free" },
            new() { Id = $"bprod-{subSlug}-3", Name = $"Daily Campus Restoring {subName} (250ml)", Brand = "Mamaearth", CategorySlug = catSlug, SubcategorySlug = subSlug, SubcategoryTag = filterItems.Count > 1 ? filterItems[1] : subName, Price = 299, OriginalPrice = 399, Rating = 4.7m, ReviewsCount = 980, IsUploadingSoon = true, Badge = "100% Toxin Free", Formulation = "Clean Formula", Finish = "Soft Touch", SkinType = "All Types", Preference = "Natural Extracts" },
            new() { Id = $"bprod-{subSlug}-4", Name = $"Advanced Repair Therapy {subName}", Brand = "The Derma Co.", CategorySlug = catSlug, SubcategorySlug = subSlug, SubcategoryTag = filterItems.Count > 3 ? filterItems[3] : subName, Price = 549, OriginalPrice = 699, Rating = 4.9m, ReviewsCount = 2100, IsUploadingSoon = true, Badge = "Clinical Actives", Formulation = "Concentrated", Finish = "Matte Glow", SkinType = "All Types", Preference = "Targeted Results" },
            new() { Id = $"bprod-{subSlug}-5", Name = $"Everyday Pocket Value {subName}", Brand = "Nykaa Cosmetics", CategorySlug = catSlug, SubcategorySlug = subSlug, SubcategoryTag = filterItems.Count > 2 ? filterItems[2] : subName, Price = 249, OriginalPrice = 349, Rating = 4.7m, ReviewsCount = 840, IsUploadingSoon = true, Badge = "Nykaa Choice", Formulation = "Lightweight", Finish = "Radiant", SkinType = "All Types", Preference = "Cruelty Free" },
            new() { Id = $"bprod-{subSlug}-6", Name = $"Luxe Intensive {subName} Holiday Edition", Brand = "Forest Essentials", CategorySlug = catSlug, SubcategorySlug = subSlug, SubcategoryTag = filterItems.Count > 1 ? filterItems[1] : subName, Price = 1250, OriginalPrice = 1600, Rating = 5.0m, ReviewsCount = 620, IsUploadingSoon = true, Badge = "Luxury Icon", Formulation = "Artisanal", Finish = "Velvet Luxe", SkinType = "All Types", Preference = "Ayurvedic Heritage" },
            new() { Id = $"bprod-{subSlug}-7", Name = $"Ultra Smooth {subName} with Keratin & Vitamin E", Brand = "TRESemmé", CategorySlug = catSlug, SubcategorySlug = subSlug, SubcategoryTag = filterItems.Count > 1 ? filterItems[1] : subName, Price = 329, OriginalPrice = 450, Rating = 4.8m, ReviewsCount = 1190, IsUploadingSoon = true, Badge = "Pro Choice", Formulation = "Smooth Silk", Finish = "Gloss", SkinType = "Dry / Frizzy", Preference = "Salon Tested" },
            new() { Id = $"bprod-{subSlug}-8", Name = $"Organic Deep Therapy {subName} Treatment", Brand = "Plum", CategorySlug = catSlug, SubcategorySlug = subSlug, SubcategoryTag = filterItems.Count > 2 ? filterItems[2] : subName, Price = 389, OriginalPrice = 550, Rating = 4.9m, ReviewsCount = 1530, IsUploadingSoon = true, Badge = "100% Vegan", Formulation = "Pure Herbal", Finish = "Weightless", SkinType = "All Types", Preference = "Clean Beauty" }
        };

        return new BeautySubcategoryPageDto
        {
            CategorySlug = catSlug,
            CategoryName = catName,
            SubcategorySlug = subSlug,
            SubcategoryName = subName,
            IconEmoji = emoji,
            HeroBanner = new BeautyCategoryHeroDto
            {
                IsEnabled = true,
                BadgeText = $"{subName.ToUpperInvariant()} ESSENTIALS",
                Title = subName,
                TitleHighlight = "VERIFIED BEAUTY & CARE",
                Subtitle = $"Explore curated {subName} formulas and verified beauty essentials.",
                ImageUrl = $"/images/beauty-hub/subcategories/hero-{subSlug}.webp",
                IsUploadingSoon = true,
                ThemeColor = themeColor,
                BannerHeight = 280
            },
            ShopByBrands = new BeautyShopByBrandsDto
            {
                IsEnabled = true,
                Title = "Shop by Brands",
                Subtitle = $"Official authorized brands certified for authentic {subName.ToLowerInvariant()} ingredients & efficacy.",
                Brands = new List<BeautyBrandItemDto>
                {
                    new() { Id = $"bnd-{subSlug}-1", Name = "L'Oréal Paris", Slug = "loreal-paris", IsUploadingSoon = true, DisplayOrder = 1 },
                    new() { Id = $"bnd-{subSlug}-2", Name = "Maybelline", Slug = "maybelline", IsUploadingSoon = true, DisplayOrder = 2 },
                    new() { Id = $"bnd-{subSlug}-3", Name = "Cetaphil", Slug = "cetaphil", IsUploadingSoon = true, DisplayOrder = 3 },
                    new() { Id = $"bnd-{subSlug}-4", Name = "The Derma Co", Slug = "the-derma-co", IsUploadingSoon = true, DisplayOrder = 4 },
                    new() { Id = $"bnd-{subSlug}-5", Name = "Minimalist", Slug = "minimalist", IsUploadingSoon = true, DisplayOrder = 5 },
                    new() { Id = $"bnd-{subSlug}-6", Name = "Nykaa", Slug = "nykaa", IsUploadingSoon = true, DisplayOrder = 6 },
                    new() { Id = $"bnd-{subSlug}-7", Name = "Mamaearth", Slug = "mamaearth", IsUploadingSoon = true, DisplayOrder = 7 },
                    new() { Id = $"bnd-{subSlug}-8", Name = "Plum", Slug = "plum", IsUploadingSoon = true, DisplayOrder = 8 },
                }
            },
            SubcategoryItems = filterItems,
            Products = products
        };
    }
}

public sealed class BeautyThemeSettingsDto
{
    public string ThemeName { get; set; } = "BIG BANG DIWALI";
    public string ThemeTagline { get; set; } = "LOWEST PRICE OF 2026 • Beauty, food & more";
    public string BadgeText { get; set; } = "✨ FESTIVE GLOW DHAMAKA";
    public string NoticeBarText { get; set; } = "🎆 Festive Glow Dhamaka is LIVE! Extra 10% instant discount + Daily Flash Deals up to 80% Off.";
    public bool IsFestiveEnabled { get; set; } = true;
    public string FrameDesignStyle { get; set; } = "FestiveArchedGolden"; // FestiveArchedGolden, RoyalMughalArch, ModernGlamGlow
    public string AccentGold { get; set; } = "#fbbf24";
    public string PrimaryBgGradient { get; set; } = "linear-gradient(135deg, #700b1a 0%, #a8122a 40%, #c2193b 70%, #d94612 100%)";
    public bool DiyaDecorationEnabled { get; set; } = true;
}

public sealed class BeautyHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public bool AutoPlay { get; set; } = true;
    public int IntervalSeconds { get; set; } = 4;
    public List<BeautyHeroSlideDto> Slides { get; set; } = new();
}

public sealed class BeautyHeroSlideDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string TagBadge { get; set; } = "BIG BANG DIWALI";
    public string Title { get; set; } = string.Empty;
    public string PriceHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public string ButtonText { get; set; } = "Explore Deals →";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyPromoBannersDto
{
    public bool IsEnabled { get; set; } = true;
    public string BankOfferHeader { get; set; } = "BANK OFFER REFRESHED DAILY";
    public string BankOfferSubtext { get; set; } = "10% Instant Discount* on SBI Debit & Credit Cards";
    public List<BeautyPromoCardDto> Cards { get; set; } = new();
}

public sealed class BeautyPromoCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string TagBadge { get; set; } = "BIG BANG DIWALI";
    public string BankOfferBadge { get; set; } = "BANK OFFER REFRESHED DAILY";
    public string Title { get; set; } = string.Empty;
    public string DiscountHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string BankOfferText { get; set; } = "10% Instant Discount* on SBI Card";
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautySubCategoriesDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Shop by Category";
    public string Subtitle { get; set; } = "Explore verified university and lifestyle beauty selections";
    public List<BeautySubCategoryItemDto> Items { get; set; } = new();
}

public sealed class BeautySubCategoryItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = "✨";
    public string Slug { get; set; } = string.Empty;
    public string? Badge { get; set; }
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public string? ImageUrl { get; set; }
    public bool IsUploadingSoon { get; set; } = true;
    public string Description { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
    public List<string> Subcategories { get; set; } = new();
}

public sealed class BeautyCategoryPageDto
{
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = "💄";
    public string? Badge { get; set; }
    public string? Description { get; set; }
    public BeautyCategoryHeroDto HeroBanner { get; set; } = new();
    public List<BeautySubcategoryCardDto> Subcategories { get; set; } = new();
    public List<string> FilterTags { get; set; } = new();
    public List<BeautyProductItemDto> FeaturedProducts { get; set; } = new();
}

public sealed class BeautySubcategoryCardDto
{
    public string SubcategorySlug { get; set; } = string.Empty;
    public string SubcategoryName { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = "💋";
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public string Description { get; set; } = string.Empty;
    public string ItemCountText { get; set; } = "15+ Products";
    public string TargetUrl { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public List<string> SampleItems { get; set; } = new();
}

public sealed class BeautySubcategoryPageDto
{
    public string CategorySlug { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string SubcategorySlug { get; set; } = string.Empty;
    public string SubcategoryName { get; set; } = string.Empty;
    public string IconEmoji { get; set; } = "💋";
    public BeautyCategoryHeroDto HeroBanner { get; set; } = new();
    public BeautyShopByBrandsDto ShopByBrands { get; set; } = new();
    public List<string> SubcategoryItems { get; set; } = new();
    public List<string> FilterTags { get; set; } = new() { "Formulation", "Finish", "Skin Type", "Preference", "Brand", "Price", "Discount" };
    public List<BeautyProductItemDto> Products { get; set; } = new();
}

public sealed class BeautyShopByBrandsDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Shop by Brands";
    public string Subtitle { get; set; } = "Certified authentic beauty brands for daily campus glow and dermatological safety.";
    public List<BeautyBrandItemDto> Brands { get; set; } = new();
}

public sealed class BeautyBrandItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public sealed class BeautyCategoryHeroDto
{
    public bool IsEnabled { get; set; } = true;
    public string BadgeText { get; set; } = "VERIFIED BEAUTY & CARE";
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public string ThemeColor { get; set; } = "#e11d48";
    public int BannerHeight { get; set; } = 320;
}

public sealed class BeautyProductItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string CategorySlug { get; set; } = string.Empty;
    public string SubcategorySlug { get; set; } = string.Empty;
    public string SubcategoryTag { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public int DiscountPercent => OriginalPrice > Price && OriginalPrice > 0 ? (int)Math.Round((OriginalPrice - Price) / OriginalPrice * 100) : 0;
    public decimal Rating { get; set; } = 4.8m;
    public int ReviewsCount { get; set; } = 120;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsUploadingSoon { get; set; } = true;
    public string? Badge { get; set; }
    public string Formulation { get; set; } = string.Empty;
    public string Finish { get; set; } = string.Empty;
    public string SkinType { get; set; } = string.Empty;
    public string Preference { get; set; } = string.Empty;
    public bool InStock { get; set; } = true;
    public int DisplayOrder { get; set; }
}

public sealed class BeautySpotlightBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "The Global Luxe Store";
    public string Subtitle { get; set; } = "Curated luxury fragrances, premium French serums & international beauty collections";
    public string ButtonText { get; set; } = "Explore now →";
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyArchedSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string SectionTitle { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string AccentColor { get; set; } = "#dc2626";
    public string BgGradient { get; set; } = "linear-gradient(135deg, #7f1d1d 0%, #b91c1c 50%, #991b1b 100%)";
    public List<BeautyArchedCardDto> Cards { get; set; } = new();
}

public sealed class BeautyArchedCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string OfferTag { get; set; } = "Special Offer";
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyDualBannersDto
{
    public bool IsEnabled { get; set; } = true;
    public BeautyDualBannerCardDto LeftBanner { get; set; } = new();
    public BeautyDualBannerCardDto RightBanner { get; set; } = new();
}

public sealed class BeautyDualBannerCardDto
{
    public string TagBadge { get; set; } = "Diwali celebration";
    public string Title { get; set; } = string.Empty;
    public string PriceHighlight { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public string ButtonText { get; set; } = "→";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyPremiumGiftsDto
{
    public bool IsEnabled { get; set; } = true;
    public string SectionTitle { get; set; } = "Premium festive gifts";
    public string Subtitle { get; set; } = "Luxury perfumes, French skincare elixirs & premium gift boxes";
    public List<BeautyBrandCardDto> Brands { get; set; } = new();
}

public sealed class BeautyBrandCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string BrandName { get; set; } = string.Empty;
    public string DiscountTag { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyFestiveLooksDto
{
    public bool IsEnabled { get; set; } = true;
    public string SectionTitle { get; set; } = "Festive looks";
    public string Subtitle { get; set; } = "Curated collections to ace every festival, wedding & Diwali gathering";
    public List<BeautyFestiveLookCardDto> Cards { get; set; } = new();
}

public sealed class BeautyFestiveLookCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string LookName { get; set; } = string.Empty;
    public string OfferTag { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyLimitedDealsRibbonDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "LIMITED PERIOD DEALS";
    public string Tagline { get; set; } = "PRICES NEVER SEEN BEFORE";
    public string Badge { get; set; } = "⚡ FLASH SAVINGS";
    public string BgGradient { get; set; } = "linear-gradient(90deg, #b91c1c 0%, #dc2626 50%, #ea580c 100%)";
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public string DiscountHighlight { get; set; } = "FLAT 50% - 80% OFF";
    public string Subtitle { get; set; } = "Hurry! Limited stock available at festival prices";
}

public sealed class BeautyFlashDealsTimerDto
{
    public bool IsEnabled { get; set; } = true;
    public string TimerTitle { get; set; } = "DEALS END IN";
    public int TimerHours { get; set; } = 28;
    public DateTime CountdownEndTimeUtc { get; set; } = DateTime.UtcNow.AddHours(28);
    public List<BeautyFlashDealCardDto> Cards { get; set; } = new();
}

public sealed class BeautyFlashDealCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string DealTag { get; set; } = "FESTIVE DEAL";
    public string OfferTag { get; set; } = "FESTIVE DEAL";
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string PriceHighlight { get; set; } = string.Empty;
    public string OriginalPrice { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyPicksUnder99Dto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Beauty picks under ₹99";
    public string PriceHighlight { get; set; } = "Under ₹99";
    public string SectionTitle { get; set; } = "Beauty picks under ₹99";
    public string Subtitle { get; set; } = "Student essentials & budget grooming must-haves";
    public string HighlightBadge { get; set; } = "BUDGET DHAMAKA";
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public List<BeautyArchedCardDto> Cards { get; set; } = new();
}

public sealed class BeautyBiggestBrandsDto
{
    public bool IsEnabled { get; set; } = true;
    public string SectionTitle { get; set; } = "Biggest brands on offer";
    public string Subtitle { get; set; } = "Official brand stores with authentic guarantee & fastest delivery";
    public List<BeautyBigBrandCardDto> Brands { get; set; } = new();
}

public sealed class BeautyBigBrandCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string BrandName { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string OfferTag { get; set; } = string.Empty;
    public string DiscountHighlight { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyPartnersDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Our Authorized Brand Partners";
    public string SectionTitle { get; set; } = "Our Authorized Brand Partners";
    public List<BeautyPartnerItemDto> Items { get; set; } = new();
}

public sealed class BeautyPartnerItemDto
{
    public string Name { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string OfferTag { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
}

public sealed class BeautyBrandNewDto
{
    public bool IsEnabled { get; set; } = true;
    public string SectionTitle { get; set; } = "Brand new in beauty";
    public string Subtitle { get; set; } = "Fresh launches, viral trends & newly imported cult favorites";
    public List<BeautyBrandNewCardDto> Cards { get; set; } = new();
}

public sealed class BeautyBrandNewCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string ProductTitle { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string OfferTag { get; set; } = string.Empty;
    public string PriceHighlight { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}
