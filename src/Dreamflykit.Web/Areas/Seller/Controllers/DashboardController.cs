using Dreamflykit.Domain.Catalog;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Seller.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Seller.Controllers;

[Route("Seller")]
[Route("Seller/[controller]")]
public sealed class DashboardController : SellerBaseController
{
    private readonly DreamflykitDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public DashboardController(
        DreamflykitDbContext dbContext,
        UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    [HttpGet("")]
    [HttpGet("Index")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var sellerId = CurrentUserId;
        var user = await _userManager.FindByIdAsync(sellerId.ToString());

        var productsQuery = _dbContext.Products
            .AsNoTracking()
            .Where(p => p.SellerId == sellerId);

        var total = await productsQuery.CountAsync(cancellationToken);
        var pending = await productsQuery.CountAsync(p => p.ApprovalStatus == ProductApprovalStatus.PendingApproval, cancellationToken);
        var approved = await productsQuery.CountAsync(p => p.ApprovalStatus == ProductApprovalStatus.Approved, cancellationToken);
        var rejected = await productsQuery.CountAsync(p => p.ApprovalStatus == ProductApprovalStatus.Rejected, cancellationToken);
        var promoted = await productsQuery.CountAsync(p => p.IsPromoted, cancellationToken);

        var recentRaw = await productsQuery
            .OrderByDescending(p => p.CreatedAtUtc)
            .Take(8)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Slug,
                CategoryName = _dbContext.Categories.Where(c => c.Id == p.CategoryId).Select(c => c.Name).FirstOrDefault() ?? "Uncategorized",
                SellingPrice = _dbContext.ProductVariants.Where(v => v.ProductId == p.Id && v.IsActive).OrderBy(v => v.SellingPrice).Select(v => v.SellingPrice).FirstOrDefault(),
                Mrp = _dbContext.ProductVariants.Where(v => v.ProductId == p.Id && v.IsActive).OrderBy(v => v.SellingPrice).Select(v => v.Mrp).FirstOrDefault(),
                MediaInfo = _dbContext.ProductMedia.Where(m => m.ProductId == p.Id && m.IsActive).OrderBy(m => m.DisplayOrder).Select(m => new { m.MediaUrl, m.MediaAssetId }).FirstOrDefault(),
                p.IsActive,
                p.IsPromoted,
                p.ApprovalStatus,
                p.RejectionReason,
                CreatedAtUtc = p.CreatedAtUtc
            })
            .ToListAsync(cancellationToken);

        var recentList = recentRaw.Select(p => new SellerProductListItemViewModel
        {
            ProductId = p.Id,
            Name = p.Name,
            Slug = p.Slug,
            CategoryName = p.CategoryName,
            SellingPrice = p.SellingPrice,
            Mrp = p.Mrp,
            ImageUrl = p.MediaInfo?.MediaUrl ?? "/images/products/product-placeholder.webp",
            IsActive = p.IsActive,
            IsPromoted = p.IsPromoted,
            ApprovalStatus = p.ApprovalStatus,
            RejectionReason = p.RejectionReason,
            CreatedAtUtc = p.CreatedAtUtc
        }).ToList();

        var model = new SellerDashboardViewModel
        {
            SellerName = user != null ? $"{user.FirstName} {user.LastName}".Trim() : "Seller Partner",
            SellerEmail = user?.Email ?? "",
            TotalProducts = total,
            PendingCount = pending,
            ApprovedCount = approved,
            RejectedCount = rejected,
            PromotedCount = promoted,
            RecentProducts = recentList
        };

        return View(model);
    }
}
