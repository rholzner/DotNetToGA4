using DotNetToGA4.Domain.Models;
using MediatR;

namespace DotNetToGA4.Domain.Notifications;

public class GaNotification : INotification
{
    public GaNotification(Core events)
    {
        Events = new Core[] { events };
    }

    public GaNotification(IEnumerable<Core> cores)
    {
        Events = cores;
    }


    public IEnumerable<Core> Events { get; }
}

public enum RunAs
{
    notset = 0,
    Prod = 1,
    TestRun = 2,
    DryRun = 3
}

