namespace DotNetToGA4.Domain.Models.Content;

/// <summary>
/// Simpel page view select_content
/// </summary>
public class ViewContent : Core
{
    public ViewContent(string Name, string Id)
    {
        this.Name = Name;
        this.Id = Id;
    }

    public string Name { get; }
    public string Id { get; }
}
