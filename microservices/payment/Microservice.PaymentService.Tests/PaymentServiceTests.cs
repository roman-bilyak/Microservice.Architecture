using Microservice.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.PaymentService;

internal abstract class PaymentServiceTests : BaseIntegrationTests<PaymentServiceTestsModule>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddScoped(provider =>
        {
            return new DbContextOptionsBuilder<PaymentServiceDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(PaymentServiceDbContext)}_{TestContext.CurrentContext.Test.FullName}")
                .Options;
        });
    }
}