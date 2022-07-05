using Castle.DynamicProxy;
using Microservice.Application.Services;
using Microservice.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Microservice.Gateway;

internal static class ServiceCollectionExtensions
{
    public static void RegisterFakeApplicationServices(this IServiceCollection services, Assembly assembly, string rootPath = null)
    {
        DefaultProxyBuilder proxyBuilder = new DefaultProxyBuilder();
        ProxyGenerationOptions proxyGenerationOptions = new ProxyGenerationOptions();
        if (!rootPath.IsNullOrEmpty())
        {
            proxyGenerationOptions.AdditionalAttributes.Add(CustomAttributeInfo.FromExpression(() => new RouteAttribute(rootPath)));
        }

        IEnumerable<Type> serviceTypes = assembly.GetTypes().Where(x => !x.IsGenericType && typeof(IApplicationService).IsAssignableFrom(x));
        foreach (Type serviceType in serviceTypes)
        {
            Type[] serviceInterfaces = serviceType.GetInterfaces();

            Type implementationType = serviceType.IsInterface
                ? proxyBuilder.CreateInterfaceProxyTypeWithoutTarget(serviceType, serviceInterfaces, proxyGenerationOptions)
                : proxyBuilder.CreateClassProxyTypeWithTarget(serviceType, serviceInterfaces, proxyGenerationOptions);

            services.AddTransient(serviceType, implementationType);
            services.AddTransient(implementationType);
        }
    }
}