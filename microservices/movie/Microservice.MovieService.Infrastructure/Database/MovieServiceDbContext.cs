using Microservice.Infrastructure.Database.EntityFrameworkCore;
using Microservice.MovieService.MovieManagement;
using Microsoft.EntityFrameworkCore;

namespace Microservice.MovieService.Database;

internal class MovieServiceDbContext : BaseDbContext<MovieServiceDbContext>
{
    public DbSet<Movie> Movies { get; set; }

    public MovieServiceDbContext(DbContextOptions<MovieServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieServiceDbContext).Assembly);
    }
}