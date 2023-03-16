using Microservice.Core.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.IdentityService;

[DependsOn<IdentityServiceApplicationModule>]
[DependsOn<IdentityServiceInfrastructureModule>]
internal sealed class IdentityServiceTestsModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddScoped(provider =>
        {
            return new DbContextOptionsBuilder<IdentityServiceDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(IdentityServiceDbContext)}_{TestContext.CurrentContext.Test.FullName}")
                .Options;
        });
    }
}