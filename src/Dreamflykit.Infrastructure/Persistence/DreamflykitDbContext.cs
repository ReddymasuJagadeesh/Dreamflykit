using Dreamflykit.Domain.Catalog;
using Dreamflykit.Domain.Geography;
using Dreamflykit.Domain.Inventory;
using Dreamflykit.Domain.Kits;
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
    }
}