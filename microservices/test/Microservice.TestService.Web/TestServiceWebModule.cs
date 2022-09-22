using Microservice.Api;
using Microservice.Core.Modularity;

namespace Microservice.TestService;

[DependsOn(typeof(TestServiceApplicationModule))]
[DependsOn(typeof(TestServiceInfrastructureModule))]
[DependsOn(typeof(ApiModule))]
public sealed class TestServiceWebModule : StartupModule
{
}