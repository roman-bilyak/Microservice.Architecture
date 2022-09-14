using Microservice.Core;
using Microservice.Core.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservice.AspNetCore;

public sealed class AspNetCoreModule : BaseModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddWrappedService<IApplicationBuilder>();
        services.AddTransient<DynamicControllerConvention>();
        services.AddTransient<DynamicControllerFeatureProvider>();

        services.AddMvc();
    }

    public override void Configure(IServiceProvider serviceProvider)
    {
        base.Configure(serviceProvider);

        MvcOptions mvcOptions = serviceProvider.GetOptions<MvcOptions>();
        mvcOptions.Conventions.Add(serviceProvider.GetRequiredService<DynamicControllerConvention>());

        ApplicationPartManager applicationPartManager = serviceProvider.GetRequiredService<ApplicationPartManager>();
        applicationPartManager.FeatureProviders.Add(serviceProvider.GetRequiredService<DynamicControllerFeatureProvider>());

        DynamicControllerOptions dynamicControllerOptions = serviceProvider.GetOptions<DynamicControllerOptions>();
        foreach (Assembly assembly in dynamicControllerOptions.Assemblies)
        {
            applicationPartManager.ApplicationParts.Add(new AssemblyPart(assembly));
        }
    }
}