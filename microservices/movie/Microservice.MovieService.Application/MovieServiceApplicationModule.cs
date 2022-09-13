using Microservice.Application.CQRS;
using Microservice.Core.Modularity;
using Microservice.MovieService.Movies;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService;

public sealed class MovieServiceApplicationModule : BaseModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddCQRS(typeof(MovieServiceApplicationModule).Assembly);

        services.AddTransient<IMovieApplicationService, MovieApplicationService>();
    }
}