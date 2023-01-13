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

        services.RegisterFakeApplicationServices(typeof(IdentityService.Identity.IUserApplicationService).Assembly, "api/IS");
        services.RegisterFakeApplicationServices(typeof(MovieService.Movies.IMovieApplicationService).Assembly, "api/MS");
        services.RegisterFakeApplicationServices(typeof(PaymentService.Payment.IPaymentApplicationService).Assembly, "api/PS");
        services.RegisterFakeApplicationServices(typeof(ReviewService.Reviews.IReviewApplicationService).Assembly, "api/RS");
        services.RegisterFakeApplicationServices(typeof(TestService.Tests.ITestApplicationService).Assembly, "api/TS");

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
            options.AddSettings(typeof(ReviewService.Reviews.IReviewApplicationService).Assembly,
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
