namespace DotNetToGA4.Domain.Models;

public class GenerateLead : Core
{
    public GenerateLead(string currency, double value)
    {
        Currency = currency;
        Value = value;
    }

    public string Currency { get; }
    public double Value { get; }
}
