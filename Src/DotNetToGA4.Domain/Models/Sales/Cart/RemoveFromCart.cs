namespace DotNetToGA4.Domain.Models.Sales.Cart;

public class RemoveFromCart : AddToCart
{
    public RemoveFromCart(string currency, double value, CartProduct product) : base(currency, value, product)
    {

    }
}
