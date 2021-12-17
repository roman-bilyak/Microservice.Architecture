using Microservice.AspNetCore;
using Microservice.Core.Modularity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.OpenApi.Models;

namespace Microservice.MovieService;

public sealed class MovieServiceWebModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie Service API", Version = "v1" });
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