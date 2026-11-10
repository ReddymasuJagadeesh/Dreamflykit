using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Catalog;

public sealed class ProductReview : AuditableEntity
{
    private ProductReview()
    {
    }

    public ProductReview(
        Guid productId,
        Guid userId,
        string customerName,
        int rating,
        string title,
        string comment,
        bool isVerifiedPurchase = false,
        bool isApproved = true)
    {
        if (productId == Guid.Empty)
        {
            throw new DomainException("Product ID is required.");
        }

        if (userId == Guid.Empty)
        {
            throw new DomainException("User ID is required.");
        }

        if (string.IsNullOrWhiteSpace(customerName))
        {
            throw new DomainException("Customer name is required.");
        }

        if (rating < 1 || rating > 5)
        {
            throw new DomainException("Rating must be between 1 and 5 stars.");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Review title is required.");
        }

        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new DomainException("Review comment is required.");
        }

        ProductId = productId;
        UserId = userId;
        CustomerName = customerName.Trim();
        Rating = rating;
        Title = title.Trim();
        Comment = comment.Trim();
        IsVerifiedPurchase = isVerifiedPurchase;
        IsApproved = isApproved;
    }

    public Guid ProductId { get; private set; }
    public Guid UserId { get; private set; }
    public string CustomerName { get; private set; } = string.Empty;
    public int Rating { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Comment { get; private set; } = string.Empty;
    public bool IsVerifiedPurchase { get; private set; }
    public bool IsApproved { get; private set; }

    public void Approve()
    {
        IsApproved = true;
    }

    public void Reject()
    {
        IsApproved = false;
    }

    public void UpdateReview(int rating, string title, string comment)
    {
        if (rating < 1 || rating > 5)
        {
            throw new DomainException("Rating must be between 1 and 5 stars.");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Review title is required.");
        }

        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new DomainException("Review comment is required.");
        }

        Rating = rating;
        Title = title.Trim();
        Comment = comment.Trim();
    }
}

