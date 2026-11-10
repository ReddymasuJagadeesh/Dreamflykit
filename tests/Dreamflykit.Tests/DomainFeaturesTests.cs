using System.ComponentModel.DataAnnotations;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Application.Email.Interfaces;
using Dreamflykit.Application.Shipping.Models;
using Dreamflykit.Domain.Catalog;
using Dreamflykit.Domain.Common;
using Dreamflykit.Infrastructure.Email;
using Dreamflykit.Infrastructure.Shipping;
using Dreamflykit.Web.Models.Account;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;

namespace Dreamflykit.Tests;

public sealed class DomainFeaturesTests
{
    // ============================================================
    // PRODUCT REVIEWS & RATINGS TESTS
    // ============================================================

    [Fact]
    public void ProductReview_WithValidInputs_InstantiatesCorrectly()
    {
        var productId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var review = new ProductReview(
            productId: productId,
            userId: userId,
            customerName: "Siddharth Verma",
            rating: 5,
            title: "Superb kit essentials",
            comment: "Loved the quality and packaging.",
            isVerifiedPurchase: true);

        Assert.Equal(productId, review.ProductId);
        Assert.Equal(userId, review.UserId);
        Assert.Equal("Siddharth Verma", review.CustomerName);
        Assert.Equal(5, review.Rating);
        Assert.Equal("Superb kit essentials", review.Title);
        Assert.Equal("Loved the quality and packaging.", review.Comment);
        Assert.True(review.IsVerifiedPurchase);
        Assert.True(review.IsApproved);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    [InlineData(-1)]
    public void ProductReview_WithInvalidRating_ThrowsDomainException(int invalidRating)
    {
        var ex = Assert.Throws<DomainException>(() =>
            new ProductReview(
                productId: Guid.NewGuid(),
                userId: Guid.NewGuid(),
                customerName: "Aarav",
                rating: invalidRating,
                title: "Nice",
                comment: "Good"));

        Assert.Contains("Rating must be between 1 and 5", ex.Message);
    }

    [Fact]
    public void ProductReview_WithEmptyTitle_ThrowsDomainException()
    {
        var ex = Assert.Throws<DomainException>(() =>
            new ProductReview(
                productId: Guid.NewGuid(),
                userId: Guid.NewGuid(),
                customerName: "Aarav",
                rating: 4,
                title: "   ",
                comment: "Good"));

        Assert.Contains("Review title is required", ex.Message);
    }

    [Fact]
    public void ProductRatingSummaryDto_CalculatesPercentagesAndAverages()
    {
        var summary = new ProductRatingSummaryDto
        {
            AverageRating = 4.5m,
            TotalReviews = 100,
            FiveStarCount = 60,
            FourStarCount = 30,
            ThreeStarCount = 10,
            TwoStarCount = 0,
            OneStarCount = 0
        };

        Assert.Equal(60, summary.GetPercentage(summary.FiveStarCount));
        Assert.Equal(30, summary.GetPercentage(summary.FourStarCount));
        Assert.Equal(10, summary.GetPercentage(summary.ThreeStarCount));
        Assert.Equal(0, summary.GetPercentage(summary.TwoStarCount));
        Assert.Equal(4.5m, summary.AverageRating);
    }

    // ============================================================
    // EMAIL SERVICE TESTS
    // ============================================================

    [Fact]
    public async Task EmailService_WhenSmtpHostEmpty_LogsGracefullyWithoutThrowing()
    {
        var options = Options.Create(new EmailSettings
        {
            SmtpHost = "" // unconfigured dev mode
        });

        var emailSender = new SmtpEmailSender(options, NullLogger<SmtpEmailSender>.Instance);

        // Should complete successfully without throwing network exceptions
        await emailSender.SendPasswordResetEmailAsync("test@example.com", "https://dreamflykit.com/reset?token=xyz");
        await emailSender.SendOrderConfirmationEmailAsync("test@example.com", "DFK-10023", 1499m);
    }

    // ============================================================
    // SHIPPING PROVIDER TESTS
    // ============================================================

    [Fact]
    public async Task ShippingService_OrderUnder999_ChargesStandardFee()
    {
        var shippingService = new DefaultShippingService(NullLogger<DefaultShippingService>.Instance);
        var request = new ShippingRateRequest
        {
            OrderSubtotal = 499m,
            DestinationPostalCode = "560001",
            IsExpress = false
        };

        var quote = await shippingService.CalculateShippingRateAsync(request);

        Assert.Equal(99m, quote.ShippingFee);
        Assert.False(quote.IsFreeShipping);
        Assert.Equal(3, quote.EstimatedDeliveryDaysMin);
    }

    [Fact]
    public async Task ShippingService_OrderAbove999_QualifiesForFreeShipping()
    {
        var shippingService = new DefaultShippingService(NullLogger<DefaultShippingService>.Instance);
        var request = new ShippingRateRequest
        {
            OrderSubtotal = 1299m,
            DestinationPostalCode = "560001",
            IsExpress = false
        };

        var quote = await shippingService.CalculateShippingRateAsync(request);

        Assert.Equal(0m, quote.ShippingFee);
        Assert.True(quote.IsFreeShipping);
    }

    [Fact]
    public async Task ShippingService_ExpressShipping_AppliesExpressFeeAndShorterWindow()
    {
        var shippingService = new DefaultShippingService(NullLogger<DefaultShippingService>.Instance);
        var request = new ShippingRateRequest
        {
            OrderSubtotal = 1500m,
            DestinationPostalCode = "560001",
            IsExpress = true
        };

        var quote = await shippingService.CalculateShippingRateAsync(request);

        Assert.Equal(100m, quote.ShippingFee);
        Assert.Equal(1, quote.EstimatedDeliveryDaysMin);
        Assert.Equal(2, quote.EstimatedDeliveryDaysMax);
    }

    [Fact]
    public async Task ShippingService_GenerateWaybill_ReturnsFormattedNumber()
    {
        var shippingService = new DefaultShippingService(NullLogger<DefaultShippingService>.Instance);
        var waybill = await shippingService.GenerateWaybillNumberAsync("100234");

        Assert.StartsWith("DFK-100234-", waybill);
    }

    // ============================================================
    // VIEW MODEL VALIDATION TESTS
    // ============================================================

    [Fact]
    public void ForgotPasswordViewModel_WithInvalidEmail_FailsValidation()
    {
        var model = new ForgotPasswordViewModel
        {
            Email = "invalid-email"
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(ForgotPasswordViewModel.Email)));
    }

    [Fact]
    public void ResetPasswordViewModel_WithMismatchedPasswords_FailsValidation()
    {
        var model = new ResetPasswordViewModel
        {
            Email = "user@dreamflykit.com",
            Code = "valid-token-code",
            Password = "SecurePassword123!",
            ConfirmPassword = "DifferentPassword123!"
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("match"));
    }
}

