using Microservice.Infrastructure.Database.EntityFrameworkCore;
using Microservice.ReviewService.Reviews;
using Microsoft.EntityFrameworkCore;

namespace Microservice.ReviewService.Database;

internal class ReviewServiceDbContext : BaseDbContext<ReviewServiceDbContext>
{
    public DbSet<Review> Reviews { get; set; }

    public ReviewServiceDbContext(DbContextOptions<ReviewServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReviewServiceDbContext).Assembly);
    }
}