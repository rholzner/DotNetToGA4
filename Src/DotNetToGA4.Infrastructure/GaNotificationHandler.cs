using DotNetToGA4.Domain.Models.Content;
using DotNetToGA4.Domain.Models.Sales.Campaign;
using DotNetToGA4.Domain.Models.System;
using DotNetToGA4.Domain.Notifications;
using DotNetToGA4.Infrastructure.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DotNetToGA4.Infrastructure;

public class GaNotificationHandler : INotificationHandler<GaNotification>
{
    private readonly IGaHttpClient gaHttpClient;
    private readonly ILogger<GaNotificationHandler> logger;

    public GaNotificationHandler(IGaHttpClient gaHttpClient, ILogger<GaNotificationHandler> logger)
    {
        this.gaHttpClient = gaHttpClient;
        this.logger = logger;
    }
    public async Task Handle(GaNotification notification, CancellationToken cancellationToken)
    {
        List<Event> events = new List<Event>();

        foreach (var item in notification.Events)
        {
            if (item is Search search)
            {
                events.Add(GaEventBuilder.Search(search.SearchText));
            }
            else if (item is SignUp signup)
            {
                events.Add(GaEventBuilder.SignUp(signup.SignUpTo));
            }
            else if (item is Share share)
            {
                events.Add(GaEventBuilder.Share(share.SharedAs, share.TypOfItemShared, share.Id));
            }
            else if (item is Login login)
            {
                events.Add(GaEventBuilder.login(login.LoginWithSystem));
            }
            else if (item is ViewCampaignArea clickCampaign && clickCampaign is not null)
            {
                var campaingProducts = new List<Item>();
                var hasAreaDataOnRoot = false;
                var hasCampaingOnRoot = false;

                if (!string.IsNullOrEmpty(clickCampaign?.CampaignName) && !string.IsNullOrEmpty(clickCampaign?.CampaignId))
                {
                    hasCampaingOnRoot = true;
                }

                if (!string.IsNullOrEmpty(clickCampaign?.AreaName) && !string.IsNullOrEmpty(clickCampaign?.AreaId))
                {
                    hasAreaDataOnRoot = true;
                }

                for (int i = 0; i < clickCampaign.Products.Count(); i++)
                {
                    var product = clickCampaign.Products[i];

                    if (!hasAreaDataOnRoot && string.IsNullOrEmpty(product.AreaName) && string.IsNullOrEmpty(product.AreaId))
                    {
                        throw new DotNetToGA4Exception($"Error on product nr {i} - missing Area data on product or added it on root object");
                    }

                    if (!hasCampaingOnRoot && string.IsNullOrEmpty(product.CampaignName) && string.IsNullOrEmpty(product.CampaignId))
                    {
                        throw new DotNetToGA4Exception($"Error on product nr {i} - missing Campaign data on product or added it on root object");
                    }

                    campaingProducts.Add(new Item() { item_id = product.Id, item_name = product.Name, promotion_id = product.CampaignId, promotion_name = product.CampaignName });
                }

                events.Add(GaEventBuilder.ViewPromotion(campaingProducts));
            }
        }

        var r = await gaHttpClient.PostGaEvents(events, notification.TestRun);
        if (r.success && notification.TestRun)
        {
            logger.LogInformation("GaNotificationHandler:Handle:Sent {0} with Ok response", events.Count);
            return;
        }
        else if (notification.TestRun)
        {
            logger.LogError("GaNotificationHandler:Handle:Sent Failed to send {0} with error: {1}", events.Count, r.msg);
            return;
        }

        logger.LogInformation("GaNotificationHandler:Handle: done sending {0} events", events.Count);
    }
}

public class DotNetToGA4Exception : Exception
{
    public DotNetToGA4Exception(string error = "") : base(error)
    {

    }
}



