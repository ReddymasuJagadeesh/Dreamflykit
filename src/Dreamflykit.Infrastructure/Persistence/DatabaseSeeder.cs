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

        // Ensure the 6 Study Essentials Subcategories
        if (catMap.TryGetValue("study-essentials", out var studyCatId))
        {
            var existingSubs = await context.Subcategories
                .Where(s => s.CategoryId == studyCatId)
                .ToListAsync(cancellationToken);

            var requiredSubs = new (string Name, string Slug, int Order)[]
            {
                ("Stationery & Study Supplies", "stationery-study-supplies", 1),
                ("Tech for study", "tech-for-study", 2),
                ("Documents & Organization", "document-organization", 3),
                ("Desk & Room Study Essentials", "desk-room-study-essential", 4),
                ("College & Campus Essentials", "college-campus-essentials", 5),
                ("Online Classes & Productivity", "online-classes-productivity", 6)
            };

            var validSlugs = new HashSet<string>(requiredSubs.Select(r => r.Slug), StringComparer.OrdinalIgnoreCase);
            foreach (var existing in existingSubs.Where(s => !validSlugs.Contains(s.Slug)))
            {
                existing.Deactivate();
            }

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
                // 1. Stationery & Study Supplies
                ("stationery-study-supplies", "Hardcover B5 Spiral Notebooks (Pack of 3)", "hardcover-b5-spiral-notebooks-pack-of-3",
                 "Thick 100gsm grid and lined notebooks engineered for university lecture notes.",
                 "Notebooks and writing pads. Water-resistant hardcover with double-wire binding, lay-flat 180 degree design, and micro-perforated tear-out pages. Contains 160 pages per book.",
                 1, "B5 / Pack of 3", "ST-NB-B5-3PK", 799m, 499m, "/images/study-hub/item-notebook.png"),
                ("stationery-study-supplies", "Ergonomic Quick-Dry Gel Pens (Pack of 10)", "ergonomic-quick-dry-gel-pens-10pk",
                 "Smudge-free 0.5mm Japanese ink pens with soft rubber comfort grip.",
                 "Pens, pencils and erasers. Fast drying smooth ink formulation that prevents paper bleed. Ideal for exam halls, fast lecture notes, and study sessions.",
                 2, "Black & Blue Assorted", "ST-PEN-QD-10PK", 399m, 249m, "/images/study-hub/item-pens.png"),
                ("stationery-study-supplies", "Pastel Aesthetic Highlighters & Sticky Notes Set", "pastel-aesthetic-highlighters-sticky-notes-set",
                 "6 soft pastel chisel tip highlighters plus translucent color-code tabs.",
                 "Highlighters and permanent markers. Sticky notes and page flags. No-bleed water-based pastel pigments that don't obscure text.",
                 3, "6 Pastel Colors + Tabs", "ST-HL-PASTEL-SET", 499m, 299m, "/images/study-hub/test-pic-placeholder.svg"),
                ("stationery-study-supplies", "A4 Expanding Document Organizer File Folder", "a4-expanding-document-organizer-file-folder",
                 "12-pocket accordion document folder with customizable index tabs.",
                 "File folders and document organizers. Heavy-duty waterproof poly folder with secure elastic closure for class notes and syllabi.",
                 4, "Black A4", "ST-FLD-EXP-12P", 599m, 349m, "/images/study-hub/item-organizer.png"),
                ("stationery-study-supplies", "Large Capacity Canvas Pencil Pouch", "large-capacity-canvas-pencil-pouch",
                 "Multi-slot aesthetic stationery organizer pouch with durable zippers.",
                 "Pencil pouch. Holds up to 50 pens, pencils, highlighters, erasers and sticky notes in convenient mesh compartments.",
                 5, "Pastel Sage", "ST-PCH-CNV-01", 449m, 279m, "/images/study-hub/test-pic-placeholder.svg"),
                ("stationery-study-supplies", "Desktop Stapler with Staples & Paper Clips Kit", "desktop-stapler-with-staples-paper-clips-kit",
                 "Compact office-grade stapler with 1000 staples and 100 color paper clips.",
                 "Stapler, staples and paper clips. Jam-free performance with built-in staple remover and paper clip storage.",
                 6, "Standard Black", "ST-STP-KIT-01", 499m, 299m, "/images/study-hub/test-pic-placeholder.svg"),
                ("stationery-study-supplies", "Advanced 417-Function Scientific Calculator", "advanced-417-function-scientific-calculator",
                 "Two-line display scientific calculator approved for university exams.",
                 "Scientific calculator (if required). Features 417 functions including statistics, linear regression, calculus, and matrix operations with hard slide-on case.",
                 7, "Solar & Battery", "ST-CALC-SCI-417", 1499m, 999m, "/images/study-hub/test-pic-placeholder.svg"),

                // 2. Tech for study
                ("tech-for-study", "Universal 65W GaN Travel Adapter with Dual Type-C", "universal-65w-gan-travel-adapter-dual-type-c",
                 "All-in-one worldwide plug adapter for US, UK, EU, AU with GaN fast charging.",
                 "Universal travel adapter compatible with destination. Powers laptops, phones, and tablets across 150+ countries. Features built-in safety fuse and surge protector.",
                 1, "Matte Black - 65W", "TC-ADP-GAN-65W", 2499m, 1499m, "/images/study-hub/test-pic-placeholder.svg"),
                ("tech-for-study", "Shockproof Water-Resistant Laptop Sleeve 14-16 Inch", "shockproof-water-resistant-laptop-sleeve-14-16",
                 "360-degree protective foam laptop case with front accessory organizer pocket.",
                 "Laptop sleeve. Laptop or laptop accessories. Ultra-soft flannel interior lining with reinforced corner bumpers.",
                 2, "Heather Grey - 15.6 Inch", "TC-SLV-156-GRY", 1299m, 799m, "/images/study-hub/item-sleeve.png"),
                ("tech-for-study", "Silent Ergonomic Dual-Mode Wireless Mouse", "silent-ergonomic-dual-mode-wireless-mouse",
                 "Rechargeable Bluetooth 5.2 and 2.4G wireless mouse for library and study rooms.",
                 "Wireless mouse. 90% noise-reduced silent click switches with adjustable DPI up to 2400. Up to 60 days battery life per USB-C charge.",
                 3, "Matte Navy", "TC-MSE-BT-NV", 1499m, 899m, "/images/study-hub/test-pic-placeholder.svg"),
                ("tech-for-study", "High-Speed 128GB Metal USB 3.2 Flash Drive", "high-speed-128gb-metal-usb-flash-drive",
                 "Rugged zinc-alloy flash drive with fast read speeds up to 150MB/s.",
                 "USB flash drive. Waterproof and shockproof design with keyring hole for university presentations and assignments backup.",
                 4, "128GB Silver", "TC-USB-128GB-MT", 999m, 599m, "/images/study-hub/test-pic-placeholder.svg"),
                ("tech-for-study", "7-in-1 Aluminum USB-C Hub with 4K HDMI & 100W PD", "7-in-1-aluminum-usb-c-hub-4k-hdmi",
                 "Expand your laptop with HDMI, 3x USB 3.0, SD card reader, and 100W pass-through charging.",
                 "USB-C hub or adapter. Aircraft-grade aluminum casing with high heat dissipation. Plug and play compatibility for MacBook, Windows, iPad, and Chromebook.",
                 5, "Space Grey", "TC-HUB-7IN1-AL", 2999m, 1799m, "/images/study-hub/test-pic-placeholder.svg"),
                ("tech-for-study", "Active Noise Cancelling Wireless Headphones with Mic", "active-noise-cancelling-wireless-headphones-mic",
                 "Over-ear ANC headphones with 40-hour playtime and deep bass.",
                 "Headphones or earphones. Blocks out dorm and library chatter for ultimate study focus with built-in clear voice mic.",
                 6, "Midnight Black", "TC-HDP-ANC-BLK", 4999m, 2999m, "/images/study-hub/test-pic-placeholder.svg"),
                ("tech-for-study", "Foldable Aluminum Ergonomic Laptop Stand", "foldable-aluminum-ergonomic-laptop-stand",
                 "Adjustable angle laptop riser with heat dissipation ventilation.",
                 "Laptop stand. Laptop or laptop accessories. Prevents neck fatigue and elevates screen to eye level.",
                 7, "Silver Metallic", "TC-STND-ALM-01", 1299m, 749m, "/images/study-hub/test-pic-placeholder.svg"),
                ("tech-for-study", "Braided 100W Fast-Charging USB-C Cable (2m)", "braided-100w-fast-charging-usb-c-cable-2m",
                 "Extra durable nylon braided Type-C to Type-C fast charge and data sync cable.",
                 "Charging cables. High wattage 100W Power Delivery compatible with all laptops and phones.",
                 8, "2 Meter Black", "TC-CBL-100W-2M", 699m, 399m, "/images/study-hub/test-pic-placeholder.svg"),
                ("tech-for-study", "20000mAh 65W Fast-Charging Power Bank", "20000mah-65w-fast-charging-power-bank",
                 "High-capacity airline approved power bank capable of charging laptops and smartphones.",
                 "Power bank. LED digital battery display with dual USB-C and USB-A outputs.",
                 9, "Graphite Black", "TC-PWR-20K-65W", 3999m, 2499m, "/images/study-hub/test-pic-placeholder.svg"),

                // 3. Documents & Organization
                ("document-organization", "RFID-Blocking Multi-Passport & Visa Travel Holder", "rfid-blocking-multi-passport-travel-visa-wallet",
                 "Zippered security document organizer for passport, visa, tickets & cards.",
                 "Passport holder. High-density nylon fabric with built-in RFID shielding material to protect against unauthorized digital scanning. Features pen loop and detachable wristlet.",
                 1, "Charcoal Black", "DOC-WL-RFID-BLK", 1199m, 699m, "/images/study-hub/item-organizer.png"),
                ("document-organization", "Waterproof 13-Pocket Expanding File Folder", "waterproof-13-pocket-expanding-file-folder",
                 "Heavy-duty poly document file with color-coded index tabs for university certificates.",
                 "Document folder. Secure buckle closure with waterproof polypropylene body. Organizes I-20, offer letters, insurance papers, transcripts, and passport copies.",
                 2, "Navy Blue - A4/Letter", "DOC-ACC-13P-NV", 899m, 499m, "/images/study-hub/item-organizer.png"),
                ("document-organization", "Clear Transparent Document Sleeves (50-Pack)", "clear-transparent-document-sleeves-50-pack",
                 "A4 archival quality non-stick sheet protectors for university documents.",
                 "Transparent document sleeves. Protects certificates and transcripts from spills, dust, and tears.",
                 3, "Pack of 50", "DOC-SLV-CLR-50", 499m, 299m, "/images/study-hub/test-pic-placeholder.svg"),
                ("document-organization", "Hardbound Academic Certificates Folder with Gold Foil", "hardbound-academic-certificates-folder-gold-foil",
                 "Deluxe padded leatherette diploma and degree certificate holder.",
                 "Academic certificates folder. Corner ribbon slots preserve official university degree and transcripts without creasing.",
                 4, "Royal Navy", "DOC-CERT-PRM-NV", 999m, 599m, "/images/study-hub/test-pic-placeholder.svg"),
                ("document-organization", "Admission Letter, I-20 & Visa Document Organizer Case", "admission-letter-i20-visa-document-organizer-case",
                 "Heavy-duty zippered portfolio with labeled dividers for visa interviews and travel.",
                 "Admission letter and visa document organizer. Copies of educational documents. Designed to keep original passports, letters, and certificates secure.",
                 5, "Executive Grey", "DOC-VISA-CS-GRY", 1299m, 799m, "/images/study-hub/item-organizer.png"),
                ("document-organization", "Durable Silicone Security Luggage Tags (Pack of 4)", "durable-silicone-security-luggage-tags-4pk",
                 "Flexible baggage tags with privacy flap and stainless steel loop.",
                 "Luggage tags. Identifies student luggage clearly on international flight transits.",
                 6, "Assorted Bright Colors", "DOC-LUG-TAG-4PK", 499m, 299m, "/images/study-hub/test-pic-placeholder.svg"),
                ("document-organization", "Waterproof Zipper Security Small Document Pouch", "waterproof-zipper-security-small-document-pouch",
                 "Compact travel pouch for boarding passes, currency, and emergency ID.",
                 "Small document pouch. Keep original passports and certificates with the student; don't pack them in checked luggage.",
                 7, "Slate Grey", "DOC-PCH-SML-01", 399m, 249m, "/images/study-hub/test-pic-placeholder.svg"),

                // 4. Desk & Room Study Essentials
                ("desk-room-study-essential", "Academic Year Study Planner & Student Diary 2026", "academic-year-study-planner-diary-2026",
                 "Comprehensive semester academic diary with weekly habit trackers and goal maps.",
                 "Study planner or academic diary. Weekly timetable. Thick ink-proof pages with ribbon markers.",
                 1, "Hardcover Midnight Blue", "DK-PLN-ACAD-26", 899m, 549m, "/images/study-hub/test-pic-placeholder.svg"),
                ("desk-room-study-essential", "Magnetic Dry-Erase Weekly Timetable Study Board", "magnetic-dry-erase-weekly-timetable-study-board",
                 "Framed weekly schedule planner whiteboard with markers and magnetic eraser.",
                 "Weekly timetable. Organize classes, assignment due dates, and study hours cleanly.",
                 2, "Whiteboard 40x30cm", "DK-BD-TIMETBL-01", 799m, 449m, "/images/study-hub/test-pic-placeholder.svg"),
                ("desk-room-study-essential", "Multi-Grid Metal Mesh Desk Organizer Caddy", "multi-grid-metal-mesh-desk-organizer-caddy",
                 "6-compartment study desk organizer with slide-out mini drawer.",
                 "Desk organizer. Pen holder. Keeps study desk tidy with dedicated slots for pens, sticky notes, rulers, and phone.",
                 3, "Matte Black Mesh", "DK-ORG-MSH-BLK", 899m, 499m, "/images/study-hub/test-pic-placeholder.svg"),
                ("desk-room-study-essential", "Dimmable LED Desk Lamp with USB Phone Charger", "dimmable-led-desk-lamp-usb-charger",
                 "Foldable eye-care reading lamp with 5 color modes and touch brightness control.",
                 "Small desk lamp. Reading light. Flicker-free natural light with flexible arm and integrated USB charging port.",
                 4, "Pure White", "DK-LMP-LED-WHT", 2199m, 1299m, "/images/study-hub/test-pic-placeholder.svg"),
                ("desk-room-study-essential", "BPA-Free Motivational Reusable Water Bottle 1L", "bpa-free-motivational-reusable-water-bottle-1l",
                 "Time marker leakproof Tritan hydration bottle with carry strap.",
                 "Reusable water bottle. Hydration reminder for long exam study sessions in library and dorm room.",
                 5, "Frosted Grey - 1L", "DK-BOT-MOTV-1L", 799m, 449m, "/images/study-hub/test-pic-placeholder.svg"),
                ("desk-room-study-essential", "Clip-On Rechargeable LED Reading Light for Study", "clip-on-rechargeable-led-reading-light-study",
                 "3 color temperature flexible clip-on light for textbooks and bedtime study.",
                 "Reading light. Bookmarks. Up to 30 hours run time on single charge with eye-friendly warm light.",
                 6, "Soft White", "DK-LMP-CLIP-01", 699m, 399m, "/images/study-hub/test-pic-placeholder.svg"),

                // 5. College & Campus Essentials
                ("college-campus-essentials", "Water-Resistant Campus Laptop Backpack 30L with USB Port", "water-resistant-campus-laptop-backpack-30l",
                 "Durable campus daypack with padded 15.6 inch laptop sleeve and luggage strap.",
                 "Laptop backpack. Multi-compartment storage with hidden anti-theft back pocket and reinforced shoulder padding for campus commuting.",
                 1, "Deep Navy - 30L", "CMP-BP-30L-NV", 3499m, 2199m, "/images/study-hub/test-pic-placeholder.svg"),
                ("college-campus-essentials", "Padded Shockproof Laptop Compartment Sleeve", "padded-shockproof-laptop-compartment-sleeve",
                 "Protective inner sleeve case designed for campus backpack compartments.",
                 "Laptop compartment sleeve. Water-repellent neoprene with soft fleece interior.",
                 2, "Charcoal 15.6\"", "CMP-SLV-PAD-15", 999m, 599m, "/images/study-hub/item-sleeve.png"),
                ("college-campus-essentials", "Windproof Compact Automatic Folding Campus Umbrella", "windproof-compact-automatic-folding-campus-umbrella",
                 "Teflon-coated 10-rib reinforced portable rain and wind umbrella.",
                 "Compact umbrella. Fits easily in college backpack side pockets with quick one-button open and close.",
                 3, "Classic Black", "CMP-UMB-WND-BLK", 899m, 499m, "/images/study-hub/test-pic-placeholder.svg"),
                ("college-campus-essentials", "Leakproof Stainless Steel Bento Lunch Box", "leakproof-stainless-steel-bento-lunch-box",
                 "3-compartment food-grade lunch container with portable cutlery set.",
                 "Lunch box. Keeps home-cooked meals fresh on long lecture and lab days.",
                 4, "Stainless / Blue Lid", "CMP-LNB-SS-3C", 1299m, 799m, "/images/study-hub/test-pic-placeholder.svg"),
                ("college-campus-essentials", "Vacuum Insulated Stainless Steel Bottle 1000ml", "vacuum-insulated-stainless-steel-bottle-1000ml",
                 "24-hour cold and 12-hour hot double-wall flask with leakproof lid.",
                 "Water bottle. Reusable water bottle. Food-grade 18/8 stainless steel with sweat-proof powder coat finish.",
                 5, "Cobalt Blue - 1000ml", "CMP-BOT-SS-1L", 1099m, 649m, "/images/study-hub/test-pic-placeholder.svg"),
                ("college-campus-essentials", "Retractable ID Card Holder and Neck Lanyard", "retractable-id-card-holder-neck-lanyard",
                 "Dual-sided clear badge protector with heavy-duty metal clip and breakaway strap.",
                 "ID card holder and lanyard. Small stationery pouch. Ideal for university access cards and student transit passes.",
                 6, "Black & Blue Pack", "CMP-ID-LNYD-01", 399m, 199m, "/images/study-hub/test-pic-placeholder.svg"),
                ("college-campus-essentials", "Foldable Eco-Friendly Reusable Campus Shopping Bag", "foldable-eco-friendly-reusable-campus-shopping-bag",
                 "Compact ripstop nylon tote that folds into an attached mini pouch.",
                 "Reusable shopping bag. Durable holds up to 20kg of textbooks or grocery essentials.",
                 7, "Navy Blue", "CMP-BAG-ECO-NV", 349m, 199m, "/images/study-hub/test-pic-placeholder.svg"),

                // 6. Online Classes & Productivity
                ("online-classes-productivity", "Full HD 1080p Webcam with Noise-Cancelling Microphone", "full-hd-1080p-webcam-noise-cancelling-mic",
                 "Wide-angle auto-focus webcam with physical privacy shutter for lectures & interviews.",
                 "Webcam, if not built into the laptop. Sharp 1080p 30fps streaming with low-light auto correction and dual noise-cancelling mics.",
                 1, "Matte Black", "PRD-CAM-1080P", 2899m, 1799m, "/images/study-hub/test-pic-placeholder.svg"),
                ("online-classes-productivity", "USB Conference Headset with Noise-Cancelling Boom Mic", "usb-conference-headset-noise-cancelling-mic",
                 "Lightweight on-ear headphones with in-line volume & mute controls.",
                 "Microphone or headset. Crystal clear audio for Zoom, Teams, and online lectures. Padded leatherette ear cups.",
                 2, "Black - USB/3.5mm", "PRD-HDS-USB-BLK", 2299m, 1399m, "/images/study-hub/test-pic-placeholder.svg"),
                ("online-classes-productivity", "Adjustable Desktop Aluminum Phone Stand", "adjustable-desktop-aluminum-phone-stand",
                 "Foldable angle and height adjustable phone holder with cable cutout.",
                 "Phone stand. Perfect for hands-free video calls, 2-factor authentication, and reference viewing while studying.",
                 3, "Space Grey", "PRD-STND-PHN-SG", 599m, 349m, "/images/study-hub/test-pic-placeholder.svg"),
                ("online-classes-productivity", "Ultra-Fast 1TB Portable External SSD Storage Drive", "ultra-fast-1tb-portable-external-ssd-drive",
                 "Compact solid-state drive with 1050MB/s transfer speeds and USB-C cable.",
                 "External storage drive. Backs up virtual machines, research datasets, lecture recordings, and semester portfolios.",
                 4, "1TB Matte Black", "PRD-SSD-1TB-EX", 6999m, 4499m, "/images/study-hub/test-pic-placeholder.svg"),
                ("online-classes-productivity", "Travel Electronics Cable Organizer Travel Bag", "travel-electronics-cable-organizer-bag",
                 "Waterproof tech accessories pouch with elastic loops and mesh pockets.",
                 "Cable organizer. Fits chargers, dongles, power bank, USB drives, and earphone cords neatly.",
                 5, "Charcoal Grey", "PRD-CBL-ORG-01", 799m, 449m, "/images/study-hub/test-pic-placeholder.svg"),
                ("online-classes-productivity", "All-in-One Screen and Electronics Cleaning Kit", "all-in-one-screen-electronics-cleaning-kit",
                 "Microfiber cloth with alcohol-free screen spray and keyboard detail brush.",
                 "Screen cleaning kit. Removes fingerprints and dust safely from laptops, tablets, and smartphone screens.",
                 6, "Full Cleaning Set", "PRD-CLN-SCR-01", 499m, 299m, "/images/study-hub/test-pic-placeholder.svg"),
                ("online-classes-productivity", "Semester Academic Planner for Assignments & Deadlines", "semester-academic-planner-assignments-deadlines",
                 "Dedicated assignment checklist book with grade tracker and milestone timelines.",
                 "Planner for assignments and deadlines. Prevents missed submissions and helps students stay ahead in rigorous coursework.",
                 7, "Spiral A5 Edition", "PRD-PLN-ASGN-A5", 599m, 349m, "/images/study-hub/test-pic-placeholder.svg")
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


        // Ensure Food Category Subcategories and Products (FreshBasket / Food Hub)
        if (catMap.TryGetValue("food", out var foodCatId))
        {
            var existingFoodSubs = await context.Subcategories
                .Where(s => s.CategoryId == foodCatId)
                .ToListAsync(cancellationToken);

            var requiredFoodSubs = new (string Name, string Slug, int Order)[]
            {
                ("Groceries", "groceries", 1),
                ("Instant Noodles", "instant-noodles", 2),
                ("Nuts & Dry Fruits", "nuts-dry-fruits", 3),
                ("Spices & Seasonings", "spices-seasonings", 4),
                ("Breakfast Cereals", "breakfast-cereals", 5),
                ("Snacks", "snacks", 6),
                ("Pickles", "pickles", 7)
            };

            foreach (var (subName, subSlug, subOrder) in requiredFoodSubs)
            {
                var existingSub = existingFoodSubs.FirstOrDefault(s => string.Equals(s.Slug, subSlug, StringComparison.OrdinalIgnoreCase));
                if (existingSub == null)
                {
                    var newSub = new Subcategory(foodCatId, subName, subSlug, subOrder);
                    context.Subcategories.Add(newSub);
                }
                else
                {
                    existingSub.Update(subName, subSlug, subOrder);
                    existingSub.Activate();
                }
            }
            await context.SaveChangesAsync(cancellationToken);

            var foodSubList = await context.Subcategories
                .Where(s => s.CategoryId == foodCatId)
                .ToListAsync(cancellationToken);

            var foodSeedProducts = new (string SubSlug, string Name, string Slug, string Short, string Desc, int Order, string Var, string Sku, decimal Mrp, decimal Price, string Media)[]
            {
                ("groceries", "Aashirvaad Superior MP Whole Wheat Atta 5kg", "aashirvaad-atta-5kg",
                 "100% pure whole wheat grain atta with zero maida and high dietary fibre.",
                 "Aashirvaad Atta is made from grains which are heavy on the palm, golden amber in colour and hard in bite. 4-step advantage process of sourcing, cleaning, and mechanised traditional chakki grinding.",
                 1, "5kg Pouch", "FOOD-ATTA-5KG", 310m, 279m, "/images/food-hub/cat-groceries.webp"),

                ("groceries", "Fortune Sunlite Refined Sunflower Oil 1L", "fortune-sunlite-sunflower-oil-1l",
                 "Light, healthy and easy to digest refined sunflower cooking oil.",
                 "Fortune Sunlite is enriched with Vitamin A and Vitamin D. Low absorb technology helps food absorb less oil, keeping your daily meals light and delicious.",
                 2, "1L Pouch", "FOOD-OIL-1L", 165m, 145m, "/images/food-hub/promo-groceries.webp"),

                ("groceries", "Daawat Rozana Super Basmati Rice 5kg", "daawat-rozana-basmati-rice-5kg",
                 "Aromatic, fluffy long grain Basmati rice ideal for everyday cooking and biryani.",
                 "Daawat Rozana is aged to perfection to bring out the exquisite aroma and slender grain size that stays non-sticky after cooking.",
                 3, "5kg Bag", "FOOD-RICE-5KG", 450m, 389m, "/images/food-hub/hero-food-basket.webp"),

                ("groceries", "Tata Tea Agni Strong Leaf Tea 1kg", "tata-tea-agni-1kg",
                 "Special blend of quality tea with 10% extra long leaves for a rich, kadak chai aroma.",
                 "Tata Tea Agni provides the energy to challenge daily tasks with strong taste and rich red liquor.",
                 4, "1kg Pack", "FOOD-TEA-1KG", 270m, 245m, "/images/food-hub/cat-groceries.webp"),

                ("groceries", "Nescafe Classic 100% Pure Instant Coffee 100g Jar", "nescafe-classic-instant-coffee-100g",
                 "Rich signature aroma and bold coffee taste crafted from fine Robusta beans.",
                 "Start every morning with fresh inspiration. Premium instant coffee powder in an airtight glass jar.",
                 5, "100g Glass Jar", "FOOD-COF-100G", 340m, 295m, "/images/food-hub/hero-food-basket.webp"),

                ("instant-noodles", "Maggi 2-Minute Masala Instant Noodles 280g", "maggi-2-minute-masala-noodles-280g",
                 "Classic Indian masala seasoning with 10 authentic roasted spices.",
                 "Maggi 2-Minute Masala noodles are fortified with iron. Quick, comforting snack for study nights and quick breaks.",
                 1, "Pack of 4 (280g)", "FOOD-NDL-MAGGI-4PK", 65m, 55m, "/images/food-hub/cat-noodles.webp"),

                ("instant-noodles", "Nissin Top Ramen Curry Instant Noodles 280g", "nissin-top-ramen-curry-noodles-280g",
                 "Non-sticky flat noodle texture with spicy curry soup base.",
                 "Top Ramen is loved for smooth noodles with a rich masala broth that cooks in just 3 minutes.",
                 2, "Pack of 4 (280g)", "FOOD-NDL-RAMEN-4PK", 60m, 52m, "/images/food-hub/cat-noodles.webp"),

                ("nuts-dry-fruits", "Premium California Whole Natural Almonds 200g", "california-whole-natural-almonds-200g",
                 "Crisp, crunchy 100% natural raw almonds vacuum sealed for freshness.",
                 "High in protein, dietary fibre and Vitamin E. Perfect healthy snack for study memory and daily vitality.",
                 1, "200g Fresh Pouch", "FOOD-NUT-ALM-200G", 230m, 199m, "/images/food-hub/cat-nuts.webp"),

                ("nuts-dry-fruits", "Crispy Whole Cashew Nuts (Kaju) 250g", "crispy-whole-cashew-nuts-250g",
                 "Grade W320 premium unbroken white cashews with creamy crunch.",
                 "Hand-sorted cashew nuts, zero preservatives, natural energy booster for active student lifestyle.",
                 2, "250g Pouch", "FOOD-NUT-KAJU-250G", 330m, 285m, "/images/food-hub/cat-nuts.webp"),

                ("spices-seasonings", "Everest Pure Turmeric (Haldi) Powder 200g", "everest-pure-turmeric-powder-200g",
                 "High natural curcumin percentage for vibrant color and medicinal healing power.",
                 "Hygienically ground from selected dry turmeric fingers under strict cold-milling technology.",
                 1, "200g Box", "FOOD-SPC-HLD-200G", 80m, 70m, "/images/food-hub/cat-spices.webp"),

                ("spices-seasonings", "Catch Super Garam Masala Powder 100g", "catch-super-garam-masala-100g",
                 "Low temperature ground exotic spices including cardamom, clove, cinnamon and mace.",
                 "Adds an irresistible royal aroma and rich gravy depth to curries, dals and gravies.",
                 2, "100g Carton", "FOOD-SPC-GM-100G", 80m, 68m, "/images/food-hub/cat-spices.webp"),

                ("breakfast-cereals", "Kellogg's Real Almond & Honey Corn Flakes 475g", "kelloggs-real-almond-honey-corn-flakes-475g",
                 "Crunchy golden corn flakes with real honey coating and sliced California almonds.",
                 "Fortified with B group vitamins, iron and calcium. Energising breakfast ready in 2 minutes with warm or chilled milk.",
                 1, "475g Carton", "FOOD-CER-KCF-475G", 210m, 185m, "/images/food-hub/cat-cereals.webp"),

                ("breakfast-cereals", "Quaker Rolled Wholegrain Oats 1kg", "quaker-rolled-wholegrain-oats-1kg",
                 "100% natural wholegrain oats packed with soluble beta-glucan fibre.",
                 "Supports heart health, helps control cholesterol and provides long-lasting sustained energy for long college days.",
                 2, "1kg Pouch", "FOOD-CER-OAT-1KG", 199m, 175m, "/images/food-hub/cat-cereals.webp"),

                ("snacks", "Crispy Golden Salted Potato Chips 150g", "crispy-golden-salted-potato-chips-150g",
                 "Thinly sliced farm potatoes fried to golden crunch and lightly seasoned with rock salt.",
                 "The timeless classic snack for study breaks, movie nights, and campus gatherings.",
                 1, "150g Party Pack", "FOOD-SNK-CHP-150G", 50m, 45m, "/images/food-hub/promo-snacks.webp"),

                ("snacks", "Haldiram's Nagpur Aloo Bhujia Namkeen 400g", "haldirams-nagpur-aloo-bhujia-400g",
                 "Tangy spiced potato and gram flour crisp noodles with mint and red chili.",
                 "India's favourite crunchy tea-time namkeen, hygienically prepared and sealed in nitrogen-flushed pouch.",
                 2, "400g Pouch", "FOOD-SNK-BHU-400G", 130m, 115m, "/images/food-hub/cat-snacks.webp"),

                ("pickles", "Mother's Recipe Traditional Mixed Pickle 500g Jar", "mothers-recipe-traditional-mixed-pickle-500g",
                 "Hand-cut mango, lime, carrot, green chili and ginger marinated in mustard oil and whole spices.",
                 "Traditional grandmother recipe that adds an explosion of tangy zest to every meal and paratha.",
                 1, "500g Glass Jar", "FOOD-PCK-MIX-500G", 145m, 125m, "/images/food-hub/promo-pickles.webp"),

                ("pickles", "Priya Spicy Mango Pickle with Garlic 300g", "priya-spicy-mango-pickle-garlic-300g",
                 "Authentic South Indian avakaya style raw mango pieces with aromatic peeled garlic pods.",
                 "Made with unrefined sesame oil, red Guntur chilies and fenugreek seeds.",
                 2, "300g Jar", "FOOD-PCK-MGO-300G", 110m, 95m, "/images/food-hub/cat-pickles.webp")
            };

            foreach (var fp in foodSeedProducts)
            {
                var matchingSub = foodSubList.FirstOrDefault(s => string.Equals(s.Slug, fp.SubSlug, StringComparison.OrdinalIgnoreCase));
                var exists = await context.Products.AnyAsync(p => p.Slug == fp.Slug, cancellationToken);
                if (!exists)
                {
                    var product = new Product(foodCatId, fp.Name, fp.Slug, fp.Short, fp.Desc, fp.Order);
                    if (matchingSub != null)
                    {
                        product.SetSubcategory(matchingSub.Id);
                    }
                    context.Products.Add(product);
                    await context.SaveChangesAsync(cancellationToken);

                    var variant = new ProductVariant(product.Id, fp.Var, fp.Sku, fp.Mrp, fp.Price, true);
                    var media = new ProductMedia(product.Id, fp.Media, fp.Name, 0, true);
                    context.ProductVariants.Add(variant);
                    context.ProductMedia.Add(media);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
        }

        // Ensure Travel Gear Category Subcategories and Products (Dreamflykit Travel Hub)
        if (catMap.TryGetValue("travel-gear", out var travelCatId))
        {
            var existingTravelSubs = await context.Subcategories
                .Where(s => s.CategoryId == travelCatId)
                .ToListAsync(cancellationToken);

            var requiredTravelSubs = new (string Name, string Slug, int Order)[]
            {
                ("Luggage & Suitcases", "luggage-suitcases", 1),
                ("Packing Organizers", "packing-organizers", 2),
                ("Comfort Essentials", "comfort-essentials", 3),
                ("Adapters & Converters", "adapters-converters", 4),
                ("Bottles & Hydration", "bottles-hydration", 5),
                ("Toiletry & Hygiene", "toiletry-hygiene", 6),
                ("Travel Documents", "travel-documents", 7),
                ("SIM & Connectivity", "sim-connectivity", 8),
                ("Travel Insurance", "travel-insurance", 9),
                ("Locks & Security", "locks-security", 10),
                ("Tech Accessories", "tech-accessories", 11),
                ("Travel Entertainment", "travel-entertainment", 12)
            };

            foreach (var (subName, subSlug, subOrder) in requiredTravelSubs)
            {
                var existingSub = existingTravelSubs.FirstOrDefault(s => string.Equals(s.Slug, subSlug, StringComparison.OrdinalIgnoreCase));
                if (existingSub == null)
                {
                    var newSub = new Subcategory(travelCatId, subName, subSlug, subOrder);
                    context.Subcategories.Add(newSub);
                }
                else
                {
                    existingSub.Update(subName, subSlug, subOrder);
                    existingSub.Activate();
                }
            }
            await context.SaveChangesAsync(cancellationToken);

            var travelSubList = await context.Subcategories
                .Where(s => s.CategoryId == travelCatId)
                .ToListAsync(cancellationToken);

            var travelSeedProducts = new (string SubSlug, string Name, string Slug, string Short, string Desc, int Order, string Var, string Sku, decimal Mrp, decimal Price, string Media)[]
            {
                ("luggage-suitcases", "American Tourister Stratum 2.0 Expandable Spinner Luggage 28\"", "american-tourister-stratum-spinner-luggage-28",
                 "Ultra-durable polycarbonate hard shell with 360-degree dual spinner wheels and TSA lock.",
                 "Engineered for international students and long-haul travel. Features scratch-resistant rugged exterior, expandable main packing compartment (+20% volume), interior mesh dividers, and ergonomic push-button trolley handle.",
                 1, "28-Inch Large / Cobalt Blue", "TRV-LUG-AT28-BLU", 12999m, 6499m, "/images/travel-hub/cat-luggage-suitcases.webp"),

                ("luggage-suitcases", "Skybags Safari Quest Lightweight Cabin Trolley 55cm", "skybags-safari-quest-cabin-trolley-55cm",
                 "Airline compliant international cabin trolley with whisper-silent spinner wheels.",
                 "Meets all major international and domestic carry-on airline baggage regulations. Durable water-resistant polyester body with reinforced corner guards and quick-access front tech pocket.",
                 2, "Cabin 55cm / Charcoal Black", "TRV-LUG-SB55-BLK", 5999m, 2899m, "/images/travel-hub/cat-luggage-suitcases.webp"),

                ("packing-organizers", "AmazonBasics 6-Piece Compression Travel Packing Cubes Set", "compression-travel-packing-cubes-set-6pc",
                 "Double zipper compression system saves up to 60% luggage suitcase volume.",
                 "Set includes 2 Extra Large cubes for winter jackets, 2 Medium cubes for clothing, 1 Slim cube for underwear/socks, and 1 water-resistant laundry/shoe bag. Made of tear-resistant ripstop nylon with breathable mesh panels.",
                 3, "Set of 6 / Slate Grey", "TRV-PCK-CUBE6-GRY", 2499m, 1199m, "/images/travel-hub/cat-packing-organizers.webp"),

                ("comfort-essentials", "Cabeau Evolution S3 Ergonomic Memory Foam Neck Pillow", "cabeau-evolution-s3-memory-foam-neck-pillow",
                 "360-degree head support with patented chin strap prevents neck strain on long flights.",
                 "High-density responsive memory foam core with seat-strap attachment system. Includes moisture-wicking washable cover, quick-stow compact travel pouch, and premium silicone earplugs.",
                 4, "Standard / Deep Indigo", "TRV-CMF-CAB-BLU", 3499m, 1999m, "/images/travel-hub/cat-comfort-essentials.webp"),

                ("adapters-converters", "Universal All-in-One 65W GaN Fast Charger Travel Adapter", "universal-all-in-one-65w-gan-travel-adapter",
                 "Worldwide plug compatibility (US/UK/EU/AU/IN) across 180+ countries with 65W dual USB-C PD.",
                 "Ultra-compact GaN fast-charge technology powers laptops, smartphones, tablets, and power banks simultaneously. Features auto-resetting safety fuse, surge protection, and child-safe shutter.",
                 5, "65W GaN Dual USB-C + 2 USB-A", "TRV-ADP-GAN65-WHT", 3999m, 1899m, "/images/travel-hub/cat-adapters-converters.webp"),

                ("bottles-hydration", "Milton Thermosteel Flip-Lid Insulated Water Bottle 1000ml", "milton-thermosteel-insulated-bottle-1000ml",
                 "24-hour cold / 18-hour hot double-wall vacuum insulated stainless steel flask.",
                 "100% leak-proof flip lid with carry loop, condensation-free powder coated grip, and BPA-free food-grade 304 stainless steel interior. Ideal for flights, university campus walks, and library study sessions.",
                 6, "1000ml / Matte Steel Black", "TRV-BTL-MLT1L-BLK", 1399m, 799m, "/images/travel-hub/cat-bottles-hydration.webp"),

                ("toiletry-hygiene", "Hanging Toiletry & Grooming Kit Bag with TSA Clear Bottles Set", "hanging-toiletry-bag-tsa-clear-bottles-set",
                 "Waterproof hanging organizer with 360 swivel hook and leakproof silicone bottle containers.",
                 "Includes 4x 100ml TSA airport security compliant leakproof travel bottles, 2 cream jars, spray mister, toothbrush sleeve, and multiple mesh dividers. Compact fold-and-zip structure.",
                 7, "Hanging Bag + 8pc Bottle Kit", "TRV-TOI-HNG8-BLK", 1799m, 899m, "/images/travel-hub/cat-toiletry-hygiene.webp"),

                ("travel-documents", "RFID-Blocking Multi-Passport & Travel Document Organizer Wallet", "rfid-blocking-multi-passport-travel-organizer-wallet",
                 "Safely holds up to 4 passports, boarding passes, currency notes, credit cards & SIM pins.",
                 "Built-in military-grade RFID blocking technology shields your personal biometric data from digital theft. Heavy-duty zinc alloy zipper with removable wrist strap and water-repellent nylon shell.",
                 8, "Multi-Passport / Navy Blue", "TRV-DOC-RFID-NVY", 1899m, 799m, "/images/travel-hub/cat-travel-documents.webp"),

                ("sim-connectivity", "Global Travel Data eSIM & Multi-Country 4G/5G Connectivity Kit", "global-travel-data-esim-multi-country-kit",
                 "Pre-activated high-speed 4G/5G international roaming data package across 140+ countries.",
                 "Seamless instant QR code activation. Unlimited local calls and generous high-speed data for USA, UK, EU, Canada, and Australia destinations without expensive roaming charges.",
                 9, "10GB 30-Day Global Roaming", "TRV-SIM-ESIM-10G", 2999m, 1499m, "/images/travel-hub/cat-sim-connectivity.webp"),

                ("locks-security", "Master Lock TSA-Approved Cable Luggage Lock (Pack of 2)", "master-lock-tsa-approved-cable-luggage-lock-2pk",
                 "Resettable 3-dial combination lock with flexible vinyl-sheathed braided steel cable.",
                 "Allows airport TSA officers to inspect and relock luggage without damaging zippers. Robust zinc body and flexible cable threads through multiple bag zips effortlessly.",
                 10, "Pack of 2 / Black & Silver", "TRV-LCK-TSA2-BLK", 1499m, 699m, "/images/travel-hub/cat-locks-security.webp"),

                ("tech-accessories", "Mi 20000mAh 50W Fast Charging Power Bank with Dual Type-C", "mi-20000mah-50w-fast-charging-power-bank",
                 "Flight-certified 74Wh capacity safe for airline cabin carry-on. Super-fast multi-device charge.",
                 "Can charge a typical smartphone 4-5 times or power Type-C compatible laptops. Features 12-layer advanced circuit protection against overcharging, short circuit, and high temperatures.",
                 11, "20000mAh / Matte Black", "TRV-TCH-PB20-BLK", 3499m, 2199m, "/images/travel-hub/cat-tech-accessories.webp"),

                ("travel-entertainment", "Sony WH-CH520 Wireless Bluetooth Headphones with 50-Hr Battery", "sony-wh-ch520-wireless-bluetooth-headphones",
                 "Comfortable lightweight swivel headphones with multipoint Bluetooth and crystal-clear sound.",
                 "Up to 50 hours battery life with 3-minute quick charge giving 1.5 hours playback. Built-in high-quality microphone for university calls and voice commands.",
                 12, "Wireless / Midnight Blue", "TRV-ENT-SNY520-BLU", 4999m, 3499m, "/images/travel-hub/cat-travel-entertainment.webp")
            };

            foreach (var tp in travelSeedProducts)
            {
                var existingProd = await context.Products
                    .FirstOrDefaultAsync(p => p.CategoryId == travelCatId && p.Slug == tp.Slug, cancellationToken);

                if (existingProd == null)
                {
                    var product = new Product(travelCatId, tp.Name, tp.Slug, tp.Short, tp.Desc, tp.Order);
                    var matchingSub = travelSubList.FirstOrDefault(s => string.Equals(s.Slug, tp.SubSlug, StringComparison.OrdinalIgnoreCase));
                    if (matchingSub != null)
                    {
                        product.SetSubcategory(matchingSub.Id);
                    }
                    context.Products.Add(product);
                    await context.SaveChangesAsync(cancellationToken);

                    var variant = new ProductVariant(product.Id, tp.Var, tp.Sku, tp.Mrp, tp.Price, true);
                    var media = new ProductMedia(product.Id, tp.Media, tp.Name, 0, true);
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
