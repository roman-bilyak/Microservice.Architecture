using Microservice.AspNetCore.Conventions;
using Microservice.Core;
using Microservice.Core.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Microservice.AspNetCore;

public sealed class AspNetCoreModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddMvc(options =>
        {
            options.Conventions.Add(new DynamicControllerConvention());
        });
        services.AddWrappedService<IApplicationBuilder>();

        ApplicationPartManager applicationPartManager = services.GetImplementationInstanceOrNull<ApplicationPartManager>();
        applicationPartManager?.FeatureProviders.Add(new DynamicControllerFeatureProvider(services.GetImplementationInstance<IApplication>()));
    }

    public override void Initialize(IServiceProvider serviceProvider)
    {
        base.Initialize(serviceProvider);

        ApplicationPartManager applicationPartManager = serviceProvider.GetRequiredService<ApplicationPartManager>();
        DynamicControllerOptions dynamicControllerOptions = serviceProvider.GetRequiredService<IOptions<DynamicControllerOptions>>().Value;
        foreach (Assembly assembly in dynamicControllerOptions.Assemblies)
        {
            applicationPartManager.ApplicationParts.Add(new AssemblyPart(assembly));
        }
    }
}