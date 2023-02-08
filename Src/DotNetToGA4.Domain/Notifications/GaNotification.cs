using DotNetToGA4.Domain.Models;
using MediatR;

namespace DotNetToGA4.Domain.Notifications;

public class GaNotification : INotification
{
    public GaNotification(Core events,bool testRun)
    {
        Events = new Core[] { events };
        TestRun = testRun;
    }

    public GaNotification(IEnumerable<Core> cores)
    {
        Events = cores;
    }

    public IEnumerable<Core> Events { get; }
    public bool TestRun { get; }
}

