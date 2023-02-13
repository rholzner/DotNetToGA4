using DotNetToGA4.Application;
using DotNetToGA4.Domain;
using DotNetToGA4.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotNetToGA4;

public static class Setup
{
    public static IServiceCollection AddDotNetToGA4(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ApplicationInfo).Assembly, typeof(DomainInfo).Assembly, typeof(InfrastructureInfo).Assembly);

        services.AddApplication();
        services.AddInfrastructure();


        return services;
    }
}

