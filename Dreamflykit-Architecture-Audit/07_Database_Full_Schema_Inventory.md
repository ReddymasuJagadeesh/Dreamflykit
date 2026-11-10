# 07. POSTGRESQL DATABASE FULL SCHEMA INVENTORY: DREAMFLYKIT

**Database Engine:** PostgreSQL 18.6 (x86_64, Windows)
**Target Database:** `dreamflykit_dev`
**Total Tables Catalogued:** 37

---

## 1. Schema Summary Table

| Schema | Table Name | Verified Rows | Total Disk Size | Data Size | Index Size | Column Count |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `public` | `AspNetRoleClaims` | **0** | 24 kB | 0 bytes | 16 kB | 4 |
| `public` | `AspNetRoles` | **4** | 48 kB | 8192 bytes | 32 kB | 4 |
| `public` | `AspNetUserClaims` | **0** | 24 kB | 0 bytes | 16 kB | 4 |
| `public` | `AspNetUserLogins` | **0** | 24 kB | 0 bytes | 16 kB | 4 |
| `public` | `AspNetUserRoles` | **13** | 40 kB | 8192 bytes | 32 kB | 2 |
| `public` | `AspNetUserTokens` | **0** | 16 kB | 0 bytes | 8192 bytes | 4 |
| `public` | `AspNetUsers` | **10** | 64 kB | 8192 bytes | 48 kB | 22 |
| `public` | `Banners` | **2** | 96 kB | 8192 bytes | 80 kB | 15 |
| `public` | `CartItems` | **3** | 64 kB | 8192 bytes | 48 kB | 12 |
| `public` | `Carts` | **5** | 40 kB | 8192 bytes | 32 kB | 4 |
| `public` | `Categories` | **15** | 104 kB | 8192 bytes | 64 kB | 8 |
| `public` | `Countries` | **0** | 16 kB | 0 bytes | 8192 bytes | 7 |
| `public` | `CustomerAddresses` | **3** | 48 kB | 8192 bytes | 32 kB | 14 |
| `public` | `HomepageSections` | **9** | 80 kB | 8192 bytes | 64 kB | 15 |
| `public` | `InventoryItems` | **1** | 56 kB | 8192 bytes | 48 kB | 7 |
| `public` | `KitItems` | **0** | 32 kB | 0 bytes | 32 kB | 7 |
| `public` | `Kits` | **0** | 56 kB | 0 bytes | 48 kB | 14 |
| `public` | `LandingPages` | **0** | 40 kB | 0 bytes | 32 kB | 14 |
| `public` | `MediaAssets` | **41** | 144 kB | 16 kB | 96 kB | 17 |
| `public` | `MediaAuditLogs` | **234** | 152 kB | 40 kB | 80 kB | 10 |
| `public` | `MediaPermissions` | **90** | 88 kB | 16 kB | 48 kB | 9 |
| `public` | `OrderItems` | **2** | 48 kB | 8192 bytes | 32 kB | 12 |
| `public` | `Orders` | **2** | 64 kB | 8192 bytes | 48 kB | 30 |
| `public` | `ProductAttributeValues` | **0** | 32 kB | 0 bytes | 24 kB | 6 |
| `public` | `ProductAttributes` | **0** | 24 kB | 0 bytes | 24 kB | 7 |
| `public` | `ProductMedia` | **108** | 120 kB | 24 kB | 64 kB | 10 |
| `public` | `ProductVariants` | **107** | 88 kB | 16 kB | 48 kB | 10 |
| `public` | `Products` | **107** | 192 kB | 48 kB | 112 kB | 18 |
| `public` | `Seasons` | **0** | 16 kB | 0 bytes | 8192 bytes | 9 |
| `public` | `SellerCategoryAssignments` | **0** | 32 kB | 0 bytes | 32 kB | 4 |
| `public` | `StockMovements` | **1** | 64 kB | 8192 bytes | 48 kB | 9 |
| `public` | `Subcategories` | **61** | 104 kB | 16 kB | 64 kB | 9 |
| `public` | `SystemThemeSettings` | **1** | 24 kB | 8192 bytes | 16 kB | 8 |
| `public` | `Warehouses` | **1** | 48 kB | 8192 bytes | 32 kB | 12 |
| `public` | `WishlistItems` | **0** | 32 kB | 0 bytes | 24 kB | 11 |
| `public` | `Wishlists` | **0** | 16 kB | 0 bytes | 16 kB | 4 |
| `public` | `__EFMigrationsHistory` | **16** | 24 kB | 8192 bytes | 16 kB | 2 |

---

## 2. Complete Exhaustive Table-by-Table Column Inventory

### Table: `public."AspNetRoleClaims"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `24 kB` (Data: `0 bytes`, Indexes: `16 kB`)
- **Foreign Keys:** 1
  - `RoleId -> AspNetRoles.Id (ON DELETE CASCADE)`
- **Indexes:** 2
  - `IX_AspNetRoleClaims_RoleId`: `CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON public."AspNetRoleClaims" USING btree ("RoleId")`
  - `PK_AspNetRoleClaims`: `CREATE UNIQUE INDEX "PK_AspNetRoleClaims" ON public."AspNetRoleClaims" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `integer` | - | 32 | 0 | `NO` | - |
| 2 | **`RoleId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`ClaimType`** | `text` | - | - | - | `YES` | - |
| 4 | **`ClaimValue`** | `text` | - | - | - | `YES` | - |

---

### Table: `public."AspNetRoles"`

- **Row Count (Live Count):** `4`
- **Total Storage Size:** `48 kB` (Data: `8192 bytes`, Indexes: `32 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 2
  - `PK_AspNetRoles`: `CREATE UNIQUE INDEX "PK_AspNetRoles" ON public."AspNetRoles" USING btree ("Id")`
  - `RoleNameIndex`: `CREATE UNIQUE INDEX "RoleNameIndex" ON public."AspNetRoles" USING btree ("NormalizedName")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`Name`** | `character varying` | 256 | - | - | `YES` | - |
| 3 | **`NormalizedName`** | `character varying` | 256 | - | - | `YES` | - |
| 4 | **`ConcurrencyStamp`** | `text` | - | - | - | `YES` | - |

---

### Table: `public."AspNetUserClaims"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `24 kB` (Data: `0 bytes`, Indexes: `16 kB`)
- **Foreign Keys:** 1
  - `UserId -> AspNetUsers.Id (ON DELETE CASCADE)`
- **Indexes:** 2
  - `IX_AspNetUserClaims_UserId`: `CREATE INDEX "IX_AspNetUserClaims_UserId" ON public."AspNetUserClaims" USING btree ("UserId")`
  - `PK_AspNetUserClaims`: `CREATE UNIQUE INDEX "PK_AspNetUserClaims" ON public."AspNetUserClaims" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `integer` | - | 32 | 0 | `NO` | - |
| 2 | **`UserId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`ClaimType`** | `text` | - | - | - | `YES` | - |
| 4 | **`ClaimValue`** | `text` | - | - | - | `YES` | - |

---

### Table: `public."AspNetUserLogins"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `24 kB` (Data: `0 bytes`, Indexes: `16 kB`)
- **Foreign Keys:** 1
  - `UserId -> AspNetUsers.Id (ON DELETE CASCADE)`
- **Indexes:** 2
  - `IX_AspNetUserLogins_UserId`: `CREATE INDEX "IX_AspNetUserLogins_UserId" ON public."AspNetUserLogins" USING btree ("UserId")`
  - `PK_AspNetUserLogins`: `CREATE UNIQUE INDEX "PK_AspNetUserLogins" ON public."AspNetUserLogins" USING btree ("LoginProvider", "ProviderKey")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`LoginProvider`** | `text` | - | - | - | `NO` | - |
| 2 | **`ProviderKey`** | `text` | - | - | - | `NO` | - |
| 3 | **`ProviderDisplayName`** | `text` | - | - | - | `YES` | - |
| 4 | **`UserId`** | `uuid` | - | - | - | `NO` | - |

---

### Table: `public."AspNetUserRoles"`

- **Row Count (Live Count):** `13`
- **Total Storage Size:** `40 kB` (Data: `8192 bytes`, Indexes: `32 kB`)
- **Foreign Keys:** 2
  - `RoleId -> AspNetRoles.Id (ON DELETE CASCADE)`
  - `UserId -> AspNetUsers.Id (ON DELETE CASCADE)`
- **Indexes:** 2
  - `IX_AspNetUserRoles_RoleId`: `CREATE INDEX "IX_AspNetUserRoles_RoleId" ON public."AspNetUserRoles" USING btree ("RoleId")`
  - `PK_AspNetUserRoles`: `CREATE UNIQUE INDEX "PK_AspNetUserRoles" ON public."AspNetUserRoles" USING btree ("UserId", "RoleId")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`UserId`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`RoleId`** | `uuid` | - | - | - | `NO` | - |

---

### Table: `public."AspNetUserTokens"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `16 kB` (Data: `0 bytes`, Indexes: `8192 bytes`)
- **Foreign Keys:** 1
  - `UserId -> AspNetUsers.Id (ON DELETE CASCADE)`
- **Indexes:** 1
  - `PK_AspNetUserTokens`: `CREATE UNIQUE INDEX "PK_AspNetUserTokens" ON public."AspNetUserTokens" USING btree ("UserId", "LoginProvider", "Name")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`UserId`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`LoginProvider`** | `text` | - | - | - | `NO` | - |
| 3 | **`Name`** | `text` | - | - | - | `NO` | - |
| 4 | **`Value`** | `text` | - | - | - | `YES` | - |

---

### Table: `public."AspNetUsers"`

- **Row Count (Live Count):** `10`
- **Total Storage Size:** `64 kB` (Data: `8192 bytes`, Indexes: `48 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 3
  - `EmailIndex`: `CREATE INDEX "EmailIndex" ON public."AspNetUsers" USING btree ("NormalizedEmail")`
  - `PK_AspNetUsers`: `CREATE UNIQUE INDEX "PK_AspNetUsers" ON public."AspNetUsers" USING btree ("Id")`
  - `UserNameIndex`: `CREATE UNIQUE INDEX "UserNameIndex" ON public."AspNetUsers" USING btree ("NormalizedUserName")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`FirstName`** | `character varying` | 100 | - | - | `NO` | - |
| 3 | **`LastName`** | `character varying` | 100 | - | - | `NO` | - |
| 4 | **`DateOfBirth`** | `date` | - | - | - | `YES` | - |
| 5 | **`Gender`** | `character varying` | 50 | - | - | `YES` | - |
| 6 | **`Country`** | `character varying` | 100 | - | - | `YES` | - |
| 7 | **`ProfileImageUrl`** | `character varying` | 1000 | - | - | `YES` | - |
| 8 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 9 | **`UserName`** | `character varying` | 256 | - | - | `YES` | - |
| 10 | **`NormalizedUserName`** | `character varying` | 256 | - | - | `YES` | - |
| 11 | **`Email`** | `character varying` | 256 | - | - | `YES` | - |
| 12 | **`NormalizedEmail`** | `character varying` | 256 | - | - | `YES` | - |
| 13 | **`EmailConfirmed`** | `boolean` | - | - | - | `NO` | - |
| 14 | **`PasswordHash`** | `text` | - | - | - | `YES` | - |
| 15 | **`SecurityStamp`** | `text` | - | - | - | `YES` | - |
| 16 | **`ConcurrencyStamp`** | `text` | - | - | - | `YES` | - |
| 17 | **`PhoneNumber`** | `text` | - | - | - | `YES` | - |
| 18 | **`PhoneNumberConfirmed`** | `boolean` | - | - | - | `NO` | - |
| 19 | **`TwoFactorEnabled`** | `boolean` | - | - | - | `NO` | - |
| 20 | **`LockoutEnd`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 21 | **`LockoutEnabled`** | `boolean` | - | - | - | `NO` | - |
| 22 | **`AccessFailedCount`** | `integer` | - | 32 | 0 | `NO` | - |

---

### Table: `public."Banners"`

- **Row Count (Live Count):** `2`
- **Total Storage Size:** `96 kB` (Data: `8192 bytes`, Indexes: `80 kB`)
- **Foreign Keys:** 1
  - `MediaAssetId -> MediaAssets.Id (ON DELETE SET NULL)`
- **Indexes:** 5
  - `IX_Banners_DisplayOrder`: `CREATE INDEX "IX_Banners_DisplayOrder" ON public."Banners" USING btree ("DisplayOrder")`
  - `IX_Banners_IsActive_IsPublished`: `CREATE INDEX "IX_Banners_IsActive_IsPublished" ON public."Banners" USING btree ("IsActive", "IsPublished")`
  - `IX_Banners_MediaAssetId`: `CREATE INDEX "IX_Banners_MediaAssetId" ON public."Banners" USING btree ("MediaAssetId")`
  - `IX_Banners_StartDateUtc_EndDateUtc`: `CREATE INDEX "IX_Banners_StartDateUtc_EndDateUtc" ON public."Banners" USING btree ("StartDateUtc", "EndDateUtc")`
  - `PK_Banners`: `CREATE UNIQUE INDEX "PK_Banners" ON public."Banners" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`MediaAssetId`** | `uuid` | - | - | - | `YES` | - |
| 3 | **`Title`** | `character varying` | 200 | - | - | `NO` | - |
| 4 | **`Subtitle`** | `character varying` | 300 | - | - | `YES` | - |
| 5 | **`CtaText`** | `character varying` | 100 | - | - | `YES` | - |
| 6 | **`CtaUrl`** | `character varying` | 500 | - | - | `YES` | - |
| 7 | **`DisplayOrder`** | `integer` | - | 32 | 0 | `NO` | - |
| 8 | **`StartDateUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 9 | **`EndDateUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 10 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 11 | **`IsPublished`** | `boolean` | - | - | - | `NO` | - |
| 12 | **`CreatedBy`** | `uuid` | - | - | - | `NO` | - |
| 13 | **`UpdatedBy`** | `uuid` | - | - | - | `YES` | - |
| 14 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 15 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."CartItems"`

- **Row Count (Live Count):** `3`
- **Total Storage Size:** `64 kB` (Data: `8192 bytes`, Indexes: `48 kB`)
- **Foreign Keys:** 2
  - `CartId -> Carts.Id (ON DELETE CASCADE)`
  - `ProductVariantId -> ProductVariants.Id (ON DELETE RESTRICT)`
- **Indexes:** 3
  - `IX_CartItems_CartId_ProductVariantId`: `CREATE UNIQUE INDEX "IX_CartItems_CartId_ProductVariantId" ON public."CartItems" USING btree ("CartId", "ProductVariantId")`
  - `IX_CartItems_ProductVariantId`: `CREATE INDEX "IX_CartItems_ProductVariantId" ON public."CartItems" USING btree ("ProductVariantId")`
  - `PK_CartItems`: `CREATE UNIQUE INDEX "PK_CartItems" ON public."CartItems" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`CartId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`ProductVariantId`** | `uuid` | - | - | - | `NO` | - |
| 4 | **`ProductName`** | `character varying` | 200 | - | - | `NO` | - |
| 5 | **`VariantName`** | `character varying` | 150 | - | - | `NO` | - |
| 6 | **`Sku`** | `character varying` | 100 | - | - | `NO` | - |
| 7 | **`ImageUrl`** | `character varying` | 2000 | - | - | `YES` | - |
| 8 | **`UnitPrice`** | `numeric` | - | 18 | 2 | `NO` | - |
| 9 | **`Mrp`** | `numeric` | - | 18 | 2 | `NO` | - |
| 10 | **`Quantity`** | `integer` | - | 32 | 0 | `NO` | - |
| 11 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 12 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."Carts"`

- **Row Count (Live Count):** `5`
- **Total Storage Size:** `40 kB` (Data: `8192 bytes`, Indexes: `32 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 2
  - `IX_Carts_UserId`: `CREATE UNIQUE INDEX "IX_Carts_UserId" ON public."Carts" USING btree ("UserId")`
  - `PK_Carts`: `CREATE UNIQUE INDEX "PK_Carts" ON public."Carts" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`UserId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 4 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."Categories"`

- **Row Count (Live Count):** `15`
- **Total Storage Size:** `104 kB` (Data: `8192 bytes`, Indexes: `64 kB`)
- **Foreign Keys:** 1
  - `ImageMediaAssetId -> MediaAssets.Id (ON DELETE SET NULL)`
- **Indexes:** 4
  - `IX_Categories_DisplayOrder`: `CREATE INDEX "IX_Categories_DisplayOrder" ON public."Categories" USING btree ("DisplayOrder")`
  - `IX_Categories_ImageMediaAssetId`: `CREATE INDEX "IX_Categories_ImageMediaAssetId" ON public."Categories" USING btree ("ImageMediaAssetId")`
  - `IX_Categories_Slug`: `CREATE UNIQUE INDEX "IX_Categories_Slug" ON public."Categories" USING btree ("Slug")`
  - `PK_Categories`: `CREATE UNIQUE INDEX "PK_Categories" ON public."Categories" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`Name`** | `character varying` | 150 | - | - | `NO` | - |
| 3 | **`Slug`** | `character varying` | 180 | - | - | `NO` | - |
| 4 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 5 | **`DisplayOrder`** | `integer` | - | 32 | 0 | `NO` | - |
| 6 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 7 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 8 | **`ImageMediaAssetId`** | `uuid` | - | - | - | `YES` | - |

---

### Table: `public."Countries"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `16 kB` (Data: `0 bytes`, Indexes: `8192 bytes`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 1
  - `PK_Countries`: `CREATE UNIQUE INDEX "PK_Countries" ON public."Countries" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`Name`** | `text` | - | - | - | `NO` | - |
| 3 | **`Code`** | `text` | - | - | - | `NO` | - |
| 4 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 5 | **`DisplayOrder`** | `integer` | - | 32 | 0 | `NO` | - |
| 6 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 7 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."CustomerAddresses"`

- **Row Count (Live Count):** `3`
- **Total Storage Size:** `48 kB` (Data: `8192 bytes`, Indexes: `32 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 2
  - `IX_CustomerAddresses_UserId_AddressType`: `CREATE INDEX "IX_CustomerAddresses_UserId_AddressType" ON public."CustomerAddresses" USING btree ("UserId", "AddressType")`
  - `PK_CustomerAddresses`: `CREATE UNIQUE INDEX "PK_CustomerAddresses" ON public."CustomerAddresses" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`UserId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`FullName`** | `character varying` | 150 | - | - | `NO` | - |
| 4 | **`Phone`** | `character varying` | 25 | - | - | `NO` | - |
| 5 | **`AddressLine1`** | `character varying` | 250 | - | - | `NO` | - |
| 6 | **`AddressLine2`** | `character varying` | 250 | - | - | `YES` | - |
| 7 | **`City`** | `character varying` | 100 | - | - | `NO` | - |
| 8 | **`State`** | `character varying` | 100 | - | - | `NO` | - |
| 9 | **`PostalCode`** | `character varying` | 20 | - | - | `NO` | - |
| 10 | **`Country`** | `character varying` | 100 | - | - | `NO` | - |
| 11 | **`AddressType`** | `integer` | - | 32 | 0 | `NO` | - |
| 12 | **`IsDefault`** | `boolean` | - | - | - | `NO` | - |
| 13 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 14 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."HomepageSections"`

- **Row Count (Live Count):** `9`
- **Total Storage Size:** `80 kB` (Data: `8192 bytes`, Indexes: `64 kB`)
- **Foreign Keys:** 1
  - `MediaAssetId -> MediaAssets.Id (ON DELETE SET NULL)`
- **Indexes:** 4
  - `IX_HomepageSections_DisplayOrder`: `CREATE INDEX "IX_HomepageSections_DisplayOrder" ON public."HomepageSections" USING btree ("DisplayOrder")`
  - `IX_HomepageSections_MediaAssetId`: `CREATE INDEX "IX_HomepageSections_MediaAssetId" ON public."HomepageSections" USING btree ("MediaAssetId")`
  - `IX_HomepageSections_SectionKey`: `CREATE UNIQUE INDEX "IX_HomepageSections_SectionKey" ON public."HomepageSections" USING btree ("SectionKey")`
  - `PK_HomepageSections`: `CREATE UNIQUE INDEX "PK_HomepageSections" ON public."HomepageSections" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`SectionKey`** | `integer` | - | 32 | 0 | `NO` | - |
| 3 | **`MediaAssetId`** | `uuid` | - | - | - | `YES` | - |
| 4 | **`Title`** | `character varying` | 200 | - | - | `NO` | - |
| 5 | **`Subtitle`** | `character varying` | 300 | - | - | `YES` | - |
| 6 | **`LinkUrl`** | `character varying` | 500 | - | - | `YES` | - |
| 7 | **`DisplayOrder`** | `integer` | - | 32 | 0 | `NO` | - |
| 8 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 9 | **`IsPublished`** | `boolean` | - | - | - | `NO` | - |
| 10 | **`StartDateUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 11 | **`EndDateUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 12 | **`CreatedBy`** | `uuid` | - | - | - | `NO` | - |
| 13 | **`UpdatedBy`** | `uuid` | - | - | - | `YES` | - |
| 14 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 15 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."InventoryItems"`

- **Row Count (Live Count):** `1`
- **Total Storage Size:** `56 kB` (Data: `8192 bytes`, Indexes: `48 kB`)
- **Foreign Keys:** 2
  - `ProductVariantId -> ProductVariants.Id (ON DELETE RESTRICT)`
  - `WarehouseId -> Warehouses.Id (ON DELETE RESTRICT)`
- **Indexes:** 3
  - `IX_InventoryItems_ProductVariantId`: `CREATE INDEX "IX_InventoryItems_ProductVariantId" ON public."InventoryItems" USING btree ("ProductVariantId")`
  - `IX_InventoryItems_WarehouseId_ProductVariantId`: `CREATE UNIQUE INDEX "IX_InventoryItems_WarehouseId_ProductVariantId" ON public."InventoryItems" USING btree ("WarehouseId", "ProductVariantId")`
  - `PK_InventoryItems`: `CREATE UNIQUE INDEX "PK_InventoryItems" ON public."InventoryItems" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`WarehouseId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`ProductVariantId`** | `uuid` | - | - | - | `NO` | - |
| 4 | **`AvailableQuantity`** | `integer` | - | 32 | 0 | `NO` | - |
| 5 | **`ReservedQuantity`** | `integer` | - | 32 | 0 | `NO` | - |
| 6 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 7 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."KitItems"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `32 kB` (Data: `0 bytes`, Indexes: `32 kB`)
- **Foreign Keys:** 2
  - `KitId -> Kits.Id (ON DELETE CASCADE)`
  - `ProductVariantId -> ProductVariants.Id (ON DELETE RESTRICT)`
- **Indexes:** 4
  - `IX_KitItems_KitId_DisplayOrder`: `CREATE INDEX "IX_KitItems_KitId_DisplayOrder" ON public."KitItems" USING btree ("KitId", "DisplayOrder")`
  - `IX_KitItems_KitId_ProductVariantId`: `CREATE UNIQUE INDEX "IX_KitItems_KitId_ProductVariantId" ON public."KitItems" USING btree ("KitId", "ProductVariantId")`
  - `IX_KitItems_ProductVariantId`: `CREATE INDEX "IX_KitItems_ProductVariantId" ON public."KitItems" USING btree ("ProductVariantId")`
  - `PK_KitItems`: `CREATE UNIQUE INDEX "PK_KitItems" ON public."KitItems" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`KitId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`ProductVariantId`** | `uuid` | - | - | - | `NO` | - |
| 4 | **`Quantity`** | `integer` | - | 32 | 0 | `NO` | - |
| 5 | **`DisplayOrder`** | `integer` | - | 32 | 0 | `NO` | - |
| 6 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 7 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."Kits"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `56 kB` (Data: `0 bytes`, Indexes: `48 kB`)
- **Foreign Keys:** 2
  - `CountryId -> Countries.Id (ON DELETE RESTRICT)`
  - `SeasonId -> Seasons.Id (ON DELETE RESTRICT)`
- **Indexes:** 6
  - `IX_Kits_CountryId_SeasonId_IsActive`: `CREATE INDEX "IX_Kits_CountryId_SeasonId_IsActive" ON public."Kits" USING btree ("CountryId", "SeasonId", "IsActive")`
  - `IX_Kits_CountryId_SeasonId_Size`: `CREATE UNIQUE INDEX "IX_Kits_CountryId_SeasonId_Size" ON public."Kits" USING btree ("CountryId", "SeasonId", "Size")`
  - `IX_Kits_DisplayOrder`: `CREATE INDEX "IX_Kits_DisplayOrder" ON public."Kits" USING btree ("DisplayOrder")`
  - `IX_Kits_SeasonId`: `CREATE INDEX "IX_Kits_SeasonId" ON public."Kits" USING btree ("SeasonId")`
  - `IX_Kits_Slug`: `CREATE UNIQUE INDEX "IX_Kits_Slug" ON public."Kits" USING btree ("Slug")`
  - `PK_Kits`: `CREATE UNIQUE INDEX "PK_Kits" ON public."Kits" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`CountryId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`SeasonId`** | `uuid` | - | - | - | `NO` | - |
| 4 | **`Name`** | `character varying` | 200 | - | - | `NO` | - |
| 5 | **`Slug`** | `character varying` | 220 | - | - | `NO` | - |
| 6 | **`Description`** | `character varying` | 5000 | - | - | `NO` | - |
| 7 | **`Size`** | `integer` | - | 32 | 0 | `NO` | - |
| 8 | **`Mrp`** | `numeric` | - | 18 | 2 | `NO` | - |
| 9 | **`SellingPrice`** | `numeric` | - | 18 | 2 | `NO` | - |
| 10 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 11 | **`IsFeatured`** | `boolean` | - | - | - | `NO` | - |
| 12 | **`DisplayOrder`** | `integer` | - | 32 | 0 | `NO` | - |
| 13 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 14 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."LandingPages"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `40 kB` (Data: `0 bytes`, Indexes: `32 kB`)
- **Foreign Keys:** 1
  - `HeroMediaAssetId -> MediaAssets.Id (ON DELETE SET NULL)`
- **Indexes:** 4
  - `IX_LandingPages_HeroMediaAssetId`: `CREATE INDEX "IX_LandingPages_HeroMediaAssetId" ON public."LandingPages" USING btree ("HeroMediaAssetId")`
  - `IX_LandingPages_Slug`: `CREATE UNIQUE INDEX "IX_LandingPages_Slug" ON public."LandingPages" USING btree ("Slug")`
  - `IX_LandingPages_Status`: `CREATE INDEX "IX_LandingPages_Status" ON public."LandingPages" USING btree ("Status")`
  - `PK_LandingPages`: `CREATE UNIQUE INDEX "PK_LandingPages" ON public."LandingPages" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`Title`** | `character varying` | 200 | - | - | `NO` | - |
| 3 | **`Slug`** | `character varying` | 220 | - | - | `NO` | - |
| 4 | **`Description`** | `character varying` | 2000 | - | - | `YES` | - |
| 5 | **`HeroMediaAssetId`** | `uuid` | - | - | - | `YES` | - |
| 6 | **`MetaTitle`** | `character varying` | 200 | - | - | `YES` | - |
| 7 | **`MetaDescription`** | `character varying` | 500 | - | - | `YES` | - |
| 8 | **`Status`** | `integer` | - | 32 | 0 | `NO` | - |
| 9 | **`StartDateUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 10 | **`EndDateUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 11 | **`CreatedBy`** | `uuid` | - | - | - | `NO` | - |
| 12 | **`UpdatedBy`** | `uuid` | - | - | - | `YES` | - |
| 13 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 14 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."MediaAssets"`

- **Row Count (Live Count):** `41`
- **Total Storage Size:** `144 kB` (Data: `16 kB`, Indexes: `96 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 6
  - `IX_MediaAssets_CreatedAtUtc`: `CREATE INDEX "IX_MediaAssets_CreatedAtUtc" ON public."MediaAssets" USING btree ("CreatedAtUtc")`
  - `IX_MediaAssets_MediaType`: `CREATE INDEX "IX_MediaAssets_MediaType" ON public."MediaAssets" USING btree ("MediaType")`
  - `IX_MediaAssets_ObjectKey`: `CREATE UNIQUE INDEX "IX_MediaAssets_ObjectKey" ON public."MediaAssets" USING btree ("ObjectKey")`
  - `IX_MediaAssets_Status`: `CREATE INDEX "IX_MediaAssets_Status" ON public."MediaAssets" USING btree ("Status")`
  - `IX_MediaAssets_UploadedBy`: `CREATE INDEX "IX_MediaAssets_UploadedBy" ON public."MediaAssets" USING btree ("UploadedBy")`
  - `PK_MediaAssets`: `CREATE UNIQUE INDEX "PK_MediaAssets" ON public."MediaAssets" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`MediaType`** | `integer` | - | 32 | 0 | `NO` | - |
| 3 | **`ObjectKey`** | `character varying` | 1000 | - | - | `NO` | - |
| 4 | **`OriginalFileName`** | `character varying` | 500 | - | - | `NO` | - |
| 5 | **`ContentType`** | `character varying` | 100 | - | - | `NO` | - |
| 6 | **`FileExtension`** | `character varying` | 10 | - | - | `NO` | - |
| 7 | **`FileSize`** | `bigint` | - | 64 | 0 | `NO` | - |
| 8 | **`Width`** | `integer` | - | 32 | 0 | `NO` | - |
| 9 | **`Height`** | `integer` | - | 32 | 0 | `NO` | - |
| 10 | **`AltText`** | `character varying` | 300 | - | - | `NO` | - |
| 11 | **`Caption`** | `character varying` | 500 | - | - | `YES` | - |
| 12 | **`Title`** | `character varying` | 200 | - | - | `YES` | - |
| 13 | **`Status`** | `integer` | - | 32 | 0 | `NO` | - |
| 14 | **`UploadedBy`** | `uuid` | - | - | - | `NO` | - |
| 15 | **`UpdatedBy`** | `uuid` | - | - | - | `YES` | - |
| 16 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 17 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."MediaAuditLogs"`

- **Row Count (Live Count):** `234`
- **Total Storage Size:** `152 kB` (Data: `40 kB`, Indexes: `80 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 5
  - `IX_MediaAuditLogs_MediaAssetId`: `CREATE INDEX "IX_MediaAuditLogs_MediaAssetId" ON public."MediaAuditLogs" USING btree ("MediaAssetId")`
  - `IX_MediaAuditLogs_OccurredAtUtc`: `CREATE INDEX "IX_MediaAuditLogs_OccurredAtUtc" ON public."MediaAuditLogs" USING btree ("OccurredAtUtc")`
  - `IX_MediaAuditLogs_ResourceType`: `CREATE INDEX "IX_MediaAuditLogs_ResourceType" ON public."MediaAuditLogs" USING btree ("ResourceType")`
  - `IX_MediaAuditLogs_UserId`: `CREATE INDEX "IX_MediaAuditLogs_UserId" ON public."MediaAuditLogs" USING btree ("UserId")`
  - `PK_MediaAuditLogs`: `CREATE UNIQUE INDEX "PK_MediaAuditLogs" ON public."MediaAuditLogs" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`UserId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`Action`** | `character varying` | 100 | - | - | `NO` | - |
| 4 | **`MediaAssetId`** | `uuid` | - | - | - | `YES` | - |
| 5 | **`ResourceType`** | `character varying` | 100 | - | - | `NO` | - |
| 6 | **`ResourceId`** | `uuid` | - | - | - | `YES` | - |
| 7 | **`OldValue`** | `character varying` | 2000 | - | - | `YES` | - |
| 8 | **`NewValue`** | `character varying` | 2000 | - | - | `YES` | - |
| 9 | **`Notes`** | `character varying` | 500 | - | - | `YES` | - |
| 10 | **`OccurredAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |

---

### Table: `public."MediaPermissions"`

- **Row Count (Live Count):** `90`
- **Total Storage Size:** `88 kB` (Data: `16 kB`, Indexes: `48 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 3
  - `IX_MediaPermissions_UserId`: `CREATE INDEX "IX_MediaPermissions_UserId" ON public."MediaPermissions" USING btree ("UserId")`
  - `IX_MediaPermissions_UserId_MediaType_PermissionType`: `CREATE UNIQUE INDEX "IX_MediaPermissions_UserId_MediaType_PermissionType" ON public."MediaPermissions" USING btree ("UserId", "MediaType", "PermissionType")`
  - `PK_MediaPermissions`: `CREATE UNIQUE INDEX "PK_MediaPermissions" ON public."MediaPermissions" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`UserId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`MediaType`** | `integer` | - | 32 | 0 | `NO` | - |
| 4 | **`PermissionType`** | `integer` | - | 32 | 0 | `NO` | - |
| 5 | **`IsGranted`** | `boolean` | - | - | - | `NO` | - |
| 6 | **`GrantedBy`** | `uuid` | - | - | - | `NO` | - |
| 7 | **`GrantedAt`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 8 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 9 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."OrderItems"`

- **Row Count (Live Count):** `2`
- **Total Storage Size:** `48 kB` (Data: `8192 bytes`, Indexes: `32 kB`)
- **Foreign Keys:** 1
  - `OrderId -> Orders.Id (ON DELETE CASCADE)`
- **Indexes:** 2
  - `IX_OrderItems_OrderId`: `CREATE INDEX "IX_OrderItems_OrderId" ON public."OrderItems" USING btree ("OrderId")`
  - `PK_OrderItems`: `CREATE UNIQUE INDEX "PK_OrderItems" ON public."OrderItems" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`OrderId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`ProductVariantId`** | `uuid` | - | - | - | `NO` | - |
| 4 | **`ProductName`** | `character varying` | 200 | - | - | `NO` | - |
| 5 | **`VariantName`** | `character varying` | 150 | - | - | `NO` | - |
| 6 | **`Sku`** | `character varying` | 100 | - | - | `NO` | - |
| 7 | **`ImageUrl`** | `character varying` | 2000 | - | - | `YES` | - |
| 8 | **`UnitPrice`** | `numeric` | - | 18 | 2 | `NO` | - |
| 9 | **`Mrp`** | `numeric` | - | 18 | 2 | `NO` | - |
| 10 | **`Quantity`** | `integer` | - | 32 | 0 | `NO` | - |
| 11 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 12 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."Orders"`

- **Row Count (Live Count):** `2`
- **Total Storage Size:** `64 kB` (Data: `8192 bytes`, Indexes: `48 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 3
  - `IX_Orders_OrderNumber`: `CREATE UNIQUE INDEX "IX_Orders_OrderNumber" ON public."Orders" USING btree ("OrderNumber")`
  - `IX_Orders_UserId_PlacedAtUtc`: `CREATE INDEX "IX_Orders_UserId_PlacedAtUtc" ON public."Orders" USING btree ("UserId", "PlacedAtUtc")`
  - `PK_Orders`: `CREATE UNIQUE INDEX "PK_Orders" ON public."Orders" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`UserId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`OrderNumber`** | `character varying` | 50 | - | - | `NO` | - |
| 4 | **`Status`** | `integer` | - | 32 | 0 | `NO` | - |
| 5 | **`Shipping_FullName`** | `character varying` | 150 | - | - | `NO` | - |
| 6 | **`Shipping_Phone`** | `character varying` | 25 | - | - | `NO` | - |
| 7 | **`Shipping_AddressLine1`** | `character varying` | 250 | - | - | `NO` | - |
| 8 | **`Shipping_AddressLine2`** | `character varying` | 250 | - | - | `YES` | - |
| 9 | **`Shipping_City`** | `character varying` | 100 | - | - | `NO` | - |
| 10 | **`Shipping_State`** | `character varying` | 100 | - | - | `NO` | - |
| 11 | **`Shipping_PostalCode`** | `character varying` | 20 | - | - | `NO` | - |
| 12 | **`Shipping_Country`** | `character varying` | 100 | - | - | `NO` | - |
| 13 | **`Billing_FullName`** | `character varying` | 150 | - | - | `NO` | - |
| 14 | **`Billing_Phone`** | `character varying` | 25 | - | - | `NO` | - |
| 15 | **`Billing_AddressLine1`** | `character varying` | 250 | - | - | `NO` | - |
| 16 | **`Billing_AddressLine2`** | `character varying` | 250 | - | - | `YES` | - |
| 17 | **`Billing_City`** | `character varying` | 100 | - | - | `NO` | - |
| 18 | **`Billing_State`** | `character varying` | 100 | - | - | `NO` | - |
| 19 | **`Billing_PostalCode`** | `character varying` | 20 | - | - | `NO` | - |
| 20 | **`Billing_Country`** | `character varying` | 100 | - | - | `NO` | - |
| 21 | **`PlacedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 22 | **`ConfirmedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 23 | **`ShippedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 24 | **`DeliveredAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 25 | **`CancelledAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 26 | **`CancellationReason`** | `character varying` | 500 | - | - | `YES` | - |
| 27 | **`PaymentOrderId`** | `character varying` | 100 | - | - | `YES` | - |
| 28 | **`PaymentId`** | `character varying` | 100 | - | - | `YES` | - |
| 29 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 30 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."ProductAttributeValues"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `32 kB` (Data: `0 bytes`, Indexes: `24 kB`)
- **Foreign Keys:** 2
  - `AttributeId -> ProductAttributes.Id (ON DELETE RESTRICT)`
  - `ProductId -> Products.Id (ON DELETE CASCADE)`
- **Indexes:** 3
  - `IX_ProductAttributeValues_AttributeId_Value`: `CREATE INDEX "IX_ProductAttributeValues_AttributeId_Value" ON public."ProductAttributeValues" USING btree ("AttributeId", "Value")`
  - `IX_ProductAttributeValues_ProductId_AttributeId`: `CREATE INDEX "IX_ProductAttributeValues_ProductId_AttributeId" ON public."ProductAttributeValues" USING btree ("ProductId", "AttributeId")`
  - `PK_ProductAttributeValues`: `CREATE UNIQUE INDEX "PK_ProductAttributeValues" ON public."ProductAttributeValues" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`ProductId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`AttributeId`** | `uuid` | - | - | - | `NO` | - |
| 4 | **`Value`** | `character varying` | 500 | - | - | `NO` | - |
| 5 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 6 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."ProductAttributes"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `24 kB` (Data: `0 bytes`, Indexes: `24 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 3
  - `IX_ProductAttributes_Code`: `CREATE UNIQUE INDEX "IX_ProductAttributes_Code" ON public."ProductAttributes" USING btree ("Code")`
  - `IX_ProductAttributes_DisplayOrder`: `CREATE INDEX "IX_ProductAttributes_DisplayOrder" ON public."ProductAttributes" USING btree ("DisplayOrder")`
  - `PK_ProductAttributes`: `CREATE UNIQUE INDEX "PK_ProductAttributes" ON public."ProductAttributes" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`Name`** | `character varying` | 100 | - | - | `NO` | - |
| 3 | **`Code`** | `character varying` | 100 | - | - | `NO` | - |
| 4 | **`DisplayOrder`** | `integer` | - | 32 | 0 | `NO` | - |
| 5 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 6 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 7 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."ProductMedia"`

- **Row Count (Live Count):** `108`
- **Total Storage Size:** `120 kB` (Data: `24 kB`, Indexes: `64 kB`)
- **Foreign Keys:** 2
  - `MediaAssetId -> MediaAssets.Id (ON DELETE SET NULL)`
  - `ProductId -> Products.Id (ON DELETE CASCADE)`
- **Indexes:** 4
  - `IX_ProductMedia_MediaAssetId`: `CREATE INDEX "IX_ProductMedia_MediaAssetId" ON public."ProductMedia" USING btree ("MediaAssetId")`
  - `IX_ProductMedia_ProductId_DisplayOrder`: `CREATE INDEX "IX_ProductMedia_ProductId_DisplayOrder" ON public."ProductMedia" USING btree ("ProductId", "DisplayOrder")`
  - `IX_ProductMedia_ProductId_IsPrimary`: `CREATE INDEX "IX_ProductMedia_ProductId_IsPrimary" ON public."ProductMedia" USING btree ("ProductId", "IsPrimary")`
  - `PK_ProductMedia`: `CREATE UNIQUE INDEX "PK_ProductMedia" ON public."ProductMedia" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`ProductId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`MediaUrl`** | `character varying` | 2000 | - | - | `NO` | - |
| 4 | **`AltText`** | `character varying` | 300 | - | - | `NO` | - |
| 5 | **`DisplayOrder`** | `integer` | - | 32 | 0 | `NO` | - |
| 6 | **`IsPrimary`** | `boolean` | - | - | - | `NO` | - |
| 7 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 8 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 9 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 10 | **`MediaAssetId`** | `uuid` | - | - | - | `YES` | - |

---

### Table: `public."ProductVariants"`

- **Row Count (Live Count):** `107`
- **Total Storage Size:** `88 kB` (Data: `16 kB`, Indexes: `48 kB`)
- **Foreign Keys:** 1
  - `ProductId -> Products.Id (ON DELETE CASCADE)`
- **Indexes:** 3
  - `IX_ProductVariants_ProductId_IsActive`: `CREATE INDEX "IX_ProductVariants_ProductId_IsActive" ON public."ProductVariants" USING btree ("ProductId", "IsActive")`
  - `IX_ProductVariants_Sku`: `CREATE UNIQUE INDEX "IX_ProductVariants_Sku" ON public."ProductVariants" USING btree ("Sku")`
  - `PK_ProductVariants`: `CREATE UNIQUE INDEX "PK_ProductVariants" ON public."ProductVariants" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`ProductId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`Name`** | `character varying` | 150 | - | - | `NO` | - |
| 4 | **`Sku`** | `character varying` | 100 | - | - | `NO` | - |
| 5 | **`Mrp`** | `numeric` | - | 18 | 2 | `NO` | - |
| 6 | **`SellingPrice`** | `numeric` | - | 18 | 2 | `NO` | - |
| 7 | **`IsDefault`** | `boolean` | - | - | - | `NO` | - |
| 8 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 9 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 10 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."Products"`

- **Row Count (Live Count):** `107`
- **Total Storage Size:** `192 kB` (Data: `48 kB`, Indexes: `112 kB`)
- **Foreign Keys:** 2
  - `CategoryId -> Categories.Id (ON DELETE RESTRICT)`
  - `SubcategoryId -> Subcategories.Id (ON DELETE RESTRICT)`
- **Indexes:** 7
  - `IX_Products_ApprovalStatus`: `CREATE INDEX "IX_Products_ApprovalStatus" ON public."Products" USING btree ("ApprovalStatus")`
  - `IX_Products_CategoryId_IsActive`: `CREATE INDEX "IX_Products_CategoryId_IsActive" ON public."Products" USING btree ("CategoryId", "IsActive")`
  - `IX_Products_IsPromoted`: `CREATE INDEX "IX_Products_IsPromoted" ON public."Products" USING btree ("IsPromoted")`
  - `IX_Products_SellerId`: `CREATE INDEX "IX_Products_SellerId" ON public."Products" USING btree ("SellerId")`
  - `IX_Products_Slug`: `CREATE UNIQUE INDEX "IX_Products_Slug" ON public."Products" USING btree ("Slug")`
  - `IX_Products_SubcategoryId`: `CREATE INDEX "IX_Products_SubcategoryId" ON public."Products" USING btree ("SubcategoryId")`
  - `PK_Products`: `CREATE UNIQUE INDEX "PK_Products" ON public."Products" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`CategoryId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`SubcategoryId`** | `uuid` | - | - | - | `YES` | - |
| 4 | **`Name`** | `character varying` | 200 | - | - | `NO` | - |
| 5 | **`Slug`** | `character varying` | 220 | - | - | `NO` | - |
| 6 | **`ShortDescription`** | `character varying` | 500 | - | - | `NO` | - |
| 7 | **`Description`** | `character varying` | 5000 | - | - | `NO` | - |
| 8 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 9 | **`IsFeatured`** | `boolean` | - | - | - | `NO` | - |
| 10 | **`DisplayOrder`** | `integer` | - | 32 | 0 | `NO` | - |
| 11 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 12 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 13 | **`ApprovalStatus`** | `integer` | - | 32 | 0 | `NO` | `0` |
| 14 | **`ApprovedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 15 | **`ApprovedBy`** | `uuid` | - | - | - | `YES` | - |
| 16 | **`IsPromoted`** | `boolean` | - | - | - | `NO` | `false` |
| 17 | **`RejectionReason`** | `character varying` | 500 | - | - | `YES` | - |
| 18 | **`SellerId`** | `uuid` | - | - | - | `YES` | - |

---

### Table: `public."Seasons"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `16 kB` (Data: `0 bytes`, Indexes: `8192 bytes`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 1
  - `PK_Seasons`: `CREATE UNIQUE INDEX "PK_Seasons" ON public."Seasons" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`Name`** | `text` | - | - | - | `NO` | - |
| 3 | **`StartMonth`** | `integer` | - | 32 | 0 | `NO` | - |
| 4 | **`StartDay`** | `integer` | - | 32 | 0 | `NO` | - |
| 5 | **`EndMonth`** | `integer` | - | 32 | 0 | `NO` | - |
| 6 | **`EndDay`** | `integer` | - | 32 | 0 | `NO` | - |
| 7 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 8 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 9 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."SellerCategoryAssignments"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `32 kB` (Data: `0 bytes`, Indexes: `32 kB`)
- **Foreign Keys:** 1
  - `CategoryId -> Categories.Id (ON DELETE CASCADE)`
- **Indexes:** 4
  - `IX_SellerCategoryAssignments_CategoryId`: `CREATE INDEX "IX_SellerCategoryAssignments_CategoryId" ON public."SellerCategoryAssignments" USING btree ("CategoryId")`
  - `IX_SellerCategoryAssignments_SellerId`: `CREATE INDEX "IX_SellerCategoryAssignments_SellerId" ON public."SellerCategoryAssignments" USING btree ("SellerId")`
  - `IX_SellerCategoryAssignments_SellerId_CategoryId`: `CREATE UNIQUE INDEX "IX_SellerCategoryAssignments_SellerId_CategoryId" ON public."SellerCategoryAssignments" USING btree ("SellerId", "CategoryId")`
  - `PK_SellerCategoryAssignments`: `CREATE UNIQUE INDEX "PK_SellerCategoryAssignments" ON public."SellerCategoryAssignments" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`SellerId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`CategoryId`** | `uuid` | - | - | - | `NO` | - |
| 4 | **`AssignedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |

---

### Table: `public."StockMovements"`

- **Row Count (Live Count):** `1`
- **Total Storage Size:** `64 kB` (Data: `8192 bytes`, Indexes: `48 kB`)
- **Foreign Keys:** 2
  - `ProductVariantId -> ProductVariants.Id (ON DELETE RESTRICT)`
  - `WarehouseId -> Warehouses.Id (ON DELETE RESTRICT)`
- **Indexes:** 3
  - `IX_StockMovements_ProductVariantId`: `CREATE INDEX "IX_StockMovements_ProductVariantId" ON public."StockMovements" USING btree ("ProductVariantId")`
  - `IX_StockMovements_WarehouseId_ProductVariantId_OccurredAtUtc`: `CREATE INDEX "IX_StockMovements_WarehouseId_ProductVariantId_OccurredAtUtc" ON public."StockMovements" USING btree ("WarehouseId", "ProductVariantId", "OccurredAtUtc")`
  - `PK_StockMovements`: `CREATE UNIQUE INDEX "PK_StockMovements" ON public."StockMovements" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`WarehouseId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`ProductVariantId`** | `uuid` | - | - | - | `NO` | - |
| 4 | **`MovementType`** | `integer` | - | 32 | 0 | `NO` | - |
| 5 | **`Quantity`** | `integer` | - | 32 | 0 | `NO` | - |
| 6 | **`Reason`** | `character varying` | 500 | - | - | `NO` | - |
| 7 | **`OccurredAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 8 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 9 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."Subcategories"`

- **Row Count (Live Count):** `61`
- **Total Storage Size:** `104 kB` (Data: `16 kB`, Indexes: `64 kB`)
- **Foreign Keys:** 2
  - `CategoryId -> Categories.Id (ON DELETE RESTRICT)`
  - `ImageMediaAssetId -> MediaAssets.Id (ON DELETE SET NULL)`
- **Indexes:** 4
  - `IX_Subcategories_CategoryId_DisplayOrder`: `CREATE INDEX "IX_Subcategories_CategoryId_DisplayOrder" ON public."Subcategories" USING btree ("CategoryId", "DisplayOrder")`
  - `IX_Subcategories_ImageMediaAssetId`: `CREATE INDEX "IX_Subcategories_ImageMediaAssetId" ON public."Subcategories" USING btree ("ImageMediaAssetId")`
  - `IX_Subcategories_Slug`: `CREATE UNIQUE INDEX "IX_Subcategories_Slug" ON public."Subcategories" USING btree ("Slug")`
  - `PK_Subcategories`: `CREATE UNIQUE INDEX "PK_Subcategories" ON public."Subcategories" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`CategoryId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`Name`** | `character varying` | 150 | - | - | `NO` | - |
| 4 | **`Slug`** | `character varying` | 180 | - | - | `NO` | - |
| 5 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 6 | **`DisplayOrder`** | `integer` | - | 32 | 0 | `NO` | - |
| 7 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 8 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |
| 9 | **`ImageMediaAssetId`** | `uuid` | - | - | - | `YES` | - |

---

### Table: `public."SystemThemeSettings"`

- **Row Count (Live Count):** `1`
- **Total Storage Size:** `24 kB` (Data: `8192 bytes`, Indexes: `16 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 1
  - `PK_SystemThemeSettings`: `CREATE UNIQUE INDEX "PK_SystemThemeSettings" ON public."SystemThemeSettings" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`FontFamily`** | `character varying` | 100 | - | - | `NO` | - |
| 3 | **`FontScale`** | `double precision` | - | 53 | - | `NO` | - |
| 4 | **`FontSizeBasePx`** | `integer` | - | 32 | 0 | `NO` | - |
| 5 | **`HeadingFontFamily`** | `character varying` | 100 | - | - | `YES` | - |
| 6 | **`UpdatedBy`** | `uuid` | - | - | - | `NO` | - |
| 7 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 8 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."Warehouses"`

- **Row Count (Live Count):** `1`
- **Total Storage Size:** `48 kB` (Data: `8192 bytes`, Indexes: `32 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 2
  - `IX_Warehouses_Code`: `CREATE UNIQUE INDEX "IX_Warehouses_Code" ON public."Warehouses" USING btree ("Code")`
  - `PK_Warehouses`: `CREATE UNIQUE INDEX "PK_Warehouses" ON public."Warehouses" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`Name`** | `character varying` | 150 | - | - | `NO` | - |
| 3 | **`Code`** | `character varying` | 50 | - | - | `NO` | - |
| 4 | **`AddressLine1`** | `character varying` | 250 | - | - | `NO` | - |
| 5 | **`AddressLine2`** | `character varying` | 250 | - | - | `YES` | - |
| 6 | **`City`** | `character varying` | 100 | - | - | `NO` | - |
| 7 | **`State`** | `character varying` | 100 | - | - | `NO` | - |
| 8 | **`PostalCode`** | `character varying` | 20 | - | - | `NO` | - |
| 9 | **`Country`** | `character varying` | 100 | - | - | `NO` | - |
| 10 | **`IsActive`** | `boolean` | - | - | - | `NO` | - |
| 11 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 12 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."WishlistItems"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `32 kB` (Data: `0 bytes`, Indexes: `24 kB`)
- **Foreign Keys:** 2
  - `ProductVariantId -> ProductVariants.Id (ON DELETE RESTRICT)`
  - `WishlistId -> Wishlists.Id (ON DELETE CASCADE)`
- **Indexes:** 3
  - `IX_WishlistItems_ProductVariantId`: `CREATE INDEX "IX_WishlistItems_ProductVariantId" ON public."WishlistItems" USING btree ("ProductVariantId")`
  - `IX_WishlistItems_WishlistId_ProductVariantId`: `CREATE UNIQUE INDEX "IX_WishlistItems_WishlistId_ProductVariantId" ON public."WishlistItems" USING btree ("WishlistId", "ProductVariantId")`
  - `PK_WishlistItems`: `CREATE UNIQUE INDEX "PK_WishlistItems" ON public."WishlistItems" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`WishlistId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`ProductVariantId`** | `uuid` | - | - | - | `NO` | - |
| 4 | **`ProductName`** | `character varying` | 200 | - | - | `NO` | - |
| 5 | **`VariantName`** | `character varying` | 150 | - | - | `NO` | - |
| 6 | **`Sku`** | `character varying` | 100 | - | - | `NO` | - |
| 7 | **`ImageUrl`** | `character varying` | 2000 | - | - | `YES` | - |
| 8 | **`UnitPrice`** | `numeric` | - | 18 | 2 | `NO` | - |
| 9 | **`Mrp`** | `numeric` | - | 18 | 2 | `NO` | - |
| 10 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 11 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."Wishlists"`

- **Row Count (Live Count):** `0`
- **Total Storage Size:** `16 kB` (Data: `0 bytes`, Indexes: `16 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 2
  - `IX_Wishlists_UserId`: `CREATE UNIQUE INDEX "IX_Wishlists_UserId" ON public."Wishlists" USING btree ("UserId")`
  - `PK_Wishlists`: `CREATE UNIQUE INDEX "PK_Wishlists" ON public."Wishlists" USING btree ("Id")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`Id`** | `uuid` | - | - | - | `NO` | - |
| 2 | **`UserId`** | `uuid` | - | - | - | `NO` | - |
| 3 | **`CreatedAtUtc`** | `timestamp with time zone` | - | - | - | `NO` | - |
| 4 | **`UpdatedAtUtc`** | `timestamp with time zone` | - | - | - | `YES` | - |

---

### Table: `public."__EFMigrationsHistory"`

- **Row Count (Live Count):** `16`
- **Total Storage Size:** `24 kB` (Data: `8192 bytes`, Indexes: `16 kB`)
- **Foreign Keys:** 0
  - *None defined at database level*
- **Indexes:** 1
  - `PK___EFMigrationsHistory`: `CREATE UNIQUE INDEX "PK___EFMigrationsHistory" ON public."__EFMigrationsHistory" USING btree ("MigrationId")`

**Columns Catalogue:**

| Pos | Column Name | PostgreSQL Data Type | Max Length | Precision | Scale | Nullable | Default Value |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **`MigrationId`** | `character varying` | 150 | - | - | `NO` | - |
| 2 | **`ProductVersion`** | `character varying` | 32 | - | - | `NO` | - |

---

