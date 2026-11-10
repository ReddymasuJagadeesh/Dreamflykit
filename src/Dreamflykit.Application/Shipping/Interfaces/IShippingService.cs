using Dreamflykit.Application.Shipping.Models;

namespace Dreamflykit.Application.Shipping.Interfaces;

public interface IShippingService
{
    Task<ShippingRateQuote> CalculateShippingRateAsync(
        ShippingRateRequest request,
        CancellationToken cancellationToken = default);

    Task<ShipmentTrackingInfo> GetTrackingInfoAsync(
        string trackingNumber,
        CancellationToken cancellationToken = default);

    Task<string> GenerateWaybillNumberAsync(
        string orderNumber,
        CancellationToken cancellationToken = default);
}

