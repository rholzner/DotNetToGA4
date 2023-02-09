using DotNetToGA4.Domain.Models.Sales;

namespace DotNetToGA4.Domain.Models.Content;

public class Search : Core
{
    public Search(string searchText)
    {
        SearchText = searchText;
    }

    public string SearchText { get; }
}