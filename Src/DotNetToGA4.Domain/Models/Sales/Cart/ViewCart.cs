﻿namespace DotNetToGA4.Domain.Models.Sales.Cart;

public class ViewCart : AddToCart
{
    public ViewCart(string currency, double value, CoreProduct product) : base(currency, value, product)
    {

    }
}
