using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

[Route("[controller]")]
[Route("Offer")]
public sealed class SpecialOfferController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return RedirectPermanent("/study-abroad-hub");
    }
}
