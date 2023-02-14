using DotNetToGA4.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetToGA4.Application.BackgroundTask;

public class SendGaEventsHostedService : BackgroundService
{
    private readonly IMediator mediator;
    private readonly IBackgroundTaskQueue backgroundTaskQueue;
    private readonly ILogger<SendGaEventsHostedService> logger;
    private readonly ApplicationSettings applicationSettings;

    private int batchSize;

    public SendGaEventsHostedService(IMediator mediator, IBackgroundTaskQueue backgroundTaskQueue, ILogger<SendGaEventsHostedService> logger, ApplicationSettings applicationSettings)
    {
        this.mediator = mediator;
        this.backgroundTaskQueue = backgroundTaskQueue;
        this.logger = logger;
        this.applicationSettings = applicationSettings;
        this.batchSize = applicationSettings.BatchSize;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation(
            $"{nameof(SendGaEventsHostedService)} is running.{Environment.NewLine}" +
            $"background queue.{Environment.NewLine}");

        return ProcessTaskQueueAsync(stoppingToken);
    }

    private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var data = await backgroundTaskQueue.DequeueEventToGaAsync(batchSize, stoppingToken);
                if (data?.Cores != null && data.Cores.Any())
                {
                    logger.LogInformation($"{nameof(SendGaEventsHostedService)} fetched {data.Cores.Count()}");
                    GaNotification notification = new GaNotification(data.Cores);
                    await mediator.Publish(notification);
                }
                if (!data.hasMoreToRead)
                {
                    batchSize = 1;
                }
                else
                {
                    batchSize = applicationSettings.BatchSize;
                }

            }
            catch (OperationCanceledException)
            {
                // Prevent throwing if stoppingToken was signaled
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred executing task work item.");
            }
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation($"{nameof(SendGaEventsHostedService)} is stopping.");

        await base.StopAsync(stoppingToken);
    }

}