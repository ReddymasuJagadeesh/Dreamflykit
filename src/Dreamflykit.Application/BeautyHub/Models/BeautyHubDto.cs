namespace Dreamflykit.Application.BeautyHub.Models;

public sealed class BeautyHubDto
{
    public BeautyThemeSettingsDto ThemeSettings { get; set; } = new();
    public BeautyHeroBannerDto HeroBanner { get; set; } = new();
    public BeautyPromoBannersDto PromoBanners { get; set; } = new();
    public BeautySubCategoriesDto SubCategories { get; set; } = new();
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

            SubCategories = new BeautySubCategoriesDto
            {
                IsEnabled = true,
                Title = "Quick Beauty & Grooming Shortcuts",
                Items = new List<BeautySubCategoryItemDto>
                {
                    new() { Id = "sub-1", Title = "Skincare", Icon = "🧴", Slug = "skincare", TargetUrl = "/category/beauty?subcategory=skincare" },
                    new() { Id = "sub-2", Title = "Top 50 Deals", Icon = "⚡", Slug = "top-50-deals", Badge = "Live now", TargetUrl = "/category/beauty?sort=Discount" },
                    new() { Id = "sub-3", Title = "Men's grooming", Icon = "🧔", Slug = "mens-grooming", TargetUrl = "/category/beauty?subcategory=mens-grooming" },
                    new() { Id = "sub-4", Title = "Global Luxe", Icon = "✨", Slug = "global-luxe", TargetUrl = "/category/beauty?subcategory=fragrances" },
                    new() { Id = "sub-5", Title = "Derma store", Icon = "🧪", Slug = "derma-store", TargetUrl = "/category/beauty?subcategory=skincare" },
                    new() { Id = "sub-6", Title = "K-beauty", Icon = "🌸", Slug = "k-beauty", TargetUrl = "/category/beauty?subcategory=skincare" },
                    new() { Id = "sub-7", Title = "Only here!", Icon = "🏷️", Slug = "exclusive", Badge = "EXCLUSIVE", TargetUrl = "/category/beauty?view=hub" },
                    new() { Id = "sub-8", Title = "ENVY", Icon = "💎", Slug = "envy", TargetUrl = "/category/beauty?search=envy" },
                    new() { Id = "sub-9", Title = "The Derma Co.", Icon = "🧬", Slug = "the-derma-co", TargetUrl = "/category/beauty?search=derma" },
                    new() { Id = "sub-10", Title = "Hair care", Icon = "💇‍♀️", Slug = "hair-care", TargetUrl = "/category/beauty?subcategory=hair-care" },
                    new() { Id = "sub-11", Title = "Makeup", Icon = "💄", Slug = "makeup", TargetUrl = "/category/beauty?subcategory=makeup" },
                    new() { Id = "sub-12", Title = "Fragrances", Icon = "🌸", Slug = "fragrances", TargetUrl = "/category/beauty?subcategory=fragrances" },
                    new() { Id = "sub-13", Title = "Bath & body", Icon = "🛁", Slug = "bath-body", TargetUrl = "/category/beauty?subcategory=bath-body" },
                    new() { Id = "sub-14", Title = "Hygiene", Icon = "🧼", Slug = "hygiene", TargetUrl = "/category/beauty?subcategory=skincare" },
                    new() { Id = "sub-15", Title = "Oral care", Icon = "🪥", Slug = "oral-care", TargetUrl = "/category/beauty?subcategory=oral-care" },
                    new() { Id = "sub-16", Title = "Combos", Icon = "🎁", Slug = "combos", Badge = "COMBOS", TargetUrl = "/category/beauty?sort=Discount" },
                    new() { Id = "sub-17", Title = "Beauty supp", Icon = "💊", Slug = "supplements", TargetUrl = "/category/beauty?subcategory=beauty-supplements" }
                }
            },

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
                    TagBadge = "Gifts for your loved ones",
                    Title = "Curated festive gift boxes",
                    PriceHighlight = "Up to 80% Off",
                    ImageUrl = "/images/beauty-hub/dual-banner-gifts.webp",
                    TargetUrl = "#beauty-products-catalog",
                    ButtonText = "Shop Gift Hampers →",
                    IsUploadingSoon = false
                }
            },

            PremiumGifts = new BeautyPremiumGiftsDto
            {
                IsEnabled = true,
                SectionTitle = "Premium gifts",
                Subtitle = "Internationally renowned premium skincare, fragrances & salon haircare",
                Brands = new List<BeautyBrandCardDto>
                {
                    new() { Id = "prem-1", BrandName = "Eucerin", DiscountTag = "Up to 30% Off", Subtitle = "Dermatologist recommended", ImageUrl = "/images/beauty-hub/brand-eucerin.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "prem-2", BrandName = "DAVIDOFF", DiscountTag = "Up to 60% Off", Subtitle = "Cool Water & luxury scents", ImageUrl = "/images/beauty-hub/brand-davidoff.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "prem-3", BrandName = "L'ORÉAL PARIS", DiscountTag = "Up to 30% Off", Subtitle = "Professional salon haircare", ImageUrl = "/images/beauty-hub/brand-loreal.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "prem-4", BrandName = "TIRTIR", DiscountTag = "Up to 50% Off", Subtitle = "Mask fit red cushion compact", ImageUrl = "/images/beauty-hub/brand-tirtir.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "prem-5", BrandName = "SKIN1004", DiscountTag = "Up to 25% Off", Subtitle = "Madagascar Centella Ampoule", ImageUrl = "/images/beauty-hub/brand-skin1004.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "prem-6", BrandName = "d'Alba", DiscountTag = "Up to 15% Off", Subtitle = "White truffle first spray serum", ImageUrl = "/images/beauty-hub/brand-dalba.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "prem-7", BrandName = "JAGUAR", DiscountTag = "Up to 60% Off", Subtitle = "Classic black eau de toilette", ImageUrl = "/images/beauty-hub/brand-jaguar.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "prem-8", BrandName = "matrix", DiscountTag = "Up to 35% Off", Subtitle = "Opti.Care smooth & straight", ImageUrl = "/images/beauty-hub/brand-matrix.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "prem-9", BrandName = "BEAUTY OF JOSEON", DiscountTag = "Up to 20% Off", Subtitle = "Relief sun rice + probiotics", ImageUrl = "/images/beauty-hub/brand-joseon.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false }
                }
            },

            FestiveLooks = new BeautyFestiveLooksDto
            {
                IsEnabled = true,
                SectionTitle = "NAIL EVERY FESTIVE LOOK",
                Subtitle = "Get runway ready with step-by-step festive trends & curated essentials",
                Cards = new List<BeautyFestiveLookCardDto>
                {
                    new() { Id = "look-1", LookName = "Flawless finish", Subtitle = "Airbrush base & setting powder", ImageUrl = "/images/beauty-hub/look-flawless.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "look-2", LookName = "Star-struck!", Subtitle = "High-shine highlighter & gloss", ImageUrl = "/images/beauty-hub/look-starstruck.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "look-3", LookName = "Mesmer-eyes", Subtitle = "Smokey shadows & winged eyeliner", ImageUrl = "/images/beauty-hub/look-mesmereyes.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "look-4", LookName = "All-nighter looks", Subtitle = "Sweat-proof 24h long stay", ImageUrl = "/images/beauty-hub/look-allnighter.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "look-5", LookName = "Sunkissed glow", Subtitle = "Peachy blush & dewy serum drops", ImageUrl = "/images/beauty-hub/look-sunkissed.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false }
                }
            },

            LimitedDealsRibbon = new BeautyLimitedDealsRibbonDto
            {
                IsEnabled = true,
                Title = "Limited Stock, Big Deals",
                DiscountHighlight = "Up to 75% Off",
                Subtitle = "Grab fast-moving festive bestsellers before inventory empties out",
                ButtonText = "Shop Fast Deals →",
                ImageUrl = "/images/beauty-hub/limited-deals-ribbon.webp",
                TargetUrl = "#beauty-products-catalog",
                IsUploadingSoon = false
            },

            FlashDealsTimer = new BeautyFlashDealsTimerDto
            {
                IsEnabled = true,
                TimerTitle = "New deals every 12 Hrs",
                TimerHours = 12,
                TimerEndIso = DateTime.UtcNow.AddHours(11).AddMinutes(28).ToString("o"),
                Cards = new List<BeautyFlashDealCardDto>
                {
                    new() { Id = "flash-1", OfferTag = "Up to 70% Off", Title = "Face washes", Subtitle = "Gentle foaming & brightening cleansers", ImageUrl = "/images/beauty-hub/flash-facewash.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "flash-2", OfferTag = "Under ₹199", Title = "Face Cream", Subtitle = "Ceramide barrier repair moisturizers", ImageUrl = "/images/beauty-hub/flash-facecream.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "flash-3", OfferTag = "From ₹199", Title = "Hair serums", Subtitle = "Heat protection & anti-frizz drops", ImageUrl = "/images/beauty-hub/flash-hairserum.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "flash-4", OfferTag = "Under ₹199", Title = "Shampoos", Subtitle = "Sulfate-free scalp cleansers", ImageUrl = "/images/beauty-hub/flash-shampoo.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "flash-5", OfferTag = "Min. 25% Off", Title = "Makeup kits", Subtitle = "Complete brushes & sponge sets", ImageUrl = "/images/beauty-hub/flash-makeupkit.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "flash-6", OfferTag = "Under ₹199", Title = "Deodorants", Subtitle = "Long-lasting fresh body sprays", ImageUrl = "/images/beauty-hub/flash-deodorant.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "flash-7", OfferTag = "Under ₹299", Title = "Perfumes", Subtitle = "Pocket perfumes & mist sprays", ImageUrl = "/images/beauty-hub/flash-perfumes.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false }
                }
            },

            BeautyPicksUnder99 = new BeautyPicksUnder99Dto
            {
                IsEnabled = true,
                Title = "Beauty Picks",
                PriceHighlight = "Under ₹99",
                Subtitle = "Pocket-friendly daily self-care & university student grooming kits",
                ImageUrl = "/images/beauty-hub/banner-under99.webp",
                TargetUrl = "#beauty-products-catalog",
                IsUploadingSoon = false
            },

            BiggestBrands = new BeautyBiggestBrandsDto
            {
                IsEnabled = true,
                SectionTitle = "Biggest brands on offer",
                Subtitle = "Explore verified authentic products with direct brand warranties",
                Brands = new List<BeautyBigBrandCardDto>
                {
                    new() { Id = "brand-1", BrandName = "Cetaphil", OfferTag = "Up to 60% Off", Subtitle = "Gentle cleanser & lotion", ImageUrl = "/images/beauty-hub/brand-cetaphil.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "brand-2", BrandName = "BEARDO", OfferTag = "Up to 75% Off", Subtitle = "Whisky smoke perfume & oils", ImageUrl = "/images/beauty-hub/brand-beardo.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "brand-3", BrandName = "mamaearth", OfferTag = "Up to 70% Off", Subtitle = "Onion shampoo & vitamin C face wash", ImageUrl = "/images/beauty-hub/brand-mamaearth.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "brand-4", BrandName = "SWISS BEAUTY", OfferTag = "From ₹149", Subtitle = "Craze lip gloss & concealers", ImageUrl = "/images/beauty-hub/brand-swissbeauty.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "brand-5", BrandName = "L'ORÉAL PARIS", OfferTag = "Up to 50% Off", Subtitle = "Hyaluronic acid serum & masks", ImageUrl = "/images/beauty-hub/brand-loreal-paris.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "brand-6", BrandName = "LAKMÉ", OfferTag = "Up to 65% Off", Subtitle = "Eyeconic kajal & 9 to 5 foundation", ImageUrl = "/images/beauty-hub/brand-lakme.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "brand-7", BrandName = "BLA BLI BLU", OfferTag = "Up to 80% Off", Subtitle = "Body mist & glow lotion", ImageUrl = "/images/beauty-hub/brand-blabliblu.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "brand-8", BrandName = "RENÉE", OfferTag = "Up to 70% Off", Subtitle = "5 in 1 fab lipstick & kohls", ImageUrl = "/images/beauty-hub/brand-renee.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false }
                }
            },

            BeautyPartners = new BeautyPartnersDto
            {
                IsEnabled = true,
                SectionTitle = "Beauty partner",
                Items = new List<BeautyPartnerItemDto>
                {
                    new() { Id = "part-1", BrandName = "foxtale", OfferTag = "Up to 50% Off", TargetUrl = "#beauty-products-catalog", ImageUrl = "/images/beauty-hub/partner-foxtale.webp" },
                    new() { Id = "part-2", BrandName = "Parachute Advansed", OfferTag = "Up to 50% Off", TargetUrl = "#beauty-products-catalog", ImageUrl = "/images/beauty-hub/partner-parachute.webp" },
                    new() { Id = "part-3", BrandName = "BEARDO", OfferTag = "Min. 60% Off", TargetUrl = "#beauty-products-catalog", ImageUrl = "/images/beauty-hub/partner-beardo.webp" }
                }
            },

            BrandNewInBeauty = new BeautyBrandNewDto
            {
                IsEnabled = true,
                SectionTitle = "Brand new in beauty!",
                Subtitle = "Exciting fresh drops, global beauty sensations and trending formulas",
                Cards = new List<BeautyBrandNewCardDto>
                {
                    new() { Id = "new-1", BrandName = "REAL TECHNIQUES", ProductTitle = "Makeup brushes", OfferTag = "Up to 25% Off", ImageUrl = "/images/beauty-hub/new-realtechniques.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "new-2", BrandName = "akind", ProductTitle = "Lip Gloss Oil", OfferTag = "Up to 25% Off", ImageUrl = "/images/beauty-hub/new-akind.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "new-3", BrandName = "essence", ProductTitle = "Blush", OfferTag = "Up to 25% Off", ImageUrl = "/images/beauty-hub/new-essence.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "new-4", BrandName = "N E X T", ProductTitle = "Attar & Perfume", OfferTag = "Up to 40% Off", ImageUrl = "/images/beauty-hub/new-next.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false },
                    new() { Id = "new-5", BrandName = "CeraVe", ProductTitle = "Brightening Care", OfferTag = "Up to 25% Off", ImageUrl = "/images/beauty-hub/new-cerave.webp", TargetUrl = "#beauty-products-catalog", IsUploadingSoon = false }
                }
            }
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
    public string OfferTag { get; set; } = "Min. 40% Off";
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
    public string TagBadge { get; set; } = string.Empty;
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
    public string SectionTitle { get; set; } = "Premium gifts";
    public string Subtitle { get; set; } = "Internationally renowned premium skincare, fragrances & salon haircare";
    public List<BeautyBrandCardDto> Brands { get; set; } = new();
}

public sealed class BeautyBrandCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string BrandName { get; set; } = string.Empty;
    public string DiscountTag { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyFestiveLooksDto
{
    public bool IsEnabled { get; set; } = true;
    public string SectionTitle { get; set; } = "NAIL EVERY FESTIVE LOOK";
    public string Subtitle { get; set; } = "Get runway ready with step-by-step festive trends & curated essentials";
    public List<BeautyFestiveLookCardDto> Cards { get; set; } = new();
}

public sealed class BeautyFestiveLookCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string LookName { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyLimitedDealsRibbonDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Limited Stock, Big Deals";
    public string DiscountHighlight { get; set; } = "Up to 75% Off";
    public string Subtitle { get; set; } = "Grab fast-moving festive bestsellers before inventory empties out";
    public string ButtonText { get; set; } = "Shop Deals →";
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyFlashDealsTimerDto
{
    public bool IsEnabled { get; set; } = true;
    public string TimerTitle { get; set; } = "New deals every 12 Hrs";
    public int TimerHours { get; set; } = 12;
    public string TimerEndIso { get; set; } = string.Empty;
    public List<BeautyFlashDealCardDto> Cards { get; set; } = new();
}

public sealed class BeautyFlashDealCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string OfferTag { get; set; } = "Up to 70% Off";
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyPicksUnder99Dto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Beauty Picks";
    public string PriceHighlight { get; set; } = "Under ₹99";
    public string Subtitle { get; set; } = "Pocket-friendly daily self-care essentials";
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyBiggestBrandsDto
{
    public bool IsEnabled { get; set; } = true;
    public string SectionTitle { get; set; } = "Biggest brands on offer";
    public string Subtitle { get; set; } = "Explore verified authentic products with direct brand warranties";
    public List<BeautyBigBrandCardDto> Brands { get; set; } = new();
}

public sealed class BeautyBigBrandCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string BrandName { get; set; } = string.Empty;
    public string OfferTag { get; set; } = "Up to 60% Off";
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class BeautyPartnersDto
{
    public bool IsEnabled { get; set; } = true;
    public string SectionTitle { get; set; } = "Beauty partner";
    public List<BeautyPartnerItemDto> Items { get; set; } = new();
}

public sealed class BeautyPartnerItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string BrandName { get; set; } = string.Empty;
    public string OfferTag { get; set; } = "Up to 50% Off";
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public string? ImageUrl { get; set; }
}

public sealed class BeautyBrandNewDto
{
    public bool IsEnabled { get; set; } = true;
    public string SectionTitle { get; set; } = "Brand new in beauty!";
    public string Subtitle { get; set; } = "Exciting fresh drops, global beauty sensations and trending formulas";
    public List<BeautyBrandNewCardDto> Cards { get; set; } = new();
}

public sealed class BeautyBrandNewCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string BrandName { get; set; } = string.Empty;
    public string ProductTitle { get; set; } = string.Empty;
    public string OfferTag { get; set; } = "Up to 25% Off";
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = "#beauty-products-catalog";
    public bool IsUploadingSoon { get; set; } = false;
}

