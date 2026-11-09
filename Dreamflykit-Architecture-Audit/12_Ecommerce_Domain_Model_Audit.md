# 12. E-COMMERCE DOMAIN MODEL AUDIT: DREAMFLYKIT

**Objective:** Audit of business entity modeling, value objects, domain rules, and mapping completeness across core e-commerce domains.

---

## 1. Domain Implementation Status Matrix

| Business Domain | Key Domain Concepts | Domain Entities / Models | Implementation Status | Deficiencies / Findings |
| :--- | :--- | :--- | :--- | :--- |
| **1. Customer & Identity** | Users, Roles, Profile, Auth, Lockout, Addresses | `ApplicationUser`, `CustomerAddress` | **Partially Implemented** | Password reset/Forgot password missing; No email/SMS verification; Addresses lack FK to user in DB. |
| **2. Product Catalogue** | Categories, Subcategories, Products, Variants, Attributes, Media | `Product`, `ProductVariant`, `Category`, `Subcategory`, `ProductMedia`, `ProductAttribute`, `ProductAttributeValue` | **Partially Implemented** | Core tables implemented, but 14 Category Hubs completely bypass this model and use disk JSON files. Brands not modeled as an entity. |
| **3. Inventory** | Warehouses, Stock items, Movements, Reservations | `Warehouse`, `InventoryItem`, `StockMovement` | **Partially Implemented** | Auto-provisions 100 units of phantom stock if missing; No explicit database transaction wrapping stock movements. |
| **4. Country Kits** | Destination Kits, Countries, Seasons, Kit Items | `Kit`, `KitItem`, `Country`, `Season`, `CountryKitsHubDto` | **Partially Implemented** | Database tables exist but have 0 rows (`Kits`, `Countries`, `Seasons`); UI reads entirely from `country_kits_hub.json`. |
| **5. Shopping Cart** | Cart, Cart Items, Line Totals, Stock Checks | `Cart`, `CartItem` | **Implemented Correctly** | Proper aggregate root with line total calculations and quantity constraints. |
| **6. Orders & Checkout** | Orders, Order Items, Status Transitions, Address Snapshots | `Order`, `OrderItem`, `OrderAddress`, `OrderStatus` | **Partially Implemented** | Immutable address snapshots implemented; financial summary fields (`Subtotal`, `Tax`, `GrandTotal`) missing in `Orders` table. |
| **7. Payments** | Gateway Abstraction, Sessions, Verification, Webhooks | `IPaymentService`, `CashfreePaymentService`, `PaymentSessionRequest` | **Partially Implemented** | Webhook signature checked, but lacks idempotency store; duplicate webhooks cause exceptions; refunds do not call gateway API. |
| **8. Shipping & Fulfillment** | Carrier Integration, Shipments, Waybills, Tracking | *None* | **Missing** | No shipping provider abstraction (Shiprocket / Delhivery); No shipment entity or tracking webhook handler. |
| **9. Reviews & Ratings** | Customer Reviews, Moderation, Verified Purchase | *None* (Only hardcoded in DTOs) | **Missing** | No database tables or domain entities for reviews. Rating stars are hardcoded DTO values. |
| **10. Promotions & Discounts** | Coupons, Promo Codes, Cart Rules | *None* | **Missing** | No coupon domain entity, coupon validator, or discount engine. |

---

## 2. In-Depth Domain Review by Sub-System

### Country Kits Domain
- **Intended Concept:** Destination-aware student kits for USA, UK, Canada, Australia, Germany, Ireland with Small, Medium, and Large options based on season.
- **Architectural Disconnect:**
  The relational model was properly designed with `Kit`, `KitItem`, `Country`, `Season`.
  However, live queries show `Countries: 0 rows`, `Seasons: 0 rows`, `Kits: 0 rows`, `KitItems: 0 rows`.
  The entire "Find My Kit" and "Country Kits" UI was wired to `CountryKitsHubService`, which reads and writes to `wwwroot/data/country_kits_hub.json`.
- **Remediation:** Seed `Countries`, `Seasons`, and `Kits` into PostgreSQL and deprecate the flat file.

### Inventory & Stock Reservation Domain
- **Strengths:** `InventoryItem` has clean domain methods: `ReserveStock()`, `CommitReservation()`, `ReleaseReservation()`, `AddStock()`, `RemoveStock()`.
- **Critical Flaw in `InventoryService.cs`:**
  Line 35 auto-provisions 100 units of stock if an item record is missing:
  ```csharp
  if (invItem is null) {
      invItem = new InventoryItem(warehouse.Id, variantId, availableQuantity: 100, reservedQuantity: 0);
  }
  ```
  This violates real-world e-commerce inventory rules and guarantees overselling physical items that do not exist in the fulfillment warehouse.
