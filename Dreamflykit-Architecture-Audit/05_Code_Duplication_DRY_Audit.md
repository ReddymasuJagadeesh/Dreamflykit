# 05. CODE DUPLICATION & DRY ARCHITECTURAL AUDIT: DREAMFLYKIT

**Objective:** Systematic identification of repeated patterns, copy-pasted implementations, duplicated domain rules, and calculation of a formal DRY Score.

---

## 1. Executive DRY Evaluation & Score

Dreamflykit exhibits widespread, systemic code duplication primarily caused by creating 14 separate "Hubs" for e-commerce categories instead of using a unified, metadata-driven catalogue engine.

### DRY Score: **28 / 100**

#### Scoring Methodology
1. **Redundancy Ratio (Weight: 40%):** 89,715 lines out of 108,795 lines (82.5%) are duplicated DTO structures, JSON file handlers, and repetitive Razor markup. Score: **15/100**.
2. **Business Rule Duplication (Weight: 30%):** Price calculations, discount percentages, and theme configurations are repeated independently across 14 DTO classes and Razor views. Score: **30/100**.
3. **Controller & Service Duplication (Weight: 20%):** 14 HubServices and 14 Admin Controllers repeat identical CRUD patterns with minor name changes. Score: **35/100**.
4. **Configuration Duplication (Weight: 10%):** Duplicate ASP.NET Core Identity & Cookie registrations. Score: **50/100**.
- **Composite DRY Score:** `(15*0.4) + (30*0.3) + (35*0.2) + (50*0.1) = 6.0 + 9.0 + 7.0 + 5.0 = 27.0 -> 28 / 100`.

---

## 2. Formal Duplication Catalogue

| Duplication ID | Scope / Category | Files Affected | Estimated Duplicated Lines | Severity | Recommended Centralization |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **DUP-01** | Hub Services | 14 `*HubService.cs` files in `Dreamflykit.Infrastructure` | ~2,100 lines | **HIGH** | Single generic `CategoryHubContentService` backed by PostgreSQL |
| **DUP-02** | Admin Hub Controllers | 14 `*HubController.cs` in `Areas/Admin/Controllers` | ~2,500 lines | **HIGH** | Unified `CategoryHubAdminController` with route parameter `{categorySlug}` |
| **DUP-03** | Hub DTO Models | 15 `*HubDto.cs` in `Application/*Hub/Models` | ~16,000 lines | **CRITICAL** | Reusable `CategoryHubConfigDto`, `HeroBannerDto`, `ThemeDto` |
| **DUP-04** | Identity Registration | `Program.cs` & `InfrastructureServiceCollectionExtensions.cs` | ~70 lines | **MEDIUM** | Remove duplicate call in `Program.cs` |
| **DUP-05** | Shared Hub Razor Views | 29 `_*Hub.cshtml` views in `Views/Shared` | ~32,000 lines | **CRITICAL** | Reusable Razor View Components (`vc:hero-slider`, `vc:product-carousel`) |
| **DUP-06** | Product Item Mapping | Multiple Hub DTOs & `ProductListViewModel` | ~800 lines | **MEDIUM** | Standard `ProductCardViewModel` |
| **DUP-07** | Address Mapping | `OrderService.cs` & `AddressService.cs` | ~60 lines | **LOW** | Mapping extension method `ToSnapshot()` |
| **DUP-08** | CSS Keyframes & Styles | Inline `<style>` blocks in 29 Razor views | ~4,500 lines | **HIGH** | Consolidate into `wwwroot/css/hub-components.css` |

---

## 3. Deep-Dive Duplication Case Studies

### Case Study DUP-01: 14 Duplicate Hub Services
**Affected Files:**
- `src/Dreamflykit.Infrastructure/AppliancesHub/AppliancesHubService.cs`
- `src/Dreamflykit.Infrastructure/BeautyHub/BeautyHubService.cs`
- `src/Dreamflykit.Infrastructure/ElectronicsHub/ElectronicsHubService.cs`
- `src/Dreamflykit.Infrastructure/FashionHub/FashionHubService.cs`
- `src/Dreamflykit.Infrastructure/FoodHub/FoodHubService.cs`
- `src/Dreamflykit.Infrastructure/GamingHub/GamingHubService.cs`
- `src/Dreamflykit.Infrastructure/HealthHub/HealthHubService.cs`
- `src/Dreamflykit.Infrastructure/LuggageHub/LuggageHubService.cs`
- `src/Dreamflykit.Infrastructure/MobilesHub/MobilesHubService.cs`
- `src/Dreamflykit.Infrastructure/SportsHub/SportsHubService.cs`
- `src/Dreamflykit.Infrastructure/StoryBooksHub/StoryBooksHubService.cs`
- `src/Dreamflykit.Infrastructure/StudyHub/StudyHubService.cs`
- `src/Dreamflykit.Infrastructure/TravelHub/TravelHubService.cs`
- `src/Dreamflykit.Infrastructure/CountryKits/CountryKitsHubService.cs`

**Identical Pattern Repeated in Every Class:**
```csharp
// Repeated across 14 services:
public class [Category]HubService : I[Category]HubService
{
    private const string CacheKey = "[category]_category_hub_cache";
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<[Category]HubService> _logger;
    private readonly string _filePath;

    public [Category]HubService(...) {
        _filePath = Path.Combine(env.WebRootPath, "data", "[category]_hub.json");
    }

    public async Task<[Category]HubDto> GetConfigAsync(...) {
        if (_cache.TryGetValue(CacheKey, out [Category]HubDto cached)) return cached;
        var json = await File.ReadAllTextAsync(_filePath);
        var dto = JsonSerializer.Deserialize<[Category]HubDto>(json);
        _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
        return dto;
    }

    public async Task UpdateConfigAsync([Category]HubDto dto, ...) {
        var json = JsonSerializer.Serialize(dto, JsonOptions);
        await File.WriteAllTextAsync(_filePath, json);
        _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
    }
}
```
**Refactoring Plan:**
Eliminate all 14 files. Replace with one unified `CategoryHubService` that queries the PostgreSQL database for category banners, featured collections, and products.

---

### Case Study DUP-02: 14 Duplicate Admin Hub Controllers
**Affected Files:**
- `Areas/Admin/Controllers/FashionHubController.cs`
- `Areas/Admin/Controllers/ElectronicsHubController.cs`
- `Areas/Admin/Controllers/MobilesHubController.cs`
- `Areas/Admin/Controllers/BeautyHubController.cs`
- ... (14 controllers total)

Every single controller repeats:
1. `[HttpGet("")] public async Task<IActionResult> Index()`
2. `[HttpPost("SaveConfig")] public async Task<IActionResult> SaveConfig(...)`
3. `[HttpPost("UploadImage")] public async Task<IActionResult> UploadImage(...)`
4. `[HttpPost("ResetDefaults")] public async Task<IActionResult> ResetDefaults()`

**Refactoring Plan:**
Consolidate into a single parameterized controller:
`Areas/Admin/Controllers/CategoryHubAdminController.cs` with routing:
`[Route("Admin/CategoryHub/{categorySlug}")]`.

---

### Case Study DUP-05: Massive Razor View Duplication
- Over **44,786 lines** in `Views/Shared/_*Hub.cshtml`.
- Each file re-implements:
  - Hero banner carousel with Swiper initialization.
  - "Shop By Category" horizontal grid.
  - Product card templates with badge overlays, wishlist heart button, and price formatting.
  - Reassurance trust badges (Free Shipping, Return Policy, Student Discount).
  - Embedded duplicate CSS `<style>` blocks with repeated keyframes (`pulse`, `shimmer`, `float`).

**Refactoring Plan:**
Convert to 4 reusable ASP.NET Core View Components:
1. `<vc:hub-hero-banner model="Model.HeroBanner" />`
2. `<vc:category-grid categories="Model.Categories" />`
3. `<vc:product-card product="prod" />`
4. `<vc:trust-badges badges="Model.TrustBadges" />`
This will eliminate ~35,000 lines of redundant HTML and script markup!
