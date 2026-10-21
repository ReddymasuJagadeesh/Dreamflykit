using System.Text.Encodings.Web;
using System.Text.Json;
using Dreamflykit.Application.StudyHub.Interfaces;
using Dreamflykit.Application.StudyHub.Models;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.StudyHub;

public sealed class StudyHubService : IStudyHubService
{
    private const string CacheKey = "study_essentials_hub_cache";
    private readonly DreamflykitDbContext _db;
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<StudyHubService> _logger;
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public StudyHubService(
        DreamflykitDbContext db,
        IMemoryCache cache,
        IWebHostEnvironment env,
        ILogger<StudyHubService> logger)
    {
        _db = db;
        _cache = cache;
        _env = env;
        _logger = logger;

        var rootPath = !string.IsNullOrWhiteSpace(env.WebRootPath)
            ? env.WebRootPath
            : Path.Combine(AppContext.BaseDirectory, "wwwroot");

        var dataDir = Path.Combine(rootPath, "data");
        if (!Directory.Exists(dataDir))
        {
            try
            {
                Directory.CreateDirectory(dataDir);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not create directory: {DataDir}", dataDir);
            }
        }

        _filePath = Path.Combine(dataDir, "study_essentials_hub.json");
    }

    public async Task<StudyHubDto> GetConfigAsync(CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(CacheKey, out StudyHubDto? cached) && cached != null)
        {
            return cached;
        }

        // 1. Try reading from persistent json configuration
        if (File.Exists(_filePath))
        {
            try
            {
                var json = await File.ReadAllTextAsync(_filePath, cancellationToken);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var dto = JsonSerializer.Deserialize<StudyHubDto>(json, JsonOptions);
                    if (dto != null)
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
                        if (dto.CategoriesPage == null || dto.CategoriesPage.Categories.Count == 0)
                            dto.CategoriesPage = def.CategoriesPage;
                        if (dto.Section3 == null || dto.Section3.Items.Count == 0)
                            dto.Section3 = def.Section3;
                        if (dto.ChecklistPage == null || dto.ChecklistPage.Categories.Count == 0)
                            dto.ChecklistPage = def.ChecklistPage;
                        if (dto.AbroadProfile == null || dto.AbroadProfile.Services.Count == 0)
                            dto.AbroadProfile = def.AbroadProfile;

                        _cache.Set(CacheKey, dto, TimeSpan.FromHours(4));
                        return dto;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed reading study essentials config from {FilePath}", _filePath);
            }
        }

        // 2. Fallback to default template
        var defaultDto = StudyHubDto.CreateDefault();
        try
        {
            var defaultJson = JsonSerializer.Serialize(defaultDto, JsonOptions);
            await File.WriteAllTextAsync(_filePath, defaultJson, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not write initial study essentials json to {FilePath}", _filePath);
        }

        _cache.Set(CacheKey, defaultDto, TimeSpan.FromHours(4));
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

        // 1. Persist full configuration into JSON file
        try
        {
            var json = JsonSerializer.Serialize(dto, JsonOptions);
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            await File.WriteAllTextAsync(_filePath, json, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to write Study Essentials Hub config to {FilePath}", _filePath);
            throw new InvalidOperationException("Failed to persist Study Hub configuration to storage: " + ex.Message, ex);
        }

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
            _logger.LogWarning(ex, "Failed updating HomepageSection metadata in DB (file storage succeeded)");
        }

        // Invalidate cache immediately
        _cache.Remove(CacheKey);
        _logger.LogInformation("Study Essentials Hub configuration updated by {UserId}", updatedBy);
    }
}
