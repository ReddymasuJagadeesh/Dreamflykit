using Dreamflykit.Application.StudyHub.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

[Route("study-abroad-hub")]
[Route("StudyAbroadHub")]
public sealed class StudyAbroadHubController : Controller
{
    private readonly IStudyHubService _studyHubService;

    public StudyAbroadHubController(IStudyHubService studyHubService)
    {
        _studyHubService = studyHubService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Study Essentials & Abroad Hub — Dreamflykit";
        var model = await _studyHubService.GetConfigAsync(cancellationToken);
        return View(model);
    }
}
