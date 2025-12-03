namespace Core.Domain;

public class Order
{
    public string OrderId { get; }
    public Customer Customer { get; }
    public Store Store { get; }
    public string DestinationZone { get; }
    public List<OrderItem> Items { get; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAtUtc { get; }
    public DateTime? CompletedAtUtc { get; private set; }
    public Delivery? Delivery { get; private set; }
    public DeliveryPartner? DeliveryPartner { get; private set; }
    public double DeliveryFee { get; private set; }

    public double Subtotal => Items.Sum(x => x.LineTotal);
    public double Discount => Customer == null ? 0 : Subtotal * Customer.Tier.GetDiscount() / 100.0;
    public double Total => Subtotal - Discount + DeliveryFee;

    public Order(string orderId, Customer customer, Store store, string destinationZone, IEnumerable<OrderItem> items)
    {
        if (string.IsNullOrWhiteSpace(orderId))
        {
            throw new ArgumentException("Order id is required", nameof(orderId));
        }

        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        Store = store ?? throw new ArgumentNullException(nameof(store));
        if (string.IsNullOrWhiteSpace(destinationZone))
        {
            throw new ArgumentException("Destination zone is required", nameof(destinationZone));
        }

        var orderItems = items?.ToList() ?? throw new ArgumentNullException(nameof(items));
        if (orderItems.Count == 0)
        {
            throw new ArgumentException("Order must have at least one item", nameof(items));
        }

        if (orderItems.Any(x => x == null))
        {
            throw new ArgumentException("Order items cannot contain null entries", nameof(items));
        }

        OrderId = orderId;
        DestinationZone = destinationZone;
        Items = orderItems;
        CreatedAtUtc = DateTime.UtcNow;
        Status = OrderStatus.CREATED;
    }

    public void AssignDeliveryDetails(Delivery delivery, DeliveryPartner? partner, double deliveryFee)
    {
        Delivery = delivery ?? throw new ArgumentNullException(nameof(delivery));
        DeliveryPartner = partner;
        if (deliveryFee < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(deliveryFee), "Delivery fee cannot be negative");
        }

        DeliveryFee = deliveryFee;
    }

    public void Complete()
    {
        EnsureMutableStatus();
        Status = OrderStatus.COMPLETED;
        CompletedAtUtc = DateTime.UtcNow;
    }

    public void Cancel()
    {
        EnsureMutableStatus();
        Status = OrderStatus.CANCELLED;
        CompletedAtUtc = DateTime.UtcNow;
    }

    public void Reject()
    {
        EnsureMutableStatus();
        Status = OrderStatus.REJECTED;
        CompletedAtUtc = DateTime.UtcNow;
    }

    private void EnsureMutableStatus()
    {
        if (Status is OrderStatus.COMPLETED or OrderStatus.CANCELLED or OrderStatus.REJECTED)
        {
            throw new InvalidOperationException($"Order is already {Status}");
        }
    }
}
