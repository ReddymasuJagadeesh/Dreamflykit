namespace Dreamflykit.Application.Media.Models;

/// <summary>
/// Represents a storage stream object retrieved from S3 or local cache.
/// </summary>
public sealed class StorageObjectResult : IAsyncDisposable, IDisposable
{
    public Stream Stream { get; }
    public string ContentType { get; }
    public string? ETag { get; }
    public long ContentLength { get; }

    public StorageObjectResult(Stream stream, string contentType, string? eTag, long contentLength)
    {
        Stream = stream;
        ContentType = contentType;
        ETag = eTag;
        ContentLength = contentLength;
    }

    public void Dispose() => Stream.Dispose();
    public ValueTask DisposeAsync() => Stream.DisposeAsync();
}
