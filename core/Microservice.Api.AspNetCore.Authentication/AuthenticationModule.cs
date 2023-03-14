using Microservice.Core.Modularity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Microservice.AspNetCore.Authentication;

[DependsOn<AspNetCoreModule>]
public sealed class AuthenticationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        AuthenticationOptions? authenticationOptions = configuration.GetSection("Authentication").Get<AuthenticationOptions>();

        AuthenticationBuilder authenticationBuilder = services
            .AddAuthentication(authenticationOptions?.Scheme ?? JwtBearerDefaults.AuthenticationScheme);

        if (authenticationOptions?.JwtBearer is not null)
        {
            authenticationBuilder
                .AddJwtBearer(authenticationOptions.Scheme ?? JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = authenticationOptions.JwtBearer.Authority;
                    options.Audience = authenticationOptions.JwtBearer.Audience;
                    options.RequireHttpsMetadata = authenticationOptions.JwtBearer.RequireHttpsMetadata ?? true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = authenticationOptions.JwtBearer.ValidIssuer ?? authenticationOptions.JwtBearer.Authority
                    };
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