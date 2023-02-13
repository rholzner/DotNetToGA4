namespace DotNetToGA4.Domain.Models.Sales.Cart;

public class AddToCart : Core
{
    public AddToCart(string currency, double value, CartProduct product)
    {
        Currency = currency;
        Value = value;
        Products = new CartProduct[] { product };
    }

    public AddToCart(string currency, double value, CartProduct[] product)
    {
        Currency = currency;
        Value = value;
        Products = product;
    }

    public string Currency { get; }
    public double Value { get; }

    public CartProduct[] Products { get; }
}

public class CartProduct : CoreProduct
{
    public CartProduct(string id, string name,int quantity,double price,double? discount = null,string? coupon = null) : base(id, name)
    {
        Quantity = quantity;
        Price = price;
        Discount = discount;
        Coupon = coupon;
    }

    public int Quantity { get; }
    public double Price { get; }
    public double? Discount { get; }
    public string? Coupon { get; }
}
