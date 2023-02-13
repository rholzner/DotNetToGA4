using DotNetToGA4.Domain.Models.Sales.Cart;

namespace DotNetToGA4.Domain.Models.Sales.Checkout;

public class EndCheckout : Core
{
    public EndCheckout(string transactionId, string currency, double value, string coupon, double shipping, double tax, IEnumerable<CartProduct> products)
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
    public double Shipping { get; }
    public double Tax { get; }
    public IEnumerable<CartProduct> Products { get; }
}
