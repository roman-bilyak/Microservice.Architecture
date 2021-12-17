using Microservice.AspNetCore.Conventions;
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
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddMvc();
        services.AddWrappedService<IApplicationBuilder>();
    }

    public override void Initialize(IServiceProvider serviceProvider)
    {
        base.Initialize(serviceProvider);

        MvcOptions mvcOptions = serviceProvider.GetOptions<MvcOptions>();
        mvcOptions.Conventions.Add(new DynamicControllerConvention());

        IApplication application = serviceProvider.GetRequiredService<IApplication>();
        ApplicationPartManager applicationPartManager = serviceProvider.GetRequiredService<ApplicationPartManager>();
        applicationPartManager.FeatureProviders.Add(new DynamicControllerFeatureProvider(application));

        DynamicControllerOptions dynamicControllerOptions = serviceProvider.GetOptions<DynamicControllerOptions>();
        foreach (Assembly assembly in dynamicControllerOptions.Assemblies)
        {
            applicationPartManager.ApplicationParts.Add(new AssemblyPart(assembly));
        }
    }
}