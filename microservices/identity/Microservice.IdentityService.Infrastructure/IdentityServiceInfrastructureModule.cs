using Microservice.Core.Modularity;

namespace Microservice.IdentityService;

[DependsOn(typeof(IdentityServiceDomainModule))]
public sealed class IdentityServiceInfrastructureModule : StartupModule
{
}