namespace DotNetToGA4.Domain.Models.Sales.Campaign;

public class ViewCampaignArea : Core
{
    /// <param name="areaId">TODO: add desc</param>
    /// <param name="areaName">TODO: add desc</param>
    /// <param name="campaignId">Fallback value if not set on product item in Products</param>
    /// <param name="campaignName">Fallback value if not set on product item in Products</param>
    /// <param name="products">TODO: add desc</param>
    public ViewCampaignArea(string areaId, string areaName, string? campaignId, string? campaignName, CampaingProduct[] products)
    {
        AreaId = areaId;
        AreaName = areaName;
        CampaignId = campaignId;
        CampaignName = campaignName;
        Products = products;
    }

    /// <param name="areaId">TODO: add desc</param>
    /// <param name="areaName">TODO: add desc</param>
    /// <param name="products">TODO: add desc</param>
    public ViewCampaignArea(string areaId, string areaName, CampaingProduct[] products)
    {
        AreaId = areaId;
        AreaName = areaName;
        Products = products;
    }

    public ViewCampaignArea(CampaingProduct[] products)
    {
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

public class CampaingProduct : CoreProduct
{
    public CampaingProduct(string id, string name) : base(id, name)
    {

    }

    public CampaingProduct(string id, string name, string? campaignId, string? campaignName) : base(id, name)
    {
        CampaignId = campaignId;
        CampaignName = campaignName;
        this.AreaId = AreaId;
        this.AreaName = AreaName;
    }

    public CampaingProduct(string id, string name, string? campaignId, string? campaignName, string? AreaId, string? AreaName) : base(id, name)
    {
        CampaignId = campaignId;
        CampaignName = campaignName;
        this.AreaId = AreaId;
        this.AreaName = AreaName;
    }

    public string? CampaignId { get; }
    public string? CampaignName { get; }
    public string? AreaId { get; }
    public string? AreaName { get; }
}