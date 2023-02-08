namespace DotNetToGA4.Domain.Models.Sales.Campaign;

public class ViewCampaignBlock : Core
{
    public ViewCampaignBlock(string blockId, string blockName, string campaignId, string campaignName, IEnumerable<CoreProduct> coreProducts)
    {
        BlockId = blockId;
        BlockName = blockName;
        CampaignId = campaignId;
        CampaignName = campaignName;
        CoreProducts = coreProducts;
    }

    public string BlockId { get; }
    public string BlockName { get; }
    public string CampaignId { get; }
    public string CampaignName { get; }
    public IEnumerable<CoreProduct> CoreProducts { get; }
}
