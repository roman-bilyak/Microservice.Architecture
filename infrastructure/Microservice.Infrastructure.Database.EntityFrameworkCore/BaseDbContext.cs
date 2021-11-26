using Microsoft.EntityFrameworkCore;

namespace Microservice.Infrastructure.Database.EntityFrameworkCore;

public abstract class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions<BaseDbContext> options)
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