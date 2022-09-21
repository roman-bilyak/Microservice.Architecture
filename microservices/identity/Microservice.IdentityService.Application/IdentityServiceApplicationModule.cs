using Microservice.Core.Modularity;

namespace Microservice.IdentityService;

[DependsOn(typeof(IdentityServiceApplicationContractsModule))]
[DependsOn(typeof(IdentityServiceDomainModule))]
public sealed class IdentityServiceApplicationModule : StartupModule
{
}