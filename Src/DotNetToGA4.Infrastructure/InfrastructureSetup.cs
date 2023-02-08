using Microsoft.Extensions.DependencyInjection;

namespace DotNetToGA4.Infrastructure;

public static class InfrastructureSetup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClient<IGaHttpClient, GaHttpClient>();

        return services;
    }
}