using Dreamflykit.Application.StudyHub.Interfaces;
using Dreamflykit.Application.StudyHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dreamflykit.Web.Controllers;

[Route("study-guide")]
[Route("StudyGuide")]
public sealed class StudyGuideController : Controller
{
    private readonly IStudyHubService _studyHubService;

    public StudyGuideController(IStudyHubService studyHubService)
    {
        _studyHubService = studyHubService;
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> Detail(string slug, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return RedirectToAction("Index", "StudyAbroadHub");
        }

        var config = await _studyHubService.GetConfigAsync(cancellationToken);
        var guide = config.GuidePages?.FirstOrDefault(g => string.Equals(g.Slug, slug.Trim(), StringComparison.OrdinalIgnoreCase));

        if (guide == null)
        {
            guide = StudyHubDto.CreateDefaultGuidePages()
                .FirstOrDefault(g => string.Equals(g.Slug, slug.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        if (guide == null)
        {
            return NotFound();
        }

        ViewData["Title"] = $"{guide.Title} — Dreamflykit Guide";
        ViewData["Description"] = guide.Subtitle;
        return View("Detail", guide);
    }
}
