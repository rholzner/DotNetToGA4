namespace DotNetToGA4.Domain.Models.Sales.Checkout;

public class EndCheckout : Core
{
    public EndCheckout(string transactionId, string currency, double value, string coupon, string shipping, string tax, IEnumerable<CoreProduct> products)
    {
        TransactionId = transactionId;
        Currency = currency;
        Value = value;
        Coupon = coupon;
        Shipping = shipping;
        Tax = tax;
        Products = products;
    }

    public string TransactionId { get; }
    public string Currency { get; }
    public double Value { get; }
    public string Coupon { get; }
    public string Shipping { get; }
    public string Tax { get; }
    public IEnumerable<CoreProduct> Products { get; }
}
