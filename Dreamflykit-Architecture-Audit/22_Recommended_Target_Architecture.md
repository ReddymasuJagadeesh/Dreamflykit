# 22. RECOMMENDED TARGET ARCHITECTURE: DREAMFLYKIT

**Objective:** Clean, modular monolith target architecture designed to eliminate code duplication, unify data into PostgreSQL, and support high-scale operations.

---

## 1. Target System Architecture Blueprint

```mermaid
graph TD
    Client[Web & Mobile Browsers] --> Proxy[Nginx Reverse Proxy / SSL Termination]
    Proxy --> Web[Dreamflykit.Web (ASP.NET Core 8 MVC)]

    subgraph "Dreamflykit.Web (Presentation Layer)"
        Controllers[Thin Controllers<br/>Catalog, Checkout, Orders, Account, Admin]
        ViewComponents[Reusable View Components<br/>HeroBanner, ProductCard, CategoryGrid, TrustBadges]
        Middleware[Auth, RateLimiter, SecurityHeaders]
    end

    Web --> App[Dreamflykit.Application (Use Cases & Orchestration)]

    subgraph "Dreamflykit.Application (Bounded Contexts)"
        subgraph IdentityContext[Identity & Customer Context]
            UserService
        end
        subgraph CatalogContext[Catalog & Category Context]
            ProductService
            CategoryService
            SearchService
        end
        subgraph InventoryContext[Inventory & Fulfillment Context]
            InventoryService
            WarehouseService
        end
        subgraph OrderingContext[Cart & Order Context]
            CartService
            OrderService
            CheckoutService
        end
        subgraph PaymentContext[Payment & Gateway Context]
            CashfreeAdapter
            WebhookProcessor
        end
    end

    App --> Domain[Dreamflykit.Domain (Pure Enterprise Domain Rules)]

    subgraph "Dreamflykit.Domain (Core Aggregates)"
        OrderAgg[Order & OrderItem]
        ProductAgg[Product & ProductVariant]
        CartAgg[Cart & CartItem]
        InvAgg[InventoryItem & StockMovement]
        KitAgg[Kit & KitItem]
        UserAgg[ApplicationUser & Address]
    end

    App --> Infra[Dreamflykit.Infrastructure (Persistence & External Adapters)]

    subgraph "Dreamflykit.Infrastructure"
        EF[DreamflykitDbContext]
        RedisAdapter[StackExchange.Redis Distributed Cache]
        S3Adapter[AWS S3 / CloudFront Media Storage]
        EmailAdapter[SendGrid / SMTP Email Service]
    end

    EF --> Postgres[(PostgreSQL 18 Primary Database)]
    RedisAdapter --> Redis[(Redis Cache Server)]
    S3Adapter --> S3[(AWS S3 Bucket)]
```

---

## 2. Bounded Context Structure & Namespace Reorganization

Rather than 15 separate Hub folders duplicating identical JSON file loaders, the Target Architecture organizes features by **Domain-Driven Design (DDD) Bounded Contexts**:

```
Dreamflykit/
├── src/
│   ├── Dreamflykit.Domain/
│   │   ├── Identity/          (ApplicationUser, CustomerAddress)
│   │   ├── Catalog/           (Product, ProductVariant, Category, Subcategory, Brand, ProductMedia)
│   │   ├── Inventory/         (Warehouse, InventoryItem, StockMovement)
│   │   ├── Kits/              (Kit, KitItem, Country, Season)
│   │   ├── Cart/              (Cart, CartItem)
│   │   ├── Orders/            (Order, OrderItem, OrderAddress, OrderStatus)
│   │   ├── Payments/          (PaymentTransaction, WebhookEvent)
│   │   ├── Promotions/        (Coupon, DiscountRule)
│   │   └── Common/            (Entity, AuditableEntity, DomainException)
│   │
│   ├── Dreamflykit.Application/
│   │   ├── Identity/          (IUserService, AuthCommands, DTOs)
│   │   ├── Catalog/           (IProductService, ICategoryService, ProductQueryDTOs)
│   │   ├── Content/           (ICategoryShowcaseService, BannerDTOs, ThemeDTOs)
│   │   ├── Inventory/         (IInventoryService, StockCommands)
│   │   ├── Kits/              (IKitService, IFindMyKitService)
│   │   ├── Orders/            (IOrderService, CheckoutCommands, OrderDTOs)
│   │   ├── Payments/          (IPaymentService, WebhookCommands)
│   │   └── Common/            (PagedResult, Result, Interfaces)
│   │
│   ├── Dreamflykit.Infrastructure/
│   │   ├── Persistence/       (DreamflykitDbContext, Migrations, Seeders)
│   │   ├── Repositories/      (ProductRepository, OrderRepository, CartRepository...)
│   │   ├── Payments/          (CashfreePaymentService, CashfreeOptions)
│   │   ├── Storage/           (S3MediaStorageService, LocalMediaStorageService)
│   │   ├── Caching/           (RedisCacheService)
│   │   └── Notifications/     (SmtpEmailService)
│   │
│   └── Dreamflykit.Web/
│       ├── Controllers/       (CatalogController, CheckoutController, OrdersController, AccountController)
│       ├── Areas/Admin/       (DashboardController, OrdersController, CatalogController, ShowcaseController)
│       ├── ViewComponents/    (HeroBannerViewComponent, ProductCardViewComponent, CategoryGridViewComponent)
│       └── Views/             (Standard Razor Views consuming shared ViewComponents)
```

---

## 3. Database Unification of Category Hubs

In the target architecture, all 14 category landing pages are unified into PostgreSQL tables:
- **`Categories` table:** Enhanced with `ThemePrimaryColor`, `ThemeAccentColor`, `HeroBannerMediaAssetId`, `Tagline`.
- **`HomepageSections` table:** Stores category carousel blocks, subcategory showcase strips, and festival promotion cards.
- **Benefits:**
  - Zero flat-file disk dependencies.
  - Full relational integrity across products, categories, and inventory.
  - Seamless horizontal scaling across multiple container instances.
  - Zero data loss on container redeployment.
