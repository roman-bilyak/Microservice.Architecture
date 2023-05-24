using Microservice.Core.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.MovieService;

[DependsOn<MovieServiceApplicationModule>]
[DependsOn<MovieServiceInfrastructureModule>]
internal sealed class MovieServiceTestsModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddScoped(provider =>
        {
            return new DbContextOptionsBuilder<MovieServiceDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(MovieServiceDbContext)}_{TestContext.CurrentContext.Test.FullName}")
                .Options;
        });
    }
}