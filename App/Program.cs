using Core.Domain;
using Core.Services;
using Core.StaticData;
using static Core.StaticData.StaticData;

// Demo flow: build an order, price it, assign delivery, and print a summary.
var customer = new Customer("c-100", "Alex", "Patel", 1200, LoyaltyTier.GOLD);
var store = Stores.First(s => s.Zone == ZONEA);

var orderItems = new List<OrderItem>
{
    new OrderItem(store.Items.First(i => i.Id == "5"), 2),
    new OrderItem(store.Items.First(i => i.Id == "6"), 1)
};

var order = new Order("order-001", customer, store, ZONEB, orderItems);

var distanceService = new DistanceService();
var deliveryPricingService = new DeliveryPricingService();
var pricingService = new OrderPricingService(distanceService, deliveryPricingService);
var breakdown = pricingService.Calculate(order);

var deliveryPartner = new DeliveryPartner("dp-1", "Priya Rider", new List<Delivery>());
var etaMinutes = (int)Math.Ceiling(breakdown.DistanceKm * 5); // simple heuristic: 5 mins per km
var delivery = new Delivery(etaMinutes, breakdown.DistanceKm);
order.AssignDeliveryDetails(delivery, deliveryPartner, breakdown.DeliveryFee);
order.Complete();

Console.WriteLine("=== JOI Delivery Demo ===");
Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName} ({customer.Tier})");
Console.WriteLine($"Store: {store.Id} in {store.Zone} -> Deliver to {order.DestinationZone}");
Console.WriteLine("Items:");
foreach (var item in order.Items)
{
    Console.WriteLine($" - {item.Item.Name} x{item.Quantity} @ ₹{item.Item.Price} = ₹{item.LineTotal:F2}");
}

Console.WriteLine($"Subtotal: ₹{order.Subtotal:F2}");
Console.WriteLine($"Loyalty discount: ₹{breakdown.Discount:F2}");
Console.WriteLine($"Delivery fee ({breakdown.DistanceKm} km): ₹{breakdown.DeliveryFee:F2}");
Console.WriteLine($"Total: ₹{breakdown.Total:F2}");
Console.WriteLine($"Status: {order.Status} | ETA: {delivery.TimeInMinutes} minutes");
