using Microservice.Core.Modularity;
using Microservice.Infrastructure.Database;
using Microservice.Infrastructure.Database.EntityFrameworkCore;
using Microservice.MovieService.Domain;
using Microservice.MovieService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService.Infrastructure;

public sealed class MovieServiceInfrastructureModule : Module
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddDbContext<MovieServiceDbContext>(options =>
        {
            options.UseInMemoryDatabase("MovieServiceDbContext");
        });

        services.AddTransient<IRepository<Movie>, BaseRepository<MovieServiceDbContext, Movie>>();
        services.AddTransient<IReadRepository<Movie>, BaseRepository<MovieServiceDbContext, Movie>>();
        services.AddTransient<IRepository<Movie, Guid>, BaseRepository<MovieServiceDbContext, Movie, Guid>>();
        services.AddTransient<IReadRepository<Movie, Guid>, BaseRepository<MovieServiceDbContext, Movie, Guid>>();
    }
}