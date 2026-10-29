using Dreamflykit.Infrastructure.DependencyInjection;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

// ------------------------------------------------------------
// Infrastructure
// ------------------------------------------------------------

builder.Services.AddInfrastructure(
    builder.Configuration);

// ------------------------------------------------------------
// ASP.NET Core Identity
// ------------------------------------------------------------

builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        // User settings
        options.User.RequireUniqueEmail = true;

        // Password policy
        options.Password.RequiredLength = 12;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;

        // Lockout policy
        options.Lockout.MaxFailedAccessAttempts = 5;

        options.Lockout.DefaultLockoutTimeSpan =
            TimeSpan.FromMinutes(15);

        options.Lockout.AllowedForNewUsers = true;

        // Sign-in policy
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<DreamflykitDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// ------------------------------------------------------------
// Authentication cookie & schemes
// ------------------------------------------------------------

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
})
.AddIdentityCookies();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Dreamflykit.Auth";

    options.LoginPath = "/Account/Login";

    options.AccessDeniedPath = "/Account/AccessDenied";

    options.ExpireTimeSpan =
        TimeSpan.FromHours(8);

    options.SlidingExpiration = true;

    options.Cookie.HttpOnly = true;

    options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
        ? CookieSecurePolicy.SameAsRequest
        : CookieSecurePolicy.Always;

    options.Cookie.SameSite =
        SameSiteMode.Lax;
});

// ------------------------------------------------------------
// MVC
// ------------------------------------------------------------

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute());
});

builder.Services.AddHealthChecks()
    .AddCheck<Dreamflykit.Web.Health.DatabaseHealthCheck>("database", tags: new[] { "ready", "db" });

var app = builder.Build();

// ------------------------------------------------------------
// Database Seeding
// ------------------------------------------------------------

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DreamflykitDbContext>();
    await dbContext.Database.MigrateAsync();
    await DatabaseSeeder.SeedCatalogIfEmptyAsync(dbContext);

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    await DatabaseSeeder.SeedRolesAndSuperAdminAsync(userManager, roleManager);
    await DatabaseSeeder.SeedMediaAndSectionsIfEmptyAsync(dbContext, userManager);
}

// ------------------------------------------------------------
// HTTP request pipeline & Security Headers
// ------------------------------------------------------------

app.UseForwardedHeaders();

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN");
    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    await next();
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Sanitize malformed consecutive slashes and duplicated study-essentials route paths
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;
    if (!string.IsNullOrEmpty(path))
    {
        if (path.Contains("//") || path.Contains("/category/study-essentials/category/study-essentials/"))
        {
            var cleanPath = path;
            while (cleanPath.Contains("//"))
            {
                cleanPath = cleanPath.Replace("//", "/");
            }
            if (cleanPath.Contains("/category/study-essentials/category/study-essentials/"))
            {
                cleanPath = cleanPath.Replace("/category/study-essentials/category/study-essentials/", "/category/study-essentials/");
            }
            context.Response.Redirect(cleanPath + context.Request.QueryString, permanent: true);
            return;
        }
    }
    await next();
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

// Observability & Orchestrator Probes
app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => false
});

app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.MapHealthChecks("/health");

// ------------------------------------------------------------
// Area routes
// ------------------------------------------------------------

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// ------------------------------------------------------------
// Default route
// ------------------------------------------------------------

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();