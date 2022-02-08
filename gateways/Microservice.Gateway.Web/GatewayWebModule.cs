using Microservice.Core.Modularity;
using Microservice.Gateway.Swagger;
using Microservice.Infrastructure.AspNetCore;
using Microservice.MovieService;
using Microservice.ReviewService;
using Microservice.TestService;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Microservice.Gateway;

public sealed class GatewayWebModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://localhost:5001";
                options.ApiName = "Gateway";
                options.RequireHttpsMetadata = true;
            });

        services.RegisterFakeApplicationServices(typeof(MovieServiceApplicationContractsModule).Assembly, "api/MS");
        services.RegisterFakeApplicationServices(typeof(ReviewServiceApplicationContractsModule).Assembly, "api/RS");
        services.RegisterFakeApplicationServices(typeof(TestServiceApplicationContractsModule).Assembly, "api/TS");

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway API", Version = "v1" });
            options.DocumentFilter<HideOcelotControllersFilter>();
            options.DocInclusionPredicate((docName, description) => true);
        });

        services.AddOcelot();
    }

    public override void Initialize(IServiceProvider serviceProvider)
    {
        base.Initialize(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.EnableTryItOutByDefault();
            options.RoutePrefix = string.Empty;
            options.DefaultModelsExpandDepth(-1);
            options.DisplayRequestDuration();
        });

        app.MapWhen(
                ctx => !ctx.Request.Path.ToString().StartsWith("/api/"),
                app2 =>
                {
                    app2.UseEndpoints(x => x.MapDefaultControllerRoute());
                }
            );

        app.UseOcelot().Wait();
    }
}
