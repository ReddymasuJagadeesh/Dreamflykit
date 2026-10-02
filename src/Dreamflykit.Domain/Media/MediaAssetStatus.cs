namespace Dreamflykit.Domain.Media;

/// <summary>
/// Lifecycle status of a media asset through the publishing workflow.
/// </summary>
public enum MediaAssetStatus
{
    /// <summary>Record created but upload not yet started.</summary>
    Draft = 0,

    /// <summary>File uploaded to S3, awaiting processing.</summary>
    Uploaded = 1,

    /// <summary>Image optimization/variant generation in progress.</summary>
    Processing = 2,

    /// <summary>Processed and ready to be published.</summary>
    Ready = 3,

    /// <summary>Publicly visible on the customer website.</summary>
    Published = 4,

    /// <summary>Removed from public visibility but retained in storage.</summary>
    Archived = 5,

    /// <summary>Soft-deleted — not visible anywhere, pending physical cleanup.</summary>
    Deleted = 6,

    /// <summary>Upload or processing encountered an unrecoverable error.</summary>
    Failed = 7
}
