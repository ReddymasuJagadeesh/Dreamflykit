using System.Security.Claims;
using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.Catalog.Models;
using Dreamflykit.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

[Authorize]
public sealed class ReviewsController : Controller
{
    private readonly IProductReviewService _productReviewService;
    private readonly IProductService _productService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ReviewsController> _logger;

    public ReviewsController(
        IProductReviewService productReviewService,
        IProductService productService,
        UserManager<ApplicationUser> userManager,
        ILogger<ReviewsController> logger)
    {
        _productReviewService = productReviewService;
        _productService = productService;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpPost("/products/{productId:guid}/reviews")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitReview(
        Guid productId,
        [FromForm] SubmitProductReviewRequest request,
        string? returnUrl,
        CancellationToken cancellationToken)
    {
        if (request == null)
        {
            TempData["ErrorMessage"] = "Invalid review data.";
            return RedirectToProduct(returnUrl, productId);
        }

        if (request.Rating < 1 || request.Rating > 5)
        {
            TempData["ErrorMessage"] = "Please select a star rating between 1 and 5.";
            return RedirectToProduct(returnUrl, productId);
        }

        if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Comment))
        {
            TempData["ErrorMessage"] = "Please provide both a headline and review comments.";
            return RedirectToProduct(returnUrl, productId);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var customerName = !string.IsNullOrWhiteSpace(user.FirstName)
            ? $"{user.FirstName} {user.LastName}".Trim()
            : user.UserName ?? "Verified Customer";

        try
        {
            await _productReviewService.SubmitReviewAsync(
                productId: productId,
                userId: user.Id,
                customerName: customerName,
                rating: request.Rating,
                title: request.Title,
                comment: request.Comment,
                cancellationToken: cancellationToken);

            TempData["SuccessMessage"] = "Thank you! Your verified review has been posted successfully.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save product review for product {ProductId} by user {UserId}", productId, user.Id);
            TempData["ErrorMessage"] = "An error occurred while posting your review. Please try again.";
        }

        return RedirectToProduct(returnUrl, productId);
    }

    private IActionResult RedirectToProduct(string? returnUrl, Guid productId)
    {
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return Redirect($"/products/{productId}");
    }
}

