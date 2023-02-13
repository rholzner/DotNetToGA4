using DotNetToGA4.Domain.Models.Sales.Cart;

namespace DotNetToGA4.Domain.Models.Sales.Checkout;

public class AddShipping : Core
{
    public AddShipping(string currency, double value, string coupon, string shippingType, IEnumerable<CartProduct> products)
    {
        Currency = currency;
        Value = value;
        Coupon = coupon;
        ShippingType = shippingType;
        Products = products;
    }

    public IEnumerable<CartProduct> Products { get; }
    public string Currency { get; }
    public double Value { get; }
    public string Coupon { get; }
    public string ShippingType { get; }
}
