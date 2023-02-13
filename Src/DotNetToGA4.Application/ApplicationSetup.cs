using Microsoft.Extensions.DependencyInjection;

namespace DotNetToGA4.Application;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGaEventService, GaEventService>();
        return services;
    }
}
