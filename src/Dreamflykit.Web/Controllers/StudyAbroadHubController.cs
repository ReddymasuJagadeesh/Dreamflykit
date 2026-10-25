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

    [HttpGet("checklist")]
    [HttpGet("abroad-packing-checklist")]
    public IActionResult RedirectToChecklist()
    {
        return RedirectPermanent("/abroad-packing-checklist");
    }

    [HttpGet("/abroad-packing-checklist")]
    [HttpGet("/checklist")]
    [HttpGet("/packing-checklist")]
    public async Task<IActionResult> Checklist(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Abroad Packing Checklist — Dreamflykit";
        var config = await _studyHubService.GetConfigAsync(cancellationToken);
        var page = config.ChecklistPage ?? Application.StudyHub.Models.StudyHubDto.CreateDefaultChecklistPage();
        return View("Checklist", page);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> ServiceDetail(string slug, CancellationToken cancellationToken)
    {
        var config = await _studyHubService.GetConfigAsync(cancellationToken);
        ViewData["Slug"] = slug;
        return View("ServiceDetail", config);
    }
}
