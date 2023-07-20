using Microservice.AspNetCore;
using Microservice.Core.Modularity;
using Microservice.IdentityService;
using Microservice.IdentityService.Identity;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;

namespace Microservice.AuthService;

[DependsOn<AspNetCoreModule>]
[DependsOn<IdentityServiceInfrastructureModule>]
public sealed class AuthServiceApiModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddIdentity<User, Role>()
            .AddDefaultTokenProviders();

        services.AddIdentityServer(options =>
            {
                options.IssuerUri = configuration.GetValue<string>("IdentityServer:IssuerUri");
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(configuration.GetSection("IdentityServer:ApiResources"))
            .AddInMemoryApiScopes(configuration.GetSection("IdentityServer:ApiScopes"))
            .AddInMemoryClients(configuration.GetSection("IdentityServer:Clients"))
            .AddAspNetIdentity<User>()
            .AddProfileService<ProfileService>()
            .AddDeveloperSigningCredential();//not recommended for production - you need to store your key material somewhere secure
    }

    public override void PreConfigure(IServiceProvider serviceProvider)
    {
        base.PreConfigure(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedProto
        });

        app.UseStaticFiles();
    }

    public override void Configure(IServiceProvider serviceProvider)
    {
        base.Configure(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();

        app.UseDeveloperExceptionPage();

        app.UseIdentityServer();
    }
}