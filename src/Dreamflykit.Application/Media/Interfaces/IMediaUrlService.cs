namespace Dreamflykit.Application.Media.Interfaces;

/// <summary>
/// Interface for constructing CDN URLs from S3 object keys.
/// </summary>
public interface IMediaUrlService
{
    string GetUrl(string objectKey);
    string GetThumbnailUrl(string objectKey);
    string GetCardUrl(string objectKey);
    string GetDetailUrl(string objectKey);
}
