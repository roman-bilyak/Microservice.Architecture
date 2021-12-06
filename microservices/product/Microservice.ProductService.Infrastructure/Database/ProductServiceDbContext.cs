using Microservice.Infrastructure.Database.EntityFrameworkCore;
using Microservice.ProductService.Domain;
using Microsoft.EntityFrameworkCore;

namespace Microservice.ProductService.Infrastructure.Database;

internal class ProductServiceDbContext : BaseDbContext<ProductServiceDbContext>
{
    public DbSet<Product> Products { get; set; }

    public ProductServiceDbContext(DbContextOptions<ProductServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductServiceDbContext).Assembly);
    }
}