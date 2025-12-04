namespace Core.Domain;

public class Order
{
    public string Id { get; }
    public Customer Customer { get; }
    public Store Store { get; }
    public List<Item> Items { get; }
    public OrderStatus Status { get; private set; }
    public DateTime OrderDate { get; private set; }
    public string DestinationZone { get; set; }
    public double TotalPrice { get; private set; }

    public Order(string id, Customer customer, Store store, List<Item> items, OrderStatus status = OrderStatus.CREATED)
    {
        Id = id;
        Customer = customer;
        Store = store;
        Items = items;
        Status = status;
        TotalPrice = Items.Sum(i => i.Price);
        OrderDate = DateTime.Now;
        ;
        DestinationZone = store.Zone;
    }

    public void SetStatus(OrderStatus status)
    {
        Status = status;
    }

    public double SubTotal
    {
        return 
}
}