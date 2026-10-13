using Dreamflykit.Domain.Catalog;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Infrastructure.Persistence;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
[Route("Admin/[controller]")]
public sealed class SellersController : AdminBaseController
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly DreamflykitDbContext _dbContext;

    public SellersController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        DreamflykitDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(
        [FromQuery] string? search,
        CancellationToken cancellationToken)
    {
        // Get all users in Seller role
        var sellerUsers = await _userManager.GetUsersInRoleAsync("Seller");
        var sellerList = sellerUsers.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLowerInvariant();
            sellerList = sellerList.Where(u =>
                (u.Email != null && u.Email.ToLower().Contains(term)) ||
                u.FirstName.ToLower().Contains(term) ||
                u.LastName.ToLower().Contains(term) ||
                (u.PhoneNumber != null && u.PhoneNumber.Contains(term)));
        }

        var sellers = sellerList.OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToList();
        var sellerIds = sellers.Select(s => s.Id).ToList();

        // Query product counts grouped by seller
        var productStats = await _dbContext.Products
            .AsNoTracking()
            .Where(p => p.SellerId.HasValue && sellerIds.Contains(p.SellerId.Value))
            .GroupBy(p => p.SellerId!.Value)
            .Select(g => new
            {
                SellerId = g.Key,
                Total = g.Count(),
                Pending = g.Count(p => p.ApprovalStatus == ProductApprovalStatus.PendingApproval),
                Approved = g.Count(p => p.ApprovalStatus == ProductApprovalStatus.Approved)
            })
            .ToDictionaryAsync(x => x.SellerId, cancellationToken);

        // Query assigned categories for each seller
        var categoryAssignments = await _dbContext.SellerCategoryAssignments
            .AsNoTracking()
            .Where(a => sellerIds.Contains(a.SellerId))
            .Join(_dbContext.Categories, a => a.CategoryId, c => c.Id, (a, c) => new { a.SellerId, c.Name })
            .ToListAsync(cancellationToken);

        var sellerCategoryMap = categoryAssignments
            .GroupBy(x => x.SellerId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Name).ToList());

        var list = new List<AdminSellerListItemViewModel>();

        foreach (var s in sellers)
        {
            var isLocked = await _userManager.IsLockedOutAsync(s);
            productStats.TryGetValue(s.Id, out var stats);
            var assignedCats = sellerCategoryMap.GetValueOrDefault(s.Id, new List<string>());

            list.Add(new AdminSellerListItemViewModel
            {
                SellerId = s.Id,
                FullName = $"{s.FirstName} {s.LastName}".Trim(),
                Email = s.Email ?? "N/A",
                PhoneNumber = s.PhoneNumber ?? "N/A",
                IsActive = s.IsActive,
                IsLockedOut = isLocked,
                TotalProducts = stats?.Total ?? 0,
                PendingProducts = stats?.Pending ?? 0,
                ApprovedProducts = stats?.Approved ?? 0,
                AssignedCategoryNames = assignedCats
            });
        }

        var model = new AdminSellerListViewModel
        {
            Sellers = list,
            SearchTerm = search,
            TotalCount = list.Count
        };

        return View(model);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        ViewBag.Categories = await _dbContext.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .ToListAsync(cancellationToken);

        return View(new AdminCreateSellerViewModel());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminCreateSellerViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _dbContext.Categories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .ToListAsync(cancellationToken);

            return View(model);
        }

        var normalizedEmail = model.Email.Trim().ToLowerInvariant();

        var existingUser = await _userManager.FindByEmailAsync(normalizedEmail);
        if (existingUser != null)
        {
            ModelState.AddModelError(nameof(model.Email), "A user account with this email address already exists.");

            ViewBag.Categories = await _dbContext.Categories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .ToListAsync(cancellationToken);

            return View(model);
        }

        // Ensure "Seller" role exists
        if (!await _roleManager.RoleExistsAsync("Seller"))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid>("Seller"));
        }

        var seller = new ApplicationUser
        {
            UserName = normalizedEmail,
            Email = normalizedEmail,
            FirstName = model.FirstName.Trim(),
            LastName = model.LastName.Trim(),
            PhoneNumber = model.PhoneNumber.Trim(),
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            IsActive = model.IsActive
        };

        var result = await _userManager.CreateAsync(seller, model.Password);
        if (!result.Succeeded)
        {
            ViewBag.Categories = await _dbContext.Categories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .ToListAsync(cancellationToken);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        await _userManager.AddToRoleAsync(seller, "Seller");

        // Save assigned categories chosen by Super Admin
        if (model.SelectedCategoryIds != null && model.SelectedCategoryIds.Count > 0)
        {
            foreach (var catId in model.SelectedCategoryIds.Distinct())
            {
                _dbContext.SellerCategoryAssignments.Add(new SellerCategoryAssignment(seller.Id, catId));
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        TempData["SuccessMessage"] = $"Seller account for '{seller.FirstName} {seller.LastName}' ({seller.Email}) created successfully with {model.SelectedCategoryIds?.Count ?? 0} authorized categories! They can now log in at /Seller/Login.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var seller = await _userManager.FindByIdAsync(id.ToString());
        if (seller is null)
        {
            return NotFound();
        }

        var assignedCategoryIds = await _dbContext.SellerCategoryAssignments
            .Where(a => a.SellerId == id)
            .Select(a => a.CategoryId)
            .ToListAsync(cancellationToken);

        ViewBag.Categories = await _dbContext.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .ToListAsync(cancellationToken);

        var model = new AdminEditSellerViewModel
        {
            SellerId = seller.Id,
            FirstName = seller.FirstName,
            LastName = seller.LastName,
            Email = seller.Email ?? string.Empty,
            PhoneNumber = seller.PhoneNumber ?? string.Empty,
            IsActive = seller.IsActive,
            SelectedCategoryIds = assignedCategoryIds
        };

        return View(model);
    }

    [HttpPost("Edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, AdminEditSellerViewModel model, CancellationToken cancellationToken)
    {
        if (id != model.SellerId)
        {
            return BadRequest();
        }

        var seller = await _userManager.FindByIdAsync(id.ToString());
        if (seller is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _dbContext.Categories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .ToListAsync(cancellationToken);

            return View(model);
        }

        seller.FirstName = model.FirstName.Trim();
        seller.LastName = model.LastName.Trim();
        seller.PhoneNumber = model.PhoneNumber.Trim();
        seller.IsActive = model.IsActive;

        await _userManager.UpdateAsync(seller);

        // Update category assignments
        var currentAssignments = await _dbContext.SellerCategoryAssignments
            .Where(a => a.SellerId == id)
            .ToListAsync(cancellationToken);

        _dbContext.SellerCategoryAssignments.RemoveRange(currentAssignments);

        if (model.SelectedCategoryIds != null && model.SelectedCategoryIds.Count > 0)
        {
            foreach (var catId in model.SelectedCategoryIds.Distinct())
            {
                _dbContext.SellerCategoryAssignments.Add(new SellerCategoryAssignment(id, catId));
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = $"Seller '{seller.FirstName} {seller.LastName}' profile and category permissions updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("ToggleLock")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleLock([FromForm] Guid sellerId)
    {
        var user = await _userManager.FindByIdAsync(sellerId.ToString());
        if (user is null)
        {
            return NotFound();
        }

        var isLocked = await _userManager.IsLockedOutAsync(user);
        if (isLocked)
        {
            await _userManager.SetLockoutEndDateAsync(user, null);
            TempData["SuccessMessage"] = $"Seller account '{user.Email}' has been unlocked.";
        }
        else
        {
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddYears(100));
            TempData["SuccessMessage"] = $"Seller account '{user.Email}' has been locked out.";
        }

        return RedirectToAction(nameof(Index));
    }
}
