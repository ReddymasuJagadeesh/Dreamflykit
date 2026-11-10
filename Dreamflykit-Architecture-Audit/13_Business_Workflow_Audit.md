# 13. BUSINESS WORKFLOW AUDIT & SEQUENCE DIAGRAMS: DREAMFLYKIT

**Objective:** End-to-end execution path tracing, transaction boundary audit, concurrency analysis, and sequence diagram visualization of critical business workflows.

---

## 1. Sequence Diagram: Customer Checkout & Order Creation

```mermaid
sequenceDiagram
    autonumber
    actor Customer
    participant CheckoutCtrl as CheckoutController
    participant CartSvc as CartService
    participant AddrSvc as AddressService
    participant OrderSvc as OrderService
    participant InvSvc as InventoryService
    participant InvRepo as InventoryRepository
    participant OrderRepo as OrderRepository
    participant Db as PostgreSQL

    Customer->>CheckoutCtrl: POST /Checkout/place-order (ShippingAddressId, PaymentMethod)
    CheckoutCtrl->>CartSvc: GetCartAsync(userId)
    CartSvc-->>CheckoutCtrl: Cart (Items, Subtotal)
    CheckoutCtrl->>OrderSvc: PlaceOrderAsync(PlaceOrderCommand)
    
    rect rgb(255, 240, 240)
        Note over OrderSvc, Db: CRITICAL: No Ambient DB Transaction!
        OrderSvc->>InvSvc: ReserveStockAsync(variantIds, orderNumber)
        InvSvc->>InvRepo: SaveRangeAsync(inventoryItems)
        InvRepo->>Db: UPDATE "InventoryItems" (Available - Qty, Reserved + Qty)
        InvSvc->>InvRepo: AddStockMovementAsync(movement)
        InvRepo->>Db: INSERT INTO "StockMovements"
        
        OrderSvc->>OrderRepo: GenerateOrderNumberAsync()
        OrderSvc->>OrderRepo: AddAsync(OrderAggregate)
        OrderRepo->>Db: INSERT INTO "Orders", "OrderItems"
        
        OrderSvc->>CartSvc: ClearCart(userId)
        CartSvc->>Db: DELETE FROM "CartItems"
    end

    alt PaymentMethod == "ONLINE"
        CheckoutCtrl->>PaymentSvc: CreatePaymentSessionAsync(...)
        PaymentSvc-->>CheckoutCtrl: PaymentSessionId
        CheckoutCtrl-->>Customer: Redirect to /Checkout/Payment?sessionId=...
    else PaymentMethod == "COD"
        CheckoutCtrl-->>Customer: Redirect to /Orders/Confirmation?orderNumber=...
    end
```

### Risk Identified in Checkout Workflow:
If step 12 (`OrderRepo.AddAsync`) fails due to a database error, step 7 (`InventoryItems` update) has already committed! Stock is reserved permanently for an order that was never created, causing inventory leak and stock starvation.

---

## 2. Sequence Diagram: Payment Webhook vs Browser Callback Race Condition

```mermaid
sequenceDiagram
    autonumber
    actor CustomerBrowser as Customer Browser
    participant Gateway as Cashfree Payment Gateway
    participant CheckoutCtrl as CheckoutController
    participant OrderSvc as OrderService
    participant Order as Order (Domain Entity)
    participant Db as PostgreSQL

    Gateway-->>CustomerBrowser: Payment Authorized -> Redirect with paymentId
    CustomerBrowser->>CheckoutCtrl: POST /Checkout/VerifyPayment (orderNumber, paymentId)
    Gateway->>CheckoutCtrl: POST /Checkout/webhook/cashfree (Webhook Payload, Signature)

    par Thread A: Browser Redirect Verification
        CheckoutCtrl->>OrderSvc: MarkPaymentCompletedAsync(orderNumber, paymentId)
        OrderSvc->>Order: RecordPaymentSuccess(paymentId)
        Note over Order: Sets Status = Confirmed
        OrderSvc->>Db: SaveChangesAsync() (Status = Confirmed)
        CheckoutCtrl-->>CustomerBrowser: 302 Redirect to /Orders/Confirmation
    and Thread B: Webhook Delivery (Arrives 50ms later)
        CheckoutCtrl->>CheckoutCtrl: VerifyWebhookSignature() (Valid)
        CheckoutCtrl->>OrderSvc: MarkPaymentCompletedAsync(orderNumber, paymentId)
        OrderSvc->>Order: RecordPaymentSuccess(paymentId)
        Order->>Order: EnsureStatus(OrderStatus.Pending)
        rect rgb(255, 220, 220)
            Note over Order: THROW DomainException!<br/>"Only pending orders can be confirmed"
        end
        CheckoutCtrl-->>Gateway: HTTP 400 Bad Request!
        Note over Gateway: Gateway assumes failure and retries webhook!
    end
```

### Root Cause:
`Order.RecordPaymentSuccess()` calls `Confirm()`, which strictly asserts `Status == OrderStatus.Pending`. In distributed payment flows, callbacks and webhooks are asynchronous and unordered.
**Remediation:** Implement idempotent state transitions:
```csharp
if (Status == OrderStatus.Confirmed) return; // Idempotent no-op
```

---

## 3. Sequence Diagram: Category Hub Request (Split-Brain Flat-File Path)

```mermaid
sequenceDiagram
    autonumber
    actor User
    participant ProductsCtrl as ProductsController
    participant FashionHubSvc as FashionHubService
    participant MemCache as IMemoryCache
    participant FileSys as Disk: wwwroot/data/fashion_hub.json
    participant View as _FashionCategoryHub.cshtml

    User->>ProductsCtrl: GET /category/fashion
    ProductsCtrl->>FashionHubSvc: GetConfigAsync()
    FashionHubSvc->>MemCache: TryGetValue("fashion_category_hub_cache")
    
    alt Cache Miss
        FashionHubSvc->>FileSys: File.ReadAllTextAsync("fashion_hub.json")
        FileSys-->>FashionHubSvc: 7,591 Lines JSON
        FashionHubSvc->>FashionHubSvc: JsonSerializer.Deserialize<FashionHubDto>()
        FashionHubSvc->>MemCache: Set(cacheKey, dto, 4 Hours)
    end
    FashionHubSvc-->>ProductsCtrl: FashionHubDto
    ProductsCtrl->>ProductsCtrl: Assemble ProductListViewModel (58 properties)
    ProductsCtrl-->>View: Render View(Model)
    View-->>User: HTML Output
    Note over ProductsCtrl, FileSys: PostgreSQL Database was NEVER contacted!
```
