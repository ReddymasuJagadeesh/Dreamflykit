using Dreamflykit.Application.Email.Interfaces;
using Dreamflykit.Infrastructure.Email;
using Dreamflykit.Application.Addressing.Interfaces;
using Dreamflykit.Application.Addressing.Services;
using Dreamflykit.Application.Caching.Interfaces;
using Dreamflykit.Application.Cart.Interfaces;
using Dreamflykit.Application.Cart.Services;
using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Services;
using Dreamflykit.Application.Categories.Interfaces;
using Dreamflykit.Application.Categories.Services;
using Dreamflykit.Application.Geography;
using Dreamflykit.Application.Kits.Interfaces;
using Dreamflykit.Application.Kits.Services;
using Dreamflykit.Application.Orders.Interfaces;
using Dreamflykit.Application.Orders.Services;
using Dreamflykit.Application.Payments.Interfaces;
using Dreamflykit.Application.Wishlist.Interfaces;
using Dreamflykit.Application.Wishlist.Services;
using Dreamflykit.Infrastructure.Addressing;
using Dreamflykit.Infrastructure.Caching;
using Dreamflykit.Infrastructure.Cart;
using Dreamflykit.Infrastructure.Categories;
using Dreamflykit.Infrastructure.Geography;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Infrastructure.Kits;
using Dreamflykit.Infrastructure.Orders;
using Dreamflykit.Infrastructure.Payments;
using Dreamflykit.Infrastructure.Wishlist;
using Dreamflykit.Infrastructure.Catalog;
using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Options;
using Dreamflykit.Infrastructure.Media;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Dreamflykit.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        RegisterDatabase(
            services,
            configuration);

        RegisterIdentity(services);

        RegisterApplicationServices(services, configuration);

        RegisterCaching(services, configuration);

        RegisterEmail(services, configuration);

        return services;
    }

    private static void RegisterDatabase(
        IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString(
                "DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'DefaultConnection' was not found.");
        }

        services.AddDbContext<DreamflykitDbContext>(
            options =>
            {
                options.UseNpgsql(connectionString);
            });
    }

    private static void RegisterIdentity(
        IServiceCollection services)
    {
        services
            .AddIdentityCore<ApplicationUser>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;

                    options.Password.RequiredLength = 12;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;

                    options.Lockout.MaxFailedAccessAttempts = 5;

                    options.Lockout.DefaultLockoutTimeSpan =
                        TimeSpan.FromMinutes(15);

                    options.Lockout.AllowedForNewUsers = true;
                })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<DreamflykitDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(
            options =>
            {
                options.Cookie.Name =
                    "Dreamflykit.Auth";

                options.LoginPath =
                    "/Account/Login";

                options.AccessDeniedPath =
                    "/Account/AccessDenied";

                options.ExpireTimeSpan =
                    TimeSpan.FromHours(8);

                options.SlidingExpiration = true;

                options.Cookie.HttpOnly = true;

                options.Cookie.SecurePolicy =
                    Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;

                options.Cookie.SameSite =
                    Microsoft.AspNetCore.Http.SameSiteMode.Lax;

                options.Cookie.IsEssential = true;
            });
    }

    private static void RegisterApplicationServices(
        IServiceCollection services,
        IConfiguration configuration)
    {
        // Find My Kit

        services.AddScoped<
            IFindMyKitRepository,
            FindMyKitRepository>();

        services.AddScoped<
            IFindMyKitService,
            FindMyKitService>();


        // Geography

        services.AddScoped<
            ICountryRepository,
            CountryRepository>();

        services.AddScoped<
            ICountryService,
            CountryService>();


        // Kits

        services.AddScoped<
            IKitRepository,
            KitRepository>();

        services.AddScoped<
            IKitService,
            KitService>();


        // Catalog / Products

        services.AddScoped<
            IProductRepository,
            ProductRepository>();

        services.AddScoped<
            IProductService,
            ProductService>();

        services.AddScoped<
            IProductReviewService,
            ProductReviewService>();


        // Categories

        services.AddScoped<
            ICategoryRepository,
            CategoryRepository>();

        services.AddScoped<
            ICategoryService,
            CategoryService>();


        // Cart

        services.AddScoped<
            ICartRepository,
            CartRepository>();

        services.AddScoped<
            ICartService,
            CartService>();


        // Addressing

        services.AddScoped<
            IAddressRepository,
            AddressRepository>();

        services.AddScoped<
            IAddressService,
            AddressService>();


        // Orders

        services.AddScoped<
            IOrderRepository,
            OrderRepository>();

        services.AddScoped<
            IOrderService,
            OrderService>();


        // Wishlist

        services.AddScoped<
            IWishlistRepository,
            WishlistRepository>();

        services.AddScoped<
            IWishlistService,
            WishlistService>();


        // Payment Gateway (Cashfree)

        services.Configure<CashfreePaymentOptions>(
            configuration.GetSection(CashfreePaymentOptions.SectionName));

        services.AddHttpClient<IPaymentService, CashfreePaymentService>();


        // Inventory & Concurrency Management

        services.AddScoped<
            Dreamflykit.Application.Inventory.Interfaces.IInventoryRepository,
            Dreamflykit.Infrastructure.Inventory.InventoryRepository>();

        services.AddScoped<
            Dreamflykit.Application.Inventory.Interfaces.IInventoryService,
            Dreamflykit.Application.Inventory.Services.InventoryService>();


        // Media Management System

        services.Configure<MediaOptions>(
            configuration.GetSection(MediaOptions.SectionName));

        services.Configure<S3Options>(
            configuration.GetSection(S3Options.SectionName));

        services.AddScoped<IMediaStorageService, S3MediaStorageService>();
        services.AddScoped<MediaValidationService>();
        services.AddScoped<IMediaUrlService, MediaUrlService>();
        services.AddScoped<IMediaService, MediaService>();
        services.AddScoped<IMediaPermissionService, MediaPermissionService>();
        services.AddScoped<IHeaderAdService, HeaderAdService>();
        services.AddHostedService<MediaCleanupBackgroundService>();

        // Multi-tier distributed & memory caching (Redis L2 + Memory L1)
        RegisterCaching(services, configuration);
        services.AddScoped<Dreamflykit.Application.Catalog.Interfaces.ICategoryHubRepository, Dreamflykit.Infrastructure.Catalog.CategoryHubRepository>();
        services.AddScoped<Dreamflykit.Application.Theme.IThemeSettingService, Dreamflykit.Infrastructure.Theme.ThemeSettingService>();

        // Header & Logo Branding Management
        services.AddScoped<Dreamflykit.Application.Logo.ILogoSettingService, Dreamflykit.Infrastructure.Logo.LogoSettingService>();

        // Study Essentials Hub Service
        services.AddScoped<Dreamflykit.Application.StudyHub.Interfaces.IStudyHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.StudyHub.StudyHubService>(sp));

        // Fashion Category Hub Service
        services.AddScoped<Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.FashionHub.FashionHubService>(sp));

        // Health Care Category Hub Service
        services.AddScoped<Dreamflykit.Application.HealthHub.Interfaces.IHealthHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.HealthHub.HealthHubService>(sp));

        // Mobiles Category Hub Service (Flipkart Template & 3D Visualization)
        services.AddScoped<Dreamflykit.Application.MobilesHub.Interfaces.IMobilesHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.MobilesHub.MobilesHubService>(sp));

        // Appliances Category Hub Service (Flipkart TV & Appliances Store Template)
        services.AddScoped<Dreamflykit.Application.AppliancesHub.Interfaces.IAppliancesHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.AppliancesHub.AppliancesHubService>(sp));

        // Beauty Category Hub Service (Flipkart Beauty & Personal Care Store Template)
        services.AddScoped<Dreamflykit.Application.BeautyHub.Interfaces.IBeautyHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.BeautyHub.BeautyHubService>(sp));

        // Food & Grocery Category Hub Service (FreshBasket Template)
        services.AddScoped<Dreamflykit.Application.FoodHub.Interfaces.IFoodHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.FoodHub.FoodHubService>(sp));

        // Travel Gear Category Hub Service (Dreamflykit Travel Template)
        services.AddScoped<Dreamflykit.Application.TravelHub.Interfaces.ITravelHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.TravelHub.TravelHubService>(sp));

        // Luggage & Bags Category Hub Service
        services.AddScoped<Dreamflykit.Application.LuggageHub.Interfaces.ILuggageHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.LuggageHub.LuggageHubService>(sp));

        // Electronics Category Hub Service
        services.AddScoped<Dreamflykit.Application.ElectronicsHub.Interfaces.IElectronicsHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.ElectronicsHub.ElectronicsHubService>(sp));

        // Story Books Category Hub Service (BooksFlix Netflix Theme)
        services.AddScoped<Dreamflykit.Application.StoryBooksHub.Interfaces.IStoryBooksHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.StoryBooksHub.StoryBooksHubService>(sp));

        // Gaming Hub Service (Consoles, VR, Retro Arcades & Accessories)
        services.AddScoped<Dreamflykit.Application.GamingHub.Interfaces.IGamingHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.GamingHub.GamingHubService>(sp));

        // Sports Hub Service (12 Sports Categories, Subcategory Hubs & Products)
        services.AddScoped<Dreamflykit.Application.SportsHub.Interfaces.ISportsHubService>(sp =>
            ActivatorUtilities.CreateInstance<Dreamflykit.Infrastructure.SportsHub.SportsHubService>(sp));

        // 3-Section Synchronized Category Sliders (All Categories)
        services.AddScoped<Dreamflykit.Application.CategoryShowcase.Interfaces.ICategoryThreeSectionSliderService, Dreamflykit.Infrastructure.CategoryShowcase.CategoryThreeSectionSliderService>();

        // Country Kits Hub Service (Customized Student Kits for USA, UK, Canada, etc.)
        services.AddScoped<Dreamflykit.Application.CountryKits.Interfaces.ICountryKitsHubService, Dreamflykit.Infrastructure.CountryKits.CountryKitsHubService>();

        // Central Enterprise Audit Logging
        services.AddScoped<Dreamflykit.Application.Audit.Interfaces.IAuditLogService, Dreamflykit.Infrastructure.Audit.AuditLogService>();

        // Shipping & Fulfillment Provider
        services.AddScoped<Dreamflykit.Application.Shipping.Interfaces.IShippingService, Dreamflykit.Infrastructure.Shipping.DefaultShippingService>();
    }

    private static void RegisterCaching(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();

        var redisConnection = configuration.GetConnectionString("Redis");
        if (!string.IsNullOrWhiteSpace(redisConnection))
        {
            try
            {
                var configOptions = ConfigurationOptions.Parse(redisConnection);
                configOptions.AbortOnConnectFail = false;
                configOptions.ConnectTimeout = 3000;
                configOptions.SyncTimeout = 3000;

                services.AddStackExchangeRedisCache(options =>
                {
                    options.ConfigurationOptions = configOptions;
                    options.InstanceName = "Dreamflykit:";
                });

                services.AddSingleton<IConnectionMultiplexer>(sp =>
                {
                    return ConnectionMultiplexer.Connect(configOptions);
                });
            }
            catch (Exception)
            {
                services.AddDistributedMemoryCache();
            }
        }
        else
        {
            services.AddDistributedMemoryCache();
        }

        services.AddSingleton<ICacheService, CacheService>();
    }

    private static void RegisterEmail(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailSettings>(
            configuration.GetSection(EmailSettings.SectionName));

        services.AddScoped<IEmailService, SmtpEmailSender>();
    }
}