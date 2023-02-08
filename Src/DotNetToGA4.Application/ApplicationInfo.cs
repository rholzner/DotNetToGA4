using DotNetToGA4.Domain.Models;
using DotNetToGA4.Domain.Notifications;
using MediatR;

namespace DotNetToGA4.Application;

public class ApplicationInfo
{
    public string Name => "ApplicationInfo";
}


public class GaEventService
{
    private readonly IMediator mediator;

    public GaEventService(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task Send(Core eventData)
    {
        var msg = new GaNotification(eventData);
        await mediator.Publish(msg);
    }

    public async Task Send(IEnumerable<Core> eventData)
    {
        var msg = new GaNotification(eventData);
        await mediator.Publish(msg);
    }

}