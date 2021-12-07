using Microservice.Core.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ProductService.Application;

public sealed class ProductServiceApplicationModule : Module
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddTransient<IProductAppService, ProductAppService>();
    }
}
