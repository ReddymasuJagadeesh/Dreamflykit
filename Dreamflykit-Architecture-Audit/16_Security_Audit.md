# 16. APPLICATION SECURITY AUDIT: DREAMFLYKIT

**Standards:** OWASP Top 10 (2021), OWASP ASVS v4.0, ASP.NET Core Security Guidelines.

---

## 1. Vulnerability Findings Summary

| Finding ID | Vulnerability Title | OWASP Category | Severity | Status | Remediation Required |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **SEC-01** | Missing Admin Multi-Factor Authentication (MFA) | A07:2021 - Identification & Auth | **HIGH** | Open | Enforce TOTP MFA for `Admin` and `SuperAdmin` roles |
| **SEC-02** | Payment Webhook Duplicate Replay Vulnerability | A04:2021 - Insecure Design | **HIGH** | Open | Implement webhook idempotency table and payload logging |
| **SEC-03** | Missing Rate Limiting on Auth Endpoints | A07:2021 - Identification & Auth | **MEDIUM** | Open | Apply ASP.NET Core 8 Rate Limiting middleware on `/Account/*` |
| **SEC-04** | Missing Password Reset / Account Recovery | A04:2021 - Insecure Design | **MEDIUM** | Open | Implement secure tokenized Password Reset workflow |
| **SEC-05** | Production Secrets in Docker Environment Variables | A05:2021 - Security Misconfiguration | **MEDIUM** | Open | Use Docker Secrets / HashiCorp Vault / Cloud Secret Manager |
| **SEC-06** | CSRF Protection on State-Changing Actions | A01:2021 - Broken Access Control | **LOW** | **Secure** | Verified: `AutoValidateAntiforgeryTokenAttribute` enabled globally |
| **SEC-07** | Password Hashing & Complexity Policy | A02:2021 - Cryptographic Failures | **LOW** | **Secure** | Verified: 12-char min, digit, upper, lower, non-alphanumeric, lockout |
| **SEC-08** | Insecure Direct Object Reference (IDOR) | A01:2021 - Broken Access Control | **LOW** | **Secure** | Verified: Address & Order queries verify current `UserId` claim |

---

## 2. Deep-Dive Security Case Studies

### SEC-01: Admin Multi-Factor Authentication (MFA)
- **Observation:** `DatabaseSeeder.cs` seeds super-admin accounts (`admin@dreamflykit.com`). Identity configuration has `TwoFactorEnabled = false` by default for all users.
- **Risk:** An administrative account with leaked or compromised credentials provides full access to orders, customer addresses, category banners, and product pricing without secondary verification.
- **Remediation:** Enforce mandatory TOTP 2FA (Google Authenticator / Microsoft Authenticator) for any user with `Admin` or `SuperAdmin` roles.

### SEC-02: Payment Webhook Replay & Verification
- **Strengths:** `CashfreePaymentService.VerifyWebhookSignature()` correctly computes HMAC-SHA256 signature using the secret key and verifies the `x-webhook-signature` and `x-webhook-timestamp` headers.
- **Vulnerability:** Webhook payloads are **not logged or stored** in a dedicated database table (`PaymentWebhooks`). No check is performed to verify whether the webhook event ID or payment ID has already been processed. Replaying a captured valid webhook causes unhandled exceptions in the order domain state machine.
- **Remediation:** Create a dedicated table `PaymentWebhookEvents` (`Id`, `Gateway`, `EventId`, `Payload`, `ProcessedAtUtc`, `IsSuccess`) and enforce idempotency.

### SEC-03: Missing IP Rate Limiting
- **Observation:** No rate limiting middleware is registered in `Program.cs`.
- **Risk:** While ASP.NET Core Identity locks out a specific user account after 5 failed attempts (`options.Lockout.MaxFailedAccessAttempts = 5`), attackers can perform credential stuffing across thousands of user emails without IP throttling, or exhaust server resources on checkout endpoints.
- **Remediation:** Configure `builder.Services.AddRateLimiter()` with sliding window policies on `/Account/Login` and `/Checkout/place-order`.
