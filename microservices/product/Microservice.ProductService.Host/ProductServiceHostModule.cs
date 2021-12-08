using Microservice.AspNetCore;
using Microservice.Core.Modularity;
using Microservice.ProductService.Application;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
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
            options.DocInclusionPredicate((docName, description) => true);
        });
    }

    public override void Initialize(IServiceProvider serviceProvider)
    {
        base.Initialize(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();

        app.UseDeveloperExceptionPage();

        ApplicationPartManager applicationPartManager = serviceProvider.GetRequiredService<ApplicationPartManager>();
        applicationPartManager.ApplicationParts.Add(new AssemblyPart(typeof(ProductServiceApplicationModule).Assembly));

        app.UseRouting();
        app.UseEndpoints(x => x.MapDefaultControllerRoute());

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
            options.DefaultModelsExpandDepth(-1);
        });
    }
}