using Microservice.Core.Modularity;
using Microservice.IdentityService.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.IdentityService;

[DependsOn<DomainModule>]
public sealed class IdentityServiceDomainModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IRoleManager, RoleManager>();

        services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
            .AddRoles<Role>()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>()
            .AddUserManager<UserManager>()
            .AddRoleManager<RoleManager>();
    }
}