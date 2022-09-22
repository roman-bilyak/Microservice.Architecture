using Microservice.Api;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;
using Microservice.MovieService;
using Microservice.ReviewService;
using Microservice.TestService;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Microservice.Gateway;

[DependsOn(typeof(MovieServiceApplicationContractsModule))]
[DependsOn(typeof(ReviewServiceApplicationContractsModule))]
[DependsOn(typeof(TestServiceApplicationContractsModule))]
[DependsOn(typeof(ApiModule))]
public sealed class GatewayModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.RegisterFakeApplicationServices(typeof(MovieServiceApplicationContractsModule).Assembly, "api/MS");
        services.RegisterFakeApplicationServices(typeof(ReviewServiceApplicationContractsModule).Assembly, "api/RS");
        services.RegisterFakeApplicationServices(typeof(TestServiceApplicationContractsModule).Assembly, "api/TS");

        services.AddOcelot();
    }

    public override void Configure(IServiceProvider serviceProvider)
    {
        base.Configure(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();
        app.UseOcelot().Wait();
    }
}
