using DotNetToGA4.Domain.Models.Content;
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
            if (item is ClickSearch clickSearch)
            {
                //ComeBack
                events.Add(GaEventBuilder.SelectContent($"Search", clickSearch.Id));
            }
            else if (item is ViewSearch viewSearch)
            {
                var products = new List<Item>();
                foreach (var product in viewSearch.Products)
                {
                    products.Add(new Item() { item_id = product.Id, item_name = product.Name });
                }
                events.Add(GaEventBuilder.ViewSearchResults(viewSearch.Name, products));
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



