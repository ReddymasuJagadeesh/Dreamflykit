using Dreamflykit.Web.Models.Info;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

public sealed class InfoController : Controller
{
    [HttpGet("About")]
    public IActionResult About()
    {
        return View();
    }

    [HttpGet("Contact")]
    public IActionResult Contact()
    {
        return View(new ContactFormViewModel());
    }

    [HttpPost("Contact")]
    [ValidateAntiForgeryToken]
    public IActionResult Contact([FromForm] ContactFormViewModel model)
    {
        // Bot honeypot protection: if filled, quietly succeed without sending
        if (!string.IsNullOrWhiteSpace(model.WebsiteUrlHoneypot))
        {
            TempData["SuccessMessage"] = "Thank you! Your message has been received.";
            return RedirectToAction(nameof(Contact));
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        TempData["SuccessMessage"] = $"Thank you, {model.FullName}! Your message regarding \"{model.Subject}\" has been dispatched to our student support desk. An advisor will respond within 4 hours.";
        return RedirectToAction(nameof(Contact));
    }

    [HttpGet("Help")]
    public IActionResult Help()
    {
        return View();
    }

    [HttpGet("shipping-rules")]
    public IActionResult ShippingRules()
    {
        return View();
    }

    [HttpGet("Consultancy")]
    public IActionResult Consultancy()
    {
        return View(new ConsultancyBookingViewModel());
    }

    [HttpPost("Consultancy")]
    [ValidateAntiForgeryToken]
    public IActionResult Consultancy([FromForm] ConsultancyBookingViewModel model)
    {
        if (!string.IsNullOrWhiteSpace(model.Honeypot))
        {
            TempData["SuccessMessage"] = "Your consultation slot has been reserved!";
            return RedirectToAction(nameof(Consultancy));
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        TempData["SuccessMessage"] = $"Great! Your 1-on-1 Pre-Departure Kit Session for {model.DestinationCountry} ({model.TargetUniversity}) has been requested. An expert travel counselor will contact you via WhatsApp / Call at {model.PhoneNumber}.";
        return RedirectToAction(nameof(Consultancy));
    }

    [HttpGet("EducationLoans")]
    public IActionResult EducationLoans()
    {
        return View();
    }
}
