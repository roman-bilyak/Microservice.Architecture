using Microservice.Core.Modularity;
using Microservice.PaymentService.Payment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.PaymentService;

[DependsOn<PaymentServiceDomainModule>]
public sealed class PaymentServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddTransient<IPaymentApplicationService, PaymentApplicationService>();
    }
}