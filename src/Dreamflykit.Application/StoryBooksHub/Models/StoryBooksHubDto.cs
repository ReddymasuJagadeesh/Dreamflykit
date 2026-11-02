namespace Dreamflykit.Application.StoryBooksHub.Models;

public sealed class StoryBooksThemeSettingsDto
{
    public string BrandName { get; set; } = "BOOKSFLIX";
    public string Tagline { get; set; } = "Unlimited Stories, Novels & Campus Literary Journals";
    public string PrimaryRed { get; set; } = "#E50914";
    public string DarkBackground { get; set; } = "#141414";
    public string CardBackground { get; set; } = "#181818";
    public bool IsDarkThemeEnabled { get; set; } = true;
}

public sealed class StoryBooksHeroBillboardDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Bon Appétit, Your Majesty";
    public string Author { get; set; } = "Elena Vance";
    public string TagBadge { get; set; } = "TOP 10 TODAY • #1 IN CAMPUS FICTION";
    public string GenreTags { get; set; } = "Campus Fiction • Romance • 2025 • University Edition • U/A 13+";
    public string Synopsis { get; set; } = "A captivating contemporary tale of an ambitious culinary exchange student at an elite European academy who uncovers a centuries-old imperial manuscript hidden in the university archives.";
    public string ImageUrl { get; set; } = "/images/hero/hero-booksflix.webp";
    public bool IsUploadingSoon { get; set; } = true;
    public string PrimaryButtonText { get; set; } = "Start Reading";
    public string PrimaryButtonUrl { get; set; } = "#read";
    public string SecondaryButtonText { get; set; } = "More Info";
    public string ReadTime { get; set; } = "340 Pages • ~6h Read";
    public string Language { get; set; } = "English";
    public decimal Price { get; set; } = 499;
    public decimal OriginalPrice { get; set; } = 799;
    public int DiscountPercent { get; set; } = 38;
    public double Rating { get; set; } = 4.9;
    public int ReviewCount { get; set; } = 1240;
}

public sealed class StoryBooksTabHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string TagBadge { get; set; } = string.Empty;
    public string MetaTags { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public string PrimaryButtonText { get; set; } = "Explore Now";
    public string PrimaryButtonUrl { get; set; } = "#explore";
    public string SecondaryButtonText { get; set; } = "More Info";
    public string SecondaryButtonUrl { get; set; } = "#info";
}

public sealed class StoryBookGenreDto
{
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Emoji { get; set; } = "📖";
    public int BookCount { get; set; }
}

public sealed class StoryBookLanguageDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string NativeLabel { get; set; } = string.Empty;
    public int BookCount { get; set; }
}

public sealed class StoryBookPreferenceDto
{
    public string Key { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
}

public sealed class StoryBookItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = "/images/fashion-hub/uploading-soon-placeholder.svg";
    public bool IsUploadingSoon { get; set; } = true;
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public int DiscountPercent { get; set; }
    public double Rating { get; set; } = 4.7;
    public int ReviewCount { get; set; } = 350;
    public int Pages { get; set; } = 280;
    public int PublicationYear { get; set; } = 2024;
    public string GenreSlug { get; set; } = "campus-fiction";
    public string GenreName { get; set; } = "Campus Fiction";
    public string Language { get; set; } = "English";
    public string LanguageCode { get; set; } = "en";
    public string EditionPreference { get; set; } = "original";
    public string Synopsis { get; set; } = string.Empty;
    public bool IsTop10 { get; set; }
    public int Top10Rank { get; set; }
    public string ReadingProgress { get; set; } = string.Empty; // e.g. "85 of 240 pages"
    public int ProgressPercent { get; set; } // e.g. 35
    public string BadgeText { get; set; } = string.Empty; // e.g. "Recently Added", "Top 10"
    public string MaturityRating { get; set; } = "U/A 13+";
    public bool InStock { get; set; } = true;
}

public sealed class StoryBooksSectionDto
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string DisplayType { get; set; } = "standard"; // "top10", "continue-reading", "standard", "grid"
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
    public List<StoryBookItemDto> Books { get; set; } = new();
}

public sealed class StoryBooksHubDto
{
    public StoryBooksThemeSettingsDto ThemeSettings { get; set; } = new();
    public StoryBooksHeroBillboardDto HeroBillboard { get; set; } = new();
    public StoryBooksTabHeroBannerDto StoryBooksBanner { get; set; } = new()
    {
        IsEnabled = true,
        Title = "Story Books & Campus Literary Journals",
        Subtitle = "Curated peer-reviewed publications, academic research papers, campus fiction, and bestselling student literature.",
        TagBadge = "EXCLUSIVE CAMPUS COLLECTION • VERIFIED EDITIONS",
        MetaTags = "Academic Editions • Peer-Reviewed • Digital & Print Ready • U/A 13+",
        ImageUrl = "/images/hero/hero-booksflix.webp",
        IsUploadingSoon = true,
        PrimaryButtonText = "Browse Collection",
        SecondaryButtonText = "Journal Info"
    };

    public StoryBooksTabHeroBannerDto NewPopularBanner { get; set; } = new()
    {
        IsEnabled = true,
        Title = "New Releases & Popular University Favorites",
        Subtitle = "Trending campus releases, student choice award nominees, and newly added library arrivals this semester.",
        TagBadge = "TRENDING THIS WEEK • #1 ON CAMPUS",
        MetaTags = "Top 10 Rankings • Award Nominees • Fresh Arrivals 2025 • Student Reviewed",
        ImageUrl = "/images/hero/hero-booksflix.webp",
        IsUploadingSoon = true,
        PrimaryButtonText = "Explore New Releases",
        SecondaryButtonText = "View Rankings"
    };

    public StoryBooksTabHeroBannerDto LanguagesBanner { get; set; } = new()
    {
        IsEnabled = true,
        Title = "Explore Literature by Global & Regional Languages",
        Subtitle = "Read original, translated, and bilingual works across 12+ international languages including English, Telugu, Hindi, Tamil, Spanish, and French.",
        TagBadge = "MULTILINGUAL CAMPUS ARCHIVE • 12+ LANGUAGES",
        MetaTags = "12+ World Languages • Dual Language • Annotated Editions • Global Student Access",
        ImageUrl = "/images/hero/hero-booksflix.webp",
        IsUploadingSoon = true,
        PrimaryButtonText = "Discover Languages",
        SecondaryButtonText = "Language Guide"
    };
    public List<StoryBookGenreDto> GenresAndJournals { get; set; } = new();

    public List<StoryBookGenreDto> Genres
    {
        get => GenresAndJournals;
        set => GenresAndJournals = value;
    }

    public List<StoryBookLanguageDto> Languages { get; set; } = new();
    public List<StoryBookPreferenceDto> Preferences { get; set; } = new();
    public List<StoryBooksSectionDto> Sections { get; set; } = new();

    public List<StoryBookItemDto> GetAllBooks()
    {
        return Sections?.SelectMany(s => s.Books ?? Enumerable.Empty<StoryBookItemDto>())
            .GroupBy(b => b.Title)
            .Select(g => g.First())
            .ToList() ?? new List<StoryBookItemDto>();
    }

    public List<StoryBookItemDto> GetContinueReadingBooks()
    {
        var crSection = Sections?.FirstOrDefault(s => s.DisplayType == "continue-reading");
        if (crSection?.Books != null && crSection.Books.Count > 0)
        {
            return crSection.Books;
        }

        return GetAllBooks().Where(b => !string.IsNullOrEmpty(b.ReadingProgress)).ToList();
    }

    public List<StoryBookItemDto> GetTop10Books()
    {
        var top10Section = Sections?.FirstOrDefault(s => s.DisplayType == "top10");
        if (top10Section?.Books != null && top10Section.Books.Count > 0)
        {
            return top10Section.Books.OrderBy(b => b.Top10Rank).ToList();
        }

        return GetAllBooks().Where(b => b.IsTop10).OrderBy(b => b.Top10Rank).ToList();
    }

    public static StoryBooksHubDto CreateDefault()
    {
        return new StoryBooksHubDto
        {
            ThemeSettings = new StoryBooksThemeSettingsDto(),
            HeroBillboard = new StoryBooksHeroBillboardDto(),
            GenresAndJournals = new List<StoryBookGenreDto>
            {
                new() { Slug = "all", Name = "All Genres & Journals", Emoji = "📚", BookCount = 36 },
                new() { Slug = "academic-journals", Name = "Academic & University Journals", Emoji = "📑", BookCount = 6 },
                new() { Slug = "campus-fiction", Name = "Campus Fiction & Rom-Coms", Emoji = "🎓", BookCount = 6 },
                new() { Slug = "mystery-thrillers", Name = "Mystery & Psychological Thrillers", Emoji = "🔍", BookCount = 5 },
                new() { Slug = "scifi-fantasy", Name = "Sci-Fi, Cyberpunk & Fantasy", Emoji = "🚀", BookCount = 5 },
                new() { Slug = "manga-comics", Name = "Manga, Graphic Novels & Comics", Emoji = "🎨", BookCount = 4 },
                new() { Slug = "biographies", Name = "Biographies & Memoirs", Emoji = "✍️", BookCount = 3 },
                new() { Slug = "self-growth", Name = "Self-Growth & Philosophy", Emoji = "🌱", BookCount = 4 },
                new() { Slug = "poetry", Name = "Poetry & Anthology", Emoji = "✒️", BookCount = 3 }
            },
            Languages = new List<StoryBookLanguageDto>
            {
                new() { Code = "en", Name = "English", NativeLabel = "English", BookCount = 18 },
                new() { Code = "te", Name = "Telugu", NativeLabel = "తెలుగు", BookCount = 6 },
                new() { Code = "hi", Name = "Hindi", NativeLabel = "हिन्दी", BookCount = 8 },
                new() { Code = "ta", Name = "Tamil", NativeLabel = "தமிழ்", BookCount = 6 },
                new() { Code = "ml", Name = "Malayalam", NativeLabel = "മലയാളം", BookCount = 5 },
                new() { Code = "kn", Name = "Kannada", NativeLabel = "ಕನ್ನಡ", BookCount = 4 },
                new() { Code = "es", Name = "Spanish", NativeLabel = "Español", BookCount = 4 },
                new() { Code = "fr", Name = "French", NativeLabel = "Français", BookCount = 4 },
                new() { Code = "de", Name = "German", NativeLabel = "Deutsch", BookCount = 3 },
                new() { Code = "ja", Name = "Japanese", NativeLabel = "日本語", BookCount = 4 },
                new() { Code = "ko", Name = "Korean", NativeLabel = "한국어", BookCount = 4 },
                new() { Code = "it", Name = "Italian", NativeLabel = "Italiano", BookCount = 3 }
            },
            Preferences = new List<StoryBookPreferenceDto>
            {
                new() { Key = "original", Label = "Original Language" },
                new() { Key = "translated", Label = "English Translated" },
                new() { Key = "bilingual", Label = "Bilingual / Dual Language Edition" },
                new() { Key = "illustrated", Label = "Illustrated & Collector's Edition" }
            },
            Sections = CreateDefaultSections()
        };
    }

    public static List<StoryBooksSectionDto> CreateDefaultSections()
    {
        return new List<StoryBooksSectionDto>
        {
            // 1. Continue Reading (Matches Screenshot 2)
            new()
            {
                Id = "continue-reading",
                Title = "Continue Reading for Student",
                Subtitle = "Pick up right where you left off in your campus reading list",
                DisplayType = "continue-reading",
                DisplayOrder = 1,
                IsActive = true,
                Books = new List<StoryBookItemDto>
                {
                    new()
                    {
                        Id = "cr1",
                        Title = "Ozark: The River's Debt",
                        Author = "Bill Dubuque",
                        Price = 449,
                        OriginalPrice = 699,
                        DiscountPercent = 35,
                        Rating = 4.8,
                        ReviewCount = 480,
                        Pages = 312,
                        PublicationYear = 2024,
                        GenreSlug = "mystery-thrillers",
                        GenreName = "Mystery & Thriller",
                        Language = "English",
                        LanguageCode = "en",
                        EditionPreference = "original",
                        ReadingProgress = "85 of 148 pages",
                        ProgressPercent = 58,
                        BadgeText = "Recently Read",
                        Synopsis = "When a financial advisor is dragged into a high-stakes lakeside operation, every decision counts in this atmospheric thriller.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "cr2",
                        Title = "Great Indian Chronicles",
                        Author = "Shashi Tharoor",
                        Price = 499,
                        OriginalPrice = 799,
                        DiscountPercent = 38,
                        Rating = 4.9,
                        ReviewCount = 720,
                        Pages = 416,
                        PublicationYear = 2023,
                        GenreSlug = "biographies",
                        GenreName = "Biographies & Memoirs",
                        Language = "English",
                        LanguageCode = "en",
                        EditionPreference = "original",
                        ReadingProgress = "140 of 416 pages",
                        ProgressPercent = 34,
                        BadgeText = "New Episode",
                        Synopsis = "A vibrant retelling of major political events through satirical and poignant retellings of legendary characters.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "cr3",
                        Title = "Sub-Inspector Yugandhar: The Midnight Murder",
                        Author = "K. Ramanathan",
                        Price = 379,
                        OriginalPrice = 599,
                        DiscountPercent = 36,
                        Rating = 4.7,
                        ReviewCount = 310,
                        Pages = 264,
                        PublicationYear = 2024,
                        GenreSlug = "mystery-thrillers",
                        GenreName = "Mystery & Thriller",
                        Language = "Telugu",
                        LanguageCode = "te",
                        EditionPreference = "original",
                        ReadingProgress = "210 of 264 pages",
                        ProgressPercent = 80,
                        BadgeText = "Top Rated",
                        Synopsis = "A gripping investigative crime novel following an honest cop solving an inexplicable locked-room puzzle in Hyderabad.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "cr4",
                        Title = "Take Charge of My Heart",
                        Author = "Aditi Singhal",
                        Price = 299,
                        OriginalPrice = 499,
                        DiscountPercent = 40,
                        Rating = 4.8,
                        ReviewCount = 650,
                        Pages = 280,
                        PublicationYear = 2024,
                        GenreSlug = "campus-fiction",
                        GenreName = "Campus Fiction",
                        Language = "Hindi",
                        LanguageCode = "hi",
                        EditionPreference = "original",
                        ReadingProgress = "45 of 280 pages",
                        ProgressPercent = 16,
                        BadgeText = "Recently Added",
                        Synopsis = "Two polar-opposite graduate students team up for a university business competition and find unexpected chemistry.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    }
                }
            },

            // 2. Top 10 Books Today (Matches Screenshot 2)
            new()
            {
                Id = "top10-today",
                Title = "Top 10 Books Today on Campus",
                Subtitle = "Ranked by reading minutes and student circulation this week",
                DisplayType = "top10",
                DisplayOrder = 2,
                IsActive = true,
                Books = new List<StoryBookItemDto>
                {
                    new()
                    {
                        Id = "top1",
                        Title = "The Midnight Library",
                        Author = "Matt Haig",
                        Price = 499,
                        OriginalPrice = 799,
                        DiscountPercent = 38,
                        Rating = 4.9,
                        ReviewCount = 2890,
                        Pages = 304,
                        PublicationYear = 2023,
                        GenreSlug = "scifi-fantasy",
                        GenreName = "Sci-Fi & Fantasy",
                        Language = "English",
                        LanguageCode = "en",
                        IsTop10 = true,
                        Top10Rank = 1,
                        BadgeText = "TOP 10",
                        Synopsis = "Between life and death there is a library where shelves go on forever and every book offers a chance to try another life.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "top2",
                        Title = "Avarachan & Sons: The Malabar Dynasty",
                        Author = "K. R. Meera",
                        Price = 420,
                        OriginalPrice = 650,
                        DiscountPercent = 35,
                        Rating = 4.8,
                        ReviewCount = 1420,
                        Pages = 360,
                        PublicationYear = 2024,
                        GenreSlug = "campus-fiction",
                        GenreName = "Campus Fiction",
                        Language = "Malayalam",
                        LanguageCode = "ml",
                        IsTop10 = true,
                        Top10Rank = 2,
                        BadgeText = "TOP 10",
                        Synopsis = "A grand multi-generational family epic chronicling heritage, rubber plantations, and modern ambitions across Kerala.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "top3",
                        Title = "Gajab Raat: Tales from Delhi After Dark",
                        Author = "Pankaj Dubey",
                        Price = 349,
                        OriginalPrice = 550,
                        DiscountPercent = 36,
                        Rating = 4.7,
                        ReviewCount = 980,
                        Pages = 240,
                        PublicationYear = 2024,
                        GenreSlug = "mystery-thrillers",
                        GenreName = "Mystery & Thriller",
                        Language = "Hindi",
                        LanguageCode = "hi",
                        IsTop10 = true,
                        Top10Rank = 3,
                        BadgeText = "TOP 10",
                        Synopsis = "Five strangers cross paths in a single night in Connaught Place, uncovering secrets that will change their destinies.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "top4",
                        Title = "Lupin: Gentleman Burglar Chronicles",
                        Author = "Maurice Leblanc",
                        Price = 399,
                        OriginalPrice = 599,
                        DiscountPercent = 33,
                        Rating = 4.9,
                        ReviewCount = 1890,
                        Pages = 280,
                        PublicationYear = 2023,
                        GenreSlug = "mystery-thrillers",
                        GenreName = "Mystery & Thriller",
                        Language = "French",
                        LanguageCode = "fr",
                        IsTop10 = true,
                        Top10Rank = 4,
                        BadgeText = "TOP 10",
                        Synopsis = "The master of disguise and quick wit outsmarts French detectives and international criminals with effortless charm.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "top5",
                        Title = "The Least Eligible Bachelor",
                        Author = "Preeti Shenoy",
                        Price = 320,
                        OriginalPrice = 499,
                        DiscountPercent = 35,
                        Rating = 4.7,
                        ReviewCount = 820,
                        Pages = 260,
                        PublicationYear = 2024,
                        GenreSlug = "campus-fiction",
                        GenreName = "Campus Fiction",
                        Language = "English",
                        LanguageCode = "en",
                        IsTop10 = true,
                        Top10Rank = 5,
                        BadgeText = "TOP 10",
                        Synopsis = "A laugh-out-loud romantic comedy set in Bangalore tech parks and college reunions.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "top6",
                        Title = "CBI 5: The Brain Archive",
                        Author = "S. N. Swamy",
                        Price = 450,
                        OriginalPrice = 699,
                        DiscountPercent = 35,
                        Rating = 4.8,
                        ReviewCount = 610,
                        Pages = 320,
                        PublicationYear = 2024,
                        GenreSlug = "mystery-thrillers",
                        GenreName = "Mystery & Thriller",
                        Language = "Malayalam",
                        LanguageCode = "ml",
                        IsTop10 = true,
                        Top10Rank = 6,
                        BadgeText = "TOP 10",
                        Synopsis = "The iconic detective Sethurama Iyer investigates an enigmatic case that puzzles the state police force.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "top7",
                        Title = "Dr. X in the Matrix",
                        Author = "Kenji Sato",
                        Price = 499,
                        OriginalPrice = 750,
                        DiscountPercent = 33,
                        Rating = 4.8,
                        ReviewCount = 740,
                        Pages = 290,
                        PublicationYear = 2024,
                        GenreSlug = "scifi-fantasy",
                        GenreName = "Sci-Fi & Fantasy",
                        Language = "Japanese",
                        LanguageCode = "ja",
                        IsTop10 = true,
                        Top10Rank = 7,
                        BadgeText = "TOP 10",
                        Synopsis = "A Tokyo neurosurgeon finds an AI consciousness lurking inside a medical scanning device.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "top8",
                        Title = "Ponniyin Selvan: The Emperor's Blade",
                        Author = "Kalki Krishnamurthy",
                        Price = 599,
                        OriginalPrice = 999,
                        DiscountPercent = 40,
                        Rating = 4.9,
                        ReviewCount = 3100,
                        Pages = 520,
                        PublicationYear = 2023,
                        GenreSlug = "campus-fiction",
                        GenreName = "Campus Fiction",
                        Language = "Tamil",
                        LanguageCode = "ta",
                        IsTop10 = true,
                        Top10Rank = 8,
                        BadgeText = "TOP 10",
                        Synopsis = "The legendary historical epic of the Chola Empire, intrigue, espionage, and unforgettable warrior friendships.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "top9",
                        Title = "Kantara Chronicles: Legends of the Forest",
                        Author = "Rishab Shetty",
                        Price = 480,
                        OriginalPrice = 750,
                        DiscountPercent = 36,
                        Rating = 4.9,
                        ReviewCount = 1450,
                        Pages = 330,
                        PublicationYear = 2024,
                        GenreSlug = "horror-legends",
                        GenreName = "Horror & Legends",
                        Language = "Kannada",
                        LanguageCode = "kn",
                        IsTop10 = true,
                        Top10Rank = 9,
                        BadgeText = "TOP 10",
                        Synopsis = "Sacred coastal legends, ancestral spirits, and humanity's eternal bond with the ancient woods.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "top10",
                        Title = "Stranger Things: The Hawkins Lab Secrets",
                        Author = "Gwenda Bond",
                        Price = 520,
                        OriginalPrice = 799,
                        DiscountPercent = 35,
                        Rating = 4.8,
                        ReviewCount = 2410,
                        Pages = 320,
                        PublicationYear = 2023,
                        GenreSlug = "scifi-fantasy",
                        GenreName = "Sci-Fi & Fantasy",
                        Language = "English",
                        LanguageCode = "en",
                        IsTop10 = true,
                        Top10Rank = 10,
                        BadgeText = "TOP 10",
                        Synopsis = "The official prequel exploring the origins of Project MKUltra and the terrifying secrets inside Hawkins laboratory.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    }
                }
            },

            // 3. Academic Journals & Research Chronicles ("journal books" requested by user)
            new()
            {
                Id = "academic-journals",
                Title = "Academic Journals & Student Research Chronicles",
                Subtitle = "Peer-reviewed university editions, case studies, and literary essays",
                DisplayType = "standard",
                DisplayOrder = 3,
                IsActive = true,
                Books = new List<StoryBookItemDto>
                {
                    new()
                    {
                        Id = "jrn1",
                        Title = "Harvard Business Review: Essential Student Case Studies",
                        Author = "HBR Press",
                        Price = 799,
                        OriginalPrice = 1199,
                        DiscountPercent = 33,
                        Rating = 4.9,
                        ReviewCount = 890,
                        Pages = 410,
                        PublicationYear = 2024,
                        GenreSlug = "academic-journals",
                        GenreName = "Academic & University Journals",
                        Language = "English",
                        LanguageCode = "en",
                        BadgeText = "Essential Academic",
                        Synopsis = "Ten landmark management, startup, and leadership case studies curated for university coursework.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "jrn2",
                        Title = "Oxford Journal of International Literature (Vol. 42)",
                        Author = "Oxford University Faculty",
                        Price = 850,
                        OriginalPrice = 1250,
                        DiscountPercent = 32,
                        Rating = 4.8,
                        ReviewCount = 420,
                        Pages = 380,
                        PublicationYear = 2024,
                        GenreSlug = "academic-journals",
                        GenreName = "Academic & University Journals",
                        Language = "English",
                        LanguageCode = "en",
                        BadgeText = "University Edition",
                        Synopsis = "Critical essays comparing post-colonial storytelling techniques in Indian, European, and East Asian fiction.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "jrn3",
                        Title = "MIT Technology Review: AI & The Creative Mind",
                        Author = "MIT Press",
                        Price = 699,
                        OriginalPrice = 999,
                        DiscountPercent = 30,
                        Rating = 4.9,
                        ReviewCount = 610,
                        Pages = 295,
                        PublicationYear = 2024,
                        GenreSlug = "academic-journals",
                        GenreName = "Academic & University Journals",
                        Language = "English",
                        LanguageCode = "en",
                        BadgeText = "Research Edition",
                        Synopsis = "Cutting-edge journal analyzing generative intelligence, computational narrative theory, and student research.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "jrn4",
                        Title = "Cambridge Anthology of Asian Literary Criticism",
                        Author = "Dr. S. Mukherjee",
                        Price = 740,
                        OriginalPrice = 1100,
                        DiscountPercent = 32,
                        Rating = 4.7,
                        ReviewCount = 310,
                        Pages = 340,
                        PublicationYear = 2023,
                        GenreSlug = "academic-journals",
                        GenreName = "Academic & University Journals",
                        Language = "English",
                        LanguageCode = "en",
                        BadgeText = "Scholarly",
                        Synopsis = "Exhaustive compilation analyzing regional dialects, folk metaphors, and narrative modernization.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    }
                }
            },

            // 4. Trending International Bestsellers (Matches Screenshot 3 titles)
            new()
            {
                Id = "trending-bestsellers",
                Title = "Because You Read Campus Mysteries & Thrillers",
                Subtitle = "High-octane page-turners recommended by fellow international students",
                DisplayType = "standard",
                DisplayOrder = 4,
                IsActive = true,
                Books = new List<StoryBookItemDto>
                {
                    new()
                    {
                        Id = "tb1",
                        Title = "The Mentalist: Art of Deduction",
                        Author = "Bruno Heller",
                        Price = 450,
                        OriginalPrice = 699,
                        DiscountPercent = 35,
                        Rating = 4.8,
                        ReviewCount = 1100,
                        Pages = 320,
                        PublicationYear = 2024,
                        GenreSlug = "mystery-thrillers",
                        GenreName = "Mystery & Thriller",
                        Language = "English",
                        LanguageCode = "en",
                        BadgeText = "Popular",
                        Synopsis = "A master mentalist assists the state bureau of investigation while playing a cat-and-mouse game with his past.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "tb2",
                        Title = "Snowpiercer: The Frozen Ark",
                        Author = "Jacques Lob",
                        Price = 490,
                        OriginalPrice = 750,
                        DiscountPercent = 34,
                        Rating = 4.7,
                        ReviewCount = 920,
                        Pages = 300,
                        PublicationYear = 2023,
                        GenreSlug = "scifi-fantasy",
                        GenreName = "Sci-Fi & Fantasy",
                        Language = "French",
                        LanguageCode = "fr",
                        BadgeText = "Cult Classic",
                        Synopsis = "On a perpetually moving train circling a frozen wasteland, class revolution brews in the tail section.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "tb3",
                        Title = "Suits: The Firm & The Prodigy",
                        Author = "Aaron Korsh",
                        Price = 475,
                        OriginalPrice = 699,
                        DiscountPercent = 32,
                        Rating = 4.9,
                        ReviewCount = 2100,
                        Pages = 350,
                        PublicationYear = 2024,
                        GenreSlug = "campus-fiction",
                        GenreName = "Campus Fiction",
                        Language = "English",
                        LanguageCode = "en",
                        BadgeText = "Bestseller",
                        Synopsis = "A top corporate closing lawyer hires a brilliant college dropout with a photographic memory.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "tb4",
                        Title = "The Good Doctor: Savant Mind",
                        Author = "David Shore",
                        Price = 420,
                        OriginalPrice = 620,
                        DiscountPercent = 32,
                        Rating = 4.8,
                        ReviewCount = 1350,
                        Pages = 280,
                        PublicationYear = 2024,
                        GenreSlug = "campus-fiction",
                        GenreName = "Campus Fiction",
                        Language = "Korean",
                        LanguageCode = "ko",
                        BadgeText = "Award Winner",
                        Synopsis = "A young surgical resident with autism and savant syndrome relocates to a prestigious teaching hospital.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    },
                    new()
                    {
                        Id = "tb5",
                        Title = "Manifest: Flight 828 Chronicles",
                        Author = "Jeff Rake",
                        Price = 460,
                        OriginalPrice = 699,
                        DiscountPercent = 34,
                        Rating = 4.7,
                        ReviewCount = 890,
                        Pages = 310,
                        PublicationYear = 2023,
                        GenreSlug = "mystery-thrillers",
                        GenreName = "Mystery & Thriller",
                        Language = "English",
                        LanguageCode = "en",
                        BadgeText = "Trending",
                        Synopsis = "A commercial airliner lands safely after severe turbulence, only for passengers to learn five years have passed.",
                        CoverImageUrl = "/images/fashion-hub/uploading-soon-placeholder.svg",
                        IsUploadingSoon = true
                    }
                }
            }
        };
    }
}
