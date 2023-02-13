using DotNetToGA4.Domain.Models;
using MediatR;

namespace DotNetToGA4.Domain.Notifications;

public class GaNotification : INotification
{
    public GaNotification(Core events, RunAs run)
    {
        Events = new Core[] { events };
        Run = run;
    }

    public GaNotification(IEnumerable<Core> cores, RunAs run)
    {
        Events = cores;
        Run = run;
    }


    public IEnumerable<Core> Events { get; }
    public RunAs Run { get; }
}

public enum RunAs
{
    notset = 0,
    Prod = 1,
    TestRun = 2,
    DryRun = 3
}

