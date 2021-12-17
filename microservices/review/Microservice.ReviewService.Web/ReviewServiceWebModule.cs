using Microservice.AspNetCore;
using Microservice.AspNetCore.Conventions;
using Microservice.Core.Modularity;
using Microsoft.OpenApi.Models;

namespace Microservice.ReviewService;

public sealed class ReviewServiceWebModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(IReviewModuleApplicationService).Assembly, 
                x => typeof(IReviewModuleApplicationService).IsAssignableFrom(x));
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Review Service API", Version = "v1" });
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
            options.RoutePrefix = string.Empty;
            options.DefaultModelsExpandDepth(-1);
        });

        app.UseRouting();
        app.UseEndpoints(x => x.MapDefaultControllerRoute());
    }
}