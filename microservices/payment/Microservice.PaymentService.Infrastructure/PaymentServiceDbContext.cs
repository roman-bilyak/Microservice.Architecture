using Microservice.Database;
using Microsoft.EntityFrameworkCore;

namespace Microservice.PaymentService;

internal class PaymentServiceDbContext : BaseDbContext<PaymentServiceDbContext>
{
    public PaymentServiceDbContext(DbContextOptions<PaymentServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentServiceDbContext).Assembly);
    }
}