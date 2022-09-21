using Microservice.Core.Modularity;

namespace Microservice.PaymentService;

[DependsOn(typeof(PaymentServiceApplicationContractsModule))]
[DependsOn(typeof(PaymentServiceDomainModule))]
public sealed class PaymentServiceApplicationModule : StartupModule
{
}