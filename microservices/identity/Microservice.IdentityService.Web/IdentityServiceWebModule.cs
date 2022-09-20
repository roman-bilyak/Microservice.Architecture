using IdentityServer4.AccessTokenValidation;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;

namespace Microservice.IdentityService;

public sealed class IdentityServiceWebModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        IdentityServerOptions identityServerOptions = Configuration
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