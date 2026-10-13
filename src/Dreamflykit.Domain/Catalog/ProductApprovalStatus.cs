namespace Dreamflykit.Domain.Catalog;

/// <summary>
/// Defines the approval state for products submitted to the catalog.
/// Admin-created products are Approved by default.
/// Seller-created products start as PendingApproval until Super Admin sets prices and approves.
/// </summary>
public enum ProductApprovalStatus
{
    /// <summary>Approved by Super Admin and ready/live in catalog (default for backward compatibility).</summary>
    Approved = 0,

    /// <summary>Submitted by a Seller; awaiting Super Admin review, pricing, and approval.</summary>
    PendingApproval = 1,

    /// <summary>Rejected by Super Admin with feedback reason.</summary>
    Rejected = 2
}
