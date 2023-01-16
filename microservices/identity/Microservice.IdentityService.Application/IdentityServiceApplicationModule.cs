using Microservice.Core.Modularity;
using Microservice.CQRS;
using Microservice.IdentityService.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.IdentityService;

[DependsOn<IdentityServiceDomainModule>]
public sealed class IdentityServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddTransient<IUsersApplicationService, UsersApplicationService>();
        services.AddTransient<IRolesApplicationService, RolesApplicationService>();

        services.AddCQRS(typeof(IdentityServiceApplicationModule).Assembly);
    }
}