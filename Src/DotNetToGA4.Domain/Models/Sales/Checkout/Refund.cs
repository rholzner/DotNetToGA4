namespace DotNetToGA4.Domain.Models.Sales.Checkout;

public class Refund : EndCheckout
{
    public Refund(string transactionId, string currency, double value, string coupon, string shipping, string tax, IEnumerable<CoreProduct> products) : base(transactionId, currency, value, coupon, shipping, tax, products)
    {

    }
}
