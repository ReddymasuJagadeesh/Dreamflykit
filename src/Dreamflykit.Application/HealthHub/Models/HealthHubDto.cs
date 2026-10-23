namespace Dreamflykit.Application.HealthHub.Models;

public sealed class HealthHubDto
{
    public HealthHeroBannerDto HeroBanner { get; set; } = new();
    public HealthCategoriesSectionDto CategoriesSection { get; set; } = new();
    public HealthPopularSectionDto PopularSection { get; set; } = new();
    public HealthTrustBarSectionDto TrustBarSection { get; set; } = new();

    public static HealthHubDto CreateDefault()
    {
        return new HealthHubDto
        {
            HeroBanner = new HealthHeroBannerDto
            {
                IsEnabled = true,
                EnableSlideshow = true,
                AutoPlay = true,
                SlideIntervalSeconds = 5,
                TransitionEffect = "fade",
                BannerHeight = 440,
                ThemeColor = "#0d7a5f",
                ThemeAccentColor = "#059669",
                Slides = new List<HealthHeroSlideDto>
                {
                    new()
                    {
                        Id = "slide-1",
                        DiscountBadge = "FLAT 20% OFF",
                        Title = "Your Health,",
                        TitleHighlight = "Our Priority.",
                        Subtitle = "Wide range of medicines & healthcare products delivered to your doorstep.",
                        ImageUrl = "/images/health-hub/health_hero_banner_1.jpg",
                        TargetUrl = "/products?category=health-care",
                        DisplayOrder = 1,
                        IsActive = true,
                        Badges = new List<HealthHeroBadgeDto>
                        {
                            new() { Id = "b1", Label = "100% Genuine Medicines", Icon = "shield-check" },
                            new() { Id = "b2", Label = "Doctor Approved", Icon = "stethoscope" },
                            new() { Id = "b3", Label = "Easy Returns", Icon = "refresh" },
                            new() { Id = "b4", Label = "Fast & Safe Delivery", Icon = "truck" }
                        }
                    },
                    new()
                    {
                        Id = "slide-2",
                        DiscountBadge = "FLAT 25% OFF",
                        Title = "Wellness & Care,",
                        TitleHighlight = "For Every Student.",
                        Subtitle = "Authentic Ayurveda, vitamins, daily immunity boosters, and health monitoring kits.",
                        ImageUrl = "/images/health-hub/health_hero_banner_2.jpg",
                        TargetUrl = "/products?category=health-care",
                        DisplayOrder = 2,
                        IsActive = true,
                        Badges = new List<HealthHeroBadgeDto>
                        {
                            new() { Id = "b5", Label = "Pure & Certified", Icon = "shield-check" },
                            new() { Id = "b6", Label = "Pharmacist Verified", Icon = "stethoscope" },
                            new() { Id = "b7", Label = "Express Dispatch", Icon = "truck" },
                            new() { Id = "b8", Label = "24/7 Health Desk", Icon = "refresh" }
                        }
                    }
                }
            },
            CategoriesSection = new HealthCategoriesSectionDto
            {
                IsEnabled = true,
                Title = "Shop by Category",
                ViewAllText = "View All >",
                ViewAllUrl = "/products?category=health-care",
                Categories = new List<HealthCategoryCardDto>
                {
                    new() { Id = "c1", Name = "Medicines", Slug = "medicines", Icon = "pills", ImageUrl = "/images/health-hub/uploading-soon-placeholder.svg", DisplayOrder = 1, IsUploadingSoon = false, ProductUrl = "/products?category=health-care&q=Medicines" },
                    new() { Id = "c2", Name = "Lab Tests", Slug = "lab-tests", Icon = "flask", ImageUrl = "/images/health-hub/uploading-soon-placeholder.svg", DisplayOrder = 2, IsUploadingSoon = false, ProductUrl = "/products?category=health-care&q=Lab+Tests" },
                    new() { Id = "c3", Name = "Health Conditions", Slug = "health-conditions", Icon = "heart-pulse", ImageUrl = "/images/health-hub/uploading-soon-placeholder.svg", DisplayOrder = 3, IsUploadingSoon = false, ProductUrl = "/products?category=health-care&q=Health+Conditions" },
                    new() { Id = "c4", Name = "Personal Care", Slug = "personal-care", Icon = "sparkles", ImageUrl = "/images/health-hub/uploading-soon-placeholder.svg", DisplayOrder = 4, IsUploadingSoon = false, ProductUrl = "/products?category=health-care&q=Personal+Care" },
                    new() { Id = "c5", Name = "Health Devices", Slug = "health-devices", Icon = "activity", ImageUrl = "/images/health-hub/uploading-soon-placeholder.svg", DisplayOrder = 5, IsUploadingSoon = false, ProductUrl = "/products?category=health-care&q=Health+Devices" },
                    new() { Id = "c6", Name = "Baby Care", Slug = "baby-care", Icon = "smile", ImageUrl = "/images/health-hub/uploading-soon-placeholder.svg", DisplayOrder = 6, IsUploadingSoon = false, ProductUrl = "/products?category=health-care&q=Baby+Care" },
                    new() { Id = "c7", Name = "Elderly Care", Slug = "elderly-care", Icon = "users", ImageUrl = "/images/health-hub/uploading-soon-placeholder.svg", DisplayOrder = 7, IsUploadingSoon = false, ProductUrl = "/products?category=health-care&q=Elderly+Care" },
                    new() { Id = "c8", Name = "Offers", Slug = "offers", Icon = "percent", ImageUrl = "/images/health-hub/uploading-soon-placeholder.svg", DisplayOrder = 8, IsUploadingSoon = false, ProductUrl = "/products?category=health-care&minDiscount=15" },
                    new() { Id = "c9", Name = "Prescription Upload", Slug = "prescription-upload", Icon = "file-text", ImageUrl = "/images/health-hub/uploading-soon-placeholder.svg", DisplayOrder = 9, IsUploadingSoon = false, ProductUrl = "/products?category=health-care&q=Prescription" }
                }
            },
            PopularSection = new HealthPopularSectionDto
            {
                IsEnabled = true,
                Title = "Popular Health Categories",
                ViewAllText = "View All >",
                ViewAllUrl = "/products?category=health-care",
                Categories = new List<HealthConditionCardDto>
                {
                    new() { Id = "pc1", Name = "Pain Relief", Slug = "pain-relief", Icon = "brain", DisplayOrder = 1, ProductUrl = "/products?category=health-care&q=Pain+Relief" },
                    new() { Id = "pc2", Name = "Fever", Slug = "fever", Icon = "thermometer", DisplayOrder = 2, ProductUrl = "/products?category=health-care&q=Fever" },
                    new() { Id = "pc3", Name = "Cough & Cold", Slug = "cough-cold", Icon = "wind", DisplayOrder = 3, ProductUrl = "/products?category=health-care&q=Cough+Cold" },
                    new() { Id = "pc4", Name = "Diabetes Care", Slug = "diabetes-care", Icon = "droplet", DisplayOrder = 4, ProductUrl = "/products?category=health-care&q=Diabetes" },
                    new() { Id = "pc5", Name = "Heart Care", Slug = "heart-care", Icon = "heart", DisplayOrder = 5, ProductUrl = "/products?category=health-care&q=Heart+Care" },
                    new() { Id = "pc6", Name = "Vitamins & Supplements", Slug = "vitamins-supplements", Icon = "capsule", DisplayOrder = 6, ProductUrl = "/products?category=health-care&q=Vitamins" },
                    new() { Id = "pc7", Name = "Stomach Care", Slug = "stomach-care", Icon = "shield", DisplayOrder = 7, ProductUrl = "/products?category=health-care&q=Stomach" },
                    new() { Id = "pc8", Name = "Skin Care", Slug = "skin-care", Icon = "sun", DisplayOrder = 8, ProductUrl = "/products?category=health-care&q=Skin+Care" },
                    new() { Id = "pc9", Name = "Women Care", Slug = "women-care", Icon = "user", DisplayOrder = 9, ProductUrl = "/products?category=health-care&q=Women+Care" },
                    new() { Id = "pc10", Name = "Ayurveda", Slug = "ayurveda", Icon = "feather", DisplayOrder = 10, ProductUrl = "/products?category=health-care&q=Ayurveda" }
                }
            },
            TrustBarSection = new HealthTrustBarSectionDto
            {
                IsEnabled = true,
                Items = new List<HealthTrustItemDto>
                {
                    new() { Id = "tb1", Title = "100% Genuine Medicines", Subtitle = "Sourced from trusted brands", Icon = "shield-check" },
                    new() { Id = "tb2", Title = "Flat 20% OFF", Subtitle = "On all medicines", Icon = "tag" },
                    new() { Id = "tb3", Title = "Easy Returns", Subtitle = "Hassle-free returns", Icon = "refresh-cw" },
                    new() { Id = "tb4", Title = "Fast Delivery", Subtitle = "On-time, every time", Icon = "truck" },
                    new() { Id = "tb5", Title = "Secure Payments", Subtitle = "100% safe & secure", Icon = "lock" },
                    new() { Id = "tb6", Title = "24/7 Customer Support", Subtitle = "We're here for you", Icon = "headphones" }
                }
            }
        };
    }
}

public sealed class HealthHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public bool EnableSlideshow { get; set; } = true;
    public bool AutoPlay { get; set; } = true;
    public int SlideIntervalSeconds { get; set; } = 5;
    public string TransitionEffect { get; set; } = "fade";
    public int BannerHeight { get; set; } = 440;
    public string ThemeColor { get; set; } = "#0d7a5f";
    public string ThemeAccentColor { get; set; } = "#059669";
    public List<HealthHeroSlideDto> Slides { get; set; } = new();
}

public sealed class HealthHeroSlideDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string DiscountBadge { get; set; } = "FLAT 20% OFF";
    public string Title { get; set; } = "Your Health,";
    public string TitleHighlight { get; set; } = "Our Priority.";
    public string Subtitle { get; set; } = "Wide range of medicines & healthcare products delivered to your doorstep.";
    public string ImageUrl { get; set; } = "/images/health-hub/health_hero_banner_1.jpg";
    public string TargetUrl { get; set; } = "/products?category=health-care";
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
    public List<HealthHeroBadgeDto> Badges { get; set; } = new();
}

public sealed class HealthHeroBadgeDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Label { get; set; } = string.Empty;
    public string Icon { get; set; } = "check";
    public bool IsActive { get; set; } = true;
}

public sealed class HealthCategoriesSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Shop by Category";
    public string ViewAllText { get; set; } = "View All >";
    public string ViewAllUrl { get; set; } = "/products?category=health-care";
    public List<HealthCategoryCardDto> Categories { get; set; } = new();
}

public sealed class HealthCategoryCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = "/images/health-hub/uploading-soon-placeholder.svg";
    public string ProductUrl { get; set; } = "/products?category=health-care";
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
    public bool IsUploadingSoon { get; set; } = false;
}

public sealed class HealthPopularSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public string Title { get; set; } = "Popular Health Categories";
    public string ViewAllText { get; set; } = "View All >";
    public string ViewAllUrl { get; set; } = "/products?category=health-care";
    public List<HealthConditionCardDto> Categories { get; set; } = new();
}

public sealed class HealthConditionCardDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string ProductUrl { get; set; } = "/products?category=health-care";
    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; } = true;
}

public sealed class HealthTrustBarSectionDto
{
    public bool IsEnabled { get; set; } = true;
    public List<HealthTrustItemDto> Items { get; set; } = new();
}

public sealed class HealthTrustItemDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
