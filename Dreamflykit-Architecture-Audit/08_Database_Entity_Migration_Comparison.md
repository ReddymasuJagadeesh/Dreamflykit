# 08. DATABASE ENTITY & MIGRATION COMPARISON: DREAMFLYKIT

**Objective:** Detailed 5-way comparative audit across:
1. C# Domain Entities (`Dreamflykit.Domain`)
2. EF Core Entity Configurations (`DreamflykitDbContext.OnModelCreating`)
3. EF Core DbSets (`DreamflykitDbContext.cs`)
4. EF Core Migrations (`Dreamflykit.Infrastructure/Migrations`)
5. Actual PostgreSQL Database Schema (`dreamflykit_dev`)

---

## 1. Five-Way Comparison Matrix

| Domain Entity | DbSet in DbContext | ModelBuilder Configured | In Migrations (16 Total) | Exists in Live PostgreSQL | Status / Discrepancy |
| :--- | :--- | :--- | :--- | :--- | :--- |
| `ApplicationUser` | (IdentityDbContext) | Yes (lines 94–115) | Yes (`InitialIdentity`) | Yes (`AspNetUsers`) | **Matched** |
| `IdentityRole<Guid>`| (IdentityDbContext) | Yes | Yes (`InitialIdentity`) | Yes (`AspNetRoles`) | **Matched** |
| `Category` | `Categories` | Yes (lines 121–156) | Yes (`AddCatalogCategories`) | Yes (`Categories`) | **Matched** |
| `Subcategory` | `Subcategories` | Yes (lines 162–206) | Yes (`AddCatalogCategories`) | Yes (`Subcategories`) | **Matched** |
| `SellerCategoryAssignment` | `SellerCategoryAssignments` | Yes (lines 212–236) | Yes (`AddSellerCategoryAssignments`) | Yes (`SellerCategoryAssignments`) | **Matched** |
| `Product` | `Products` | Yes (lines 242–321) | Yes (`AddProductsAndVariants`) | Yes (`Products`) | **Matched** |
| `ProductVariant` | `ProductVariants` | Yes (lines 327–376) | Yes (`AddProductsAndVariants`) | Yes (`ProductVariants`) | **Drift Detected** (Check constraints in C# missing in PG) |
| `ProductMedia` | `ProductMedia` | Yes (lines 382–433) | Yes (`AddProductMedia`) | Yes (`ProductMedia`) | **Matched** |
| `ProductAttribute` | `ProductAttribute` | Yes (lines 440–467) | Yes (`AddProductAttributes`) | Yes (`ProductAttributes`) | **Matched** |
| `ProductAttributeValue`| `ProductAttributeValue` | Yes (lines 473–507) | Yes (`AddProductAttributes`) | Yes (`ProductAttributeValues`) | **Matched** |
| `Warehouse` | `Warehouses` | Yes (lines 513–558) | Yes (`AddInventoryManagement`) | Yes (`Warehouses`) | **Matched** |
| `InventoryItem` | `InventoryItems` | Yes (lines 564–600) | Yes (`AddInventoryManagement`) | Yes (`InventoryItems`) | **Drift Detected** (Check constraints in C# missing in PG) |
| `StockMovement` | `StockMovements` | Yes (lines 607–646) | Yes (`AddInventoryManagement`) | Yes (`StockMovements`) | **Matched** |
| `Kit` | `Kits` | Yes (lines 649–726) | Yes (`AddKitManagement`) | Yes (`Kits`) | **Matched** |
| `KitItem` | `KitItems` | Yes (lines 729–772) | Yes (`AddKitManagement`) | Yes (`KitItems`) | **Matched** |
| `Cart` | `Carts` | Yes (lines 778–798) | Yes (`AddCart`) | Yes (`Carts`) | **Matched** |
| `CartItem` | `CartItems` | Yes (lines 804–857) | Yes (`AddCart`) | Yes (`CartItems`) | **Drift Detected** (Check constraints in C# missing in PG) |
| `CustomerAddress` | `CustomerAddresses` | Yes (lines 862–910) | Yes (`AddAddressingAndOrders`) | Yes (`CustomerAddresses`) | **Matched** |
| `Order` | `Orders` | Yes (lines 913–975) | Yes (`AddAddressingAndOrders`) | Yes (`Orders`) | **Matched** |
| `OrderItem` | `OrderItems` | Yes (lines 978–1035) | Yes (`AddAddressingAndOrders`) | Yes (`OrderItems`) | **Matched** |
| `Wishlist` | `Wishlists` | Yes (lines 1040–1058) | Yes (`AddWishlist`) | Yes (`Wishlists`) | **Matched** |
| `WishlistItem` | `WishlistItems` | Yes (lines 1061–1105) | Yes (`AddWishlist`) | Yes (`WishlistItems`) | **Matched** |
| `MediaAsset` | `MediaAssets` | Yes (lines 1111–1145) | Yes (`AddMediaManagementModule`) | Yes (`MediaAssets`) | **Matched** |
| `MediaPermission` | `MediaPermissions` | Yes (lines 1147–1155) | Yes (`AddMediaManagementModule`) | Yes (`MediaPermissions`) | **Matched** |
| `MediaAuditLog` | `MediaAuditLogs` | Yes (lines 1157–1172) | Yes (`AddMediaManagementModule`) | Yes (`MediaAuditLogs`) | **Matched** |
| `Banner` | `Banners` | Yes (lines 1175–1200) | Yes (`AddMediaManagementModule`) | Yes (`Banners`) | **Matched** |
| `HomepageSection` | `HomepageSections` | Yes (lines 1203–1225) | Yes (`AddMediaManagementModule`) | Yes (`HomepageSections`) | **Matched** |
| `LandingPage` | `LandingPages` | Yes (lines 1228–1248) | Yes (`AddMediaManagementModule`) | Yes (`LandingPages`) | **Matched** |
| `SystemThemeSetting` | `SystemThemeSettings` | Yes (lines 1250) | Yes (`AddSystemThemeSettings`) | Yes (`SystemThemeSettings`) | **Matched** |
| **`AuditLog`** | **`AuditLogs` (line 87)** | **Yes (lines 1254–1278)** | **NO MIGRATION** | **MISSING FROM DB** | **CRITICAL MIGRATION DRIFT: Table does NOT exist in PostgreSQL!** |

---

## 2. Major Findings & Verification Evidence

### 1. `AuditLog` Table Completely Missing in PostgreSQL
- **Evidence in C# Code:**
  `DreamflykitDbContext.cs` line 87 declares:
  `public DbSet<AuditLog> AuditLogs => Set<AuditLog>();`
  Lines 1254–1278 configure table mapping:
  `builder.Entity<AuditLog>(entity => { entity.ToTable("AuditLogs"); ... });`
- **Evidence in Migrations:**
  Inspected all 16 migration files in `src/Dreamflykit.Infrastructure/Migrations/`. The word `AuditLogs` does **not** appear in any migration file.
- **Evidence in PostgreSQL:**
  `SELECT table_name FROM information_schema.tables WHERE table_schema='public' AND table_name='AuditLogs';` returned **0 rows**.
- **Impact:** Any invocation of enterprise audit logging (`IAuditLogService.LogAsync`) that tries to persist an `AuditLog` to `_dbContext.AuditLogs.AddAsync(log)` will crash with a fatal PostgreSQL runtime exception: `42P01: relation "AuditLogs" does not exist`.

---

### 2. Check Constraints Configured in C# But Absent in Database
- **Evidence in C# Code:**
  `DreamflykitDbContext.cs` lines 330–334:
  ```csharp
  t.HasCheckConstraint("CK_ProductVariants_SellingPrice_NonNegative", "\"SellingPrice\" >= 0");
  t.HasCheckConstraint("CK_ProductVariants_Mrp_NonNegative", "\"Mrp\" >= 0");
  t.HasCheckConstraint("CK_ProductVariants_SellingPrice_Lte_Mrp", "\"SellingPrice\" <= \"Mrp\"");
  ```
  Lines 568–569:
  ```csharp
  t.HasCheckConstraint("CK_InventoryItems_AvailableQuantity_NonNegative", "\"AvailableQuantity\" >= 0");
  t.HasCheckConstraint("CK_InventoryItems_ReservedQuantity_NonNegative", "\"ReservedQuantity\" >= 0");
  ```
- **Evidence in PostgreSQL:**
  Querying `pg_constraint` for `contype = 'c'` returned **0 rows**.
- **Root Cause:** These check constraints were typed into `DreamflykitDbContext.cs` directly, but `dotnet ef migrations add` was never executed to create the migration, and `dotnet ef database update` was never executed against the PostgreSQL database.
- **Impact:** The database provides **zero safety guarantees** against negative prices, negative inventory quantities, or SellingPrice > Mrp.
