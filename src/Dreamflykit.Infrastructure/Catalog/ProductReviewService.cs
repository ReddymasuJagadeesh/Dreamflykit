using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Domain.Catalog;
using Dreamflykit.Domain.Orders;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.Catalog;

public sealed class ProductReviewService : IProductReviewService
{
    private readonly DreamflykitDbContext _context;
    private readonly ILogger<ProductReviewService> _logger;

    public ProductReviewService(
        DreamflykitDbContext context,
        ILogger<ProductReviewService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IReadOnlyList<ProductReviewDto>> GetApprovedReviewsAsync(
        Guid productId,
        int take = 20,
        CancellationToken cancellationToken = default)
    {
        if (_context == null) return Array.Empty<ProductReviewDto>();

        var reviews = await _context.ProductReviews
            .AsNoTracking()
            .Where(r => r.ProductId == productId && r.IsApproved)
            .OrderByDescending(r => r.CreatedAtUtc)
            .Take(take)
            .Select(r => new ProductReviewDto
            {
                Id = r.Id,
                ProductId = r.ProductId,
                UserId = r.UserId,
                CustomerName = r.CustomerName,
                Rating = r.Rating,
                Title = r.Title,
                Comment = r.Comment,
                IsVerifiedPurchase = r.IsVerifiedPurchase,
                CreatedAtUtc = r.CreatedAtUtc
            })
            .ToListAsync(cancellationToken);

        return reviews;
    }

    public async Task<ProductRatingSummaryDto> GetRatingSummaryAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        if (_context == null) return new ProductRatingSummaryDto();

        var ratings = await _context.ProductReviews
            .AsNoTracking()
            .Where(r => r.ProductId == productId && r.IsApproved)
            .Select(r => r.Rating)
            .ToListAsync(cancellationToken);

        if (!ratings.Any())
        {
            return new ProductRatingSummaryDto
            {
                AverageRating = 0m,
                TotalReviews = 0,
                FiveStarCount = 0,
                FourStarCount = 0,
                ThreeStarCount = 0,
                TwoStarCount = 0,
                OneStarCount = 0
            };
        }

        var total = ratings.Count;
        var avg = Math.Round((decimal)ratings.Average(), 1);

        return new ProductRatingSummaryDto
        {
            AverageRating = avg,
            TotalReviews = total,
            FiveStarCount = ratings.Count(r => r == 5),
            FourStarCount = ratings.Count(r => r == 4),
            ThreeStarCount = ratings.Count(r => r == 3),
            TwoStarCount = ratings.Count(r => r == 2),
            OneStarCount = ratings.Count(r => r == 1)
        };
    }

    public async Task<ProductReviewDto> SubmitReviewAsync(
        Guid productId,
        Guid userId,
        string customerName,
        int rating,
        string title,
        string comment,
        CancellationToken cancellationToken = default)
    {
        if (_context == null)
        {
            throw new InvalidOperationException("Database context is not available.");
        }

        // Check if verified buyer
        var isVerified = await _context.OrderItems
            .AsNoTracking()
            .AnyAsync(
                oi => _context.ProductVariants.Any(pv => pv.Id == oi.ProductVariantId && pv.ProductId == productId) &&
                      _context.Orders.Any(o => o.Id == oi.OrderId && o.UserId == userId && o.Status != OrderStatus.Cancelled),
                cancellationToken);

        var review = new ProductReview(
            productId: productId,
            userId: userId,
            customerName: customerName,
            rating: rating,
            title: title,
            comment: comment,
            isVerifiedPurchase: isVerified,
            isApproved: true);

        _context.ProductReviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Product review {ReviewId} created for Product {ProductId} by User {UserId}. Verified: {IsVerified}",
            review.Id, productId, userId, isVerified);

        return new ProductReviewDto
        {
            Id = review.Id,
            ProductId = review.ProductId,
            UserId = review.UserId,
            CustomerName = review.CustomerName,
            Rating = review.Rating,
            Title = review.Title,
            Comment = review.Comment,
            IsVerifiedPurchase = review.IsVerifiedPurchase,
            CreatedAtUtc = review.CreatedAtUtc
        };
    }
}

