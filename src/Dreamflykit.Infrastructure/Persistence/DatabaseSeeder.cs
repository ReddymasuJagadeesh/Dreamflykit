using System.Text.Json;
using Dreamflykit.Application.StudyHub.Models;
using Dreamflykit.Domain.Catalog;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static readonly (string Name, string Slug, int Order)[] FinalCategories =
    [
        ("Study Essentials", "study-essentials", 1),
        ("Fashion", "fashion", 2),
        ("Travel Gear", "travel-gear", 3),
        ("Mobiles", "mobiles", 4),
        ("Electronics", "electronics", 5),
        ("Appliances", "appliances", 6),
        ("Beauty", "beauty", 7),
        ("Health Care", "health-care", 8),
        ("Food", "food", 9),
        ("Room & Living", "room-living", 10),
        ("Kitchen Appliances", "kitchen-appliances", 11),
        ("Story Books", "story-books", 12),
        ("Premium Hub", "premium-hub", 13),
        ("Gaming Hub", "gaming-hub", 14),
        ("Sports Hub", "sports-hub", 15)
    ];

    public static async Task SeedCatalogIfEmptyAsync(
        DreamflykitDbContext context,
        CancellationToken cancellationToken = default)
    {
        // 1. Ensure and Sync Final 15 Categories
        var existingCategories = await context.Categories.ToListAsync(cancellationToken);
        var finalSlugs = FinalCategories.Select(f => f.Slug).ToHashSet(StringComparer.OrdinalIgnoreCase);

        // A. Upsert the 15 final categories
        foreach (var (name, slug, order) in FinalCategories)
        {
            var existing = existingCategories.FirstOrDefault(c => string.Equals(c.Slug, slug, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                existing.Update(name, slug, order);
                existing.Activate();
            }
            else
            {
                var newCat = new Category(name, slug, order);
                context.Categories.Add(newCat);
            }
        }
        await context.SaveChangesAsync(cancellationToken);

        // Reload to have all current IDs
        var currentCategories = await context.Categories.ToListAsync(cancellationToken);
        var catMap = currentCategories.ToDictionary(c => c.Slug, c => c.Id, StringComparer.OrdinalIgnoreCase);

        // Ensure the 8 Study Essentials Subcategories
        if (catMap.TryGetValue("study-essentials", out var studyCatId))
        {
            var existingSubs = await context.Subcategories
                .Where(s => s.CategoryId == studyCatId)
                .ToListAsync(cancellationToken);

            var requiredSubs = new (string Name, string Slug, int Order)[]
            {
                ("Stationary & study supplies", "stationery-study-supplies", 1),
                ("Tech for study", "tech-for-study", 2),
                ("Document & organization", "document-organization", 3),
                ("Desk & Room study essential", "desk-room-study-essential", 4),
                ("College & Campus Essentials", "college-campus-essentials", 5),
                ("Online Classes & Productivity", "online-classes-productivity", 6),
                ("Exam preparation ( Gre ,tofel ,)", "exam-preparation", 7),
                ("Books & references", "books-references", 8)
            };

            foreach (var (subName, subSlug, subOrder) in requiredSubs)
            {
                var existingSub = existingSubs.FirstOrDefault(s => string.Equals(s.Slug, subSlug, StringComparison.OrdinalIgnoreCase));
                if (existingSub == null)
                {
                    var newSub = new Subcategory(studyCatId, subName, subSlug, subOrder);
                    context.Subcategories.Add(newSub);
                }
                else
                {
                    existingSub.Update(subName, subSlug, subOrder);
                    existingSub.Activate();
                }
            }
            await context.SaveChangesAsync(cancellationToken);

            // Ensure active inventory items for each Study Essentials subcategory
            var subList = await context.Subcategories
                .Where(s => s.CategoryId == studyCatId)
                .ToListAsync(cancellationToken);

            var studyProducts = new (string SubSlug, string Name, string Slug, string Short, string Desc, int Order, string Var, string Sku, decimal Mrp, decimal Price, string Media)[]
            {
                // 1. Stationery & study supplies
                ("stationery-study-supplies", "Hardcover B5 Spiral Notebooks (Pack of 3)", "hardcover-b5-spiral-notebooks-pack-of-3",
                 "Thick 100gsm grid and lined notebooks engineered for university lecture notes.",
                 "Water-resistant hardcover with double-wire binding, lay-flat 180 degree design, and micro-perforated tear-out pages. Contains 160 pages per book.",
                 1, "B5 / Pack of 3", "ST-NB-B5-3PK", 799m, 499m, "/images/study-hub/item-notebook.png"),
                ("stationery-study-supplies", "Ergonomic Quick-Dry Gel Pens (Pack of 10)", "ergonomic-quick-dry-gel-pens-10pk",
                 "Smudge-free 0.5mm Japanese ink pens with soft rubber comfort grip.",
                 "Fast drying smooth ink formulation that prevents paper bleed. Ideal for exam halls, fast lecture notes, and study sessions.",
                 2, "Black & Blue Assorted", "ST-PEN-QD-10PK", 399m, 249m, "/images/study-hub/item-pens.png"),
                ("stationery-study-supplies", "Pastel Aesthetic Highlighters & Sticky Notes Set", "pastel-aesthetic-highlighters-sticky-notes-set",
                 "6 soft pastel chisel tip highlighters plus translucent color-code tabs.",
                 "No-bleed water-based pastel pigments that don't obscure text. Includes 200 translucent index tabs for textbook marking.",
                 3, "6 Pastel Colors + Tabs", "ST-HL-PASTEL-SET", 499m, 299m, "/images/study-hub/test-pic-placeholder.svg"),

                // 2. Tech for study
                ("tech-for-study", "Universal 65W GaN Travel Adapter with Dual Type-C", "universal-65w-gan-travel-adapter-dual-type-c",
                 "All-in-one worldwide plug adapter for US, UK, EU, AU with GaN fast charging.",
                 "Powers laptops, phones, and tablets across 150+ countries. Features built-in safety fuse, surge protector, and 65W Power Delivery fast charging.",
                 1, "Matte Black - 65W", "TC-ADP-GAN-65W", 2499m, 1499m, "/images/study-hub/test-pic-placeholder.svg"),
                ("tech-for-study", "Shockproof Water-Resistant Laptop Sleeve 14-16 Inch", "shockproof-water-resistant-laptop-sleeve-14-16",
                 "360-degree protective foam laptop case with front accessory organizer pocket.",
                 "Ultra-soft flannel interior lining with reinforced corner bumpers. Front zippered compartment holds charger, mouse, and adapter.",
                 2, "Heather Grey - 15.6 Inch", "TC-SLV-156-GRY", 1299m, 799m, "/images/study-hub/item-sleeve.png"),
                ("tech-for-study", "Silent Ergonomic Dual-Mode Wireless Mouse", "silent-ergonomic-dual-mode-wireless-mouse",
                 "Rechargeable Bluetooth 5.2 and 2.4G wireless mouse for library and study rooms.",
                 "90% noise-reduced silent click switches with adjustable DPI up to 2400. Up to 60 days battery life per USB-C charge.",
                 3, "Matte Navy", "TC-MSE-BT-NV", 1499m, 899m, "/images/study-hub/test-pic-placeholder.svg"),
                ("tech-for-study", "7-in-1 Aluminum USB-C Hub with 4K HDMI & 100W PD", "7-in-1-aluminum-usb-c-hub-4k-hdmi",
                 "Expand your laptop with HDMI, 3x USB 3.0, SD card reader, and 100W pass-through charging.",
                 "Aircraft-grade aluminum casing with high heat dissipation. Plug and play compatibility for MacBook, Windows, iPad, and Chromebook.",
                 4, "Space Grey", "TC-HUB-7IN1-AL", 2999m, 1799m, "/images/study-hub/test-pic-placeholder.svg"),

                // 3. Document & organization
                ("document-organization", "RFID-Blocking Multi-Passport & Travel Visa Wallet", "rfid-blocking-multi-passport-travel-visa-wallet",
                 "Zippered security document organizer for passport, visa, tickets & cards.",
                 "High-density nylon fabric with built-in RFID shielding material to protect against unauthorized digital scanning. Features pen loop and detachable wristlet.",
                 1, "Charcoal Black", "DOC-WL-RFID-BLK", 1199m, 699m, "/images/study-hub/item-organizer.png"),
                ("document-organization", "Waterproof 13-Pocket Expanding Accordion File Folder", "waterproof-13-pocket-expanding-file-folder",
                 "Heavy-duty poly document file with color-coded index tabs for university certificates.",
                 "Secure buckle closure with waterproof polypropylene body. Organizes I-20, offer letters, insurance papers, transcripts, and passport copies.",
                 2, "Navy Blue - A4/Letter", "DOC-ACC-13P-NV", 899m, 499m, "/images/study-hub/item-organizer.png"),

                // 4. Desk & Room study essential
                ("desk-room-study-essential", "Dimmable LED Desk Lamp with USB Phone Charger", "dimmable-led-desk-lamp-usb-charger",
                 "Foldable eye-care reading lamp with 5 color modes and touch brightness control.",
                 "Flicker-free natural light with flexible gooseneck arm and integrated 5V/2A USB charging port. Auto-off 45-minute sleep timer.",
                 1, "Pure White", "DK-LMP-LED-WHT", 2199m, 1299m, "/images/study-hub/test-pic-placeholder.svg"),
                ("desk-room-study-essential", "Ergonomic Memory Foam Seat Cushion for Dorm Chairs", "ergonomic-memory-foam-seat-cushion",
                 "Orthopedic coccyx cushion for long study sessions and back relief.",
                 "High-density 100% memory foam with non-slip rubber bottom and breathable washable velour cover. Relieves pressure on hips and spine.",
                 2, "Midnight Black", "DK-CSH-MEM-BLK", 1699m, 999m, "/images/study-hub/test-pic-placeholder.svg"),

                // 5. College & Campus Essentials
                ("college-campus-essentials", "Water-Resistant Campus Laptop Backpack 30L with USB Port", "water-resistant-campus-laptop-backpack-30l",
                 "Durable campus daypack with padded 15.6 inch laptop sleeve and luggage strap.",
                 "Multi-compartment storage with hidden anti-theft back pocket, reinforced top handle, and breathable mesh shoulder padding for campus commuting.",
                 1, "Deep Navy - 30L", "CMP-BP-30L-NV", 3499m, 2199m, "/images/study-hub/test-pic-placeholder.svg"),
                ("college-campus-essentials", "Vacuum Insulated Stainless Steel Bottle 1000ml", "vacuum-insulated-stainless-steel-bottle-1000ml",
                 "24-hour cold and 12-hour hot double-wall flask with leakproof lid.",
                 "Food-grade 18/8 stainless steel with sweat-proof powder coat finish. Wide mouth accommodates ice cubes and easy cleaning.",
                 2, "Cobalt Blue - 1000ml", "CMP-BOT-SS-1L", 1099m, 649m, "/images/study-hub/test-pic-placeholder.svg"),

                // 6. Online Classes & Productivity
                ("online-classes-productivity", "Full HD 1080p Webcam with Noise-Cancelling Microphone", "full-hd-1080p-webcam-noise-cancelling-mic",
                 "Wide-angle auto-focus webcam with physical privacy shutter for lectures & interviews.",
                 "Sharp 1080p 30fps streaming with low-light auto correction and dual noise-cancelling mics. Universal tripod mount and monitor clamp.",
                 1, "Matte Black", "PRD-CAM-1080P", 2899m, 1799m, "/images/study-hub/test-pic-placeholder.svg"),
                ("online-classes-productivity", "USB Conference Headset with Noise-Cancelling Boom Mic", "usb-conference-headset-noise-cancelling-mic",
                 "Lightweight on-ear headphones with in-line volume & mute controls.",
                 "Crystal clear audio for Zoom, Teams, and online lectures. Padded leatherette ear cups and adjustable headband for all-day comfort.",
                 2, "Black - USB/3.5mm", "PRD-HDS-USB-BLK", 2299m, 1399m, "/images/study-hub/test-pic-placeholder.svg"),

                // 7. Exam preparation
                ("exam-preparation", "Official GRE General Test Prep Super Power Pack 2026", "official-gre-prep-super-power-pack-2026",
                 "Complete quantitative, verbal reasoning, and analytical writing preparation set.",
                 "Comprehensive authentic practice questions with complete explanations, sample essay responses, and online scoring practice access.",
                 1, "Paperback Edition", "EXM-GRE-PWR-26", 1999m, 1299m, "/images/study-hub/test-pic-placeholder.svg"),
                ("exam-preparation", "TOEFL iBT Official Guide with Interactive Online Audio Practice", "toefl-ibt-official-guide-online-audio",
                 "Full-length authentic practice tests covering Reading, Listening, Speaking & Writing.",
                 "Real TOEFL practice questions with scoring rubrics and downloadable audio tracks. Crucial for university English proficiency requirements.",
                 2, "Latest Edition", "EXM-TOEFL-IBT-GD", 1799m, 1199m, "/images/study-hub/test-pic-placeholder.svg"),

                // 8. Books & references
                ("books-references", "International Student's Guide to Thriving in Global Universities", "international-students-guide-to-thriving-in-global-universities",
                 "Essential handbook on academic writing, cultural adaptation, and university life.",
                 "Step-by-step guidance on thesis research, plagiarism rules, university networking, budgeting, and career pathways in the US, UK, and Canada.",
                 1, "Paperback", "BK-INT-SURV-GD", 799m, 499m, "/images/study-hub/test-pic-placeholder.svg"),
                ("books-references", "Academic Writing & Research Methodology for Graduate Students", "academic-writing-and-research-methodology",
                 "Clear guidelines on literature reviews, citations (APA, MLA, Chicago), and thesis defense.",
                 "Practical guide for term papers, literature synthesis, and publishing scholarly articles with confidence.",
                 2, "Hardcover Edition", "BK-ACD-WRITE-GD", 1099m, 699m, "/images/study-hub/test-pic-placeholder.svg")
            };

            foreach (var sp in studyProducts)
            {
                var matchingSub = subList.FirstOrDefault(s => string.Equals(s.Slug, sp.SubSlug, StringComparison.OrdinalIgnoreCase));
                if (matchingSub == null) continue;

                var exists = await context.Products.AnyAsync(p => p.Slug == sp.Slug, cancellationToken);
                if (!exists)
                {
                    var product = new Product(studyCatId, sp.Name, sp.Slug, sp.Short, sp.Desc, sp.Order);
                    product.SetSubcategory(matchingSub.Id);
                    context.Products.Add(product);
                    await context.SaveChangesAsync(cancellationToken);

                    var variant = new ProductVariant(product.Id, sp.Var, sp.Sku, sp.Mrp, sp.Price, true);
                    var media = new ProductMedia(product.Id, sp.Media, sp.Name, 0, true);
                    context.ProductVariants.Add(variant);
                    context.ProductMedia.Add(media);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
        }

        // Ensure Beauty Category Subcategories and Products
        if (catMap.TryGetValue("beauty", out var beautyCatId))
        {
            var existingBeautySubs = await context.Subcategories
                .Where(s => s.CategoryId == beautyCatId)
                .ToListAsync(cancellationToken);

            var requiredBeautySubs = new (string Name, string Slug, int Order)[]
            {
                ("Skincare", "skincare", 1),
                ("Hair Care", "hair-care", 2),
                ("Makeup", "makeup", 3),
                ("Fragrances", "fragrances", 4),
                ("Men's Grooming", "mens-grooming", 5),
                ("Bath & Body", "bath-body", 6),
                ("Oral Care", "oral-care", 7),
                ("Beauty Supplements", "beauty-supplements", 8)
            };

            foreach (var (subName, subSlug, subOrder) in requiredBeautySubs)
            {
                var existingSub = existingBeautySubs.FirstOrDefault(s => string.Equals(s.Slug, subSlug, StringComparison.OrdinalIgnoreCase));
                if (existingSub == null)
                {
                    var newSub = new Subcategory(beautyCatId, subName, subSlug, subOrder);
                    context.Subcategories.Add(newSub);
                }
                else
                {
                    existingSub.Update(subName, subSlug, subOrder);
                    existingSub.Activate();
                }
            }
            await context.SaveChangesAsync(cancellationToken);

            var beautySubList = await context.Subcategories
                .Where(s => s.CategoryId == beautyCatId)
                .ToListAsync(cancellationToken);

            var beautySeedProducts = new (string SubSlug, string Name, string Slug, string Short, string Desc, int Order, string Var, string Sku, decimal Mrp, decimal Price, string Media)[]
            {
                ("skincare", "Vitamin C 15% Face Serum with Hyaluronic Acid (30ml)", "vitamin-c-15-face-serum-hyaluronic-acid-30ml",
                 "Brightening face serum with antioxidant Vitamin C, Ferulic Acid & Hyaluronic Acid for all-day radiance.",
                 "Advanced dermatologist-formulated brightening serum that diminishes dark spots, protects against pollution, and deeply hydrates tired skin during study hours.",
                 1, "30ml Dropper Bottle", "BPC-SKIN-VITC-30ML", 699m, 399m, "/images/products/product-placeholder.webp"),

                ("skincare", "Gentle Daily Hydrating Cleanser for Sensitive Skin (250ml)", "gentle-daily-hydrating-cleanser-sensitive-skin",
                 "Soap-free, fragrance-free gentle face and body wash with barrier-protecting ceramides.",
                 "Hypoallergenic cleansing wash clinically proven to maintain natural skin barrier moisture. Non-comedogenic and ideal for changing climates during travel.",
                 2, "250ml Pump Bottle", "BPC-SKIN-CLN-250ML", 549m, 389m, "/images/products/product-placeholder.webp"),

                ("skincare", "Ultra-Light Matte Sunscreen Gel SPF 50 PA++++ (50g)", "ultra-light-matte-sunscreen-gel-spf50",
                 "Broad spectrum UVA/UVB protection with zero white cast and sweat-resistant matte finish.",
                 "Non-greasy sun gel enriched with niacinamide and green tea. Absorbs instantly and doubles as a protective makeup primer for outdoor student life.",
                 3, "50g Matte Tube", "BPC-SKIN-SUN-50G", 499m, 299m, "/images/products/product-placeholder.webp"),

                ("hair-care", "Moroccan Argan Hair Smoothing Oil & Serum (100ml)", "moroccan-argan-hair-smoothing-oil-serum",
                 "Anti-frizz hair serum with pure cold-pressed argan oil and heat protectant up to 230°C.",
                 "Instantly tames flyaways, seals split ends, and delivers salon-gloss shine without weighing hair down. Perfect for humid and cold weather transitions.",
                 4, "100ml Glass Pump", "BPC-HAIR-ARG-100ML", 799m, 449m, "/images/products/product-placeholder.webp"),

                ("hair-care", "Keratin Smooth Sulfate-Free Scalp & Hair Shampoo (400ml)", "keratin-smooth-sulfate-free-shampoo-400ml",
                 "Color-safe, paraben-free strengthening shampoo with hydrolysed keratin and biotin.",
                 "Deeply cleanses roots without stripping natural oils. Strengthens brittle hair strands and restores manageability for busy lecture mornings.",
                 5, "400ml Bottle", "BPC-HAIR-KRT-400ML", 650m, 399m, "/images/products/product-placeholder.webp"),

                ("fragrances", "Bella Vita Luxury Unisex Eau De Parfum Gift Set (4 x 20ml)", "bella-vita-luxury-unisex-edp-set-4x20ml",
                 "Pocket luxury perfume collection featuring White Oud, Skai Aquatic, Fresh & CEO Man.",
                 "Long-lasting 8+ hour fragrance concentration in compact, travel-friendly atomizer vials certified for carry-on flight luggage.",
                 6, "4 x 20ml Travel Gift Box", "BPC-FRAG-BV-4PK", 999m, 549m, "/images/products/product-placeholder.webp"),

                ("mens-grooming", "Activated Charcoal Beard Wash & Growth Oil Grooming Kit", "activated-charcoal-beard-wash-growth-oil-kit",
                 "2-in-1 men's beard care kit with cedarwood beard wash and nutrient-rich growth oil.",
                 "Prevents beard itch and dandruff while promoting fuller, softer facial hair. Formulated with red clover extract, almond oil, and tea tree.",
                 7, "Wash 100ml + Oil 30ml", "BPC-MENS-BRD-KIT", 899m, 499m, "/images/products/product-placeholder.webp"),

                ("makeup", "Swiss Beauty Non-Transfer Matte Liquid Lipstick Set (Pack of 3)", "swiss-beauty-non-transfer-matte-liquid-lipstick-set",
                 "12-hour smudge-proof transfer-resistant waterproof liquid lipstick in flattering nude shades.",
                 "Enriched with Vitamin E and lightweight hydrating polymers that lock in color without drying lips. Features precision doe-foot applicator.",
                 8, "Nude Festive Trio", "BPC-MKP-LIP-3PK", 599m, 349m, "/images/products/product-placeholder.webp"),

                ("bath-body", "Exfoliating Arabica Coffee Body Scrub with Coconut Butter (100g)", "exfoliating-arabica-coffee-body-scrub",
                 "Pure roasted coffee granules and virgin coconut butter for glowing, smooth skin.",
                 "Gently polishes away dead cells, unclogs pores, and visibly reduces ingrown hair and strawberry legs for an all-over festive glow.",
                 9, "100g Tub", "BPC-BATH-COF-100G", 449m, 279m, "/images/products/product-placeholder.webp"),

                ("skincare", "Complete Travel Toiletry & Skincare Flight Kit (7 Pcs)", "complete-travel-toiletry-skincare-flight-kit",
                 "TSA-compliant silicone leakproof bottles, toothbrush case, micro towel & waterproof pouch.",
                 "Airport security approved travel kit with 3x 100ml squeeze bottles, 2x cream jars, and clear toiletry travel case ready for study abroad flights.",
                 10, "7-Piece Clear Pouch Set", "BPC-TRV-KIT-7PC", 899m, 499m, "/images/products/product-placeholder.webp")
            };

            foreach (var bp in beautySeedProducts)
            {
                var matchingSub = beautySubList.FirstOrDefault(s => string.Equals(s.Slug, bp.SubSlug, StringComparison.OrdinalIgnoreCase));
                var exists = await context.Products.AnyAsync(p => p.Slug == bp.Slug, cancellationToken);
                if (!exists)
                {
                    var product = new Product(beautyCatId, bp.Name, bp.Slug, bp.Short, bp.Desc, bp.Order);
                    if (matchingSub != null)
                    {
                        product.SetSubcategory(matchingSub.Id);
                    }
                    context.Products.Add(product);
                    await context.SaveChangesAsync(cancellationToken);

                    var variant = new ProductVariant(product.Id, bp.Var, bp.Sku, bp.Mrp, bp.Price, true);
                    var media = new ProductMedia(product.Id, bp.Media, bp.Name, 0, true);
                    context.ProductVariants.Add(variant);
                    context.ProductMedia.Add(media);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
        }

        // B. Reassign products from obsolete categories & remove obsolete categories
        var obsoleteCategories = currentCategories.Where(c => !finalSlugs.Contains(c.Slug)).ToList();
        if (obsoleteCategories.Count > 0)
        {
            var defaultFallbackId = catMap["study-essentials"];
            foreach (var obs in obsoleteCategories)
            {
                // Determine best target category for existing products
                Guid targetCategoryId = obs.Slug switch
                {
                    "adapters-sim" or "adapters" or "sim" => catMap.GetValueOrDefault("electronics", defaultFallbackId),
                    "luggage-bags" or "luggage" or "bags" => catMap.GetValueOrDefault("travel-gear", defaultFallbackId),
                    "lifestyle" => catMap.GetValueOrDefault("room-living", defaultFallbackId),
                    "stationery" => catMap.GetValueOrDefault("study-essentials", defaultFallbackId),
                    _ => defaultFallbackId
                };

                var productsToReassign = await context.Products.Where(p => p.CategoryId == obs.Id).ToListAsync(cancellationToken);
                foreach (var prod in productsToReassign)
                {
                    prod.Update(targetCategoryId, prod.Name, prod.Slug, prod.ShortDescription, prod.Description, prod.DisplayOrder);
                    prod.RemoveSubcategory();
                }

                var subsToRemove = await context.Subcategories.Where(s => s.CategoryId == obs.Id).ToListAsync(cancellationToken);
                if (subsToRemove.Count > 0)
                {
                    context.Subcategories.RemoveRange(subsToRemove);
                }

                context.Categories.Remove(obs);
            }
            await context.SaveChangesAsync(cancellationToken);
        }

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
                Cat = "electronics",
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
                Cat = "mobiles",
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
                Cat = "travel-gear",
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
                Cat = "appliances",
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
        string[] roles = ["SuperAdmin", "Admin", "Customer", "Seller"];
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
        else
        {
            var adminUser = await userManager.FindByEmailAsync("superadmin@dreamflykit.com");
            var adminId = adminUser?.Id ?? Guid.NewGuid();

            // Purge old 2-slot ads completely from database
            var oldAds = await context.HomepageSections
                .Where(s => s.SectionKey == HomepageSectionKey.HeaderAdSlot1 || s.SectionKey == HomepageSectionKey.HeaderAdSlot2)
                .ToListAsync(cancellationToken);
            if (oldAds.Count > 0)
            {
                context.HomepageSections.RemoveRange(oldAds);
            }

            if (!await context.HomepageSections.AnyAsync(s => s.SectionKey == HomepageSectionKey.HeaderAdSpace, cancellationToken))
            {
                var adSpace = new HomepageSection(HomepageSectionKey.HeaderAdSpace, "Dreamflykit Special Offers", 8, adminId);
                adSpace.Update("Dreamflykit Special Offers", "{\"sub\":\"Exclusive student travel deals & partner perks\",\"badge\":\"SPECIAL OFFER\",\"mode\":\"Hybrid\",\"blank\":false}", "/SpecialOffer", 8, null, null, null, adminId);
                adSpace.Activate();
                adSpace.Publish(adminId);
                context.HomepageSections.Add(adSpace);
            }

            if (!await context.HomepageSections.AnyAsync(s => s.SectionKey == HomepageSectionKey.StudyEssentialsHub, cancellationToken))
            {
                var defaultHub = StudyHubDto.CreateDefault();
                var hubSection = new HomepageSection(HomepageSectionKey.StudyEssentialsHub, defaultHub.Section1.Title, 13, adminId);
                var safeSubtitle = defaultHub.Section1.Subtitle != null && defaultHub.Section1.Subtitle.Length > 290
                    ? defaultHub.Section1.Subtitle[..290]
                    : defaultHub.Section1.Subtitle;
                hubSection.Update(defaultHub.Section1.Title, safeSubtitle, "/category/study-essentials", 13, null, null, null, adminId);
                hubSection.Activate();
                hubSection.Publish(adminId);
                context.HomepageSections.Add(hubSection);
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
