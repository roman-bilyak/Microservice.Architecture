using Microservice.Core.Modularity;

namespace Microservice.TestService;

[DependsOn(typeof(TestServiceDomainModule))]
public sealed class TestServiceInfrastructureModule : StartupModule
{
}