using DotNetToGA4.Domain.Models;
using DotNetToGA4.Domain.Notifications;
using MediatR;

namespace DotNetToGA4.Application;
public interface IGaEventService
{
    void OverRideDefualtRunAs(RunAs run);

    Task Send(Core eventData, RunAs run = RunAs.Prod);
    Task Send(IEnumerable<Core> eventData, RunAs run = RunAs.Prod);
}

public class GaEventService : IGaEventService
{
    private readonly IMediator mediator;
    private RunAs defualtRunAs = RunAs.notset;
    public GaEventService(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public void OverRideDefualtRunAs(RunAs run)
    {
        defualtRunAs = run;
    }

    public async Task Send(Core eventData, RunAs run = RunAs.Prod)
    {
        if (defualtRunAs != RunAs.notset)
        {
            run = defualtRunAs;
        }

        var msg = new GaNotification(eventData, run);
        await mediator.Publish(msg);
    }

    public async Task Send(IEnumerable<Core> eventData, RunAs run = RunAs.Prod)
    {
        if (defualtRunAs != RunAs.notset)
        {
            run = defualtRunAs;
        }

        var msg = new GaNotification(eventData, run);
        await mediator.Publish(msg);
    }
}