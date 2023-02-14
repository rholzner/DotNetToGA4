using DotNetToGA4.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace DotNetToGA4.Application.BackgroundTask;

public interface IBackgroundTaskQueue
{
    ValueTask<DequeueEventToGaAsyncResult> DequeueEventToGaAsync(int take, CancellationToken cancellationToken);
    ValueTask QueEventToGaAsync(Core workItem, CancellationToken cancellationToken);
}

internal class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private Channel<Core> _queue;
    private readonly ILogger<BackgroundTaskQueue> logger;

    public BackgroundTaskQueue(ILogger<BackgroundTaskQueue> logger)
    {
        _queue = Channel.CreateUnbounded<Core>();
        this.logger = logger;
    }

    public async ValueTask QueEventToGaAsync(Core workItem, CancellationToken cancellationToken)
    {
        if (workItem is null)
        {
            throw new ArgumentNullException(nameof(workItem));
        }

        await _queue.Writer.WriteAsync(workItem, cancellationToken);
    }

    public async ValueTask<DequeueEventToGaAsyncResult> DequeueEventToGaAsync(int take, CancellationToken cancellationToken)
    {
        List<Core> list = new List<Core>();

        //INFO: if que is smaller then take set take to less to keep it rolling
        bool hasMoreItems = true;
        if (_queue.Reader.CanCount && _queue.Reader.Count < take)
        {
            take = _queue.Reader.Count;
            hasMoreItems = false;
            logger.LogInformation("BackgroundTaskQueue:DequeueEventToGaAsync: nr of items in que: {queCount}", _queue.Reader.Count);
        }

        if (take == 0)
        {
            take = 1;
        }

        for (int i = 0; i < take; i++)
        {

            Core? workItem = await _queue.Reader.ReadAsync(cancellationToken);
            list.Add(workItem);
        }

        if (_queue.Reader.CanCount && _queue.Reader.Count == 0)
        {
            logger.LogInformation("BackgroundTaskQueue:DequeueEventToGaAsync: reset que");
            _queue = Channel.CreateUnbounded<Core>();
        }

        return new DequeueEventToGaAsyncResult(list, hasMoreItems);
    }

}

public record DequeueEventToGaAsyncResult(IEnumerable<Core> Cores, bool hasMoreToRead);

