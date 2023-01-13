using Microservice.Core.Modularity;

namespace Microservice.PaymentService;

[DependsOn<PaymentServiceDomainModule>]
public sealed class PaymentServiceInfrastructureModule : StartupModule
{
}