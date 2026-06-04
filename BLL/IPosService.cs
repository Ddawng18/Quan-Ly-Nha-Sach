using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public interface IPosService
{
    ValidationResult AddOrUpdateLine(IList<CartLine> lines, Book book, int quantity, DiscountType discountType, decimal discountValue);
    CartTotals CalculateTotals(IReadOnlyList<CartLine> lines, DiscountType orderDiscountType, decimal orderDiscountValue, decimal taxRate, decimal loyaltyDiscount);
    CheckoutResult PrepareCheckout(CheckoutRequest request);
    ValidationResult CompleteCheckout(CheckoutResult checkout);
}
