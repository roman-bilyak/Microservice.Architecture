using Microservice.Core.Modularity;
using Microservice.MovieService.MovieManagement;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService;

public sealed class MovieServiceApplicationModule : Module
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddTransient<IMovieApplicationService, MovieApplicationService>();
    }
}
