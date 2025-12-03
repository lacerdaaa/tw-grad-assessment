namespace Core.Domain;

public class Delivery
{
    public int? TimeInMinutes { get; set; }
    public double Distance { get; set; }

    public Delivery()
    {
        TimeInMinutes = null;
        Distance = 0;
    }

    public Delivery(int timeInMinutes, double distance)
    {
        if (timeInMinutes < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(timeInMinutes));
        }

        if (distance < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(distance));
        }

        TimeInMinutes = timeInMinutes;
        Distance = distance;
    }
}
