using IdentityServer4.AccessTokenValidation;
using Microservice.Api;
using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.Core.Modularity;

namespace Microservice.IdentityService;

[DependsOn(typeof(IdentityServiceApplicationModule))]
[DependsOn(typeof(IdentityServiceInfrastructureModule))]
[DependsOn(typeof(ApiModule))]
public sealed class IdentityServiceWebModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        IConfiguration configuration = services.GetImplementationInstance<IConfiguration>();
        IdentityServerOptions identityServerOptions = configuration
            .GetSection("IdentityServer")
            .Get<IdentityServerOptions>();

        services
            .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = identityServerOptions.Authority;
                options.ApiName = identityServerOptions.ApiName;
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