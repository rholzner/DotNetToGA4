using DotNetToGA4.Domain.Notifications;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace DotNetToGA4.Infrastructure;

public static class InfrastructureSetup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var settings = new InfrastructureSetting(new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull }, RunAs.DryRun);
        services.AddSingleton(settings);
        services.AddHttpClient<IGaHttpClient, GaHttpClient>();

        return services;
    }
}

public class InfrastructureSetting
{
    public InfrastructureSetting(JsonSerializerOptions GaJsonSerializerOptions, RunAs RunAs)
    {
        this.GaJsonSerializerOptions = GaJsonSerializerOptions;
        this.RunAs = RunAs;
    }
    public JsonSerializerOptions GaJsonSerializerOptions { get; }
    public RunAs RunAs { get; }
}