using Microservice.Api;
using Microservice.Application;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Microservice.Gateway;

[DependsOn<ApiModule>]
public sealed class GatewayModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.RegisterFakeApplicationServices(typeof(IdentityService.Identity.IUsersApplicationService).Assembly, "api/IS");
        services.RegisterFakeApplicationServices(typeof(MovieService.Movies.IMoviesApplicationService).Assembly, "api/MS");
        services.RegisterFakeApplicationServices(typeof(PaymentService.Payment.IPaymentsApplicationService).Assembly, "api/PS");
        services.RegisterFakeApplicationServices(typeof(ReviewService.Reviews.IMoviesApplicationService).Assembly, "api/RS");
        services.RegisterFakeApplicationServices(typeof(TestService.Tests.ITestsApplicationService).Assembly, "api/TS");

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(IdentityService.Identity.IUsersApplicationService).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(MovieService.Movies.IMoviesApplicationService).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(PaymentService.Payment.IPaymentsApplicationService).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(ReviewService.Reviews.IMoviesApplicationService).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(TestService.Tests.ITestsApplicationService).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });

        services.AddOcelot();
    }

    public override void Configure(IServiceProvider serviceProvider)
    {
        base.Configure(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();
        app.UseOcelot().Wait();
    }
}
