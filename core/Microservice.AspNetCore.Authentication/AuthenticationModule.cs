using Microservice.Core;
using Microservice.Core.Modularity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.AspNetCore.Authentication;

[DependsOn(typeof(AspNetCoreModule))]
public sealed class AuthenticationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        IConfiguration configuration = services.GetImplementationInstance<IConfiguration>();
        AuthenticationOptions authenticationOptions = configuration.GetSection("Authentication").Get<AuthenticationOptions>();

        AuthenticationBuilder authenticationBuilder = services
            .AddAuthentication(authenticationOptions.Scheme ?? "Bearer");

        if (authenticationOptions.IdentityServer is not null)
        {
            authenticationBuilder.AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authenticationOptions.IdentityServer.Authority;
                    options.ApiName = authenticationOptions.IdentityServer.ApiName;
                });
        }
    }

    public override void Configure(IServiceProvider serviceProvider)
    {
        base.Configure(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();
        app.UseAuthentication();
    }
}