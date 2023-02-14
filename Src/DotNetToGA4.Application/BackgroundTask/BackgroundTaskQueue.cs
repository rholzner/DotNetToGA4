using DotNetToGA4.Domain.Models;
using System.Threading.Channels;

namespace DotNetToGA4.Application.BackgroundTask;

public interface IBackgroundTaskQueue
{
    ValueTask<DequeueEventToGaAsyncResult> DequeueEventToGaAsync(int take, CancellationToken cancellationToken);
    ValueTask QueEventToGaAsync(Core workItem, CancellationToken cancellationToken);
}

internal class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly Channel<Core> _queue;

    public BackgroundTaskQueue()
    {
        _queue = Channel.CreateUnbounded<Core>();
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

        return new DequeueEventToGaAsyncResult(list, hasMoreItems);
    }

}

public record DequeueEventToGaAsyncResult(IEnumerable<Core> Cores,bool hasMoreToRead);

