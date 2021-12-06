using Microservice.Core;
using Microsoft.AspNetCore.Builder;

namespace Microservice.AspNetCore;

public static class ServiceProviderExtensions
{
    public static IApplicationBuilder GetApplicationBuilder(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetWrappedService<IApplicationBuilder>();
    }

    public static void SetApplicationBuilder(this IServiceProvider serviceProvider, IApplicationBuilder applicationBuilder)
    {
        serviceProvider.SetWrappedService(applicationBuilder);
    }
}