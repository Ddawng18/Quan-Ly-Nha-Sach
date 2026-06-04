using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public class LoyaltyService : ILoyaltyService
{
    public int CalculateEarnedPoints(decimal paidAmount)
    {
        if (paidAmount <= 0)
        {
            return 0;
        }

        return (int)Math.Floor(paidAmount * LoyaltySettings.EarnPointsPerCurrencyUnit);
    }

    public decimal CalculateRedemptionValue(int points)
    {
        if (points <= 0)
        {
            return 0;
        }

        return points * LoyaltySettings.RedemptionValuePerPoint;
    }

    public int CalculateRedeemablePoints(Customer customer, decimal orderAmount, int requestedPoints)
    {
        if (customer is null || requestedPoints <= 0 || orderAmount <= 0)
        {
            return 0;
        }

        var maxByAmount = (int)Math.Floor(orderAmount / LoyaltySettings.RedemptionValuePerPoint);
        return Math.Max(0, Math.Min(Math.Min(customer.LoyaltyPoints, requestedPoints), maxByAmount));
    }
}
