using Microservice.Core.Modularity;

namespace Microservice.TestService;

[DependsOn<TestServiceDomainModule>]
public sealed class TestServiceInfrastructureModule : StartupModule
{
}