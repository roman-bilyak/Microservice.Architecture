using Microsoft.EntityFrameworkCore;

namespace Microservice.Database;

public abstract class BaseDbContext<TDbContext> : DbContext
    where TDbContext : DbContext
{
    public BaseDbContext(DbContextOptions<TDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}