using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.Catalog.Interfaces;
using Dreamflykit.Application.StudyHub.Interfaces;
using Dreamflykit.Application.StudyHub.Models;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.StudyHub;

public sealed class StudyHubService : IStudyHubService
{
    private const string HubSlug = "study-essentials";
    private const string HubTitle = "Study Essentials Hub";
    private const string FallbackFileName = "study_essentials_hub.json";

    private readonly DreamflykitDbContext _db;
    private readonly ICategoryHubRepository _hubRepository;
    private readonly ILogger<StudyHubService> _logger;

    [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
    public StudyHubService(
        DreamflykitDbContext db,
        ICategoryHubRepository hubRepository,
        ILogger<StudyHubService> logger)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _hubRepository = hubRepository ?? throw new ArgumentNullException(nameof(hubRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public StudyHubService(
        DreamflykitDbContext db,
        Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
        ILogger<StudyHubService> logger)
        : this(db, new Dreamflykit.Infrastructure.Catalog.CategoryHubRepository(db, cache, env), logger)
    {
    }

    public async Task<StudyHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        var dto = await _hubRepository.GetPayloadAsync<StudyHubDto>(HubSlug, FallbackFileName, cancellationToken);
        if (dto != null)
        {
            EnsureDefaults(dto);
            return dto;
        }

        var defaultDto = StudyHubDto.CreateDefault();
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, defaultDto, cancellationToken);
        return defaultDto;
    }

    public async Task UpdateConfigAsync(StudyHubDto dto, Guid updatedBy, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (dto.HeroBanner != null)
        {
            if (dto.HeroBanner.AutoSlideIntervalSeconds <= 0)
                dto.HeroBanner.AutoSlideIntervalSeconds = 4;

            if (dto.HeroBanner.Slides != null && dto.HeroBanner.Slides.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(dto.HeroBanner.ImageUrl) || dto.HeroBanner.ImageUrl == "/images/study-hub/test-pic-placeholder.svg")
                    dto.HeroBanner.ImageUrl = dto.HeroBanner.Slides[0].ImageUrl;
                if (string.IsNullOrWhiteSpace(dto.HeroBanner.SecondaryBadge))
                    dto.HeroBanner.SecondaryBadge = dto.HeroBanner.Slides[0].SecondaryBadge;
            }
        }

        // 1. Persist full configuration into PostgreSQL & mirror file via ICategoryHubRepository
        await _hubRepository.SavePayloadAsync(HubSlug, HubTitle, FallbackFileName, dto, cancellationToken);

        // 2. Keep HomepageSection in DB in sync with safe length strings (< 200 / < 300)
        try
        {
            var safeTitle = dto.Section1?.Title ?? "Study Essentials Hub";
            if (safeTitle.Length > 190) safeTitle = safeTitle[..190];

            var safeSubtitle = dto.Section1?.Subtitle ?? "Curated study kits and student abroad essentials.";
            if (safeSubtitle.Length > 290) safeSubtitle = safeSubtitle[..290];

            var section = await _db.HomepageSections
                .FirstOrDefaultAsync(s => s.SectionKey == HomepageSectionKey.StudyEssentialsHub, cancellationToken);

            if (section == null)
            {
                section = new HomepageSection(
                    HomepageSectionKey.StudyEssentialsHub,
                    safeTitle,
                    13,
                    updatedBy);

                section.Update(
                    safeTitle,
                    safeSubtitle,
                    "/category/study-essentials",
                    13,
                    null,
                    null,
                    null,
                    updatedBy);

                section.Activate();
                section.Publish(updatedBy);
                _db.HomepageSections.Add(section);
            }
            else
            {
                section.Update(
                    safeTitle,
                    safeSubtitle,
                    "/category/study-essentials",
                    section.DisplayOrder,
                    section.MediaAssetId,
                    section.StartDateUtc,
                    section.EndDateUtc,
                    updatedBy);

                section.Activate();
                section.Publish(updatedBy);
            }

            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed updating HomepageSection metadata in DB");
        }

        _logger.LogInformation("Study Essentials Hub configuration updated in PostgreSQL by {UserId}", updatedBy);
    }

    private static void EnsureDefaults(StudyHubDto dto)
    {
        var def = StudyHubDto.CreateDefault();
        if (dto.HeroBanner == null || string.IsNullOrWhiteSpace(dto.HeroBanner.Title))
        {
            dto.HeroBanner = def.HeroBanner;
        }
        else
        {
            if (dto.HeroBanner.AutoSlideIntervalSeconds <= 0)
                dto.HeroBanner.AutoSlideIntervalSeconds = 4;
            if (dto.HeroBanner.Slides == null || dto.HeroBanner.Slides.Count == 0)
                dto.HeroBanner.Slides = def.HeroBanner.Slides;
        }
        if (dto.TrustItems == null || dto.TrustItems.Count == 0)
            dto.TrustItems = def.TrustItems;
        if (dto.Section1 == null || dto.Section1.Cards.Count == 0)
            dto.Section1 = def.Section1;
        else
        {
            foreach (var card in dto.Section1.Cards)
            {
                if (!string.IsNullOrWhiteSpace(card.LinkUrl) && card.LinkUrl.Contains("//"))
                {
                    card.LinkUrl = card.LinkUrl.Replace("/category/study-essentials//category/study-essentials/", "/category/study-essentials/").Replace("//", "/");
                }
            }
        }
        if (dto.Section2 == null || dto.Section2.Kits.Count == 0)
            dto.Section2 = def.Section2;
        if (dto.Section3Detail == null || dto.Section3Detail.Cards.Count == 0 || (dto.Section3Detail.Title != null && dto.Section3Detail.Title.Contains("Key Study Essentials")))
            dto.Section3Detail = def.Section3Detail;
        if (dto.Section4 == null || dto.Section4.Items.Count == 0)
            dto.Section4 = def.Section4;
        if (dto.Section5 == null || dto.Section5.Countries.Count == 0)
            dto.Section5 = def.Section5;
        if (dto.Section6 == null || dto.Section6.Items.Count == 0)
            dto.Section6 = def.Section6;
        if (dto.Section7 == null || dto.Section7.Products.Count == 0)
            dto.Section7 = def.Section7;
        if (dto.Section8 == null || dto.Section8.Items.Count == 0)
            dto.Section8 = def.Section8;
        if (dto.Section9 == null || dto.Section9.Pillars.Count == 0)
            dto.Section9 = def.Section9;
        if (dto.Section10 == null || dto.Section10.Faqs.Count == 0)
            dto.Section10 = def.Section10;
        if (dto.BottomBanner == null || string.IsNullOrWhiteSpace(dto.BottomBanner.Title))
            dto.BottomBanner = def.BottomBanner;
        if (dto.CategoriesPage == null || dto.CategoriesPage.Categories.Count == 0 || dto.CategoriesPage.Categories.Count > 6)
        {
            dto.CategoriesPage = def.CategoriesPage;
        }
        else
        {
            foreach (var cat in dto.CategoriesPage.Categories)
            {
                var defCat = def.CategoriesPage.Categories.FirstOrDefault(c => string.Equals(c.Slug, cat.Slug, StringComparison.OrdinalIgnoreCase));
                if (defCat != null)
                {
                    if (cat.Subcategories == null || cat.Subcategories.Count == 0)
                    {
                        cat.Subcategories = defCat.Subcategories;
                    }
                    if (string.IsNullOrWhiteSpace(cat.ImportantNote))
                    {
                        cat.ImportantNote = defCat.ImportantNote;
                    }
                }
            }
        }
        if (dto.Section3 == null || dto.Section3.Items.Count == 0)
            dto.Section3 = def.Section3;
        if (dto.ChecklistPage == null || dto.ChecklistPage.Categories.Count == 0)
            dto.ChecklistPage = def.ChecklistPage;
        if (dto.EducationLoansPage == null || string.IsNullOrWhiteSpace(dto.EducationLoansPage.HeroTitle))
            dto.EducationLoansPage = def.EducationLoansPage;
        if (dto.ConsultancyDetailsPage == null || string.IsNullOrWhiteSpace(dto.ConsultancyDetailsPage.HeroHeadline))
            dto.ConsultancyDetailsPage = def.ConsultancyDetailsPage;
        dto.ConsultancyDetailsPage.EnsureDefaults();
        if (dto.AbroadProfile == null || dto.AbroadProfile.Services.Count == 0)
            dto.AbroadProfile = def.AbroadProfile;
        if (dto.AbroadProfile.ConsultancyDetailsPage == null)
            dto.AbroadProfile.ConsultancyDetailsPage = dto.ConsultancyDetailsPage;
        else
            dto.AbroadProfile.ConsultancyDetailsPage.EnsureDefaults();
        if (dto.AbroadProfile.EducationLoansPage == null)
            dto.AbroadProfile.EducationLoansPage = dto.EducationLoansPage;
        if (dto.GuidePages == null || dto.GuidePages.Count == 0)
            dto.GuidePages = def.GuidePages;

        if (dto.Section3Detail?.Cards != null)
        {
            foreach (var card in dto.Section3Detail.Cards)
            {
                if (card.Id == "abroad-study-ready" && (string.IsNullOrWhiteSpace(card.LinkUrl) || card.LinkUrl.Contains("category")))
                    card.LinkUrl = "/study-guide/stay-study-ready";
                else if (card.Id == "abroad-connected" && (string.IsNullOrWhiteSpace(card.LinkUrl) || card.LinkUrl.Contains("category")))
                    card.LinkUrl = "/study-guide/stay-connected";
                else if (card.Id == "abroad-documents" && (string.IsNullOrWhiteSpace(card.LinkUrl) || card.LinkUrl.Contains("category")))
                    card.LinkUrl = "/study-guide/keep-documents-organized";
                else if (card.Id == "abroad-student-life" && (string.IsNullOrWhiteSpace(card.LinkUrl) || card.LinkUrl.Contains("category")))
                    card.LinkUrl = "/study-guide/set-up-your-student-life";
            }
        }
    }
}
