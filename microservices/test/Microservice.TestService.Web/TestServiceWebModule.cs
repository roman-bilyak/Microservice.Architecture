using Microservice.AspNetCore;
using Microservice.Core.Modularity;
using Microsoft.OpenApi.Models;

namespace Microservice.TestService;

public class TestServiceWebModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Test Service API", Version = "v1" });
            options.DocInclusionPredicate((docName, description) => true);
        });
    }

    public override void Initialize(IServiceProvider serviceProvider)
    {
        base.Initialize(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();

        app.UseDeveloperExceptionPage();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.EnableTryItOutByDefault();
            options.RoutePrefix = string.Empty;
            options.DefaultModelsExpandDepth(-1);
            options.DisplayRequestDuration();
        });

        app.UseRouting();
        app.UseEndpoints(x => x.MapDefaultControllerRoute());
    }
}