using Microservice.Core.Modularity;

namespace Microservice.MovieService;

[DependsOn<MovieServiceApplicationModule>]
[DependsOn<MovieServiceInfrastructureModule>]
internal sealed class MovieServiceTestsModule : StartupModule
{
}