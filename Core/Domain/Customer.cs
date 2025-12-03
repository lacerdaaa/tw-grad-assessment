namespace Core.Domain;

public class Customer
{
    public string CustomerId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int LoyaltyPoints { get; set; }
    public LoyaltyTier Tier { get; set; }

    public Customer()
    {
    }

    public Customer(string customerId, string firstName, string lastName)
    {
        CustomerId = customerId;
        FirstName = firstName;
        LastName = lastName;
    }

    public Customer(string customerId, string firstName, string lastName, int loyaltyPoints, LoyaltyTier tier)
    {
        CustomerId = customerId;
        FirstName = firstName;
        LastName = lastName;
        LoyaltyPoints = loyaltyPoints;
        Tier = tier;
    }
}
