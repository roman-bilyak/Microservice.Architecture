using Microservice.Database;
using Microsoft.EntityFrameworkCore;

namespace Microservice.TestService;

internal class TestServiceDbContext : BaseDbContext<TestServiceDbContext>
{
    public TestServiceDbContext(DbContextOptions<TestServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestServiceDbContext).Assembly);
    }
}