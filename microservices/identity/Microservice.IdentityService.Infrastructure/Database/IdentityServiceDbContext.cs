using Microservice.Database;
using Microservice.IdentityService.Identity;
using Microsoft.EntityFrameworkCore;

namespace Microservice.IdentityService.Database;

public class IdentityServiceDbContext : BaseDbContext<IdentityServiceDbContext>
{
    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public IdentityServiceDbContext(DbContextOptions<IdentityServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityServiceDbContext).Assembly);
    }
}
