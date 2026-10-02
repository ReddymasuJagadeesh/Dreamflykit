using Dreamflykit.Application.Addressing.Interfaces;
using Dreamflykit.Application.Addressing.Models;
using Dreamflykit.Domain.Common;
using Dreamflykit.Web.Models.Addressing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dreamflykit.Web.Controllers;

[Authorize]
[Route("[controller]")]
public sealed class AddressController : Controller
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(
        [FromQuery] string? returnUrl,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var addresses = await _addressService.GetAddressesAsync(userId, cancellationToken);

        var vm = new AddressListViewModel
        {
            Addresses = addresses,
            ReturnUrl = returnUrl
        };

        return View(vm);
    }

    [HttpGet("Create")]
    public IActionResult Create([FromQuery] string? returnUrl)
    {
        var vm = new AddressFormViewModel
        {
            ReturnUrl = returnUrl
        };
        return View(vm);
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [FromForm] AddressFormViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = GetCurrentUserId();

        var command = new SaveAddressCommand
        {
            FullName = model.FullName,
            Phone = model.Phone,
            AddressLine1 = model.AddressLine1,
            AddressLine2 = model.AddressLine2,
            City = model.City,
            State = model.State,
            PostalCode = model.PostalCode,
            Country = model.Country,
            AddressType = model.AddressType,
            SetAsDefault = model.SetAsDefault
        };

        try
        {
            await _addressService.AddAddressAsync(userId, command, cancellationToken);
            TempData["SuccessMessage"] = "Address added successfully.";

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction(nameof(Index));
        }
        catch (DomainException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    [HttpGet("Edit/{id:guid}")]
    public async Task<IActionResult> Edit(
        [FromRoute] Guid id,
        [FromQuery] string? returnUrl,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var address = await _addressService.GetAddressAsync(id, userId, cancellationToken);

        if (address is null)
        {
            return NotFound();
        }

        var vm = new AddressFormViewModel
        {
            AddressId = address.AddressId,
            FullName = address.FullName,
            Phone = address.Phone,
            AddressLine1 = address.AddressLine1,
            AddressLine2 = address.AddressLine2,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country,
            AddressType = address.AddressType,
            SetAsDefault = address.IsDefault,
            ReturnUrl = returnUrl
        };

        return View(vm);
    }

    [HttpPost("Edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        [FromRoute] Guid id,
        [FromForm] AddressFormViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = GetCurrentUserId();

        var command = new SaveAddressCommand
        {
            FullName = model.FullName,
            Phone = model.Phone,
            AddressLine1 = model.AddressLine1,
            AddressLine2 = model.AddressLine2,
            City = model.City,
            State = model.State,
            PostalCode = model.PostalCode,
            Country = model.Country,
            AddressType = model.AddressType,
            SetAsDefault = model.SetAsDefault
        };

        try
        {
            await _addressService.UpdateAddressAsync(id, userId, command, cancellationToken);
            TempData["SuccessMessage"] = "Address updated successfully.";

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction(nameof(Index));
        }
        catch (DomainException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    [HttpPost("Delete/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromForm] string? returnUrl,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        try
        {
            await _addressService.DeleteAddressAsync(id, userId, cancellationToken);
            TempData["SuccessMessage"] = "Address deleted successfully.";
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Could not delete address.";
        }

        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("SetDefault/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetDefault(
        [FromRoute] Guid id,
        [FromForm] string? returnUrl,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        try
        {
            await _addressService.SetDefaultAsync(id, userId, cancellationToken);
            TempData["SuccessMessage"] = "Default address updated.";
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Could not update default address.";
        }

        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction(nameof(Index));
    }

    private Guid GetCurrentUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId))
        {
            throw new InvalidOperationException("User ID claim is missing or invalid.");
        }
        return userId;
    }
}
