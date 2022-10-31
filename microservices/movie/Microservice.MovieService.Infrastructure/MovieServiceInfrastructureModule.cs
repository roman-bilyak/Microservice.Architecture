using Microservice.Database;
using Microservice.Core.Modularity;
using Microservice.MovieService.Database;
using Microservice.MovieService.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService;

[DependsOn(typeof(MovieServiceDomainModule))]
public sealed class MovieServiceInfrastructureModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddDbContext<MovieServiceDbContext>(options =>
        {
            options.UseInMemoryDatabase(nameof(MovieServiceDbContext));
        });

        services.AddTransient<IRepository<Movie>, BaseRepository<MovieServiceDbContext, Movie>>();
        services.AddTransient<IRepository<Movie, Guid>, BaseRepository<MovieServiceDbContext, Movie, Guid>>();
        services.AddTransient<IReadRepository<Movie>, BaseRepository<MovieServiceDbContext, Movie>>();
        services.AddTransient<IReadRepository<Movie, Guid>, BaseRepository<MovieServiceDbContext, Movie, Guid>>();
    }
}