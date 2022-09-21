using Microservice.Core.Modularity;

namespace Microservice.PaymentService;

[DependsOn(typeof(PaymentServiceDomainModule))]
public sealed class PaymentServiceInfrastructureModule : StartupModule
{
}