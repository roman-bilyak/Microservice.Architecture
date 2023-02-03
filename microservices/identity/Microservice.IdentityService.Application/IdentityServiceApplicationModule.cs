using FluentValidation;
using Microservice.Core.Modularity;
using Microservice.CQRS;
using Microservice.IdentityService.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.IdentityService;

[DependsOn<IdentityServiceDomainModule>]
public sealed class IdentityServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddScoped<IValidator<CreateRoleDto>, CreateRoleDtoValidator>();

        services.AddTransient<IUserApplicationService, UserApplicationService>();
        services.AddTransient<IRoleApplicationService, RoleApplicationService>();

        services.AddCQRS(typeof(IdentityServiceApplicationModule).Assembly);
    }
}