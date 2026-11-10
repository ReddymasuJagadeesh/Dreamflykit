namespace Dreamflykit.Application.Catalog.Models;

public sealed class ProductReviewDto
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public Guid UserId { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public int Rating { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Comment { get; init; } = string.Empty;
    public bool IsVerifiedPurchase { get; init; }
    public DateTime CreatedAtUtc { get; init; }
}

public sealed class ProductRatingSummaryDto
{
    public decimal AverageRating { get; init; }
    public int TotalReviews { get; init; }
    public int FiveStarCount { get; init; }
    public int FourStarCount { get; init; }
    public int ThreeStarCount { get; init; }
    public int TwoStarCount { get; init; }
    public int OneStarCount { get; init; }

    public int GetPercentage(int starCount) =>
        TotalReviews > 0 ? (int)Math.Round((double)starCount / TotalReviews * 100) : 0;
}

public sealed class SubmitProductReviewRequest
{
    public Guid ProductId { get; init; }
    public int Rating { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Comment { get; init; } = string.Empty;
}

