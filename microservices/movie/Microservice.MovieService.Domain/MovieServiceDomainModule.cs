using Microservice.Core.Modularity;
using Microservice.MovieService.Movies;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService;

public sealed class MovieServiceDomainModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddTransient<IMovieManager, MovieManager>();
    }
}