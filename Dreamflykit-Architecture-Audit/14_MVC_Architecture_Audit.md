# 14. ASP.NET CORE MVC ARCHITECTURE AUDIT: DREAMFLYKIT

**Objective:** Audit of MVC Controllers, Razor Views, ViewModels, Model Binding, Filters, Middleware, and Security Configuration.

---

## 1. Controller Architecture Review

| Area / Controller | Line Count | Injected Dependencies | Architectural Health | Key Findings |
| :--- | :--- | :--- | :--- | :--- |
| `Controllers/ProductsController.cs` | **1,873 lines** | **15 services** | **CRITICAL FAILURE** | Extreme God Controller; procedural routing for 14 hubs; 40+ conditional branches. |
| `Controllers/CheckoutController.cs` | 256 lines | 5 services | **Moderate** | Missing idempotency in webhook; no transaction around order placement. |
| `Controllers/AccountController.cs` | 195 lines | 3 services | **Good** | Strict password policy, anti-forgery tokens, local redirect protection. Missing forgot password. |
| `Controllers/OrdersController.cs` | 120 lines | 1 service | **Good** | Clean customer orders controller; properly enforces user authorization. |
| `14 Admin Hub Controllers` | ~2,500 lines total | 4 services each | **Poor (Duplicated)** | 14 identical CRUD controllers managing JSON files. |
| `15 Admin Controllers with DbContext`| ~3,200 lines total | `DreamflykitDbContext` | **Architecture Violation** | Directly execute EF Core queries in presentation layer. |

---

## 2. Razor View Architecture & Hygiene

### View File Size Distribution:
- **`Areas/Admin/Views/FashionHub/Index.cshtml`:** **9,638 lines** (Highest in application).
- **`Views/StudyAbroadHub/ConsultancyDetails.cshtml`:** 3,808 lines.
- **`Areas/Admin/Views/StudyAbroadHub/Index.cshtml`:** 3,428 lines.
- **`Views/Shared/_FoodCategoryHub.cshtml`:** 3,286 lines.
- **`Areas/Admin/Views/StudyHub/Index.cshtml`:** 3,047 lines.
- **`Views/Shared/_MobilesCategoryHub.cshtml`:** 2,732 lines.
- **`Views/Shared/_StudyEssentialsHub.cshtml`:** 2,376 lines.
- **`Views/Shared/_StoryBooksHub.cshtml`:** 2,272 lines.
- **`Views/Shared/_FashionWomenClothingHub.cshtml`:** 2,212 lines.
- **`Views/Products/Index.cshtml`:** 1,812 lines.

### Critical View Defects:
1. **Embedded Inline Styles:** Razor views contain thousands of lines of inline `<style>` blocks with custom CSS, Swiper overrides, and duplicate animation keyframes (`@keyframes pulse`, `@keyframes shimmer`).
2. **Inline Business Logic:** Views perform inline mathematical computations, discount percentage formulas, and LINQ sorting directly inside `@foreach` loops.
3. **Service Injection (`@inject`):**
   `_Layout.cshtml` and `_Header.cshtml` inject `IThemeSettingService`, `ICategoryService`, `IHeaderAdService`, `ILogoSettingService`, causing multiple asynchronous database/disk queries during layout rendering.

---

## 3. Middleware & Security Filter Review

### Healthy Configuration in `Program.cs`:
1. **AutoValidateAntiforgeryTokenAttribute:**
   ```csharp
   builder.Services.AddControllersWithViews(options => {
       options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
   });
   ```
   Ensures all state-changing `POST`, `PUT`, `DELETE` requests automatically require valid CSRF tokens.
2. **Security Headers Middleware (lines 130–137):**
   Appends `X-Content-Type-Options: nosniff`, `X-Frame-Options: SAMEORIGIN`, `Referrer-Policy: strict-origin-when-cross-origin`, `X-XSS-Protection: 1; mode=block`.
3. **Cookie Security Policies (lines 84–92):**
   `HttpOnly = true`, `SameSite = Lax`, `SecurePolicy = Always` in production.
