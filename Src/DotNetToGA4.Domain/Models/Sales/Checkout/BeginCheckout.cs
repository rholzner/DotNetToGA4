using DotNetToGA4.Domain.Models.Sales.Cart;

namespace DotNetToGA4.Domain.Models.Sales.Checkout;

public class BeginCheckout : Core
{
    public BeginCheckout(string currency, double value, string coupon, IEnumerable<CartProduct> products)
    {
        Currency = currency;
        Value = value;
        Coupon = coupon;
        Products = products;
    }

    public string Currency { get; }
    public double Value { get; }
    public string Coupon { get; }
    public IEnumerable<CartProduct> Products { get; }
}
