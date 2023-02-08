namespace DotNetToGA4.Domain.Models.Sales.Content;

public class ClickCategorie : Core
{
    public ClickCategorie(string pageId, string pageName, IEnumerable<CoreProduct> coreProducts)
    {
        PageId = pageId;
        PageName = pageName;
        CoreProducts = coreProducts;
    }

    public string PageId { get; }
    public string PageName { get; }
    public IEnumerable<CoreProduct> CoreProducts { get; }
}
