using Microservice.Core.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.TestService;

[DependsOn<TestServiceApplicationModule>]
[DependsOn<TestServiceInfrastructureModule>]
internal sealed class TestServiceTestsModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddScoped(provider =>
        {
            return new DbContextOptionsBuilder<TestServiceDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(TestServiceDbContext)}_{TestContext.CurrentContext.Test.FullName}")
                .Options;
        });
    }
}