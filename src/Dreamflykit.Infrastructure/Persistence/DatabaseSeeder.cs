using Dreamflykit.Domain.Catalog;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedCatalogIfEmptyAsync(
        DreamflykitDbContext context,
        CancellationToken cancellationToken = default)
    {
        // 1. Ensure Categories
        var categories = await context.Categories.ToListAsync(cancellationToken);
        if (categories.Count == 0)
        {
            var catList = new List<Category>
            {
                new("Study Essentials", "study-essentials", 1),
                new("Electronics", "electronics", 2),
                new("Travel Gear", "travel-gear", 3),
                new("Adapters & SIM", "adapters-sim", 4),
                new("Lifestyle", "lifestyle", 5),
                new("Health & Care", "health-care", 6),
                new("Luggage & Bags", "luggage-bags", 7),
                new("Fashion", "fashion", 8)
            };

            context.Categories.AddRange(catList);
            await context.SaveChangesAsync(cancellationToken);
            categories = catList;
        }

        var catMap = categories.ToDictionary(c => c.Slug, c => c.Id);

        // 2. Ensure Products
        if (await context.Products.AnyAsync(cancellationToken))
        {
            return;
        }

        var seedItems = new[]
        {
            new
            {
                Cat = "fashion",
                Name = "Men's Hoodie",
                Slug = "mens-hoodie",
                Short = "Comfortable cotton blend hoodie for autumn and winter travel.",
                Desc = "Premium heavyweight fleece hoodie with front kangaroo pocket, ribbed cuffs, and soft brushed interior. Perfect for cool campus days and airport travel.",
                Order = 1,
                VarName = "Navy Blue - Medium",
                Sku = "HD-MEN-NV-M",
                Mrp = 1999m,
                Price = 999m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Men's Hoodie"
            },
            new
            {
                Cat = "fashion",
                Name = "Men's Bomber Jacket",
                Slug = "mens-bomber-jacket",
                Short = "Water-resistant flight bomber jacket with thermal lining.",
                Desc = "Stylish and functional bomber jacket built with windproof shell fabric and dual utility pockets. Essential outerwear for international journeys.",
                Order = 2,
                VarName = "Khaki Tan - Large",
                Sku = "BJ-MEN-KH-L",
                Mrp = 2499m,
                Price = 1499m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Men's Bomber Jacket"
            },
            new
            {
                Cat = "fashion",
                Name = "Men's Casual Shirt",
                Slug = "mens-casual-shirt",
                Short = "Breathable 100% cotton Oxford button-down casual shirt.",
                Desc = "A versatile button-down Oxford shirt designed for both university lectures and casual weekend strolls. Wrinkle-resistant and ultra-comfortable.",
                Order = 3,
                VarName = "Sky Blue - 40",
                Sku = "CS-MEN-SB-40",
                Mrp = 1249m,
                Price = 749m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Men's Casual Shirt"
            },
            new
            {
                Cat = "fashion",
                Name = "Men's Joggers",
                Slug = "mens-joggers",
                Short = "Stretch athletic jogger pants with zippered pockets.",
                Desc = "Slim-tapered fit jogger pants crafted from moisture-wicking French terry cotton with secure zippered side pockets for your phone and passport.",
                Order = 4,
                VarName = "Jet Black - Medium",
                Sku = "JG-MEN-BK-M",
                Mrp = 1299m,
                Price = 799m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Men's Joggers"
            },
            new
            {
                Cat = "fashion",
                Name = "Women's Sweatshirt",
                Slug = "womens-sweatshirt",
                Short = "Pastel pink drop-shoulder casual crewneck sweatshirt.",
                Desc = "Cozy oversized sweatshirt made from premium brushed cotton fleece. Features subtle drop-shoulder styling and relaxed fit.",
                Order = 5,
                VarName = "Pastel Pink - Small",
                Sku = "SW-WOM-PK-S",
                Mrp = 1499m,
                Price = 899m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Women's Sweatshirt"
            },
            new
            {
                Cat = "fashion",
                Name = "Women's Leggings",
                Slug = "womens-leggings",
                Short = "High-waist four-way stretch opaque everyday leggings.",
                Desc = "Buttery soft high-rise activewear leggings designed for maximum freedom of movement during long flights and daily campus commutes.",
                Order = 6,
                VarName = "Matte Black - Medium",
                Sku = "LG-WOM-BK-M",
                Mrp = 999m,
                Price = 599m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Women's Leggings"
            },
            new
            {
                Cat = "fashion",
                Name = "Women's Casual Dress",
                Slug = "womens-casual-dress",
                Short = "Printed midi A-line flowy dress with comfortable waist tie.",
                Desc = "Effortless casual elegance made from lightweight, breathable fabric. Ideal for orientation events, campus life, and social outings.",
                Order = 7,
                VarName = "Floral Print - Medium",
                Sku = "DR-WOM-FL-M",
                Mrp = 1799m,
                Price = 1099m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Women's Casual Dress"
            },
            new
            {
                Cat = "fashion",
                Name = "Women's Denim Jacket",
                Slug = "womens-denim-jacket",
                Short = "Classic vintage wash denim jacket with silver hardware.",
                Desc = "Timeless medium-wash denim jacket cut in a comfortable tailored silhouette. Pairs effortlessly with hoodies, dresses, and tees.",
                Order = 8,
                VarName = "Vintage Blue - Small",
                Sku = "DJ-WOM-VB-S",
                Mrp = 2199m,
                Price = 1299m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Women's Denim Jacket"
            },
            new
            {
                Cat = "adapters-sim",
                Name = "All-in-One Universal Travel Adapter",
                Slug = "universal-travel-adapter",
                Short = "Worldwide power adapter covering USA, UK, EU, AU plugs with 4x USB & Type-C.",
                Desc = "Must-have travel companion compatible in over 150+ countries. Features smart high-speed USB-C PD charging and built-in surge and fuse protection.",
                Order = 9,
                VarName = "Pearl White - Universal",
                Sku = "AD-UNIV-WH",
                Mrp = 999m,
                Price = 499m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Universal Travel Adapter"
            },
            new
            {
                Cat = "adapters-sim",
                Name = "International Student SIM Card - USA/UK",
                Slug = "international-student-sim",
                Short = "Pre-activated high-speed 5G data & local calling SIM card with free incoming.",
                Desc = "Stay connected the minute your flight lands. Includes 50GB high-speed 5G data, unlimited local calls, and international calling minutes back home.",
                Order = 10,
                VarName = "Multi-Fit SIM - 30 Days",
                Sku = "SIM-STUDENT-30D",
                Mrp = 1299m,
                Price = 899m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "International Student SIM"
            },
            new
            {
                Cat = "luggage-bags",
                Name = "Anti-Theft Laptop Backpack (35L)",
                Slug = "anti-theft-laptop-backpack-35l",
                Short = "Waterproof TSA-friendly carry-on backpack with dedicated 16-inch laptop compartment.",
                Desc = "Engineered for student flyers with hidden anti-theft zipper compartments, luggage strap, external USB pass-through port, and ergonomic padded straps.",
                Order = 11,
                VarName = "Charcoal Grey - 35L",
                Sku = "BP-ANTITHEFT-35L",
                Mrp = 2499m,
                Price = 1299m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Anti-Theft Laptop Backpack"
            },
            new
            {
                Cat = "electronics",
                Name = "Noise-Cancelling Over-Ear Headphones",
                Slug = "noise-cancelling-headphones",
                Short = "Active Noise Cancelling Bluetooth 5.3 headphones with 40h battery.",
                Desc = "Block out airplane cabin roar and bustling library sounds with 40dB hybrid ANC. Deep bass drivers and ultra-soft memory foam ear cushions.",
                Order = 12,
                VarName = "Matte Midnight Black",
                Sku = "HP-ANC-BT-BK",
                Mrp = 4999m,
                Price = 2499m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Noise-Cancelling Headphones"
            },
            new
            {
                Cat = "health-care",
                Name = "Compact First Aid & Health Travel Kit",
                Slug = "compact-first-aid-health-kit",
                Short = "Customs-compliant 60-piece medical emergency essentials kit for abroad students.",
                Desc = "Pre-packed travel healthcare essentials including bandages, antiseptics, digital thermometer, blister relief pads, and essential health checklist.",
                Order = 13,
                VarName = "Red Waterproof Case",
                Sku = "MED-FIRSTAID-60PC",
                Mrp = 799m,
                Price = 449m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "First Aid Travel Kit"
            },
            new
            {
                Cat = "lifestyle",
                Name = "Compact Steam Travel Iron (Dual Voltage)",
                Slug = "dual-voltage-travel-steam-iron",
                Short = "Foldable dual voltage 110V/220V steam iron for crisp interview and campus wear.",
                Desc = "Ultra-compact folding handle design that fits neatly into carry-on luggage. Rapid 30-second heat up with non-stick soleplate and continuous steam.",
                Order = 14,
                VarName = "Navy / White - Foldable",
                Sku = "IR-TRAVEL-STEAM-DV",
                Mrp = 1199m,
                Price = 699m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Dual Voltage Travel Steam Iron"
            },
            new
            {
                Cat = "study-essentials",
                Name = "Student Study Abroad Planner & Organizer",
                Slug = "student-study-abroad-planner",
                Short = "Comprehensive academic semester planner, visa tracker & budget organizer.",
                Desc = "Thick 120gsm ink-proof paper with dedicated sections for visa deadlines, university course schedules, emergency contacts, and monthly budget logs.",
                Order = 15,
                VarName = "Hardcover Royal Blue - A5",
                Sku = "PL-STUDY-ABROAD-A5",
                Mrp = 699m,
                Price = 399m,
                Media = "/images/products/product-placeholder.webp",
                Alt = "Study Abroad Planner"
            }
        };

        foreach (var s in seedItems)
        {
            if (!catMap.TryGetValue(s.Cat, out var categoryId))
            {
                continue;
            }

            var product = new Product(
                categoryId,
                s.Name,
                s.Slug,
                s.Short,
                s.Desc,
                s.Order);

            context.Products.Add(product);
            await context.SaveChangesAsync(cancellationToken);

            var variant = new ProductVariant(
                product.Id,
                s.VarName,
                s.Sku,
                s.Mrp,
                s.Price,
                true);

            var media = new ProductMedia(
                product.Id,
                s.Media,
                s.Alt,
                0,
                true);

            context.ProductVariants.Add(variant);
            context.ProductMedia.Add(media);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public static async Task SeedRolesAndSuperAdminAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        // 1. Roles
        string[] roles = ["SuperAdmin", "Admin", "Customer"];
        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }
        }

        // 2. SuperAdmin User
        const string superAdminEmail = "superadmin@dreamflykit.com";
        var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);
        if (superAdmin is null)
        {
            superAdmin = new ApplicationUser
            {
                UserName = superAdminEmail,
                Email = superAdminEmail,
                FirstName = "Super",
                LastName = "Admin",
                PhoneNumber = "+919999999999",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };

            var createRes = await userManager.CreateAsync(superAdmin, "SuperAdmin@Dreamflykit2026!");
            if (createRes.Succeeded)
            {
                await userManager.AddToRolesAsync(superAdmin, ["SuperAdmin", "Admin", "Customer"]);
            }
        }
        else
        {
            // Ensure roles
            foreach (var r in roles)
            {
                if (!await userManager.IsInRoleAsync(superAdmin, r))
                {
                    await userManager.AddToRoleAsync(superAdmin, r);
                }
            }
        }

        // 3. Operations Admin User
        const string adminEmail = "admin@dreamflykit.com";
        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Store",
                LastName = "Manager",
                PhoneNumber = "+918888888888",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };

            var createRes = await userManager.CreateAsync(admin, "Admin@Dreamflykit2026!");
            if (createRes.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }

    public static async Task SeedMediaAndSectionsIfEmptyAsync(
        DreamflykitDbContext context,
        UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken = default)
    {
        // 1. Ensure Homepage Sections
        if (!await context.HomepageSections.AnyAsync(cancellationToken))
        {
            var adminUser = await userManager.FindByEmailAsync("superadmin@dreamflykit.com");
            var adminId = adminUser?.Id ?? Guid.NewGuid();

            var sections = new List<HomepageSection>
            {
                new(HomepageSectionKey.HeroBanner, "Hero Banner", 1, adminId),
                new(HomepageSectionKey.FeaturedCategories, "Featured Categories", 2, adminId),
                new(HomepageSectionKey.PromotionalBanner, "Special Promotions", 3, adminId),
                new(HomepageSectionKey.ProductCollectionBanner, "Top Trending Kits", 4, adminId),
                new(HomepageSectionKey.BrandSection, "Trusted Brands", 5, adminId),
                new(HomepageSectionKey.OfferSection, "Seasonal Offers", 6, adminId),
                new(HomepageSectionKey.FooterBanner, "Footer Banner", 7, adminId)
            };

            foreach (var s in sections)
            {
                s.Activate();
                s.Publish(adminId);
            }

            context.HomepageSections.AddRange(sections);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
