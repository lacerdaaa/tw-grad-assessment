namespace Core.Domain;

public class OrderItem
{
    public Item Item { get; }
    public int Quantity { get; }
    public double LineTotal => Item.Price * Quantity;

    public OrderItem(Item item, int quantity)
    {
        Item = item ?? throw new ArgumentNullException(nameof(item));
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive");
        }

        Quantity = quantity;
    }
}
