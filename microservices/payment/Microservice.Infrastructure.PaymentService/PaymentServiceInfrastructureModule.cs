using Microservice.Core.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.PaymentService;

[DependsOn<PaymentServiceDomainModule>]
public sealed class PaymentServiceInfrastructureModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddDbContext<PaymentServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(nameof(PaymentServiceDbContext)));
        });
    }
}