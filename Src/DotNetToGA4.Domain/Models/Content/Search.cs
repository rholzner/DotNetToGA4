using DotNetToGA4.Domain.Models.Sales;

namespace DotNetToGA4.Domain.Models.Content;

public class ClickSearch : Core
{
    public ClickSearch(string searchText)
    {
        SearchText = searchText;
    }

    public string SearchText { get; }
}