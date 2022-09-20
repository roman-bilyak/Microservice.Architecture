using IdentityServer4.AccessTokenValidation;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;
using Microservice.MovieService;
using Microservice.ReviewService;
using Microservice.TestService;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Microservice.Gateway;

public sealed class GatewayModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://localhost:7111";
                options.ApiName = "gateway";
            });

        services.RegisterFakeApplicationServices(typeof(MovieServiceApplicationContractsModule).Assembly, "api/MS");
        services.RegisterFakeApplicationServices(typeof(ReviewServiceApplicationContractsModule).Assembly, "api/RS");
        services.RegisterFakeApplicationServices(typeof(TestServiceApplicationContractsModule).Assembly, "api/TS");

        services.AddOcelot();
    }

    public override void Configure(IServiceProvider serviceProvider)
    {
        base.Configure(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

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
