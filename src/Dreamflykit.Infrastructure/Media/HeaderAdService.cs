using System.Text.Json;
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

    public async Task<HeaderAdDto?> GetActiveHeaderAdSpaceAsync(CancellationToken cancellationToken = default)
    {
        // Try finding the dedicated HeaderAdSpace first
        var section = await _db.HomepageSections
            .AsNoTracking()
            .Where(s => s.SectionKey == HomepageSectionKey.HeaderAdSpace && s.IsActive && s.IsPublished)
            .FirstOrDefaultAsync(cancellationToken);

        // Fallback for transition if HeaderAdSpace hasn't been saved yet but HeaderAdSlot1 exists
        if (section == null)
        {
            section = await _db.HomepageSections
                .AsNoTracking()
                .Where(s => s.SectionKey == HomepageSectionKey.HeaderAdSlot1 && s.IsActive && s.IsPublished)
                .FirstOrDefaultAsync(cancellationToken);
        }

        if (section == null)
        {
            return null;
        }

        string? imgUrl = null;
        if (section.MediaAssetId.HasValue)
        {
            var asset = await _db.MediaAssets
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == section.MediaAssetId.Value, cancellationToken);
            if (asset != null)
            {
                imgUrl = _urlService.GetUrl(asset.ObjectKey);
            }
        }

        var subtitle = section.Subtitle;
        var badgeText = "SPECIAL OFFER";
        var displayMode = "Hybrid";
        var openInNewTab = false;

        // Parse optional JSON metadata stored in Subtitle
        if (!string.IsNullOrWhiteSpace(subtitle) && subtitle.TrimStart().StartsWith("{"))
        {
            try
            {
                using var doc = JsonDocument.Parse(subtitle);
                var root = doc.RootElement;
                if (root.TryGetProperty("sub", out var subProp))
                {
                    subtitle = subProp.GetString();
                }
                if (root.TryGetProperty("badge", out var badgeProp))
                {
                    badgeText = badgeProp.GetString() ?? "SPECIAL OFFER";
                }
                if (root.TryGetProperty("mode", out var modeProp))
                {
                    displayMode = modeProp.GetString() ?? "Hybrid";
                }
                if (root.TryGetProperty("blank", out var blankProp))
                {
                    openInNewTab = blankProp.GetBoolean();
                }
                if (root.TryGetProperty("img", out var imgProp) && string.IsNullOrWhiteSpace(imgUrl))
                {
                    imgUrl = imgProp.GetString();
                }
            }
            catch
            {
                // Fallback to raw subtitle if parsing fails
            }
        }
        else if (!string.IsNullOrWhiteSpace(subtitle) && subtitle.StartsWith("/uploads/", StringComparison.OrdinalIgnoreCase))
        {
            // If local upload path was directly saved in subtitle
            imgUrl = subtitle;
            subtitle = null;
        }

        var redirectUrl = SanitizeRedirectUrl(section.LinkUrl, "/SpecialOffer");
        if (redirectUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        {
            openInNewTab = true;
        }

        return new HeaderAdDto
        {
            SlotNumber = 1,
            Title = string.IsNullOrWhiteSpace(section.Title) ? "Special Partner Offer" : section.Title,
            Subtitle = subtitle,
            BadgeText = badgeText,
            ImageUrl = imgUrl,
            RedirectUrl = redirectUrl,
            OpenInNewTab = openInNewTab,
            DisplayMode = displayMode,
            IsActive = section.IsActive
        };
    }

    public async Task<IReadOnlyList<HeaderAdDto>> GetActiveHeaderAdsAsync(CancellationToken cancellationToken = default)
    {
        var adSpace = await GetActiveHeaderAdSpaceAsync(cancellationToken);
        if (adSpace != null)
        {
            return new List<HeaderAdDto> { adSpace };
        }

        return Array.Empty<HeaderAdDto>();
    }

    public static string SanitizeRedirectUrl(string? rawUrl, string fallback)
    {
        if (string.IsNullOrWhiteSpace(rawUrl))
        {
            return fallback;
        }

        var trimmed = rawUrl.Trim();

        // Disallow dangerous pseudo-schemes
        if (trimmed.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("data:", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("vbscript:", StringComparison.OrdinalIgnoreCase) ||
            trimmed.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
        {
            return fallback;
        }

        // Allow relative URLs starting with / (disallow protocol-relative // or /\\)
        if (trimmed.StartsWith('/') && !trimmed.StartsWith("//") && !trimmed.StartsWith("/\\"))
        {
            return trimmed;
        }

        // Allow safe absolute URLs (http and https)
        if (Uri.TryCreate(trimmed, UriKind.Absolute, out var uri) &&
            (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
        {
            return trimmed;
        }

        // Auto-prefix https if user entered www.
        if (trimmed.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
        {
            return "https://" + trimmed;
        }

        return fallback;
    }
}
