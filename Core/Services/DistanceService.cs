using Core.Domain;
using StaticDataSource = Core.StaticData.StaticData;

namespace Core.Services;

public class DistanceService
{
    public int GetDistanceInKm(string fromZone, string toZone)
    {
        if (string.IsNullOrWhiteSpace(fromZone))
        {
            throw new ArgumentException("From zone is required", nameof(fromZone));
        }

        if (string.IsNullOrWhiteSpace(toZone))
        {
            throw new ArgumentException("To zone is required", nameof(toZone));
        }

        var match = StaticDataSource.DistanceMap.FirstOrDefault(map =>
            string.Equals(map.ZoneFrom, fromZone, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(map.ZoneTo, toZone, StringComparison.OrdinalIgnoreCase));

        return match?.Distance ?? throw new InvalidOperationException($"No distance found from {fromZone} to {toZone}");
    }
}
