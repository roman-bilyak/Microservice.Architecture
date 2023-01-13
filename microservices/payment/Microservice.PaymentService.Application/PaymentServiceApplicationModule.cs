using Microservice.Core.Modularity;
using Microservice.PaymentService.Payment;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.PaymentService;

[DependsOn<PaymentServiceDomainModule>]
public sealed class PaymentServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddTransient<IPaymentApplicationService, PaymentApplicationService>();
    }
}