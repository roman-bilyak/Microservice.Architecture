using Microservice.Core.Modularity;

namespace Microservice.PaymentService;

[DependsOn<DomainModule>]
public sealed class PaymentServiceDomainModule : StartupModule
{
}