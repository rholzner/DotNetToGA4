namespace DotNetToGA4.Domain.Models.Sales.Content;

public class ViewCategorie : Core
{
    public ViewCategorie(string pageId, string pageName, IEnumerable<CoreProduct> coreProducts)
    {
        PageId = pageId;
        PageName = pageName;
        CoreProducts = coreProducts;
    }

    public string PageId { get; }
    public string PageName { get; }
    public IEnumerable<CoreProduct> CoreProducts { get; }
}
