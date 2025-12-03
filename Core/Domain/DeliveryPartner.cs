namespace Core.Domain;

public class DeliveryPartner
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<Delivery> Deliveries { get; set; } = new();

    public DeliveryPartner()
    {
    }

    public DeliveryPartner(string id, string name, List<Delivery> deliveries)
    {
        Id = id;
        Name = name;
        Deliveries = deliveries;
    }
}
