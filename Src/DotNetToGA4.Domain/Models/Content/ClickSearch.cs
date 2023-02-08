using DotNetToGA4.Domain.Models.Sales;

namespace DotNetToGA4.Domain.Models.Content;

public class ClickSearch : Core
{
    public ClickSearch(string name, string id,CoreProduct product)
    {
        this.Name = name;
        this.Id = id;
        Product = product;
    }

    public string Name { get; }
    public string Id { get; }
    public CoreProduct Product { get; }
}