using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

[Route("study-abroad-hub")]
[Route("StudyAbroadHub")]
public sealed class StudyAbroadHubController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        ViewData["Title"] = "Study Abroad Hub — Dreamflykit";
        return View();
    }
}
