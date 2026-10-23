using Dreamflykit.Application.Addressing.Interfaces;
using Dreamflykit.Application.Addressing.Services;
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

        // Theme & Typography Management
        services.AddMemoryCache();
        services.AddScoped<Dreamflykit.Application.Theme.IThemeSettingService, Dreamflykit.Infrastructure.Theme.ThemeSettingService>();

        // Study Essentials Hub Service
        services.AddScoped<Dreamflykit.Application.StudyHub.Interfaces.IStudyHubService, Dreamflykit.Infrastructure.StudyHub.StudyHubService>();

        // Fashion Category Hub Service
        services.AddScoped<Dreamflykit.Application.FashionHub.Interfaces.IFashionHubService, Dreamflykit.Infrastructure.FashionHub.FashionHubService>();

        // Central Enterprise Audit Logging
        services.AddScoped<Dreamflykit.Application.Audit.Interfaces.IAuditLogService, Dreamflykit.Infrastructure.Audit.AuditLogService>();
    }
}