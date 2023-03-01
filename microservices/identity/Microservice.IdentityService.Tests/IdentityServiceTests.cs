using Microservice.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.IdentityService;

internal abstract class IdentityServiceTests : BaseIntegrationTests<IdentityServiceTestsModule>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddScoped(provider =>
        {
            return new DbContextOptionsBuilder<IdentityServiceDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(IdentityServiceDbContext)}_{TestContext.CurrentContext.Test.FullName}")
                .Options;
        });
    }
}