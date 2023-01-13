using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.Core.Modularity;
using Microservice.IdentityService;
using Microservice.IdentityService.Identity;
using Microsoft.AspNetCore.Identity;

namespace Microservice.IdentityServer;

[DependsOn<AspNetCoreModule>]
[DependsOn<IdentityServiceInfrastructureModule>]
public sealed class IdentityServerModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        IConfiguration configuration = services.GetImplementationInstance<IConfiguration>();

        services.AddIdentity<User, Role>()
            .AddDefaultTokenProviders();

        services.AddIdentityServer(options =>
            {
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
            .AddDeveloperSigningCredential();//not recommended for production - you need to store your key material somewhere secure
    }

    public override void Configure(IServiceProvider serviceProvider)
    {
        base.Configure(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();
        app.UseDeveloperExceptionPage();

        app.UseStaticFiles();

        app.UseIdentityServer();
        app.UseAuthorization();
    }
}