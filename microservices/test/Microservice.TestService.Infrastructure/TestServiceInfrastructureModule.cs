using Microservice.Core.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.TestService;

[DependsOn<TestServiceDomainModule>]
public sealed class TestServiceInfrastructureModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddDbContext<TestServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("TestServiceDb"));
        });
    }

    public override void Configure(IServiceProvider serviceProvider)
    {
        base.Configure(serviceProvider);

        using IServiceScope scope = serviceProvider.CreateScope();

        TestServiceDbContext dbContext = scope.ServiceProvider.GetRequiredService<TestServiceDbContext>();
        if (dbContext.Database.IsRelational())
        {
            dbContext.Database.Migrate();
        }
    }
}