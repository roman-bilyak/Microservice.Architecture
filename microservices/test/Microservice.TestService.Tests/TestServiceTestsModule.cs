using Microservice.Core.Modularity;

namespace Microservice.TestService;

[DependsOn<TestServiceApplicationModule>]
[DependsOn<TestServiceInfrastructureModule>]
internal sealed class TestServiceTestsModule : StartupModule
{
}