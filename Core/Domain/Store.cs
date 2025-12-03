namespace Core.Domain;

public class Store
{
    public string Id { get; set; }
    public string Zone { get; set; }
    public List<Item> Items { get; set; }
    
    public Store() {}

    public Store(string id, string zone, List<Item> items)
    {
        Id = id;
        Zone = zone;
        Items = items;
    }
}