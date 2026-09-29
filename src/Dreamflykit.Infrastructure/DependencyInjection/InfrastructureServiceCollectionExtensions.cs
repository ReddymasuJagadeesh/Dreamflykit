using Dreamflykit.Application.Geography;
using Dreamflykit.Application.Kits.Interfaces;
using Dreamflykit.Application.Kits.Services;
using Dreamflykit.Infrastructure.Geography;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Infrastructure.Kits;
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

        RegisterApplicationServices(services);

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
     IServiceCollection services)
    {
        services.AddScoped<
            IFindMyKitRepository,
            FindMyKitRepository>();

        services.AddScoped<
            IFindMyKitService,
            FindMyKitService>();

        services.AddScoped<
            ICountryRepository,
            CountryRepository>();

        services.AddScoped<
            ICountryService,
            CountryService>();

        services.AddScoped<
            IKitRepository,
            KitRepository>();

        services.AddScoped<
            IKitService,
            KitService>();
    }
}