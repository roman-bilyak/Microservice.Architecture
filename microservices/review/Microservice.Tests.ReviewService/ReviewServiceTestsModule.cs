using Microservice.Core.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.ReviewService;

[DependsOn<ReviewServiceApplicationModule>]
[DependsOn<ReviewServiceInfrastructureModule>]
internal sealed class ReviewServiceTestsModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddScoped(provider =>
        {
            return new DbContextOptionsBuilder<ReviewServiceDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(ReviewServiceDbContext)}_{TestContext.CurrentContext.Test.FullName}")
                .Options;
        });
    }
}