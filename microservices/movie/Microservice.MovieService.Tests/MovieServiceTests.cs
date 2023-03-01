using Microservice.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.MovieService;

internal abstract class MovieServiceTests : BaseIntegrationTests<MovieServiceTestsModule>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddScoped(provider =>
        {
            return new DbContextOptionsBuilder<MovieServiceDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(MovieServiceDbContext)}_{TestContext.CurrentContext.Test.FullName}")
                .Options;
        });
    }
}