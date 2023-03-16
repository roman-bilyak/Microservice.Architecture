using Microservice.Core.Modularity;

namespace Microservice;

[DependsOn<CoreModule>]
public sealed class DomainModule : StartupModule
{
}