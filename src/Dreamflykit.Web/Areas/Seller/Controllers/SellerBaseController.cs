using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Areas.Seller.Controllers;

[Area("Seller")]
[Authorize(Roles = "Seller")]
public abstract class SellerBaseController : Controller
{
    protected Guid CurrentUserId
    {
        get
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(idClaim, out var id) ? id : Guid.Empty;
        }
    }
}
