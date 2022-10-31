using Microservice.Core.Modularity;
using Microservice.CQRS;
using Microservice.MovieService.Movies;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService;

[DependsOn(typeof(MovieServiceDomainModule))]
public sealed class MovieServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddTransient<IMovieApplicationService, MovieApplicationService>();

        services.AddCQRS(typeof(MovieServiceApplicationModule).Assembly);
    }
}