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
}