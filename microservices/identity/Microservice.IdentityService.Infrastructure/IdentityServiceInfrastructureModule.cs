using Microservice.Core.Modularity;
using Microservice.Database;
using Microservice.IdentityService.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.IdentityService;

[DependsOn<IdentityServiceDomainModule>]
public sealed class IdentityServiceInfrastructureModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddDbContext<IdentityServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("IdentityServiceDb"));
        });

        services.AddTransient<IRepository<User>, BaseRepository<IdentityServiceDbContext, User>>();
        services.AddTransient<IRepository<User, Guid>, BaseRepository<IdentityServiceDbContext, User, Guid>>();
        services.AddTransient<IReadRepository<User>, BaseRepository<IdentityServiceDbContext, User>>();
        services.AddTransient<IReadRepository<User, Guid>, BaseRepository<IdentityServiceDbContext, User, Guid>>();

        services.AddTransient<IRepository<Role>, BaseRepository<IdentityServiceDbContext, Role>>();
        services.AddTransient<IRepository<Role, Guid>, BaseRepository<IdentityServiceDbContext, Role, Guid>>();
        services.AddTransient<IReadRepository<Role>, BaseRepository<IdentityServiceDbContext, Role>>();
        services.AddTransient<IReadRepository<Role, Guid>, BaseRepository<IdentityServiceDbContext, Role, Guid>>();
    }
}