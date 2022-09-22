using Microservice.Api;
using Microservice.Core.Modularity;

namespace Microservice.PaymentService;

[DependsOn(typeof(PaymentServiceApplicationModule))]
[DependsOn(typeof(PaymentServiceInfrastructureModule))]
[DependsOn(typeof(ApiModule))]
public sealed class PaymentServiceWebModule : StartupModule
{
}