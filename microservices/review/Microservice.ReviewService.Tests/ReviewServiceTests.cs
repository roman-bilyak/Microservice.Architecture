using Microservice.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.ReviewService;

internal abstract class ReviewServiceTests : BaseIntegrationTests<ReviewServiceTestsModule>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddScoped(provider =>
        {
            return new DbContextOptionsBuilder<ReviewServiceDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(ReviewServiceDbContext)}_{TestContext.CurrentContext.Test.FullName}")
                .Options;
        });
    }
}