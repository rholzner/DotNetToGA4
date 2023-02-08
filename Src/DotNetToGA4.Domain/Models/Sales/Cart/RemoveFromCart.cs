namespace DotNetToGA4.Domain.Models.Sales.Cart;

public class RemoveFromCart : AddToCart
{
    public RemoveFromCart(string currency, double value, CoreProduct product) : base(currency, value, product)
    {

    }
}
