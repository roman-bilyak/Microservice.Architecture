using Microservice.Core.Modularity;

namespace Microservice.IdentityService;

[DependsOn<IdentityServiceApplicationModule>]
[DependsOn<IdentityServiceInfrastructureModule>]
internal sealed class IdentityServiceTestsModule : StartupModule
{
}