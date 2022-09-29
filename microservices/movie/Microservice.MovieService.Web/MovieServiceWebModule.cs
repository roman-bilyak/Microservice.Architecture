using Microservice.Api;
using Microservice.Core.Modularity;

namespace Microservice.MovieService;

[DependsOn(typeof(MovieServiceApplicationModule))]
[DependsOn(typeof(MovieServiceInfrastructureModule))]
[DependsOn(typeof(ApiModule))]
public sealed class MovieServiceWebModule : StartupModule
{
}