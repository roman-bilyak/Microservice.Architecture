using Microservice.Core.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ProductService.Domain;

public sealed class ProductServiceDomainModule : Module
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddTransient<IProductManager, ProductManager>();
    }
}