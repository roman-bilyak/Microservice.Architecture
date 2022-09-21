using Microservice.Application.CQRS;
using Microservice.Core.Modularity;
using Microservice.MovieService.Movies;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService;

[DependsOn(typeof(MovieServiceApplicationContractsModule))]
[DependsOn(typeof(MovieServiceDomainModule))]
public sealed class MovieServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddCQRS(typeof(MovieServiceApplicationModule).Assembly);

        services.AddTransient<IMovieApplicationService, MovieApplicationService>();
    }
}