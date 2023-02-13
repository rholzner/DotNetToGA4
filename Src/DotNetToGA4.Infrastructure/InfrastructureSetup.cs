using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace DotNetToGA4.Infrastructure;

public static class InfrastructureSetup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var settings = new InfrastructureSetting(new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull });
        services.AddSingleton(settings);
        services.AddHttpClient<IGaHttpClient, GaHttpClient>();

        return services;
    }
}

public class InfrastructureSetting
{
    public InfrastructureSetting(JsonSerializerOptions GaJsonSerializerOptions)
    {
        this.GaJsonSerializerOptions = GaJsonSerializerOptions;
    }
    public JsonSerializerOptions GaJsonSerializerOptions { get; }
}