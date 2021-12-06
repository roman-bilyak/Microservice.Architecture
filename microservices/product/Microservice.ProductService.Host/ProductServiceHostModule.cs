using Microservice.AspNetCore;
using Microservice.Core.Modularity;
using Microsoft.OpenApi.Models;

namespace Microservice.ProductService.Host;

public sealed class ProductServiceHostModule : Module
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddControllers();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Service API", Version = "v1" });
        });
    }

    public override void Initialize(IServiceProvider serviceProvider)
    {
        base.Initialize(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();

        app.UseDeveloperExceptionPage();

        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });

        app.UseEndpoints(x => x.MapDefaultControllerRoute());
    }
}