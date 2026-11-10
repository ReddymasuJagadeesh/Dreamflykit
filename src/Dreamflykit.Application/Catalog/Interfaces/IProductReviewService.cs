using Dreamflykit.Application.Catalog.Models;

namespace Dreamflykit.Application.Catalog.Interfaces;

public interface IProductReviewService
{
    Task<IReadOnlyList<ProductReviewDto>> GetApprovedReviewsAsync(
        Guid productId,
        int take = 20,
        CancellationToken cancellationToken = default);

    Task<ProductRatingSummaryDto> GetRatingSummaryAsync(
        Guid productId,
        CancellationToken cancellationToken = default);

    Task<ProductReviewDto> SubmitReviewAsync(
        Guid productId,
        Guid userId,
        string customerName,
        int rating,
        string title,
        string comment,
        CancellationToken cancellationToken = default);
}

