using Core.Domain;

namespace Core.Tests.OrderTests;

public class OrderTest
{
    [Fact]
    public void CreateOrder()
    {
        var customer = new Customer("C1", "Eduardo", "Lacerda", 0, LoyaltyTier.SILVER);
        var items = new List<Item>
        {
            new Item("IT-0", "Bread", "", 15),
            new Item("IT-1", "Cheese", "", 15)
        };
        var store = new Store("ST-01", "ZoneA", items);
        
        var order = new Order("O-01", customer, store, items);
    }
}