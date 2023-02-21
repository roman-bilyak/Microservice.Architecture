using Microservice.Core.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Microservice.AspNetCore.Swagger;

[DependsOn<AspNetCoreModule>]
public class SwaggerModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddOptions<SwaggerOptions>()
            .Bind(configuration.GetSection(SwaggerOptions.Swagger))
            .ValidateDataAnnotations();

        SwaggerOptions swaggerOptions = services.BuildServiceProvider().GetOptions<SwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(swaggerOptions.Version,
                new OpenApiInfo
                {
                    Title = swaggerOptions.Title,
                    Version = swaggerOptions.Version
                });
            options.DocInclusionPredicate((docName, description) => true);
            options.TagActionsBy(x => new[] { x.GroupName });
            options.DocumentFilter<IgnorePathsFilter>();
            options.OperationFilter<InheritDocOperationFilter>();

            if (swaggerOptions.Security?.Flow is not null)
            {
                options.AddSecurityDefinition(swaggerOptions.Security.Name,
                    new OpenApiSecurityScheme
                    {
                        Type = swaggerOptions.Security.Type.GetValueOrDefault(),
                        Scheme = swaggerOptions.Security.Scheme,
                        Flows = GetOpenApiOAuthFlows(swaggerOptions.Security.Flow)
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = swaggerOptions.Security.Name,
                                Type = ReferenceType.SecurityScheme,
                            }
                        },
                        swaggerOptions.Security.Flow.Scopes?.Keys.ToArray() ?? Array.Empty<string>()
                    }
                });
            }
        });
    }

    public override void Configure(IServiceProvider serviceProvider)
    {
        base.Configure(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();

        SwaggerOptions swaggerOptions = serviceProvider.GetOptions<SwaggerOptions>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", swaggerOptions.Version);
            options.EnableTryItOutByDefault();
            options.RoutePrefix = string.Empty;
            options.DefaultModelsExpandDepth(-1);
            options.DisplayRequestDuration();

            if (swaggerOptions.Security?.Flow is not null)
            {
                options.OAuthClientId(swaggerOptions.Security.Flow.ClientId);
                options.OAuthAppName(swaggerOptions.Title);
                options.OAuthScopes(swaggerOptions.Security.Flow.Scopes?.Keys.ToArray() ?? Array.Empty<string>());
                if (swaggerOptions.Security.Flow.UsePkce ?? false)
                {
                    options.OAuthUsePkce();
                }
            }
        });
    }

    #region helper methods

    private static OpenApiOAuthFlows GetOpenApiOAuthFlows(SwaggerSecurityFlow flow)
    {
        OpenApiOAuthFlows result = new();
        switch (flow.GrantType)
        {
            case GrantTypes.AuthorizationCode:
                result.AuthorizationCode = GetOpenApiOAuthFlow(flow);
                break;
            case GrantTypes.ClientCredentials:
                result.ClientCredentials = GetOpenApiOAuthFlow(flow);
                break;
            case GrantTypes.Implicit:
                result.Implicit = GetOpenApiOAuthFlow(flow);
                break;
            case GrantTypes.Password:
                result.Password = GetOpenApiOAuthFlow(flow);
                break;
        };
        return result;
    }

    private static OpenApiOAuthFlow GetOpenApiOAuthFlow(SwaggerSecurityFlow flow)
    {
        return new OpenApiOAuthFlow
        {
            AuthorizationUrl = GetUri(flow.AuthorityUrl, flow.AuthorizationUrl),
            TokenUrl = GetUri(flow.AuthorityUrl, flow.TokenUrl),
            RefreshUrl = GetUri(flow.AuthorityUrl, flow.RefreshUrl),
            Scopes = flow.Scopes
        };
    }

    private static Uri? GetUri(string? baseUrl, string? segment)
    {
        if (string.IsNullOrEmpty(segment))
        {
            return null;
        }
        if (string.IsNullOrEmpty(baseUrl) || segment.StartsWith(baseUrl))
        {
            return new Uri(segment);
        }
        string url = string.Join("/", new[] { baseUrl.TrimEnd('/'), segment.Trim('/') });
        return new Uri(url);
    }

    #endregion
}