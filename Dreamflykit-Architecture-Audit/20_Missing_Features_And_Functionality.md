# 20. MISSING FEATURES & FUNCTIONALITY GAP ANALYSIS: DREAMFLYKIT

**Objective:** Systematic gap analysis comparing Dreamflykit against industry-standard e-commerce platforms (Flipkart, Amazon).

---

## 1. Comprehensive E-Commerce Capability Gap Matrix

| Feature / Module | Enterprise Requirement | Current Dreamflykit Implementation | Gap Severity |
| :--- | :--- | :--- | :--- |
| **Password Reset / Forgot Password** | Secure email token reset link with expiry | **Completely Missing** (No action in `AccountController`) | **HIGH** |
| **Customer Email Notifications** | Order confirmation, shipping, password reset | **Completely Missing** (No `IEmailService` or SMTP) | **HIGH** |
| **SMS / OTP Authentication** | Mobile login, order dispatch SMS | **Completely Missing** (No SMS gateway integration) | **MEDIUM** |
| **Shipping Carrier Integration** | Real-time rate calculation, automated waybill generation (Shiprocket/Delhivery) | **Completely Missing** (No shipping abstraction) | **HIGH** |
| **Shipment Tracking Engine** | Real-time tracking status, carrier webhooks | **Completely Missing** (Only stores static tracking number) | **HIGH** |
| **GST Tax Invoicing Engine** | HSN/SAC codes, CGST/SGST/IGST breakdown, PDF invoice download | **Completely Missing** (No tax fields in Order table) | **HIGH** |
| **Payment Gateway Refunds** | API-driven automatic refund initiation on return approval | **Completely Missing** (`ProcessRefundAsync` only updates enum) | **HIGH** |
| **Customer Product Reviews & Ratings** | Star ratings, text reviews, photo uploads, verified purchase badge | **Completely Missing** in DB (Ratings are hardcoded in DTOs) | **HIGH** |
| **Coupons & Promotional Rules** | Percentage/flat discounts, minimum cart value, usage limits | **Completely Missing** (No coupon domain model or engine) | **MEDIUM** |
| **Customer Address Book** | Multiple saved addresses, edit/delete address | **Partially Implemented** (Add/List works; Edit/Delete missing) | **LOW** |
| **Wishlist to Cart Migration** | One-click "Move to Cart" button | **Partially Implemented** (Domain model exists; UI incomplete) | **LOW** |
| **Full-Text Catalogue Search** | Typo tolerance, stemming, synonym search | **Basic SQL ILIKE** (No trigram index or dedicated engine) | **MEDIUM** |
