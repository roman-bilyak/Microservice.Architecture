using Microservice.Core.Modularity;

namespace Microservice.PaymentService;

[DependsOn<PaymentServiceApplicationModule>]
[DependsOn<PaymentServiceInfrastructureModule>]
internal sealed class PaymentServiceTestsModule : StartupModule
{
}