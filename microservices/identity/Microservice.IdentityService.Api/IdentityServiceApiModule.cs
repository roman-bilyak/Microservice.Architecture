using Microservice.Api;
using Microservice.Application;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;

namespace Microservice.IdentityService;

[DependsOn<IdentityServiceApplicationModule>]
[DependsOn<IdentityServiceInfrastructureModule>]
[DependsOn<ApiModule>]
public sealed class IdentityServiceApiModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(IdentityServiceApplicationModule).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });
    }
}