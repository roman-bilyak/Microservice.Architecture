using IdentityServer4.AccessTokenValidation;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;

namespace Microservice.TestService;

public class TestServiceWebModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services
            .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://localhost:7111";
                options.ApiName = "test-service";
            });
        services.AddAuthorization();
    }

    public override void Configure(IServiceProvider serviceProvider)
    {
        base.Configure(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();

        app.UseDeveloperExceptionPage();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(x => x.MapDefaultControllerRoute());
    }
}