using Core.Domain;

namespace Core.Services;

public class OrderPricingService
{
    private readonly DistanceService _distanceService;
    private readonly DeliveryPricingService _deliveryPricingService;

    public OrderPricingService(DistanceService distanceService, DeliveryPricingService deliveryPricingService)
    {
        _distanceService = distanceService ?? throw new ArgumentNullException(nameof(distanceService));
        _deliveryPricingService = deliveryPricingService ?? throw new ArgumentNullException(nameof(deliveryPricingService));
    }

    public PriceBreakdown Calculate(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }

        var distanceKm = _distanceService.GetDistanceInKm(order.Store.Zone, order.DestinationZone);
        var deliveryFee = _deliveryPricingService.Calculate(distanceKm);

        return new PriceBreakdown(order.Subtotal, order.Discount, deliveryFee, distanceKm);
    }
}

public record PriceBreakdown(double Subtotal, double Discount, double DeliveryFee, double DistanceKm)
{
    public double Total => Subtotal - Discount + DeliveryFee;
}
