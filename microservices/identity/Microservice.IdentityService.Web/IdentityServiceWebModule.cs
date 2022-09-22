using Microservice.Api;
using Microservice.Core.Modularity;

namespace Microservice.IdentityService;

[DependsOn(typeof(IdentityServiceApplicationModule))]
[DependsOn(typeof(IdentityServiceInfrastructureModule))]
[DependsOn(typeof(ApiModule))]
public sealed class IdentityServiceWebModule : StartupModule
{
}