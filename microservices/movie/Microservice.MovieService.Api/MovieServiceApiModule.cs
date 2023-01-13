using Microservice.Api;
using Microservice.Application;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;

namespace Microservice.MovieService;

[DependsOn<MovieServiceApplicationModule>]
[DependsOn<MovieServiceInfrastructureModule>]
[DependsOn<ApiModule>]
public sealed class MovieServiceApiModule : StartupModule
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