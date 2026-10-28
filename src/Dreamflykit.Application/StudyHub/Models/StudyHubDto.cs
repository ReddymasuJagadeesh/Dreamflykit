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
    public AbroadChecklistPageDto ChecklistPage { get; set; } = new();
    public EducationLoansPageDto EducationLoansPage { get; set; } = new();
    public ConsultancyDetailsPageDto ConsultancyDetailsPage { get; set; } = new();
    public StudyAbroadHubProfileDto AbroadProfile { get; set; } = new();
    public List<StudyGuideDetailPageDto> GuidePages { get; set; } = new();

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
                SecondaryBadge = "New Country New Opportunities Same You! ✈️",
                AutoSlideIntervalSeconds = 4,
                Slides = new List<StudyHubHeroSlideDto>
                {
                    new()
                    {
                        Id = "hero-slide-1",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        SecondaryBadge = "New Country New Opportunities Same You! ✈️",
                        LinkUrl = "/category/study-essentials",
                        Title = "Study Essentials Global Journey"
                    },
                    new()
                    {
                        Id = "hero-slide-2",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        SecondaryBadge = "Curated Student Bundles & Starter Kits 🎓",
                        LinkUrl = "/category/study-essentials",
                        Title = "Curated Student Bundles"
                    },
                    new()
                    {
                        Id = "hero-slide-3",
                        ImageUrl = "/images/study-hub/test-pic-placeholder.svg",
                        SecondaryBadge = "Campus Gear & International Luggage 🧳",
                        LinkUrl = "/category/study-essentials",
                        Title = "Campus & Travel Essentials"
                    }
                }
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
                Title = "What Every Student Needs Before Going Abroad",
                Subtitle = "Prepare smarter, pack better and feel confident about your journey abroad.",
                BadgeText = "Be Ready for New Adventures ✈",
                TitleFontSize = "large",
                Cards = new List<StudyHubDetailCardDto>
                {
                    new()
                    {
                        Id = "abroad-study-ready",
                        Title = "Stay Study-Ready",
                        Description = "Keep your stationery, academic planner and essential study tools ready for your new university.",
                        IconType = "study",
                        ImageUrl = "/images/study-hub/card-study-ready.png",
                        LinkUrl = "/study-guide/stay-study-ready",
                        ButtonText = "Explore Essentials →"
                    },
                    new()
                    {
                        Id = "abroad-connected",
                        Title = "Stay Connected",
                        Description = "Prepare your laptop accessories, headphones, chargers and other digital essentials for your studies.",
                        IconType = "tech",
                        ImageUrl = "/images/study-hub/card-connected.png",
                        LinkUrl = "/study-guide/stay-connected",
                        ButtonText = "Explore Essentials →"
                    },
                    new()
                    {
                        Id = "abroad-documents",
                        Title = "Keep Documents Organized",
                        Description = "Keep your passport, visa documents, admission letter and academic certificates safely organized.",
                        IconType = "documents",
                        ImageUrl = "/images/study-hub/card-documents.png",
                        LinkUrl = "/study-guide/keep-documents-organized",
                        ButtonText = "Explore Essentials →"
                    },
                    new()
                    {
                        Id = "abroad-student-life",
                        Title = "Set Up Your Student Life",
                        Description = "Discover useful everyday essentials for your campus, accommodation and comfortable student life abroad.",
                        IconType = "life",
                        ImageUrl = "/images/study-hub/card-student-life.png",
                        LinkUrl = "/study-guide/set-up-your-student-life",
                        ButtonText = "Explore Essentials →"
                    }
                }
            },
            Section4 = new StudyHubSection4ChecklistDto
            {
                Title = "Abroad Packing Checklist",
                BadgeText = "Highly Recommended",
                Subtitle = "Make sure you don't forget anything important.",
                ButtonText = "View Full Checklist →",
                ButtonUrl = "/abroad-packing-checklist",
                ImageUrl = "/images/study-hub/checklist-hero.jpg",
                Items = new List<StudyHubChecklistItemDto>
                {
                    new() { Id = "chk-1", Text = "Study Materials", IsChecked = true, TargetUrl = "/abroad-packing-checklist" },
                    new() { Id = "chk-2", Text = "Electronics & Accessories", IsChecked = true, TargetUrl = "/abroad-packing-checklist" },
                    new() { Id = "chk-3", Text = "Travel Essentials", IsChecked = false, TargetUrl = "/abroad-packing-checklist" },
                    new() { Id = "chk-4", Text = "Documents & Organizers", IsChecked = false, TargetUrl = "/abroad-packing-checklist" },
                    new() { Id = "chk-5", Text = "Personal Essentials", IsChecked = false, TargetUrl = "/abroad-packing-checklist" }
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

        dto.ChecklistPage = CreateDefaultChecklistPage();
        dto.EducationLoansPage = CreateDefaultEducationLoansPage();
        dto.ConsultancyDetailsPage = CreateDefaultConsultancyDetailsPage();
        dto.AbroadProfile = CreateDefaultAbroadProfile();
        dto.GuidePages = CreateDefaultGuidePages();

        return dto;
    }

    public static AbroadChecklistPageDto CreateDefaultChecklistPage()
    {
        return new AbroadChecklistPageDto
        {
            HeroPill = "STUDY ESSENTIALS",
            HeroTitle = "Abroad Packing Checklist",
            HeroSubtitle = "Don't just pack. Pack smart!",
            HeroDescription = "Your complete checklist for a hassle-free journey abroad. From study essentials to travel must-haves, we've got you covered.",
            HeroBadgeText = "Pack Smart, Travel Far ✈",
            HeroImageUrl = "/images/study-hub/checklist-hero.jpg",
            SelectedDestination = "USA",
            ShopMissingButtonText = "Shop Missing Essentials →",
            ShopMissingButtonUrl = "/category/study-essentials",
            Categories = new List<ChecklistCategoryGroupDto>
            {
                new()
                {
                    Id = "cat-study",
                    Number = 1,
                    Title = "1. Study Essentials",
                    Subtitle = "6 of 10 items checked",
                    IconType = "study",
                    ImageUrl = "/images/study-hub/checklist-study.jpg",
                    ShopButtonText = "Shop Now →",
                    ShopButtonUrl = "/category/study-essentials?subcategory=stationery-study-supplies",
                    Items = new List<ChecklistGroupItemDto>
                    {
                        new() { Id = "s-1", Name = "Notebooks & Writing Pads", IsChecked = true },
                        new() { Id = "s-2", Name = "Pens, Pencils & Highlighters", IsChecked = true },
                        new() { Id = "s-3", Name = "Academic Planner", IsChecked = false },
                        new() { Id = "s-4", Name = "Calculator", IsChecked = true },
                        new() { Id = "s-5", Name = "Document Folders", IsChecked = false },
                        new() { Id = "s-6", Name = "Highlighter Set", IsChecked = true },
                        new() { Id = "s-7", Name = "Sticky Notes & Index Tabs", IsChecked = true },
                        new() { Id = "s-8", Name = "Desk Organizer", IsChecked = false },
                        new() { Id = "s-9", Name = "Backpack Laptop Sleeve", IsChecked = true },
                        new() { Id = "s-10", Name = "Correction Tape & Erasers", IsChecked = false }
                    }
                },
                new()
                {
                    Id = "cat-electronics",
                    Number = 2,
                    Title = "2. Electronics",
                    Subtitle = "4 of 8 items checked",
                    IconType = "tech",
                    ImageUrl = "/images/study-hub/checklist-electronics.jpg",
                    ShopButtonText = "Shop Now →",
                    ShopButtonUrl = "/category/study-essentials?subcategory=tech-for-study",
                    Items = new List<ChecklistGroupItemDto>
                    {
                        new() { Id = "e-1", Name = "Laptop & Mouse", IsChecked = true },
                        new() { Id = "e-2", Name = "Headphones / Earphones", IsChecked = true },
                        new() { Id = "e-3", Name = "Chargers & Cables", IsChecked = false },
                        new() { Id = "e-4", Name = "Power Bank", IsChecked = false },
                        new() { Id = "e-5", Name = "Universal Travel Adapter", IsChecked = false },
                        new() { Id = "e-6", Name = "External Hard Drive / SSD", IsChecked = true },
                        new() { Id = "e-7", Name = "Multi-Port USB Hub", IsChecked = true },
                        new() { Id = "e-8", Name = "Surge Protector Spike Guard", IsChecked = false }
                    }
                },
                new()
                {
                    Id = "cat-clothing",
                    Number = 3,
                    Title = "3. Clothing & Fashion",
                    Subtitle = "5 of 12 items checked",
                    IconType = "fashion",
                    ImageUrl = "/images/study-hub/checklist-clothing.jpg",
                    ShopButtonText = "Shop Now →",
                    ShopButtonUrl = "/category/study-essentials",
                    Items = new List<ChecklistGroupItemDto>
                    {
                        new() { Id = "c-1", Name = "T-shirts & Shirts", IsChecked = true },
                        new() { Id = "c-2", Name = "Jeans & Casual Wear", IsChecked = true },
                        new() { Id = "c-3", Name = "Jackets & Winter Wear", IsChecked = false },
                        new() { Id = "c-4", Name = "Innerwear & Thermals", IsChecked = false },
                        new() { Id = "c-5", Name = "Socks", IsChecked = false },
                        new() { Id = "c-6", Name = "Formal Suit / Blazers", IsChecked = true },
                        new() { Id = "c-7", Name = "Track Pants & Sweatshirts", IsChecked = true },
                        new() { Id = "c-8", Name = "Raincoat & Compact Umbrella", IsChecked = true },
                        new() { Id = "c-9", Name = "Belts & Traditional Attire", IsChecked = false },
                        new() { Id = "c-10", Name = "Beanie & Winter Gloves", IsChecked = false },
                        new() { Id = "c-11", Name = "Nightwear & Loungewear", IsChecked = false },
                        new() { Id = "c-12", Name = "Extra Pairs of Hangers", IsChecked = false }
                    }
                },
                new()
                {
                    Id = "cat-footwear",
                    Number = 4,
                    Title = "4. Footwear",
                    Subtitle = "2 of 4 items checked",
                    IconType = "footwear",
                    ImageUrl = "/images/study-hub/checklist-footwear.jpg",
                    ShopButtonText = "Shop Now →",
                    ShopButtonUrl = "/category/study-essentials",
                    Items = new List<ChecklistGroupItemDto>
                    {
                        new() { Id = "f-1", Name = "Casual Shoes", IsChecked = true },
                        new() { Id = "f-2", Name = "Formal Shoes", IsChecked = false },
                        new() { Id = "f-3", Name = "Sports Shoes", IsChecked = false },
                        new() { Id = "f-4", Name = "Slippers", IsChecked = false }
                    }
                },
                new()
                {
                    Id = "cat-care",
                    Number = 5,
                    Title = "5. Personal Care",
                    Subtitle = "3 of 8 items checked",
                    IconType = "care",
                    ImageUrl = "/images/study-hub/checklist-personal-care.jpg",
                    ShopButtonText = "Shop Now →",
                    ShopButtonUrl = "/category/study-essentials",
                    Items = new List<ChecklistGroupItemDto>
                    {
                        new() { Id = "p-1", Name = "Toothbrush & Toothpaste", IsChecked = true },
                        new() { Id = "p-2", Name = "Shampoo & Soap", IsChecked = true },
                        new() { Id = "p-3", Name = "Skincare Products", IsChecked = false },
                        new() { Id = "p-4", Name = "Grooming Essentials", IsChecked = false },
                        new() { Id = "p-5", Name = "Nail Clipper Kit", IsChecked = true },
                        new() { Id = "p-6", Name = "Hair Dryer / Trimmer", IsChecked = false },
                        new() { Id = "p-7", Name = "Sunscreen & Moisturizer", IsChecked = false },
                        new() { Id = "p-8", Name = "Quick-Dry Towels", IsChecked = false }
                    }
                },
                new()
                {
                    Id = "cat-travel",
                    Number = 6,
                    Title = "6. Travel Essentials",
                    Subtitle = "4 of 8 items checked",
                    IconType = "travel",
                    ImageUrl = "/images/study-hub/checklist-travel.jpg",
                    ShopButtonText = "Shop Now →",
                    ShopButtonUrl = "/category/study-essentials",
                    Items = new List<ChecklistGroupItemDto>
                    {
                        new() { Id = "t-1", Name = "Backpack", IsChecked = true },
                        new() { Id = "t-2", Name = "Suitcase", IsChecked = false },
                        new() { Id = "t-3", Name = "Travel Pillow", IsChecked = false },
                        new() { Id = "t-4", Name = "Water Bottle", IsChecked = false },
                        new() { Id = "t-5", Name = "Luggage Tags", IsChecked = false },
                        new() { Id = "t-6", Name = "TSA Approved Locks", IsChecked = true },
                        new() { Id = "t-7", Name = "Luggage Weighing Scale", IsChecked = true },
                        new() { Id = "t-8", Name = "Eye Mask & Earplugs", IsChecked = true }
                    }
                },
                new()
                {
                    Id = "cat-documents",
                    Number = 7,
                    Title = "7. Important Documents",
                    Subtitle = "5 of 7 items checked",
                    IconType = "documents",
                    ImageUrl = "/images/study-hub/checklist-documents.jpg",
                    ShopButtonText = "Shop Now →",
                    ShopButtonUrl = "/category/study-essentials?subcategory=document-organization",
                    Items = new List<ChecklistGroupItemDto>
                    {
                        new() { Id = "d-1", Name = "Passport", IsChecked = true },
                        new() { Id = "d-2", Name = "Student Visa", IsChecked = true },
                        new() { Id = "d-3", Name = "University Admission Letter", IsChecked = true },
                        new() { Id = "d-4", Name = "Academic Certificates", IsChecked = false },
                        new() { Id = "d-5", Name = "Insurance Documents", IsChecked = false },
                        new() { Id = "d-6", Name = "Flight Tickets & Itinerary", IsChecked = true },
                        new() { Id = "d-7", Name = "Passport Size Photographs", IsChecked = true }
                    }
                },
                new()
                {
                    Id = "cat-living",
                    Number = 8,
                    Title = "8. Living & Room Essentials",
                    Subtitle = "Get country-wise recommendations for your study abroad journey.",
                    IconType = "living",
                    ImageUrl = "/images/study-hub/checklist-living.jpg",
                    ShopButtonText = "Shop Now →",
                    ShopButtonUrl = "/category/study-essentials?subcategory=desk-room-study-essential",
                    Items = new List<ChecklistGroupItemDto>
                    {
                        new() { Id = "l-1", Name = "Bedsheets", IsChecked = true },
                        new() { Id = "l-2", Name = "Pillowcase", IsChecked = false },
                        new() { Id = "l-3", Name = "Laundry Bag", IsChecked = false },
                        new() { Id = "l-4", Name = "Storage Organizers", IsChecked = false },
                        new() { Id = "l-5", Name = "Bath Towels", IsChecked = true },
                        new() { Id = "l-6", Name = "Clothes Hangers", IsChecked = false }
                    }
                },
                new()
                {
                    Id = "cat-firstaid",
                    Number = 9,
                    Title = "9. Health & First Aid",
                    Subtitle = "1 of 5 items checked",
                    IconType = "health",
                    ImageUrl = "/images/study-hub/checklist-firstaid.jpg",
                    ShopButtonText = "Shop Now →",
                    ShopButtonUrl = "/category/study-essentials",
                    Items = new List<ChecklistGroupItemDto>
                    {
                        new() { Id = "h-1", Name = "First-Aid Kit", IsChecked = true },
                        new() { Id = "h-2", Name = "Personal Medications", IsChecked = false },
                        new() { Id = "h-3", Name = "Prescriptions", IsChecked = false },
                        new() { Id = "h-4", Name = "Health Supplements", IsChecked = false },
                        new() { Id = "h-5", Name = "Thermometer & Pain Relief", IsChecked = false }
                    }
                },
                new()
                {
                    Id = "cat-kitchen",
                    Number = 10,
                    Title = "10. Food & Kitchen Essentials",
                    Subtitle = "2 of 5 items checked",
                    IconType = "kitchen",
                    ImageUrl = "/images/study-hub/checklist-kitchen.jpg",
                    ShopButtonText = "Shop Now →",
                    ShopButtonUrl = "/category/study-essentials",
                    Items = new List<ChecklistGroupItemDto>
                    {
                        new() { Id = "k-1", Name = "Food Containers", IsChecked = true },
                        new() { Id = "k-2", Name = "Lunch Box", IsChecked = false },
                        new() { Id = "k-3", Name = "Reusable Cutlery", IsChecked = false },
                        new() { Id = "k-4", Name = "Travel Mug", IsChecked = false },
                        new() { Id = "k-5", Name = "Ready-to-Eat Instant Food", IsChecked = true },
                        new() { Id = "k-6", Name = "Compact Electric Kettle", IsChecked = false },
                        new() { Id = "k-7", Name = "Water Purifier Bottle", IsChecked = false }
                    }
                }
            },
            DestinationGuides = new List<ChecklistDestinationGuideDto>
            {
                new() { CountryName = "USA", CountryCode = "US", FlagEmoji = "🇺🇸", FlagUrl = "/images/countries/usa.webp", AdapterType = "Adapter Type A/B", Voltage = "(120V)", LinkUrl = "/category/study-essentials?country=usa" },
                new() { CountryName = "UK", CountryCode = "GB", FlagEmoji = "🇬🇧", FlagUrl = "/images/countries/uk.webp", AdapterType = "Adapter Type G", Voltage = "(230V)", LinkUrl = "/category/study-essentials?country=uk" },
                new() { CountryName = "Canada", CountryCode = "CA", FlagEmoji = "🇨🇦", FlagUrl = "/images/countries/canada.webp", AdapterType = "Adapter Type A/B", Voltage = "(120V)", LinkUrl = "/category/study-essentials?country=canada" },
                new() { CountryName = "Australia", CountryCode = "AU", FlagEmoji = "🇦🇺", FlagUrl = "/images/countries/australia.webp", AdapterType = "Adapter Type I", Voltage = "(230V)", LinkUrl = "/category/study-essentials?country=australia" },
                new() { CountryName = "France", CountryCode = "FR", FlagEmoji = "🇫🇷", FlagUrl = "/images/countries/france.webp", AdapterType = "Adapter Type C/E", Voltage = "(230V)", LinkUrl = "/category/study-essentials?country=france" },
                new() { CountryName = "Germany", CountryCode = "DE", FlagEmoji = "🇩🇪", FlagUrl = "/images/countries/germany.webp", AdapterType = "Adapter Type C/F", Voltage = "(230V)", LinkUrl = "/category/study-essentials?country=germany" }
            },
            QuickTips = new List<string>
            {
                "Check destination's customs rules before packing food, medicines or restricted items.",
                "Keep important documents in your hand luggage.",
                "Pack according to the climate of your destination.",
                "Keep emergency contact numbers and university accommodation details handy."
            },
            BottomBannerTitle = "Ready for Your Next Chapter?",
            BottomBannerSubtitle = "Everything you need, all in one place.",
            BottomBannerButtonText = "Shop Study Essentials →",
            BottomBannerButtonUrl = "/category/study-essentials",
            BottomBannerImageUrl = "/images/study-hub/checklist-bottom-banner.jpg"
        };
    }

    public static EducationLoansPageDto CreateDefaultEducationLoansPage()
    {
        return new EducationLoansPageDto
        {
            TopNavBrandName = "UniCreds",
            TopNavBrandSubtitle = "Global Student Financing",
            TopNavLogoUrl = "",
            TopNavLinks = new List<string> { "Abroad Education Loan", "Student Loan", "Study Abroad", "Contact Us" },
            TopNavCtaText = "Apply For Loan",
            TopNavCtaUrl = "#apply-loan-modal",

            HeroTagline = "FUNDING DREAMS. FUELLING CAREERS.",
            HeroTitle = "Get Abroad Education Loan in",
            HeroTitleHighlight = "48 hours*",
            HeroBullets = new List<string>
            {
                "Interest Rate Starting at 8.33%*",
                "Sanctions in 48 Hours*",
                "Savings up to ₹15 lacs",
                "Non Collateral Loans"
            },
            HeroCtaText = "Check your Eligibility",
            HeroCtaUrl = "#apply-loan-modal",
            HeroImageUrl = "/images/study-hub/gr-loans-hero-banner.jpg",
            HeroBgColor = "#022340",
            HeroMinHeightPx = 620,

            ReviewRating = "4.8 out of 5",
            ReviewCountText = "Google Reviews Rated 4.8 out of 5",
            Metric1Value = "20+",
            Metric1Label = "Loan Partners",
            Metric2Value = "₹ 2,000 Cr+",
            Metric2Label = "Amount Disbursed",
            Metric3Value = "50+",
            Metric3Label = "Countries Served",
            Metric4Value = "20,000+",
            Metric4Label = "Students Supported",

            PartnersTitle = "Our Loan Partners",
            PartnersSubtitle = "Find Your Fit: Top Interest Rates from 20+ Lenders",
            PartnersViewMoreText = "View More ⌵",
            Partners = new List<EducationLoanPartnerDto>
            {
                new() { Name = "ICICI Bank", InterestRateText = "Interest rate 10.25% p.a* >", BadgeColor = "#ea580c" },
                new() { Name = "Credila", InterestRateText = "Interest rate 10.00% p.a* >", BadgeColor = "#0284c7" },
                new() { Name = "AVANSE", InterestRateText = "Interest rate 10.5% p.a* >", BadgeColor = "#16a34a" },
                new() { Name = "Union Bank", InterestRateText = "Interest rate 8.99% p.a* >", BadgeColor = "#dc2626" },
                new() { Name = "InCred!", InterestRateText = "Interest rate 11.4% p.a* >", BadgeColor = "#ea580c" },
                new() { Name = "Axis Bank", InterestRateText = "Interest rate 10.15% p.a* >", BadgeColor = "#9333ea" },
                new() { Name = "State Bank of India", InterestRateText = "Interest rate 8.65% p.a* >", BadgeColor = "#0284c7" },
                new() { Name = "HDFC Bank", InterestRateText = "Interest rate 9.75% p.a* >", BadgeColor = "#2563eb" }
            },

            HowItWorksTitle = "How it Works?",
            HowItWorksSubtitle = "Take advantage of our 100% digital process in just 5 easy steps",
            Steps = new List<EducationLoanStepDto>
            {
                new() { StepNumber = 1, Title = "Check Loan Eligibility", Subtitle = "Quickly verify your loan", Icon = "check-circle" },
                new() { StepNumber = 2, Title = "Talk to Our Loan Experts", Subtitle = "Speak with our loan specialists", Icon = "headset" },
                new() { StepNumber = 3, Title = "Submit Your Documents & Get Verified", Subtitle = "100% digital paperless verification", Icon = "file-text" },
                new() { StepNumber = 4, Title = "Unlock the Best Interest Rates", Subtitle = "Our experts negotiate top rates", Icon = "coins" },
                new() { StepNumber = 5, Title = "Loan Approval & Sanction in 48 Hours*", Subtitle = "Direct sanction & speedy disbursement", Icon = "check-badge" }
            },

            CoursesTag = "TRENDING COURSES & FINANCING",
            CoursesTitle = "Get Future-Ready with AI",
            CoursesSubtitle = "Fund high-impact degrees & certifications with flexible student financing at low interest rates.",
            CoursesBgImageUrl = "/images/study-hub/edu-courses-tech-bg.jpg",
            CoursesSlideIntervalSec = 3,
            Courses = new List<EducationLoanCourseDto>
            {
                new()
                {
                    Institution = "IIIT Bangalore",
                    Title = "EPGP in Applied AI and Agentic AI",
                    ImageUrl = "/images/study-hub/edu-campus-iiit.jpg",
                    Badge1 = "Certification",
                    Badge2 = "Building AI Agent",
                    InfoButtonText = "More Information",
                    InfoButtonUrl = "#apply-loan-modal",
                    ApplyButtonText = "Apply Now",
                    ApplyButtonUrl = "#apply-loan-modal"
                },
                new()
                {
                    Institution = "IIT Kharagpur",
                    Title = "EPGC in Building AI Products, Systems & Services",
                    ImageUrl = "/images/study-hub/edu-campus-iit.jpg",
                    Badge1 = "Executive Post Graduate Certificate",
                    Badge2 = "6 Months",
                    InfoButtonText = "More Information",
                    InfoButtonUrl = "#apply-loan-modal",
                    ApplyButtonText = "Apply Now",
                    ApplyButtonUrl = "#apply-loan-modal"
                },
                new()
                {
                    Institution = "IIT Delhi",
                    Title = "Certificate Programme in Cybersecurity and AI Security",
                    ImageUrl = "/images/study-hub/edu-campus-iiit.jpg",
                    Badge1 = "Certification",
                    Badge2 = "6 Months",
                    InfoButtonText = "More Information",
                    InfoButtonUrl = "#apply-loan-modal",
                    ApplyButtonText = "Apply Now",
                    ApplyButtonUrl = "#apply-loan-modal"
                },
                new()
                {
                    Institution = "IIM Kozhikode",
                    Title = "PCP in AI for Business Professionals",
                    ImageUrl = "/images/study-hub/edu-campus-iit.jpg",
                    Badge1 = "Certification",
                    Badge2 = "6 Months",
                    InfoButtonText = "More Information",
                    InfoButtonUrl = "#apply-loan-modal",
                    ApplyButtonText = "Apply Now",
                    ApplyButtonUrl = "#apply-loan-modal"
                },
                new()
                {
                    Institution = "IIT Madras",
                    Title = "Advanced Certification in Generative AI & Deep Learning",
                    ImageUrl = "/images/study-hub/edu-campus-iiit.jpg",
                    Badge1 = "PG Certification",
                    Badge2 = "9 Months",
                    InfoButtonText = "More Information",
                    InfoButtonUrl = "#apply-loan-modal",
                    ApplyButtonText = "Apply Now",
                    ApplyButtonUrl = "#apply-loan-modal"
                },
                new()
                {
                    Institution = "IIT Bombay",
                    Title = "Executive Programme in Machine Learning & AI",
                    ImageUrl = "/images/study-hub/edu-campus-iit.jpg",
                    Badge1 = "Executive Diploma",
                    Badge2 = "1 Year",
                    InfoButtonText = "More Information",
                    InfoButtonUrl = "#apply-loan-modal",
                    ApplyButtonText = "Apply Now",
                    ApplyButtonUrl = "#apply-loan-modal"
                },
                new()
                {
                    Institution = "BITS Pilani",
                    Title = "M.Tech in Artificial Intelligence & Data Science",
                    ImageUrl = "/images/study-hub/edu-campus-iiit.jpg",
                    Badge1 = "Master of Technology",
                    Badge2 = "2 Years",
                    InfoButtonText = "More Information",
                    InfoButtonUrl = "#apply-loan-modal",
                    ApplyButtonText = "Apply Now",
                    ApplyButtonUrl = "#apply-loan-modal"
                },
                new()
                {
                    Institution = "IISc Bangalore",
                    Title = "Computational Data Science & Autonomous Systems",
                    ImageUrl = "/images/study-hub/edu-campus-iit.jpg",
                    Badge1 = "Executive Track",
                    Badge2 = "10 Months",
                    InfoButtonText = "More Information",
                    InfoButtonUrl = "#apply-loan-modal",
                    ApplyButtonText = "Apply Now",
                    ApplyButtonUrl = "#apply-loan-modal"
                },
                new()
                {
                    Institution = "Stanford Online",
                    Title = "Artificial Intelligence Graduate Certification",
                    ImageUrl = "/images/study-hub/edu-campus-iiit.jpg",
                    Badge1 = "Graduate Certificate",
                    Badge2 = "Self-Paced",
                    InfoButtonText = "More Information",
                    InfoButtonUrl = "#apply-loan-modal",
                    ApplyButtonText = "Apply Now",
                    ApplyButtonUrl = "#apply-loan-modal"
                },
                new()
                {
                    Institution = "Carnegie Mellon",
                    Title = "Executive Certificate in Agentic AI Systems",
                    ImageUrl = "/images/study-hub/edu-campus-iit.jpg",
                    Badge1 = "Professional Track",
                    Badge2 = "8 Months",
                    InfoButtonText = "More Information",
                    InfoButtonUrl = "#apply-loan-modal",
                    ApplyButtonText = "Apply Now",
                    ApplyButtonUrl = "#apply-loan-modal"
                },
                new()
                {
                    Institution = "MIT Professional",
                    Title = "Machine Learning: Algorithms & Practice",
                    ImageUrl = "/images/study-hub/edu-campus-iiit.jpg",
                    Badge1 = "Professional Certificate",
                    Badge2 = "6 Months",
                    InfoButtonText = "More Information",
                    InfoButtonUrl = "#apply-loan-modal",
                    ApplyButtonText = "Apply Now",
                    ApplyButtonUrl = "#apply-loan-modal"
                },
                new()
                {
                    Institution = "Oxford Saïd",
                    Title = "AI in FinTech and Financial Innovation",
                    ImageUrl = "/images/study-hub/edu-campus-iit.jpg",
                    Badge1 = "Executive Programme",
                    Badge2 = "6 Months",
                    InfoButtonText = "More Information",
                    InfoButtonUrl = "#apply-loan-modal",
                    ApplyButtonText = "Apply Now",
                    ApplyButtonUrl = "#apply-loan-modal"
                }
            },

            FloatingPromptTitle = "Start your degree or diploma at 10k per month!",
            FloatingPromptDesc = "Best-in-class courses in Data Science, Management and Technology",
            FloatingPromptPhone = "1800-DREAMFLY",
            FloatingPromptWhatsApp = "+919163012345"
        };
    }

    public static ConsultancyDetailsPageDto CreateDefaultConsultancyDetailsPage()
    {
        return new ConsultancyDetailsPageDto
        {
            BrandName = "Dreamflykit",
            LogoUrl = "/images/branding/dreamflykit-logo-clean.webp",
            NavLinks = new List<string>
            {
                "Upskilling Courses",
                "Study Abroad",
                "Professional Certifications",
                "Learn Offline",
                "School of Tech",
                "More"
            },
            SignUpButtonText = "SIGN UP",
            SignUpButtonUrl = "#consultancy-lead-modal",

            HeroMediaType = "image",
            HeroImageUrl = "/images/study-hub/consultancy-hero-campus.jpg",
            HeroVideoUrl = "",
            HeroHeadline = "For Your Every Next",
            HeroHeadlineHighlight = "Opportunity",
            SearchPlaceholder = "Tell us what you're looking to learn...",
            SearchButtonUrl = "#trending-courses",
            CategoryPills = new List<ConsultancyCategoryPillDto>
            {
                new() { Icon = "🤖", Title = "Agentic AI", FilterTag = "ai" },
                new() { Icon = "🧠", Title = "Artificial Intelligence", FilterTag = "ai" },
                new() { Icon = "🎓", Title = "Doctorate", FilterTag = "doctorate" },
                new() { Icon = "💻", Title = "Machine Learning", FilterTag = "ml" },
                new() { Icon = "🏛️", Title = "IIT/IIM Courses", FilterTag = "iit" },
                new() { Icon = "📈", Title = "Data Science", FilterTag = "data" },
                new() { Icon = "💼", Title = "MBA", FilterTag = "mba" },
                new() { Icon = "📢", Title = "Marketing", FilterTag = "marketing" },
                new() { Icon = "🏢", Title = "Management", FilterTag = "management" }
            },

            SubBannerPrefix = "Master tomorrow's skills",
            SubBannerHighlight = "with India's top upskilling platform.",

            PartnerTitle = "Great universities, top firms",
            Partners = new List<ConsultancyPartnerLogoDto>
            {
                new() { Name = "PeopleCert", LogoUrl = "/images/study-hub/partners/peoplecert.svg" },
                new() { Name = "Scrum.org", LogoUrl = "/images/study-hub/partners/scrumorg.svg" },
                new() { Name = "AXELOS", LogoUrl = "/images/study-hub/partners/axelos.svg" },
                new() { Name = "DevOps Institute", LogoUrl = "/images/study-hub/partners/devops.svg" },
                new() { Name = "Scrum Alliance", LogoUrl = "/images/study-hub/partners/scrumalliance.svg" },
                new() { Name = "Kanban University", LogoUrl = "/images/study-hub/partners/kanban.svg" },
                new() { Name = "ICAgile", LogoUrl = "/images/study-hub/partners/icagile.svg" },
                new() { Name = "Microsoft", LogoUrl = "/images/study-hub/partners/microsoft.svg" },
                new() { Name = "Dell", LogoUrl = "/images/study-hub/partners/dell.svg" }
            },

            TrendingBadge = "TRENDING COURSES",
            TrendingTitle = "Get Future-Ready with AI",
            TrendingSubtitle = "Equip yourself with next-generation AI and systems expertise from premier institutes.",
            TrendingCourses = new List<ConsultancyCourseDto>
            {
                new()
                {
                    Institution = "IIIT Bangalore",
                    Title = "EPGP in Applied AI and Agentic AI",
                    ImageUrl = "/images/study-hub/edu-campus-iiit.jpg",
                    BadgeTag = "Building AI Agent",
                    BadgeType = "Certification",
                    Duration = "6 Months",
                    Button1Text = "View Program",
                    Button1Url = "#consultancy-lead-modal",
                    Button2Text = "Syllabus",
                    Button2Url = "#consultancy-lead-modal"
                },
                new()
                {
                    Institution = "IIT Kharagpur",
                    Title = "EPGC in Building AI Products, Systems & Services",
                    ImageUrl = "/images/study-hub/campus-tech-institute.jpg",
                    BadgeTag = "Executive Track",
                    BadgeType = "Executive Post Graduate Certificate",
                    Duration = "6 Months",
                    Button1Text = "View Program",
                    Button1Url = "#consultancy-lead-modal",
                    Button2Text = "Syllabus",
                    Button2Url = "#consultancy-lead-modal"
                },
                new()
                {
                    Institution = "IIT Delhi",
                    Title = "Certificate Programme in Cybersecurity and AI Security",
                    ImageUrl = "/images/study-hub/edu-campus-iit.jpg",
                    BadgeTag = "Industry Accredited",
                    BadgeType = "Certification",
                    Duration = "6 Months",
                    Button1Text = "View Program",
                    Button1Url = "#consultancy-lead-modal",
                    Button2Text = "Syllabus",
                    Button2Url = "#consultancy-lead-modal"
                },
                new()
                {
                    Institution = "IIM Kozhikode",
                    Title = "PCP in AI for Business Professionals",
                    ImageUrl = "/images/study-hub/campus-paris-school.jpg",
                    BadgeTag = "Executive Alumni",
                    BadgeType = "Certification",
                    Duration = "6 Months",
                    Button1Text = "View Program",
                    Button1Url = "#consultancy-lead-modal",
                    Button2Text = "Syllabus",
                    Button2Url = "#consultancy-lead-modal"
                }
            },

            PopularTitle = "Explore our most popular programs",
            PopularSubtitle = "Transform your professional trajectory with globally recognized programs.",
            PopularPrograms = new List<ConsultancyCourseDto>
            {
                new()
                {
                    Institution = "IIT Kharagpur",
                    Title = "Advanced Semiconductor Packaging Reliability Workshop",
                    ImageUrl = "/images/study-hub/campus-tech-institute.jpg",
                    BadgeTag = "IIT Alumni Centre Bengaluru",
                    BadgeType = "In person Workshop",
                    Duration = "4th - 6th Dec 2026",
                    Button1Text = "View Program",
                    Button1Url = "#consultancy-lead-modal",
                    Button2Text = "Apply Now",
                    Button2Url = "#consultancy-lead-modal"
                },
                new()
                {
                    Institution = "IIIT Bangalore",
                    Title = "Executive Diploma in Machine Learning and AI",
                    ImageUrl = "/images/study-hub/edu-campus-iiit.jpg",
                    BadgeTag = "360° Career Support",
                    BadgeType = "Executive Diploma",
                    Duration = "12 Months",
                    Button1Text = "View Program",
                    Button1Url = "#consultancy-lead-modal",
                    Button2Text = "Syllabus",
                    Button2Url = "#consultancy-lead-modal"
                },
                new()
                {
                    Institution = "Paris School of Business",
                    Title = "MBA from Paris School of Business with Certification from IIM...",
                    ImageUrl = "/images/study-hub/campus-paris-school.jpg",
                    BadgeTag = "PSB MBA + IIML Certification",
                    BadgeType = "Master's Degree",
                    Duration = "18 Months",
                    Button1Text = "View Program",
                    Button1Url = "#consultancy-lead-modal",
                    Button2Text = "Syllabus",
                    Button2Url = "#consultancy-lead-modal"
                },
                new()
                {
                    Institution = "MICA",
                    Title = "Advanced Certificate in AI-Powered Digital Marketing &...",
                    ImageUrl = "/images/study-hub/checklist-study.jpg",
                    BadgeTag = "MICA Executive Alumni Status",
                    BadgeType = "Certification",
                    Duration = "5 Specialisation",
                    Button1Text = "View Program",
                    Button1Url = "#consultancy-lead-modal",
                    Button2Text = "Syllabus",
                    Button2Url = "#consultancy-lead-modal"
                }
            },

            DestinationSectionTitle = "Study in France for Indian Students",
            DestinationExploreText = "Explore programs >",
            DestinationExploreUrl = "#consultancy-lead-modal",
            Countries = new List<ConsultancyCountryDto>
            {
                new()
                {
                    CountryCode = "FR",
                    CountryName = "France",
                    HeroImageUrl = "/images/study-hub/study-france-alexandre.jpg",
                    SlideNumber = "03 / 9",
                    IsActive = true,
                    Stats = new List<ConsultancyStatDto>
                    {
                        new() { Value = "98%", Label = "Admit Rate" },
                        new() { Value = "2-5 Years", Label = "Post Study Visa" },
                        new() { Value = "₹10L+", Label = "Scholarships" },
                        new() { Value = "2", Label = "Available Intakes" }
                    },
                    Programs = new List<ConsultancyCourseDto>
                    {
                        new()
                        {
                            Institution = "Paris School of Business",
                            Title = "MSc in Luxury and Fashion Management",
                            ImageUrl = "/images/study-hub/campus-paris-school.jpg",
                            BadgeTag = "AACSB, EFMD & AMBA Accredited",
                            BadgeType = "Degree",
                            Duration = "20 Months",
                            Button1Text = "View Program",
                            Button1Url = "#consultancy-lead-modal"
                        },
                        new()
                        {
                            Institution = "Paris School of Business",
                            Title = "MSc Supply Chain Management at PSB France",
                            ImageUrl = "/images/study-hub/campus-paris-school.jpg",
                            BadgeTag = "#43 by Subject (FT 2025)",
                            BadgeType = "Degree",
                            Duration = "20 Months",
                            Button1Text = "View Program",
                            Button1Url = "#consultancy-lead-modal"
                        }
                    }
                },
                new()
                {
                    CountryCode = "CA",
                    CountryName = "Canada",
                    HeroImageUrl = "/images/study-hub/checklist-travel.jpg",
                    SlideNumber = "01 / 9",
                    IsActive = false,
                    Stats = new List<ConsultancyStatDto>
                    {
                        new() { Value = "95%", Label = "Admit Rate" },
                        new() { Value = "3 Years", Label = "Post Study Work Visa" },
                        new() { Value = "₹8L+", Label = "Scholarships" },
                        new() { Value = "3", Label = "Available Intakes" }
                    },
                    Programs = new List<ConsultancyCourseDto>
                    {
                        new()
                        {
                            Institution = "University of Windsor",
                            Title = "Master of Applied Computing (AI & Systems)",
                            ImageUrl = "/images/study-hub/edu-campus-iiit.jpg",
                            BadgeTag = "Co-op Work Term Included",
                            BadgeType = "Degree",
                            Duration = "16 Months",
                            Button1Text = "View Program",
                            Button1Url = "#consultancy-lead-modal"
                        }
                    }
                },
                new()
                {
                    CountryCode = "FI",
                    CountryName = "Finland",
                    HeroImageUrl = "/images/study-hub/checklist-hero.jpg",
                    SlideNumber = "02 / 9",
                    IsActive = false,
                    Stats = new List<ConsultancyStatDto>
                    {
                        new() { Value = "94%", Label = "Admit Rate" },
                        new() { Value = "2 Years", Label = "Post Study Visa" },
                        new() { Value = "100%", Label = "Tuition Waiver Option" },
                        new() { Value = "2", Label = "Available Intakes" }
                    },
                    Programs = new List<ConsultancyCourseDto>
                    {
                        new()
                        {
                            Institution = "Aalto University",
                            Title = "MSc in Computer Science & Autonomous AI",
                            ImageUrl = "/images/study-hub/campus-tech-institute.jpg",
                            BadgeTag = "Nordic Tech Leader",
                            BadgeType = "Degree",
                            Duration = "24 Months",
                            Button1Text = "View Program",
                            Button1Url = "#consultancy-lead-modal"
                        }
                    }
                },
                new()
                {
                    CountryCode = "GB",
                    CountryName = "United Kingdom",
                    HeroImageUrl = "/images/study-hub/checklist-living.jpg",
                    SlideNumber = "04 / 9",
                    IsActive = false,
                    Stats = new List<ConsultancyStatDto>
                    {
                        new() { Value = "97%", Label = "Admit Rate" },
                        new() { Value = "2 Years", Label = "Graduate Route Visa" },
                        new() { Value = "£10K+", Label = "Vice Chancellor Grants" },
                        new() { Value = "3", Label = "Available Intakes" }
                    },
                    Programs = new List<ConsultancyCourseDto>
                    {
                        new()
                        {
                            Institution = "University of Bristol",
                            Title = "MSc Data Science & Artificial Intelligence",
                            ImageUrl = "/images/study-hub/edu-campus-iit.jpg",
                            BadgeTag = "Russell Group University",
                            BadgeType = "Degree",
                            Duration = "12 Months",
                            Button1Text = "View Program",
                            Button1Url = "#consultancy-lead-modal"
                        }
                    }
                }
            },

            FloatingCallButtonText = "Free Counselling",
            FloatingContactPhone = "1800-DREAMFLY",
            LeadModalTitle = "Book Free 1-on-1 Study Abroad & Career Counselling",
            LeadModalSubtitle = "Get evaluated by certified consultants for profile scoring, university shortlists & scholarships."
        };
    }

    public static StudyAbroadHubProfileDto CreateDefaultAbroadProfile()
    {
        return new StudyAbroadHubProfileDto
        {
            HeroBadge = "Study Abroad Hub",
            HeroTitle = "Your Dreams Abroad.",
            HeroTitleHighlight = "Our Support.",
            HeroSubtitle = "Everything you need to plan your international education journey — all in one place.",
            HeroCtaText = "Explore Study Abroad Hub →",
            HeroCtaUrl = "#explore-services",
            HeroImageUrl = "",
            HeroHandwrittenNote = "Study Explore Grow ♡",
            HeroServices = new List<AbroadHeroServiceItemDto>
            {
                new() { Title = "Educational Loans", IconType = "loan", TargetUrl = "#service-loans", IsEnabled = true },
                new() { Title = "GR Loans", IconType = "gr_loan", TargetUrl = "#service-loans", IsEnabled = true },
                new() { Title = "Consultancy Details", IconType = "consultancy", TargetUrl = "/study-abroad-hub/consultancy", IsEnabled = true },
                new() { Title = "Find Consultancies Near Me", IconType = "near_me", TargetUrl = "#service-consultancies", IsEnabled = true },
                new() { Title = "Health Insurance", IconType = "insurance", TargetUrl = "#service-insurance", IsEnabled = true }
            },
            ServicesSectionTitle = "Explore Our Services",
            ServicesSectionSubtitle = "Your complete guide to financial support, trusted consultancies and health coverage for a global education.",
            Services = new List<AbroadPrimaryServiceDto>
            {
                new()
                {
                    Id = "srv-loans",
                    Title = "Educational Loans",
                    Description = "Finance your dreams with the right education loan options from trusted banks and lenders.",
                    ButtonText = "Get Started →",
                    ButtonUrl = "/study-abroad-hub/education-loans",
                    IconType = "loan",
                    ThemeColor = "blue",
                    ImageUrl = "",
                    SubService = new AbroadSubServiceDto
                    {
                        Title = "GR Loans",
                        Description = "Explore special loan schemes and financial assistance (GR Loans).",
                        ButtonUrl = "/study-abroad-hub/gr-loans",
                        IconType = "bank"
                    }
                },
                new()
                {
                    Id = "srv-consultancy",
                    Title = "Consultancy Details",
                    Description = "Find trusted study abroad consultancies and get expert guidance.",
                    ButtonText = "Get Started →",
                    ButtonUrl = "/study-abroad-hub/consultancy",
                    IconType = "consultancy",
                    ThemeColor = "purple",
                    ImageUrl = "",
                    SubService = new AbroadSubServiceDto
                    {
                        Title = "Find Consultancies Near Me",
                        Description = "Select your state, district or village to find the best consultancies near you.",
                        ButtonUrl = "/study-abroad-hub/consultancies#near-me",
                        IconType = "pin"
                    }
                },
                new()
                {
                    Id = "srv-insurance",
                    Title = "Health Insurance",
                    Description = "Stay protected during your international journey.",
                    ButtonText = "Get Started →",
                    ButtonUrl = "/study-abroad-hub/health-insurance",
                    IconType = "insurance",
                    ThemeColor = "teal",
                    ImageUrl = "",
                    Bullets = new List<string>
                    {
                        "Global Coverage",
                        "Medical & Emergency",
                        "Compare Plans"
                    }
                },
                new()
                {
                    Id = "srv-booking",
                    Title = "Booking Slots",
                    Description = "Book appointment slots for consultancy sessions, visa guidance and student counselling.",
                    ButtonText = "Get Started →",
                    ButtonUrl = "/study-abroad-hub/booking",
                    IconType = "calendar",
                    ThemeColor = "blue",
                    ImageUrl = ""
                },
                new()
                {
                    Id = "srv-tests",
                    Title = "Language Tests",
                    Description = "IELTS, PTE, TOEFL preparation and test booking guidance.",
                    ButtonText = "Get Started →",
                    ButtonUrl = "/study-abroad-hub/language-tests",
                    IconType = "test",
                    ThemeColor = "teal",
                    ImageUrl = ""
                },
                new()
                {
                    Id = "srv-scholarships",
                    Title = "Scholarships & Grants",
                    Description = "Discover scholarships, grants, eligibility and deadlines.",
                    ButtonText = "Get Started →",
                    ButtonUrl = "/study-abroad-hub/scholarships",
                    IconType = "scholarship",
                    ThemeColor = "purple",
                    ImageUrl = ""
                }
            },
            TrustPillars = new List<AbroadTrustPillarDto>
            {
                new() { Icon = "shield", Title = "Safe & Trusted", Description = "Verified partners and secure transactions" },
                new() { Icon = "plane", Title = "Global Reach", Description = "Support for 50+ countries" },
                new() { Icon = "users", Title = "Happy Students", Description = "10,000+ students and growing" },
                new() { Icon = "headset", Title = "24/7 Support", Description = "We're here when you need us" }
            },
            FooterSlogan = "Your Global Education Partner ♡",
            FiveSectionCarousel = new AbroadFiveSectionCarouselDto
            {
                IsEnabled = true,
                SlideIntervalSeconds = 5,
                AutoPlay = true,
                SectionTitle = "Featured Opportunities & Global Services",
                SectionSubtitle = "Synchronized study abroad offerings curated for international students.",
                TransitionDirection = "rtl",
                Slides = new List<AbroadFiveSectionSlideDto>
                {
                    // SLIDE SET 1
                    new()
                    {
                        SlideIndex = 0,
                        SlideLabel = "Slide 1 - University Finance & Support",
                        Spotlight = new AbroadCarouselSpotlightDto
                        {
                            Title = "Global Education Ambassador",
                            Subtitle = "Guiding students to world-class universities & verified careers",
                            TagText = "Official Partner",
                            ImageUrl = "",
                            ButtonText = "Meet Ambassador →",
                            ButtonUrl = "/study-abroad-hub/global-ambassador",
                            BackgroundGradient = "linear-gradient(135deg, #f59e0b 0%, #ea580c 100%)"
                        },
                        Card1 = new AbroadCarouselCardDto
                        {
                            Title = "Educational Loans",
                            Subtitle = "Student financing up to ₹1.5 Cr with low interest & fast sanction",
                            BadgeText = "Low Interest",
                            ButtonText = "Know More",
                            ButtonUrl = "/study-abroad-hub/education-loans",
                            IconType = "loan",
                            IsHighlighted = true
                        },
                        Card2 = new AbroadCarouselCardDto
                        {
                            Title = "Consultancy Details",
                            Subtitle = "Certified study abroad counsellors & university matching",
                            BadgeText = "Trusted",
                            ButtonText = "Know More",
                            ButtonUrl = "/study-abroad-hub/consultancy",
                            IconType = "consultancy"
                        },
                        Card3 = new AbroadCarouselCardDto
                        {
                            Title = "Health Insurance",
                            Subtitle = "Comprehensive international medical & emergency travel cover",
                            BadgeText = "Global",
                            ButtonText = "Know More",
                            ButtonUrl = "/study-abroad-hub/health-insurance",
                            IconType = "insurance"
                        },
                        Card4 = new AbroadCarouselCardDto
                        {
                            Title = "Scholarships & Grants",
                            Subtitle = "Merit-based financial aid & government university grants",
                            BadgeText = "Deadlines Open",
                            ButtonText = "Know More",
                            ButtonUrl = "/study-abroad-hub/scholarships",
                            IconType = "scholarship"
                        }
                    },
                    // SLIDE SET 2
                    new()
                    {
                        SlideIndex = 1,
                        SlideLabel = "Slide 2 - Visa & Language Readiness",
                        Spotlight = new AbroadCarouselSpotlightDto
                        {
                            Title = "Embassy Visa Support",
                            Subtitle = "Expert mock interviews with 99.2% student visa success rate",
                            TagText = "Visa Guidance",
                            ImageUrl = "",
                            ButtonText = "Visa Guide →",
                            ButtonUrl = "/study-abroad-hub/visa-support",
                            BackgroundGradient = "linear-gradient(135deg, #0284c7 0%, #1e40af 100%)"
                        },
                        Card1 = new AbroadCarouselCardDto
                        {
                            Title = "Language Tests",
                            Subtitle = "IELTS, TOEFL, PTE mock tests & booking vouchers",
                            BadgeText = "Test Prep",
                            ButtonText = "Know More",
                            ButtonUrl = "/study-abroad-hub/language-tests",
                            IconType = "test",
                            IsHighlighted = true
                        },
                        Card2 = new AbroadCarouselCardDto
                        {
                            Title = "Booking Slots",
                            Subtitle = "Direct 1-on-1 advisor video consultation slots",
                            BadgeText = "Instant Booking",
                            ButtonText = "Know More",
                            ButtonUrl = "/study-abroad-hub/booking",
                            IconType = "booking"
                        },
                        Card3 = new AbroadCarouselCardDto
                        {
                            Title = "Special GR Loans",
                            Subtitle = "Collateral-free government student loan schemes (GR Loans)",
                            BadgeText = "Government Scheme",
                            ButtonText = "Know More",
                            ButtonUrl = "/study-abroad-hub/gr-loans",
                            IconType = "gr_loan"
                        },
                        Card4 = new AbroadCarouselCardDto
                        {
                            Title = "Consultancies Near Me",
                            Subtitle = "Find verified counsellors in your state, district or city",
                            BadgeText = "Local Support",
                            ButtonText = "Know More",
                            ButtonUrl = "/study-abroad-hub/consultancies#near-me",
                            IconType = "near_me"
                        }
                    },
                    // SLIDE SET 3
                    new()
                    {
                        SlideIndex = 2,
                        SlideLabel = "Slide 3 - Campus Living & Forex",
                        Spotlight = new AbroadCarouselSpotlightDto
                        {
                            Title = "Campus Life & Housing",
                            Subtitle = "Safe student dorms, campus living and international peer network",
                            TagText = "Student Housing",
                            ImageUrl = "",
                            ButtonText = "Explore Housing →",
                            ButtonUrl = "/study-abroad-hub/accommodation",
                            BackgroundGradient = "linear-gradient(135deg, #0d9488 0%, #065f46 100%)"
                        },
                        Card1 = new AbroadCarouselCardDto
                        {
                            Title = "Student Accommodation",
                            Subtitle = "Verified student apartments & homestays near campus",
                            BadgeText = "Safe Stay",
                            ButtonText = "Know More",
                            ButtonUrl = "/study-abroad-hub/accommodation",
                            IconType = "accommodation",
                            IsHighlighted = true
                        },
                        Card2 = new AbroadCarouselCardDto
                        {
                            Title = "Forex & Bank Accounts",
                            Subtitle = "Zero-markup multi-currency student forex cards & bank accounts",
                            BadgeText = "Best Rates",
                            ButtonText = "Know More",
                            ButtonUrl = "/study-abroad-hub/forex",
                            IconType = "forex"
                        },
                        Card3 = new AbroadCarouselCardDto
                        {
                            Title = "Student Flight Savings",
                            Subtitle = "Exclusive student airfares with extra 23kg luggage allowance",
                            BadgeText = "Extra Luggage",
                            ButtonText = "Know More",
                            ButtonUrl = "/study-abroad-hub/flights",
                            IconType = "flight"
                        },
                        Card4 = new AbroadCarouselCardDto
                        {
                            Title = "Abroad Packing Checklist",
                            Subtitle = "Smart travel and university readiness packing checklist",
                            BadgeText = "100% Free",
                            ButtonText = "Know More",
                            ButtonUrl = "/abroad-packing-checklist",
                            IconType = "checklist"
                        }
                    }
                }
            },
            EducationLoansPage = CreateDefaultEducationLoansPage(),
            ConsultancyDetailsPage = CreateDefaultConsultancyDetailsPage()
        };
    }

    public static List<StudyGuideDetailPageDto> CreateDefaultGuidePages()
    {
        return new List<StudyGuideDetailPageDto>
        {
            // 1. Stay Study-Ready
            new()
            {
                Slug = "stay-study-ready",
                BadgeText = "STUDY GUIDE",
                Title = "Stay Study-Ready",
                Subtitle = "Prepare for your academic journey abroad.",
                Description = "Get the right study materials, stay organized, and build good study habits for a successful university experience.",
                HeroImageUrl = "/images/study-hub/checklist-study.jpg",
                HeroCursiveNote = "Better Preparation Brighter Future ➹",
                TrustPillars = new List<StudyGuideTrustPillarDto>
                {
                    new() { Icon = "academic", Title = "Academic Success", Subtitle = "Start with the right tools" },
                    new() { Icon = "brand", Title = "Trusted Brands", Subtitle = "Quality you can rely on" },
                    new() { Icon = "price", Title = "Student-Friendly Prices", Subtitle = "More value, less stress" },
                    new() { Icon = "delivery", Title = "Fast & Reliable Delivery", Subtitle = "To Your Doorstep" }
                },
                Section1Title = "What Do You Need for Your Studies?",
                Section1Subtitle = "Before you head abroad, make sure you have the right study essentials and check your university's guidelines for specific requirements.",
                PrepGuideTitle = "Your Study Preparation Guide",
                PrepGuideItems = new List<string>
                {
                    "Check if your university recommends specific books or materials",
                    "Carry essential notebooks and writing supplies",
                    "Keep an academic planner to manage assignments and deadlines",
                    "Organize your certificates and study documents",
                    "Check if your course requires a calculator or other tools"
                },
                ChecklistTitle = "Study Essentials Checklist",
                ChecklistItems = new List<string>
                {
                    "Notebooks & writing pads",
                    "Pens & highlighters",
                    "Academic planner / diary",
                    "Pencil pouch",
                    "Files & folders",
                    "Calculator (if required)"
                },
                ProTipTitle = "Pro Tip",
                ProTipContent = "Always check your university's website for a detailed list of recommended materials for your course.",
                ShowMistakesSection = true,
                MistakesSectionTitle = "Common Mistakes to Avoid",
                MistakesSectionSubtitle = "Tips to help international students succeed.",
                Mistakes = new List<StudyGuideMistakeCardDto>
                {
                    new() { Title = "Overpacking", Description = "Only carry what you really need" },
                    new() { Title = "Forgetting important documents", Description = "Keep digital and physical copies" },
                    new() { Title = "Ignoring climate needs", Description = "Pack suitable clothing for your destination" },
                    new() { Title = "Not checking power compatibility", Description = "Use the right adapters and converters" }
                },
                ProductsSectionTitle = "Recommended Study Essentials",
                ViewAllLinkText = "View All Study Essentials →",
                ViewAllLinkUrl = "/category/study-essentials",
                Products = new List<StudyGuideProductDto>
                {
                    new() { Id = "sr-prod-1", Title = "Spiral Notebook Set", Price = 349, Rating = 5.0m, ImageUrl = "/images/study-hub/item-notebook.png", ButtonText = "Add to Cart" },
                    new() { Id = "sr-prod-2", Title = "Gel Pen Set", Price = 299, Rating = 5.0m, ImageUrl = "/images/study-hub/item-pens.png", ButtonText = "Add to Cart" },
                    new() { Id = "sr-prod-3", Title = "Academic Planner", Price = 599, Rating = 5.0m, ImageUrl = "/images/study-hub/item-planner.png", ButtonText = "Add to Cart" },
                    new() { Id = "sr-prod-4", Title = "Scientific Calculator", Price = 899, Rating = 5.0m, ImageUrl = "/images/study-hub/item-sleeve.png", ButtonText = "Add to Cart" },
                    new() { Id = "sr-prod-5", Title = "Pencil Pouch", Price = 249, Rating = 5.0m, ImageUrl = "/images/study-hub/item-organizer.png", ButtonText = "Add to Cart" },
                    new() { Id = "sr-prod-6", Title = "File Folder Set", Price = 399, Rating = 5.0m, ImageUrl = "/images/study-hub/card-study-ready.png", ButtonText = "Add to Cart" }
                },
                BottomBannerTitle = "Build Your Study Kit",
                BottomBannerSubtitle = "Customize your perfect study kit with essentials for your course and destination.",
                BottomBannerButtonText = "Create Your Kit →",
                BottomBannerButtonUrl = "/study-abroad-hub#kits-section",
                BottomBannerImageUrl = "/images/study-hub/kit-complete.png"
            },

            // 2. Stay Connected
            new()
            {
                Slug = "stay-connected",
                BadgeText = "GUIDE",
                Title = "Stay Connected",
                Subtitle = "Get your digital essentials ready before departure.",
                Description = "Stay connected, stay productive and make the most of your online classes, research and campus life.",
                HeroImageUrl = "/images/study-hub/checklist-electronics.jpg",
                HeroCursiveNote = "Better Tech Bigger Opportunities ➹",
                TrustPillars = new List<StudyGuideTrustPillarDto>
                {
                    new() { Icon = "tech", Title = "Stay Connected", Subtitle = "For a seamless experience" },
                    new() { Icon = "brand", Title = "Trusted Brands", Subtitle = "Top global brands" },
                    new() { Icon = "price", Title = "Student-Friendly Prices", Subtitle = "More value, less stress" },
                    new() { Icon = "delivery", Title = "Fast & Reliable Delivery", Subtitle = "To Your Doorstep" }
                },
                Section1Title = "What Technology Will You Need?",
                Section1Subtitle = "From online classes to research and assignments, the right tech makes it easier to stay productive and connected.",
                PrepGuideTitle = "Your Digital Preparation Guide",
                PrepGuideItems = new List<string>
                {
                    "Check if your laptop meets your course requirements",
                    "Verify charger and voltage compatibility with your destination",
                    "Choose a mouse and headphones for comfortable study",
                    "Check if you need a USB hub or external storage",
                    "Get a travel adapter for your destination's plug type"
                },
                ChecklistTitle = "Digital Essentials Checklist",
                ChecklistItems = new List<string>
                {
                    "Laptop and charger",
                    "Mouse",
                    "Headphones / earphones",
                    "USB hub",
                    "Charging cables",
                    "Travel adapter"
                },
                ProTipTitle = "Pro Tip",
                ProTipContent = "Check your destination's plug type and voltage (e.g. 110V/220V) before buying electronics.",
                ProductsSectionTitle = "Popular Digital Essentials",
                ViewAllLinkText = "View All Electronics →",
                ViewAllLinkUrl = "/category/electronics",
                Products = new List<StudyGuideProductDto>
                {
                    new() { Id = "sc-prod-1", Title = "Laptop Sleeve", Price = 1299, Rating = 5.0m, ImageUrl = "/images/study-hub/item-sleeve.png", ButtonText = "Add to Cart" },
                    new() { Id = "sc-prod-2", Title = "Wireless Mouse", Price = 799, Rating = 5.0m, ImageUrl = "/images/study-hub/card-connected.png", ButtonText = "Add to Cart" },
                    new() { Id = "sc-prod-3", Title = "Headphones", Price = 2499, Rating = 5.0m, ImageUrl = "/images/study-hub/checklist-electronics.jpg", ButtonText = "Add to Cart" },
                    new() { Id = "sc-prod-4", Title = "USB Hub", Price = 1099, Rating = 5.0m, ImageUrl = "/images/study-hub/test-pic-placeholder.svg", ButtonText = "Add to Cart" },
                    new() { Id = "sc-prod-5", Title = "Power Bank", Price = 1499, Rating = 5.0m, ImageUrl = "/images/study-hub/card-connected.png", ButtonText = "Add to Cart" },
                    new() { Id = "sc-prod-6", Title = "Travel Adapter", Price = 1199, Rating = 5.0m, ImageUrl = "/images/study-hub/checklist-electronics.jpg", ButtonText = "Add to Cart" }
                },
                BottomBannerTitle = "Need Help Choosing?",
                BottomBannerSubtitle = "Use our tech guide to find the right devices for your study needs.",
                BottomBannerButtonText = "View Tech Guide →",
                BottomBannerButtonUrl = "/category/electronics",
                BottomBannerImageUrl = "/images/study-hub/card-connected.png"
            },

            // 3. Keep Documents Organized
            new()
            {
                Slug = "keep-documents-organized",
                BadgeText = "GUIDE",
                Title = "Keep Documents Organized",
                Subtitle = "Keep your important documents safe and accessible.",
                Description = "From passports to admission letters, make sure you have everything ready for a smooth journey.",
                HeroImageUrl = "/images/study-hub/checklist-documents.jpg",
                HeroCursiveNote = "Your Documents Your Journey ➹",
                TrustPillars = new List<StudyGuideTrustPillarDto>
                {
                    new() { Icon = "documents", Title = "Stay Organized", Subtitle = "No last minute stress" },
                    new() { Icon = "brand", Title = "Trusted Brands", Subtitle = "Quality you can rely on" },
                    new() { Icon = "price", Title = "Student-Friendly Prices", Subtitle = "More value, less stress" },
                    new() { Icon = "delivery", Title = "Fast & Reliable Delivery", Subtitle = "To Your Doorstep" }
                },
                Section1Title = "Which Documents Should You Prepare?",
                Section1Subtitle = "Each country and university may have different requirements. Check the official guidelines and plan accordingly.",
                PrepGuideTitle = "Document Preparation Guide",
                PrepGuideItems = new List<string>
                {
                    "Keep your passport and visa documents accessible",
                    "Carry your university admission letter",
                    "Organize academic certificates and transcripts",
                    "Keep accommodation and travel details available",
                    "Store secure digital copies of important documents",
                    "Check official instructions for your destination"
                },
                ChecklistTitle = "Document Checklist",
                ChecklistItems = new List<string>
                {
                    "Passport",
                    "Visa documents",
                    "Admission letter",
                    "Academic certificates",
                    "Accommodation details",
                    "Travel insurance (if applicable)"
                },
                ProTipTitle = "Pro Tip",
                ProTipContent = "Keep both physical and digital copies of your important documents in a safe place.",
                ProductsSectionTitle = "Recommended Document Organizers",
                ViewAllLinkText = "View All Travel Accessories →",
                ViewAllLinkUrl = "/category/travel-gear",
                Products = new List<StudyGuideProductDto>
                {
                    new() { Id = "kd-prod-1", Title = "Passport Holder", Price = 249, Rating = 5.0m, ImageUrl = "/images/study-hub/card-documents.png", ButtonText = "Add to Cart" },
                    new() { Id = "kd-prod-2", Title = "Document Organizer", Price = 899, Rating = 5.0m, ImageUrl = "/images/study-hub/item-organizer.png", ButtonText = "Add to Cart" },
                    new() { Id = "kd-prod-3", Title = "Certificate Folder", Price = 399, Rating = 5.0m, ImageUrl = "/images/study-hub/card-documents.png", ButtonText = "Add to Cart" },
                    new() { Id = "kd-prod-4", Title = "Travel Pouch", Price = 259, Rating = 5.0m, ImageUrl = "/images/study-hub/card-documents.png", ButtonText = "Add to Cart" },
                    new() { Id = "kd-prod-5", Title = "Luggage Tag", Price = 349, Rating = 5.0m, ImageUrl = "/images/study-hub/card-documents.png", ButtonText = "Add to Cart" },
                    new() { Id = "kd-prod-6", Title = "Waterproof Bag", Price = 449, Rating = 5.0m, ImageUrl = "/images/study-hub/checklist-documents.jpg", ButtonText = "Add to Cart" }
                },
                BottomBannerTitle = "Travel with Confidence",
                BottomBannerSubtitle = "Organized documents = less stress and more time for what matters.",
                BottomBannerButtonText = "Shop Document Organizers →",
                BottomBannerButtonUrl = "/category/travel-gear",
                BottomBannerImageUrl = "/images/study-hub/card-documents.png"
            },

            // 4. Set Up Your Student Life
            new()
            {
                Slug = "set-up-your-student-life",
                BadgeText = "GUIDE",
                Title = "Set Up Your Student Life",
                Subtitle = "Make your new student life abroad comfortable and convenient.",
                Description = "From campus essentials to everyday living, get everything you need to feel at home, wherever you are.",
                HeroImageUrl = "/images/study-hub/checklist-living.jpg",
                HeroCursiveNote = "New Place New Routine Same You ➹",
                TrustPillars = new List<StudyGuideTrustPillarDto>
                {
                    new() { Icon = "life", Title = "Comfort & Convenience", Subtitle = "For everyday living" },
                    new() { Icon = "brand", Title = "Trusted Brands", Subtitle = "Quality you can rely on" },
                    new() { Icon = "price", Title = "Student-Friendly Prices", Subtitle = "More value, less stress" },
                    new() { Icon = "delivery", Title = "Fast & Reliable Delivery", Subtitle = "To Your Doorstep" }
                },
                Section1Title = "What Will Make Your Daily Life Easier?",
                Section1Subtitle = "Be prepared for campus, accommodation and daily routines with the right essentials.",
                PrepGuideTitle = "Student Life Preparation Guide",
                PrepGuideItems = new List<string>
                {
                    "Check what your accommodation already provides",
                    "Choose a comfortable backpack for your campus",
                    "Carry a reusable water bottle and lunch box",
                    "Consider a desk lamp and organizer for your room",
                    "Check the local climate before packing clothes",
                    "Avoid bulky items you can easily buy there"
                },
                ChecklistTitle = "Everyday Essentials Checklist",
                ChecklistItems = new List<string>
                {
                    "Backpack",
                    "Water bottle",
                    "Lunch box",
                    "Compact umbrella",
                    "Desk organizer",
                    "Laundry bag"
                },
                ProTipTitle = "Pro Tip",
                ProTipContent = "Pack versatile items that work for both campus and daily life. Keep it light and organized.",
                IsCategoryMode = true,
                ProductsSectionTitle = "Essential Categories for Student Life",
                ViewAllLinkText = "View All Student Life Essentials →",
                ViewAllLinkUrl = "/category/study-essentials",
                Categories = new List<StudyGuideCategoryCardDto>
                {
                    new() { Id = "sl-cat-1", Title = "Campus Essentials", Subtitle = "Backpacks, bottles, ID holders", ImageUrl = "/images/study-hub/card-student-life.png", ButtonText = "View Items →", TargetUrl = "/category/study-essentials?subcategory=college-campus-essentials" },
                    new() { Id = "sl-cat-2", Title = "Study Room Essentials", Subtitle = "Desk lamps, organizers", ImageUrl = "/images/study-hub/checklist-living.jpg", ButtonText = "View Items →", TargetUrl = "/category/study-essentials?subcategory=desk-room-study-essential" },
                    new() { Id = "sl-cat-3", Title = "Everyday Living Essentials", Subtitle = "Bags, laundry, room organizers", ImageUrl = "/images/study-hub/checklist-living.jpg", ButtonText = "View Items →", TargetUrl = "/category/study-essentials" },
                    new() { Id = "sl-cat-4", Title = "Comfort Essentials", Subtitle = "Neck pillows, eye masks...", ImageUrl = "/images/study-hub/checklist-travel.jpg", ButtonText = "View Items →", TargetUrl = "/category/travel-gear" }
                },
                BottomBannerTitle = "Ready for a Better Student Life?",
                BottomBannerSubtitle = "Find all the essentials you need for a comfortable and productive stay abroad.",
                BottomBannerButtonText = "Shop Student Life Essentials →",
                BottomBannerButtonUrl = "/category/study-essentials",
                BottomBannerImageUrl = "/images/study-hub/card-student-life.png"
            }
        };
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
    public int AutoSlideIntervalSeconds { get; set; } = 4;
    public List<StudyHubHeroSlideDto> Slides { get; set; } = new();
}

public sealed class StudyHubHeroSlideDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public string SecondaryBadge { get; set; } = "New Country New Opportunities Same You! ✈️";
    public string? LinkUrl { get; set; } = "/category/study-essentials";
    public string? Title { get; set; }
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
    public string Title { get; set; } = "What Every Student Needs Before Going Abroad";
    public string Subtitle { get; set; } = "Prepare smarter, pack better and feel confident about your journey abroad.";
    public string BadgeText { get; set; } = "Be Ready for New Adventures ✈";
    public string TitleFontSize { get; set; } = "large";
    public List<StudyHubDetailCardDto> Cards { get; set; } = new();
}

public sealed class StudyHubDetailCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconType { get; set; } = "study";
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public string LinkUrl { get; set; } = "/category/study-essentials";
    public string ButtonText { get; set; } = "Explore Essentials →";
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

// -------------------------------------------------------------
// DTOs for Dedicated Abroad Packing Checklist Page
// -------------------------------------------------------------

public sealed class AbroadChecklistPageDto
{
    public string HeroPill { get; set; } = "STUDY ESSENTIALS";
    public string HeroTitle { get; set; } = "Abroad Packing Checklist";
    public string HeroSubtitle { get; set; } = "Don't just pack. Pack smart!";
    public string HeroDescription { get; set; } = "Your complete checklist for a hassle-free journey abroad. From study essentials to travel must-haves, we've got you covered.";
    public string HeroBadgeText { get; set; } = "Pack Smart, Travel Far ✈";
    public string HeroImageUrl { get; set; } = "/images/study-hub/checklist-hero.jpg";

    public string SelectedDestination { get; set; } = "USA";
    public string ShopMissingButtonText { get; set; } = "Shop Missing Essentials →";
    public string ShopMissingButtonUrl { get; set; } = "/category/study-essentials";

    public List<ChecklistCategoryGroupDto> Categories { get; set; } = new();
    public List<ChecklistDestinationGuideDto> DestinationGuides { get; set; } = new();
    public List<string> QuickTips { get; set; } = new();

    public string BottomBannerTitle { get; set; } = "Ready for Your Next Chapter?";
    public string BottomBannerSubtitle { get; set; } = "Everything you need, all in one place.";
    public string BottomBannerButtonText { get; set; } = "Shop Study Essentials →";
    public string BottomBannerButtonUrl { get; set; } = "/category/study-essentials";
    public string BottomBannerImageUrl { get; set; } = "/images/study-hub/checklist-bottom-banner.jpg";
}

public sealed class ChecklistCategoryGroupDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public int Number { get; set; } = 1;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string IconType { get; set; } = "study";
    public string ImageUrl { get; set; } = "/images/study-hub/test-pic-placeholder.svg";
    public string ShopButtonText { get; set; } = "Shop Now →";
    public string ShopButtonUrl { get; set; } = "/category/study-essentials";
    public string TotalCountDisplay { get; set; } = string.Empty;
    public List<ChecklistGroupItemDto> Items { get; set; } = new();
}

public sealed class ChecklistGroupItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public bool IsChecked { get; set; } = false;
    public string TargetUrl { get; set; } = "/category/study-essentials";
}

public sealed class ChecklistDestinationGuideDto
{
    public string CountryName { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string FlagEmoji { get; set; } = string.Empty;
    public string FlagUrl { get; set; } = string.Empty;
    public string AdapterType { get; set; } = string.Empty;
    public string Voltage { get; set; } = string.Empty;
    public string LinkUrl { get; set; } = "/category/study-essentials";
}

// -------------------------------------------------------------
// DTOs for Study Abroad Hub Profile (Services Portal)
// -------------------------------------------------------------

public sealed class StudyAbroadHubProfileDto
{
    public string HeroBadge { get; set; } = "Study Abroad Hub";
    public string HeroTitle { get; set; } = "Your Dreams Abroad.";
    public string HeroTitleHighlight { get; set; } = "Our Support.";
    public string HeroSubtitle { get; set; } = "Everything you need to plan your international education journey — all in one place.";
    public string HeroCtaText { get; set; } = "Explore Study Abroad Hub →";
    public string HeroCtaUrl { get; set; } = "#explore-services";
    public string HeroImageUrl { get; set; } = string.Empty;
    public string HeroVideoUrl { get; set; } = string.Empty;
    public bool HeroEnableVideo { get; set; } = false;
    public bool HeroShowText { get; set; } = false;
    public string HeroHandwrittenNote { get; set; } = "Study Explore Grow ♡";

    public List<AbroadHeroServiceItemDto> HeroServices { get; set; } = new();

    public AbroadFiveSectionCarouselDto FiveSectionCarousel { get; set; } = new();

    public string ServicesSectionTitle { get; set; } = "Explore Our Services";
    public string ServicesSectionSubtitle { get; set; } = "Your complete guide to financial support, trusted consultancies and health coverage for a global education.";

    public List<AbroadPrimaryServiceDto> Services { get; set; } = new();
    public List<AbroadTrustPillarDto> TrustPillars { get; set; } = new();
    public EducationLoansPageDto? EducationLoansPage { get; set; }
    public ConsultancyDetailsPageDto? ConsultancyDetailsPage { get; set; }
    public string FooterSlogan { get; set; } = "Your Global Education Partner ♡";
    public string TrustTagline { get => FooterSlogan; set => FooterSlogan = value; }
}

public sealed class AbroadFiveSectionCarouselDto
{
    public bool IsEnabled { get; set; } = true;
    public int SlideIntervalSeconds { get; set; } = 5;
    public bool AutoPlay { get; set; } = true;
    public string SectionTitle { get; set; } = "Featured Opportunities & Global Services";
    public string SectionSubtitle { get; set; } = "Synchronized study abroad offerings curated for international students.";
    public string TransitionDirection { get; set; } = "rtl"; // "rtl", "ltr", "btt", "ttb"
    public List<AbroadFiveSectionSlideDto> Slides { get; set; } = new();
}

public sealed class AbroadFiveSectionSlideDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public int SlideIndex { get; set; } = 0;
    public string SlideLabel { get; set; } = "Slide 1";

    public AbroadCarouselSpotlightDto Spotlight { get; set; } = new();
    public AbroadCarouselCardDto Card1 { get; set; } = new();
    public AbroadCarouselCardDto Card2 { get; set; } = new();
    public AbroadCarouselCardDto Card3 { get; set; } = new();
    public AbroadCarouselCardDto Card4 { get; set; } = new();
}

public sealed class AbroadCarouselSpotlightDto
{
    public string Title { get; set; } = "Global Study Ambassador";
    public string Subtitle { get; set; } = "Guiding students to top world universities";
    public string TagText { get; set; } = "Official Partner";
    public string ImageUrl { get; set; } = "";
    public string ButtonText { get; set; } = "Meet Ambassador →";
    public string ButtonUrl { get; set; } = "/study-abroad-hub/global-ambassador";
    public string BackgroundGradient { get; set; } = "linear-gradient(135deg, #f59e0b 0%, #ea580c 100%)";
}

public sealed class AbroadCarouselCardDto
{
    public string Title { get; set; } = "Education Loans";
    public string Subtitle { get; set; } = "Low interest student financing";
    public string BadgeText { get; set; } = "";
    public string ButtonText { get; set; } = "Know More";
    public string ButtonUrl { get; set; } = "/study-abroad-hub/education-loans";
    public string IconType { get; set; } = "loan";
    public string ImageUrl { get; set; } = "";
    public bool IsHighlighted { get; set; } = false;
}

public sealed class AbroadHeroServiceItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string IconType { get; set; } = "loan";
    public string TargetUrl { get; set; } = "#";
    public bool IsEnabled { get; set; } = true;
}

public sealed class AbroadPrimaryServiceDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ButtonText { get; set; } = "Get Started →";
    public string ButtonUrl { get; set; } = "#";
    public string IconType { get; set; } = "study";
    public string ThemeColor { get; set; } = "blue";
    public string ImageUrl { get; set; } = string.Empty;
    public List<string> Bullets { get; set; } = new();
    public AbroadSubServiceDto? SubService { get; set; }
}

public sealed class AbroadSubServiceDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ButtonUrl { get; set; } = "#";
    public string IconType { get; set; } = "sub";
}

public sealed class AbroadTrustPillarDto
{
    public string Icon { get; set; } = "shield";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

// -------------------------------------------------------------
// DTOs for 4 Study Guide Detail Pages (Dreamflykit Store Profile)
// -------------------------------------------------------------

public sealed class StudyGuideDetailPageDto
{
    public string Slug { get; set; } = string.Empty;
    public string BadgeText { get; set; } = "STUDY GUIDE";
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string HeroImageUrl { get; set; } = string.Empty;
    public string HeroCursiveNote { get; set; } = string.Empty;

    public List<StudyGuideTrustPillarDto> TrustPillars { get; set; } = new();

    public string Section1Title { get; set; } = string.Empty;
    public string Section1Subtitle { get; set; } = string.Empty;

    public string PrepGuideTitle { get; set; } = "Your Preparation Guide";
    public List<string> PrepGuideItems { get; set; } = new();

    public string ChecklistTitle { get; set; } = "Essentials Checklist";
    public List<string> ChecklistItems { get; set; } = new();

    public string ProTipTitle { get; set; } = "Pro Tip";
    public string ProTipContent { get; set; } = string.Empty;

    public bool ShowMistakesSection { get; set; } = false;
    public string MistakesSectionTitle { get; set; } = "Common Mistakes to Avoid";
    public string MistakesSectionSubtitle { get; set; } = "Tips to help international students succeed.";
    public List<StudyGuideMistakeCardDto> Mistakes { get; set; } = new();

    public bool IsCategoryMode { get; set; } = false;
    public string ProductsSectionTitle { get; set; } = "Recommended Essentials";
    public string ViewAllLinkText { get; set; } = "View All →";
    public string ViewAllLinkUrl { get; set; } = "/category/study-essentials";
    public List<StudyGuideProductDto> Products { get; set; } = new();
    public List<StudyGuideCategoryCardDto> Categories { get; set; } = new();

    public string BottomBannerTitle { get; set; } = string.Empty;
    public string BottomBannerSubtitle { get; set; } = string.Empty;
    public string BottomBannerButtonText { get; set; } = string.Empty;
    public string BottomBannerButtonUrl { get; set; } = "/study-abroad-hub#kits-section";
    public string BottomBannerImageUrl { get; set; } = string.Empty;
}

public sealed class StudyGuideTrustPillarDto
{
    public string Icon { get; set; } = "academic";
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
}

public sealed class StudyGuideMistakeCardDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public sealed class StudyGuideProductDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Rating { get; set; } = 5.0m;
    public string ButtonText { get; set; } = "Add to Cart";
    public string TargetUrl { get; set; } = "/cart";
}

public sealed class StudyGuideCategoryCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ButtonText { get; set; } = "View Items →";
    public string TargetUrl { get; set; } = "/category/study-essentials";
}

// -------------------------------------------------------------
// DTOs for Dedicated Education Loans Portal
// -------------------------------------------------------------

public sealed class EducationLoansPageDto
{
    // Top Navigation (Student Accommodation removed as requested)
    public bool ShowTopNav { get; set; } = true;
    public string TopNavBrandName { get; set; } = "UniCreds";
    public string TopNavBrandSubtitle { get; set; } = "Global Student Financing";
    public string TopNavLogoUrl { get; set; } = "";
    public List<string> TopNavLinks { get; set; } = new() { "Abroad Education Loan", "Student Loan", "Study Abroad", "Contact Us" };
    public string TopNavCtaText { get; set; } = "Apply For Loan";
    public string TopNavCtaUrl { get; set; } = "#apply-loan-modal";

    // Full Hero Banner (Image 2)
    public string HeroTagline { get; set; } = "FUNDING DREAMS. FUELLING CAREERS.";
    public string HeroTitle { get; set; } = "Get Abroad Education Loan in";
    public string HeroTitleHighlight { get; set; } = "48 hours*";
    public List<string> HeroBullets { get; set; } = new()
    {
        "Interest Rate Starting at 8.33%*",
        "Sanctions in 48 Hours*",
        "Savings up to ₹15 lacs",
        "Non Collateral Loans"
    };
    public string HeroCtaText { get; set; } = "Check your Eligibility";
    public string HeroCtaUrl { get; set; } = "#apply-loan-modal";
    public string HeroImageUrl { get; set; } = "/images/study-hub/gr-loans-hero-banner.jpg";
    public string HeroBgColor { get; set; } = "#f0fdf4";
    public int HeroMinHeightPx { get; set; } = 620;

    // Our Achievements (Image 3)
    public string ReviewRating { get; set; } = "4.8 out of 5";
    public string ReviewCountText { get; set; } = "Google Reviews Rated 4.8 out of 5";
    public string Metric1Value { get; set; } = "20+";
    public string Metric1Label { get; set; } = "Loan Partners";
    public string Metric2Value { get; set; } = "₹ 2,000 Cr+";
    public string Metric2Label { get; set; } = "Amount Disbursed";
    public string Metric3Value { get; set; } = "50+";
    public string Metric3Label { get; set; } = "Countries Served";
    public string Metric4Value { get; set; } = "20,000+";
    public string Metric4Label { get; set; } = "Students Supported";

    // Our Loan Partners (Image 4)
    public string PartnersTitle { get; set; } = "Our Loan Partners";
    public string PartnersSubtitle { get; set; } = "Find Your Fit: Top Interest Rates from 20+ Lenders";
    public string PartnersViewMoreText { get; set; } = "View More ⌵";
    public List<EducationLoanPartnerDto> Partners { get; set; } = new();

    // How It Works (Image 4)
    public string HowItWorksTitle { get; set; } = "How it Works?";
    public string HowItWorksSubtitle { get; set; } = "Take advantage of our 100% digital process in just 5 easy steps";
    public List<EducationLoanStepDto> Steps { get; set; } = new();

    // Featured / Trending Courses & Loan Schemes (Image 5)
    public string CoursesTag { get; set; } = "TRENDING COURSES & FINANCING";
    public string CoursesTitle { get; set; } = "Get Future-Ready with AI";
    public string CoursesSubtitle { get; set; } = "Fund high-impact degrees & certifications with flexible student financing at low interest rates.";
    public string CoursesBgImageUrl { get; set; } = "/images/study-hub/edu-courses-tech-bg.jpg";
    public int CoursesSlideIntervalSec { get; set; } = 3;
    public List<EducationLoanCourseDto> Courses { get; set; } = new();

    // Floating Loan Assistance Bubble (Image 5 bottom right)
    public string FloatingPromptTitle { get; set; } = "Start your degree or diploma at 10k per month!";
    public string FloatingPromptDesc { get; set; } = "Best-in-class courses in Data Science, Management and Technology";
    public string FloatingPromptPhone { get; set; } = "1800-DREAMFLY";
    public string FloatingPromptWhatsApp { get; set; } = "+919163012345";
}

public sealed class EducationLoanPartnerDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string InterestRateText { get; set; } = string.Empty;
    public string BadgeColor { get; set; } = "#16a34a";
    public string LinkUrl { get; set; } = "#apply-loan-modal";
}

public sealed class EducationLoanStepDto
{
    public int StepNumber { get; set; } = 1;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Icon { get; set; } = "check-circle";
}

public sealed class EducationLoanCourseDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Institution { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string Badge1 { get; set; } = string.Empty;
    public string Badge2 { get; set; } = string.Empty;
    public string InfoButtonText { get; set; } = "More Information";
    public string InfoButtonUrl { get; set; } = "#apply-loan-modal";
    public string ApplyButtonText { get; set; } = "Apply Now";
    public string ApplyButtonUrl { get; set; } = "#apply-loan-modal";
}

public sealed class EducationLoanApplicationLeadDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string TargetCountry { get; set; } = string.Empty;
    public string DesiredLoanAmount { get; set; } = string.Empty;
    public string CourseOrProgram { get; set; } = string.Empty;
    public string CollateralType { get; set; } = "Non-Collateral";
    public string Notes { get; set; } = string.Empty;
}

// -------------------------------------------------------------
// DTOs for Dedicated Study Abroad Consultancy Portal (upGrad-style)
// -------------------------------------------------------------

public sealed class ConsultancyDetailsPageDto
{
    // Branding & Header
    public string BrandName { get; set; } = "Dreamflykit";
    public string LogoUrl { get; set; } = "/images/branding/dreamflykit-logo-clean.webp";
    public List<string> NavLinks { get; set; } = new()
    {
        "Upskilling Courses",
        "Study Abroad",
        "Professional Certifications",
        "Learn Offline",
        "School of Tech",
        "More"
    };
    public string SignUpButtonText { get; set; } = "SIGN UP";
    public string SignUpButtonUrl { get; set; } = "#consultancy-lead-modal";

    // Hero Visual & Media (Secure video or image chosen by Admin)
    public string HeroMediaType { get; set; } = "image"; // "image" or "video"
    public string HeroImageUrl { get; set; } = "/images/study-hub/consultancy-hero-campus.jpg";
    public string HeroVideoUrl { get; set; } = "";
    public string HeroHeadline { get; set; } = "For Your Every Next";
    public string HeroHeadlineHighlight { get; set; } = "Opportunity";
    public string SearchPlaceholder { get; set; } = "Tell us what you're looking to learn...";
    public string SearchButtonUrl { get; set; } = "#trending-courses";
    public List<ConsultancyCategoryPillDto> CategoryPills { get; set; } = new();

    // Sub-Banner
    public string SubBannerPrefix { get; set; } = "Master tomorrow's skills";
    public string SubBannerHighlight { get; set; } = "with India's top upskilling platform.";

    // Partner Universities & Top Firms Bar
    public string PartnerTitle { get; set; } = "Great universities, top firms";
    public List<ConsultancyPartnerLogoDto> Partners { get; set; } = new();

    // Trending AI Courses Section (Image 3)
    public string TrendingBadge { get; set; } = "TRENDING COURSES";
    public string TrendingTitle { get; set; } = "Get Future-Ready with AI";
    public string TrendingSubtitle { get; set; } = "Equip yourself with breakthrough Agentic AI and ML expertise from world-class institutions.";
    public List<ConsultancyCourseDto> TrendingCourses { get; set; } = new();

    // Popular Programs Section (Image 4)
    public string PopularTitle { get; set; } = "Explore our most popular programs";
    public string PopularSubtitle { get; set; } = "Top-rated global degree & certificate pathways empowering career transitions.";
    public List<ConsultancyCourseDto> PopularPrograms { get; set; } = new();

    // Country Showcase / Study Abroad (Image 5)
    public string DestinationSectionTitle { get; set; } = "Study in France for Indian Students";
    public string DestinationExploreText { get; set; } = "Explore programs >";
    public string DestinationExploreUrl { get; set; } = "#consultancy-lead-modal";
    public List<ConsultancyCountryDto> Countries { get; set; } = new();

    // Floating Lead Capture & Free Counselling
    public string FloatingCallButtonText { get; set; } = "Free Counselling";
    public string FloatingContactPhone { get; set; } = "1800-DREAMFLY";
    public string LeadModalTitle { get; set; } = "Book Free 1-on-1 Study Abroad & Career Counselling";
    public string LeadModalSubtitle { get; set; } = "Speak with certified advisors for university matching, visa guidance & scholarships.";
}

public sealed class ConsultancyCategoryPillDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Icon { get; set; } = "🤖";
    public string Title { get; set; } = string.Empty;
    public string FilterTag { get; set; } = string.Empty;
}

public sealed class ConsultancyPartnerLogoDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Name { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
}

public sealed class ConsultancyCourseDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string Institution { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string BadgeTag { get; set; } = string.Empty;
    public string BadgeType { get; set; } = "Certification";
    public string Duration { get; set; } = "6 Months";
    public string Button1Text { get; set; } = "View Program";
    public string Button1Url { get; set; } = "#consultancy-lead-modal";
    public string Button2Text { get; set; } = "Syllabus";
    public string Button2Url { get; set; } = "#consultancy-lead-modal";
}

public sealed class ConsultancyCountryDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
    public string CountryCode { get; set; } = "FR";
    public string CountryName { get; set; } = "France";
    public string HeroImageUrl { get; set; } = "/images/study-hub/study-france-alexandre.jpg";
    public string SlideNumber { get; set; } = "03 / 9";
    public bool IsActive { get; set; } = false;
    public List<ConsultancyStatDto> Stats { get; set; } = new();
    public List<ConsultancyCourseDto> Programs { get; set; } = new();
}

public sealed class ConsultancyStatDto
{
    public string Value { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
}

public sealed class ConsultancyLeadDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string TargetDegree { get; set; } = string.Empty;
    public string DestinationCountry { get; set; } = string.Empty;
    public string InterestedProgram { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}



