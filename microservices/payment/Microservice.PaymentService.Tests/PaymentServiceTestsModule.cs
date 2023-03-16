using Microservice.Core.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Microservice.PaymentService;

[DependsOn<PaymentServiceApplicationModule>]
[DependsOn<PaymentServiceInfrastructureModule>]
internal sealed class PaymentServiceTestsModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddScoped(provider =>
        {
            return new DbContextOptionsBuilder<PaymentServiceDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(PaymentServiceDbContext)}_{TestContext.CurrentContext.Test.FullName}")
                .Options;
        });
    }
}