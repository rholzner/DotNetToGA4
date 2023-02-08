namespace DotNetToGA4.Domain.Models.Sales.Checkout;

public class AddPayment : Core
{
    public AddPayment(string currency, double value, string coupon, string paymentType, IEnumerable<CoreProduct> products)
    {
        Currency = currency;
        Value = value;
        Coupon = coupon;
        PaymentType = paymentType;
        Products = products;
    }

    public string Currency { get; }
    public double Value { get; }
    public string Coupon { get; }
    public string PaymentType { get; }
    public IEnumerable<CoreProduct> Products { get; }
}
