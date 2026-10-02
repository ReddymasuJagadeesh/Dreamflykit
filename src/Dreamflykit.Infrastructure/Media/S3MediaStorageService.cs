using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Dreamflykit.Application.Media.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dreamflykit.Infrastructure.Media;

/// <summary>
/// AWS S3 implementation of IMediaStorageService.
/// All S3 SDK dependencies are contained entirely within this class.
/// No S3 types leak into Application or Domain layers.
/// </summary>
public sealed class S3MediaStorageService : IMediaStorageService, IDisposable
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly ILogger<S3MediaStorageService> _logger;

    public S3MediaStorageService(
        IOptions<S3Options> options,
        ILogger<S3MediaStorageService> logger)
    {
        _logger = logger;
        var s3Opts = options.Value;
        _bucketName = s3Opts.BucketName;

        var region = !string.IsNullOrWhiteSpace(s3Opts.Region)
            ? RegionEndpoint.GetBySystemName(s3Opts.Region)
            : RegionEndpoint.APSouth1;

        if (!string.IsNullOrWhiteSpace(s3Opts.AccessKeyId) && !string.IsNullOrWhiteSpace(s3Opts.SecretAccessKey))
        {
            var credentials = new BasicAWSCredentials(s3Opts.AccessKeyId, s3Opts.SecretAccessKey);
            _s3Client = new AmazonS3Client(credentials, region);
        }
        else
        {
            // Fallback to standard AWS SDK credential chain (env vars, IAM role, or ~/.aws credentials)
            _s3Client = new AmazonS3Client(region);
        }
    }

    /// <inheritdoc />
    public async Task<bool> PutObjectAsync(
        string objectKey,
        Stream stream,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = objectKey,
                InputStream = stream,
                ContentType = contentType,
                // Enforce private — never use CannedACL.PublicRead
                CannedACL = S3CannedACL.Private,
                ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256
            };

            var response = await _s3Client.PutObjectAsync(request, cancellationToken);

            if ((int)response.HttpStatusCode >= 200 && (int)response.HttpStatusCode < 300)
            {
                _logger.LogInformation(
                    "Successfully uploaded object {ObjectKey} to S3 bucket {BucketName}",
                    objectKey, _bucketName);

                return true;
            }

            _logger.LogWarning(
                "S3 PutObject returned unexpected status {StatusCode} for key {ObjectKey}",
                response.HttpStatusCode, objectKey);

            return false;
        }
        catch (AmazonS3Exception ex)
        {
            _logger.LogError(ex,
                "S3 error uploading object {ObjectKey}: {ErrorCode} - {Message}",
                objectKey, ex.ErrorCode, ex.Message);

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Unexpected error uploading object {ObjectKey} to S3",
                objectKey);

            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> ObjectExistsAsync(
        string objectKey,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new GetObjectMetadataRequest
            {
                BucketName = _bucketName,
                Key = objectKey
            };

            await _s3Client.GetObjectMetadataAsync(request, cancellationToken);

            return true;
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error checking object existence for key {ObjectKey}",
                objectKey);

            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteObjectAsync(
        string objectKey,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = objectKey
            };

            await _s3Client.DeleteObjectAsync(request, cancellationToken);

            _logger.LogInformation(
                "Deleted S3 object {ObjectKey} from bucket {BucketName}",
                objectKey, _bucketName);

            return true;
        }
        catch (AmazonS3Exception ex)
        {
            _logger.LogError(ex,
                "S3 error deleting object {ObjectKey}: {ErrorCode} - {Message}",
                objectKey, ex.ErrorCode, ex.Message);

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Unexpected error deleting object {ObjectKey}",
                objectKey);

            return false;
        }
    }

    /// <inheritdoc />
    public Task<string> GeneratePresignedPutUrlAsync(
        string objectKey,
        string contentType,
        int expirySeconds,
        CancellationToken cancellationToken = default)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = objectKey,
            Verb = HttpVerb.PUT,
            ContentType = contentType,
            Expires = DateTime.UtcNow.AddSeconds(expirySeconds),
            Protocol = Protocol.HTTPS
        };

        var url = _s3Client.GetPreSignedURL(request);

        _logger.LogInformation(
            "Generated pre-signed PUT URL for key {ObjectKey}, expires in {Seconds}s",
            objectKey, expirySeconds);

        return Task.FromResult(url);
    }

    public void Dispose()
    {
        _s3Client.Dispose();
    }
}
