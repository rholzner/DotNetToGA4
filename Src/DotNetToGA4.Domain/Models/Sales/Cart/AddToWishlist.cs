namespace DotNetToGA4.Domain.Models.Sales.Cart;

public class AddToWishlist : AddToCart
{
    public AddToWishlist(string currency, double value, CoreProduct product) : base(currency, value, product)
    {

    }
}
