using IdentityServer4.AccessTokenValidation;
using Microservice.Core.Modularity;
using Microservice.Infrastructure.AspNetCore;
using Microsoft.OpenApi.Models;

namespace Microservice.IdentityService;

public sealed class IdentityServiceWebModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services
            .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://localhost:7111";
                options.ApiName = "identity-service";
            });
        services.AddAuthorization();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity Service API", Version = "v1" });
            options.DocInclusionPredicate((docName, description) => true);

            options.AddSecurityDefinition(nameof(SecuritySchemeType.OAuth2),
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Scheme = IdentityServerAuthenticationDefaults.AuthenticationScheme,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:7111/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:7111/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "identity-service", "Identity Service API" }
                            }
                        }
                    }
                });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = nameof(SecuritySchemeType.OAuth2),
                            Type = ReferenceType.SecurityScheme,
                        }
                    },
                    new[]
                    {
                        "identity-service"
                    }
                }
            });
        });
    }

    public override void Initialize(IServiceProvider serviceProvider)
    {
        base.Initialize(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();

        app.UseDeveloperExceptionPage();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(x => x.MapDefaultControllerRoute());

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.EnableTryItOutByDefault();
            options.RoutePrefix = string.Empty;
            options.DefaultModelsExpandDepth(-1);
            options.DisplayRequestDuration();

            options.OAuthClientId("api_client");
            options.OAuthAppName("Identity Service API");
            options.OAuthScopes("identity-service");
            options.OAuthUsePkce();
        });
    }
}