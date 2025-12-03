namespace Core.Domain;

public class Item
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }

    public Item()
    {
    }

    public Item(string id, string name, string description, double price, Category category)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Category = category;
    }
}