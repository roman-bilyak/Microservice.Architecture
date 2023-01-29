using Microservice.Core.Modularity;
using Microservice.CQRS;
using Microservice.MovieService.Movies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService;

[DependsOn<MovieServiceDomainModule>]
public sealed class MovieServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddTransient<IMoviesApplicationService, MoviesApplicationService>();

        services.AddCQRS(typeof(MovieServiceApplicationModule).Assembly);
    }
}