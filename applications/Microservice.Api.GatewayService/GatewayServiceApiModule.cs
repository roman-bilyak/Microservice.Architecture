using Microservice.Api;
using Microservice.Application;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Microservice.GatewayService;

[DependsOn<ApiModule>]
public sealed class GatewayServiceApiModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.RegisterFakeApplicationServices(typeof(IdentityService.Identity.IUserApplicationService).Assembly, "IS");
        services.RegisterFakeApplicationServices(typeof(MovieService.Movies.IMovieApplicationService).Assembly, "MS");
        services.RegisterFakeApplicationServices(typeof(PaymentService.Payment.IPaymentApplicationService).Assembly, "PS");
        services.RegisterFakeApplicationServices(typeof(ReviewService.Reviews.IMovieApplicationService).Assembly, "RS");
        services.RegisterFakeApplicationServices(typeof(TestService.Tests.ITestApplicationService).Assembly, "TS");

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(IdentityService.Identity.IUserApplicationService).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(MovieService.Movies.IMovieApplicationService).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(PaymentService.Payment.IPaymentApplicationService).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(ReviewService.Reviews.IMovieApplicationService).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(TestService.Tests.ITestApplicationService).Assembly,
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
