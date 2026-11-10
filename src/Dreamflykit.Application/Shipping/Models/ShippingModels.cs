namespace Dreamflykit.Application.Shipping.Models;

public enum ShipmentStatus
{
    OrderPlaced,
    Packed,
    Shipped,
    InTransit,
    OutForDelivery,
    Delivered,
    Returned
}

public sealed class ShippingRateRequest
{
    public string DestinationPostalCode { get; init; } = string.Empty;
    public string DestinationCountry { get; init; } = "India";
    public decimal TotalWeightKg { get; init; } = 1.0m;
    public decimal OrderSubtotal { get; init; }
    public bool IsExpress { get; init; }
}

public sealed class ShippingRateQuote
{
    public string CarrierName { get; init; } = "Dreamflykit Express";
    public decimal ShippingFee { get; init; }
    public int EstimatedDeliveryDaysMin { get; init; }
    public int EstimatedDeliveryDaysMax { get; init; }
    public bool IsFreeShipping { get; init; }
    public string DeliveryEstimateText =>
        $"Estimated delivery in {EstimatedDeliveryDaysMin}-{EstimatedDeliveryDaysMax} business days";
}

public sealed class TrackingCheckpoint
{
    public DateTime TimestampUtc { get; init; }
    public string Location { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public ShipmentStatus Status { get; init; }
}

public sealed class ShipmentTrackingInfo
{
    public string TrackingNumber { get; init; } = string.Empty;
    public string CarrierName { get; init; } = string.Empty;
    public ShipmentStatus CurrentStatus { get; init; }
    public DateTime? EstimatedDeliveryUtc { get; init; }
    public IReadOnlyList<TrackingCheckpoint> Checkpoints { get; init; } = Array.Empty<TrackingCheckpoint>();
}

