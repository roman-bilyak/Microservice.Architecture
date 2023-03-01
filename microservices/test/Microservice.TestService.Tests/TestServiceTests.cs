using Microservice.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.TestService;

internal abstract class TestServiceTests : BaseIntegrationTests<TestServiceTestsModule>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddScoped(provider =>
        {
            return new DbContextOptionsBuilder<TestServiceDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(TestServiceDbContext)}_{TestContext.CurrentContext.Test.FullName}")
                .Options;
        });
    }
}