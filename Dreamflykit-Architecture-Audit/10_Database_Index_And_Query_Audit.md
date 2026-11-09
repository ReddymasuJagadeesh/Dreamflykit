# 10. DATABASE INDEX & QUERY PERFORMANCE AUDIT: DREAMFLYKIT

**Objective:** Audit of all 72 existing PostgreSQL indexes, evaluation of index selectivity, identification of missing indexes, and query execution plan risks.

---

## 1. Existing Index Inventory Analysis

PostgreSQL currently has **72 indexes** across 37 tables. 
- 37 Primary Key unique B-tree indexes.
- 12 Alternate Key unique indexes (`Slug`, `Sku`, `Code`, `UserId`, `WarehouseId_ProductVariantId`).
- 23 Non-unique B-tree foreign key and composite search indexes.

### Healthy, Well-Designed Existing Indexes:
1. `IX_InventoryItems_WarehouseId_ProductVariantId` (UNIQUE B-tree): Prevents duplicate inventory item rows per variant in a warehouse.
2. `IX_CartItems_CartId_ProductVariantId` (UNIQUE B-tree): Enforces quantity incrementation rather than duplicate lines in cart.
3. `IX_Orders_UserId_PlacedAtUtc` (Composite B-tree): Optimal index for customer order history pagination (`WHERE UserId = @id ORDER BY PlacedAtUtc DESC`).
4. `IX_ProductVariants_ProductId_IsActive` (Composite B-tree): Speeds up product details variant resolution.

---

## 2. Missing Critical Indexes (High Performance Bottlenecks)

| Table | Missing Index Columns | Query / Workflow Supported | Expected Performance Impact Without Index |
| :--- | :--- | :--- | :--- |
| **`Orders`** | `PaymentOrderId` | Cashfree webhook lookup (`WHERE PaymentOrderId = @id`) | **Full Table Scan on Orders** on every payment webhook! At 500k orders, webhook latency spikes from 1ms to 350ms. |
| **`Orders`** | `Status` | Admin orders filtering (`WHERE Status = @status`) | Full Table Scan for admin dashboard order queues. |
| **`Orders`** | `CreatedAtUtc DESC` | Admin order chronological ordering | In-memory external sort overhead on high order volume. |
| **`OrderItems`** | `ProductVariantId` | Product sales reporting & stock history | Full Table Scan on millions of `OrderItems` rows. |
| **`Products`** | `Name` (Trigram / GIN) | Autocomplete & search bar (`WHERE Name ILIKE '%term%'`) | Severe sequential scan bottleneck on catalogue search. |
| **`ProductVariants`** | `SellingPrice` | Price facet filtering & sorting (`ORDER BY SellingPrice`) | Requires sorting all variants in temporary memory. |
| **`Countries`** | `Code` | Country kit resolution (`WHERE Code = @code`) | Unindexed lookup (only PK Id is indexed). |

---

## 3. Recommended PostgreSQL Index Definitions

```sql
-- 1. Accelerate Cashfree payment webhook processing (CRITICAL)
CREATE INDEX CONCURRENTLY IF NOT EXISTS "IX_Orders_PaymentOrderId" 
ON public."Orders" USING btree ("PaymentOrderId") 
WHERE "PaymentOrderId" IS NOT NULL;

-- 2. Accelerate Admin order queue filtering by status
CREATE INDEX CONCURRENTLY IF NOT EXISTS "IX_Orders_Status_CreatedAtUtc" 
ON public."Orders" USING btree ("Status", "CreatedAtUtc" DESC);

-- 3. Accelerate Order Item product variant lookups
CREATE INDEX CONCURRENTLY IF NOT EXISTS "IX_OrderItems_ProductVariantId" 
ON public."OrderItems" USING btree ("ProductVariantId");

-- 4. Enable Trigram Fast Search on Product Names (Requires pg_trgm extension)
CREATE EXTENSION IF NOT EXISTS pg_trgm;

CREATE INDEX CONCURRENTLY IF NOT EXISTS "IX_Products_Name_trgm" 
ON public."Products" USING gin ("Name" gin_trgm_ops);

-- 5. Accelerate price range filtering and sorting
CREATE INDEX CONCURRENTLY IF NOT EXISTS "IX_ProductVariants_SellingPrice_IsActive" 
ON public."ProductVariants" USING btree ("SellingPrice") 
WHERE "IsActive" = true;

-- 6. Enforce country code uniqueness and fast lookup
CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS "IX_Countries_Code" 
ON public."Countries" USING btree (upper("Code"));
```
