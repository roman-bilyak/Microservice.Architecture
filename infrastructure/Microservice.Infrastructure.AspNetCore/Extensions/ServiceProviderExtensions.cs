using Microservice.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microservice.Infrastructure.AspNetCore;

public static class ServiceProviderExtensions
{
    public static T GetOptions<T>(this IServiceProvider serviceProvider)
        where T : class
    {
        return serviceProvider.GetRequiredService<IOptions<T>>().Value;
    }

    public static IApplicationBuilder GetApplicationBuilder(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetWrappedService<IApplicationBuilder>();
    }

    public static void SetApplicationBuilder(this IServiceProvider serviceProvider, IApplicationBuilder applicationBuilder)
    {
        serviceProvider.SetWrappedService(applicationBuilder);
    }
}