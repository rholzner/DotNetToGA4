namespace DotNetToGA4.Domain.Models.Sales.Checkout;

public class BeginCheckout : Core
{
    public BeginCheckout(string currency, double value, string coupon, IEnumerable<CoreProduct> products)
    {
        Currency = currency;
        Value = value;
        Coupon = coupon;
        Products = products;
    }

    public string Currency { get; }
    public double Value { get; }
    public string Coupon { get; }
    public IEnumerable<CoreProduct> Products { get; }
}
