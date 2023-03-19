using Microservice.Core.Modularity;
using Microservice.MovieService.Movies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService;

[DependsOn<DomainModule>]
public sealed class MovieServiceDomainModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddTransient<IMovieManager, MovieManager>();
    }
}