namespace Core.Domain;

public class Store
{
    public string Id { get; set; } = string.Empty;
    public string Zone { get; set; } = string.Empty;
    public List<Item> Items { get; set; } = new();
    
    public Store() {}

    public Store(string id, string zone, List<Item> items)
    {
        Id = id;
        Zone = zone;
        Items = items;
    }
}
