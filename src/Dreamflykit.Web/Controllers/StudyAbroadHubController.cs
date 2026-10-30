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

    [HttpGet("education-loans")]
    [HttpGet("/education-loans")]
    public async Task<IActionResult> EducationLoans(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Get Abroad Education Loan in 48 Hours — Dreamflykit";
        var config = await _studyHubService.GetConfigAsync(cancellationToken);
        var page = config.EducationLoansPage ?? Application.StudyHub.Models.StudyHubDto.CreateDefaultEducationLoansPage();
        page.EnsureDefaults();
        return View("EducationLoans", page);
    }

    [HttpPost("apply-loan")]
    [HttpPost("/apply-loan")]
    [IgnoreAntiforgeryToken]
    public IActionResult SubmitLoanApplication([FromBody] Application.StudyHub.Models.EducationLoanApplicationLeadDto lead)
    {
        if (lead == null || string.IsNullOrWhiteSpace(lead.FullName))
        {
            return Json(new { success = false, message = "Please enter your full name." });
        }

        if (string.IsNullOrWhiteSpace(lead.PhoneNumber) && string.IsNullOrWhiteSpace(lead.Email))
        {
            return Json(new { success = false, message = "Please provide your phone number or email address." });
        }

        // Return positive confirmation with immediate next steps
        return Json(new
        {
            success = true,
            message = $"Thank you {lead.FullName}! Your loan eligibility application has been registered. Our international education loan specialist will contact you on {(!string.IsNullOrWhiteSpace(lead.PhoneNumber) ? lead.PhoneNumber : lead.Email)} within 24 hours."
        });
    }

    [HttpGet("consultancy")]
    [HttpGet("consultancy-details")]
    [HttpGet("consultancies")]
    [HttpGet("/consultancy-details")]
    public async Task<IActionResult> ConsultancyDetails(CancellationToken cancellationToken)
    {
        ViewData["Title"] = "Study Abroad Consultancy & Global Career Programs — Dreamflykit";
        var config = await _studyHubService.GetConfigAsync(cancellationToken);
        var page = config.ConsultancyDetailsPage 
            ?? config.AbroadProfile?.ConsultancyDetailsPage 
            ?? Application.StudyHub.Models.StudyHubDto.CreateDefaultConsultancyDetailsPage();
        return View("ConsultancyDetails", page);
    }

    [HttpPost("consultancy-lead")]
    [HttpPost("/consultancy-lead")]
    [IgnoreAntiforgeryToken]
    public IActionResult SubmitConsultancyLead([FromBody] Application.StudyHub.Models.ConsultancyLeadDto lead)
    {
        if (lead == null || string.IsNullOrWhiteSpace(lead.FullName))
        {
            return Json(new { success = false, message = "Please enter your full name." });
        }

        if (string.IsNullOrWhiteSpace(lead.PhoneNumber) && string.IsNullOrWhiteSpace(lead.Email))
        {
            return Json(new { success = false, message = "Please provide your phone number or email address." });
        }

        return Json(new
        {
            success = true,
            message = $"Thank you, {lead.FullName}! Your free 1-on-1 study abroad consultation session has been reserved. Our senior international advisor will contact you on {(!string.IsNullOrWhiteSpace(lead.PhoneNumber) ? lead.PhoneNumber : lead.Email)} within 24 hours."
        });
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> ServiceDetail(string slug, CancellationToken cancellationToken)
    {
        var config = await _studyHubService.GetConfigAsync(cancellationToken);
        ViewData["Slug"] = slug;
        return View("ServiceDetail", config);
    }
}
