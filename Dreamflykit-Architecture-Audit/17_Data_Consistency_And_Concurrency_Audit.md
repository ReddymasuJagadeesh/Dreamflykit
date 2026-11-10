# 17. DATA CONSISTENCY & CONCURRENCY AUDIT: DREAMFLYKIT

**Objective:** Audit of database transactions, race conditions, deadlocks, inventory overselling protection, and idempotency.

---

## 1. Critical Finding: Fatal Recursive Deadlock in `CountryKitsHubService.cs`

### Bug Location:
`src/Dreamflykit.Infrastructure/CountryKits/CountryKitsHubService.cs` (lines 43, 91, 123)

### Mechanism of Deadlock:
`CountryKitsHubService` defines a static, non-reentrant semaphore:
```csharp
private static readonly SemaphoreSlim Semaphore = new(1, 1);
```

In `SaveCountryProfileAsync` (lines 87–94):
```csharp
public async Task<bool> SaveCountryProfileAsync(CountryKitProfileDto profile, CancellationToken ct = default)
{
    if (profile == null || string.IsNullOrWhiteSpace(profile.CountrySlug)) return false;

    await Semaphore.WaitAsync(ct); // Thread acquires Semaphore (Count: 1 -> 0)
    try
    {
        var all = await GetAllProfilesAsync(ct); // Calls GetAllProfilesAsync!
        // ...
```

In `GetAllProfilesAsync` (lines 36–45):
```csharp
public async Task<CountryKitsHubDto> GetAllProfilesAsync(CancellationToken ct = default)
{
    if (_cache.TryGetValue(CacheKey, out CountryKitsHubDto? cached) && cached != null)
    {
        return cached;
    }

    await Semaphore.WaitAsync(ct); // DEADLOCK! Semaphore count is 0!
    try
    {
        // Thread hangs indefinitely waiting for ITSELF to release the semaphore!
```

### Verification & Empirical Proof:
- Running `dotnet test --filter "FullyQualifiedName~CountryKitsHubTests"` hangs indefinitely on `CountryKitsHubService_GetAndSaveProfile_PersistsChanges`.
- The moment `SaveCountryProfileAsync` runs when the cache is empty or expired, the thread deadlocks itself permanently.
- Any subsequent user or admin attempting to browse or save country kits is blocked forever.

### Remediation:
Do not call `GetAllProfilesAsync` inside a method that already holds the semaphore lock. Use an internal private `GetAllProfilesInternalAsync` that assumes the lock is already held.

---

## 2. Multi-Operation Checkout Transaction Atomicity

### The Defect:
In `OrderService.PlaceOrderAsync`:
```csharp
// 1. Reserves stock & inserts stock movements (Commits to DB)
await _inventoryService.ReserveStockAsync(itemsToReserve, orderNumber, cancellationToken);

// 2. Persists order aggregate (Separate SaveChangesAsync)
await _orderRepository.AddAsync(order, cancellationToken);

// 3. Clears cart items (Separate SaveChangesAsync)
cart.Clear();
await _cartRepository.SaveAsync(cart, cancellationToken);
```

### Risk of Partial Failure:
There is **NO shared database transaction** (`_dbContext.Database.BeginTransactionAsync()`).
If the server crashes or the database connection drops after step 1:
- Stock is deducted and reserved in `InventoryItems` and `StockMovements`.
- The customer has NO order in `Orders`.
- The customer's cart still contains the items.
- The merchant loses inventory availability, and the customer is not billed or notified.

### Remediation:
Wrap the entire order checkout execution inside an explicit execution strategy and database transaction:
```csharp
var strategy = _dbContext.Database.CreateExecutionStrategy();
await strategy.ExecuteAsync(async () =>
{
    using var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
    try
    {
        await _inventoryService.ReserveStockAsync(...);
        await _orderRepository.AddAsync(...);
        await _cartRepository.SaveAsync(...);
        await transaction.CommitAsync(cancellationToken);
    }
    catch
    {
        await transaction.RollbackAsync(cancellationToken);
        throw;
    }
});
```

---

## 3. Concurrency Tokens & Concurrency Conflict Handling

- `InventoryItem` is configured with PostgreSQL `xmin` system column in `DreamflykitDbContext.cs` (line 583):
  `entity.Property<uint>("xmin").IsRowVersion();`
- **The Gap:** When two customers attempt to purchase the final unit of stock simultaneously, EF Core detects the concurrency conflict and throws `DbUpdateConcurrencyException`.
- However, **nowhere in `CheckoutController`, `OrderService`, or `InventoryService` is `DbUpdateConcurrencyException` caught or handled!**
- Instead of receiving a graceful user-friendly message ("Item was just purchased by another customer"), the second customer receives an unhandled HTTP 500 internal server error crash!
- **Remediation:** Implement optimistic concurrency retry loop (up to 3 retries) in `InventoryService` to re-read available quantity before reporting out-of-stock.
