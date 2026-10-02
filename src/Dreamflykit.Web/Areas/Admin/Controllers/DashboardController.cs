using Dreamflykit.Domain.Orders;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Route("Admin")]
[Route("Admin/[controller]")]
public sealed class DashboardController : AdminBaseController
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
        var totalOrders = await _dbContext.Orders.CountAsync(cancellationToken);
        var totalRevenue = await _dbContext.Orders
            .Where(o => o.Status != OrderStatus.Cancelled)
            .SelectMany(o => o.Items)
            .SumAsync(i => (decimal?)(i.UnitPrice * i.Quantity), cancellationToken) ?? 0m;

        var totalProducts = await _dbContext.Products.CountAsync(cancellationToken);
        var totalCustomers = await _userManager.Users.CountAsync(cancellationToken);

        var pendingOrders = await _dbContext.Orders
            .CountAsync(o => o.Status == OrderStatus.Pending || o.Status == OrderStatus.Confirmed || o.Status == OrderStatus.Packed, cancellationToken);

        var deliveredOrders = await _dbContext.Orders
            .CountAsync(o => o.Status == OrderStatus.Delivered, cancellationToken);

        var recentOrdersRaw = await _dbContext.Orders
            .AsNoTracking()
            .OrderByDescending(o => o.CreatedAtUtc)
            .Take(10)
            .Select(o => new
            {
                o.Id,
                o.OrderNumber,
                o.UserId,
                TotalAmount = o.Items.Sum(i => i.UnitPrice * i.Quantity),
                o.Status,
                o.CreatedAtUtc,
                ItemCount = o.Items.Count
            })
            .ToListAsync(cancellationToken);

        var userIds = recentOrdersRaw.Select(r => r.UserId).Distinct().ToList();
        var users = await _userManager.Users
            .Where(u => userIds.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, u => new { Name = $"{u.FirstName} {u.LastName}".Trim(), u.Email }, cancellationToken);

        var recentOrderVms = recentOrdersRaw.Select(r =>
        {
            users.TryGetValue(r.UserId, out var usr);
            return new AdminOrderListItemViewModel
            {
                OrderId = r.Id,
                OrderNumber = r.OrderNumber,
                CustomerName = string.IsNullOrWhiteSpace(usr?.Name) ? "Customer" : usr.Name,
                CustomerEmail = usr?.Email ?? "N/A",
                ItemCount = r.ItemCount,
                TotalAmount = r.TotalAmount,
                Status = r.Status,
                CreatedAtUtc = r.CreatedAtUtc
            };
        }).ToList();

        var model = new AdminDashboardViewModel
        {
            TotalOrders = totalOrders,
            TotalRevenue = totalRevenue,
            TotalProducts = totalProducts,
            TotalCustomers = totalCustomers,
            PendingOrders = pendingOrders,
            DeliveredOrders = deliveredOrders,
            RecentOrders = recentOrderVms,
            IsSuperAdmin = User.IsInRole("SuperAdmin")
        };

        return View(model);
    }
}
