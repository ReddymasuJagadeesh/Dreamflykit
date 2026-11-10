using Dreamflykit.Application.Shipping.Interfaces;
using Dreamflykit.Application.Shipping.Models;
using Microsoft.Extensions.Logging;

namespace Dreamflykit.Infrastructure.Shipping;

public sealed class DefaultShippingService : IShippingService
{
    private readonly ILogger<DefaultShippingService> _logger;

    public DefaultShippingService(ILogger<DefaultShippingService> logger)
    {
        _logger = logger;
    }

    public Task<ShippingRateQuote> CalculateShippingRateAsync(
        ShippingRateRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Standard Indian e-commerce shipping rule: Free delivery above Rs. 999
        bool isFree = request.OrderSubtotal >= 999m;
        decimal baseFee = isFree ? 0m : 99m;

        if (request.IsExpress)
        {
            baseFee += 100m;
        }

        var quote = new ShippingRateQuote
        {
            CarrierName = request.IsExpress ? "Dreamflykit Priority Air" : "Dreamflykit Surface Standard",
            ShippingFee = baseFee,
            IsFreeShipping = isFree && !request.IsExpress,
            EstimatedDeliveryDaysMin = request.IsExpress ? 1 : 3,
            EstimatedDeliveryDaysMax = request.IsExpress ? 2 : 5
        };

        return Task.FromResult(quote);
    }

    public Task<ShipmentTrackingInfo> GetTrackingInfoAsync(
        string trackingNumber,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(trackingNumber);

        var now = DateTime.UtcNow;
        var info = new ShipmentTrackingInfo
        {
            TrackingNumber = trackingNumber,
            CarrierName = "Dreamflykit Express Logistics",
            CurrentStatus = ShipmentStatus.InTransit,
            EstimatedDeliveryUtc = now.AddDays(2),
            Checkpoints = new List<TrackingCheckpoint>
            {
                new()
                {
                    TimestampUtc = now.AddHours(-36),
                    Location = "Bangalore Central Hub",
                    Description = "Order packed and manifest generated.",
                    Status = ShipmentStatus.Packed
                },
                new()
                {
                    TimestampUtc = now.AddHours(-24),
                    Location = "Bangalore Airport Sorting Facility",
                    Description = "Package arrived at sorting facility and dispatched.",
                    Status = ShipmentStatus.Shipped
                },
                new()
                {
                    TimestampUtc = now.AddHours(-4),
                    Location = "Regional Transit Hub",
                    Description = "Package in transit to destination delivery station.",
                    Status = ShipmentStatus.InTransit
                }
            }
        };

        return Task.FromResult(info);
    }

    public Task<string> GenerateWaybillNumberAsync(
        string orderNumber,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(orderNumber);

        var random = Random.Shared.Next(1000, 9999);
        var waybill = $"DFK-{orderNumber.TrimStart('#')}-{random}";
        _logger.LogInformation("Generated waybill {WaybillNumber} for Order {OrderNumber}", waybill, orderNumber);

        return Task.FromResult(waybill);
    }
}

