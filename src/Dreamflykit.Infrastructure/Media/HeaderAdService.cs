using Dreamflykit.Application.Media.Interfaces;
using Dreamflykit.Application.Media.Models;
using Dreamflykit.Domain.Media;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Media;

public sealed class HeaderAdService : IHeaderAdService
{
    private readonly DreamflykitDbContext _db;
    private readonly IMediaUrlService _urlService;

    public HeaderAdService(
        DreamflykitDbContext db,
        IMediaUrlService urlService)
    {
        _db = db;
        _urlService = urlService;
    }

    public async Task<IReadOnlyList<HeaderAdDto>> GetActiveHeaderAdsAsync(CancellationToken cancellationToken = default)
    {
        var sections = await _db.HomepageSections
            .AsNoTracking()
            .Where(s => (s.SectionKey == HomepageSectionKey.HeaderAdSlot1 || s.SectionKey == HomepageSectionKey.HeaderAdSlot2)
                        && s.IsActive && s.IsPublished)
            .OrderBy(s => s.SectionKey)
            .ToListAsync(cancellationToken);

        var mediaIds = sections
            .Where(s => s.MediaAssetId.HasValue)
            .Select(s => s.MediaAssetId!.Value)
            .Distinct()
            .ToList();

        var mediaMap = mediaIds.Count > 0
            ? await _db.MediaAssets
                .AsNoTracking()
                .Where(m => mediaIds.Contains(m.Id))
                .ToDictionaryAsync(m => m.Id, m => _urlService.GetUrl(m.ObjectKey), cancellationToken)
            : new Dictionary<Guid, string>();

        var sec1 = sections.FirstOrDefault(s => s.SectionKey == HomepageSectionKey.HeaderAdSlot1);
        var sec2 = sections.FirstOrDefault(s => s.SectionKey == HomepageSectionKey.HeaderAdSlot2);

        var result = new List<HeaderAdDto>();

        // Slot 1
        if (sec1 != null)
        {
            var imgUrl = sec1.MediaAssetId.HasValue && mediaMap.TryGetValue(sec1.MediaAssetId.Value, out var url)
                ? url
                : (!string.IsNullOrWhiteSpace(sec1.Subtitle) && sec1.Subtitle.StartsWith("http") ? sec1.Subtitle : null);

            result.Add(new HeaderAdDto
            {
                SlotNumber = 1,
                Title = sec1.Title,
                Subtitle = sec1.Subtitle,
                ImageUrl = imgUrl,
                RedirectUrl = SanitizeRedirectUrl(sec1.LinkUrl, "/Kits/FindMyKit"),
                IsActive = sec1.IsActive
            });
        }
        else
        {
            result.Add(new HeaderAdDto
            {
                SlotNumber = 1,
                Title = "Student Flight & Travel Kits",
                Subtitle = "Save 25% on Complete Bundles",
                ImageUrl = null,
                RedirectUrl = "/Kits/FindMyKit",
                IsActive = true
            });
        }

        // Slot 2
        if (sec2 != null)
        {
            var imgUrl = sec2.MediaAssetId.HasValue && mediaMap.TryGetValue(sec2.MediaAssetId.Value, out var url)
                ? url
                : (!string.IsNullOrWhiteSpace(sec2.Subtitle) && sec2.Subtitle.StartsWith("http") ? sec2.Subtitle : null);

            result.Add(new HeaderAdDto
            {
                SlotNumber = 2,
                Title = sec2.Title,
                Subtitle = sec2.Subtitle,
                ImageUrl = imgUrl,
                RedirectUrl = SanitizeRedirectUrl(sec2.LinkUrl, "/category/adapters-sim"),
                IsActive = sec2.IsActive
            });
        }
        else
        {
            result.Add(new HeaderAdDto
            {
                SlotNumber = 2,
                Title = "International SIM & Adapters",
                Subtitle = "Pre-activated 5G abroad SIM",
                ImageUrl = null,
                RedirectUrl = "/category/adapters-sim",
                IsActive = true
            });
        }

        return result;
    }

    private static string SanitizeRedirectUrl(string? rawUrl, string fallback)
    {
        if (string.IsNullOrWhiteSpace(rawUrl))
        {
            return fallback;
        }

        var trimmed = rawUrl.Trim();

        // Disallow javascript: or data: or file: pseudo-schemes
        if (trimmed.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("data:", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("vbscript:", StringComparison.OrdinalIgnoreCase))
        {
            return fallback;
        }

        // Allow relative URLs starting with /
        if (trimmed.StartsWith('/') && !trimmed.StartsWith("//"))
        {
            return trimmed;
        }

        // Allow safe absolute URLs
        if (Uri.TryCreate(trimmed, UriKind.Absolute, out var uri) &&
            (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
        {
            return trimmed;
        }

        return fallback;
    }
}
