using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core;

public static class ServiceProviderExtensions
{
    public static T GetWrappedService<T>(this IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        return serviceProvider.GetRequiredService<ObjectWrapper<T>>().Object;
    }

    public static void SetWrappedService<T>(this IServiceProvider serviceProvider, T service)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        serviceProvider.GetRequiredService<ObjectWrapper<T>>().Object = service;
    }
}
