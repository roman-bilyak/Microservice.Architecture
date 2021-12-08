using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core;

public static class ServiceCollectionExtensions
{
    public static IApplication AddApplication(this IServiceCollection services,
        Action<ApplicationConfigurationOptions> configurationOptionsAction = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        return new Application(services, configurationOptionsAction);
    }

    public static void AddWrappedService<T>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<ObjectWrapper<T>>(new ObjectWrapper<T>());
    }

    public static T GetSingletonServiceOrNull<T>(this IServiceCollection services)
    {
        return (T)(services.FirstOrDefault((ServiceDescriptor d) => d.ServiceType == typeof(T))?.ImplementationInstance);
    }

    public static T GetSingletonService<T>(this IServiceCollection services)
    {
        return services.GetSingletonServiceOrNull<T>() ?? throw new Exception("Can not find service: " + typeof(T).AssemblyQualifiedName);
    }
}