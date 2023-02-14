using DotNetToGA4.Application.BackgroundTask;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetToGA4.Application;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var appSettings = new ApplicationSettings(100);

        services.AddSingleton(appSettings);

        services.AddScoped<IGaEventService, GaEventService>();
        services.AddSingleton<IBackgroundTaskQueue,BackgroundTaskQueue>();
        services.AddHostedService<SendGaEventsHostedService>();
        return services;
    }
}

public class ApplicationSettings
{
    public ApplicationSettings(int batchSize)
    {
        BatchSize = batchSize;
    }

    public int BatchSize { get; }
    
}
