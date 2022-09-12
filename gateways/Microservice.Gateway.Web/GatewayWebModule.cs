using IdentityServer4.AccessTokenValidation;
using Microservice.Core.Modularity;
using Microservice.Gateway.Swagger;
using Microservice.Infrastructure.AspNetCore;
using Microservice.MovieService;
using Microservice.ReviewService;
using Microservice.TestService;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Microservice.Gateway;

public sealed class GatewayWebModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://localhost:9101";
                options.ApiName = "gateway";
            });

        services.RegisterFakeApplicationServices(typeof(MovieServiceApplicationContractsModule).Assembly, "api/MS");
        services.RegisterFakeApplicationServices(typeof(ReviewServiceApplicationContractsModule).Assembly, "api/RS");
        services.RegisterFakeApplicationServices(typeof(TestServiceApplicationContractsModule).Assembly, "api/TS");

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway API", Version = "v1" });
            options.DocumentFilter<HideOcelotControllersFilter>();
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
                            AuthorizationUrl = new Uri("https://localhost:9101/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:9101/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "identity-service", "Identity Service API" },
                                { "movie-service", "Movie Service API" },
                                { "payment-service", "Payment Service API" },
                                { "review-service", "Review Service API" },
                                { "test-service", "Test Service API" }
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
                    new [] 
                    {
                        "identity-service",
                        "movie-service",
                        "payment-service",
                        "review-service",
                        "test-service"
                    }
                }
            });
        });

        services.AddOcelot();
    }

    public override void Initialize(IServiceProvider serviceProvider)
    {
        base.Initialize(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.EnableTryItOutByDefault();
            options.RoutePrefix = string.Empty;
            options.DefaultModelsExpandDepth(-1);
            options.DisplayRequestDuration();

            options.OAuthClientId("api_client");
            options.OAuthAppName("Gateway API");
            options.OAuthScopes
            (
                "identity-service",
                "movie-service",
                "payment-service",
                "review-service",
                "test-service"
            );
            options.OAuthUsePkce();
        });

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
