using Dreamflykit.Infrastructure.DependencyInjection;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddSingleton(TimeProvider.System);

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
// Authentication cookie
// ------------------------------------------------------------

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Dreamflykit.Auth";

    options.LoginPath = "/Account/Login";

    options.AccessDeniedPath = "/Account/AccessDenied";

    options.ExpireTimeSpan =
        TimeSpan.FromHours(8);

    options.SlidingExpiration = true;

    options.Cookie.HttpOnly = true;

    options.Cookie.SecurePolicy =
        CookieSecurePolicy.Always;

    options.Cookie.SameSite =
        SameSiteMode.Lax;
});

// ------------------------------------------------------------
// MVC
// ------------------------------------------------------------

builder.Services.AddControllersWithViews();

var app = builder.Build();

// ------------------------------------------------------------
// HTTP request pipeline
// ------------------------------------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

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