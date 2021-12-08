using Microservice.AspNetCore.Conventions;
using Microservice.AspNetCore.Providers;
using Microservice.Core;
using Microservice.Core.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.AspNetCore;

public sealed class AspNetCoreModule : Module
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddMvcCore(options =>
        {
            options.Conventions.Add(new ApplicationServiceConvention());
        });
        services.AddWrappedService<IApplicationBuilder>();

        ApplicationPartManager applicationPartManager = services.GetSingletonServiceOrNull<ApplicationPartManager>();
        applicationPartManager?.FeatureProviders.Add(new ApplicationServiceControllerFeatureProvider());
    }
}