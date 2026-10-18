namespace Dreamflykit.Application.StudyHub.Models;

public sealed class StudyHubDto
{
    public StudyHubHeroDto HeroBanner { get; set; } = new();
    public List<StudyHubTrustItemDto> TrustItems { get; set; } = new();

    public StudyHubSection1Dto Section1 { get; set; } = new();
    public StudyHubSection2Dto Section2 { get; set; } = new();
    public StudyHubSection3DetailDto Section3Detail { get; set; } = new();
    public StudyHubSection3Dto Section3 { get; set; } = new(); // Backward compatibility
    public StudyHubSection4ChecklistDto Section4 { get; set; } = new();
    public StudyHubSection5DestinationDto Section5 { get; set; } = new();
    public StudyHubSection6BuilderDto Section6 { get; set; } = new();
    public StudyHubSection7BestSellersDto Section7 { get; set; } = new();
    public StudyHubSection8SetupDto Section8 { get; set; } = new();
    public StudyHubSection9WhyUsDto Section9 { get; set; } = new();
    public StudyHubSection10FaqDto Section10 { get; set; } = new();
    public StudyHubBottomBannerDto BottomBanner { get; set; } = new();

    public StudyHubCategoriesPageDto CategoriesPage { get; set; } = new();

    public static StudyHubDto CreateDefault()
    {
        var dto = new StudyHubDto
        {
            HeroBanner = new StudyHubHeroDto
            {
                PillText = "STUDY ESSENTIALS",
                Title = "Everything You Need for Your Global Journey",
                Subtitle = "Study smarter. Travel better. Build your future.",
                ButtonText = "Explore Study Essentials →",
                ButtonUrl = "/category/study-essentials",
                ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                SecondaryBadge = "New Country New Opportunities Same You! ✈️"
            },
            TrustItems = new List<StudyHubTrustItemDto>
            {
                new() { Title = "Quality Products", Subtitle = "Handpicked for Students", Icon = "quality" },
                new() { Title = "Affordable Bundles", Subtitle = "Save More, Study More", Icon = "bundle" },
                new() { Title = "Secure Checkout", Subtitle = "Multiple Payment Options", Icon = "lock" },
                new() { Title = "Fast & Reliable Delivery", Subtitle = "To Your Doorstep", Icon = "truck" }
            },
            Section1 = new StudyHubSection1Dto
            {
                Title = "Shop by Category",
                Subtitle = "Find everything you need, all in one place.",
                ViewAllLinkText = "View All Categories →",
                ViewAllLinkUrl = "/category/study-essentials",
                TitleFontSize = "large",
                Cards = new List<StudyHubCardDto>
                {
                    new()
                    {
                        Id = "cat-stationery",
                        Title = "Stationary & study supplies",
                        Description = "Notebooks, pens, highlighters and daily university study gear.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        LinkUrl = "/category/study-essentials?subcategory=stationery-study-supplies",
                        IconType = "academic"
                    },
                    new()
                    {
                        Id = "cat-tech",
                        Title = "Tech for study",
                        Description = "Calculators, headphones, hard drives, USB hubs & power accessories.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        LinkUrl = "/category/study-essentials?subcategory=tech-for-study",
                        IconType = "digital"
                    },
                    new()
                    {
                        Id = "cat-documents",
                        Title = "Document & organization",
                        Description = "Passport wallets, secure visa files, portfolios & document vaults.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        LinkUrl = "/category/study-essentials?subcategory=document-organization",
                        IconType = "passport"
                    },
                    new()
                    {
                        Id = "cat-desk",
                        Title = "Desk & Room study essential",
                        Description = "Desk lamps, cable organizers, organizers & study room comfort.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        LinkUrl = "/category/study-essentials?subcategory=desk-room-study-essential",
                        IconType = "lifestyle"
                    },
                    new()
                    {
                        Id = "cat-campus",
                        Title = "College & Campus Essentials",
                        Description = "Ergonomic student backpacks, water bottles, ID holders & locks.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        LinkUrl = "/category/study-essentials?subcategory=college-campus-essentials",
                        IconType = "academic"
                    },
                    new()
                    {
                        Id = "cat-online",
                        Title = "Online Classes & Productivity",
                        Description = "HD webcams, noise-canceling mic headsets, planners & study timers.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        LinkUrl = "/category/study-essentials?subcategory=online-classes-productivity",
                        IconType = "digital"
                    }
                }
            },
            Section2 = new StudyHubSection2Dto
            {
                Title = "Our Study Essentials Kits",
                Subtitle = "Complete bundles for every student's journey.",
                TitleFontSize = "large",
                ViewAllLinkText = "View All Kits →",
                ViewAllLinkUrl = "#kits-section",
                Kits = new List<StudyHubKitDto>
                {
                    new()
                    {
                        Id = "kit-basic",
                        Title = "Basic Study Kit",
                        BadgeText = "Budget-friendly starter kit",
                        BadgeVariant = "green",
                        Description = "Perfect for getting started with your studies abroad.",
                        Features = new List<string>
                        {
                            "2 Notebooks",
                            "3-5 Pens",
                            "2 Highlighters",
                            "Sticky Notes",
                            "Pencil Pouch",
                            "Document Folder"
                        },
                        PriceDisplay = "₹499 - ₹999",
                        Price = 499,
                        ImageUrl = "/images/study-hub/kit-basic.png",
                        ButtonText = "View Kit →",
                        TargetUrl = "/category/study-essentials"
                    },
                    new()
                    {
                        Id = "kit-premium",
                        Title = "Premium Study Kit",
                        BadgeText = "Most Popular",
                        BadgeVariant = "green",
                        Description = "Best for DreamFlyKit's main bundle.",
                        Features = new List<string>
                        {
                            "Basic Study Kit items",
                            "Academic Planner",
                            "Laptop Sleeve",
                            "Wireless Mouse",
                            "USB Hub / Flash Drive",
                            "Cable Organizer",
                            "Passport / Document Organizer"
                        },
                        PriceDisplay = "₹1,499 - ₹2,999",
                        Price = 1499,
                        ImageUrl = "/images/study-hub/kit-premium.png",
                        ButtonText = "View Kit →",
                        TargetUrl = "/category/study-essentials"
                    },
                    new()
                    {
                        Id = "kit-complete",
                        Title = "Complete Abroad Student Kit",
                        BadgeText = "All-in-One",
                        BadgeVariant = "blue",
                        Description = "All-in-one study and travel bundle.",
                        Features = new List<string>
                        {
                            "Premium Study Kit items",
                            "Travel Adapter (destination specific)",
                            "Power Bank",
                            "Backpack",
                            "Luggage Tags",
                            "Water Bottle",
                            "Additional Document Pouch"
                        },
                        PriceDisplay = "₹2,999 - ₹5,999+",
                        Price = 2999,
                        ImageUrl = "/images/study-hub/kit-complete.png",
                        ButtonText = "View Kit →",
                        TargetUrl = "/category/study-essentials"
                    }
                }
            },
            Section3Detail = new StudyHubSection3DetailDto
            {
                Title = "Key Study Essentials - In Detail",
                Subtitle = "Everything you need, category-wise",
                TitleFontSize = "large",
                Cards = new List<StudyHubDetailCardDto>
                {
                    new()
                    {
                        Id = "detail-stationery",
                        Title = "Stationery & Study Supplies",
                        Description = "Notebooks, pens, highlighters, more...",
                        IconType = "pen",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        LinkUrl = "/category/study-essentials?subcategory=stationery-study-supplies"
                    },
                    new()
                    {
                        Id = "detail-electronics",
                        Title = "Electronics & Digital Tools",
                        Description = "Laptops, mouse, headphones, USB hub...",
                        IconType = "laptop",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        LinkUrl = "/category/study-essentials?subcategory=tech-for-study"
                    },
                    new()
                    {
                        Id = "detail-documents",
                        Title = "Documents & Organization",
                        Description = "Passport holder, folders, file organizers...",
                        IconType = "file",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        LinkUrl = "/category/study-essentials?subcategory=document-organization"
                    },
                    new()
                    {
                        Id = "detail-desk",
                        Title = "Desk & Room Essentials",
                        Description = "Planner, lamp, water bottle, organizers...",
                        IconType = "lamp",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        LinkUrl = "/category/study-essentials?subcategory=desk-room-study-essential"
                    },
                    new()
                    {
                        Id = "detail-campus",
                        Title = "College & Campus Essentials",
                        Description = "Backpack, lunch box, ID holder...",
                        IconType = "bag",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        LinkUrl = "/category/study-essentials?subcategory=college-campus-essentials"
                    },
                    new()
                    {
                        Id = "detail-online",
                        Title = "Online Classes & Productivity",
                        Description = "Webcam, microphone, external drive...",
                        IconType = "monitor",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        LinkUrl = "/category/study-essentials?subcategory=online-classes-productivity"
                    }
                }
            },
            Section4 = new StudyHubSection4ChecklistDto
            {
                Title = "Abroad Packing Checklist",
                BadgeText = "Highly Recommended",
                Subtitle = "Make sure you don't forget anything important.",
                ButtonText = "View Missing Essentials →",
                ButtonUrl = "/category/study-essentials",
                ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                Items = new List<StudyHubChecklistItemDto>
                {
                    new() { Id = "chk-1", Text = "Study Materials", IsChecked = true, TargetUrl = "/category/study-essentials?subcategory=stationery-study-supplies" },
                    new() { Id = "chk-2", Text = "Electronics & Accessories", IsChecked = true, TargetUrl = "/category/study-essentials?subcategory=tech-for-study" },
                    new() { Id = "chk-3", Text = "Travel Essentials", IsChecked = false, TargetUrl = "/category/travel-gear" },
                    new() { Id = "chk-4", Text = "Documents & Organizers", IsChecked = false, TargetUrl = "/category/study-essentials?subcategory=document-organization" },
                    new() { Id = "chk-5", Text = "Personal Essentials", IsChecked = false, TargetUrl = "/category/room-living" }
                }
            },
            Section5 = new StudyHubSection5DestinationDto
            {
                Title = "Shop by Destination",
                Subtitle = "Get country-specific essentials and travel adapters.",
                ButtonText = "Explore Essentials by Country →",
                ButtonUrl = "/category/study-essentials",
                Countries = new List<StudyHubCountryDto>
                {
                    new() { Id = "usa", Name = "USA", Code = "US", FlagUrl = "/images/countries/usa.webp", ImageUrl = "/images/study-hub/test-pic-placeholder.svg", LinkUrl = "/category/study-essentials?country=usa" },
                    new() { Id = "uk", Name = "UK", Code = "GB", FlagUrl = "/images/countries/uk.webp", ImageUrl = "/images/study-hub/test-pic-placeholder.svg", LinkUrl = "/category/study-essentials?country=uk" },
                    new() { Id = "canada", Name = "Canada", Code = "CA", FlagUrl = "/images/countries/canada.webp", ImageUrl = "/images/study-hub/test-pic-placeholder.svg", LinkUrl = "/category/study-essentials?country=canada" },
                    new() { Id = "australia", Name = "Australia", Code = "AU", FlagUrl = "/images/countries/australia.webp", ImageUrl = "/images/study-hub/test-pic-placeholder.svg", LinkUrl = "/category/study-essentials?country=australia" },
                    new() { Id = "france", Name = "France", Code = "FR", FlagUrl = "/images/countries/france.webp", ImageUrl = "/images/study-hub/test-pic-placeholder.svg", LinkUrl = "/category/study-essentials?country=france" }
                }
            },
            Section6 = new StudyHubSection6BuilderDto
            {
                StepNumber = "6.",
                Title = "Build Your Own Study Kit",
                BadgeText = "Customize Your Perfect Kit",
                Subtitle = "Choose what you need and create your own bundle.",
                ButtonText = "Build My Study Kit →",
                TargetUrl = "/cart",
                Items = new List<StudyHubCustomItemDto>
                {
                    new() { Id = "item-notebook", Name = "Notebook", Price = 100, ImageUrl = "/images/study-hub/item-notebook.png", IsSelected = false, SortOrder = 1 },
                    new() { Id = "item-pens", Name = "Pen Set", Price = 150, ImageUrl = "/images/study-hub/item-pens.png", IsSelected = false, SortOrder = 2 },
                    new() { Id = "item-sleeve", Name = "Laptop Sleeve", Price = 499, ImageUrl = "/images/study-hub/item-sleeve.png", IsSelected = false, SortOrder = 3 },
                    new() { Id = "item-organizer", Name = "Document Organizer", Price = 199, ImageUrl = "/images/study-hub/item-organizer.png", IsSelected = false, SortOrder = 4 },
                    new() { Id = "item-planner", Name = "Study Planner", Price = 149, ImageUrl = "/images/study-hub/item-planner.png", IsSelected = false, SortOrder = 5 },
                    new() { Id = "item-adapter", Name = "Universal Adapter", Price = 799, ImageUrl = "/images/study-hub/test-pic-placeholder.svg", IsSelected = false, SortOrder = 6 }
                }
            },
            Section7 = new StudyHubSection7BestSellersDto
            {
                Title = "Best Sellers & Student Favorites",
                Subtitle = "Top products loved by students worldwide.",
                Products = new List<StudyHubBestSellerDto>
                {
                    new() { Id = "bs-sleeve", Name = "Laptop Sleeve", Price = 1299, OriginalPrice = 1799, Rating = 4.8m, ReviewsCount = 142, ImageUrl = "/images/study-hub/item-sleeve.png", LinkUrl = "/category/study-essentials", ButtonText = "Add to Cart" },
                    new() { Id = "bs-mouse", Name = "Wireless Mouse", Price = 899, OriginalPrice = 1299, Rating = 4.7m, ReviewsCount = 98, ImageUrl = "/images/study-hub/test-pic-placeholder.svg", LinkUrl = "/category/study-essentials", ButtonText = "Add to Cart" },
                    new() { Id = "bs-adapter", Name = "Travel Adapter", Price = 1499, OriginalPrice = 1999, Rating = 4.9m, ReviewsCount = 310, ImageUrl = "/images/study-hub/test-pic-placeholder.svg", LinkUrl = "/category/study-essentials", ButtonText = "Add to Cart" },
                    new() { Id = "bs-backpack", Name = "Campus Backpack", Price = 2499, OriginalPrice = 3499, Rating = 4.9m, ReviewsCount = 275, ImageUrl = "/images/study-hub/test-pic-placeholder.svg", LinkUrl = "/category/study-essentials", ButtonText = "Add to Cart" },
                    new() { Id = "bs-organizer", Name = "Document Organizer", Price = 799, OriginalPrice = 1199, Rating = 4.6m, ReviewsCount = 89, ImageUrl = "/images/study-hub/item-organizer.png", LinkUrl = "/category/study-essentials", ButtonText = "Add to Cart" }
                }
            },
            Section8 = new StudyHubSection8SetupDto
            {
                Title = "Smart Study Setup",
                Subtitle = "Create a productive and comfortable study space.",
                ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                ButtonText = "Shop Study Setup →",
                ButtonUrl = "/category/study-essentials",
                Items = new List<StudyHubSetupItemDto>
                {
                    new() { Id = "st-stand", Name = "Laptop Stand", Price = 1299, IconType = "laptop" },
                    new() { Id = "st-organizer", Name = "Desk Organizer", Price = 689, IconType = "file" },
                    new() { Id = "st-lamp", Name = "Study Lamp", Price = 1099, IconType = "lamp" },
                    new() { Id = "st-headphones", Name = "Headphones", Price = 1499, IconType = "headphones" },
                    new() { Id = "st-planner", Name = "Notebook & Planner", Price = 499, IconType = "pen" },
                    new() { Id = "st-cable", Name = "Cable Organizer", Price = 399, IconType = "cable" }
                }
            },
            Section9 = new StudyHubSection9WhyUsDto
            {
                Title = "Why Choose DreamFlyKit?",
                Subtitle = "More than a store — we're your study partner.",
                Pillars = new List<StudyHubPillarDto>
                {
                    new() { Id = "p-bundles", Title = "Student-Friendly Bundles", Description = "Curated specifically for international student needs with massive bundle discounts.", IconType = "gift" },
                    new() { Id = "p-easy", Title = "Easy Shopping", Description = "Everything in one place — avoid multiple shipments and missing university items.", IconType = "check" },
                    new() { Id = "p-destination", Title = "Destination-Based Essentials", Description = "Country-specific plug adapters, luggage specifications, and university-aligned kits.", IconType = "globe" },
                    new() { Id = "p-delivery", Title = "Convenient Delivery", Description = "On-time doorstep delivery right before your flight or directly to your campus dorm.", IconType = "truck" }
                }
            },
            Section10 = new StudyHubSection10FaqDto
            {
                Title = "Frequently Asked Questions",
                Subtitle = "Quick answers to help you shop with confidence.",
                Faqs = new List<StudyHubFaqItemDto>
                {
                    new()
                    {
                        Id = "faq-1",
                        Question = "What should I buy before studying abroad?",
                        Answer = "Focus on university-specified stationery, universal destination power adapters, a reliable laptop sleeve, waterproof document portfolios for visa/passport files, and compact study room essentials."
                    },
                    new()
                    {
                        Id = "faq-2",
                        Question = "Can I customize my study kit?",
                        Answer = "Yes! Use our 'Build Your Own Study Kit' interactive builder in Section 6 to choose the exact components you need with live price calculation."
                    },
                    new()
                    {
                        Id = "faq-3",
                        Question = "Can I purchase products individually?",
                        Answer = "Yes, all products featured in our kits and setups can be ordered individually directly from our Study Essentials catalog."
                    },
                    new()
                    {
                        Id = "faq-4",
                        Question = "Do you deliver internationally?",
                        Answer = "Yes, Dreamflykit delivers to dorms, apartments, and student residences across the USA, UK, Canada, Australia, Europe, and India."
                    },
                    new()
                    {
                        Id = "faq-5",
                        Question = "What are the return and refund policies?",
                        Answer = "We offer a 30-day student satisfaction guarantee with no-hassle replacements or full refunds on any transit-damaged or unsuitable items."
                    }
                }
            },
            BottomBanner = new StudyHubBottomBannerDto
            {
                Title = "Your Global Journey Starts Here.",
                Subtitle = "Prepare for your next chapter with the right study essentials from DreamFlyKit.",
                ButtonText = "Shop Study Essentials →",
                ButtonUrl = "/category/study-essentials",
                ImageUrl = "/images/study-hub/test-pic-placeholder.svg"
            },
            CategoriesPage = new StudyHubCategoriesPageDto
            {
                HeroTitle = "Study Essentials",
                HeroSubtitle = "Everything you need to study smart, stay organized and ace your academic journey abroad.",
                HeroImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                Badges = new List<string>
                {
                    "Student Trusted • 10K+ Happy Students",
                    "Curated for Abroad • Universities Worldwide",
                    "Smart & Affordable • Best Value Essentials"
                },
                Categories = new List<StudyHubCategoryCardDto>
                {
                    new()
                    {
                        Id = "cat-1",
                        Name = "Stationary & study supplies",
                        Slug = "stationery-study-supplies",
                        Description = "Pens, pencils, highlighters, notebooks, sticky notes and more for everyday writing.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        Icon = "pen",
                        ProductCount = 24
                    },
                    new()
                    {
                        Id = "cat-2",
                        Name = "Tech for study",
                        Slug = "tech-for-study",
                        Description = "Calculators, headphones, USB drives, laptop stands, mice & digital study tools.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        Icon = "laptop",
                        ProductCount = 18
                    },
                    new()
                    {
                        Id = "cat-3",
                        Name = "Document & organization",
                        Slug = "document-organization",
                        Description = "Folders, file organizers, passport holders, dividers and safe certificates storage.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        Icon = "file",
                        ProductCount = 15
                    },
                    new()
                    {
                        Id = "cat-4",
                        Name = "Desk & Room study essential",
                        Slug = "desk-room-study-essential",
                        Description = "Lamps, desk organizers, whiteboards, study clocks and room desk must-haves.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        Icon = "lamp",
                        ProductCount = 22
                    },
                    new()
                    {
                        Id = "cat-5",
                        Name = "College & Campus Essentials",
                        Slug = "college-campus-essentials",
                        Description = "Backpacks, water bottles, lunch boxes, ID card holders and campus everyday gear.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        Icon = "bag",
                        ProductCount = 19
                    },
                    new()
                    {
                        Id = "cat-6",
                        Name = "Online Classes & Productivity",
                        Slug = "online-classes-productivity",
                        Description = "Webcams, conference mics, semester planners, timers & study focus gear.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        Icon = "monitor",
                        ProductCount = 14
                    },
                    new()
                    {
                        Id = "cat-7",
                        Name = "Exam preparation ( Gre ,tofel ,)",
                        Slug = "exam-preparation",
                        Description = "GRE, TOEFL, IELTS test prep books, practice sets, vocab cards and guides.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        Icon = "book",
                        ProductCount = 16
                    },
                    new()
                    {
                        Id = "cat-8",
                        Name = "Books & references",
                        Slug = "books-references",
                        Description = "Academic textbooks, reference guides, course literature, dictionaries and eBooks.",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        Icon = "bookmark",
                        ProductCount = 21
                    }
                },
                Promo1Title = "Need a Complete Study Kit?",
                Promo1Subtitle = "Get everything you need in one smart kit.",
                Promo1ButtonText = "Explore Study Kits →",
                Promo1ButtonUrl = "/study-abroad-hub#kits-section",
                Promo1ImageUrl = "/images/study-hub/kit-premium.png",
                Promo2Title = "Need Help Choosing?",
                Promo2Subtitle = "We're here to help you pick the right essentials.",
                Promo2ButtonText = "Chat with Us",
                Promo2ButtonUrl = "https://wa.me/919123456789",
                FooterFeatures = new List<string>
                {
                    "Fast & Reliable Delivery • On-time delivery worldwide",
                    "100% Secure Payments • Multiple payment options",
                    "Premium Quality Products • Genuine & durable",
                    "24/7 Customer Support • We're here to help",
                    "Easy Returns • Hassle-free returns"
                }
            }
        };

        // Populate legacy Section3 items for backward compatibility
        dto.Section3 = new StudyHubSection3Dto
        {
            StepNumber = dto.Section6.StepNumber,
            Title = dto.Section6.Title,
            BadgeText = dto.Section6.BadgeText,
            Subtitle = dto.Section6.Subtitle,
            ButtonText = dto.Section6.ButtonText,
            TargetUrl = dto.Section6.TargetUrl,
            Items = dto.Section6.Items
        };

        return dto;
    }
}

// -------------------------------------------------------------
// DTOs for Section 1 - 10 + Hero + Categories Page
// -------------------------------------------------------------

public sealed class StudyHubHeroDto
{
    public string PillText { get; set; } = "STUDY ESSENTIALS";
    public string Title { get; set; } = "Everything You Need for Your Global Journey";
    public string Subtitle { get; set; } = "Study smarter. Travel better. Build your future.";
    public string ButtonText { get; set; } = "Explore Study Essentials →";
    public string ButtonUrl { get; set; } = "/category/study-essentials";
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public string SecondaryBadge { get; set; } = "New Country New Opportunities Same You! ✈️";
}

public sealed class StudyHubTrustItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Icon { get; set; } = "quality";
}

public sealed class StudyHubSection1Dto
{
    public string Title { get; set; } = "Shop by Category";
    public string Subtitle { get; set; } = "Find everything you need, all in one place.";
    public string ViewAllLinkText { get; set; } = "View All Categories →";
    public string ViewAllLinkUrl { get; set; } = "/category/study-essentials";
    public string BadgeText { get; set; } = "Be Ready for New Adventures ✈️";
    public string TitleFontSize { get; set; } = "large";
    public List<StudyHubCardDto> Cards { get; set; } = new();
}

public sealed class StudyHubCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public string LinkUrl { get; set; } = "/category/study-essentials";
    public string IconType { get; set; } = "academic";
}

public sealed class StudyHubSection2Dto
{
    public string Title { get; set; } = "Our Study Essentials Kits";
    public string Subtitle { get; set; } = "Complete bundles for every student's journey.";
    public string TitleFontSize { get; set; } = "large";
    public string ViewAllLinkText { get; set; } = "View All Kits →";
    public string ViewAllLinkUrl { get; set; } = "#kits-section";
    public List<StudyHubKitDto> Kits { get; set; } = new();
}

public sealed class StudyHubKitDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string BadgeText { get; set; } = "Recommended";
    public string BadgeVariant { get; set; } = "green"; // green, blue, amber, purple
    public string Description { get; set; } = string.Empty;
    public List<string> Features { get; set; } = new();
    public string PriceDisplay { get; set; } = "₹499 - ₹999";
    public decimal Price { get; set; } = 499;
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public string ButtonText { get; set; } = "View Kit →";
    public string TargetUrl { get; set; } = "/category/study-essentials";
}

public sealed class StudyHubSection3DetailDto
{
    public string Title { get; set; } = "Key Study Essentials - In Detail";
    public string Subtitle { get; set; } = "Everything you need, category-wise";
    public string TitleFontSize { get; set; } = "large";
    public List<StudyHubDetailCardDto> Cards { get; set; } = new();
}

public sealed class StudyHubDetailCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconType { get; set; } = "pen";
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public string LinkUrl { get; set; } = "/category/study-essentials";
}

public sealed class StudyHubSection3Dto
{
    public string StepNumber { get; set; } = "6.";
    public string Title { get; set; } = "Build Your Own Study Kit";
    public string BadgeText { get; set; } = "Customize Your Perfect Kit";
    public string Subtitle { get; set; } = "Choose what you need and create your own bundle.";
    public string TitleFontSize { get; set; } = "large";
    public string ButtonText { get; set; } = "Build My Study Kit →";
    public string TargetUrl { get; set; } = "/cart";
    public List<StudyHubCustomItemDto> Items { get; set; } = new();
}

public sealed class StudyHubCustomItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public bool IsSelected { get; set; } = false;
    public int SortOrder { get; set; } = 0;
}

public sealed class StudyHubSection4ChecklistDto
{
    public string Title { get; set; } = "Abroad Packing Checklist";
    public string BadgeText { get; set; } = "Highly Recommended";
    public string Subtitle { get; set; } = "Make sure you don't forget anything important.";
    public string TitleFontSize { get; set; } = "large";
    public string ButtonText { get; set; } = "View Missing Essentials →";
    public string ButtonUrl { get; set; } = "/category/study-essentials";
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public List<StudyHubChecklistItemDto> Items { get; set; } = new();
}

public sealed class StudyHubChecklistItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Text { get; set; } = string.Empty;
    public bool IsChecked { get; set; } = false;
    public string TargetUrl { get; set; } = "/category/study-essentials";
}

public sealed class StudyHubSection5DestinationDto
{
    public string Title { get; set; } = "Shop by Destination";
    public string Subtitle { get; set; } = "Get country-specific essentials and travel adapters.";
    public string TitleFontSize { get; set; } = "large";
    public string ButtonText { get; set; } = "Explore Essentials by Country →";
    public string ButtonUrl { get; set; } = "/category/study-essentials";
    public List<StudyHubCountryDto> Countries { get; set; } = new();
}

public sealed class StudyHubCountryDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string FlagUrl { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public string LinkUrl { get; set; } = "/category/study-essentials";
}

public sealed class StudyHubSection6BuilderDto
{
    public string StepNumber { get; set; } = "6.";
    public string Title { get; set; } = "Build Your Own Study Kit";
    public string BadgeText { get; set; } = "Customize Your Perfect Kit";
    public string Subtitle { get; set; } = "Choose what you need and create your own bundle.";
    public string TitleFontSize { get; set; } = "large";
    public string ButtonText { get; set; } = "Build My Study Kit →";
    public string TargetUrl { get; set; } = "/cart";
    public List<StudyHubCustomItemDto> Items { get; set; } = new();
}

public sealed class StudyHubSection7BestSellersDto
{
    public string Title { get; set; } = "Best Sellers & Student Favorites";
    public string Subtitle { get; set; } = "Top products loved by students worldwide.";
    public string TitleFontSize { get; set; } = "large";
    public List<StudyHubBestSellerDto> Products { get; set; } = new();
}

public sealed class StudyHubBestSellerDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public decimal OriginalPrice { get; set; } = 0;
    public decimal Rating { get; set; } = 4.8m;
    public int ReviewsCount { get; set; } = 100;
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public string LinkUrl { get; set; } = "/category/study-essentials";
    public string ButtonText { get; set; } = "Add to Cart";
}

public sealed class StudyHubSection8SetupDto
{
    public string Title { get; set; } = "Smart Study Setup";
    public string Subtitle { get; set; } = "Create a productive and comfortable study space.";
    public string TitleFontSize { get; set; } = "large";
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public string ButtonText { get; set; } = "Shop Study Setup →";
    public string ButtonUrl { get; set; } = "/category/study-essentials";
    public List<StudyHubSetupItemDto> Items { get; set; } = new();
}

public sealed class StudyHubSetupItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public string IconType { get; set; } = "laptop";
}

public sealed class StudyHubSection9WhyUsDto
{
    public string Title { get; set; } = "Why Choose DreamFlyKit?";
    public string Subtitle { get; set; } = "More than a store — we're your study partner.";
    public string TitleFontSize { get; set; } = "large";
    public List<StudyHubPillarDto> Pillars { get; set; } = new();
}

public sealed class StudyHubPillarDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconType { get; set; } = "gift";
}

public sealed class StudyHubSection10FaqDto
{
    public string Title { get; set; } = "Frequently Asked Questions";
    public string Subtitle { get; set; } = "Quick answers to help you shop with confidence.";
    public string TitleFontSize { get; set; } = "large";
    public List<StudyHubFaqItemDto> Faqs { get; set; } = new();
}

public sealed class StudyHubFaqItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public bool IsOpen { get; set; } = false;
}

public sealed class StudyHubBottomBannerDto
{
    public string Title { get; set; } = "Your Global Journey Starts Here.";
    public string Subtitle { get; set; } = "Prepare for your next chapter with the right study essentials from DreamFlyKit.";
    public string ButtonText { get; set; } = "Shop Study Essentials →";
    public string ButtonUrl { get; set; } = "/category/study-essentials";
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
}

public sealed class StudyHubCategoriesPageDto
{
    public string HeroTitle { get; set; } = "Study Essentials";
    public string HeroSubtitle { get; set; } = "Everything you need to study smart, stay organized and ace your academic journey abroad.";
    public string HeroImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public List<string> Badges { get; set; } = new();
    public List<StudyHubCategoryCardDto> Categories { get; set; } = new();

    public string Promo1Title { get; set; } = "Need a Complete Study Kit?";
    public string Promo1Subtitle { get; set; } = "Get everything you need in one smart kit.";
    public string Promo1ButtonText { get; set; } = "Explore Study Kits →";
    public string Promo1ButtonUrl { get; set; } = "/study-abroad-hub#kits-section";
    public string Promo1ImageUrl { get; set; } = "/images/study-hub/kit-premium.png";

    public string Promo2Title { get; set; } = "Need Help Choosing?";
    public string Promo2Subtitle { get; set; } = "We're here to help you pick the right essentials.";
    public string Promo2ButtonText { get; set; } = "Chat with Us";
    public string Promo2ButtonUrl { get; set; } = "https://wa.me/919123456789";

    public List<string> FooterFeatures { get; set; } = new();
}

public sealed class StudyHubCategoryCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public string Icon { get; set; } = "pen";
    public int ProductCount { get; set; } = 0;
}
