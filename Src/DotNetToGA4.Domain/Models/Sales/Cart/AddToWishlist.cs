namespace DotNetToGA4.Domain.Models.Sales.Cart;

public class AddToWishlist : AddToCart
{
    public AddToWishlist(string currency, double value, CartProduct product) : base(currency, value, product)
    {

    }
}
