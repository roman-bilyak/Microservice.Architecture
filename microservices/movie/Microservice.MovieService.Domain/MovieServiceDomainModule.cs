using Microservice.Core.Modularity;
using Microservice.MovieService.Movies;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService;

public sealed class MovieServiceDomainModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddTransient<IMovieManager, MovieManager>();
    }
}