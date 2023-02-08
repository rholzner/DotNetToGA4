namespace DotNetToGA4.Domain.Models.Sales.Content;

public class ClickProduct : Core
{
    public ClickProduct(string currency, double value, CoreProduct product)
    {
        Currency = currency;
        Value = value;
        Product = product;
    }

    public string Currency { get; }
    public double Value { get; }

    public CoreProduct Product { get; }
}