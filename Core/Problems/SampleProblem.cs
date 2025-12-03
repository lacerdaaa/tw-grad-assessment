using Core.Services;

namespace Core.Problems;

public class SampleProblem
{
    public static void Run()
    {
        double cost = CalculateCost(8.0);
        Console.WriteLine($"Delivery cost for 8 km: ₹{cost}");
    }

    public static double CalculateCost(double distanceKm)
    {
        var pricingService = new DeliveryPricingService();
        return pricingService.Calculate(distanceKm);
    }
}
