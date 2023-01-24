using Microservice.Core.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core;

public static class ServiceCollectionExtensions
{
    public static IApplication AddApplication<TStartupModule>(this IServiceCollection services, IConfiguration configuration,
        Action<ApplicationConfigurationOptions>? configurationOptionsAction = null)
        where TStartupModule : class, IStartupModule, new()
    {
        ArgumentNullException.ThrowIfNull(services);

        return new Application<TStartupModule>(services, configuration, configurationOptionsAction);
    }

    public static void AddWrappedService<T>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<ObjectWrapper<T>>(new ObjectWrapper<T>());
    }

    public static Type? GetImplementationType<T>(this IServiceCollection services)
    {
        return services.GetImplementationType(typeof(T));
    }

    public static Type? GetImplementationType(this IServiceCollection services, Type type)
    {
        return services.FirstOrDefault((ServiceDescriptor d) => d.ServiceType == type)?.ImplementationType;
    }

    public static T GetImplementationInstance<T>(this IServiceCollection services)
    {
        return (T)(services.FirstOrDefault((ServiceDescriptor d) => d.ServiceType == typeof(T))?.ImplementationInstance);
    }
}