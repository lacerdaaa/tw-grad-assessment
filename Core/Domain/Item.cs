namespace Core.Domain;

public class Item
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public Category Category { get; set; }

    public Item()
    {
    }

    public Item(string id, string name, string description, double price, Category category = Category.DEFAULT)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Category = category;
    }
}
