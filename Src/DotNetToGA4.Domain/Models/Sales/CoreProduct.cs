namespace DotNetToGA4.Domain.Models.Sales;

public class CoreProduct
{
    public CoreProduct(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; }
    public string Name { get; }

}

