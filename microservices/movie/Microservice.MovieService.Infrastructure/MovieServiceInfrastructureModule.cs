using Microservice.Core.Database;
using Microservice.Core.Modularity;
using Microservice.Infrastructure.Database.EntityFrameworkCore;
using Microservice.MovieService.Database;
using Microservice.MovieService.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService;

public sealed class MovieServiceInfrastructureModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddDbContext<MovieServiceDbContext>(options =>
        {
            options.UseInMemoryDatabase(nameof(MovieServiceDbContext));
        });

        services.AddTransient<IRepository<Movie>, BaseRepository<MovieServiceDbContext, Movie>>();
        services.AddTransient<IReadRepository<Movie>, BaseRepository<MovieServiceDbContext, Movie>>();
        services.AddTransient<IRepository<Movie, Guid>, BaseRepository<MovieServiceDbContext, Movie, Guid>>();
        services.AddTransient<IReadRepository<Movie, Guid>, BaseRepository<MovieServiceDbContext, Movie, Guid>>();
    }
}