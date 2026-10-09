namespace Dreamflykit.Application.Media.Interfaces;

/// <summary>
/// Abstraction over AWS S3 operations.
/// Infrastructure provides the concrete implementation using AWSSDK.S3.
/// No S3 SDK types leak into the Application layer through this interface.
/// </summary>
public interface IMediaStorageService
{
    /// <summary>
    /// The last error message encountered during a storage operation, if any.
    /// </summary>
    string? LastError { get; }

    /// <summary>
    /// Uploads a file stream to S3 at the given object key.
    /// </summary>
    /// <param name="objectKey">S3 key (e.g., media/products/{ownerId}/{mediaId}.webp)</param>
    /// <param name="stream">File content stream.</param>
    /// <param name="contentType">MIME type of the file.</param>
    /// <returns>True on success.</returns>
    Task<bool> PutObjectAsync(
        string objectKey,
        Stream stream,
        string contentType,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns true if an object with the given key exists in S3.
    /// Used to verify direct uploads completed successfully.
    /// </summary>
    Task<bool> ObjectExistsAsync(
        string objectKey,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Permanently deletes an object from S3.
    /// Called when a MediaAsset is hard-deleted.
    /// </summary>
    Task<bool> DeleteObjectAsync(
        string objectKey,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates a pre-signed URL allowing a client to PUT a file directly to S3
    /// without going through the ASP.NET server.
    /// </summary>
    /// <param name="objectKey">The S3 key the client will upload to.</param>
    /// <param name="contentType">Enforce content type on the PUT.</param>
    /// <param name="expirySeconds">How long the URL is valid.</param>
    Task<string> GeneratePresignedPutUrlAsync(
        string objectKey,
        string contentType,
        int expirySeconds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a file stream from S3.
    /// </summary>
    Task<Dreamflykit.Application.Media.Models.StorageObjectResult?> GetObjectAsync(
        string objectKey,
        CancellationToken cancellationToken = default);
}
