namespace Dreamflykit.Application.FoodHub.Models;

public sealed class FoodHubDto
{
    public FoodThemeSettingsDto ThemeSettings { get; set; } = new();
    public FoodTopNoticeBarDto TopNoticeBar { get; set; } = new();
    public FoodHeroBannerDto HeroBanner { get; set; } = new();
    public FoodShopByCategoriesDto ShopByCategories { get; set; } = new();
    public FoodPromoCardsDto PromoCards { get; set; } = new();
    public FoodBestSellersDto BestSellers { get; set; } = new();
    public FoodBottomTrustBarDto BottomTrustBar { get; set; } = new();
    public FoodStayUpdatedDto StayUpdated { get; set; } = new();

    public static FoodHubDto CreateDefault()
    {
        return new FoodHubDto
        {
            ThemeSettings = new FoodThemeSettingsDto
            {
                StoreTitle = "FreshBasket • Food & Grocery",
                AccentColor = "#1f3d24",
                PrimaryGreen = "#234d20",
                SoftBackground = "#fbf9f5"
            },

            TopNoticeBar = new FoodTopNoticeBarDto
            {
                IsEnabled = true,
                Item1Text = "Free Shipping on orders above ₹499",
                Item2Text = "Best Quality Products",
                Item3Text = "Secure Payments",
                Item4Text = "Easy Returns"
            },

            HeroBanner = new FoodHeroBannerDto
            {
                IsEnabled = true,
                Heading = "Good Food. Good Mood.",
                Subtitle = "Your one-stop shop for groceries, snacks & more.",
                ButtonText = "Shop Now →",
                ButtonUrl = "#best-sellers-section",
                ImageUrl = "/images/food-hub/hero-food-basket.webp",
                IsUploadingSoon = false,
                TrustBadges = new List<string>
                {
                    "100% Quality Products",
                    "Wide Variety",
                    "Best Prices Everyday",
                    "Delivered With Care"
                }
            },

            ShopByCategories = new FoodShopByCategoriesDto
            {
                Heading = "Shop By Categories",
                Cards = new List<FoodCategoryCardDto>
                {
                    new()
                    {
                        Id = "cat-groceries",
                        Title = "Groceries",
                        Slug = "groceries",
                        ImageUrl = "/images/food-hub/cat-groceries.webp",
                        TargetUrl = "/category/food?subcatItem=Groceries",
                        Icon = "basket",
                        Items = new List<string> { "Flour", "Edible Oils", "Tea & Coffee Powders", "Ready to Cook" }
                    },
                    new()
                    {
                        Id = "cat-instant-noodles",
                        Title = "Instant Noodles",
                        Slug = "instant-noodles",
                        ImageUrl = "/images/food-hub/cat-noodles.webp",
                        TargetUrl = "/category/food?subcatItem=Instant Noodles",
                        Icon = "noodles",
                        Items = new List<string> { "Maggi 2-Minute", "Ramen & Cup Noodles", "Hakka Noodles", "Pasta & Vermicelli" }
                    },
                    new()
                    {
                        Id = "cat-nuts-dry-fruits",
                        Title = "Nuts & Dry Fruits",
                        Slug = "nuts-dry-fruits",
                        ImageUrl = "/images/food-hub/cat-nuts.webp",
                        TargetUrl = "/category/food?subcatItem=Nuts & Dry Fruits",
                        Icon = "nuts",
                        Items = new List<string> { "California Almonds", "Whole Cashews", "Golden Raisins", "Walnuts & Pistachios" }
                    },
                    new()
                    {
                        Id = "cat-spices-seasonings",
                        Title = "Spices & Seasonings",
                        Slug = "spices-seasonings",
                        ImageUrl = "/images/food-hub/cat-spices.webp",
                        TargetUrl = "/category/food?subcatItem=Spices & Seasonings",
                        Icon = "spices",
                        Items = new List<string> { "Turmeric Powder", "Red Chili Powder", "Garam Masala", "Cumin & Coriander" }
                    },
                    new()
                    {
                        Id = "cat-breakfast-cereals",
                        Title = "Breakfast Cereals",
                        Slug = "breakfast-cereals",
                        ImageUrl = "/images/food-hub/cat-cereals.webp",
                        TargetUrl = "/category/food?subcatItem=Breakfast Cereals",
                        Icon = "cereal",
                        Items = new List<string> { "Corn Flakes", "Rolled Oats", "Muesli & Granola", "Chocos & Puffs" }
                    },
                    new()
                    {
                        Id = "cat-snacks",
                        Title = "Snacks",
                        Slug = "snacks",
                        ImageUrl = "/images/food-hub/cat-snacks.webp",
                        TargetUrl = "/category/food?subcatItem=Snacks",
                        Icon = "snacks",
                        Items = new List<string> { "Potato Chips", "Bhujia & Namkeen", "Biscuits & Cookies", "Roasted Makhana" }
                    },
                    new()
                    {
                        Id = "cat-pickles",
                        Title = "Pickles",
                        Slug = "pickles",
                        ImageUrl = "/images/food-hub/cat-pickles.webp",
                        TargetUrl = "/category/food?subcatItem=Pickles",
                        Icon = "pickles",
                        Items = new List<string> { "Mixed Vegetable", "Mango Pickle", "Lemon & Garlic", "Green Chili Thecha" }
                    }
                }
            },

            PromoCards = new FoodPromoCardsDto
            {
                IsEnabled = true,
                Cards = new List<FoodPromoCardItemDto>
                {
                    new()
                    {
                        Id = "promo-1",
                        Heading = "Daily Essentials at Best Prices",
                        Subtitle = "Top quality groceries for your everyday needs.",
                        ButtonText = "Shop Groceries →",
                        ButtonUrl = "/category/food?subcatItem=Groceries",
                        ImageUrl = "/images/food-hub/promo-groceries.webp",
                        BgColor = "#f7f0e6"
                    },
                    new()
                    {
                        Id = "promo-2",
                        Heading = "Crunchy, Tasty & Irresistible",
                        Subtitle = "Explore a wide range of snacks for every mood.",
                        ButtonText = "Shop Snacks →",
                        ButtonUrl = "/category/food?subcatItem=Snacks",
                        ImageUrl = "/images/food-hub/promo-snacks.webp",
                        BgColor = "#fdf4e7"
                    },
                    new()
                    {
                        Id = "promo-3",
                        Heading = "Pickles That Make Every Meal Special",
                        Subtitle = "Authentic taste. Traditional recipes.",
                        ButtonText = "Shop Pickles →",
                        ButtonUrl = "/category/food?subcatItem=Pickles",
                        ImageUrl = "/images/food-hub/promo-pickles.webp",
                        BgColor = "#f6efe7"
                    }
                }
            },

            BestSellers = new FoodBestSellersDto
            {
                Heading = "Best Sellers",
                ViewAllText = "View All",
                ViewAllUrl = "/category/food",
                Items = new List<FoodCuratedProductDto>
                {
                    new()
                    {
                        Title = "Aashirvaad Superior MP Whole Wheat Atta 5kg",
                        Subtitle = "5kg Pouch • 100% Pure Sharbati Wheat",
                        Price = 279,
                        OriginalPrice = 310,
                        Rating = 4.6m,
                        ReviewCount = 2300,
                        ImageUrl = "/images/products/food/aashirvaad-atta.webp",
                        SubcategorySlug = "groceries",
                        Badge = "BESTSELLER"
                    },
                    new()
                    {
                        Title = "Fortune Sunlite Refined Sunflower Oil 1L",
                        Subtitle = "1 Litre Pouch • Rich in Vitamin E & Low Absorb",
                        Price = 145,
                        OriginalPrice = 165,
                        Rating = 4.5m,
                        ReviewCount = 1800,
                        ImageUrl = "/images/products/food/fortune-sunflower-oil.webp",
                        SubcategorySlug = "groceries",
                        Badge = "MUST HAVE"
                    },
                    new()
                    {
                        Title = "Tata Tea Agni Strong Leaf Tea 1kg",
                        Subtitle = "1kg Pack • Extra Strong Kadak Taste with 10% Extra Leaves",
                        Price = 245,
                        OriginalPrice = 270,
                        Rating = 4.6m,
                        ReviewCount = 1500,
                        ImageUrl = "/images/products/food/tata-tea-agni.webp",
                        SubcategorySlug = "groceries",
                        Badge = "TOP RATED"
                    },
                    new()
                    {
                        Title = "Maggi 2-Minute Masala Instant Noodles 280g",
                        Subtitle = "Pack of 4 • Classic Indian Spice Blend",
                        Price = 55,
                        OriginalPrice = 65,
                        Rating = 4.4m,
                        ReviewCount = 4200,
                        ImageUrl = "/images/products/food/maggi-noodles.webp",
                        SubcategorySlug = "instant-noodles",
                        Badge = "HOT DEAL"
                    },
                    new()
                    {
                        Title = "California Whole Natural Almonds 200g",
                        Subtitle = "200g Pouch • High Protein, Vacuum Packed Fresh",
                        Price = 199,
                        OriginalPrice = 230,
                        Rating = 4.7m,
                        ReviewCount = 980,
                        ImageUrl = "/images/products/food/california-almonds.webp",
                        SubcategorySlug = "nuts-dry-fruits",
                        Badge = "PREMIUM"
                    },
                    new()
                    {
                        Title = "Everest Pure Turmeric (Haldi) Powder 200g",
                        Subtitle = "200g Box • High Curcumin, Golden Color & Aroma",
                        Price = 70,
                        OriginalPrice = 80,
                        Rating = 4.6m,
                        ReviewCount = 1200,
                        ImageUrl = "/images/products/food/everest-turmeric.webp",
                        SubcategorySlug = "spices-seasonings",
                        Badge = "ESSENTIAL"
                    },
                    new()
                    {
                        Title = "Kellogg's Real Almond & Honey Corn Flakes 475g",
                        Subtitle = "475g Carton • Crunchy Corn Flakes with Real Almonds",
                        Price = 185,
                        OriginalPrice = 210,
                        Rating = 4.5m,
                        ReviewCount = 1100,
                        ImageUrl = "/images/products/food/kelloggs-corn-flakes.webp",
                        SubcategorySlug = "breakfast-cereals",
                        Badge = "HEALTHY"
                    }
                }
            },

            BottomTrustBar = new FoodBottomTrustBarDto
            {
                Item1Title = "Free Shipping",
                Item1Subtitle = "On orders above ₹499",
                Item2Title = "Secure Payments",
                Item2Subtitle = "100% safe & secure",
                Item3Title = "Easy Returns",
                Item3Subtitle = "7-day hassle free returns",
                Item4Title = "Customer Support",
                Item4Subtitle = "We're here to help you"
            },

            StayUpdated = new FoodStayUpdatedDto
            {
                Heading = "Stay Updated!",
                Subtitle = "Subscribe to get exclusive offers and new product updates.",
                ButtonText = "Subscribe",
                ImageUrl = "/images/food-hub/newsletter-basket.webp"
            }
        };
    }
}

public sealed class FoodThemeSettingsDto
{
    public string StoreTitle { get; set; } = "FreshBasket • Food & Grocery";
    public string AccentColor { get; set; } = "#1f3d24";
    public string PrimaryGreen { get; set; } = "#234d20";
    public string SoftBackground { get; set; } = "#fbf9f5";
}

public sealed class FoodTopNoticeBarDto
{
    public bool IsEnabled { get; set; } = true;
    public string Item1Text { get; set; } = "Free Shipping on orders above ₹499";
    public string Item2Text { get; set; } = "Best Quality Products";
    public string Item3Text { get; set; } = "Secure Payments";
    public string Item4Text { get; set; } = "Easy Returns";
}

public sealed class FoodHeroBannerDto
{
    public bool IsEnabled { get; set; } = true;
    public string Heading { get; set; } = "Good Food. Good Mood.";
    public string Subtitle { get; set; } = "Your one-stop shop for groceries, snacks & more.";
    public string ButtonText { get; set; } = "Shop Now →";
    public string ButtonUrl { get; set; } = "#best-sellers-section";
    public string ImageUrl { get; set; } = "/images/food-hub/hero-food-basket.webp";
    public bool IsUploadingSoon { get; set; } = false;
    public List<string> TrustBadges { get; set; } = new();
}

public sealed class FoodShopByCategoriesDto
{
    public string Heading { get; set; } = "Shop By Categories";
    public List<FoodCategoryCardDto> Cards { get; set; } = new();
}

public sealed class FoodCategoryCardDto
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = string.Empty;
    public string Icon { get; set; } = "basket";
    public List<string> Items { get; set; } = new();
}

public sealed class FoodPromoCardsDto
{
    public bool IsEnabled { get; set; } = true;
    public List<FoodPromoCardItemDto> Cards { get; set; } = new();
}

public sealed class FoodPromoCardItemDto
{
    public string Id { get; set; } = string.Empty;
    public string Heading { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ButtonText { get; set; } = string.Empty;
    public string ButtonUrl { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string BgColor { get; set; } = "#f7f0e6";
}

public sealed class FoodBestSellersDto
{
    public string Heading { get; set; } = "Best Sellers";
    public string ViewAllText { get; set; } = "View All";
    public string ViewAllUrl { get; set; } = "/category/food";
    public List<FoodCuratedProductDto> Items { get; set; } = new();
}

public sealed class FoodCuratedProductDto
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal Rating { get; set; } = 4.5m;
    public int ReviewCount { get; set; } = 1000;
    public string ImageUrl { get; set; } = string.Empty;
    public string SubcategorySlug { get; set; } = string.Empty;
    public string Badge { get; set; } = "BESTSELLER";

    public int DiscountPercentage => OriginalPrice > Price && OriginalPrice > 0
        ? (int)Math.Round((OriginalPrice - Price) / OriginalPrice * 100m)
        : 0;
}

public sealed class FoodBottomTrustBarDto
{
    public string Item1Title { get; set; } = "Free Shipping";
    public string Item1Subtitle { get; set; } = "On orders above ₹499";
    public string Item2Title { get; set; } = "Secure Payments";
    public string Item2Subtitle { get; set; } = "100% safe & secure";
    public string Item3Title { get; set; } = "Easy Returns";
    public string Item3Subtitle { get; set; } = "7-day hassle free returns";
    public string Item4Title { get; set; } = "Customer Support";
    public string Item4Subtitle { get; set; } = "We're here to help you";
}

public sealed class FoodStayUpdatedDto
{
    public string Heading { get; set; } = "Stay Updated!";
    public string Subtitle { get; set; } = "Subscribe to get exclusive offers and new product updates.";
    public string ButtonText { get; set; } = "Subscribe";
    public string ImageUrl { get; set; } = "/images/food-hub/newsletter-basket.webp";
}

