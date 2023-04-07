using Microservice.Core.Modularity;

namespace Microservice.TestService;

[DependsOn<DomainModule>]
public sealed class TestServiceDomainModule : StartupModule
{
}
