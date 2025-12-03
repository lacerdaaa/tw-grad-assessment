namespace Core.Domain;

public enum LoyaltyTier
{
    DIAMONG = 10,
    GOLD = 5,
    SILVER = 3
}

public static class LoyaltyTierExtensions
{
    public static int GetDiscount(this LoyaltyTier tier)
    {
        return (int) tier;
    }
}
