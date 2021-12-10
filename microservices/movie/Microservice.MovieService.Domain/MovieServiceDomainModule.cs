using Microservice.Core.Modularity;
using Microservice.MovieService.Domain.MovieManagement;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService.Domain;

public sealed class MovieServiceDomainModule : Module
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddTransient<IMovieManager, MovieManager>();
    }
}