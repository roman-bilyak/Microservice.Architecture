using Microservice.Api;
using Microservice.Application;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;

namespace Microservice.MovieService;

[DependsOn(typeof(MovieServiceApplicationModule))]
[DependsOn(typeof(MovieServiceInfrastructureModule))]
[DependsOn(typeof(ApiModule))]
public sealed class MovieServiceWebModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(MovieServiceApplicationModule).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });
    }
}