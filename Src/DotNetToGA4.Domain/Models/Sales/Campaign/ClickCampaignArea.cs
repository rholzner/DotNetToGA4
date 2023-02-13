namespace DotNetToGA4.Domain.Models.Sales.Campaign;

public class ClickCampaignArea : Core
{
    public ClickCampaignArea(string areaId, string areaName, string campaignId, string campaignName, CampaingProduct[] products)
    {
        AreaId = areaId;
        AreaName = areaName;
        CampaignId = campaignId;
        CampaignName = campaignName;
        Products = products;
    }

    public string? AreaId { get; }
    public string? AreaName { get; }

    /// <summary>
    /// Fallback value if not set on product item in Products
    /// </summary>
    public string? CampaignId { get; }
    /// <summary>
    /// Fallback value if not set on product item in Products
    /// </summary>
    public string? CampaignName { get; }
    public CampaingProduct[] Products { get; }
}
