using Core.Domain;
using Core.Services;
using Core.StaticData;
using static Core.StaticData.StaticData;

namespace Core.Tests;

public class DeliveryPricingServiceTests
{
    [Fact]
    public void UsesBaseCostWithinThreshold()
    {
        var service = new DeliveryPricingService();

        service.Calculate(5).Should().Be(50);
    }

    [Fact]
    public void AddsExtraCostBeyondThreshold()
    {
        var service = new DeliveryPricingService();

        service.Calculate(8).Should().Be(80);
    }

    [Fact]
    public void RejectsNegativeDistance()
    {
        var service = new DeliveryPricingService();

        Assert.Throws<ArgumentOutOfRangeException>(() => service.Calculate(-1));
    }
}

public class DistanceServiceTests
{
    [Fact]
    public void ReturnsDistanceFromStaticMap()
    {
        var service = new DistanceService();

        service.GetDistanceInKm(ZONEA, ZONEB).Should().Be(3);
    }

    [Fact]
    public void ThrowsForMissingRoute()
    {
        var service = new DistanceService();

        Assert.Throws<InvalidOperationException>(() => service.GetDistanceInKm("UnknownZone", ZONEB));
    }
}

public class OrderPricingServiceTests
{
    [Fact]
    public void CalculatesBreakdownWithDiscountsAndDelivery()
    {
        var store = Stores.First(s => s.Zone == ZONEA);
        var item = store.Items.First(i => i.Id == "4"); // monitor 75
        var order = new Order(
            "order-123",
            new Customer("c-2", "Taylor", "Jones", 400, LoyaltyTier.SILVER),
            store,
            ZONEB,
            new[] { new OrderItem(item, 1) }
        );

        var pricing = new OrderPricingService(new DistanceService(), new DeliveryPricingService());
        var breakdown = pricing.Calculate(order);

        breakdown.DistanceKm.Should().Be(3);
        breakdown.Subtotal.Should().Be(item.Price);
        breakdown.Discount.Should().BeApproximately(item.Price * 0.03, 0.001);
        breakdown.DeliveryFee.Should().Be(50);
        breakdown.Total.Should().BeApproximately(item.Price - breakdown.Discount + breakdown.DeliveryFee, 0.001);
    }
}

public class OrderTests
{
    [Fact]
    public void ThrowsWhenNoItems()
    {
        var store = Stores.First();
        Assert.Throws<ArgumentException>(() =>
            new Order("o-1", new Customer("c", "A", "B", 0, LoyaltyTier.GOLD), store, ZONEA, Array.Empty<OrderItem>()));
    }

    [Fact]
    public void TracksStatusTransitions()
    {
        var store = Stores.First();
        var item = store.Items.First();
        var order = new Order(
            "o-2",
            new Customer("c-1", "Sam", "Patel", 0, LoyaltyTier.GOLD),
            store,
            ZONEA,
            new[] { new OrderItem(item, 1) });

        order.AssignDeliveryDetails(new Delivery(15, 3), null, 50);
        order.Complete();

        order.Status.Should().Be(OrderStatus.COMPLETED);
        order.CompletedAtUtc.Should().NotBeNull();
        Assert.Throws<InvalidOperationException>(() => order.Cancel());
    }
}
