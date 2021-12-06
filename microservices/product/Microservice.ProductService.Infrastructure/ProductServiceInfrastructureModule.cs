using Microservice.Core.Modularity;
using Microservice.Infrastructure.Database;
using Microservice.Infrastructure.Database.EntityFrameworkCore;
using Microservice.ProductService.Domain;
using Microservice.ProductService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ProductService.Infrastructure;

public sealed class ProductServiceInfrastructureModule : Module
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddDbContext<ProductServiceDbContext>(options =>
        {
            options.UseInMemoryDatabase("ProductServiceDbContext");
        });

        services.AddTransient<IRepository<Product>, BaseRepository<ProductServiceDbContext, Product>>();
        services.AddTransient<IReadRepository<Product>, BaseRepository<ProductServiceDbContext, Product>>();
        services.AddTransient<IRepository<Product, Guid>, BaseRepository<ProductServiceDbContext, Product, Guid>>();
        services.AddTransient<IReadRepository<Product, Guid>, BaseRepository<ProductServiceDbContext, Product, Guid>>();
    }
}