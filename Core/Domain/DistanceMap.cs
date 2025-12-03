namespace Core.Domain;

public class DistanceMap
{
    public string ZoneTo { get; set; } = string.Empty;
    public string ZoneFrom { get; set; } = string.Empty;
    public int Distance { get; set; }

    public DistanceMap()
    {
    }

    public DistanceMap(string zoneTo, string zoneFrom, int distance)
    {
        ZoneTo = zoneTo;
        ZoneFrom = zoneFrom;
        Distance = distance;
    }
}
