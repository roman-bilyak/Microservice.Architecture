using Microservice.Database;
using Microservice.Core.Modularity;
using Microservice.MovieService.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Microservice.MovieService;

[DependsOn<MovieServiceDomainModule>]
public sealed class MovieServiceInfrastructureModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddDbContext<MovieServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("MovieServiceDb"));
        });

        services.AddTransient<IRepository<Movie>, BaseRepository<MovieServiceDbContext, Movie>>();
        services.AddTransient<IRepository<Movie, Guid>, BaseRepository<MovieServiceDbContext, Movie, Guid>>();
        services.AddTransient<IReadRepository<Movie>, BaseRepository<MovieServiceDbContext, Movie>>();
        services.AddTransient<IReadRepository<Movie, Guid>, BaseRepository<MovieServiceDbContext, Movie, Guid>>();
    }
}