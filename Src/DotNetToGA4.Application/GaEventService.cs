using DotNetToGA4.Application.BackgroundTask;
using DotNetToGA4.Domain.Models;
using System.ComponentModel;

namespace DotNetToGA4.Application;
public interface IGaEventService
{
    ValueTask Send(Core gaEvent, CancellationToken cancellationToken = default);
    ValueTask Send(IEnumerable<Core> gaEvents, CancellationToken cancellationToken = default);
}

public class GaEventService : IGaEventService
{
    private readonly IBackgroundTaskQueue backgroundTaskQueue;

    public GaEventService(IBackgroundTaskQueue backgroundTaskQueue)
    {
        this.backgroundTaskQueue = backgroundTaskQueue;
    }

    public async ValueTask Send(Core gaEvent, CancellationToken cancellationToken = default)
    {
        await backgroundTaskQueue.QueEventToGaAsync(gaEvent, cancellationToken);
    }

    public async ValueTask Send(IEnumerable<Core> gaEvents, CancellationToken cancellationToken = default)
    {
        foreach (var gaEvent in gaEvents)
        {
            await backgroundTaskQueue.QueEventToGaAsync(gaEvent, cancellationToken);
        }
    }
}
