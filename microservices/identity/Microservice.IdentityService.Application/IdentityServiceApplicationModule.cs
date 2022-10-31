using Microservice.Core.Modularity;
using Microservice.IdentityService.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.IdentityService;

[DependsOn(typeof(IdentityServiceDomainModule))]
public sealed class IdentityServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddTransient<IUserApplicationService, UserApplicationService>();
        services.AddTransient<IRoleApplicationService, RoleApplicationService>();
    }
}