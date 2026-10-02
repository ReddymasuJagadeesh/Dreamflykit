using Dreamflykit.Application.Inventory.Interfaces;
using Dreamflykit.Domain.Common;
using Dreamflykit.Domain.Orders;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Route("Admin/[controller]")]
public sealed class OrdersController : AdminBaseController
{
    private readonly DreamflykitDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IInventoryService _inventoryService;

    public OrdersController(
        DreamflykitDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        IInventoryService inventoryService)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _inventoryService = inventoryService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(
        [FromQuery] OrderStatus? status,
        [FromQuery] string? search,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Orders.AsNoTracking().AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(o => o.Status == status.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLowerInvariant();
            query = query.Where(o => o.OrderNumber.ToLower().Contains(term));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var ordersRaw = await query
            .OrderByDescending(o => o.CreatedAtUtc)
            .Take(50)
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

        var userIds = ordersRaw.Select(r => r.UserId).Distinct().ToList();
        var users = await _userManager.Users
            .Where(u => userIds.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, u => new { Name = $"{u.FirstName} {u.LastName}".Trim(), u.Email }, cancellationToken);

        var items = ordersRaw.Select(o =>
        {
            users.TryGetValue(o.UserId, out var usr);
            return new AdminOrderListItemViewModel
            {
                OrderId = o.Id,
                OrderNumber = o.OrderNumber,
                CustomerName = string.IsNullOrWhiteSpace(usr?.Name) ? "Customer" : usr.Name,
                CustomerEmail = usr?.Email ?? "N/A",
                ItemCount = o.ItemCount,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                CreatedAtUtc = o.CreatedAtUtc
            };
        }).ToList();

        var model = new AdminOrderListViewModel
        {
            Orders = items,
            SelectedStatus = status,
            SearchTerm = search,
            TotalCount = totalCount
        };

        return View(model);
    }

    [HttpGet("Details/{id:guid}")]
    public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        if (order is null)
        {
            return NotFound();
        }

        var customer = await _userManager.FindByIdAsync(order.UserId.ToString());

        var items = order.Items.Select(i => new AdminOrderItemViewModel
        {
            ProductId = i.ProductVariantId,
            ProductName = i.ProductName,
            VariantName = i.VariantName,
            Sku = i.Sku,
            UnitPrice = i.UnitPrice,
            Quantity = i.Quantity,
            ImageUrl = i.ImageUrl
        }).ToList();

        var availableStatuses = GetNextAllowedStatuses(order.Status);

        var model = new AdminOrderDetailsViewModel
        {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            CustomerName = customer != null ? $"{customer.FirstName} {customer.LastName}".Trim() : "Customer",
            CustomerEmail = customer?.Email ?? "N/A",
            CustomerPhone = customer?.PhoneNumber ?? order.ShippingAddress.Phone,
            Status = order.Status,
            PaymentMethod = "COD / Online",
            TrackingNumber = order.OrderNumber,
            TotalAmount = order.GrandTotal,
            Subtotal = order.Subtotal,
            ShippingFee = order.DeliveryCharge,
            CreatedAtUtc = order.CreatedAtUtc,
            ShippedAtUtc = order.ShippedAtUtc,
            DeliveredAtUtc = order.DeliveredAtUtc,
            ShippingFullName = order.ShippingAddress.FullName,
            ShippingPhone = order.ShippingAddress.Phone,
            ShippingAddressLine = $"{order.ShippingAddress.AddressLine1} {order.ShippingAddress.AddressLine2}".Trim(),
            ShippingCity = order.ShippingAddress.City,
            ShippingState = order.ShippingAddress.State,
            ShippingPostalCode = order.ShippingAddress.PostalCode,
            ShippingCountry = order.ShippingAddress.Country,
            CancellationReason = order.CancellationReason,
            PaymentOrderId = order.PaymentOrderId,
            Items = items,
            AvailableNextStatuses = availableStatuses
        };

        return View(model);
    }

    [HttpPost("UpdateStatus")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(
        [FromForm] Guid orderId,
        [FromForm] OrderStatus newStatus,
        [FromForm] string? cancellationReason,
        CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order is null)
        {
            return NotFound();
        }

        try
        {
            switch (newStatus)
            {
                case OrderStatus.Confirmed:
                    order.Confirm();
                    break;
                case OrderStatus.Processing:
                    order.StartProcessing();
                    break;
                case OrderStatus.Packed:
                    order.Pack();
                    break;
                case OrderStatus.Shipped:
                    order.Ship();
                    break;
                case OrderStatus.OutForDelivery:
                    order.MarkOutForDelivery();
                    break;
                case OrderStatus.Delivered:
                    order.MarkDelivered();
                    break;
                case OrderStatus.Cancelled:
                    order.Cancel(string.IsNullOrWhiteSpace(cancellationReason) ? "Cancelled by Admin" : cancellationReason);
                    var itemsToRelease = order.Items.Select(i => (i.ProductVariantId, i.Quantity)).ToList();
                    await _inventoryService.ReleaseReservedStockAsync(
                        itemsToRelease,
                        order.OrderNumber,
                        string.IsNullOrWhiteSpace(cancellationReason) ? "Cancelled by Admin" : cancellationReason,
                        cancellationToken);
                    break;
                case OrderStatus.ReturnApproved:
                    order.ApproveReturn();
                    break;
                case OrderStatus.Refunded:
                    order.MarkRefunded();
                    var itemsToRestock = order.Items.Select(i => (i.ProductVariantId, i.Quantity)).ToList();
                    await _inventoryService.RestockReturnedItemsAsync(
                        itemsToRestock,
                        order.OrderNumber,
                        order.CancellationReason ?? "Customer Return Refund Processed",
                        cancellationToken);
                    break;
                default:
                    TempData["ErrorMessage"] = $"Direct transition to {newStatus} is not supported.";
                    return RedirectToAction("Details", new { id = orderId });
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            TempData["SuccessMessage"] = $"Order {order.OrderNumber} status successfully updated to {newStatus}.";
        }
        catch (DomainException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("Details", new { id = orderId });
    }

    private static IReadOnlyList<OrderStatus> GetNextAllowedStatuses(OrderStatus current)
    {
        return current switch
        {
            OrderStatus.Pending => [OrderStatus.Confirmed, OrderStatus.Cancelled],
            OrderStatus.Confirmed => [OrderStatus.Processing, OrderStatus.Cancelled],
            OrderStatus.Processing => [OrderStatus.Packed, OrderStatus.Cancelled],
            OrderStatus.Packed => [OrderStatus.Shipped],
            OrderStatus.Shipped => [OrderStatus.OutForDelivery],
            OrderStatus.OutForDelivery => [OrderStatus.Delivered],
            OrderStatus.ReturnRequested => [OrderStatus.ReturnApproved],
            OrderStatus.ReturnApproved => [OrderStatus.Refunded],
            _ => []
        };
    }
}
