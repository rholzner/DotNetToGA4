using DotNetToGA4.Domain.Models.Sales.Cart;

namespace DotNetToGA4.Domain.Models.Sales.Checkout;

public class Refund : EndCheckout
{
    public Refund(string transactionId, string currency, double value, string coupon, double shipping, double tax, IEnumerable<CartProduct> products) : base(transactionId, currency, value, coupon, shipping, tax, products)
    {

    }
}
