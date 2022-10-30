using Microservice.Api;
using Microservice.Application;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;

namespace Microservice.IdentityService;

[DependsOn(typeof(IdentityServiceApplicationModule))]
[DependsOn(typeof(IdentityServiceInfrastructureModule))]
[DependsOn(typeof(ApiModule))]
public sealed class IdentityServiceWebModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(IdentityServiceApplicationModule).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });
    }
}