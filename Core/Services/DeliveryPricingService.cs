namespace Core.Services;

public class DeliveryPricingService
{
    private readonly double _baseCost;
    private readonly double _baseDistanceKm;
    private readonly double _additionalCostPerKm;

    public DeliveryPricingService(double baseCost = 50.0, double baseDistanceKm = 5.0, double additionalCostPerKm = 10.0)
    {
        if (baseCost < 0) throw new ArgumentOutOfRangeException(nameof(baseCost));
        if (baseDistanceKm <= 0) throw new ArgumentOutOfRangeException(nameof(baseDistanceKm));
        if (additionalCostPerKm < 0) throw new ArgumentOutOfRangeException(nameof(additionalCostPerKm));

        _baseCost = baseCost;
        _baseDistanceKm = baseDistanceKm;
        _additionalCostPerKm = additionalCostPerKm;
    }

    public double Calculate(double distanceKm)
    {
        if (distanceKm < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(distanceKm), "Distance must be non-negative");
        }

        if (distanceKm <= _baseDistanceKm)
        {
            return _baseCost;
        }

        var extraDistance = distanceKm - _baseDistanceKm;
        return _baseCost + (extraDistance * _additionalCostPerKm);
    }
}
