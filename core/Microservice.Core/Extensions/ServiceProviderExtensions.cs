using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core;

public static class ServiceProviderExtensions
{
    public static T GetWrappedService<T>(this IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        ObjectWrapper<T> objectWrapper = serviceProvider.GetRequiredService<ObjectWrapper<T>>();
        if (objectWrapper.Object is null)
        {
            throw new InvalidOperationException($"Object is not set for wrapper of type '{typeof(T)}'");
        }
        return objectWrapper.Object;
    }

    public static void SetWrappedService<T>(this IServiceProvider serviceProvider, T service)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        serviceProvider.GetRequiredService<ObjectWrapper<T>>().Object = service;
    }
}
