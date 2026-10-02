using Dreamflykit.Domain.Addressing;
using Dreamflykit.Domain.Catalog;
using Dreamflykit.Domain.Geography;
using Dreamflykit.Domain.Inventory;
using Dreamflykit.Domain.Kits;
using Dreamflykit.Domain.Media;
using Dreamflykit.Domain.Orders;
using Dreamflykit.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Persistence;

public sealed class DreamflykitDbContext
    : IdentityDbContext<
        ApplicationUser,
        IdentityRole<Guid>,
        Guid>
{
    public DreamflykitDbContext(
        DbContextOptions<DreamflykitDbContext> options)
        : base(options)
    {
    }
    public DbSet<Country> Countries => Set<Country>();

    public DbSet<Season> Seasons => Set<Season>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Subcategory> Subcategories => Set<Subcategory>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductVariant> ProductVariants =>Set<ProductVariant>();

    public DbSet<ProductMedia> ProductMedia =>
    Set<ProductMedia>();

    public DbSet<ProductAttribute> ProductAttribute=> Set<ProductAttribute>();

    public DbSet<ProductAttributeValue> ProductAttributeValue => Set<ProductAttributeValue>();

    public DbSet<Warehouse> Warehouses =>
    Set<Warehouse>();

    public DbSet<InventoryItem> InventoryItems =>
        Set<InventoryItem>();

    public DbSet<StockMovement> StockMovements =>
        Set<StockMovement>();

    public DbSet<Kit> Kits => Set<Kit>();
    public DbSet<KitItem> KitItems => Set<KitItem>();

    public DbSet<Domain.Cart.Cart> Carts => Set<Domain.Cart.Cart>();
    public DbSet<Domain.Cart.CartItem> CartItems => Set<Domain.Cart.CartItem>();

    public DbSet<CustomerAddress> CustomerAddresses => Set<CustomerAddress>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<Domain.Wishlist.Wishlist> Wishlists => Set<Domain.Wishlist.Wishlist>();
    public DbSet<Domain.Wishlist.WishlistItem> WishlistItems => Set<Domain.Wishlist.WishlistItem>();

    // ----------------------------------------------------------------
    // Media Management Module
    // ----------------------------------------------------------------

    public DbSet<MediaAsset> MediaAssets => Set<MediaAsset>();

    public DbSet<MediaPermission> MediaPermissions => Set<MediaPermission>();

    public DbSet<MediaAuditLog> MediaAuditLogs => Set<MediaAuditLog>();

    public DbSet<Banner> Banners => Set<Banner>();

    public DbSet<HomepageSection> HomepageSections => Set<HomepageSection>();

    public DbSet<LandingPage> LandingPages => Set<LandingPage>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("public");

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.DateOfBirth)
                .HasColumnType("date");

            entity.Property(x => x.Gender)
                .HasMaxLength(50);

            entity.Property(x => x.Country)
                .HasMaxLength(100);

            entity.Property(x => x.ProfileImageUrl)
                .HasMaxLength(1000);
        });

        // ------------------------------------------------------------
        // Category
        // ------------------------------------------------------------

        builder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Slug)
                .HasMaxLength(180)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .IsRequired();

            entity.Property(x => x.DisplayOrder)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => x.Slug)
                .IsUnique();

            entity.HasIndex(x => x.DisplayOrder);

            entity.Property(x => x.ImageMediaAssetId);

            entity.HasOne<MediaAsset>()
                .WithMany()
                .HasForeignKey(x => x.ImageMediaAssetId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        });

        // ------------------------------------------------------------
        // Subcategory
        // ------------------------------------------------------------

        builder.Entity<Subcategory>(entity =>
        {
            entity.ToTable("Subcategories");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Slug)
                .HasMaxLength(180)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .IsRequired();

            entity.Property(x => x.DisplayOrder)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => x.Slug)
                .IsUnique();

            entity.HasIndex(x => new
            {
                x.CategoryId,
                x.DisplayOrder
            });

            entity.HasOne<Category>()
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.ImageMediaAssetId);

            entity.HasOne<MediaAsset>()
                .WithMany()
                .HasForeignKey(x => x.ImageMediaAssetId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        });

        // ------------------------------------------------------------
        // Product
        // ------------------------------------------------------------

        builder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.Slug)
                .HasMaxLength(220)
                .IsRequired();

            entity.Property(x => x.ShortDescription)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(x => x.Description)
                .HasMaxLength(5000)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .IsRequired();

            entity.Property(x => x.IsFeatured)
                .IsRequired();

            entity.Property(x => x.DisplayOrder)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => x.Slug)
                .IsUnique();

            entity.HasIndex(x => new
            {
                x.CategoryId,
                x.IsActive
            });

            entity.HasIndex(x => x.SubcategoryId);

            entity.HasOne<Category>()
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Subcategory>()
                .WithMany()
                .HasForeignKey(x => x.SubcategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ------------------------------------------------------------
        // Product Variant
        // ------------------------------------------------------------

        builder.Entity<ProductVariant>(entity =>
        {
            entity.ToTable("ProductVariants");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Sku)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.Mrp)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(x => x.SellingPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(x => x.IsDefault)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => x.Sku)
                .IsUnique();

            entity.HasIndex(x => new
            {
                x.ProductId,
                x.IsActive
            });

            entity.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // --------------------------------------------------------
        // Product Media
        // --------------------------------------------------------

        builder.Entity<ProductMedia>(entity =>
        {
            entity.ToTable("ProductMedia");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.MediaUrl)
                .HasMaxLength(2000)
                .IsRequired();

            entity.Property(x => x.AltText)
                .HasMaxLength(300)
                .IsRequired();

            entity.Property(x => x.DisplayOrder)
                .IsRequired();

            entity.Property(x => x.IsPrimary)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => new
            {
                x.ProductId,
                x.DisplayOrder
            });

            entity.HasIndex(x => new
            {
                x.ProductId,
                x.IsPrimary
            });

            entity.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // FK to centralized MediaAsset (nullable for backward-compat with legacy URL records)
            entity.Property(x => x.MediaAssetId);

            entity.HasOne<MediaAsset>()
                .WithMany()
                .HasForeignKey(x => x.MediaAssetId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        });


        // --------------------------------------------------------
        // Product Attribute
        // --------------------------------------------------------

        builder.Entity<ProductAttribute>(entity =>
        {
            entity.ToTable("ProductAttributes");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.Code)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.DisplayOrder)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => x.Code)
                .IsUnique();

            entity.HasIndex(x => x.DisplayOrder);
        });

        // --------------------------------------------------------
        // Product Attribute Value
        // --------------------------------------------------------

        builder.Entity<ProductAttributeValue>(entity =>
        {
            entity.ToTable("ProductAttributeValues");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Value)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => new
            {
                x.ProductId,
                x.AttributeId
            });

            entity.HasIndex(x => new
            {
                x.AttributeId,
                x.Value
            });

            entity.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<ProductAttribute>()
                .WithMany()
                .HasForeignKey(x => x.AttributeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // --------------------------------------------------------
        // Warehouse
        // --------------------------------------------------------

        builder.Entity<Warehouse>(entity =>
        {
            entity.ToTable("Warehouses");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Code)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(x => x.AddressLine1)
                .HasMaxLength(250)
                .IsRequired();

            entity.Property(x => x.AddressLine2)
                .HasMaxLength(250);

            entity.Property(x => x.City)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.State)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.PostalCode)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(x => x.Country)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => x.Code)
                .IsUnique();
        });

        // --------------------------------------------------------
        // Inventory Item
        // --------------------------------------------------------

        builder.Entity<InventoryItem>(entity =>
        {
            entity.ToTable("InventoryItems");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.AvailableQuantity)
                .IsRequired();

            entity.Property(x => x.ReservedQuantity)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.Property<uint>("xmin").IsRowVersion();

            entity.HasIndex(x => new
            {
                x.WarehouseId,
                x.ProductVariantId
            })
            .IsUnique();

            entity.HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<ProductVariant>()
                .WithMany()
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // --------------------------------------------------------
        // Stock Movement
        // --------------------------------------------------------

        builder.Entity<StockMovement>(entity =>
        {
            entity.ToTable("StockMovements");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.MovementType)
                .HasConversion<int>()
                .IsRequired();

            entity.Property(x => x.Quantity)
                .IsRequired();

            entity.Property(x => x.Reason)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(x => x.OccurredAtUtc)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => new
            {
                x.WarehouseId,
                x.ProductVariantId,
                x.OccurredAtUtc
            });

            entity.HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<ProductVariant>()
                .WithMany()
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Kit
        builder.Entity<Kit>(entity =>
        {
            entity.ToTable("Kits");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.CountryId)
                .IsRequired();

            entity.Property(x => x.SeasonId)
                .IsRequired();

            entity.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.Slug)
                .HasMaxLength(220)
                .IsRequired();

            entity.Property(x => x.Description)
                .HasMaxLength(5000)
                .IsRequired();

            entity.Property(x => x.Size)
                .HasConversion<int>()
                .IsRequired();

            entity.Property(x => x.Mrp)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(x => x.SellingPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .IsRequired();

            entity.Property(x => x.IsFeatured)
                .IsRequired();

            entity.Property(x => x.DisplayOrder)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => x.Slug)
                .IsUnique();

            entity.HasIndex(x => new
            {
                x.CountryId,
                x.SeasonId,
                x.Size
            })
            .IsUnique();

            entity.HasIndex(x => new
            {
                x.CountryId,
                x.SeasonId,
                x.IsActive
            });

            entity.HasIndex(x => x.DisplayOrder);

            entity.HasOne<Country>()
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Season>()
                .WithMany()
                .HasForeignKey(x => x.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // KitItem
        builder.Entity<KitItem>(entity =>
        {
            entity.ToTable("KitItems");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.KitId)
                .IsRequired();

            entity.Property(x => x.ProductVariantId)
                .IsRequired();

            entity.Property(x => x.Quantity)
                .IsRequired();

            entity.Property(x => x.DisplayOrder)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => new
            {
                x.KitId,
                x.ProductVariantId
            })
            .IsUnique();

            entity.HasIndex(x => new
            {
                x.KitId,
                x.DisplayOrder
            });

            entity.HasOne<Kit>()
                .WithMany()
                .HasForeignKey(x => x.KitId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<ProductVariant>()
                .WithMany()
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // --------------------------------------------------------
        // Cart
        // --------------------------------------------------------

        builder.Entity<Domain.Cart.Cart>(entity =>
        {
            entity.ToTable("Carts");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.UserId)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            // One cart per user
            entity.HasIndex(x => x.UserId)
                .IsUnique();

            entity.HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey(x => x.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // --------------------------------------------------------
        // CartItem
        // --------------------------------------------------------

        builder.Entity<Domain.Cart.CartItem>(entity =>
        {
            entity.ToTable("CartItems");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.CartId)
                .IsRequired();

            entity.Property(x => x.ProductVariantId)
                .IsRequired();

            entity.Property(x => x.ProductName)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.VariantName)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Sku)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.ImageUrl)
                .HasMaxLength(2000);

            entity.Property(x => x.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(x => x.Mrp)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(x => x.Quantity)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            // One variant per cart (enforces merge-on-add logic)
            entity.HasIndex(x => new { x.CartId, x.ProductVariantId })
                .IsUnique();

            entity.HasOne<ProductVariant>()
                .WithMany()
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // --------------------------------------------------------
        // CustomerAddress
        // --------------------------------------------------------
        builder.Entity<CustomerAddress>(entity =>
        {
            entity.ToTable("CustomerAddresses");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.UserId)
                .IsRequired();

            entity.Property(x => x.FullName)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Phone)
                .HasMaxLength(25)
                .IsRequired();

            entity.Property(x => x.AddressLine1)
                .HasMaxLength(250)
                .IsRequired();

            entity.Property(x => x.AddressLine2)
                .HasMaxLength(250);

            entity.Property(x => x.City)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.State)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.PostalCode)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(x => x.Country)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.AddressType)
                .HasConversion<int>()
                .IsRequired();

            entity.Property(x => x.IsDefault)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => new { x.UserId, x.AddressType });
        });

        // --------------------------------------------------------
        // Order
        // --------------------------------------------------------
        builder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.UserId)
                .IsRequired();

            entity.Property(x => x.OrderNumber)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(x => x.Status)
                .HasConversion<int>()
                .IsRequired();

            entity.Property(x => x.CancellationReason)
                .HasMaxLength(500);

            entity.Property(x => x.PaymentOrderId)
                .HasMaxLength(100);

            entity.Property(x => x.PaymentId)
                .HasMaxLength(100);

            entity.Property(x => x.PlacedAtUtc)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => x.OrderNumber)
                .IsUnique();

            entity.HasIndex(x => new { x.UserId, x.PlacedAtUtc });

            // Owned entity: ShippingAddress
            entity.OwnsOne(x => x.ShippingAddress, sa =>
            {
                sa.Property(p => p.FullName).HasColumnName("Shipping_FullName").HasMaxLength(150).IsRequired();
                sa.Property(p => p.Phone).HasColumnName("Shipping_Phone").HasMaxLength(25).IsRequired();
                sa.Property(p => p.AddressLine1).HasColumnName("Shipping_AddressLine1").HasMaxLength(250).IsRequired();
                sa.Property(p => p.AddressLine2).HasColumnName("Shipping_AddressLine2").HasMaxLength(250);
                sa.Property(p => p.City).HasColumnName("Shipping_City").HasMaxLength(100).IsRequired();
                sa.Property(p => p.State).HasColumnName("Shipping_State").HasMaxLength(100).IsRequired();
                sa.Property(p => p.PostalCode).HasColumnName("Shipping_PostalCode").HasMaxLength(20).IsRequired();
                sa.Property(p => p.Country).HasColumnName("Shipping_Country").HasMaxLength(100).IsRequired();
            });

            // Owned entity: BillingAddress
            entity.OwnsOne(x => x.BillingAddress, ba =>
            {
                ba.Property(p => p.FullName).HasColumnName("Billing_FullName").HasMaxLength(150).IsRequired();
                ba.Property(p => p.Phone).HasColumnName("Billing_Phone").HasMaxLength(25).IsRequired();
                ba.Property(p => p.AddressLine1).HasColumnName("Billing_AddressLine1").HasMaxLength(250).IsRequired();
                ba.Property(p => p.AddressLine2).HasColumnName("Billing_AddressLine2").HasMaxLength(250);
                ba.Property(p => p.City).HasColumnName("Billing_City").HasMaxLength(100).IsRequired();
                ba.Property(p => p.State).HasColumnName("Billing_State").HasMaxLength(100).IsRequired();
                ba.Property(p => p.PostalCode).HasColumnName("Billing_PostalCode").HasMaxLength(20).IsRequired();
                ba.Property(p => p.Country).HasColumnName("Billing_Country").HasMaxLength(100).IsRequired();
            });

            entity.HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // --------------------------------------------------------
        // OrderItem
        // --------------------------------------------------------
        builder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItems");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.OrderId)
                .IsRequired();

            entity.Property(x => x.ProductVariantId)
                .IsRequired();

            entity.Property(x => x.ProductName)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.VariantName)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Sku)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.ImageUrl)
                .HasMaxLength(2000);

            entity.Property(x => x.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(x => x.Mrp)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(x => x.Quantity)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => x.OrderId);
        });

        // --------------------------------------------------------
        // Wishlist
        // --------------------------------------------------------
        builder.Entity<Domain.Wishlist.Wishlist>(entity =>
        {
            entity.ToTable("Wishlists");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.UserId)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => x.UserId)
                .IsUnique();

            entity.HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey(x => x.WishlistId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // --------------------------------------------------------
        // WishlistItem
        // --------------------------------------------------------
        builder.Entity<Domain.Wishlist.WishlistItem>(entity =>
        {
            entity.ToTable("WishlistItems");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.WishlistId)
                .IsRequired();

            entity.Property(x => x.ProductVariantId)
                .IsRequired();

            entity.Property(x => x.ProductName)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.VariantName)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Sku)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.ImageUrl)
                .HasMaxLength(2000);

            entity.Property(x => x.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(x => x.Mrp)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            entity.HasIndex(x => new { x.WishlistId, x.ProductVariantId })
                .IsUnique();

            entity.HasOne<ProductVariant>()
                .WithMany()
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ================================================================
        // MEDIA MANAGEMENT MODULE
        // ================================================================

        builder.Entity<MediaAsset>(entity =>
        {
            entity.ToTable("MediaAssets");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.MediaType).HasConversion<int>().IsRequired();
            entity.Property(x => x.ObjectKey).HasMaxLength(1000).IsRequired();
            entity.Property(x => x.OriginalFileName).HasMaxLength(500).IsRequired();
            entity.Property(x => x.ContentType).HasMaxLength(100).IsRequired();
            entity.Property(x => x.FileExtension).HasMaxLength(10).IsRequired();
            entity.Property(x => x.FileSize).IsRequired();
            entity.Property(x => x.Width).IsRequired();
            entity.Property(x => x.Height).IsRequired();
            entity.Property(x => x.AltText).HasMaxLength(300).IsRequired();
            entity.Property(x => x.Caption).HasMaxLength(500);
            entity.Property(x => x.Title).HasMaxLength(200);
            entity.Property(x => x.Status).HasConversion<int>().IsRequired();
            entity.Property(x => x.UploadedBy).IsRequired();
            entity.Property(x => x.CreatedAtUtc).IsRequired();
            entity.HasIndex(x => x.MediaType);
            entity.HasIndex(x => x.Status);
            entity.HasIndex(x => x.CreatedAtUtc);
            entity.HasIndex(x => x.UploadedBy);
            entity.HasIndex(x => x.ObjectKey).IsUnique();
        });

        builder.Entity<MediaPermission>(entity =>
        {
            entity.ToTable("MediaPermissions");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.UserId).IsRequired();
            entity.Property(x => x.MediaType).HasConversion<int>().IsRequired();
            entity.Property(x => x.PermissionType).HasConversion<int>().IsRequired();
            entity.Property(x => x.IsGranted).IsRequired();
            entity.Property(x => x.GrantedBy).IsRequired();
            entity.Property(x => x.GrantedAt).IsRequired();
            entity.Property(x => x.CreatedAtUtc).IsRequired();
            entity.HasIndex(x => new { x.UserId, x.MediaType, x.PermissionType }).IsUnique();
            entity.HasIndex(x => x.UserId);
        });

        builder.Entity<MediaAuditLog>(entity =>
        {
            entity.ToTable("MediaAuditLogs");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.UserId).IsRequired();
            entity.Property(x => x.Action).HasMaxLength(100).IsRequired();
            entity.Property(x => x.ResourceType).HasMaxLength(100).IsRequired();
            entity.Property(x => x.OldValue).HasMaxLength(2000);
            entity.Property(x => x.NewValue).HasMaxLength(2000);
            entity.Property(x => x.Notes).HasMaxLength(500);
            entity.Property(x => x.OccurredAtUtc).IsRequired();
            entity.HasIndex(x => x.UserId);
            entity.HasIndex(x => x.MediaAssetId);
            entity.HasIndex(x => x.OccurredAtUtc);
            entity.HasIndex(x => x.ResourceType);
        });

        builder.Entity<Banner>(entity =>
        {
            entity.ToTable("Banners");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Subtitle).HasMaxLength(300);
            entity.Property(x => x.CtaText).HasMaxLength(100);
            entity.Property(x => x.CtaUrl).HasMaxLength(500);
            entity.Property(x => x.DisplayOrder).IsRequired();
            entity.Property(x => x.IsActive).IsRequired();
            entity.Property(x => x.IsPublished).IsRequired();
            entity.Property(x => x.CreatedBy).IsRequired();
            entity.Property(x => x.CreatedAtUtc).IsRequired();
            entity.HasIndex(x => x.DisplayOrder);
            entity.HasIndex(x => new { x.IsActive, x.IsPublished });
            entity.HasIndex(x => new { x.StartDateUtc, x.EndDateUtc });
            entity.HasOne<MediaAsset>()
                .WithMany()
                .HasForeignKey(x => x.MediaAssetId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        });

        builder.Entity<HomepageSection>(entity =>
        {
            entity.ToTable("HomepageSections");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.SectionKey).HasConversion<int>().IsRequired();
            entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Subtitle).HasMaxLength(300);
            entity.Property(x => x.LinkUrl).HasMaxLength(500);
            entity.Property(x => x.DisplayOrder).IsRequired();
            entity.Property(x => x.IsActive).IsRequired();
            entity.Property(x => x.IsPublished).IsRequired();
            entity.Property(x => x.CreatedBy).IsRequired();
            entity.Property(x => x.CreatedAtUtc).IsRequired();
            entity.HasIndex(x => x.SectionKey).IsUnique();
            entity.HasIndex(x => x.DisplayOrder);
            entity.HasOne<MediaAsset>()
                .WithMany()
                .HasForeignKey(x => x.MediaAssetId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        });

        builder.Entity<LandingPage>(entity =>
        {
            entity.ToTable("LandingPages");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Slug).HasMaxLength(220).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(2000);
            entity.Property(x => x.MetaTitle).HasMaxLength(200);
            entity.Property(x => x.MetaDescription).HasMaxLength(500);
            entity.Property(x => x.Status).HasConversion<int>().IsRequired();
            entity.Property(x => x.CreatedBy).IsRequired();
            entity.Property(x => x.CreatedAtUtc).IsRequired();
            entity.HasIndex(x => x.Slug).IsUnique();
            entity.HasIndex(x => x.Status);
            entity.HasOne<MediaAsset>()
                .WithMany()
                .HasForeignKey(x => x.HeroMediaAssetId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        });
    }
}