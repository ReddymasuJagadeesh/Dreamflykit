namespace Dreamflykit.Infrastructure.Media;

/// <summary>
/// AWS S3 configuration options.
/// Sensitive values (AccessKeyId, SecretAccessKey) must be stored in user secrets
/// or environment variables — NEVER in appsettings.json or source control.
/// </summary>
public sealed class S3Options
{
    public const string SectionName = "AwsS3";

    /// <summary>
    /// AWS S3 bucket name. Non-sensitive, set in appsettings.json via Media:BucketName.
    /// </summary>
    public string BucketName { get; set; } = string.Empty;

    /// <summary>
    /// AWS region code (e.g., ap-south-1).
    /// </summary>
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// AWS Access Key ID.
    /// Must be set via user secrets or environment variable ONLY.
    /// dotnet user-secrets set "AwsS3:AccessKeyId" "YOUR_KEY"
    /// </summary>
    public string AccessKeyId { get; set; } = string.Empty;

    /// <summary>
    /// AWS Secret Access Key.
    /// Must be set via user secrets or environment variable ONLY.
    /// dotnet user-secrets set "AwsS3:SecretAccessKey" "YOUR_SECRET"
    /// </summary>
    public string SecretAccessKey { get; set; } = string.Empty;

    /// <summary>
    /// AWS Session Token (required when using temporary credentials such as AWS Learner Lab, STS, or SSO).
    /// </summary>
    public string? SessionToken { get; set; }
}
