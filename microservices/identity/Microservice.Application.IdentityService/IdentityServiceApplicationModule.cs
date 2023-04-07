using FluentValidation;
using Microservice.Application;
using Microservice.Core.Modularity;
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
        services.AddScoped<IValidator<UpdateRoleDto>, UpdateRoleDtoValidator>();

        services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
        services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
        services.AddScoped<IValidator<UpdateUserPasswordDto>, UpdateUserPasswordDtoValidator>();

        services.AddTransient<IUserApplicationService, UserApplicationService>();
        services.AddTransient<IRoleApplicationService, RoleApplicationService>();

        services.AddCQRS(typeof(IdentityServiceApplicationModule).Assembly);
    }
}