using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public interface ILoyaltyService
{
    int CalculateEarnedPoints(decimal paidAmount);
    decimal CalculateRedemptionValue(int points);
    int CalculateRedeemablePoints(Customer customer, decimal orderAmount, int requestedPoints);
}
