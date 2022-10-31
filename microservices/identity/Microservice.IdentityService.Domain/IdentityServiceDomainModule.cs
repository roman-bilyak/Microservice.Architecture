using Microservice.Core.Modularity;
using Microservice.IdentityService.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.IdentityService;

public sealed class IdentityServiceDomainModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

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