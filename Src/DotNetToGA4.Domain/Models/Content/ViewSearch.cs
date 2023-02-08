using DotNetToGA4.Domain.Models.Sales;

namespace DotNetToGA4.Domain.Models.Content;

/// <summary>
/// Will be two events
/// </summary>
public class ViewSearch : Core
{
    public ViewSearch(string name,string id,string searchText,IEnumerable<CoreProduct> products)
    {
        this.Name = name;
        this.Id = id;
        Products = products;
    }

    public string Name { get; }
    public string Id { get; }
    public IEnumerable<CoreProduct> Products { get; }
}
